<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF02-11.aspx.cs" Inherits="NotaliaOnline.BF02_11" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                Mandat posthume & Mandat de protection future
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
                <div class="row form-group">
                    <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                    <div class="col-12 col-sm-6 col-sm-offset-4">
                        <asp:RadioButton runat="server" ClientIDMode="Static" ID="radMandat1" CssClass="checkbox-custom mandat"
                            GroupName="Mandat" Text="Etablissement du mandat" Checked="True" />
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                    <div class="col-12 col-sm-6 col-sm-offset-4">
                        <asp:RadioButton runat="server" ClientIDMode="Static" ID="radMandat2" CssClass="checkbox-custom mandat"
                            GroupName="Mandat" Text="Acceptation du mandat par acte séparé" />
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                    <div class="col-12 col-sm-6 col-sm-offset-4">
                        <asp:RadioButton runat="server" ClientIDMode="Static" ID="radMandat3" CssClass="checkbox-custom mandat"
                            GroupName="Mandat" Text="Révocation du mandat par la mandant" />
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                    <div class="col-12 col-sm-6 col-sm-offset-4">
                        <asp:RadioButton runat="server" ClientIDMode="Static" ID="radMandat4" CssClass="checkbox-custom mandat"
                            GroupName="Mandat" Text="Renonciation au mandat par le mandataire " />
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                    <div class="col-12 col-sm-6 col-sm-offset-4">
                        <asp:RadioButton runat="server" ClientIDMode="Static" ID="radMandat5" CssClass="checkbox-custom mandat"
                            GroupName="Mandat" Text="Examen des comptes du mandataire" />
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-12 col-sm-4 text-left text-sm-right">
                        <label class="col-form-label">Montant des recettes :</label>
                    </div>
                    <div class="col-12 col-sm-3">
                        <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtValueMandat1"></asp:TextBox>
                        <i class="form-control-feedback fa fa-euro"></i>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-12 col-sm-4 text-left text-sm-right">
                        <label class="col-form-label">Montant des dépenses :</label>
                    </div>
                    <div class="col-12 col-sm-3">
                        <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtValueMandat2"></asp:TextBox>
                        <i class="form-control-feedback fa fa-euro"></i>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Emoluments de formalités afférents à la rédaction de l'acte
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-12 col-sm-4 text-left text-sm-right">
                                <label class="col-form-label">Veuillez indiquer le nombre de pages :</label>
                            </div>
                            <div class="col-12 col-sm-3">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ID="txtNumPages" ClientIDMode="Static" value="5"></asp:TextBox>
                                <i class="form-control-feedback" style="right: 30px; top: 6px;">Pages</i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-12 col-sm-4 text-left text-sm-right">
                                <label class="col-form-label">Nombre de copies :</label>
                            </div>
                            <div class="col-12 col-sm-3">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ID="txtNumCopies" ClientIDMode="Static" value="1"></asp:TextBox>
                                <i class="form-control-feedback" style="right: 30px; top: 6px;">Copie(s)</i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-12 col-sm-4 text-left text-sm-right">
                                <label class="col-form-label">Nombre de copies authentiques :</label>
                            </div>
                            <div class="col-12 col-sm-3">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtNumExp" value="1"></asp:TextBox>
                                <i class="form-control-feedback" style="right: 30px; top: 6px; white-space: nowrap">Copies(s) authentique(s)</i>
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
                                    Text="19,74"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Débours :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDébours" CssClass="form-control numberFormatFR"
                                    Text="50,00"></asp:TextBox>
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
                    <label class="col-6 col-sm-6">Emoluments HT du notaire :</label>
                    <label runat="server" id="D523" clientidmode="Static" class="col-6 col-sm-3"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-6">Débours : </label>
                    <label runat="server" id="D524" clientidmode="Static" class="col-6 col-sm-3"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-6">Trésor public : </label>
                    <label runat="server" id="D525" clientidmode="Static" class="col-6 col-sm-3"></label>
                </div>
                <div class="row fa-2x">
                    <label class="col-6 col-sm-6">Montant des frais : </label>
                    <label runat="server" id="D526" clientidmode="Static" class="col-6 col-sm-3"></label>
                </div>


            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Détail des frais
            </div>
            <div class="card-body">
                <div class="sub-card">
                    <div class="card-header">
                        <span runat="server" clientidmode="Static" id="lblEmolumentTitle">Emoluments du notaire</span>
                    </div>
                    <div class="card-body text-right">
                        <div class="row ">
                            <div class="col-6 col-sm-9">
                                <label>Emolument réglementé :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D505"></label>
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
                            <div class="col-6 col-sm-9">
                                <label>Total HT des émoluments du notaire :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D509"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>Emoluments de formalités (HT) :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D510"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>Total HT :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D511"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>TVA :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D512"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>Total TTC :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D513"></label>
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
                            <div class="col-6 col-sm-9">
                                <label>Débours :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D517"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <div class="col-12 text-left fa-2x">
                                <label>Enregistrement :</label>
                            </div>
                            <div class="col-6 col-sm-9">
                                <label>Enregistrement :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D521"></label>
                            </div>
                        </div>
                        <div class="row text-center">
                            <div class="col-12 col-sm-12 ">
                                <label>
                                    Acte dispensé de formalité (CGI art 680) - Paiement du droit sur état
                                </label>
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
    <script type="text/javascript" src="Scripts/BF02-11.js"></script>
</asp:Content>
