<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF09.aspx.cs" Inherits="NotaliaOnline.BF09" ValidateRequest="false" %>

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
                Echange
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
                        Premier échangiste
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Noms / Prénoms :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtNoms1" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nature du bien :</label>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl01" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Text="Immeuble droit commun" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Immeuble rural" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Evaluation du bien :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone01" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Deuxième échangiste
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Noms / Prénoms :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtNoms2" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nature du bien :</label>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl02" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Text="Immeuble droit commun" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Immeuble rural" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Evaluation du bien :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone02" CssClass="form-control numberFormatFR"></asp:TextBox>
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
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone03" CssClass="form-control numberFormatFR"
                                    Text="800,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Débours :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone04" CssClass="form-control numberFormatFR"
                                    Text="600,00"></asp:TextBox>
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
    <div runat="server" clientidmode="Static" ID="divResult">
        <div class="card">
            <div class="card-header">
                Total des droits et frais
            </div>
            <div class="card-body text-right">
                <div class="row">
                    <label class="col-sm-6 col-6">Emoluments du notaire :</label>
                    <label class="col-sm-3 col-6" runat="server" ClientIDMode="Static" id="F44"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Débours :</label>
                    <label class="col-sm-3 col-6" runat="server" ClientIDMode="Static" id="F43"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Trésor public :</label>
                    <label class="col-sm-3 col-6" runat="server" ClientIDMode="Static" id="F42"></label>
                </div>
                <div class="row fa-2x">
                    <label class="col-sm-6 col-6">Montant des frais : </label>
                    <label class="col-sm-3 col-6" runat="server" ClientIDMode="Static" ID="F45"></label>
                </div>
            </div>
        </div>
        <div class="card" >
            <div class="card-header">
                Détail des frais
            </div>
            <div class="card-body text-right">
                <div runat="server" id="row6">
                    <div class="sub-card">
                        <div class="card-header">
                            Emoluments du notaire - C.com. Art. A 444-117
                        </div>
                        <div class="card-body text-right">
                            <div class="row " runat="server" ID="row8" ClientIDMode="Static">
                                <div class="col-4 col-sm-3">
                                    <label runat="server" ID="E8"></label>
                                </div>
                                <div class="col-2 col-sm-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="F8"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="G8"></label>
                                </div>
                            </div>
                            <div class="row " runat="server" ID="row9" ClientIDMode="Static">
                                <div class="col-4 col-sm-3">
                                    <label runat="server" ID="E9"></label>
                                </div>
                                <div class="col-2 col-sm-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="F9"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="G9"></label>
                                </div>
                            </div>
                            <div class="row " runat="server" ID="row10" ClientIDMode="Static">
                                <div class="col-4 col-sm-3">
                                    <label runat="server" ID="E10"></label>
                                </div>
                                <div class="col-2 col-sm-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="F10"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="G10"></label>
                                </div>
                            </div>
                            <div class="row " runat="server" ID="row11" ClientIDMode="Static">
                                <div class="col-4 col-sm-3">
                                    <label runat="server" ID="E11"></label>
                                </div>
                                <div class="col-2 col-sm-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="F11"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="G11"></label>
                                </div>
                            </div>
                            <div class="row " runat="server" ID="row12" ClientIDMode="Static">
                                <div class="col-6 col-sm-4">
                                    <label>Total :</label>
                                </div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="F12"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" ID="row14">
                                <div class="col-6 col-sm-9">
                                    <label>Emolument minimum :</label>
                                </div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="G14"></label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6 col-sm-9">
                                    <label>Emolument réglementé :</label>
                                </div>
                                <div class="col-6 col-sm-3">
                                    <label runat="server" ID="G17"></label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card">
                        <div class="card-header">
                            Récapitulatif et calcul de la TVA
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-sm-9 col-6">Total HT des émoluments du notaire :</label>
                                <label class="col-sm-3 col-6" runat="server" ID="G19"></label>
                            </div>
                            <div class="row">
                                <label class="col-sm-9 col-6">Emoluments de formalités (HT) :</label>
                                <label class="col-sm-3 col-6" runat="server" ID="G20"></label>
                            </div>
                            <div class="row">
                                <label class="col-sm-9 col-6">Total HT :</label>
                                <label class="col-sm-3 col-6" runat="server" ID="G21"></label>
                            </div>
                            <div class="row">
                                <label class="col-sm-9 col-6">TVA :</label>
                                <label class="col-sm-3 col-6" runat="server" ID="G22"></label>
                            </div>
                            <div class="row">
                                <label class="col-sm-9 col-6">Total TTC :</label>
                                <label class="col-sm-3 col-6" runat="server" ID="G23"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card">
                        <div class="card-header">
                            Débours
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-sm-9 col-6">Débours :</label>
                                <label class="col-sm-3 col-6" runat="server" ID="G27"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="row29">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body">
                        <div runat="server" id="row31">
                            <div class="row">
                                <label class="col-12 text-left fa-2x">Droit d'échange :</label>
                            </div>
                            <div class="row" runat="server" ID="row32" ClientIDMode="Static">
                                <div class="col-4 col-sm-5 col-md-4">
                                    <label runat="server" ID="E32"></label>
                                </div>
                                <div class="col-2 col-sm-1 col-md-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3 col-md-3">
                                    <label runat="server" ID="F32"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-1 col-sm-offset-0 col-md-1">=</div>
                                <div class="col-6 col-sm-2 col-md-3">
                                    <label runat="server" ID="G32"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" ID="row33" ClientIDMode="Static">
                                <div class="col-12 text-center">
                                    <label>Exonération du droit d'échange</label>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="row35">
                            <div class="row">
                                <label class="col-12 text-left fa-2x">Droit de mutation :</label>
                            </div>
                            <div class="row" runat="server" ID="row36" ClientIDMode="Static">
                                <div class="col-12 col-sm-3 col-md-2 text-left">
                                    <label>Taxe départementale :</label>
                                </div>
                                <div class="col-4 col-sm-2 col-md-2">
                                    <label runat="server" ID="E36"></label>
                                </div>
                                <div class="col-2 col-sm-1 col-md-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3 col-md-3">
                                    <label runat="server" ID="F36"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-1 col-sm-offset-0 col-md-1">=</div>
                                <div class="col-6 col-sm-2 col-md-3">
                                    <label runat="server" ID="G36"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" ID="row37" ClientIDMode="Static">
                                <div class="col-12 col-sm-3 col-md-2 text-left">
                                    <label>Prélèvement de l'état sur taxe départementale :</label>
                                </div>
                                <div class="col-4 col-sm-2 col-md-2">
                                    <label runat="server" ID="E37"></label>
                                </div>
                                <div class="col-2 col-sm-1 col-md-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3 col-md-3">
                                    <label runat="server" ID="F37"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-1 col-sm-offset-0 col-md-1">=</div>
                                <div class="col-6 col-sm-2 col-md-3">
                                    <label runat="server" ID="G37"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" ID="row38" ClientIDMode="Static">
                                <div class="col-12 col-sm-3 col-md-2 text-left">
                                    <label>Taxe locale :</label>
                                </div>
                                <div class="col-4 col-sm-2 col-md-2">
                                    <label runat="server" ID="E38"></label>
                                </div>
                                <div class="col-2 col-sm-1 col-md-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3 col-md-3">
                                    <label runat="server" ID="F38"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-1 col-sm-offset-0 col-md-1">=</div>
                                <div class="col-6 col-sm-2 col-md-3">
                                    <label runat="server" ID="G38"></label>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-12 text-left fa-2x">CSI :</label>
                            </div>
                            <div class="row" runat="server" ID="row41" ClientIDMode="Static">
                                <div class="col-4 col-sm-5 col-md-4">
                                    <label runat="server" ID="E41"></label>
                                </div>
                                <div class="col-2 col-sm-1 col-md-1"><label>sur</label></div>
                                <div class="col-6 col-sm-3 col-md-3">
                                    <label runat="server" ID="F41"></label>
                                </div>
                                <div class="col-2 col-offset-4 col-sm-1 col-sm-offset-0 col-md-1">=</div>
                                <div class="col-6 col-sm-2 col-md-3">
                                    <label runat="server" ID="G41"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" ID="row42" ClientIDMode="Static">
                                <div class="col-12 text-center">
                                    <label>Exonération totale.</label>
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
    <script type="text/javascript" src="Scripts/BF09.js"></script>
</asp:Content>
