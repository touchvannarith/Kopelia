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

var $chk1 = $('#chk1'), $chk2 = $('#chk2'), $chk3 = $('#chk3');
var $txtZone04 = $('#txtZone04'), $txtZone05 = $('#txtZone05'), $txtZone06 = $('#txtZone06');
$('#ddl1').change(function () {
    if ($(this).val() === "1") {
        $('#subDonnées_particulières').show();
        ShowElements([$chk1, $chk2], "div.row");
        if (!$chk1.is(':checked') && !$chk2.is(':checked')) {
            $('#div130').show();
            ShowElements([$txtZone04, $txtZone05, $chk3], "div.row");
            if ($chk3.is(':checked')) {
                $('#div136').show();
                ShowElements([$txtZone06], "div.row");
            } else {
                $('#div136').hide();
                HideElements([$txtZone06], "div.row");
            }
        } else {
            $('#div130, #div136').hide();
            HideElements([$txtZone04, $txtZone05, $txtZone06, $chk3], "div.row");
        }
    } else {
        $('#subDonnées_particulières').hide();
        HideElements([$chk1, $chk2, $chk3, $txtZone04, $txtZone05, $txtZone06], "div.row");
    }
}).change();
$('#chk1, #chk2, #chk3').change(function () {
    $('#ddl1').change();
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
        ["Convention d'honoraires", parseFloat($('#lblF102').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#lblF103').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#lblF104').text().replace('€', '').replace(' ', ''))]
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Cession de droits sociaux ordinaire</font></b></td></tr>";
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
    //Données générales
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Données générales</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Date de la cession :</td>";
    html += "<td width='100%' align='left'>" + $("#txtDate_de_la_cession").val() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Nature de la société émettrice :</td>";
    html += "<td width='100%' align='left' colspan='2'>" + $("#ddl1 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Prix de cession
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Prix de cession</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Prix de cession (hors comptes courants) :</td>";
    html += "<td width='100%' align='right'>" + $("#txtZone01").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Montant des charges :</td>";
    html += "<td width='100%' align='right'>" + $("#txtZone02").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Honoraires
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Honoraires</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Montant des honoraires HT :</td>";
    html += "<td width='100%' align='right'>" + $("#txtZone03").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Données particulières
    if ($('#subDonnées_particulières').css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Données particulières</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        if ($chk1.css('display') !== none) {
            html += "<tr>";
            html += "<td width='40%' align='right' colspan='2'>Cession d'actions (726 1-1° du CGI) ?</td>";
            html += "<td width='100%' align='right'>" + ($chk1.is(':checked') ? "OUI" : "NON") + "</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
        }
        if ($chk2.css('display') !== none) {
            html += "<tr>";
            html += "<td width='40%' align='right' colspan='2'>Rachat intervenant entre sociétés ?</td>";
            html += "<td width='100%' align='right'>" + ($chk2.is(':checked') ? "OUI" : "NON") + "</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
        }
        if ($chk3.css('display') !== none) {
            html += "<tr>";
            html += "<td width='40%' align='right' colspan='2'>Bénéfice de l'abattement global de 300.000 € ?</td>";
            html += "<td width='100%' align='right'>" + ($chk3.is(':checked') ? "OUI" : "NON") + "</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
        }
        if ($txtZone04.css('display') !== none) {
            html += "<tr><td colspan='2' align='right'>Données pour l'abattement du 726 I-1° bis :</td><td colspan='2'></td></tr>";
            html += "<tr>";
            html += "<td width='40%' align='right' colspan='2'>Nombre de parts ou actions formant le capital social :</td>";
            html += "<td width='100%' align='right'>" + $txtZone04.val() + "</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
        }
        if ($txtZone05.css('display') !== none) {
            html += "<tr>";
            html += "<td width='40%' align='right' colspan='2'>Nombre de parts ou actions cédées :</td>";
            html += "<td width='100%' align='right'>" + $txtZone05.val() + "</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
        }
        if ($txtZone06.css('display') !== none) {
            html += "<tr><td colspan='2' align='right'>Données pour l'abattement du 732 Ter :</td><td colspan='2'></td></tr>";
            html += "<tr>";
            html += "<td width='40%' align='right' colspan='2'>Valeur des titres représentative du fond social (*) :</td>";
            html += "<td width='100%' align='right'>" + $txtZone06.val() + " €</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
        }
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Débours :</td>";
    html += "<td width='100%' align='right'>" + $("#txtDébours").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "</table>";
    $('#hdSaisie').val(html);
}

$('.btn-calculate, #btnSendEmail, #btnPrint').click(function () {
    htmlReport();
});