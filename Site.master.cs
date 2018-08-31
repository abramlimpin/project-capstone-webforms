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
            ToggleModules();
            ToggleMenu();
            //ToggleAdvising();
            GetUser();
            //GetLogs();
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
                    query = @"SELECT p.FirstName + ' ' + p.LastName AS Name, a.Email,
                    p.Image
                    FROM Personnel p
                    INNER JOIN Account a ON p.AccountNo = a.AccountNo
                    WHERE a.AccountNo = @AccountNo";
                }
                else if (Session["typeid"].ToString() == "2" ||
                    Session["typeid"].ToString() == "3")
                {
                    query = @"SELECT f.FirstName + ' ' + f.LastName AS Name, a.Email,
                    f.Image
                    FROM Faculty f
                    INNER JOIN Account a ON f.AccountNo = a.AccountNo
                    WHERE a.AccountNo = @AccountNo";

                    menu_advising.Visible = true;
                }
                else
                {
                    query = @"SELECT s.FirstName + ' ' + s.LastName AS Name, a.Email,
                        s.Image
                        FROM Students s
                        INNER JOIN Account a ON s.AccountNo = a.AccountNo
                        WHERE a.AccountNo = @AccountNo";
                }
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            string imageURL = data["Image"].ToString() == "" ? "images/user-placeholder.jpg" :
                                "images/users/" + data["Image"].ToString();
                            avatar.Attributes.Add("style", "background-image: url('" + Helper.GetURL() + imageURL + "')");
                            ltUser.Text = data["Name"].ToString();
                            ltEmail.Text = data["Email"].ToString();
                        }
                    }
                }
            }
        }
    }

    void ToggleModules()
    {
        if (Session["roleid"] != null)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT ModuleID FROM Roles_Modules
                    WHERE RoleID=@RoleID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RoleID", Session["roleid"].ToString());
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            if (data["ModuleID"].ToString() == "1")
                                menu_users.Visible = true;
                            if (data["ModuleID"].ToString() == "2")
                                menu_students.Visible = true;
                            if (data["ModuleID"].ToString() == "3")
                                menu_faculty.Visible = true;
                            //if (data["ModuleID"].ToString() == "4")
                            //    user.Visible = true;
                            if (data["ModuleID"].ToString() == "5")
                                menu_enlistment.Visible = true;
                            //if (data["ModuleID"].ToString() == "6")
                            //    ass.Visible = true;
                            if (data["ModuleID"].ToString() == "7")
                                menu_reports.Visible = true;
                            if (data["ModuleID"].ToString() == "8")
                                menu_news.Visible = true;
                            if (data["ModuleID"].ToString() == "9")
                                menu_admin.Visible = true;
                            if (data["ModuleID"].ToString() == "10")
                                menu_admin.Visible = true;
                            if (data["ModuleID"].ToString() == "11")
                                menu_admin.Visible = true;
                            if (data["ModuleID"].ToString() == "15")
                                menu_admin.Visible = true;
                            if (data["ModuleID"].ToString() == "16")
                                menu_directory.Visible = true;
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
            else if (Session["module"].ToString() == "Account")
                menu_account.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Portfolio")
                menu_portfolio.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Application")
                menu_application.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Advising")
                menu_advising.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Users")
                menu_users.Attributes.Add("class", "active");
            else if (Session["module"].ToString() == "Directory")
                menu_directory.Attributes.Add("class", "active");
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
            else
                menu_home.Attributes.Add("class", "active");
        }
    }

    void ToggleAdvising()
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
                menu_advising.Visible = cmd.ExecuteScalar() == null ? false : true;
                menu_application.Visible = !menu_advising.Visible;
            }
        }
    }


    void GetLogs()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT TOP 5 LogType, Description, LogDate
                FROM Logs
                WHERE AccountNo = @AccountNo AND LogType!=@LogType
                ORDER BY LogDate DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", Helper.Encrypt(Session["accountno"] == null ? "0" : 
                    Session["accountno"].ToString()));
                cmd.Parameters.AddWithValue("@LogType", "Error");
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
