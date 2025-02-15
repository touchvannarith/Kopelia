<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirmation_du_compte.aspx.cs" Inherits="NotaliaOnline.Confirmation_du_compte" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        label {
            font-family: DIN Cond-Light;
            font-size: 1.5rem !important;
            font-weight: 700;
        }

        .col-form-label {
            padding-top: calc(0.375rem - 6px);
            padding-bottom: calc(0.375rem + 2px);
        }
    </style>
    <div class="m-3">
        <div class="row justify-content-center" runat="server" clientmode="static" id="divActivateAccount" visible="false">
            <div class="col-12 col-sm-6">
                <div class="card">
                    <div class="card-header">
                        Activation de compte
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Adresse email : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtEmail" placeholder="Adresse email"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Mot de passe : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtPassword" type="password" placeholder="Mot de passe"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Confirmer le mot de passe : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtPasswordConfirm" type="password" placeholder="Confirmer le mot de passe"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Numéro de téléphone : </label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtTelephone" placeholder="Numéro de téléphone"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:LinkButton type="button" class="btn btn-primary" runat="server" ClientIDMode="Static" OnClientClick="return ValidateFields();" ID="btnCreateUser" OnClick="btnCreateUser_Click" Style="background: #304f73; color: #fff">
                        <i class="fa fa-chevron-circle-right" aria-hidden="true"></i>
                        Confirmer
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div runat="server" id="div_iframe">
            <iframe id="content_iframe" src="" style="width: 100%; height: 850px;" frameborder="0"></iframe>
        </div>
    </div>
    <script type="text/javascript">
        function iframeContent(src) {
            document.getElementById('content_iframe').src = src;
        }
        function isEmail(email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(email);
        }
        function ValidateFields() {
            $('input').removeClass("required");
            var ok = true;
            if (!isEmail($('#txtEmail').val()) || $('#txtEmail').val() === "") {
                $('#txtEmail').addClass("required");
                toastr.error("Le format de l`adresse email est incorrect.", "Notification", { timeOut: 5000 });
                ok = false;
            }
            if ($('#txtPassword').val() === "") {
                $('#txtPassword').addClass("required");
                ok = false;
            }
            if ($('#txtPasswordConfirm').val() === "") {
                $('#txtPasswordConfirm').addClass("required");
                ok = false;
            }
            if ($('#txtPassword').val() !== $('#txtPasswordConfirm').val()) {
                $('#txtPassword').addClass("required");
                $('#txtPasswordConfirm').addClass("required");
                ok = false;
            }
            return ok;
        }
    </script>
</asp:Content>
