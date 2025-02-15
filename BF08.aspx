<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF08.aspx.cs" Inherits="NotaliaOnline.BF08" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #select2-ddl2-container {
            text-align: center;
        }

        ul#select2-ddl2-results li {
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
                Le crédit bail
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
                    <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Nature de l'opération :</label>
                    <div class="col-sm-6 col-12">
                        <asp:DropDownList ID="ddl1" ClientIDMode="Static" runat="server" CssClass="select2">
                            <asp:ListItem Value="1" Text="Crédit bail"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Cession du crédit bail par le preneur"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divMain3">
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Durée du bail :</label>
                        <div class="col-sm-3 col-12">
                            <asp:DropDownList CssClass="select2" ID="ddl2" ClientIDMode="Static" runat="server">
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="21">21</asp:ListItem>
                                <asp:ListItem Value="22">22</asp:ListItem>
                                <asp:ListItem Value="23">23</asp:ListItem>
                                <asp:ListItem Value="24">24</asp:ListItem>
                                <asp:ListItem Value="25">25</asp:ListItem>
                                <asp:ListItem Value="26">26</asp:ListItem>
                                <asp:ListItem Value="27">27</asp:ListItem>
                                <asp:ListItem Value="28">28</asp:ListItem>
                                <asp:ListItem Value="29">29</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="31">31</asp:ListItem>
                                <asp:ListItem Value="32">32</asp:ListItem>
                                <asp:ListItem Value="33">33</asp:ListItem>
                                <asp:ListItem Value="34">34</asp:ListItem>
                                <asp:ListItem Value="35">35</asp:ListItem>
                                <asp:ListItem Value="36">36</asp:ListItem>
                                <asp:ListItem Value="37">37</asp:ListItem>
                                <asp:ListItem Value="38">38</asp:ListItem>
                                <asp:ListItem Value="39">39</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="41">41</asp:ListItem>
                                <asp:ListItem Value="42">42</asp:ListItem>
                                <asp:ListItem Value="43">43</asp:ListItem>
                                <asp:ListItem Value="44">44</asp:ListItem>
                                <asp:ListItem Value="45">45</asp:ListItem>
                                <asp:ListItem Value="46">46</asp:ListItem>
                                <asp:ListItem Value="47">47</asp:ListItem>
                                <asp:ListItem Value="48">48</asp:ListItem>
                                <asp:ListItem Value="49">49</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="51">51</asp:ListItem>
                                <asp:ListItem Value="52">52</asp:ListItem>
                                <asp:ListItem Value="53">53</asp:ListItem>
                                <asp:ListItem Value="54">54</asp:ListItem>
                                <asp:ListItem Value="55">55</asp:ListItem>
                                <asp:ListItem Value="56">56</asp:ListItem>
                                <asp:ListItem Value="57">57</asp:ListItem>
                                <asp:ListItem Value="58">58</asp:ListItem>
                                <asp:ListItem Value="59">59</asp:ListItem>
                                <asp:ListItem Value="60">60</asp:ListItem>
                                <asp:ListItem Value="61">61</asp:ListItem>
                                <asp:ListItem Value="62">62</asp:ListItem>
                                <asp:ListItem Value="63">63</asp:ListItem>
                                <asp:ListItem Value="64">64</asp:ListItem>
                                <asp:ListItem Value="65">65</asp:ListItem>
                                <asp:ListItem Value="66">66</asp:ListItem>
                                <asp:ListItem Value="67">67</asp:ListItem>
                                <asp:ListItem Value="68">68</asp:ListItem>
                                <asp:ListItem Value="69">69</asp:ListItem>
                                <asp:ListItem Value="70">70</asp:ListItem>
                                <asp:ListItem Value="71">71</asp:ListItem>
                                <asp:ListItem Value="72">72</asp:ListItem>
                                <asp:ListItem Value="73">73</asp:ListItem>
                                <asp:ListItem Value="74">74</asp:ListItem>
                                <asp:ListItem Value="75">75</asp:ListItem>
                                <asp:ListItem Value="76">76</asp:ListItem>
                                <asp:ListItem Value="77">77</asp:ListItem>
                                <asp:ListItem Value="78">78</asp:ListItem>
                                <asp:ListItem Value="79">79</asp:ListItem>
                                <asp:ListItem Value="80">80</asp:ListItem>
                                <asp:ListItem Value="81">81</asp:ListItem>
                                <asp:ListItem Value="82">82</asp:ListItem>
                                <asp:ListItem Value="83">83</asp:ListItem>
                                <asp:ListItem Value="84">84</asp:ListItem>
                                <asp:ListItem Value="85">85</asp:ListItem>
                                <asp:ListItem Value="86">86</asp:ListItem>
                                <asp:ListItem Value="87">87</asp:ListItem>
                                <asp:ListItem Value="88">88</asp:ListItem>
                                <asp:ListItem Value="89">89</asp:ListItem>
                                <asp:ListItem Value="90">90</asp:ListItem>
                                <asp:ListItem Value="91">91</asp:ListItem>
                                <asp:ListItem Value="92">92</asp:ListItem>
                                <asp:ListItem Value="93">93</asp:ListItem>
                                <asp:ListItem Value="94">94</asp:ListItem>
                                <asp:ListItem Value="95">95</asp:ListItem>
                                <asp:ListItem Value="96">96</asp:ListItem>
                                <asp:ListItem Value="97">97</asp:ListItem>
                                <asp:ListItem Value="98">98</asp:ListItem>
                                <asp:ListItem Value="99">99</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Montant annuel du loyer : </label>
                        <div class="col-sm-3 col-12">
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone01" CssClass="form-control numberFormatFR"></asp:TextBox>
                            <i class="form-control-feedback fa fa-euro"></i>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Montant annuel des charges : </label>
                        <div class="col-sm-3 col-12">
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone02" CssClass="form-control numberFormatFR"></asp:TextBox>
                            <i class="form-control-feedback fa fa-euro"></i>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Montant TTC de l'investissement : </label>
                        <div class="col-sm-3 col-12">
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone03" CssClass="form-control numberFormatFR"></asp:TextBox>
                            <i class="form-control-feedback fa fa-euro"></i>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Quote part des loyers afférente aux frais financiers : </label>
                        <div class="col-sm-3 col-12">
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone04" CssClass="form-control numberFormatFR"></asp:TextBox>
                            <i class="form-control-feedback fa fa-euro"></i>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right"></label>
                        <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk1" CssClass="checkbox-custom col-sm-8 col-sm-offset-4 col-12"
                            Text="Le crédit bail est-il cautionné ?" />
                    </div>
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Montant des sommes cautionnées ? </label>
                        <div class="col-sm-3 col-12">
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone05" CssClass="form-control numberFormatFR"></asp:TextBox>
                            <i class="form-control-feedback fa fa-euro"></i>
                        </div>
                    </div>
                </div>
                <div id="divMain4">
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Prix exprimé : </label>
                        <div class="col-sm-3 col-12">
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone06" CssClass="form-control numberFormatFR"></asp:TextBox>
                            <i class="form-control-feedback fa fa-euro"></i>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Montant TTC résiduel de l'investissement : </label>
                        <div class="col-sm-3 col-12">
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtZone07" CssClass="form-control numberFormatFR"></asp:TextBox>
                            <i class="form-control-feedback fa fa-euro"></i>
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
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmolument_de_formalités_HT" CssClass="form-control numberFormatFR"
                                    Text="800,00"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 col-form-label text-left text-sm-right">Débours :</label>
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
                    <label class="col-sm-6 col-6">Emoluments HT du notaire :</label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="lblEmoluments_HT_du_notaire"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Débours :</label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="lblDébours_et_émoluments"></label>
                </div>
                <div class="row">
                    <label class="col-sm-6 col-6">Trésor public : </label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="lblTrésor_public"></label>
                </div>
                <div class="row fa-2x">
                    <label class="col-sm-6 col-6">Montant des frais : </label>
                    <label class="col-sm-3 col-6" runat="server" clientidmode="Static" id="lblMontant_des_frais"></label>
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
                        <span runat="server" clientidmode="Static" id="lblEmolumentTitle">Emoluments du notaire</span>
                    </div>
                    <div class="card-body text-right">
                        <div class="row" runat="server" id="div1">
                            <label runat="server" id="lblRow11" class="col-4 col-sm-3"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" id="lblRow12" class="col-6 col-sm-3"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" id="lblRow13" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="div2">
                            <label runat="server" id="lblRow21" class="col-4 col-sm-3"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" id="lblRow22" class="col-6 col-sm-3"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" id="lblRow23" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="div3">
                            <label runat="server" id="lblRow31" class="col-4 col-sm-3"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" id="lblRow32" class="col-6 col-sm-3"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" id="lblRow33" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="div4">
                            <label runat="server" id="lblRow41" class="col-4 col-sm-3"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" id="lblRow42" class="col-6 col-sm-3"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" id="lblRow43" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="divTotal">
                            <label class="col-6 col-sm-4">Total :</label>
                            <label runat="server" id="lblTotal" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">TOTAL Hors T.V.A :</label>
                            <label class="col-sm-3 col-6" runat="server" id="lblTOTAL_Hors_TVA"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">Emoluments de caution HT :</label>
                            <label class="col-sm-3 col-6" runat="server" id="lblEmoluments_de_caution_HT"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">Total HT des émoluments réglementés :</label>
                            <label class="col-sm-3 col-6" runat="server" id="lblTotal_HT_des_émoluments"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Récapitulatif et calcul de la TVA
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-sm-9 col-6">Total HT des émoluments du notaire :</label>
                            <label class="col-sm-3 col-6" runat="server" id="F124"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">Emoluments de formalités :</label>
                            <label class="col-sm-3 col-6" runat="server" id="F125"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">Total HT :</label>
                            <label class="col-sm-3 col-6" runat="server" id="F126"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">TVA :</label>
                            <label class="col-sm-3 col-6" runat="server" id="F127"></label>
                        </div>
                        <div class="row">
                            <label class="col-sm-9 col-6">Total TTC :</label>
                            <label class="col-sm-3 col-6" runat="server" id="F128"></label>
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
                            <label class="col-sm-3 col-6" runat="server" id="F130"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Trésor public
                    </div>
                    <div class="card-body text-right">
                        <div class="row">
                            <label class="col-12 text-left fa-2x">Contribution pour la sécurité immobilière (art. 879 du CGI) :</label>
                        </div>
                        <div class="row" runat="server" id="row134">
                            <label runat="server" id="F134" class="col-4 col-sm-3"></label>
                            <label class="col-2 col-sm-1">sur</label>
                            <label runat="server" id="G134" class="col-6 col-sm-3"></label>
                            <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                            <label runat="server" id="H134" class="col-6 col-sm-3"></label>
                        </div>
                        <div class="row" runat="server" id="row135">
                            <label class="col-sm-9 col-6">Minimum de perception : </label>
                            <label class="col-sm-3 col-6" runat="server" id="H135"></label>
                        </div>

                        <div class="row">
                            <label class="col-12 text-left fa-2x">Fiscalité immobilière :</label>
                        </div>
                        <div runat="server" id="row138">
                            <div class="row">
                                <label class="col-12 text-left">Taxe départementale :</label>
                                <label runat="server" id="F139" class="col-4 col-sm-3"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" id="G139" class="col-6 col-sm-3"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" id="H139" class="col-6 col-sm-3"></label>
                            </div>
                            <div class="row">
                                <label class="col-12 text-left">Prélèvement de l'Etat sur taxe départementale :</label>
                                <label runat="server" id="F140" class="col-4 col-sm-3"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" id="G140" class="col-6 col-sm-3"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" id="H140" class="col-6 col-sm-3"></label>
                            </div>
                            <div class="row" runat="server" id="row141">
                                <label class="col-sm-9 col-6">Minimum de perception : </label>
                                <label class="col-sm-3 col-6" runat="server" id="H141"></label>
                            </div>
                            <div class="row" runat="server" id="row142">
                                <label class="col-sm-9 col-6">Total : </label>
                                <label class="col-sm-3 col-6" runat="server" id="H142"></label>
                            </div>
                        </div>
                        <div runat="server" id="row144">
                            <div class="row">
                                <label class="col-sm-9 col-6">Assiette de perception : </label>
                                <label class="col-sm-3 col-6" runat="server" id="F144"></label>
                            </div>
                            <div class="row">
                                <label class="col-12 text-left">Jusqu'à 23.000€ :</label>
                                <label runat="server" id="F146" class="col-4 col-sm-3"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" id="G146" class="col-6 col-sm-3"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" id="H146" class="col-6 col-sm-3"></label>
                            </div>
                            <div class="row">
                                <label class="col-12 text-left">De 23.000 à 107.000€ : </label>
                                <label runat="server" id="F147" class="col-4 col-sm-3"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" id="G147" class="col-6 col-sm-3"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" id="H147" class="col-6 col-sm-3"></label>
                            </div>
                            <div class="row">
                                <label class="col-12 text-left">De 107.000 à 200.000€ : </label>
                                <label runat="server" id="F148" class="col-4 col-sm-3"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" id="G148" class="col-6 col-sm-3"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" id="H148" class="col-6 col-sm-3"></label>
                            </div>
                            <div class="row">
                                <label class="col-12 text-left">Supérieur à 200.000€ : </label>
                                <label runat="server" id="F149" class="col-4 col-sm-3"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label runat="server" id="G149" class="col-6 col-sm-3"></label>
                                <label class="col-2 col-offset-4 col-sm-2 col-sm-offset-0">=</label>
                                <label runat="server" id="H149" class="col-6 col-sm-3"></label>
                            </div>
                            <div class="row" runat="server" id="row151">
                                <label class="col-sm-9 col-6">Minimum de perception : </label>
                                <label class="col-sm-3 col-6" runat="server" id="F151"></label>
                            </div>
                            <div class="row" runat="server" id="row152">
                                <label class="col-sm-9 col-6">Total : </label>
                                <label class="col-sm-3 col-6" runat="server" id="F152"></label>
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
    <script type="text/javascript" src="Scripts/BF08.js"></script>
</asp:Content>