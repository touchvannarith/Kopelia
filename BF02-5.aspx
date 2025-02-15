<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF02-5.aspx.cs" Inherits="NotaliaOnline.BF02_5" ValidateRequest="false" %>

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
                Le contrat de mariage
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
                    <div class="col-12 col-sm-8 col-sm-offset-4">
                        <asp:CheckBox CssClass="checkbox-custom" runat="server" ClientIDMode="Static"
                            ID="chkContratDeMariage" Text="Y a-t-il des biens dont la valeur est déclarée?" />
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Valeur totale des biens mobiliers et immobiliers :</label>
                    <div class="col-12 col-sm-3 ">
                        <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtValueContratDeMariage1"></asp:TextBox>
                        <i class="form-control-feedback fa fa-euro"></i>
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Valeur des droits immobiliers transmis au conjoint :</label>
                    <div class="col-12 col-sm-3 ">
                        <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtValueContratDeMariage2"></asp:TextBox>
                        <i class="form-control-feedback fa fa-euro"></i>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Emoluments de formalités afférents à la rédaction de l'acte
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Veuillez indiquer le nombre de pages :</label>
                            <div class="col-12 col-sm-3">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ID="txtNumPages" ClientIDMode="Static" value="5"></asp:TextBox>
                                <i class="form-control-feedback" style="right: 30px; top: 6px;">Pages</i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de copies :</label>
                            <div class="col-12 col-sm-3 ">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtNumCopies" value="1"></asp:TextBox>
                                <i class="form-control-feedback" style="right: 30px; top: 6px;">Copie(s)</i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de copies authentiques :</label>
                            <div class="col-12 col-sm-3 ">
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
                                    Text="23,51"></asp:TextBox>
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
                    <label runat="server" clientidmode="Static" id="D240" class="col-6 col-sm-3"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-6">Débours : </label>
                    <label runat="server" clientidmode="Static" id="D241" class="col-6 col-sm-3"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-6">Trésor public : </label>
                    <label runat="server" clientidmode="Static" id="D242" class="col-6 col-sm-3"></label>
                </div>
                <div class="row fa-2x">
                    <label class="col-6 col-sm-6">Montant des frais : </label>
                    <label runat="server" id="D243" class="col-6 col-sm-3"></label>
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
                        Emoluments du notaire - C.com. Art. A 444-82
                    </div>
                    <div class="card-body text-right">
                        <div class="row " runat="server" id="row207" clientidmode="Static">
                            <div class="col-4 col-sm-3">
                                <label runat="server" id="lblRow11"></label>
                            </div>
                            <div class="col-2 col-sm-1">
                                <label>sur</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblRow12"></label>
                            </div>
                            <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblRow13"></label>
                            </div>
                        </div>
                        <div class="row " runat="server" id="row208" clientidmode="Static">
                            <div class="col-4 col-sm-3">
                                <label runat="server" id="lblRow21"></label>
                            </div>
                            <div class="col-2 col-sm-1">
                                <label>sur</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblRow22"></label>
                            </div>
                            <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblRow23"></label>
                            </div>
                        </div>
                        <div class="row " runat="server" id="row209" clientidmode="Static">
                            <div class="col-4 col-sm-3">
                                <label runat="server" id="lblRow31"></label>
                            </div>
                            <div class="col-2 col-sm-1">
                                <label>sur</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblRow32"></label>
                            </div>
                            <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblRow33"></label>
                            </div>
                        </div>
                        <div class="row " runat="server" id="row210" clientidmode="Static">
                            <div class="col-4 col-sm-3">
                                <label runat="server" id="lblRow41"></label>
                            </div>
                            <div class="col-2 col-sm-1">
                                <label>sur</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblRow42"></label>
                            </div>
                            <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblRow43"></label>
                            </div>
                        </div>
                        <div class="row " runat="server" id="row211" clientidmode="Static">
                            <div class="col-6 col-sm-4">
                                <label>Total :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="lblTotal"></label>
                            </div>
                        </div>
                        <div class="row text-center" runat="server" id="row213" clientidmode="Static">
                            <div class="col-12" style="color: red">
                                <label>Prise en compte de l'émolument minimum.</label>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col-6 col-sm-9">
                                <label>Emolument réglementé :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D215"></label>
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
                            <div class="col-6 col-sm-9 ">
                                <label>Total HT des émoluments du notaire :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D218"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9 ">
                                <label>Emoluments de formalités (HT) :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D219"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9 ">
                                <label>Total HT :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D220"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9 ">
                                <label>TVA :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D221"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9 ">
                                <label>Total TTC :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D222"></label>
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
                            <div class="col-6 col-sm-9 ">
                                <label>Débours :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D226"></label>
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
                            <div class="col-12 col-sm-12 text-left fa-2x">
                                <label>Enregistrement :</label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row230">
                            <div class="col-6 col-sm-9 ">
                                <label>Enregistrement :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="D230"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 text-center">
                                <label>Acte enregistré gratuitement (CGI art 847-1)</label>
                            </div>
                        </div>
                        <div runat="server" id="row232">
                            <div runat="server" id="row234">
                                <div class="row text-left">
                                    <div class="col-12 col-sm-12 fa-2x">
                                        <label>Taxe de publicité :</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4 col-sm-3 ">
                                        <label runat="server" id="lblTaxeRow11"></label>
                                    </div>
                                    <div class="col-2 col-sm-1 ">
                                        <label>sur</label>
                                    </div>
                                    <div class="col-6 col-sm-3 ">
                                        <label runat="server" id="lblTaxeRow12"></label>
                                    </div>
                                    <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                    <div class="col-6 col-sm-3 ">
                                        <label runat="server" id="lblTaxeRow13"></label>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="row235">
                                    <div class="col-12 col-sm-12 text-center">
                                        <label style="color: red">Prise en compte de l'émolument minimum.</label>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="row237">
                                <div class="row text-left">
                                    <div class="col-12 col-sm-12 fa-2x">
                                        <label>CSI (art. 879 du CGI) :</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4 col-sm-3 ">
                                        <label runat="server" id="lblCSIRow11"></label>
                                    </div>
                                    <div class="col-2 col-sm-1 ">
                                        <label>sur</label>
                                    </div>
                                    <div class="col-6 col-sm-3 ">
                                        <label runat="server" id="lblCSIRow12"></label>
                                    </div>
                                    <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                    <div class="col-6 col-sm-3 ">
                                        <label runat="server" id="lblCSIRow13"></label>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="row238">
                                    <div class="col-12 col-sm-12 text-center">
                                        <label style="color: red">Prise en compte de l'émolument minimum.</label>
                                    </div>
                                </div>
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
    <script type="text/javascript" src="Scripts/BF02-5.js"></script>
</asp:Content>
