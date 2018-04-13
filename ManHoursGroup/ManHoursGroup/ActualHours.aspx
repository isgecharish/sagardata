<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ActualHours.aspx.cs" Inherits="ManHoursGroup.ActualHours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="scripts/jquery.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/bootstrap.min.js"></script>
    <link href="css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="scripts/bootstrap-multiselect.js"></script>


    <script type="text/javascript">
        $(function () {
            //Year
            $('[id*=ContentPlaceHolder1_ddlYear]').multiselect({
                includeSelectAllOption: true,
            })
             .multiselect('selectAll', false)
             .multiselect('updateButtonText');

            // Month
            $('[id*=ContentPlaceHolder1_ddlMonth]').multiselect({
                includeSelectAllOption: true,
            })
             .multiselect('selectAll', false)
             .multiselect('updateButtonText');

            //Project
            $('[id*=ContentPlaceHolder1_ddlProjects]').multiselect({
                includeSelectAllOption: true,
            })
          .multiselect('selectAll', false)
          .multiselect('updateButtonText');

            //Role
            $('[id*=ContentPlaceHolder1_ddlRoleType]').multiselect({
                includeSelectAllOption: true,
            })
          .multiselect('selectAll', false)
          .multiselect('updateButtonText');

            //Group
            $('[id*=ContentPlaceHolder1_ddlGroup]').multiselect({
                includeSelectAllOption: true,
            })
          .multiselect('selectAll', false)
          .multiselect('updateButtonText');

        })

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <table>
            <tr>
                <td>
                    <asp:ListBox runat="server" ID="ddlYear" SelectionMode="Multiple"></asp:ListBox></td>
                <td>
                    <asp:ListBox runat="server" ID="ddlMonth" SelectionMode="Multiple" /></td>
                <td>
                    <asp:ListBox runat="server" ID="ddlProjects" SelectionMode="Multiple" /></td>
                <td>
                    <asp:ListBox runat="server" ID="ddlRoleType" SelectionMode="Multiple" /></td>
                <td>
                    <asp:ListBox runat="server" ID="ddlGroup" SelectionMode="Multiple" /></td>
                <td>
                    <asp:Button runat="server" ID="btnSerach" Text="Search" OnClick="btnSerach_Click" /></td>
            </tr>
        </table>
    </div>

    <div style="margin-top: 30px; margin-left: 20px; font-size: 12px">
        <div runat="server" id="divAheader" visible="false" style="font-size: 12px; font-weight: bold">
            <table>
                <tr>
                    <td>Available/Actual Hours</td>
                    <td style="width: 860px"></td>
                    <td>
                        <asp:Button runat="server" Text="export to excel" ID="btnExportActualHours" OnClick="btnExportActualHours_Click" BorderStyle="Inset" BorderColor="White" /></td>
                </tr>
            </table>
        </div>
        <asp:GridView runat="server" ID="gvActualHours" AutoGenerateColumns="false" Width="90%" HeaderStyle-BackColor="#efefef" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvActualHours_PageIndexChanging"
            EnableEventValidation="false" EnableViewState="false">
            <Columns>
                <%-- <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Group ID">
                    <ItemTemplate>
                        <%#Eval("GroupID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Group Description">
                    <ItemTemplate>
                        <%#Eval("GroupDescription") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Role Type">
                    <ItemTemplate>
                        <%#Eval("RoleType") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Role Id" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("RoleID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Role Description">
                    <ItemTemplate>
                        <%#Eval("RoleDescription") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date1" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("Date1") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Week1" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("Week1") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Available Hours" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("AvailableHours") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actual Hours" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("ActualHours") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Noof Employees" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("NoofEmployees") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Month/Year" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("MonthYear") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("Year") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div style="font-size: 26px" runat="server" id="divNoRecActual" visible="false">No Record Found</div>
    </div>
    <div style="margin-top: 30px; margin-left: 20px; font-size: 12px">
        <div runat="server" id="divPheader" visible="false" style="font-size: 12px; font-weight: bold">
            <table>
                <tr>
                    <td>Planned Hours </td>
                    <td style="width: 900px"></td>
                    <td>
                        <asp:Button runat="server" Text="export to excel" ID="btnExportPlannedHours" OnClick="btnExportPlannedHours_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:GridView runat="server" ID="gvPlannedHors" AutoGenerateColumns="false" Width="90%" HeaderStyle-BackColor="#efefef" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPlannedHors_PageIndexChanging"
            EnableEventValidation="false" EnableViewState="false">
            <Columns>
                <%-- <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Project">
                    <ItemTemplate>
                        <%#Eval("Project") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Project Description">
                    <ItemTemplate>
                        <%#Eval("ProjectDescription") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Group ID">
                    <ItemTemplate>
                        <%#Eval("GroupID") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Group Description">
                    <ItemTemplate>
                        <%#Eval("GroupDescription") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Role Type">
                    <ItemTemplate>
                        <%#Eval("RoleType") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--    <asp:TemplateField HeaderText="Role Id" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%#Eval("RoleID") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Role Description">
                            <ItemTemplate>
                                <%#Eval("RoleDescription") %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Week1" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("Week1") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Planned Hours" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("PlannedHours") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date1">
                    <ItemTemplate>
                        <%#Eval("Date1") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Month/Year" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("MonthYear") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("Year") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div style="font-size: 26px;text-align:center" runat="server" id="divNorecordPlanned" visible="false">No Record Found</div>
    </div>
</asp:Content>
