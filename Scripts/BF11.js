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

var $ddl1 = $("#ddl1"), $ddl2 = $("#ddl2"), $ddl3 = $("#ddl3"), $ddl4 = $("#ddl4");
var $txtZone01 = $('#txtZone01'), $txtZone02 = $('#txtZone02'), $txtZone03 = $('#txtZone03'), $txtZone04 = $('#txtZone04');

function ShowHideElements() {
    var ddl1Value = parseInt($ddl1.val());
    var caseSelected = 0;
    if (ddl1Value === 1) {
        caseSelected = parseInt($ddl2.val());
    } else if (ddl1Value === 2) {
        caseSelected = parseInt($ddl3.val()) + 7;
    } else {
        caseSelected = parseInt($ddl4.val()) + 11;
    }
    switch (caseSelected) {
        case 1:
        case 2:
        case 6:
        case 7:
            ShowElements([$txtZone01], "div.row");
            HideElements([$txtZone02, $txtZone03, $txtZone04], "div.row");
            break;
        case 3:
            ShowElements([$txtZone01, $txtZone02], "div.row");
            HideElements([$txtZone03, $txtZone04], "div.row");
            break;
        case 4:
            ShowElements([$txtZone01, $txtZone03], "div.row");
            HideElements([$txtZone02, $txtZone04], "div.row");
            break;
        case 5:
            ShowElements([$txtZone01, $txtZone02, $txtZone03], "div.row");
            HideElements([$txtZone04], "div.row");
            break;
        case 8:
        case 9:
        case 10:
        case 11:
        case 12:
            ShowElements([$txtZone01, $txtZone04], "div.row");
            HideElements([$txtZone02, $txtZone03], "div.row");
            break;
        case 13:
            ShowElements([$txtZone01, $txtZone02, $txtZone04], "div.row");
            HideElements([$txtZone03], "div.row");
            break;
        case 14:
            ShowElements([$txtZone01, $txtZone03, $txtZone04], "div.row");
            HideElements([$txtZone02], "div.row");
            break;
        case 15:
            ShowElements([$txtZone01, $txtZone02, $txtZone03, $txtZone04], "div.row");
            break;
        case 16:
        case 17:
            ShowElements([$txtZone01, $txtZone04], "div.row");
            HideElements([$txtZone02, $txtZone03], "div.row");
            break;
        default:
    }
}

$("#ddl2, #ddl3, #ddl4").change(function () {
    ShowHideElements();
}).change();

$ddl1.on('change', function () {
    var self = $(this);
    if (self.val() === "1") {
        ShowElements([$ddl2], "div.row");
        HideElements([$ddl3, $ddl4], "div.row");
    } else if (self.val() === "2") {
        ShowElements([$ddl3], "div.row");
        HideElements([$ddl2, $ddl4], "div.row");
    } else if (self.val() === "3") {
        ShowElements([$ddl4], "div.row");
        HideElements([$ddl2, $ddl3], "div.row");
    }
    ShowHideElements();
}).change();

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
        ['Emoluments HT du notaire', parseFloat($('#G83').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#G82').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#G81').text().replace('€', '').replace(' ', ''))]
    ]);
    var chart = new google.visualization.PieChart(document.getElementById('piechartContainer'));
    chart.draw(data, options);
    $('#hdPiechart').val(chart.getImageURI());
}
function GenerateChart() {
    google.charts.setOnLoadCallback(drawChart);
}

function htmlReport() {
    var html = "", none = "none";
    html += "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
    //Mainlevée et/ou de quittance
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Mainlevée et/ou de quittance</font></b></td></tr>";
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
    html += "<tr><td width='700' bgcolor='#304F73' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Saisie</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
    //Nature de l'acte
    html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Nature de l'acte</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Choix :</td>";
    html += "<td width='100%' colspan='2'>" + $("#ddl1 option:selected").text() + "</td>";
    html += "</tr>";
    if ($ddl2.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Type de Mainlevée :</td>";
        html += "<td width='100%' colspan='2'>" + $("#ddl2 option:selected").text() + "</td>";
        html += "</tr>";
    }
    if ($ddl3.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Type de Quittance :</td>";
        html += "<td width='100%' colspan='2'>" + $("#ddl3 option:selected").text() + "</td>";
        html += "</tr>";
    }
    if ($ddl4.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Type de Quittance - Mainlevée :</td>";
        html += "<td width='100%' colspan='2'>" + $("#ddl4 option:selected").text() + "</td>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Donnees generales
    html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Donnees generales</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    if ($('#txtZone01').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Capital évalué au bordereau :</td>";
        html += "<td width='100%' align='right'>" + $('#txtZone01').val() + " €</td><td></td>";
        html += "</tr>";
    }
    if ($('#txtZone02').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant de la somme réduisant la créance :</td>";
        html += "<td width='100%' align='right'>" + $("#txtZone02").val() + " €</td><td></td>";
        html += "</tr>";
    }
    if ($('#txtZone03').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant de la valeur du gage dégrevé :</td>";
        html += "<td width='100%' align='right'>" + $("#txtZone03").val() + " €</td><td></td>";
        html += "</tr>";
    }
    if ($('#txtZone04').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant des capitaux quittancés :</td>";
        html += "<td width='100%' align='right'>" + $("#txtZone04").val() + " €</td><td></td>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Emoluments de formalités et Débours
    html += "<tr><td width='700' bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Emolument de formalités HT :</td>";
    html += "<td width='100%' align='right'>" + $("#txtEmolument_de_formalités_HT").val() + " €</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Débours :</td>";
    html += "<td width='100%' align='right'>" + $("#txtDébours").val() + " €</td><td></td>";
    html += "</tr>";
    html += "</table>";
    $('#hdSaisie').val(html);
}
$('.btn-calculate, #btnSendEmail, #btnPrint').click(function () {
    htmlReport();
});