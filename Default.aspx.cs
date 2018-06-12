using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Home";
        Session["page"] = "Dashboard";

        if (Session["typeid"].ToString() == "1" ||
            Session["typeid"].ToString() == "2")
        {
            stat_admin.Visible = true;
            stat_faculty.Visible = false;

            ltCount_Users.Text = DB.CountRecords("Account");
            count_users.Attributes.Add("data-to", ltCount_Users.Text);

            ltCount_Students.Text = DB.CountRecords("Students");
            count_students.Attributes.Add("data-to", ltCount_Students.Text);

            ltCount_Faculty.Text = DB.CountRecords("Faculty");
            count_faculty.Attributes.Add("data-to", ltCount_Faculty.Text);

            ltCount_Enlist.Text = DB.CountRecords("Enlistment");
            count_enlist.Attributes.Add("data-to", ltCount_Enlist.Text);
        }
        else if (Session["typeid"].ToString() == "3")
        {
            stat_admin.Visible = false;
            stat_faculty.Visible = true;

            ltCount_Advisees.Text = DB.CountRecords_Advisees();
            count_students.Attributes.Add("data-to", ltCount_Advisees.Text);
        }
        else
        {
            stat_admin.Visible = false;
            stat_faculty.Visible = false;
        }

        if (!IsPostBack)
        {
            GetNews();
        }
    }

    void GetNews()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT TOP 5 Title, Post, DateAdded, DateModified
                FROM News
                ORDER BY DateAdded DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvNews.DataSource = data;
                    lvNews.DataBind();
                }
            }
        }
    }
}