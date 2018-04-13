<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="MDLReportActualHours.aspx.cs" Inherits="MDLReport.MDLReportActualHours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #tablePMDL tr td:first-child {
            text-align: left;
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
        System.Data.DataSet dsMDLReport = new System.Data.DataSet();
        MDLReport.ReportClass objReportClass = new MDLReport.ReportClass();
        System.Data.DataTable dtActualHoursPMDL = new System.Data.DataTable();
        System.Data.DataTable dtActualHoursPMAL = new System.Data.DataTable();
        System.Data.DataTable dtPlannedHoursPMDL = new System.Data.DataTable();
        System.Data.DataTable dtPlannedHoursPMAL = new System.Data.DataTable();
        System.Data.DataTable dtReleasedPMDLCount = new System.Data.DataTable();
        System.Data.DataTable dtPlannedPMDLCount = new System.Data.DataTable();

        if (Request.QueryString["Project"] != null)
        {
            objReportClass.Project = Request.QueryString["Project"];
            spProjectName.InnerHtml = Request.QueryString["Project"];
            spProjectId.InnerHtml = Request.QueryString["Project"];
        }
        else
        {
            objReportClass.Project = "";
        }
        if (Request.QueryString["InputDate"] != null)
        {
            objReportClass.InputDate = Convert.ToDateTime(Request.QueryString["InputDate"]).ToString("yyyy-MM-dd 23:59:59");
            spInputDate.InnerHtml = Request.QueryString["InputDate"];
        }
        else
        {
            objReportClass.InputDate = "";
        }
        if (Request.QueryString["Project"] != null && Request.QueryString["InputDate"] != null)
        {
            string Connection= objReportClass.GetEnv(Request.QueryString["env"]);
            dsMDLReport = objReportClass.GetManHourConsumed_Disciplinewise(Connection);
            dtActualHoursPMDL = dsMDLReport.Tables[0];
            dtActualHoursPMAL = dsMDLReport.Tables[1];
            dtPlannedHoursPMDL = dsMDLReport.Tables[2];
            dtPlannedHoursPMAL = dsMDLReport.Tables[3];
            dtReleasedPMDLCount = dsMDLReport.Tables[4];
            dtPlannedPMDLCount = dsMDLReport.Tables[5];

            if (dtActualHoursPMDL.Rows.Count > 0 && dtActualHoursPMDL.Rows[0][0].ToString() != "")
            {
                System.Data.DataRow dr = dtActualHoursPMDL.NewRow();
                dr[1] = "Total hour consumed as on input date";

                for (int i = 2; i < dtActualHoursPMDL.Columns.Count; i++)
                {
                    if (dtActualHoursPMDL.Columns[i].ColumnName == "C&I")
                        dtActualHoursPMDL.Columns[i].ColumnName = "CI";
                    dr[dtActualHoursPMDL.Columns[i].ColumnName] = dtActualHoursPMDL.Compute("Sum(" + dtActualHoursPMDL.Columns[i].ColumnName + ")", "");
                    //  dtActualHoursPMDL.Columns[i].ColumnName = "C&I";
                }
                dtActualHoursPMDL.Rows.Add(dr);
                dtActualHoursPMDL.AcceptChanges();
            }

            if (dtActualHoursPMAL.Rows.Count > 0 && dtActualHoursPMAL.Rows[0][0].ToString() != "")
            {
                System.Data.DataRow dr = dtActualHoursPMAL.NewRow();

                dr[1] = "Total hour consumed as on input date";

                for (int i = 2; i < dtActualHoursPMAL.Columns.Count; i++)
                {
                    if (dtActualHoursPMAL.Columns[i].ColumnName == "C&I")
                        dtActualHoursPMAL.Columns[i].ColumnName = "CI";
                    dr[dtActualHoursPMAL.Columns[i].ColumnName] = dtActualHoursPMAL.Compute("Sum(" + dtActualHoursPMAL.Columns[i].ColumnName + ")", "");
                    // dtActualHoursPMAL.Columns[i].ColumnName = "C&I";
                }
                dtActualHoursPMAL.Rows.Add(dr);
                dtActualHoursPMAL.AcceptChanges();
            }
        }
        else
        {

        }
    %>
        <div style="text-align: center; font-weight: bold" id="divHeding">Overall Engineering Manhours</div>
    <div style="margin-top:20px;margin-left:20px">
        <table>
            <tr>
                <td>Project:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProject" Width="100"></asp:DropDownList></td>
                <td>&nbsp &nbsp Report Date</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDate" CssClass="txtDate"></asp:TextBox></td>
                <td>&nbsp &nbsp
                    <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Display" /></td>
                <td>&nbsp &nbsp
                    <input type="button" id="btnExport" value="Export to excel" onclick="Export()" /></td>
            </tr>
        </table>
    </div>

    <%   if (Request.QueryString["Project"] != null && Request.QueryString["InputDate"] != null)
        { %>

    <div style="font-size: 12px; margin-top: 20px;margin-left:20px" id="divTableDataHolder">
         <div style="display:none;"><table><tr><td colspan="4"></td><td  id="divhed" style="font-weight: bold">Overall Engineering Manhours</td></tr></table></div>
        <table border="1" style="border-collapse: collapse; width: 60%">
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
            </tr>
            <tr>
                <td colspan="2">
                    <img src="image/logo.png" /></td>
                <td colspan="11" style="height: 30px">Overall Engineering Manhours</td>
            </tr>
            <tr>
                <td colspan="2">Project</td>
                <td colspan="5"><span runat="server" id="spProjectName"></span>
                </td>
                <td colspan="3"></td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td colspan="2">Project ID </td>
                <td colspan="5"><span runat="server" id="spProjectId"></span>
                </td>
                <td colspan="3">As on Date</td>
                <td colspan="3"><span runat="server" id="spInputDate"></span></td>
            </tr>
        </table>

        <!--  PMDL TAble-->
        <%  if (dtActualHoursPMDL.Rows.Count > 0 && dtActualHoursPMDL.Rows[0][0].ToString() != "")
            {
        %>
        <table border="1" style="border-collapse: collapse; width: 60%" id="tablePMDL">
               <tr>
                <td colspan="14"><strong>Man Hour Consumed  (hrs) – PMDL</strong></td>
            </tr>
            <tr>
                <% for (int k = 0; k < dtActualHoursPMDL.Columns.Count; k++)
                    { %>
                <th><%Response.Write(dtActualHoursPMDL.Columns[k].ColumnName.ToString()); %></th>
                <%}%>
            </tr>
            <%
                for (int i = 0; i < dtActualHoursPMDL.Rows.Count; i++)
                {
            %>
            <tr class="trHours">
                <%   for (int j = 0; j < 2; j++)
                    {
                %>
                <td style="text-align: left">
                    <%Response.Write(dtActualHoursPMDL.Rows[i][j].ToString()); %>
                </td>
                <%}
                %>
                <%   for (int j = 2; j < dtActualHoursPMDL.Columns.Count; j++)
                    {
                %>
                <td style="text-align: right;width:200px">
                    <%Response.Write(dtActualHoursPMDL.Rows[i][j].ToString() != "" ? Math.Round(Convert.ToDecimal(dtActualHoursPMDL.Rows[i][j]), 2).ToString("0.00") : "0.00"); %>
                </td>
                <%}

                %>
            </tr>
            <%}
            %>

            <!-- Planned Hours -->
            <%--   <tr>
                <th colspan="2"></th>
                <% for (int k = 1; k < dtPlannedHoursPMDL.Columns.Count; k++)
                    { %>
                <th><%Response.Write(dtPlannedHoursPMDL.Columns[k].ColumnName.ToString()); %></th>
                <%}%>
            </tr>--%>
            <tr>
                <td colspan="2" style="border: none">Planned Man Hours(Total planed for project)</td>
                <% if (dtPlannedHoursPMDL.Rows.Count > 0)
                    {
                        for (int m = 2; m < dtPlannedHoursPMDL.Columns.Count; m++)
                        { %>
                <td style="text-align: right;width:200px"><%Response.Write(dtPlannedHoursPMDL.Rows[0][m].ToString() != "" ? Math.Round(Convert.ToDecimal(dtPlannedHoursPMDL.Rows[0][m]), 2).ToString("0.00") : "0.00"); %></td>
                <%}
                %>
            </tr>

            <!--Balance in Hours -->
            <tr>
                <td colspan="2">Balance in Hours</td>
                <%int count = dtActualHoursPMDL.Rows.Count - 1; %>
                <%for (int l = 2; l < dtActualHoursPMDL.Columns.Count; l++)
                    { %>
                <%if (dtActualHoursPMDL.Rows[count][l].ToString() == "")
                        dtActualHoursPMDL.Rows[count][l] = 0;

                    if (dtPlannedHoursPMDL.Rows[0][l].ToString() == "")
                        dtPlannedHoursPMDL.Rows[0][l] = 0;
                %>
                <td style="text-align: right;width:200px"><%Response.Write(Math.Round(Convert.ToDecimal(dtPlannedHoursPMDL.Rows[0][l]) - Convert.ToDecimal(dtActualHoursPMDL.Rows[count][l]), 2).ToString("0.00")); %></td>
                <%} %>
            </tr>

            <!--% OF Man hour utilised -->
            <tr>
                <td colspan="2">% OF Man hour utilised</td>
                <%int countPMDLRow = dtActualHoursPMDL.Rows.Count - 1; %>
                <%for (int l = 2; l < dtActualHoursPMDL.Columns.Count; l++)
                    { %>
                <%if (dtActualHoursPMDL.Rows[countPMDLRow][l].ToString() == "")
                        dtActualHoursPMDL.Rows[countPMDLRow][l] = 0;

                    if (dtPlannedHoursPMDL.Rows[0][l].ToString() == "")
                        dtPlannedHoursPMDL.Rows[0][l] = 0;

                    decimal V1 = Convert.ToDecimal(dtPlannedHoursPMDL.Rows[0][l]);
                    decimal V2 = Convert.ToDecimal(dtPlannedHoursPMDL.Rows[0][l]) - Convert.ToDecimal(dtActualHoursPMDL.Rows[count][l]);
                    decimal diff = V1 - V2;
                    decimal sumdivby2 = (V1 + V2) / 2;
                    decimal FinalPerc;
                    if (sumdivby2 == 0)
                        FinalPerc = 0;
                    else
                        FinalPerc = (diff / sumdivby2) * 100;
                %>
                <td style="text-align: right;width:200px"><%Response.Write(Math.Round(FinalPerc, 2).ToString("0.00")); %></td>

                <%} %>
            </tr>

            <!-- % Drg/Doc Released	-->
            <tr>
                <td colspan="2">% Drg/Doc Released	</td>

                <%for (int l = 0; l < dtReleasedPMDLCount.Columns.Count; l++)
                    { %>
                <%if (dtReleasedPMDLCount.Rows[0][l].ToString() == "")
                        dtReleasedPMDLCount.Rows[0][l] = 0;

                    if (dtPlannedPMDLCount.Rows[0][l].ToString() == "")
                        dtPlannedPMDLCount.Rows[0][l] = 0;

                    decimal RCount = Convert.ToDecimal(dtReleasedPMDLCount.Rows[0][l]);
                    decimal PCount = Convert.ToDecimal(dtPlannedPMDLCount.Rows[0][l]);
                    decimal difference = RCount - PCount;
                    decimal sumdiv = (RCount + PCount) / 2;
                    decimal DocPerc;
                    if (sumdiv == 0)
                        DocPerc = 0;
                    else
                        DocPerc = (difference / sumdiv) * 100;
                %>
                <td style="text-align: right;width:200px"><%Response.Write(Math.Round(DocPerc, 2).ToString("0.00")); %></td>

                <%}
                    } %>
            </tr>
        </table>

        <!----------------------------------------------------------------------------------------------------------------------------- -->
        <!-- PMAL TAble -->
        <%  if (dtActualHoursPMAL.Rows.Count > 0 && dtActualHoursPMAL.Rows[0][0].ToString() != "")
            {
        %>
        <table border="1" style="border-collapse: collapse; width: 60%; margin-top: 20px">
            <tr>
                <td colspan="14"><strong>Man Hour Consumed  (hrs) for  PAL</strong></td>
            </tr>

            <tr>
                <% for (int k = 0; k < dtActualHoursPMAL.Columns.Count; k++)
                    { %>
                <th><%Response.Write(dtActualHoursPMAL.Columns[k].ColumnName.ToString()); %></th>
                <%}%>
            </tr>

            <%
                for (int i = 0; i < dtActualHoursPMAL.Rows.Count; i++)
                {
            %>
            <tr>
                <%   for (int j = 0; j < 2; j++)
                    {
                %>
                <td style="text-align:left">
                    <%Response.Write(dtActualHoursPMAL.Rows[i][j].ToString()); %>
                </td>

                <%} %>

                <%   for (int j = 2; j < dtActualHoursPMAL.Columns.Count; j++)
                    {
                %>
                <td style="text-align:right;width:200px">
                    <%Response.Write(dtActualHoursPMAL.Rows[i][j].ToString() != "" ? Math.Round(Convert.ToDecimal(dtActualHoursPMAL.Rows[i][j]), 2).ToString("0.00") : "0.00"); %>
                </td>

                <%} %>
            </tr>

            <%}

            %>

            <!-- Planned Hours -->
            <%--    <tr>
                <th colspan="2"></th>
                <% for (int k = 1; k < dtPlannedHoursPMAL.Columns.Count; k++)
                    { %>
                <th><%Response.Write(dtPlannedHoursPMAL.Columns[k].ColumnName.ToString()); %></th>
                <%}%>
            </tr>--%>

            <tr>
                <td colspan="2">Planned Man Hours(Total planed for project)</td>
                <%
                    if (dtPlannedHoursPMAL.Rows.Count > 0)
                    {
                        for (int m = 2; m < dtPlannedHoursPMAL.Columns.Count; m++)
                        { %>

                <td style="text-align: right;width:200px"><%Response.Write(dtPlannedHoursPMAL.Rows[0][m].ToString() != "" ? Math.Round(Convert.ToDecimal(dtPlannedHoursPMAL.Rows[0][m]), 2).ToString("0.00") : "0.00"); %></td>
                <%}

                %>
            </tr>

            <!--Balance in Hours -->
            <tr>
                <td colspan="2">Balance in Hours</td>
                <%int countPMAl = dtActualHoursPMAL.Rows.Count - 1; %>
                <%for (int l = 2; l < dtActualHoursPMAL.Columns.Count; l++)
                    { %>
                <%if (dtActualHoursPMAL.Rows[countPMAl][l].ToString() == "")
                        dtActualHoursPMAL.Rows[countPMAl][l] = 0;

                    if (dtPlannedHoursPMAL.Rows[0][l].ToString() == "")
                        dtPlannedHoursPMAL.Rows[0][l] = 0;
                %>

                <td style="text-align: right;width:200px"><%Response.Write(Math.Round(Convert.ToDecimal(dtPlannedHoursPMAL.Rows[0][l]) - Convert.ToDecimal(dtActualHoursPMAL.Rows[countPMAl][l]), 2).ToString("0.00")); %></td>

                <%} %>
            </tr>
            <!--End Balance in Hours -->

            <!--% OF Man hour utilised -->
            <tr>
                <td colspan="2">% OF Man hour utilised</td>
                <%int countPMALRow = dtActualHoursPMAL.Rows.Count - 1; %>
                <%for (int l = 2; l < dtActualHoursPMAL.Columns.Count; l++)
                    { %>
                <%if (dtActualHoursPMAL.Rows[countPMALRow][l].ToString() == "")
                        dtActualHoursPMAL.Rows[countPMALRow][l] = 0;

                    if (dtPlannedHoursPMAL.Rows[0][l].ToString() == "")
                        dtPlannedHoursPMAL.Rows[0][l] = 0;

                    decimal V1 = Convert.ToDecimal(dtPlannedHoursPMAL.Rows[0][l]);
                    decimal V2 = Convert.ToDecimal(dtPlannedHoursPMAL.Rows[0][l]) - Convert.ToDecimal(dtActualHoursPMAL.Rows[countPMALRow][l]);
                    decimal diff = V1 - V2;
                    decimal sumdivby2 = (V1 + V2) / 2;
                    decimal FinalPerc;
                    if (sumdivby2 == 0)
                        FinalPerc = 0;
                    else
                        FinalPerc = (diff / sumdivby2) * 100;
                %>
                <td style="text-align: right;width:200px"><%Response.Write(Math.Round(FinalPerc, 2).ToString("0.00")); %></td>

                <%}
                %>
            </tr>
            <!--% OF Man hour utilised  -->
        </table>
    </div>

    <%}
                }
            }
        }%>
</asp:Content>
