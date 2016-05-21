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
    private const string connectionString = "server=AVITALHOVAV\\SQLEXPRESS;" +
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
            structuresArr[i] = (string) structures[i];
        }

        return structuresArr[num] ;
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

    public void WriteMove(string player, int indexHit, bool hit)
    {
        
    }

    public void AddPlayerData(string player, bool isWin)
    {
        
    }
}