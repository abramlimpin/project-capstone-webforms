using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Porfolio_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Portfolio";
        Session["page"] = "Edit Portfolio Details";

        if (Request.QueryString["no"] == null)
            Response.Redirect("~/Account/Portfolio");
        else
        {
            Guid code = new Guid();
            bool validCode = Guid.TryParse(Request.QueryString["no"].ToString(), out code);
            if (validCode)
            {
                if (!IsPostBack)
                {
                    GetInfo(code);
                }
            }
            else
                Response.Redirect("~/Account/Portfolio");
        }
    }

    void GetInfo(Guid code)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT Title, Description, Keywords, Status,
                Image, DateStart, DateCompleted
                FROM Portfolio
                WHERE Code=@Code";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", code);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            txtTitle.Text = data["Title"].ToString();
                            txtDesc.Text = Server.HtmlDecode(data["Description"].ToString());
                            txtKeywords.Text = data["Keywords"].ToString();
                            cbStatus.Checked = data["Status"].ToString() == "Published" ? true : false;
                            imgPost.ImageUrl = "~/images/portfolio/" + data["Image"].ToString();
                            Session["image"] = data["Image"].ToString();
                            DateTime dateStart = DateTime.Now;
                            DateTime dateEnd = DateTime.Now;
                            bool validDateStart = DateTime.TryParse(data["DateStart"].ToString(), out dateStart);
                            bool validDateEnd = DateTime.TryParse(data["DateCompleted"].ToString(), out dateEnd);
                            if (validDateStart)
                                txtStart.Text = dateStart.ToString("yyyy-MM-dd");
                            if (validDateEnd)
                                txtEnd.Text = dateEnd.ToString("yyyy-MM-dd");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Account/Portfolio");
                    }
                }
            }
        }
    }

    void AddToMeta(int ID, string type, string value)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Portfolio_Meta VALUES
                (@PortfolioID, @Type, @Value, @DateAdded, @DateModified)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@PortfolioID", ID);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Value", value);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();
                Helper.Log("Add", "Added " + type + " '" + value + "'");
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Portfolio 
                SET Title=@Title, Description=@Description,
                Keywords=@Keywords, DateStart=@DateStart, 
                DateCompleted=@DateCompleted,
                Image=@Image, Status=@Status,
                DateModified=@DateModified
                WHERE Code=@Code AND AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                string code = Request.QueryString["no"].ToString();
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDesc.Text));
                cmd.Parameters.AddWithValue("@Keywords", txtKeywords.Text);
                cmd.Parameters.AddWithValue("@DateStart", txtStart.Text);
                cmd.Parameters.AddWithValue("@DateCompleted", txtEnd.Text);
                if (fuImage.HasFile)
                {
                    string ext = Path.GetExtension(fuImage.FileName);
                    cmd.Parameters.AddWithValue("@Image", Session["accountno"].ToString() + "-" + code + ext);
                    fuImage.SaveAs(Server.MapPath("~/images/portfolio/" + Session["accountno"].ToString() + "-" + code + ext));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Image", Session["image"].ToString());
                    Session.Remove("image");
                }
                cmd.Parameters.AddWithValue("@Status", cbStatus.Checked ? "Published" : "Draft");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.ExecuteNonQuery();
                Helper.Log("Updated", "Updated porfolio.");
                Session["add"] = "yes";
                Response.Redirect("~/Account/Portfolio");
            }
        }
    }
}