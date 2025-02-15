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

var $ddl1 = $("#ddl1"), $ddl2 = $("#ddl2"), $ddl3 = $("#ddl3"), $ddl4 = $("#ddl4"), $ddl5 = $("#ddl5");
var $chk1 = $("#chk1"), $chk2 = $("#chk2"), $chk3 = $("#chk3");
var $txtZone01 = $("#txtZone01"),
    $txtZone02 = $("#txtZone02"),
    $txtZone03 = $("#txtZone03"),
    $txtZone04 = $("#txtZone04"),
    $txtZone05 = $("#txtZone05"),
    $txtZone06 = $("#txtZone06"),
    $txtZone07 = $("#txtZone07"),
    $txtZone08 = $("#txtZone08");

$ddl1.change(function () {
    var value = parseInt($ddl1.val());
    $("#sub_Donnees_economiques_du_bail, #sub_Bail_emphyteotique, #sub_Option_pour_la_TVA").hide();
    switch (value) {
        case 1:
            $("#lbl_ddl1_description").text("Baux de 18 ans au moins ou de 25 ans au moins régis par les articles L416-1 et L416-3 du Code Rural.");
            $("#sub_Donnees_economiques_du_bail").show();
            ShowElements([$chk1, $ddl3, $txtZone01], "div.row");
            HideElements([$ddl4, $txtZone05, $txtZone06, $ddl5, $txtZone07, $ddl2, $txtZone02, $txtZone03, $txtZone04], "div.row");
            break;
        case 2:
            $("#lbl_ddl1_description").text("Bail constituant un droit réel spécial immobilier pour plus de 18 années avec un maximum de 99 ans incorporé au Code Rural (Art. L.451-1).");
            $("#sub_Donnees_economiques_du_bail").show();
            ShowElements([$chk1, $ddl2, $txtZone01, $txtZone02, $ddl2], "div.row");
            HideElements([$ddl4, $txtZone05, $txtZone06, $ddl5, $txtZone07, $ddl3, $txtZone03, $txtZone04], "div.row");
            break;
        case 4:
            $("#lbl_ddl1_description").text("Bail consistant en la mise à disposition d'un terrain appartenant à une collectivité publique en vue de réaliser un ouvrage ayant vocation en retour à être mis à disposition du bailleur pour une durée comprise entre 18 et 99 ans.");
            $("#sub_Donnees_economiques_du_bail").show();
            ShowElements([$chk1, $ddl2, $txtZone01, $txtZone02], "div.row");
            HideElements([$ddl4, $txtZone05, $txtZone06, $ddl5, $txtZone07, $ddl3, $txtZone04], "div.row");
            if ($ddl2.val() === "18" || $ddl2.val() === "19" || $ddl2.val() === "20") {
                ShowElements([$txtZone03], "div.row");
            } else {
                HideElements([$txtZone03], "div.row");
            }
            break;
        case 7:
            $("#lbl_ddl1_description").text("Bail pour un usage d'habitation ou professionnel établi pour une durée supérieure à 12 ans.");
            $("#sub_Donnees_economiques_du_bail").show();
            ShowElements([$chk1, $ddl2, $txtZone01], "div.row");
            HideElements([$ddl4, $txtZone05, $txtZone06, $ddl5, $txtZone07, $ddl3, $txtZone02, $txtZone03, $txtZone04], "div.row");
            break;
        //case 8:
        //    $("#lbl_ddl1_description").text("Bail donné par une société de crédit-bail à un utilisateur d'un bien acquis ou construit par elle.");
        //    $("#sub_Donnees_economiques_du_bail").show();
        //    ShowElements([$chk1, $ddl2, $ddl3, $txtZone01, $txtZone02, $txtZone04, $ddl2], "div.row");
        //    HideElements([$ddl4, $txtZone05, $txtZone06, $ddl5, $txtZone07, $ddl3, $txtZone03], "div.row");
        //    break;
        case 3:
            $("#lbl_ddl1_description").text("Bail constituant un droit réel spécial immobilier pour plus de 18 années avec un maximum de 99 ans en vue de la construction de bâtiments par le preneur. Le baillleur peut opter pour la TVA (260, 5ème du CGI).");
            $("#sub_Donnees_economiques_du_bail, #sub_Option_pour_la_TVA").show();
            ShowElements([$chk1, $ddl4, $txtZone01, $txtZone05, $txtZone06, $ddl5, $txtZone07, $ddl2, $txtZone02], "div.row");
            HideElements([$ddl3, $txtZone04], "div.row");
            if ($ddl2.val() === "18" || $ddl2.val() === "19" || $ddl2.val() === "20") {
                ShowElements([$txtZone03], "div.row");
            } else {
                HideElements([$txtZone03], "div.row");
            }
            break;
        case 5:
            $("#lbl_sub_Bail_emphyteotique").text("Bail a construction");
            $("#lbl_ddl1_description").text("Bail portant sur un terrain pour une durée comprise entre 18 ans et 99 ans sans prolongation par tacite reconduction comprenant engagement par le preneur à titre principal d'édifier des constructions (L. 251-1 à L. 251-9 du CCH).");
            $("#sub_Option_pour_la_TVA, #sub_Bail_emphyteotique").show();
            ShowElements([$ddl4, $txtZone05, $txtZone06, $ddl5, $txtZone07], "div.row");
            HideElements([$chk1, $ddl2, $ddl3, $txtZone01, $txtZone02, $txtZone03, $txtZone04], "div.row");
            break;
        case 6:
            $("#lbl_sub_Bail_emphyteotique").text("Bail a rehabilitation");
            $("#lbl_ddl1_description").text("Bail portant sur un terrain pour une durée comprise entre 18 ans et 99 ans sans prolongation par tacite reconduction comprenant engagement par le preneur à titre principal de réhabiliter des constructions (L. 252-1).");
            $("#sub_Bail_emphyteotique").show();
            ShowElements([$ddl4, $txtZone05, $txtZone06, $ddl5, $txtZone07], "div.row");
            HideElements([$chk1, $ddl2, $ddl3, $txtZone01, $txtZone02, $txtZone03, $txtZone04], "div.row");
            break;
        default:
    }
}).change();

$ddl2.change(function () {
    $ddl1.change();
}).change();

var value = parseInt($ddl4.val());
if (value >= 6) {
    $("#sub_b").show();
}
if (value >= 21) {
    $("#sub_b1").show();
}
if (value >= 61) {
    $("#sub_b2").show();
}

$chk1.change(function () {
    if ($(this).is(":checked")) {
        $("#lblZone01").text("Montant du loyer annuel TTC :");
        $("#lblZone02").text("Montant des charges annuelles TTC :");
        $("#lblZone03").text("Option - Valeur locative réelle TTC (cumulée sur la durée du bail) :");
        $("#lblZone04").text("Crédit-bail (montant TTC de l'investissement) :");
    } else {
        $("#lblZone01").text("Montant du loyer annuel :");
        $("#lblZone02").text("Montant des charges annuelles :");
        $("#lblZone03").text("Option - Valeur locative réelle (cumulée sur la durée du bail) :");
        $("#lblZone04").text("Crédit-bail (montant de l'investissement) :");
    }
}).change();

$chk2.change(function () {
    if ($(this).is(":checked")) {
        $("#lbl_Option_pour_la_TVA").show();
    } else {
        $("#lbl_Option_pour_la_TVA").hide();
    }
}).change();

$chk3.change(function () {
    if ($(this).is(":checked")) {
        ShowElements([$txtZone08], "div.row");
    } else {
        HideElements([$txtZone08], "div.row");
    }
}).change();

$('#txtZone06, #ddl4, #ddl5').on("keypress change input", function () {
    var ddl5Value = parseInt($('#ddl5').val());
    var txtZone06Value = $('#txtZone06').val() === "" ? 0 : parseFloat($('#txtZone06').val());
    var result001 = txtZone06Value / ddl5Value;
    $('#txtResultZone001').val(result001.toFixed(2).replace(".", ","));
    var ddl4Value = parseInt($('#ddl4').val());
    $('#txtResultZone002').val((txtZone06Value - (result001 * ddl4Value)).toFixed(2).replace(".", ","));
}).change();

//Chart
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
    var data, chart;
    if ("12478".indexOf($ddl1.val()) !== -1) {
        data = google.visualization.arrayToDataTable([
            ['Taxation', 'Total Taxation'],
            ['Emoluments HT du notaire', parseFloat($('#G47').text().replace('€', '').replace(' ', ''))],
            ['Débours', parseFloat($('#G46').text().replace('€', '').replace(' ', ''))],
            ['Trésor public', parseFloat($('#G45').text().replace('€', '').replace(' ', ''))]
        ]);
        chart = new google.visualization.PieChart(document.getElementById('piechartContainer'));
        chart.draw(data, options);
        $('#hdPiechart').val(chart.getImageURI());
    } else {
        data = google.visualization.arrayToDataTable([
            ['Taxation', 'Total Taxation'],
            ['Emoluments HT du notaire', parseFloat($('#G124').text().replace('€', '').replace(' ', ''))],
            ['Débours', parseFloat($('#G123').text().replace('€', '').replace(' ', ''))],
            ['Trésor public', parseFloat($('#G122').text().replace('€', '').replace(' ', ''))]
        ]);
        chart = new google.visualization.PieChart(document.getElementById('piechartContainer'));
        chart.draw(data, options);
        $('#hdPiechart').val(chart.getImageURI());
    }
}
function GenerateChart() {
    google.charts.setOnLoadCallback(drawChart);
}
//End Chart

function htmlReport() {
    var html = "", none = "none";
    html += "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Les baux spécifiques (+ de 12 ans)</font></b></td></tr>";
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
    //Saisie
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Saisie</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
    //Sur le type de bail
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Sur le type de bail</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Sur le type de bail :</td>";
    html += "<td width='100%' colspan='2'>" + $("#ddl1 option:selected").text() + "</td>";
    html += "</tr>";
    html += "<tr><td colspan='4' align='center'>" + $("#lbl_ddl1_description").text() + "</td></tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Donnees economiques du bail
    if ($("#sub_Donnees_economiques_du_bail").css("display") !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Donnees economiques du bail</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Application de la TVA au bail ?</td>";
        html += "<td align='right'>" + ($chk1.is(':checked') ? "OUI" : "NON") + "</td><td></td>";
        html += "</tr>";
        if ($ddl2.css("display") !== none) {
            html += "<tr>";
            html += "<td align='right' colspan='2'>Durée en années :</td>";
            html += "<td align='right'>" + $("#ddl2 option:selected").text() + "</td><td></td>";
            html += "</tr>";
        }
        if ($ddl3.css("display") !== none) {
            html += "<tr>";
            html += "<td align='right' colspan='2'>Durée en années :</td>";
            html += "<td align='right'>" + $("#ddl3 option:selected").text() + "</td><td></td>";
            html += "</tr>";
        }
        html += "<tr>";
        html += "<td align='right' colspan='2'>Montant du loyer annuel :</td>";
        html += "<td align='right'>" + $txtZone01.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Montant des charges annuelles :</td>";
        html += "<td align='right'>" + $txtZone02.val() + " €</td><td></td>";
        html += "</tr>";
        if ($txtZone03.css("display") !== none) {
            html += "<tr>";
            html += "<td align='right' colspan='2'>Option - Valeur locative réelle (cumulée sur la durée du bail) :</td>";
            html += "<td align='right'>" + $txtZone03.val() + " €</td><td></td>";
            html += "</tr>";
        }
        if ($txtZone04.css("display") !== none) {
            html += "<tr>";
            html += "<td align='right' colspan='2'>Crédit-bail (montant de l'investissement) :</td>";
            html += "<td align='right'>" + $txtZone04.val() + " €</td><td></td>";
            html += "</tr>";
        }
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Bail emphyteotique (production d'immeubles)
    if ($("#sub_Bail_emphyteotique").css("display") !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Bail emphyteotique (production d'immeubles)</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Durée du bail :</td>";
        html += "<td align='right'>" + $("#ddl4 option:selected").text() + "</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Montant des versements annuels (*) :</td>";
        html += "<td align='right'>" + $txtZone05.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Valeur des constructions à édifier (HT) :</td>";
        html += "<td align='right'>" + $txtZone06.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Choix de l'amortissement linéaire en nombre d'année :</td>";
        html += "<td align='right'>" + $("#ddl5 option:selected").text() + "</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Valeur d'amortissement remise chaque année :</td>";
        html += "<td align='right'>" + $("#txtResultZone001").val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Valeur résiduelle des constructions à remettre en fin de bail :</td>";
        html += "<td align='right'>" + $("#txtResultZone002").val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Option - Valeur résiduelle suivant estimation des parties :</td>";
        html += "<td align='right'>" + $txtZone07.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr><td colspan='4' align='center'>(*) : Il s'agit de tous les versements effectués à quelque titre que ce soit (loyer, indemnités, charges, etc … à l'exclusion des charges d'entretien et de réparations).</td></tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Option pour la TVA
    if ($("#sub_Option_pour_la_TVA").css("display") !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Option pour la TVA</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Le bailleur exerce-t-il une option pour la TVA (Art. 260.5 du CGI) ?</td>";
        html += "<td align='right'>" + ($chk2.is(':checked') ? "OUI" : "NON") + "</td><td></td>";
        html += "</tr>";
        if ($("#lbl_Option_pour_la_TVA").css("display") !== none) {
            html += "<tr><td colspan='4' align='center'>Le bailleur doit assurer le paiement de la TVA au taux légal sur relevé CA3 calculée sur le montant des loyers et la valeur de reprise des immeubles déduction faite du montant des loyers et de l'indemnité de reprise au profit du preneur.</td></tr>";
        }
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Conditions particulieres
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Conditions particulieres</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Cautionnement par un tiers dans l'acte principal ?</td>";
    html += "<td align='right'>" + ($chk3.is(':checked') ? "OUI" : "NON") + "</td><td></td>";
    html += "</tr>";
    if ($txtZone08.css("display") !== none) {
        html += "<tr>";
        html += "<td align='right' colspan='2'>Montant de la somme cautionnée réellement :</td>";
        html += "<td align='right'>" + $txtZone08.val() + " €</td><td></td>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Emolument de formalités HT :</td>";
    html += "<td align='right'>" + $("#txtEmolument_de_formalités_HT").val() + " €</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Débours :</td>";
    html += "<td align='right'>" + $("#txtDébours").val() + " €</td><td></td>";
    html += "</tr>";
    html += "</table>";
    $('#hdSaisie').val(html);
};

$('.btn-calculate, #btnSendEmail, #btnPrint').click(function () {
    htmlReport();
});