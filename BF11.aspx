<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF11.aspx.cs" Inherits="NotaliaOnline.BF11" ValidateRequest="false" %>

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
                Mainlevée et/ou de quittance
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
                        Nature de l'acte
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Choix :</label>
                            </div>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl1" CssClass="select2">
                                    <asp:ListItem Value="1" Text="Mainlevée"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Quittance"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Quittance et Mainlevée"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Type de Mainlevée :</label>
                            </div>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl2" CssClass="select2">
                                    <asp:ListItem Value="1" Text="Mainlevée hypothécaire simplifiée"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Mainlevée hypothécaire"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Mainlevée hypothécaire réduisant la créance"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Mainlevée hypothécaire réduisant le gage"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Mainlevée réduisant la créance et le gage"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Mainlevée de saisie"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Mainlevée de nantissement"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Type de Quittance :</label>
                            </div>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl3" CssClass="select2">
                                    <asp:ListItem Value="1" Text="Quittance pure et simple"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Quittance subrogative (1250-2 et 1251 CC)"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Quittance d'ordre judiciaire"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Quittance subrogative (1250-1 CC)"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Type de Quittance - Mainlevée :</label>
                            </div>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl4" CssClass="select2">
                                    <asp:ListItem Value="1" Text="Quittance et Mainlevée hypothécaire"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Quittance et Mainlevée hypothécaire réduisant la créance"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Quittance et Mainlevée hypothécaire réduisant le gage"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Quittance et Mainlevée réduisant la créance et le gage"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Quittance et Mainlevée de saisie"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Quittance et Mainlevée de nantissement"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Donnees generales
                    </div>
                    <div class="card-body">
                        <div class="row form-group" style="display: none">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Capital évalué au bordereau :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control control-input numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone01" Style="display: none"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdZone01" />
                            </div>
                        </div>
                        <div class="row form-group" style="display: none">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant de la somme réduisant la créance :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control control-input numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone02" Style="display: none"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdZone02" />
                            </div>
                        </div>
                        <div class="row form-group" style="display: none">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant de la valeur du gage dégrevé :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control control-input numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone03" Style="display: none"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdZone03" />
                            </div>
                        </div>
                        <div class="row form-group" style="display: none">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant des capitaux quittancés :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control control-input numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone04" Style="display: none"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdZone04" />
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
                            <label class="col-sm-4 col-12 text-left text-sm-right col-form-label">Emolument de formalités HT :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control control-input numberFormatFR"
                                    Text="150,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 text-left text-sm-right col-form-label">Débours :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDébours" CssClass="form-control control-input numberFormatFR"
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
                    <label runat="server" clientidmode="Static" id="G83" class="col-6 col-sm-3"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-6">Débours : </label>
                    <label runat="server" clientidmode="Static" id="G82" class="col-6 col-sm-3"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-6">Trésor public : </label>
                    <label runat="server" clientidmode="Static" id="G81" class="col-6 col-sm-3"></label>
                </div>
                <div class="row fa-2x">
                    <label class="col-6 col-sm-6">Montant des frais : </label>
                    <label runat="server" clientidmode="Static" id="G84" class="col-6 col-sm-3"></label>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Détail des frais
            </div>
            <div class="card-body">
                <div class="sub-card" runat="server" id="row5" visible="False">
                    <div class="card-header">
                        Emoluments proportionnels
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="row6" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E6"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F6"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G6"></label>
                        </div>
                        <div class="row" runat="server" id="row7" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E7"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F7"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G7"></label>
                        </div>
                        <div class="row" runat="server" id="row8" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E8"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F8"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G8"></label>
                        </div>
                        <div class="row" runat="server" id="row9" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E9"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F9"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G9"></label>
                        </div>
                        <div class="row" runat="server" id="row10" visible="False">
                            <label class="col-6 col-sm-3">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F10"></label>
                        </div>
                        <div class="row" runat="server" id="row11" visible="False">
                            <label class="col-6 col-sm-8">TOTAL Hors T.V.A :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G11"></label>
                        </div>
                        <div class="row" runat="server" id="row12" visible="False">
                            <label class="col-6 col-sm-8" runat="server" id="F12"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G12"></label>
                        </div>
                        <div class="row" runat="server" id="row13" visible="False">
                            <label class="col-6 col-sm-8">Emolument fixe HT :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G13"></label>
                        </div>
                        <div class="row" runat="server" id="row14" visible="False">
                            <label class="col-6 col-sm-8">Minimum de perception HT :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G14"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="row16" visible="False">
                    <div class="card-header">
                        Emoluments proportionnels
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="row17" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E17"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F17"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G17"></label>
                        </div>
                        <div class="row" runat="server" id="row18" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E18"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F18"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G18"></label>
                        </div>
                        <div class="row" runat="server" id="row19" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E19"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F19"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G19"></label>
                        </div>
                        <div class="row" runat="server" id="row20" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E20"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F20"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G20"></label>
                        </div>
                        <div class="row" runat="server" id="row21" visible="False">
                            <label class="col-6 col-sm-3">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F21"></label>
                        </div>
                        <div class="row" runat="server" id="row22" visible="False">
                            <label class="col-6 col-sm-8">TOTAL Hors T.V.A :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G22"></label>
                        </div>
                        <div class="row" runat="server" id="row23" visible="False">
                            <label class="col-6 col-sm-8">Minimum de perception HT :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G23"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="row25" visible="False">
                    <div class="card-header">
                        Emoluments proportionnels
                    </div>
                    <div class="card-body text-right">
                        <div class="text-center">
                            <label class="col-12 fa-2x">Au titre de la quittance</label>
                        </div>
                        <div class="row" runat="server" id="row27" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E27"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F27"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G27"></label>
                        </div>
                        <div class="row" runat="server" id="row28" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E28"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F28"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G28"></label>
                        </div>
                        <div class="row" runat="server" id="row29" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E29"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F29"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G29"></label>
                        </div>
                        <div class="row" runat="server" id="row30" visible="False">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E30"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F30"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G30"></label>
                        </div>
                        <div class="row" runat="server" id="row31" visible="False">
                            <label class="col-6 col-sm-3">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F31"></label>
                        </div>
                        <div class="row" runat="server" id="row32" visible="False">
                            <label class="col-6 col-sm-8">TOTAL Hors T.V.A :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G32"></label>
                        </div>
                        <div class="row" runat="server" id="row33" visible="False">
                            <label class="col-6 col-sm-8">Minimum de perception HT :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G33"></label>
                        </div>
                        <div runat="server" id="row35">
                            <div class="text-center">
                                <label class="col-12 fa-2x">Au titre de la mainlevée</label>
                            </div>
                            <div class="row" runat="server" id="row37" visible="False">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E37"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F37"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G37"></label>
                            </div>
                            <div class="row" runat="server" id="row38" visible="False">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E38"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F38"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G38"></label>
                            </div>
                            <div class="row" runat="server" id="row39" visible="False">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E39"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F39"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G39"></label>
                            </div>
                            <div class="row" runat="server" id="row40" visible="False">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E40"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F40"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G40"></label>
                            </div>
                            <div class="row" runat="server" id="row41" visible="False">
                                <label class="col-6 col-sm-3">Total :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F41"></label>
                            </div>
                            <div class="row" runat="server" id="row42" visible="False">
                                <label class="col-6 col-sm-8">TOTAL Hors T.V.A :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G42"></label>
                            </div>
                            <div class="row" runat="server" id="row43" visible="False">
                                <label class="col-6 col-sm-8">Emolument fixe HT :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G43"></label>
                            </div>
                            <div class="row" runat="server" id="row44" visible="False">
                                <label class="col-6 col-sm-8">Minimum de perception HT :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G44"></label>
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
                            <label class="col-6 col-sm-8">Emoluments du notaire HT :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G53"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">Emoluments de formalités (HT) :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G54"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">Total HT :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G55"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">TVA :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G56"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">Total TTC :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G57"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Debours
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-6 col-sm-8">Débours :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G61"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div class="row text-left">
                            <label class="col-12 fa-2x">Enregistrement :</label>
                        </div>
                        <div class="row text-center">
                            <label class="col-12" runat="server" id="D67" visible="False">Acte dispensé de la formalité (CGI art 846 bis)</label>
                            <label class="col-12" runat="server" id="D68" visible="False">Acte dispensé de la formalité (CGI art 680)</label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">Paiement du droit sur état :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G69"></label>
                        </div>
                        <div runat="server" id="row71" visible="False">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Contribution pour la sécurité immobilière :</label>
                            </div>
                            <div class="row" runat="server" id="row72" visible="False">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E72"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F72"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G72"></label>
                            </div>
                            <div class="row" runat="server" id="row73" visible="False">
                                <label class="col-6 col-sm-8">Minimum de perception :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G73"></label>
                            </div>
                        </div>
                        <div runat="server" id="row75" visible="False">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Greffe :</label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Coût pour une radiation totale :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G77"></label>
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
    <script type="text/javascript" src="Scripts/BF11.js"></script>
</asp:Content>
