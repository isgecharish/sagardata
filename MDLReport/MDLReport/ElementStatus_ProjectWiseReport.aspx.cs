using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MDLReport
{
    public partial class ElementStatus_ProjectWiseReport : System.Web.UI.Page
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlProjectFrom.SelectedValue != "Select" && ddlProjectTo.SelectedValue != "Select")
            {
                Response.Redirect("ElementStatus_ProjectWiseReport.aspx?env=" + Request.QueryString["env"] + "&From=" + ddlProjectFrom.SelectedValue + "&To=" + ddlProjectTo.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Select all fields.');", true);
            }
        }
    }
}