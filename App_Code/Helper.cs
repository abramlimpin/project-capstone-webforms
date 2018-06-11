using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Helper
{
    public static string GetCon()
    {
        return ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
    }
    public Helper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void ValidateUser()
    {
        if (HttpContext.Current.Session["accountno"] == null)
        {
            HttpContext.Current.Response.Redirect("~/Account/SignIn?url=" + HttpContext.Current.Request.Url.AbsoluteUri.Replace(".aspx", ""));
        }
    }

    public static void Log(string logType, string desc)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO Logs VALUES (@AccountID, @LogType, @Description, @LogDate)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountID", Encrypt(HttpContext.Current.Session["accountno"] == null ? "0" :
                    HttpContext.Current.Session["accountno"].ToString()));
                cmd.Parameters.AddWithValue("@LogType", Encrypt(logType));
                cmd.Parameters.AddWithValue("@Description", Encrypt(desc));
                cmd.Parameters.AddWithValue("@LogDate", DateTime.Now);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                
                }
            }
        }
    }

    public static string Hash(string keyword)
    {
        return Convert.ToBase64String(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(keyword)));
    }

    public static string Encrypt(string keyword)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(keyword);
        
        string key = "capstone";

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string Decrypt(string keyword)
    {
        byte[] keyArray;
        byte[] toEncryptArray = Convert.FromBase64String(keyword);

        string key = "capstone";

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        tdes.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    public static string ToRelativeDate(DateTime input)
    {
        TimeSpan oSpan = DateTime.Now.Subtract(input);
        double TotalMinutes = oSpan.TotalMinutes;
        string Suffix = " ago";

        if (TotalMinutes < 0.0)
        {
            TotalMinutes = Math.Abs(TotalMinutes);
            Suffix = " from now";
        }

        var aValue = new SortedList<double, Func<string>>();
        aValue.Add(0.75, () => "few seconds");
        aValue.Add(1.5, () => "1 min");
        aValue.Add(45, () => string.Format("{0} min", Math.Round(TotalMinutes)));
        aValue.Add(90, () => "an hour");
        aValue.Add(1440, () => string.Format("{0} hours", Math.Round(Math.Abs(oSpan.TotalHours)))); // 60 * 24
        aValue.Add(2880, () => "a day"); // 60 * 48
        aValue.Add(43200, () => string.Format("{0} days", Math.Floor(Math.Abs(oSpan.TotalDays)))); // 60 * 24 * 30
        aValue.Add(86400, () => "a month"); // 60 * 24 * 60
        aValue.Add(525600, () => string.Format("{0} months", Math.Floor(Math.Abs(oSpan.TotalDays / 30)))); // 60 * 24 * 365 
        aValue.Add(1051200, () => "a year"); // 60 * 24 * 365 * 2
        aValue.Add(double.MaxValue, () => string.Format("{0} years", Math.Floor(Math.Abs(oSpan.TotalDays / 365))));

        return aValue.First(n => TotalMinutes < n.Key).Value.Invoke() + Suffix;
    }

    public static string GetURL()
    {
        string URL = HttpContext.Current.Request.Url.Scheme + "://" +
            HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
        return URL;
    }


}