using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Agents_ViewStudentsaspx : System.Web.UI.Page
{
    protected void showData(string query,string Search="")
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                if (Search != "")
                {
                    cmd.Parameters.AddWithValue("@emailid", Search);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@emailid", "rohinchuran@gmail.com");
                }
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }
        }
    }
    string currentStudentsQuery = "select distinct tblstudent.emailId,firstName,middleName,formerLastName,preferredFirstName,familyName,Gender,Dob,Phone,Nationality,immigrationStatus,firstSpokenLanguage,currentAddress,permanentAddress,nameOfHighSchool,dateOfSchoolLeaving,nameOfPostSecondary,highestEducaion,fieldOfStudy,lastAttendedPostSecondary,IELTSResult,termToBegin from  tblstudent,tblApplications where tblApplications.agent=@emailid and applicationStatus='hold'";
    string PreviousStudentsQuery = "select distinct tblstudent.emailId,firstName,middleName,formerLastName,preferredFirstName,familyName,Gender,Dob,Phone,Nationality,immigrationStatus,firstSpokenLanguage,currentAddress,permanentAddress,nameOfHighSchool,dateOfSchoolLeaving,nameOfPostSecondary,highestEducaion,fieldOfStudy,lastAttendedPostSecondary,IELTSResult,termToBegin from  tblstudent,tblApplications where tblApplications.agent=@emailid and applicationStatus='approved' or applicationStatus='declined'";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            showData(currentStudentsQuery);
        }
    }
    protected void radioCurrentStudents_CheckedChanged(object sender, EventArgs e)
    {
        showData(currentStudentsQuery);
    }
    protected void radioPreviousStudents_CheckedChanged(object sender, EventArgs e)
    {
        showData(PreviousStudentsQuery);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string searchQuery = "Select distinct tblstudent.emailId,firstName,middleName,formerLastName,preferredFirstName,familyName,Gender,Dob,Phone,Nationality,immigrationStatus,firstSpokenLanguage,currentAddress,permanentAddress,nameOfHighSchool,dateOfSchoolLeaving,nameOfPostSecondary,highestEducaion,fieldOfStudy,lastAttendedPostSecondary,IELTSResult,termToBegin from  tblstudent where emailid=@emailid";
        showData(searchQuery,txtemail.Text.ToString());
    }
}