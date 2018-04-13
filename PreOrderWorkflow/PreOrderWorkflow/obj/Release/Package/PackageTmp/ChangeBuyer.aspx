<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ChangeBuyer.aspx.cs" Inherits="PreOrderWorkflow.ChangeBuyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-panel {
            background: #ffffff;
            margin: 10px;
            padding: 10px;
            box-shadow: 0px 3px 2px #aab2bd;
            text-align: left;
        }

        .mt {
            margin-top: 10px;
        }

        .row {
            margin-right: -1px;
            margin-left: -1px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="text-align: center; font-size: 16px; font-weight: bold">Update Buyer</div>
        <div class="col-lg-12">
        <div class="form-panel">
            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-2">Project:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" ID="txtProject" CssClass="form-control" Enabled="false" />
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label for="pwd" class="col-sm-2">Element :</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" ID="txtElement" CssClass="form-control" Enabled="false" />
                    </div>
                </div>
            </div>
            <div class="row mt">
                <div class="form-group">
                    <label for="pwd" class="col-sm-2">Specification No/Details:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" ID="txtSpecification" CssClass="form-control" Enabled="false" />
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label for="pwd" class="col-sm-2">Buyer:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" ID="txtBuyer" CssClass="form-control" placeholder="Type name or emp id"  />
                          <ajaxToolkit:AutoCompleteExtender ServiceMethod="GetUSer" MinimumPrefixLength="3"
                                    CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtBuyer"
                                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false" CompletionListElementID="divBuyer">
                                </ajaxToolkit:AutoCompleteExtender>
                                <div id="divBuyer" style="overflow-y: scroll; overflow-x: hidden; max-height: 400px;"></div>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label for="pwd" class="col-sm-2">Status:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" ID="txtStatus" CssClass="form-control" Enabled="false" />
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-2">Notes:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" Height="100" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-2">Created Date:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" ID="txtDAte" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-2">Creater:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" ID="txtCreater" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
              <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-2"></label>
                    <div class="col-sm-6">
                       <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info btn-sm" Text="Update" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
