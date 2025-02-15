<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="BF04.aspx.cs" Inherits="NotaliaOnline.BF04" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #select2-ddl2-container, #select2-ddl3-container, #select2-ddl4-container, #select2-ddl5-container {
            text-align: center;
        }

        ul#select2-ddl2-results li, ul#select2-ddl3-results li, ul#select2-ddl4-results li, ul#select2-ddl5-results li {
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
                Les baux spécifiques (+ de 12 ans)
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
                        Sur le type de bail
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Sur le type de bail :</label>
                            <div class="col-sm-6 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl1" CssClass="select2">
                                    <asp:ListItem Value="1" Text="Bail rural à long terme"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Bail emphythéotique simple"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Bail emphytéotique (production d'immeubles)"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Bail emphytéotique administratif ( L.2341-1 du CGPPP )"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Bail à construction"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Bail à réhabilitation"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Bail immobilier plus de 12 ans"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group text-center">
                            <label class="col-12" id="lbl_ddl1_description"></label>
                        </div>
                    </div>
                </div>
                <div class="sub-card" id="sub_Donnees_economiques_du_bail">
                    <div class="card-header">
                        Donnees economiques du bail
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk1" CssClass="checkbox-custom col-sm-8 col-sm-offset-4 col-12"
                                Text="Application de la TVA au bail ?" />
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Durée en années :</label>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl2" CssClass="select2">
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
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Durée en années :</label>
                            <div class="col-sm-3 col-12">
                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl3" CssClass="select2">
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
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right" id="lblZone01">Montant du loyer annuel :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone01"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right" id="lblZone02">Montant des charges annuelles :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone02"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right" id="lblZone03">Option - Valeur locative réelle (cumulée sur la durée du bail) :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone03"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right" id="lblZone04">Crédit-bail (montant de l'investissement) :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone04"></asp:TextBox>
                                <i class="form-control-feedback fa fa-euro"></i>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="sub_Bail_emphyteotique">
                    <div class="sub-card">
                        <div class="card-header">
                            <span id="lbl_sub_Bail_emphyteotique">Bail emphyteotique (production d'immeubles)</span>
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Durée du bail :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl4" CssClass="select2">
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
                                        <asp:ListItem Value="70" Selected="True">70</asp:ListItem>
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
                                <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Montant des versements annuels (*) :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone05"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Valeur des constructions à édifier (HT) :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone06"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Choix de l'amortissement linéaire en nombre d'année :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddl5" CssClass="select2">
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
                                        <asp:ListItem Value="70" Selected="True">70</asp:ListItem>
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
                                <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Valeur d'amortissement remise chaque année :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtResultZone001" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Valeur résiduelle des constructions à remettre en fin de bail :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtResultZone002" Enabled="False"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-form-label col-sm-4 col-12 text-left text-sm-right">Option - Valeur résiduelle suivant estimation des parties :</label>
                                <div class="col-sm-3 col-12">
                                    <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone07"></asp:TextBox>
                                    <i class="form-control-feedback fa fa-euro"></i>
                                </div>
                            </div>
                            <div class="row form-group text-center">
                                <label class="col-12">(*) : Il s'agit de tous les versements effectués à quelque titre que ce soit (loyer, indemnités, charges, etc … à l'exclusion des charges d'entretien et de réparations).</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="sub-card" id="sub_Option_pour_la_TVA">
                    <div class="card-header">
                        Option pour la TVA
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk2" CssClass="checkbox-custom col-sm-8 col-sm-offset-4 col-12"
                                Text="Le bailleur exerce-t-il une option pour la TVA (Art. 260.5 du CGI) ?" />
                        </div>
                        <div class="row form-group text-center" id="lbl_Option_pour_la_TVA">
                            <label class="col-12">Le bailleur doit assurer le paiement de la TVA au taux légal sur relevé CA3 calculée sur le montant des loyers et la valeur de reprise des immeubles déduction faite du montant des loyers et de l'indemnité de reprise au profit du preneur.</label>
                        </div>
                    </div>
                </div>
                <div class="sub-card">
                    <div class="card-header">
                        Conditions particulieres
                    </div>
                    <div class="card-body">
                        <div class="row form-group">
                            <label class="col-form-label col-sm-4 col-12 text-left text-sm-right"></label>
                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chk3" CssClass="checkbox-custom col-sm-8 col-sm-offset-4 col-12"
                                Text="Cautionnement par un tiers dans l'acte principal ?" />
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-12 text-left text-sm-right col-form-label ">Montant de la somme cautionnée réellement :</label>
                            <div class="col-sm-3 col-12">
                                <asp:TextBox CssClass="form-control numberFormatFR" runat="server" ClientIDMode="Static" ID="txtZone08"></asp:TextBox>
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
        <div runat="server" id="divTaxation" visible="False">
            <div class="card">
                <div class="card-header">
                    Total des droits et frais
                </div>
                <div class="card-body text-right">
                    <div class="row">
                        <label class="col-6 col-sm-6">Emoluments HT du notaire :</label>
                        <label runat="server" clientidmode="Static" id="G47" class="col-6 col-sm-3"></label>
                    </div>
                    <div class="row">
                        <label class="col-6 col-sm-6">Débours : </label>
                        <label runat="server" clientidmode="Static" id="G46" class="col-6 col-sm-3"></label>
                    </div>
                    <div class="row">
                        <label class="col-6 col-sm-6">Trésor public : </label>
                        <label runat="server" clientidmode="Static" id="G45" class="col-6 col-sm-3"></label>
                    </div>
                    <div class="row fa-2x">
                        <label class="col-6 col-sm-6">Montant des frais : </label>
                        <label runat="server" clientidmode="Static" id="G48" class="col-6 col-sm-3"></label>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    Détail des frais
                </div>
                <div class="card-body text-right">
                    <div class="sub-card" runat="server" id="row12">
                        <div class="card-header">
                            Emoluments proportionnels
                        </div>
                        <div class="card-body text-right">
                            <div class="row" runat="server" id="row13">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E13"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F13"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G13"></label>
                            </div>
                            <div class="row" runat="server" id="row14">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E14"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F14"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G14"></label>
                            </div>
                            <div class="row" runat="server" id="row15">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E15"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F15"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G15"></label>
                            </div>
                            <div class="row" runat="server" id="row16">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E16"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F16"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G16"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-3">Total :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F17"></label>
                            </div>
                            <div class="row" runat="server" id="row18">
                                <label class="col-6 col-sm-8">Minimum de perception HT : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G18"></label>
                            </div>
                            <div class="row" runat="server" id="row19">
                                <label class="col-6 col-sm-8">Total HT :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G19"></label>
                            </div>
                            <div class="row" runat="server" id="row20">
                                <label class="col-6 col-sm-8">Emoluments de caution (HT) :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G20"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Total des émoluments du notaire :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G21"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card">
                        <div class="card-header">
                            Récapitulatif et calcul de la TVA
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-6 col-sm-8">Total HT des émoluments du notaire :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G23"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Emoluments de formalités :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G24"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">TOTAL Hors T.V.A :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G25"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">TVA :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G26"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Total TTC :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G27"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card">
                        <div class="card-header">
                            Débours
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-6 col-sm-8">Débours :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G30"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card">
                        <div class="card-header">
                            Trésor public
                        </div>
                        <div class="card-body">
                            <div runat="server" id="row34">
                                <div class="row text-left">
                                    <label class="col-12 fa-2x">Droit d'enregistrement : </label>
                                </div>
                                <div class="row">
                                    <label class="col-6 col-sm-8">Droits fixes :</label>
                                    <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G35"></label>
                                </div>
                            </div>
                            <div runat="server" id="row36">
                                <div class="row text-left">
                                    <label class="col-12 fa-2x">Taxe de Publicité foncière :</label>
                                </div>
                                <div class="row" runat="server" id="row37">
                                    <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E37"></label>
                                    <label class="col-2 col-sm-1">sur</label>
                                    <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F37"></label>
                                    <label class="col-6 col-sm-1">=</label>
                                    <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G37"></label>
                                </div>
                                <div class="row" runat="server" id="row38">
                                    <label class="col-6 col-sm-8">Minimum de perception : </label>
                                    <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G38"></label>
                                </div>
                                <div class="row text-center" runat="server" id="row39">
                                    <label class="col-12">Exonération.</label>
                                </div>
                            </div>
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Contribution pour la sécurité immobilière (art. 879 du CGI) : </label>
                            </div>
                            <div class="row" runat="server" id="row41">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E41"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F41"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G41"></label>
                            </div>
                            <div class="row" runat="server" id="row42">
                                <label class="col-6 col-sm-8">Minimum de perception : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G42"></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div runat="server" id="divTaxationC" visible="False">
            <div class="card">
                <div class="card-header">
                    Total des droits et frais
                </div>
                <div class="card-body text-right">
                    <div class="row">
                        <label class="col-6 col-sm-6">Emoluments HT du notaire :</label>
                        <label runat="server" clientidmode="Static" id="G124" class="col-6 col-sm-3"></label>
                    </div>
                    <div class="row">
                        <label class="col-6 col-sm-6">Débours : </label>
                        <label runat="server" clientidmode="Static" id="G123" class="col-6 col-sm-3"></label>
                    </div>
                    <div class="row">
                        <label class="col-6 col-sm-6">Trésor public : </label>
                        <label runat="server" clientidmode="Static" id="G122" class="col-6 col-sm-3"></label>
                    </div>
                    <div class="row fa-2x">
                        <label class="col-6 col-sm-6">Montant des frais : </label>
                        <label runat="server" clientidmode="Static" id="G125" class="col-6 col-sm-3"></label>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    Détail des frais
                </div>
                <div class="card-body text-right">
                    <div class="sub-card" runat="server" id="row60">
                        <div class="card-header">
                            Emoluments du notaire sur les versements des 5 premières années
                        </div>
                        <div class="card-body">
                            <div class="row text-center">
                                <label class="col-12" runat="server" clientidmode="Static" id="E61"></label>
                            </div>
                            <div class="row text-center">
                                <label class="col-12" runat="server" clientidmode="Static" id="F61"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Base :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G61"></label>
                            </div>
                            <div class="row" runat="server" id="row62">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E62"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F62"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G62"></label>
                            </div>
                            <div class="row" runat="server" id="row63">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E63"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F63"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G63"></label>
                            </div>
                            <div class="row" runat="server" id="row64">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E64"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F64"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G64"></label>
                            </div>
                            <div class="row" runat="server" id="row65">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E65"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F65"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G65"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-3">Total :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F66"></label>
                            </div>
                            <div class="row" runat="server" id="row67">
                                <label class="col-6 col-sm-8">Minimum de perception HT : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G67"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Total HT :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G68"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card" runat="server" id="row70">
                        <div class="card-header">
                            Emoluments du notaire sur le surplus des versements après correction au-delà des 5 années
                        </div>
                        <div class="card-body">
                            <div runat="server" id="row72">
                                <div class="row text-left">
                                    <label class="col-12 fa-2x">b) Versements à effectuer entre la 6ème et la 20ème année :</label>
                                </div>
                                <div class="row text-center">
                                    <label class="col-12" runat="server" clientidmode="Static" id="E72"></label>
                                </div>
                                <div class="row text-center">
                                    <label class="col-12" runat="server" clientidmode="Static" id="F72"></label>
                                </div>
                                <div class="row">
                                    <label class="col-12 col-sm-4" runat="server" clientidmode="Static" id="F73"></label>
                                    <label class="col-6 col-sm-4">Retenus pour :</label>
                                    <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G73"></label>
                                </div>
                            </div>
                            <div runat="server" id="row74">
                                <div class="row text-left">
                                    <label class="col-12 fa-2x">b1) Versements à effectuer entre la 21ème et la 60ème année :</label>
                                </div>
                                <div class="row text-center">
                                    <label class="col-12" runat="server" clientidmode="Static" id="E74"></label>
                                </div>
                                <div class="row text-center">
                                    <label class="col-12" runat="server" clientidmode="Static" id="F74"></label>
                                </div>
                                <div class="row">
                                    <label class="col-12 col-sm-4" runat="server" clientidmode="Static" id="F75"></label>
                                    <label class="col-6 col-sm-4">Retenus pour 1/2 :</label>
                                    <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G75"></label>
                                </div>
                            </div>
                            <div runat="server" id="row76">
                                <div class="row text-left">
                                    <label class="col-12 fa-2x">b2) Versements à effectuer entre la 61ème et le terme du bail :</label>
                                </div>
                                <div class="row text-center">
                                    <label class="col-12" runat="server" clientidmode="Static" id="E76"></label>
                                </div>
                                <div class="row text-center">
                                    <label class="col-12" runat="server" clientidmode="Static" id="F76"></label>
                                </div>
                                <div class="row">
                                    <label class="col-12 col-sm-4" runat="server" clientidmode="Static" id="F77"></label>
                                    <label class="col-6 col-sm-4">Retenus pour 1/4 :</label>
                                    <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G77"></label>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Base :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G79"></label>
                            </div>
                            <div class="row" runat="server" id="row80">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E80"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F80"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G80"></label>
                            </div>
                            <div class="row" runat="server" id="row81">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E81"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F81"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G81"></label>
                            </div>
                            <div class="row" runat="server" id="row82">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E82"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F82"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G82"></label>
                            </div>
                            <div class="row" runat="server" id="row83">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E83"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F83"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G83"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-3">Total :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F84"></label>
                            </div>
                            <div class="row" runat="server" id="row85">
                                <label class="col-6 col-sm-8">Minimum de perception HT : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G85"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Total HT :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G86"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card" runat="server" id="row88">
                        <div class="card-header">
                            Emoluments du notaire sur la valeur résiduelle des constructions
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-6 col-sm-8">Base :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G89"></label>
                            </div>
                            <div class="row" runat="server" id="row90">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E90"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F90"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G90"></label>
                            </div>
                            <div class="row" runat="server" id="row91">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E91"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F91"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G91"></label>
                            </div>
                            <div class="row" runat="server" id="row92">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E92"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F92"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G92"></label>
                            </div>
                            <div class="row" runat="server" id="row93">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E93"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F93"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G93"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-3">Total :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F94"></label>
                            </div>
                            <div class="row" runat="server" id="row95">
                                <label class="col-6 col-sm-8">Minimum de perception HT : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G95"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Total HT :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G96"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card" runat="server" id="row98">
                        <div class="card-header">
                            Emoluments du notaire - Récapitulatif
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-6 col-sm-8">Emoluments du notaire (HT) :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G100"></label>
                            </div>
                            <div class="row" runat="server" id="row101">
                                <label class="col-6 col-sm-8">Emoluments de caution (HT) :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G101"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Total des émoluments du notaire :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G102"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card">
                        <div class="card-header">
                            Récapitulatif et calcul de la TVA
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-6 col-sm-8">Total HT des émoluments du notaire :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G105"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Emoluments de formalités :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G106"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">TOTAL Hors T.V.A :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G107"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">TVA :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G108"></label>
                            </div>
                            <div class="row">
                                <label class="col-6 col-sm-8">Total TTC :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G109"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card">
                        <div class="card-header">
                            Débours
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-6 col-sm-8">Débours :</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G113"></label>
                            </div>
                        </div>
                    </div>
                    <div class="sub-card">
                        <div class="card-header">
                            Trésor public
                        </div>
                        <div class="card-body">
                            <div class="row text-left">
                                <label class="col-12 fa-2x">Contribution pour la sécurité immobilière (art. 879 du CGI) : </label>
                            </div>
                            <div class="row" runat="server" id="row118">
                                <label class="col-4 col-sm-2" runat="server" clientidmode="Static" id="E118"></label>
                                <label class="col-2 col-sm-1">sur</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="F118"></label>
                                <label class="col-6 col-sm-1">=</label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G118"></label>
                            </div>
                            <div class="row" runat="server" id="row119">
                                <label class="col-6 col-sm-8">Minimum de perception : </label>
                                <label class="col-6 col-sm-4" runat="server" clientidmode="Static" id="G119"></label>
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
    <script type="text/javascript" src="Scripts/BF04.js"></script>
</asp:Content>
