$(".select2").select2({
    //placeholder: "Sélectionnez un acte ...",
    allowClear: true,
    minimumResultsForSearch: -1
});

if ($("#postback").val() === "false") {
    ShowHide_input_form($("#btnInputForm1"));
}
$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

var regex = /^[-]?[\d ]*$|^[-]?[\d ]+[,]?\d*$/;
var ddl1 = $("#ddl1Choix");
var ddl3 = $("#ddl3Agriculture");
var ddl4 = $("#ddl4LogementsSociaux");
var ddl5 = $("#ddl5Vendeur");
var ddl6 = $("#ddl6Acquereur");
var ddl7 = $("#ddl7AucuneOption");
var ddl8 = $("#ddl8TvasurleprixTotal");
var ddl9 = $("#ddl9AucumEngagement");
var ddl10 = $("#ddl10AucumEngagement");
var txtZone1 = $("#txtZone1");
var chkBox1 = $("#chkBox1");
var txtZone2 = $("#txtZone2");
var txtZone3 = $("#txtZone3");
var txtZone4 = $("#txtZone4");
var txtZone5 = $("#txtZone5");
var txtZone6 = $("#txtZone6");
var chkBox2 = $("#chkBox2");
var chkBox3 = $("#chkBox3");
var txtLabel1 = $("#txtLabel1");
var txtLabel2 = $("#txtLabel2");
var txtLabel3 = $("#txtLabel3");
var txtLabel4 = $("#txtLabel4");
var txtLabel5 = $("#txtLabel5");
var ddl2 = $("#ddl2Situation");

function showHideSectionCalcul_de_la_tva_sur_marge_en_raison_de_option() {
    var condition = false;
    if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "1" && ddl7.val() === "3") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "2" && ddl9.val() === "1" && ddl7.val() === "3") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "2" && ddl9.val() === "2" && ddl7.val() === "3") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "2" && ddl6.val() === "1" && ddl8.val() === "2") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "2" && ddl6.val() === "2" && ddl10.val() === "1" && ddl8.val() === "2") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "2" && ddl6.val() === "2" && ddl10.val() === "2" && ddl8.val() === "2") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "2" && ddl6.val() === "2" && ddl10.val() === "3" && ddl8.val() === "2") {
        condition = true;
    } else {
        condition = false;
    }

    if (condition === true) //if (condition === true && txtvalue > 0)
    {
        $("#section_Calcul_de_la_TVA").show();
    } else {
        $("#section_Calcul_de_la_TVA").hide();
    }
}

function calculateLabel01() {
    if (chkBox1[0].checked) {
        var value1 = ConvertToFloat(txtZone1.val().trim());
        var value2 = ConvertToFloat(txtZone2.val().trim());
        var results = value1 - value2;
        txtLabel1.val(results);
    } else {
        txtLabel1.val(txtZone1.val().trim());
    }
    $("#txtLabel1").change();
    calculateLabel05();
}

function calculateLabel02() {
    try {
        var value1 = ConvertToFloat(txtLabel1.val().trim());
        var value2 = ConvertToFloat(txtZone4.val().trim());
        var result = value1 - value2;
        txtLabel2.val(result);
        calculateLabel04();
    } catch (e) {
        console.error(e);
    }
}

function calculateLabel03() {
    if (ddl2.length) {
        if (ddl2.val() === "1" | ddl2.val() === "2" | ddl2.val() === "5") {
            txtLabel3.val("20");
        } else if (ddl2.val() === "3") {
            txtLabel3.val("8,5");
        } else if (ddl2.val() === "4") {
            txtLabel3.val("0");
        } else {
            txtLabel3.val("0");
        }
        calculateLabel04();
    } else error("element " + "'" + $(this).selector + "'" + " not exist");
}

function calculateLabel04() {
    try {
        var LABEL02 = ConvertToFloat(txtLabel2.val().trim()); //getNumberFromTextBox(removeLastComma(txtLabel2.val().trim()));
        var LABEL03 = ConvertToFloat(txtLabel3.val().trim()); //getNumberFromTextBox(removeLastComma(txtLabel3.val().trim()));

        //LABEL02 = returnEnNumberCalc(LABEL02);
        //LABEL03 = returnEnNumberCalc(LABEL03);

        var result = (LABEL02 - (LABEL02 / (1 + LABEL03 / 100))).toFixed(2);
        txtLabel4.val(result);
        calculateLabel05();
    } catch (e) {
        console.error(e);
    }
}

function calculateLabel05() {
    try {
        var LABEL01 = ConvertToFloat(txtLabel1.val().trim());
        var LABEL04 = ConvertToFloat(txtLabel4.val().trim());
        var result = (LABEL01 - LABEL04).toFixed(2);;
        txtLabel5.val(result);
    } catch (e) {
        console.error(e);
    }
}

function showHideDroit_de_mutation() {;
    var condition = false;
    if (ddl1.val() === "1" && ddl5.val() === "1" && ddl6.val() === "1") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "1" && ddl6.val() === "2" && ddl9.val() === "1") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "1" && ddl7.val() === "1") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "1" && ddl7.val() === "2") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "1" && ddl7.val() === "3") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "2" && ddl9.val() === "1" && ddl7.val() === "1") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "2" && ddl9.val() === "1" && ddl7.val() === "2") {
        condition = true;
    }
    else if (ddl1.val() === "1" && ddl5.val() === "2" && ddl6.val() === "2" && ddl9.val() === "1" && ddl7.val() === "3") {
        condition = true;
    }
    else if (ddl1.val() === "2" && ddl5.val() === "1" && ddl6.val() === "1") {
        condition = true;
    }
    else if (ddl1.val() === "2" && ddl5.val() === "1" && ddl6.val() === "2" && ddl9.val() === "1") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "1" && ddl6.val() === "1") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "1" && ddl6.val() === "2" && ddl10.val() === "1") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "2" && ddl6.val() === "1" && ddl8.val() === "2") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "2" && ddl6.val() === "2" && ddl10.val() === "1" && ddl8.val() === "2") {
        condition = true;
    }
    else if (ddl1.val() === "3" && ddl5.val() === "2" && ddl6.val() === "2" && ddl10.val() === "2" && ddl8.val() === "2") {
        condition = true;
    } else
        condition = false;

    if (condition === true)
        $("#section_Droit_de_mutation").show();
    else
        $("#section_Droit_de_mutation").hide();
}

$("#ddl1Choix").change(function () {
    if ($(this).length) {
        if ($(this).val() === "5") {
            ShowElements([ddl3], "div.sub-card");
            HideElements([ddl4, ddl5, ddl6], "div.sub-card");
        } else if ($(this).val() === "6") {
            ShowElements([ddl4], "div.sub-card");
            HideElements([ddl3, ddl5, ddl6], "div.sub-card");
        } else if ($(this).val() === "1" | $(this).val() === "2" | $(this).val() === "3") {
            ShowElements([ddl5, ddl6], "div.sub-card");
            HideElements([ddl3, ddl4], "div.sub-card");
        } else {
            HideElements([ddl3, ddl4, ddl5, ddl6, ddl7], "div.sub-card");
        }

        if ($(this).val() === "1" && ddl5.val() === "2") {
            ShowElements([ddl7], "div.sub-card");
        } else {
            HideElements([ddl7, ddl8], "div.sub-card");
        }

        if ($(this).val() === "3" && ddl5.val() === "2") {
            ShowElements([ddl8], "div.sub-card");
        } else {
            HideElements([ddl8], "div.sub-card");
        }

        if ($(this).val() === "3" && ddl6.val() === "2") {
            ShowElements([ddl10], "div.sub-card");
        } else {
            HideElements([ddl10], "div.sub-card");
        }

        if ((ddl1.val() === "1" && ddl6.val() === "2") || (ddl1.val() === "2" && ddl5.val() === "1" && ddl6.val() === "2")) {
            ShowElements([ddl9], "div.sub-card");
        } else {
            HideElements([ddl9], "div.sub-card");
        }

        if ($(this).val() === "1" | $(this).val() === "2") {
            ShowElements([chkBox1], "div.row");
        } else {
            HideElements([chkBox1], "div.row");
        }

        if ($(this).val() === "1" && chkBox1[0].checked) {
            ShowElements([txtZone2], "div.row");
        } else {
            HideElements([txtZone2], "div.row");
        }

        if ($(this).val() === "7") {
            ShowElements([txtZone3], "div.row");
        } else {
            HideElements([txtZone3], "div.row");
        }

        if (ddl1.val() === "1" && ddl2.val() === "5")
            ShowElements([chkBox3], "div.row");
        else
            HideElements([chkBox3], "div.row");

        showHideSectionCalcul_de_la_tva_sur_marge_en_raison_de_option();
        showHideDroit_de_mutation();
    }
}).change();

$("#ddl2Situation").change(function () {
    if (ddl1.val() === "1" && ddl2.val() === "5") {
        ShowElements([chkBox3], "div.row");
    } else {
        HideElements([chkBox3], "div.row");
    }
    calculateLabel03();
}).change();

$("#ddl5Vendeur").change(function () {
    if ($(this).length) {
        if (ddl1.val() === "1" && $(this).val() === "2") {
            ShowElements([ddl7], "div.sub-panel");
        }
        else if (ddl1.val() === "3" && $(this).val() === "2") {
            ShowElements([ddl8], "div.sub-panel");
        } else {
            HideElements([ddl7, ddl8], "div.sub-panel");
        }
        if ((ddl1.val() === "1" && ddl6.val() === "2") || (ddl1.val() === "2" && ddl5.val() === "1" && ddl6.val() === "2")) {
            ShowElements([ddl9], "div.sub-panel");
        } else {
            HideElements([ddl9], "div.sub-panel");
        }
        showHideSectionCalcul_de_la_tva_sur_marge_en_raison_de_option();
        showHideDroit_de_mutation();
    }
}).change();

$("#ddl6Acquereur").change(function () {
    if ($(this).length) {
        if (ddl1.val() === "3" && $(this).val() === "2") {
            ShowElements([ddl10], "div.sub-panel");
        } else {
            HideElements([ddl10], "div.sub-panel");
        }
        if ((ddl1.val() === "1" && ddl6.val() === "2") || (ddl1.val() === "2" && ddl5.val() === "1" && ddl6.val() === "2")) {
            ShowElements([ddl9], "div.sub-panel");
        } else {
            HideElements([ddl9], "div.sub-panel");
        }
        showHideSectionCalcul_de_la_tva_sur_marge_en_raison_de_option();
        showHideDroit_de_mutation();
    }
});

$("#ddl7AucuneOption, #ddl8TvasurleprixTotal, #ddl9AucumEngagement, #ddl10AucumEngagement").change(function () {
    showHideSectionCalcul_de_la_tva_sur_marge_en_raison_de_option();
    showHideDroit_de_mutation();
}).change();

$("#chkBox1").change(function () {
    if (this.checked && ddl1.val() === "1") {
        ShowElements([txtZone2], "div.row");
    } else {
        HideElements([txtZone2], "div.row");
    }
    calculateLabel01();
}).change();

$("#txtZone1").on('keyup change', function () {
    if (ddl1.val() === "7") {
        ShowElements([txtZone3], "div.row");
    } else {
        HideElements([txtZone3], "div.row");
    }
    calculateLabel01();
    showHideSectionCalcul_de_la_tva_sur_marge_en_raison_de_option();
    showHideDroit_de_mutation();
});

$("#txtZone2").on('keyup change', function () {
    calculateLabel01();
});

$("#txtZone4").on('keyup change', function () {
    calculateLabel02();
    calculateLabel04();
});

$("#txtLabel1").change(function () {
    calculateLabel02();
}).change();

$("#chkBox2").change(function () {
    if (this.checked) {
        ShowElements([txtZone5], "div.row");
    } else {
        HideElements([txtZone5], "div.row");
    }
}).change();

$("#chkBox3").change(function () {
    if (this.checked) {
        ShowElements([txtZone6], "div.row");
    } else {
        HideElements([txtZone6], "div.row");
    }
}).change();

function htmlReport() {
    var html = "", none = "none";
    //Saisie
    html += "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style:inset' cellspacing='0'>";
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Les ventes immobilières</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Références du dossier</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Dossier :</td>";
    html += "<td colspan='2'>" + $("#txtDossier").val() + "</td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Date de signature :</td>";
    html += "<td colspan='2'>" + $("#txtDateSignature").val() + "</td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Rédacteur :</td>";
    html += "<td colspan='2'>" + $("#txtRedacteur").val() + "</td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";

    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Saisie</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Nature et situation du Bien vendu</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Choix :</td>";
    html += "<td width='100%' align='left' colspan='2'>" + $("#ddl1Choix option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Situation :</td>";
    html += "<td width='100%' align='left' colspan='2'>" + $("#ddl2Situation option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Agriculture
    if (ddl3.css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Agriculture</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Agriculture :</td>";
        html += "<td width='100%' align='left' colspan='2'>" + $("#ddl3Agriculture option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Logements sociaux
    if (ddl4.css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Logements sociaux</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Logements sociaux :</td>";
        html += "<td width='100%' align='left' colspan='2'>" + $("#ddl4LogementsSociaux option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Qualités des parties
    if (ddl5.css('display') !== none && ddl6.css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Qualités des parties</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Vendeur :</td>";
        html += "<td width='100%' align='left' colspan='2'>" + $("#ddl5Vendeur option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Acquéreur :</td>";
        html += "<td width='100%' align='left' colspan='2'>" + $("#ddl6Acquereur option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Option fiscale du vendeur au regard de la TVA (bien autre que terrain à batir)
    if (ddl7.css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Option fiscale du vendeur au regard de la TVA (bien autre que terrain à batir)</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Option fiscale du vendeur au regard de la TVA (bien autre que terrain à batir) :</td>";
        html += "<td width='100%' align='right'>" + $("#ddl7AucuneOption option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }

    //Option fiscale du vendeur au regard de la TVA (terrain à bâtir ou assimilé)
    if (ddl8.css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Option fiscale du vendeur au regard de la TVA (terrain à bâtir ou assimilé)</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Option fiscale du vendeur au regard de la TVA (terrain à bâtir ou assimilé) :</td>";
        html += "<td width='100%' align='right'>" + $("#ddl8TvasurleprixTotal option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }

    //Engagement de l'Acquéreur assujetti (terrain à bâtir ou assimilé)
    if (ddl9.css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Engagement de l'Acquéreur assujetti (bien autre que terrain à batir)</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Engagement de l'Acquéreur assujetti (bien autre que terrain à batir) :</td>";
        html += "<td width='100%' align='right'>" + $("#ddl9AucumEngagement option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }

    //Engagement de l'Acquéreur assujetti (terrain à bâtir ou assimilé)
    if (ddl10.css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Engagement de l'Acquéreur assujetti (terrain à bâtir ou assimilé)</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Engagement de l'Acquéreur assujetti (terrain à bâtir ou assimilé) :</td>";
        html += "<td width='100%' align='right'>" + $("#ddl10AucumEngagement option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }

    //Données économiques
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Données économiques</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Prix de vente :</td>";
    html += "<td width='100%' align='right'>" + txtZone1.val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if (txtZone2.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Prix des meubles compris dans le prix global :</td>";
        html += "<td width='100%' align='right'>" + txtZone2.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }

    if (txtZone3.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Prix symbolique - Valeur vénale :</td>";
        html += "<td width='100%' align='right'>" + txtZone3.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";

    if ($('#section_Calcul_de_la_TVA').css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Calcul de la TVA sur marge en raison de l'option</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";

        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Prix de vente exprimé tenant compte des charges :</td>";
        html += "<td width='100%' align='right'>" + txtLabel1.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";

        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Prix d'achat attesté par le vendeur :</td>";
        html += "<td width='100%' align='right'>" + txtZone4.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";

        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Marge taxable :</td>";
        html += "<td width='100%' align='right'>" + txtLabel2.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";

        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Taux de la Tva applicable à l'opération :</td>";
        html += "<td width='100%' align='right'>" + txtLabel3.val() + " %</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";

        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>TVA sur marge :</td>";
        html += "<td width='100%' align='right'>" + txtLabel4.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";

        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Base d'imposition des droits :</td>";
        html += "<td width='100%' align='right'>" + txtLabel5.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }

    //Droits de mutation - Taxes complémentaires
    if (($('#section_Droit_de_mutation').css('display') !== none && chkBox2.css('display') !== none) ||
        ($('#section_Droit_de_mutation').css('display') !== none && chkBox3.css('display') !== none)) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Droits de mutation - Taxes complémentaires</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        if (txtZone5.css('display') !== none) {
            html += "<tr>";
            html += "<td width='40%' colspan='2' align='right'>Taux de la taxe départementale voté :</td>";
            html += "<td width='100%' align='right'>" + txtZone5.val() + " %</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
        }
        if (txtZone6.css('display') !== none) {
            html += "<tr>";
            html += "<td width='40%' colspan='2' align='right'>Partie du prix des locaux soumis à la taxe additionnelle :</td>";
            html += "<td width='100%' align='right'>" + txtZone6.val() + " €</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
        }
        html += "<tr><td colspan='4'></td></tr>";
    }

    //Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";

    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Emolument de formalités HT :</td>";
    html += "<td width='100%' align='right'>" + $('#txtEmolument_de_formalités_HT').val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";

    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Débours :</td>";
    html += "<td width='100%' align='right'>" + $('#txtDébours').val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</table>";
    $('#hdSaisie').val(html.toString());
}

google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    var options = {
        title: '', //'Répartition des frais',
        is3D: true,
        colors: ['#009900', '#e6ac00', '#8600b3'],
        legend: {
            position: 'labeled',
            alignment: 'center',
            textStyle: {
                fontSize: 16,
                color: '#000000'
            }
        },
        pieSliceText: 'none',
        width: 530,
        height: 300,
        sliceVisibilityThreshold: 0,
        chartArea: {
            bottom: 0,
            top: 0,
            left: 0,
            right: 0
        }
    };
    var data = google.visualization.arrayToDataTable([
        ['Taxation', 'Total Taxation'],
        ['Emoluments HT du notaire', parseFloat($('#lblG58').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#lblG57').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#lblG59').text().replace('€', '').replace(' ', ''))]
    ]);
    var chart = new google.visualization.PieChart(document.getElementById('piechartContainer'));
    chart.draw(data, options);
    $('#hdPiechart').val(chart.getImageURI());
}

$(".btn-calculate, #btnSendEmail, #btnPrint").click(function () {
    htmlReport();
});