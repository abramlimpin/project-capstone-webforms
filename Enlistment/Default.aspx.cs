using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Enlistment_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Enlistment";
        Session["page"] = "Manage Records";
        if (!IsPostBack)
        {
            if (Session["typeid"].ToString() == "1")
                GetEnlistment();
            else
                GetEnlistment(Session["accountno"].ToString());
        }
    }

    void GetEnlistment()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT DISTINCT e.EnlistID, e.DateAdded, s.LastName + ', ' + s.FirstName AS Student,
                f.LastName + ', ' + f.FirstName AS Faculty,
                e.Status, s.AccountNo AS Code_Student, e.AccountNo,
                f.AccountNo AS Code_Faculty,
                (SELECT TOP 1 sa.Course FROM Students_Application sa
                WHERE sa.AccountNo = s.AccountNo) AS Course
                FROM Enlistment e
                INNER JOIN Students s ON e.AccountNo = s.AccountNo
                INNER JOIN Faculty f ON e.FacultyID = f.FacultyID
                WHERE e.Status!=@Status
                ORDER BY e.DateAdded DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvRecords.DataSource = data;
                    lvRecords.DataBind();
                }
            }
        }
    }

    void GetEnlistment(string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT DISTINCT e.EnlistID, e.DateAdded, s.LastName + ', ' + s.FirstName AS Student,
                f.LastName + ', ' + f.FirstName AS Faculty,
                e.Status, s.AccountNo AS Code_Student, e.AccountNo,
                f.AccountNo AS Code_Faculty,
                (SELECT TOP 1 sa.Course FROM Students_Application sa
                WHERE sa.AccountNo = s.AccountNo) AS Course
                FROM Enlistment e
                INNER JOIN Students s ON e.AccountNo = s.AccountNo
                INNER JOIN Faculty f ON e.FacultyID = f.FacultyID
                WHERE e.Status!=@Status AND f.AccountNo=@AccountNo
                ORDER BY e.DateAdded DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvRecords.DataSource = data;
                    lvRecords.DataBind();
                }
            }
        }
    }

    void ProcessEnlistment(string enlistID, string status,
        string remarks, string accountNo)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = "";
            if (status == "approve")
            {
                query = @"UPDATE Enlistment SET Status=@Status,
                    Remarks=@Remarks, DateModified=@DateModified
                    WHERE EnlistID=@EnlistID;
                    UPDATE Enlistment SET Status='Closed'
                    WHERE AccountNo=@AccountNo
                    AND Status!='Archived' AND EnlistID!=@EnlistID";
            }
            else
            {
                query = @"UPDATE Enlistment SET Status=@Status,
                    Remarks = @Remarks, DateModified = @DateModified
                    WHERE EnlistID = @EnlistID";
            }
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", status == "approve" ? "Approved" : "Disapproved");
                cmd.Parameters.AddWithValue("@Remarks", remarks);
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@EnlistID", enlistID);
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                cmd.ExecuteNonQuery();
                if (status == "approve")
                {
                    Helper.Log("Update", "Approved enlistment.");
                }
                else
                {
                    Helper.Log("Update", "Disapproved enlistment");
                }
            }
        }
    }

    protected void lvRecords_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltEnlistID = (Literal)e.Item.FindControl("ltEnlistID");
        Literal ltAccountNo = (Literal)e.Item.FindControl("ltAccountNo");
        TextBox txtRemarks = (TextBox)e.Item.FindControl("txtRemarks");
        string status = e.CommandName;

        if (status == "approve")
        {
            if (DB.TotalAdvisees() >= 10)
            {
                error.Visible = true;
            }
            else
            {
                ProcessEnlistment(ltEnlistID.Text, status, txtRemarks.Text, ltAccountNo.Text);
                update.Visible = true;
                if (Session["typeid"].ToString() == "1")
                    GetEnlistment();
                else
                    GetEnlistment(Session["accountno"].ToString());
            }
        }
        else
        {
            if (txtRemarks.Text.Trim() == "")
            {
                blank.Visible = true;
            }
            else
            {
                ProcessEnlistment(ltEnlistID.Text, status, txtRemarks.Text, "");
                update.Visible = true;
                if (Session["typeid"].ToString() == "1")
                    GetEnlistment();
                else
                    GetEnlistment(Session["accountno"].ToString());
            }
        }
        
    }
}