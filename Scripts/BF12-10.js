if ($("#postback").val() === "false") {
    ShowHide_input_form($("#btnInputForm1"));
}
$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

var $txtZone832 = $("#txtZone832"), $txtZone834 = $("#txtZone834"), $txtZone836 = $("#txtZone836");
$("#txtZone832, #txtZone834").on("keypress blur change input", function () {
    var value1 = parseFloat($txtZone832.val() === "" ? "0" : $txtZone832.val().replace(",", "."));
    var value2 = parseFloat($txtZone834.val() === "" ? "0" : $txtZone834.val().replace(",", "."));
    var result = value1 - value2;
    $txtZone836.val(result.toString().replace(".", ","));
    if ($("#postback").val() === "false") {
        if (value2 > 0) {
            $('#txtEmolument_de_formalités_HT').val("800,00");
            $('#txtDébours').val("200,00");
        } else {
            $('#txtEmolument_de_formalités_HT').val("400,00");
            $('#txtDébours').val("200,00");
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
        ["Emoluments du notaire", parseFloat($('#lblF104').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#lblF103').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#lblF102').text().replace('€', '').replace(' ', ''))]
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Cession de droit indivis – Indivision familiale</font></b></td></tr>";
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
    //Prix de cession d'une licitation ne faisant pas cesser l'indivision
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Prix de cession d`une licitation ne faisant pas cesser l`indivision</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Prix (en ce compris la prise en charge du passif) :</td>";
        html += "<td align='right'>" + $txtZone832.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Afférent aux biens immobiliers :</td>";
        html += "<td align='right'>" + $txtZone834.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Afférent aux biens mobiliers :</td>";
        html += "<td align='right'>" + $txtZone836.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    //Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Emolument de formalités HT :</td>";
    html += "<td width='100%' align='right'>" + $("#txtEmolument_de_formalités_HT").val() + " €</td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Débours :</td>";
    html += "<td width='100%' align='right'>" + $("#txtDébours").val() + " €</td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</table>";
    $('#hdSaisie').val(html);
}
$('.btn-calculate, #btnSendEmail, #btnPrint').click(function () {
    htmlReport();
});