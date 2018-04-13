<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="LN_PMDL007.aspx.cs" Inherits="ManHoursGroup.LN_PMDL007" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #e6e4e4;
        }

        .GridPager a,
        .GridPager span {
            display: inline-block;
            padding: 0px 9px;
            margin-right: 4px;
            border-radius: 3px;
            border: solid 1px #c0c0c0;
            background: #e9e9e9;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            /*background-color: #5a7af7;*/
            color: #1d6af3;
            /*border: 1px solid #969696;*/
        }

        .GridPager span {
            background: #616161;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #f0f0f0;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #3AC0F2;
        }
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script>
        $(function () {
            $(".txtDate").datepicker({
                dateFormat: 'dd/mm/yy',
                // startDate: '-3d'
            });
            //var currentDate = new Date();
            //$(".txtDate").datepicker("setDate", currentDate)
        });

        function PrintDoc() {
            var toPrint = document.getElementById('divData');
            var popupWin = window.open(''); // window.open('', '_blank', 'width=950,height=800,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><style>table th{font-size:8px;background-color:#f4f4f4}table td{font-size:8px}</style></head><body onload="window.print()">') // th:nth-child(16){display:none;}td:nth-child(16){display:none;}
          //  popupWin.document.write('<div style="font-size:14px;text-align:center">Projectwise Documents<div>')
            popupWin.document.write(toPrint.innerHTML);
            popupWin.document.write('</html>');
            popupWin.document.close();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; font-size: 20px">Projectwise Documents</div>
    <div style="margin-top: 20px; font-size: 14px">
        <table>
            <tr>
                <td></td>
                <td>From</td>
                <td>To</td>
            </tr>
            <tr>
                <td>Project</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProjectFrom" Height="20" Width="172"></asp:DropDownList></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProjectTo" Height="20" Width="172"></asp:DropDownList></td>
                <td> &nbsp  &nbsp  
                    <asp:RadioButton runat="server" ID="rdAllData" Text="Planned Document" Checked="true" GroupName="a" /></td>
                <td>
                    <asp:RadioButton runat="server" ID="rdPending" Text="Pendig Document" GroupName="a" /></td>
                <td>
                    <asp:RadioButton runat="server" ID="rdReleased" Text="Only Released" GroupName="a" /></td>
            </tr>
            <tr>
                <td>Discipline</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlFromDescipline" Height="20" Width="172"></asp:DropDownList></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlToDescipline" Height="20" Width="172"></asp:DropDownList></td>
                
            </tr>
            <tr>
                <td>Date</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDateFrom" CssClass="txtDate" Height="15" Text="01/01/2000"></asp:TextBox></td>
                <td>
                    <asp:TextBox runat="server" ID="txtDateTo" CssClass="txtDate" Height="15"></asp:TextBox></td>
                <td>&nbsp  &nbsp  &nbsp  &nbsp  
                      <asp:Button runat="server" ID="btnShow" Text="Display" OnClick="btnShow_Click" Height="23" Width="100" /></td>
                    <td>  &nbsp  &nbsp  
                    <asp:Button runat="server" ID="btnExport" Text="Export to excel" OnClick="btnExport_Click" Visible="false" Height="23" /></td>
                     <td> &nbsp  &nbsp  
                    <asp:Button runat="server" Visible="false" ID="btnPrint" Text="Print" OnClientClick="PrintDoc()" Height="23" /></td>
            </tr>
            <tr>
            </tr>
        </table>

    </div>
    <div style="font-size: 10px; margin-top: 30px" id="divData">
         <div style="text-align: center; font-size: 20px;margin-bottom:20px"><span runat="server" id="spHeader" visible="false"> Projectwise Documents</span></div>
        <asp:GridView runat="server" ID="gvPMDL" CssClass="GridPager" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="White" AllowPaging="true" PageSize="50"
            EnableEventValidation="false" EnableViewState="false" OnPageIndexChanging="gvPMDL_PageIndexChanging">
            <Columns>
                <%-- <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                <%--<asp:TemplateField HeaderText="Project">
                    <ItemTemplate>
                        <%#Eval("Project") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DUID">
                    <ItemTemplate>
                        <%#Eval("DUID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Latest Revision">
                    <ItemTemplate>
                        <%#Eval("LatestRevision") %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                
               <asp:TemplateField HeaderText="Project">
                    <ItemTemplate>
                       <%#Eval("Project") %>( <%#Eval("t_dsca") %>)
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Document ID">
                    <ItemTemplate>
                        <%#Eval("DocumentID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Document Descriptionn">
                    <ItemTemplate>
                        <%#Eval("DocumentDescription") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Element">
                    <ItemTemplate>
                        <%#Eval("Element") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Document Category ID">
                    <ItemTemplate>
                        <%#Eval("DocumentCategoryID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Responsible Discipline">
                    <ItemTemplate>
                        <%#Eval("ResponsibleDiscipline") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Document Size ID">
                    <ItemTemplate>
                        <%#Eval("DocumentSizeID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Document Owner">
                    <ItemTemplate>
                        <%#Eval("DocumentOwner") %>
                    </ItemTemplate>
                </asp:TemplateField>

              <%--  <asp:TemplateField HeaderText="Planned Drawn By">
                    <ItemTemplate>
                        <%#Eval("PlannedDrawnBy") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Planned Checked By">
                    <ItemTemplate>
                        <%#Eval("PlannedCheckedBy") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PlannedEngineer">
                    <ItemTemplate>
                        <%#Eval("PlannedEngineer") %>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Zero Revision Release Date">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("ActualReleaseDate")).ToString("dd-MM-yyyy")!="01-01-1970"?Convert.ToDateTime(Eval("ActualReleaseDate")).ToString("dd-MM-yyyy") :"" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Baseline Schedule Start Date">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("BaselineScheduleStartDate")).ToString("dd-MM-yyyy")!="01-01-1970"?Convert.ToDateTime(Eval("BaselineScheduleStartDate")).ToString("dd-MM-yyyy") :""%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Baseline Schedule Finish Date">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("BaselineScheduleFinishDate")).ToString("dd-MM-yyyy")!="01-01-1970"?Convert.ToDateTime(Eval("BaselineScheduleFinishDate")).ToString("dd-MM-yyyy") :"" %>
                    </ItemTemplate>
                 </asp:TemplateField>

                   <asp:TemplateField HeaderText="Latest Revised Schedule Start Date">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("LatestRevisedScheduleStartDate")).ToString("dd-MM-yyyy")!="01-01-1970"?Convert.ToDateTime(Eval("LatestRevisedScheduleStartDate")).ToString("dd-MM-yyyy") :"" %>
                    </ItemTemplate>
                  </asp:TemplateField>

                   <asp:TemplateField HeaderText="Latest Revised Schedule Finish Date">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("LatestRevisedScheduleFinishDate")).ToString("dd-MM-yyyy")!="01-01-1970"?Convert.ToDateTime(Eval("LatestRevisedScheduleFinishDate")).ToString("dd-MM-yyyy") :"" %>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
