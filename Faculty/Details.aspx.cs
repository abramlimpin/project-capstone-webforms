using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Faculty";
        Session["page"] = "Faculty Details";

        if (Request.QueryString["no"] == null)
            Response.Redirect("~/Faculty");
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
                Response.Redirect("~/Faculty");
        }
    }

    void GetInfo(Guid code)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = "";
            if (Session["typeid"].ToString() == "1")
            {
                query = @"SELECT f.Image, f.AccountNo, a.Email,
                    f.FirstName, f.MiddleName, f.LastName, f.Nickname,
                    f.Birthdate
                    FROM Account a 
                    INNER JOIN Faculty f ON a.AccountNo = f.AccountNo
                    WHERE a.Code = @Code";
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", code);
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
                query = @"UPDATE Faculty SET Image=@Image, MiddleName=@MiddleName,
                    Nickname=@Nickname,
                    Birthdate=@Birthdate, DateModified=@DateModified
                    WHERE Account=@AccountNo";
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
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@AccountNo", ltAccountNo.Text);
                cmd.ExecuteNonQuery();
                Helper.Log("Account", "Updated faculty '" + ltAccountNo.Text + "'.");
                Session["update"] = "yes";
                Response.Redirect("~/Faculty");
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
                UPDATE Faculty SET Status=@Status,
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
                Response.Redirect("~/Faculty");
            }
        }
    }
}