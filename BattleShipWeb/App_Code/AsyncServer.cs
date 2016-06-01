using System;
using System.Activities.Debugger;
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
        }

        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        locResult result = GameManager.RegisterClient(state.ClientGuid);

        string resultStr = myJavaScriptSerializer.Serialize(result);
        state._context.Response.Write(resultStr);
    }

    public static void UnregisterClient(AsyncResult state, String playerId)
    {
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        if (_clientStateList2.Count != 0 && GameManager.playerExists(playerId))
        {
            string otherPlayerId = GameManager.getOtherPlayerID(playerId);
            GameManager.RemovePlayer(playerId);
            string resultStr = myJavaScriptSerializer.Serialize("The other player left. Game over. New Game?");
            updateOtherPlayer(resultStr, otherPlayerId, 1, _clientStateList2);

            lock (_lock)
            {
                _clientStateList1.Remove(playerId);
                _clientStateList2.Remove(playerId);
            }
        }
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
        bool update = true;
       
        lock (_lock) {
            foreach (AsyncResult clientState in _clientStateList2.Values)       //check if it already exists in list2
            {
                if (clientState.ClientGuid.Equals(playerId))
                {
                    update = false;
                    clientState._state = state;
                    clientState.ClientGuid = playerId;
                }
            }
        }

        if (update)     //if not then add for the first time
        {
            state.ClientGuid = playerId;
            _clientStateList2.Add(playerId, state);     
        }
    }

    public static void LoadBoard(AsyncResult state, string playerId, string playerNumber, string loadOrChange, string username)
    {
        int firstToConnect = GameManager.getFirstPlayer(playerId);
        string resultStr = "";
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();

        Board b = null;
        if (loadOrChange.Equals("load"))
            b = GameManager.LoadBoard(playerId, playerNumber, username);
        if (loadOrChange.Equals("change"))
            b = GameManager.ChangeBoard(playerId);

        List<BoardData> boardDataList = GameManager.getEnemyShipsData(playerId, firstToConnect, b);

        if (boardDataList.Count == 2)
        {
            resultStr = myJavaScriptSerializer.Serialize(boardDataList[1]);
            updatePlayerMove(resultStr, GameManager.getOtherPlayerID(playerId));
            resultStr = myJavaScriptSerializer.Serialize(boardDataList[0]);
        }
        if (boardDataList.Count == 1)
            resultStr = myJavaScriptSerializer.Serialize(boardDataList[0]);
        state._context.Response.Write(resultStr);
    }

    public static void MakeMove(AsyncResult state, string playerId, string indexes)
    {
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr="", otherClientGuid;

        locResult result = GameManager.MakeMove(playerId, indexes);

        if (result.boardName.Equals("")) //wait for turn ot for another to join
        {
            if (result.indexes.Equals("join"))
                resultStr = myJavaScriptSerializer.Serialize("Please wait for another player to join the game");
            if (result.indexes.Equals("turn"))
                resultStr = myJavaScriptSerializer.Serialize("Please wait for your turn");
           
            state._context.Response.Write(resultStr);
            updatePlayerMove(resultStr, playerId);  //only completeRequest
        }

        if (!result.boardName.Equals(""))
        {
            if (result.boardName.Equals("w"))   //prepare data to the wininng player
                result.isHit = "Game over! You won";

            resultStr = myJavaScriptSerializer.Serialize(result);
            state._context.Response.Write(resultStr); //send data to xmlHttp_MakeMove
            updatePlayerMove(resultStr, playerId); //send data to xmlHttp_MakeMoveProcess
            Game game = GameManager.getGameOfPlayer(playerId);
            
            string otherPlayerId;
            if (game.Player1.ID.Equals(playerId))
                otherPlayerId = game.Player2.ID;
            else
                otherPlayerId = game.Player1.ID;

            if (result.boardName.Equals("w"))   //prepare data to the losing player
                result.isHit = "Game over! You lost";

            if (result.boardName.Equals("r"))   //prepare data to the player who didnt click
            {
                result.boardName = "l";
                result.submarinesLeft = "";
            }
            //send data to the player who didn't clicked
            resultStr = myJavaScriptSerializer.Serialize(result);
            updateOtherPlayer(resultStr, otherPlayerId, 1, _clientStateList1);
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

    public static void updateOtherPlayer(string resultStr, string otherPlayerId, int sendData, Dictionary<string, AsyncResult> list)
    {
        lock (_lock)
        {
            foreach (AsyncResult clientState in list.Values)
            {
                if (clientState.ClientGuid.Equals(otherPlayerId))
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