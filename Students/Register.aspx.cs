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
        string lastName = txtFormerLastName.Text.ToString();
        string prefferedFirstName = txtPrefferedFirstName.Text.ToString();
        string familyName = txtFamilyName.Text.ToString();
        string gender = ddlGender.SelectedItem.ToString();
        string dob = txtDob.Text.ToString();
        string nationality = txtNationality.Text.ToString();
        string immigrationStatus = txtImmigrationStatus.Text.ToString();
        string firstLanguage = txtFirstLanguageSpoke.Text.ToString();
        string currentAddress = txtCurrentAddress.Text.ToString();
        string permanentAddres = txtPermanentAddress.Text.ToString();
        string highSchool = txtNameofHighSchool.Text.ToString();
        string dateOfHighSchool = txtDateOFSchoolLeaving.Text.ToString();
        string postSecondarySchool = txtNameofPostSecondarySchool.Text.ToString();
        string dateOfPostSecondary = DatelastattendedPostsecondary.Text.ToString();
        string highestEducation = ddlHighestEducation.SelectedItem.ToString();
        string field = ddlField.SelectedItem.ToString();
        //string IELTS = txtIELTSresult.Text.ToString();
        Stream fsIELTS = pdfIELTS.PostedFile.InputStream;
        string term = txtTerm.Text.ToString();
        BinaryReader br = new BinaryReader(fsIELTS);
        Byte[] bytesIELTS = br.ReadBytes((Int32)fsIELTS.Length);
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                string isExist = "select count(emailid) from tblstudent where emailid = @emailid";
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
                    string insert = "insert into tblstudent(emailId,Password,firstName,middleName,formerLastName,preferredFirstName,familyName,Gender,Dob,Phone,Nationality,immigrationStatus,firstSpokenLanguage,currentAddress,permanentAddress,nameOfHighSchool,dateOfSchoolLeaving,nameOfPostSecondary,highestEducaion,fieldOfStudy,lastAttendedPostSecondary,IELTSResult,termToBegin)"
                    + "values(@emailId,@Password,@firstName,@middleName,@formerLastName,@preferredFirstName,@familyName,@Gender,@Dob,@Phone,@Nationality,@immigrationStatus,@firstSpokenLanguage,@currentAddress,@permanentAddress,@nameOfHighSchool,@dateOfSchoolLeaving,@nameOfPostSecondary,@highestEducaion,@fieldOfStudy,@lastAttendedPostSecondary,@IELTSResult,@termToBegin)";
                    SqlCommand cmd2 = new SqlCommand(insert, conn);
                    cmd2.Parameters.AddWithValue("emailId", email);
                    cmd2.Parameters.AddWithValue("Password", password);
                    cmd2.Parameters.AddWithValue("firstName", firstName);
                    cmd2.Parameters.AddWithValue("middleName", middleName);
                    cmd2.Parameters.AddWithValue("formerLastName", lastName);

                    cmd2.Parameters.AddWithValue("preferredFirstName", prefferedFirstName);
                    cmd2.Parameters.AddWithValue("familyName", familyName);
                    cmd2.Parameters.AddWithValue("Gender", gender);
                    cmd2.Parameters.AddWithValue("Dob", dob);
                    cmd2.Parameters.AddWithValue("Phone", mobile);
                    cmd2.Parameters.AddWithValue("Nationality", nationality);

                    cmd2.Parameters.AddWithValue("immigrationStatus", immigrationStatus);
                    cmd2.Parameters.AddWithValue("firstSpokenLanguage", firstLanguage);
                    cmd2.Parameters.AddWithValue("currentAddress", currentAddress);
                    cmd2.Parameters.AddWithValue("permanentAddress", permanentAddres);
                    cmd2.Parameters.AddWithValue("nameOfHighSchool", highSchool);

                    cmd2.Parameters.AddWithValue("dateOfSchoolLeaving", dateOfHighSchool);
                    cmd2.Parameters.AddWithValue("nameOfPostSecondary", postSecondarySchool);
                    cmd2.Parameters.AddWithValue("highestEducaion", highestEducation);
                    cmd2.Parameters.AddWithValue("fieldOfStudy", field);
                    cmd2.Parameters.AddWithValue("lastAttendedPostSecondary", dateOfPostSecondary);
                    cmd2.Parameters.AddWithValue("IELTSResult", bytesIELTS);
                    cmd2.Parameters.AddWithValue("termToBegin", term);
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