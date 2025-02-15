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

var $chk1 = $("#chk1"), $chk2 = $("#chk2"), $ddl1 = $("#ddl1");
var $txtZone01 = $("#txtZone01"), $txtZone02 = $("#txtZone02"), $txtZone03 = $("#txtZone03"), $txtZone04 = $("#txtZone04"), $txtZone05 = $("#txtZone05");

function ShowHideElements() {
    if ($chk1.is(":checked")) {
        ShowElements([$txtZone03], "div.row");
        if ($chk1.is(":checked") && $chk2.is(":checked")) {
            ShowElements([$txtZone05], "div.row");
        } else {
            HideElements([$txtZone05], "div.row");
        }
    } else {
        HideElements([$txtZone03, $txtZone05], "div.row");
        $("#lblAbattement_déjà_utilisé").hide();
    }

    if ("13458".indexOf($ddl1.val()) !== -1) {
        ShowElements([$("#label1")], "div.row");
        if ($chk1.is(":checked")) {
            ShowElements([$txtZone04], "div.row");
        } else {
            HideElements([$txtZone04], "div.row");
        }
    } else {
        HideElements([$txtZone04, $("#label1")], "div.row");
    }
}

$("#ddl1, #chk1, #chk2, #chk3, #chk4").change(function () {
    ShowHideElements();
}).change();

function htmlReport() {
    var html = "", none = "none";
    html += "<table border-collapse='collapse' border='0' width='100%' size='1' style='border-style: inset' cellspacing='0'>";
    //SUCCESSION - Droits de mutation à titre gratuit
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>SUCCESSION - Droits de mutation à titre gratuit</font></b></td></tr>";
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
    html += "<tr><td bgcolor='#304F73' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Saisie</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#304F73'></td></tr>";
    //Détermination de la parenté De cujus / Héritier
    html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Détermination de la parenté De cujus / Héritier</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Choix :</td>";
    html += "<td align='left' colspan='2'>" + $('#ddl1 option:selected').text() + "</td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Données économiques
    html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Données économiques</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Date de la mutation :</td>";
    html += "<td align='right'>" + $txtZone01.val() + "</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Base taxable :</td>";
    html += "<td align='right'>" + $txtZone02.val() + " €</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Application de la TVA à taux réduit ?</td>";
    html += "<td align='right'>" + ($chk1.is(":checked") ? "Oui" : "Non") + "</td><td></td>";
    html += "</tr>";
    if ($txtZone03.css("display") !== none) {
        html += "<tr>";
        html += "<td align='right' colspan='2'>Dons antérieurs taxés :</td>";
        html += "<td align='right'>" + $txtZone03.val() + " €</td><td></td>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Gestion des abattements
    html += "<tr><td bgcolor='#01ABE4' valign='middle' align='center' colspan='4'><b><font color='#FFFFFF' size='11px'>Gestion des abattements</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    if ($("#label1").css("display") !== none) {
        html += "<tr>";
        html += "<td align='right' colspan='2'>Abattement commun :</td>";
        html += "<td align='right'></td><td></td>";
        html += "</tr>";
    }
    if ($txtZone04.css("display") !== none) {
        html += "<tr>";
        html += "<td align='right' colspan='2'>Abattement commun utilisé :</td>";
        html += "<td align='right'>" + $txtZone04.val() + " €</td><td></td>";
        html += "</tr>";
    }
    html += "<tr>";
    html += "<td align='right' colspan='2'>Abattement au titre des handicapés :</td>";
    html += "<td align='right'>Cocher si Oui : " + ($chk2.is(":checked") ? "Oui" : "Non") + "</td><td align='right'></td>";
    html += "</tr>";
    if ($txtZone05.css("display") !== none) {
        html += "<tr>";
        html += "<td align='right' colspan='2'>Abattement handicapé utilisé :</td>";
        html += "<td align='right'>" + $txtZone05.val() + " €</td><td></td>";
        html += "</tr>";
    }
    html += "</table>";
    $('#hdSaisie').val(html);
}

$('.btn-calculate, #btnSendEmail, #btnPrint').click(function () {
    htmlReport();
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
        ["Emoluments du notaire", parseFloat(0)],
        ['Débours', parseFloat(0)],
        ['Trésor public', parseFloat(0)]
    ]);
    var chart = new google.visualization.PieChart(document.getElementById('piechartContainer'));
    chart.draw(data, options);
    $('#hdPiechart').val(chart.getImageURI());
}
function GenerateChart() {
    google.charts.setOnLoadCallback(drawChart);
}
// End Chart