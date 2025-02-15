<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF061.aspx.cs" Inherits="NotaliaOnline.BF061" ValidateRequest="false" %>

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
                Cession de droits sociaux d'une SCI d'attribution
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
                        Prix de cession
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-form-label text-left text-sm-right col-sm-4 col-12">Prix de cession (hors comptes courants) : </label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone01" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label text-left text-sm-right col-sm-4 col-12">Montant des charges : </label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone02" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Honoraires
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-form-label text-left text-sm-right col-sm-4 col-12">Montant des honoraires HT : </label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone03" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Droits de mutation - Taxes complémentaires
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-form-label text-left text-sm-right col-sm-4 col-12"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk1" CssClass="checkbox-custom col-sm-8 col-sm-offset-4 col-12"
                                Text="Le département a-t-il voté l'augmentation de sa part ?" Checked="True" />
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label text-left text-sm-right col-sm-4 col-12">Taux de la taxe départementale voté :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone04" CssClass="form-control numberFormatFR"
                                    Text="4,5"></asp:TextBox>
                                <i class="form-control-feedback fa fa-percent"></i>
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
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Débours :</label>
                            <div class="col-sm-6 col-12">
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
                    <label class="col-sm-6 col-6">Convention d'honoraires : </label>
                    <label runat="server" id="lblF102" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Débours : </label>
                    <label runat="server" id="lblF103" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Trésor public : </label>
                    <label runat="server" id="lblF104" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row fa-2x">
                    <label class="col-sm-6 col-6">Montant des frais : </label>
                    <label runat="server" id="lblF105" clientidmode="Static" class="col-sm-3 col-6"></label>
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
                        Convention d'honoraires
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-sm-6 col-6">Montant HT : </label>
                            <label runat="server" id="lblF107" class="col-sm-3 col-6"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-6">TVA : </label>
                            <label runat="server" id="lblF108" class="col-sm-3 col-6"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-6">Total TTC : </label>
                            <label runat="server" id="lblF109" class="col-sm-3 col-6"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Débours
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-sm-6 col-6">Débours : </label>
                            <label runat="server" id="lblF111" class="col-sm-3 col-6"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div class="row text-left">
                            <label class="col-12 col-sm-12 fa-2x">Montant des droits de mutation :</label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">Base taxable : </label>
                            <label runat="server" id="lblG114" class="col-sm-3 col-6"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-12">Taxe départementale : </label>
                            <label runat="server" id="lblF116" class="col-sm-3 col-6"></label>
                            <label runat="server" id="lblG116" class="col-sm-3 col-6"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-12">Prélèvement de l'Etat sur taxe départementale : </label>
                            <label runat="server" id="lblF117" class="col-sm-3 col-6"></label>
                            <label runat="server" id="lblG117" class="col-sm-3 col-6"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-12">Taxe locale : </label>
                            <label runat="server" id="lblF118" class="col-sm-3 col-6"></label>
                            <label runat="server" id="lblG118" class="col-sm-3 col-6"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">Total : </label>
                            <label runat="server" id="lblG119" class="col-sm-3 col-6"></label>
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
    <script type="text/javascript" src="Scripts/BF061.js"></script>
</asp:Content>
