<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="SupEnquiryResponseForm.aspx.cs" Inherits="PreOrderWorkflow_Supplier.SupEnquiryResponseForm" %>

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
            margin-top: 12px;
        }

        .row {
            margin-right: -1px;
            margin-left: -1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb" runat="server" id="hHeader" style="text-align: center; font-size: 16px; font-weight: bold">View Enquiry</div>


    <div class="col-lg-10">
        <div class="form-panel">

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Project</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtProject" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Element</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtElement" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Specification No/Details</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSpecification" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Buyer</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtBuyer" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Supplier</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSupplier" Enabled="false"></asp:TextBox>
                        <%--  <ajaxToolkit:AutoCompleteExtender ServiceMethod="GetSupplier" MinimumPrefixLength="3"
                            CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtSupplier"
                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false" CompletionListElementID="divBuyer">
                        </ajaxToolkit:AutoCompleteExtender>
                        <div id="divBuyer" style="overflow-y: scroll; overflow-x: hidden; max-height: 400px;"></div>--%>
                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Supplier Email</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSupplierEmail" Enabled="false"></asp:TextBox>

                    </div>
                </div>
            </div>

            <div class="row mt">
                <div class="form-group">
                    <label class="col-sm-3">Notes</label>
                    <div class="col-sm-8">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNotes" TextMode="MultiLine" Height="100" Enabled="false"></asp:TextBox>

                    </div>
                </div>
            </div>

               <hr />

            <!-- Notes Strat -->

            <div style="font-size: 22px; font-weight: bold; margin-top: 20px; margin-left: 200px">
                <div style="text-align: center; font-size: 16px">Notes for <strong runat="server" id="spIndex"></strong></div>
                <div style="font-size: 12px;">
                    <asp:Button runat="server" ID="btnNewNotes" OnClick="btnNewNotes_Click" Text="New Notes" BackColor="" />
                </div>
            </div>

            <div style="margin-top: 10px; margin-left: 200px">
                <div style="background-color: ; min-height: 510px; width: 700px; border: 1px solid; overflow: hidden;">
                    <table style="margin-top: 10px">
                        <tr>
                            <td style="font-weight: bold">Title: &nbsp &nbsp &nbsp &nbsp &nbsp</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtTitle" Width="500"></asp:TextBox></td>
                        </tr>
                        <tr style="height: 10px"></tr>
                        <tr>
                            <td style="text-align: start; font-weight: bold">Desription</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="300" Width="500" />
                            </td>
                        </tr>
                        <tr style="height: 10px"></tr>
                        <tr>
                            <td style="font-weight: bold">Send mail to:&nbsp&nbsp&nbsp</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtMailTo" Width="500" />
                                &nbsp &nbsp
                            </td>
                        </tr>

                        <tr style="height: 10px"></tr>

                        <%--   <tr>
                    <td style="font-weight: bold">Reminder:</td>
                    <td>E-mail Id:&nbsp<asp:TextBox ID="txtMailIdReminder" runat="server" Width="430" />
                    </td>
                </tr>
                <tr style="height: 10px"></tr>
                <tr>
                    <td></td>
                    <td>Date :&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:TextBox runat="server" ID="txtDate" />
                        &nbsp &nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp<asp:TextBox runat="server" ID="txtTime" Text="9:00" ReadOnly="true" Enabled="false" Visible="false" />
                    </td>
                </tr>--%>
                        <tr style="height:20px"></tr>
                        <tr><td style="font-weight: bold">Attachments:</td>
                            <td>
                                <asp:FileUpload runat="server" ID="FileUpload" AllowMultiple="true" /></td>
                        </tr>
                         <tr style="height: 20px"></tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button runat="server" ID="btnSaveNotes" OnClick="btnSaveNotes_Click" Text="Submit" Width="100" />
                                <asp:Button runat="server" ID="btnDeleteNotes" OnClick="btnDeleteNotes_Click" Text="Delete" Visible="false" />
                                <%--<asp:Button runat="server" ID="btnAttachment" OnClick="btnAttachment_Click" Text="Attachment" />--%></td>
                        </tr>
                        <tr style="height: 10px"></tr>

                    </table>
                </div>

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

                        <%--<asp:TemplateField HeaderText="Attachment Handle">
                    <ItemTemplate>
                        <%#Eval("t_hndl") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Index Value">
                    <ItemTemplate>
                        <%#Eval("t_indx") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Purpose">
                    <ItemTemplate>
                        <%#Eval("t_desc") %>
                    </ItemTemplate>
                </asp:TemplateField>--%>

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
