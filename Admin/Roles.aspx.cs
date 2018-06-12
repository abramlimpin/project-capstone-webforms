using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Roles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateUser();
        Session["module"] = "Admin";
        Session["page"] = "Manage Roles";

        if (Session["add"] != null)
        {
            success.Visible = true;
            ltSuccess.Text = "Record added.";
            Session.Remove("add");
        }
        else if (Session["update"] != null)
        {
            success.Visible = true;
            ltSuccess.Text = "Record updated.";
            Session.Remove("update");
        }
        else
        {
            success.Visible = false;
        }

        if (!IsPostBack)
        {
            GetModules();
            GetRoles();
        }
    }


    void GetModules()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT ModuleID, Name FROM Modules
                ORDER BY Name";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    cbModules.DataSource = data;
                    cbModules.DataTextField = "Name";
                    cbModules.DataValueField = "ModuleID";
                    cbModules.DataBind();
                };
            }
        }
    }

    void AddRoleToModule(string roleID, string moduleID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Roles_Modules VALUES
                (@RoleID, @ModuleID, @DateAdded, @AccountNo)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RoleID", roleID);
                cmd.Parameters.AddWithValue("@ModuleID", moduleID);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@AccountNo", Session["accountno"] == null ? "0" : Session["accountno"].ToString());
                cmd.ExecuteNonQuery();
            }
        }
    }

    void DeleteAllModules(string roleID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"DELETE FROM Roles_Modules 
                WHERE RoleID=@RoleID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RoleID", roleID);
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Roles VALUES
                (@Code, @Name, @Description, @DateAdded, @DateModified, @Status);
                SELECT TOP 1 RoleID FROM Roles
                ORDER BY RoleID DESC;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Code", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Active");
                int roleID = (int)cmd.ExecuteScalar();

                foreach (ListItem item in cbModules.Items)
                {
                    if (item.Selected)
                    {
                        AddRoleToModule(roleID.ToString(), item.Value);
                    }
                }

                Helper.Log("Add", "Added role #" + roleID.ToString());
                Session["add"] = "yes";
                Response.Redirect("~/Admin/Roles");
            }
        }
    }

    void GetRoles()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RoleID, Name, Description, Status, DateAdded
                FROM Roles
                WHERE Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    lvRoles.DataSource = data;
                    lvRoles.DataBind();
                }
            }
        }
    }

    void DisplayModules(string roleID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT ModuleID FROM Roles_Modules
                WHERE RoleID=@RoleID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RoleID", roleID);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    while (data.Read())
                    {
                        foreach (ListItem item in cbModules.Items)
                        {
                            if (item.Value == data["ModuleID"].ToString())
                                item.Selected = true;
                        }
                    }
                }
            }
        }
    }

    protected void lvRoles_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltRoleID = (Literal)e.Item.FindControl("ltRoleID");

        if (e.CommandName == "manage")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT RoleID, Name, Description, Status
                    FROM Roles
                    WHERE RoleID=@RoleID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RoleID", ltRoleID.Text);
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            ltRole.Text = data["RoleID"].ToString();
                            txtName.Text = data["Name"].ToString();
                            txtDesc.Text = data["Description"].ToString();
                            cboStatus.Checked = data["Status"].ToString() == "Active" ? true: false;
                        }
                    }
                }
            }

            DisplayModules(ltRoleID.Text);
            status.Visible = true;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
        }
        else if (e.CommandName == "archive")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE Roles SET Status=@Status
                    WHERE RoleID=@RoleID;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "Archived");
                    cmd.Parameters.AddWithValue("@RoleID", ltRoleID.Text);
                    cmd.ExecuteNonQuery();

                    Helper.Log("Archive", "Archived role #" + ltRoleID.Text);
                    Session["update"] = "yes";
                    Response.Redirect("~/Admin/Roles");
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Roles");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE Roles SET Name=@Name, Description=@Description,
                Status=@Status, DateModified=@DateModified
                WHERE RoleID=@RoleID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                cmd.Parameters.AddWithValue("@Status", cboStatus.Checked ? "Active" : "Inactive");
                cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmd.Parameters.AddWithValue("@RoleID", ltRole.Text);
                cmd.ExecuteNonQuery();

                DeleteAllModules(ltRole.Text);

                foreach (ListItem item in cbModules.Items)
                {
                    if (item.Selected)
                    {
                        AddRoleToModule(ltRole.Text, item.Value);
                    }
                }
                Helper.Log("Update", "Updated role #" + ltRole.Text);
                Session["update"] = "yes";
                Response.Redirect("~/Admin/Roles");
            }
        }
    }
}