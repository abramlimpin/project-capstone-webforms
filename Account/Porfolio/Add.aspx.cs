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
        Session["module"] = "Account";
        Session["page"] = "Add a Portfolio";
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
            string query = @"INSERT INTO Portfolio VALUES
                (@Code, @AccountNo, @Title, @Description,
                @Keywords, @DateStart, @DateCompleted,
                @Image, @Status, @DateAdded, @DateModified);
                SELECT TOP 1 PortfolioID FROM Portfolio
                ORDER BY PortfolioID DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                string code = Guid.NewGuid().ToString();
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"].ToString());
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Description", Server.HtmlEncode(txtDesc.Text));
                cmd.Parameters.AddWithValue("@Keywords", txtKeywords.Text);
                cmd.Parameters.AddWithValue("@DateStart", txtStart.Text);
                cmd.Parameters.AddWithValue("@DateCompleted", txtEnd.Text);
                string ext = Path.GetExtension(fuImage.FileName);
                cmd.Parameters.AddWithValue("@Image", Session["accountno"].ToString() + "-" + code + ext);
                fuImage.SaveAs(Server.MapPath("~/images/portfolio/" + Session["accountno"].ToString() + "-" + code + ext));
                cmd.Parameters.AddWithValue("@Status", cbStatus.Checked ? "Published" : "Draft");
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                int porfolioID = (int)cmd.ExecuteScalar();
                if (txtTypology.Text != "")
                    AddToMeta(porfolioID, "Typology", txtTypology.Text);
                if (txtFunction.Text != "")
                    AddToMeta(porfolioID, "Function", txtFunction.Text);
                if (txtLocation.Text != "")
                    AddToMeta(porfolioID, "Location", txtLocation.Text);
                if (txtCustom1.Text != "" && txtCustom1_Value.Text != "" )
                    AddToMeta(porfolioID, txtCustom1.Text, txtCustom1_Value.Text);
                if (txtCustom2.Text != "" && txtCustom2_Value.Text != "")
                    AddToMeta(porfolioID, txtCustom2.Text, txtCustom2_Value.Text);
                Helper.Log("Add", "Added porfolio #" + porfolioID.ToString());
                Session["add"] = "yes";
                Response.Redirect("~/Account/Portfolio");
            }
        }
    }
}