<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF10.aspx.cs" Inherits="NotaliaOnline.BF10" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #select2-ddl1-container, #select2-ddl2-container, #select2-ddl3-container {
            text-align: center;
        }

        ul#select2-ddl1-results li, ul#select2-ddl2-results li, ul#select2-ddl3-results li {
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
                Liquidation d'un régime communautaire dans le cas d'un divorce
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
                    <div class="col-sm-4 col-12 text-left text-sm-right">
                        <label class="col-form-label">Date envisagée de l'acte :</label>
                    </div>
                    <div class="col-sm-3 col-12">
                        <asp:TextBox CssClass="form-control control-input datePicker" runat="server" ClientIDMode="Static" ID="txtZone01"></asp:TextBox>
                        <i class="form-control-feedback fa fa-calendar"></i>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Cadre d'intervention de la liquidation Partage
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right"></div>
                            <div class=" col-sm-6 col-12 col-sm-offset-4">
                                <asp:DropDownList CssClass="select2" ID="ddl01" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Text="Divorce par consentement mutuel" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Divorce contentieux" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Fiscalité de la liquidation
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right"></div>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk2" CssClass="checkbox-custom col-12 col-sm-8 col-sm-offset-4"
                                Text="L'une des parties bénéficie-t-elle de l'aide juridictionnelle dans le cadre d'un divorce ?" />
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right"></div>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk3" CssClass="checkbox-custom col-12 col-sm-8 col-sm-offset-4"
                                Text="Les frais du partage sont-ils incorporés dans la liquidation ?" />
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Biens immobiliers
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Nombre de biens immobiliers partagés ?</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl1" ClientIDMode="Static" runat="server">
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
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 1</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_1_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier2">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 2</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_2_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier3">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 3</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_3_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier4">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 4</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_4_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier5">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 5</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_5_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier6">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 6</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_6_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier7">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 7</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_7_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier8">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 8</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_8_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier9">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 9</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_9_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divImmobilier10">
                            <label class="col-12 col-sm-6 col-form-label">Bien Immobilier - Article 10</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_10_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
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
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Nombre de biens mobiliers partagés ?</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddl2">
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
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 1</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_1_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier2">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 2</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_2_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier3">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 3</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_3_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier4">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 4</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_4_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier5">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 5</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_5_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier6">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 6</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_6_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier7">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 7</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_7_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier8">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 8</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_8_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier9">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 9</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_9_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divMobilier10">
                            <label class="col-12 col-sm-6 col-form-label">Bien Mobilier - Article 10</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_10_Valeur_PP" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Passif
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Nombre de sommes au passif ?</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddl3">
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
                        <div class="row form-group" id="divPassif1">
                            <label class="col-12 col-sm-6">Passif - Article 1</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_1_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif2">
                            <label class="col-12 col-sm-6">Passif - Article 2</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_2_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif3">
                            <label class="col-12 col-sm-6">Passif - Article 3</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_3_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif4">
                            <label class="col-12 col-sm-6">Passif - Article 4</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_4_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif5">
                            <label class="col-12 col-sm-6">Passif - Article 5</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_5_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif6">
                            <label class="col-12 col-sm-6">Passif - Article 6</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_6_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif7">
                            <label class="col-12 col-sm-6">Passif - Article 7</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_7_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif8">
                            <label class="col-12 col-sm-6">Passif - Article 8</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_8_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif9">
                            <label class="col-12 col-sm-6">Passif - Article 9</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_9_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif10">
                            <label class="col-12 col-sm-6">Passif - Article 10</label>
                            <label class="col-sm-3 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_10_Valeur" CssClass=" form-control numberFormatFR"></asp:TextBox>
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
                <div class="row" style="font-size: 1.5rem">
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
                <div class="sub-card">
                    <div class="card-header">
                        Emoluments du notaire - C.com. Art. A 444-121
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="div112">
                            <label runat="server" class="col-4 col-sm-3" id="lblF112"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblG112"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblH112"></label>
                        </div>
                        <div class="row" runat="server" id="div113">
                            <label runat="server" class="col-4 col-sm-3" id="lblF113"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblG113"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblH113"></label>
                        </div>
                        <div class="row" runat="server" id="div114">
                            <label runat="server" class="col-4 col-sm-3" id="lblF114"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblG114"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblH114"></label>
                        </div>
                        <div class="row" runat="server" id="div115">
                            <label runat="server" class="col-4 col-sm-3" id="lblF115"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblG115"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblH115"></label>
                        </div>
                        <div class="row" runat="server" id="div117">
                            <label class="col-6 col-sm-4">Total :</label>
                            <label runat="server" id="lblG117" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="div118">
                            <label class="col-6 col-sm-9">Total des émoluments réglementés :</label>
                            <label runat="server" id="lblH118" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="div120">
                            <label class="col-6 col-sm-9">Emolument minimum :</label>
                            <label runat="server" id="lblH120" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="row122">
                            <label class="col-12 col-sm-9">Dépôt Art.229-1 du Code civil - C.com A444-173-1 Tableau 5-222 :</label>
                            <label runat="server" id="H122" class="col-12 col-sm-3"></label>
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
                            <label runat="server" id="H127" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Emoluments de formalités (HT) :</label>
                            <label runat="server" id="H128" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">Total HT :</label>
                            <label runat="server" id="H129" class="col-sm-3 col-6"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">TVA :</label>
                            <label runat="server" id="H130" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total TTC :</label>
                            <label runat="server" id="H131" class="col-6 col-sm-3"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Débours
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-sm-9 col-6">Débours :</label>
                            <label runat="server" id="H135" class="col-sm-3 col-6"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div runat="server" id="row138">
                            <div class="row">
                                <label class="col-12 text-left fa-2x">Taxe de publicité foncière : </label>
                                <div runat="server" id="row139" class="col-12 text-center">
                                    <label class="text-danger">Exonération (CGI art. 1090 A-I)</label>
                                </div>
                            </div>
                            <div class="row" runat="server" id="row140">
                                <label runat="server" class="col-4 col-sm-3" id="lblF140"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" class="col-6 col-sm-3" id="lblG140"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" class="col-6 col-sm-3" id="lblH140"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row142">
                            <label class="col-12 text-left" style="font-size: 1.4em">CSI (art. 879 du CGI) : </label>
                            <label runat="server" class="col-4 col-sm-3" id="lblF143"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblG143"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="lblH143"></label>
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
    <script type="text/javascript" src="Scripts/BF10.js"></script>
</asp:Content>
