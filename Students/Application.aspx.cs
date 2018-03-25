using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Application : System.Web.UI.Page
{
    string conn = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = "select * from tblFieldOfInterest;";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                ddField.DataSource = dt;
                ddField.DataBind();

                ddField.DataTextField = "fieldName";
                ddField.DataValueField = "fieldName";
                ddField.DataBind();
                MultiView1.ActiveViewIndex = 0;
            }
        }
        
    }
    protected void ddField_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = "select ProgramName from tblProgramOfInterest where fieldName='" + ddField.SelectedItem.ToString() + "';";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                ddProgram.DataSource = dt;
                ddProgram.DataBind();

                ddProgram.DataTextField = "ProgramName";
                ddProgram.DataValueField = "ProgramName";
                ddProgram.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void ddUniversity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddUniversity.SelectedIndex > 0)
        {
            // Response.Write(ddUniversity.SelectedItem);
            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = "select TOP 1 max((successCount/totalattempt)*100) as HighestRate, tblSuccessRate.emailId from tblSuccessRate, tblUniversities " +
                    "where tblUniversities.emailId=tblSuccessRate.emailId and tblUniversities.University='" + ddUniversity.SelectedItem.ToString() + "' group by tblSuccessRate.emailId " +
                    "order by HighestRate DESC;";
                SqlCommand cmd = new SqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@university",ddUniversity.SelectedItem.ToString());
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int successRate;
                string emailId;
                if (dr.Read())
                {
                    lblAgentSuccessRate.Text = Convert.ToInt32(dr[0]).ToString() + "%";
                    lblAgentEmail.Text = dr[1].ToString();
                    emailId = dr[1].ToString();
                    dr.Close();
                    string queryAgent = "select firstname,middlename,lastname,gender,phone from tblAgent where emailid='" + emailId + "';";
                    SqlCommand cmd2 = new SqlCommand(queryAgent, con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        lblAgentName.Text = dr2[0].ToString() + " " + dr2[1].ToString() + " " + dr2[2].ToString();
                        lblAgentGender.Text = dr2[3].ToString();
                        lblAgentPhone.Text = dr2[4].ToString();
                        divAgent.Style.Add("display", "block");
                    }
                    dr2.Close();
                }
            }
        }
    }
    protected void rdOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdOptions.SelectedIndex == 1)
        {
            btnSubmitRecommended.Visible = false;
            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = "select max((successCount/totalattempt)*100) as HighestRate, tblSuccessRate.emailId from tblSuccessRate,tblUniversities " +
                    "where tblUniversities.emailId=tblSuccessRate.emailId and tblSuccessRate.emailId<>'" + lblAgentEmail.Text + "' " +
                    "and tblUniversities.University='" + ddUniversity.SelectedItem.ToString() + "' group by tblSuccessRate.emailId order by HighestRate DESC;";
                //SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                string queryAgent = "select distinct firstname,middlename,lastname,tblAgent.emailid,gender,phone,CEILING(successcount*100/totalattempt) as successrate from tblAgent,tblSuccessRate,tblUniversities where tblAgent.emailid=tblsuccessrate.emailid and tblUniversities.university='" + ddUniversity.SelectedItem.ToString() + "' order by successrate desc;";
                SqlCommand cmd = new SqlCommand(queryAgent, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                List<string> li = new List<String>();
                while (sdr.Read())
                {
                    li.Add(sdr["emailid"].ToString());
                }
                con.Close();
                ddlPrefferedAgent.DataSource = li;
                //ddlPrefferedAgent.DataValueField = "emailid";
                //ddlPrefferedAgent.DataTextField = "emailid";
                ddlPrefferedAgent.DataBind();
                ddlPrefferedAgent.Items.Insert(0, new ListItem("--Select Preffered Agent--", "0"));

                con.Open();
                string queryAgent1 = "select distinct firstname,middlename,lastname,tblAgent.emailid,gender,phone,CEILING(successcount*100/totalattempt) as successrate from tblAgent,tblSuccessRate,tblUniversities where tblAgent.emailid=tblsuccessrate.emailid and tblUniversities.university='" + ddUniversity.SelectedItem.ToString() + "' order by successrate desc;";
                SqlCommand cmd1 = new SqlCommand(queryAgent, con);
                gvAgents.DataSource = cmd1.ExecuteReader();
                gvAgents.DataBind();
                con.Close();
                divAgentList.Style.Remove("display");
                btnSubmit.Visible = true;
                //SqlDataAdapter sda = new SqlDataAdapter(query, con);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);

                ////gvAgents.DataSource = dt;
                ////gvAgents.DataBind();
                //List<string> li = new List<string>();
                //if (dt.Rows.Count > 0)
                //{
                //    System.Web.UI.HtmlControls.HtmlGenericControl agentDivs = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                //    int i = 0;
                //    DataTable masterData = new DataTable();
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        string emailId = dr[1].ToString();
                //        li.Add(emailId);
                //        //Response.Write(emailId);
                //        string successRate = Convert.ToInt32(dr[0]).ToString() + "%";
                //        System.Web.UI.HtmlControls.HtmlGenericControl anAgentDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                //        //dr.Close();
                //        string queryAgent = "select distinct firstname,middlename,lastname,tblAgent.emailid,gender,phone,CEILING(successcount*100/totalattempt) as successrate from tblAgent,tblSuccessRate,tblUniversities where tblAgent.emailid=tblsuccessrate.emailid and and tblUniversities.university='"+ddUniversity.SelectedItem.ToString()+"';";
                //        SqlCommand cmd2 = new SqlCommand(queryAgent, con);
                //        //dr.Close();
                //        SqlDataAdapter sda2 = new SqlDataAdapter(queryAgent, con);
                //        // DataTable dt2 = new DataTable();
                //        //masterData.Rows.Add(sda2);
                //       //sda2.Fill(masterData);
                //    }
                //    gvAgents.DataSource = masterData;
                //    gvAgents.DataBind();
                //    li.Insert(0, "--Select Your Preffered Agent!--");
                //    ddlPrefferedAgent.DataSource = li;
                //    ddlPrefferedAgent.DataBind();
                //}
            }
        }
        else
        {
            btnSubmitRecommended.Visible = true;
            btnSubmit.Visible = false;
            divAgentList.Visible = false;
        }
    }
    protected void setPage(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == 1)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = "select distinct University from tblUniversities;";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                ddUniversity.DataSource = dt;
                ddUniversity.DataBind();

                ddUniversity.DataTextField = "University";
                ddUniversity.DataValueField = "University";
                ddUniversity.DataBind();
                ListItem li = new ListItem("--Select University--", "-1");
                ddUniversity.Items.Insert(0, li);
                //MultiView1.ActiveViewIndex = 0;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void btnSubmitRecommended_Click(object sender, EventArgs e)
    {

    }
}