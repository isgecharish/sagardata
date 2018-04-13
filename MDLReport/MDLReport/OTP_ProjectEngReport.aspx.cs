using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MDLReport
{
    public partial class OTP_ProjectEngReport : System.Web.UI.Page
    {
        ReportClass objReportClass;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetProjects();
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
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            BindOTPProjectReport();
        }
        protected void btnOTP_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Cache.Remove("OTPReport");
            BindOTPProjectReport();
            divOtpreport.Visible = true;
            divOtpSummary.Visible = false;
            btnExportOTP.Visible = true;
            btnExportOTPSummary.Visible = false;
        }
        protected void btnOTPSummary_Click(object sender, EventArgs e)
        {
            BindGridOTPSummary();
            divOtpreport.Visible = false;
            divOtpSummary.Visible = true;
            btnExportOTP.Visible = false;
            btnExportOTPSummary.Visible = true;
        }
        private void BindOTPProjectReport()
        {
            try
            {
                DataTable dt = new DataTable();
                if (HttpContext.Current.Cache["OTPReport"] == null)
                {
                    if (ddlProjectFrom.SelectedValue != "Select" && ddlProjectTo.SelectedValue != "Select" && txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        objReportClass = new ReportClass();
                        string Connection = objReportClass.GetEnv(Request.QueryString["env"]);
                        objReportClass.ProjectFrom = ddlProjectFrom.SelectedValue;
                        objReportClass.ProjectTo = ddlProjectTo.SelectedValue;
                        objReportClass.DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy-MM-dd 00:00:00");
                        objReportClass.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy-MM-dd 23:59:59");
                        dt = objReportClass.GetOTP_ProjectEng(Connection);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Select all fields');", true);
                    }
                }
                else
                {
                    dt = (DataTable)HttpContext.Current.Cache["OTPReport"];
                }
                if (dt.Rows.Count > 0)
                {
                    gvData.DataSource = dt;
                    gvData.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        private void BindGridOTPSummary()
        {
            try
            {
                if (ddlProjectFrom.SelectedValue != "Select" && ddlProjectTo.SelectedValue != "Select" && txtDateFrom.Text != "" && txtDateTo.Text != "")
                {
                    objReportClass = new ReportClass();
                    string Connection = objReportClass.GetEnv(Request.QueryString["env"]);
                    objReportClass.ProjectFrom = ddlProjectFrom.SelectedValue;
                    objReportClass.ProjectTo = ddlProjectTo.SelectedValue;
                    objReportClass.DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy-MM-dd 00:00:00");
                    objReportClass.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy-MM-dd 23:59:59");
                    DataSet ds = objReportClass.GetonTimePerfomanceReport(Connection);
                    DataTable dtBaseLine = new DataTable();
                    DataTable dtRevised = new DataTable();
                    DataRow dr = null;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            dtBaseLine.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                        }

                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            dr = dtBaseLine.NewRow();
                            for (int k = 0; k < dtBaseLine.Columns.Count; k++)
                            {
                                if (k == 0)
                                {
                                    dr[k] = ds.Tables[0].Rows[j][k].ToString();
                                }
                                if (k > 0)
                                {
                                    if (ds.Tables[0].Rows[j][dtBaseLine.Columns[k].ColumnName].ToString() == "")
                                    {
                                        ds.Tables[0].Rows[j][dtBaseLine.Columns[k].ColumnName] = 0;
                                    }
                                    if (ds.Tables[1].Rows[j][dtBaseLine.Columns[k].ColumnName].ToString() == "")
                                    {
                                        ds.Tables[1].Rows[j][dtBaseLine.Columns[k].ColumnName] = 0;
                                    }
                                    if (Convert.ToInt32(ds.Tables[0].Rows[j][dtBaseLine.Columns[k].ColumnName]) != 0)
                                    {
                                        dr[k] = Math.Round((Convert.ToDecimal(ds.Tables[1].Rows[j][dtBaseLine.Columns[k].ColumnName]) / Convert.ToDecimal(ds.Tables[0].Rows[j][dtBaseLine.Columns[k].ColumnName])) * 100, 2);
                                    }
                                }
                            }
                            dtBaseLine.Rows.Add(dr);
                        }
                        gvBaseLineReport.DataSource = dtBaseLine;
                        gvBaseLineReport.DataBind();

                        // Bind Revised Ontime

                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            dtRevised.Columns.Add(ds.Tables[0].Columns[i].ColumnName);
                        }

                        for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                        {
                            dr = dtRevised.NewRow();
                            for (int k = 0; k < dtRevised.Columns.Count; k++)
                            {
                                if (k == 0)
                                {
                                    dr[k] = ds.Tables[0].Rows[j][k].ToString();
                                }
                                if (k > 0)
                                {
                                    if (ds.Tables[0].Rows[j][dtBaseLine.Columns[k].ColumnName].ToString() == "")
                                    {
                                        ds.Tables[0].Rows[j][dtBaseLine.Columns[k].ColumnName] = 0;
                                    }
                                    if (ds.Tables[2].Rows[j][dtRevised.Columns[k].ColumnName].ToString() == "")
                                    {
                                        ds.Tables[2].Rows[j][dtRevised.Columns[k].ColumnName] = 0;
                                    }
                                    if (Convert.ToInt32(ds.Tables[0].Rows[j][dtBaseLine.Columns[k].ColumnName]) != 0)
                                    {
                                        dr[k] = Math.Round((Convert.ToDecimal(ds.Tables[2].Rows[j][dtRevised.Columns[k].ColumnName]) / Convert.ToDecimal(ds.Tables[0].Rows[j][dtRevised.Columns[k].ColumnName])) * 100, 2);
                                    }
                                }
                            }
                            dtRevised.Rows.Add(dr);
                        }
                        dtRevised.Columns.RemoveAt(0);
                        gvRevisedReport.DataSource = dtRevised;
                        gvRevisedReport.DataBind();
                        spASonDate.InnerHtml = txtDateFrom.Text +" to "+ txtDateTo.Text;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Select all fields');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
        protected void btnExportOTP_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=OTPReport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    hw.Write("<table><tr><td></td><td></td><td></td><td></td><td></td><td colspan='10'><b> Project Engineering  OTP Detail <b></td></tr>");
                    hw.Write("<tr><td></td><td></td><td></td><td></td><td colspan='12'><b> As On Date " + txtDateTo.Text + "(Project From- " + ddlProjectFrom.SelectedValue + " To " + ddlProjectTo.SelectedValue + ")<b></td></tr></table>");
                    //To Export all pages
                    gvData.AllowPaging = false;
                    this.BindOTPProjectReport();
                    foreach (TableCell cell in gvData.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvData.HeaderRow.Height = 30;
                    }
                    gvData.RenderControl(hw);
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

            }
        }
        protected void btnExportOTPSummary_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=OTPReport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    hw.Write("<table><tr><td></td><td></td><td></td><td></td><td></td><td colspan='12'><b> Project Engineering OTP Summary<b></td></tr>");
                    hw.Write("<tr><td></td><td></td><td></td><td></td><td colspan='12'><b> As On Date " + txtDateTo.Text + "(Project From " + ddlProjectFrom.SelectedValue + " To " + ddlProjectTo.SelectedValue + ")<b></td></tr></table>");
                    //To Export all pages
                    gvBaseLineReport.AllowPaging = false;
                    gvRevisedReport.AllowPaging = false;
                    this.BindGridOTPSummary();
                    foreach (TableCell cell in gvBaseLineReport.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvBaseLineReport.HeaderRow.Height = 30;
                    }
                    gvBaseLineReport.RenderControl(hw);
                    foreach (TableCell cell in gvRevisedReport.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvRevisedReport.HeaderRow.Height = 30;
                    }
                    gvRevisedReport.RenderControl(hw);
                    // style to format numbers to string
                    string style = @"<style>   </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}