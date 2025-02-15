<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF13.aspx.cs" Inherits="NotaliaOnline.BF13" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #select2-ddl2-container, #select2-ddl3-container, #select2-ddl4-container {
            text-align: center;
        }

        ul#select2-ddl2-results li, ul#select2-ddl3-results li, ul#select2-ddl4-results li {
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
                Les prêts hypothécaires
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
                        Caracteristique de l'emprunt
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Choisissez le type de prêt :</label>
                            </div>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl1" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="0" Text="Sélectionnez un type de prêt …"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Prêt, ouverture de crédit"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Prêt conventionné, PEL"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Prêt aidé (Art 312-1 du CCH)"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Prêt construction multiples"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Convention de rechargement d'une hypothéque"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Avenant transformant en hypothéque rechargeable"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Prêt hypothécaire finançant une activité professionelle"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant total du prêt :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone01"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Partie du prêt au taux normal :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone02"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Partie prêt conventionné, PEL, prêt aidé (art L 312-1 du CCH) :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone02_1" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="sub-card">
                    <div class="card-header">
                        Garanties
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Evaluation des intérêts et accessoires :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl2" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="10%" Text="10%"></asp:ListItem>
                                    <asp:ListItem Value="15%" Text="15%"></asp:ListItem>
                                    <asp:ListItem Value="20%" Text="20%"></asp:ListItem>
                                    <asp:ListItem Value="25%" Text="25%"></asp:ListItem>
                                    <asp:ListItem Value="30%" Text="30%"></asp:ListItem>
                                    <asp:ListItem Value="35%" Text="35%"></asp:ListItem>
                                    <asp:ListItem Value="40%" Text="40%"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="sub-card">
                    <div class="card-header">
                        Cautionnement
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-8 col-sm-offset-4 col-12">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk01" CssClass="checkbox-custom"
                                    Text="Intervention de caution(s) dans l'acte authentique ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant cautionné (intérêts et accessoires compris) :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone03"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-8 col-sm-offset-4 col-12">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk02" CssClass="checkbox-custom"
                                    Text="La caution est-elle hypothécaire ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Nombre de biens grevés :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl3" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12">Montant inscrit sur les biens immobiliers en capital, intérêts et accessoires :</label>
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">1er Bien :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone04"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">2ème Bien :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone05"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">3ème Bien :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone06"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">4ème Bien :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone07"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">5ème Bien :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone08"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="sub-card">
                    <div class="card-header">
                        Privilèges et hypothèque conventionelle
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-8 col-sm-offset-4  col-12">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk03" CssClass="checkbox-custom"
                                    Text="Privilége de vendeur ?" />
                            </div>
                        </div>
                        <div class="row form-group ">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-8 col-sm-offset-4  col-12">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk04" CssClass="checkbox-custom"
                                    Text="Privilège de Prêteur de Deniers ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant garanti :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox class="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone09"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-12  col-12 text-center">
                                <label style="color: red" id="errPrivilèges1">Valeur impossible. Corrigez le montant.</label>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-8 col-sm-offset-4 col-12">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk05" CssClass="checkbox-custom"
                                    Text="Hypothèque Conventionnelle ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant garanti :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone10"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Provenant du prêt normal à hauteur de :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone11"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-12 col-12 text-center">
                                <label style="color: red" id="errPrivilèges3">123</label>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant dispensé de l'inscription hypothécaire par la banque :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox class="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone12"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Nombre de bureaux des hypothèques requis :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList CssClass="select2" ID="ddl4" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="sub-card">
                    <div class="card-header">
                        Nantissement
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-8 col-sm-offset-4  col-12">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk06" CssClass="checkbox-custom"
                                    Text="Nantissement donné par un tiers ?" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="sub-card">
                    <div class="card-header">
                        Dispositions particulières
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-8 col-sm-offset-4 col-12">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk07" CssClass="checkbox-custom"
                                    Text="Cession d'antériorité (disposition indépendante) ?" />
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-sm-4 col-12 text-left text-sm-right">
                                <label class="col-form-label">Montant profitant de l'antériorité :</label>
                            </div>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone13"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-12 col-sm-4 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-8 col-sm-offset-4 col-12">
                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk08" CssClass="checkbox-custom"
                                    Text="Le prêt bénéficie-t'il d'une dispense ou exonération de droits d'enregistrement ?" />
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
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control numberFormatFR"
                                    Text="800,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 text-left text-sm-right col-form-label">Débours :</label>
                            <div class="col-sm-3 col-12">
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
                    <label class="col-sm-6 col-6">Emoluments du notaire :</label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="H45"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Débours :</label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="H46"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Trésor public :</label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="H44"></label>
                </div>
                <div class="row fa-2x">
                    <label class="col-sm-6 col-6">Montant des frais : </label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="H48"></label>
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
                        Emoluments du notaire - C.com. Art. A 444-143
                    </div>
                    <div class="card-body text-right">
                        <div class="row " runat="server" id="row7" clientidmode="Static">
                            <div class="col-4 col-sm-3">
                                <label runat="server" id="G7"></label>
                            </div>
                            <div class="col-2 col-sm-1">
                                <label>sur</label></div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="H7"></label>
                            </div>
                            <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="I7"></label>
                            </div>
                        </div>
                        <div class="row " runat="server" id="row8" clientidmode="Static">
                            <div class="col-4 col-sm-3">
                                <label runat="server" id="G8"></label>
                            </div>
                            <div class="col-2 col-sm-1">
                                <label>sur</label></div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="H8"></label>
                            </div>
                            <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="I8"></label>
                            </div>
                        </div>
                        <div class="row " runat="server" id="row9" clientidmode="Static">
                            <div class="col-4 col-sm-3">
                                <label runat="server" id="G9"></label>
                            </div>
                            <div class="col-2 col-sm-1">
                                <label>sur</label></div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="H9"></label>
                            </div>
                            <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="I9"></label>
                            </div>
                        </div>
                        <div class="row " runat="server" id="row10" clientidmode="Static">
                            <div class="col-4 col-sm-3">
                                <label runat="server" id="G10"></label>
                            </div>
                            <div class="col-2 col-sm-1">
                                <label>sur</label></div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="H10"></label>
                            </div>
                            <div class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="I10"></label>
                            </div>
                        </div>
                        <div class="row " runat="server" id="row11" clientidmode="Static">
                            <div class="col-6 col-sm-4">
                                <label>Total :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="H11"></label>
                            </div>
                            <div class="col-6 col-sm-2">
                                <label>Total HT :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="I11"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row12">
                            <div class="col-6 col-sm-9">
                                <label>Emoluments de caution (HT) :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G12"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row13">
                            <div class="col-6 col-sm-9">
                                <label>Emoluments d'antériorité (HT) :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G13"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row14">
                            <div class="col-6 col-sm-9">
                                <label>Emoluments de nantissement (HT) :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G14"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>Emoluments de formalités (HT) :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G15"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>Total :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G16"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        TVA
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>Total HT :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G18"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>TVA :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G19"></label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 col-sm-9">
                                <label>Total TTC :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G20"></label>
                            </div>
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
                                <label runat="server" id="G22"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div class="row text-left">
                            <label class="col-12 fa-2x">Enregistrement</label>
                        </div>
                        <div class="row text-center">
                            <label class="col-12" runat="server" id="G26"></label>
                        </div>
                        <div class="row" runat="server" id="row27">
                            <div class="col-6 col-sm-9">
                                <label>Droit sur état :</label>
                            </div>
                            <div class="col-6 col-sm-3">
                                <label runat="server" id="G27"></label>
                            </div>
                        </div>
                        <div runat="server" id="row28">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Hypothèques</label>
                            </div>
                            <div class="text-center">
                                <label style="font-size: 1.2em">CSI (art. 879 du CGI)</label>
                            </div>
                            <div class="row " runat="server" id="row29" clientidmode="Static">
                                <div class="col-8 col-sm-3">
                                    <label>Privilége de prêteur :</label></div>
                                <div class="col-4 col-sm-2">
                                    <label runat="server" id="G29"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="H29"></label>
                                </div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="I29"></label>
                                </div>
                            </div>
                            <div class="row " runat="server" id="row30" clientidmode="Static">
                                <div class="col-8 col-sm-3">
                                    <label>Hypothèque :</label></div>
                                <div class="col-4 col-sm-2">
                                    <label runat="server" id="G30"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="H30"></label>
                                </div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="I30"></label>
                                </div>
                            </div>
                            <div class="row " runat="server" id="row31" clientidmode="Static">
                                <div class="col-8 col-sm-3">
                                    <label>Privilége de vendeur :</label></div>
                                <div class="col-4 col-sm-2">
                                    <label runat="server" id="G31"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="H31"></label>
                                </div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="I31"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" id="row32">
                                <div class="col-8 col-sm-9">
                                    <label>CSI complémentaire pour pluralité de biens ou de bureaux (partie afférente au(x) privilège(s)) :</label>
                                </div>
                                <div class="col-4 col-sm-3">
                                    <label runat="server" id="G32"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" id="row33">
                                <div class="col-8 col-sm-9">
                                    <label>CSI complémentaire pour pluralité de biens ou de bureaux (partie afférente au cautionnement hypothécaire) :</label>
                                </div>
                                <div class="col-4 col-sm-3">
                                    <label runat="server" id="G33"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" id="row34">
                                <div class="col-8 col-sm-9">
                                    <label>Cession d'antériorité :</label>
                                </div>
                                <div class="col-4 col-sm-3">
                                    <label runat="server" id="G34"></label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-8 col-sm-9">
                                    <label>Sous-total :</label>
                                </div>
                                <div class="col-4 col-sm-3">
                                    <label runat="server" id="G35"></label>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="row36">
                            <div class="text-center">
                                <label style="font-size: 1.2em">Publicité foncière</label>
                            </div>
                            <div class="row " runat="server" id="row37" clientidmode="Static">
                                <div class="col-8 col-sm-3">
                                    <label>Taxe de publicité :</label></div>
                                <div class="col-4 col-sm-2">
                                    <label runat="server" id="G37"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="H37"></label>
                                </div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="I37"></label>
                                </div>
                            </div>
                            <div class="row " runat="server" id="row38" clientidmode="Static">
                                <div class="col-8 col-sm-3">
                                    <label>Taxe Etat :</label></div>
                                <div class="col-4 col-sm-2">
                                    <label runat="server" id="G38"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="H38"></label>
                                </div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="I38"></label>
                                </div>
                            </div>
                            <div class="row " runat="server" id="row39" clientidmode="Static">
                                <div class="col-8 col-sm-3">
                                    <label>FAR :</label></div>
                                <div class="col-4 col-sm-2">
                                    <label runat="server" id="G39"></label>
                                </div>
                                <div class="col-2 col-sm-1">
                                    <label>sur</label></div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="H39"></label>
                                </div>
                                <div class="col-5 col-sm-3">
                                    <label runat="server" id="I39"></label>
                                </div>
                            </div>
                            <div class="row" runat="server" id="row40">
                                <div class="col-8 col-sm-9">
                                    <label>Taxe complémentaire  sur les bordereaux d'inscriptions :</label>
                                </div>
                                <div class="col-4 col-sm-3">
                                    <label runat="server" id="I40"></label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-8 col-sm-9">
                                    <label>Sous-total :</label>
                                </div>
                                <div class="col-4 col-sm-3">
                                    <label runat="server" id="I41"></label>
                                </div>
                            </div>
                            <div class="row text-center" runat="server" id="row42">
                                <label class="col-12" style="color: red" runat="server" id="G42"></label>
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
    <script type="text/javascript" src="Scripts/BF13.js"></script>
</asp:Content>
