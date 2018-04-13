<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="AllHoursBarChartReport.aspx.cs" Inherits="ManHoursGroup.AllHoursBarChartReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--//script--%>
    <script src="scripts/jquery.js"></script>
    <script src="scripts/jquery.flot.js"></script>
    <script src="scripts/jquery.flot.axislabels.js"></script>
    <script src="scripts/jquery.flot.time.js"></script>
    <script src="scripts/jquery.flot.categories.js"></script>
    <script>
        function BindHours() {
            $.ajax({
                type: "POST",
                url: "AllHoursBarChartReport.aspx/BindHours",
                contentType: "application/json;charset=utf-8",
                data: "{'Year': '" + 2017 + "' }",
                dataType: "json",
                success: function (data) {
                    $("#divChart").html("");
                    //var obj = JSON.parse(data.d);

                    var Planned = [];
                    var Actual = [];
                    var Available = [];
                    var Month = [];

                    for (var i = 0; i < data.d.length; i++) {
                        Planned.push([parseInt(data.d[i]["Month"]), parseInt(data.d[i]["PlannedHours"])]);
                        Actual.push([parseInt(data.d[i]["Month"]), parseInt(data.d[i]["ActualHours"])]);
                        Available.push([parseInt(data.d[i]["Month"]), parseInt(data.d[i]["AvailableHours"])]);

                        //Planned.push([data.d[i]["MonthName"], parseInt(data.d[i]["PlannedHours"])]);
                        //Actual.push([data.d[i]["MonthName"], parseInt(data.d[i]["ActualHours"])]);
                        //Available.push([data.d[i]["MonthName"], parseInt(data.d[i]["AvailableHours"])]);

                        Month.push([parseInt(data.d[i]["Month"]), data.d[i]["MonthName"]]);
                    }

                    //------------------------------------------------
                    console.log(Available);

                    var data = [
                                  {
                                      label: "Planned",
                                      data: Planned,
                                      bars: {
                                          show: true,
                                          align: "center",
                                          //    barWidth:5,
                                          fill: true,
                                          lineWidth: 1
                                      },
                                      color: "#a4d246"
                                  },
                                  {
                                      label: "Actual",
                                      data: Actual,
                                      lines: {
                                          show: true,
                                          fill: false
                                      },
                                      points: {
                                          show: false,
                                          fillColor: '#AA4643'
                                      },
                                      color: '#AA4643',
                                      yaxis: 1
                                  },
                                  {
                                      label: "Available",
                                      data: Available,
                                      lines: {
                                          show: true,
                                          fill: false
                                      },
                                      points: {
                                          show: false,
                                          fillColor: '#4572A7'
                                      },
                                      color: '#4572A7',
                                      yaxis: 1
                                  }
                    ];

                    //-------------------------------------
                    $.plot($("#container"), data, {
                      
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
                            //  tickLength: 0, // Hide gridlines
                            //  axisLabel: "Months",
                            axisLabelUseCanvas: true,
                            axisLabelFontSizePixels: 12,
                            axisLabelFontFamily: "Verdana, Arial, Helvetica, Tahoma, sans-serif",
                            axisLabelPadding: 15,
                        },

                        yaxis: {
                            tickFormatter: function (val, axis) {
                                return val;
                            },
                            //    max: 70000,
                            tickSize: 5000,
                            axisLabel: "Hours",
                            axisLabelUseCanvas: true,
                            axisLabelFontSizePixels: 12,
                            axisLabelFontFamily: "Verdana, Arial, Helvetica, Tahoma, sans-serif",
                            axisLabelPadding: 60
                        },
                        grid: {
                            hoverable: true,
                            //borderWidth: 1
                        },
                        legend: {
                            labelBoxBorderColor: "none",
                            position: "right"
                        }
                    });
                    //-------
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <button type="button" id="btnMonthlyHours" value="View Chart" onclick="BindHours()">View Chart</button>
    <div id="container" style="width: 900px; height: 500px; margin-top: 30px"></div>
    <div id="legendholder" style="float:right">Legend Container</div>

</asp:Content>
