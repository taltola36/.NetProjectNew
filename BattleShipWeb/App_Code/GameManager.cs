using System;
using System.Activities;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
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
        int pairNumber = arr.Count;
        locResult result;
        registered = !registered;

        //result is guid and labels info (number of pair and player)
        if (registered)
            result = new locResult(playerId, "firstPlayer", (pairNumber+1).ToString(), "");
        else
            result = new locResult(playerId, "secondPlayer", (pairNumber).ToString(), "");
        
        return result;
    }

    public static void RemoveClient(string playerId)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i].Player1.ID.Equals(playerId))
            {
                arr[i].Player1 = null;
                break;
            }
            if (arr[i].Player2.ID.Equals(playerId))
            {
                arr[i].Player2 = null;
                registered = !registered;
                break;
            }
        }
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
                //there is a game already with 1 player
                if (arr[i].NumberOfPlayers == 1 && playerNumber.Equals("1"))
                {
                    Player player1 = new Player(playerId);
                    player1.Board = board;
                    arr[i].Player1 = player1;
                    break;
                }
                if (arr[i].NumberOfPlayers == 1 && playerNumber.Equals("2"))
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
        if (playerNumber.Equals("1"))
        {
            Player player1 = new Player(playerId);
            player1.Board = board;
            game.Player1 = player1;
        }
        if (playerNumber.Equals("2"))
        {
            Player player2 = new Player(playerId);
            player2.Board = board;
            game.Player2 = player2;
        }
        arr.Add(game);
    }

    public static locResult MakeMove(string playerId, string indexes)
    {
        int k, l, i;
        locResult result = null;
        string isHit = "";
        bool hasWon = false;
        string subsLeft = "";

        k = Int32.Parse(indexes) / size;
        l = Int32.Parse(indexes) - k * size;
        
        for (i = 0; i < arr.Count; i++)
        {
            if (arr[i].NumberOfPlayers == 2 && arr[i].Player1.ID.Equals(playerId))
            {
                if (arr[i].playTurn == 0)
                {
                    isHit = arr[i].Player1.Board.BoardArray[k][l].ToString();
                    if (isHit.Equals("1"))
                        arr[i].Player1.numberOfHits++;
                    changeTurn(isHit, arr[i]);
                    subsLeft = (arr[i].Player2.numberOfSubmarines - arr[i].Player1.numberOfHits).ToString();
                    hasWon = checkEndGame(isHit, arr[i].Player1, arr[i].Player2);
                }
            }
            if (arr[i].NumberOfPlayers == 2 && arr[i].Player2.ID.Equals(playerId))
            {
                if (arr[i].playTurn == 1)
                {
                    isHit = arr[i].Player2.Board.BoardArray[k][l].ToString();
                    if (isHit.Equals("1"))
                        arr[i].Player2.numberOfHits++;
                    
                    changeTurn(isHit, arr[i]);
                    subsLeft = (arr[i].Player1.numberOfSubmarines - arr[i].Player2.numberOfHits).ToString();
                    hasWon = checkEndGame(isHit, arr[i].Player2, arr[i].Player1);
                }
            }
        }

        if (isHit != "")
        {
            if (!hasWon)
                result = new locResult(indexes, "r", isHit, subsLeft);    //game not over
            else
                result = new locResult(indexes, "w", isHit, subsLeft);    //game over
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
            if (arr[i].NumberOfPlayers == 2 && (arr[i].Player1.ID.Equals(playerId) || arr[i].Player2.ID.Equals(playerId)))
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