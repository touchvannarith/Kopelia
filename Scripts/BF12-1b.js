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

function calculateValeurNP(value, percent) {
    value = value.replace(",", ".");
    var num = value === '' ? 0 : parseFloat(value);
    var result = num - ((num * percent) / 100);
    return result.toString().replace(".", ",");
}

//Immobilier
$('#ddl1').change(function () {
    $('#divImmobilier1, #divImmobilier2, #divImmobilier3, #divImmobilier4, #divImmobilier5, #divImmobilier6, #divImmobilier7, #divImmobilier8, #divImmobilier9, #divImmobilier10').hide();
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        $('#divImmobilier' + i).show();
        ShowElements([$('#chkImmobilier_Article_' + i + '_partage'), $('#chkImmobilier_Article_' + i + '_usufruit'), $('#txtImmobilier_Article_' + i + '_Valeur_PP')], 'div.row');
        if ($('#chkImmobilier_Article_' + i + '_usufruit').is(':checked')) {
            ShowElements([$('#ddlImmobilier_Article_' + i), $('#txtImmobilier_Article_' + i + '_Valeur_NP')], 'div.row');
        } else {
            HideElements([$('#ddlImmobilier_Article_' + i), $('#txtImmobilier_Article_' + i + '_Valeur_NP')], 'div.row');
        }
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        HideElements([$('#chkImmobilier_Article_' + j + '_partage'), $('#chkImmobilier_Article_' + j + '_usufruit'), $('#txtImmobilier_Article_' + j + '_Valeur_PP'),
        $('#ddlImmobilier_Article_' + j), $('#txtImmobilier_Article_' + j + '_Valeur_NP')], 'div.row');
    }
    //default value
    if ($("#postback").val() === "false") {
        if (parseInt($(this).val()) > 0) {
            $('#txtEmolument_de_formalités_HT').val("800,00");
            $('#txtDébours').val("200,00");
        } else {
            $('#txtEmolument_de_formalités_HT').val("400,00");
            $('#txtDébours').val("200,00");
        }
    }
}).change();

//Mobilier
$('#ddl2').change(function () {
    $('#divMobilier1,#divMobilier2,#divMobilier3,#divMobilier4,#divMobilier5,#divMobilier6,#divMobilier7,#divMobilier8,#divMobilier9,#divMobilier10').hide();
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        $('#divMobilier' + i).show();
        ShowElements([$('#chkMobilier_Article_' + i + '_usufruit'), $('#txtMobilier_Article_' + i + '_Valeur_PP')], 'div.row');
        if ($('#chkMobilier_Article_' + i + '_usufruit').is(':checked')) {
            ShowElements([$('#ddlMobilier_Article_' + i), $('#txtMobilier_Article_' + i + '_Valeur_NP')], 'div.row');
        } else {
            HideElements([$('#ddlMobilier_Article_' + i), $('#txtMobilier_Article_' + i + '_Valeur_NP')], 'div.row');
        }
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        HideElements([$('#chkMobilier_Article_' + j + '_usufruit'), $('#txtMobilier_Article_' + j + '_Valeur_PP'),
        $('#ddlMobilier_Article_' + j), $('#txtMobilier_Article_' + j + '_Valeur_NP')], 'div.row');
    }
}).change();

//Passif
$('#ddl3').change(function () {
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        $('#divPassif' + i).show();
        ShowElements([$('#txtPassif_Article_' + i + '_Valeur')], 'div.row');
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        $('#divPassif' + j).hide();
        HideElements([$('#txtPassif_Article_' + j + '_Valeur')], 'div.row');
    }
}).change();

$('#chkImmobilier_Article_1_usufruit,#chkImmobilier_Article_2_usufruit,#chkImmobilier_Article_3_usufruit,#chkImmobilier_Article_4_usufruit,#chkImmobilier_Article_5_usufruit,#chkImmobilier_Article_6_usufruit,#chkImmobilier_Article_7_usufruit,#chkImmobilier_Article_8_usufruit,#chkImmobilier_Article_9_usufruit,#chkImmobilier_Article_10_usufruit').change(function () {
    $('#ddl1').change();
}).change();
$('#chkMobilier_Article_1_usufruit,#chkMobilier_Article_2_usufruit,#chkMobilier_Article_3_usufruit,#chkMobilier_Article_4_usufruit,#chkMobilier_Article_5_usufruit,#chkMobilier_Article_6_usufruit,#chkMobilier_Article_7_usufruit,#chkMobilier_Article_8_usufruit,#chkMobilier_Article_9_usufruit,#chkMobilier_Article_10_usufruit').change(function () {
    $('#ddl2').change();
}).change();

//Biens immobiliers
$('#txtImmobilier_Article_1_Valeur_PP, #ddlImmobilier_Article_1, #chkImmobilier_Article_1_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_1_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_1_Valeur_PP').val(), $('#ddlImmobilier_Article_1').val()));
}).change();
$('#txtImmobilier_Article_2_Valeur_PP, #ddlImmobilier_Article_2, #chkImmobilier_Article_2_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_2_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_2_Valeur_PP').val(), $('#ddlImmobilier_Article_2').val()));
}).change();
$('#txtImmobilier_Article_3_Valeur_PP, #ddlImmobilier_Article_3, #chkImmobilier_Article_3_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_3_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_3_Valeur_PP').val(), $('#ddlImmobilier_Article_3').val()));
}).change();
$('#txtImmobilier_Article_4_Valeur_PP, #ddlImmobilier_Article_4, #chkImmobilier_Article_4_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_4_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_4_Valeur_PP').val(), $('#ddlImmobilier_Article_4').val()));
}).change();
$('#txtImmobilier_Article_5_Valeur_PP, #ddlImmobilier_Article_5, #chkImmobilier_Article_5_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_5_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_5_Valeur_PP').val(), $('#ddlImmobilier_Article_5').val()));
}).change();
$('#txtImmobilier_Article_6_Valeur_PP, #ddlImmobilier_Article_6, #chkImmobilier_Article_6_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_6_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_6_Valeur_PP').val(), $('#ddlImmobilier_Article_6').val()));
}).change();
$('#txtImmobilier_Article_7_Valeur_PP, #ddlImmobilier_Article_7, #chkImmobilier_Article_7_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_7_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_7_Valeur_PP').val(), $('#ddlImmobilier_Article_7').val()));
}).change();
$('#txtImmobilier_Article_8_Valeur_PP, #ddlImmobilier_Article_8, #chkImmobilier_Article_8_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_8_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_8_Valeur_PP').val(), $('#ddlImmobilier_Article_8').val()));
}).change();
$('#txtImmobilier_Article_9_Valeur_PP, #ddlImmobilier_Article_9, #chkImmobilier_Article_9_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_9_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_9_Valeur_PP').val(), $('#ddlImmobilier_Article_9').val()));
}).change();
$('#txtImmobilier_Article_10_Valeur_PP, #ddlImmobilier_Article_10, #chkImmobilier_Article_10_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_10_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_10_Valeur_PP').val(), $('#ddlImmobilier_Article_10').val()));
}).change();
//Biens mobiliers
$('#txtMobilier_Article_1_Valeur_PP, #ddlMobilier_Article_1, #chkMobilier_Article_1_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_1_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_1_Valeur_PP').val(), $('#ddlMobilier_Article_1').val()));
}).change();
$('#txtMobilier_Article_2_Valeur_PP, #ddlMobilier_Article_2, #chkMobilier_Article_2_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_2_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_2_Valeur_PP').val(), $('#ddlMobilier_Article_2').val()));
}).change();
$('#txtMobilier_Article_3_Valeur_PP, #ddlMobilier_Article_3, #chkMobilier_Article_3_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_3_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_3_Valeur_PP').val(), $('#ddlMobilier_Article_3').val()));
}).change();
$('#txtMobilier_Article_4_Valeur_PP, #ddlMobilier_Article_4, #chkMobilier_Article_4_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_4_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_4_Valeur_PP').val(), $('#ddlMobilier_Article_4').val()));
}).change();
$('#txtMobilier_Article_5_Valeur_PP, #ddlMobilier_Article_5, #chkMobilier_Article_5_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_5_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_5_Valeur_PP').val(), $('#ddlMobilier_Article_5').val()));
}).change();
$('#txtMobilier_Article_6_Valeur_PP, #ddlMobilier_Article_6, #chkMobilier_Article_6_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_6_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_6_Valeur_PP').val(), $('#ddlMobilier_Article_6').val()));
}).change();
$('#txtMobilier_Article_7_Valeur_PP, #ddlMobilier_Article_7, #chkMobilier_Article_7_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_7_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_7_Valeur_PP').val(), $('#ddlMobilier_Article_7').val()));
}).change();
$('#txtMobilier_Article_8_Valeur_PP, #ddlMobilier_Article_8, #chkMobilier_Article_8_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_8_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_8_Valeur_PP').val(), $('#ddlMobilier_Article_8').val()));
}).change();
$('#txtMobilier_Article_9_Valeur_PP, #ddlMobilier_Article_9, #chkMobilier_Article_9_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_9_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_9_Valeur_PP').val(), $('#ddlMobilier_Article_9').val()));
}).change();
$('#txtMobilier_Article_10_Valeur_PP, #ddlMobilier_Article_10, #chkMobilier_Article_10_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_10_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_10_Valeur_PP').val(), $('#ddlMobilier_Article_10').val()));
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Partage de biens indivis - indivision familiale (art 748 CGI)</font></b></td></tr>";
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
    //Biens immobiliers
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Biens immobiliers</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Nombre de biens immobiliers partagés ?</td>";
    html += "<td width='100%' align='right'>" + $("#ddl1 option:selected").text() + "</td><td></td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    var i;
    for (i = 1; i <= parseInt($('#ddl1').val()); i++) {
        html += "<tr><td colspan='4'>Bien Immobilier - Article " + i + "</td></tr>";
        html += "<tr>";
        html += "<td align='right'>Exonération droit de partage : " + ($('#chkImmobilier_Article_' + i + '_partage').is(':checked') === true ? "OUI" : "NON") + "</td>";
        html += "<td align='right'>Grevé d'usufruit : " + ($('#chkImmobilier_Article_' + i + '_usufruit').is(':checked') === true ? "OUI" : "NON") + "</td>";
        html += "<td align='right'>Valeur PP :</td>";
        html += "<td align='right'>" + $('#txtImmobilier_Article_' + i + '_Valeur_PP').val() + " €</td>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
        if ($('#ddlImmobilier_Article_' + i).css('display') !== none) {
            html += "<tr>";
            html += "<td align='right'>Pourcentage d'usufruit (%) :</td>";
            html += "<td align='right'>" + $('#ddlImmobilier_Article_' + i + ' option:selected').text() + "</td>";
            html += "<td align='right'>Valeur NP :</td>";
            html += "<td align='right'>" + $('#txtImmobilier_Article_' + i + '_Valeur_NP').val() + " €</td>";
            html += "</tr>";
            html += "<tr><td colspan='4'></td></tr>";
        }
    }
    //Biens mobiliers
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Biens mobiliers</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Nombre de biens mobiliers partagés ?</td>";
    html += "<td width='100%' align='right'>" + $("#ddl2 option:selected").text() + "</td><td></td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    for (i = 1; i <= parseInt($('#ddl2').val()); i++) {
        html += "<tr><td colspan='4'>Bien Mobilier - Article " + i + "</td></tr>";
        html += "<tr>";
        html += "<td align='right'></td>";
        html += "<td align='right'>Grevé d'usufruit : " + ($('#chkMobilier_Article_' + i + '_usufruit').is(':checked') === true ? "OUI" : "NON") + "</td>";
        html += "<td align='right'>Valeur PP :</td>";
        html += "<td align='right'>" + $('#txtMobilier_Article_' + i + '_Valeur_PP').val() + " €</td>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
        if ($('#ddlMobilier_Article_' + i).css('display') !== none) {
            html += "<tr>";
            html += "<td align='right'>Pourcentage d'usufruit (%) :</td>";
            html += "<td align='right'>" + $('#ddlMobilier_Article_' + i + ' option:selected').text() + "</td>";
            html += "<td align='right'>Valeur NP :</td>";
            html += "<td align='right'>" + $('#txtMobilier_Article_' + i + '_Valeur_NP').val() + " €</td>";
            html += "</tr>";
            html += "<tr><td colspan='4'></td></tr>";
        }
    }
    //Passif
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Passif</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Nombres de sommes au passif ?</td>";
    html += "<td width='100%' align='right'>" + $("#ddl3 option:selected").text() + "</td><td></td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    for (i = 1; i <= parseInt($('#ddl3').val()); i++) {
        html += "<tr><td colspan='4'>Passif  - Article " + i + "</td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='3'>Valeur :</td>";
        html += "<td align='right'>" + $('#txtPassif_Article_' + i + '_Valeur').val() + " €</td>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Deduction des frais de partage
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Deduction des frais de partage</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Pour le calcul de la TPF, voulez-vous déduire les frais de partage ?</td>";
    html += "<td width='100%' align='right'>" + ($('#chk1').is(':checked') === true ? "OUI" : "NON") + "</td><td></td>";
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