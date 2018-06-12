using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Advising : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Faculty";
        Session["page"] = "Manage Advising";

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
            GetFaculty();
            GetSlots();
        }
    }

    void GetFaculty()
    {
        ddlFaculty.DataSource = DB.GetFaculty();
        ddlFaculty.DataTextField = "Name";
        ddlFaculty.DataValueField = "FacultyID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, new ListItem("Select a faculty...", ""));
    }

    bool IsExisting(string facultyID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID FROM Faculty_Slots
                WHERE FacultyID=@FacultyID AND SchoolYear=@SchoolYear
                AND Term=@Term AND Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", facultyID);
                cmd.Parameters.AddWithValue("@SchoolYear", ddlSY.SelectedValue);
                cmd.Parameters.AddWithValue("@Term", ddlTerms.SelectedValue);
                cmd.Parameters.AddWithValue("@Status", "Archived");
                return cmd.ExecuteScalar() == null ? false : true;
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (IsExisting(ddlFaculty.SelectedValue))
        {
            existing.Visible = true;
        }
        else
        {
            existing.Visible = false;
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO Faculty_Slots VALUES
                (@FacultyID, @SchoolYear, @Term, @Slot, 
                @DateAdded, @DateModified, @Status);";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FacultyID", ddlFaculty.SelectedValue);
                    cmd.Parameters.AddWithValue("@SchoolYear", ddlSY.SelectedValue);
                    cmd.Parameters.AddWithValue("@Term", ddlTerms.SelectedValue);
                    cmd.Parameters.AddWithValue("@Slot", txtSlot.Text);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.ExecuteNonQuery();

                    Helper.Log("Add", "Added slots for " + ddlFaculty.Text + ".");
                    Session["add"] = "yes";
                    Response.Redirect("~/Faculty/Advising");
                }
            }
        }
    }

    void GetSlots()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT fs.RecordID, f.LastName + ', ' + f.FirstName AS Name,
                fs.SchoolYear, fs.Term, fs.Slot, fs.DateAdded, fs.DateModified,  fs.Status
                FROM Faculty_Slots fs 
                INNER JOIN Faculty f ON fs.FacultyID = f.FacultyID
                WHERE fs.Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvSlots.DataSource = data;
                    lvSlots.DataBind();
                }
            }
        }
    }

    protected void lvSlots_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltRecordID = (Literal)e.Item.FindControl("ltRecordID");

        if (e.CommandName == "manage")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT RecordID, FacultyID, 
                    SchoolYear, Term, Slot, Status
                    FROM Faculty_Slots
                    WHERE RecordID=@RecordID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RecordID", ltRecordID.Text);
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            ltRecord.Text = data["RecordID"].ToString();
                            cboStatus.Checked = data["Status"].ToString() == "Active" ? true: false;
                            ddlFaculty.SelectedValue = data["FacultyID"].ToString();
                            ddlSY.SelectedValue = data["SchoolYear"].ToString();
                            ddlTerms.SelectedValue = data["Term"].ToString();
                            txtSlot.Text = data["Slot"].ToString();
                        }

                        ddlFaculty.Attributes.Add("disabled", "");
                        ddlSY.Attributes.Add("disabled", "");
                        ddlTerms.Attributes.Add("disabled", "");
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
                string query = @"UPDATE Faculty_Slots SET Status=@Status
                    WHERE RecordID=@RecordID;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@RecordID", ltRecordID.Text);
                    cmd.ExecuteNonQuery();

                    Helper.Log("Archive", "Archived slot record #" + ltRecordID.Text);
                    Session["update"] = "yes";
                    Response.Redirect("~/Faculty/Advising");
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Faculty/Advising");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Faculty_Slots SET Slot=@Slot,
                Status=@Status, DateModified=@DateModified
                WHERE RecordID=@RecordID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Slot", txtSlot.Text);
                cmd.Parameters.AddWithValue("@Status", cboStatus.Checked ? "Active" : "Inactive");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@RecordID", ltRecord.Text);
                cmd.ExecuteNonQuery();

               
                Helper.Log("Update", "Updated slots for faculty " + ddlFaculty.SelectedValue);
                Session["update"] = "yes";
                Response.Redirect("~/Faculty/Advising");
            }
        }
    }
}