using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Teaching : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Admin";
        Session["page"] = "Manage Teaching Topics";

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
            GetRecords();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Topics_Teaching VALUES
                (@Code, @Name, @DateAdded, @DateModified, @Status);
                SELECT TOP 1 RecordID FROM Topics_Teaching
                ORDER BY RecordID DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Active");
                int recordID = (int)cmd.ExecuteScalar();
                
                Helper.Log("Add", "Added teaching topic " + recordID.ToString());
                Session["add"] = "yes";
                Response.Redirect("~/Admin/Teaching");
            }
        }
    }

    void GetRecords()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID, Name, Status, DateAdded
                FROM Topics_Teaching
                WHERE Status!=@Status";
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
        Literal ltRecordID = (Literal)e.Item.FindControl("ltRecordID");

        if (e.CommandName == "manage")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT RecordID, Name, Status
                    FROM Topics_Teaching
                    WHERE RecordID=@RecordID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RecordID", ltRecordID.Text);
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            ltRecord.Text = data["RecordID"].ToString();
                            txtName.Text = data["Name"].ToString();
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
                string query = @"UPDATE Topics_Teaching SET Status=@Status,
                    DateModified=@DateModified
                    WHERE RecordID=@RecordID;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@RecordID", ltRecordID.Text);
                    cmd.ExecuteNonQuery();

                    Helper.Log("Archive", "Archived teaching topic #" + ltRecordID.Text);
                    Session["update"] = "yes";
                    Response.Redirect("~/Admin/Teaching");
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Teaching");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Topics_Teaching SET Name=@Name,
                Status=@Status, DateModified=@DateModified
                WHERE RecordID=@RecordID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Status", cboStatus.Checked ? "Active" : "Inactive");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@RecordID", ltRecord.Text);
                cmd.ExecuteNonQuery();
                
                Helper.Log("Update", "Updated teaching topic #" + ltRecord.Text);
                Session["update"] = "yes";
                Response.Redirect("~/Admin/Teaching");
            }
        }
    }
}