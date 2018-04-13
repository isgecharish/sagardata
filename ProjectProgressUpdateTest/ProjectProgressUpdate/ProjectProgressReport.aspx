<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectProgressReport.aspx.cs" Inherits="ProjectProgressUpdateTest.ProjectProgressReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title></title>
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <link href="scripts/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row text-center">
            <div class="col-sm-12">
                <div style="text-align: center; font-size: 14px; font-weight: bold"">
        <asp:Label id="lblTestServer" runat="server"   Visible="false" ForeColor="Black" ></asp:Label>
            </div>
                </div>
        </div>
        <div class="row text-center">
            <div class="col-sm-3">
                
            </div>
            <div class="col-sm-6">
                <h5>Project Progress Update</h5>
            </div>
            <div class="col-sm-3"></div>
        </div>
        <div class="row text-center">
            <div class="col-sm-4"></div>
             <div class="col-sm-4">
        <div style="text-align: center; font-size: 14px; font-weight: bold"">
        <asp:Label id="lblProjDetail" runat="server"   Visible="true" ForeColor="Black" ></asp:Label>
            </div>
                 </div>
                 <div class="col-sm-4"></div>
                 </div>
        <br />
        <div class="container" >
            <div class="row">
                <div class="col-xs-1" style="text-align:left">
                <label id="lblSearch"  style="margin-right:5px; margin-left:20px; margin-top:5px; font:300">Activity</label>
                    </div>
                <div class="col-xs-1" style="width:150px;margin-right:5px">
         <asp:TextBox runat="server" CssClass="form-control" ID="txtDsc" Enabled="true" CausesValidation="false" />
                    </div>
                <div class="col-xs-10" style="margin-left :0px"">
         <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click"
                 CssClass="btn" Text="Search" BackColor="#cccccc"/>
                    </div>
            </div>
            </div>
        <br />
         <div class="row text-center">
            <div class="col-sm-4">
                <div style="text-align: left; margin-left:20px; font-size: 14px; font-weight:normal"">
        <asp:Label id="lblActivityShow" runat="server"   Visible="true" ForeColor="Black" ></asp:Label>
            </div>
                </div>
             <div class="col-sm-4">
        <div style="text-align: center; font-size: 14px; font-weight: bold"">
        <asp:Label id="Label5" runat="server"   Visible="true" ForeColor="Red" ></asp:Label>
            </div>
                 </div>
                 <div class="col-sm-4"></div>
                 </div>
         <br />
        <div class="container-fluid">
            <div class="row">
        <div class="col-xs-12"></div>
        <div class="container text-center" runat="server" id="mydiv">
        </div>
                </div>
            </div>
    </form>
</body>
</html>