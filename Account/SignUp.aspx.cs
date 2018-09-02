using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Regex emailRegEx = new Regex("^[A-Za-z0-9._%+-]+@benilde.edu.ph$");
        if (emailRegEx.Match(txtEmail.Text).Success)
        {
            email.Visible = false;
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
                        cmd.Parameters.AddWithValue("@TypeID", 4);
                        cmd.Parameters.AddWithValue("@RoleID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Code", code);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Password", Helper.Hash("temppassword"));
                        cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DateActivated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", "For Confirmation");
                        cmd.ExecuteNonQuery();
                    }

                    string query2 = @"INSERT INTO Students VALUES (@Code, @AccountNo, @CourseID,
                    @FirstName, @MiddleName, @LastName, @Nickname, @Image, @Birthdate,
                    @Gender, @DateAdded, @DateModified, @Status)";
                    using (SqlCommand cmd = new SqlCommand(query2, con))
                    {
                        cmd.Parameters.AddWithValue("@Code", Guid.NewGuid());
                        cmd.Parameters.AddWithValue("@AccountNo", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@CourseID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@FirstName", txtFN.Text);
                        cmd.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", txtLN.Text);
                        cmd.Parameters.AddWithValue("@Nickname", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Birthdate", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", "For Confirmation");
                        cmd.ExecuteNonQuery();
                        Helper.Log("Account", "Student '" + txtUsername.Text + "' signed up.");

                        string URL = Helper.GetURL() + "Account/Activate?code=" + code;
                        string message = "Welcome, " + txtFN.Text + " " + txtLN.Text + "!<br/><br/>" +
                            "You have a created an account. Please click the link below to confirm: <br/>" +
                            "<a href='" + URL + "'>" + URL + "</a><br/><br/>" +
                            "Thank you.";
                        Helper.SendEmail(txtEmail.Text, "Account Confirmation", message);
                        Session["signup"] = "yes";
                        Response.Redirect("SignIn");
                    }
                }
            }
        }
        else
        {
            email.Visible = true;
        }
    }
}