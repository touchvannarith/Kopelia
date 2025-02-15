
if ($("#postback").val() === "false") {
    ShowHide_input_form($("#btnInputForm1"));
}
$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

var $txtZone01 = $('#txtZone01'),
    $txtZone02 = $('#txtZone02'),
    $txtZone03 = $('#txtZone03'),
    $txtZone04 = $('#txtZone04');
var $radAttestation1 = $('#radAttestation1'),
    $radAttestation2 = $('#radAttestation2');

$(".attestation input").change(function () {
    $("#chkAttestation1").change();
}).change();

$("#chkAttestation1").change(function () {
    if ($(this).is(':checked')) {
        ShowElements([$txtZone01], "div.row");
        HideElements([$txtZone02, $txtZone03, $txtZone04, $radAttestation1, $radAttestation2], "div.row");
    } else {
        ShowElements([$radAttestation1, $radAttestation2], "div.row");
        HideElements([$txtZone01], "div.row");
        if ($('#radAttestation1').is(':checked')) {
            ShowElements([$txtZone02, $txtZone03], "div.row");
        } else {
            HideElements([$txtZone02, $txtZone03], "div.row");
        }
        if ($('#radAttestation2').is(':checked')) {
            ShowElements([$txtZone04], "div.row");
        } else {
            HideElements([$txtZone04], "div.row");
        }
    }

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
        ['Emoluments HT du notaire', parseFloat($('#D942').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#D943').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#D944').text().replace('€', '').replace(' ', ''))]
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Attestation notariée</font></b></td></tr>";
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
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Transmission au profit d'une personne publique ou morale</td>";
    html += "<td width='100%' align='right'>" + ($("#chkAttestation1").is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($('#radAttestation1').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Transmission successorale</td>";
        html += "<td width='100%' align='right'>" + ($("#radAttestation1").is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#radAttestation2').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Transmission en cas d'un changement de régime matrimonial</td>";
        html += "<td width='100%' align='right'>" + ($("#radAttestation2").is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#txtZone01').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur des biens immobiliers légués :</td>";
        html += "<td width='100%' align='right'>" + $("#txtZone01").val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#txtZone02').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur des biens immobiliers propres :</td>";
        html += "<td width='100%' align='right'>" + $("#txtZone02").val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#txtZone03').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur des biens immobiliers communs :</td>";
        html += "<td width='100%' align='right'>" + $("#txtZone03").val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#txtZone04').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur des biens immobiliers transmise au conjoint :</td>";
        html += "<td width='100%' align='right'>" + $("#txtZone04").val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    // Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Emolument de formalités HT :</td>";
    html += "<td width='100%' align='right'>" + $("#txtEmolument_de_formalités_HT").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Débours :</td>";
    html += "<td width='100%' align='right'>" + $("#txtDébours").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "</table>";
    $('#hdSaisie').val(html);
}
$('.btn-calculate, #btnSendEmail, #btnPrint').click(function () {
    htmlReport();
});