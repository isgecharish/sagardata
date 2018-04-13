<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="BillForm.aspx.cs" Inherits="TABillSanctions.BillForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validate() {
            var txtRequestNo = document.getElementById("txtRequestNo");
            var txtFairAmount = document.getElementById("txtFairAmount");
            var txtHotelCharges = document.getElementById("txtHotelCharges");
            var txtLocalConv = document.getElementById("txtLocalConv");

            if (txtRequestNo.val() == "") {
                txtRequestNo.focus = true;
                return false;
            }
            else {
                txtRequestNo.focus = false;
                return true;
            }

            if (txtFairAmount.val() == "") {
                txtFairAmount.focus = true;
                return false;
            }
            else {
                txtFairAmount.focus = false;
                return true;
            }
           
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12">
        <div class="row">
            <div class="col-md-10">
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2">Request No:</label>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRequestNo" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2">Amount</label>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="txtFairAmount" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2">Hotel Charges</label>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="txtHotelCharges" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2">Local Conveyance</label>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="txtLocalConv" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2">Project Code</label>
                        <div class="col-sm-4">
                            <asp:DropDownList runat="server" ID="ddlProjectCode" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-10">
        <asp:Button runat="server" ID="btnSave" Text="Submit" OnClick="btnSave_Click" CssClass="btn btn-primary" OnClientClick="return validate()" />
    </div>
</asp:Content>
