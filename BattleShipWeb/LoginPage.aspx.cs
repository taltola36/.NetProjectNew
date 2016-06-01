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
    private const string connectionString = "server=AVITALHOVAV\\SQLEXPRESS;" +
                                            "uid=root;" +
                                            "pwd=root; database=.NetProject";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserToken"] != null && Session["refreshed"] == null)
            Response.Redirect("http://localhost:54968/Default.aspx");
    }

    protected void Button_Login_Click(object sender, EventArgs e)
    {
        Session["UserName"] = TextBoxUserName.Text;
        Session["UserName2"] = TextBoxUserName.Text;
        Session["UserToken"] = Guid.NewGuid().ToString();

        string url = "http://localhost:54968/Default.aspx";

        Response.Redirect(url);
    }
}