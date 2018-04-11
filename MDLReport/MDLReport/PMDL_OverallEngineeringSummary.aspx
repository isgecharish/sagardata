<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="PMDL_OverallEngineeringSummary.aspx.cs" Inherits="MDLReport.PMDL_OverallEngineeringSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #ccc7c7;
        }
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; font-size: 16px; font-weight: bold">Overall Engineering Summary</div>
    <div style="margin-top: 20px; font-size: 14px">
        <table>
            <tr>
                <td></td>
                <td>&nbsp From</td>
                <td>&nbsp To</td>
            </tr>
            <tr>
                <td>Project</td>
                <td>&nbsp
                    <asp:DropDownList runat="server" ID="ddlProjectFrom" Height="20" Width="172"></asp:DropDownList></td>
                <td>&nbsp
                    <asp:DropDownList runat="server" ID="ddlProjectTo" Height="20" Width="172"></asp:DropDownList></td>

                <td>&nbsp&nbsp Report Date</td>
                <td>&nbsp
                    <asp:TextBox runat="server" ID="txtDate" CssClass="txtDate" Height="21"></asp:TextBox></td>
                <td>&nbsp
                    <asp:Button runat="server" ID="btnShow" Text="Display" OnClick="btnShow_Click" Height="23" Width="100" /></td>
                <td>&nbsp&nbsp
                    <asp:Button runat="server" ID="btnExport" Text="Export to excel" OnClick="btnExport_Click" Height="23" Width="105"  Visible="false"/></td>
            </tr>
        </table>
    </div>
    <div style="font-size: 10px; margin-top: 30px" id="divData">
        <asp:GridView runat="server" ID="gvEngineerSummary" Width="100%" CssClass="GridPager" AutoGenerateColumns="false" HeaderStyle-BackColor="White"
            EnableEventValidation="false" EnableViewState="false">
            <Columns>
                <asp:TemplateField HeaderText="Project">
                    <ItemTemplate>
                        <%#Eval("ProjectCode") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Target Completion Date">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("TargetDate")).ToString("dd-MM-yyyy") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Planned Document" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Math.Round(Convert.ToDecimal(Eval("NoofDocs")),0) %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Planned Document as on Report Date" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Math.Round(Convert.ToDecimal(Eval("PlannedDocs")),0) %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Planned Progress % as on Report Date" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Math.Round(Convert.ToDecimal(Eval("PlannedPerc")),2) %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Released Document as on Report Date" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Math.Round(Convert.ToDecimal(Eval("ReleasedDocs")),0) %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Project Actual Progress % as on Report Date" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Math.Round(Convert.ToDecimal(Eval("ReleasedPerc")),2) %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Project completion date">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("ProjectCompletionDate")).ToString("dd-MM-yyyy") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Key reason for delay, if any">
                    <ItemTemplate>
                      
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
