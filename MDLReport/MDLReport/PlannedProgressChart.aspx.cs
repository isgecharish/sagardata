using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

namespace MDLReport
{
    public partial class PlannedProgressChart : System.Web.UI.Page
    {
        ReportClass objReport;
        PlannedProgressChartClass objPlanned;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetProjects();
                    GetYear();
                    GetMonth();
                    objReport = new ReportClass();
                    string Connection = objReport.GetEnv(Request.QueryString["env"]);
                    hdfEnv.Value = Connection;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        private void GetProjects()
        {
            DataTable dt = new DataTable();
            objReport = new ReportClass();
            string Connection = objReport.GetEnv(Request.QueryString["env"]);
            dt = objReport.GetProjectFromPMDL(Connection);
            ddlProject.DataSource = dt;
            ddlProject.DataValueField = "Project";
            ddlProject.DataTextField = "Project";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "Select");
        }
        private void GetYear()
        {
            DataTable dt = new DataTable();
            objReport = new ReportClass();
            string Connection = objReport.GetEnv(Request.QueryString["env"]);
            dt = objReport.GetYear(Connection);
            ddlYear.DataSource = dt;
            ddlYear.DataValueField = "year";
            ddlYear.DataTextField = "year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, "Select");
        }
        private void GetMonth()
        {
            DataTable dt = new DataTable();
            objReport = new ReportClass();
            string Connection = objReport.GetEnv(Request.QueryString["env"]);
            dt = objReport.GetMonth(Connection);
            ddlMonthFrom.DataSource = dt;
            ddlMonthFrom.DataValueField = "Month";
            ddlMonthFrom.DataTextField = "Monthname";
            ddlMonthFrom.DataBind();
            ddlMonthFrom.Items.Insert(0, "Select");

            ddlMonthto.DataSource = dt;
            ddlMonthto.DataValueField = "Month";
            ddlMonthto.DataTextField = "Monthname";
            ddlMonthto.DataBind();
            ddlMonthto.Items.Insert(0, "Select");
        }
        private void GetPlannedProgress()
        {
            try
            {
                if (ddlProject.SelectedValue != "Select" && ddlYear.SelectedValue != "Select" && ddlMonthFrom.SelectedValue != "Select" && ddlMonthto.SelectedValue != "Select")
                {
                    objReport = new ReportClass();
                    string Connection = objReport.GetEnv(Request.QueryString["env"]);
                    objReport.Project = ddlProject.SelectedValue;
                    objReport.Year = ddlYear.SelectedValue;
                    objReport.MonthFrom = ddlMonthFrom.SelectedValue;
                    objReport.MonthTo = ddlMonthto.SelectedValue;
                    DataSet ds = objReport.GetPlannedPercProgress(Connection);

                    List<PlannedProgressChartClass> lstPlanned = new List<PlannedProgressChartClass>();
                    if (ds.Tables[0].Rows[0][0].ToString() != "0" && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            objPlanned = new PlannedProgressChartClass();
                            objPlanned.BaselinePlannedPerc = Math.Round((Convert.ToDecimal(dr["BaselinePlanned"]) / Convert.ToDecimal(ds.Tables[0].Rows[0][0])) * 100, 0);
                            objPlanned.RevisedPlannedPerc = Math.Round((Convert.ToDecimal(dr["RevisedPlanned"]) / Convert.ToDecimal(ds.Tables[0].Rows[0][0])) * 100, 0);
                            objPlanned.ActualPerc = Math.Round((Convert.ToDecimal(dr["Actual"]) / Convert.ToDecimal(ds.Tables[0].Rows[0][0])) * 100, 0);
                            objPlanned.slNo = Convert.ToInt32(dr["SlNo"]);
                            objPlanned.Months = dr["Months"].ToString();
                            lstPlanned.Add(objPlanned);
                        }
                        gvData.DataSource = lstPlanned;
                        gvData.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Record not found');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Please Select all fields');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetPlannedProgress();
        }

        [WebMethod]
        public static List<PlannedProgressChartClass> GetPlannedProgress(string Project, string Year, string MonthFrom, string Monthto,string env)
        {
            ReportClass objReport = new ReportClass();
            objReport.Project = Project;
            objReport.Year = Year;
            objReport.MonthFrom = MonthFrom;
            objReport.MonthTo = Monthto;
            DataSet ds = objReport.GetPlannedPercProgress(env);

            List<PlannedProgressChartClass> lstPlanned = new List<PlannedProgressChartClass>();
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                PlannedProgressChartClass objPlanned = new PlannedProgressChartClass();
                objPlanned.BaselinePlannedPerc = Math.Round((Convert.ToDecimal(dr["BaselinePlanned"]) / Convert.ToDecimal(ds.Tables[0].Rows[0][0])) * 100, 0);
                objPlanned.RevisedPlannedPerc = Math.Round((Convert.ToDecimal(dr["RevisedPlanned"]) / Convert.ToDecimal(ds.Tables[0].Rows[0][0])) * 100, 0);
                objPlanned.ActualPerc = Math.Round((Convert.ToDecimal(dr["Actual"]) / Convert.ToDecimal(ds.Tables[0].Rows[0][0])) * 100, 0);
                objPlanned.slNo = Convert.ToInt32(dr["SlNo"]);
                objPlanned.Months = dr["Months"].ToString();
                lstPlanned.Add(objPlanned);
            }
            return lstPlanned;
        }
    }
}