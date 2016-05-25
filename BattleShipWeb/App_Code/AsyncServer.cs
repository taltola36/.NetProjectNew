using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BattleShipModel;
using System.Web.SessionState;

public class AsyncServer
{
    private static Object _lock = new Object();
    private static Dictionary<string, AsyncResult> _clientStateList1 = new Dictionary<string, AsyncResult>();
    private static Dictionary<string, AsyncResult> _clientStateList2 = new Dictionary<string, AsyncResult>();
    
    public static void RegisterClient(AsyncResult state)
    {
        lock (_lock)
        {
            state.ClientGuid = Guid.NewGuid().ToString();
            _clientStateList1.Add(state.ClientGuid, state);
            //_clientStateList2.Add(state.ClientGuid, state);
        }

        int numberOfOtherPlayer = 0, numberOfCurrentPlayer;
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        locResult result = GameManager.RegisterClient(state.ClientGuid);

        if (result.boardName.Equals("secondPlayer"))
        {
            //return answer to first player with loadBoardProcess
            numberOfCurrentPlayer = getRegisteredPlayerIndex(state);
            numberOfOtherPlayer = getOtherPlayerIndex(numberOfCurrentPlayer);
            updateOtherPlayer("", numberOfOtherPlayer, 0, _clientStateList1);
        }

        string resultStr = myJavaScriptSerializer.Serialize(result);
        state._context.Response.Write(resultStr);
    }

    public static int getRegisteredPlayerIndex(AsyncResult state)
    {
        int numberOfOtherPlayer = 0;
        lock (_lock)
        {
            foreach (AsyncResult clientState in _clientStateList1.Values)
            {
                numberOfOtherPlayer++;
                if (clientState._context.CurrentHandler != null && state.ClientGuid != null && state.ClientGuid.Equals(clientState.ClientGuid))
                    break;
            }
        }
        return numberOfOtherPlayer;
    }

    public static void UnregisterClient(AsyncResult state, String playerId)
    {
        string resultStr;
        int numberOfCurrentPlayer, numberOfOtherPlayer;
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        
        GameManager.RemoveClient(playerId);
        resultStr = myJavaScriptSerializer.Serialize("The other player left. Game over. New Game?");
        numberOfCurrentPlayer = getPlayerIndex(playerId);
        numberOfOtherPlayer = getOtherPlayerIndex(numberOfCurrentPlayer);
        updateOtherPlayer(resultStr, numberOfOtherPlayer, 1, _clientStateList2);

        lock (_lock)
        {
            _clientStateList1.Remove(playerId);
            _clientStateList2.Remove(playerId);
        }
    }

    public static int getPlayerIndex(string playerId)
    {
        int numberOfCurrentPlayer = 0;
        lock (_lock)
        {
            foreach (AsyncResult clientState in _clientStateList2.Values)
            {
                numberOfCurrentPlayer++;
                if (playerId.Equals(clientState.ClientGuid))
                {
                    clientState.CompleteRequest();
                    break;
                }
            }
        }
        return numberOfCurrentPlayer;
    }

    public static void UpdateClient1(AsyncResult state, String playerId)
    {
        lock (_lock)
        {
            AsyncResult clientState = null;
            _clientStateList1.TryGetValue(playerId, out clientState);
            if (clientState != null)
            {
                clientState._context = state._context;
                clientState._state = state._state;
                clientState._callback = state._callback;
            }
        }
    }

    public static void UpdateClient2(AsyncResult state, String playerId)
    {
        lock (_lock)
        {
            //AsyncResult clientState = null;

            //_clientStateList2.TryGetValue(playerId, out clientState);
            //if (clientState != null)
            //{
            //    clientState._context = state._context;
            //    clientState._state = state._state;
            //    clientState._callback = state._callback;
            //}

            _clientStateList2.Add(playerId, state);
        }
    }

    public static void LoadBoard(AsyncResult state, string playerId, string playerNumber)
    {
        Board b = GameManager.LoadBoard(playerId, playerNumber);
        string numberOfShips = GameManager.getNumberOfShips(playerId, playerNumber);
        BoardData bd;

        if (GameManager.hasBeenClosed(playerId))
            bd = new BoardData(b.BoardArray, numberOfShips, numberOfShips, false);
        else
            bd = new BoardData(b.BoardArray, numberOfShips, numberOfShips, true);

        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr = myJavaScriptSerializer.Serialize(bd);
        state._context.Response.Write(resultStr);
        
    }

    public static int getOtherPlayerIndex(int numberOfCurrentPlayer)
    {
        int numberOfOtherPlayer;

        if (numberOfCurrentPlayer % 2 == 0)
            numberOfOtherPlayer = numberOfCurrentPlayer - 1;
        else
            numberOfOtherPlayer = numberOfCurrentPlayer + 1;
        
        return numberOfOtherPlayer;
    }

    public static void MakeMove(AsyncResult state, string playerId, string indexes)
    {
        int numberOfCurrentPlayer = 0, numberOfOtherPlayer;
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr, otherClientGuid;

        locResult result = GameManager.MakeMove(playerId, indexes);

        if (result == null) //It is not this player's turn
        {
            resultStr = myJavaScriptSerializer.Serialize("Please wait for your turn");
            state._context.Response.Write(resultStr);
            updatePlayerMove(resultStr, playerId);  //only completeRequest
        }

        if (result != null)
        {
            if (result.boardName.Equals("w"))   //prepare data to the wininng player
                result.isHit = "Game over! You won";

            resultStr = myJavaScriptSerializer.Serialize(result);
            state._context.Response.Write(resultStr); //send data to xmlHttp_MakeMove
            numberOfCurrentPlayer = updatePlayerMove(resultStr, playerId); //send data to xmlHttp_MakeMoveProcess
            numberOfOtherPlayer = getOtherPlayerIndex(numberOfCurrentPlayer);

            if (result.boardName.Equals("w"))   //prepare data to the losing player
                result.isHit = "Game over! You lost";

            if (result.boardName.Equals("r"))   //prepare data to the player who didnt click
            {
                result.boardName = "l";
                result.submarinesLeft = "";
            }
            //send data to the player who didn't clicked
            resultStr = myJavaScriptSerializer.Serialize(result);
            updateOtherPlayer(resultStr, numberOfOtherPlayer, 1, _clientStateList1);
        }
    }

    public static int updatePlayerMove(string resultStr, string playerId)
    {
        int numberOfCurrentPlayer = 0;
        lock (_lock)
        {
            foreach (AsyncResult clientState in _clientStateList1.Values)
            {
                numberOfCurrentPlayer++;
                if (playerId.Equals(clientState.ClientGuid))
                {
                    clientState._context.Response.Write(resultStr);
                    clientState.CompleteRequest();
                    break;
                }
            }
        }
        return numberOfCurrentPlayer;
    }

    public static void updateOtherPlayer(string resultStr, int numberOfOtherPlayer, int sendData, Dictionary<string, AsyncResult> list)
    {
        //sendData = 0 only completeRequest for xmlHttp_ProcessLoadBoard for the first player who waited for second to connect
        //sendData = 1 send button data for player who didn't make the move
        lock (_lock)
        {
            foreach (AsyncResult clientState in list.Values)
            {
                numberOfOtherPlayer--;
                if (numberOfOtherPlayer == 0)
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