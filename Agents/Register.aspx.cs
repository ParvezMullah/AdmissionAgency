using System;
using System.Linq;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class Account_Register : Page
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
    protected void Register_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        string email = txtEmail.Text.ToString();
        // long mobile = Convert.ToInt64(txtMobile.Text);
        string mobile = txtMobile.Text.ToString();
        string password = Encrypt(txtPassword.Text.ToString());
        string firstName = txtFirstName.Text.ToString();
        string middleName = txtMiddleName.Text.ToString();
        string lastName = txtLastName.Text.ToString();
        string licenseNumber = txtLicenseNumber.Text.ToString();
        string gender = ddlGender.SelectedItem.ToString();
        string designation = txtDesignation.Text.ToString();
        string mailingAddress = txtMailAddress.Text.ToString();
        string country = txtCountry.Text.ToString();
        string highestEducation = ddlHighestEducation.SelectedItem.ToString();
        string experience = ddlExperience.SelectedItem.ToString();
        string location = txtLocation.Text.ToString();
        string ctc = txtCTC.Text.ToString();
        Stream fsResume = pdfResume.PostedFile.InputStream;
        Stream fsBusiness = pdfBusiness.PostedFile.InputStream;
        BinaryReader brIELTS = new BinaryReader(fsResume);
        Byte[] bytesResume = brIELTS.ReadBytes((Int32)fsResume.Length);
        BinaryReader brBusiness = new BinaryReader(fsBusiness);
        Byte[] bytesBusiness = brBusiness.ReadBytes((Int32)fsBusiness.Length);
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                string isExist = "select count(emailid) from tblAgent where emailid = @emailid";
                SqlCommand cmd1 = new SqlCommand(isExist, conn);
                cmd1.Parameters.AddWithValue("emailid", email);
                conn.Open();
                int count = Convert.ToInt32(cmd1.ExecuteScalar());
                if (count != 0)
                {
                    lblMsg.Text = "Email Id already exists!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                }
                else
                {
                    lblMsg.Visible = false;
                    string insert = "insert into tblAgent(emailId,Password,firstName,middleName,lastName,Gender,Phone,LicenseNo,Designation,country,highestEducation,Experience,location,CTC,resume,businessCard)"
                    + "values(@emailId,@Password,@firstName,@middleName,@lastName,@Gender,@Phone,@LicenseNo,@Designation,@country,@highestEducation,@Experience,@location,@CTC,@resume,@businessCard)";
                    SqlCommand cmd2 = new SqlCommand(insert, conn);
                    cmd2.Parameters.AddWithValue("emailId", email);
                    cmd2.Parameters.AddWithValue("Password", password);
                    cmd2.Parameters.AddWithValue("firstName", firstName);
                    cmd2.Parameters.AddWithValue("middleName", middleName);
                    cmd2.Parameters.AddWithValue("lastName", lastName);

                    cmd2.Parameters.AddWithValue("Gender", gender);
                    cmd2.Parameters.AddWithValue("LicenseNo", licenseNumber);
                    cmd2.Parameters.AddWithValue("Designation", designation);
                    cmd2.Parameters.AddWithValue("Phone", mobile);
                    cmd2.Parameters.AddWithValue("highestEducation", highestEducation);

                    cmd2.Parameters.AddWithValue("Experience", experience);
                    cmd2.Parameters.AddWithValue("location", location);
                    cmd2.Parameters.AddWithValue("CTC", ctc);
                    cmd2.Parameters.AddWithValue("resume", bytesResume);
                    cmd2.Parameters.AddWithValue("businessCard", bytesBusiness);
                    cmd2.Parameters.AddWithValue("country", country);
                    cmd2.ExecuteNonQuery();
                    lblMsg.Text = "Registration Successful!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Visible = true;
                }
            }
            catch (Exception exc)
            {
                //Response.Write(exc.StackTrace);
                lblMsg.Text = exc.Message.ToString();
                lblMsg.Visible = true;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}