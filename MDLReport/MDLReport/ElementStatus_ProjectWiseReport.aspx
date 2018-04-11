<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ElementStatus_ProjectWiseReport.aspx.cs" Inherits="MDLReport.ElementStatus_ProjectWiseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tdCount
        {
            text-align:right;
        }
    </style>
    <script>
        function Export() {
            $("#divhead").show();
            window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=divData]').html()));
            e.preventDefault();
            $("#divhead").hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        System.Data.DataSet dsReport = new System.Data.DataSet();
        System.Data.DataTable dtTotalElementCount = new System.Data.DataTable();
        System.Data.DataTable dtCompletedElementCount = new System.Data.DataTable();
        System.Data.DataTable dtPartialElementCount = new System.Data.DataTable();
        System.Data.DataTable dtTotalDesiplineCount = new System.Data.DataTable();
        System.Data.DataTable dtCompltedDesiplineCount = new System.Data.DataTable();
        System.Data.DataTable dtPartialDesiplineCount = new System.Data.DataTable();

        MDLReport.ReportClass objReportClass = new MDLReport.ReportClass();
        if (Request.QueryString["From"] != null)
            objReportClass.ProjectFrom = Request.QueryString["From"];
        else
            objReportClass.ProjectFrom = "";
        if (Request.QueryString["To"] != null)
            objReportClass.ProjectTo = Request.QueryString["To"];
        else
            objReportClass.ProjectTo = "";

          string Connection= objReportClass.GetEnv(Request.QueryString["env"]);
          dsReport = objReportClass.GetElementStatus_ProjectWise(Connection);

        if (dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows[0][0].ToString()!="")
        {
            dtTotalElementCount = dsReport.Tables[0];
            dtCompletedElementCount = dsReport.Tables[1];
            dtPartialElementCount = dsReport.Tables[2];
            dtTotalDesiplineCount = dsReport.Tables[3];
            dtCompltedDesiplineCount = dsReport.Tables[4];
            dtPartialDesiplineCount = dsReport.Tables[5];
        }
        else
        {

        }
    %>
    <div style="text-align: center; font-weight: bold" id="divHeding">Element Status ProjectWise Report</div>
    <div style="margin-top: 20px;margin-left:20px">
        <table>
            <tr>
                <td>Project From:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProjectFrom" Width="100"></asp:DropDownList></td>
                <td>&nbsp &nbsp Project To:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProjectTo" Width="100"></asp:DropDownList></td>
                <td>&nbsp &nbsp
                    <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Display" /></td>
                 <td>&nbsp &nbsp<input type="button" id="btnExport" value="Export to excel"  onclick="Export()"/></td>
            </tr>
        </table>
    </div>
    <%if (dtTotalElementCount.Rows.Count > 0)
        { %>
    <div style="font-size: 12px; margin-top: 20px;margin-left:10px;margin-right:10px; width:100%" id="divData" >
            <div style="text-align: center; font-weight: bold;display:none" id="divhead">Element Status ProjectWise Report</div>
        <table border="1" >
            <tr style="height:27px;background-color:white">
                <td>Project Code</td>
                <td>Total No. of Elements</td>
                <td>No. of elements Completed</td>
                <td>No. of elements in Partial</td>
                <td>Overall % of Completion</td>
                <td colspan="<%Response.Write(dtPartialDesiplineCount.Columns.Count-1); %>">No. of Elements in Partial/Disciplinewise</td>
                <td colspan="<%Response.Write(dtPartialDesiplineCount.Columns.Count-1); %>">No. of Elements in Completed/Disciplinewise</td>
                <td colspan="<%Response.Write(dtPartialDesiplineCount.Columns.Count-1); %>">% of Elements in Completed/Disciplinewise</td>
            </tr>
            <tr>
                <td colspan="5"></td>

                <% for (int i = 1; i < dtPartialDesiplineCount.Columns.Count; i++)
                    { %>
                <td><%Response.Write((dtPartialDesiplineCount.Columns[i].ColumnName).ToString()); %></td>

                <%} %>

                <% for (int i = 1; i < dtCompltedDesiplineCount.Columns.Count; i++)
                    { %>
                <td><%Response.Write((dtCompltedDesiplineCount.Columns[i].ColumnName).ToString()); %></td>

                <%} %>

                <% for (int i = 1; i < dtTotalDesiplineCount.Columns.Count; i++)
                    { %>
                <td><%Response.Write((dtTotalDesiplineCount.Columns[i].ColumnName).ToString()); %></td>

                <%} %>
            </tr>

            <%
    for (int j = 0; j < dtTotalElementCount.Rows.Count-1; j++)
    { %>
            <tr>
                <td>
                    <%Response.Write(dtTotalElementCount.Rows[j]["Project"].ToString()); %>
                </td>
                <td class="tdCount">
                    <%Response.Write(dtTotalElementCount.Rows[j]["ElementCount"].ToString()); %>
                </td>
                <td class="tdCount">
                    <%Response.Write(dtCompletedElementCount.Rows[j]["ElementCompletedCount"].ToString()); %>
                </td>
                <td class="tdCount">
                    <%Response.Write(dtPartialElementCount.Rows[j]["ElementPartialCount"].ToString()); %>
                </td>
                <td class="tdCount">
                   <% if (dtTotalElementCount.Rows[j]["ElementCount"].ToString() == "")
                                    {
                                       dtTotalElementCount.Rows[j]["ElementCount"] = 0;
                                    }
                                    if (dtCompletedElementCount.Rows[j]["ElementCompletedCount"].ToString() == "")
                                    {
                                        dtCompletedElementCount.Rows[j]["ElementCompletedCount"]= 0;
                                    }
                                    if (Convert.ToInt32(dtTotalElementCount.Rows[j]["ElementCount"]) != 0)
                                    {%>
                              <%Response.Write(Math.Round(Convert.ToDecimal(dtCompletedElementCount.Rows[j]["ElementCompletedCount"]) / Convert.ToDecimal (dtTotalElementCount.Rows[j]["ElementCount"]) * 100, 2)); %>

                                  <% }%>
                </td>
              
                    <%for (int k = 1; k < dtPartialDesiplineCount.Columns.Count; k++) { %>
                  <td class="tdCount">
                     <%Response.Write(dtPartialDesiplineCount.Rows[j][k].ToString()!=""?dtPartialDesiplineCount.Rows[j][k].ToString():"0"); %>
                             </td>
                    <%} %>
                    <%for (int k = 1; k < dtCompltedDesiplineCount.Columns.Count; k++) { %>
                  <td class="tdCount">
                     <%Response.Write(dtCompltedDesiplineCount.Rows[j][k].ToString()!=""?dtCompltedDesiplineCount.Rows[j][k].ToString():"0"); %>
                             </td>
                    <%} %>
                 <%for (int k = 1; k < dtTotalDesiplineCount.Columns.Count; k++) { %>
                  <td class="tdCount">
                       <% if (dtCompltedDesiplineCount.Rows[j][k].ToString() == "")
                           {
                               dtCompltedDesiplineCount.Rows[j][k] = 0;
                           }
                           if (dtTotalDesiplineCount.Rows[j][k].ToString() == "")
                           {
                               dtTotalDesiplineCount.Rows[j][k]= 0;
                           }
                           if (Convert.ToInt32(dtTotalDesiplineCount.Rows[j][k]) != 0)
                           {%>
                      <%Response.Write(Math.Round(Convert.ToDecimal(dtCompltedDesiplineCount.Rows[j][k]) / Convert.ToDecimal (dtTotalDesiplineCount.Rows[j][k]) * 100, 2)); %>
                      <%} %>
                             </td>
                    <%} %> 
            </tr>
            <%} %>
        </table>
    </div>
    <%} %>
</asp:Content>
