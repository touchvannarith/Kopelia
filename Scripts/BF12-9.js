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
    FillValueToTextZone864();
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
    FillValueToTextZone866();
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

function calculateValeurNP(value, percent) {
    value = value.replace(",", ".");
    var num = value === '' ? 0 : parseFloat(value);
    var result = num - ((num * percent) / 100);
    return result.toString().replace(".", ",");
}

//Fill value for "Valeur des biens immobiliers"
function FillValueToTextZone864() {
    var intddl1Value = parseInt($("#ddl1").val());
    var value = 0;
    for (var i = 1; i <= intddl1Value; i++) {
        var valNP = parseFloat($("#txtImmobilier_Article_" + i + "_Valeur_NP").val().replace(",", ".")),
            valPP = parseFloat($("#txtImmobilier_Article_" + i + "_Valeur_PP").val().replace(",", "."));
        value += $("#chkImmobilier_Article_" + i + "_usufruit").is(":checked")
            ? (isNaN(valNP) ? 0 : valNP)
            : (isNaN(valPP) ? 0 : valPP);
    }
    $("#txtZone864").val(value);
}

//Fill value for "Valeur des biens mobiliers"
function FillValueToTextZone866() {
    var intddl2Value = parseInt($("#ddl2").val());
    var value = 0;
    for (var i = 1; i <= intddl2Value; i++) {
        var valNP = parseFloat($("#txtMobilier_Article_" + i + "_Valeur_NP").val().replace(",", ".")),
            valPP = parseFloat($("#txtMobilier_Article_" + i + "_Valeur_PP").val().replace(",", "."));
        value += $("#chkMobilier_Article_" + i + "_usufruit").is(":checked")
            ? (isNaN(valNP) ? 0 : valNP)
            : (isNaN(valPP) ? 0 : valPP);
    }
    $("#txtZone866").val(value);
}

//Biens immobiliers
$('#txtImmobilier_Article_1_Valeur_PP, #ddlImmobilier_Article_1, #chkImmobilier_Article_1_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_1_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_1_Valeur_PP').val(), $('#ddlImmobilier_Article_1').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_2_Valeur_PP, #ddlImmobilier_Article_2, #chkImmobilier_Article_2_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_2_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_2_Valeur_PP').val(), $('#ddlImmobilier_Article_2').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_3_Valeur_PP, #ddlImmobilier_Article_3, #chkImmobilier_Article_3_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_3_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_3_Valeur_PP').val(), $('#ddlImmobilier_Article_3').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_4_Valeur_PP, #ddlImmobilier_Article_4, #chkImmobilier_Article_4_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_4_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_4_Valeur_PP').val(), $('#ddlImmobilier_Article_4').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_5_Valeur_PP, #ddlImmobilier_Article_5, #chkImmobilier_Article_5_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_5_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_5_Valeur_PP').val(), $('#ddlImmobilier_Article_5').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_6_Valeur_PP, #ddlImmobilier_Article_6, #chkImmobilier_Article_6_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_6_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_6_Valeur_PP').val(), $('#ddlImmobilier_Article_6').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_7_Valeur_PP, #ddlImmobilier_Article_7, #chkImmobilier_Article_7_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_7_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_7_Valeur_PP').val(), $('#ddlImmobilier_Article_7').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_8_Valeur_PP, #ddlImmobilier_Article_8, #chkImmobilier_Article_8_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_8_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_8_Valeur_PP').val(), $('#ddlImmobilier_Article_8').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_9_Valeur_PP, #ddlImmobilier_Article_9, #chkImmobilier_Article_9_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_9_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_9_Valeur_PP').val(), $('#ddlImmobilier_Article_9').val()));
    FillValueToTextZone864();
}).change();
$('#txtImmobilier_Article_10_Valeur_PP, #ddlImmobilier_Article_10, #chkImmobilier_Article_10_usufruit').on('keyup change', function () {
    $('#txtImmobilier_Article_10_Valeur_NP').val(calculateValeurNP($('#txtImmobilier_Article_10_Valeur_PP').val(), $('#ddlImmobilier_Article_10').val()));
    FillValueToTextZone864();
}).change();

//Biens mobiliers
$('#txtMobilier_Article_1_Valeur_PP, #ddlMobilier_Article_1, #chkMobilier_Article_1_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_1_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_1_Valeur_PP').val(), $('#ddlMobilier_Article_1').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_2_Valeur_PP, #ddlMobilier_Article_2, #chkMobilier_Article_2_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_2_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_2_Valeur_PP').val(), $('#ddlMobilier_Article_2').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_3_Valeur_PP, #ddlMobilier_Article_3, #chkMobilier_Article_3_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_3_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_3_Valeur_PP').val(), $('#ddlMobilier_Article_3').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_4_Valeur_PP, #ddlMobilier_Article_4, #chkMobilier_Article_4_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_4_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_4_Valeur_PP').val(), $('#ddlMobilier_Article_4').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_5_Valeur_PP, #ddlMobilier_Article_5, #chkMobilier_Article_5_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_5_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_5_Valeur_PP').val(), $('#ddlMobilier_Article_5').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_6_Valeur_PP, #ddlMobilier_Article_6, #chkMobilier_Article_6_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_6_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_6_Valeur_PP').val(), $('#ddlMobilier_Article_6').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_7_Valeur_PP, #ddlMobilier_Article_7, #chkMobilier_Article_7_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_7_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_7_Valeur_PP').val(), $('#ddlMobilier_Article_7').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_8_Valeur_PP, #ddlMobilier_Article_8, #chkMobilier_Article_8_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_8_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_8_Valeur_PP').val(), $('#ddlMobilier_Article_8').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_9_Valeur_PP, #ddlMobilier_Article_9, #chkMobilier_Article_9_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_9_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_9_Valeur_PP').val(), $('#ddlMobilier_Article_9').val()));
    FillValueToTextZone866();
}).change();
$('#txtMobilier_Article_10_Valeur_PP, #ddlMobilier_Article_10, #chkMobilier_Article_10_usufruit').on('keyup change', function () {
    $('#txtMobilier_Article_10_Valeur_NP').val(calculateValeurNP($('#txtMobilier_Article_10_Valeur_PP').val(), $('#ddlMobilier_Article_10').val()));
    FillValueToTextZone866();
}).change();

var $txtZone906 = $("#txtZone906"), $txtZone866 = $("#txtZone866"), $txtZone916 = $("#txtZone916"), $txtZone918 = $("#txtZone918"),
    $txtZone920 = $("#txtZone920"), $txtZone920Base = $("#txtZone920Base"), $txtZone864 = $("#txtZone864"), $txtZone871 = $("#txtZone871");
var taux1 = 0;
var taux2 = 0.71498;
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
$("#txtZone864, #txtZone866, #txtZone871, #txtZone834, #txtZone836").on("keyup change input", function () {
    var value01 = $txtZone864.val() === "" ? 0 : parseFloat($txtZone864.val().replace(",", "."));
    var value02 = $txtZone866.val() === "" ? 0 : parseFloat($txtZone866.val().replace(",", "."));
    var value03 = $txtZone871.val() === "" ? 0 : parseFloat($txtZone871.val().replace(",", "."));
    var value0102 = value01 + value02;
    var valueA = ((value01 * value03) / value0102).toFixed(2);
    var valueB = ((value02 * value03) / value0102).toFixed(2);
    $("#txtFormula01").val(isNaN(valueA) ? "0,00" : valueA.toString().replace(".", ","));
    $("#txtFormula02").val(isNaN(valueB) ? "0,00" : valueB.toString().replace(".", ","));
    if ($("#postback").val() !== "true") {
        $txtZone916.val($("#txtFormula01").val());
    }
    $txtZone920Base.val($("#txtFormula02").val());
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
function DynamicLabel() {
    var value03 = $txtZone871.val() === "" ? 0 : parseFloat($txtZone871.val().replace(",", "."));
    var value05 = $txtZone916.val() === "" ? 0 : parseFloat($txtZone916.val().replace(",", "."));
    var value06 = $txtZone918.val() === "" ? 0 : parseFloat($txtZone918.val().replace(",", "."));
    var value07Base = $txtZone920Base.val() === "" ? 0 : parseFloat($txtZone920Base.val().replace(",", "."));
    var value050607 = value05 + value06 + value07Base;
    $("#divTextRed02, #divText03").hide();
    $("#lblTextZone03Msg").text("La licitation a lieu moyennant une soulte de " + $txtZone871.val() + " €. Il vous appartient d'appliquer le régime fiscal afférant à ce montant.");
    if ((value03 - value050607) !== 0) {
        $("#divTextRed02").show();
        $("#lblTextZone02050607Msg").text("Il reste " + (value03 - value050607).toFixed(2).toString().replace(".", ",") + " € à ventiler.");
        $("#divText03").show();
    }
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Licitation faisant cesser une indivision de droit commun</font></b></td></tr>";
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
    //Licitation mettant fin a l'indivision - droit commun
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Licitation mettant fin a l`indivision - droit commun</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        //
        html += "<tr><td valign='middle' align='center' colspan='4'><b>Données économiques des biens licités</b></td></tr>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Valeur des biens immobiliers :</td>";
        html += "<td align='right'>" + $txtZone864.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Valeur des biens mobiliers :</td>";
        html += "<td align='right'>" + $txtZone866.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
        //
        html += "<tr><td valign='middle' align='center' colspan='4'><b>Prix de cession</b></td></tr>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Prix (en ce compris la prise en charge du passif) :</td>";
        html += "<td align='right'>" + $txtZone871.val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Afférent aux biens immobiliers :</td>";
        html += "<td align='right'>" + $("#txtFormula01").val() + " €</td><td></td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right' colspan='2'>Afférent aux biens mobiliers :</td>";
        html += "<td align='right'>" + $("#txtFormula02").val() + " €</td><td></td>";
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
        html += "<td align='right'>" + $("#lblTaux1").text() + " %</td>";
        html += "<td align='right'>" + $("#lblTextZone05Montant").text() + " €</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right'>Immobilier - Régime de la TVA :</td>";
        html += "<td align='right'>" + $txtZone918.val() + " €</td>";
        html += "<td align='right'>" + $("#lblTaux2").text() + " %</td>";
        html += "<td align='right'>" + $("#lblTextZone06Montant").text() + " €</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td align='right'>Autre cas :</td>";
        html += "<td align='right'>" + $txtZone920Base.val() + " €</td>";
        html += "<td align='right'>" + $txtZone920.val() + " %</td>";
        html += "<td align='right'>" + $("#lblTextZone07Montant").text() + " €</td>";
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