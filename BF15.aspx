<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF15.aspx.cs" Inherits="NotaliaOnline.BF15" ValidateRequest="false" %>

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
                Programme de vente en l'état futur d'achèvement portant sur le secteur protégé
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
                        Situation du programme - Détermination de la TVA
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Situation du bien vendu :</label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl1" CssClass="select2">
                                    <asp:ListItem Value="1" Text="France Métropolitaine"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Corse"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="DOM (hors Guyane)"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Guyane"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Application de la TVA à taux réduit ?</label>
                            <asp:RadioButton runat="server" ClientIDMode="Static" ID="rad1Oui" CssClass="checkbox-custom col-sm-1 col-sm-offset-0 col-3 col-offset-3" GroupName="radio1"
                                Text="Oui" />
                            <asp:RadioButton runat="server" ClientIDMode="Static" ID="rad1Non" CssClass="checkbox-custom col-sm-1 col-3" GroupName="radio1" Checked="True"
                                Text="Non" />
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Sélectionnez le taux réduit à appliquer : </label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl2" CssClass="select2">
                                    <asp:ListItem Value="1" Text="Taux réduit à 10%"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Taux réduit à 5,5%"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Taux de TVA libre"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Taux de TVA libre : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone01" CssClass="form-control control-input numberFormatFR" Text="3"></asp:TextBox>
                                <i class="form-control-feedback fa fa-percent"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Données juridiques du programme
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right"></label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl3" CssClass="select2">
                                    <asp:ListItem Value="1" Text="Le Maître d'ouvrage est un promoteur privé"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Le Maître d'ouvrage est un organisme d'HLM"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Nombre d'unités d'habitation : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone02" CssClass="form-control control-input numberFormatFR" Text="1"></asp:TextBox>
                                <i class="form-control-feedback" style="right: 30px; top: 6px;">unité(s)</i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Prix
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Prix de la vente TVA comprise : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone03" CssClass="form-control control-input numberFormatFR"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Montant du prix Hors Taxe : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="value01" CssClass="form-control control-input numberFormatFR" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Montant de la TVA : </label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="value00" CssClass="form-control control-input numberFormatFR" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                            <div class="col-sm-2 col-12">
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtTaux" CssClass="form-control control-input numberFormatFR" Enabled="False"></asp:TextBox>
                                <i class="form-control-feedback fa fa-percent"></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Condition particulière de la vente
                    </div>
                    <div class="card-body">
                        <div class="row form-group text-center">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">La vente est-elle consentie contrat en mains ?</label>
                            <asp:RadioButton runat="server" ClientIDMode="Static" ID="rad2Oui" CssClass="checkbox-custom col-sm-1 col-sm-offset-0 col-3 col-offset-3" GroupName="radio2"
                                Text="Oui" />
                            <asp:RadioButton runat="server" ClientIDMode="Static" ID="rad2Non" CssClass="checkbox-custom col-sm-1 col-3" GroupName="radio2" Checked="True"
                                Text="Non" />
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
                <div class="row fa-2x">
                    <label class="col-sm-6 col-6">Montant des frais :</label>
                    <label runat="server" id="F106" clientidmode="Static" class="col-sm-3 col-6"></label>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header">
                Détail des frais
            </div>
            <div class="card-body text-right">
                <div class="sub-card">
                    <div class="card-header">
                        Emoluments du notaire
                    </div>
                    <div class="card-body">
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
                        <div class="row" runat="server" id="row117">
                            <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F117"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G117"></label>
                            <label class="col-6 col-sm-1">=</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H117"></label>
                        </div>
                        <div class="row" runat="server" id="row119">
                            <label class="col-6 col-sm-3">Total :</label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G119"></label>
                        </div>
                        <div class="row" runat="server" id="row122">
                            <label class="col-6 col-sm-8">Emolument minimum : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H122"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">Total des émoluments réglementés : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H124"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Récapitulatif et calcul de la TVA
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <label class="col-6 col-sm-8">Total HT des émoluments du notaire : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H126"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">Emoluments de formalités (HT) : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H127"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">Total HT : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H128"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">TVA : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H129"></label>
                        </div>
                        <div class="row">
                            <label class="col-6 col-sm-8">Total TTC : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H130"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Débours
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <label class="col-6 col-sm-8">Débours : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H132"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Tresor public
                    </div>
                    <div class="card-body">
                        <div runat="server" id="row137">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">CSI (art. 879 du CGI) :</label>
                            </div>
                            <div class="row" runat="server" id="row138">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F138"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G138"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H138"></label>
                            </div>
                            <div class="row" runat="server" id="row139">
                                <label class="col-6 col-sm-8">Minimum de perception : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H139"></label>
                            </div>
                        </div>
                        <div runat="server" id="row141">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Contrat en mains - Frais déductibles :</label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Frais fixes : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H142"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Emoluments du notaire : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H143"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">CSI sur l'immeuble (art. 879 du CGI) : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H144"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Total : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H145"></label>
                            </div>
                        </div>
                        <div id="row147">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Fiscalité immobilière :</label>
                            </div>
                            <div class="row">
                                <label class="col-12 col-sm-8" style="font-size: 1.2em; text-decoration: underline">TVA immobilière :</label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Prix TTC : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H148"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Taux de la TVA : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H149"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Montant de la TVA sur le prix total : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H150"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Base HT : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H151"></label>
                            </div>
                        </div>
                        <div class="row">
                            <label class="col-12 col-sm-8" style="font-size: 1.2em; text-decoration: underline" runat="server" id="F152"></label>
                        </div>
                        <div class="row" runat="server" id="row153">
                            <label class="col-6 col-sm-8">Base HT du prix exprimé : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H153"></label>
                        </div>
                        <div class="row" runat="server" id="row154">
                            <label class="col-6 col-sm-8">Déduction des frais : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H154"></label>
                        </div>
                        <div class="row" runat="server" id="row155">
                            <label class="col-6 col-sm-8">Montant des droits venant en déduction : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H155"></label>
                        </div>
                        <div class="row" runat="server" id="row156">
                            <label class="col-6 col-sm-8">Base taxable : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H156"></label>
                        </div>
                        <div runat="server" id="row158">
                            <div class="row">
                                <label class="col-12 col-sm-8" style="font-size: 1.2em; text-decoration: underline">Montant des droits :</label>
                            </div>
                            <div class="row text-left">
                                <label class="col-12">Taxe départementale :</label>
                            </div>
                            <div class="row">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F159"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G159"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H159"></label>
                            </div>
                        </div>
                        <div runat="server" id="row161">
                            <div class="row text-left">
                                <label class="col-12">Prélèvement de l'Etat sur taxe départementale :</label>
                            </div>
                            <div class="row">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="F162"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G162"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H162"></label>
                            </div>
                        </div>
                        <div class="row" runat="server" id="row164">
                            <label class="col-6 col-sm-8">Minimum de perception : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H164"></label>
                        </div>
                        <div class="row" runat="server" id="row166">
                            <label class="col-6 col-sm-8">Total : </label>
                            <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="H166"></label>
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
    <script type="text/javascript" src="Scripts/BF15.js"></script>
</asp:Content>
