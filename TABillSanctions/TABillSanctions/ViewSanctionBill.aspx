<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ViewSanctionBill.aspx.cs" Inherits="TABillSanctions.ViewSanctionBill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvBill" Width="70%" CssClass="table table-bordered table-hover">
                <Columns>
                    <asp:BoundField DataField="RequestNumber" HeaderText="Request No" />
                    <asp:BoundField DataField="SerialNo" HeaderText="Serial No" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" ItemStyle-HorizontalAlign="Right" />
                
                    <asp:TemplateField HeaderText="Project/Code" ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>
                           <%#Eval("ProjectCode").ToString() %>
                        </ItemTemplate>
                    </asp:TemplateField>   
                       <asp:BoundField DataField="Activity" HeaderText="Activity" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
</asp:Content>
