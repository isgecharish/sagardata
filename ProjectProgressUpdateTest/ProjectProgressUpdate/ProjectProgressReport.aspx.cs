using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
namespace ProjectProgressUpdateTest
{
   
    public partial class ProjectProgressReport : System.Web.UI.Page
    {
        public static string cs = ConfigurationManager.AppSettings["ConnectionTest"];
        public static string csLive = ConfigurationManager.AppSettings["ConnectionLive"];
        protected void page_load(object s, EventArgs e)
        {
            string sUsername = (string)(Session["Username"]);
            if (!IsPostBack)
            {
                string sProjId = (string)(Session["projId"]);
                int nDay = (int)(Session["day"]);
                string sProjname = (string)(Session["projName"]);
                lblProjDetail.Text = sProjId + " - " + sProjname;
                lblActivityShow.Visible = true;
                lblActivityShow.Text = "Activities to be started upto " + (DateTime.Today.AddDays(nDay)).ToString("dd-MM-yyyy");
                List<Project> tmp = GetProject(sUsername,sProjId, nDay);
                if (tmp != null)
                {
                    string str = "";
                    str += "<table class='table'><thead><tr class='btn-dark'>";
                    str += "<td>Activity </td><td>Schedule Start Date</td><td>Scheduled Finish Date</td>";
                    str += "</tr></thead>";
                    foreach (Project x in tmp)
                    {
                        if (x.sRemarks != string.Empty || ((x.ActualStartDate).ToString("dd-mm-yyyy") != ("01-00-1900") && (x.ActualStartDate).ToString("dd-mm-yyyy") != ("01-00-1753")))
                        {
                            str += "<tr class='table-info'><td><a style='text-decoration:none; color: black'  target='_blank'  href='UpdateProjectProgress.aspx?cprj=" + sProjId + "&cact=" + x.ActivityID + "'>" + x.Activity + "</a></td><td>" + x.ScheduledStartDate.ToString("dd/MM/yyyy") + "</td><td>" + x.ScheduledFinishedDate.ToString("dd/MM/yyyy") + "</td></tr>";
                        }
                        else
                        {
                           
                            str += "<tr class='table-warning'><td><a style='text-decoration:none; color: black'  target='_blank'  href='UpdateProjectProgress.aspx?cprj=" + sProjId + "&cact=" + x.ActivityID + "'>" + x.Activity + "</a></td><td>" + x.ScheduledStartDate.ToString("dd/MM/yyyy") + "</td><td>" + x.ScheduledFinishedDate.ToString("dd/MM/yyyy") + "</td></tr>";
                        }
                    }
                    str += "</table>";
                    mydiv.InnerHtml = str;
                }
            }
            else
            {
                mydiv.Visible = false;
            }
            if (sUsername == "isgec")
            {
                lblTestServer.Visible = true;
                lblTestServer.Text = "Test Server";
            }
            else
            {
                lblTestServer.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sProjId = (string)(Session["projId"]);
            int nDay = (int)(Session["day"]);
            string sProjname = (string)(Session["projName"]);
            string sUsername = (string)(Session["Username"]);
            lblProjDetail.Text = sProjId + " - " + sProjname;
            lblActivityShow.Text = "Activities to be started upto " + (DateTime.Today.AddDays(nDay)).ToString("dd-MM-yyyy");
            string search = txtDsc.Text;
            string conStrng=string.Empty;
            List<Project> ProjectDetails = new List<Project>();
            if (sUsername == "isgec")
            { 
                conStrng = cs;
            }
            else
            { 
            conStrng = csLive;
            }
            using (SqlConnection con = new SqlConnection(conStrng))
            {
                string SearchActivity = @"select top 100 a.t_cprj, a.t_remk, a.t_cact, a.t_sdst, a.t_sdfn, a.t_acsd, a.t_acfn,a.t_dsca, b.t_dsca from ttpisg910200 a inner join ttcmcs052200 b on a.t_cprj=b.t_cprj WHERE  a.t_cprj = '" + sProjId + "' and UPPER(a.t_dsca) like '%" + search.ToUpper() + "%'  and UPPER(a.t_pact) not in ('PARENT') and t_sdst <= (GETDATE() +  30) order by a.t_sdst asc";
                SqlCommand cmd = new SqlCommand(SearchActivity, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    lblActivityShow.Visible = true;
                    mydiv.Visible = true;
                    Label5.Visible = false;
                    while (rdr.Read())
                    {
                        Project projDetail = new Project();
                        projDetail.Activity = (rdr["t_dsca"]).ToString();
                        projDetail.ActivityID = (rdr["t_cact"]).ToString();
                        projDetail.ScheduledStartDate = Convert.ToDateTime(rdr["t_sdst"]);
                        projDetail.ScheduledFinishedDate = Convert.ToDateTime(rdr["t_sdfn"]);
                        projDetail.ActualStartDate = Convert.ToDateTime(rdr["t_acsd"]);
                        projDetail.ActualFinishDate = Convert.ToDateTime(rdr["t_acfn"]);
                        projDetail.sRemarks = (rdr["t_remk"]).ToString();
                        ProjectDetails.Add(projDetail);
                    }
                    List<Project> tmp = ProjectDetails;
                    string str = "";
                    str += "<table class='table'><thead><tr class='btn-dark'>";
                    str += "<td>Activity</td><td>Schedule Start Date</td><td>Scheduled Finish Date</td>";
                    str += "</tr></thead>";
                    foreach (Project x in tmp)
                    {
                        if ((x.ActualFinishDate).ToString("dd-mm-yyyy") != ("01-00-1900") && (x.ActualFinishDate).ToString("dd-mm-yyyy") != ("01-00-1753"))
                        {
                            str += "<tr class='table-success'><td><a style='text-decoration:none; color: black'  target='_blank'  href='UpdateProjectProgress.aspx?cprj=" + sProjId + "&cact=" + x.ActivityID + "'>" + x.Activity + "</a></td><td>" + x.ScheduledStartDate.ToString("dd/MM/yyyy") + "</td><td>" + x.ScheduledFinishedDate.ToString("dd/MM/yyyy") + "</td></tr>";
                        }
                        else if ((x.sRemarks != string.Empty || ((x.ActualStartDate).ToString("dd-mm-yyyy") != ("01-00-1900") && (x.ActualStartDate).ToString("dd-mm-yyyy") != ("01-00-1753"))))
                        {
                            str += "<tr class='table-info'><td><a style='text-decoration:none; color: black' target='_blank' href='UpdateProjectProgress.aspx?cprj=" + sProjId + "&cact=" + x.ActivityID + "'> " + x.Activity + "</a></td><td>" + x.ScheduledStartDate.ToString("dd/MM/yyyy") + "</td><td>" + x.ScheduledFinishedDate.ToString("dd/MM/yyyy") + "</td></tr>";
                        }
                        else
                        {
                            str += "<tr class='table-warning'><td><a style='text-decoration:none; color: black'  target='_blank'  href='UpdateProjectProgress.aspx?cprj=" + sProjId + "&cact=" + x.ActivityID + "'>" + x.Activity + "</a></td><td>" + x.ScheduledStartDate.ToString("dd/MM/yyyy") + "</td><td>" + x.ScheduledFinishedDate.ToString("dd/MM/yyyy") + "</td></tr>";
                        }

                    }
                    str += "</table>";
                    mydiv.InnerHtml = str;
                }
                else
                {
                    lblActivityShow.Visible = false;
                    mydiv.Visible = false;
                    Label5.Visible = true;
                    Label5.Text = "No Activity found with such description !";
                }
            }
        }

        public static List<Project> GetProject(string userName,string ProjectId, int nDay)
        {
            
            string conStrng = string.Empty;
            List<Project> ProjectDetails = new List<Project>();
            if (userName == "isgec")
            { conStrng = cs; }
            else
            { conStrng = csLive; }
                
            using (SqlConnection con = new SqlConnection(conStrng))
            {
                string Getproj = @"select top 100 a.t_cprj, a.t_remk, a.t_cact, a.t_sdst, a.t_sdfn, a.t_acsd, a.t_acfn, a.t_dsca, b.t_dsca from ttpisg910200 a join ttcmcs052200 b on a.t_cprj = b.t_cprj WHERE a.t_cprj = '" + ProjectId + "'  and UPPER(a.t_pact)not in ('PARENT')  and t_sdst <= (GETDATE() +  " + nDay + ") and a.t_acfn in ('1753-01-01','1900-01-01') order by a.t_sdst asc";
                SqlCommand cmd = new SqlCommand(Getproj, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Project projDetail = new Project();
                        projDetail.Activity = (rdr["t_dsca"]).ToString();
                        projDetail.ActivityID = (rdr["t_cact"]).ToString();
                        projDetail.ScheduledStartDate = Convert.ToDateTime(rdr["t_sdst"]);
                        projDetail.ScheduledFinishedDate = Convert.ToDateTime(rdr["t_sdfn"]);
                        projDetail.ActualStartDate = Convert.ToDateTime(rdr["t_acsd"]);
                        projDetail.ActualFinishDate = Convert.ToDateTime(rdr["t_acfn"]);
                        projDetail.sRemarks = (rdr["t_remk"]).ToString();
                        ProjectDetails.Add(projDetail);
                    }
                }
                else
                {
                    //lblActivityShow.Visible = false;
                    //mydiv.Visible = false;
                    //Label5.Visible = true;
                    //Label5.Text = "No Activity found with such description !";
                }
                return ProjectDetails;
            }
        }
    }
}