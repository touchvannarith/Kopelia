<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF17.aspx.cs" Inherits="NotaliaOnline.BF17" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #select2-ddl4-container, #select2-ddl5-container {
            text-align: center;
        }

        ul#select2-ddl4-results li, ul#select2-ddl5-results li {
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
                Le changement de régime matrimonial
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
                        Régime matrimonial d'origine
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Régime matrimonial d'origine :</label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl1" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1">Régime communautaire</asp:ListItem>
                                    <asp:ListItem Value="2">Régime séparatiste</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Changement - Adoption du nouveau régime
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Changement - Adoption du nouveau régime :</label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl2" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1">Régime communautaire</asp:ListItem>
                                    <asp:ListItem Value="2">Régime séparatiste</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Cas traité
                    </div>
                    <div class="card-body">
                        <div class="row form-group text-center">
                            <label class="col-12" id="lblCas_traité_message"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Situation familiale
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk1" CssClass="checkbox-custom col-sm-8 col-sm-offset-4 col-12"
                                Text="Cette situation entraîne-t-elle une homologation ?" />
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Etablissement de l'état liquidatif sans partage
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Etablissement de l'état liquidatif sans partage :</label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl3" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1">Liquidation estimée non nécessaire</asp:ListItem>
                                    <asp:ListItem Value="2">Liquidation contenue dans l'acte</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Valeur brute des biens déclarés : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone01"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Mutation des biens et droits entre les époux
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk2" CssClass="checkbox-custom col-sm-8 col-sm-offset-4 col-12"
                                Text="Y-a-t'il un transfert de biens et droits ? " />
                        </div>
                    </div>
                </div>
                <div id="mainDETERMINATION_DES_BIENS">
                    <div class="sub-card">
                        <div class="card-header">
                            Biens immobiliers transférés
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de biens immobiliers ?</label>
                                <div class="col-md-3 col-sm-6 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddl4">
                                        <asp:ListItem Value="0">0</asp:ListItem>
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
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier1">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 1</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_1_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_1_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_1_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier2">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 2</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_2_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_2_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_2_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier3">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 3</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_3_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_3_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_3_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier4">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 4</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_4_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_4_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_4_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier5">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 5</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_5_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_5_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_5_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier6">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 6</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_6_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_6_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_6_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier7">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 7</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_7_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_7_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_7_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier8">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 8</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_8_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_8_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_8_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier9">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 9</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_9_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_9_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_9_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divImmobilier10">
                                <div class="col-sm-12"><span>Bien Immobilier - Article 10</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_10_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_10_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_10_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="sub-card">
                        <div class="card-header">
                            Biens mobiliers
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de biens mobiliers ?</label>
                                <div class="col-md-3 col-sm-6  col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddl5">
                                        <asp:ListItem Value="0">0</asp:ListItem>
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
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier1">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 1</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_1_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_1_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_1_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier2">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 2</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_2_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_2_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_2_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier3">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 3</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_3_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_3_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_3_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier4">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 4</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_4_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_4_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_4_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier5">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 5</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_5_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_5_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_5_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier6">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 6</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_6_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_6_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_6_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier7">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 7</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_7_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_7_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_7_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier8">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 8</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_8_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_8_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_8_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier9">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 9</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_9_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_9_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_9_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group" id="divMobilier10">
                                <div class="col-sm-12"><span>Bien Mobilier - Article 10</span></div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit actuel de l'apporteur :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_10_Percent_1" CssClass="form-control numberFormatFR" Text="100">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Droit après changement :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_10_Percent_2" CssClass="form-control numberFormatFR" Text="50">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-percent"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row form-group">
                                        <label class=" col-sm-6 col-form-label text-left text-sm-right">Valeur de la PP :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_10_Valeur" CssClass="form-control numberFormatFR">
                                            </asp:TextBox>
                                            <i class="form-control-feedback fa fa-euro"></i>
                                        </div>
                                    </div>
                                </div>
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
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Débours :</label>
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
    <div runat="server" id="divResult" clientidmode="Static">
        <div class="card">
            <div class="card-header">
                Total des droits et frais
            </div>
            <div class="card-body text-right">
                <div class="row">
                    <div class="col-sm-6  col-6">
                        <label>Emoluments du notaire :</label>
                    </div>
                    <div class="col-sm-3 col-6">
                        <label runat="server" id="lblEmoluments_HT_du_notaire" clientidmode="Static"></label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6  col-6">
                        <label>Débours :</label>
                    </div>
                    <div class="col-sm-3  col-6">
                        <label runat="server" id="lblDébours" clientidmode="Static"></label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-6">
                        <label>Trésor public :</label>
                    </div>
                    <div class="col-sm-3 col-6">
                        <label runat="server" id="lblTrésor" clientidmode="Static"></label>
                    </div>
                </div>
                <div class="row " style="font-size: 1.5rem">
                    <div class="col-sm-6 col-6 ">
                        <label>Montant des frais :</label>
                    </div>
                    <div class="col-sm-3 col-6">
                        <label runat="server" id="lblMontant_des_frais" clientidmode="Static"></label>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="subAttestation_notariée">
                    <div class="card-header">
                        Attestation notariée
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-6 col-6 ">
                                <label>Trésor :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" id="lblTrésor2"></label>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col-sm-6 col-6 ">
                                <label>Emoluments HT du notaire :</label>
                            </div>
                            <div class="col-sm-3 col-6">
                                <label runat="server" id="lblEmoluments_HT_du_notaire2"></label>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-6 ">Montant des frais :</label>
                            <div class="col-sm-3 col-6">
                                <label runat="server" id="lblMontant_des_frais2"></label>
                            </div>
                        </div>
                    </div>
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
                        Emoluments du Notaire (état liquidatif) - C.com. Art. A 444-82
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="div1">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow11"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow12"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow13"></label>
                        </div>
                        <div class="row" runat="server" id="div2">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow21"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow22"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow23"></label>
                        </div>
                        <div class="row" runat="server" id="div3">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow31"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow32"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow33"></label>
                        </div>
                        <div class="row" runat="server" id="div4">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow41"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow42"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow43"></label>
                        </div>
                        <div class="row" runat="server" id="divTotal1">
                            <label class="col-6 col-sm-4">Total :</label>
                            <label runat="server" id="lblTotal" class="col-6 col-sm-3"></label>
                            <label runat="server" id="lblTotal1" class="col-12 col-sm-5"></label>
                        </div>
                        <div class="row text-center" runat="server" id="divWarning1">
                            <label class="col-12 text-danger">Prise en compte de l'émolument minimum.</label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total HT :</label>
                            <label runat="server" id="H129" class="col-6 col-sm-3"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="divSub_Emoluments_du_notaire">
                    <div class="card-header">
                        Emoluments du notaire (mutation de biens - disposition indépendante)
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="div5">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow51"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow52"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow53"></label>
                        </div>
                        <div class="row" runat="server" id="div6">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow61"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow62"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow63"></label>
                        </div>
                        <div class="row" runat="server" id="div7">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow71"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow72"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow73"></label>
                        </div>
                        <div class="row" runat="server" id="div8">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow81"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow82"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow83"></label>
                        </div>
                        <div class="row" runat="server" id="divTotal2">
                            <label class="col-6 col-sm-4">Total :</label>
                            <label runat="server" id="lblTotal2" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row text-center" runat="server" id="divWarning2">
                            <label class="col-sm-12 text-danger">Prise en compte de l'émolument minimum.</label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total HT :</label>
                            <label runat="server" id="H143" class="col-6 col-sm-3"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Récapitulatif et calcul de la TVA
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="row148">
                            <label class="col-6 col-sm-9">Emoluments du notaire - Etat liquidatif :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H148"></label>
                        </div>
                        <div class="row" runat="server" id="row149">
                            <label class="col-6 col-sm-9">Emoluments du notaire - Acte de disposition indépendante :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H149"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Emoluments de formalités (HT) :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H150"></label>
                        </div>
                        <div class="row" runat="server" id="row151">
                            <label class="col-6 col-sm-9">Total HT :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H151"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">TVA :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H152"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total TTC :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H153"></label>
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
                                <label runat="server" id="H157"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="divSub_Fiscalité">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div class="row text-center">
                            <label class="col-12 text-left fa-2x">Droit fixe (Art. 680) :</label>
                            <label class="col-12">Exonération - Enregistrement gratuit.</label>
                        </div>
                        <div class="row" runat="server" id="div9">
                            <label class="col-12 text-left fa-2x">Taxe de publicité :</label>
                            <label runat="server" class="col-4 col-sm-3" id="lblRow91"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow92"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow93"></label>
                        </div>
                        <div class="row" runat="server" id="divWarning3">
                            <label class="col-12 text-danger text-center">Prise en compte du minimum de perception.</label>
                        </div>
                        <div class="row" runat="server" id="div10">
                            <label class="col-12 text-left fa-2x">CSI (art. 879 du CGI) :</label>
                            <label runat="server" class="col-4 col-sm-3" id="lblRow101"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow102"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow103"></label>
                        </div>
                        <div class="row" runat="server" id="divWarning4">
                            <label class="col-12 text-danger text-center">Prise en compte du minimum de perception.</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card" runat="server" id="divMain_DEPOT_DES_PIECES__">
            <div class="card-header">
                Dépot des pièces du jugement d'homologation
            </div>
            <div class="card-body text-right">
                <div class="row">
                    <label class="col-6 col-sm-9">Emoluments fixes :</label>
                    <label class="col-6 col-sm-3" runat="server" id="lblEmoluments_fixes"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-9">Droit fixe (Art. 680) :</label>
                    <label class="col-6 col-sm-3" runat="server" id="lblDroit_fixe"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-9">Total :</label>
                    <label class="col-6 col-sm-3" runat="server" id="lblTotal3"></label>
                </div>
            </div>
        </div>
        <div class="card" runat="server" id="divMain_ATTESTATION_NOTARIEE_ULTERIEURE">
            <div class="card-header">
                Attestation notariée ultérieure
            </div>
            <div class="card-body">
                <div class="sub-card" runat="server" id="divSub_Emoluments_du_notaire2">
                    <div class="card-header">
                        Emoluments du Notaire - C.com. Art. A 444-59
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="div11">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow111"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow112"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow113"></label>
                        </div>
                        <div class="row" runat="server" id="div12">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow121"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow122"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow123"></label>
                        </div>
                        <div class="row" runat="server" id="div13">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow131"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow132"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow133"></label>
                        </div>
                        <div class="row" runat="server" id="div14">
                            <label runat="server" class="col-4 col-sm-3" id="lblRow141"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow142"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow143"></label>
                        </div>
                        <div class="row" runat="server" id="divTotal4">
                            <label class="col-6 col-sm-4">Total :</label>
                            <label class="col-6 col-sm-3" runat="server" id="lblTotal4"></label>
                        </div>
                        <div class="row text-center" runat="server" id="divWarning5">
                            <label class="col-12 text-danger">Prise en compte de l'émolument minimum.</label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Emoluments réglementés :</label>
                            <label runat="server" id="lblTVA3" class="col-6 col-sm-3"></label>
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
                            <label runat="server" id="H189" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">TVA :</label>
                            <label runat="server" id="H190" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total TTC :</label>
                            <label runat="server" id="H191" class="col-6 col-sm-3"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="divSub_Fiscalité2">
                    <div class="card-header">
                        Fiscalité
                    </div>
                    <div class="card-body text-right">
                        <div class="row text-center">
                            <label class="col-12 text-left fa-2x">Droit fixe (Art. 847) :</label>
                            <label class="col-12">Exonération - Enregistrement gratuit.</label>
                        </div>
                        <div class="row" runat="server" id="div15">
                            <label class="col-12 text-left fa-2x">Taxe de publicité :</label>
                            <label runat="server" class="col-4 col-sm-3" id="lblRow151"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow152"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow153"></label>
                        </div>
                        <div class="row" runat="server" id="divWarning6">
                            <label class="col-12 text-danger text-center">Prise en compte du minimum de perception.</label>
                        </div>
                        <div class="row" runat="server" id="div16">
                            <label class="col-12 text-left fa-2x">CSI (art. 879 du CGI) :</label>
                            <label runat="server" class="col-4 col-sm-3" id="lblRow161"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow162"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblRow163"></label>
                        </div>
                        <div class="row" runat="server" id="divWarning7">
                            <label class="col-12 text-danger text-center">Prise en compte du minimum de perception.</label>
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
    <script type="text/javascript" src="Scripts/BF17.js"></script>
</asp:Content>
