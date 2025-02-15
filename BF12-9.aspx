<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF12-9.aspx.cs" Inherits="NotaliaOnline.BF12_9" ValidateRequest="false" %>

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
                Licitation faisant cesser une indivision de droit commun
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
                        Biens immobiliers
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Nombre de biens immobiliers partagés ?</label>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddl1">
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier1">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 1</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_1_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_1_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-12 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_1_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_1">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_1_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier2">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 2</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_2_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_2_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_2_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_2">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_2_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier3">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 3</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_3_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_3_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_3_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_3">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_3_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier4">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 4</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_4_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_4_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_4_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_4">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_4_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier5">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 5</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_5_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_5_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_5_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_5">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_5_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier6">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 6</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_6_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_6_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_6_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_6">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_6_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier7">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 7</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_7_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_7_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_7_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_7">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_7_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier8">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 8</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_8_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_8_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_8_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_8">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_8_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier9">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 9</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_9_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_9_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_9_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_9">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_9_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divImmobilier10">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label ">Bien Immobilier - Article 10</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_10_partage" CssClass="col-sm-3 col-12 checkbox" Text="Exonération droit de partage" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkImmobilier_Article_10_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_10_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlImmobilier_Article_10">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtImmobilier_Article_10_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
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
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Nombre de biens mobiliers partagés ?</label>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddl2">
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier1">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 1</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_1_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_1_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_1">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_1_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier2">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 2</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_2_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_2_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_2">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_2_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier3">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 3</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_3_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_3_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_3">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_3_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier4">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 4</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_4_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_4_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_4">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_4_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier5">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 5</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_5_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_5_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_5">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_5_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier6">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 6</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_6_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_6_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_6">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_6_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier7">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 7</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_7_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_7_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_7">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_7_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier8">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 8</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_8_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_8_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_8">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_8_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier9">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 9</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_9_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_9_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_9">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_9_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divMobilier10">
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label ">Bien Mobilier - Article 10</label>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkMobilier_Article_10_usufruit" CssClass="col-sm-2 col-12 checkbox" Text="Grevé d'usufruit" />
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur PP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_10_Valeur_PP" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Pourcentage d'usufruit (%) :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddlMobilier_Article_10">
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 col-form-label text-left text-sm-right">Valeur NP :</label>
                                <div class="col-sm-2 col-12">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMobilier_Article_10_Valeur_NP" CssClass=" form-control control-input numberFormatFR"
                                        ReadOnly="True"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
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
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Nombres de sommes au passif ?</label>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" runat="server" ClientIDMode="Static" ID="ddl3">
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif1">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 1</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_1_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif2">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 2</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_2_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif3">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 3</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_3_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif4">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 4</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_4_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif5">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 5</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_5_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif6">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 6</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_6_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif7">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 7</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_7_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif8">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 8</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_8_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif9">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 9</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_9_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group" id="divPassif10">
                            <label class="col-sm-6 col-12 col-form-label ">Passif - Article 10</label>
                            <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">Valeur :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPassif_Article_10_Valeur" CssClass=" form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        <label>Licitation mettant fin a l'indivision - droit commun</label>
                    </div>
                    <div class="card-body">
                        <div class="row form-group text-center">
                            <label class="col-12" style="font-size: 1.2em">Données économiques des biens licités</label>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Valeur des biens immobiliers : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone864" CssClass="form-control numberFormatFR" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Valeur des biens mobiliers : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone866" CssClass="form-control numberFormatFR" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-12" style="font-size: 1.2em">Prix de cession</label>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Prix (en ce compris la prise en charge du passif) :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone871" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Afférent aux biens immobiliers :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFormula01" CssClass="form-control numberFormatFR" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Afférent aux biens mobiliers :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFormula02" CssClass="form-control numberFormatFR" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Droit de mutation sur la soulte
                    </div>
                    <div class="card-body">
                        <div class="row form-group text-center">
                            <label class="col-12 fa-2x">Augmentation de la part départementale ( LF 2014 )</label>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk904" CssClass="checkbox-custom col-sm-8 col-sm-offset-4 col-12" Checked="True"
                                Text="Le bien indivis est-il situé dans un département ayant voté la surtaxe ?" />
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Indiquez le montant de la taxe départementale votée :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone906" CssClass="form-control numberFormatFR" Text="4,50"></asp:TextBox>
                                <i class="form-control-feedback fa fa-percent"></i>
                            </div>
                        </div>
                        <div class="row form-group text-center" id="divTextRed01">
                            <label class="col-12 text-danger">Le montant de la taxe départementale ne doit pas dépasser 4,5 %</label>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-12 fa-2x">Régime fiscal</label>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-12" id="lblTextZone03Msg"></label>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-sm-3"></label>
                            <label class="col-sm-3 text-left">Base de perception</label>
                            <label class="col-sm-3">Taux</label>
                            <label class="col-sm-3">Montant des droits</label>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-3 col-12 col-form-label text-sm-right">Immobilier - Droit commun : </label>
                            <div class="col-sm-3 col-8">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone916" CssClass="form-control numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                            <label class="col-sm-3 col-4 col-form-label text-right"><span id="lblTaux1"></span></label>
                            <label class="col-sm-3 col-12 col-form-label text-right"><span id="lblTextZone05Montant"></span></label>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-3 col-12 col-form-label text-sm-right">Immobilier - Régime de la TVA : </label>
                            <div class="col-sm-3 col-8">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone918" CssClass="form-control numberFormatFR" Text="0,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                            <label class="col-sm-3 col-4 col-form-label text-right"><span id="lblTaux2">0,71498 %</span></label>
                            <label class="col-sm-3 col-12 col-form-label text-right"><span id="lblTextZone06Montant"></span></label>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-3 col-12 col-form-label text-sm-right">Autre cas : </label>
                            <div class="col-sm-3 col-8">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone920Base" CssClass="form-control numberFormatFR" Enabled="False" Text="0,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                            <div class="col-sm-3 col-4">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone920" CssClass="form-control numberFormatFR text-center" Text="0"></asp:TextBox>
                                <i class="form-control-feedback fa fa-percent"></i>
                            </div>
                            <label class="col-sm-3 col-12 col-form-label text-right"><span id="lblTextZone07Montant"></span></label>
                        </div>
                        <div class="row form-group text-center" id="divTextRed02">
                            <label class="col-12" style="color: red" id="lblTextZone02050607Msg"></label>
                        </div>
                        <div class="row form-group text-center" id="divText03">
                            <label class="col-12">La ventilation des bases ne correspond pas à la somme totale.</label>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-12">Attention, la soulte et la prise en charge du passif sont représentatives des biens partagés ou licités et doit permettre l'application du tarif propre à chaque bien (CGI, art. 747).</label>
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
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control control-input numberFormatFR"
                                    Text="800,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Débours :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDébours" CssClass="form-control control-input numberFormatFR"
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
                <div class="sub-card" runat="server" id="row141">
                    <div class="card-header">
                        Emoluments du notaire - C.com. Art. A 444-87a
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="row142">
                            <label runat="server" class="col-4 col-sm-3" id="F142"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="G142"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="H142"></label>
                        </div>
                        <div class="row" runat="server" id="row143">
                            <label runat="server" class="col-4 col-sm-3" id="F143"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="G143"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="H143"></label>
                        </div>
                        <div class="row" runat="server" id="row144">
                            <label runat="server" class="col-4 col-sm-3" id="F144"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="G144"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="H144"></label>
                        </div>
                        <div class="row" runat="server" id="row145">
                            <label runat="server" class="col-4 col-sm-3" id="F145"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" class="col-6 col-sm-3" id="G145"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" class="col-6 col-sm-3" id="H145"></label>
                        </div>
                        <div class="row" runat="server" id="row147">
                            <label class="col-6 col-sm-4">Total :</label>
                            <label runat="server" id="G147" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="row148">
                            <label class="col-6 col-sm-9">TOTAL Hors T.V.A :</label>
                            <label runat="server" id="H148" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="row150">
                            <label class="col-6 col-sm-9">Emolument minimum :</label>
                            <label runat="server" id="H150" class="col-6 col-sm-3"></label>
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
    <script type="text/javascript" src="Scripts/BF12-9.js"></script>
</asp:Content>
