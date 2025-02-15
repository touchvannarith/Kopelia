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

function ConvertToFloat(value) {
    value = value.replace(",", ".");
    return value === "" ? 0 : parseFloat(value);
}

var div_row = "div.row";
var $txtZone01 = $('#txtZone01'),
    $txtZone02 = $('#txtZone02'),
    $txtZone02_1 = $('#txtZone02_1'),
    $ddl1 = $('#ddl1'),
    $ddl2 = $('#ddl2'),
    $chk01 = $('#chk01'),
    $txtZone03 = $('#txtZone03'),
    $chk02 = $('#chk02'),
    $ddl3 = $('#ddl3'),
    $txtZone04 = $('#txtZone04'),
    $txtZone05 = $('#txtZone05'),
    $txtZone06 = $('#txtZone06'),
    $txtZone07 = $('#txtZone07'),
    $txtZone08 = $('#txtZone08'),
    $chk03 = $('#chk03'),
    $chk04 = $('#chk04'),
    $txtZone09 = $('#txtZone09'),
    $ddl4 = $('#ddl4'),
    $chk05 = $('#chk05'),
    $txtZone10 = $('#txtZone10'),
    $txtZone11 = $('#txtZone11'),
    $txtZone12 = $('#txtZone12'),
    $txtZone13 = $('#txtZone13');

function errPrivilèges3() {
    if (($chk04.is(':checked') && $chk03.is(':checked')) || $chk05.is(':checked')) { //F659
        var f667 = ConvertToFloat($txtZone01.val()) - (ConvertToFloat($txtZone09.val()) + ConvertToFloat($txtZone10.val())); //F667
        if (f667 !== 0) {
            if (f667 > 0) {
                $('#errPrivilèges3').text("Il manque " + f667.toFixed(2).replace(".", ",") + " Euros pour égaler le montant total du prêt.");
            } else {
                $('#errPrivilèges3').text("Le montant total du prêt a été dépassée de " + f667.toFixed(2).replace("-", "").replace(".", ",") + " Euros.");
            }
        } else {
            $('#errPrivilèges3').text("");
        }
    } else {
        $('#errPrivilèges3').text("");
    }
}

$ddl1.change(function () {
    var self = $(this);
    if (self.val() !== "0") {
        ShowElements([$txtZone01], div_row);
        if (ConvertToFloat($txtZone01.val()) > 0) {
            ShowElements([$ddl2], "div.sub-card");
        } else {
            HideElements([$ddl2], "div.sub-card");
        }
    } else {
        HideElements([$txtZone01], div_row);
        HideElements([$ddl2], "div.sub-card");
    }

    if (self.val() === "4") {
        ShowElements([$txtZone02, $txtZone02_1], div_row);
    } else {
        HideElements([$txtZone02, $txtZone02_1], div_row);
    }
    $('#chk05').change();
}).change();

$('#txtZone01, #txtZone02').on('keyup change', function () {
    var a1 = ConvertToFloat($txtZone01.val());
    var a2 = ConvertToFloat($txtZone02.val());
    var ab = a1 - a2;
    $txtZone02_1.val(ab.toString().replace(".", ","));

    $ddl1.change();
    $('#txtZone09').change();
    $('#txtZone10').change();
});

$('#chk01, #chk02').change(function () {
    if ($chk01.is(':checked') && $chk02.is(':checked')) {
        ShowElements([$ddl3], div_row);
    } else {
        HideElements([$ddl3], div_row);
    }

    if ($chk01.is(':checked')) {
        ShowElements([$txtZone03], div_row);
        if (ConvertToFloat($txtZone03.val()) > 0) {
            ShowElements([$chk02], div_row);
        }
        if ($chk02.is(':checked')) {
            if ($ddl3.val() >= 5) {
                ShowElements([$txtZone04, $txtZone05, $txtZone06, $txtZone07, $txtZone08], div_row);
            } else if ($ddl3.val() >= 4) {
                ShowElements([$txtZone04, $txtZone05, $txtZone06, $txtZone07], div_row);
                HideElements([$txtZone08], div_row);
            } else if ($ddl3.val() >= 3) {
                ShowElements([$txtZone04, $txtZone05, $txtZone06], div_row);
                HideElements([$txtZone07, $txtZone08], div_row);
            } else if ($ddl3.val() >= 2) {
                ShowElements([$txtZone04, $txtZone05], div_row);
                HideElements([$txtZone06, $txtZone07, $txtZone08], div_row);
            } else if ($ddl3.val() >= 1) {
                ShowElements([$txtZone04], div_row);
                HideElements([$txtZone05, $txtZone06, $txtZone07, $txtZone08], div_row);
            }
        } else {
            HideElements([$ddl3, $txtZone04, $txtZone05, $txtZone06, $txtZone07, $txtZone08], div_row);
        }
    } else {
        HideElements([$txtZone03, $chk02, $txtZone04, $txtZone05, $txtZone06, $txtZone07, $txtZone08], div_row);
    }
    errPrivilèges3();
}).change();

$('#txtZone03').on('keyup change', function () {
    $('#chk01').change();
});

$('#ddl3').change(function () {
    $('#chk02').change();
}).change();

$('#chk04, #chk03').change(function () {
    if ($chk04.is(':checked') || $chk03.is(':checked')) {
        ShowElements([$txtZone09], div_row);
        if (ConvertToFloat($txtZone09.val()) > ConvertToFloat($txtZone01.val())) {
            ShowElements([$('#errPrivilèges1')], div_row);
        } else {
            HideElements([$('#errPrivilèges1')], div_row);
        }
    } else {
        HideElements([$txtZone09, $('#errPrivilèges1')], div_row);
    }
    if ($chk04.is(':checked') || $chk03.is(':checked') || $chk05.is(':checked')) {
        ShowElements([$ddl4], div_row);
    } else {
        HideElements([$ddl4], div_row);
    }
    errPrivilèges3();
}).change();

$('#txtZone09').on('keyup change', function () {
    $('#chk04, #chk03').change();
});

$('#chk05').change(function () {
    if ($chk05.is(':checked')) {
        ShowElements([$txtZone10, $txtZone12], div_row);
        if ($ddl1.val() === "4") {
            ShowElements([$txtZone11], div_row);
        } else {
            HideElements([$txtZone11], div_row);
        }
    } else {
        HideElements([$txtZone10, $txtZone11, $txtZone12], div_row);
    }
    if ($chk04.is(':checked') || $chk03.is(':checked') || $chk05.is(':checked')) {
        ShowElements([$ddl4], div_row);
    } else {
        HideElements([$ddl4], div_row);
    }
    errPrivilèges3();
}).change();

$('#txtZone10').on('keyup change', function () {
    $('#chk05').change();
});

$('#chk07').change(function () {
    if ($(this).is(':checked')) {
        ShowElements([$txtZone13], div_row);
    } else {
        HideElements([$txtZone13], div_row);
    }
}).change();

$('#chkUtilisation_du_futur_tarif').change(function () {
    if ($(this).is(':checked')) {
        $('#hdUtilisation_du_futur_tarif').val('1');
    } else {
        $('#hdUtilisation_du_futur_tarif').val('0');
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
        ["Emoluments du notaire", parseFloat($('#H45').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#H46').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#H44').text().replace('€', '').replace(' ', ''))]
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Les prêts hypothécaires</font></b></td></tr>";
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
    //Caracteristique de l'emprunt
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Caracteristique de l'emprunt</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Choisissez le type de prêt :</td>";
    html += "<td width='100%'>" + $('#ddl1 option:selected').text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($txtZone01.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant total du prêt :</td>";
        html += "<td width='100%' align='right'>" + $txtZone01.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone02.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Partie du prêt au taux normal :</td>";
        html += "<td width='100%' align='right'>" + $txtZone02.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($txtZone02_1.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Partie prêt conventionné, PEL, prêt aidé (art L 312-1 du CCH) :</td>";
        html += "<td width='100%' align='right'>" + $txtZone02_1.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Garanties
    if ($ddl2.css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Garanties</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Evaluation des intérêts et accessoires :</td>";
        html += "<td width='100%' align='right'>" + $('#ddl2 option:selected').text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }
    //Cautionnement
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Cautionnement</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Intervention de caution(s) dans l'acte authentique ?</td>";
    html += "<td width='100%' align='right'>" + ($chk01.is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($txtZone03.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant cautionné (intérêts et accessoires compris) :</td>";
        html += "<td width='100%' align='right'>" + $txtZone03.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($chk02.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>La caution est-elle hypothécaire ?</td>";
        html += "<td width='100%' align='right'>" + ($chk02.is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        if ($chk02.is(':checked')) {
            html += "<tr>";
            html += "<td width='40%' align='right' colspan='2'>Nombre de biens grevés :</td>";
            html += "<td width='100%' align='right'>" + $('#ddl3 option:selected').text() + "</td>";
            html += "<tr><td colspan='4'></td></tr>";
            html += "</tr>";
            html += "<tr><td colspan='4'>Montant inscrit sur les biens immobiliers en capital, intérêts et accessoires :</td></tr>";
            for (var i = 1; i <= parseInt($ddl3.val()); i++) {
                html += "<tr>";
                html += "<td width='40%' align='right' colspan='2'>" + i + "er Bien :</td>";
                html += "<td width='100%' align='right'>" + $('#txtZone0' + (i + 3)).val() + " €</td>";
                html += "<tr><td colspan='4'></td></tr>";
                html += "</tr>";
            }
        }
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Privilèges et hypothèque conventionelle
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Privilèges et hypothèque conventionelle</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Privilége de vendeur ?</td>";
    html += "<td width='100%' align='right'>" + ($chk03.is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Privilège de Prêteur de Deniers ?</td>";
    html += "<td width='100%' align='right'>" + ($chk04.is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($txtZone09.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant garanti :</td>";
        html += "<td width='100%' align='right'>" + $txtZone09.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#errPrivilèges1').css('display') !== none) {
        html += "<tr><td colspan='4' align='center'><font color='#FF0000'>Valeur impossible. Corrigez le montant.</font></td></tr>";
    }
    if ($ddl4.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Nombre de bureaux des hypothèques requis :</td>";
        html += "<td width='100%' align='right'>" + $('#ddl4 option:selected').text() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Hypothèque Conventionnelle ?</td>";
    html += "<td width='100%' align='right'>" + ($chk05.is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($txtZone10.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant garanti :</td>";
        html += "<td width='100%' align='right'>" + $txtZone10.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    //if ($('#errPrivilèges2').css('display') !== none) {
    //    html += "<tr><td colspan='4' align='center'><font color='#FF0000'>Valeur impossible. Corrigez le montant.</font></td></tr>";
    //}
    if ($txtZone11.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Provenant du prêt normal à hauteur de :</td>";
        html += "<td width='100%' align='right'>" + $txtZone11.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4' align='center'><font color='#FF0000'>" + $('#errPrivilèges3').text() + "</font></td></tr>";
    if ($txtZone12.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant dispensé de l'inscription hypothécaire par la banque :</td>";
        html += "<td width='100%' align='right'>" + $txtZone12.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Nantissement
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Nantissement</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Nantissement donné par un tiers ?</td>";
    html += "<td width='100%' align='right'>" + ($('#chk06').is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Dispositions particulières
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Dispositions particulières</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Cession d'antériorité (disposition indépendante) ?</td>";
    html += "<td width='100%' align='right'>" + ($('#chk07').is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($txtZone13.css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Montant profitant de l'antériorité :</td>";
        html += "<td width='100%' align='right'>" + $txtZone13.val() + " €</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Le prêt bénéficie-t'il d'une dispense ou exonération de droits d'enregistrement ?</td>";
    html += "<td width='100%' align='right'>" + ($('#chk08').is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
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