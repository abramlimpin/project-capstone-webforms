using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Students_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Students";
        Session["title"] = "Students List";

        if (!IsPostBack)
        {
            GetStudents();
        }
    }


    void GetStudents()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT s.Code, s.AccountNo, s.LastName + ', ' + s.FirstName AS Name,
                a.Email, s.Gender, s.DateAdded, s.DateModified, s.Status
                FROM Students s
                INNER JOIN Account a ON s.AccountNo = a.AccountNo
                WHERE a.Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvStudents.DataSource = data;
                    lvStudents.DataBind();
                }
            }
        }
    }
}