using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class News_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "News";
        Session["page"] = "View Records";

        if (!IsPostBack)
        {
            GetNews();
        }
    }

    void GetNews()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT n.Code, n.AccountNo, 
                p.LastName  + ', ' + p.FirstName AS Name,
                n.Type, n.Title, n.Status, n.DateAdded, n.DateModified
                FROM News n
                LEFT JOIN Personnel p ON p.AccountNo = n.AccountNo
                WHERE n.Status!=@Status";
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