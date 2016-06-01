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
        string otherPlayerId = GameManager.getOtherPlayerID(playerId);        
        GameManager.RemoveClient(playerId);
        string resultStr = myJavaScriptSerializer.Serialize("The other player left. Game over. New Game?");
        updateOtherPlayer(resultStr, otherPlayerId, 1, _clientStateList2);

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
        int count = 0;
        lock (_lock)
        {
            foreach (AsyncResult clientState1 in _clientStateList1.Values)
            {
                count++;
                if (count == _clientStateList2.Count + 1)
                {
                    state.ClientGuid = playerId;
                    _clientStateList2.Add(playerId, state);
                }
            }
        }
    }

    public static void LoadBoard(AsyncResult state, string playerId, string playerNumber, string username)
    {
        Board b = GameManager.LoadBoard(playerId, playerNumber, username);
        string numberOfShips1 = "", numberOfShips2 = "";
        BoardData bd = null, bd1 = null;
        string resultStr;
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();        
        Game game = GameManager.getGameOfPlayer(playerId);

        if (game.Player1 != null && game.Player1.ID.Equals(playerId))   //if its the first player only send board
        {
            if (GameManager.hasBeenClosed(playerId))
            {
                numberOfShips2 = GameManager.getNumberOfShips(game.Player2.ID, "2");
                bd = new BoardData(b.BoardArray, numberOfShips2, numberOfShips2, !GameManager.hasBeenClosed(playerId));

                numberOfShips1 = GameManager.getNumberOfShips(game.Player1.ID, "1");
                bd1 = new BoardData(b.BoardArray, numberOfShips1, numberOfShips1, !GameManager.hasBeenClosed(playerId));
                
                resultStr = myJavaScriptSerializer.Serialize(bd1);
                updatePlayerMove(resultStr, game.Player2.ID);
                resultStr = myJavaScriptSerializer.Serialize(bd);
            }
            else
            {
                bd = new BoardData(b.BoardArray, "", "", !GameManager.hasBeenClosed(playerId));                            
            }
        }
        if (game.Player2 != null && game.Player2.ID.Equals(playerId))   //if its the second player, send board + remaining ships
        {
            numberOfShips1 = GameManager.getNumberOfShips(game.Player1.ID, "1");
            bd = new BoardData(b.BoardArray, numberOfShips1, numberOfShips1, !GameManager.hasBeenClosed(playerId));

            numberOfShips2 = GameManager.getNumberOfShips(game.Player2.ID, "2");
            bd1 = new BoardData(null, numberOfShips2, numberOfShips2, !GameManager.hasBeenClosed(playerId));
            resultStr = myJavaScriptSerializer.Serialize(bd1);
            updatePlayerMove(resultStr, game.Player1.ID);
            resultStr = myJavaScriptSerializer.Serialize(bd);
        }
        resultStr = myJavaScriptSerializer.Serialize(bd);
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
            Game game = GameManager.getGameOfPlayer(playerId);
            string otherPlayerId;
            if (game.Player1.ID.Equals(playerId))
                otherPlayerId = game.Player2.ID;
            else
                otherPlayerId = game.Player1.ID;

            //numberOfOtherPlayer = getOtherPlayerIndex(numberOfCurrentPlayer);

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