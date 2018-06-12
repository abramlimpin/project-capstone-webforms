using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Schools : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Admin";
        Session["page"] = "Manage Schools";

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
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Schools VALUES
                (@Code, @Name, @Description, @DateAdded, @DateModified, @Status);
                SELECT TOP 1 SchoolID FROM Schools
                ORDER BY SchoolID DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Active");
                int schoolID = (int)cmd.ExecuteScalar();
                
                Helper.Log("Add", "Added school #" + schoolID.ToString());
                Session["add"] = "yes";
                Response.Redirect("~/Admin/Schools");
            }
        }
    }

    void GetSchools()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT SchoolID, Name, Description, Status, DateAdded
                FROM Schools
                WHERE Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvSchools.DataSource = data;
                    lvSchools.DataBind();
                }
            }
        }
    }

    protected void lvSchools_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltSchoolID = (Literal)e.Item.FindControl("ltSchoolID");

        if (e.CommandName == "manage")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT SchoolID, Name, Description, Status
                    FROM Schools
                    WHERE SchoolID=@SchoolID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SchoolID", ltSchoolID.Text);
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            ltSchool.Text = data["SchoolID"].ToString();
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
                string query = @"UPDATE Schools SET Status=@Status,
                    DateModified=@DateModified
                    WHERE SchoolID=@SchoolID;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@SchoolID", ltSchoolID.Text);
                    cmd.ExecuteNonQuery();

                    Helper.Log("Archive", "Archived school #" + ltSchoolID.Text);
                    Session["update"] = "yes";
                    Response.Redirect("~/Admin/Schools");
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Schools");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Schools SET Name=@Name, Description=@Description,
                Status=@Status, DateModified=@DateModified
                WHERE SchoolID=@SchoolID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                cmd.Parameters.AddWithValue("@Status", cboStatus.Checked ? "Active" : "Inactive");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@SchoolID", ltSchool.Text);
                cmd.ExecuteNonQuery();
                
                Helper.Log("Update", "Updated school #" + ltSchool.Text);
                Session["update"] = "yes";
                Response.Redirect("~/Admin/Schools");
            }
        }
    }
}