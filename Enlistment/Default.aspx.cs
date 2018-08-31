using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Enlistment_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();

        if (!IsPostBack)
        {
            GetEnlistment();
        }
    }

    void GetEnlistment()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT e.DateAdded, s.LastName + ', ' + s.FirstName AS Student,
                f.LastName + ', ' + f.FirstName AS Faculty,
                e.Status, s.AccountNo AS Code_Student,
                f.AccountNo AS Code_Faculty
                FROM Enlistment e
                INNER JOIN Students s ON e.AccountNo = s.AccountNo
                INNER JOIN Faculty f ON e.FacultyID = f.FacultyID
                ORDER BY e.DateAdded DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvRecords.DataSource = data;
                    lvRecords.DataBind();
                }
            }
        }
    }
}