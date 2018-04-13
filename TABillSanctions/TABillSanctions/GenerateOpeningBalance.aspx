<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="GenerateOpeningBalance.aspx.cs" Inherits="TABillSanctions.GenerateOpeningBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12">
        <div class="row">
            <div class="form-group">
                <label class="col-sm-2">From Project</label>
                <div class="col-sm-4">
                    <asp:DropDownList runat="server" ID="ddlProjectFrom" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label class="col-sm-2">To Project </label>
                <div class="col-sm-4">
                    <asp:DropDownList runat="server" ID="ddlProjectTo" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-10">
        <asp:Button runat="server" ID="btnGenerate" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn btn-primary" />
    </div>

</asp:Content>
