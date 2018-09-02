using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class student : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Directory";
        
        if (Request.QueryString["u"] == null)
        {
            Response.Redirect("~/");
        }
        else
        {
            string accountNo = Request.QueryString["u"].ToString();
            GetInfo(accountNo);
            GetUploads(accountNo);
            GetPortfolio(accountNo);
        }
    }

    
    void GetInfo(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT s.Image, s.StudentID, s.FirstName,
                s.LastName, s.Nickname, a.Email, e.FacultyID,
                sa.Course, sa.Agenda, e.Status
                FROM Students s
                INNER JOIN Students_Application sa ON s.AccountNo = sa.AccountNo
                INNER JOIN Account a ON s.AccountNo = a.AccountNo
                INNER JOIN Enlistment e ON s.AccountNo = e.AccountNo
                INNER JOIN Faculty f ON e.FacultyID = f.FacultyID
                WHERE s.AccountNo=@AccountNo AND a.Status!=@Status
                AND e.Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            imgUser.ImageUrl = data["Image"].ToString() == "" ? "~/images/user-placeholder.jpg" :
                                "~/images/users/" + data["Image"].ToString();
                            
                            ltStudentID.Text = data["StudentID"].ToString();
                            if (data["Nickname"].ToString() == "")
                                ltName.Text = data["FirstName"].ToString() + " " + data["LastName"].ToString();
                            else
                                ltName.Text = data["FirstName"].ToString() + " \"" + data["Nickname"].ToString() + "\" " + data["LastName"].ToString();
                            Session["page"] = ltName.Text;
                            ltEmail.Text = data["Email"].ToString();
                            ltCourse.Text = data["Course"].ToString();
                            ltAgenda.Text = Server.HtmlDecode(data["Agenda"].ToString());
                        }
                    }
                    else
                    {
                        Response.Redirect("~/");
                    }
                }
            }
        }
    }

    void GetUploads(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT Code, Name, FileName, DateAdded FROM Uploads
                WHERE AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvUploads.DataSource = data;
                    lvUploads.DataBind();
                }
            }
        }
    }

    void GetPortfolio(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT Code, Title, Image FROM Portfolio
                WHERE AccountNo=@AccountNo AND Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                cmd.Parameters.AddWithValue("@Status", "Published");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvPortfolio.DataSource = data;
                    lvPortfolio.DataBind();
                }
            }
        }
    }
}