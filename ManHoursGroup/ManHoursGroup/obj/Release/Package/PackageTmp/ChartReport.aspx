<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ChartReport.aspx.cs" Inherits="ManHoursGroup.ChartReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function BindHours() {

            $.ajax({
                type: "POST",
                url: "ChartReport.aspx/BindHours",
                contentType: "application/json;charset=utf-8",
                data: "{'Year': '" + 2017 + "' }",
                dataType: "json",
                success: function (data) {
                    $("#divChart").html("");
                    //var obj = JSON.parse(data.d);

                    var result = [];

                    var planned = [];
                    var actual = [];
                    var availablehours = [];

                    for (var i = 0; i < data.d.length; i++) {
                        planned.push([data.d[i]["Month"], parseInt(data.d[i]["PlannedHours"])]);
                        actual.push([data.d[i]["Month"], parseInt(data.d[i]["ActualHours"])]);
                        availablehours.push([data.d[i]["Month"], parseInt(data.d[i]["AvailableHours"])]);
                    }
                    //Complaints on device
                    var plot1 = $.jqplot('divChart', [planned], {
                        series: [{ renderer: $.jqplot.BarRenderer },
                        ],

                        //    seriesColors: ['#85802b', '#00749F', '#73C774', '#C7754C', '#17BDB8'],

                        axesDefaults: {
                            tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                            tickOptions: {
                                angle: -30,
                                fontSize: '10pt',
                            }
                        },
                        seriesDefaults: {
                            renderer: $.jqplot.BarRenderer,
                            pointLabels: { show: true },

                            rendererOptions: {
                                // varyBarColor: true,    // Set the varyBarColor option to true to use different colors for each bar.
                                barWidth: 150

                            }
                        },
                        axes: {
                            xaxis: {
                                renderer: $.jqplot.CategoryAxisRenderer,
                                label: "Months",
                                axisLabelUseCanvas: true,
                            }
                                ,
                            yaxis: {
                                tickOptions: {
                                    formatString: '%d'
                                }
                            }
                        },

                        canvasOverlay: {
                            show: true,
                            objects: [
                        {
                            horizontalLine: {
                                name: 'barney',
                                yaxis: [{ actual }],
                                //x: 5000,
                                lineWidth: 4,
                                color: 'rgb(100, 55, 124)',
                                shadow: false
                            }
                        },
                            ]
                        }

                        //highlighter: {
                        //    show: true,
                        //    sizeAdjust: 7.5
                        //},
                        //cursor: {
                        //    show: false,
                        //    tooltipLocation: 'sw'
                        //}
                    });


                },
                error: function (result) {
                }
            });
        }

        function BindHourschart() {

            $.ajax({
                type: "POST",
                url: "ChartReport.aspx/BindHours",
                contentType: "application/json;charset=utf-8",
                data: "{'Year': '" + 2017 + "' }",
                dataType: "json",
                success: function (data) {
                    $("#divChart").html("");
                    //var obj = JSON.parse(data.d);

                    var planned = [];
                    var actual = [];
                    var availablehours = [];

                    for (var i = 0; i < data.d.length; i++) {
                        planned.push([data.d[i]["Month"], parseInt(data.d[i]["PlannedHours"])]);
                        actual.push([data.d[i]["Month"], parseInt(data.d[i]["ActualHours"])]);
                        availablehours.push([data.d[i]["Month"], parseInt(data.d[i]["AvailableHours"])]);
                    }
                    //Complaints on device
                    var plot1 = $.jqplot('divChart', [actual, availablehours], {


                        //    seriesColors: ['#85802b', '#00749F', '#73C774', '#C7754C', '#17BDB8'],

                        axesDefaults: {
                           // labelRenderer: $.jqplot.CanvasAxisLabelRenderer
                        },

                        seriesDefaults: {
                            rendererOptions: {
                                smooth: true
                            }
                        },

                        //series: [
                        //            {
                        //                lineWidth: 1,
                        //                markerOptions: false,
                        //            }
                        //],
                        axes: {
                            xaxis: {
                                renderer: $.jqplot.CategoryAxisRenderer,
                                label: "Months",
                                axisLabelUseCanvas: true,
                            }
                                ,
                            yaxis: {
                                tickOptions: {
                                    formatString: '%d'
                                }
                            }
                        },

                        //canvasOverlay: {
                        //    show: true,
                        //    objects: [
                        //{
                        //    line: {
                        //        name: 'barney',
                        //        y: 6000,
                        //        x: 5000,
                        //        lineWidth: 6,
                        //        color: 'rgb(100, 55, 124)',
                        //        shadow: false
                        //    }
                        //},
                        //    ]
                        //}

                        //highlighter: {
                        //    show: true,
                        //    sizeAdjust: 7.5
                        //},
                        //cursor: {
                        //    show: false,
                        //    tooltipLocation: 'sw'
                        //}
                    });


                },
                error: function (result) {
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <button type="button" id="btnMonthlyHours" value="View Chart" onclick="BindHours()">View Chart</button>
    <button type="button" value="View  Chart" onclick="BindHourschart()">View line Chart </button>
    <div id="divChart" style="height: 500px"></div>


    <%--//script--%>
    <script src="scripts/jquery.js"></script>
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
    <script src="jqplot/plugins/jqplot.canvasOverlay.js"></script>


</asp:Content>

