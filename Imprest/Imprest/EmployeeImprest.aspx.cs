using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Imprest
{
    public partial class EmployeeImprest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmpImprest();
        }
        protected void EmpImprest()
        {
            string Id = Request.QueryString["empId"];
            DataTable dtImprest = new DataTable();
            dtImprest.Columns.Add("Company");
            dtImprest.Columns.Add("DocType");
            dtImprest.Columns.Add("DocNo");
            dtImprest.Columns.Add("Date");
            dtImprest.Columns.Add("Debit");
            dtImprest.Columns.Add("Credit");
            dtImprest.Columns.Add("Reference");

            List<string> list = new List<string>();
            int empId=0;
            try
            {
                empId = Convert.ToInt32(Id);
            }
            catch(Exception ex)
            {

            }

            using (StreamReader sr = new StreamReader(Server.MapPath("~/Imprest/imprestdata.txt")))
            {

                while (!sr.EndOfStream)
                {
                    string lines = sr.ReadLine();
                    if (lines.Contains("<StartEmp_" + empId + " >"))
                    {
                        while ((lines = sr.ReadLine()) != "<EndEmp_"+ empId+" >")
                        {
                            list.Add(lines); // Add to list.
                        }

                        string[] empcontact = list[0].Split('|');
                        spdateFrom.InnerHtml = empcontact[2];
                        spdateTo.InnerHtml = empcontact[3];
                        spCode.InnerHtml = empcontact[0];
                        spName.InnerHtml = empcontact[1];
                        spBalance.InnerHtml = empcontact[4];
                        divEmplDetails.Visible = true;
                        divNoRecord.Visible = false;
                        for (int j = 1; j <list.Count ; j++)
                        {
                            if (list[j].Contains('|'))
                            {
                                string[] empDetail = list[j].Split('|');
                                empDetail = empDetail.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                dtImprest.Rows.Add(empDetail[0], empDetail[1], empDetail[2], empDetail[3], empDetail[4], empDetail[5], empDetail[6]);
                            }
                            if (list[j].Contains("Debit"))
                            {
                                dtImprest.Rows.Add("Closing Balance as on " + spdateTo.InnerHtml, list[j + 1], "", "", "", "", "");
                            }
                            if (list[j].Contains("Credit"))
                            {
                                dtImprest.Rows.Add("Closing Balance as on " + spdateTo.InnerHtml, "", list[j + 1], "", "", "", "");
                            }
                            if (list[j].Contains("Db/Cr."))
                            {
                                dtImprest.Rows.Add("Closing Balance as on " + spdateTo.InnerHtml, "", list[j + 1], "", "", "", "");
                            }
                        }

                        break;
                    }
                    else
                    {
                        divNoRecord.Visible = true;
                        divEmplDetails.Visible = false;

                    }
                }
            }
            gdImprset.DataSource = dtImprest;
            gdImprset.DataBind();
        }
        protected void gdImprset_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataRow row = ((System.Data.DataRowView)e.Row.DataItem).Row;
                if (row["Company"].ToString() == "Closing Balance as on " + spdateTo.InnerHtml)
                {
                    e.Row.BackColor = Color.Green;
                    e.Row.Cells[0].ColumnSpan = 4;
                    e.Row.Cells.RemoveAt(6);
                    e.Row.Cells.RemoveAt(5);
                    e.Row.Cells.RemoveAt(4);
                    e.Row.ForeColor = Color.White;
                }
            }
        }
    }
}