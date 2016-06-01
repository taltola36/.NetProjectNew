using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using BattleShipModel;

public class DBManager
{
    private const string connectionString = "server=TALBENAMI\\SQLEXPRESS;" +
                                            "uid=root;" +
                                            "pwd=root; database=.NetProject";

    private static string GetRandStructerBoard()
    {

        ArrayList structures = new ArrayList();
        string retStruct = "";
        int rows = 0;

        SqlConnection con = new SqlConnection(connectionString);
        string sql = "select * from Board";
        con.Open();

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            rows++;
            structures.Add(dr["Structure_Board"].ToString());
        }

        Random rndNum = new Random();
        int num;
        num = rndNum.Next(0, rows);
        string[] structuresArr = new string[rows];

        for (int i = 0; i < structures.Count; i++)
        {
            structuresArr[i] = (string)structures[i];
        }

        return structuresArr[num];
    }

    public static void ExecuteQuery(string sql)
    {

        SqlConnection con = new SqlConnection(connectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();

    }

    public static Board GetNewBoard()
    {
        string structer = GetRandStructerBoard();
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        int[][] structArr = (int[][])myJavaScriptSerializer.Deserialize(structer, typeof(int[][]));

        Board retBoard = new Board();
        retBoard.BoardArray = structArr;

        return retBoard;
    }

    public static void WriteMove(string player, int indexHit, bool hit)
    {
        SqlConnection con = new SqlConnection(connectionString);
        string sql = "Insert into Move(Player, IndexHit, Hit) Values('" + player + "','" + indexHit + "','" + hit.ToString() + "')";
        con.Open();

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
    }

    public static void AddPlayerData(string player, bool isWin)
    {
        bool playerExist = false;
        SqlConnection con = new SqlConnection(connectionString);
        string sql = "Select * from Player";
        con.Open();

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            if (player.Equals(dr["UserName"]))
            {
                playerExist = true;
            }
        }
        con.Close();
        int numOfVictories = 0;
        int numOfGames = 0;

        if (playerExist)
        {
            sql = "Select * from Player where UserName ='" + player + "'";
            con.Open();

            cmd = new SqlCommand(sql, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                numOfGames = int.Parse(dr["Number_Of_Games"].ToString());
                numOfGames++;
                if (isWin)
                {
                    numOfVictories = int.Parse(dr["Number_Of_Victories"].ToString());
                    numOfVictories++;
                }
            }
            con.Close();

            sql = "Update Player Set Number_Of_Games="+ numOfGames + " , Number_Of_Victories=" + numOfVictories + " where UserName='" + player + "'";
            con.Open();

            cmd = new SqlCommand(sql, con);
            dr = cmd.ExecuteReader();
            con.Close();
        }
        else // add new player
        {
            if (isWin)
            {
                numOfVictories = 1;
            }
            sql = "Insert Into Player(UserName, Number_Of_Games, Number_Of_Victories) values('"+ player +"', 1," + numOfVictories + ")";
            con.Open();

            cmd = new SqlCommand(sql, con);
            dr = cmd.ExecuteReader();
            con.Close();

        }
    }
}