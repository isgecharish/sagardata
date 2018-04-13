<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="EncryptAttachmentTest.aspx.cs" Inherits="VendorDocument.EncryptAttachmentTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:FileUpload CssClass="button"  ID="FileUpload" runat="server" AllowMultiple="true"/>  
    <hr />  
    <asp:Button ID="btnEncrypt" CssClass="button" Text="Upload File" runat="server" OnClick="btnEncrypt_Click"  />  
      <asp:Button ID="btnDecrypt" CssClass="button" Text="Download" runat="server"  OnClick="btnDecrypt_Click" />  
</asp:Content>
