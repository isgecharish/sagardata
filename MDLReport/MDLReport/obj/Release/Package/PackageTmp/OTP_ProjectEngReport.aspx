<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="OTP_ProjectEngReport.aspx.cs" Inherits="MDLReport.OTP_ProjectEngReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .left {
            float: left;
            width: 500px;
            text-align: right;
            margin: 2px 10px;
            display: inline;
            font-size: 12px;
        }

        .right {
            float: left;
            text-align: left;
            margin: 2px 10px;
            display: inline;
            width: 600px;
            font-size: 12px;
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
            $("#ContentPlaceHolder1_txtDateTo").datepicker("setDate", currentDate)
        });
        function Export() {
            window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=ContentPlaceHolder1_divOtpSummary]').html()));
            e.preventDefault();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; font-weight: bold; font-size: 12px">Project Engineering OTP Summary & Detail</div>
    <div>
    </div>
    <div style="margin-top: 20px">
        <table>
            <tr>
                <td>Project From:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProjectFrom" Width="100"></asp:DropDownList></td>
                <td>&nbsp &nbsp Project To:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProjectTo" Width="100"></asp:DropDownList></td></tr>
            <tr style="height:20px"></tr>
            <tr>
                <td>&nbsp &nbsp Date From :</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDateFrom" Width="100" CssClass="txtDate" Text="01/01/2000"></asp:TextBox></td>
                <td>&nbsp &nbsp Date To:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDateTo" Width="100" CssClass="txtDate"></asp:TextBox></td>
                <td>&nbsp &nbsp
                     <asp:Button runat="server" ID="btnOTP" OnClick="btnOTP_Click" Text="OTP Report" />
                    <%--<asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Display" OnClientClick="BindPlannedProgress()" />--%></td>
                <td>&nbsp &nbsp<asp:Button runat="server" ID="btnOTPSummary" OnClick="btnOTPSummary_Click" Text="OTP Summary" /></td>
                <td>&nbsp &nbsp
                    <asp:Button runat="server" ID="btnExportOTP" Text="Export to excel" OnClick="btnExportOTP_Click" Visible="false" />
                    <asp:Button runat="server" ID="btnExportOTPSummary" Text="Export to excel"  Visible="false" OnClientClick="Export()"/> <%-- OnClick="btnExportOTPSummary_Click" --%>
                </td>
            </tr>
        </table>
    </div>

    <div style="margin-top: 20px; font-size: 10px; margin-left: 10px" runat="server" id="divOtpreport" visible="false">
        <div style="text-align: center; font-weight: bold; font-size: 12px">Project Engineering – OTP Detail </div>
        <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" AllowPaging="true" PageSize="50" OnPageIndexChanging="gvData_PageIndexChanging" EnableEventValidation="false" EnableViewState="false" HeaderStyle-BackColor="White">
            <Columns>
                <asp:TemplateField HeaderText="Project" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Project") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Element" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Element") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DocumentID" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("DocumentID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Document Description" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("DocumentDescription") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Responsible Discipline" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("ResponsibleDiscipline") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Baseline End Date" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("BaselineScheduleFinishDate")).ToString("dd-MM-yyyy")!="01-01-1970"?Convert.ToDateTime(Eval("BaselineScheduleFinishDate")).ToString("dd-MM-yyyy"):"" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Revised Schedule End Date" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("LatestRevisedScheduleFinishDate")).ToString("dd-MM-yyyy")!="01-01-1970"?Convert.ToDateTime(Eval("LatestRevisedScheduleFinishDate")).ToString("dd-MM-yyyy"):"" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actual Release Date" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("ActualReleaseDate")).ToString("dd-MM-yyyy")!="01-01-1970"?Convert.ToDateTime(Eval("ActualReleaseDate")).ToString("dd-MM-yyyy"):"" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Base Line End Date Status" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("BaseLineStatus") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Revised Line End Date Status" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("RevisedScheduleStatus") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reason for Delay" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("ReasonforScheduleVariance") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Corrective Action" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("CorrectivePreventiveAction") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div runat="server" id="divOtpSummary" visible="false" style="margin-top:20px ;width:100%">
        <div style="text-align: center; font-weight: bold; font-size: 12px">Project Engineering  OTP Summary From <span runat="server" id="spASonDate"></span></div>
        <table style="width:100%;border:1px solid;margin-top:10px;">
            <tr>
                <td>
       <%-- <div class="left" style="margin-top:10px">--%>
            <div style="font-weight:bold;font-size:10px;margin-left:20px;font-size:13px;border-right:1px solid">ON TIME PERFORMANCE wrt Baseline Schedule</div>
            <asp:GridView runat="server" ID="gvBaseLineReport" HeaderStyle-BackColor="White" Width="100%" BorderStyle="Solid"></asp:GridView></td>
     <%--   </div>
        <div class="right" style="margin-top:10px">--%>
                <td>
            <div style="font-weight:bold;font-size:10px;margin-left:20px;font-size:13px">ON TIME PERFORMANCE wrt Revised Schedule</div>
            <asp:GridView runat="server" ID="gvRevisedReport" HeaderStyle-BackColor="White" Width="100%" BorderStyle="Solid"></asp:GridView>
        <%--</div>--%></td>
                </tr>
        </table>
    </div>
</asp:Content>
