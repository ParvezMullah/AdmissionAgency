using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class Agents_AgentLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    protected void StudLogin(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        string email = Email.Text.ToString();
        string password = Encrypt(Password.Text.ToString());
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "select count(emailid) from tblagent where emailid = @email and password=@password";
            SqlCommand cmd1 = new SqlCommand(query, conn);
            cmd1.Parameters.AddWithValue("email", email);
            cmd1.Parameters.AddWithValue("password", password);
            try
            {
                conn.Open();
                int result = (int)cmd1.ExecuteScalar();
                if (result > 0)
                {
                    //Session["UserMobile"] = mobile;
                    Response.Redirect("https://www.google.com");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}