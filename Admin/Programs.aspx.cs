using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Programs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Admin";
        Session["page"] = "Manage Programs";

        if (Session["add"] != null)
        {
            success.Visible = true;
            ltSuccess.Text = "Record added.";
            Session.Remove("add");
        }
        else if (Session["update"] != null)
        {
            success.Visible = true;
            ltSuccess.Text = "Record updated.";
            Session.Remove("update");
        }
        else
        {
            success.Visible = false;
        }

        if (!IsPostBack)
        {
            GetSchools();
            GetPrograms();
        }
    }

    void GetSchools()
    {
        ddlSchools.DataSource = DB.GetSchools();
        ddlSchools.DataTextField = "Name";
        ddlSchools.DataValueField = "SchoolID";
        ddlSchools.DataBind();
        ddlSchools.Items.Insert(0, new ListItem("Select a school...", ""));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Programs VALUES
                (@Code, @SchoolID, @ProgramCode, @Name,
                @Description, @DateAdded, @DateModified, @Status);
                SELECT TOP 1 ProgramID FROM Programs
                ORDER BY ProgramID DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@SchoolID", ddlSchools.SelectedValue);
                cmd.Parameters.AddWithValue("@ProgramCode", txtCode.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Active");
                int programID = (int)cmd.ExecuteScalar();
                
                Helper.Log("Add", "Added program #" + programID.ToString());
                Session["add"] = "yes";
                Response.Redirect("~/Admin/Programs");
            }
        }
    }

    void GetPrograms()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT p.ProgramID, s.Name AS School, p.Name, 
                p.ProgramCode, p.Description, p.Status, p.DateAdded
                FROM Programs p
                INNER JOIN Schools s ON p.SchoolID = s.SchoolID
                WHERE p.Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvPrograms.DataSource = data;
                    lvPrograms.DataBind();
                }
            }
        }
    }

    protected void lvPrograms_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltProgramID = (Literal)e.Item.FindControl("ltProgramID");

        if (e.CommandName == "manage")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT ProgramID, SchoolID, Name, ProgramCode, 
                    Description, Status
                    FROM Programs
                    WHERE ProgramID=@ProgramID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProgramID", ltProgramID.Text);
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            ltProgram.Text = data["ProgramID"].ToString();
                            ddlSchools.SelectedValue = data["SchoolID"].ToString();
                            txtCode.Text = data["ProgramCode"].ToString();
                            txtName.Text = data["Name"].ToString();
                            txtDesc.Text = data["Description"].ToString();
                            cboStatus.Checked = data["Status"].ToString() == "Active" ? true: false;
                        }
                    }
                }
            }

            status.Visible = true;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
        }
        else if (e.CommandName == "archive")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Programs SET Status=@Status
                    WHERE ProgramID=@ProgramID;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@ProgramID", ltProgramID.Text);
                    cmd.ExecuteNonQuery();

                    Helper.Log("Archive", "Archived program #" + ltProgramID.Text);
                    Session["update"] = "yes";
                    Response.Redirect("~/Admin/Programs");
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Programs");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Programs SET SchoolID=@SchoolID,
                Name=@Name, ProgramCode=@ProgramCode, Description=@Description,
                Status=@Status, DateModified=@DateModified
                WHERE ProgramID=@ProgramID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@SchoolID", ddlSchools.SelectedValue);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@ProgramCode", txtCode.Text);
                cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                cmd.Parameters.AddWithValue("@Status", cboStatus.Checked ? "Active" : "Inactive");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@ProgramID", ltProgram.Text);
                cmd.ExecuteNonQuery();
                
                Helper.Log("Update", "Updated program #" + ltProgram.Text);
                Session["update"] = "yes";
                Response.Redirect("~/Admin/Programs");
            }
        }
    }
}