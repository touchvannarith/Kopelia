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

var $ddl1 = $('#ddl1'), $ddl2 = $('#ddl2');
var $txtZone01 = $('#txtZone01');
var $chk2 = $('#chk2'), $chk1 = $('#chk1');
var $mainDETERMINATION_DES_BIENS = $('#mainDETERMINATION_DES_BIENS');
var $ddl4 = $('#ddl4'), $ddl5 = $('#ddl5');

$txtZone01.on('keyup change', function () {
    $('#ddl3').change();
});
$('#ddl3').change(function () {
    switch (this.value) {
        case "2": //Liquidation contenue dans l'acte
            ShowElements([$txtZone01], "div.row");
            break;
        default:
            HideElements([$txtZone01], "div.row");
    }
}).change();

function ShowHide__() {
    var Régime_communautaire = "1", Régime_séparatiste = "2";
    if (($ddl1.val() === Régime_communautaire && $ddl2.val() === Régime_communautaire) || ($ddl1.val() === Régime_séparatiste && $ddl2.val() === Régime_séparatiste)
        || ($ddl1.val() === Régime_séparatiste && $ddl2.val() === Régime_communautaire)) {
        ShowElements([$chk2], "div.sub-panel");
        if ($chk2.is(':checked')) {
            ShowElements([$ddl4, $ddl5], "div.sub-panel");
            $mainDETERMINATION_DES_BIENS.show();
        } else {
            HideElements([$ddl4, $ddl5], "div.sub-panel");
            $mainDETERMINATION_DES_BIENS.hide();
        }
    } else {
        HideElements([$chk2, $ddl4, $ddl5], "div.sub-panel");
        $mainDETERMINATION_DES_BIENS.hide();
    }

    if ($ddl1.val() === Régime_communautaire && $ddl2.val() === Régime_communautaire) {
        $('#lblCas_traité_message').html("Epoux mariés sous un régime communautaire (régime légal ou régime conventionnel) et adoptant un nouveau régime communautaire contenant éventuellement des apports de biens propres.");
    } else if ($ddl1.val() === Régime_séparatiste && $ddl2.val() === Régime_communautaire) {
        $('#lblCas_traité_message').html("Epoux mariés sous un régime séparatiste et adoptant un régime communautaire ou l'adjonction d'une société d'acquêts avec apport de biens propres.");
    } else if ($ddl1.val() === Régime_communautaire && $ddl2.val() === Régime_séparatiste) {
        $('#lblCas_traité_message').html("Epoux mariés sous un régime communautaire et adoptant un régime séparatiste pur et simple.");
    } else if ($ddl1.val() === Régime_séparatiste && $ddl2.val() === Régime_séparatiste) {
        $('#lblCas_traité_message').html("Epoux mariés sous un régime séparatiste adoptant un régime séparatiste incluant une société d'acquêts avec apport de biens propres.");
    }
}

$('#ddl1, #ddl2').change(function () {
    ShowHide__();
}).change();

$chk2.change(function () {
    ShowHide__();
    $('#ddl4, #ddl5').change();
}).change();

$ddl4.change(function () {
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        $('#divImmobilier' + i).show();
        ShowElements([$('#txtImmobilier_Article_' + i + '_Valeur')], "");
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        $('#divImmobilier' + j).hide();
        HideElements([$('#txtImmobilier_Article_' + j + '_Valeur')], "");
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

$ddl5.change(function () {
    for (var i = 1; i <= parseInt($(this).val()); i++) {
        $('#divMobilier' + i).show();
        ShowElements([$('#txtMobilier_Article_' + i + '_Valeur')], "");
    }
    for (var j = 10; j > parseInt($(this).val()); j--) {
        $('#divMobilier' + j).hide();
        HideElements([$('#txtMobilier_Article_' + j + '_Valeur')], "");
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
        ["Emoluments du notaire", parseFloat($('#lblEmoluments_HT_du_notaire').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#lblDébours').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#lblTrésor').text().replace('€', '').replace(' ', ''))]
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Le changement de régime matrimonial</font></b></td></tr>";
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
    //Régime matrimonial d'origine
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Régime matrimonial d'origine</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Régime matrimonial d'origine :</td>";
    html += "<td width='100%'>" + $("#ddl1 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Changement - Adoption du nouveau régime
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Changement - Adoption du nouveau régime</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Changement - Adoption du nouveau régime :</td>";
    html += "<td width='100%'>" + $("#ddl2 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Cas traité
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Cas traité</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr><td colspan='4' align='center'>" + $('#lblCas_traité_message').text() + "</td></tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Situation familiale
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Situation familiale</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Cette situation entraîne-t-elle une homologation ?</td>";
    html += "<td width='100%' align='right'>" + ($chk1.is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Etablissement de l'état liquidatif sans partage
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Etablissement de l'état liquidatif sans partage</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Etablissement de l'état liquidatif sans partage :</td>";
    html += "<td width='100%'>" + $("#ddl3 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($txtZone01.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Valeur brute des biens déclarés :</td>";
        html += "<td width='100%' align='right'>" + $txtZone01.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Mutation des biens et droits entre les époux
    if ($chk2.is(':visible')) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Mutation des biens et droits entre les époux</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Y-a-t'il un transfert de biens et droits ?</td>";
        html += "<td width='100%' align='right'>" + ($chk2.is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }
    if ($('#mainDETERMINATION_DES_BIENS').css('display') !== none) {
        if (parseInt($ddl4.val()) > 0) {
            //Biens immobiliers transférés
            html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Biens immobiliers transférés</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr>";
            html += "<td align='right' colspan='2'>Nombre de biens immobiliers ?</td>";
            html += "<td align='right'>" + $("#ddl4 option:selected").text() + "</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
            for (var i = 1; i <= parseInt($ddl4.val()); i++) {
                html += "<tr><td colspan='4'>Bien Immobilier - Article " + i + "</td></tr>";
                html += "<tr>";
                html += "<td align='right'>Droit actuel de l'apporteur :</td>";
                html += "<td align='right'>" + $('#txtImmobilier_Article_' + i + '_Percent_1').val() + " %</td>";
                html += "<td align='right'>Valeur de la PP :</td>";
                html += "<td align='right'>" + $('#txtImmobilier_Article_' + i + '_Valeur').val() + " €</td>";
                html += "<tr><td colspan='4'></td></tr>";
                html += "</tr>";
                html += "<tr>";
                html += "<td align='right'>Droit après changement :</td>";
                html += "<td align='right'>" + $('#txtImmobilier_Article_' + i + '_Percent_2').val() + " %</td>";
                html += "<tr><td colspan='4'></td></tr>";
                html += "</tr>";
            }
            html += "<tr><td colspan='4'></td></tr>";
        }
        if (parseInt($ddl5.val()) > 0) {
            //Biens mobiliers transférés
            html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Biens mobiliers transférés</font></b></td></tr>";
            html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
            html += "<tr>";
            html += "<td align='right' colspan='2'>Nombre de biens mobiliers ?</td>";
            html += "<td align='right'>" + $("#ddl5 option:selected").text() + "</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
            for (var i = 1; i <= parseInt($ddl5.val()); i++) {
                html += "<tr><td colspan='4'>Bien Mobilier - Article " + i + "</td></tr>";
                html += "<tr>";
                html += "<td align='right'>Droit actuel de l'apporteur :</td>";
                html += "<td align='right'>" + $('#txtMobilier_Article_' + i + '_Percent_1').val() + " %</td>";
                html += "<td align='right'>Valeur de la PP :</td>";
                html += "<td align='right'>" + $('#txtMobilier_Article_' + i + '_Valeur').val() + " €</td>";
                html += "<tr><td colspan='4'></td></tr>";
                html += "</tr>";
                html += "<tr>";
                html += "<td align='right'>Droit après changement :</td>";
                html += "<td align='right'>" + $('#txtMobilier_Article_' + i + '_Percent_2').val() + " %</td>";
                html += "<tr><td colspan='4'></td></tr>";
                html += "</tr>";
            }
            html += "<tr><td colspan='4'></td></tr>";
        }
    }
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