using System;
using System.Activities;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using BattleShipModel;

public class GameManager
{
    private static volatile bool _registered = false;
    private static List<Game> _arr = new List<Game>();
    private const int Size = 10;

    public GameManager()
    {

    }

    public static locResult RegisterClient(string playerId)
    {
        int pairNumber = -1, i = 0;
        locResult result;
        _registered = false;
        if (_arr == null)
            _arr = new List<Game>();

        if (_arr.Count != 0)
        {
            for (i = 0; i < _arr.Count; i++)
            {
                if (_arr[i] != null && _arr[i].NumberOfPlayers == 1)
                {
                    pairNumber = i + 1;
                    if (_arr[i].Player1 != null)
                        _registered = false;
                    if (_arr[i].Player2 != null)
                        _registered = true;
                    break;
                }
                if (_arr[i] == null)
                {
                    _registered = true;
                    pairNumber = i + 1;
                }
            }
        }
        else
        {
            pairNumber = 1;
            _registered = true;
        }

        if (pairNumber == -1)
        {
            pairNumber = i + 1;
            _registered = true;
        }

        if (_registered)
            result = new locResult(playerId, "firstPlayer", (pairNumber).ToString(), "");
        else
            result = new locResult(playerId, "secondPlayer", (pairNumber).ToString(), "");
        
        return result;
    }

    public static void RemovePlayer(string playerId)
    {
        for (int i = 0; i < _arr.Count; i++)
        {
            if (_arr[i] != null && _arr[i].Player1 != null && _arr[i].Player1.ID.Equals(playerId))
            {
                if (_arr[i].Player2 == null)
                    _arr[i] = null;
                else
                {
                    _arr[i].Player1 = null;
                    _registered = !_registered;
                    _arr[i].hasBeenClosed = true;
                    _arr[i].playTurn = -1;
                }
                break;
            }
            if (_arr[i] != null && _arr[i].Player2 != null && _arr[i].Player2.ID.Equals(playerId))
            {
                if (_arr[i].Player1 == null)
                    _arr[i] = null;
                else
                {
                    _arr[i].Player2 = null;
                    _registered = !_registered;
                    _arr[i].hasBeenClosed = true;
                    _arr[i].playTurn = -1;
                }
                break;
            }
        }
        if (isEmpty())
            _arr = null;
    }

    public static bool isEmpty()
    {
        for (int i = 0; i < _arr.Count; i++)
            if (_arr[i] != null)                
                return false;
        return true;
    }
    
    public static bool hasBeenClosed(string playerId)
    {
        Game game = getGameOfPlayer(playerId);
        if (game != null)        
            return game.hasBeenClosed;
        return false;
    }

    public static int getFirstPlayer(string playerId)
    {
        int getFirstPlayer = 0;
        Game game = getGameOfPlayer(playerId);
        Player currentPlayer = null, otherPlayer = null;

        if (game != null && game.Player1 != null && game.Player2 != null)
        {
            currentPlayer = game.Player1;
            otherPlayer = game.Player2;

            if (game.Player2.ID.Equals(playerId))
                getFirstPlayer = 1;
        }

        if (currentPlayer != null && otherPlayer != null)
        {
            if ((currentPlayer.numberOfHits == otherPlayer.numberOfSubmarines &&
                 otherPlayer.numberOfSubmarines != 0) ||
                (otherPlayer.numberOfHits == currentPlayer.numberOfSubmarines &&
                 currentPlayer.numberOfSubmarines != 0))
                game.playTurn = getFirstPlayer;
            else
            {
                game.playTurn = 1 - getFirstPlayer;
                getFirstPlayer = game.playTurn;
            }
        }

        else if (game != null && SinglePlayer() == 1)
            game.playTurn = getFirstPlayer = 1;
        if (game == null)
        {
            if (SinglePlayer() == 1) 
                getFirstPlayer = 1;
            if (SinglePlayer() == 0)
                getFirstPlayer = 0;
        }

        return getFirstPlayer;
    }

    public static int SinglePlayer()
    {
        for (int i = 0; i < _arr.Count; i++)
        {
            if (_arr[i] != null && _arr[i].NumberOfPlayers == 1 && _arr[i].Player2 != null)
                return 1;
            if (_arr[i] != null && _arr[i].NumberOfPlayers == 1 && _arr[i].Player1 != null)
                return 0;
        }
        return -1;
    }
        
    public static Board LoadBoard(string playerId, string playerNumber, string username)
    {
        Board board = DBManager.GetNewBoard();
        if (_arr.Count != 0)
        {
            for (int i = 0; i < _arr.Count; i++)
            {
                if (_arr[i] == null)
                {
                    _arr.Remove(_arr[i]);
                    CreateNewGame(playerId, playerNumber, board, username);
                    _arr[i].playTurn = 0;
                    _arr[i].ID = i.ToString();
                    break;
                }

                //there is a game already with 1 player
                if (_arr[i].NumberOfPlayers == 1 && _arr[i].Player1 == null)
                {
                    Player player1 = new Player(playerId);
                    player1.Board = board;
                    player1.userName = username;
                    _arr[i].Player1 = player1;
                    _arr[i].playTurn = 1;
                    break;
                }
                if (_arr[i].NumberOfPlayers == 1 && _arr[i].Player2 == null)
                {
                    Player player2 = new Player(playerId);
                    player2.Board = board;
                    player2.userName = username;
                    _arr[i].Player2 = player2;
                    _arr[i].playTurn = 0;
                    break;
                }

                //this is the last game with 2 players. need to create a new game with 1 player
                if (_arr[i].NumberOfPlayers == 2 && _arr.Count - i == 1)
                {
                    CreateNewGame(playerId, playerNumber, board, username);
                    _arr[i + 1].playTurn = 0;
                    _arr[i + 1].ID = (i + 1).ToString();
                    break;
                }
                if (_arr[i].NumberOfPlayers == 0)
                {
                    Player player1 = new Player(playerId);
                    player1.Board = board;
                    player1.userName = username;
                    _arr[i].Player1 = player1;
                    break;
                }
            }
        }

        if (_arr.Count == 0) //first Game with player1
        {
            CreateNewGame(playerId, playerNumber, board, username);
            _arr[0].playTurn = 0;
            _arr[0].ID = "0";
        }

        return board;
    }

    public static void CreateNewGame(string playerId, string playerNumber, Board board, string username)
    {
        Game game = new Game();
        Player player1 = new Player(playerId);
        player1.userName = username;
        player1.Board = board;
        game.Player1 = player1;
        _arr.Add(game);
    }

    public static Board ChangeBoard(string playerId)
    {
        Board board = DBManager.GetNewBoard();
        for (int i = 0; i < _arr.Count; i++)
        {
            if (_arr[i] != null)
            {
                if (_arr[i].Player1 != null && _arr[i].Player1.ID.Equals(playerId))
                {
                    _arr[i].Player1.Board = board;
                    _arr[i].Player1.numberOfHits = 0;
                    _arr[i].Player1.numberOfSubmarines = 0;
                    break;
                }
                if (_arr[i].Player2 != null && _arr[i].Player2.ID.Equals(playerId))
                {
                    _arr[i].Player2.Board = board;
                    _arr[i].Player2.numberOfHits = 0;
                    _arr[i].Player2.numberOfSubmarines = 0;
                    break;
                }
            }
        }
        return board;
    }

    public static locResult MakeMove(string playerId, string indexes)
    {
        locResult result = null;

        int k = Int32.Parse(indexes) / Size;
        int l = Int32.Parse(indexes) - k * Size;

        for (int i = 0; i < _arr.Count; i++)
        {
            if (_arr[i].NumberOfPlayers == 2 && _arr[i].Player1.ID.Equals(playerId))
            {
                result = CalcHit(_arr[i].Player1, _arr[i].Player2, _arr[i], k, l, indexes, 0);
                _arr[i].hasBeenClosed = false;
                break;
            }

            if (_arr[i].NumberOfPlayers == 2 && _arr[i].Player2.ID.Equals(playerId))
            {
                result = CalcHit(_arr[i].Player2, _arr[i].Player1, _arr[i], k, l, indexes, 1);
                _arr[i].hasBeenClosed = false;
                break;
            }
            if (_arr[i].NumberOfPlayers == 1)
                result = new locResult("join", "", "", "");
        }
        return result;
    }

    public static locResult CalcHit(Player currentPlayer, Player otherPlayer, Game game, int k, int l, string indexes, int turn)
    {
        locResult result = null;

        if (game.playTurn == turn)
        {
            string isHit = otherPlayer.Board.BoardArray[k][l].ToString();
            if (isHit.Equals("1"))
            {
                currentPlayer.numberOfHits++;
                DBManager.WriteMove(currentPlayer.userName, Int32.Parse(indexes), true);
            }
            if (isHit.Equals("0"))
                DBManager.WriteMove(currentPlayer.userName, Int32.Parse(indexes), false);

            changeTurn(isHit, game);
            string subsLeft = (otherPlayer.numberOfSubmarines - currentPlayer.numberOfHits).ToString();
            
            bool hasWon = checkEndGame(isHit, currentPlayer, otherPlayer);
            if (hasWon)
            {
                result = new locResult(indexes, "w", isHit, subsLeft); //game over
                
                DBManager.AddPlayerData(currentPlayer.userName, true);
                DBManager.AddPlayerData(otherPlayer.userName, false);
            }
            else
                result = new locResult(indexes, "r", isHit, subsLeft); //game not over
        }
        else
            result = new locResult("turn", "", "", "");
        return result;
    }

    public static void changeTurn(string isHit, Game game)
    {
        if (isHit.Equals("0"))
            game.playTurn = 1 - game.playTurn;
    }

    public static bool checkEndGame(string isHit, Player currentPlayer, Player otherPlayer)
    {
        if (currentPlayer.numberOfHits.Equals(otherPlayer.numberOfSubmarines))
            return true;
        return false;
    }

    public static bool playerExists(string playerId)
    {
        if (_arr != null)
        {
            for (int i = 0; i < _arr.Count; i++)
            {
                if ((_arr[i] != null && _arr[i].Player1 != null && _arr[i].Player1.ID.Equals(playerId)) || 
                    (_arr[i] != null && _arr[i].Player2 != null && _arr[i].Player2.ID.Equals(playerId)))
                    return true;
            }
        }
       
        return false;
    }

    public static string getOtherPlayerID(string playerId)
    {
        for (int i = 0; i < _arr.Count; i++)
        {
            if (_arr[i] != null && _arr[i].Player1 != null && _arr[i].Player1.ID.Equals(playerId))
                if (_arr[i].Player2 != null)
                    return _arr[i].Player2.ID;

            if (_arr[i] != null && _arr[i].Player2 != null && _arr[i].Player2.ID.Equals(playerId))
                if (_arr[i].Player1 != null)                
                    return _arr[i].Player1.ID;
        }
        return "";
    }

    public static Game getGameOfPlayer(string playerId)
    {
        for (int i = 0; i < _arr.Count; i++)
        {
            if (_arr[i] != null && ((_arr[i].Player1 != null && _arr[i].Player1.ID.Equals(playerId)) || 
                (_arr[i].Player2 != null && _arr[i].Player2.ID.Equals(playerId))))
                return _arr[i];
        }
        return null;
    }

    public static List<BoardData> getEnemyShipsData(string playerId, int firstToConnect, Board b)
    {
        List<BoardData> resutList= new List<BoardData>();
        Game game = getGameOfPlayer(playerId);
        Player currentPlayer = null, otherPlayer = null;
        string playerNumber="", otherPlayerNumber="";

        if (game == null)
            return resutList;

        if (game.Player1 != null && game.Player1.ID.Equals(playerId))
        {
            currentPlayer = game.Player1;
            otherPlayer = game.Player2;
            playerNumber = "1";
            otherPlayerNumber = "2";
        }
        if (game.Player2 != null && game.Player2.ID.Equals(playerId))
        {
            currentPlayer = game.Player2;
            otherPlayer = game.Player1;
            playerNumber = "2";
            otherPlayerNumber = "1";
        }
        
        if (otherPlayer != null && (hasBeenClosed(playerId) ||
            (playerNumber.Equals("2") && firstToConnect == 0) ||
            (playerNumber.Equals("1") && firstToConnect == 1)))
        {
            string numberOfOtherShips = getNumberOfShips(otherPlayer.ID, otherPlayerNumber, firstToConnect);
            resutList.Add(new BoardData(currentPlayer.Board.BoardArray, numberOfOtherShips, numberOfOtherShips, hasBeenClosed(playerId), firstToConnect));

            string numberOfCurrentShips = getNumberOfShips(currentPlayer.ID, playerNumber, firstToConnect);
            resutList.Add(new BoardData(null, numberOfCurrentShips, numberOfCurrentShips, !hasBeenClosed(playerId), firstToConnect));
        }
        else
        {
            bool wait;
            if (otherPlayer != null)
                wait = !hasBeenClosed(playerId);
            else
                wait = true;
            resutList.Add(new BoardData(b.BoardArray, "", "", wait, firstToConnect));
            game.hasBeenClosed = false;
        }
            
        return resutList;
    }

    public static string getNumberOfShips(string playerId, string playerNumber, int firstToConnect)
    {
        int[][] board_arr;
        string numberOfShips = "";
        Player currentPlayer = null;
        
        for (int i = 0; i < _arr.Count; i++)
        {
            if (_arr[i] != null && _arr[i].NumberOfPlayers == 2 &&
                (_arr[i].Player1.ID.Equals(playerId) || _arr[i].Player2.ID.Equals(playerId)))
            {
                if (playerNumber.Equals("1"))
                    currentPlayer = _arr[i].Player1;

                if (playerNumber.Equals("2"))
                    currentPlayer = _arr[i].Player2;
                if (currentPlayer != null) {
                    if (currentPlayer.numberOfSubmarines == 0)
                    {
                        board_arr = currentPlayer.Board.BoardArray;
                        numberOfShips = calculateNumberOfShips(currentPlayer, board_arr);
                    }
                    else
                        numberOfShips = currentPlayer.numberOfSubmarines.ToString();
                }
                break;
            }

            if (_arr[i].NumberOfPlayers == 1 && _arr[i].Player1.ID.Equals(playerId))
            {
                currentPlayer = _arr[i].Player1;
                board_arr = currentPlayer.Board.BoardArray;
                numberOfShips = calculateNumberOfShips(currentPlayer, board_arr);
                break;
            }
        }
        return numberOfShips;
    }

    public static string calculateNumberOfShips(Player currentPlayer, int[][] board_arr)
    {
        if (currentPlayer != null)
        {
            for (int j = 0; j < Size; j++)
                for (int k = 0; k < Size; k++)
                    if (board_arr[j][k] == 1)
                        currentPlayer.numberOfSubmarines++;
            return currentPlayer.numberOfSubmarines.ToString();
        }
        return "";
    }
}