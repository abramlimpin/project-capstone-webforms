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
                a.Email, s.DateAdded, s.DateModified, s.Status
                FROM Students s
                INNER JOIN Account a ON s.AccountNo = a.AccountNo
                WHERE a.Status!=@Status";
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
        if (e.CommandName == "activate")
        {
            Literal ltAccountNo = (Literal)e.Item.FindControl("ltAccountNo");
            Literal ltEmail = (Literal)e.Item.FindControl("ltEmail");

            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Account SET Status=@Status,
                DateModified=@DateModified
                WHERE AccountNo=@AccountNo;
                UPDATE Students SET Status=@Status,
                DateModified=@DateModified
                WHERE AccountNo=@AccountNo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@AccountNo", ltAccountNo.Text);
                    cmd.ExecuteNonQuery();
                    Helper.Log("Update", "Activated account '" + ltAccountNo.Text + "'.");
                    Session["update"] = "yes";

                    string URL = Helper.GetURL() + "Account/SignIn";
                    string message = "Your account is now activated. You may now sign in.<br/><br/." +
                        "Click the link below:<br/>" +
                        "<a href='" + URL + "'>" + URL + "</a>";
                    Helper.SendEmail(ltEmail.Text, "Account Activated", message);

                    Response.Redirect("~/Students");
                }
            }
        }
    }
}