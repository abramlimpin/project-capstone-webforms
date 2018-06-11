using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["module"] = "Users";
        Session["page"] = "User Details";

        if (Request.QueryString["no"] == null)
            Response.Redirect("~/Users");
        else
        {
            Guid code = new Guid();
            bool validCode = Guid.TryParse(Request.QueryString["no"].ToString(), out code);
            if (validCode)
            {
                if (!IsPostBack)
                {
                    GetInfo(code);
                }
            }
            else
                Response.Redirect("~/Users");
        }
    }

    void GetInfo(Guid code)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT a.Status, at.UserType, a.AccountNo,
                ISNULL(p.LastName, ISNULL(f.LastName, s.LastName)) AS LastName,
                ISNULL(p.FirstName, ISNULL(f.FirstName, s.FirstName)) AS FirstName,
                a.Email, 
                ISNULL(p.Gender, ISNULL(f.Gender, s.Gender)) AS Gender,
                a.DateAdded, a.DateModified, a.Status
                FROM Account a 
                INNER JOIN Account_Type at ON a.TypeID = at.TypeID
                LEFT JOIN Personnel p ON p.AccountNo = a.AccountNo
                LEFT JOIN Faculty f ON f.AccountNo = a.AccountNo
                LEFT JOIN Students s ON s.AccountNo = a.AccountNo
                WHERE a.Status!=@Status AND a.Code=@Code";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                cmd.Parameters.AddWithValue("@Code", code);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            cboStatus.Checked = data["Status"].ToString() == "Active" ? true : false;
                            txtUserType.Text = data["UserType"].ToString();
                            txtUsername.Text = data["AccountNo"].ToString();
                            txtEmail.Text = data["Email"].ToString();
                            txtFN.Text = data["FirstName"].ToString();
                            txtLN.Text = data["LastName"].ToString();
                            // txtProgram.Text = data["Program"].ToString();
                            txtGender.Text = data["Gender"].ToString();
                        }
                    }
                    else
                        Response.Redirect("~/Users");
                }
            }
        }
    }
}