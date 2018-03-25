using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Agents_ViewDocuments : System.Web.UI.Page
{
    protected void showData(string query, string Search = "")
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
                    cmd.Parameters.AddWithValue("@studentMail", Search);
                    cmd.Parameters.AddWithValue("@emailid", "rohinchuran@gmail.com");
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

    string currentStudentsQuery = "select distinct tblApplications.emailId,Passport,Transcript,docOfFamilyWealth,IELTSResults,tblApplications.visaStatus from tblApplications,tbldocuments where tblApplications.agent=@emailid and tbldocuments.emailid= tblApplications.emailid and applicationStatus='hold'";
    string PreviousStudentsQuery = "select distinct tblApplications.emailId,Passport,Transcript,docOfFamilyWealth,IELTSResults,tblApplications.visaStatus from tblApplications,tbldocuments where tblApplications.agent=@emailid and tbldocuments.emailid= tblApplications.emailid  and applicationStatus='approved' or applicationStatus='declined'";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        showData(currentStudentsQuery);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string searchQuery = "select distinct tblApplications.emailId,Passport,Transcript,docOfFamilyWealth,IELTSResults,tblApplications.visaStatus from tblApplications,tbldocuments where tbldocuments.emailid=@studentMail";
        showData(searchQuery, txtemail.Text.ToString());
    }
    protected void radioCurrentStudents_CheckedChanged(object sender, EventArgs e)
    {
        showData(currentStudentsQuery);
    }
    protected void radioPreviousStudents_CheckedChanged(object sender, EventArgs e)
    {
        showData(PreviousStudentsQuery);
    }
}