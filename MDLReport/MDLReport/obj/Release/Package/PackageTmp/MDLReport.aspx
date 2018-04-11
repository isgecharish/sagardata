<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="MDLReport.aspx.cs" Inherits="MDLReport.MDLReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #tbl tr td {
            min-width: 65px;
        }
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link href="css/calanderui.css" rel="stylesheet" />
    <script src="scripts/datepicker.js"></script>
    <script src="scripts/datepickerui.js"></script>
    <script>
        $(function () {

            $(".txtDate").datepicker({
                dateFormat: 'dd/mm/yy',
                // defaultDate: new date()
                // startDate: '-3d'
            });
            var currentDate = new Date();
            $(".txtDate").datepicker("setDate", currentDate)
        });

        function Export() {
            $("#divhed").show();
            window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=divTableDataHolder]').html()));
            e.preventDefault();
            $("#divhed").hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%
        ddlProject.SelectedValue = Request.QueryString["Project"];
        System.Data.DataSet dsMDLReport = new System.Data.DataSet();
        System.Data.DataTable dtHeader = new System.Data.DataTable();
        System.Data.DataTable dtDisciplineCount = new System.Data.DataTable();
        System.Data.DataTable dtPlannedPMDL = new System.Data.DataTable();
        System.Data.DataTable dtPlannedHoursPMAL = new System.Data.DataTable();
        System.Data.DataTable dtA1Euivalent = new System.Data.DataTable();
        System.Data.DataTable dtOutsourced = new System.Data.DataTable();
        System.Data.DataTable dtActualHoursPMAL = new System.Data.DataTable();
        System.Data.DataTable dtActualHoursPMDL = new System.Data.DataTable();

        MDLReport.ReportClass objReportClass = new MDLReport.ReportClass();

        if (Request.QueryString["Project"] != null)
            objReportClass.Project = Request.QueryString["Project"];
        else
            objReportClass.Project = "";
        if (Request.QueryString["InputDate"] != null)
            objReportClass.InputDate = Convert.ToDateTime(Request.QueryString["InputDate"]).ToString("yyyy-MM-dd 23:59:59");
        else
            objReportClass.InputDate = "";

        if (Request.QueryString["Project"] != null)
        {
            try
            {
                string Connection= objReportClass.GetEnv(Request.QueryString["env"]);
                dsMDLReport = objReportClass.GetMDLReport(Connection);
                if (dsMDLReport.Tables.Count > 0)
                {
                    dtHeader = dsMDLReport.Tables[0];
                    dtDisciplineCount = dsMDLReport.Tables[1];
                    dtPlannedPMDL = dsMDLReport.Tables[2];
                    dtPlannedHoursPMAL = dsMDLReport.Tables[3];
                    dtA1Euivalent = dsMDLReport.Tables[4];
                    dtOutsourced = dsMDLReport.Tables[5];
                    dtActualHoursPMAL = dsMDLReport.Tables[6];
                    dtActualHoursPMDL = dsMDLReport.Tables[7];


                    if (dtDisciplineCount.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dtDisciplineCount.NewRow();
                        dr[0] = "Total";
                        for (int i = 1; i < dtDisciplineCount.Columns.Count; i++)
                        {
                            dr[dtDisciplineCount.Columns[i].ColumnName] = dtDisciplineCount.Compute("Sum(" + dtDisciplineCount.Columns[i].ColumnName + ")", "");
                        }
                        dtDisciplineCount.Rows.Add(dr);
                        dtDisciplineCount.AcceptChanges();
                    }
                    //
                    if (dtPlannedPMDL.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dtPlannedPMDL.NewRow();
                        for (int i = 1; i < dtPlannedPMDL.Columns.Count; i++)
                        {
                            dr[dtPlannedPMDL.Columns[i].ColumnName] = dtPlannedPMDL.Compute("Sum(" + dtPlannedPMDL.Columns[i].ColumnName + ")", "");
                        }
                        dtPlannedPMDL.Rows.Add(dr);
                        dtPlannedPMDL.AcceptChanges();
                    }
                    //
                    if (dtPlannedHoursPMAL.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dtPlannedHoursPMAL.NewRow();
                        for (int i = 1; i < dtPlannedHoursPMAL.Columns.Count; i++)
                        {
                            dr[dtPlannedHoursPMAL.Columns[i].ColumnName] = dtPlannedHoursPMAL.Compute("Sum(" + dtPlannedHoursPMAL.Columns[i].ColumnName + ")", "");
                        }
                        dtPlannedHoursPMAL.Rows.Add(dr);
                        dtPlannedHoursPMAL.AcceptChanges();
                    }
                    //
                    if (dtA1Euivalent.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dtA1Euivalent.NewRow();
                        for (int i = 1; i < dtA1Euivalent.Columns.Count; i++)
                        {
                            dr[dtA1Euivalent.Columns[i].ColumnName] = dtA1Euivalent.Compute("Sum(" + dtA1Euivalent.Columns[i].ColumnName + ")", "");
                        }
                        dtA1Euivalent.Rows.Add(dr);
                        dtA1Euivalent.AcceptChanges();
                    }
                    if (dtOutsourced.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dtOutsourced.NewRow();
                        for (int i = 1; i < dtOutsourced.Columns.Count; i++)
                        {
                            dr[dtOutsourced.Columns[i].ColumnName] = dtOutsourced.Compute("Sum(" + dtOutsourced.Columns[i].ColumnName + ")", "");
                        }
                        dtOutsourced.Rows.Add(dr);
                        dtOutsourced.AcceptChanges();
                    }
                    if (dtActualHoursPMAL.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dtActualHoursPMAL.NewRow();
                        for (int i = 1; i < dtActualHoursPMAL.Columns.Count; i++)
                        {
                            dr[dtActualHoursPMAL.Columns[i].ColumnName] = dtActualHoursPMAL.Compute("Sum(" + dtActualHoursPMAL.Columns[i].ColumnName + ")", "");
                        }
                        dtActualHoursPMAL.Rows.Add(dr);
                        dtActualHoursPMAL.AcceptChanges();
                    }
                    if (dtActualHoursPMDL.Rows.Count > 0)
                    {
                        System.Data.DataRow dr = dtActualHoursPMDL.NewRow();
                        for (int i = 1; i < dtActualHoursPMDL.Columns.Count; i++)
                        {
                            dr[dtActualHoursPMDL.Columns[i].ColumnName] = dtActualHoursPMDL.Compute("Sum(" + dtActualHoursPMDL.Columns[i].ColumnName + ")", "");
                        }
                        dtActualHoursPMDL.Rows.Add(dr);
                        dtActualHoursPMDL.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Due to some technical issue record not found');", true);
            }
        }
    %>
    <div style="text-align: center; font-weight: bold;margin-top:15px" id="divHeding">Project - Engineering Progress</div>
    <div style="margin-top: 20px">
        <table>
            <tr>
                <td>Project:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProject" Width="100"></asp:DropDownList></td>
                <td>&nbsp &nbsp Report Date:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDate" CssClass="txtDate"></asp:TextBox></td>
                <td>&nbsp &nbsp
                    <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Display" /></td>
                <td>&nbsp &nbsp<input type="button" id="btnExport" value="Export to excel" onclick="Export()" /></td>
            </tr>
        </table>
    </div>
    <%--  <div runat="server" id="divNoRecord">No Record Found</div>--%>

    <%if (dtHeader.Rows.Count > 0)
        { %>

    <div style="margin-top: 20px" id="divTableDataHolder">
        <div style="display: none;">
            <table>
                <tr>
                    <td colspan="4"></td>
                    <td id="divhed" style="font-weight: bold">Project - Engineering Progress</td>
                </tr>
            </table>
        </div>
        <table border="1" style="border-collapse: collapse; font-size: 12px" id="tbl">
            <tr style="display: none">
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td><b>Product</b></td>
                <td><%Response.Write((dtHeader.Rows[0]["Product"]).ToString()); %></td>
                <td></td>
                <td><b>Project Id</b></td>
                <td><%Response.Write((dtHeader.Rows[0]["Project"]).ToString()); %></td>
                <td></td>
                <td><b>Project Name</b></td>
                <td colspan="2"><%Response.Write((dtHeader.Rows[0]["ProjectName"]).ToString()); %></td>
                <%--<td></td>--%>
                <td colspan="2"><b>Project Engg Start Date</b></td>
                <td><%Response.Write(Convert.ToDateTime((dtHeader.Rows[0]["ProjectEngStartDate"]).ToString()).ToString("dd-MM-yyyy")); %></td>
                <td colspan="2"><b>Project engg End Date( Base plan)</b></td>
                <td><%Response.Write(Convert.ToDateTime((dtHeader.Rows[0]["ProjectEngEndDate"]).ToString()).ToString("dd-MM-yyyy")); %></td>
                <td colspan="2"><b>Report as on Date</b></td>
                <td><%Response.Write(Convert.ToDateTime((dtHeader.Rows[0]["ReportDate"]).ToString()).ToString("dd-MM-yyyy")); %></td>
                <td colspan="2"><b>Project engg End Date( Look ahead)</b></td>
                <td><%Response.Write(Convert.ToDateTime((dtHeader.Rows[0]["ProjectEngEndDateLookAhead"]).ToString()).ToString("dd-MM-yyyy")); %></td>

            </tr>
            <tr>
                <td><b>Responcible Department</b></td>
                <td style="">Total no of Drg/Doc</td>
                <td colspan="6" style="text-align: center; ">No.of Drg/Doc</td>
                <td style="">A1 Equivalent Drawing Count</td>
                <td style="">No. of Outsourced Drg/Doc</td>
                <td colspan="2" style="">Draftman Hours (PMDL)(for released Drg/Doc)</td>
                <td colspan="2" style="">Checker Hours(PMDL)(for released Drg/Doc)</td>
                <td colspan="2" style="">Engineer Hours(PMDL)(for released Drg/Doc)</td>
                <td colspan="2" style="">Manager Hours(PMDL)(for released Drg/Doc)</td>
                <td colspan="2" style="">Total Hours (PMDL)(for released Drg/Doc)</td>
                <td colspan="2" style="">Draftman Hours (PMAL)(Based on per day distributed hours)</td>
                <td colspan="2" style="">Checker Hours(PMAL)(Based on per day distributed hours)</td>
                <td colspan="2" style="">Engineer Hours(PMAL)(Based on per day distributed hours)</td>
                <td colspan="2" style="">Manager Hours(PMAL)(Based on per day distributed hours)</td>
                <td colspan="2" style="">Total Hours (PMAL)(Total of all PMAL fields)</td>
                <td colspan="2" style="">Grand Total Hours (Project)(Total of all previous fields)</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td colspan="2">Planned as per BaseLine Schedule</td>
                  <td colspan="2">Planned as per Revised Schedule</td>
                <td colspan="2">Released</td>
                <td>Released</td>
                <td>Released</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
                <td>Planned</td>
                <td>Actual</td>
            </tr>
            <tr>
                <td style=""></td>
                <td style="">Total count of drawings in PMDL </td>
                <td style="">Planned count as on report date</td>
                <td style="">% of Total count</td>
                <td style="">Planned count as on report date</td>
                <td style="">% of Total count</td>
                <td style="">Released count as on report date</td>
                <td style="">% of Total Released count</td>
                <td style=""></td>
                <td style=""></td>
                <td style=""></td>
                <td style=""></td>
                <td style=""></td>
                <td style=""></td>
                <td style=""></td>
                <td style=""></td>
                <td style=""></td>
                <td style=""></td>
                <td></td>
                <td></td>
                <td colspan="8" style="text-align: center">As on Report Date</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td style="text-align: right">.=c/b * 100</td>
                 <td></td>
                <td style="text-align: right">.=c/b * 100</td>
                <td></td>
                <td style="text-align: right">.=e/b * 100</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>

            <%if (dtDisciplineCount.Rows.Count > 0)
                {
                    //  divNoRecord.Visible = false;
                    for (int i = 0; i < dtDisciplineCount.Rows.Count; i++)
                    { %>
            <tr>
                <td>
                    <%Response.Write(dtDisciplineCount.Rows[i]["ResponsibleDiscipline"].ToString()); %>
                </td>
                <td style="text-align: right"><%Response.Write(dtDisciplineCount.Rows[i]["TotalnoofDrgDoc"].ToString()); %></td>
                <td style="text-align: right">
                    <%Response.Write(dtDisciplineCount.Rows[i]["BaselinePlannedCount"].ToString()); %>
                </td>
                <td style="text-align: right"><%Response.Write(dtDisciplineCount.Rows[i]["BaselinePlannedCount"].ToString() != "0" ? Math.Round(((Convert.ToDecimal(dtDisciplineCount.Rows[i]["BaselinePlannedCount"]) / Convert.ToDecimal(dtDisciplineCount.Rows[i]["TotalnoofDrgDoc"])) * 100), 2).ToString("0.00") : "0.00"); %></td>
                 <td style="text-align: right">
                    <%Response.Write(dtDisciplineCount.Rows[i]["RevisedPlannedCount"].ToString()); %>
                </td>
                <td style="text-align: right"><%Response.Write(dtDisciplineCount.Rows[i]["RevisedPlannedCount"].ToString() != "0" ? Math.Round(((Convert.ToDecimal(dtDisciplineCount.Rows[i]["RevisedPlannedCount"]) / Convert.ToDecimal(dtDisciplineCount.Rows[i]["TotalnoofDrgDoc"])) * 100), 2).ToString("0.00") : "0.00"); %></td>
                <td style="text-align: right"><%Response.Write(dtDisciplineCount.Rows[i]["ReleasedCount"].ToString()); %>
                </td>
                <td style="text-align: right"><%Response.Write(dtDisciplineCount.Rows[i]["ReleasedCount"].ToString() != "0" ? Math.Round(((Convert.ToDecimal(dtDisciplineCount.Rows[i]["ReleasedCount"]) / Convert.ToDecimal(dtDisciplineCount.Rows[i]["TotalnoofDrgDoc"])) * 100), 2).ToString("0.00") : "0.00"); %></td>
                <td style="text-align: right"><%Response.Write(dtA1Euivalent.Rows[i]["A1EquivalentReleasedCount"].ToString()); %></td>
                <td style="text-align: right"><%Response.Write(dtOutsourced.Rows[i]["NoOfOutsourced"].ToString()); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedPMDL.Rows[i]["PlannedDraftingHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMDL.Rows[i]["DraftmanActualHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedPMDL.Rows[i]["PlannedCheckingHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMDL.Rows[i]["CheckerActualHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedPMDL.Rows[i]["PlannedEngineerHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMDL.Rows[i]["LeadEngineerActualHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedPMDL.Rows[i]["PlannedLeadManagerHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMDL.Rows[i]["LeadManagerActualHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedPMDL.Rows[i]["TotalPlannedHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMDL.Rows[i]["TotalActualHors"])), 2).ToString("0.00")); %></td>

                <!-- PMAL Hours -->
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedHoursPMAL.Rows[i]["DraftmanPlannedHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMAL.Rows[i]["DraftmanActualHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedHoursPMAL.Rows[i]["CheckerPlannedHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMAL.Rows[i]["CheckerActualHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedHoursPMAL.Rows[i]["EngineerPlannedHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMAL.Rows[i]["EngineerActualHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedHoursPMAL.Rows[i]["ManagerPlannedHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMAL.Rows[i]["ManagerActualHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedHoursPMAL.Rows[i]["TotalPlannedHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMAL.Rows[i]["TotalActualHors"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtPlannedPMDL.Rows[i]["TotalPlannedHours"]) + Convert.ToDecimal(dtPlannedHoursPMAL.Rows[i]["TotalPlannedHours"])), 2).ToString("0.00")); %></td>
                <td style="text-align: right"><%Response.Write(Math.Round((Convert.ToDecimal(dtActualHoursPMDL.Rows[i]["TotalActualHors"]) + Convert.ToDecimal(dtActualHoursPMAL.Rows[i]["TotalActualHors"])), 2).ToString("0.00")); %></td>
            </tr>
            <%}
                }
                else
                {
                    // divNoRecord.Visible = true;
                } %>
        </table>
    </div>
    <%}
        else
        {
            // divNoRecord.Visible = true;
        } %>
</asp:Content>
