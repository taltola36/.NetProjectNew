using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Game
{
    private Player p1, p2;

    public Game(string guid)
    {
        p1 = new Player(guid);
    }

    public void addPlayer(string guid)
    {
        this.p2 = new Player(guid);
    }

    public Board getBoardOfPlayer(string guid)
    {
        if (p1.getGuid().Equals(guid))
            return p1.GetBoard();
        else
            return p2.GetBoard();
    }
}