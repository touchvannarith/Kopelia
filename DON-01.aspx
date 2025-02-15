<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="DON-01.aspx.cs" Inherits="NotaliaOnline.DON_01" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdUtilisation_du_futur_tarif" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="postback" Value="" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdJsonDeterminationDesDonataires" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdJsonDeterminationDesBiens" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdJsonRappelDeDonationsAnterieures" />

    <div class="text-center">
        <label runat="server" clientidmode="static" id="lblShowProcessTime"></label>
    </div>
    <div class="row text-center">
        <div class="col mb-2">
            <button type="button" id="btnInputForm1" class="btn btn-default btnInputForm w-100 text-nowrap">Détermination des donataires</button>
        </div>
        <div class="col mb-2">
            <button type="button" id="btnInputForm2" class="btn btn-default btnInputForm w-100 text-nowrap">Détermination des biens</button>
        </div>
        <div class="col mb-2">
            <button type="button" id="btnInputForm3" class="btn btn-default btnInputForm w-100 text-nowrap">Rappel des donations antérieures</button>
        </div>
    </div>
    <div class="row text-center">
        <div class="col mb-2">
            <%--<asp:Button CssClass="btn btn-default btn-calculate w-100" runat="server" ClientIDMode="Static" ID="btnSynthese" Text="Synthèse" OnClick="btnSynthese_Click" />--%>
            <button type="button" class="btn btn-default btn-calculate w-100 text-nowrap" id="btnSynthese">Synthèse</button>
        </div>
        <div class="col mb-2">
            <%--<asp:Button CssClass="btn btn-default btn-calculate w-100" runat="server" ClientIDMode="Static" ID="btnSyntheseTaxePere" Text="Synthèse taxe Père" OnClick="btnSyntheseTaxePere_Click" />--%>
            <button type="button" class="btn btn-default btn-calculate w-100 text-nowrap" id="btnSyntheseTaxePere">Synthèse taxe Père</button>
        </div>
        <div class="col mb-2">
            <%--<asp:Button CssClass="btn btn-default btn-calculate w-100" runat="server" ClientIDMode="Static" ID="btnSyntheseTaxeMere" Text="Synthèse taxe Mère" OnClick="btnSyntheseTaxeMere_Click" />--%>
            <button type="button" class="btn btn-default btn-calculate w-100 text-nowrap" id="btnSyntheseTaxeMere">Synthèse taxe Mère</button>
        </div>
        <div class="col mb-2">
            <%--<asp:Button CssClass="btn btn-default btn-calculate w-100" runat="server" ClientIDMode="Static" ID="btnTaxeDonation" Text="Taxation" OnClick="btnTaxeDonation_Click" />--%>
            <button type="button" class="btn btn-default btn-calculate w-100 text-nowrap" id="btnTaxeDonation">Taxation</button>
        </div>
        <div class="col mb-2">
            <button type="button" class="btn btn-default btn-calculate w-100 text-nowrap" id="btnSynthaseDonation">Synthèse des Donations antérieures</button>
        </div>
    </div>

    <div runat="server" clientidmode="Static" id="div_input_form_1">
        <div class="card">
            <div class="card-header">
                <button type="button" class="btn"><i class="fa fa-plus"></i></button>
                DON01
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
                Caractéristiques de la Donation
            </div>
            <div class="card-body">
                <div class="sub-card">
                    <div class="card-header">
                        Date de la donation
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Date de la donation :</label>
                            <div class="col-12 col-sm-8">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone00" CssClass="form-control datePicker"></asp:TextBox>
                                <i class="form-control-feedback fa fa-calendar"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Donateur(s)
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donateurs :</label>
                            <div class="col-12 col-sm-8">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl1" CssClass="form-control select2">
                                    <asp:ListItem Value="1" Text="Donation par le Père"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Donation par la Mère"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Donation par les deux ascendants"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Date de naissance du Donateur :</label>
                            <div class="col-9 col-sm-6">
                                <asp:TextBox CssClass="form-control datePicker" runat="server" ClientIDMode="Static" ID="txtZone01a"></asp:TextBox>
                                <i class="form-control-feedback fa fa-calendar"></i>
                            </div>
                            <div class="col-3 col-sm-2">
                                <label class="col-form-label" id="lblYear1"></label>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Date de naissance de la Donatrice :</label>
                            <div class="col-9 col-sm-6">
                                <asp:TextBox CssClass="form-control datePicker" runat="server" ClientIDMode="Static" ID="txtZone01b"></asp:TextBox>
                                <i class="form-control-feedback fa fa-calendar"></i>
                            </div>
                            <div class="col-3 col-sm-2">
                                <label class="col-form-label" id="lblYear2"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Donataires de 1er degré
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donataires :</label>
                            <div class="col-12 col-sm-8">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl3" CssClass="form-control select2">
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
                        <div id="div_donataire_1">
                            <label>1er Donataire (Nom/Prénom) :</label>
                            <div class="row form-group">
                                <div class="col-12 col-sm-4">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_1"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_1" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_1" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_1">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_1">
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
                        </div>
                        <div id="div_donataire_2">
                            <label>2ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_2"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_2" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_2" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_2">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_2">
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
                        </div>
                        <div id="div_donataire_3">
                            <label>3ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_3"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_3" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_3" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_3">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_3">
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
                        </div>
                        <div id="div_donataire_4">
                            <label>4ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_4"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_4" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_4" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_4">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_4">
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
                        </div>
                        <div id="div_donataire_5">
                            <label>5ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_5"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_5" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_5" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_5">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_5">
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
                        </div>
                        <div id="div_donataire_6">
                            <label>6ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_6"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_6" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_6" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_6">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_6">
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
                        </div>
                        <div id="div_donataire_7">
                            <label>7ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_7"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_7" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_7" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_7">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_7">
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
                        </div>
                        <div id="div_donataire_8">
                            <label>8ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_8"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_8" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_8" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_8">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_8">
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
                        </div>
                        <div id="div_donataire_9">
                            <label>9ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_9"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_9" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_9" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_9">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_9">
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
                        </div>
                        <div id="div_donataire_10">
                            <label>10ème Donataire (Nom/Prénom) : </label>
                            <div class="row form-group">
                                <div class="col-sm-4 col-12">
                                    <asp:TextBox CssClass="form-control txtZone01_donataire" runat="server" ClientIDMode="Static" ID="txtZone01_donataire_10"></asp:TextBox>
                                </div>
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb2_donataire_10" CssClass="col-sm-4 col-12 checkbox cb2_donataire" Text="Majeur / émancipé" />
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="cb1_donataire_10" CssClass="col-sm-4 col-12 checkbox cb1_donataire" Text="Abattement infirmité" />
                            </div>
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) du Père :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl4_donataire" runat="server" ClientIDMode="Static" ID="ddl4_donataire_10">
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
                            <div class="row form-group">
                                <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Nombre de donation(s) reçue(s) de la Mère :</label>
                                <div class="col-sm-8 col-12">
                                    <asp:DropDownList CssClass="form-control select2 ddl5_donataire" runat="server" ClientIDMode="Static" ID="ddl5_donataire_10">
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
                        </div>
                    </div>
                </div>
                <div class="sub-card" id="sub_nature_de_la_donation">
                    <div class="card-header">
                        Nature de la donation
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Type de donation :</label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="form-control select2" runat="server" ClientIDMode="Static" ID="ddl6">
                                    <asp:ListItem Value="Donation simple" Text="Donation simple"></asp:ListItem>
                                    <asp:ListItem Value="Donation partage" Text="Donation partage"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Quotités données :</label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="form-control select2" runat="server" ClientIDMode="Static" ID="ddl7">
                                    <asp:ListItem Value="Egalitaires" Text="Egalitaires"></asp:ListItem>
                                    <asp:ListItem Value="Inégalitaires" Text="Inégalitaires"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divInégalitaires">
                            <div class="row form-group">
                                <label class="col-sm-3 col-12"></label>
                                <label class="col-sm-3 col-12">Quotité par défaut</label>
                                <label class="col-sm-3 col-12" id="lblZone02_donataire_title">Quotité choisie par le Donateur</label>
                                <label class="col-sm-3 col-12" id="lblZone03_donataire_title">Quotité choisie par la Donatrice</label>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">1er donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox1" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_1"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_1"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">2ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox2" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_2"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_2"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">3ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox3" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_3"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_3"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">4ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox4" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_4"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_4"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">5ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox5" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_5"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_5"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">6ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox6" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_6"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_6"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">7ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox7" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_7"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_7"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">8ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox8" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_8"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_8"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">9ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox9" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_9"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_9"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-3 col-12 col-form-label text-left text-sm-right">10ème donataire :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="TextBox10" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone02" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_10"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR txtZone03" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_10"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-6 col-12 col-form-label text-left text-sm-right">Quotité restant à attribuer :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtZone02_donataire_total" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtZone03_donataire_total" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-percent"></i>
                                </div>
                            </div>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-12" id="lblMsgTextZone02" style="display: none">La répartition des quotités choisies par le Donateur est incorrecte.</label>
                            <label class="col-12" id="lblMsgTextZone03" style="display: none">La répartition des quotités choisies par la Donatrice est incorrecte.</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" clientidmode="Static" id="div_input_form_2">
        <div class="card">
            <div class="card-header">
                Détermination des biens
            </div>
            <div class="card-body">
                <div class="sub-card">
                    <div class="card-header">
                        Bien immobilier
                    </div>
                    <div id="div_immobilier" class="card-body">
                        <div id="div_add_immobilier" class="text-center mb-2">
                            <button type="button" id="btnAddImmobilier" class="btn btn-success">
                                Add bien immobilier
                            </button>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Bien immobilier exonéré
                    </div>
                    <div id="div_immobilierexonéré" class="card-body">
                        <div id="div_add_immobilierexonéré" class="text-center mb-2">
                            <button type="button" id="btnAddImmobilierExonéré" class="btn btn-success">
                                Add bien immobilier exonéré
                            </button>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Bien mobilier
                    </div>
                    <div class="card-body" id="div_mobilier">
                        <div class="text-center mb-2" id="div_add_mobilier">
                            <button type="button" id="btnAddMobilier" class="btn btn-success">
                                Add bien mobilier
                            </button>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Sommes d'argent éventuellement exonérées (Art. 790 du CGI)
                    </div>
                    <div class="card-body" id="div_sommeargent">
                        <div class="text-center mb-2" id="div_add_sommeargent">
                            <button type="button" id="btnAddSommeArgent" class="btn btn-success">
                                Add somme d'argent
                            </button>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Bien mobilier exonéré
                    </div>
                    <div class="card-body" id="div_mobilierexonéré">
                        <div class="text-center mb-2" id="div_add_mobilierexonéré">
                            <button type="button" id="btnAddMobilierExonéré" class="btn btn-success">
                                Add bien mobilier exonéré
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="div_input_form_3">
        <div class="card">
            <div class="card-header">
                Sélection du donataire
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col">
                        <button type="button" id="btnDonataire_1" class="btn btn-default btnDonataire w-100 mb-2" data-value="1">1er Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataire_2" class="btn btn-default btnDonataire w-100 mb-2" data-value="2">2e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataire_3" class="btn btn-default btnDonataire w-100 mb-2" data-value="3">3e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataire_4" class="btn btn-default btnDonataire w-100 mb-2" data-value="4">4e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataire_5" class="btn btn-default btnDonataire w-100 mb-2" data-value="5">5e Donataire</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <button type="button" id="btnDonataire_6" class="btn btn-default btnDonataire w-100 mb-2" data-value="6">6er Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataire_7" class="btn btn-default btnDonataire w-100 mb-2" data-value="7">7e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataire_8" class="btn btn-default btnDonataire w-100 mb-2" data-value="8">8e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataire_9" class="btn btn-default btnDonataire w-100 mb-2" data-value="9">9e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataire_10" class="btn btn-default btnDonataire w-100 mb-2" data-value="10">10e Donataire</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Donations antérieures consenties par le(s) donateur(s)
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-12">
                        <label>Donation(s) reçue(s) du Père :</label>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_1" class="btn btn-default btnRappelPere w-100 mb-2" data-value="1">1er Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_2" class="btn btn-default btnRappelPere w-100 mb-2" data-value="2">2e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_3" class="btn btn-default btnRappelPere w-100 mb-2" data-value="3">3e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_4" class="btn btn-default btnRappelPere w-100 mb-2" data-value="4">4e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_5" class="btn btn-default btnRappelPere w-100 mb-2" data-value="5">5e Rappel</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <button type="button" id="btnRappelPère_6" class="btn btn-default btnRappelPere w-100 mb-2" data-value="6">6e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_7" class="btn btn-default btnRappelPere w-100 mb-2" data-value="7">7e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_8" class="btn btn-default btnRappelPere w-100 mb-2" data-value="8">8e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_9" class="btn btn-default btnRappelPere w-100 mb-2" data-value="9">9e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPère_10" class="btn btn-default btnRappelPere w-100 mb-2" data-value="10">10e Rappel</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <label>Donation(s) reçue(s) de la Mère :</label>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_1" class="btn btn-default btnRappelMere w-100 mb-2" data-value="1">1er Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_2" class="btn btn-default btnRappelMere w-100 mb-2" data-value="2">2e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_3" class="btn btn-default btnRappelMere w-100 mb-2" data-value="3">3e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_4" class="btn btn-default btnRappelMere w-100 mb-2" data-value="4">4e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_5" class="btn btn-default btnRappelMere w-100 mb-2" data-value="5">5e Rappel</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <button type="button" id="btnRappelMère_6" class="btn btn-default btnRappelMere w-100 mb-2" data-value="6">6e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_7" class="btn btn-default btnRappelMere w-100 mb-2" data-value="7">7e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_8" class="btn btn-default btnRappelMere w-100 mb-2" data-value="8">8e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_9" class="btn btn-default btnRappelMere w-100 mb-2" data-value="9">9e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMère_10" class="btn btn-default btnRappelMere w-100 mb-2" data-value="10">10e Rappel</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                <span id="lblRappelTitle">1ERE DONATION</span>
            </div>
            <div class="card-body">
                <div class="sub-card">
                    <div class="card-header">
                        Date de la donation
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Saisir la date :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox CssClass="form-control datePicker" runat="server" ClientIDMode="Static" ID="txtZone00_"></asp:TextBox>
                                <i class="form-control-feedback fa fa-calendar"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Incorporation (Art. 1078-2 du C.civil)
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-12 col-sm-8 col-sm-offset-4">
                                <asp:CheckBox CssClass="checkbox-custom" runat="server" ClientIDMode="Static"
                                    ID="chk01" Text="Cette donation est-elle incorporée dans la donation partage actuelle ?" />
                            </div>
                        </div>
                        <div class="row form-group text-center text-danger">
                            <div class="col-12" id="chk01_message_1">
                                Vous avez convenu d'incorporer le don antérieur. Seul est incorporable le don en avance de part soit d'origine soit par transformation préalable aux présentes. Vous devez saisir la valeur du bien donné au jour de la mutation et la valeur d'incorporation à ce jour. (Pour plus de précisions consulter la documentation - "applications pratiques".
                            </div>
                            <div class="col-12" id="chk01_message_2">
                                Vous avez convenu de ne pas incorporer le don antérieur. Vous devez uniquement saisir la valeur du bien donné au jour de la mutation sans préciser la nature de la donation car il s'agit d'un simple rappel fiscal.
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Données économiques de la donation
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Valeur des biens donnés en avancement de part :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone01"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Valeur des biens donnés au jour de la mutation :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone02"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Exonérations liées aux biens
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Cumul des exonérations utilisées :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone03"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-12">(Ensemble des exonérations à l'exception de celles résulatnt de l'article 790 G du CGI.)</label>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Valeur des biens donnés au jour de la mutation :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone04"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Valeur des biens incorporés à la donation présente
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Valeur à ce jour de l'incorporation de ces biens :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone05"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Valeur des biens immobiliers incorporés :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone06"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Base de taxation au titre des émoluments de la donation :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone07"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="div_synthese" runat="server" clientidmode="Static">
        <div class="card" runat="server" clientidmode="Static" id="row50">
            <div class="card-header">
                MASSE DONNEE EN NUE PROPRIETE
            </div>
            <div class="card-body text-right">
                <div id="row10"></div>
                <div class="sub-card">
                    <div class="card-header">
                        Situation active nette
                    </div>
                    <div class="card-body text-right" id="row13">
                    </div>
                </div>
            </div>
        </div>
        <div class="card" runat="server" clientidmode="Static" id="row200">
            <div class="card-header">
                MASSE DONNEE EN PLEINE PROPRIETE
            </div>
            <div class="card-body">
                <div class="sub-card">
                    <div class="card-header">
                        Situation active nette
                    </div>
                    <div class="card-body text-right" id="row20to26">
                    </div>
                </div>
            </div>
        </div>
        <div class="card" runat="server" clientidmode="Static" id="row27">
            <div class="card-header">
                INCORPORATION DES DONS ANTERIEURS
            </div>
            <div class="card-body">
                <div class="sub-card" runat="server" clientidmode="Static" id="row28">
                    <div class="card-header">
                        Du chef du Donateur
                    </div>
                    <div class="card-body text-right" id="row29to31">
                    </div>
                </div>
                <div class="sub-card" runat="server" clientidmode="Static" id="row32">
                    <div class="card-header">
                        Du chef de la Donatrice
                    </div>
                    <div class="card-body text-right" id="row33to35">
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                DROITS DANS LA MASSE GLOBALE
            </div>
            <div class="card-body">
                <div class="sub-card" runat="server" clientidmode="Static" id="row38">
                    <div class="card-header">
                        Concernant le donateur
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col"></label>
                            <label class="col">Droits dans la masse globale</label>
                            <label class="col">Quote part</label>
                        </div>
                        <div id="row39to48">
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" clientidmode="Static" id="row49">
                    <div class="card-header">
                        Concernant le donatrice
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col"></label>
                            <label class="col">Droits dans la masse globale</label>
                            <label class="col">Quote part</label>
                        </div>
                        <div id="row50to59">
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" clientidmode="Static" id="row60">
                    <div class="card-header">
                        Récapitulatif général
                    </div>
                    <div class="card-body text-right">
                        <div id="row61to70">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divDonataireAllotissement">
        </div>
        <div class="card" runat="server" clientidmode="Static" id="row2700">
            <div class="card-header">
                CUMUL DES ATTRIBUTIONS ET DES SOULTES
            </div>
            <div class="card-body text-right">
                <div class="row">
                    <label class="col-sm-6 col-6">Biens donnés par le Père :</label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="H2702"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Biens donnés par la Mère :</label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="H2704"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Total des biens donnés :</label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="H2706"></label>
                </div>
                <div id="row1171"></div>
            </div>
        </div>
    </div>
    <div id="div_synthese_taxe_pere">
        <%--  --%>
    </div>
    <div id="div_synthese_taxe_mere">
        <%--  --%>
    </div>
    <div id="div_taxe_donation" runat="server" clientidmode="Static">
        <%--  --%>
    </div>
    <div id="div_synthase_donation">
        <div class="card">
            <div class="card-header">
                Sélection du donataire
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col">
                        <button type="button" id="btnDonataireResult_1" data-value="1" class="btn btn-default btnDonataireResult w-100 mb-2">1er Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataireResult_2" data-value="2" class="btn btn-default btnDonataireResult w-100 mb-2">2e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataireResult_3" data-value="3" class="btn btn-default btnDonataireResult w-100 mb-2">3e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataireResult_4" data-value="4" class="btn btn-default btnDonataireResult w-100 mb-2">4e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataireResult_5" data-value="5" class="btn btn-default btnDonataireResult w-100 mb-2">5e Donataire</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <button type="button" id="btnDonataireResult_6" data-value="6" class="btn btn-default btnDonataireResult w-100 mb-2">6e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataireResult_7" data-value="7" class="btn btn-default btnDonataireResult w-100 mb-2">7e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataireResult_8" data-value="8" class="btn btn-default btnDonataireResult w-100 mb-2">8e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataireResult_9" data-value="9" class="btn btn-default btnDonataireResult w-100 mb-2">9e Donataire</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnDonataireResult_10" data-value="10" class="btn btn-default btnDonataireResult w-100 mb-2">10e Donataire</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Donations antérieures consenties par le(s) donateur(s)
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-12">
                        <label>Donation(s) reçue(s) du Père :</label>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_1" data-value="1" class="btn btn-default btnRappelPereResult w-100 mb-2">1er Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_2" data-value="2" class="btn btn-default btnRappelPereResult w-100 mb-2">2e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_3" data-value="3" class="btn btn-default btnRappelPereResult w-100 mb-2">3e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_4" data-value="4" class="btn btn-default btnRappelPereResult w-100 mb-2">4e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_5" data-value="5" class="btn btn-default btnRappelPereResult w-100 mb-2">5e Rappel</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_6" data-value="6" class="btn btn-default btnRappelPereResult w-100 mb-2">6e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_7" data-value="7" class="btn btn-default btnRappelPereResult w-100 mb-2">7e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_8" data-value="8" class="btn btn-default btnRappelPereResult w-100 mb-2">8e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_9" data-value="9" class="btn btn-default btnRappelPereResult w-100 mb-2">9e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelPereResult_10" data-value="10" class="btn btn-default btnRappelPereResult w-100 mb-2">10e Rappel</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <label>Donation(s) reçue(s) de la Mère :</label>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_1" data-value="1" class="btn btn-default btnRappelMereResult w-100 mb-2">1er Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_2" data-value="2" class="btn btn-default btnRappelMereResult w-100 mb-2">2e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_3" data-value="3" class="btn btn-default btnRappelMereResult w-100 mb-2">3e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_4" data-value="4" class="btn btn-default btnRappelMereResult w-100 mb-2">4e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_5" data-value="5" class="btn btn-default btnRappelMereResult w-100 mb-2">5e Rappel</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_6" data-value="6" class="btn btn-default btnRappelMereResult w-100 mb-2">6e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_7" data-value="7" class="btn btn-default btnRappelMereResult w-100 mb-2">7e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_8" data-value="8" class="btn btn-default btnRappelMereResult w-100 mb-2">8e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_9" data-value="9" class="btn btn-default btnRappelMereResult w-100 mb-2">9e Rappel</button>
                    </div>
                    <div class="col">
                        <button type="button" id="btnRappelMereResult_10" data-value="10" class="btn btn-default btnRappelMereResult w-100 mb-2">10e Rappel</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header" id="headerPereMere"></div>
            <div class="card-body">
                <div class="sub-card">
                    <div class="card-header" id="subHeaderPereMere"></div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-sm-6 col-6">Rappel de la valeur des biens donnés :</label>
                            <label class="col-sm-3 col-6" id="SORTIE_DON_1"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-6">Montant de la base fiscale :</label>
                            <label class="col-sm-3 col-6" id="SORTIE_DON_2"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-6">Abattement personnel en vigueur au jour de cette donation :</label>
                            <label class="col-sm-3 col-6" id="SORTIE_DON_3"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-6">Abattement utilisé lors de cette donation :</label>
                            <label class="col-sm-3 col-6" id="SORTIE_DON_4"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-6">Sommes taxées :</label>
                            <label class="col-sm-3 col-6" id="SORTIE_DON_5"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-6 col-6">Droits taxés avant réduction :</label>
                            <label class="col-sm-3 col-6" id="SORTIE_DON_6"></label>
                        </div>
                        <div class="row text-center text-danger" id="SORTIE_DON_7" style="display: none">
                            <label class="col">
                                Attention :
                            <br />
                                Les calculs des sommes taxées et des droits ont fait l'objet du lissage prévu à l'Art. 784 du CGI.
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card" runat="server" clientidmode="Static" id="divLibelleSimulation">
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
    <div class="card" runat="server" clientidmode="Static" id="divSendEmail">
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
    <div class="card" runat="server" clientidmode="Static" id="divPrint">
        <div class="card-header">
            Imprimer la simulation
        </div>
        <div class="card-body">
            <div class="form-group text-center">
                <button type="button" class="btn btn-success" id="btnPrint">
                    <i class="fa fa-print" aria-hidden="true"></i>
                    Imprimer
                </button>
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
    <script type="text/javascript" src="/Scripts/Site.js"></script>
    <script type="text/javascript" src="/Scripts/DON-01.js"></script>

</asp:Content>
