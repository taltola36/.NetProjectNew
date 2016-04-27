using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBConnection
/// </summary>
public class DBConnection
{
	public DBConnection()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string GetStructerBoard(out string id, out string structure)
    {
        string retStruct = "";
        string connectionString;

        id = null;
        structure = null;

        connectionString = "server=TALBENAMI\\SQLEXPRESS;" + "uid=root;" +
                           "pwd=root; database=.NetProject";
        SqlConnection con = new SqlConnection(connectionString);
        string sql = "select * from Board";
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            id = dr["Board_ID"].ToString();
            structure = dr["Structure_Board"].ToString();
            retStruct = dr["Structure_Board"].ToString();
        }
        return retStruct;
    }
}