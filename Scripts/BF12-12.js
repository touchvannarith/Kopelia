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
    $("#lblTextZone03Msg").text("La licitation a lieu moyennant une soulte de " + $txtZone832.val() + " €. Il vous appartient d'appliquer le régime fiscal afférant à ce montant.");
    //default value
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

var $txtZone906 = $("#txtZone906"), $txtZone916 = $("#txtZone916"), $txtZone918 = $("#txtZone918"),
    $txtZone920 = $("#txtZone920"), $txtZone920Base = $("#txtZone920Base");
var taux1 = 0;
var taux2 = 0.71498;

function DynamicLabel() {
    var value05 = $txtZone916.val() === "" ? 0 : parseFloat($txtZone916.val().replace(",", "."));
    var value06 = $txtZone918.val() === "" ? 0 : parseFloat($txtZone918.val().replace(",", "."));
    var value07Base = $txtZone920Base.val() === "" ? 0 : parseFloat($txtZone920Base.val().replace(",", "."));
    var value050607 = value05 + value06 + value07Base;
    $("#divTextRed02, #divText03").hide();
    var value832 = $txtZone832.val() === "" ? 0 : parseFloat($txtZone832.val().replace(",", "."));
    if ((value832 - value050607) !== 0) {
        $("#divTextRed02").show();
        $("#lblTextZone02050607Msg").text("Il reste " + (value832 - value050607).toFixed(2).toString().replace(".", ",") + " € à ventiler.");
        $("#divText03").show();
    }
}

$("#chk904").change(function () {
    if ($(this).is(":checked")) {
        ShowElements([$txtZone906], "div.row");
    }
    else {
        HideElements([$txtZone906], "div.row");
        $txtZone906.val("4,50");
    }
    $txtZone906.change();
}).change();
$txtZone906.on("keyup blur change input", function () {
    $("#divTextRed01").hide();
    var defaultTaux = parseFloat($(this).val().replace(",", "."));
    if (defaultTaux > 4.5) {
        $("#divTextRed01").show();
    }
    taux1 = defaultTaux + ((defaultTaux * 2.37) / 100) + 1.2;
    $("#lblTaux1").text(taux1.toString().replace(".", ",") + " %");
    $txtZone916.change();
}).change();
$("#txtZone834, #txtZone836").on("keyup change input", function () {
    if ($("#postback").val() !== "true") {
        $txtZone916.val($txtZone834.val());
    }
    $txtZone920Base.val($txtZone836.val());
    $txtZone916.change();
    $txtZone918.change();
    $("#txtZone920, #txtZone920Base").change();
}).change();
$txtZone916.on("keyup change input", function () {
    var value = $txtZone916.val() === "" ? 0 : parseFloat($txtZone916.val().replace(",", "."));
    $("#lblTextZone05Montant").text(Math.round(value * (taux1 / 100)).toFixed(2).toString().replace(".", ",") + " €");
    DynamicLabel();
}).change();
$txtZone918.on("keyup change input", function () {
    var value = $txtZone918.val() === "" ? 0 : parseFloat($txtZone918.val().replace(",", "."));
    $("#lblTextZone06Montant").text(Math.round(value * (taux2 / 100)).toFixed(2).toString().replace(".", ",") + " €");
    DynamicLabel();
}).change();
$("#txtZone920, #txtZone920Base").on("keyup change input", function () {
    var value = $txtZone920Base.val() === "" ? 0 : parseFloat($txtZone920Base.val().replace(",", "."));
    var taux3 = $txtZone920.val() === "" ? 0 : parseFloat($txtZone920.val().replace(",", "."));
    $("#lblTextZone07Montant").text(Math.round(value * (taux3 / 100)).toFixed(2).toString().replace(".", ",") + " €");
    DynamicLabel();
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Cession de droit indivis - Droit commun</font></b></td></tr>";
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
    //Droit de mutation sur la soulte
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Droit de mutation sur la soulte</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr><td valign='middle' align='center' colspan='4'><b>Augmentation de la part départementale ( LF 2014 )</b></td></tr>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Le bien indivis est-il situé dans un département ayant voté la surtaxe ?</td>";
        html += "<td align='right'>" + ($("#chk904").is(":checked") === true ? "OUI" : "NON") + "</td><td></td>";
        html += "</tr>";
        if ($txtZone906.css("display") !== none) {
            html += "<tr>";
            html += "<td align='right' colspan='2'>Indiquez le montant de la taxe départementale votée :</td>";
            html += "<td align='right'>" + $txtZone906.val() + " %</td><td></td>";
            html += "</tr>";
        }
        if ($("#divTextRed01").css("display") !== none) {
            html += "<tr><td colspan='4' align='center'>Le montant de la taxe départementale ne doit pas dépasser 4,5 %</td></tr>";
        }
        html += "<tr><td colspan='4'></td></tr>";
        //
        html += "<tr><td valign='middle' align='center' colspan='4'><b>Régime fiscal</b></td></tr>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "<tr><td colspan='4' align='center'>" + $("#lblTextZone03Msg").text() + "</td></tr>";
        html += "<tr>";
        html += "<td></td><td align='center'>Base de perception</td><td align='center'>Taux</td><td align='center'>Montant des droits</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right'>Immobilier - Droit commun :</td>";
        html += "<td align='right'>" + $txtZone916.val() + " €</td>";
        html += "<td align='right'>" + $("#lblTaux1").text() + "</td>";
        html += "<td align='right'>" + $("#lblTextZone05Montant").text() + "</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right'>Immobilier - Régime de la TVA :</td>";
        html += "<td align='right'>" + $txtZone918.val() + " €</td>";
        html += "<td align='right'>" + $("#lblTaux2").text() + "</td>";
        html += "<td align='right'>" + $("#lblTextZone06Montant").text() + "</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right'>Autre cas :</td>";
        html += "<td align='right'>" + $txtZone920Base.val() + " €</td>";
        html += "<td align='right'>" + $txtZone920.val() + " %</td>";
        html += "<td align='right'>" + $("#lblTextZone07Montant").text() + "</td>";
        html += "</tr>";
        if ($("#divTextRed02").css("display") !== none) {
            html += "<tr><td colspan='4' align='center'>" + $("#lblTextZone02050607Msg").text() + "</td></tr>";
        }
        if ($("#divText03").css("display") !== none) {
            html += "<tr><td colspan='4' align='center'>La ventilation des bases ne correspond pas à la somme totale.</td></tr>";
        }
        html += "<tr><td colspan='4' align='center'>Attention, la soulte et la prise en charge du passif sont représentatives des biens partagés ou licités et doit permettre l'application du tarif propre à chaque bien (CGI, art. 747).</td></tr>";
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