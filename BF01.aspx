<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF01.aspx.cs" Inherits="NotaliaOnline.BF01" ValidateRequest="false" %>

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
                Les ventes immobilières
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
                                <input type="text" class="form-control" id="TextBox1" name="TextBox1" />
                            </div>
                        </div>
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
                        Nature et situation du Bien vendu
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Choix :</label>
                            <div class="col-12 col-sm-6">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl1Choix" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Immeuble ancien et assimilé"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Immeuble neuf"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Terrain à batir et assimilé"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Bois et forêts"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Bien affecté à l'exploitation agricole"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Logements sociaux"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Biens affectés au service du public"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Situation :</label>
                            <div class="col-12 col-sm-6">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl2Situation" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="France (Province)"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="France (Ile de France)"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Corse"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="DOM (hors Guyane)"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Guyane"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Agriculture
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <span class="col-12 col-sm-4 col-form-label text-left text-sm-right"></span>
                            <div class="col-12 col-sm-6">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl3Agriculture" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Preneur en place"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Acquisition par la SAFER"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Vente par la SAFER"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Logements sociaux
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <span class="col-12 col-sm-4 col-form-label text-left text-sm-right"></span>
                            <div class="col-12 col-sm-6">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl4LogementsSociaux" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Acquisition par société de HLM - Exonération"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Acquisition par société de HLM - Droit fixe"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Mutation entre sociétés de HLM - Exonération"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Mutation entre sociétés de HLM - Droit fixe"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Qualités des parties
                    </div>
                    <div class="card-body">
                        <div class="row form-group ">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Vendeur :</label>
                            <div class="col-12 col-sm-6">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl5Vendeur" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Vendeur non assujetti à la TVA"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Vendeur assujetti à la TVA"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Acquéreur :</label>
                            <div class="col-12 col-sm-6">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl6Acquereur" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Acquéreur non assujetti à la TVA"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Acquéreur assujetti à la TVA"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Option fiscale du vendeur au regard de la TVA (bien autre que terrain à batir)
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <span class="col-12 col-sm-4 col-form-label text-left text-sm-right"></span>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl7AucuneOption" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Aucune option"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="TVA sur le prix total"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="TVA sur la marge"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Option fiscale du vendeur au regard de la TVA (terrain à bâtir ou assimilé)
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <span class="col-12 col-sm-4 col-form-label text-left text-sm-right"></span>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl8TvasurleprixTotal" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Tva sur le prix total"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Tva sur la marge"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Engagement de l'Acquéreur assujetti (bien autre que terrain à batir)
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <span class="col-12 col-sm-4 col-form-label text-left text-sm-right"></span>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl9AucumEngagement" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Aucun engagement"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Revendre"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Engagement de l'Acquéreur assujetti (terrain à bâtir ou assimilé)
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <span class="col-12 col-sm-4 col-form-label text-left text-sm-right"></span>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="form-control select2" ID="ddl10AucumEngagement" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="Aucun engagement"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Revendre"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Construire"></asp:ListItem>
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
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Prix de vente :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox ID="txtZone1" ClientIDMode="Static" CssClass="form-control numberFormatFR" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-12 col-sm-4 col-form-label text-left text-sm-right">
                            </div>
                            <div class="col-sm-8 col-12">
                                <asp:CheckBox ID="chkBox1" runat="server" ClientIDMode="Static" CssClass="checkbox"
                                    Text="La vente comprend des meubles ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Prix des meubles compris dans le prix global :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox ID="txtZone2" ClientIDMode="Static" CssClass="form-control numberFormatFR" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Prix symbolique - Valeur vénale :</label>
                            <div class="col-sm-6 col-12 ">
                                <asp:TextBox ID="txtZone3" ClientIDMode="Static" CssClass="form-control numberFormatFR" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="section_Calcul_de_la_TVA" clientidmode="Static">
                    <div class="card-header">
                        Calcul de la TVA sur marge en raison de l'option
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Prix de vente exprimé tenant compte des charges :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox ID="txtLabel1" ClientIDMode="Static" Enabled="False" CssClass="form-control control-input" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Prix d'achat attesté par le vendeur :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox ID="txtZone4" ClientIDMode="Static" CssClass="form-control numberFormatFR" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Marge taxable :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox ID="txtLabel2" ClientIDMode="Static" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Taux de la Tva applicable à l'opération :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox ID="txtLabel3" ClientIDMode="Static" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-percent"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">TVA sur marge :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox ID="txtLabel4" ClientIDMode="Static" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group ">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Base d'imposition des droits :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox ID="txtLabel5" ClientIDMode="Static" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card" runat="server" id="section_Droit_de_mutation" clientidmode="Static">
                    <div class="card-header">
                        Droits de mutation - Taxes complémentaires
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-12 col-sm-4 col-form-label text-left text-sm-right">
                            </div>
                            <div class="col-sm-8 col-12">
                                <asp:CheckBox ID="chkBox2" ClientIDMode="Static" runat="server" CssClass="checkbox" Checked="True"
                                    Text="Le département a-t-il voté l'augmentation de sa part ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Taux de la taxe départementale voté :</label>
                            <div class="col-sm-6 col-12">
                                <asp:TextBox ID="txtZone5" ClientIDMode="Static" value="4,5" CssClass="form-control numberFormatFR control-input" runat="server"></asp:TextBox>
                                <i class="form-control-feedback fa fa-percent"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-12 col-sm-4 col-form-label text-left text-sm-right"></div>
                            <div class="col-sm-8 col-12">
                                <asp:CheckBox ID="chkBox3" ClientIDMode="Static" runat="server" CssClass="checkbox"
                                    Text="Le bien est-il concerné par la taxe additionnelle ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Partie du prix des locaux soumis à la taxe additionnelle :</label>
                            <div class="col-sm-6 col-12 ">
                                <asp:TextBox ID="txtZone6" ClientIDMode="Static" CssClass="form-control numberFormatFR control-input" runat="server"></asp:TextBox>
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
                            <div class="col-12 col-sm-6">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control numberFormatFR"
                                    Text="800,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right">Débours :</label>
                            <div class="col-12 col-sm-6">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDébours" CssClass="form-control numberFormatFR"
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
                    <label class="col-6 col-sm-6">Emoluments HT du notaire :</label>
                    <label runat="server" clientidmode="Static" id="lblG58" class="col-6 col-sm-3"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-6">Débours : </label>
                    <label runat="server" clientidmode="Static" id="lblG57" class="col-6 col-sm-3"></label>
                </div>
                <div class="row">
                    <label class="col-6 col-sm-6">Trésor public : </label>
                    <label runat="server" clientidmode="Static" id="lblG59" class="col-6 col-sm-3"></label>
                </div>
                <div class="row" style="font-size: 1.5rem">
                    <label class="col-6 col-sm-6">Montant des frais : </label>
                    <label runat="server" clientidmode="Static" id="lblG56" class="col-6 col-sm-3"></label>
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
                        Emoluments du notaire
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="divRow1">
                            <label runat="server" id="lblX1" class="col-4 col-sm-2"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" id="lblY1" class="col-6 col-sm-4"></label>
                            <label class="col-2 col-sm-1">=</label>
                            <label runat="server" id="lblZ1" class="col-6 col-sm-4"></label>
                        </div>
                        <asp:Panel class="row" runat="server" ID="divRow2">
                            <label runat="server" id="lblX2" class="col-4 col-sm-2"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label id="lblY2" runat="server" class="col-6 col-sm-4"></label>
                            <label class="col-2 col-sm-1">=</label>
                            <label id="lblZ2" runat="server" class="col-6 col-sm-4"></label>
                        </asp:Panel>
                        <asp:Panel class="row" runat="server" ID="divRow3">
                            <label id="lblX3" runat="server" class="col-4 col-sm-2"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label id="lblY3" runat="server" class="col-6 col-sm-4"></label>
                            <label class="col-2 col-sm-1">=</label>
                            <label id="lblZ3" runat="server" class="col-6 col-sm-4"></label>
                        </asp:Panel>
                        <asp:Panel class="row" runat="server" ID="divRow4">
                            <label id="lblX4" runat="server" class="col-4 col-sm-2"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label id="lblY4" runat="server" class="col-6 col-sm-4"></label>
                            <label class="col-2 col-sm-1">=</label>
                            <label id="lblZ4" runat="server" class="col-6 col-sm-4"></label>
                        </asp:Panel>
                        <asp:Panel class="row" runat="server" ID="divRow5">
                            <label runat="server" class="col-6 col-sm-3">Total :</label>
                            <label runat="server" class="col-6 col-sm-4" id="lblTotal"></label>
                        </asp:Panel>
                        <asp:Panel class="row" runat="server" ID="divRow6">
                            <label runat="server" class="col-6 col-sm-8">Emolument minimum :</label>
                            <label runat="server" class="col-6 col-sm-4" id="lblEmolument_min"></label>
                        </asp:Panel>
                        <div class="row">
                            <label runat="server" class="col-6 col-sm-8">Total HT des émoluments réglementés :</label>
                            <label runat="server" class="col-6 col-sm-4" id="G9"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Récapitulatif et calcul de la TVA
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label runat="server" class="col-6 col-sm-8">Total HT des émoluments du notaire :</label>
                            <label runat="server" class="col-6 col-sm-4" id="G12"></label>
                        </div>
                        <div class="row">
                            <label runat="server" class="col-6 col-sm-8">Emoluments de formalités :</label>
                            <label runat="server" class="col-6 col-sm-4" id="G13"></label>
                        </div>
                        <div class="row">
                            <label runat="server" class="col-6 col-sm-8">Total HT :</label>
                            <label runat="server" class="col-6 col-sm-4" id="G14"></label>
                        </div>
                        <div class="row">
                            <label runat="server" class="col-6 col-sm-8">TVA :</label>
                            <label runat="server" class="col-6 col-sm-4" id="G15"></label>
                        </div>
                        <div class="row">
                            <label runat="server" class="col-6 col-sm-8">Total TTC :</label>
                            <label runat="server" class="col-6 col-sm-4" id="G16"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Débours
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label runat="server" class="col-6 col-sm-8">Débours :</label>
                            <label runat="server" class="col-6 col-sm-4" id="G19"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div class="row" style="font-size: 1.4em">
                            <label class="col-12 text-left">Contribution pour la sécurité immobilière (art. 879 du CGI)</label>
                        </div>
                        <asp:Panel class="row" ID="divRow18" runat="server">
                            <label id="lblVente_X" runat="server" class="col-4 col-md-2"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label id="lblVente_Y" runat="server" class="col-6 col-md-4"></label>
                            <label class="col-2 col-sm-1">=</label>
                            <label id="lblVente_Z" runat="server" class="col-6 col-md-4"></label>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="divRow19" class="row text-center">
                            <label id="lblVente_NA" runat="server" class="col-12">NA</label>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="divRow21">
                            <div class="row text-center">
                                <label runat="server" class="col-12">Privilège de vendeur :</label>
                            </div>
                            <div class="row">
                                <label id="lblPrivilege_de_vendeur_X1" runat="server" class="col-4 col-md-2"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label id="lblPrivilege_de_vendeur_Y1" runat="server" class="col-4 col-md-4"></label>
                                <label class="col-2 col-sm-1">=</label>
                                <label id="lblPrivilege_de_vendeur_Z1" runat="server" class="col-4 col-md-4"></label>
                            </div>
                        </asp:Panel>
                        <div class="row" style="font-size: 1.4em">
                            <label class="col-12 text-left">Taxe de publicité foncière </label>
                        </div>
                        <asp:Panel ID="divRow23" runat="server" CssClass="text-right">
                            <div class="row">
                                <div class="col-sm-8 col-12 taxition-border-bottom text-right" style="font-size: 1.2em">
                                    <label style="text-decoration: underline">TVA immobilière :</label>
                                </div>
                            </div>
                            <div class="row">
                                <label runat="server" class="col-6 col-sm-8">Prix TTC (charges comprises) :</label>
                                <label runat="server" class="col-6 col-sm-4" id="lblPrix_TTC"></label>
                            </div>
                            <div class="row">
                                <label runat="server" class="col-6 col-sm-8">Taux de la TVA :</label>
                                <label runat="server" class="col-6 col-sm-4" id="lblTaux_de_la_TVA"></label>
                            </div>

                            <asp:Panel ID="divRow26" runat="server" CssClass="row">
                                <label runat="server" class="col-6 col-sm-8">Montant de la TVA sur le prix total :</label>
                                <label runat="server" class="col-6 col-sm-4" id="lblMontant_de_la_TVA_sur_le_prix_total"></label>
                            </asp:Panel>
                            <asp:Panel ID="divRow27" runat="server" CssClass="row">
                                <label runat="server" class="col-6 col-sm-8">Montant de la TVA sur marge :</label>
                                <label runat="server" class="col-6 col-sm-4" id="lblMontant_de_la_TVA_sur_marge"></label>
                            </asp:Panel>
                            <div class="row">
                                <label runat="server" class="col-6 col-sm-8">Base HT :</label>
                                <label runat="server" class="col-6 col-sm-4" id="lblBaseHT"></label>
                            </div>
                            <span class="col-12 text-center">
                                <label runat="server" id="Label35">Le vendeur doit être informé de son obligation d'inclure dans ses déclarations CA12 la présente TVA via Internet. Les frais calculés ne sont pas impactés par cette formalité.</label>
                            </span>
                        </asp:Panel>
                        <asp:Panel ID="divRow29" runat="server" CssClass="text-right">
                            <div class="row">
                                <div class="col-sm-8 col-12" style="font-size: 1.2em">
                                    <label style="text-decoration: underline">Base de taxation de la TPF :</label>
                                </div>
                            </div>
                            <div class="row">
                                <span class="col-6 col-sm-8 text-center-xxs padding-row">
                                    <label>Base taxable :</label>
                                </span>
                                <span class="col-6 col-sm-4 text-center-xxs padding-row">
                                    <label runat="server" text="300,000.00 €" cssclass="" id="lblBase_taxable"></label>
                                </span>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="divRow34" runat="server">
                            <div class="row">
                                <div class="col-sm-8 col-12 taxition-border-bottom text-right" style="font-size: 1.2em">
                                    <label style="text-decoration: underline">Montant des droits :</label>
                                </div>
                            </div>
                            <asp:Panel ID="divRow35" runat="server" CssClass="row">
                                <span class="col-6 col-sm-6">
                                    <label runat="server" class="">Taxe départementale :</label>
                                </span>
                                <span class="col-3 col-sm-3">
                                    <label runat="server" text="4.500%" cssclass="" id="lblTaxe_departementale_X"></label>
                                </span>
                                <span class="col-3 col-sm-3">
                                    <label runat="server" text="13,500 €" cssclass="" id="lblTaxe_departementale_Y"></label>
                                </span>
                            </asp:Panel>
                            <asp:Panel ID="divRow36" runat="server" CssClass="row">
                                <span class="col-6 col-sm-6">
                                    <label runat="server" class="">Prélèvement de l'Etat sur taxe départementale :</label>
                                </span>
                                <span class="col-3 col-sm-3">
                                    <label runat="server" text="0.10665%" cssclass="" id="lblprelevement_de_lEtat_sur_taxe_departementale_X"></label>
                                </span>
                                <span class="col-3 col-sm-3 ">
                                    <label runat="server" text="320 €" cssclass="" id="lblprelevement_de_lEtat_sur_taxe_departementale_Y"></label>
                                </span>
                            </asp:Panel>
                            <asp:Panel ID="divRow37" runat="server" CssClass="row">
                                <span class="col-6 col-sm-6">
                                    <label runat="server" class="">Taxe locale :</label>
                                </span>
                                <span class="col-3 col-sm-3 text-center-xxs padding-row">
                                    <label runat="server" text="1.200%" cssclass="" id="lblTaxe_locale_X"></label>
                                </span>
                                <span class="col-3 col-sm-3 text-center-xxs padding-row">
                                    <label runat="server" text="3,600 €" cssclass="" id="lblTaxe_locale_Y"></label>
                                </span>
                            </asp:Panel>
                            <asp:Panel ID="divRow38" runat="server" CssClass="row">
                                <span class="col-6 col-sm-6 ">
                                    <label runat="server" class="">Droits fixes (Art.691 bis CGI) :</label>
                                </span>
                                <span class="col-6 col-sm-6 text-center-xxs padding-row">
                                    <label runat="server" text="0 €" cssclass="" id="lblDroits_fixes"></label>
                                </span>
                            </asp:Panel>
                            <asp:Panel ID="divRow39" runat="server" CssClass="row">
                                <span class="col-6 col-sm-6 ">
                                    <label runat="server" class="">Taxe additionnelle (Art.1599 sexies CGI) :</label>
                                </span>
                                <span class="col-3 col-sm-3 text-center-xxs padding-row">
                                    <label runat="server" text="0.600%" cssclass="" id="lblTaxe_additionnelle_X"></label>
                                </span>
                                <span class="col-3 col-sm-3 text-center-xxs padding-row">
                                    <label runat="server" text="0 €" cssclass="" id="lblTaxe_additionnelle_Y"></label>
                                </span>
                            </asp:Panel>
                            <div class="row text-center">
                                <span class="col-12">
                                    <label id="lblAssiette_de_taxation" clientidmode="Static" runat="server" class="">Assiette de taxation : 0 Euros.</label>
                                </span>
                            </div>
                            <asp:Panel ID="divRow41" runat="server" CssClass="row">
                                <div class="col-6 col-sm-9 text-center-xxs padding-row">
                                    <label runat="server" class="">Total :</label>
                                </div>
                                <div class="col-6 col-sm-3 text-center-xxs padding-row">
                                    <label runat="server" text="17,420.00 €" cssclass="" id="lblTotal_Montant_des_droits"></label>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="divRow42" runat="server">
                                <div class="col-md-12 text-center">
                                    <label runat="server" class="" id="lblNA">NA</label>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
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
    <script type="text/javascript" src="Scripts/BF01.js"></script>
</asp:Content>
