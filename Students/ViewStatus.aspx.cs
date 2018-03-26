using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class Students_ViewStatus : System.Web.UI.Page
{
    string applicationquery = "select University,fieldOfInterest,programOfInterest,Agent,applicationStatus from tblApplications where emailid=@emailid order by applicationid desc";
    string visaquery = "select University,fieldOfInterest,programOfInterest,visaStatus,scheduledVisaAppointment,expectedVisaArrival from tblApplications where emailid=@emailid order by applicationid desc";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            showData(applicationquery);
        }
    }

    protected void showData(string query)
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@emailid", "rohinchuran@gmail.com");
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }
        }
    }
    protected void radioVisa_CheckedChanged(object sender, EventArgs e)
    {
        showData(visaquery);
    }
    protected void radioApplication_CheckedChanged(object sender, EventArgs e)
    {
        showData(applicationquery);
    }
    
}