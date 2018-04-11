<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="VendorDocument.aspx.cs" Inherits="VendorDocument.VendorDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            background-color:#dcd5d5;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center;font-size:16px;font-weight:bold">Vendor Document Post Order Lines</div>
    <div style="font-size:11px;margin-top:20px">
    <asp:GridView runat="server" ID="gvVandorDoc" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="White">
        <Columns>
            <asp:TemplateField HeaderText="Serial No">
                <ItemTemplate>
                    <%#Eval("SerialNo") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Item No">
                <ItemTemplate>
                    <%#Eval("ItemNo") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Upload No">
                <ItemTemplate>
                    <%#Eval("UploadNo") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="DocSerial No">
                <ItemTemplate>
                    <%#Eval("DocSerialNo") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Document ID">
                <ItemTemplate>
                    <%#Eval("DocumentID") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Document Rev">
                <ItemTemplate>
                    <%#Eval("DocumentRev") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Document Description">
                <ItemTemplate>
                    <%#Eval("DocumentDescription") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Receipt No">
                <ItemTemplate>
                    <%#Eval("ReceiptNo") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Revision No">
                <ItemTemplate>
                    <%#Eval("RevisionNo") %>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="File Name">
                <ItemTemplate>
                    <asp:LinkButton Text='<%#Eval("FileName")%>' runat="server" ID="lnkOpenFile" CommandArgument='<%#Eval("DiskFileName")+"&"+Eval("FileName")  %>' OnClick="lnkOpenFile_Click"></asp:LinkButton> 
                    <!-- OnClientClick='window.open("newPage.aspx?fileName=<%# Eval("DiskFileName") %>", "_newtab");' -->
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        </div>
</asp:Content>
