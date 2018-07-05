using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Faculty";
        Session["page"] = "View Faculty";

        if (!IsPostBack)
        {
            GetFaculty();
        }
    }

    void GetFaculty()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT a.Code, a.AccountNo, f.LastName + ', ' + f.FirstName AS Name,
                a.Email, p.Name AS Program,
                a.DateAdded, a.DateModified, a.Status
                FROM Account a 
                INNER JOIN Faculty f ON a.AccountNo = f.AccountNo
                INNER JOIN Programs p ON f.ProgramID = p.ProgramID
                WHERE a.Status!=@Status";
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
}