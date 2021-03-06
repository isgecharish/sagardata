﻿<%--<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="UpdateProjectProgress.aspx.cs" Inherits="ProjectProgressUpdate.UpdateProjectProgress" %>--%>
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center; font-size:16px; font-weight:bold" class="col-xs-12">Project Progress Update</div>
    <div class="col-xs-12" style="margin-top:20px">
        <div class="container">

    <div style="text-align: center; font-size: 14px; font-weight: bold"">
        <asp:Label id="lblProjDetail1" runat="server"   Visible="true" ForeColor="Black" ></asp:Label>
    </div>
    <br />
           
       <div class="container">
<div class="row" style="margin-top:10px">
            <div class="col-xs-3">
                <label>Activity</label>
                </div>
<div class="col-xs-9">
              <asp:TextBox runat="server" CssClass="form-control" ID="txtActivity" Enabled="false" />
                </div>

            </div>
            </div>

           <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-3">
                <label>Description</label>
                </div>
            <div class="col-xs-9">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" Enabled="false" />
                </div>

            </div>
            </div>
        
         <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-3">
                <label>Scheduled Start Date</label>
                </div>
<div class="col-xs-9">
<asp:TextBox runat="server" CssClass="form-control" ID="txtScheduledStartDate" Enabled="false" />
                </div>

            </div>
            </div>
      

             <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-3">
                <label>Scheduled Finish Date</label>
                </div>
<div class="col-xs-9">
                  <asp:TextBox runat="server" CssClass="form-control" ID="txtScheduledFinishDate" Enabled="false" />
                </div>

            </div>
            </div>

        <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-3">
                <label>Actual Start Date</label>
                </div>
                <div class="col-xs-9">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtActualStartDate" />
                </div>
            </div>
            </div>

            </div>
        <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-3">
                <label>Actual Finish Date</label>
                </div>
            <div class="col-xs-9">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtActualFinishDate" />
                </div>
            </div>
          </div>
        <div class="container">
               <div class="row" style="margin-top:10px">
            <div class="col-xs-3">
                <label>Remarks</label>
                
                </div>
            <div class="col-xs-9">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtRemarks" MaxLength="150" TextMode="MultiLine" BorderColor="#999966" Height="100px" />
                
                </div>

            </div>
            </div>

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
    


 


   <div style="font-size: 22px; font-weight: bold; margin-top: 20px; margin-left: 200px">
                <div style="text-align: center; font-size: 16px">Notes for <strong runat="server" id="spIndex"></strong></div>
                <div style="font-size: 12px">
                    <asp:Button runat="server" ID="btnNewNotes" OnClick="btnNewNotes_Click" Text="New Notes" BackColor="" />
                </div>
            </div>
   
            <%--<div style="margin-top: 10px; margin-left: 200px">
                <div style="min-height: 510px; width: 700px; border: 1px solid; overflow: hidden">--%>
      <%-- <div class="form-responsive">    --%>    
    <div class="container-fluid" style="column-width:auto">
      
           
        <div class="row" style="margin-top:10px">    
    <div class="col-xs-12"> 
    <div class="table-responsive"; style="border: 1px solid; margin-left:25px; margin-right:30px">
      <%--  <div style="min-height: 510px; width: 700px; border: 1px solid; overflow: hidden" class="table">--%>
      <%--  <div class="form-responsive">--%>
                    <%--<table style="margin-top: 10px">
                        <tr>
                            <td style="font-weight: bold">Title: &nbsp &nbsp &nbsp &nbsp &nbsp</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtTitle" Width="500"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: start; font-weight: bold">Desription</td>
                            <td>
                                <asp:TextBox runat="server" ID="TextBox1" TextMode="MultiLine" Height="300" Width="500" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">Send mail to:&nbsp&nbsp&nbsp</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtMailTo" Width="500" />
                                &nbsp &nbsp
                            </td>
                        </tr>

                        <tr><td style="font-weight: bold">Attachments:</td>
                            <td>
                                <asp:FileUpload runat="server" ID="FileUpload" AllowMultiple="true" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button runat="server" ID="btnSaveNotes" OnClick="btnSaveNotes_Click" Text="Submit" Width="100" />
                                <asp:Button runat="server" ID="btnDeleteNotes" OnClick="btnDeleteNotes_Click" Text="Delete" Visible="false" />
                        <%--</tr>
                        
                    <%--</table>--%>
            <%--<div class="container">--%>

             <div class="row" style="margin-top:10px">

            <div class="col-xs-3">
                <label> Title: </label>
                </div>
            <div class="col-xs-9">
                <div style="margin-left :0px; margin-right:15px">
                <%--Width="500"--%>
 <asp:TextBox runat="server" CssClass="form-control" ID="txtTitle"></asp:TextBox>
                    </div>
                </div>

                </div>

             <div class="row" style="margin-top:10px">

            <div class="col-xs-3">
                <label> Desription: </label>
                </div>
            <div class="col-xs-9">
                  <div style="margin-left :0px; margin-right:15px">
 <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" TextMode="MultiLine" Height="300"/>  
                      </div>              
</div>

                </div>

             <div class="row" style="margin-top:10px">

            <div class="col-xs-3">
                <label> Send mail: </label>
                </div>
            <div class="col-xs-9">
                  <div style="margin-left :0px; margin-right:15px">
            <asp:TextBox runat="server" ID="txtMailTo" CssClass="form-control"/>
                        </div>
                </div>

                </div>

             <div class="row" style="margin-top:10px">

            <div class="col-xs-3">
                <label> Attachments: </label>
                </div>
            <div class="col-xs-9">
                  <div style="margin-left :0px; margin-right:15px">
<asp:FileUpload runat="server" ID="FileUpload" CssClass="form-control" AllowMultiple="true" BorderColor="White"/>
                      </div>
                </div>

                </div>

             <div class="row" style="margin-top:10px">

            <div class="col-xs-6">
                  <div style="margin-left :0px; margin-right:15px;float: right">
<asp:Button runat="server" ID="btnSaveNotes"  CssClass="form-control" OnClick="btnSaveNotes_Click" Text="Submit" Width="100" />
                      </div>
                                                </div>
            <div class="col-xs-6">
                  <div style="margin-left :0px; margin-right:15px">
<asp:Button runat="server" ID="btnDeleteNotes" CssClass="form-control" OnClick="btnDeleteNotes_Click" Text="Delete" Visible="false" />
                      </div>
                </div>

                </div>


             <%--   </div>--%>


          <%--  form responsive end tag</div>--%>
              </div>
        </div> 
            </div>
        </div>
       <%-- </div>--%>
         
               <%-- </div>--%>
    <div class="table-responsive"; style="align-content:space-between; border:hidden; margin-left:25px">

                <div style="background-color: #cccaca; width: 800px; min-height: 100px; margin-top: 30px; font-weight: bold; overflow: hidden; font-size: 12px">
                    <div style="float: left; width: 50px">Notes</div>
                    <div style="float: right; width: 750px;">
                        <asp:Repeater runat="server" ID="rptNotes" OnItemDataBound="rptNotes_ItemDataBound">
                            <ItemTemplate>
                                <table>
                                    <tr id="row" runat="server"><td>
                                         <asp:HiddenField Value='<%#Eval("ColorId") %>' runat="server" ID="hdfUserID" /></td>
                                        <td style="width: 400px">
                                            <asp:LinkButton Text='<%#Eval("Description").ToString().Length>80 ? Eval("Description").ToString().Substring(0,80):Eval("Title").ToString() %>' runat="server" ID="lnkUpdate" CommandArgument='<%#Eval("UserId") +"&"+ Eval("NotesId") %>' OnClick="lnkUpdate_Click"></asp:LinkButton></td>
                                        <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%# Convert.ToDateTime(Eval("Created_Date")).ToString("dd-MM-yyyy HH:mm ")%></td>
                                        <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%#Eval("EmployeeName") %></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            

            <hr />
            <!-- Attachment -->
          <div style="text-align: center;font-size=16px"> <strong >Attachments</strong></div>
     <div class="container-fluid">
        <div class="table">
            <div style="margin-top: 20px; font-size: 12px; color: #646363" runat="server" id="divViewAttachment">
                <asp:GridView runat="server" HorizontalAlign="Center" ID="gvAttachment" AutoGenerateColumns="false" Width="90%"
                    HeaderStyle-BackColor="#cccccc" HeaderStyle-Height="20" ForeColor="" OnPageIndexChanging="gvAttachment_PageIndexChanging" AllowPaging="true" PageSize="50">
                    <%--OnRowDataBound="gvAttachment_RowDataBound" --%>
                    <Columns>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <%#Eval("t_fnam") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Upload Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120">
                            <ItemTemplate>
                                <%#Convert.ToDateTime(Eval("t_aton")).ToString("dd-MM-yyyy") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkDownload" CommandArgument='<%#Eval("t_dcid") +"@"+ Eval("t_fnam") +"@"+ Eval("t_hndl")%>' OnClick="lnkDownload_Click"><i class="fa fa-download" aria-hidden="true" style="color:#4371f1;font-size:12px"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
              
            <div runat="server" id="divNoRecord" style="text-align: center; font-size: 20px; margin-top: 50px; color: #b44c4c">Attachment not Found.</div>
     </div>
        </div>

    <asp:HiddenField runat="server" ID="hdfNoteId" />
    <asp:HiddenField runat="server" ID="hdfNewNoteId" />
    <asp:HiddenField runat="server" ID="hdfUser" />
    <asp:HiddenField runat="server" ID="hdfWFID" />
    <asp:HiddenField runat="server" ID="hdfPWFID" />
</asp:Content>

        
        
  
  

