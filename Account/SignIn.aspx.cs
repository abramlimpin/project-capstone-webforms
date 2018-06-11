using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_SignIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["signout"] != null)
        {
            signout.Visible = true;
            Session.Remove("signout");
        }
        else
        {
            signout.Visible = false;
        }
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AccountNo, TypeID, RoleID FROM Account
                WHERE (AccountNo=@AccountNo OR Email=@AccountNo) AND Password=@Password
                AND Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", Helper.Hash(txtPassword.Text));
                cmd.Parameters.AddWithValue("@Status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            Session["accountno"] = data["AccountNo"].ToString();
                            Session["typeid"] = data["TypeID"].ToString();
                            Session["roleid"] = data["RoleID"].ToString();
                        }
                        Helper.Log("Account", "Signed in.");
                        if (Request.QueryString["url"] == null)
                            Response.Redirect("~/");
                        else
                            Response.Redirect(Request.QueryString["url"].ToString().Replace(".aspx", ""));
                    }
                    else
                    {
                        error.Visible = true;
                    }
                }
            }
        }
    }
}