<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectLogin.aspx.cs" Inherits="ProjectProgressUpdate.ProjectLogin" %>

<%--<script src="scripts/bootstrap-datetimepicker.js"></script>
    <script src="scripts/bootstrap-datetimepicker.min.js"></script>
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title  style="text-align: center; font-size: 16px; font-weight: bold">Project Login </title>
     
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
  <%-- <link href="~/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
  
    <form id="form1" runat="server">
   <div class="container">
         <div class="row">
             <div class="col-xs-4"></div>
        <div class="col-xs-4  img-responsive"style="width:50%">
            <%--style="width:50%"      inside image Height="80px" Width="120px"--%>
            <asp:Image runat="server" ImageUrl="~/Images/ISGEC_Logo.jpeg" 
                 Height="80px" Width="120px" AlternateText ="ISGEC HEAVY ENGINEERING LTD."/>
            <div class="col-xs-4"></div>
        </div>
           
            </div>
      </div>
        <br />
      <%--  <div class="container-fluid" Height="80px" Width="120px"style="max-height:50% ;max-width:50%" align="Middle" >--%>
            <div class="row">
                <div class="col-xs-3"></div>
        <div class="col-xs-6"style="text-align:center" >
                <label id="lblSignIn">Sign in to application</label>
            <div class="col-xs-3"></div>
            </div>
                </div>
             <%--</div>--%>
        <hr />
                 <div class="container-fluid" >
            <div class="row">
                <div class="col-xs-4" style="text-align:right">
                <label id="lblLoginId">Username</label>
                    </div>
                <div class="col-xs-4">
         <asp:TextBox runat="server" CssClass="form-control" ID="txtUserName" Enabled="true" CausesValidation="True" />
                    </div>
               <div class="col-xs-4">
        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtUserName"
             errormessage="Please enter your Username!"  OnClick="btnEnter_Click" ForeColor="Red">Please enter Username!</asp:RequiredFieldValidator>
            </div>
                </div>
           
        </div>
        
         <br />
        <div class="container-fluid" >
            <div class="row">
          <div class="col-xs-4"style="text-align:right"">
    <label id="lblPassword"> Password </label>
              </div>
              <div class="col-xs-4">
         <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" Enabled="true" CausesValidation="True"/>
                 <%-- TextMode="Password" --%>
                 </div>
                   <div class="col-xs-4">
              <asp:RequiredFieldValidator runat="server" id="reqPassword" controltovalidate="txtPassword" 
                  errormessage="Please enter your Password!" OnClick="btnEnter_Click" ForeColor="Red">Please enter Password!</asp:RequiredFieldValidator>
    </div>
            </div>
            </div>
            <%--</div>--%>
        <%--<div class="col-lg-12" style="margin-top:30px">--%>
        <%--<div class="row" style="margin-top:0px">
            <div class="col-sm-4">
                <asp:TextBox runat="server" CssClass="form-control" ID="TextBox2" Enabled="true" CausesValidation="True"/>
              <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="txtPassword" 
                  errormessage="Please enter your Password!"  OnClick="btnEnter_Click" >Please enter your Password!</asp:RequiredFieldValidator>
            </div>
        </div>--%>
           
         <br />
        <div class="container-fluid">
            <div class="row">
        <div class="col-xs-4"></div>
        <div class="col-xs-4" style="margin-right:0px">
           <%-- <button class="btn btn-primary btn-lg btn-block" id="btnEnter" Width="200px">--%>
            <asp:Button runat="server" ID="btnEnter" OnClick="btnEnter_Click"
                 CssClass="btn" Text="Login" BackColor="#cccccc"/>
           
          <%--  <asp:CheckBox runat="server" Id ="chkRememberMe" />--%>
                <%--</button>--%>
        </div>
        <%--<div class="col-xs-1" style="margin-left:5px; margin-top:10px">
            <asp:CheckBox runat="server" Id ="chkRememberMe" />
        </div>--%>
                <div class="col-xs-4" style="text-align:left; text-size-adjust:auto; font:400; margin-left:0px; margin-right:20px ;margin-top:10px" >
            <%--<label id="lblRememberMe" >Remember Me</label>--%>
        </div>
            </div>
            </div>
        <br />
        <div class="col-xs-4"></div>
        <div class="col-xs-4">
            <label  runat="server" id="lblError" hidden="hidden"></label>
        </div>
        <div class="col-xs-4"></div>
    </form>
   <%--  <script src="scripts/jquery-2.1.1.min.js"></script>--%>
   <script src="scripts/bootstrap.min.js"></script>
</body>
</html>
