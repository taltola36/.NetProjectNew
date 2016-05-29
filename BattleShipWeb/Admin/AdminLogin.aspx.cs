using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin : System.Web.UI.Page
{
    private const string connectionString = "server=TALBENAMI\\SQLEXPRESS;" +
                                            "uid=root;" +
                                            "pwd=root; database=.NetProject";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminToken"] != null)
            Response.Redirect("http://localhost:54968/Admin/AdminOptions.aspx");
    }

    protected void Button_Login_Click(object sender, EventArgs e)
    {
        bool adminExist = false;
        SqlConnection con = new SqlConnection(connectionString);
        string sql = "select * from Admin_Table";
        con.Open();

        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            if (dr["Admin_User_Name"].ToString().Equals(TextBoxUserName.Text) && dr["Admin_Password"].ToString().Equals(TextBoxPassword.Text))
            {
                adminExist = true;
            }
        }

        if (!adminExist)
        {
            Response.Write("Username or Password are incorrect");
        }
        else
        {
            Session["adminToken"] = Guid.NewGuid().ToString();
            
            string url = "http://localhost:54968/Admin/AdminOptions.aspx";
            if (Request["urlback"] != null)
            {
                url = Request["urlback"];
            }

            Response.Redirect(url);
        }
    }
}