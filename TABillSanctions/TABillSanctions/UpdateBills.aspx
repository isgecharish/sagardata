<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="UpdateBills.aspx.cs" Inherits="TABillSanctions.UpdateBills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
            <asp:GridView AutoGenerateColumns="false" runat="server" ID="gvBill" Width="100%" CssClass="table table-bordered table-hover">
                <Columns>
                    <asp:BoundField DataField="RequestNumber" HeaderText="Request No" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                    <asp:BoundField DataField="ProjectCode" HeaderText="Project Name" />                 
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("RequestNumber").ToString() %>' ID="lnkUpdate" Text="Update" OnClick="lnkUpdate_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkDelete" CommandArgument='<%#Eval("RequestNumber").ToString() +"$"+Eval("TotalAmount").ToString()+"$"+Eval("ProjectCode").ToString()+"$"+ Eval("FairAmount").ToString()+"$"+Eval("HotelCharges").ToString()+"$"+Eval("LocalConveyance").ToString() %>' Text="Delete" OnClick="lnkDelete_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
</asp:Content>
