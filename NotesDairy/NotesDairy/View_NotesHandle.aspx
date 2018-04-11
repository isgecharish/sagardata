<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="View_NotesHandle.aspx.cs" Inherits="NotesDairy.View_NotesHandle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            background-color: #d7d7d7;
        }
        div{
            margin-left:30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
        <div style="float: left; width: 200px">
            <asp:Button runat="server" ID="btnCreate" Text="New Notes Handle" OnClick="btnCreate_Click" ForeColor="White" BackColor="#0065ca" /></div>
        <div style="text-align: initial; font-size: 16px; font-weight: bold; text-align: center; width: 500px">Notes Handle </div>
    </div>
    <div style="margin-left: 0px; margin-top: 15px">
        <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="White">
            <Columns>
                <asp:TemplateField HeaderText="Notes Handle">
                    <ItemTemplate>
                        <%#Eval("NotesHandle") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Access Index">
                    <ItemTemplate>
                        <%#Eval("AccessIndex") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DataBase ID">
                    <ItemTemplate>
                        <%#Eval("DBID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Table Name">
                    <ItemTemplate>
                        <%#Eval("TableName") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <%#Eval("Remarks") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkUpdate" Text="Edit" CommandArgument='<%#Eval("NotesHandle") %>' OnClick="lnkUpdate_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
