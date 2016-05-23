using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminOptions : System.Web.UI.Page
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

}