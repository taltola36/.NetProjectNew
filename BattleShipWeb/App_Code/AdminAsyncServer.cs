using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public class AdminAsyncServer
{
    private const string connectionString = "server=TALBENAMI\\SQLEXPRESS;" +
                                        "uid=root;" +
                                        "pwd=root; database=.NetProject";

    public static void addBoard(AsyncResult state, string stringBoard)
    {

        //'[[0,0,0,1,1,1,0,0,0,1],[1,0,0,0,0,0,0,1,0,1],[0,0,0,1,1,0,0,1,0,1],[0,1,0,0,0,0,0,1,0,0],[0,1,0,0,0,0,0,1,0,0],[0,0,0,1,1,1,0,0,0,1],[0,0,0,1,1,1,0,0,0,1],[1,0,0,1,0,0,0,0,0,1],[1,0,0,1,0,0,0,0,0,0],[0,0,0,1,0,0,0,1,0,0]]';


        SqlConnection con = new SqlConnection(connectionString);
        string sql = "Insert into Board(Structure_Board) Values('" + stringBoard + "')";
        con.Open();

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();

        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr = myJavaScriptSerializer.Serialize("Board added successfully");

        state._context.Response.Write(resultStr);
        //Response.Write("Board added successfully");

        //Server.Transfer("addedSuccessfully.aspx", true);

    }
}
