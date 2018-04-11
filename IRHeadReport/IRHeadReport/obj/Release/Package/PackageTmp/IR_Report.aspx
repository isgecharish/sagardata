<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="IR_Report.aspx.cs" Inherits="IRHeadReport.IR_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .leftdiv {
            float: left;
            margin: 2px 10px;
            display: inline;
            font-size: 12px;
        }

        .rightdiv {
            float: left;
            margin: 2px 10px;
            display: inline;
            font-size: 12px;
        }
    </style>
    <script>
        var currentDate = new Date();

        function PrintDoc() {
            var toPrint = document.getElementById('divMain');
            var popupWin = window.open(''); // window.open('', '_blank', 'width=950,height=800,location=no,left=200px');
            popupWin.document.open();
            // popupWin.document.write('<div>' + currentDate + '</div>')
            popupWin.document.write('<html><style>table {border:1px solid;} table th{font-size:14px}table td{font-size:12px}#tbl1{border:none}.tblT{border:none}</style></head><body onload="window.print()">') // th:nth-child(16){display:none;}td:nth-child(16){display:none;}
            popupWin.document.write(toPrint.innerHTML);
            popupWin.document.write('</html>');
            popupWin.document.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftdiv" id="divMain">
        <div style="text-align: center; font-weight: bold">IR And Charge Heads</div>
        <div style="margin-left: 30px">
            <div style="text-align: center; font-size: 12px; font-weight: bold; margin-bottom: 10px; margin-top: 20px">Annexure For charge heads against B/L No. <span runat="server" id="spBLNO"></span></div>
            <div style="border: 1px solid; font-size: 12px; width: 850px">
                <table border="0" id="tbl1">
                    <tr>
                        <td>IR No </td>
                        <td>:</td>
                        <td><span runat="server" id="spIrNo"></span></td>
                        <td>IRN Date </td>
                        <td>:</td>
                        <td><span runat="server" id="spIRNDate"></span></td>
                        <td>&nbsp &nbsp &nbsp IR Amount </td>
                        <td>:</td>
                        <td><span runat="server" id="spIRAmount"></span></td>
                    </tr>
                    <tr>
                        <td>Bussiness Partner</td>
                        <td>:</td>
                        <td><span runat="server" id="spBussinessPartner"></span></td>
                        <td colspan="3"><span runat="server" id="spName"></span></td>
                    </tr>
                    <tr>
                        <td>Purchase Receipt No </td>
                        <td>:</td>
                        <td><span runat="server" id="spRecNo"></span></td>
                        <td colspan="3"></td>
                        <td>&nbsp &nbsp &nbsp Purchase Amount </td>
                        <td>:</td>
                        <td><span runat="server" id="spPurAmount"></span></td>
                    </tr>
                    <tr>
                        <td>Supplier Inv. No</td>
                        <td>:</td>
                        <td><span runat="server" id="spInvoiceNo"></span></td>
                        <td colspan="3"></td>
                        <td>&nbsp &nbsp &nbsp Purchase Order Number</td>
                        <td>:</td>
                        <td><span runat="server" id="spOrderNo"></span></td>
                    </tr>
                    <tr>
                        <td>Supplier Inv. Date</td>
                        <td>:</td>
                        <td><span runat="server" id="spSupplierDate"></span></td>
                        <td colspan="6"></td>
                    </tr>
                    <tr>
                        <td>Project Code</td>
                        <td>:</td>
                        <td><span runat="server" id="spProjectCode"></span></td>
                        <td colspan="3"></td>
                        <td>&nbsp &nbsp &nbsp Project. Description</td>
                        <td>:</td>
                        <td><span runat="server" id="spDescription"></span></td>
                    </tr>
                    <tr>
                        <td>Cargo Type</td>
                        <td>:</td>
                        <td><span runat="server" id="spCargoType"></span></td>
                        <td colspan="6"></td>
                    </tr>
                    <tr>
                        <td>Shipping Line Name</td>
                        <td>:</td>
                        <td><span runat="server" id="spShippingName"></span></td>
                        <td colspan="3"></td>
                        <td>&nbsp &nbsp &nbsp MBL No</td>
                        <td>:</td>
                        <td><span runat="server" id="spMBLNo"></span></td>
                    </tr>
                </table>
            </div>
            <div style="font-size: 12px; margin-top: 40px">
                <asp:GridView runat="server" ID="gvIrDetails" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Charge Head">
                            <ItemTemplate>
                                <%#Eval("ChargeHead") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Charge Head Description">
                            <ItemTemplate>
                                <%#Eval("ChargeheadDiscription") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Basic Amount" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("Amount") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <table style="float:right" class="tblT">
                    <tr>
                        <td></td>
                        <td><b>Total:</b></td>
                        <td><span runat="server" id="spTotalBAsicAmount"></span></td>
                    </tr>
                </table>
            </div>
            <div style="font-size: 12px; margin-top: 40px">
                <div style="text-align: center; font-weight: bold; margin-bottom: 10px;">Annexure For charge heads against B/L No. <span runat="server" id="spBLNo1"></span></div>
                <asp:GridView runat="server" ID="gvIRDetailsRefNo" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Charge Head">
                            <ItemTemplate>
                                <%#Eval("ChargeHead") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Charge Head Description">
                            <ItemTemplate>
                                <%#Eval("ChargeheadDiscription") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Basic Amount" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("Amount") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reference IR No" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("ReferemceIRNo") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                     <table style="float:right;margin-right:163px" class="tblT">
                    <tr>
                        <td></td>
                        <td><b>Total:</b></td>
                        <td><span runat="server" id="spTotalBAsicAmountDetails"></span></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div class="rightdiv">
        <input type="button" value="Print" onclick="PrintDoc()" /></div>
</asp:Content>
