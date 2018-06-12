using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Activate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["code"] == null)
            Response.Redirect("~/Account/SignIn");
        else
        {
            Guid code = new Guid();
            bool validCode = Guid.TryParse(Request.QueryString["code"].ToString(), out code);
            if (validCode)
            {
                if (IsExisting(code))
                {
                    ActivateAccount(code);
                }
                else
                    Response.Redirect("~/Account/SignIn");
            }
            else
                Response.Redirect("~/Account/SignIn");
        }
    }

    bool IsExisting(Guid code)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AccountNo FROM Account
                WHERE Code=@Code AND Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@Status", "For Confirmation");
                if (cmd.ExecuteScalar() == null)
                    return false;
                else
                {
                    Session["studentno"] = (string)cmd.ExecuteScalar();
                    return true;
                }
            }
        }
    }

    void ActivateAccount(Guid code)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Account SET DateActivated=@DateActivated,
                DateModified=@DateModified, Status=@Status
                WHERE Code=@Code;
                UPDATE s SET s.DateModified=@DateModified, s.Status=@Status
                FROM Students s
                INNER JOIN Account a ON s.AccountNo = s.AccountNo
                WHERE a.Code=@Code;
                SELECT Email FROM Account WHERE Code=@Code";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@DateActivated", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                string email = (string)cmd.ExecuteScalar();
                Helper.Log(Session["studentno"].ToString(), "Account", "Activated account.");
                Session.Remove("studentno");
                Session["activate"] = "yes";

                string message = "Thank you for activating your account. Your records are currently processed. " +
                    "Please wait for an email notification when to sign in.";
                Helper.SendEmail(email, "Account Activated", message);
                Response.Redirect("~/Account/SignIn");
            }
        }
    }
}