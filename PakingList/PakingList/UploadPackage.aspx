<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="UploadPackage.aspx.cs" Inherits="PakingList.UploadPackage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 20px;margin-left:30px">
        <div>
            <asp:FileUpload runat="server" ID="fileUpload"  BackColor="White"  />
        </div>
        <div style="margin-top:10px">
            <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Upload" BorderStyle="Inset" BorderColor="White" />
        </div>
    </div>
      <div>
        <asp:Label runat="server" ID="lblItemCode"></asp:Label>
        <ajaxToolkit:ModalPopupExtender ID="mpeShowIsgecitemCode" runat="server" TargetControlID="lblItemCode" PopupControlID="pnlShowIsgecitemCode" CancelControlID="btncloseCancel"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlShowIsgecitemCode" runat="server" CssClass="mfp-dialog-login" Width="60%" Height="500" BorderStyle="Solid" BorderWidth="1" BorderColor="Black" Style="display: none; background: #fff; overflow:scroll;margin-left:50px">
            <div class="box-w" style="overflow: scroll; background-color: #fff;">
                <asp:Button ID="btncloseCancel" runat="server" CssClass="mfp-close" Text="x" Style="width: 24px; float: right; margin-bottom: 5px; font-size: 24px;" />
               <strong> Receipt No: </strong><span runat="server" id="stRcno" style="font-size:12px;margin-left: 10px; margin-top: 10px"></span>
                <div style="margin-left: 10px; margin-top: 10px"  runat="server" id="divMatchItem">
                    <strong>Total Weight of following ISGEC Item Code is not correct in your excel file</strong><br />
                    <br />
                    <span runat="server" id="spItemCode" style="font-size: 12px;"></span>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
