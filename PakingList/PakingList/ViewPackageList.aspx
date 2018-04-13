
<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ViewPackageList.aspx.cs" Inherits="PakingList.ViewPackageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        span {
            font-size: 10px;
            color: black;
        }
    </style>
    <script>
        function PrintDoc() {
            var toPrint = document.getElementById('ContentPlaceHolder1_divProducts');
           // toPrint+=  document.getElementById('trRc');
           // toPrint+= document.getElementById('trPo');
            var popupWin = window.open(''); // window.open('', '_blank', 'width=950,height=800,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><style>table th{font-size:8px}table td{font-size:8px}</style></head><body onload="window.print()">') // th:nth-child(16){display:none;}td:nth-child(16){display:none;}
           
            popupWin.document.write(toPrint.innerHTML);
            popupWin.document.write('</html>');
            popupWin.document.close();
        }

        function myFunction() {
            var txt;
            var r = confirm("Are you sure to Update Quantity and Total Weight!");
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; font-size: 16px; font-weight: bold; margin-top: 20px">Update Packing List in Receipt</div>
    <div style="margin-left: 50px; margin-top: 20px">
        <table style="font-size: 14px">
            <tr id="trRc">
                <td><b>Receipt Number: </b></td>
                <td>&nbsp &nbsp <span runat="server" id="spRecNo" style="font-size: 14px"></span></td>
            </tr>
            <tr id="trPo">
                <td><b>Purchase Order No:</b></td>
                <td>&nbsp &nbsp <span runat="server" id="spPoNo" style="font-size: 14px"></span></td>
            </tr>
            <tr>
                <td><b>PakingList No:</b></td>
                <td>&nbsp &nbsp   <asp:DropDownList runat="server" ID="ddlPKNo" Width="180" Height="25"></asp:DropDownList></td>
                <td> &nbsp &nbsp &nbsp   <asp:Button runat="server" ID="btnShow" OnClick="btnShow_Click" Text="Show" /></td>

                <td><asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" Text="Update Receipt" Visible="false" BorderStyle="inset" BorderColor="White" />
                    <asp:Button runat="server" ID="btnPrint" OnClientClick="PrintDoc()" Text="Print" Visible="false" BorderStyle="inset" BorderColor="White" />
                </td>
            </tr>
        </table>
    </div>


    <div runat="server" id="divProducts" visible="false" style="background-color: ">
       <%-- <div style="text-align: center; font-size: 20px"><strong>Packing List</strong></div>--%>
        <div style="font-size: 12px; margin-left: 50px;margin-top:20px">
            <table border="0">
            <%--    <tr>
                    <td><strong>Receipt No</strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spReceiptNo"></span></td>
                </tr>
                <tr>
                    <td><strong>ISGEC PO No </strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spIsgecPONo"></span></td>
                </tr>--%>
                <tr>
                    <td><strong>Supplier's Packing List/ Invoice No </strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spInvoiceNo"></span></td>
                </tr>
                <tr>
                    <td><strong>Packing List Date </strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spPackingDate"></span></td>
                </tr>
                <tr>
                    <td><strong>Net Weight of Material </strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spNetWeight"></span></td>
                </tr>
                <tr>
                    <td><strong>Transporter Name </strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spTransporterName"></span></td>
                </tr>
                <tr>
                    <td><strong>Vehicle No :</strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spVehicleNo"></span></td>
                </tr>
                <tr>
                    <td><strong>LR No </strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spLRNo"></span></td>
                </tr>
                <tr>
                    <td><strong>LR Date </strong></td>
                    <td>&nbsp&nbsp :</td>
                    <td>&nbsp&nbsp<span runat="server" id="spLRDate"></span></td>
                </tr>
            </table>
        </div>
        <div style="text-align: center; font-size: 18px">Packing List</div>
        <div style="margin-top: 10px; margin-left: 30px; font-size: 12px">
            <asp:GridView runat="server" ID="gvProductDetails" AutoGenerateColumns="false" Width="98%" CssClass="" BorderStyle="Solid" BorderColor="Black" HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Black" RowStyle-Height="5"
                DataKeyNames="ReceiptNumber,ISGEC_ItemCode,Quantity,TotalWeight">
                <Columns>
                    <%--<asp:TemplateField HeaderText="Receipt No">
                        <ItemTemplate>
                            <%#Eval("ReceiptNumber") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="S.N." ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#Eval("SerialNumber") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ISGEC Item Code">
                        <ItemTemplate>
                            <%#Eval("ISGEC_ItemCode") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="Item Description">
                        <ItemTemplate>
                            <%#Eval("ItemDescription") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#Eval("UOM") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("Quantity") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit Weight" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Math.Round(Convert.ToDecimal(Eval("UnitWeight")),2) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Weight" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Math.Round(Convert.ToDecimal(Eval("TotalWeight")),2) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Supplier's/ ISGEC Drawing ID" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#Eval("DrawingId") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Drawing Revision Number" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#Eval("RevisionNumber") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Package Type">
                        <ItemTemplate>
                            <%#Eval("PackageType") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Shipping/Package Marks">
                        <ItemTemplate>
                            <%#Eval("PackageMarks") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("Length") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("Width") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Height" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Eval("Height") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#Eval("UOMDimension") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="Update">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkUpdate" Text="Update" OnClientClick="return myFunction()" CommandArgument='<%#Eval("ReceiptNumber")+","+ Eval("ISGEC_ItemCode") +","+ Eval("Quantity")+","+Eval("TotalWeight") %>' OnClick="lnkUpdate_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div id="divNorecord" runat="server" visible="false" style="text-align: center; font-size: 14px; margin-top: 10px">
        No Record Found
    </div>

    <div>
        <asp:Label runat="server" ID="lblItemCode"></asp:Label>
        <ajaxToolkit:ModalPopupExtender ID="mpeShowIsgecitemCode" runat="server" Drag="true" TargetControlID="lblItemCode" PopupControlID="pnlShowIsgecitemCode" CancelControlID="btncloseCancel"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlShowIsgecitemCode" runat="server" CssClass="mfp-dialog-login" Width="60%" Height="500" BorderStyle="Solid" BorderWidth="1" BorderColor="Black" Style="display: none; background: #fff; overflow: scroll; margin-left: 50px">
            <div class="box-w" style="overflow: scroll; background-color: #fff;">
                <asp:Button ID="btncloseCancel" runat="server" CssClass="mfp-close" Text="x" Style="width: 24px; float: right; margin-bottom: 5px; font-size: 24px;" />
                <strong>Receipt No: </strong><span runat="server" id="stRcno" style="font-size: 12px; margin-left: 10px; margin-top: 10px"></span>
                <div style="margin-left: 10px; margin-top: 10px" runat="server" id="divMatchItem">
                    <strong>Following ISGEC Item Code not Updated because it does not match in given table.</strong><br />
                    <br />
                    <span runat="server" id="spItemCode" style="font-size: 12px;"></span>
                </div>
                <div style="margin-left: 10px; margin-top: 10px" runat="server" id="divMatchQuant">
                    <strong>Packing List quantity is more than po quantity</strong>
                    <br />
                    Item Code &nbsp  &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp  &nbsp &nbsp    &nbsp &nbsp &nbsp 
                    <br />
                    <span runat="server" id="spItemCodeQuant" style="font-size: 12px;"></span>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
