<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeImprest.aspx.cs" Inherits="Imprest.EmployeeImprest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: #ebf6f9;">
    <form id="form1" runat="server">
      <%--  <div>
            <asp:TextBox ID="txtEmpId" runat="server"></asp:TextBox>
            <asp:Button Text="Show" ID="btnShow" OnClick="btnShow_Click" runat="server" />
        </div>--%>
        <div style="height: 30px"></div>
        <div style="background-color: #ebf6f9; text-align: center; width:100%" id="divEmplDetails" runat="server" visible="false">
            <div style="color: black; height: 150px">
                <div style="margin-top: 20px; font-weight: bold;">
                    Imprest Detail From  <span id="spdateFrom" runat="server"></span> To <span id="spdateTo" runat="server"></span>
                </div>
                <div style="margin-top: 20px; font-weight: bold;">
                    (Whatsoever is finalized in BaaN)
                </div>
                <div style="margin-top: 20px;">
                    Employee Code :<span id="spCode" runat="server" style="font-weight: bold;"></span>
                </div>
                <div style="margin-top: 20px;">
                    Employee Name :<span id="spName" runat="server" style="font-weight: bold;"></span>
                </div>
                 <div style="margin-top: 20px;">
                    Opening Balance :<span id="spBalance" runat="server" style="font-weight: bold;"></span>
                </div>
            </div>
            <div style="height: 30px"></div>
            <div style="margin-left:250px">
                <asp:GridView OnRowDataBound="gdImprset_RowDataBound" runat="server" ID="gdImprset"
                    AutoGenerateColumns="false" Width="80%" BorderColor="Black" HeaderStyle-BackColor="Blue"
                    HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" RowStyle-BackColor="#b3e6ff"
                    RowStyle-Font-Size="Small" RowStyle-ForeColor="Black">
                    <Columns>
                        <asp:BoundField HeaderText="Company" DataField="Company" />
                        <asp:BoundField HeaderText="Doc Type" DataField="DocType" />
                        <asp:BoundField HeaderText="Doc No" DataField="DocNo" />
                        <asp:BoundField HeaderText="Date" DataField="Date" />
                        <asp:BoundField HeaderText="Debit" DataField="Debit" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Credit" DataField="Credit" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Reference" DataField="Reference" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div id="divNoRecord" runat="server" visible="false" style="height:30px;background-color:cornflowerblue;color:white">
            No Record Found
        </div>
    </form>
</body>
</html>
