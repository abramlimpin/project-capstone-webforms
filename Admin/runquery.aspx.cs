using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class runquery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["code"] != null)
        {
            if (Request.QueryString["code"] != "abramlimpin")
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        string SQL = txtQuery.Text;
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                try
                {
                    Response.Write("OK");
                    DataSet result;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        result = new DataSet();
                        adapter.Fill(result);
                    }

                    if (result.DefaultViewManager.DataViewSettingCollectionString.Contains("Table") == true)
                    {
                        gvResults.Visible = true;
                        gvResults.DataSource = result;
                        gvResults.DataBind();
                    }
                    if (gvResults.Rows.Count == 1 && gvResults.Rows[0].Cells.Count == 1)
                    {
                        string text = (gvResults.Rows[0].Cells[0]).Text;
                        if (text.Contains("row(s) affected"))
                        {
                            Response.Write("\n" + text);
                            gvResults.DataSource = null;
                            gvResults.DataBind();
                        }
                    }
                }
                catch (Exception error)
                {
                    Response.Write(error.Message);
                }
            }
        }
    }
}