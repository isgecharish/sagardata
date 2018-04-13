<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="NotesDairy.Notes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="font-size:22px; font-weight: bold;">
        <table><tr><td>   Notes</td><td> &nbsp &nbsp &nbsp &nbsp<asp:Button runat="server" ID="btnNewNotes" OnClick="btnNewNotes_Click" Text="New Notes" /></td></tr></table>
     
   </div>
    <div style="margin-top:10px">
        <div style="background-color: #a4c0f4; height: 420px; width: 700px; border: 2px solid">
            <table>
                <tr>
                    <td>Title &nbsp &nbsp &nbsp &nbsp &nbsp</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" Width="250"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Refrence</td>
                    <td><span runat="server" id="spIndex"></span></td>
                </tr>
                <tr>
                    <td>Desription</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="300" Width="500" /></td>
                </tr>
                <tr>
                    <td>Remember on:</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnSaveNotes" OnClick="btnSaveNotes_Click" Text="Save" /></td>
                    <td><asp:Button runat="server" ID="btnDeleteNotes" OnClick="btnDeleteNotes_Click" Text="Delete" Visible="false" /></td>
                </tr>
            </table>
        </div>

        <div style="background-color: #a4c0f4; width: 800px; min-height: 100px; margin-top: 30px; font-weight: bold;overflow:hidden">
            <div style="float: left; width: 50px">Notes</div>
            <div style="float: right; width: 750px;">
                <asp:Repeater runat="server" ID="rptNotes">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton Text='<%#Eval("Title") %>' runat="server" ID="lnkUpdate" CommandArgument='<%#Eval("UserId") +"&"+ Eval("NotesId") %>' OnClick="lnkUpdate_Click"></asp:LinkButton></td>

                                <td><%# Convert.ToDateTime(Eval("Created_Date")).ToString("dd-MM-yyyy HH:mm ")%></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <asp:HiddenField runat="server" ID="hdfNoteId" />
    </div>


</asp:Content>
