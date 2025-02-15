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

var $txtTaux = $("#txtTaux"), $ddl1 = $("#ddl1"), $ddl2 = $("#ddl2"), $txtZone01 = $("#txtZone01"), $txtZone02 = $("#txtZone02"), $txtZone03 = $("#txtZone03");
var taux = 0;
function ShowHideElements() {
    if ($ddl1.val() === "1") {
        if ($("#rad1Non").is(":checked")) {
            taux = 20;
        } else {
            taux = 10;
        }
    } else if ($ddl1.val() === "2") {
        if ($("#rad1Non").is(":checked")) {
            taux = 10;
        } else {
            taux = 5.5;
        }
    } else if ($ddl1.val() === "3") {
        if ($("#rad1Non").is(":checked")) {
            taux = 8.5;
        } else {
            taux = 2.1;
        }
    } else if ($ddl1.val() === "4") {
        taux = 0;
    }
    if ($("#rad1Oui").is(":checked") && $ddl1.val() === "1") {
        ShowElements([$ddl2], "div.row");
        switch ($ddl2.val()) {
            case "1":
                HideElements([$txtZone01], "div.row");
                taux = 10.0;
                break;
            case "2":
                HideElements([$txtZone01], "div.row");
                taux = 5.5;
                break;
            case "3":
                ShowElements([$txtZone01], "div.row");
                taux = $txtZone01.val() === "" ? 0 : parseFloat($txtZone01.val());
                break;
            default:
        }
    } else {
        HideElements([$("#ddl2"), $("#txtZone01")], "div.row");
    }
    $txtTaux.val(taux.toFixed(2).toString().replace(".", ","));
    taux = taux / 100;
    var value01 = $txtZone03.val() === "" ? 0 : parseFloat($txtZone03.val()) / (1 + taux);
    var value00 = $txtZone03.val() === "" ? 0 : parseFloat($txtZone03.val()) - value01;
    $("#value01").val(value01.toFixed(2).toString().replace(".", ","));
    $("#value00").val(value00.toFixed(2).toString().replace(".", ","));
}

$("#ddl1, #rad1Oui, #rad1Non, #ddl2").change(function () {
    ShowHideElements();
}).change();

$("#txtZone01, #txtZone03").on("keyup blur change input", function () {
    ShowHideElements();
}).change();

// Chart
google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    var options = {
        title: '',
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
        ["Emoluments du notaire", parseFloat($('#F104').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#F103').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#F102').text().replace('€', '').replace(' ', ''))]
    ]);
    var chart = new google.visualization.PieChart(document.getElementById('piechartContainer'));
    chart.draw(data, options);
    $('#hdPiechart').val(chart.getImageURI());
}
function GenerateChart() {
    google.charts.setOnLoadCallback(drawChart);
}
// End Chart

function htmlReport() {
    var html = "", none = "none";
    html += "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Programme de vente en l'état futur d'achèvement portant sur le secteur protégé</font></b></td></tr>";
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
    //Situation du programme - Détermination de la TVA
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Situation du programme - Détermination de la TVA</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Situation du bien vendu :</td>";
    html += "<td align='left' colspan='2'>" + $('#ddl1 option:selected').text() + "</td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Application de la TVA à taux réduit ?</td>";
    html += "<td align='left'>" + ($("#rad1Oui").is(":checked") ? "Oui" : "Non") + "</td><td></td>";
    html += "</tr>";
    if ($ddl2.css("display") !== none) {
        html += "<tr>";
        html += "<td align='right' colspan='2'>Sélectionnez le taux réduit à appliquer :</td>";
        html += "<td align='left' colspan='2'>" + $('#ddl2 option:selected').text() + "</td>";
        html += "</tr>";
    }
    if ($txtZone01.css("display") !== none) {
        html += "<tr>";
        html += "<td align='right' colspan='2'>Taux de TVA libre :</td>";
        html += "<td align='right'>" + $txtZone01.val() + " %</td><td></td>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Données juridiques du programme
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Données juridiques du programme</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'></td>";
    html += "<td align='left' colspan='2'>" + $('#ddl3 option:selected').text() + "</td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Nombre d'unités d'habitation :</td>";
    html += "<td align='right'>" + $txtZone02.val() + " unité(s)</td><td></td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Prix
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Prix</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Prix de la vente TVA comprise :</td>";
    html += "<td align='right'>" + $txtZone03.val() + " €</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Montant du prix Hors Taxe :</td>";
    html += "<td align='right'>" + $("#value01").val() + " €</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Montant de la TVA :</td>";
    html += "<td align='right'>" + $("#value00").val() + " €</td><td align='right'>" + $("#txtTaux").val() + " %</td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Condition particulière de la vente
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Condition particulière de la vente</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>La vente est-elle consentie contrat en mains ?</td>";
    html += "<td align='left'>" + ($("#rad2Oui").is(":checked") ? "Oui" : "Non") + "</td><td></td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td colspan='2' align='right'>Emolument de formalités HT :</td>";
    html += "<td align='right'>" + $('#txtEmolument_de_formalités_HT').val() + " €</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td colspan='2' align='right'>Débours :</td>";
    html += "<td align='right'>" + $('#txtDébours').val() + " €</td><td></td>";
    html += "</tr>";
    html += "</table>";
    $('#hdSaisie').val(html);
}

$('.btn-calculate, #btnSendEmail, #btnPrint').click(function () {
    htmlReport();
});