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

$('#ddl0').change(function () {
    ShowElements([$('#chk1'), $('#chk3')], "div.row");
    ShowElements([$('#chk4')], 'div#mainConditionsParticulieres');
    switch (this.value) {
        case "1":
            HideElements([$('#chk3')], "div.row");
            $('#lblDesc').text("Bail d'habitation établi conformément à la Loi 89-462 du 6 Juillet 1989.");
            break;
        case "2":
            HideElements([$('#chk3')], "div.row");
            $('#lblDesc').text("Bail d'un local affecté à un usage exclusivement professionnel et devant être conclu uniquement par les professions dont les revenus sont imposés dans la catégorie des bénéfices non commerciaux (BNC).");
            break;
        case "3":
            HideElements([$('#chk3')], "div.row");
            $('#lblDesc').text("Bail portant sur un usage professionnel et d'habitation.");
            break;
        case "5":
            HideElements([$('#chk1')], "div.row");
            $('#lblDesc').text("Bail concernant la jouissance d'un fond de terre ou d'un bien rural à destination agricole soumis ou non au statut du fermage.");
            break;
        case "4":
            HideElements([$('#chk4')], 'div#mainConditionsParticulieres');
            HideElements([$('#chk1'), $('#chk3')], "div.row");
            $('#lblDesc').text("Bail pour un usage d'habitation ou professionnel établi pour une durée inférieure à 13 ans.");
            break;
        case "6":
            HideElements([$('#chk4')], 'div#mainConditionsParticulieres');
            HideElements([$('#chk1'), $('#chk3')], "div.row");
            $('#lblDesc').text("Bail pour un usage rural établi pour une durée inférieure à 13 ans.");
            break;
        case "7":
            HideElements([$('#chk4')], 'div#mainConditionsParticulieres');
            HideElements([$('#chk1'), $('#chk3')], "div.row");
            $('#lblDesc').text("Bail portant sur un local à usage commercial et régi par les articles L145-1 à L145-60 du Code de Commerce.");
            break;
        default:
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
        ['Emoluments du notaire', parseFloat($('#lblEmoluments_TTC_du_notaire').text().replace('€', '').replace(' ', ''))],
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
    html += "<tr><td colspan='4' bgcolor='#304F73' align='center'><b><font color='#FFFFFF' size='11px'>Les baux de droit commun (- de 12 ans)</font></b></td></tr>";
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
    //Sur le type de bail
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Sur le type de bail</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Choix de l'acte :</td>";
    html += "<td width='100%' align='left' colspan='2'>" + $("#ddl0 option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr><td colspan='4' align='center'>" + $("#lblDesc").text() + "</td></tr>";
    html += "<tr><td colspan='4'></td></tr>";
    //Donnees economiques du bail
    html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Donnees economiques du bail</font></b></td></tr>";
    html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Montant du loyer mensuel :</td>";
    html += "<td width='100%' align='right'>" + $("#txtAmountRent").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' colspan='2' align='right'>Montant des charges mensuelles :</td>";
    html += "<td width='100%' align='right'>" + $("#txtAmountCharge").val() + " €</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Durée en années :</td>";
    html += "<td width='100%' align='right'>" + $("#ddlDurée option:selected").text() + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($('#chk1').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Le rédacteur a-t-il négocié le bail ?</td>";
        html += "<td width='100%' align='right'>" + ($("#chk1").is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr>";
    html += "<td width='40%' align='right' colspan='2'>Le bail est-il présenté volontairement à l'administration fiscale ?</td>";
    html += "<td width='100%' align='right'>" + ($("#chk2").is(':checked') ? "OUI" : "NON") + "</td>";
    html += "<tr><td colspan='4'></td></tr>";
    html += "</tr>";
    if ($('#chk3').css('display') !== none) {
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Bail à ferme - Renouvellement ou prorogation ?</td>";
        html += "<td width='100%' align='right'>" + ($("#chk3").is(':checked') ? "OUI" : "NON") + "</td>";
        html += "<tr><td colspan='4'></td></tr>";
        html += "</tr>";
    }
    html += "<tr><td colspan='4'></td></tr>";
    //Conditions particulieres
    if ($('#chk4').css('display') !== none) {
        html += "<tr><td colspan='4' bgcolor='#01ABE4' align='center'><b><font color='#FFFFFF' size='11px'>Conditions particulieres</font></b></td></tr>";
        html += "<tr><td colspan='4' bgcolor='#01ABE4'></td></tr>";
        html += "<tr>";
        html += "<td width='40%' align='right' colspan='2'>Bail à ferme - Renouvellement ou prorogation ?</td>";
        html += "<td width='100%' align='right'>" + ($("#chk4").is(':checked') ? "OUI" : "NON") + "</td>";
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