<%@ Page Title="Logiciel notaire SaaS - actes notariés et calcul - Kopelia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NotaliaOnline._Default"
    MetaDescription="Logiciel des notaires professionnel de calcul pour frais d'actes notariés, plus values, donations, successions, viager - Kopelia."%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background: #C6EAFB">
        <div class="container text-center">
            <h1>LE CALCUL EN LIGNE DE LA TAXATION DES ACTES NOTARIÉS</h1>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-12 col-md-8">
                <p class="lead">
                    <strong>Kopelia</strong> un outil de calcul en ligne performant qui vous permet de calculer rapidement vos frais liés aux actes notariés.<br />
                    Simple et efficace, un gain de temps considérable en seulement quelques clics permettant de taxer en temps réel devant vos clients.<br />
                    Du temps gagné, un client immédiatement renseigné. Dotez-vous d'une solution informatique experte dans le domaine du droit notarial.
                </p>
            </div>
            <div class="col-12 col-md-4 d-none d-md-block text-center">
                <img src="images/100-compatible.png" alt="" class="img-fluid" />
            </div>
        </div>
    </div>
    <div class="container" style="margin-bottom: -90px;">
            <img src="images/logiciel-calcul-notaire.png" alt="logiciel-calcul-notaire" class="img-fluid" />
        </div>
    <div class="banner-section">
        <%--<div class="container">
            <img src="images/logiciel-calcul-notaire.png" alt="logiciel-calcul-notaire" class="img-fluid" />
        </div>--%>
    </div>
    <div class="call-center-section">
        <div class="container text-center text-white">
            <span>Commencez l’essai gratuit de​ <strong>KOPELIA</strong></span>
        </div>
    </div>
    <div class="container text-center pt-2">
        <p class="ff-din-cond-light font-italic display-6" style="color: #304f73">5 minutes d’installation, découvrez pendant <strong>8 jours</strong> les fonctionnalités du Forfait Équipe, sans carte de crédit</p>
        <div class="row form-group d-block d-sm-none">
            <div class="col-12 mb-2">
                <input type="text" runat="server" id="txtEmail" class="form-control" placeholder="Votre mail pro">
            </div>
            <div class="col-12">
                <asp:LinkButton runat="server" class="btn btn-success" id="btnRegisterForFree" OnClick="btnRegisterForFree_Click" style="background: #8FC73E"><i class="fa fa-check-circle"></i>S’incrire gratuitement !</asp:LinkButton>
            </div>
        </div>
        <div class="input-group mb-3 d-none d-sm-flex">
            <input type="text" runat="server" id="txtEmail1" class="form-control" placeholder="Votre mail pro">
            <div class="input-group-append">
                <asp:LinkButton runat="server" class="btn btn-success" id="btnRegisterForFree1" OnClick="btnRegisterForFree1_Click" style="background: #8FC73E"><i class="fa fa-check-circle"></i>S’incrire gratuitement !</asp:LinkButton>
            </div>
        </div>
        <p class="ff-din-cond-light font-italic display-6" style="color: #304f73">N’êtes-vous toujours pas convaincu ? <strong>Regardez ce que nous pouvons aussi vous offrir !</strong></p>
    </div>

    <script type="text/javascript">

</script>
</asp:Content>
