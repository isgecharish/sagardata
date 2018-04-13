using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MDLReport
{
    public partial class MDLReport : System.Web.UI.Page
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
        private void GetProjects()
        {
            DataTable dt = new DataTable();
            objReportClass = new ReportClass();
            string Connection = objReportClass.GetEnv(Request.QueryString["env"]);
            dt = objReportClass.GetProjectFromPMDLPMAL(Connection);
            ddlProject.DataSource = dt;
            ddlProject.DataValueField = "Project";
            ddlProject.DataTextField = "Project";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "Select");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlProject.SelectedValue != "Select" && txtDate.Text != "")
            {
                Response.Redirect("MDLReport.aspx?env=" + Request.QueryString["env"] + "&Project=" + ddlProject.SelectedValue + "&InputDate=" + txtDate.Text.Trim());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Select all fields.');", true);
            }
        }
    }
}