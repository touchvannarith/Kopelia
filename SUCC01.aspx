﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="SUCC01.aspx.cs" Inherits="NotaliaOnline.SUCC01" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #select2-ddl1-container {
            text-align: center;
        }
        ul#select2-ddl1-results li {
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
                Règlement successoral ab intestat
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
                        Ouverture de la succession
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Date du décès :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control datePicker" runat="server" ClientIDMode="Static" ID="txtZone01"></asp:TextBox>
                                <i class="form-control-feedback fa fa-calendar"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Quels sont les actes que vous voulez taxer ?
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk1" CssClass="checkbox-custom col col-sm-8"
                                Text="Notoriété" />
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk2" CssClass="checkbox-custom col col-sm-8"
                                Text="Inventaire" />
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk3" CssClass="checkbox-custom col col-sm-8"
                                Text="Attestation immobilière" />
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk4" CssClass="checkbox-custom col col-sm-8"
                                Text="Déclaration de succession" />
                        </div>
                    </div>
                </div>
                <div class="sub-card" id="divDetermination_des_biens">
                    <div class="card-header">
                        Determination des biens
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre d'articles ?</label>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl1" CssClass="select2">
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
                        <div id="divArticle1">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 1</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle1">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value1"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle2">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 2</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle2">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value2"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle3">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 3</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle3">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value3"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle4">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 4</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle4">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value4"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle5">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 5</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle5">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value5"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle6">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 6</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle6">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value6"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle7">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 7</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle7">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value7"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle8">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 8</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle8">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value8"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle9">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 9</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle9">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value9"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div id="divArticle10">
                            <div class="row form-group">
                                <label class="col-sm-2 col-form-label">Article 10</label>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Type de bien :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlArticle10">
                                        <asp:ListItem Value="1">Immobilier</asp:ListItem>
                                        <asp:ListItem Value="2">Mobilier</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur :</label>
                                <div class="col-sm-3 col">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="value10"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
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
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Emolument de formalités HT :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Débours :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDébours" CssClass="form-control control-input numberFormatFR"></asp:TextBox>
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
                    <label runat="server" id="F104" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Débours :</label>
                    <label runat="server" id="F103" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Trésor public :</label>
                    <label runat="server" id="F102" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6 fa-2x">Montant des frais :</label>
                    <label runat="server" id="F106" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Total des droits et frais
            </div>
            <div class="card-body text-right">
                <div class="sub-card" runat="server" id="row111">
                    <div class="card-header">
                        Emoluments sur l'attestation immobilière - C.com. Art. A 444-59
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row113">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F113"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G113"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H113"></label>
                        </div>
                        <div class="row" runat="server" id="row114">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F114"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G114"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H114"></label>
                        </div>
                        <div class="row" runat="server" id="row115">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F115"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G115"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H115"></label>
                        </div>
                        <div class="row" runat="server" id="row116">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F116"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G116"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H116"></label>
                        </div>
                        <div class="row" runat="server" id="row118">
                            <label class="col-6 col-sm-3">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G118"></label>
                        </div>
                        <div class="row" runat="server" id="row119">
                            <label class="col-6 col-sm-8">Total Hors TVA : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H119"></label>
                        </div>
                        <div class="row" runat="server" id="row121">
                            <label class="col-6 col-sm-8">Emolument minimum : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H121"></label>
                        </div>
                        <div class="row text-center" runat="server" id="row123">
                            <label class="col-12">Dispense de l'attestation immobilière.</label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="row128">
                    <div class="card-header">
                        Emoluments sur la déclaration de succession - C.com. Art. A 444-63
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row130">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F130"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G130"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H130"></label>
                        </div>
                        <div class="row" runat="server" id="row131">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F131"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G131"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H131"></label>
                        </div>
                        <div class="row" runat="server" id="row132">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F132"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G132"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H132"></label>
                        </div>
                        <div class="row" runat="server" id="row133">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F133"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G133"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H133"></label>
                        </div>
                        <div class="row" runat="server" id="row135">
                            <label class="col-6 col-sm-3">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G135"></label>
                        </div>
                        <div class="row" runat="server" id="row136">
                            <label class="col-6 col-sm-8">Total Hors TVA : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H136"></label>
                        </div>
                        <div class="row text-center" runat="server" id="row140">
                            <label class="col-12">Dispense de dépôt de la déclaration de succession.</label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="row144">
                    <div class="card-header">
                        Emoluments du notaire pour l'acte de notoriété - C.com. Art. A 444-66
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <label class="col-6 col-sm-9">Emolument fixe :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H146"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="row150">
                    <div class="card-header">
                        Emoluments du notaire pour l'acte d'inventaire - C.com. Art. A 444-155
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <label class="col-6 col-sm-9">Emolument fixe :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H152"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Récapitulatif et calcul de la TVA
                    </div>
                    <div class="card-body">
                        <div class="row" runat="server" id="row158">
                            <label class="col-6 col-sm-9">Emoluments sur l'attestation immobilière :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H158"></label>
                        </div>
                        <div class="row" runat="server" id="row159">
                            <label class="col-6 col-sm-9">Emoluments sur la déclaration de succession :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H159"></label>
                        </div>
                        <div class="row" runat="server" id="row160">
                            <label class="col-6 col-sm-9">Emoluments du notaire pour la notoriété :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H160"></label>
                        </div>
                        <div class="row" runat="server" id="row161">
                            <label class="col-6 col-sm-9">Emoluments du notaire pour l'inventaire :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H161"></label>
                        </div>
                        <div class="row" runat="server" id="row162">
                            <label class="col-6 col-sm-9">Emoluments de formalités (HT) :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H162"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total HT des émoluments :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H166"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">TVA :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H167"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-9">Total TTC :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H168"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Débours
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <label class="col-6 col-sm-9">Débours :</label>
                            <label class="col-6 col-sm-3" runat="server" id="H172"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body">
                        <div runat="server" id="row178">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Notoriété :</label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-9">Enregistrement :</label>
                                <label class="col-6 col-sm-3" runat="server" id="H180"></label>
                            </div>
                            <div class="row text-center">
                                <label class="col-12">
                                    Acte dispensé de la formalité (CGI art 846 bis)<br />
                                    Paiement du droit sur état
                                </label>
                            </div>
                        </div>
                        <div runat="server" id="row182">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Inventaire :</label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-9">Enregistrement :</label>
                                <label class="col-6 col-sm-3" runat="server" id="H184"></label>
                            </div>
                            <div class="row text-center">
                                <label class="col-12">
                                    Acte dispensé de la formalité (CGI art 635 1)<br />
                                    Paiement du droit sur état
                                </label>
                            </div>
                        </div>
                        <div class="row text-left" runat="server" id="row186">
                            <label class="col-12 col-sm-9 fa-2x">Taxe fixe d'enregistrement :</label>
                            <label class="col-12 col-sm-3 text-right" runat="server" id="H186"></label>
                        </div>
                        <div runat="server" id="row188">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">CSI (art. 879 du CGI) :</label>
                            </div>
                            <div class="row" runat="server" id="row189">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F189"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G189"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H189"></label>
                            </div>
                            <div class="row" runat="server" id="row190">
                                <label class="col-12 col-sm-9">Pour la CSI, il a été pris le minimum de perception :</label>
                                <label class="col-12 col-sm-3">15.00 €</label>
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
    <script type="text/javascript" src="Scripts/SUCC01.js"></script>
</asp:Content>
