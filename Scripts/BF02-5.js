if ($("#postback").val() === "false") {
    SetDefaultValues();
    ShowHide_input_form($("#btnInputForm1"));
}
$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

var $chkContratDeMariage = $('#chkContratDeMariage'),
    $txtValueContratDeMariage1 = $('#txtValueContratDeMariage1'),
    $txtValueContratDeMariage2 = $('#txtValueContratDeMariage2');

function ShowHideElements() {
    if ($chkContratDeMariage.is(":checked")) {
        ShowElements([$txtValueContratDeMariage1, $txtValueContratDeMariage2], "div.row");
    } else {
        HideElements([$txtValueContratDeMariage1, $txtValueContratDeMariage2], "div.row");
    }
}

function SetDefaultValues() {
    var debours, emoluments;
    var page = $('#txtNumPages').val() === "" ? 0 : parseInt($('#txtNumPages').val());
    var copies = $('#txtNumCopies').val() === "" ? 0 : parseInt($('#txtNumCopies').val());
    var authentique = $('#txtNumExp').val() === "" ? 0 : parseInt($('#txtNumExp').val());
    var valueCopieAuthentique = page * authentique * 1.13;
    var valueCopieLibre = page * copies * 0.38;
    var valueArchivage = page * 0.19;
    var total = valueCopieAuthentique + valueCopieLibre + valueArchivage;
    if ($("#chkContratDeMariage").is(":checked") && parseFloat($("#txtValueContratDeMariage2").val().replace(",", ".")) > 0) {
        debours = 50.00;
        emoluments = 348.08;
    } else {
        debours = 50.00;
        emoluments = 23.51;
    }
    total += emoluments;
    $("#txtDébours").val(debours.toFixed(2).replace(".", ","));
    $("#txtEmolument_de_formalités_HT").val(total.toFixed(2).replace(".", ","));
}

ShowHideElements();

$("#chkContratDeMariage, #txtValueContratDeMariage2").on("keypress change input", function () {
    ShowHideElements();
    SetDefaultValues();
});

$('#txtNumPages, #txtNumCopies, #txtNumExp').on("keypress change input", function () {
    SetDefaultValues();
});

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
        ['Emoluments HT du notaire', parseFloat($('#D240').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#D241').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#D242').text().replace('€', '').replace(' ', ''))]
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
    //
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Le contrat de mariage</font></b></td></tr>";
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
    html += "<td width='40%' align='right' colspan='2'>Y a-t-il des biens dont la valeur est déclarée ?</td>";
    html += "<td width='100%' align='right'>" + ($("#chkContratDeMariage").is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($('#txtValueContratDeMariage1').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur totale des biens mobiliers et immobiliers :</td>";
        html += "<td width='100%' align='right'>" + $("#txtValueContratDeMariage1").val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#txtValueContratDeMariage2').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur des droits immobiliers transmis au conjoint :</td>";
        html += "<td width='100%' align='right'>" + $("#txtValueContratDeMariage2").val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    // Emoluments de formalités afférents á la rédaction de l'acte
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités afférents à la rédaction de l'acte</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Veuillez indiquer le nombre de pages :</td>";
    html += "<td width='100%' align='right'>" + $("#txtNumPages").val() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Nombre de copies :</td>";
    html += "<td width='100%' align='right'>" + $("#txtNumCopies").val() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Nombre de copies authentiques :</td>";
    html += "<td width='100%' align='right'>" + $("#txtNumExp").val() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
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