using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class AgentRating : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        string showQuery = "select emailid,successcount,totalattempt from tblsuccessrate";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(showQuery, conn);
            try
            {
                conn.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string updateQuery = "update tblagent set priority = @priority where emailid= @emailid";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(updateQuery, conn);
            cmd.Parameters.AddWithValue("@priority", ddlPriority.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@emailid",txtemailid.Text.ToString());

            try
            {
                conn.Open();
                int count = cmd.ExecuteNonQuery();
                if(count > 0)
                {
                    lblMsg.Text = "Priority updated successfully!!";
                }
                else
                {
                    lblMsg.Text = "Invalid Emailid";
                }
            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }
        }
    }

}