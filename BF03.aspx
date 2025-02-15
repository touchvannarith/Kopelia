<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF03.aspx.cs" Inherits="NotaliaOnline.BF03" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #select2-ddlDurée-container {
            text-align: center;
        }

        ul#select2-ddlDurée-results li {
            text-align: center;
            padding-right: 2px;
        }
    </style>
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdUtilisation_du_futur_tarif" Value="1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="postback" Value="" />

    <div class="text-center">
        <label runat="server" clientidmode="static" id="lblShowProcessTime"></label>
    </div>

    <div class="text-center">
        <button type="button" id="btnInputForm1" class="btn btn-default btnInputForm mb-3 mr-1 ml-1">Saisie des données</button>
        <asp:Button CssClass="btn btn-default btn-calculate mb-3 mr-1 ml-1" runat="server" ClientIDMode="Static" ID="btnSynthese" Text="Calculez maintenant" OnClick="btnSynthese_Click" />
    </div>

    <div runat="server" clientidmode="Static" id="div_input_form_1">
        <div class="card">
            <div class="card-header">
                <button type="button" class="btn"><i class="fa fa-plus"></i></button>
                Les baux de droit commun (- de 12 ans)
            </div>
            <div class="card-body" style="display: none">
                <div class="sub-card">
                    <div class="card-header">
                        Références du dossier
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Dossier :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtDossier"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Date de signature :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control datePicker" runat="server" ClientIDMode="Static" ID="txtDateSignature"></asp:TextBox>
                                <i class="form-control-feedback fa fa-calendar"></i>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Rédacteur :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtRedacteur"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                <button type="button" class="btn"><i class="fa fa-minus"></i></button>
                Saisie
            </div>
            <div class="card-body">
                <div class="sub-card">
                    <div class="card-header">
                        Sur le type de bail
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Choix de l'acte :</label>
                            </div>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl0" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Bail à loyer d'habitation"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Bail à usage professionnel"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Bail mixte"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Bail sur bien immobilier - de 12 ans"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Bail rural à ferme (statut du fermage)"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Immeuble rural mise à disposition - de 12 ans"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Bail commercial"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-12 text-center">
                                <label id="lblDesc">Bail portant sur un local à usage commercial et régi par les articles L145-1 à L145-60 du Code de Commerce.</label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="sub-card">
                    <div class="card-header">
                        Donnees economiques du bail
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant du loyer mensuel :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtAmountRent"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant des charges mensuelles :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtAmountCharge"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Durée en années :</label>
                            </div>
                            <div class="col-sm-3  col-12">
                                <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlDurée">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-6 col-sm-offset-4 col-12">
                                <asp:CheckBox type="checkbox" CssClass="checkbox-custom" runat="server" ClientIDMode="Static"
                                    ID="chk1" Text="Le rédacteur a-t-il négocié le bail ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-6 col-sm-offset-4 col-12">
                                <asp:CheckBox type="checkbox" CssClass="checkbox-custom" runat="server" ClientIDMode="Static"
                                    ID="chk2" Text="Le bail est-il présenté volontairement à l'administration fiscale ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-6 col-sm-offset-4 col-12">
                                <asp:CheckBox type="checkbox" CssClass="checkbox-custom" runat="server" ClientIDMode="Static"
                                    ID="chk3" Text="Bail à ferme - Renouvellement ou prorogation ?" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" clientidmode="Static" id="mainConditionsParticulieres">
                    <div class="card-header">
                        Conditions particulieres
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-6 col-sm-offset-4 col-12">
                                <asp:CheckBox type="checkbox" CssClass="checkbox-custom" runat="server" ClientIDMode="Static"
                                    ID="chk4" Text="Cautionnement par un tiers dans l'acte principal ?" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Emoluments de formalités et Débours
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Emolument de formalités HT :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control numberFormatFR"
                                    Text="400,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Débours :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDébours" CssClass="form-control numberFormatFR"
                                    Text="200,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="text-center">
            <asp:Button CssClass="btn btn-default btn-calculate mb-3 mr-1 ml-1" runat="server" ClientIDMode="Static" ID="btnCalculez" Text="Calculez maintenant" OnClick="btnSynthese_Click" />
        </div>
    </div>
    <div runat="server" clientidmode="Static" id="divResult">
        <div class="card">
            <div class="card-header">
                Total des droits et frais
            </div>
            <div class="card-body text-right">
                <div class="row">
                    <div class="col-sm-6 col-12">
                        <label>Emoluments du notaire :</label>
                    </div>
                    <div class="col-sm-3 col-12">
                        <label runat="server" clientidmode="Static" id="lblEmoluments_TTC_du_notaire"></label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-12">
                        <label>Débours :</label>
                    </div>
                    <div class="col-sm-3 col-12">
                        <label runat="server" clientidmode="Static" id="lblDébours"></label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-12">
                        <label>Trésor public :</label>
                    </div>
                    <div class="col-sm-3 col-12">
                        <label runat="server" clientidmode="Static" id="lblTrésor"></label>
                    </div>
                </div>
                <div class="row fa-2x">
                    <div class="col-sm-6 col-12">
                        <label>Montant des frais :</label>
                    </div>
                    <div class="col-sm-3 col-12">
                        <label runat="server" clientidmode="Static" id="lblFrais_droits_et_émoluments"></label>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Détail des frais
            </div>
            <div class="card-body">
                <div class="sub-card" runat="server" clientidmode="Static" id="subEmoluments_du_notaire1">
                    <div class="card-header">
                        Emoluments du notaire
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <div class="col-sm-9 col-12">
                                <label>Emoluments fixes :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <label runat="server" clientidmode="Static" id="lblEmoluments_fixes"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" clientidmode="Static" id="subEmoluments_du_notaire2">
                    <div class="card-header">
                        Emoluments du notaire
                    </div>
                    <div class="card-body text-right">
                        <div>
                            <div runat="server" clientidmode="Static" id="divRow1" class="row">
                                <div class="col-4 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow11"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow12"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow13"></label>
                                </div>
                            </div>
                            <div runat="server" clientidmode="Static" id="divRow2" class="row">
                                <div class="col-4 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow21"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow22"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow23"></label>
                                </div>
                            </div>
                            <div runat="server" clientidmode="Static" id="divRow3" class="row">
                                <div class="col-4 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow31"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow32"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow33"></label>
                                </div>
                            </div>
                            <div runat="server" clientidmode="Static" id="divRow4" class="row">
                                <div class="col-4 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow41"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow42"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRow43"></label>
                                </div>
                            </div>
                            <div runat="server" clientidmode="Static" id="divRowTotal" class="row">
                                <div class="col-6 col-sm-4">
                                    <label>Total :</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" clientidmode="Static" id="lblRowTotal"></label>
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row121">
                            <div class="col-sm-9 col-6">
                                <label>Minimum de perception HT :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H121"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row122">
                            <div class="col-sm-9 col-6">
                                <label>Total HT :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H122"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row123">
                            <div class="col-sm-9 col-6">
                                <label>Emoluments fixes :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H123"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="row125">
                    <div class="card-header">
                        Emoluments du notaire - Récapitulatif
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <div class="col-sm-9 col-6">
                                <label>Emoluments du notaire (HT) :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H126"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row127">
                            <div class="col-sm-9 col-6">
                                <label>Emoluments de caution (HT) :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H127"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-9 col-6">
                                <label>Montant total (HT) :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H128"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Récapitulatif et calcul de la TVA
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <div class="col-sm-9 col-6">
                                <label>Total HT des émoluments du notaire :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H131"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-9 col-6">
                                <label>Emoluments de formalités :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H132"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-9 col-6">
                                <label>Total HT :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H133"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-9 col-6">
                                <label>TVA :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H134"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-9 col-6">
                                <label>Total TTC :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H135"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Débours
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <div class="col-sm-9 col-6">
                                <label>Débours :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" clientidmode="Static" id="H138"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="row141">
                            <div class="col-sm-9 col-12">
                                <label>Présentation volontaire - Droit fixe (739 du CGI) :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <label runat="server" clientidmode="Static" id="H141"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row142">
                            <div class="col-sm-12 text-center">
                                <label>Pas de présentation - Exonération</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card" runat="server" clientmode="static" id="divLibelleSimulation">
            <div class="card-header">
                Enregistrer la simulation
            </div>
            <div class="card-body text-right">
                <div class="row">
                    <label class="col-12 col-sm-3 col-form-label text-left text-sm-right">Nom de la simulation :</label>
                    <div class="col-12 col-sm-6">
                        <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtLibelle"></asp:TextBox>
                    </div>
                    <div class="col-12 col-sm-3 text-center">
                        <asp:LinkButton runat="server" ClientIDMode="Static" ID="btnEnregistrer" class="btn btn-success mb-2" OnClientClick="return ValidationSave();" OnClick="btnEnregistrer_Click">
                            <i class="fa fa-save" aria-hidden="true"></i>
                            Enregistrer
                        </asp:LinkButton>
                        <button type="button" runat="server" clientidmode="Static" id="btnOpenEnregistrerSous" class="btn btn-success mb-2" data-toggle="modal" data-target="#modalEnregistrerSous" visible="False">
                            <i class="fa fa-save" aria-hidden="true"></i>
                            Enregistrer sous
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <div class="card" runat="server" clientmode="static" id="divSendEmail">
            <div class="card-header">
                Transmettre la simulation
            </div>
            <div class="card-body text-center">
                <div class="row">
                    <label class="col-12 col-sm-3 col-form-label text-left text-sm-right">Adresse e-mail :</label>
                    <div class="col-12 col-sm-6">
                        <input type="text" runat="server" clientidmode="Static" class="form-control" style="text-align: center;" id="txtEmail" textmode="Email" />
                    </div>
                    <div class="col-12 col-sm-3">
                        <asp:LinkButton runat="server" ClientIDMode="Static" ID="btnSendEmail" CssClass="btn btn-success mb-2" OnClientClick="return ValidationEmail();" OnClick="btnSendEmail_Click">
                        <i class="fa fa-envelope" aria-hidden="true"></i>
                        Envoyer
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="card" runat="server" clientmode="static" id="divPrint">
            <div class="card-header">
                Imprimer la simulation
            </div>
            <div class="card-body">
                <div class="form-group text-center">
                    <asp:LinkButton runat="server" ClientIDMode="Static" ID="btnPrint" CssClass="btn btn-success" OnClick="btnPrint_Click">
                    <i class="fa fa-print" aria-hidden="true"></i>
                    Imprimer
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalEnregistrerSous" style="text-align: center" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Enregistrer la simulation</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Nom de la simulation :
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtLibelleSous"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton class="btn btn-success" runat="server" ClientIDMode="Static" ID="btnEnregistrerSous" OnClientClick="return ValidationSaveAs();" OnClick="btnEnregistrerSous_Click">Ok</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div id="piechartContainer" runat="server" clientidmode="Static" style="display: none"></div>
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdPiechart" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdSaisie" Value="" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdResult" />

    <script type="text/javascript" src="Scripts/charts.min.js"></script>
    <script type="text/javascript" src="Scripts/Site.js"></script>
    <script type="text/javascript" src="Scripts/BF03.js"></script>
</asp:Content>
