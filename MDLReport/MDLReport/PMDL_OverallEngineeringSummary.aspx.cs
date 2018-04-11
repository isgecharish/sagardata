using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace MDLReport
{
    public partial class PMDL_OverallEngineeringSummary : System.Web.UI.Page
    {
        ReportClass objReportClass;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["env"] != null && Request.QueryString["cmpId"] !=null)
                    {
                        GetProjects();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void GetProjects()
        {
            DataTable dt = new DataTable();
            objReportClass = new ReportClass();
            string Connection = objReportClass.GetEnv(Request.QueryString["env"]);
            dt = objReportClass.GetProjectFromPMDL(Connection);
            ddlProjectFrom.DataSource = dt;
            ddlProjectFrom.DataValueField = "Project";
            ddlProjectFrom.DataTextField = "Project";
            ddlProjectFrom.DataBind();
            ddlProjectFrom.Items.Insert(0, "Select");

            ddlProjectTo.DataSource = dt;
            ddlProjectTo.DataValueField = "Project";
            ddlProjectTo.DataTextField = "Project";
            ddlProjectTo.DataBind();
            ddlProjectTo.Items.Insert(0, "Select");
        }
        protected void GetEngineerSummary()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                if (HttpContext.Current.Cache["PMDLData"] == null)
                {
                    if (ddlProjectFrom.SelectedValue != "Select" && ddlProjectTo.SelectedValue != "Select" && txtDate.Text != "")
                    {
                        objReportClass = new ReportClass();
                        string Connection = objReportClass.GetEnv(Request.QueryString["env"]);
                        objReportClass.ProjectFrom = ddlProjectFrom.SelectedValue;
                        objReportClass.ProjectTo = ddlProjectTo.SelectedValue;
                        objReportClass.InputDate = Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy-MM-dd");
                        DataSet dsEngSummary = objReportClass.GetLN_OverallEngineeringSummary(Connection, Request.QueryString["cmpId"]);

                        for (int i = 0; i < dsEngSummary.Tables.Count; i++)
                        {
                            dtTemp.Merge(dsEngSummary.Tables[i]);
                        }
                        HttpContext.Current.Cache.Insert("PMDLData", dtTemp, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Select all fields.');", true);
                    }
                }
                else
                {
                    dtTemp = (DataTable)HttpContext.Current.Cache["PMDLData"];
                }
                if (dtTemp.Rows.Count > 0)
                {
                    gvEngineerSummary.DataSource = dtTemp;
                    gvEngineerSummary.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    btnExport.Visible = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Record not found.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Cache.Remove("PMDLData");
            GetEngineerSummary();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=PmdlEngineeringSummary" + Request.QueryString["cmpId"] + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    hw.Write("<table><tr><td></td><td></td><td></td><td colspan='12'><b>Overall Engineering Summary (As on Date: " + txtDate.Text + ")<b></td></tr>");
                    //To Export all pages
                    gvEngineerSummary.AllowPaging = false;
                    this.GetEngineerSummary();
                    foreach (TableCell cell in gvEngineerSummary.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvEngineerSummary.HeaderRow.Height = 30;
                    }
                    gvEngineerSummary.RenderControl(hw);
                    // style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
    }
}