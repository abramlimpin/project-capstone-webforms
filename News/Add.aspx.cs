using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class News_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["module"] = "News";
        Session["page"] = "Add a Post";
    }

    protected void btnPublish_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            int newsID = 0;
            con.Open();
            string query = @"INSERT INTO News VALUES
                (@Code, @Type, @AccountNo, @Title, @Post,
                @DateAdded, @DateModified, @Status);
                SELECT TOP 1 NewsID FROM News
                ORDER BY NewsID DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@Type", ddlTypes.SelectedValue);
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"] == null ? "0" : Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Post", Server.HtmlEncode(txtPost.Text));
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Published");
                newsID = (int)cmd.ExecuteScalar();
                Helper.Log("Add", "Published post id '" + newsID.ToString() + "'.");
                Session["add"] = "yes";
                Response.Redirect("~/News");
            }
        }
    }
}