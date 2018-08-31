using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Directory_Advisers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Directory";
        Session["page"] = "Advisers";

        GetTopics_Teaching();
        GetTopics_Research();
        GetDirections();
        GetAffiliations();

        if (Request.QueryString["t"] != null &&
            Request.QueryString["r"] != null &&
            Request.QueryString["a"] != null)
        {
            string[] topics_teaching = Request.QueryString["t"].ToString().Split(',');
            for (int x = 0; x < topics_teaching.Length; x++)
            {
                foreach (ListItem item in cbTopics_Teaching.Items)
                {
                    if (item.Value == topics_teaching[x])
                        item.Selected = true;
                }
            }

            string[] topics_research = Request.QueryString["r"].ToString().Split(',');
            for (int x = 0; x < topics_research.Length; x++)
            {
                foreach (ListItem item in cbTopics_Research.Items)
                {
                    if (item.Value == topics_research[x])
                        item.Selected = true;
                }
            }

            string[] directions = Request.QueryString["a"].ToString().Split(',');
            for (int x = 0; x < directions.Length; x++)
            {
                foreach (ListItem item in cbDirections.Items)
                {
                    if (item.Value == directions[x])
                        item.Selected = true;
                }
            }

            string[] affiliations = Request.QueryString["a"].ToString().Split(',');
            for (int x = 0; x < affiliations.Length; x++)
            {
                foreach (ListItem item in cbAffiliations.Items)
                {
                    if (item.Value == affiliations[x])
                        item.Selected = true;
                }
            }

            if (!IsPostBack)
            {
                GetAdvisers();
            }
        }
        else
        {
            if (!IsPostBack)
            {
                GetAdvisers();
            }
        }
        
    }

    void GetTopics_Teaching()
    {
        cbTopics_Teaching.DataSource = DB.GetTopics_Teaching();
        cbTopics_Teaching.DataTextField = "Name";
        cbTopics_Teaching.DataValueField = "RecordID";
        cbTopics_Teaching.DataBind();
    }

    void GetTopics_Research()
    {
        cbTopics_Research.DataSource = DB.GetTopics_Research();
        cbTopics_Research.DataTextField = "Name";
        cbTopics_Research.DataValueField = "RecordID";
        cbTopics_Research.DataBind();
    }

    void GetDirections()
    {
        cbDirections.DataSource = DB.GetDirections();
        cbDirections.DataTextField = "Name";
        cbDirections.DataValueField = "DirectID";
        cbDirections.DataBind();
    }

    void GetAffiliations()
    {
        cbAffiliations.DataSource = DB.GetAffiliations();
        cbAffiliations.DataTextField = "Name";
        cbAffiliations.DataValueField = "AffID";
        cbAffiliations.DataBind();
    }

    void GetAdvisers()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT DISTINCT(f.AccountNo), 
                f.LastName + ', ' + f.FirstName AS Name,
                f.Image
                FROM Faculty f
                WHERE f.Status = @Status
                ORDER BY f.AccountNo, Name, f.Image";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvAdvisers.DataSource = data;
                    lvAdvisers.DataBind();
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string[] topic_Teaching = cbTopics_Teaching.Items.Cast<ListItem>().Where(l => l.Selected)
            .Select(l => l.Value).ToArray();
        string selected_topic_teaching = String.Join(",", topic_Teaching);

        string[] topic_Research = cbTopics_Research.Items.Cast<ListItem>().Where(l => l.Selected)
            .Select(l => l.Value).ToArray();
        string selected_topic_research = String.Join(",", topic_Research);

        string[] affiliations = cbAffiliations.Items.Cast<ListItem>().Where(l => l.Selected)
            .Select(l => l.Value).ToArray();
        string select_affiliations = String.Join(",", affiliations);

        Response.Redirect("Advisers?t=" + selected_topic_teaching + "&r=" +
            selected_topic_research + "&a=" +
            select_affiliations);
    }

    protected void lvAdvisers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltImage = (Literal)e.Item.FindControl("ltImage");
            HtmlGenericControl ratio = (HtmlGenericControl)e.Item.FindControl("ratio");

            string URL = Helper.GetURL();
            if (ltImage.Text == "")
            {
                ratio.Attributes.Add("style", "background-image: url('" + URL + "images/user-placeholder.jpg" + "')");
            }
            else
            {
                ratio.Attributes.Add("style", "background-image: url('" + URL + "images/users/" + ltImage.Text + "')");
            }
        }
    }
}