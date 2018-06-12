using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Affiliations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Admin";
        Session["page"] = "Manage Direction & Platform Affiliations";

        if (!IsPostBack)
        {
            GetModules();
        }
    }

    void GetModules()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AffID, Code, Name, DateAdded, DateModified, Status
                FROM Affiliations";
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            int affID = 0;
            con.Open();
            string query = @"INSERT INTO Affiliations VALUES
                (@Code, @Name, @DateAdded, @DateModified, @Status);
                SELECT TOP 1 AffID FROM Affiliations
                ORDER BY AffID DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Active");
                affID = (int)cmd.ExecuteScalar();
                Helper.Log("Add", "Added affiliation id '" + affID.ToString() + "'.");
                update.Visible = true;
                GetModules();
                txtName.Text = string.Empty;
            }
        }
    }
}