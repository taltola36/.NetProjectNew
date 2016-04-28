using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Player
{
    private Board myBoard;
    private string myID;
	
    public Player(string GuID)
    {
        createNewBoard();
        this.myID = GuID;
    }

    public string getGuid()
    {
        return myID;
    }

    public Board GetBoard()
    {
        return myBoard;
    }

    public Board createNewBoard()
    {
        myBoard = new Board();
        return myBoard;
    }
}