<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="DON-DROITS.aspx.cs" Inherits="NotaliaOnline.DON_DROITS" ValidateRequest="false" %>

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
                DONATION - Droits de mutation à titre gratuit
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
                        Détermination de la parenté Donateur/Donataire
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Choix :</label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddl1">
                                    <asp:ListItem Value="1">Ligne directe descendant (1er degré)</asp:ListItem>
                                    <asp:ListItem Value="2">Ligne directe descendant (2ème degré) de son chef</asp:ListItem>
                                    <asp:ListItem Value="3">Collatéral privilégié (2ème degré)</asp:ListItem>
                                    <asp:ListItem Value="4">Collatéral ordinaire (3ème degré)</asp:ListItem>
                                    <asp:ListItem Value="5">Collatéral ordinaire (4ème degré par représentation)</asp:ListItem>
                                    <asp:ListItem Value="6">Collatéral ordinaire (4ème degré de son chef)</asp:ListItem>
                                    <asp:ListItem Value="7">Parent au-delà du 4ème degré et étranger</asp:ListItem>
                                    <asp:ListItem Value="8">Au profit du conjoint marié ou pacsé</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Données économiques
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Date de la mutation : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control control-input datePicker" runat="server" ClientIDMode="Static" ID="txtZone01"></asp:TextBox>
                                <i class="form-control-feedback fa fa-calendar"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Base taxable hors exonération : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone02"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk1" CssClass="checkbox-custom col-12 col-sm-8"
                                Text="Présence de Dons antérieurs ?" />
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Dons antérieurs taxés : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone03"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Gestion des abattements
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right" style="font-size: 1.2em" id="label1">Abattement commun : </label>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Abattement commun  utilisé : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone04"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right" style="font-size: 1.2em">Abattement au titre des handicapés : </label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk2" CssClass="checkbox-custom col-12 col-sm-8"
                                Text="Cocher si Oui" />
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Abattement handicapé utilisé : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone05"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right" style="font-size: 1.2em">Abattement spécifique pour somme d'argent : </label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk3" CssClass="checkbox-custom col-12 col-sm-8"
                                Text="Cocher si Oui" />
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Abattement (790 A bis) utilisé : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone06"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right" style="font-size: 1.2em">Abattement descendant au 2éme degré : </label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk4" CssClass="checkbox-custom col-12 col-sm-8"
                                Text="Cocher si Oui" />
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Abattement (790 B) utilisé : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone07"></asp:TextBox>
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
                Détail des frais
            </div>
            <div class="card-body text-right">
                <div class="sub-card">
                    <div class="card-header">
                        Détermination de l'assiette taxable
                    </div>
                    <div class="card-body">
                        <div class="row" style="font-size: 1.2em">
                            <label class="col-12 col-sm-6">Part du donataire dans la donation :</label>
                            <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="G10"></label>
                        </div>
                        <div runat="server" id="row12">
                            <div class="row">
                                <label class="col-12 col-sm-6">Abattement général (Art. 779 IV CGI) :</label>
                                <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="G12"></label>
                            </div>
                            <div runat="server" id="row14">
                                <div class="row">
                                    <label class="col-12 col-sm-9">Abattement général déjà utilisé :</label>
                                    <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="H14"></label>
                                </div>
                                <div class="row">
                                    <label class="col-12 col-sm-9">Abattement résiduel :</label>
                                    <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="H15"></label>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="row17">
                            <div class="row">
                                <label class="col-12 col-sm-6">Abattement pour infirmité :</label>
                                <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="G17"></label>
                            </div>
                            <div runat="server" id="row19">
                                <div class="row">
                                    <label class="col-12 col-sm-9">Abattement déjà utilisé :</label>
                                    <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="H19"></label>
                                </div>
                                <div class="row">
                                    <label class="col-12 col-sm-9">Abattement résiduel :</label>
                                    <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="H20"></label>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="row22">
                            <div class="row">
                                <label class="col-12 col-sm-6">Abattement spécifique pour somme d'argent (Art. 790Abis du CGI) :</label>
                                <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="G22"></label>
                            </div>
                            <div runat="server" id="row24">
                                <div class="row">
                                    <label class="col-12 col-sm-9">Abattement déjà utilisé :</label>
                                    <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="H24"></label>
                                </div>
                                <div class="row">
                                    <label class="col-12 col-sm-9">Abattement résiduel :</label>
                                    <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="H25"></label>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="row27">
                            <div class="row">
                                <label class="col-12 col-sm-6">Abattement descendant au 2ème degré (Art. 790B du CGI) :</label>
                                <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="G27"></label>
                            </div>
                            <div runat="server" id="row29">
                                <div class="row">
                                    <label class="col-12 col-sm-9">Abattement déjà utilisé :</label>
                                    <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="H29"></label>
                                </div>
                                <div class="row">
                                    <label class="col-12 col-sm-9">Abattement résiduel :</label>
                                    <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="H30"></label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="font-size: 1.2em">
                            <label class="col-12 col-sm-6">Part taxable : </label>
                            <label class="col-12 col-sm-3" runat="server" clientidmode="Static" id="G32"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="case1">
                    <div class="card-header">
                        Calcul des droits
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row100">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F100"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G100"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H100"></label>
                        </div>
                        <div class="row" runat="server" id="row101">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F101"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G101"></label>
                        </div>
                        <div class="row" runat="server" id="row102">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F102"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G102"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H102"></label>
                        </div>
                        <div class="row" runat="server" id="row103">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F103"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G103"></label>
                        </div>
                        <div class="row " runat="server" id="row104">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F104"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G104"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H104"></label>
                        </div>
                        <div class="row" runat="server" id="row105">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F105"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G105"></label>
                        </div>
                        <div class="row " runat="server" id="row106">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F106"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G106"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H106"></label>
                        </div>
                        <div class="row" runat="server" id="row107">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F107"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G107"></label>
                        </div>
                        <div class="row " runat="server" id="row108">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F108"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G108"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H108"></label>
                        </div>
                        <div class="row" runat="server" id="row109">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F109"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G109"></label>
                        </div>
                        <div class="row " runat="server" id="row110">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F110"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G110"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H110"></label>
                        </div>
                        <div class="row" runat="server" id="row111">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F111"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G111"></label>
                        </div>
                        <div class="row " runat="server" id="row112">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F112"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G112"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H112"></label>
                        </div>
                        <div class="row" runat="server" id="row113">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F113"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G113"></label>
                        </div>
                        <div class="row" runat="server" id="row114">
                            <label class="col-12 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G114" style="font-size: 1.2em"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H114" style="font-size: 1.2em"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="case2">
                    <div class="card-header">
                        Calcul des droits
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row200">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F200"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G200"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H200"></label>
                        </div>
                        <div class="row" runat="server" id="row201">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F201"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G201"></label>
                        </div>
                        <div class="row " runat="server" id="row202">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F202"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G202"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H202"></label>
                        </div>
                        <div class="row" runat="server" id="row203">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F203"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G203"></label>
                        </div>
                        <div class="row " runat="server" id="row204">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F204"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G204"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H204"></label>
                        </div>
                        <div class="row" runat="server" id="row205">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F205"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G205"></label>
                        </div>
                        <div class="row " runat="server" id="row206">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F206"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G206"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H206"></label>
                        </div>
                        <div class="row" runat="server" id="row207">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F207"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G207"></label>
                        </div>
                        <div class="row " runat="server" id="row208">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F208"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G208"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H208"></label>
                        </div>
                        <div class="row" runat="server" id="row209">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F209"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G209"></label>
                        </div>
                        <div class="row " runat="server" id="row210">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F210"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G210"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H210"></label>
                        </div>
                        <div class="row" runat="server" id="row211">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F211"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G211"></label>
                        </div>
                        <div class="row " runat="server" id="row212">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F212"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G212"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H212"></label>
                        </div>
                        <div class="row" runat="server" id="row213">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F213"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G213"></label>
                        </div>
                        <div class="row" runat="server" id="row214">
                            <label class="col-12 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G214" style="font-size: 1.2em"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H214" style="font-size: 1.2em"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="case3">
                    <div class="card-header">
                        Calcul des droits
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row300">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F300"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G300"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H300"></label>
                        </div>
                        <div class="row" runat="server" id="row301">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F301"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G301"></label>
                        </div>
                        <div class="row " runat="server" id="row302">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F302"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G302"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H302"></label>
                        </div>
                        <div class="row" runat="server" id="row303">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F303"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G303"></label>
                        </div>
                        <div class="row" runat="server" id="row304">
                            <label class="col-12 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G304" style="font-size: 1.2em"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H304" style="font-size: 1.2em"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="case4">
                    <div class="card-header">
                        Calcul des droits
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row400">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F400"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G400"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H400"></label>
                        </div>
                        <div class="row" runat="server" id="row401">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F401"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G401"></label>
                        </div>
                        <div class="row " runat="server" id="row402">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F402"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G402"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H402"></label>
                        </div>
                        <div class="row" runat="server" id="row403">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F403"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G403"></label>
                        </div>
                        <div class="row" runat="server" id="row404">
                            <label class="col-12 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G404" style="font-size: 1.2em"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H404" style="font-size: 1.2em"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="case5">
                    <div class="card-header">
                        Calcul des droits
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row500">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F500"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G500"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H500"></label>
                        </div>
                        <div class="row" runat="server" id="row501">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F501"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G501"></label>
                        </div>
                        <div class="row " runat="server" id="row502">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F502"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G502"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H502"></label>
                        </div>
                        <div class="row" runat="server" id="row503">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F503"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G503"></label>
                        </div>
                        <div class="row" runat="server" id="row504">
                            <label class="col-12 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G504" style="font-size: 1.2em"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H504" style="font-size: 1.2em"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="case6">
                    <div class="card-header">
                        Calcul des droits
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row600">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F600"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G600"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H600"></label>
                        </div>
                        <div class="row" runat="server" id="row601">
                            <label class="col-12 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G601" style="font-size: 1.2em"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H601" style="font-size: 1.2em"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="case7">
                    <div class="card-header">
                        Calcul des droits
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row700">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F700"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G700"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H700"></label>
                        </div>
                        <div class="row" runat="server" id="row701">
                            <label class="col-12 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G701" style="font-size: 1.2em"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H701" style="font-size: 1.2em"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="case8">
                    <div class="card-header">
                        Calcul des droits
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row800">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F800"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G800"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H800"></label>
                        </div>
                        <div class="row" runat="server" id="row801">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F801"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G801"></label>
                        </div>
                        <div class="row" runat="server" id="row802">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F802"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G802"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H802"></label>
                        </div>
                        <div class="row" runat="server" id="row803">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F803"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G803"></label>
                        </div>
                        <div class="row " runat="server" id="row804">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F804"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G804"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H804"></label>
                        </div>
                        <div class="row" runat="server" id="row805">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F805"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G805"></label>
                        </div>
                        <div class="row " runat="server" id="row806">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F806"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G806"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H806"></label>
                        </div>
                        <div class="row" runat="server" id="row807">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F807"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G807"></label>
                        </div>
                        <div class="row " runat="server" id="row808">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F808"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G808"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H808"></label>
                        </div>
                        <div class="row" runat="server" id="row809">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F809"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G809"></label>
                        </div>
                        <div class="row " runat="server" id="row810">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F810"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G810"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H810"></label>
                        </div>
                        <div class="row" runat="server" id="row811">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F811"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G811"></label>
                        </div>
                        <div class="row " runat="server" id="row812">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F812"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G812"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H812"></label>
                        </div>
                        <div class="row" runat="server" id="row813">
                            <label class="col-4 col-sm-3" runat="server" clientidmode="Static" id="F813"></label>
                            <label class="col-8 col-sm-9 text-center" runat="server" clientidmode="Static" id="G813"></label>
                        </div>
                        <div class="row" runat="server" id="row814">
                            <label class="col-12 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G814" style="font-size: 1.2em"></label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H814" style="font-size: 1.2em"></label>
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
    <script type="text/javascript" src="Scripts/DON-DROITS.js"></script>
</asp:Content>
