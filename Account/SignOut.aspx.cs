using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_SignOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["accountno"] != null)
        {
            Helper.Log("Account", "Signed out.");
        }

        Session.Clear();
        Session["signout"] = "yes";
        Response.Redirect("~/Account/SignIn");
    }
}