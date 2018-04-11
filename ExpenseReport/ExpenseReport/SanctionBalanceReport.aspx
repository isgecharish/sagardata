<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="SanctionBalanceReport.aspx.cs" Inherits="ExpenseReport.SanctionBalanceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 128px;
                width: 128px;
            }

        .multiselect {
            height: 28px;
        }
    </style>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <link href="css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="scripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=ContentPlaceHolder1_lstProjectCode]').multiselect({
                includeSelectAllOption: true,
            })
        })
        function onchangeDivision() {
            // alert("hi")
            $('[id*=ContentPlaceHolder1_lstProjectCode]').multiselect({
                includeSelectAllOption: true,
            })
            .multiselect('selectAll', false)
            .multiselect('updateButtonText');
        }

        function PrintDoc() {
            var toPrint = document.getElementById('divData');
            var Division = $('#ContentPlaceHolder1_ddlDevision').val();
            var HeaderTravel = "Projectwise Travel Expense Sanction Balance (for Active Projects)";
            var HeaderSite = "Projectwise Site Expense Sanction Balance (for Active Projects)";
            if ($('input:radio:checked').val() == "rdTravelSanction") {
                var popupWin = window.open(''); // window.open('', '_blank', 'width=950,height=800,location=no,left=200px');
                popupWin.document.open();
                popupWin.document.write('<html><style>table th{font-size:8px;background-color:#f4f4f4}table td{font-size:8px}</style></head><body onload="window.print()">') // th:nth-child(16){display:none;}td:nth-child(16){display:none;}
                popupWin.document.write('<div style="font-size:14px;text-align:center">' + HeaderTravel + '<div>')
                popupWin.document.write('<div style="font-size:12px;text-align:center">Division:<span id=spDevision>' + Division + '</span><div>')
                popupWin.document.write(toPrint.innerHTML);
                popupWin.document.write('</html>');
                popupWin.document.close();
            }
            else if ($('input:radio:checked').val() == "rdSiteSanction") {
                var popupWin = window.open(''); // window.open('', '_blank', 'width=950,height=800,location=no,left=200px');
                popupWin.document.open();
                popupWin.document.write('<html><style>table th{font-size:8px;background-color:#f4f4f4}table td{font-size:8px}</style></head><body onload="window.print()">') // th:nth-child(16){display:none;}td:nth-child(16){display:none;}
                popupWin.document.write('<div style="font-size:14px;text-align:center">' + HeaderSite + '<div>')
                popupWin.document.write('<div style="font-size:12px;text-align:center">Division:<span id=spDevision>' + Division + '</span><div>')
                popupWin.document.write(toPrint.innerHTML);
                popupWin.document.write('</html>');
                popupWin.document.close();
            }
            else {

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPanel">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upPanel">
        <ContentTemplate>
            <div style="margin-top: 2px; margin-left: 20px; background-color: #d7d6d6; margin-right: 30px;">
                <div style="text-align: center; font-weight: bold; margin-top: 10px">Projectwise Travel/Site Expense Sanction Balance (for Active Projects)</div>
                <table style="margin-top: 10px">
                    <tr>
                        <td>
                            <asp:DropDownList runat="server" BackColor="#f4f4f4" ID="ddlDevision" OnSelectedIndexChanged="ddlDevision_SelectedIndexChanged" AutoPostBack="true" Width="150" Height="28"></asp:DropDownList></td>
                        <td>&nbsp &nbsp<asp:ListBox runat="server" BackColor="#f4f4f4" ID="lstProjectCode" Height="200" Width="100" SelectionMode="Multiple"></asp:ListBox></td>
                        <td>&nbsp &nbsp<asp:RadioButton runat="server" ID="rdTravelSanction" Text="Travel Sanction" GroupName="a" Checked="true" />
                            <asp:RadioButton runat="server" ID="rdSiteSanction" Text="Site Sanction" GroupName="a" /></td>
                        <td>&nbsp &nbsp &nbsp &nbsp<asp:Button runat="server" ID="btnSearch" Text="Display Sanction" OnClick="btnSearch_Click" BorderColor="White" BorderStyle="Inset" /></td>
                        <td>&nbsp &nbsp &nbsp
                            <asp:Button runat="server" Visible="false" ID="btnExportToExcel" OnClick="btnExportToExcel_Click" Text="Export to excel" BorderColor="White" BorderStyle="Inset" /></td>
                        <td>&nbsp &nbsp &nbsp 
                            <asp:Button runat="server" Visible="false" ID="btnPrint" Text="Print" BorderColor="White" BorderStyle="Inset" OnClientClick="PrintDoc()" OnClick="btnPrint_Click" /></td>
                    </tr>
                </table>
                <div style="height: 10px"></div>
            </div>

            <div style="margin-top: 30px; margin-left: 20px; font-size: 12px; margin-right: 20px" id="divData">
                <asp:GridView runat="server" ID="gvBalnaceReport" BorderStyle="Solid" BorderWidth="1" BorderColor="Black" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="#efefef" OnRowDataBound="gvBalnaceReport_RowDataBound"
                    AllowPaging="true" OnPageIndexChanging="gvBalnaceReport_PageIndexChanging" PageSize="50" PagerStyle-Font-Size="Small" PagerStyle-HorizontalAlign="Center"
                    EnableEventValidation="false" EnableViewState="false">
                    <Columns>
                        <%-- <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Division">
                            <ItemTemplate>
                                <%#Eval("Division") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Code">
                            <ItemTemplate>
                                <%#Eval("ProjectCode") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Description">
                            <ItemTemplate>
                                <%#Eval("ProjectDescription") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="L3 Level Balance" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("L3Level") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="L4 Level Balance" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# Eval("L4Level") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
