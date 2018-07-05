using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DB
/// </summary>
public class DB
{
    public DB()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool IsAccountExisting(string accountID)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AccountNo FROM Account
                WHERE AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", accountID);
                return cmd.ExecuteScalar() == null ? false : true;
            }
        }
    }

    public static string CountRecords(string table)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT COUNT(*) FROM " + table + " WHERE Status!=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Archived");
                return ((int)cmd.ExecuteScalar()).ToString();
            }
        }
    }

    public static string CountRecords_Advisees()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT COUNT(fa.AssignID) FROM Faculty_Assignments fa
                INNER JOIN Faculty f ON fa.FacultyID = f.FacultyID
                WHERE f.AccountNo=@AccountNo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNo", HttpContext.Current.Session["accountno"].ToString());
                return ((int)cmd.ExecuteScalar()).ToString();
            }
        }
    }

    public static DataTable GetFaculty()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT FacultyID, LastName + ', ' + FirstName AS Name 
                FROM Faculty
                WHERE Status=@Status
                ORDER BY LastName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetTypes()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT TypeID, UserType FROM Account_Type";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetSchools()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT SchoolID, Name FROM Schools";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetRoles()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RoleID, Name FROM Roles";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetPrograms()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT ProgramID, ProgramCode FROM Programs";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetTopics_Teaching()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID, Name FROM Topics_Teaching
                WHERE Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetTopics_Research()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT RecordID, Name FROM Topics_Research
                WHERE Status=@Status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }

    public static DataTable GetAffiliations()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT AffID, Name FROM Affiliations";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }
}