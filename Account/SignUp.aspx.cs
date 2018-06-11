using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        if (DB.IsAccountExisting(txtUsername.Text))
        {
            error.Visible = true;
        }
        else
        {
            error.Visible = false;
            Guid code = Guid.NewGuid();
            int accountID = 0;
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO Account VALUES (@AccountID,
                @Code, @Password, @DateAdded, @DateActivated,
                @DateModified, @Status);
                SELECT TOP 1 AccountID FROM Account
                ORDER BY AccountID DESC;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AccountID", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Password", Helper.Hash(txtPassword.Text));
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateActivated", DBNull.Value);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    accountID = (int)cmd.ExecuteScalar();
                }

                string query2 = @"INSERT INTO Students VALUES (@Code, @AccountNo, @CourseID,
                @FirstName, @MiddleName, @LastName, @Nickname, @Image, @Birthdate,
                @Gender, @DateAdded, @DateModified, @Status)";
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@Code", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@AccountNo", accountID);
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
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    cmd.ExecuteNonQuery();
                    Helper.Log("Account", "Student #" + accountID.ToString() + " signed up.");
                    Session["signup"] = "yes";
                    Response.Redirect("SignIn.aspx");
                }
            }
        }
    }
}