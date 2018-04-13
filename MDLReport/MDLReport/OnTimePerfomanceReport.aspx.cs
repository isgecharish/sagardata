using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MDLReport
{
    public partial class OnTimePerfomanceReport : System.Web.UI.Page
    {
        ReportClass objReportClass;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            objReportClass = new ReportClass();
            string Connection = objReportClass.GetEnv(Request.QueryString["env"]);
            DataSet ds = objReportClass.GetonTimePerfomanceReport(Connection);
            DataTable dtBaseLine = new DataTable();
            DataTable dtRevised = new DataTable();
            DataRow dr = null;
            if (ds.Tables.Count > 0)
            {
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

                }
            }
        }
    }
}