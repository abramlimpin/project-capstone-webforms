using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adviser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Directory";

        if (Request.QueryString["u"]== null)
        {
            Response.Redirect("~/");
        }
        else
        {
            string accountNo = Request.QueryString["u"].ToString();
            GetInfo(accountNo);
            GetAffiliations(accountNo);
        }
    }

    void GetInfo(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT f.FacultyID, f.Image, 
                f.FirstName + ' ""' + f.Nickname + '"" ' + f.LastName AS Name,
                a.Email, 
                fa.StudioName, fa.Teaching, fa.Research,
                fa.Statement, fa.Resume, fa.Agenda, fa.Manifesto,
                fa.Availability, fa.Others
                FROM Faculty f 
                INNER JOIN Account a ON f.AccountNo = a.AccountNo
                INNER JOIN Faculty_Advising fa ON f.FacultyID = fa.FacultyID
                WHERE f.AccountNo=@AccountNo AND f.Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                cmd.Parameters.AddWithValue("@Status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            imgUser.ImageUrl = data["Image"].ToString() == "" ? "~/images/user-placeholder.jpg" :
                                "~/images/users/" + data["Image"].ToString();
                            Session["page"] = data["Name"].ToString();
                            ltName.Text = data["Name"].ToString();
                            ltEmail.Text = data["Email"].ToString();
                            ltStudio.Text = data["StudioName"].ToString();
                            ltManifesto.Text = data["Manifesto"].ToString();

                            ltTeaching.Text = data["Teaching"].ToString();
                            ltResearch.Text = data["Research"].ToString();
                            ltStatement.Text = data["Statement"].ToString();

                            ltResume.Text = data["Resume"].ToString();
                            ltAgenda.Text = data["Agenda"].ToString();
                            ltOthers.Text = data["Others"].ToString();

                            pnlOthers.Visible = ltOthers.Text == "" ? false : true;

                            ltAvailability.Text = data["Availability"].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("~/");
                    }
                }
            }
        }
    }

    void GetAffiliations(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT a.Name FROM Faculty_Affiliations fa
                INNER JOIN Affiliations a ON fa.AffID = a.AffID
                INNER JOIN Faculty f ON fa.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvAffiliations.DataSource = data;
                    lvAffiliations.DataBind();
                }
            }
        }
    }
}