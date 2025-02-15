$(".select2").select2({
    //placeholder: "Sélectionnez un acte ...",
    allowClear: true,
    minimumResultsForSearch: -1
});
$('#txtZone02').datetimepicker({
    locale: 'fr',
    format: 'L',
    defaultDate: new Date(1945, 0, 1)
});

if ($("#postback").val() === "false") {
    ShowHide_input_form($("#btnInputForm1"));
}
$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

var $chk1 = $("#chk1"), $txtZone01 = $("#txtZone01"), $txtZone02 = $("#txtZone02");

function ShowHideElements(element) {
    var i;
    var blnImmobilierSelected = false;
    $("#divArticle1, #divArticle2, #divArticle3, #divArticle4, #divArticle5, #divArticle6, #divArticle7, #divArticle8, #divArticle9, #divArticle10").hide();
    var ddl1Value = parseInt($("#ddl1").val());
    for (i = 1; i <= ddl1Value; i++) {
        $("#divArticle" + i).show();
        ShowElements([$("#ddlArticle" + i), $("#chkArticle" + i), $("#valuePP" + i)], "div.row");
        if (!$chk1.is(":checked")) {
            HideElements([$("#chkArticle" + i)], "span");
        }
    }
    for (var j = 10; j > ddl1Value; j--) {
        HideElements([$("#chkArticle" + j), $("#valuePP" + j)], "div.row");
    }
    if ($chk1.is(":checked")) {
        ShowElements([$txtZone02], "div.row");
        for (i = 1; i <= parseInt($("#ddl1").val()); i++) {
            if ($("#ddlArticle" + i).val() === "1") {
                ShowElements([$("#chkArticle" + i)], "span");
            } else {
                HideElements([$("#chkArticle" + i)], "span");
            }
        }
    } else {
        HideElements([$txtZone02], "div.row");
        for (i = 1; i <= 10; i++) {
            HideElements([$("#chkArticle" + i)], "span");
        }
    }
    for (i = 1; i <= parseInt($("#ddl1").val()); i++) {
        if ($("#ddlArticle" + i).val() === "1") {
            blnImmobilierSelected = true;
        }
    }
    if ($('#postback').val() === "false") {
        if (blnImmobilierSelected) {
            $('#txtEmolument_de_formalités_HT').val("800,00");
            $('#txtDébours').val("200,00");
        } else {
            $('#txtEmolument_de_formalités_HT').val("400,00");
            $('#txtDébours').val("200,00");
        }
    }
}

ShowHideElements();

$chk1.change(function () {
    ShowHideElements();
    if ($chk1.is(":checked")) {
        $txtZone02.val("01/01/1945");
    }
});

$("#ddl1").change(function () {
    ShowHideElements();
});

$("#chkArticle1, #chkArticle2, #chkArticle3, #chkArticle4, #chkArticle5, #chkArticle6, #chkArticle7, #chkArticle8, #chkArticle9, #chkArticle10").change(function () {
    ShowHideElements($(this));
});

$("#ddlArticle1, #ddlArticle2, #ddlArticle3, #ddlArticle4, #ddlArticle5, #ddlArticle6, #ddlArticle7, #ddlArticle8, #ddlArticle9, #ddlArticle10").change(function () {
    ShowHideElements($(this));
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Donation et donation partage (1 donateur)</font></b></td></tr>";
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
    //Determination de l'operation
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Determination de l'operation</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Date de la donation :</td>";
    html += "<td align='right'>" + $txtZone01.val() + "</td><td></td>";
    html += "</tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>La donation comporte-t-elle des réserves d'usufruit ?</td>";
    html += "<td align='right'>" + ($chk1.is(':checked') ? 'Oui' : 'Non') + "</td><td></td>";
    html += "</tr>";
    if ($txtZone02.css("display") !== none) {
        html += "<tr>";
        html += "<td align='right' colspan='2'>Date de naissance du donateur :</td>";
        html += "<td align='right'>" + $txtZone02.val() + "</td><td></td>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Determination des biens
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Determination des biens</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'>Nombre d'articles ?</td>";
    html += "<td align='right'>" + $('#ddl1 option:selected').text() + "</td><td></td>";
    html += "</tr>";
    for (var i = 1; i <= parseInt($("#ddl1").val()); i++) {
        html += "<tr><td colspan='4'>Article " + i + "</td></tr>";
        html += "<tr>";
        html += "<td align='right'>Type de bien :</td><td align='right'>" + $('#ddlArticle' + i + ' option:selected').text() + "</td>";
        if ($('#chkArticle' + i).css("display") !== none) {
            html += "<td align='right'>Réserve d'usufruit :</td><td align='right'>" + ($('#chkArticle' + i).is(':checked') ? 'Oui' : 'Non') + "</td>";
        } else {
            html += "<td></td><td></td>";
        }
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right'>Valeur PP :</td><td align='right'>" + $('#valuePP' + i).val() + " €</td>";
        html += "<td></td><td></td>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
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