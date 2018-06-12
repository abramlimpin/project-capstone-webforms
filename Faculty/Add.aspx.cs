using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Faculty";
        Session["page"] = "Add a Faculty";

        if (!IsPostBack)
        {
            GetPrograms();
        }
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
        if (DB.IsAccountExisting(txtUsername.Text))
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
                    cmd.Parameters.AddWithValue("@TypeID", 3);
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

                string query2 = @"INSERT INTO Faculty VALUES
                    (@Code, @AccountNo, @ProgramID, @FirstName, @MiddleName, 
                    @LastName, @Nickname, @Image, @Birthdate, @Gender,
                    @DateAdded, @DateModified, @Status)";
            
                using (SqlCommand cmd = new SqlCommand(query2, con))
                {
                    cmd.Parameters.AddWithValue("@Code", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@AccountNo", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@ProgramID", ddlPrograms.SelectedValue);
                    cmd.Parameters.AddWithValue("@FirstName", txtFN.Text);
                    cmd.Parameters.AddWithValue("@MiddleName", txtMN.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLN.Text);
                    cmd.Parameters.AddWithValue("@Nickname", txtNickname.Text);
                    if (fuImage.HasFile)
                    {
                        string ext = Path.GetExtension(fuImage.FileName);

                        cmd.Parameters.AddWithValue("@Image", txtUsername.Text + "-" + "image" + ext);
                        fuImage.SaveAs(Server.MapPath("~/images/users/" + txtUsername.Text + "-" + "image" + ext));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                    }
                    DateTime birthDate = DateTime.Now;
                    bool validBirthDate = DateTime.TryParse(txtBirthdate.Text, out birthDate);
                    if (validBirthDate)
                        cmd.Parameters.AddWithValue("@Birthdate", birthDate);
                    else
                        cmd.Parameters.AddWithValue("@Birthdate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.ExecuteNonQuery();
                    Helper.Log("Add", "Added account '" + txtUsername.Text + "'.");
                    Session["add"] = "yes";
                    Response.Redirect("~/Faculty");
                }
            }
        }
    }
}