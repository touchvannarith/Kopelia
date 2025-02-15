<%@ Page Title="Espace client" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Espace_client.aspx.cs" Inherits="NotaliaOnline.Espace_client" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .btn-collapse-container {
    position: absolute;
    left: 0;
}
        .card-header > img{
            width:44px;
            margin-left: -25px;
            float: left;
        }
    </style>
    <div class="card m-3">
        <div class="card-header">
            <button type="button" class="btn"><i class="fa fa-plus"></i></button>
            Informations personnelles
        </div>
        <div class="card-body" style="display: none;">
            <div class="row form-group">
                <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">ADRESSE E-MAIL&nbsp;<span class="text-danger">*</span></label>
                <div class="col-sm-5 col-12">
                    <input type="text" name="txt_ADRESSE_EMAIL" class="form-control control-input" clientidmode="Static" value="" runat="server" id="txt_ADRESSE_EMAIL" />
                </div>
            </div>
            <div class="row form-group">
                <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">MOT DE PASSE <span class="text-danger">*</span></label>
                <div class="col-sm-5 col-12">
                    <asp:TextBox runat="server" CssClass="form-control control-input" ClientIDMode="Static" TextMode="Password" ID="txt_MOT_DE_PASSE" />
                </div>
            </div>
            <div class="row form-group">
                <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">CONFIRMATION MOT DE PASSE&nbsp;<span class="text-danger">*</span></label>
                <div class="col-sm-5 col-12">
                    <asp:TextBox runat="server" ClientIDMode="Static" TextMode="Password" CssClass="form-control control-input" ID="txt_CONFIRMATION_MOT_DE_PASSE" />
                </div>
            </div>
            <div class="row form-group">
                <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">TÉLÉPHONE&nbsp;&nbsp;<span class="critical"></span></label>
                <div class="col-sm-5 col-12">
                    <input class="form-control control-input" type="text" name="txt_TÉLÉPHONE" clientidmode="Static" id="txt_TÉLÉPHONE" runat="server" />
                </div>
            </div>
            <div class="row form-group text-center">
                <div class="col-12">
                    <asp:LinkButton OnClick="btnEnregistrer_Click" OnClientClick="return ValidateFields();" class="btn btn-success" runat="server" id="btnEnregistrer" clientidmode="Static"><i class="fa fa-arrow-circle-right" aria-hidden="true"></i>&nbsp;Enregistrer</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="card m-3" runat="server" id="divUserManagement" visible="False">
        <div class="card-header">
            <button type="button" class="btn"><i class="fa fa-plus"></i></button>
            Gestion des utilisateurs
        </div>
        <div class="card-body" style="display: none">
            <div class="row form-group justify-content-center">
                <div class="col-12 col-sm-4">
                    <button type="button" runat="server" clientidmode="Static" id="btnInviteUser" class="btn btn-primary" onclick="openModalInviteUser();">Inviter un utilisateur</button>
                </div>
                <div class="col-12 col-sm-6 fa-2x">
                    <label runat="server" clientidmode="Static" id="lblTotalLicense"></label>
                    <br />
                    <label runat="server" clientidmode="Static" id="lblLicenseActivated"></label>
                </div>
            </div>
            <div class="row form-group text-center" runat="server" id="divAllLicensesActivated">
                <label class="col-12 fa-2x">Toutes les licences sont affectées. Vous pouvez modifier votre abonnement dans l'Espace client.</label>
            </div>
            <asp:Repeater runat="server" ClientIDMode="Static" ID="RepeaterDeviceForOwner">
                <ItemTemplate>
                    <div class="row form-group">
                        <label class="col-sm-2 col-12"><%# Eval("UserRole") %></label>
                        <label class="col-sm-5 col-12"><%# Eval("DeviceNumber") %></label>
                        <label class="col-sm-3 col-12"><%# Eval("Email") %></label>
                        <div class="col-sm-2 col-12"></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater runat="server" ClientIDMode="Static" ID="RepeaterDeviceForUser">
                <ItemTemplate>
                    <div class="row form-group">
                        <label class="col-sm-2 col-12"><%# Eval("UserRole") %></label>
                        <label class="col-sm-5 col-12"><%# Eval("DeviceNumber") %></label>
                        <label class="col-sm-3 col-12"><%# Eval("Email") %></label>
                        <div class="col-sm-2 col-12">
                            <button type="button" runat="server"
                                clientidmode="Static"
                                class="btn btn-success btn-sm"
                                id="btnDeleteDevice"
                                onserverclick="btnDeleteDevice_ServerClick">
                                <asp:HiddenField runat="server" ID="hdId" Value='<%#Eval("Id")%>' />
                                Supprimer
                            </button>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="card m-3">
        <div class="card-header">
            <button type="button" class="btn"><i class="fa fa-minus"></i></button>
            Modules de calcul
        </div>
        <div class="card-body pt-0" id="divCardBody_Modules">
            <div class="row">
                <div class="col-md-4 col-sm-12">
                    <div class="sub-card">
                        <div class="card-header">
                                <img src="images/divorce-partage.png" alt="" />
                            Actes portant sur la transmission du patrimoine à titre gratuit
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-12">
                                    <a href="BF02-19" id="BF02-19"><span>1.1 Attestation notariée</span> </a>
                                </div>
                                <div class="col-12">
                                    <a href="DON01" id="DON01"><span>1.2 Donation et donation partage (1 donateur)</span> </a>
                                </div>
                                <div class="col-12">
                                    <a href="DON02" id="DON02"><span>1.3 Donation et donation partage (2 donateurs)</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="DON-DROITS" id="DON-DROITS"><span>1.4 Donation - Calcul des droits de mutation à titre gratuit </span></a>
                                </div>
                                <div class="col-12">
                                    <a href="SUCC-DROITS" id="SUCC-DROITS"><span>1.5 Succession - Calcul des droits de mutation à titre gratuit </span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF02-121" id="BF02-121"><span>1.6 Notoriété après décès</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="SUCC01" id="SUCC01"><span>1.7 Règlement successoral ab intestat</span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-12">
                    <div class="sub-card">
                        <div class="card-header">
                                <img src="images/demembrement.png" alt="" />
                            Actes de partage et de licitation - indivision familiale
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-12">
                                    <a href="BF12-10" id="BF12-10"><span>2.1 Cession de droit indivis – Indivision familiale</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF12-7" id="BF12-7"><span>2.2 Licitation faisant cesser une indivision familiale</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF12-8" id="BF12-8"><span>2.3 Licitation faisant cesser une indivision suite à divorce ou rupture de PACS </span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF12-2" id="BF12-2"><span>2.4 Partage de biens de communauté consécutif à divorce (art 746 CGI)</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF12-1" id="BF12-1"><span>2.5 Partage de biens de communauté ou de succession (art 748 CGI)</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF12-1b" id="BF12-1b"><span>2.6 Partage de biens indivis - Indivision familiale (art 748 CGI)</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF12-2b" id="BF12-2b"><span>2.7 Partage de biens indivis consécutif à divorce ou rupture de PACS (art 746 CGI)</span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-12">
                    <div class="sub-card">
                        <div class="card-header">
                                <img src="images/plus-values.png" alt="" />
                            Actes relatifs régissant les liens familiaux
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-12">
                                    <a href="BF17" id="BF17"><span>3.1 Changement de régime matrimonial</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF02-5" id="BF02-5"><span>3.2 Contrat de mariage</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF02-9" id="BF02-9"><span>3.3 Donation entre époux</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF10" id="BF10"><span>3.4 Liquidation d'un régime communautaire dans le cas d'un divorce</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF02-11" id="BF02-11"><span>3.5 Mandat posthume & Mandat de protection future</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF02-2" id="BF02-2"><span>3.6 Pacte civil de solidarité</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF02-13" id="BF02-13"><span>3.7 Procuration</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF02-16" id="BF02-16"><span>3.8 Testaments (authentique et olographe)</span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-sm-12">
                    <div class="sub-card">
                        <div class="card-header">
                                <img src="images/ventes-cessions.png" alt="" />
                            Actes relatifs aux biens immobiliers
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-12">
                                    <a href="BF03" id="BF03"><span>4.1 Baux de droit commun (- de 12 ans)</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF04" id="BF04"><span>4.2 Baux spécifiques (+ de 12 ans)</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF02-122" id="BF02-122"><span>4.3 Notoriété prescription acquisitive</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF15" id="BF15"><span>4.4 Ventes en l'état futur d'achèvement</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF01" id="BF01"><span>4.5 Ventes immobilières</span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-12">
                    <div class="sub-card">
                        <div class="card-header">
                                <img src="images/donations-successions.png" alt="" />
                            Actes relatifs à l'activité économique
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-12">
                                    <a href="BF12-12" id="BF12-12"><span>5.1 Cession de droit indivis - Droit commun</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF061" id="BF061"><span>5.2 Cession de droits sociaux d'une SCI d'attribution</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF062" id="BF062"><span>5.3 Cession de droits sociaux ordinaire</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF08" id="BF08"><span>5.4 Crédit bail</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF09" id="BF09"><span>5.5 Echange</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF12-9" id="BF12-9"><span>5.6 Licitation faisant cesser une indivision de droit commun </span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF11" id="BF11"><span>5.7 Mainlevées et quittances</span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF12-3" id="BF12-3"><span>5.8 Partage de droit commun </span></a>
                                </div>
                                <div class="col-12">
                                    <a href="BF13" id="BF13"><span>5.9 Prêts hypothécaires</span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card m-3" id="mainSimulationActes">
        <div class="card-header">
            Simulations enregistrées
        </div>
        <div class="card-body">
            <div id="divSimulationActe"></div>
            <div class="sub-card" id="subSimulationArchive">
                <div class="card-header">
                    Archives
                </div>
                <div class="card-body">
                    <div id="divSimulationActeArchive"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalInviteUser" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Inviter un utilisateur</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmail" CssClass="form-control control-input" Placeholder="Email"></asp:TextBox>
                    <label>Vous pouvez saisir plusieurs e-mails. Séparez-les par un point-virgule.</label>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-primary" ID="btnEnvoyerModal" Text="Envoyer" OnClientClick="return ValidateEmail();" OnClick="btnEnvoyerModal_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="Scripts/Site.js"></script>
    <script type="text/javascript">
        function loadSimulations() {
            //Simulation acte
            $.ajax({
                url: 'Espace_client.aspx/GetSimulationActe',
                data: "{'clientId':<%=Session["CLIENT_ID"].ToString() %>, 'archive':false , 'pageIndex':1}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                //beforeSend: function () {
                //    //Show(); // Show loader icon
                //},
                success: function (response) {
                    $("#divSimulationActe").empty();
                    $.map(response.d, function (item) {
                        var row = "<div class='row form-group'>"
                            + "<div class='col-sm-3 col-12'>" + item.Libelle + "</div>"
                            + "<div class='col-sm-2 col-12'>" + item.PageFullName + "</div>"
                            + "<div class='col-sm-2 col-12'>" + item.EmailAddress + "</div>"
                            + "<div class='col-sm-2 col-12'>" + moment(item.DateUpdated).format('DD/MM/YYYY HH:mm:ss') + "</div>"
                            + "<div class='col-sm-3 col-12'>"
                            + "<button type='button' class='btn btn-success btn-edit' style='font-size: 1rem' data-redirect='" + item.PageName + "?Voir=" + item.Id + "'>Voir / Modifier</button>&nbsp;"
                            + "<button type='button' class='btn btn-success btn-archive' style='font-size: 1rem' id='btnArchive' data-id='" + item.Id + "'>Archiver</button>&nbsp;"
                        if (item.AllowDelete) {
                            row += "<button type='button' class='btn btn-success btn-delete' style='font-size: 1rem' id='btnDelete' data-id='" + item.Id + "'>Supprimer</button>"
                        }
                        + "</div>"
                            + "</div>";
                        $("#divSimulationActe").append(row);
                    });
                },
                //complete: function () {
                //    //Hide(); // Hide loader icon  
                //},
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });

            //Simulation acte archive
            <%--$.ajax({
                url: 'Espace_client.aspx/GetSimulationActe',
                data: "{'clientId':<%=Session["CLIENT_ID"].ToString() %>, 'archive':true , 'pageIndex':1}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                beforeSend: function () {
                    //Show(); // Show loader icon
                },
                success: function (response) {
                    $("#divSimulationActeArchive").empty();
                    $.map(response.d, function (item) {
                        var row = "<div class='row form-group'>"
                            + "<div class='col-sm-3 col-12'>" + item.Libelle + "</div>"
                            + "<div class='col-sm-2 col-12'>" + item.PageFullName + "</div>"
                            + "<div class='col-sm-2 col-12'>" + item.EmailAddress + "</div>"
                            + "<div class='col-sm-2 col-12'>" + moment(item.DateUpdated).format('DD/MM/YYYY HH:mm:ss') + "</div>"
                            + "<div class='col-sm-3 col-12'>"
                            + "<button type='button' class='btn btn-success btn-edit' style='font-size: 1rem' data-redirect='" + item.PageName + "?Voir=" + item.Id + "'>Voir / Modifier</button>&nbsp;"
                            + "<button type='button' class='btn btn-success btn-restart' style='font-size: 1rem' id='btnRestart' data-id='" + item.Id + "'>Restaurer</button>&nbsp;"
                        if (item.AllowDelete) {
                            row += "<button type='button' class='btn btn-success btn-delete' style='font-size: 1rem' id='btnDelete' data-id='" + item.Id + "'>Supprimer</button>"
                        }
                        + "</div>"
                            + "</div>";
                        $("#divSimulationActeArchive").append(row);
                    });
                },
                complete: function () {
                    //Hide(); // Hide loader icon  
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });--%>
        }

        function redirectTo(url) {
            window.location.href = url;
        }

        $(document).ready(function () {
            loadSimulations();
        });

        $("#divSimulationActe, #divSimulationActeArchive").on('click', '.btn-edit', function () {
            var redirect = $(this).attr("data-redirect");
            redirectTo(redirect)
        });

        $("#divSimulationActe").on('click', '.btn-archive', function () {
            var id = $(this).attr("data-id");
            $.ajax({
                url: 'Espace_client.aspx/Archive',
                data: "{'simulationId':" + id + "}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    toastr.success('Archivage effectué avec succès.', 'Notification', 'success');
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            loadSimulations();
        });

        $("#divSimulationActeArchive").on('click', '.btn-restart', function () {
            var id = $(this).attr("data-id");
            $.ajax({
                url: 'Espace_client.aspx/Restart',
                data: "{'simulationId':" + id + "}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    toastr.success('Restauration effectuée avec succès.', 'Notification', 'success');
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            loadSimulations();
        });

        $("#divSimulationActe, #divSimulationActeArchive").on('click', '.btn-delete', function () {
            var id = $(this).attr("data-id");
            $.ajax({
                url: 'Espace_client.aspx/Remove',
                data: "{'simulationId':" + id + "}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    toastr.success('Suppression terminée avec succès.', 'Notification', 'success');
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            loadSimulations();
        });

        var maxHeight = Math.max.apply(null, $("div#divCardBody_Modules div.card-header").map(function () {
            return $(this).height();
        }).get());
        $('div#divCardBody_Modules div.card-header').css('height', (maxHeight) + 'px');

        function openModalInviteUser() {
            $("#modalInviteUser").modal();
        }
        function isEmail(email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(email);
        }
        function ValidateFields() {
            $('input').removeClass("required");
            var ok = true;
            if (!isEmail($('#txt_ADRESSE_EMAIL').val()) || $('#txt_ADRESSE_EMAIL').val() === "") {
                $('#txt_ADRESSE_EMAIL').addClass("required");
                toastr.error("Le format de l'adresse email est incorrect.", "Notification", { timeOut: 5000 });
                ok = false;
            }
            if ($('#txt_MOT_DE_PASSE').val() === "") {
                $('#txt_MOT_DE_PASSE').addClass("required");
                toastr.error("Veuillez entrer un mot de passe.", "Notification", { timeOut: 5000 });
                ok = false;
            }
            if ($('#txt_CONFIRMATION_MOT_DE_PASSE').val() === "") {
                $('#txt_CONFIRMATION_MOT_DE_PASSE').addClass("required");
                toastr.error("Veuillez entrer un comfirmation mot de passe.", "Notification", { timeOut: 5000 });
                ok = false;
            }
            if ($('#txt_MOT_DE_PASSE').val() !== $('#txt_CONFIRMATION_MOT_DE_PASSE').val()) {
                $('#txt_MOT_DE_PASSE').addClass("required");
                $('#txt_CONFIRMATION_MOT_DE_PASSE').addClass("required");
                toastr.error("Veuillez renseigner un mot de passe commun aux champs MOT DE PASSE et CONFIRMATION MOT DE PASSE.", "Notification", { timeOut: 5000 });
                ok = false;
            }
            return ok;
        }
    </script>
</asp:Content>
