using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_addedSuccessfully : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ButtonAddMore_Click(object sender, EventArgs e)
    {
        Server.Transfer("AdminAdd.aspx", true);
    }
    protected void ButtonBack_Click(object sender, EventArgs e)
    {
        Server.Transfer("AdminOptions.aspx", true);
    }
}