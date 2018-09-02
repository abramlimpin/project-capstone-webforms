using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adviser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Directory";

        if (Session["add"] != null)
        {
            add.Visible = true;
            Session.Remove("add");
        }
        else
        {
            add.Visible = false;
        }
        if (Request.QueryString["u"] == null)
        {
            Response.Redirect("~/");
        }
        else
        {
            string accountNo = Request.QueryString["u"].ToString();
            GetInfo(accountNo);
            GetTopics_Teaching(accountNo);
            GetTopics_Research(accountNo);
            GetDirecions(accountNo);
            GetAffiliations(accountNo);
            GetEducation(accountNo);
            GetPortfolio(accountNo);
            GetSchedule(accountNo);
            ToggleButton();
        }
    }

    
    void GetInfo(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT f.FacultyID, f.Image, 
                f.FirstName, f.Nickname, f.LastName,
                a.Email, 
                fa.StudioName,
                fa.Statement, fa.Resume, fa.Agenda, fa.Manifesto,
                fa.Others
                FROM Faculty f 
                INNER JOIN Account a ON f.AccountNo = a.AccountNo
                LEFT JOIN Faculty_Advising fa ON fa.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo AND f.Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                cmd.Parameters.AddWithValue("@Status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            imgUser.ImageUrl = data["Image"].ToString() == "" ? "~/images/user-placeholder.jpg" :
                                "~/images/users/" + data["Image"].ToString();
                            
                            ltFacultyID.Text = data["FacultyID"].ToString();
                            if (data["Nickname"].ToString() == "")
                                ltName.Text = data["FirstName"].ToString() + " " + data["LastName"].ToString();
                            else
                                ltName.Text = data["FirstName"].ToString() + " \"" + data["Nickname"].ToString() + "\" " + data["LastName"].ToString();
                            Session["page"] = ltName.Text;
                            ltEmail.Text = data["Email"].ToString();
                            ltStudio.Text = data["StudioName"].ToString();
                            ltManifesto.Text = Server.HtmlDecode(data["Manifesto"].ToString());

                            ltStatement.Text = data["Statement"].ToString();

                            ltResume.Text = Server.HtmlDecode(data["Resume"].ToString());
                            ltAgenda.Text = Server.HtmlDecode(data["Agenda"].ToString());
                            ltOthers.Text = Server.HtmlDecode(data["Others"].ToString());

                            pnlOthers.Visible = ltOthers.Text == "" ? false : true;
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

    void GetTopics_Teaching(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT tt.Name FROM Faculty_Topics_Teaching ft
                INNER JOIN Topics_Teaching tt ON ft.RecordID = tt.RecordID
                INNER JOIN Faculty f ON ft.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvTopics_Teaching.DataSource = data;
                    lvTopics_Teaching.DataBind();
                }
            }
        }
    }

    void GetTopics_Research(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT tr.Name FROM Faculty_Topics_Research ft
                INNER JOIN Topics_Research tr ON ft.RecordID = tr.RecordID
                INNER JOIN Faculty f ON ft.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvTopics_Research.DataSource = data;
                    lvTopics_Research.DataBind();
                }
            }
        }
    }

    void GetDirecions(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT d.Name FROM Faculty_Directions fd
                INNER JOIN Directions d ON fd.DirectID = d.DirectID
                INNER JOIN Faculty f ON fd.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvDirections.DataSource = data;
                    lvDirections.DataBind();
                }
            }
        }
    }

    void GetAffiliations(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT a.Name FROM Faculty_Affiliations fa
                INNER JOIN Affiliations a ON fa.AffID = a.AffID
                INNER JOIN Faculty f ON fa.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvAffiliations.DataSource = data;
                    lvAffiliations.DataBind();
                }
            }
        }
    }

    void GetEducation(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT fe.Institution, fe.Degree,
                fe.YearStart, fe.YearEnd
                FROM Faculty_Education fe
                INNER JOIN Faculty f ON fe.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvEducation.DataSource = data;
                    lvEducation.DataBind();
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

    void GetSchedule(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT fa.Day, fa.StartTime,
                fa. EndTime
                FROM Faculty_Availability fa
               INNER JOIN Faculty f ON fa.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvSchedule.DataSource = data;
                    lvSchedule.DataBind();
                }
            }
        }
    }


    void ToggleButton()
    {
        if (Session["typeid"].ToString() == "4")
        {
            btnSelect.Visible = true;
        }
        else
            btnSelect.Visible = false;
    }

    bool IsExisting(string facultyID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT e.EnlistID FROM Enlistment e
                INNER JOIN Students s ON e.AccountNo = s.AccountNo
                WHERE s.AccountNo=@AccountNo AND e.FacultyID=@FacultyID
                AND e.Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@FacultyID", facultyID);
                cmd.Parameters.AddWithValue("@Status", "Archived");
                return cmd.ExecuteScalar() == null ? false : true;
            }
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (IsExisting(ltFacultyID.Text))
        {
            error.Visible = true;
        }
        else
        {
            error.Visible = false;
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO Enlistment VALUES
                    (@AccountNo, @FacultyID, @DateAdded, @DateModified, @Status, @Remarks)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                    cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@Remarks", "");
                    cmd.ExecuteNonQuery();
                    Helper.Log("Add", "Added enlistment record.");
                    Session["add"] = "yes";
                    Response.Redirect("~/adviser?u=" + Request.QueryString["u"].ToString());
                }
            }
        }

    }
}