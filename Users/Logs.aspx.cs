using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Logs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Helper.ValidateAdmin();

        Session["module"] = "Admin";
        Session["page"] = "Audit Logs";

        if (!IsPostBack)
        {
            GetLogs();
        }
    }

    void GetLogs()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AccountNo, LogType, Description, LogDate
                FROM Logs";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvRecords.DataSource = data;
                    lvRecords.DataBind();
                }
            }
        }
    }

    protected void lvRecords_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltDesc = (Literal)e.Item.FindControl("ltDesc");
            Literal ltDesc_Enc = (Literal)e.Item.FindControl("ltDesc_Enc");

            try
            {
                ltDesc.Text = Helper.Decrypt(ltDesc_Enc.Text);
            }
            catch
            {
                ltDesc.Text = "";
            }
        }
    }
}