using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProjectProgressUpdate
{
    public partial class UpdateProjectProgress : System.Web.UI.Page
    {
        ProjectClass objProjectCls;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                }

            }
            catch (Exception ex)
                {
                    string tt = ex.Message;
                }
            string sProjId = (string)(Session["projId"]);
            string sProjName = (string)(Session["projName"]);
            string sUsername = (string)(Session["Username"]);
            lblProjDetail1.Text = sProjId + " - " + sProjName;

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
           string sUsername = (string)(Session["Username"]);
            if (Request.QueryString["cprj"] != null && Request.QueryString["cact"] != null)
            {
                GetProjectProgress(sUsername);
            }
        }
        protected void GetProjectProgress(string username)
        {
            objProjectCls = new ProjectClass();
            DataTable dt = objProjectCls.GetProjectProgressByID(username,Request.QueryString["cprj"],Request.QueryString["cact"]);
            if (dt.Rows.Count > 0)
            {
                txtActivity.Text = dt.Rows[0]["t_cact"].ToString();
                txtDescription.Text = dt.Rows[0]["t_dsca"].ToString();

                txtScheduledStartDate.Text = Convert.ToDateTime(dt.Rows[0]["t_sdst"]).ToString("dd-MM-yyyy");
                txtScheduledFinishDate.Text = Convert.ToDateTime(dt.Rows[0]["t_sdfn"]).ToString("dd-MM-yyyy");
                txtRemarks.Text = dt.Rows[0]["t_remk"].ToString();
            }
            txtActualStartDate.Attributes.Add("type", "date");
            if (( Convert.ToDateTime(dt.Rows[0]["t_acsd"]).ToString("yyyy") == "1753"|| Convert.ToDateTime(dt.Rows[0]["t_acsd"]).ToString("yyyy") == "1900"|| (dt.Rows[0]["t_acsd"]).ToString() == string.Empty))
            {
                string sdate = "yyyy-MM-dd";
                txtActualStartDate.Attributes.Add("value", sdate);

            }
            else
            {
                txtActualStartDate.Attributes.Add("value", Convert.ToDateTime(dt.Rows[0]["t_acsd"]).ToString("yyyy-MM-dd"));
            }
            txtActualFinishDate.Attributes.Add("type", "date");
            if (( Convert.ToDateTime(dt.Rows[0]["t_acfn"]).ToString("yyyy") == "1753"|| Convert.ToDateTime(dt.Rows[0]["t_acfn"]).ToString("yyyy") == "1900" || (dt.Rows[0]["t_acfn"]).ToString() == string.Empty))
            {
                string fdate = "yyyy-MM-dd";
                txtActualFinishDate.Attributes.Add("value", fdate);

            }
            else
            {
                txtActualFinishDate.Attributes.Add("value", Convert.ToDateTime(dt.Rows[0]["t_acfn"]).ToString("yyyy-MM-dd"));
                btnUpdate.Enabled = false;
            }
          
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string sUsername = (string)(Session["Username"]);
            string Actual_SDate;
            string Actual_FDate;
            string Remarks;
            try
            {
                objProjectCls = new ProjectClass();
                if (txtActualStartDate.Text == "")
                    Actual_SDate = "";
                else { Actual_SDate= Convert.ToDateTime(txtActualStartDate.Text).ToString("yyyy-MM-dd"); }
                if (txtActualFinishDate.Text == "")
                    Actual_FDate = "";
                else { Actual_FDate = Convert.ToDateTime(txtActualFinishDate.Text).ToString("yyyy-MM-dd"); }
                if (txtRemarks.Text == "")
                    Remarks = "";
                else { Remarks = txtRemarks.Text.ToString(); }
                int res = objProjectCls.UpdatRecords(sUsername,Request.QueryString["cprj"], Request.QueryString["cact"], Actual_SDate, Actual_FDate,Remarks);
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Successfully Updated');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Not Updated');", true);
                }
            }

            catch (Exception ex)
            {
                string tt = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectProgressReport.aspx");
        }
    }
}