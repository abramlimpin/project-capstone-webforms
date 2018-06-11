using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["module"] = "Home";
        Session["page"] = "Dashboard";

        ltCount_Users.Text = DB.CountRecords("Account");
        count_users.Attributes.Add("data-to", ltCount_Users.Text);

        ltCount_Students.Text = DB.CountRecords("Students");
        count_students.Attributes.Add("data-to", ltCount_Students.Text);

        ltCount_Faculty.Text = DB.CountRecords("Faculty");
        count_faculty.Attributes.Add("data-to", ltCount_Faculty.Text);

        ltCount_Enlist.Text = DB.CountRecords("Enlistment");
        count_enlist.Attributes.Add("data-to", ltCount_Enlist.Text);
    }
}