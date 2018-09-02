using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Helper.ValidateAdmin();
        Session["module"] = "Users";
        Session["page"] = "View Users";

        if (!IsPostBack)
        {
            GetUsers();
        }
    }

    void GetUsers()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT DISTINCT a.Code, a.AccountNo, 
                at.UserType,
                ISNULL(p.LastName, ISNULL(f.LastName, s.LastName)) + ', ' +
                ISNULL(p.FirstName, ISNULL(f.FirstName, s.FirstName)) AS Name,
                a.Email, 
                a.DateAdded, a.DateModified, a.Status
                FROM Account a 
                INNER JOIN Account_Type at ON a.TypeID = at.TypeID
                LEFT JOIN Personnel p ON p.AccountNo = a.AccountNo
                LEFT JOIN Faculty f ON f.AccountNo = a.AccountNo
                LEFT JOIN Students s ON s.AccountNo = a.AccountNo
                WHERE a.Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvRecords.DataSource = data;
                    lvRecords.DataBind();
                }
            }
        }
    }

    protected void lvRecords_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltAccountNo = (Literal)e.Item.FindControl("ltAccountNo");
        Literal ltName = (Literal)e.Item.FindControl("ltName");
        Literal ltEmail = (Literal)e.Item.FindControl("ltEmail");

        if (e.CommandName == "reset")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Account SET Password=@Password,
                        DateModified=@DateModified
                        WHERE AccountNo=@AccountNo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Password", Helper.Hash("temppassword"));
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@AccountNo", ltAccountNo.Text);
                    cmd.ExecuteNonQuery();

                    Helper.Log("Account", "Password reset.");
                    Session["reset"] = "yes";
                    string URL = Helper.GetURL() + "Account/SignIn";

                    string message = "Dear " + ltAccountNo.Text + ",<br/><br/>" +
                        "Your password has been reset to <strong>temppassword</strong>.<br/><br/." +
                        "Click the link below to sign in:<br/>" +
                        "<a href='" + URL + "'>" + URL + "</a>";
                    Helper.SendEmail(ltEmail.Text, "Password Reset", message);
                    Response.Redirect("~/Users/");
                }
            }
        }

        else if (e.CommandName == "remove")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Account SET Status=@Status,
                DateModified=@DateModified
                WHERE AccountNo=@AccountNo AND Email=@Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@AccountNo", ltAccountNo.Text);
                    cmd.Parameters.AddWithValue("@Email", ltEmail.Text);
                    cmd.ExecuteNonQuery();
                    Helper.Log("Delete", "Removed account '" + ltAccountNo.Text + "'.");
                    Session["update"] = "yes";
                    Response.Redirect("~/Users");
                }
            }
        }
    }
}