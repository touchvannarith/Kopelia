<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="NotaliaOnline.Connexion" %>

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

        .card-body {
            min-height: 276px
        }

        /*footer {
            clear: both;
            position: absolute;
            bottom: 0;
            width: 100%;
        }*/
        @media screen and (min-width:900px){
            footer {
                clear: both;
                position: absolute;
                bottom: 0;
                width: 100%;
            }
        }
    </style>
    <div class="m-3">
        <div class="row justify-content-center" runat="server" clientmode="static" id="divUser">
            <div class="col-12 col-lg-6 mb-3">
                <div class="card">
                    <div class="card-header" style="background: #ff7d26">
                        <button type="button" class="btn"><i class="fa fa-user"></i></button>
                        Je me connecte
                    </div>
                    <div class="card-body" style="background: #fddec3; background-image: url(./images/Kopelia-a-logo1.png); background-repeat: no-repeat; background-size: 80%; background-position: -15px 45px;">
                        <div class="text-center m-2">
                            <label>J'ai déjà un compte et je me connecte :</label>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Adresse email : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtEmail" placeholder="Adresse email"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Mot de passe : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtPassword" type="password" placeholder="Mot de passe" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 text-center"><a style="text-decoration: underline; color: red" runat="server" id="linkForgetPassword" onserverclick="linkForgetPassword_ServerClick">J’ai oublié mon mot de passe</a></label>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:LinkButton type="button" class="btn btn-warning" runat="server" ClientIDMode="Static" OnClientClick="return ValidateFields();" ID="btnLogin" OnClick="btnLogin_Click" Style="background: #ff7d26; color: #fff">
                        <i class="fa fa-chevron-circle-right" aria-hidden="true"></i>
                        Je me connecte
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-12 col-lg-6" runat="server" id="divUserCreate">
                <div class="card">
                    <div class="card-header">
                        <button type="button" class="btn"><i class="fa fa-user-plus"></i></button>
                        Je crée un compte
                    </div>
                    <div class="card-body">
                        <div class="text-center m-2">
                            <label>Je n'ai pas de compte et je renseigne les champs ci-dessous :</label>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Adresse email : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtEmail1" placeholder="Adresse email"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Mot de passe : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtPassword1" type="password" placeholder="Mot de passe"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Confirmer le mot de passe : <span class="text-danger">*</span></label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtPasswordConfirm1" type="password" placeholder="Confirmer le mot de passe"></asp:TextBox>
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
                    <asp:LinkButton type="button" class="btn btn-warning" runat="server" ClientIDMode="Static" OnClientClick="return ValidateFields1();" ID="btnCreateUser" OnClick="btnCreateUser_Click" Style="background: #304f73; color: #fff">
                        <i class="fa fa-chevron-circle-right" aria-hidden="true"></i>
                        Je crée un compte
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="row justify-content-center" runat="server" clientmode="static" id="divForgetPassword">
            <div class="col-12 col-sm-6">
                <div class="card">
                    <div class="card-header">
                        Mot de passe perdu
                    </div>
                    <div class="card-body" style="min-height: 60px">
                        <div class="row form-group">
                            <label class="col-12 col-sm-6 col-form-label text-left text-sm-right">Adresse email : </label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtEmailAddress" placeholder="Adresse email"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:LinkButton type="button" class="btn btn-warning" runat="server" ClientIDMode="Static" ID="btnEnvoyer" OnClick="btnEnvoyer_Click" Style="background: #304f73; color: #fff">
                        <i class="fa fa-chevron-circle-right" aria-hidden="true"></i>
                        Envoyer
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="row justify-content-center" runat="server" clientmode="static" id="divConfirm">
            <div class="col-12 col-sm-6">
                <div class="card">
                    <div class="card-header">
                        Confirmation
                    </div>
                    <div class="card-body" style="min-height: 88px">
                        <div class="row text-center">
                            <label class="col-12" runat="server" id="lblMessage"></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function defaultEnterKey(btn, event) {
            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                btn.click();
            }
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
                toastr.error('Le format de l`adresse email est incorrect.', "Notification", { timeOut: 5000 });
                ok = false;
            }
            if ($('#txtPassword').val() === "") {
                $('#txtPassword').addClass("required");
                ok = false;
            }
            return ok;
        }
        function ValidateFields1() {
            $('input').removeClass("required");
            var ok = true;
            if (!isEmail($('#txtEmail1').val()) || $('#txtEmail1').val() === "") {
                $('#txtEmail1').addClass("required");
                toastr.error("Le format de l`adresse email est incorrect.", "Notification", { timeOut: 5000 });
                ok = false;
            }
            if ($('#txtPassword1').val() === "") {
                $('#txtPassword1').addClass("required");
                ok = false;
            }
            if ($('#txtPasswordConfirm1').val() === "") {
                $('#txtPasswordConfirm1').addClass("required");
                ok = false;
            }
            if ($('#txtPassword1').val() !== $('#txtPasswordConfirm1').val()) {
                $('#txtPassword1').addClass("required");
                $('#txtPasswordConfirm1').addClass("required");
                toastr.error("Please, enter the same password.", "Notification", { timeOut: 5000 });
                ok = false;
            }
            return ok;
        }
    </script>
</asp:Content>
