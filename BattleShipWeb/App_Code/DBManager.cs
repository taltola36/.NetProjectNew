using System;
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
        string retStruct = "";

        SqlConnection con = new SqlConnection(connectionString);
        string sql = "select * from Board";
        con.Open();

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            retStruct = dr["Structure_Board"].ToString();
        }

        return retStruct;
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
}