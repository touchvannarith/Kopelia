$(".select2").select2({
    //placeholder: "Sélectionnez un acte ...",
    allowClear: true,
    minimumResultsForSearch: -1
});

$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

var $chk1 = $("#chk1"), $chk2 = $("#chk2"), $chk3 = $("#chk3"), $chk4 = $("#chk4"), $ddl1 = $("#ddl1");

function ShowHideElements() {
    var blnImmobilierSelected = false;
    if ($chk3.is(":checked") || $chk4.is(":checked")) {
        ShowElements([$ddl1], "div.row");
        $("#divDetermination_des_biens").show();
        for (var k = 1; k <= $ddl1.val(); k++) {
            if ($("#ddlArticle" + k).val() === "1") {
                blnImmobilierSelected = true;
            }
        }
    } else {
        HideElements([$ddl1], "div.row");
        $("#divDetermination_des_biens").hide();

    }
    if ($("#postback").val() === "false") {
        if (blnImmobilierSelected) {
            $('#txtEmolument_de_formalités_HT').val("800,00");
            $('#txtDébours').val("200,00");
        } else {
            $('#txtEmolument_de_formalités_HT').val("400,00");
            $('#txtDébours').val("200,00");
        }
    }
}

function EmolumentDeboursCalculation() {
    var emolument = 0.00, debours = 0.00;
    if ($chk1.is(":checked")) {
        emolument += 73.04;
        debours += 10.00;
    }
    if ($chk2.is(":checked")) {
        emolument += 16.45;
        debours += 10.00;
    }
    if ($chk3.is(":checked")) {
        emolument += 333.33;
        debours += 50.00;
    }
    if ($chk4.is(":checked")) {
        emolument += 0.00;
        debours += 0.00;
    }
    $("#txtEmolument_de_formalités_HT").val(emolument.toFixed(2).toString().replace(".", ","));
    $("#txtDébours").val(debours.toFixed(2).toString().replace(".", ","));
}

$ddl1.change(function () {
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        ShowElements([$("#ddlArticle" + i), $("#value" + i)], "div.row");
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        HideElements([$("#ddlArticle" + j), $("#value" + j)], "div.row");
    }
    ShowHideElements();
}).change();

$("#chk3, #chk4").change(function () {
    $ddl1.change();
});

$("#ddlArticle1, #ddlArticle2, #ddlArticle3, #ddlArticle4, #ddlArticle5, #ddlArticle6, #ddlArticle7, #ddlArticle8, #ddlArticle9, #ddlArticle10").change(function () {
    ShowHideElements();
});

if ($("#postback").val() === "false") {
    ShowHide_input_form($("#btnInputForm1"));
    ShowHideElements();
}

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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Règlement successoral ab intestat</font></b></td></tr>";
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
    //Ouverture de la succession
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Ouverture de la succession</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Date du décès :</td>";
    html += "<td align='right'>" + $('#txtZone01').val() + "</td><td></td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Quels sont les actes que vous voulez taxer ?
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Quels sont les actes que vous voulez taxer ?</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Notoriété :</td>";
    html += "<td align='right'>" + ($chk1.is(':checked') ? 'Oui' : 'Non') + "</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Inventaire :</td>";
    html += "<td align='right'>" + ($chk2.is(':checked') ? 'Oui' : 'Non') + "</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Attestation immobilière :</td>";
    html += "<td align='right'>" + ($chk3.is(':checked') ? 'Oui' : 'Non') + "</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Déclaration de succession :</td>";
    html += "<td align='right'>" + ($chk4.is(':checked') ? 'Oui' : 'Non') + "</td><td></td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Determination des biens
    if ($ddl1.css("display") !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Determination des biens</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Nombre d'articles ?</td>";
        html += "<td align='right'>" + $('#ddl1 option:selected').text() + "</td><td></td>";
        html += "</tr>";
        for (var i = 1; i <= parseInt($ddl1.val()); i++) {
            html += "<tr><td colspan='4'>Article " + i + "</td></tr>";
            html += "<tr>";
            html += "<td align='right'>Type de bien :</td><td align='right'>" + $('#ddlArticle' + i + ' option:selected').text() + "</td>";
            html += "<td align='right'>Valeur :</td><td align='right'>" + $('#value' + i).val() + " €</td>";
            html += "</tr>";
        }
        html += "<tr><td colspan='4'></td></tr>";
    }
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