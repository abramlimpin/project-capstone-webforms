using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Advising : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Advising";
        Session["page"] = "Adviser Profile";

        if (Session["update"] != null)
        {
            update.Visible = true;
            Session.Remove("update");
        }
        else
        {
            update.Visible = false;
        }

        if (!IsPostBack)
        {
            AccessAdvising();
            GetInfo();
            GetAffiliations();
            GetAffiliations_Faculty();
        }
    }

    void AccessAdvising()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID FROM Faculty_Slots fs
                INNER JOIN Faculty f ON fs.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo AND fs.Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@Status", "Active");
                bool canAccess = cmd.ExecuteScalar() == null ? false : true;
                if (!canAccess)
                    Response.Redirect("~/Account/Profile");
            }
        }
    }

    static bool accountExisting = false;

    void GetInfo()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT f.FirstName + ' ' + f.LastName AS Name,
                f.FacultyID, fa.StudioName, fa.Teaching, fa.Research,
                fa.Statement, fa.Resume, fa.Agenda, fa.Manifesto
                FROM Faculty f
                LEFT JOIN Faculty_Advising fa ON fa.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        txtName.Text = data["Name"].ToString();
                        ltFacultyID.Text = data["FacultyID"].ToString();

                        accountExisting = data["StudioName"].ToString() == "" ? false : true;
                        txtStudio.Text = data["StudioName"].ToString();
                        txtTeaching.Text = data["Teaching"].ToString();
                        txtResearch.Text = data["Research"].ToString();
                        txtStatement.Text = data["Statement"].ToString();
                        txtResume.Text = data["Resume"].ToString();
                        txtAgenda.Text = data["Agenda"].ToString();
                        txtManifesto.Text = data["Manifesto"].ToString();
                    }

                    GetAffiliations_Faculty();
                }
            }
        }
    }

    void GetAffiliations()
    {
        cbAffiliations.DataSource = DB.GetAffiliations();
        cbAffiliations.DataTextField = "Name";
        cbAffiliations.DataValueField = "AffID";
        cbAffiliations.DataBind();
    }

    void AddAffiliation(string affID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Faculty_Affiliations VALUES
                (@FacultyID, @AffID, @DateAdded)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@AffID", affID);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void DeleteAllAffiliations()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"DELETE FROM Faculty_Affiliations 
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }

    void GetAffiliations_Faculty()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AffID FROM Faculty_Affiliations
                WHERE FacultyID=@FacultyID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            foreach (ListItem item in cbAffiliations.Items)
                            {
                                if (item.Value == data["AffID"].ToString())
                                    item.Selected = true;
                            }
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
            if (accountExisting)
            {
                query = @"UPDATE Faculty_Advising SET StudioName=@StudioName,
                    Teaching=@Teaching, Research=@Research, Statement=@Statement,
                    Resume=@Resume, Agenda=@Agenda, Manifesto=@Manifesto,
                    DateModified=@DateModified
                    WHERE FacultyID=@FacultyID";
            }
            else
            {
                query = @"INSERT INTO Faculty_Advising VALUES 
                    (@FacultyID, @StudioName, @Teaching, @Research, @Statement,
                    @Resume, @Agenda, @Manifesto, @DateAdded, @DateModified)";
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FacultyID", ltFacultyID.Text);
                cmd.Parameters.AddWithValue("@StudioName", txtStudio.Text);
                cmd.Parameters.AddWithValue("@Teaching", txtTeaching.Text);
                cmd.Parameters.AddWithValue("@Research", txtResearch.Text);
                cmd.Parameters.AddWithValue("@Statement", txtStatement.Text);
                cmd.Parameters.AddWithValue("@Resume", txtResume.Text);
                cmd.Parameters.AddWithValue("@Agenda", txtAgenda.Text);
                cmd.Parameters.AddWithValue("@Manifesto", txtManifesto.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                if (accountExisting)
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                else
                    cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);

                cmd.ExecuteNonQuery();

                DeleteAllAffiliations();

                foreach (ListItem item in cbAffiliations.Items)
                {
                    if (item.Selected)
                    {
                        AddAffiliation(item.Value);
                    }
                }

                Helper.Log("Update", "Updated advising profile.");
                Session["update"] = "yes";
                Response.Redirect("~/Account/Advising");
            }
        }
    }
}