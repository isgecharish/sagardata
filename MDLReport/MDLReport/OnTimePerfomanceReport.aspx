<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="OnTimePerfomanceReport.aspx.cs" Inherits="MDLReport.OnTimePerfomanceReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:GridView runat="server" ID="gvBaseLineReport"></asp:GridView>
    </div>
    <div>
        <asp:GridView runat="server" ID="gvRevisedReport"></asp:GridView>
    </div>
</asp:Content>
