using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Helper.ValidateAdmin();
        Session["module"] = "Users";
        Session["page"] = "Add a User";

        if (!IsPostBack)
        {
            GetAccountTypes();
            GetPrograms();
        }
    }

    void GetAccountTypes()
    {
        ddlTypes.DataSource = DB.GetTypes();
        ddlTypes.DataTextField = "UserType";
        ddlTypes.DataValueField = "TypeID";
        ddlTypes.DataBind();
        ddlTypes.Items.Insert(0, new ListItem("Select account type...", ""));
    }

    void GetPrograms()
    {
        ddlPrograms.DataSource = DB.GetPrograms();
        ddlPrograms.DataTextField = "ProgramCode";
        ddlPrograms.DataValueField = "ProgramID";
        ddlPrograms.DataBind();
        ddlPrograms.Items.Insert(0, new ListItem("Select program...", ""));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (DB.IsAccountExisting(txtUsername.Text, txtEmail.Text))
        {
            error.Visible = true;
        }
        else
        {
            error.Visible = false;
            Guid code = Guid.NewGuid();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO Account VALUES (@AccountNo,
                    @TypeID, @RoleID, @Code, @Email, @Password, @DateAdded, @DateActivated,
                    @DateModified, @Status);";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AccountNo", txtUsername.Text.ToLower());
                    cmd.Parameters.AddWithValue("@TypeID", ddlTypes.SelectedValue);
                    cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", Helper.Hash("temppassword"));
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateActivated", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.ExecuteNonQuery();
                }

                string query2 = "";
                if (ddlTypes.SelectedIndex == 1) // Administrator
                {
                    query2 = @"INSERT INTO Personnel 
                        (Code, AccountNo, FirstName, LastName, Gender,
                        DateAdded, Status)
                        VALUES
                        (@Code, @AccountNo, @FirstName, @LastName, @Gender,
                        @DateAdded, @Status)";
                }
                else if (ddlTypes.SelectedIndex == 2 ||
                    ddlTypes.SelectedIndex == 3) // Personnel & Faculty
                {
                    query2 = @"INSERT INTO Faculty
                        (Code, AccountNo, ProgramID, FirstName, LastName, DateAdded, Status)
                        VALUES
                        (@Code, @AccountNo, @ProgramID, @FirstName, @LastName,
                        @DateAdded, @Status)";
                }
                else
                {
                    query2 = @"INSERT INTO Students 
                        Code, AccountNo, ProgramID, FirstName, LastName,
                        Gender, DateAdded, Status)
                        VALUES 
                        (@Code, @AccountNo, @ProgramID, @FirstName, @LastName, @Gender, 
                        @DateAdded, @Status)";
                }
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@Code", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@AccountNo", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@ProgramID", ddlPrograms.SelectedValue);
                    cmd.Parameters.AddWithValue("@FirstName", txtFN.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLN.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.ExecuteNonQuery();
                    
                    string message = "Welcome, " + txtFN.Text + " " + txtLN.Text + "!<br/><br/>" +
                        "Your account has been created. Please use the following credentials to sign in:<br/><br/>" +
                        "Username: <strong>" + txtUsername.Text + "</strong> / <strong>" + txtEmail.Text + "</strong><br/>" +
                        "Password: <strong>temppassword</strong><br/>" +
                        "Website: <strong>https://project-capstone.azurewebsites.net</strong><br/><br/>" +
                        "Please change your temporary password as soon as possible.<br/><br/>" +
                        "Thank you.<br/><br/>" +
                        "<hr/>"+
                        "<small><em>Please do not reply to this email; this address is not monitored.</em></small>";
                    Helper.SendEmail(txtEmail.Text, "Account Created", message);

                    Helper.Log("Add", "Added account '" + txtUsername.Text + "'.");
                    Session["add"] = "yes";
                    Response.Redirect("~/Users");
                }
            }
        }
    }

    protected void ddlTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTypes.SelectedIndex == 2 ||
            ddlTypes.SelectedIndex == 3 ||
            ddlTypes.SelectedIndex == 4)
        {
            program.Visible = true;
        }
        else
            program.Visible = false;
    }
}