using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BattleShipModel;

/// <summary>
/// Summary description for GameManager
/// </summary>
public class GameManager
{
    private static volatile bool wait = false, registered = false;
    private static List<Game> arr = new List<Game>();

	public GameManager()
	{
		
	}

    public static Board LoadBoard(string playerId)
    {
        wait = !wait;

        // create new random board
        Board board = DBManager.GetNewBoard();

        if (arr.Count != 0 && arr[0].NumberOfPlayers == 2) // why only arr[0]?? what about more games in the array??
        {
            // ????? why setting again player2 board
            Player player;
            if (arr[0].Player1.ID == playerId)
            {
                player = arr[0].Player1;
            }
            else
            {
                player = arr[0].Player2; // WHAT IF ID ISN'T EQUAL???
            }
            player.Board = board;
        }
        else
        {
            if (wait)
            {
                // create first player
                Player player1 = new Player(playerId);
                player1.Board = board;

                // create game with first player
                Game game = new Game();
                game.Player1 = player1;

                // add game to games list
                arr.Add(game);
            }
            else
            {
                // create second player for the game
                Player player2 = new Player(playerId);
                player2.Board = board;
                arr[0].Player2 = player2;
            }
        }

        return board;
    }
}