using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminDelete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string query = "DELETE FROM Board WHERE Board_ID =";
        string boardId = GridView1.Rows[e.RowIndex].Cells[0].Text;
        
        query += boardId;
        DBManager.ExecuteQuery(query);
    }
}