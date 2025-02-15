if ($("#postback").val() === "false") {
    ShowHide_input_form($("#btnInputForm1"));
}
$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

var $chk1 = $("#chk1"), $txtZone04 = $("#txtZone04");

function ShowHideElements() {
    if ($chk1.is(":checked")) {
        ShowElements([$txtZone04], "div.row");
    } else {
        HideElements([$txtZone04], "div.row");
    }
}

ShowHideElements();

$chk1.change(function () {
    ShowHideElements();
    if ($(this).is(":checked")) {
        $("#txtZone04").val("4,5");
    } else {
        $("#txtZone04").val("");
    }
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
    var html = "";
    html += "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Cession de droits sociaux d'une SCI d'attribution</font></b></td></tr>";
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
    //Prix de cession
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Prix de cession</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Prix de cession (hors comptes courants) : </td>";
    html += "<td width='100%' align='right'>" + $("#txtZone01").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Montant des charges :</td>";
    html += "<td width='100%' align='right'>" + $("#txtZone02").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Honoraires
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Honoraires</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Montant des honoraires HT :</td>";
    html += "<td width='100%' align='right'>" + $("#txtZone03").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Droits de mutation - Taxes complémentaires
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Droits de mutation - Taxes complémentaires</font></b></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Le département a-t-il voté l'augmentation de sa part ?</td>";
    html += "<td width='100%' align='right'>" + ($("#chk1").is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Taux de la taxe départementale voté :</td>";
    html += "<td width='100%' align='right'>" + $("#txtZone04").val() + " %</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    // Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
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