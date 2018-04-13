<%--<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="UpdateProjectProgress.aspx.cs" Inherits="ProjectProgressUpdate.UpdateProjectProgress" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="UpdateProjectProgress.aspx.cs" Inherits="ProjectProgressUpdate.UpdateProjectProgress" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="scripts/bootstrap.min.js"></script>
    <script src="scripts/bootstrap-datetimepicker.js"></script>
    <script src="scripts/bootstrap-datetimepicker.min.js"></script>
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />

<%--    <script>
        $(function () {

            $(".txtDate").datetimepicker({
                // startDate: date,                             for hide back date
                format: "dd-mm-yyyy",
                autoclose: true,
                linkFormat: 'yyyy-mm-dd',
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0,
                daysOfWeekDisabled: "0",
                //daysOfWeekHighlighted: "6",
                beforeShowDay: function (date) {
                    // Always available
                    return [true, 'available', null];
                }
            });
           
        });
      
    </script>--%>
    <%--<script>
        if (txtActualFinishDate.value==null ||txtActualFinishDate.value=="01-01-1753" )
            document.getElementById('txtActualFinishDate').value = new Date().toISOString().substring(0, 10);
        if (txtActualStartDate.value == null || txtActualStartDate.value == "01-01-1753")
            //document.getElementById('txtActualStartDate').value = new Date().toISOString().substring(0, 10);
            document.getElementById('txtActualStartDate').value = new Date().toDateString("yyyy-MM-dd");
    </script>--%>
</asp:Content>





    
<%--</asp:Content>--%>
 
   
  
    <%--<form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center; font-size:16px; font-weight:bold" class="col-xs-12">Project Progress Update</div>
    <div class="col-xs-12" style="margin-top:20px">
        <div class="container">
    <div style="text-align: center; font-size: 14px; font-weight: bold"">
        <asp:Label id="lblProjDetail1" runat="server"   Visible="true" ForeColor="Black" ></asp:Label>
    </div>
    <br />
           
        <div class="row" style="margin-top:10px">
            <div class="col-xs-8">
                <label>Activity</label>
                </div>
            </div>
            </div>
        <div class="container">
      
           
        <div class="row" style="margin-top:10px">
            <div class="col-xs-12">
              <asp:TextBox runat="server" CssClass="form-control" ID="txtActivity" Enabled="false" />
                </div>
            </div>
            </div>

           <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-8">
                <label>Description</label>
                </div>
            </div>
            </div>
        <div class="container">
      
           
        <div class="row" style="margin-top:10px">
            <div class="col-xs-12">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" Enabled="false" />
                </div>
            </div>
            </div>

         <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-12">
                <label>Scheduled Start Date</label>
                </div>
            </div>
            </div>
        <div class="container">
      
           
        <div class="row" style="margin-top:10px">
            <div class="col-xs-12">
               <asp:TextBox runat="server" CssClass="form-control" ID="txtScheduledStartDate" Enabled="false" />
                </div>
            </div>
            </div>

             <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-12">
                <label>Scheduled Finish Date</label>
                </div>
            </div>
            </div>
        <div class="container">
      
           
        <div class="row" style="margin-top:10px">
            <div class="col-xs-12">
                  <asp:TextBox runat="server" CssClass="form-control" ID="txtScheduledFinishDate" Enabled="false" />
                </div>
            </div>
            </div>

        <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-8">
                <label>Actual Start Date</label>
                </div>
            </div>
            </div>
        <div class="container">
      
           
        <div class="row" style="margin-top:10px">
            <div class="col-xs-12">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtActualStartDate" />
               <%-- <input type="date" runat="server" class="form-control txtDate" id="txtActualStartDate" />--%>
                </div>
            </div>
            </div>

        <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-8">
                <label>Actual Finish Date</label>
                
                </div>
            </div>
            </div>
        <div class="container">
      
           
        <div class="row" style="margin-top:10px">
            <div class="col-xs-12">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtActualFinishDate" />
                </div>
            </div>
            </div>
        <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-8">
                <label>Remarks</label>
                
                </div>
            </div>
            </div>
        <div class="container">
      
           
        <div class="row" style="margin-bottom:10px">
            <div class="col-xs-12">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtRemarks" MaxLength="150" TextMode="MultiLine" BorderColor="#999966" Height="100px" />
                </div>
            </div>
            </div>

      <%--  <div class="row" style="margin-top:10px">
            <div class="col-sm-4">
                <label>Scheduled Start Date</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtScheduledStartDate" Enabled="false" />
            </div>
            <div class="col-sm-4">
                <label>Scheduled Finish Date</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtScheduledFinishDate" Enabled="false" />
                
            </div>
        </div>--%>

       <%-- <div class="row" style="margin-top:10px">
            <div class="col-sm-4">
                <label>Actual Start Date</label>
                <asp:TextBox runat="server" CssClass="form-control txtDate" ID="txtActualStartDate" />
            </div>
            <div class="col-sm-4">
                <label>Actual Finish Date</label>
                <asp:TextBox runat="server" CssClass="form-control txtDate" ID="txtActualFinishDate" />
            </div>
        </div>--%>
         <div class="container">
      
           
        <div class="row" style="margin-top:10px">
            
            <div class="col-xs-4">
                  <div style="margin-top:0px; margin-bottom:10px; float: right">
            <asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" CssClass="btn" Text="Update" />
            
        </div>
                </div>
            <div class="col-xs-4">
       <%-- <div style="margin-top: 20px; float: right">--%>
            <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn" Text="Back to Activity List" />
        <%--</div>--%>
                
                </div>
            </div>
             </div>
    </div>
    <div class="container">
      
           
        <div class="row" style="margin-bottom:300px">
</div>
             </div>
    <%--<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    </asp:Content>--%>

</asp:Content>

        
        
  
  

