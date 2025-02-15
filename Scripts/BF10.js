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

var $ddl1 = $('#ddl1'), $ddl2 = $('#ddl2'), $ddl3 = $('#ddl3');

//Immobilier
$ddl1.change(function () {
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        $('#divImmobilier' + i).show();
        ShowElements([$('#txtImmobilier_Article_' + i + '_Valeur_PP')], 'div.row');
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        $('#divImmobilier' + j).hide();
        HideElements([$('#txtImmobilier_Article_' + j + '_Valeur_PP')], 'div.row');
    }
    //default value
    if ($('#postback').val() === "false") {
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
$ddl2.change(function () {
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        $('#divMobilier' + i).show();
        ShowElements([$('#txtMobilier_Article_' + i + '_Valeur_PP')], 'div.row');
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        $('#divMobilier' + j).hide();
        HideElements([$('#txtMobilier_Article_' + j + '_Valeur_PP')], 'div.row');
    }
}).change();

//Passif
$ddl3.change(function () {
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        $('#divPassif' + i).show();
        ShowElements([$('#txtPassif_Article_' + i + '_Valeur')], 'div.row');
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        $('#divPassif' + j).hide();
        HideElements([$('#txtPassif_Article_' + j + '_Valeur')], 'div.row');
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Partage de biens communs</font></b></td></tr>";
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
    html += "<td align='right' colspan='2'>Date envisagée de l'acte :</td>";
    html += "<td align='right'>" + $('#txtZone01').val() + "</td><td></td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Cadre d'intervention de la liquidation Partage
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Cadre d'intervention de la liquidation Partage</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td align='right' colspan='2'></td>";
    html += "<td colspan='2'>" + $('#ddl01 option:selected').text() + "</td>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Premier échangiste
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Fiscalité de la liquidation</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>L'une des parties bénéficie-t-elle de l'aide juridictionnelle dans le cadre d'un divorce ?</td>";
    html += "<td width='100%' align='right'>" + ($('#chk2').is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Les frais du partage sont-ils incorporés dans la liquidation ?</td>";
    html += "<td width='100%' align='right'>" + ($('#chk3').is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Biens immobiliers
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Biens immobiliers</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Nombre de biens immobiliers partagés ?</td>";
    html += "<td width='100%' align='right'>" + $("#ddl1 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    var i;
    for (i = 1; i <= parseInt($ddl1.val()); i++) {
        html += "<tr><td colspan='4'>Bien Immobilier - Article " + i + "</td></tr>";
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur PP :</td>";
        html += "<td width='100%' align='right'>" + $('#txtImmobilier_Article_' + i + '_Valeur_PP').val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Biens mobiliers
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Biens mobiliers</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Nombre de biens mobiliers partagés ?</td>";
    html += "<td width='100%' align='right'>" + $("#ddl2 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    for (i = 1; i <= parseInt($ddl2.val()); i++) {
        html += "<tr><td colspan='4'>Bien Mobilier - Article " + i + "</td></tr>";
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur PP :</td>";
        html += "<td width='100%' align='right'>" + $('#txtMobilier_Article_' + i + '_Valeur_PP').val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Passif
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Passif</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Nombres de sommes au passif ?</td>";
    html += "<td width='100%' align='right'>" + $("#ddl3 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    for (i = 1; i <= parseInt($ddl3.val()); i++) {
        html += "<tr><td colspan='4'>Passif  - Article " + i + "</td></tr>";
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur :</td>";
        html += "<td width='100%' align='right'>" + $('#txtPassif_Article_' + i + '_Valeur').val() + " €</td>";
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