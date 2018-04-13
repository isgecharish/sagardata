<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="PlannedProgressChart.aspx.cs" Inherits="MDLReport.PlannedProgressChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .left {
            float: left;
            width: 500px;
            text-align: right;
            margin: 2px 10px;
            display: inline;
        }

        .right {
            float: left;
            text-align: left;
            margin: 2px 10px;
            display: inline;
            width:600px;
        }
    </style>

    <script src="scripts/jquery.js"></script>
    <script>
        function BindPlannedProgress() {
            $("#divChart").empty();

            $.ajax({
                type: "POST",
                url: "PlannedProgressChart.aspx/GetPlannedProgress",
                contentType: "application/json;charset=utf-8",
                data: "{'Project':'" + $("#ContentPlaceHolder1_ddlProject").val() + "','Year': '" + $("#ContentPlaceHolder1_ddlYear").val() + "','MonthFrom':'" + $("#ContentPlaceHolder1_ddlMonthFrom").val() + "','Monthto':'" + $("#ContentPlaceHolder1_ddlMonthto").val() + "','env':'" + $("#ContentPlaceHolder1_hdfEnv").val() + "' }",
                dataType: "json",
                success: function (data) {

                    console.log(data.d);
                    var Month = [];
                    var plannedData = [];
                    var Actual = [];
                    var Revised = [];

                    for (var i = 0; i < data.d.length; i++) {
                        plannedData.push([parseInt(data.d[i]["slNo"]), parseInt(data.d[i]["BaselinePlannedPerc"])]);
                        Revised.push([parseInt(data.d[i]["slNo"]), parseInt(data.d[i]["RevisedPlannedPerc"])]);
                        Actual.push([parseInt(data.d[i]["slNo"]), parseInt(data.d[i]["ActualPerc"])]);

                        //plannedData.push([parseInt(data.d[i]["slNo"]),data.d[i]["BaselinePlannedPer"]]);
                        //Revised.push([parseInt(data.d[i]["slNo"]), data.d[i]["RevisedPlannedPer"]]);
                        //Actual.push([parseInt(data.d[i]["slNo"]), data.d[i]["ActualPer"]]);

                        Month.push([parseInt(data.d[i]["slNo"]), data.d[i]["Months"]]);
                    }
                    var plot3 = $.jqplot('divChart', [plannedData, Revised, Actual],
                           {
                               title: 'Overall project progress',
                               // Set default options on all series, turn on smoothing.
                               seriesDefaults: {
                                   rendererOptions: {
                                       smooth: true,
                                       dataLabelFormatString: "%d %d%%",
                                   }
                               },
                               legend: {
                                   show: true,
                                   location: 'e',
                                   placement: 'outside',
                                   height:'100'

                               },
                               axes: {
                                   xaxis: {

                                       // mode: "time",
                                       //  timeformat: "%b",
                                       //  min: 1,
                                       //  max:12,
                                       //  mode: "time",
                                       //  timeformat: "%b %Y",
                                       tickSize: 1,
                                       tickFormatter: function (v, a) { return Month[v] },
                                       ticks: Month,
                                   },
                                   yaxis: {
                                     //  min: -2,
                                       //  autoscale:true, 
                                       //  borderColor: "#222",
                                     //  tickOptions: { formatString: '%d', formatter: $.jqplot.euroFormatter }
                                   }
                               },
                               series: [
                                      {
                                          // Change our line width and use a diamond shaped marker.
                                          lineWidth: 1,
                                          markerOptions: { style: 'circle' },
                                          color: "#ff3c1a",
                                          label: "Baseline Planned progress %",
                                      },
                                      {
                                          // Change our line width and use a diamond shaped marker.
                                          lineWidth: 1,
                                          markerOptions: { style: 'circle' },
                                          color: "#33cc33",
                                          label: "Revised Planned progress %",
                                      },
                                      {
                                          // Change our line width and use a diamond shaped marker.
                                          lineWidth: 1,
                                          markerOptions: { style: 'circle' },
                                          color: "#1a75ff",
                                          label: "Actual Planned progress %",
                                      },
                               ],

                           });
                },
                error: function (result) {
                }
            });
        }   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div style="text-align: center; font-weight: bold; font-size: 12px">Overall engineering project progress</div>
            <div style="margin-top: 20px">
                <table>
                    <tr>
                        <td>Project:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlProject" Width="100"></asp:DropDownList></td>
                        <td>&nbsp &nbsp Year:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlYear" Width="100"></asp:DropDownList></td>
                        <td>&nbsp &nbsp From Month:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlMonthFrom" Width="100"></asp:DropDownList></td>
                        <td>&nbsp &nbsp To Month:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlMonthto" Width="100"></asp:DropDownList></td>
                        <td>&nbsp &nbsp
                        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Display" OnClientClick="BindPlannedProgress()" /></td>
                        <td>
                            <%--<button onclick="BindPlannedProgress()" type="button">Chart</button></td>--%>
                            <%-- <td>
                    <input type="button" id="btnExport" value="Export to excel" onclick="Export()" /></td>--%>
                    </tr>
                    <asp:HiddenField runat="server" ID="hdfEnv" />
                </table>
            </div>

            <div style="font-size: 12px; margin-top: 20px;" class="left">
                <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" HeaderStyle-BackColor="White">
                    <Columns>
                        <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("Months") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Baseline Planned Percentage">
                            <ItemTemplate>
                                <%#Eval("BaselinePlannedPerc") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Revised Planned Percentage">
                            <ItemTemplate>
                                <%#Eval("RevisedPlannedPerc") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actual Percentage">
                            <ItemTemplate>
                                <%#Eval("ActualPerc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divChart" style="font-size: 12px;" class="right"></div>

    <script src="scripts/jquery.js"></script>

    <script src="jqplot/jquery.jqplot.js"></script>
    <script type="text/javascript" src="jqplot/jquery.jqplot.min.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.pieRenderer.min.js"></script>
    <link rel="stylesheet" type="text/css" href="jqplot/jquery.jqplot.min.css" />

    <script type="text/javascript" src="jqplot/plugins/jqplot.highlighter.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.cursor.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.dateAxisRenderer.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.pieRenderer.min.js"></script>

    <script type="text/javascript" src="jqplot/plugins/jqplot.barRenderer.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.pieRenderer.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.categoryAxisRenderer.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.pointLabels.js"></script>

    <script type="text/javascript" src="jqplot/plugins/jqplot.dateAxisRenderer.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.logAxisRenderer.js"></script>
    <script type="text/javascript" src="jqplot/plugins/jqplot.canvasTextRenderer.js"></script>

</asp:Content>
