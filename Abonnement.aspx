<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Abonnement.aspx.cs" Inherits="NotaliaOnline.Abonnement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-md">
        <iframe id="content_iframe" src="" style="width: 100%; min-height: 565px;" frameborder="0"></iframe>
    </div>
    <script type="text/javascript">
        function iframeContent(src) {
            document.getElementById('content_iframe').src = src;
        }
    </script>
</asp:Content>
