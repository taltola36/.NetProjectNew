using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Player
{
    private Board myBoard = new Board();
    private string myID;
	
    public Player(string GuID)
    {
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
}