<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF12-10.aspx.cs" Inherits="NotaliaOnline.BF12_10" ValidateRequest="false" %>

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
                Cession de droit indivis – Indivision familiale
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
                        Prix de cession d'une licitation ne faisant pas cesser l'indivision
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Prix (en ce compris la prise en charge du passif) : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone832" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Afférent aux biens immobiliers : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone834" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Afférent aux biens mobiliers : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone836" CssClass="form-control numberFormatFR" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
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
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Emolument de formalités HT :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Débours :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDébours" CssClass="form-control numberFormatFR"></asp:TextBox>
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
                    <label class="col-sm-6 col-6">Emoluments du notaire :</label>
                    <label runat="server" id="lblF104" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Débours :</label>
                    <label runat="server" id="lblF103" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Trésor public :</label>
                    <label runat="server" id="lblF102" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row fa-2x">
                    <label class="col-sm-6 col-6">Montant des frais :</label>
                    <label runat="server" id="lblF106" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Détail des frais
            </div>
            <div class="card-body">
                <div class="sub-card" runat="server" id="row155">
                    <div class="card-header">
                        Emoluments du notaire - C.com. Art. A 444-87b
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="row156">
                            <label runat="server" class="col-4 col-sm-3" id="F156"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="G156"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="H156"></label>
                        </div>
                        <div class="row" runat="server" id="row157">
                            <label runat="server" class="col-4 col-sm-3" id="F157"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="G157"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="H157"></label>
                        </div>
                        <div class="row" runat="server" id="row158">
                            <label runat="server" class="col-4 col-sm-3" id="F158"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="G158"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="H158"></label>
                        </div>
                        <div class="row" runat="server" id="row159">
                            <label runat="server" class="col-4 col-sm-3" id="F159"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="G159"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="H159"></label>
                        </div>
                        <div class="row" runat="server" id="row161">
                            <label class="col-6 col-sm-4">Total :</label>
                            <label runat="server" id="G161" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="row162">
                            <label class="col-6 col-sm-9">TOTAL Hors T.V.A :</label>
                            <label runat="server" id="H162" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="row164">
                            <label class="col-6 col-sm-9">Emolument minimum :</label>
                            <label runat="server" id="H164" class="col-6 col-sm-3"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Récapitulatif et calcul de la TVA
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-6 col-sm-9">Total HT des émoluments du notaire :</label>
                            <label runat="server" id="H171" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Emoluments de formalités (HT) :</label>
                            <label runat="server" id="H173" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total HT :</label>
                            <label runat="server" id="H175" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">TVA :</label>
                            <label runat="server" id="H176" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total TTC :</label>
                            <label runat="server" id="H177" class="col-6 col-sm-3"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Débours
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-6 col-sm-9">Débours :</label>
                            <label runat="server" id="lblH182" class="col-6 col-sm-3"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div runat="server" id="div187">
                            <div class="row text-left fa-2x">
                                <label class="col-12">Taxe publicité foncière :</label>
                            </div>
                            <div class="row" runat="server" id="div188">
                                <label runat="server" class="col-4 col-sm-3" id="lblF188"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" class="col-6 col-sm-3" id="lblG188"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" class="col-6 col-sm-3" id="lblH188"></label>
                            </div>
                            <div class="row text-center">
                                <label class="col-12" runat="server" id="lblF189"></label>
                                <div class="col-12" runat="server" id="div190">
                                    <label>Pour la TPF, il a été pris le minimum de perception soit 25 Euros.</label>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="row192">
                            <div class="row">
                                <label class="col-6 col-sm-9">Imputation des droits payés antérieurement :</label>
                                <label runat="server" id="H192" class="col-6 col-sm-3"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-9">Droits dûs après imputation :</label>
                                <label runat="server" id="H193" class="col-6 col-sm-3"></label>
                            </div>
                        </div>
                        <div runat="server" id="row195">
                            <div class="row">
                                <label class="col-12 text-left fa-2x">CSI (art. 879 du CGI) :</label>
                            </div>
                            <div class="row" runat="server" id="row196">
                                <label runat="server" class="col-4 col-sm-3" id="F196"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" class="col-6 col-sm-3" id="G196"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" class="col-6 col-sm-3" id="H196"></label>
                            </div>
                            <div class="row text-center" runat="server" id="row197">
                                <label class="col-12">Pour la CSI, il a été pris le minimum de perception soit 15 Euros.</label>
                            </div>
                        </div>
                        <div runat="server" id="row199">
                            <div class="row">
                                <label class="col-12 text-left fa-2x">Droit de mutation sur le prix de cession :</label>
                            </div>
                            <div class="row" runat="server" id="row200">
                                <label runat="server" class="col-4 col-sm-3" id="F200"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" class="col-6 col-sm-3" id="G200"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" class="col-6 col-sm-3" id="H200"></label>
                            </div>
                            <div class="row" runat="server" id="row201">
                                <label runat="server" class="col-4 col-sm-3" id="F201"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" class="col-6 col-sm-3" id="G201"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" class="col-6 col-sm-3" id="H201"></label>
                            </div>
                            <div class="row" runat="server" id="row202">
                                <label runat="server" class="col-4 col-sm-3" id="F202"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" class="col-6 col-sm-3" id="G202"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" class="col-6 col-sm-3" id="H202"></label>
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
    <script type="text/javascript" src="Scripts/BF12-10.js"></script>
</asp:Content>
