using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminAdd : System.Web.UI.Page
{
    private Button add = new Button();

    private const string connectionString = "server=TALBENAMI\\SQLEXPRESS;" +
                                        "uid=root;" +
                                        "pwd=root; database=.NetProject";

    protected void Page_Load(object sender, EventArgs e)
    {
        ButtonAddBoard.Attributes.Add("onclick", "buttonAddClick(this);return false");
        if (!IsLoggedIn())
        {
            var url = Request.Url.LocalPath;
            Response.Redirect("AdminLogin.aspx?urlback=" + url);
        }
    }

    private bool IsLoggedIn()
    {
        if (Session["adminToken"] != null)
        {
            return true;
        }

        return false;
    }


    protected void ButtonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://localhost:54968/Admin/AdminOptions.aspx");
    }
}