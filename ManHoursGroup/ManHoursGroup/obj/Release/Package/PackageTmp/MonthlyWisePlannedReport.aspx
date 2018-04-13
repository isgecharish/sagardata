<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="MonthlyWisePlannedReport.aspx.cs" Inherits="ManHoursGroup.MonthlyWisePlannedReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script src="scripts/jquery.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <link href="css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="scripts/bootstrap-multiselect.js"></script>


    <script type="text/javascript">
        $(function () {


            //Project
            $('[id*=ContentPlaceHolder1_ddlProjects]').multiselect({
                includeSelectAllOption: true,
            })
          .multiselect('selectAll', false)
          .multiselect('updateButtonText');

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top:20px;margin-left:20px">
        <table>
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="ddlYearFrom" Width="100" Height="25"></asp:DropDownList></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlYearTo" Width="100" Height="25"></asp:DropDownList></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlMonthFrom" Width="100" Height="25"/></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlMonthTo" Width="100" Height="25"/></td>
                <td>
                    <asp:ListBox runat="server" ID="ddlProjects" SelectionMode="Multiple"  Visible="false" Width="100" Height="25"/></td>
                <td>
                    <asp:Button runat="server" ID="btnSerach" Text="Search" OnClick="btnSerach_Click" BorderStyle="Inset" BorderColor="White"/></td>
                <td>
                    <asp:Button runat="server" Text="export to excel" ID="btnExport" OnClick="btnExport_Click" BorderStyle="Inset" BorderColor="White"  Visible="false"/></td>
            </tr>
        </table>
    </div>

    <div style="font-size:10px;margin-top:20px;margin-left:2px;margin-right:2px">
        <asp:GridView runat="server" ID="gvPlannedHors" OnRowDataBound="gvPlannedHors_RowDataBound" Width="100%" HeaderStyle-BackColor="#cccccc" EnableEventValidation="false" EnableViewState="false" HeaderStyle-HorizontalAlign="Center"> 
            <Columns></Columns>
        </asp:GridView>
    </div>

</asp:Content>
