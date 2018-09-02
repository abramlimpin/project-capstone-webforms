using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Students_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Helper.ValidateAdmin();
        Session["module"] = "Students";
        Session["page"] = "Students List";

        if (Session["update"] != null)
        {
            update.Visible = true;
            Session.Remove("update");
        }
        else
        {
            update.Visible = false;
        }


        if (!IsPostBack)
        {
            GetStudents();
        }
    }


    void GetStudents()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT s.Code, s.AccountNo, s.LastName + ', ' + s.FirstName AS Name,
                (SELECT TOP 1 a.Email FROM Account a
                WHERE a.AccountNo = s.AccountNo AND a.Status!=@Status
                ORDER BY a.RecordID DESC) AS Email, s.Image,
                s.DateAdded, s.DateModified, s.Status
                FROM Students s
                WHERE s.Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvStudents.DataSource = data;
                    lvStudents.DataBind();
                }
            }
        }
    }

    protected void lvStudents_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltAccountNo = (Literal)e.Item.FindControl("ltAccountNo");
        Literal ltEmail = (Literal)e.Item.FindControl("ltEmail");
        Literal ltCode = (Literal)e.Item.FindControl("ltCode");
        if (e.CommandName == "activate")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Account SET Status=@Status, RoleID=7,
                Password=@Password,
                DateModified=@DateModified
                WHERE AccountNo=@AccountNo AND Status!='Archived';
                UPDATE Students SET Status=@Status,
                DateModified=@DateModified
                WHERE AccountNo=@AccountNo AND Status!='Archived'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Password", Helper.Hash("temppassword"));
                    cmd.Parameters.AddWithValue("@AccountNo", ltAccountNo.Text);
                    cmd.ExecuteNonQuery();
                    Helper.Log("Update", "Activated account '" + ltAccountNo.Text + "'.");
                    Session["update"] = "yes";

                    string URL = Helper.GetURL() + "Account/SignIn";
                    string message = "Your account is now activated.<br/><br/." +
                        "Please use the following credentials to sign in:<br/><br/>" +
                        "Username: <strong>" + ltAccountNo.Text + "</strong> / <strong>" + ltEmail.Text + "</strong><br/>" +
                        "Password: <strong>temppassword</strong><br/>" +
                        "Website: <strong>https://project-capstone.azurewebsites.net</strong><br/><br/>" +
                        "Please change your temporary password as soon as possible.<br/><br/>" +
                        "Thank you.<br/><br/>" +
                        "<hr/>" +
                        "<small><em>Please do not reply to this email; this address is not monitored.</em></small>";
                    Helper.SendEmail(ltEmail.Text, "Account Activated", message);

                    Response.Redirect("~/Students");
                }
            }
        }

        else if (e.CommandName == "remove")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Students SET Status=@Status,
                DateModified=@DateModified
                WHERE Code=@Code AND AccountNo=@AccountNo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@AccountNo", ltAccountNo.Text);
                    cmd.Parameters.AddWithValue("@Code", ltCode.Text);
                    cmd.ExecuteNonQuery();
                    Helper.Log("Delete", "Removed account '" + ltAccountNo.Text + "'.");
                    Session["update"] = "yes";
                    Response.Redirect("~/Students");
                }
            }
        }
    }
}