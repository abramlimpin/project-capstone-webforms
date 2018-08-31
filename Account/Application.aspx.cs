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
        else
        {
            add.Visible = false;
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
            string query = @"SELECT s.AccountNo, s.StudentID, s.FirstName + ' ' + s.LastName AS Name
                FROM Students s
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
            string query = @"SELECT f.LastName + ', ' + f.FirstName AS Name, e.Status
                FROM Enlistment e
                INNER JOIN Faculty f ON e.FacultyID = f.FacultyID
                WHERE e.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvMentors.DataSource = data;
                    lvMentors.DataBind();
                }
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

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
        if (e.CommandName == "display")
        {
            Response.Redirect("derp");
            Literal ltCode = (Literal)e.Item.FindControl("ltCode");
            string url = Helper.GetURL() + "download.aspx?f=" + ltCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openpage", "window.open('" + url + "', '_blank');", true);
        }
        else if (e.CommandName == "remove")
        {
            Response.Redirect("test");
        }
    }
}