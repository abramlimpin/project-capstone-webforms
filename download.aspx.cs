using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["f"] == null)
        {
            Response.Redirect("~/");
        }
        else
        {
            Guid code = new Guid();
            bool validFile = Guid.TryParse(Request.QueryString["f"].ToString(), out code);
            if (validFile)
            {
                DisplayFile(code);
            }
            else
            {
                Response.Redirect("~/");
            }
        }
    }

    void DisplayFile(Guid code)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT FileName FROM Uploads
                WHERE Code=@Code";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", code);
                if (cmd.ExecuteScalar() == null)
                {
                    Response.Redirect("~/");
                }
                else
                {
                    Response.Redirect("~/uploads/" + (string)cmd.ExecuteScalar());
                }
            }
        }
    }
}