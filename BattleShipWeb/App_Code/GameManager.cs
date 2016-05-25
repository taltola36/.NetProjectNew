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
    private static volatile bool registered = false;
    private static List<Game> arr = new List<Game>();
    private static int size = 10;

    public GameManager()
    {

    }

    public static locResult RegisterClient(string playerId)
    {
        int pairNumber = -1, i = 0;
        locResult result;
        registered = false;
        if (arr == null)
            arr = new List<Game>();

        if (arr.Count != 0)
        {
            for (i = 0; i < arr.Count; i++)
            {
                if (arr[i] == null || arr[i].NumberOfPlayers < 2)
                    pairNumber = i + 1;
                if (arr[i] == null || arr[i].NumberOfPlayers == 0 || arr[i].NumberOfPlayers == 2)
                    registered = true;
                else
                {
                    if (arr[i].Player1 != null)
                        registered = false;
                    if (arr[i].Player2 != null)
                        registered = true;
                }
            }
        }
        else
        {
            pairNumber = 1;
            registered = true;
        }
        if (pairNumber == -1)
            pairNumber = i+1;
        //result is guid and labels info (number of pair and player)
        if (registered)
            result = new locResult(playerId, "firstPlayer", (pairNumber).ToString(), "");
        else
            result = new locResult(playerId, "secondPlayer", (pairNumber).ToString(), "");
        
        return result;
    }

    public static void RemoveClient(string playerId)
    {
        bool isEmpty = false;

        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i].Player1 != null && arr[i].Player1.ID.Equals(playerId))
            {
                if (arr[i].Player2 == null)
                    arr[i] = null;
                else
                {
                    arr[i].Player1 = null;
                    registered = !registered;
                    arr[i].hasBeenClosed = true;
                }
                break;
            }
            if (arr[i].Player2 != null && arr[i].Player2.ID.Equals(playerId))
            {
                if (arr[i].Player1 == null)
                    arr[i] = null;
                else
                {
                    arr[i].Player2 = null;
                    registered = !registered;
                    arr[i].hasBeenClosed = true;
                }
                
                break;
            }
        }
        if (isEmpty = GameManager.isEmpty())
            arr = null;
    }

    public static bool isEmpty()
    {
        for (int i = 0; i < arr.Count; i++)
            if (arr[i] != null)                
                return false;
        return true;
    }

    public static bool hasBeenClosed(string playerId)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i].Player1.ID.Equals(playerId))
                return arr[i].hasBeenClosed;
            if (arr[i].Player2.ID.Equals(playerId))
                return arr[i].hasBeenClosed;
        }
        return false;
    }

    public static int getNumberofGames()
    {
        return arr.Count;
    }

    public static Board LoadBoard(string playerId, string playerNumber)
    {
        Board board = DBManager.GetNewBoard();
        
        if (arr.Count != 0)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] == null)
                {
                    arr.Remove(arr[i]);
                    CreateNewGame(playerId, playerNumber, board);
                    arr[i].playTurn = 0;
                    arr[i].ID = i.ToString();
                    break;
                }

                //there is a game already with 1 player
                if (arr[i].NumberOfPlayers == 1 && arr[i].Player1 == null)
                {
                    Player player1 = new Player(playerId);
                    player1.Board = board;
                    arr[i].Player1 = player1;
                    break;
                }
                if (arr[i].NumberOfPlayers == 1 && arr[i].Player2 == null)
                {
                    Player player2 = new Player(playerId);
                    player2.Board = board;
                    arr[i].Player2 = player2;
                    break;
                }

                //this is the last game with 2 players. need to create a new game with 1 player
                if (arr[i].NumberOfPlayers == 2 && arr.Count - i == 1)
                {
                    CreateNewGame(playerId, playerNumber, board);
                    arr[i + 1].playTurn = 0;
                    arr[i + 1].ID = (i + 1).ToString();
                    break;
                }
                if (arr[i].NumberOfPlayers == 0)
                {
                    Player player1 = new Player(playerId);
                    player1.Board = board;
                    arr[i].Player1 = player1;
                    break;
                }
            }
        }

        if (arr.Count == 0) //first Game with player1
        {
            CreateNewGame(playerId, playerNumber, board);
            arr[0].playTurn = 0;
            arr[0].ID = "0";
        }

        return board;
    }

    public static void CreateNewGame(string playerId, string playerNumber, Board board)
    {
        Game game = new Game();
        Player player1 = new Player(playerId);
        player1.Board = board;
        game.Player1 = player1;
        arr.Add(game);
    }

    public static locResult CalcMakeMove(Player currentPlayer, Player otherPlayer, Game game, int k, int l, string indexes, int turn)
    {
        string isHit= "";
        locResult result = null;
        bool hasWon = false;
        string subsLeft = "";

        if (game.playTurn == turn)
        {
            isHit = currentPlayer.Board.BoardArray[k][l].ToString();
            if (isHit.Equals("1"))
            {
                currentPlayer.numberOfHits++;
                DBManager.WriteMove(currentPlayer.ID, Int32.Parse(indexes), true);
            }
            if (isHit.Equals("0"))
                DBManager.WriteMove(currentPlayer.ID, Int32.Parse(indexes), false);

            changeTurn(isHit, game);
            subsLeft = (otherPlayer.numberOfSubmarines - currentPlayer.numberOfHits).ToString();
            
            hasWon = checkEndGame(isHit, currentPlayer, otherPlayer);
            if (hasWon)
            {
                result = new locResult(indexes, "w", isHit, subsLeft); //game over
                DBManager.AddPlayerData(currentPlayer.ID, true);
                DBManager.AddPlayerData(otherPlayer.ID, false);
            }
            else
                result = new locResult(indexes, "r", isHit, subsLeft); //game not over
        }
        return result;
    }

    public static locResult MakeMove(string playerId, string indexes)
    {
        int k, l, i;
        locResult result = null;

        k = Int32.Parse(indexes) / size;
        l = Int32.Parse(indexes) - k * size;
        
        for (i = 0; i < arr.Count; i++)
        {
            if (arr[i].NumberOfPlayers == 2 && arr[i].Player1.ID.Equals(playerId))
            {
                result = CalcMakeMove(arr[i].Player1, arr[i].Player2, arr[i], k, l, indexes, 0);
                break;
            }

            if (arr[i].NumberOfPlayers == 2 && arr[i].Player2.ID.Equals(playerId))
            {
                result = CalcMakeMove(arr[i].Player2, arr[i].Player1, arr[i], k, l, indexes, 1);               
                break;
            }
        }
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

    public static string getNumberOfShips(string playerId, string playerNumber)
    {
        int[][] boardArr = null;
        string numberOfShips = "";
        Player currentPlayer = null;
        
        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i] != null && arr[i].NumberOfPlayers == 2 && (arr[i].Player1.ID.Equals(playerId) || arr[i].Player2.ID.Equals(playerId)))
            {
                if (playerNumber.Equals("1"))
                    currentPlayer = arr[i].Player1;
                    
                if (playerNumber.Equals("2"))
                    currentPlayer = arr[i].Player2;
                    
                boardArr = currentPlayer.Board.BoardArray;
                numberOfShips = calculateNumberOfShips(currentPlayer, boardArr);
            }

            if (arr[i].NumberOfPlayers == 1 && arr[i].Player1.ID.Equals(playerId))
            {
                currentPlayer = arr[i].Player1;
                boardArr = currentPlayer.Board.BoardArray;
                numberOfShips = calculateNumberOfShips(currentPlayer, boardArr);
            }
        }
        return numberOfShips;
    }

    public static string calculateNumberOfShips(Player currentPlayer, int[][] boardArr)
    {
        if (currentPlayer != null)
            for (int j = 0; j < size; j++)
                for (int k = 0; k < size; k++)
                    if (boardArr[j][k] == 1)
                        currentPlayer.numberOfSubmarines++;
        return currentPlayer.numberOfSubmarines.ToString();
    }
}