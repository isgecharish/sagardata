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
    public partial class LN_PMDL007 : System.Web.UI.Page
    {
        GlobalClass objGlobal;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["env"] != null)
                    {
                        GetProjects();
                        GetResponsible_Discipline();
                        txtDateFrom.Text = System.DateTime.Now.ToString("dd/MM/yy");
                        txtDateTo.Text = System.DateTime.Now.ToString("dd/MM/yy");
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
            objGlobal = new GlobalClass();
            dt = objGlobal.GetProjectFromPMDL(Request.QueryString["env"]);
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
        private void GetResponsible_Discipline()
        {
            DataTable dt = new DataTable();
            objGlobal = new GlobalClass();
            dt = objGlobal.GetResponsible_Discipline(Request.QueryString["env"]);
            int RowCount = dt.Rows.Count;
            ddlFromDescipline.DataSource = dt;
            ddlFromDescipline.DataValueField = "ResponsibleDiscipline";
            ddlFromDescipline.DataTextField = "ResponsibleDiscipline";
            ddlFromDescipline.DataBind();
            ddlFromDescipline.Items.Insert(0, dt.Rows[0][0].ToString());

            ddlToDescipline.DataSource = dt;
            ddlToDescipline.DataValueField = "ResponsibleDiscipline";
            ddlToDescipline.DataTextField = "ResponsibleDiscipline";
            ddlToDescipline.DataBind();
            ddlToDescipline.Items.Insert(0, dt.Rows[RowCount-1][0].ToString());
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Cache.Remove("PMDLData");
            BindGrid();
         
        }
        private void BindGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                if (HttpContext.Current.Cache["PMDLData"] == null)
                {
                    if (ddlProjectFrom.SelectedValue != "Select" && ddlProjectTo.SelectedValue != "Select" && txtDateTo.Text != "" && ddlToDescipline.SelectedValue != "Select" && ddlFromDescipline.SelectedValue != "Select")
                    {
                        objGlobal = new GlobalClass();
                        objGlobal.ProjectFrom = ddlProjectFrom.SelectedValue;
                        objGlobal.ProjectTo = ddlProjectTo.SelectedValue;
                        objGlobal.DisciplineFrom = ddlFromDescipline.SelectedValue;
                        objGlobal.DisciplineTo = ddlToDescipline.SelectedValue;
                        objGlobal.DateFrom = txtDateFrom.Text!=""? Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy-MM-dd 00:00:00"):"1970-01-01 00:00:00";
                        objGlobal.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy-MM-dd 23:59:59");
                        if (rdAllData.Checked == true)
                        {
                            spHeader.InnerText = " Projectwise Planned Documents";
                            dt = objGlobal.GetPMDLDATA(Request.QueryString["env"]);
                        }
                        else if (rdPending.Checked == true)
                        {
                            dt = objGlobal.GetPendingPMDLDATA(Request.QueryString["env"]);
                            spHeader.InnerText = " Projectwise Pending Documents";
                        }
                       else if (rdReleased.Checked == true)
                        {
                            spHeader.InnerText = " Projectwise Released Documents";
                            dt = objGlobal.GetOnlyRelesaedPMDLDATA(Request.QueryString["env"]);
                        }
                        else
                        {

                        }
                        HttpContext.Current.Cache.Insert("PMDLData", dt, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Select all fields.');", true);
                    }
                }
                else
                {
                    dt = (DataTable)HttpContext.Current.Cache["PMDLData"];
                }
                if (dt.Rows.Count > 0)
                {
                    gvPMDL.DataSource = dt;
                    gvPMDL.DataBind();
                    btnExport.Visible = true;
                    btnPrint.Visible = true;
                    spHeader.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Record not found');", true);
                    btnExport.Visible = false;
                    btnPrint.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue data not found');", true);
            }
        }
        protected void gvPMDL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPMDL.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Pmdl007.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    hw.Write("<table><tr><td></td><td></td><td></td><td></td><td colspan='12'><b>Project wise Planned Documents<b></td></tr>");
                    //To Export all pages
                    gvPMDL.AllowPaging = false;
                    this.BindGrid();
                    foreach (TableCell cell in gvPMDL.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvPMDL.HeaderRow.Height = 30;
                    }
                    gvPMDL.RenderControl(hw);
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
    }
}