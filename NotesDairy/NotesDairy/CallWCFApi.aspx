<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="CallWCFApi.aspx.cs" Inherits="NotesDairy.CallWCFApi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>  
    <script>
        $(function () {
            var Data = {
                "AthHandleS": 'J_IDMSPOSTORDERREC',
                "AthHandleT": 'Test',
                "IndexS": '5_11_18_83',
                "IndexT": '123'
            };
            $.ajax({
            type: "GET",
            url: "http://192.9.200.146/ProjectApi/AttachmentApi.svc/Attachments/J_IDMSPOSTORDERREC/Test/5_11_18_83/123",
            //   data: Data,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert("Success");
            },
            error: function (xhr) {
                alert("error");
            }
        });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
