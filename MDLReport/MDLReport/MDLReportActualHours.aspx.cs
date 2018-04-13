using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MDLReport
{
    public partial class MDLReportActualHours : System.Web.UI.Page
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
            try
            {
                if (ddlProject.SelectedValue != "Select" && txtDate.Text != "")
                {
                    Response.Redirect("MDLReportActualHours.aspx?env=" + Request.QueryString["env"] + "&Project=" + ddlProject.SelectedValue + "&InputDate=" + txtDate.Text.Trim());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Select all fields.');", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }


        //int total = 0;
        //protected void gvPmdldata_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Amount_column"));
        //    }
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        Label lblAmount = (Label)e.Row.FindControl("amountLabel");
        //        lblAmount.Text = total.ToString();
        //    }
        //}

        protected void gvPmdldata()
        {
            //objReportClass = new ReportClass();
            //objReportClass.Project = ddlProject.SelectedValue;
            //DataSet ds = objReportClass.GetManHourConsumed_Disciplinewise();
            //DataTable dtPmdl = ds.Tables[0];
            ////DataTable dtPmal = ds.Tables[1];

            //if (dtPmdl.Rows.Count > 0 && dtPmdl.Rows[0][0].ToString() != "")
            //{

            //    dtPmdl.Columns.Remove("MonthNoPMAL");
            //    dtPmdl.Columns.Remove("MonthPMAL");
            //    gvPmdldata.DataSource = dtPmdl;
            //    gvPmdldata.DataBind();
            //    //for (int i = 0; i < dtPmdl.Rows.Count; i++)
            //    //{
            //    //    for (int j = 2; j < dtPmdl.Columns.Count; j++)
            //    //    {
            //    //        gvPmdldata.FooterRow.Cells[i].Text = dtPmdl.Compute("Sum('" + dtPmdl.Rows[i][j] + "')", "").ToString();
            //    //    }
            //    //}
            //    gvPmdldata.Visible = true;
            //    divData.Visible = true;
            //}
            //else
            //{
            //    gvPmdldata.Visible = false;
            //    divData.Visible = false;
            //}

            //int columnscount = gvPmdldata.Columns.Count;
            //decimal total = 0;
            //foreach (GridViewRow row in gvPmdldata.Rows)
            //{
            //    int rowcount = this.gvPmdldata.Rows.Count;

            //    for (int i = 0; i < rowcount; i++)
            //    {
            //        for(int j = 2; j < row.Cells.Count; j++)
            //        {
            //            total +=Convert.ToDecimal(row.Cells[j].Text);

            //        }
            //    }
            //}

            //if (dtPmal.Rows.Count > 0 && dtPmal.Rows[0][0].ToString() != "")
            //{
            //    gvPmaldata.DataSource = dtPmal;
            //    gvPmaldata.DataBind();
            //    gvPmaldata.Visible = true;
            //}
            //else
            //{
            //    gvPmaldata.Visible = false;
            //}
            //decimal total = 0;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    // System.Data.DataRow row = ((System.Data.DataRowView)e.Row.DataItem).Row;

            //    for (int i = 2; i < e.Row.Cells.Count; i++)
            //    {

            //        if (!string.IsNullOrEmpty(e.Row.Cells[i].Text))
            //        {
            //            total = total + Convert.ToDecimal(e.Row.Cells[i].Text);
            //        }
            //        if (e.Row.RowType == DataControlRowType.Footer)
            //        {
            //            Label label = new Label();
            //            e.Row.Cells[i].Text = total.ToString();
            //            label.Text = "total" + " " + total;
            //            e.Row.Cells[i].Controls.Add(label);
            //        }
            //    }
            // }
        }
    }
}