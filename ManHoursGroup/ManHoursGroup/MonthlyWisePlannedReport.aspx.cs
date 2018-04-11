using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManHoursGroup
{
    public partial class MonthlyWisePlannedReport : System.Web.UI.Page
    {
        GlobalClass objGlobal;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetYear();
                    GetMonth();
                    GetProjects();
                }
            }
            catch
            {

            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btnSerach_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Cache.Remove("PlannedHours");
            BindGridMonthlyPlannedReport();
        }

        private void BindGridMonthlyPlannedReport()
        {
            try
            {
                if (ddlYearFrom.SelectedValue != "From Year" && ddlYearTo.SelectedValue != "To Year" && ddlMonthFrom.SelectedValue != "From Month" && ddlMonthTo.SelectedValue != "To Month")
                {
                    DataTable dtPlannedHors = new DataTable();
                    // string listProjects = string.Empty;
                    if (HttpContext.Current.Cache["PlannedHours"] == null)
                    {
                        //foreach (ListItem item in ddlProjects.Items)
                        //{
                        //    if (item.Selected)
                        //    {
                        //        listProjects += "'" + item.Value + "',";
                        //    }
                        //}

                        objGlobal = new GlobalClass();
                        objGlobal.YearFrom = ddlYearFrom.SelectedValue;
                        objGlobal.YearTo = ddlYearTo.SelectedValue;
                        objGlobal.MonthFrom = ddlMonthFrom.SelectedValue;
                        objGlobal.MonthTo = ddlMonthTo.SelectedValue;
                        //objGlobal.Project = listProjects.TrimEnd(','); 
                        dtPlannedHors = objGlobal.GetMonthlyWisePlannedReport();
                        HttpContext.Current.Cache.Insert("PlannedHours", dtPlannedHors, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                    }
                    else
                    {
                        dtPlannedHors = (DataTable)HttpContext.Current.Cache["PlannedHours"];
                    }
                    //Planned             
                    if (dtPlannedHors.Rows.Count > 0 && dtPlannedHors.Rows[0][0].ToString()!="")
                    {
                        gvPlannedHors.DataSource = dtPlannedHors;
                        gvPlannedHors.DataBind();
                        btnExport.Visible = true;
                    }
                    else
                    {
                        btnExport.Visible = false;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Data not found');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Select all fields.');", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue data not found.');", true);
            }
        }
        private void GetYear()
        {
            DataTable dt = new DataTable();
            objGlobal = new GlobalClass();
            dt = objGlobal.GetYear();
            ddlYearFrom.DataSource = dt;
            ddlYearFrom.DataValueField = "year";
            ddlYearFrom.DataTextField = "year";
            ddlYearFrom.DataBind();
            ddlYearFrom.Items.Insert(0, "From Year");

            ddlYearTo.DataSource = dt;
            ddlYearTo.DataValueField = "year";
            ddlYearTo.DataTextField = "year";
            ddlYearTo.DataBind();
            ddlYearTo.Items.Insert(0, "To Year");
        }
        private void GetMonth()
        {
            DataTable dt = new DataTable();
            objGlobal = new GlobalClass();
            dt = objGlobal.GetMonth();
            ddlMonthFrom.DataSource = dt;
            ddlMonthFrom.DataValueField = "Month";
            ddlMonthFrom.DataTextField = "Monthname";
            ddlMonthFrom.DataBind();
            ddlMonthFrom.Items.Insert(0, "From Month");

            ddlMonthTo.DataSource = dt;
            ddlMonthTo.DataValueField = "Month";
            ddlMonthTo.DataTextField = "Monthname";
            ddlMonthTo.DataBind();
            ddlMonthTo.Items.Insert(0, "To Month");


        }
        private void GetProjects()
        {
            DataTable dt = new DataTable();
            objGlobal = new GlobalClass();
            dt = objGlobal.GetProject();
            ddlProjects.DataSource = dt;
            ddlProjects.DataValueField = "Project";
            ddlProjects.DataTextField = "Project";
            ddlProjects.DataBind();
            // ddlProjects.Items.Insert(0, "ALL");
        }

        protected void gvPlannedHors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 0; i < gvPlannedHors.HeaderRow.Cells.Count; i++)
                    {
                        string HeaderName = gvPlannedHors.HeaderRow.Cells[i].Text;
                        switch (HeaderName)
                        {
                            case "1":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "Jan";
                                break;
                            case "2":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "Feb";
                                break;
                            case "3":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "March";
                                break;
                            case "4":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "April";
                                break;
                            case "5":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "May";
                                break;
                            case "6":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "June";
                                break;
                            case "7":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "July";
                                break;
                            case "8":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "Aug";
                                break;
                            case "9":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "Sep";
                                break;
                            case "10":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "Oct";
                                break;
                            case "11":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "Nov";
                                break;
                            case "12":
                                gvPlannedHors.HeaderRow.Cells[i].Text = "Dec";
                                break;
                        }
                        System.Data.DataRow row = ((System.Data.DataRowView)e.Row.DataItem).Row;
                        for (int j = 2; j < e.Row.Cells.Count; j++)
                        {
                            e.Row.Cells[j].Style.Add("text-align", "right");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=MonthlyPlannedHours.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                  //  hw.Write("<table><tr><td></td><td></td><td></td><td></td><td colspan='12'><b>Project wise Planned Documents<b></td></tr>");
                    //To Export all pages
                    gvPlannedHors.AllowPaging = false;
                    this.BindGridMonthlyPlannedReport();
                    foreach (TableCell cell in gvPlannedHors.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvPlannedHors.HeaderRow.Height = 30;
                    }
                    gvPlannedHors.RenderControl(hw);
                    //style to format numbers to string
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
    }
}