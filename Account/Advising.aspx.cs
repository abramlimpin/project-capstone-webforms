using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Advising : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Advising";
        Session["page"] = "Adviser Profile";

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
            //AccessAdvising();
            GetInfo();
            GetTopics_Teaching();
            GetTopics_Research();
            GetTopics_Teaching_Faculty();
            GetTopics_Research_Faculty();

            GetDirections();
            GetDirections_Faculty();
            GetAffiliations();
            GetAffiliations_Faculty();
            GetEducation();
            GetSchedule();
        }
    }

    void AccessAdvising()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID FROM Faculty_Slots fs
                INNER JOIN Faculty f ON fs.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo AND fs.Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@Status", "Active");
                bool canAccess = cmd.ExecuteScalar() == null ? false : true;
                if (!canAccess)
                    Response.Redirect("~/Account/Profile");
            }
        }
    }

    bool IsExisting()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT FacultyID FROM Faculty_Advising
                WHERE FacultyID = @FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                return cmd.ExecuteScalar() == null ? false : true;
            }
        }
    }

    void GetInfo()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT f.AccountNo, f.FirstName + ' ' + f.LastName AS Name,
                f.FacultyID, fa.StudioName,
                fa.Statement, fa.Resume, fa.Agenda, fa.Manifesto,
                fa.Others
                FROM Faculty f
                LEFT JOIN Faculty_Advising fa ON fa.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        hlkProfile.NavigateUrl = "~/adviser?u=" + data["AccountNo"].ToString();
                        txtName.Text = data["Name"].ToString();
                        ltFacultyID.Text = data["FacultyID"].ToString();
                        txtStudio.Text = data["StudioName"].ToString();
                        txtStatement.Text = data["Statement"].ToString();
                        txtResume.Text = Server.HtmlDecode(data["Resume"].ToString());
                        txtAgenda.Text = Server.HtmlDecode(data["Agenda"].ToString());
                        txtManifesto.Text = Server.HtmlDecode(data["Manifesto"].ToString());
                        txtOthers.Text = data["Others"].ToString();
                    }

                    GetAffiliations_Faculty();
                }
            }
        }
    }

    void GetTopics_Teaching()
    {
        cbTopics_Teaching.DataSource = DB.GetTopics_Teaching();
        cbTopics_Teaching.DataTextField = "Name";
        cbTopics_Teaching.DataValueField = "RecordID";
        cbTopics_Teaching.DataBind();
    }

    void GetTopics_Research()
    {
        cbTopics_Research.DataSource = DB.GetTopics_Research();
        cbTopics_Research.DataTextField = "Name";
        cbTopics_Research.DataValueField = "RecordID";
        cbTopics_Research.DataBind();
    }

    void GetDirections()
    {
        cbDirections.DataSource = DB.GetDirections();
        cbDirections.DataTextField = "Name";
        cbDirections.DataValueField = "DirectID";
        cbDirections.DataBind();
    }

    void AddDirections(string directID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Faculty_Directions VALUES
                (@FacultyID, @DirectID, @DateAdded)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@DirectID", directID);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void GetAffiliations()
    {
        cbAffiliations.DataSource = DB.GetAffiliations();
        cbAffiliations.DataTextField = "Name";
        cbAffiliations.DataValueField = "AffID";
        cbAffiliations.DataBind();
    }

    void AddAffiliation(string affID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Faculty_Affiliations VALUES
                (@FacultyID, @AffID, @DateAdded)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@AffID", affID);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void AddTopic_Teaching(string recordID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Faculty_Topics_Teaching VALUES
                (@FacultyID, @RecordID, @DateAdded)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@RecordID", recordID);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void AddTopic_Research(string recordID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Faculty_Topics_Research VALUES
                (@FacultyID, @RecordID, @DateAdded)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@RecordID", recordID);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void DeleteAllTopics_Teaching()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"DELETE FROM Faculty_Topics_Teaching 
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void DeleteAllTopics_Research()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"DELETE FROM Faculty_Topics_Research
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void DeleteAllDirections()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"DELETE FROM Faculty_Directions 
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void DeleteAllAffiliations()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"DELETE FROM Faculty_Affiliations 
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void GetTopics_Teaching_Faculty()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID FROM Faculty_Topics_Teaching
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            foreach (ListItem item in cbTopics_Teaching.Items)
                            {
                                if (item.Value == data["RecordID"].ToString())
                                    item.Selected = true;
                            }
                        }
                    }
                }
            }
        }
    }

    void GetTopics_Research_Faculty()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID FROM Faculty_Topics_Research
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            foreach (ListItem item in cbTopics_Research.Items)
                            {
                                if (item.Value == data["RecordID"].ToString())
                                    item.Selected = true;
                            }
                        }
                    }
                }
            }
        }
    }

    void GetDirections_Faculty()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT DirectID FROM Faculty_Directions
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            foreach (ListItem item in cbDirections.Items)
                            {
                                if (item.Value == data["DirectID"].ToString())
                                    item.Selected = true;
                            }
                        }
                    }
                }
            }
        }
    }

    void GetAffiliations_Faculty()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AffID FROM Faculty_Affiliations
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            foreach (ListItem item in cbAffiliations.Items)
                            {
                                if (item.Value == data["AffID"].ToString())
                                    item.Selected = true;
                            }
                        }
                    }
                }
            }
        }
    }

    void GetEducation()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID, Institution, Degree,
                YearStart, YearEnd
                FROM Faculty_Education
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvEducation.DataSource = data;
                    lvEducation.DataBind();
                }
            }
        }
    }

    void GetSchedule()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID, Day, StartTime,
                EndTime
                FROM Faculty_Availability
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvSchedule.DataSource = data;
                    lvSchedule.DataBind();
                }
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = "";
            if (IsExisting())
            {
                query = @"UPDATE Faculty_Advising SET StudioName=@StudioName,
                    Statement=@Statement,
                    Resume=@Resume, Agenda=@Agenda, Manifesto=@Manifesto,
                    Others=@Others,
                    DateModified=@DateModified
                    WHERE FacultyID=@FacultyID";
            }
            else
            {
                query = @"INSERT INTO Faculty_Advising VALUES 
                    (@FacultyID, @StudioName, @Statement,
                    @Resume, @Agenda, @Manifesto, @Others, 
                    @DateAdded, @DateModified)";
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@StudioName", txtStudio.Text);
                cmd.Parameters.AddWithValue("@Statement", txtStatement.Text);
                cmd.Parameters.AddWithValue("@Resume", Server.HtmlEncode(txtResume.Text));
                cmd.Parameters.AddWithValue("@Agenda", Server.HtmlEncode(txtAgenda.Text));
                cmd.Parameters.AddWithValue("@Manifesto", Server.HtmlEncode(txtManifesto.Text));
                cmd.Parameters.AddWithValue("@Others", txtOthers.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                if (IsExisting())
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                else
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);

                cmd.ExecuteNonQuery();

                DeleteAllTopics_Teaching();
                DeleteAllTopics_Research();
                DeleteAllDirections();
                DeleteAllAffiliations();

                foreach (ListItem item in cbTopics_Teaching.Items)
                {
                    if (item.Selected)
                    {
                        AddTopic_Teaching(item.Value);
                    }
                }

                foreach (ListItem item in cbTopics_Research.Items)
                {
                    if (item.Selected)
                    {
                        AddTopic_Research(item.Value);
                    }
                }

                foreach (ListItem item in cbDirections.Items)
                {
                    if (item.Selected)
                    {
                        AddDirections(item.Value);
                    }
                }

                foreach (ListItem item in cbAffiliations.Items)
                {
                    if (item.Selected)
                    {
                        AddAffiliation(item.Value);
                    }
                }

                int teachingID = 0;
                if (cbTopics_Teaching_Other.Checked && txtTopics_Teaching_Other.Text != "")
                {
                    var keywords = txtTopics_Teaching_Other.Text.Split(new[]
                    { ','}, System.StringSplitOptions.RemoveEmptyEntries);

                    foreach (string item in keywords)
                    {
                        string other = @"INSERT INTO Topics_Teaching VALUES
                        (@Code, @Name, @DateAdded, @DateModified, @Status);
                        SELECT TOP 1 RecordID FROM Topics_Teaching
                        ORDER BY RecordID DESC;";
                        using (SqlCommand cmd_other = new SqlCommand(other, con))
                        {
                            cmd_other.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                            cmd_other.Parameters.AddWithValue("@Name", item);
                            cmd_other.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                            cmd_other.Parameters.AddWithValue("@DateModified", DBNull.Value);
                            cmd_other.Parameters.AddWithValue("@Status", "Active");
                            teachingID = (int)cmd_other.ExecuteScalar();
                            AddTopic_Teaching(teachingID.ToString());
                        }
                    }
                }

                int researchID = 0;
                if (cbTopics_Research_Others.Checked && txtTopics_Research_Others.Text != "")
                {
                    var keywords = txtTopics_Research_Others.Text.Split(new[]
                    { ','}, System.StringSplitOptions.RemoveEmptyEntries);

                    foreach (string item in keywords)
                    {
                        string other = @"INSERT INTO Topics_Research VALUES
                        (@Code, @Name, @DateAdded, @DateModified, @Status);
                        SELECT TOP 1 RecordID FROM Topics_Research
                        ORDER BY RecordID DESC;";
                        using (SqlCommand cmd_other = new SqlCommand(other, con))
                        {
                            cmd_other.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                            cmd_other.Parameters.AddWithValue("@Name", item);
                            cmd_other.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                            cmd_other.Parameters.AddWithValue("@DateModified", DBNull.Value);
                            cmd_other.Parameters.AddWithValue("@Status", "Active");
                            researchID = (int)cmd_other.ExecuteScalar();
                            AddTopic_Research(researchID.ToString());
                        }
                        
                    }
                }
                Helper.Log("Update", "Updated advising profile.");
                Session["update"] = "yes";
                Response.Redirect("~/Account/Advising");
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Faculty_Education VALUES
                (@FacultyID, @Institution, @Degree, @YearStart, @YearEnd,
                @DateAdded)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                cmd.Parameters.AddWithValue("@Degree", txtDegree.Text);
                cmd.Parameters.AddWithValue("@YearStart", txtStart.Text);
                cmd.Parameters.AddWithValue("@YearEnd", txtEnd.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.ExecuteNonQuery();

                Helper.Log("Add", "Added educational background.");
                Session["update"] = "yes";
                Response.Redirect("Advising");
            }
        }
    }

    protected void lvEducation_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "remove")
        {
            Literal ltRecordID = (Literal)e.Item.FindControl("ltRecordID");
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"DELETE FROM Faculty_Education
                    WHERE RecordID=@RecordID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RecordID", ltRecordID.Text);
                    cmd.ExecuteNonQuery();

                    Helper.Log("Delete", "Removed educational background.");
                    Session["update"] = "yes";
                    Response.Redirect("Advising");
                }
            }
        }
    }

    protected void cbTopics_Teaching_Other_CheckedChanged(object sender, EventArgs e)
    {
        txtTopics_Teaching_Other.Visible = cbTopics_Teaching_Other.Checked ? true : false;
    }

    protected void btnAddSched_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Faculty_Availability VALUES
                (@FacultyID, @Day, @StartTime, @EndTime, @DateAdded)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@Day", txtDay.Text);
                cmd.Parameters.AddWithValue("@StartTime", txtStart_Time.Text);
                cmd.Parameters.AddWithValue("@EndTime", txtEnd_Time.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.ExecuteNonQuery();

                Helper.Log("Add", "Added schedule.");
                Session["update"] = "yes";
                Response.Redirect("Advising");
            }
        }
    }

    protected void lvSchedule_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "remove")
        {
            Literal ltRecordID = (Literal)e.Item.FindControl("ltRecordID");
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"DELETE FROM Faculty_Availability
                    WHERE RecordID=@RecordID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RecordID", ltRecordID.Text);
                    cmd.ExecuteNonQuery();

                    Helper.Log("Delete", "Removed schedule.");
                    Session["update"] = "yes";
                    Response.Redirect("Advising");
                }
            }
        }
    }
}