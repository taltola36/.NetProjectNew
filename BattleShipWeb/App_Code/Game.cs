//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//public class Game
//{
//    private Player p1, p2;

//    public Game(string guid)
//    {
//        p1 = new Player(guid);
//    }

//    public void addPlayer(string guid)
//    {
//        this.p2 = new Player(guid);
//    }

//    public Board getBoardOfPlayer(string guid)
//    {
//        if (p1.getGuid().Equals(guid))
//            return p1.GetBoard();
//        else
//            return p2.GetBoard();
//    }

//    public Board createNewBoard(string guid)
//    {
//        if (p1.getGuid().Equals(guid))
//            return p1.createNewBoard();
//        return p2.createNewBoard();
//    }

//    public Player getPlayer(int num)
//    {
//        if (num == 1)
//            return p1;
//        return p2;
//    }

//    public void removePlayer(int num)
//    {
//        if (num == 1)
//            p1 = null;
//        else
//            p2 = null;
//    }

//    public int getNumberofPlayers()
//    {
//        if (p1 == null && p2 == null)
//            return 0;
//        if (p1 != null && p2 != null)
//            return 2;
//        return 1;
//    }
//}