using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Script.Serialization;
using BattleShipModel;

public class AsyncServer
{
    private static Object _lock = new Object();
    private static Dictionary<string, AsyncResult> _clientStateList = new Dictionary<string, AsyncResult>();

    public static void RegisterClient(AsyncResult state)
    {
        lock (_lock)
        {
            state.ClientGuid = Guid.NewGuid().ToString();
            _clientStateList.Add(state.ClientGuid, state);
        }
        int numberOfPlayer1 = 0, numberOfPlayer2;
        
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();

        //resylt is guid and labels info (number of pair and player)
        locResult result = GameManager.RegisterClient(state.ClientGuid);

        if (result.boardName.Equals("secondPlayer"))
        {
            numberOfPlayer2 = getSecondPlayerIndex(state);
            if (numberOfPlayer2 % 2 == 0)
                numberOfPlayer1 = numberOfPlayer2 - 1;
            else
                numberOfPlayer1 = numberOfPlayer2 + 1;
            
            updateOtherPlayer("", numberOfPlayer1, 0);
        }

        string resultStr = myJavaScriptSerializer.Serialize(result);
        state._context.Response.Write(resultStr);
    }

    public static int getSecondPlayerIndex(AsyncResult state)
    {
        int numberOfPlayer2 = 0;
        lock (_lock)
        {
            foreach (AsyncResult clientState in _clientStateList.Values)
            {
                numberOfPlayer2++;
                if (clientState._context.CurrentHandler != null && state.ClientGuid != null && state.ClientGuid.Equals(clientState.ClientGuid))
                    break;
            }
        }
        return numberOfPlayer2;
    }

    public static void UnregisterClient(AsyncResult state, String guid)
    {
        string resultStr;
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        //GameManager.RemoveClient(guid);
        //List<Game> l = GameManager.retArr();
        //make sure that the remaining player gets a message to wait(like in register) and to activate loadBoard

        //resultStr = myJavaScriptSerializer.Serialize("play");
        //updateWaitingClient(state, resultStr);
        //state._context.Response.Write(resultStr);
        //resultStr = myJavaScriptSerializer.Serialize("Please wait for another player to join the game");

        //lock (_lock)
        //{
        //    foreach (AsyncResult clientState in _clientStateList.Values)
        //    {
        //        if (!guid.Equals(clientState.ClientGuid))
        //        {
        //            clientState._context.Response.Write(resultStr);
        //            clientState.CompleteRequest();
        //            break;
        //        }
        //    }
        //}
        lock (_lock)
        {
            _clientStateList.Remove(guid);
        }
    }

    public static void UpdateClient(AsyncResult state, String guid)
    {
        lock (_lock)
        {
            AsyncResult clientState = null;
            _clientStateList.TryGetValue(guid, out clientState);
            if (clientState != null)
            {
                clientState._context = state._context;
                clientState._state = state._state;
                clientState._callback = state._callback;
            }
        }
    }

    public static void LoadBoard(AsyncResult state, string playerId, string playerNumber)
    {
        Board b = GameManager.LoadBoard(playerId, playerNumber);

        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr = myJavaScriptSerializer.Serialize(b.BoardArray);
        state._context.Response.Write(resultStr);
        
        //updateWaitingClient(state, resultStr);
    }

    //public static void updateWaitingClient(AsyncResult state, string resultStr)
    //{
    //    lock (_lock)
    //    {
    //        foreach (AsyncResult clientState in _clientStateList.Values)
    //        {
    //            if (clientState._context.CurrentHandler != null && state.ClientGuid != null && state.ClientGuid.Equals(clientState.ClientGuid))
    //            {
    //                clientState._context.Response.Write(resultStr);
    //                clientState.CompleteRequest();
    //            }
    //        }
    //    }
    //}

    public static void MakeMove(AsyncResult state, string playerId, string indexes)
    {
        int numberOfPlayer1 = 0, numberOfPlayer2;
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr;

        locResult result = GameManager.MakeMove(playerId, indexes);

        if (result == null) //It is not this player's turn
        {
            resultStr = myJavaScriptSerializer.Serialize("Please wait for your turn");
            state._context.Response.Write(resultStr);

            //complete request for client that it is not his turn
            updatePlayerMove(resultStr, playerId);
        }
        if (result != null)
        {
            if (result.boardName.Equals("w"))
                result.isHit = "Game over! You won";
            resultStr = myJavaScriptSerializer.Serialize(result);
            state._context.Response.Write(resultStr);   //send data to xmlHttp_MakeMove
            numberOfPlayer1 = updatePlayerMove(resultStr, playerId);    //send data to xmlHttp_MakeMoveProcess

            if (numberOfPlayer1 % 2 == 0)
                numberOfPlayer2 = numberOfPlayer1 - 1;
            else
                numberOfPlayer2 = numberOfPlayer1 + 1;

            //prepare data to the player who didn't clicked
            if (result.boardName.Equals("w"))
                result.isHit = "Game over! You lost";
            if (result.boardName.Equals("r"))
                result.boardName = "l";

            //send data to the player who didn't clicked
            resultStr = myJavaScriptSerializer.Serialize(result);
            updateOtherPlayer(resultStr, numberOfPlayer2, 1);
        }
    }

    public static int updatePlayerMove(string resultStr, string playerId)
    {
        int numberOfPlayer1 = 0;
        lock (_lock)
        {
            foreach (AsyncResult clientState in _clientStateList.Values)
            {
                numberOfPlayer1++;
                if (playerId.Equals(clientState.ClientGuid))
                {
                    clientState._context.Response.Write(resultStr);
                    clientState.CompleteRequest();
                    break;
                }
            }
        }
        return numberOfPlayer1;
    }

    public static void updateOtherPlayer(string resultStr, int numberOfPlayer2, int sendData)
    {
        //sendData = 0 only completeRequest for xmlHttp_ProcessLoadBoard for the first player who waited for second to connect
        //sendData = 1 send button data for player who didn't make the move

        int count = 0;
        lock (_lock)
        {
            foreach (AsyncResult clientState in _clientStateList.Values)
            {
                count++;
                if (count == numberOfPlayer2)
                {
                    if (sendData == 1)
                        clientState._context.Response.Write(resultStr);
                    clientState.CompleteRequest();
                    break;
                }
            }
        }
    }

}