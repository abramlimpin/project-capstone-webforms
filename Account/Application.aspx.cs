using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Application : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Application";
        Session["page"] = "Student Profile";

        if (Session["add"] != null)
        {
            add.Visible = true;
            Session.Remove("add");
        }
        else if (Session["update"] != null)
        {
            update.Visible = true;
            Session.Remove("update");
        }
        else
        {
            add.Visible = false;
            update.Visible = false;
        }

        if (!IsPostBack)
        {
            GetInfo();
            GetMentors();
            GetUploads(); 
        }
    }

    void GetInfo()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT s.AccountNo, s.StudentID, 
                s.FirstName + ' ' + s.LastName AS Name,
                sa.Course, sa.Agenda
                FROM Students s
                LEFT JOIN Students_Application sa ON sa.AccountNo = s.AccountNo
                WHERE s.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        hlkProfile.NavigateUrl = "~/student?u=" + data["AccountNo"].ToString();
                        txtName.Text = data["Name"].ToString();
                        ltStudentID.Text = data["StudentID"].ToString();
                        txtStudentNo.Text = data["AccountNo"].ToString();

                        cbCourse.Checked = data["Course"].ToString() == "ARCDES9" ? false : true;
                        txtAgenda.Text = Server.HtmlDecode(data["Agenda"].ToString());
                    }
                }
            }
        }
    }


    void GetMentors()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT e.EnlistID, f.LastName + ', ' + f.FirstName AS Name, e.Status
                FROM Enlistment e
                INNER JOIN Faculty f ON e.FacultyID = f.FacultyID
                WHERE e.AccountNo=@AccountNo
                AND e.Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvMentors.DataSource = data;
                    lvMentors.DataBind();
                }
            }
        }
    }

    bool IsExisting(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID FROM Students_Application
                WHERE AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                return cmd.ExecuteScalar() == null ? false : true;
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = "";
            bool existingRecord = IsExisting(Session["accountno"].ToString());
            if (existingRecord)
            {
                query = @"UPDATE Students_Application 
                    SET Course=@Course, Agenda=@Agenda,
                    DateModified=@DateModified
                    WHERE AccountNo=@AccountNo";
            }
            else
            {
                query = @"INSERT INTO Students_Application
                    VALUES (@AccountNo, @Course, @Term,
                    @SchoolYear, @Agenda, @DateAdded,
                    @DateModified, @Status);";
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@Course", cbCourse.Checked ? "ARCDS10" : "ARCDES9");
                cmd.Parameters.AddWithValue("@Term", "1st");
                cmd.Parameters.AddWithValue("@SchoolYear", "2018-2019");
                cmd.Parameters.AddWithValue("@Agenda", Server.HtmlEncode(txtAgenda.Text));
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                if (existingRecord)
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                else
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.ExecuteNonQuery();

                if (existingRecord)
                    Helper.Log("Update", "Updated application.");
                else
                    Helper.Log("Add", "Added application");
                Session["update"] = "yes";
                Response.Redirect("~/Account/Application");
            }
        }
    }


    void GetUploads()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT Code, Name, FileName, DateAdded FROM Uploads
                WHERE AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvUploads.DataSource = data;
                    lvUploads.DataBind();
                }
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fuFile.HasFile)
        {
            int fileID = 0;
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO Uploads VALUES
                    (@Code, @AccountNo, @Name, @Description,
                    @FileName, @DateAdded);
                    SELECT TOP 1 FileID FROM Uploads
                    ORDER BY FileID DESC;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                    cmd.Parameters.AddWithValue("@Name", fuFile.FileName);
                    cmd.Parameters.AddWithValue("@Description", "For Application");
                    string ext = Path.GetExtension(fuFile.FileName);
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss-") +
                        Session["accountno"].ToString() + "-application" + ext;
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    fuFile.SaveAs(Server.MapPath("~/uploads/" + fileName));
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    fileID = (int)cmd.ExecuteScalar();
                    Helper.Log("Add", "Added file #" + fileID.ToString());
                    Session["add"] = "yes";
                    Response.Redirect("~/Account/Application");
                }
            }
        }
    }

    protected void lvUploads_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltCode = (Literal)e.Item.FindControl("ltCode");
        if (e.CommandName == "display")
        {
            Response.Redirect("derp");
            
            string url = Helper.GetURL() + "download.aspx?f=" + ltCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openpage", "window.open('" + url + "', '_blank');", true);
        }
        else if (e.CommandName == "remove")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"DELETE FROM Uploads
                    WHERE Code=@Code AND AccountNo=@AccountNo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Code", ltCode.Text);
                    cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                    cmd.ExecuteNonQuery();
                    Helper.Log("Delete", "Removed file.");
                    Session["add"] = "yes";
                    Response.Redirect("~/Account/Application");
                }
            }
        }
    }

    protected void lvMentors_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "remove")
        {
            Literal ltEnlistID = (Literal)e.Item.FindControl("ltEnlistID");
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Enlistment SET Status=@Status,
                    DateModified=@DateModified
                    WHERE EnlistID=@EnlistID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@EnlistID", ltEnlistID.Text);
                    cmd.ExecuteNonQuery();
                    Helper.Log("Delete", "Removed enlistment");
                    Session["update"] = "yes";
                    Response.Redirect("~/Account/Application");
                }
            }
        }
    }
}