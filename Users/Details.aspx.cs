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
        Helper.ValidateUser();
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
                    GetRoles();
                    GetInfo(code);
                }
            }
            else
                Response.Redirect("~/Users");
        }
    }

    void GetRoles()
    {
        ddlRoles.DataSource = DB.GetRoles();
        ddlRoles.DataTextField = "Name";
        ddlRoles.DataValueField = "RoleID";
        ddlRoles.DataBind();
        ddlRoles.Items.Insert(0, new ListItem("Select one...", ""));
    }

    void GetInfo(Guid code)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT a.Status, at.UserType, a.AccountNo,
                ISNULL(p.LastName, ISNULL(f.LastName, s.LastName)) AS LastName,
                ISNULL(p.FirstName, ISNULL(f.FirstName, s.FirstName)) AS FirstName,
                a.RoleID, a.Email, 
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
                            ltAccountNo.Text = data["AccountNo"].ToString();
                            txtEmail.Text = data["Email"].ToString();
                            txtFN.Text = data["FirstName"].ToString();
                            txtLN.Text = data["LastName"].ToString();

                            ddlRoles.SelectedValue = data["RoleID"].ToString();
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Account SET Status=@Status, RoleID=@RoleID,
                DateModified=@DateModified
                WHERE Code=@Code";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", cboStatus.Checked ? "Active" : "Inactive");
                if (ddlRoles.SelectedIndex == 0)
                    cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@RoleID", ddlRoles.SelectedValue);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@Code", Request.QueryString["no"].ToString());
                cmd.ExecuteNonQuery();
                Helper.Log("Update", "Updated account '" + ltAccountNo.Text + "' details.");
                Session["update"] = "yes";
                Response.Redirect("~/Users");
            }
        }
    }

    protected void btnArchive_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Account SET Status=@Status,
                DateModified=@DateModified
                WHERE AccountNo=@AccountNo;
                UPDATE Personnel SET Status=@Status,
                DateModified=@DateModified
                WHERE AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@AccountNo", ltAccountNo.Text);
                cmd.ExecuteNonQuery();
                Helper.Log("Delete", "Removed account '" + ltAccountNo.Text + "'.");
                Session["update"] = "yes";
                Response.Redirect("~/Users");
            }
        }
    }
}