using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;

namespace ExpenseReport
{
    public partial class SanctionBalanceReport : System.Web.UI.Page
    {
        Sanction objSanction;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    BindDevision();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found.');", true);
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void BindDevision()
        {
            try
            {
                objSanction = new Sanction();
                DataTable dt = objSanction.GetDivision(Request.QueryString["env"]);
                ddlDevision.DataSource = dt;
                ddlDevision.DataTextField = "Division";
                ddlDevision.DataValueField = "Division";
                ddlDevision.DataBind();
                ddlDevision.Items.Insert(0, "Select Devision");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found.');", true);
            }
        }
        protected void ddlDevision_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjectCode();
        }
        private void BindProjectCode()
        {
            try
            {
                objSanction = new Sanction();
                objSanction.Division = ddlDevision.SelectedValue;
                DataTable dt = objSanction.GetProjectCode(Request.QueryString["env"]);
                lstProjectCode.DataSource = dt;
                lstProjectCode.DataTextField = "ProjectCode";
                lstProjectCode.DataValueField = "ProjectCode";
                lstProjectCode.DataBind();
                lstProjectCode.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found.');", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "onchangeDivision();", true);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Cache.Remove("Balance");
            BindGrid();
        }
        private void BindGrid()
        {
            try
            {
                string listItem = string.Empty;
                DataTable dt = new DataTable();
                if (HttpContext.Current.Cache["Balance"] == null)
                {
                    foreach (ListItem item in lstProjectCode.Items)
                    {
                        if (item.Selected)
                        {
                            listItem += "'" + item.Value + "',";
                        }
                    }
                    listItem = listItem.TrimEnd(',');
                    objSanction = new Sanction();
                    objSanction.Division = ddlDevision.SelectedValue;
                    objSanction.ProjectCode = listItem;
                
                    if (rdSiteSanction.Checked)
                    {
                        dt = objSanction.GeSitetSanctionBalance(Request.QueryString["env"]);
                    }
                    else if (rdTravelSanction.Checked)
                    {
                        dt = objSanction.GeTraveltSanctionBalance(Request.QueryString["env"]);
                    }
                    else { }

                    HttpContext.Current.Cache.Insert("Balance", dt, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                }
                else
                {
                    dt = (DataTable)HttpContext.Current.Cache["Balance"];
                }
                if (dt.Rows.Count > 0)
                {
                    gvBalnaceReport.DataSource = dt;
                    gvBalnaceReport.DataBind();
                    btnExportToExcel.Visible = true;
                    btnPrint.Visible = true;
                }
                else
                {
                    btnExportToExcel.Visible = false;
                    btnPrint.Visible = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Record not found');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found.');", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "onchangeDivision();", true);
        }
        protected void gvBalnaceReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBalnaceReport.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvBalnaceReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //OnRowDataBound="gvBalnaceReport_RowDataBound"
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    System.Data.DataRow row = ((System.Data.DataRowView)e.Row.DataItem).Row;
                    int Balance = Convert.ToInt32(row["L4Level"].ToString());
                    if (Balance == 0)
                    {
                        e.Row.Cells[4].BackColor = Color.Red;
                    }
                    if (rdSiteSanction.Checked)
                    {
                        gvBalnaceReport.HeaderRow.Cells[3].Text = "Site L3 Level Balance";
                        gvBalnaceReport.HeaderRow.Cells[4].Text = "Site L4 Level Balance";
                        if (Balance <= 50000 && Balance > 0)
                        {
                            e.Row.Cells[4].BackColor = Color.Yellow;
                        }
                    }
                    else if (rdTravelSanction.Checked)
                    {
                        gvBalnaceReport.HeaderRow.Cells[3].Text = "Travel L3 Level Balance";
                        gvBalnaceReport.HeaderRow.Cells[4].Text = "Travel L4 Level Balance";
                        if (Balance <= 10000 && Balance > 0)
                        {
                            e.Row.Cells[4].BackColor = Color.Yellow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=BalanceReport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //To Export all pages
                    gvBalnaceReport.AllowPaging = false;
                    this.BindGrid();
                    foreach (TableCell cell in gvBalnaceReport.HeaderRow.Cells)
                    {
                        // gvBalnaceReport.HeaderRow.BackColor = Color.Gray;
                        gvBalnaceReport.HeaderRow.Height = 30;
                    }
                    gvBalnaceReport.RenderControl(hw);
                    // string filePath = Server.MapPath("BalanceReport{0}.xls");
                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    //  Response.TransmitFile(filePath);
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
           // spDevision.InnerHtml = ddlDevision.SelectedValue;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "onchangeDivision();", true);
        }
    }
}