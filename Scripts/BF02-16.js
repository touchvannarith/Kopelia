function ShowHideElements() {
    if ($('#radTestament1').is(':checked')) {
        ShowElements([$('#txtNumPages'), $('#txtNumCopies'), $('#txtNumExp')], "div.row");
        HideElements([$('#radTestament4'), $('#radTestament5')], "div.row");
        $('#sub1').show();
    } else if ($('#radTestament3').is(':checked')) {
        HideElements([$('#txtNumPages'), $('#txtNumCopies'), $('#txtNumExp')], "div.row");
        ShowElements([$('#radTestament4'), $('#radTestament5')], "div.row");
        $('#sub1').hide();
    } else {
        HideElements([$('#txtNumPages'), $('#txtNumCopies'), $('#txtNumExp'), $('#radTestament4'), $('#radTestament5')], "div.row");
        $('#sub1').hide();
    }
    if ($('#radTestament5').is(':checked')) {
        ShowElements([$('#chkTestament1')], "div.row");
    } else {
        HideElements([$('#chkTestament1'), $('#txtTestament')], "div.row");
    }
    if ($('#chkTestament1').is(":checked")) {
        ShowElements([$('#txtTestament')], "div.row");
        //$('#txtTestament').val("5");
    } else {
        HideElements([$('#txtTestament')], "div.row");
    }
}

function SetDefaultValues() {
    if ($("#radTestament1").is(":checked")) {
        var page = $('#txtNumPages').val() === "" ? 0 : parseInt($('#txtNumPages').val());
        var copies = $('#txtNumCopies').val() === "" ? 0 : parseInt($('#txtNumCopies').val());
        var authentique = $('#txtNumExp').val() === "" ? 0 : parseInt($('#txtNumExp').val());
        var valueCopieAuthentique = page * authentique * 1.13;
        var valueCopieLibre = page * copies * 0.38;
        var valueArchivage = page * 0.19;
        var total = valueCopieAuthentique + valueCopieLibre + valueArchivage + 50.00;
        $('#txtEmolument_de_formalités_HT').val(total.toFixed(2).replace(".", ","));
    } else {
        $('#txtEmolument_de_formalités_HT').val("50,00");
    }
}

ShowHideElements();

if ($("#postback").val() === "false") {
    ShowHide_input_form($("#btnInputForm1"));
    SetDefaultValues();
}
$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

$('#txtNumPages, #txtNumCopies, #txtNumExp').on("keypress change input", function () {
    SetDefaultValues();
});

$('.testament input').change(function () {
    if ($("#radTestament3").is(":checked")) {
        $('#radTestament4').prop("checked", true);
    }
});

$('.testament input, .testament-sub input, #chkTestament1').change(function () {
    ShowHideElements();
    if ($("#radTestament1").is(":checked")) {
        $('#txtNumPages').val("5");
        $('#txtNumCopies').val("1");
        $('#txtNumExp').val("1");
    }
    if ($('#chkTestament1').is(":checked")) {
        $('#txtTestament').val("5");
    }
    SetDefaultValues();
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
        ['Emoluments HT du notaire', parseFloat($('#D828').text().replace('€', '').replace(' ', ''))],
        ['Débours', parseFloat($('#D829').text().replace('€', '').replace(' ', ''))],
        ['Trésor public', parseFloat($('#D830').text().replace('€', '').replace(' ', ''))]
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Testaments (authentique et olographe)</font></b></td></tr>";
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
    html += "<td width='40%' align='right' colspan='2'>S'agit-il d'un testament authentique ?</td>";
    html += "<td width='100%' align='right'>" + ($("#radTestament1").is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>S'agit-il d'un testament mystique ?</td>";
    html += "<td width='100%' align='right'>" + ($("#radTestament2").is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>S'agit-il d'un testament olographe ?</td>";
    html += "<td width='100%' align='right'>" + ($("#radTestament3").is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($('#radTestament4').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Est-ce la garde de testament avant le décés ?</td>";
        html += "<td width='100%' align='right'>" + ($("#radTestament4").is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#radTestament5').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Est-ce le procés verbal de dépôt du testament ?</td>";
        html += "<td width='100%' align='right'>" + ($("#radTestament5").is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#chkTestament1').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Y-a-t-il dépôt de codicille(s) ?</td>";
        html += "<td width='100%' align='right'>" + ($("#chkTestament1").is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    if ($('#txtTestament').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Nombre de codicille(s) ?</td>";
        html += "<td width='100%' align='right'>" + $("#txtTestament").val() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    // Emoluments de formalités afférents á la rédaction de l'acte
    if ($('#sub1').css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités afférents à la rédaction de l'acte</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Veuillez indiquer le nombre de pages :</td>";
        html += "<td width='100%' align='right'>" + $("#txtNumPages").val() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Nombre de copies :</td>";
        html += "<td width='100%' align='right'>" + $("#txtNumCopies").val() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr>";
        html += "<td width='40%' colspan='2' align='right'>Nombre de copies authentiques :</td>";
        html += "<td width='100%' align='right'>" + $("#txtNumExp").val() + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
        html += "<tr><td colspan='4'></td></tr>";
    }
    // Emoluments de formalités et Débours
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Emoluments de formalités et Débours</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Emolument de formalités HT :</td>";
    html += "<td width='100%' align='right'>" + $("#txtEmolument_de_formalités_HT").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Débours :</td>";
    html += "<td width='100%' align='right'>" + $("#txtDébours").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "</table>";
    $('#hdSaisie').val(html);
}

$('.btn-calculate, #btnSendEmail, #btnPrint').click(function () {
    htmlReport();
});