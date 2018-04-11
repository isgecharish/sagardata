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
    public partial class ActualHours : System.Web.UI.Page
    {
        GlobalClass objGlobal;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetYear();
                GetMonth();
                GetProjects();
                GetRole();
                GetGroup();
                //BindGridActualHours();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void BindGridHours()
        {
            DataTable dtActualHors = new DataTable();
            DataTable dtPlannedHors = new DataTable();
            string listYear = string.Empty;
            string listMonth = string.Empty;
            string listProjects = string.Empty;
            string listRoleType = string.Empty;
            string listGroupId = string.Empty;
            if (HttpContext.Current.Cache["ActualHours"] == null || HttpContext.Current.Cache["PlannedHours"] == null)
            {
                foreach (ListItem item in ddlYear.Items)
                {
                    if (item.Selected)
                    {
                        listYear += ("'" + item.Value + "',");
                    }
                }
                foreach (ListItem item in ddlMonth.Items)
                {
                    if (item.Selected)
                    {
                        listMonth += "'" + item.Value + "',";
                    }
                }
                foreach (ListItem item in ddlProjects.Items)
                {
                    if (item.Selected)
                    {
                        listProjects += "'" + item.Value + "',";
                    }
                }
                foreach (ListItem item in ddlRoleType.Items)
                {
                    if (item.Selected)
                    {
                        listRoleType += "'" + item.Value + "',";
                    }
                }
                foreach (ListItem item in ddlGroup.Items)
                {
                    if (item.Selected)
                    {
                        listGroupId += "'" + item.Value + "',";
                    }
                }

                objGlobal = new GlobalClass();
                objGlobal.Year = listYear.TrimEnd(',');
                objGlobal.Month = listMonth.TrimEnd(',');
                objGlobal.Project = listProjects.TrimEnd(',');
                objGlobal.RoleType = listRoleType.TrimEnd(',');
                objGlobal.GroupId = listGroupId.TrimEnd(',');
                dtActualHors = objGlobal.GetAvailableActualHours();
                dtPlannedHors = objGlobal.GetPlannedHours();
                // DataSet ds = objGlobal.GetManHoursReport();
                HttpContext.Current.Cache.Insert("ActualHours", dtActualHors, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("PlannedHours", dtPlannedHors, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            }
            else
            {
                dtActualHors = (DataTable)HttpContext.Current.Cache["ActualHours"];
                dtPlannedHors = (DataTable)HttpContext.Current.Cache["PlannedHours"];
            }
            if (dtActualHors.Rows.Count > 0)
            {
                gvActualHours.DataSource = dtActualHors;
                gvActualHours.DataBind();
                divAheader.Visible = true;
                divNoRecActual.Visible = false;
                gvActualHours.Visible = true;                
            }
            else
            {
                divAheader.Visible = false;
                divNoRecActual.Visible = true;
                gvActualHours.Visible = false;
            }

            //Planned
            if (dtPlannedHors.Rows.Count > 0)
            {
                gvPlannedHors.DataSource = dtPlannedHors;
                gvPlannedHors.DataBind();
                divPheader.Visible = true;
                divNorecordPlanned.Visible = false;
                gvPlannedHors.Visible = true;
            }
            else
            {
                divPheader.Visible = false;
                divNorecordPlanned.Visible = true;
                gvPlannedHors.Visible = false;
            }
        }
        protected void btnSerach_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Cache.Remove("ActualHours");
            HttpContext.Current.Cache.Remove("PlannedHours");
            BindGridHours();
        }
        protected void gvPlannedHors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlannedHors.PageIndex = e.NewPageIndex;
            BindGridHours();
        }
        protected void gvActualHours_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActualHours.PageIndex = e.NewPageIndex;
            BindGridHours();
        }
        protected void btnExportActualHours_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ActualHours.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //To Export all pages
                    gvActualHours.AllowPaging = false;
                    this.BindGridHours();
                    foreach (TableCell cell in gvActualHours.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvActualHours.HeaderRow.Height = 30;
                    }
                    gvActualHours.RenderControl(hw);
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
        protected void btnExportPlannedHours_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=PlannedHours.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //To Export all pages
                    gvPlannedHors.AllowPaging = false;
                    this.BindGridHours();
                    foreach (TableCell cell in gvPlannedHors.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvPlannedHors.HeaderRow.Height = 30;
                    }
                    gvPlannedHors.RenderControl(hw);
                  //  style to format numbers to string
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
        private void GetYear()
        {
            DataTable dt = new DataTable();
            objGlobal = new GlobalClass();
            dt = objGlobal.GetYear();
            ddlYear.DataSource = dt;
            ddlYear.DataValueField = "year";
            ddlYear.DataTextField = "year";
            ddlYear.DataBind();
            //ddlYear.Items.Insert(0, "ALL");
        }
        private void GetMonth()
        {
            DataTable dt = new DataTable();
            objGlobal = new GlobalClass();
            dt = objGlobal.GetMonth();
            ddlMonth.DataSource = dt;
            ddlMonth.DataValueField = "Month";
            ddlMonth.DataTextField = "Monthname";
            ddlMonth.DataBind();
            //ddlMonth.Items.Insert(0, "ALL");
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
        private void GetRole()
        {
            DataTable dt = new DataTable();
            objGlobal = new GlobalClass();
            dt = objGlobal.GetRoleType();
            ddlRoleType.DataSource = dt;
            ddlRoleType.DataValueField = "RoleType";
            ddlRoleType.DataTextField = "RoleType";
            ddlRoleType.DataBind();
            //ddlRoleType.Items.Insert(0, "ALL");
        }
        private void GetGroup()
        {
            DataTable dt = new DataTable();
            objGlobal = new GlobalClass();
            dt = objGlobal.GetManhoursGroup();
            ddlGroup.DataSource = dt;
            ddlGroup.DataValueField = "GroupID";
            ddlGroup.DataTextField = "GroupID";
            ddlGroup.DataBind();
            // ddlGroup.Items.Insert(0, "ALL");
        }

    }
}