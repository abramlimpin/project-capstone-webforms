using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Users";
        Session["page"] = "View Users";

        if (!IsPostBack)
        {
            GetUsers();
        }
    }

    void GetUsers()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT a.Code, a.AccountNo, 
                at.UserType,
                ISNULL(p.LastName, ISNULL(f.LastName, s.LastName)) + ', ' +
                ISNULL(p.FirstName, ISNULL(f.FirstName, s.FirstName)) AS Name,
                a.Email, 
                ISNULL(p.Gender, ISNULL(f.Gender, s.Gender)) AS Gender,
                a.DateAdded, a.DateModified, a.Status
                FROM Account a 
                INNER JOIN Account_Type at ON a.TypeID = at.TypeID
                LEFT JOIN Personnel p ON p.AccountNo = a.AccountNo
                LEFT JOIN Faculty f ON f.AccountNo = a.AccountNo
                LEFT JOIN Students s ON s.AccountNo = a.AccountNo
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