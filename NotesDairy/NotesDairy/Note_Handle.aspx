<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="Note_Handle.aspx.cs" Inherits="NotesDairy.Note_Handle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div style="text-align: initial; font-size: 16px; font-weight: bold" runat="server" id="divHeader">Notes Handle</div>

    <div style="margin-left: 0px; margin-top: 15px">
        <table>
            <tr>
                <td>Notes Handle</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtNotesHandle" MaxLength="50" /></td>
            </tr>

            <tr>
                <td>Database Id</td>
                <td>:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlDBID" Width="170"></asp:DropDownList></td>
            </tr>

            <tr>
                <td>Table Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTableName" /></td>
            </tr>

            <tr>
                <td>Acess Index</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtAcessIndex" MaxLength="50" /></td>
            </tr>

            <tr>
                <td>Remarks</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtRemarks" MaxLength="50" /></td>
            </tr>

            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button Text="Save" runat="server" ID="btnSaveHandle" OnClick="btnSaveHandle_Click" />
                    <asp:Button runat="server" ID="lnkDelete" Text="Delete" OnClick="lnkDelete_Click" Visible="false"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
