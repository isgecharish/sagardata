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

namespace MobileTest
{
   
    public partial class ProjectProgressReport : System.Web.UI.Page
    {
        public static string cs = ConfigurationManager.AppSettings["ConnectionTest"];
        public static string csLive = ConfigurationManager.AppSettings["ConnectionLive"];
        protected void page_load(object s, EventArgs e)
        {
          

            if (!IsPostBack)
            {
                DataTable dt = (DataTable)(Session["RecordTable"]);
                string sProjId = (string)(Session["projId"]);
                int nDay = (int)(Session["day"]);
                string sProjname = (string)(Session["projName"]);
                string sDept = (string)(Session["Dept"]);
                if (dt.Rows.Count > 1)
                {
                    lblSelectProject.Visible = true;
                    ddlProject.Visible = true;
                    PopulateProject();
                    lblProjDetail.Text = sProjId + " - " + sProjname;

                }
                else if (dt.Rows.Count == 1)
                {
                    lblSelectProject.Visible = false;
                    ddlProject.Visible = false;
                    lblProjDetail.Text = sProjId + " - " + sProjname;
                }
                else
                {
                    lblSelectProject.Visible = false;
                    ddlProject.Visible = false;
                    lblProjDetail.Visible = false;
                    mydiv.Visible = false;
                    lblActivityShow.Visible = false;
                }
                ShowActivity(sProjId, nDay, sDept);
            }
            string sUsername = (string)(Session["Username"]);
            if (sUsername == "isgec"|| sUsername=="3194" || sUsername == "0330")
            {
                lblTestServer.Visible = true;
                lblTestServer.Text = "Test Server";
            }
            else
            {
                lblTestServer.Visible = false;
            }
        }

        private void PopulateProject()
        {
            //DataTable dt1 = new DataTable();
            DataTable dt = (DataTable)(Session["RecordTable"]);
            //dt1 = dt.Copy();
            //dt1.Columns.Remove("t_day");
            //dt1.Columns.Remove("t_dsca");
            ddlProject.DataSource = dt;
            ddlProject.DataValueField = "t_cprj";
            ddlProject.DataTextField = "drpdwn";
            ddlProject.DataBind();
            string sProjId = (string)(Session["projId"]);
            ddlProject.SelectedValue = sProjId; 
            //ddlProject.Items.Insert(0, (dt.Rows[0]["t_cprj"]).ToString());
            //ddlProject.Items.Insert(0, sProjId);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sProjId = (string)(Session["projId"]);
            int nDay = (int)(Session["day"]);
            string sProjname = (string)(Session["projName"]);
            string sUsername = (string)(Session["Username"]);
            string sDept = (string)(Session["Dept"]);
            lblProjDetail.Text = sProjId + " - " + sProjname;
            lblActivityShow.Text = "Activities to be started upto " + (DateTime.Today.AddDays(nDay)).ToString("dd-MM-yyyy");
            string search = txtDsc.Text;
            string conStrng=string.Empty;
            List<ProjectClass> ProjectDetails = new List<ProjectClass>();
            if (sUsername == "isgec" || sUsername == "3194" || sUsername == "0330")
            { 
                conStrng = cs;
            }
            else
            { 
            conStrng = csLive;
            }
            using (SqlConnection con = new SqlConnection(conStrng))
            {
                string SearchActivity = @"select top 100 a.t_cprj, a.t_remk, a.t_cact, a.t_sdst, a.t_sdfn, a.t_acsd, a.t_acfn,a.t_dsca, b.t_dsca from ttpisg910200 a inner join ttcmcs052200 b on a.t_cprj=b.t_cprj WHERE  a.t_cprj = '" + sProjId + "'  and a.t_dept = '" + sDept + "' and UPPER(a.t_dsca) like '%" + search.ToUpper() + "%'  and UPPER(a.t_pact) not in ('PARENT') and t_sdst <= (GETDATE() +  30) order by a.t_sdst asc";
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
                        ProjectClass projDetail = new ProjectClass();
                        projDetail.Activity = (rdr["t_dsca"]).ToString();
                        projDetail.ActivityID = (rdr["t_cact"]).ToString();
                        projDetail.ScheduledStartDate = Convert.ToDateTime(rdr["t_sdst"]);
                        projDetail.ScheduledFinishedDate = Convert.ToDateTime(rdr["t_sdfn"]);
                        projDetail.ActualStartDate = Convert.ToDateTime(rdr["t_acsd"]);
                        projDetail.ActualFinishDate = Convert.ToDateTime(rdr["t_acfn"]);
                        projDetail.sRemarks = (rdr["t_remk"]).ToString();
                        ProjectDetails.Add(projDetail);
                    }
                    List<ProjectClass> tmp = ProjectDetails;
                    string str = "";
                    str += "<table class='table'><thead><tr class='btn-dark'>";
                    str += "<td>Activity</td><td>Schedule Start Date</td><td>Scheduled Finish Date</td>";
                    str += "</tr></thead>";
                    foreach (ProjectClass x in tmp)
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

        public static List<ProjectClass> GetProject(string userName,string ProjectId, int nDay, string Dept)
        {
            
            string conStrng = string.Empty;
            List<ProjectClass> ProjectDetails = new List<ProjectClass>();
            if (userName == "isgec" || userName == "3194" || userName == "0330")
            { conStrng = cs; }
            else
            { conStrng = csLive; }
                
            using (SqlConnection con = new SqlConnection(conStrng))
            {
                string Getproj = @"select top 100 a.t_cprj, a.t_remk, a.t_cact, a.t_sdst, a.t_sdfn, a.t_acsd, a.t_acfn, a.t_dsca, b.t_dsca from ttpisg910200 a join ttcmcs052200 b on a.t_cprj = b.t_cprj WHERE a.t_cprj = '" + ProjectId + "' and a.t_dept = '" + Dept + "'  and UPPER(a.t_pact)not in ('PARENT')  and t_sdst <= (GETDATE() +  " + nDay + ") and a.t_acfn in ('1753-01-01','1900-01-01') order by a.t_sdst asc";
                SqlCommand cmd = new SqlCommand(Getproj, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ProjectClass projDetail = new ProjectClass();
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

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProject.SelectedValue != null)
            {
                DataTable dtProjDetails = new DataTable();
                string sUsername = (string)(Session["Username"]);
                ProjectClass projDetail = new ProjectClass();
                projDetail.sProjId = ddlProject.SelectedValue;
                
                projDetail.User = sUsername;
                dtProjDetails = projDetail.GetSelectedProjectDetails();
                if (dtProjDetails.Rows.Count > 0)
                {
                    string sProjName = (dtProjDetails.Rows[0]["t_dsca"]).ToString();
                    string sDept = (dtProjDetails.Rows[0]["t_dept"]).ToString();
                    int nDay = (int)(dtProjDetails.Rows[0]["t_day"]);
                    lblProjDetail.Text = projDetail.sProjId + " - " + sProjName;
                    Session["projId"] = projDetail.sProjId;
                    Session["day"] = nDay;
                    Session["projName"] = sProjName;
                    Session["Dept"] = sDept;
                    ShowActivity(projDetail.sProjId, nDay, sDept);
                }

            }

        }

        protected void ShowActivity(string sProjId, int nDay, string sDept)
        {
            string sUsername = (string)(Session["Username"]);
            lblActivityShow.Visible = true;
            lblActivityShow.Text = "Activities to be started upto " + (DateTime.Today.AddDays(nDay)).ToString("dd-MM-yyyy");
            List<ProjectClass> tmp = GetProject(sUsername, sProjId, nDay, sDept);
            if (tmp != null)
            {
                lblActivityShow.Visible = true;
                mydiv.Visible = true;
                Label5.Visible = false;
                txtDsc.Text = string.Empty;
                string str = "";
                str += "<table class='table'><thead><tr class='btn-dark'>";
                str += "<td>Activity </td><td>Schedule Start Date</td><td>Scheduled Finish Date</td>";
                str += "</tr></thead>";
                foreach (ProjectClass x in tmp)
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
    }
}