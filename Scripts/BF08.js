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

var $txtZone01 = $('#txtZone01'),
    $txtZone02 = $('#txtZone02'),
    $txtZone03 = $('#txtZone03'),
    $txtZone04 = $('#txtZone04'),
    $txtZone05 = $('#txtZone05'),
    $txtZone06 = $('#txtZone06'),
    $txtZone07 = $('#txtZone07');
var $chk1 = $('#chk1'), $ddl1 = $('#ddl1'), $ddl2 = $('#ddl2');

$chk1.change(function () {
    if ($(this).is(':checked')) {
        ShowElements([$('#txtZone05')], "div.row");
    } else {
        HideElements([$('#txtZone05')], "div.row");
    }
}).change();

$ddl1.change(function () {
    if (this.value === "1") { //Crédit bail
        ShowElements([$ddl2, $txtZone01, $txtZone02, $txtZone03, $txtZone04, $chk1, $txtZone05], "div.row");
        $('#divMain3').show();
        HideElements([$txtZone06, $txtZone07], "div.row");
        $('#divMain4').hide();
        $chk1.change();
    } else if (this.value === "2") { //Cession du crédit bail par le preneur
        HideElements([$ddl2, $txtZone01, $txtZone02, $txtZone03, $txtZone04, $chk1, $txtZone05], "div.row");
        $('#divMain3').hide();
        ShowElements([$txtZone06, $txtZone07], "div.row");
        $('#divMain4').show();
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
        ["Emoluments HT du notaire", parseFloat($('#lblEmoluments_HT_du_notaire').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#lblDébours_et_émoluments').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#lblTrésor_public').text().replace('€', '').replace(' ', ''))]
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
    //Le crédit bail
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Le crédit bail</font></b></td></tr>";
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
    html += "<td width='40%' align='right' colspan='2'>Nature de l'opération :</td>";
    html += "<td width='100%' align='left' colspan='2'>" + $("#ddl1 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($ddl2.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Durée du bail :</td>";
        html += "<td width='100%' align='right' colspan='1'>" + $("#ddl2 option:selected").text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone01.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant annuel du loyer :</td>";
        html += "<td width='100%' align='right'>" + $txtZone01.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone02.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant annuel des charges :</td>";
        html += "<td width='100%' align='right'>" + $txtZone02.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone03.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant TTC de l'investissement :</td>";
        html += "<td width='100%' align='right'>" + $txtZone03.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone04.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Quote part des loyers afférente aux frais financiers :</td>";
        html += "<td width='100%' align='right'>" + $txtZone04.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($chk1.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Le crédit bail est-il cautionné ?</td>";
        html += "<td width='100%' align='right'>" + ($chk1.is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone05.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant des sommes cautionnées ?</td>";
        html += "<td width='100%' align='right'>" + $txtZone05.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone06.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Prix exprimé :</td>";
        html += "<td width='100%' align='right'>" + $txtZone06.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone07.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant TTC résiduel de l'investissement :</td>";
        html += "<td width='100%' align='right'>" + $txtZone07.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Emolument de formalités HT :</td>";
    html += "<td width='100%' align='right'>" + $("#txtEmolument_de_formalités_HT").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
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