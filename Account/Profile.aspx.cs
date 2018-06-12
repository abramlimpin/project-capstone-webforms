using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Account";
        Session["page"] = "My Account";

        if (Session["update"] != null)
        {
            update.Visible = true;
            Session.Remove("update");
        }
        else if (Session["account"] != null)
        {
            account.Visible = true;
            Session.Remove("account");
        }
        else
        {
            update.Visible = false;
            account.Visible = false;
        }
        if (!IsPostBack)
        {
            GetInfo();
        }
    }

    void GetInfo()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = "";
            if (Session["typeid"].ToString() == "1")
            {
                query = @"SELECT p.Image, p.AccountNo, a.Email,
                    p.FirstName, p.MiddleName, p.LastName, p.Nickname,
                    p.Birthdate, p.Gender
                    FROM Account a 
                    INNER JOIN Personnel p ON a.AccountNo = p.AccountNo
                    WHERE a.AccountNo=@AccountNo";
            }
            else if (Session["typeid"].ToString() == "2" ||
                Session["typeid"].ToString() == "3")
            {
                query = @"SELECT f.Image, f.AccountNo, a.Email,
                    f.FirstName, f.MiddleName, f.LastName, f.Nickname,
                    f.Birthdate, f.Gender
                    FROM Account a 
                    INNER JOIN Faculty f ON a.AccountNo = f.AccountNo
                    WHERE a.AccountNo=@AccountNo";
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        imgUser.ImageUrl = data["Image"].ToString() == "" ? "~/images/user-placeholder.jpg" : 
                            "~/images/users/" + data["Image"].ToString();
                        Session["image"] = data["Image"].ToString();
                        txtUsername.Text = data["AccountNo"].ToString();
                        ltAccountNo.Text = data["AccountNo"].ToString();
                        txtEmail.Text = data["Email"].ToString();
                        txtFN.Text = data["FirstName"].ToString();
                        txtMN.Text = data["MiddleName"].ToString();
                        txtLN.Text = data["LastName"].ToString();
                        txtNick.Text = data["Nickname"].ToString();
                        // txtProgram.Text = data["Program"].ToString();
                        DateTime birthDate = DateTime.Now;
                        bool validBirthDate = DateTime.TryParse(data["Birthdate"].ToString(), out birthDate);
                        if (validBirthDate)
                        {
                            txtBirthdate.Text = birthDate.ToString("yyyy-MM-dd");
                        }
                        ddlGender.SelectedValue = data["Gender"].ToString();
                    }
                }
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = "";
            if (Session["typeid"].ToString() == "1")
            {
                query = @"UPDATE Personnel SET Image=@Image, MiddleName=@MiddleName,
                    Nickname=@Nickname,
                    Birthdate=@Birthdate, Gender=@Gender, DateModified=@DateModified
                    WHERE AccountNo=@AccountNo";
            }
            else if (Session["typeid"].ToString() == "2" ||
                Session["typeid"].ToString() == "3")
            {
                query = @"UPDATE Faculty SET Image=@Image, MiddleName=@MiddleName,
                    Nickname=@Nickname,
                    Birthdate=@Birthdate, Gender=@Gender, DateModified=@DateModified
                    WHERE AccountNo=@AccountNo";
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (fuImage.HasFile)
                {
                    string ext = Path.GetExtension(fuImage.FileName);

                    cmd.Parameters.AddWithValue("@Image", Session["accountno"].ToString() + "-" + "image" + ext);
                    fuImage.SaveAs(Server.MapPath("~/images/users/" + Session["accountno"].ToString() + "-" + "image" + ext));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Image", Session["image"].ToString());
                    Session.Remove("image");
                }
                cmd.Parameters.AddWithValue("@MiddleName", txtMN.Text);
                cmd.Parameters.AddWithValue("@Nickname", txtNick.Text);
                cmd.Parameters.AddWithValue("@Birthdate", txtBirthdate.Text);
                cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.ExecuteNonQuery();
                Helper.Log("Account", "Updated profile.");
                Session["update"] = "yes";
                Response.Redirect("~/Account/Profile");
            }
        }
    }

    bool IsValid(string password)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AccountNo FROM Account
                WHERE AccountNo=@AccountNo AND Password=@Password";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@Password", Helper.Hash(password));
                return cmd.ExecuteScalar() == null ? false : true;
            }
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        if (txtPassword_Old.Text != "" && txtPassword_New.Text != "")
        {
            if (IsValid(txtPassword_Old.Text))
            {
                password.Visible = false;
                using (SqlConnection con = new SqlConnection(Helper.GetCon()))
                {
                    con.Open();
                    string query = @"UPDATE Account SET Password=@Password,
                        DateModified=@DateModified
                        WHERE AccountNo=@AccountNo";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Password", Helper.Hash(txtPassword_New.Text));
                        cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                        cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                        cmd.ExecuteNonQuery();

                        Helper.Log("Account", "Changed password.");
                        Session["account"] = "yes";
                        Response.Redirect("~/Account/Profile");
                    }
                }
            }
            else
            {
                password.Visible = true;
            }
        }
    }
}