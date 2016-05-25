using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ThemesUC : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private void Page_Init(object sender, System.EventArgs e)
    {
        this.Page.Init += UC_Init;
    }

    private void UC_Init(object sender, EventArgs e)
    {
        ChangeTheme();
    }

    protected void themesRb_SelectedIndexChanged(object sender, EventArgs e)
    {
        // save last change in user's session:
        Session["userThemeValue"] = themesRb.SelectedValue;
        ChangeTheme(); // will be happening in init
    }

    private void ChangeTheme()
    {
        // if the theme was changed before then we put it's value from the session:
        if (Session["userThemeValue"] != null)
        {
            string userThemeValue = Session["userThemeValue"].ToString();
            themesRb.SelectedValue = userThemeValue;
        }

        string cssFile; 

        switch (themesRb.SelectedValue.ToLower())
        {
            case "black":
                cssFile = "styleBlack.css";
                break;
            case "white":
                cssFile = "styleWhite.css";
                break;
            case "brown":
                cssFile = "styleBrown.css";
                break;
            default:
                cssFile = "styleBlack.css";
                break;
        }

        Page.Header.Controls.Add(
            new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" +
                                             ResolveUrl(cssFile) + "\" />"));
    }
}