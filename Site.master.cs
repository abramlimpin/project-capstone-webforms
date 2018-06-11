using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ToggleMenu();
            GetUser();
            GetLogs();
        }
    }

    void GetUser()
    {
        if (Session["accountno"] != null)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = "";
                if (Session["typeid"].ToString() == "1")
                {
                    query = @"SELECT p.FirstName + ' ' + p.LastName AS Name, a.Email
                    FROM Personnel p
                    INNER JOIN Account a ON p.AccountNo = a.AccountNo
                    WHERE a.AccountNo = @AccountNo";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            while (data.Read())
                            {
                                ltUser.Text = data["Name"].ToString();
                                ltEmail.Text = data["Email"].ToString();
                            }
                        }
                    }
                }
            }
        }
    }

    void ToggleMenu()
    {
        if (Session["module"] != null)
        {
            if (Session["module"].ToString() == "Home")
                menu_home.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Users")
                menu_users.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Students")
                menu_students.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Faculty")
                menu_faculty.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Enlistment")
                menu_enlistment.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "News")
                menu_news.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Reports")
                menu_reports.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Admin")
                menu_admin.Attributes.Add("class", "active");
        }
    }


    void GetLogs()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT TOP 5 LogType, Description, LogDate
                FROM Logs
                WHERE AccountNo = @AccountNo
                ORDER BY LogDate DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Helper.Encrypt(Session["accountno"] == null ? "0" : 
                    Session["accountno"].ToString()));
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvNotifications.DataSource = data;
                    lvNotifications.DataBind();
                }
            }
        }
    }

    protected void lvNotifications_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltTimestamp = (Literal)e.Item.FindControl("ltTimestamp");
            Label ltRelative = (Label)e.Item.FindControl("ltRelative");

            ltRelative.Text = Helper.ToRelativeDate(DateTime.Parse(ltTimestamp.Text));
        }
    }
}
