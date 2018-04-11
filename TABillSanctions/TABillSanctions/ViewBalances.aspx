<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ViewBalances.aspx.cs" Inherits="TABillSanctions.ViewBalances" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView runat="server" ID="gvOpeningBalance" AutoGenerateColumns="false" Width="90%" CssClass="table table-bordered table-hover">
        <Columns>
            <asp:BoundField DataField="ProjectCode" HeaderText="Project Code" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Element" HeaderText="Element" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Balance Date" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Convert.ToDateTime(Eval("OpeningBalanceDate").ToString()).ToString("dd-MM-yyyy") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="OpeningBalance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="15%" />
            <asp:BoundField DataField="Consumption" HeaderText="Consumption" ItemStyle-HorizontalAlign="Right" />
            <asp:TemplateField HeaderText="Last Consumption Date" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%#Eval("LastConsumptionDate").ToString()!=""?Convert.ToDateTime(Eval("LastConsumptionDate").ToString()).ToString("dd-MM-yyyy"):"" %>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</asp:Content>
