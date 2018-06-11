using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class News_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "News";
        Session["page"] = "Add a Post";

        if (Request.QueryString["id"] == null)
            Response.Redirect("~/News");
        else
        {
            Guid code = new Guid();
            bool validCode = Guid.TryParse(Request.QueryString["id"].ToString(), out code);
            if (validCode)
            {
                if (!IsPostBack)
                {
                    GetInfo(code);
                }
            }
            else
                Response.Redirect("~/News");
        }
    }

    void GetInfo(Guid code)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT Type, Title, Post
                FROM News
                WHERE Status!=@Status AND Code=@Code";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                cmd.Parameters.AddWithValue("@Code", code);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            ddlTypes.SelectedValue = data["Type"].ToString();
                            txtTitle.Text = data["Title"].ToString();
                            txtPost.Text = Server.HtmlDecode(data["Post"].ToString());
                        }
                    }
                    else
                        Response.Redirect("~/News");
                }
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using(SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE News SET Type=@Type,
                Title=@Title, Post=@Post, DateModified=@DateModified,
                Status=@Status
                WHERE Code=@Code";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Type", ddlTypes.SelectedValue);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Post", Server.HtmlEncode(txtPost.Text));
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@Status", "Published");
                cmd.Parameters.AddWithValue("@Code", Request.QueryString["id"].ToString());
                cmd.ExecuteNonQuery();
                Helper.Log("Update", "Updated post id '" + ltNewsID.Text + "'.");
                Session["add"] = "yes";
                Response.Redirect("~/News");
            }
        }
    }
}