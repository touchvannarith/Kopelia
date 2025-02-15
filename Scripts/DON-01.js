$(".select2").select2({
    //placeholder: "Sélectionnez un acte ...",
    allowClear: true,
    minimumResultsForSearch: -1
});
var resultSynthese, resultSyntheseTaxePere, resultSyntheseTaxeMere, resultTaxeDonation, resultSynthaseDonation, enumCalculation;
var ddl1Value, ddl3Value;
var arrImmobilier = [], arrMobilier = [], arrImmobilierexonere = [], arrMobilierexonere = [], arrSommeargent = [], arrRappelDeDonationsAnterieures = [];
var arrDonatairesDeDegre = [], arrNatureDeLaDonation = [];
var determinationDesDonataires = {}, determinationDesBiens = {};

if ($("#postback").val() === "false") {
    $("#divLibelleSimulation, #divSendEmail, #divPrint").hide();
    $('#txtZone01a, #txtZone01b').val("02/01/1953");
    ShowHide_input_form($("#btnInputForm1"));

    //Détermination des biens
    for (var i = 0; i < 20; i++) {
        arrImmobilier.push({ origine: "", reserve: false, valeur: "", attribution: "", passif: "" });
        arrMobilier.push({ origine: "", reserve: false, valeur: "", attribution: "", passif: "" });
    }
    for (var i = 0; i < 2; i++) {
        arrImmobilierexonere.push({ typedebien: "", typedebienText: "", origine: "", reserve: false, valeur: "", attribution: "", passif: "" });
        arrMobilierexonere.push({ typedebien: "", typedebienText: "", origine: "", reserve: false, valeur: "", attribution: "", passif: "" });
    }
    for (var i = 0; i < 10; i++) {
        arrSommeargent.push({ origine: "", valeur: "", attribution: "" });
    }
    determinationDesBiens = {
        "immobilierCount": 0, "immobilierexonereCount": 0, "mobilierCount": 0, "mobilierexonereCount": 0, "sommeargentCount": 0,
        "arrImmobilier": arrImmobilier, "arrImmobilierexonere": arrImmobilierexonere, "arrMobilier": arrMobilier, "arrMobilierexonere": arrMobilierexonere, "arrSommeargent": arrSommeargent
    }

    //Rappel des donations antérieures
    for (var i = 1; i <= 10; i++) {
        var arrPere = [], arrMere = [];
        for (var j = 1; j <= 10; j++) {
            arrPere.push({ "Id": j, "txtZone01": "", "txtZone02": "", "txtZone03": "", "txtZone04": "", "txtZone05": "", "txtZone06": "", "txtZone07": "", "txtZone00_": $('#txtZone00_').val(), "chk01": true });
            arrMere.push({ "Id": j, "txtZone01": "", "txtZone02": "", "txtZone03": "", "txtZone04": "", "txtZone05": "", "txtZone06": "", "txtZone07": "", "txtZone00_": $('#txtZone00_').val(), "chk01": true });
        }
        arrRappelDeDonationsAnterieures.push({ "Id": i, "Pere": arrPere, "Mere": arrMere });
    }

    //Détermination des donataires
    for (var i = 1; i <= 10; i++) {
        arrDonatairesDeDegre.push({ "index": i, "txtZone01": $("#txtZone01_donataire_" + i).val(), "cb2": $("#cb2_donataire_" + i).is(":checked"), "cb1": $("#cb1_donataire_" + i).is(":checked"), "ddl4": $("#ddl4_donataire_" + i).val(), "ddl5": $("#ddl5_donataire_" + i).val() });
        arrNatureDeLaDonation.push({ "index": i, "txtZone01": $("#TextBox" + i).val(), "txtZone02": $("#txtZone02_donataire_" + i).val(), "txtZone03": $("#txtZone03_donataire_" + i).val() });
    }
    determinationDesDonataires = {
        "txtZone00": $("#txtZone00").val(), "ddl1": $("#ddl1").val(), "ddl1Text": $("#ddl1").text(), "txtZone01a": $("#txtZone01a").val(), "txtZone01b": $("#txtZone01b").val(), "ddl3": $("#ddl3").val(),
        "arrDonatairesDeDegre": arrDonatairesDeDegre, "ddl6": $("#ddl6").val(), "ddl7": $("#ddl7").val(), "arrNatureDeLaDonation": arrNatureDeLaDonation,
        "txtZone02Total": $("#txtZone02_donataire_total").val(), "txtZone03Total": $("#txtZone03_donataire_total").val(), "msgTxtZone02AndTxtZone03": ""
    };
}

if ($("#postback").val() === "true") {
    determinationDesDonataires = $.parseJSON($("#hdJsonDeterminationDesDonataires").val());
    $("#txtZone00").val(determinationDesDonataires.txtZone00);
    $("#ddl1").val(determinationDesDonataires.ddl1).trigger("change");
    $("#txtZone01a").val(determinationDesDonataires.txtZone01a);
    $("#txtZone01b").val(determinationDesDonataires.txtZone01b);
    $("#ddl3").val(determinationDesDonataires.ddl3).trigger("change");
    for (var i = 1; i <= 10; i++) {
        $("#txtZone01_donataire_" + i).val(determinationDesDonataires.arrDonatairesDeDegre[i - 1].txtZone01);
        $("#cb2_donataire_" + i).prop('checked', determinationDesDonataires.arrDonatairesDeDegre[i - 1].cb2);
        $("#cb1_donataire_" + i).prop('checked', determinationDesDonataires.arrDonatairesDeDegre[i - 1].cb1);
        $("#ddl4_donataire_" + i).val(determinationDesDonataires.arrDonatairesDeDegre[i - 1].ddl4);
        $("#ddl5_donataire_" + i).val(determinationDesDonataires.arrDonatairesDeDegre[i - 1].ddl5);
        $("#txtZone02_donataire_" + i).val(determinationDesDonataires.arrNatureDeLaDonation[i - 1].txtZone02);
        $("#txtZone03_donataire_" + i).val(determinationDesDonataires.arrNatureDeLaDonation[i - 1].txtZone03);
    }
    $("#ddl6").val(determinationDesDonataires.ddl6).trigger("change");
    $("#ddl7").val(determinationDesDonataires.ddl7).trigger("change");

    determinationDesBiens = $.parseJSON($("#hdJsonDeterminationDesBiens").val());
    arrRappelDeDonationsAnterieures = $.parseJSON($("#hdJsonRappelDeDonationsAnterieures").val());
}

$(".btn-calculate").click(function () {
    $("#hdJsonDeterminationDesDonataires").val(JSON.stringify(determinationDesDonataires));
    $("#hdJsonDeterminationDesBiens").val(JSON.stringify(determinationDesBiens))
    $("#hdJsonRappelDeDonationsAnterieures").val(JSON.stringify(arrRappelDeDonationsAnterieures))
    $("#divLibelleSimulation, #divSendEmail, #divPrint").show();
    $("#preloader").show();
});

function ShowElementsSection(section, index) {
    var origine = "";
    if (ddl1Value === 1) {
        origine = "Mr";
    } else if (ddl1Value === 2) {
        origine = "Mme";
    } else if (ddl1Value === 3) {
        origine = "Commun";
    }
    if (section === "immobilier") {
        $("#ddl1_immobilier_" + index).val(origine).trigger("change");
        $("#ddl2_immobilier_" + index).trigger("change");
        $("#cb1_immobilier_" + index).change();
    } else if (section === "mobilier") {
        $("#ddl1_mobilier_" + index).val(origine).change();
        $("#ddl2_mobilier_" + index).trigger("change");
        $("#cb1_mobilier_" + index).change();
    } else if (section === "sommeargent") {
        $("#ddl1_sommeargent_" + index).val(origine).change();
        $("#ddl2_sommeargent_" + index).trigger("change");
    } else if (section === "immobilierexonéré") {
        $("#ddl1_immobilierexonéré_" + index).val(origine).change();
        $("#ddl2_immobilierexonéré_" + index).trigger("change");
        $("#ddl3_immobilierexonéré_" + index).trigger("change");
        $("#cb1_immobilierexonéré_" + index).change();
    } else if (section === "mobilierexonéré") {
        $("#ddl1_mobilierexonéré_" + index).val(origine).change();
        $("#ddl2_mobilierexonéré_" + index).trigger("change");
        $("#ddl3_mobilierexonéré_" + index).trigger("change");
        $("#cb1_mobilierexonéré_" + index).change();
    }
}

function HideElementsSection(section, index) {
    if (section === "immobilier") {
        $("#div_immobilier_" + index).hide();
        HideElements([$("#ddl1_immobilier_" + index), $("#cb1_immobilier_" + index), $("#txtZone01_immobilier_" + index), $("#ddl2_immobilier_" + index), $("#txtZone02_immobilier_" + index)], "");
    } else if (section === "mobilier") {
        $("#div_mobilier_" + index).hide();
        HideElements([$("#ddl1_mobilier_" + index), $("#cb1_mobilier_" + index), $("#txtZone01_mobilier_" + index), $("#ddl2_mobilier_" + index), $("#txtZone02_mobilier_" + index)], "");
    } else if (section === "sommeargent") {
        $("#div_sommeargent_" + index).hide();
        HideElements([$("#ddl1_sommeargent_" + index), $("#txtZone01_sommeargent_" + index), $("#ddl2_sommeargent_" + index)], "");
    } else if (section === "immobilierexonéré") {
        $("#div_immobilierexonéré_" + index).hide();
        HideElements([$("#ddl3_immobilierexonéré_" + index), $("#ddl1_immobilierexonéré_" + index), $("#cb1_immobilierexonéré_" + index), $("#txtZone01_immobilierexonéré_" + index), $("#ddl2_immobilierexonéré_" + index), $("#txtZone02_immobilierexonéré_" + index)], "");
    } else if (section === "mobilierexonéré") {
        $("#div_mobilierexonéré_" + index).hide();
        HideElements([$("#ddl4_mobilierexonéré_" + index), $("#ddl1_mobilierexonéré_" + index), $("#cb1_mobilierexonéré_" + index), $("#txtZone01_mobilierexonéré_" + index), $("#ddl2_mobilierexonéré_" + index), $("#txtZone02_mobilierexonéré_" + index)], "");
    }
}

function numberFormatFr() {
    $(".numberFormatFR").on("keypress change input", function (event) {
        $(this).val($(this).val().replace(/[^0-9\,]/g, ''));
        if ((($(this).val().indexOf(',') !== -1) && (event.which < 48 || event.which > 57)) || event.which === 46) {
            event.preventDefault();
        }
    });
}

function setOrigine(section, idx) {
    $("#ddl1_" + section + "_" + idx).on("change", function (event) {
        switch (section) {
            case "immobilier":
                determinationDesBiens.arrImmobilier[idx - 1].origine = $(this).val();
                break;
            case "immobilierexonéré":
                determinationDesBiens.arrImmobilierexonere[idx - 1].origine = $(this).val();
                break;
            case "mobilier":
                determinationDesBiens.arrMobilier[idx - 1].origine = $(this).val();
                break;
            case "mobilierexonéré":
                determinationDesBiens.arrMobilierexonere[idx - 1].origine = $(this).val();
                break;
            case "sommeargent":
                determinationDesBiens.arrSommeargent[idx - 1].origine = $(this).val();
                break;
            default:
        }
    });
}

function setReserve(section, idx) {
    $("#cb1_" + section + "_" + idx).change(function () {
        switch (section) {
            case "immobilier":
                determinationDesBiens.arrImmobilier[idx - 1].reserve = $(this).is(":checked");
                break;
            case "immobilierexonéré":
                determinationDesBiens.arrImmobilierexonere[idx - 1].reserve = $(this).is(":checked");
                break;
            case "mobilier":
                determinationDesBiens.arrMobilier[idx - 1].reserve = $(this).is(":checked");
                break;
            case "mobilierexonéré":
                determinationDesBiens.arrMobilierexonere[idx - 1].reserve = $(this).is(":checked");
                break;
            case "sommeargent":
                break;
            default:
        }
    });
}

function setValeurPP(section, idx) {
    $("#txtZone01_" + section + "_" + idx).on("keypress change input", function (event) {
        switch (section) {
            case "immobilier":
                determinationDesBiens.arrImmobilier[idx - 1].valeur = $(this).val();
                break;
            case "immobilierexonéré":
                determinationDesBiens.arrImmobilierexonere[idx - 1].valeur = $(this).val();
                break;
            case "mobilier":
                determinationDesBiens.arrMobilier[idx - 1].valeur = $(this).val();
                break;
            case "mobilierexonéré":
                determinationDesBiens.arrMobilierexonere[idx - 1].valeur = $(this).val();
                break;
            case "sommeargent":
                determinationDesBiens.arrSommeargent[idx - 1].valeur = $(this).val();
                break;
            default:
        }
    });
}

function setAttribution(section, idx) {
    $("#ddl2_" + section + "_" + idx).on("change", function (event) {
        switch (section) {
            case "immobilier":
                determinationDesBiens.arrImmobilier[idx - 1].attribution = $(this).val();
                break;
            case "immobilierexonéré":
                determinationDesBiens.arrImmobilierexonere[idx - 1].attribution = $(this).val();
                break;
            case "mobilier":
                determinationDesBiens.arrMobilier[idx - 1].attribution = $(this).val();
                break;
            case "mobilierexonéré":
                determinationDesBiens.arrMobilierexonere[idx - 1].attribution = $(this).val();
                break;
            case "sommeargent":
                determinationDesBiens.arrSommeargent[idx - 1].attribution = $(this).val();
                break;
            default:
        }
    });
}

function setPassif(section, idx) {
    $("#txtZone02_" + section + "_" + idx).on("keypress change input", function (event) {
        switch (section) {
            case "immobilier":
                determinationDesBiens.arrImmobilier[idx - 1].passif = $(this).val();
                break;
            case "immobilierexonéré":
                determinationDesBiens.arrImmobilierexonere[idx - 1].passif = $(this).val();
                break;
            case "mobilier":
                determinationDesBiens.arrMobilier[idx - 1].passif = $(this).val();
                break;
            case "mobilierexonéré":
                determinationDesBiens.arrMobilierexonere[idx - 1].passif = $(this).val();
                break;
            case "sommeargent":
                break;
            default:
        }
    });
}

function setTypeDeBien(section, idx) {
    $("#ddl3_" + section + "_" + idx).on("change", function (event) {
        switch (section) {
            case "immobilier":
                break;
            case "immobilierexonéré":
                determinationDesBiens.arrImmobilierexonere[idx - 1].typedebien = $(this).val();
                determinationDesBiens.arrImmobilierexonere[idx - 1].typedebienText = $("#ddl3_" + section + "_" + idx + " option:selected").text();
                break;
            case "mobilier":
                break;
            case "mobilierexonéré":
                determinationDesBiens.arrMobilierexonere[idx - 1].typedebien = $(this).val();
                determinationDesBiens.arrMobilierexonere[idx - 1].typedebienText = $("#ddl3_" + section + "_" + idx + " option:selected").text();
                break;
            case "sommeargent":
                break;
            default:
        }
    });
}

function removeImmobilier(index) {
    if (index < determinationDesBiens.immobilierCount) {
        for (i = index; i < determinationDesBiens.immobilierCount; i++) {
            AssignValues("immobilier", i, i + 1);
        }
        $("#div_immobilier_" + determinationDesBiens.immobilierCount).remove();
    } else {
        $("#div_immobilier_" + determinationDesBiens.immobilierCount).remove();
    }
    determinationDesBiens.immobilierCount -= 1;
    ShowElements([$("#btnAddImmobilier")], "div");
    determinationDesBiens.arrImmobilier.splice(index - 1, 1);
    determinationDesBiens.arrImmobilier.push({ origine: "", reserve: false, valeur: "", attribution: "", passif: "" });
}

function removeImmobilierExonéré(index) {
    if (index < determinationDesBiens.immobilierexonereCount) {
        for (i = index; i < determinationDesBiens.immobilierexonereCount; i++) {
            AssignValues("immobilierexonéré", i, i + 1);
        }
        $("#div_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount).remove();
    } else {
        $("#div_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount).remove();
    }
    determinationDesBiens.immobilierexonereCount -= 1;
    ShowElements([$("#btnAddImmobilierExonéré")], "div");
    determinationDesBiens.arrImmobilierexonere.splice(index - 1, 1);
    determinationDesBiens.arrImmobilierexonere.push({ typedebien: "", typedebienText: "", origine: "", reserve: false, valeur: "", attribution: "", passif: "" });
}

function removeMobilier(index) {
    if (index < determinationDesBiens.mobilierCount) {
        for (i = index; i < determinationDesBiens.mobilierCount; i++) {
            AssignValues("mobilier", i, i + 1);
        }
        $("#div_mobilier_" + determinationDesBiens.mobilierCount).remove();
    } else {
        $("#div_mobilier_" + determinationDesBiens.mobilierCount).remove();
    }
    determinationDesBiens.mobilierCount -= 1;
    ShowElements([$("#btnAddMobilier")], "div");
    determinationDesBiens.arrMobilier.splice(index - 1, 1);
    determinationDesBiens.arrMobilier.push({ origine: "", reserve: false, valeur: "", attribution: "", passif: "" });
}

function removeSommeargent(index) {
    if (index < determinationDesBiens.sommeargentCount) {
        for (i = index; i < determinationDesBiens.sommeargentCount; i++) {
            AssignValues("sommeargent", i, i + 1);
        }
        $("#div_sommeargent_" + determinationDesBiens.sommeargentCount).remove();
    } else {
        $("#div_sommeargent_" + determinationDesBiens.sommeargentCount).remove();
    }
    determinationDesBiens.sommeargentCount -= 1;
    ShowElements([$("#btnAddSommeArgent")], "div");
    determinationDesBiens.arrSommeargent.splice(index - 1, 1);
    determinationDesBiens.arrSommeargent.push({ origine: "", valeur: "", attribution: "" });
}

function removeMobilierExonéré(index) {
    if (index < determinationDesBiens.mobilierexonereCount) {
        for (i = index; i < determinationDesBiens.mobilierexonereCount; i++) {
            AssignValues("mobilierexonéré", i, i + 1);
        }
        $("#div_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount).remove();
    } else {
        $("#div_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount).remove();
    }
    determinationDesBiens.mobilierexonereCount -= 1;
    ShowElements([$("#btnAddMobilierExonéré")], "div");
    determinationDesBiens.arrMobilierexonere.splice(index - 1, 1);
    determinationDesBiens.arrMobilierexonere.push({ typedebien: "", typedebienText:"", origine: "", reserve: false, valeur: "", attribution: "", passif: "" });
}

function createLabelOrigine() {
    const labelOrigine = document.createElement("label");
    labelOrigine.setAttribute("class", "col-sm-2 col-12 col-form-label text-left text-sm-right");
    labelOrigine.innerHTML = "Origine :"
    return labelOrigine;
}

function createSelectOrigine(section, idx) {
    const divOrigine = document.createElement("div");
    divOrigine.setAttribute("class", "col-sm-3 col-12");
    const selectOrigine = document.createElement("select");
    selectOrigine.setAttribute("class", "form-control select2 ddl1");
    selectOrigine.setAttribute("id", "ddl1_" + section + "_" + idx);
    selectOrigine.setAttribute("name", "ddl1_" + section + "_" + idx);
    if (ddl1Value !== 3) {
        selectOrigine.setAttribute("disabled", "disabled");
    }
    selectOrigine.onchange = function () { setOrigine(section, idx); }
    divOrigine.appendChild(selectOrigine);
    return divOrigine;
}

function createCheckboxReserve(section, idx) {
    const spanCheckbox = document.createElement("span");
    spanCheckbox.setAttribute("class", "col-sm-2 col-12 checkbox");
    const checkbox = document.createElement("input");
    checkbox.setAttribute("type", "checkbox");
    checkbox.setAttribute("id", "cb1_" + section + "_" + idx);
    checkbox.setAttribute("name", "cb1_" + section + "_" + idx);
    checkbox.onchange = function () { setReserve(section, idx); }
    const labelCheckbox = document.createElement("label");
    labelCheckbox.setAttribute("for", "cb1_" + section + "_" + idx);
    labelCheckbox.innerHTML = "Réserve d'usufruit";
    spanCheckbox.appendChild(checkbox);
    spanCheckbox.appendChild(labelCheckbox);
    return spanCheckbox;
}

function createLabelValeurPP() {
    const labelValeurPP = document.createElement("label");
    labelValeurPP.setAttribute("class", "col-sm-2 col-12 col-form-label text-left text-sm-right");
    labelValeurPP.innerHTML = "Valeur PP :"
    return labelValeurPP;
}

function createTextBoxValeurPP(section, idx) {
    const divValeurPP = document.createElement("div");
    divValeurPP.setAttribute("class", "col-sm-3 col-12");
    const inputValeurPP = document.createElement("input");
    inputValeurPP.setAttribute("type", "text");
    inputValeurPP.setAttribute("id", "txtZone01_" + section + "_" + idx);
    inputValeurPP.setAttribute("name", "txtZone01_" + section + "_" + idx);
    inputValeurPP.setAttribute("class", "form-control numberFormatFR");
    inputValeurPP.onkeypress = function () { numberFormatFr(); };
    inputValeurPP.oninput = function () { setValeurPP(section, idx); };
    const faValeur = document.createElement("i");
    faValeur.setAttribute("class", "form-control-feedback fa fa-euro");
    divValeurPP.appendChild(inputValeurPP);
    divValeurPP.appendChild(faValeur);
    return divValeurPP;
}

function createLabelAttribution() {
    const labelAttribution = document.createElement("label");
    labelAttribution.setAttribute("class", "col-sm-2 col-12 col-form-label text-left text-sm-right lblAttribution");
    labelAttribution.innerHTML = "Attribution :";
    return labelAttribution;
}

function createSelectAttribution(section, idx) {
    const divAttribution = document.createElement("div");
    divAttribution.setAttribute("class", "col-sm-5 col-12");
    const div = document.createElement("div");
    const selectAttribution = document.createElement("select");
    selectAttribution.setAttribute("class", "form-control select2 ddl2");
    selectAttribution.setAttribute("id", "ddl2_" + section + "_" + idx);
    selectAttribution.setAttribute("name", "ddl2_" + section + "_" + idx);
    selectAttribution.onchange = function () { setAttribution(section, idx); }
    div.appendChild(selectAttribution);
    divAttribution.appendChild(div);
    return divAttribution;
}

function createLabelPassif() {
    const labelPassif = document.createElement("label");
    labelPassif.setAttribute("class", "col-sm-2 col-12 col-form-label text-left text-sm-right");
    labelPassif.innerHTML = "Passif :"
    return labelPassif;
}
function createTextBoxPassif(section, idx) {
    const divPassif = document.createElement("div");
    divPassif.setAttribute("class", "col-sm-3 col-12");
    const inputPassif = document.createElement("input");
    inputPassif.setAttribute("type", "text");
    inputPassif.setAttribute("id", "txtZone02_" + section + "_" + idx);
    inputPassif.setAttribute("name", "txtZone02_" + section + "_" + idx);
    inputPassif.setAttribute("class", "form-control numberFormatFR");
    inputPassif.onkeypress = function () { numberFormatFr(); };
    inputPassif.oninput = function () { setPassif(section, idx); };
    const faPassif = document.createElement("i");
    faPassif.setAttribute("class", "form-control-feedback fa fa-euro");
    divPassif.appendChild(inputPassif);
    divPassif.appendChild(faPassif);
    return divPassif;
}

function createLabelTypeDeBien() {
    const labelTypeDeBien = document.createElement("label");
    labelTypeDeBien.setAttribute("class", "col-sm-2 col-12 col-form-label text-left text-sm-right");
    labelTypeDeBien.innerHTML = "Type de bien :"
    return labelTypeDeBien;
}
function createSelectTypeDeBien(section, idx) {
    const divTypeDeBien = document.createElement("div");
    divTypeDeBien.setAttribute("class", "col-sm-5 col-12");
    const div = document.createElement("div");
    const selectTypeDeBien = document.createElement("select");
    selectTypeDeBien.setAttribute("class", "form-control select2 ddl3");
    selectTypeDeBien.setAttribute("id", "ddl3_" + section + "_" + idx);
    selectTypeDeBien.setAttribute("name", "ddl3_" + section + "_" + idx);
    selectTypeDeBien.onchange = function () { setTypeDeBien(section, idx); }
    div.appendChild(selectTypeDeBien);
    divTypeDeBien.appendChild(div);
    return divTypeDeBien;
}

function createButtonRemove(section, idx) {
    const divRemove = document.createElement("div");
    divRemove.setAttribute("class", "text-center");
    const buttonRemove = document.createElement("button");
    buttonRemove.setAttribute("type", "button");
    buttonRemove.setAttribute("id", "btnRemove_" + section + "_" + idx);
    buttonRemove.setAttribute("name", "btnRemove_" + section + "_" + idx);
    buttonRemove.setAttribute("class", "btn btn-success btnRemove");
    buttonRemove.innerHTML = "Remove";
    switch (section) {
        case "immobilier" :
            buttonRemove.onclick = function () { removeImmobilier(idx); };
            break;
        case "immobilierexonéré" :
            buttonRemove.onclick = function () { removeImmobilierExonéré(idx); };
            break;
        case "mobilier":
            buttonRemove.onclick = function () { removeMobilier(idx); };
            break;
        case "sommeargent":
            buttonRemove.onclick = function () { removeSommeargent(idx); };
            break;
        case "mobilierexonéré":
            buttonRemove.onclick = function () { removeMobilierExonéré(idx); };
            break;
        default:
    }
    const hr = document.createElement("hr");
    divRemove.appendChild(buttonRemove);
    divRemove.appendChild(hr);
    return divRemove;
}

function AddImmobilier(index) {
    const section = "immobilier";
    const divImmobilier = document.createElement("div");
    divImmobilier.setAttribute("id", "div_immobilier_" + index);
    const label = document.createElement("label");
    label.innerHTML = "Bien Immobilier - Article " + index;

    const divRow1 = document.createElement("div");
    divRow1.setAttribute("class", "row form-group");
    //Oringine :
    const labelOrigine = createLabelOrigine();
    const divOrigine = createSelectOrigine(section, index);

    //Réserve d'usufruit
    const spanCheckbox = createCheckboxReserve(section, index);

    //Valeur PP :
    const labelValeurPP = createLabelValeurPP();
    const divValeurPP = createTextBoxValeurPP(section, index);

    const divRow2 = document.createElement("div");
    divRow2.setAttribute("class", "row form-group");
    //Attribution :
    const labelAttribution = createLabelAttribution();
    const divAttribution = createSelectAttribution(section, index);

    //Passif :
    const labelPassif = createLabelPassif();
    const divPassif = createTextBoxPassif(section, index);

    //btnRemove
    const divRemove = createButtonRemove(section, index);

    divRow1.appendChild(labelOrigine);
    divRow1.appendChild(divOrigine);
    divRow1.appendChild(spanCheckbox);
    divRow1.appendChild(labelValeurPP);
    divRow1.appendChild(divValeurPP);
    divRow2.appendChild(labelAttribution);
    divRow2.appendChild(divAttribution);
    divRow2.appendChild(labelPassif);
    divRow2.appendChild(divPassif);
    divImmobilier.appendChild(label);
    divImmobilier.appendChild(divRow1);
    divImmobilier.appendChild(divRow2);
    divImmobilier.appendChild(divRemove);

    const cardBody = document.getElementById("div_immobilier");
    const buttonAdd = document.getElementById("div_add_immobilier");
    cardBody.insertBefore(divImmobilier, buttonAdd);
    $("#ddl1_immobilier_" + index).html("").select2({
        allowClear: true,
        minimumResultsForSearch: -1,
        data: [
            { id: 'Commun', text: "Commun" },
            { id: 'Mr', text: "Mr" },
            { id: 'Mme', text: "Mme" }
        ]
    });
    $("#ddl3").change();
    ShowElementsSection(section, index);
    if (index >= 20) {
        HideElements([$("#btnAddImmobilier")], "div");
    }
}

function AddImmobilierExonere(index) {
    const section = "immobilierexonéré";
    const divImmobilier = document.createElement("div");
    divImmobilier.setAttribute("id", "div_immobilierexonéré_" + index);
    const label = document.createElement("label");
    label.innerHTML = "Bien Immobilier exonéré - Article " + index;

    const divRow = document.createElement("div");
    divRow.setAttribute("class", "row form-group");
    const labelTypeDeBien = createLabelTypeDeBien();
    const divTypeDeBien = createSelectTypeDeBien(section, index);
    divRow.appendChild(labelTypeDeBien);
    divRow.appendChild(divTypeDeBien);

    const divRow1 = document.createElement("div");
    divRow1.setAttribute("class", "row form-group");
    //Oringine :
    const labelOrigine = createLabelOrigine();
    const divOrigine = createSelectOrigine(section, index);
    //
    //Réserve d'usufruit
    const spanCheckbox = createCheckboxReserve(section, index);
    //
    //Valeur PP :
    const labelValeurPP = createLabelValeurPP();
    const divValeurPP = createTextBoxValeurPP(section, index);
    //
    divRow1.appendChild(labelOrigine);
    divRow1.appendChild(divOrigine);
    divRow1.appendChild(spanCheckbox);
    divRow1.appendChild(labelValeurPP);
    divRow1.appendChild(divValeurPP);

    const divRow2 = document.createElement("div");
    divRow2.setAttribute("class", "row form-group");
    //Attribution :
    const labelAttribution = createLabelAttribution();
    const divAttribution = createSelectAttribution(section, index);
    //
    //Passif :
    const labelPassif = createLabelPassif();
    const divPassif = createTextBoxPassif(section, index);
    //
    divRow2.appendChild(labelAttribution);
    divRow2.appendChild(divAttribution);
    divRow2.appendChild(labelPassif);
    divRow2.appendChild(divPassif);

    //btnRemove
    const divRemove = createButtonRemove(section, index);
    //
    divImmobilier.appendChild(label);
    divImmobilier.appendChild(divRow);
    divImmobilier.appendChild(divRow1);
    divImmobilier.appendChild(divRow2);
    divImmobilier.appendChild(divRemove);

    const cardBody = document.getElementById("div_immobilierexonéré");
    const buttonAdd = document.getElementById("div_add_immobilierexonéré");
    cardBody.insertBefore(divImmobilier, buttonAdd);
    $("#ddl1_immobilierexonéré_" + index).html("").select2({
        allowClear: true,
        minimumResultsForSearch: -1,
        data: [
            { id: 'Commun', text: "Commun" },
            { id: 'Mr', text: "Mr" },
            { id: 'Mme', text: "Mme" }
        ]
    });
    $("#ddl3_immobilierexonéré_" + index).html("").select2({
        allowClear: true,
        minimumResultsForSearch: -1,
        data: [
            { id: '0', text: "Sélectionnez un type de bien ...", },
            { id: '1', text: "Immeuble neuf entre 01/06/93 au 31/12/94" },
            { id: '2', text: "Immeuble neuf entre 01/08/95 au 31/12/95" },
            { id: '3', text: "Immeuble locatif entre 01/08/95 au 31/12/96" },
            { id: '4', text: "Immeuble monument historique" },
            { id: '5', text: "Bois et forêts" },
            { id: '6', text: "Bien rural loué par bail à long terme" }
        ]
    });
    $("#ddl3").change();
    ShowElementsSection(section, index);
    if (index >= 2) {
        HideElements([$("#btnAddImmobilierExonéré")], "div");
    }
}

function AddMobilier(index) {
    const section = "mobilier";
    const divImmobilier = document.createElement("div");
    divImmobilier.setAttribute("id", "div_mobilier_" + index);
    const label = document.createElement("label");
    label.innerHTML = "Bien Mobilier - Article " + index;

    const divRow1 = document.createElement("div");
    divRow1.setAttribute("class", "row form-group");
    //Oringine :
    const labelOrigine = createLabelOrigine();
    const divOrigine = createSelectOrigine(section, index);
    //
    //Réserve d'usufruit
    const spanCheckbox = createCheckboxReserve(section, index);
    //
    //Valeur PP :
    const labelValeurPP = createLabelValeurPP();
    const divValeurPP = createTextBoxValeurPP(section, index);
    //

    const divRow2 = document.createElement("div");
    divRow2.setAttribute("class", "row form-group");
    //Attribution :
    const labelAttribution = createLabelAttribution();
    const divAttribution = createSelectAttribution(section, index);
    //
    //Passif :
    const labelPassif = createLabelPassif();
    const divPassif = createTextBoxPassif(section, index);
    //

    //btnRemove
    const divRemove = createButtonRemove(section, index);
    //

    divRow1.appendChild(labelOrigine);
    divRow1.appendChild(divOrigine);
    divRow1.appendChild(spanCheckbox);
    divRow1.appendChild(labelValeurPP);
    divRow1.appendChild(divValeurPP);
    divRow2.appendChild(labelAttribution);
    divRow2.appendChild(divAttribution);
    divRow2.appendChild(labelPassif);
    divRow2.appendChild(divPassif);
    divImmobilier.appendChild(label);
    divImmobilier.appendChild(divRow1);
    divImmobilier.appendChild(divRow2);
    divImmobilier.appendChild(divRemove);

    const cardBody = document.getElementById("div_mobilier");
    const buttonAdd = document.getElementById("div_add_mobilier");
    cardBody.insertBefore(divImmobilier, buttonAdd);
    $("#ddl1_mobilier_" + index).html("").select2({
        allowClear: true,
        minimumResultsForSearch: -1,
        data: [
            { id: 'Commun', text: "Commun" },
            { id: 'Mr', text: "Mr" },
            { id: 'Mme', text: "Mme" }
        ]
    });
    $("#ddl3").change();
    ShowElementsSection(section, index);
    if (index >= 20) {
        HideElements([$("#btnAddMobilier")], "div");
    }
}

function AddMobilierExonere(index) {
    const section = "mobilierexonéré";
    const divImmobilier = document.createElement("div");
    divImmobilier.setAttribute("id", "div_mobilierexonéré_" + index);
    const label = document.createElement("label");
    label.innerHTML = "Bien Mobilier exonéré - Article " + index;

    const divRow = document.createElement("div");
    divRow.setAttribute("class", "row form-group");
    const labelTypeDeBien = createLabelTypeDeBien();
    const divTypeDeBien = createSelectTypeDeBien(section, index);
    divRow.appendChild(labelTypeDeBien);
    divRow.appendChild(divTypeDeBien);

    const divRow1 = document.createElement("div");
    divRow1.setAttribute("class", "row form-group");
    //Oringine :
    const labelOrigine = createLabelOrigine();
    const divOrigine = createSelectOrigine(section, index);
    //
    //Réserve d'usufruit
    const spanCheckbox = createCheckboxReserve(section, index);
    //
    //Valeur PP :
    const labelValeurPP = createLabelValeurPP();
    const divValeurPP = createTextBoxValeurPP(section, index);
    //
    divRow1.appendChild(labelOrigine);
    divRow1.appendChild(divOrigine);
    divRow1.appendChild(spanCheckbox);
    divRow1.appendChild(labelValeurPP);
    divRow1.appendChild(divValeurPP);

    const divRow2 = document.createElement("div");
    divRow2.setAttribute("class", "row form-group");
    //Attribution :
    const labelAttribution = createLabelAttribution();
    const divAttribution = createSelectAttribution(section, index);
    //
    //Passif :
    const labelPassif = createLabelPassif();
    const divPassif = createTextBoxPassif(section, index);
    //
    divRow2.appendChild(labelAttribution);
    divRow2.appendChild(divAttribution);
    divRow2.appendChild(labelPassif);
    divRow2.appendChild(divPassif);

    //btnRemove
    const divRemove = createButtonRemove(section, index);
    //
    divImmobilier.appendChild(label);
    divImmobilier.appendChild(divRow);
    divImmobilier.appendChild(divRow1);
    divImmobilier.appendChild(divRow2);
    divImmobilier.appendChild(divRemove);

    const cardBody = document.getElementById("div_mobilierexonéré");
    const buttonAdd = document.getElementById("div_add_mobilierexonéré");
    cardBody.insertBefore(divImmobilier, buttonAdd);
    $("#ddl1_mobilierexonéré_" + index).html("").select2({
        allowClear: true,
        minimumResultsForSearch: -1,
        data: [
            { id: 'Commun', text: "Commun" },
            { id: 'Mr', text: "Mr" },
            { id: 'Mme', text: "Mme" }
        ]
    });
    $("#ddl3_mobilierexonéré_" + index).html("").select2({
        allowClear: true,
        minimumResultsForSearch: -1,
        data: [
            { id: '0', text: "Sélectionnez un type de bien ..." },
            { id: '1', text: "Transmission d'entreprise (pacte Dutreil)" },
            { id: '2', text: "Parts de groupements forestiers" },
            { id: '3', text: "Parts de GFA" }
        ]
    });
    $("#ddl3").change();
    ShowElementsSection(section, index);
    if (index >= 2) {
        HideElements([$("#btnAddMobilierExonéré")], "div");
    }
}

function AddSommeargent(index) {
    const section = "sommeargent";
    const divImmobilier = document.createElement("div");
    divImmobilier.setAttribute("id", "div_sommeargent_" + index);
    const label = document.createElement("label");
    label.innerHTML = "Somme d'argent - Article " + index;

    const divRow1 = document.createElement("div");
    divRow1.setAttribute("class", "row form-group");
    //Oringine :
    const labelOrigine = createLabelOrigine();
    const divOrigine = createSelectOrigine(section, index);
    //
    const div = document.createElement("div");
    div.setAttribute("class", "col-sm-2 col-12")
    //Valeur PP :
    const labelValeurPP = createLabelValeurPP();
    const divValeurPP = createTextBoxValeurPP(section, index);
    //

    const divRow2 = document.createElement("div");
    divRow2.setAttribute("class", "row form-group");
    //Attribution :
    const labelAttribution = createLabelAttribution();
    const divAttribution = createSelectAttribution(section, index);
    //

    //btnRemove
    const divRemove = createButtonRemove(section, index);
    //

    divRow1.appendChild(labelOrigine);
    divRow1.appendChild(divOrigine);
    divRow1.appendChild(div);
    divRow1.appendChild(labelValeurPP);
    divRow1.appendChild(divValeurPP);
    divRow2.appendChild(labelAttribution);
    divRow2.appendChild(divAttribution);
    divImmobilier.appendChild(label);
    divImmobilier.appendChild(divRow1);
    divImmobilier.appendChild(divRow2);
    divImmobilier.appendChild(divRemove);

    const cardBody = document.getElementById("div_sommeargent");
    const buttonAdd = document.getElementById("div_add_sommeargent");
    cardBody.insertBefore(divImmobilier, buttonAdd);
    $("#ddl1_sommeargent_" + index).html("").select2({
        allowClear: true,
        minimumResultsForSearch: -1,
        data: [
            { id: 'Commun', text: "Commun" },
            { id: 'Mr', text: "Mr" },
            { id: 'Mme', text: "Mme" }
        ]
    });
    $("#ddl3").change();
    ShowElementsSection(section, index);
    if (index >= 10) {
        HideElements([$("#btnAddSommeArgent")], "div");
    }
}

//button : Add bien immobilier
$("#btnAddImmobilier").click(function () {
    determinationDesBiens.immobilierCount += 1;
    if (determinationDesBiens.immobilierCount > 20) {
        determinationDesBiens.immobilierCount = 20;
        return;
    }
    AddImmobilier(determinationDesBiens.immobilierCount);
    determinationDesBiens.arrImmobilier[determinationDesBiens.immobilierCount - 1].origine = $("#ddl1_immobilier_" + determinationDesBiens.immobilierCount).val();
    determinationDesBiens.arrImmobilier[determinationDesBiens.immobilierCount - 1].reserve = $("#cb1_immobilier_" + determinationDesBiens.immobilierCount).is(":checked");
    determinationDesBiens.arrImmobilier[determinationDesBiens.immobilierCount - 1].valeur = $("#txtZone01_immobilier_" + determinationDesBiens.immobilierCount).val();
    determinationDesBiens.arrImmobilier[determinationDesBiens.immobilierCount - 1].attribution = $("#ddl2_immobilier_" + determinationDesBiens.immobilierCount).val();
    determinationDesBiens.arrImmobilier[determinationDesBiens.immobilierCount - 1].passif = $("#txtZone02_immobilier_" + determinationDesBiens.immobilierCount).val();
});

//button : Add bien immobilier exonéré
$("#btnAddImmobilierExonéré").click(function () {
    determinationDesBiens.immobilierexonereCount += 1;
    if (determinationDesBiens.immobilierexonereCount > 2) {
        determinationDesBiens.immobilierexonereCount = 2;
        return;
    }
    AddImmobilierExonere(determinationDesBiens.immobilierexonereCount);
    determinationDesBiens.arrImmobilierexonere[determinationDesBiens.immobilierexonereCount - 1].typedebien = $("#ddl3_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount).val();
    determinationDesBiens.arrImmobilierexonere[determinationDesBiens.immobilierexonereCount - 1].typedebienText = $("#ddl3_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount +" option:selected").text();
    determinationDesBiens.arrImmobilierexonere[determinationDesBiens.immobilierexonereCount - 1].origine = $("#ddl1_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount).val();
    determinationDesBiens.arrImmobilierexonere[determinationDesBiens.immobilierexonereCount - 1].reserve = $("#cb1_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount).is(":checked");
    determinationDesBiens.arrImmobilierexonere[determinationDesBiens.immobilierexonereCount - 1].valeur = $("#txtZone01_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount).val();
    determinationDesBiens.arrImmobilierexonere[determinationDesBiens.immobilierexonereCount - 1].attribution = $("#ddl2_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount).val();
    determinationDesBiens.arrImmobilierexonere[determinationDesBiens.immobilierexonereCount - 1].passif = $("#txtZone02_immobilierexonéré_" + determinationDesBiens.immobilierexonereCount).val();
});

//button : Add bien mobilier
$("#btnAddMobilier").click(function () {
    determinationDesBiens.mobilierCount += 1;
    if (determinationDesBiens.mobilierCount > 20) {
        determinationDesBiens.mobilierCount = 20;
        return;
    }
    AddMobilier(determinationDesBiens.mobilierCount);
    determinationDesBiens.arrMobilier[determinationDesBiens.mobilierCount - 1].origine = $("#ddl1_mobilier_" + determinationDesBiens.mobilierCount).val();
    determinationDesBiens.arrMobilier[determinationDesBiens.mobilierCount - 1].reserve = $("#cb1_mobilier_" + determinationDesBiens.mobilierCount).is(":checked");
    determinationDesBiens.arrMobilier[determinationDesBiens.mobilierCount - 1].valeur = $("#txtZone01_mobilier_" + determinationDesBiens.mobilierCount).val();
    determinationDesBiens.arrMobilier[determinationDesBiens.mobilierCount - 1].attribution = $("#ddl2_mobilier_" + determinationDesBiens.mobilierCount).val();
    determinationDesBiens.arrMobilier[determinationDesBiens.mobilierCount - 1].passif = $("#txtZone02_mobilier_" + determinationDesBiens.mobilierCount).val();
});

//button : Add somme argent
$("#btnAddSommeArgent").click(function () {
    determinationDesBiens.sommeargentCount += 1;
    if (determinationDesBiens.sommeargentCount > 10) {
        determinationDesBiens.sommeargentCount = 10;
        return;
    }
    AddSommeargent(determinationDesBiens.sommeargentCount);
    determinationDesBiens.arrSommeargent[determinationDesBiens.sommeargentCount - 1].origine = $("#ddl1_sommeargent_" + determinationDesBiens.sommeargentCount).val();
    determinationDesBiens.arrSommeargent[determinationDesBiens.sommeargentCount - 1].valeur = $("#txtZone01_sommeargent_" + determinationDesBiens.sommeargentCount).val();
    determinationDesBiens.arrSommeargent[determinationDesBiens.sommeargentCount - 1].attribution = $("#ddl2_sommeargent_" + determinationDesBiens.sommeargentCount).val();
});

//button : Add bien mobilier exonéré
$("#btnAddMobilierExonéré").click(function () {
    determinationDesBiens.mobilierexonereCount += 1;
    if (determinationDesBiens.mobilierexonereCount > 2) {
        determinationDesBiens.mobilierexonereCount = 2;
        return;
    }
    AddMobilierExonere(determinationDesBiens.mobilierexonereCount);
    determinationDesBiens.arrMobilierexonere[determinationDesBiens.mobilierexonereCount - 1].typedebien = $("#ddl3_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount).val();
    determinationDesBiens.arrMobilierexonere[determinationDesBiens.mobilierexonereCount - 1].typedebienText = $("#ddl3_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount + " option:selected").text();
    determinationDesBiens.arrMobilierexonere[determinationDesBiens.mobilierexonereCount - 1].origine = $("#ddl1_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount).val();
    determinationDesBiens.arrMobilierexonere[determinationDesBiens.mobilierexonereCount - 1].reserve = $("#cb1_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount).is(":checked");
    determinationDesBiens.arrMobilierexonere[determinationDesBiens.mobilierexonereCount - 1].valeur = $("#txtZone01_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount).val();
    determinationDesBiens.arrMobilierexonere[determinationDesBiens.mobilierexonereCount - 1].attribution = $("#ddl2_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount).val();
    determinationDesBiens.arrMobilierexonere[determinationDesBiens.mobilierexonereCount - 1].passif = $("#txtZone02_mobilierexonéré_" + determinationDesBiens.mobilierexonereCount).val();
});

function AssignValues(section, index1, index2) {
    if (section === "immobilier") {
        $("#ddl1_immobilier_" + index1).val($("#ddl1_immobilier_" + index2).val()).trigger("change");
        $("#ddl2_immobilier_" + index1).val($("#ddl2_immobilier_" + index2).val()).trigger("change");
        $("#cb1_immobilier_" + index1).prop("checked", $("#cb1_immobilier_" + index2).is(":checked"));
        $("#txtZone01_immobilier_" + index1).val($("#txtZone01_immobilier_" + index2).val());
        $("#txtZone02_immobilier_" + index1).val($("#txtZone02_immobilier_" + index2).val());
    } else if (section === "mobilier") {
        $("#ddl1_mobilier_" + index1).val($("#ddl1_mobilier_" + index2).val()).trigger("change");
        $("#ddl2_mobilier_" + index1).val($("#ddl2_mobilier_" + index2).val()).trigger("change");
        $("#cb1_mobilier_" + index1).prop("checked", $("#cb1_mobilier_" + index2).is(":checked"));
        $("#txtZone01_mobilier_" + index1).val($("#txtZone01_mobilier_" + index2).val());
        $("#txtZone02_mobilier_" + index1).val($("#txtZone02_mobilier_" + index2).val());
    } else if (section === "immobilierexonéré") {
        $("#ddl1_immobilierexonéré_" + index1).val($("#ddl1_immobilierexonéré_" + index2).val()).trigger("change");
        $("#ddl2_immobilierexonéré_" + index1).val($("#ddl2_immobilierexonéré_" + index2).val()).trigger("change");
        $("#ddl3_immobilierexonéré_" + index1).val($("#ddl3_immobilierexonéré_" + index2).val()).trigger("change");
        $("#cb1_immobilierexonéré_" + index1).prop("checked", $("#cb1_immobilierexonéré_" + index2).is(":checked"));
        $("#txtZone01_immobilierexonéré_" + index1).val($("#txtZone01_immobilierexonéré_" + index2).val());
        $("#txtZone02_immobilierexonéré_" + index1).val($("#txtZone02_immobilierexonéré_" + index2).val());
    } else if (section === "sommeargent") {
        $("#ddl1_sommeargent_" + index1).val($("#ddl1_sommeargent_" + index2).val()).trigger("change");
        $("#ddl2_sommeargent_" + index1).val($("#ddl2_sommeargent_" + index2).val()).trigger("change");
        $("#txtZone01_sommeargent_" + index1).val($("#txtZone01_sommeargent_" + index2).val());
    } else if (section === "mobilierexonéré") {
        $("#ddl1_mobilierexonéré_" + index1).val($("#ddl1_mobilierexonéré_" + index2).val()).trigger("change");
        $("#ddl2_mobilierexonéré_" + index1).val($("#ddl2_mobilierexonéré_" + index2).val()).trigger("change");
        $("#ddl4_mobilierexonéré_" + index1).val($("#ddl4_mobilierexonéré_" + index2).val()).trigger("change");
        $("#cb1_mobilierexonéré_" + index1).prop("checked", $("#cb1_mobilierexonéré_" + index2).is(":checked"));
        $("#txtZone01_mobilierexonéré_" + index1).val($("#txtZone01_mobilierexonéré_" + index2).val());
        $("#txtZone02_mobilierexonéré_" + index1).val($("#txtZone02_mobilierexonéré_" + index2).val());
    }
}

function Section__Donataires_de_1er_degré() {
    //DDL: Nombre de donation(s) reçue(s) du Père 
    if (ddl1Value === 1 || ddl1Value === 3) {
        for (var i = 1; i <= ddl3Value; i++) {
            ShowElements([$("#ddl4_donataire_" + i)], "div.row");
        }
    } else {
        for (var j = 1; j <= ddl3Value; j++) {
            HideElements([$("#ddl4_donataire_" + j)], "div.row");
        }
    }
    //DDL: Nombre de donation(s) reçue(s) de la Mère
    if (ddl1Value === 2 || ddl1Value === 3) {
        for (var k = 1; k <= ddl3Value; k++) {
            ShowElements([$("#ddl5_donataire_" + k)], "div.row");
        }
    } else {
        for (var l = 1; l <= ddl3Value; l++) {
            HideElements([$("#ddl5_donataire_" + l)], "div.row");
        }
    }
}

function Display__Donations_antérieures_consenties_par_le_donateur(btnDonataireIndex) {
    var ddl4DonataireValue = $("#ddl4_donataire_" + btnDonataireIndex).val();
    var ddl5DonataireValue = $("#ddl5_donataire_" + btnDonataireIndex).val();
    HideElements([$(".btnRappelPere"), $(".btnRappelMere"), $(".btnRappelPereResult"), $(".btnRappelMereResult")], "div.row");
    if (ddl1Value === 1) {
        for (var i = 1; i <= ddl4DonataireValue; i++) {
            ShowElements([$("#btnRappelPère_" + i), $("#btnRappelPereResult_" + i)], "div.row");
        }
    } else if (ddl1Value === 2) {
        for (var j = 1; j <= ddl5DonataireValue; j++) {
            ShowElements([$("#btnRappelMère_" + j), $("#btnRappelMereResult_" + j)], "div.row");
        }
    } else if (ddl1Value === 3) {
        for (var k = 1; k <= ddl4DonataireValue; k++) {
            ShowElements([$("#btnRappelPère_" + k), $("#btnRappelPereResult_" + k)], "div.row");
        }
        for (var l = 1; l <= ddl5DonataireValue; l++) {
            ShowElements([$("#btnRappelMère_" + l), $("#btnRappelMereResult_" + l)], "div.row");
        }
    }
    if (ddl4DonataireValue > 0) {
        $("#btnRappelPère_1").click();
        $("#btnRappelPereResult_1").click();
    } else if (ddl5DonataireValue > 0) {
        $("#btnRappelMère_1").click();
        $("#btnRappelMereResult_1").click();
    }
}

function Status_btnInputForm() {
    $("#btnInputForm3, #btnSynthaseDonation").attr("disabled", "disabled");
    if (ddl1Value === 1) {
        for (var i = 1; i <= ddl3Value; i++) {
            if ($("#ddl4_donataire_" + i).val() !== "0") {
                $("#btnInputForm3, #btnSynthaseDonation").removeAttr("disabled");
            }
        }
    } else if (ddl1Value === 2) {
        for (var i = 1; i <= ddl3Value; i++) {
            if ($("#ddl5_donataire_" + i).val() !== "0") {
                $("#btnInputForm3, #btnSynthaseDonation").removeAttr("disabled");
            }

        }
    } else if (ddl1Value === 3) {
        for (var i = 1; i <= ddl3Value; i++) {
            if ($("#ddl4_donataire_" + i).val() !== "0" || $("#ddl5_donataire_" + i).val() !== "0") {
                $("#btnInputForm3, #btnSynthaseDonation").removeAttr("disabled");
            }
        }
    }
}

function Status_btnDonataire() {
    var blnSelected = false;
    for (var i = 1; i <= ddl3Value; i++) {
        $("#btnDonataire_" + i).show();
        $("#btnDonataireResult_" + i).show();
        if ($("#ddl4_donataire_" + i).val() === "0" && $("#ddl5_donataire_" + i).val() === "0") {
            $("#btnDonataire_" + i).attr("disabled", "disabled");
            $("#btnDonataire_" + i).removeClass("active");
            $("#btnDonataireResult_" + i).attr("disabled", "disabled");
            $("#btnDonataireResult_" + i).removeClass("active");
        } else {
            $("#btnDonataire_" + i).removeAttr("disabled");
            $("#btnDonataireResult_" + i).removeAttr("disabled");
            if (!blnSelected) {
                $("#btnDonataire_" + i).click();
                $("#btnDonataireResult_" + i).click();
                blnSelected = true;
            }
        }
    }
    for (var j = 10; j > ddl3Value; j--) {
        $("#btnDonataire_" + j).hide();
        $("#btnDonataireResult_" + j).hide();
    }
    if (!blnSelected) {
        $(".btnRappelPere, .btnRappelMere").hide();
        $(".btnRappelPereResult, .btnRappelMereResult").hide();
    }
}

function Sum_ddl4_donataire() {
    var total = 0;
    for (var i = 1; i <= ddl3Value; i++) {
        total += parseInt($("#ddl4_donataire_" + i).val());
    }
    return total;
}

function Sum_ddl5_donataire() {
    var total = 0;
    for (var i = 1; i <= ddl3Value; i++) {
        total += parseInt($("#ddl5_donataire_" + i).val());
    }
    return total;
}

function Nature_de_la_donation() {
    $("#sub_nature_de_la_donation").hide();
    if (ddl3Value > 1) {
        $("#sub_nature_de_la_donation").show();
    }
    $("#divInégalitaires").hide();
    var ddl7Value = $("#ddl7").val();
    if (ddl7Value === "Inégalitaires") {
        $("#divInégalitaires").show();
        for (var i = 1; i <= ddl3Value; i++) {
            ShowElements([$("#txtZone02_donataire_" + i), $("#txtZone03_donataire_" + i), $("#TextBox" + i)], "div.row");
            var value = 100 / ddl3Value;
            $("#txtZone02_donataire_" + i).val(value);
            $("#txtZone03_donataire_" + i).val(value);
            $("#TextBox" + i).val(value);
            determinationDesDonataires.arrNatureDeLaDonation[i - 1].txtZone01 = determinationDesDonataires.arrNatureDeLaDonation[i - 1].txtZone02 = determinationDesDonataires.arrNatureDeLaDonation[i - 1].txtZone03 = value;
        }
        for (var j = 10; j > ddl3Value; j--) {
            HideElements([$("#txtZone02_donataire_" + j), $("#txtZone03_donataire_" + j), $("#TextBox" + j)], "div.row");
        }
    } else {
        for (var k = 1; k <= 10; k++) {
            HideElements([$("#txtZone02_donataire_" + k), $("#txtZone03_donataire_" + k), $("#TextBox" + k)], "div.row");
        }
    }
    $("#txtZone02_donataire_total, #txtZone03_donataire_total").val("100");
    determinationDesDonataires.txtZone02Total = determinationDesDonataires.txtZone03Total = 100;
}

$("#ddl1").change(function () {
    ddl1Value = parseInt($(this).val());
    determinationDesDonataires.ddl1 = ddl1Value;
    determinationDesDonataires.ddl1Text = $("#ddl1 option:selected").text();
    Nature_de_la_donation();
    if (ddl1Value === 1) {
        ShowElements([$("#txtZone01a")], "div.row");
        $("#lblZone02_donataire_title").show();
        ShowElements([$(".txtZone02, #txtZone02_donataire_total")], "div.col-12");
        $(".txtZone02").change();
        $(".ddl1").val("Mr").change();
        $(".ddl1").prop('disabled', true);
        HideElements([$("#txtZone01b")], "div.row");
        $("#txtZone01b").val("02/01/1953");
        HideElements([$(".txtZone03, #txtZone03_donataire_total")], "div.col-12");
        $("#lblZone03_donataire_title").hide();
    } else if (ddl1Value === 2) {
        ShowElements([$("#txtZone01b")], "div.row");
        $("#lblZone03_donataire_title").show();
        ShowElements([$(".txtZone03, #txtZone03_donataire_total")], "div.col-12");
        $(".txtZone03").change();
        $(".ddl1").val("Mme").change();
        $(".ddl1").prop('disabled', true);
        HideElements([$("#txtZone01a")], "div.row");
        $("#txtZone01a").val("02/01/1953");
        HideElements([$(".txtZone02, #txtZone02_donataire_total")], "div.col-12");
        $("#lblZone02_donataire_title").hide();
    } else if (ddl1Value === 3) {
        ShowElements([$("#txtZone01a, #txtZone01b")], "div.row");
        $("#lblZone02_donataire_title, #lblZone03_donataire_title").show();
        ShowElements([$(".txtZone02, #txtZone02_donataire_total, .txtZone03, #txtZone03_donataire_total")], "div.col-12");
        $(".ddl1").removeAttr('disabled');
    }
    Section__Donataires_de_1er_degré();
    Status_btnInputForm();
    Status_btnDonataire();
    Display__Donations_antérieures_consenties_par_le_donateur(1);
}).change();

$("#ddl3").change(function () {
    ddl3Value = parseInt($(this).val());
    determinationDesDonataires.ddl3 = ddl3Value;
    for (var i = 1; i <= ddl3Value; i++) {
        ShowElements([$("#div_donataire_" + i)], "");
        ShowElements([$("#txtZone01_donataire_" + i), $("#cb1_donataire_" + i), $("#cb2_donataire_" + i), $("#ddl4_donataire_" + i), $("#ddl5_donataire_" + i)], "div.row");
    }
    for (var j = 10; j > ddl3Value; j--) {
        HideElements([$("#div_donataire_" + j)], "");
        HideElements([$("#txtZone01_donataire_" + j), $("#cb1_donataire_" + j), $("#cb2_donataire_" + j), $("#ddl4_donataire_" + j), $("#ddl5_donataire_" + j)], "div.row");
    }
    Nature_de_la_donation();
    $("#ddl6").change();
    Section__Donataires_de_1er_degré();
    Status_btnInputForm();
    Status_btnDonataire();
}).change();

$("#ddl7").change(function () {
    var self = $(this);
    determinationDesDonataires.ddl7 = self.val();
    Nature_de_la_donation();
}).change();

$("#ddl6").change(function () {
    var self = $(this);
    determinationDesDonataires.ddl6 = self.val();
    if (ddl3Value > 1 && self.val() === "Donation partage") {
        ShowElements([$(".ddl2")], "div");
        $(".lblAttribution").text("Attribution :");

        var arrDdl3Data = [{ id: 'Indivision selon quotité choisie', text: "Indivision selon quotité choisie" }, { id: '1er Donataire', text: "1er Donataire" }];
        for (var i = 2; i <= ddl3Value; i++) {
            var obj = { id: i + 'ème Donataire', text: i + "ème Donataire" }
            arrDdl3Data.push(obj);
        }

        for (var i = 1; i <= determinationDesBiens.immobilierCount; i++) {
            var value = $("#ddl2_immobilier_" + i).val();
            $("#ddl2_immobilier_" + i).html("").select2({
                allowClear: true,
                minimumResultsForSearch: -1,
                data: arrDdl3Data
            });
            if (value !== null)
                $("#ddl2_immobilier_" + i).val(value).trigger("change");
        }
        for (var i = 1; i <= determinationDesBiens.immobilierexonereCount; i++) {
            var value = $("#ddl2_immobilierexonéré_" + i).val();
            $("#ddl2_immobilierexonéré_" + i).html("").select2({
                allowClear: true,
                minimumResultsForSearch: -1,
                data: arrDdl3Data
            });
            if (value !== null)
                $("#ddl2_immobilierexonéré_" + i).val(value).trigger("change");
        }
        for (var i = 1; i <= determinationDesBiens.mobilierCount; i++) {
            var value = $("#ddl2_mobilier_" + i).val();
            $("#ddl2_mobilier_" + i).html("").select2({
                allowClear: true,
                minimumResultsForSearch: -1,
                data: arrDdl3Data
            });
            if (value !== null)
                $("#ddl2_mobilier_" + i).val(value).trigger("change");
        }
        for (var i = 1; i <= determinationDesBiens.mobilierexonereCount; i++) {
            var value = $("#ddl2_mobilierexonéré_" + i).val();
            $("#ddl2_mobilierexonéré_" + i).html("").select2({
                allowClear: true,
                minimumResultsForSearch: -1,
                data: arrDdl3Data
            });
            if (value !== null)
                $("#ddl2_mobilierexonéré_" + i).val(value).trigger("change");
        }
        for (var i = 1; i <= determinationDesBiens.sommeargentCount; i++) {
            var value = $("#ddl2_sommeargent_" + i).val();
            $("#ddl2_sommeargent_" + i).html("").select2({
                allowClear: true,
                minimumResultsForSearch: -1,
                data: arrDdl3Data
            });
            if (value !== null)
                $("#ddl2_sommeargent_" + i).val(value).trigger("change");
        }
    } else {
        HideElements([$(".ddl2")], "div");
        $(".lblAttribution").text("");
    }
    $("#chk01").change();
}).change();

function ShowHide_input_form(element) {
    $(".btnInputForm, #btnSynthese, #btnSyntheseTaxePere, #btnSyntheseTaxeMere, #btnTaxeDonation, #btnSynthaseDonation").removeClass("active");
    element.addClass("active");
    $("#div_input_form_1, #div_input_form_2, #div_input_form_3, #div_synthese, #div_synthese_taxe_pere, #div_synthese_taxe_mere, #div_taxe_donation, #div_synthase_donation").hide();
    var id = element.attr("id");
    if (id === "btnInputForm1") {
        $("#div_input_form_1").show();
    } else if (id === "btnInputForm2") {
        $("#div_input_form_2").show();
    } else if (id === "btnInputForm3") {
        $("#div_input_form_3").show();
    } else if (id === "btnSynthese") {
        $("#div_synthese").show();
    } else if (id === "btnSyntheseTaxePere") {
        $("#div_synthese_taxe_pere").show();
    } else if (id === "btnSyntheseTaxeMere") {
        $("#div_synthese_taxe_mere").show();
    } else if (id === "btnTaxeDonation") {
        $("#div_taxe_donation").show();
    } else if (id === "btnSynthaseDonation") {
        $("#div_synthase_donation").show();
    }
}

$(".btnInputForm").click(function () {
    ShowHide_input_form($(this));
});

$(".txtZone02").on("keypress change input blur", function () {
    var self = $(this);
    var id = self.attr("id");
    var index = id.split("_")[2];
    determinationDesDonataires.arrNatureDeLaDonation[index - 1].txtZone02 = self.val();
    if (parseFloat(self.val()) > 100) {
        $("#txtZone02_donataire_" + index).val("100");
        determinationDesDonataires.arrNatureDeLaDonation[index - 1].txtZone02 = 100;
    }
    $("#lblMsgTextZone02").hide();
    determinationDesDonataires.msgTxtZone02AndTxtZone03 = "";
    var total = 0;
    for (var i = 1; i <= ddl3Value; i++) {
        var eachValue = $("#txtZone02_donataire_" + i).val() === "" ? 0 : parseFloat($("#txtZone02_donataire_" + i).val());
        determinationDesDonataires.arrNatureDeLaDonation[index - 1].txtZone02 = eachValue;
        total += eachValue;
    }
    total = 100 - total;
    $("#txtZone02_donataire_total").val(total);
    determinationDesDonataires.txtZone02Total = total;
    if (total === 0) {
        $("#txtZone02_donataire_total").val(100);
        determinationDesDonataires.txtZone02Total = 100;
    }
    if (total !== 100 && total !== 0) {
        $("#lblMsgTextZone02").show();
        determinationDesDonataires.msgTxtZone02AndTxtZone03 = $("#lblMsgTextZone02").text();
    }
});

$(".txtZone03").on("keypress change input blur", function () {
    var self = $(this);
    var id = self.attr("id");
    var index = id.split("_")[2];
    determinationDesDonataires.arrNatureDeLaDonation[index - 1].txtZone03 = self.val();
    if (parseFloat(self.val()) > 100) {
        $("#txtZone03_donataire_" + index).val("100");
        determinationDesDonataires.arrNatureDeLaDonation[index - 1].txtZone03 = 100;
    }
    $("#lblMsgTextZone03").hide();
    determinationDesDonataires.msgTxtZone02AndTxtZone03 = "";
    var total = 0;
    for (var i = 1; i <= ddl3Value; i++) {
        var eachValue = $("#txtZone03_donataire_" + i).val() === "" ? 0 : parseFloat($("#txtZone03_donataire_" + i).val());
        determinationDesDonataires.arrNatureDeLaDonation[index - 1].txtZone03 = eachValue;
        total += eachValue;
    }
    total = 100 - total;
    $("#txtZone03_donataire_total").val(total);
    determinationDesDonataires.txtZone03Total = total;
    if (total === 0) {
        $("#txtZone03_donataire_total").val(100);
        determinationDesDonataires.txtZone03Total = 100;
    }
    if (total !== 100 && total !== 0) {
        $("#lblMsgTextZone03").show();
        determinationDesDonataires.msgTxtZone02AndTxtZone03 = $("#lblMsgTextZone03").text();
    }
});

$(".btnDonataire").click(function () {
    $(".btnDonataire").removeClass("active");
    $(this).addClass("active");
    var index = $(this).attr("data-value");
    Display__Donations_antérieures_consenties_par_le_donateur(index);
});

$(".btnRappelPere, .btnRappelMere").click(function () {
    $(".btnRappelPere, .btnRappelMere").removeClass("active");
    $(this).addClass("active");
    var indexDonataireSelected = $(".btnDonataire.active").attr("data-value") - 1;
    var indexRappelSelected = $(this).attr("data-value");
    $("#lblRappelTitle").text(indexRappelSelected + "ERE DONATION");
    indexRappelSelected -= 1;
    if ($(this).attr("id").split("_")[0] === "btnRappelPère") {
        var arrPere = arrRappelDeDonationsAnterieures[indexDonataireSelected].Pere;
        $("#txtZone01").val(arrPere[indexRappelSelected].txtZone01);
        $("#txtZone02").val(arrPere[indexRappelSelected].txtZone02);
        $("#txtZone03").val(arrPere[indexRappelSelected].txtZone03);
        $("#txtZone04").val(arrPere[indexRappelSelected].txtZone04);
        $("#txtZone05").val(arrPere[indexRappelSelected].txtZone05);
        $("#txtZone06").val(arrPere[indexRappelSelected].txtZone06);
        $("#txtZone07").val(arrPere[indexRappelSelected].txtZone07);
        $("#txtZone00_").val(arrPere[indexRappelSelected].txtZone00_);
        $("#chk01").prop("checked", arrPere[indexRappelSelected].chk01).change();
        $("#txtZone00_").blur();
    } else {
        var arrMere = arrRappelDeDonationsAnterieures[indexDonataireSelected].Mere;
        $("#txtZone01").val(arrMere[indexRappelSelected].txtZone01);
        $("#txtZone02").val(arrMere[indexRappelSelected].txtZone02);
        $("#txtZone03").val(arrMere[indexRappelSelected].txtZone03);
        $("#txtZone04").val(arrMere[indexRappelSelected].txtZone04);
        $("#txtZone05").val(arrMere[indexRappelSelected].txtZone05);
        $("#txtZone06").val(arrMere[indexRappelSelected].txtZone06);
        $("#txtZone07").val(arrMere[indexRappelSelected].txtZone07);
        $("#txtZone00_").val(arrMere[indexRappelSelected].txtZone00_);
        $("#chk01").prop("checked", arrMere[indexRappelSelected].chk01).change();
        $("#txtZone00_").blur();
    }
});

$(".btnDonataireResult").click(function () {
    $(".btnDonataireResult").removeClass("active");
    $(this).addClass("active");
    var index = $(this).attr("data-value");
    Display__Donations_antérieures_consenties_par_le_donateur(index);
    $("#headerPereMere").html($(this).text());
});

$(".btnRappelPereResult, .btnRappelMereResult").click(function () {
    $(".btnRappelPereResult, .btnRappelMereResult").removeClass("active");
    $(this).addClass("active");
    var id = $(this).attr("id").split("_")[0];
    if (id === "btnRappelPereResult") {
        $("#subHeaderPereMere").html("DU CHEF DU PERE");
    } else {
        $("#subHeaderPereMere").html("DU CHEF DE LA MERE");
    }
});

$(".ddl4_donataire").change(function () {
    var self = $(this);
    var id = self.attr("id");
    var index = id.split("_")[2];
    determinationDesDonataires.arrDonatairesDeDegre[index - 1].ddl4 = self.val();
    if ($("#btnDonataire_" + index).hasClass("active")) {
        $("#btnDonataire_" + index).click();
    }
    Status_btnInputForm();
    Status_btnDonataire();
}).change();

$(".ddl5_donataire").change(function () {
    var self = $(this);
    var id = self.attr("id");
    var index = id.split("_")[2];
    determinationDesDonataires.arrDonatairesDeDegre[index - 1].ddl5 = self.val();
    if ($("#btnDonataire_" + index).hasClass("active")) {
        $("#btnDonataire_" + index).click();
    }
    Status_btnInputForm();
    Status_btnDonataire();
}).change();

$("#txtZone00, #txtZone01a, #txtZone01b").on("keypress change input blur", function () {
    var zone00 = $("#txtZone00").val().split("/");
    var zone01A = $("#txtZone01a").val().split("/");
    var zone01B = $("#txtZone01b").val().split("/");
    var result = parseInt(zone00[2]) - parseInt(zone01A[2]);
    $("#lblYear1").text(result + " ans");
    result = parseInt(zone00[2]) - parseInt(zone01B[2]);
    $("#lblYear2").text(result + " ans");
    determinationDesDonataires.txtZone00 = $("#txtZone00").val();
    determinationDesDonataires.txtZone01a = $("#txtZone01a").val();
    determinationDesDonataires.txtZone01b = $("#txtZone01b").val();
}).blur();

$(".txtZone01_donataire").on("keypress change input blur", function () {
    var self = $(this);
    var id = self.attr("id");
    var index = id.split("_")[2];
    $("#ddl2_immobilier_" + index).val(self.val()).trigger("change");
    determinationDesDonataires.arrDonatairesDeDegre[index - 1].txtZone01 = self.val();
});

$(".cb2_donataire").change(function () {
    var self = $(this).children();
    var id = self.attr("id");
    var index = id.split("_")[2];
    determinationDesDonataires.arrDonatairesDeDegre[index - 1].cb2 = self.is(":checked");
}).change();

$(".cb1_donataire").change(function () {
    var self = $(this).children();
    var id = self.attr("id");
    var index = id.split("_")[2];
    determinationDesDonataires.arrDonatairesDeDegre[index - 1].cb1 = self.is(":checked");
}).change();

function displaySubCard_Exonérations_liées_aux_biens(date) {
    var splitDay1 = $("#txtZone00").val().split("/");
    var day1 = new Date(splitDay1[2], parseInt(splitDay1[1]) - 1, splitDay1[0]);
    var splitDay2 = date.split("/");
    var day2 = new Date(splitDay2[2], parseInt(splitDay2[1]) - 1, splitDay2[0]);
    var days = (day2 - day1) / (1000 * 3600 * 24);
    if (days < 15 || (days >= 15 && parseInt($("#ddl4").val()) > 1 && $("#ddl6").val() === "Donation partage" && !$("#chk01").is(":checked"))) {
        ShowElements([$("#txtZone03"), $("#txtZone04")], "div.sub-card");
    } else {
        HideElements([$("#txtZone03"), $("#txtZone04")], "div.sub-card");
    }
}

$("#txtZone01, #txtZone02, #txtZone03, #txtZone04, #txtZone05, #txtZone06, #txtZone07, #txtZone00_").on("keypress change input blur", function () {
    var thisVal = $(this).val();
    var thisId = $(this).attr("id");
    var rappelSelected = $(".btnRappelPere.active, .btnRappelMere.active");
    var indexRappelSelected = rappelSelected.attr("data-value") - 1;
    var donataireSelected = arrRappelDeDonationsAnterieures[$(".btnDonataire.active").attr("data-value") - 1];
    if (rappelSelected[0] === undefined)
        return;
    if (rappelSelected[0].classList[2] === "btnRappelPere") {
        var pere = donataireSelected.Pere[indexRappelSelected]
        switch (thisId) {
            case "txtZone01":
                pere.txtZone01 = thisVal;
                break;
            case "txtZone02":
                pere.txtZone02 = thisVal;
                break;
            case "txtZone03":
                pere.txtZone03 = thisVal;
                break;
            case "txtZone04":
                pere.txtZone04 = thisVal;
                break;
            case "txtZone05":
                pere.txtZone05 = thisVal;
                break;
            case "txtZone06":
                pere.txtZone06 = thisVal;
                break;
            case "txtZone07":
                pere.txtZone07 = thisVal;
                break;
            case "txtZone00_":
                pere.txtZone00_ = thisVal;
                displaySubCard_Exonérations_liées_aux_biens(thisVal);
                break;
            default:
        }
    } else {
        var mere = donataireSelected.Mere[indexRappelSelected]
        switch (thisId) {
            case "txtZone01":
                mere.txtZone01 = thisVal;
                break;
            case "txtZone02":
                mere.txtZone02 = thisVal;
                break;
            case "txtZone03":
                mere.txtZone03 = thisVal;
                break;
            case "txtZone04":
                mere.txtZone04 = thisVal;
                break;
            case "txtZone05":
                mere.txtZone05 = thisVal;
                break;
            case "txtZone06":
                mere.txtZone06 = thisVal;
                break;
            case "txtZone07":
                mere.txtZone07 = thisVal;
                break;
            case "txtZone00_":
                mere.txtZone00_ = thisVal;
                displaySubCard_Exonérations_liées_aux_biens(thisVal);
                break;
            default:
        }
    }
}).blur();

$("#chk01").on("change", function () {
    var thisVal = $(this).is(':checked');
    if ($("#ddl6").val() === "Donation partage") {
        ShowElements([$("#chk01_message_1"), $("#chk01_message_2"), $("#txtZone05"), $("#txtZone06"), $("#txtZone07")], "div.sub-card");
        $("#chk01_message_1, #chk01_message_2").hide();
        if (thisVal) {
            $("#chk01_message_1").show();
            ShowElements([$("#txtZone01")], "div.row");
        } else {
            $("#chk01_message_2").show();
            HideElements([$("#txtZone01")], "div.row");
        }
    } else {
        HideElements([$("#chk01_message_1"), $("#chk01_message_2"), $("#txtZone05"), $("#txtZone06"), $("#txtZone07")], "div.sub-card");
        HideElements([$("#txtZone01")], "div.row");
    }
    var rappelSelected = $(".btnRappelPere.active, .btnRappelMere.active");
    var indexRappelSelected = rappelSelected.attr("data-value") - 1;
    var donataireSelected = arrRappelDeDonationsAnterieures[$(".btnDonataire.active").attr("data-value") - 1];
    if (rappelSelected[0] === undefined)
        return;
    if (rappelSelected[0].classList[2] === "btnRappelPere") {
        var pere = donataireSelected.Pere[indexRappelSelected]
        pere.chk01 = thisVal;
    } else {
        var mere = donataireSelected.Mere[indexRappelSelected]
        mere.chk01 = thisVal;
    }
    $("#txtZone00_").blur();
}).change();

$("#txtZone00").on("keypress change input blur", function () {
    $("#txtZone00_").blur();
}).blur();

function createCard() {
    const card = document.createElement("div");
    card.setAttribute("class", "card");
    return card;
}
function createCardHeader(title) {
    const cardHeader = document.createElement("div");
    cardHeader.setAttribute("class", "card-header");
    cardHeader.innerHTML = title;
    return cardHeader;
}
function createCardBody(cssClass = "") {
    const cardBody = document.createElement("div");
    cardBody.setAttribute("class", "card-body" + " " + cssClass);
    return cardBody
}
function createSubCard() {
    const subCard = document.createElement("div");
    subCard.setAttribute("class", "sub-card");
    return subCard;
}
function createDiv(cssClass = "") {
    const div = document.createElement("div");
    div.setAttribute("class", cssClass);
    return div;
}
function createLabel(cssClass = "", text) {
    const label = document.createElement("label");
    label.setAttribute("class", cssClass);
    label.innerHTML = text;
    return label;
}

function setValueSynthese(result1, result2) {
    //card : MASSE DONNEE EN NUE PROPRIETE
    var row;
    $("#row50").hide();
    var row10 = document.getElementById("row10");
    $("#row10").empty();
    if (result1[0][5] !== "0") {
        $("#row50").show();
        row = createDiv("row text-center");
        row.appendChild(createLabel("col", "Cette masse a été calculée en fonction de l'age du Donateur sur les bases de l'article 669 du CGI."));
        row10.appendChild(row);
        row = createDiv("row");
        row.appendChild(createLabel("col", "Age"));
        row.appendChild(createLabel("col", "Taux"));
        row10.appendChild(row);
        if (result1[1][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Donateur :"));
            row.appendChild(createLabel("col", result1[1][1]));
            row.appendChild(createLabel("col", result1[1][2]));
            row10.appendChild(row);
        }
        if (result1[2][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Donatrice :"));
            row.appendChild(createLabel("col", result1[2][1]));
            row.appendChild(createLabel("col", result1[2][2]));
            row10.appendChild(row);
        }
        //sub-card : Situation active nette
        var row13 = document.getElementById("row13");
        $("#row13").empty();
        row = createDiv("row");
        row.appendChild(createLabel("col", ""));
        row.appendChild(createLabel("col", "Donateur"));
        row.appendChild(createLabel("col", "Donatrice"));
        row13.appendChild(row);
        if (result1[3][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens immobiliers taxables :"));
            row.appendChild(createLabel("col", result1[3][1]));
            row.appendChild(createLabel("col", result1[3][2]));
            row13.appendChild(row);
        }
        if (result1[4][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens immobiliers exonérés :"));
            row.appendChild(createLabel("col", result1[4][1]));
            row.appendChild(createLabel("col", result1[4][2]));
            row13.appendChild(row);
        }
        if (result1[5][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens mobiliers taxables :"));
            row.appendChild(createLabel("col", result1[5][1]));
            row.appendChild(createLabel("col", result1[5][2]));
            row13.appendChild(row);
        }
        if (result1[6][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens mobiliers exonérés :"));
            row.appendChild(createLabel("col", result1[6][1]));
            row.appendChild(createLabel("col", result1[6][2]));
            row13.appendChild(row);
        }
        if (result1[7][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Rapport dons en avance sur part :"));
            row.appendChild(createLabel("col", result1[7][1]));
            row.appendChild(createLabel("col", result1[7][2]));
            row13.appendChild(row);
        }
        row = createDiv("row");
        row.appendChild(createLabel("col", "Total de l'actif net :"));
        row.appendChild(createLabel("col", result1[8][1]));
        row.appendChild(createLabel("col", result1[8][2]));
        row13.appendChild(row);
    }
    //card : MASSE DONNEE EN PLEINE PROPRIETE
    //sub-card : Situation active nette
    $("#row200").hide();
    if (result1[9][5] !== "0") {
        $("#row200").show();
        var row20to26 = document.getElementById("row20to26");
        $("#row20to26").empty();
        row = createDiv("row");
        row.appendChild(createLabel("col", ""));
        row.appendChild(createLabel("col", "Donateur"));
        row.appendChild(createLabel("col", "Donatrice"));
        row20to26.appendChild(row);
        if (result1[10][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens immobiliers taxables :"));
            row.appendChild(createLabel("col", result1[10][1]));
            row.appendChild(createLabel("col", result1[10][2]));
            row20to26.appendChild(row);
        }
        if (result1[11][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens immobiliers exonérés :"));
            row.appendChild(createLabel("col", result1[11][1]));
            row.appendChild(createLabel("col", result1[11][2]));
            row20to26.appendChild(row);
        }
        if (result1[12][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens mobiliers taxables :"));
            row.appendChild(createLabel("col", result1[12][1]));
            row.appendChild(createLabel("col", result1[12][2]));
            row20to26.appendChild(row);
        }
        if (result1[13][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens mobiliers exonérés :"));
            row.appendChild(createLabel("col", result1[13][1]));
            row.appendChild(createLabel("col", result1[13][2]));
            row20to26.appendChild(row);
        }
        if (result1[14][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Somme d'argent :"));
            row.appendChild(createLabel("col", result1[14][1]));
            row.appendChild(createLabel("col", result1[14][2]));
            row20to26.appendChild(row);
        }
        if (result1[15][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "Rapport dons en avance sur part :"));
            row.appendChild(createLabel("col", result1[15][1]));
            row.appendChild(createLabel("col", result1[15][2]));
            row20to26.appendChild(row);
        }
        row = createDiv("row");
        row.appendChild(createLabel("col", "Total de l'actif net :"));
        row.appendChild(createLabel("col", result1[16][1]));
        row.appendChild(createLabel("col", result1[16][2]));
        row20to26.appendChild(row);
    }
    //card : INCORPORATION DES DONS ANTERIEURS
    $("#row27").hide();
    if (result1[17][5] !== "0") {
        $("#row27").show();
        //sub-card : Du chef du Donateur
        $("#row28").hide();
        if (result1[18][5] !== "0") {
            $("#row28").show();
            var row29to31 = document.getElementById("row29to31");
            $("#row29to31").empty();
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens"));
            row.appendChild(createLabel("col", "Valeur"));
            row.appendChild(createLabel("col", "%"));
            row.appendChild(createLabel("col", "Incorporation"));
            row29to31.appendChild(row);
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens en Nue Propriété :"));
            row.appendChild(createLabel("col", result1[19][1]));
            row.appendChild(createLabel("col", result1[19][2]));
            row.appendChild(createLabel("col", result1[19][3]));
            row29to31.appendChild(row);
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens en Pleine propriété :"));
            row.appendChild(createLabel("col", result1[20][1]));
            row.appendChild(createLabel("col", result1[20][2]));
            row.appendChild(createLabel("col", result1[20][3]));
            row29to31.appendChild(row);
            row = createDiv("row");
            row.appendChild(createLabel("col", "Total :"));
            row.appendChild(createLabel("col", result1[21][1]));
            row.appendChild(createLabel("col", result1[21][2]));
            row.appendChild(createLabel("col", result1[21][3]));
            row29to31.appendChild(row);
        }
        //sub-card : Du chef de la Donatrice
        $("#row32").hide();
        if (result1[22][5] !== "0") {
            $("#row32").show();
            var row33to35 = document.getElementById("row33to35");
            $("#row33to35").empty();
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens"));
            row.appendChild(createLabel("col", "Valeur"));
            row.appendChild(createLabel("col", "%"));
            row.appendChild(createLabel("col", "Incorporation"));
            row33to35.appendChild(row);
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens en Nue Propriété :"));
            row.appendChild(createLabel("col", result1[23][1]));
            row.appendChild(createLabel("col", result1[23][2]));
            row.appendChild(createLabel("col", result1[23][3]));
            row33to35.appendChild(row);
            row = createDiv("row");
            row.appendChild(createLabel("col", "Biens en Pleine propriété :"));
            row.appendChild(createLabel("col", result1[24][1]));
            row.appendChild(createLabel("col", result1[24][2]));
            row.appendChild(createLabel("col", result1[24][3]));
            row33to35.appendChild(row);
            row = createDiv("row");
            row.appendChild(createLabel("col", "Total :"));
            row.appendChild(createLabel("col", result1[25][1]));
            row.appendChild(createLabel("col", result1[25][2]));
            row.appendChild(createLabel("col", result1[25][3]));
            row33to35.appendChild(row);
        }
    }
    //card : DROITS DANS LA MASSE GLOBALE
    //sub-card : Concernant le donateur
    $("#row38").hide();
    if (result1[28][5] !== "0") {
        $("#row38").show();
        var row39to48 = document.getElementById("row39to48");
        $("#row39to48").empty();
        for (var i = 1; i <= ddl3Value; i++) {
            var index = 28 + i;
            row = createDiv("row");
            row.appendChild(createLabel("col", "Donataire " + i + " :"));
            row.appendChild(createLabel("col", result1[index][1]));
            row.appendChild(createLabel("col", result1[index][2]));
            row39to48.appendChild(row);
        }
    }
    //sub-card : Concernant le donatrice
    $("#row49").hide();
    if (result1[39][5] !== "0") {
        $("#row49").show();
        var row50to59 = document.getElementById("row50to59");
        $("#row50to59").empty();
        for (var i = 1; i <= ddl3Value; i++) {
            var index = 39 + i;
            row = createDiv("row");
            row.appendChild(createLabel("col", "Donataire " + i + " :"));
            row.appendChild(createLabel("col", result1[index][1]));
            row.appendChild(createLabel("col", result1[index][2]));
            row50to59.appendChild(row);
        }
    }
    //sub-card : Récapitulatif général
    $("#row60").hide();
    if (result1[50][5] !== "0") {
        $("#row60").show();
        var row61to70 = document.getElementById("row61to70");
        $("#row61to70").empty();
        for (var i = 1; i <= ddl3Value; i++) {
            var index = 50 + i;
            row = createDiv("row");
            row.appendChild(createLabel("col", "Donataire " + i + " :"));
            row.appendChild(createLabel("col", result1[index][1]));
            row.appendChild(createLabel("col", result1[index][2]));
            row61to70.appendChild(row);
        }
    }
    var divDonataireAllotissement = document.getElementById("divDonataireAllotissement");
    $("#divDonataireAllotissement").empty();
    for (var i = 1; i <= ddl3Value; i++) {
        //card : Donataire - Allotissement
        var title = i == 1 ? "1er Donataire - Allotissement" : i + "ème Donataire - Allotissement";
        var card = createCard();
        var cardHeader = createCardHeader(title);
        var cardBody = createCardBody();
        var subCard, subCardHeader, subCardBody;
        var divRow, label;
        //sub-card : Allotissement en provenance du Donateur
        if (result1[(110 * (i - 1)) + 63][5] !== "0") {
            subCard = createSubCard();
            subCardHeader = createCardHeader("Allotissement en provenance du Donateur");
            subCardBody = createCardBody("text-right");
            subCard.appendChild(subCardHeader);
            if (result1[(110 * (i - 1)) + 64][5] !== "0") {
                var divImmobiliers = createDiv();
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens immobiliers"));
                divImmobiliers.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Art."));
                divRow.appendChild(createLabel("col", "Quote part"));
                divRow.appendChild(createLabel("col", "Valeur donnée"));
                divRow.appendChild(createLabel("col", "Libellé"));
                divImmobiliers.appendChild(divRow);
                for (var k = 0; k < determinationDesBiens.immobilierCount; k++) {
                    var idx = (110 * (i - 1)) + 65 + k;
                    divRow = createDiv("row");
                    divRow.appendChild(createLabel("col", k + 1));
                    divRow.appendChild(createLabel("col", result1[idx][1]));
                    divRow.appendChild(createLabel("col", result1[idx][2]));
                    divRow.appendChild(createLabel("col", result1[idx][3]));
                    divRow.appendChild(createLabel("col", result1[idx][4]));
                    divImmobiliers.appendChild(divRow);
                }
                subCardBody.appendChild(divImmobiliers);
            }
            if (result1[(110 * (i - 1)) + 85][5] !== "0") {
                var divMobiliers = createDiv();
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens mobiliers"));
                divMobiliers.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Art."));
                divRow.appendChild(createLabel("col", "Quote part"));
                divRow.appendChild(createLabel("col", "Valeur donnée"));
                divRow.appendChild(createLabel("col", "Libellé"));
                divMobiliers.appendChild(divRow);
                for (var k = 0; k < determinationDesBiens.mobilierCount; k++) {
                    var idx = (110 * (i - 1)) + 65 + 21 + k;
                    divRow = createDiv("row");
                    divRow.appendChild(createLabel("col", k + 1));
                    divRow.appendChild(createLabel("col", result1[idx][1]));
                    divRow.appendChild(createLabel("col", result1[idx][2]));
                    divRow.appendChild(createLabel("col", result1[idx][3]));
                    divRow.appendChild(createLabel("col", result1[idx][4]));
                    divMobiliers.appendChild(divRow);
                }
                subCardBody.appendChild(divMobiliers);
            }
            if (result1[(110 * (i - 1)) + 106][5] !== "0") {
                var divImmobiliersEx = createDiv();
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens immobiliers exonérés"));
                divImmobiliersEx.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Art."));
                divRow.appendChild(createLabel("col", "Quote part"));
                divRow.appendChild(createLabel("col", "Valeur donnée"));
                divRow.appendChild(createLabel("col", "Libellé"));
                divImmobiliersEx.appendChild(divRow);
                for (var k = 0; k < determinationDesBiens.immobilierexonereCount; k++) {
                    var idx = (110 * (i - 1)) + 65 + 42 + k;
                    divRow = createDiv("row");
                    divRow.appendChild(createLabel("col", k + 1));
                    divRow.appendChild(createLabel("col", result1[idx][1]));
                    divRow.appendChild(createLabel("col", result1[idx][2]));
                    divRow.appendChild(createLabel("col", result1[idx][3]));
                    divRow.appendChild(createLabel("col", result1[idx][4]));
                    divImmobiliersEx.appendChild(divRow);
                }
                subCardBody.appendChild(divImmobiliersEx);
            }
            if (result1[(110 * (i - 1)) + 109][5] !== "0") {
                var divMobiliersEx = createDiv();
                divRow = createDiv("row text-center");
                label = createLabel("col", "Biens mobiliers exonérés");
                divRow.appendChild(label);
                divMobiliersEx.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Art."));
                divRow.appendChild(createLabel("col", "Quote part"));
                divRow.appendChild(createLabel("col", "Valeur donnée"));
                divRow.appendChild(createLabel("col", "Libellé"));
                divMobiliersEx.appendChild(divRow);
                for (var k = 0; k < determinationDesBiens.mobilierexonereCount; k++) {
                    var idx = (110 * (i - 1)) + 65 + 45 + k;
                    divRow = createDiv("row");
                    divRow.appendChild(createLabel("col", k + 1));
                    divRow.appendChild(createLabel("col", result1[idx][1]));
                    divRow.appendChild(createLabel("col", result1[idx][2]));
                    divRow.appendChild(createLabel("col", result1[idx][3]));
                    divRow.appendChild(createLabel("col", result1[idx][4]));
                    divMobiliersEx.appendChild(divRow);
                }
                subCardBody.appendChild(divMobiliersEx);
            }
            var divSommes = createDiv();
            if (result1[(110 * (i - 1)) + 112][5] !== "0") {
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Sommes d'argent"));
                divSommes.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Montant :"));
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 112][3]));
                divRow.appendChild(createLabel("col", ""));
                divSommes.appendChild(divRow);
            }
            if (result1[(110 * (i - 1)) + 113][5] !== "0") {
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens reçus antérieurement"));
                divSommes.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Montant :"));
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 113][3]));
                divRow.appendChild(createLabel("col", ""));
                divSommes.appendChild(divRow);
            }
            if (result1[(110 * (i - 1)) + 114][5] !== "0") {
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Soulte à verser"));
                divSommes.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Montant :"));
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 114][3]));
                divRow.appendChild(createLabel("col", ""));
                divSommes.appendChild(divRow);
            }
            if (result1[(110 * (i - 1)) + 115][5] !== "0") {
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Soulte à recevoir"));
                divSommes.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Montant :"));
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 115][3]));
                divRow.appendChild(createLabel("col", ""));
                divSommes.appendChild(divRow);
            }
            divRow = createDiv("row");
            divRow.appendChild(createLabel("col", ""));
            divRow.appendChild(createLabel("col", "Total reçu :"));
            divRow.appendChild(createLabel("col", ""));
            divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 116][3]));
            divRow.appendChild(createLabel("col", ""));
            divSommes.appendChild(divRow);
            subCardBody.appendChild(divSommes);
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Allotissement en provenance de la Donatrice
        if (result1[(110 * (i - 1)) + 117][5] !== "0") {
            subCard = createSubCard();
            subCardHeader = createCardHeader("Allotissement en provenance de la Donatrice");
            subCardBody = createCardBody("text-right");
            subCard.appendChild(subCardHeader);
            if (result1[(110 * (i - 1)) + 118][5] !== "0") {
                var divImmobiliers = createDiv();
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens immobiliers"));
                divImmobiliers.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Art."));
                divRow.appendChild(createLabel("col", "Quote part"));
                divRow.appendChild(createLabel("col", "Valeur donnée"));
                divRow.appendChild(createLabel("col", "Libellé"));
                divImmobiliers.appendChild(divRow);
                for (var k = 0; k < determinationDesBiens.immobilierCount; k++) {
                    var idx = (110 * (i - 1)) + 119 + k;
                    divRow = createDiv("row");
                    divRow.appendChild(createLabel("col", k + 1));
                    divRow.appendChild(createLabel("col", result1[idx][1]));
                    divRow.appendChild(createLabel("col", result1[idx][2]));
                    divRow.appendChild(createLabel("col", result1[idx][3]));
                    divRow.appendChild(createLabel("col", result1[idx][4]));
                    divImmobiliers.appendChild(divRow);
                }
                subCardBody.appendChild(divImmobiliers);
            }
            if (result1[(110 * (i - 1)) + 139][5] !== "0") {
                var divMobiliers = createDiv();
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens mobiliers"));
                divMobiliers.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Art."));
                divRow.appendChild(createLabel("col", "Quote part"));
                divRow.appendChild(createLabel("col", "Valeur donnée"));
                divRow.appendChild(createLabel("col", "Libellé"));
                divMobiliers.appendChild(divRow);
                for (var k = 0; k < determinationDesBiens.mobilierCount; k++) {
                    var idx = (110 * (i - 1)) + 140 + k;
                    divRow = createDiv("row");
                    divRow.appendChild(createLabel("col", k + 1));
                    divRow.appendChild(createLabel("col", result1[idx][1]));
                    divRow.appendChild(createLabel("col", result1[idx][2]));
                    divRow.appendChild(createLabel("col", result1[idx][3]));
                    divRow.appendChild(createLabel("col", result1[idx][4]));
                    divMobiliers.appendChild(divRow);
                }
                subCardBody.appendChild(divMobiliers);
            }
            if (result1[(110 * (i - 1)) + 160][5] !== "0") {
                var divImmobiliersEx = createDiv();
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens immobiliers exonérés"));
                divImmobiliersEx.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Art."));
                divRow.appendChild(createLabel("col", "Quote part"));
                divRow.appendChild(createLabel("col", "Valeur donnée"));
                divRow.appendChild(createLabel("col", "Libellé"));
                divImmobiliersEx.appendChild(divRow);
                for (var k = 0; k < determinationDesBiens.immobilierexonereCount; k++) {
                    var idx = (110 * (i - 1)) + 161 + k;
                    divRow = createDiv("row");
                    divRow.appendChild(createLabel("col", k + 1));
                    divRow.appendChild(createLabel("col", result1[idx][1]));
                    divRow.appendChild(createLabel("col", result1[idx][2]));
                    divRow.appendChild(createLabel("col", result1[idx][3]));
                    divRow.appendChild(createLabel("col", result1[idx][4]));
                    divImmobiliersEx.appendChild(divRow);
                }
                subCardBody.appendChild(divImmobiliersEx);
            }
            if (result1[(110 * (i - 1)) + 163][5] !== "0") {
                var divMobiliersEx = createDiv();
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens mobiliers exonérés"));
                divMobiliersEx.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Art."));
                divRow.appendChild(createLabel("col", "Quote part"));
                divRow.appendChild(createLabel("col", "Valeur donnée"));
                divRow.appendChild(createLabel("col", "Libellé"));
                divMobiliersEx.appendChild(divRow);
                for (var k = 0; k < determinationDesBiens.mobilierexonereCount; k++) {
                    var idx = (110 * (i - 1)) + 164 + k;
                    divRow = createDiv("row");
                    divRow.appendChild(createLabel("col", k + 1));
                    divRow.appendChild(createLabel("col", result1[idx][1]));
                    divRow.appendChild(createLabel("col", result1[idx][2]));
                    divRow.appendChild(createLabel("col", result1[idx][3]));
                    divRow.appendChild(createLabel("col", result1[idx][4]));
                    divMobiliersEx.appendChild(divRow);
                }
                subCardBody.appendChild(divMobiliersEx);
            }
            var divSommes = createDiv();
            if (result1[(110 * (i - 1)) + 166][5] !== "0") {
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Sommes d'argent"));
                divSommes.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Montant :"));
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 166][3]));
                divRow.appendChild(createLabel("col", ""));
                divSommes.appendChild(divRow);
            }
            if (result1[(110 * (i - 1)) + 167][5] !== "0") {
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Biens reçus antérieurement"));
                divSommes.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Montant :"));
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 167][3]));
                divRow.appendChild(createLabel("col", ""));
                divSommes.appendChild(divRow);
            }
            if (result1[(110 * (i - 1)) + 168][5] !== "0") {
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Soulte à verser"));
                divSommes.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Montant :"));
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 168][3]));
                divRow.appendChild(createLabel("col", ""));
                divSommes.appendChild(divRow);
            }
            if (result1[(110 * (i - 1)) + 169][5] !== "0") {
                divRow = createDiv("row text-center");
                divRow.appendChild(createLabel("col", "Soulte à recevoir"));
                divSommes.appendChild(divRow);
                divRow = createDiv("row");
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", "Montant :"));
                divRow.appendChild(createLabel("col", ""));
                divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 169][3]));
                divRow.appendChild(createLabel("col", ""));
                divSommes.appendChild(divRow);
            }
            divRow = createDiv("row");
            divRow.appendChild(createLabel("col", ""));
            divRow.appendChild(createLabel("col", "Total reçu :"));
            divRow.appendChild(createLabel("col", ""));
            divRow.appendChild(createLabel("col", result1[(110 * (i - 1)) + 170][3]));
            divRow.appendChild(createLabel("col", ""));
            divSommes.appendChild(divRow);
            subCardBody.appendChild(divSommes);
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        card.appendChild(cardHeader);
        card.appendChild(cardBody);
        divDonataireAllotissement.appendChild(card);
    }
    //card : CUMUL DES ATTRIBUTIONS ET DES SOULTES
    $("#row2700").hide();
    if (result2[0][5] !== "0") {
        $("#row2700").show();
        var divRow1171 = document.getElementById("row1171");
        $("#row1171").empty();
        $("#H2702").text(result2[1][3]);
        $("#H2704").text(result2[2][3]);
        $("#H2706").text(result2[3][3]);
        for (var i = 0; i < ddl3Value; i++) {
            subCard = createSubCard();
            subCardHeader = createCardHeader();
            subCardHeader.innerHTML = (i + 1) == 1 ? "1er Donataire" : (i + 1) + "ème Donataire";
            subCard.appendChild(subCardHeader);
            subCardBody = createCardBody("text-right");
            divRow = createDiv("row");
            divRow.appendChild(createLabel("col-sm-6 col-6", "Reçu du Père :"))
            divRow.appendChild(createLabel("col-sm-3 col-6", result2[(5 * i) + 4 + i][3]))
            subCardBody.appendChild(divRow);
            divRow = createDiv("row");
            divRow.appendChild(createLabel("col-sm-6 col-6", "Reçu de la Mère :"))
            divRow.appendChild(createLabel("col-sm-3 col-6", result2[(5 * i) + 5 + i][3]))
            subCardBody.appendChild(divRow);
            divRow = createDiv("row");
            divRow.appendChild(createLabel("col-sm-6 col-6", "Total reçu :"))
            divRow.appendChild(createLabel("col-sm-3 col-6", result2[(5 * i) + 6 + i][3]))
            subCardBody.appendChild(divRow);
            divRow = createDiv("row");
            divRow.appendChild(createLabel("col-sm-6 col-6", "Soulte à recevoir :"))
            divRow.appendChild(createLabel("col-sm-3 col-6", result2[(5 * i) + 7 + i][3]))
            subCardBody.appendChild(divRow);
            divRow = createDiv("row");
            divRow.appendChild(createLabel("col-sm-6 col-6", "Total de ses droits :"))
            divRow.appendChild(createLabel("col-sm-3 col-6", result2[(5 * i) + 8 + i][3]))
            subCardBody.appendChild(divRow);
            subCard.appendChild(subCardBody);
            divRow1171.appendChild(subCard);
        }
    }
}

function setValueSyntheseTaxePere(result) {
    var divSyntheseTaxePere = document.getElementById("div_synthese_taxe_pere");
    $("#div_synthese_taxe_pere").empty();
    for (var i = 0; i < ddl3Value; i++) {
        var index = 65 * i;
        var card = createCard();
        card.appendChild(createCardHeader(result[index][0].replace(':', '')));
        var cardBody = createCardBody("text-right");
        //sub-card : Part du donataire dans la donation
        var subCard = createSubCard();
        subCard.appendChild(createCardHeader("Part du donataire dans la donation"));
        var subCardBody = createCardBody();
        var row;
        if (result[index + 2][4] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-sm-6 col-6", result[index + 2][0]));
            row.appendChild(createLabel("col-sm-3 col-6", result[index + 2][3]));
            subCardBody.appendChild(row);
        }
        row = createDiv("row");
        row.appendChild(createLabel("col-sm-6 col-6", result[index + 3][0]));
        row.appendChild(createLabel("col-sm-3 col-6", result[index + 3][3]));
        subCardBody.appendChild(row);
        row = createDiv("row");
        row.appendChild(createLabel("col-sm-6 col-6", result[index + 4][0]));
        row.appendChild(createLabel("col-sm-3 col-6", result[index + 4][3]));
        subCardBody.appendChild(row);
        subCard.appendChild(subCardBody);
        cardBody.appendChild(subCard);
        //sub-card : Détermination de l'assiette taxable - Nue propriété
        if (result[index + 5][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Détermination de l'assiette taxable - Nue propriété"));
            subCardBody = createCardBody();
            if (result[index + 6][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 6][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 6][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 7][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 7][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 7][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 8][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 8][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 8][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 9][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 9][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 9][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 10][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 10][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 10][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 11][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 11][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 11][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 12][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 12][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 12][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 13][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 13][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 13][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 14][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 14][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 14][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 15][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 15][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 15][3]));
                subCardBody.appendChild(row);
            }
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Calcul des droits - Nue propriété
        if (result[index + 16][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Calcul des droits - Nue propriété"));
            subCardBody = createCardBody();
            if (result[index + 17][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 17][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 17][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 17][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 18][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 18][1]));
                var div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 18][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 19][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 19][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 19][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 19][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 20][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 20][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 20][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 21][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 21][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 21][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 21][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 22][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 22][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 22][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 23][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 23][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 23][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 23][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 24][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 24][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 24][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 25][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 25][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 25][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 25][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 26][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 26][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 26][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 27][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 27][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 27][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 27][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 28][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 28][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 28][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 29][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 29][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 29][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 29][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 30][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 30][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 30][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            row = createDiv("row");
            row.appendChild(createLabel("col-12 col-sm-4", result[index + 31][0]));
            row.appendChild(createLabel("col-6 col-sm-3", result[index + 31][2]));
            row.appendChild(createLabel("col-6 col-sm-5", result[index + 31][3]));
            subCardBody.appendChild(row);
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Détermination de l'assiette taxable - Pleine propriété
        if (result[index + 32][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Détermination de l'assiette taxable - Pleine propriété"));
            subCardBody = createCardBody();
            if (result[index + 33][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 33][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 33][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 34][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 34][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 34][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 35][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 35][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 35][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 36][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 36][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 36][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 37][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 37][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 37][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 38][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 38][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 38][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 39][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 39][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 39][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 40][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 40][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 40][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 41][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 41][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 41][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 42][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 42][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 42][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 43][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 43][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 43][3]));
                subCardBody.appendChild(row);
            }
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Calcul des droits - Pleine propriété
        if (result[index + 44][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Calcul des droits - Pleine propriété"));
            subCardBody = createCardBody();
            if (result[index + 45][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 45][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 45][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 45][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 46][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 46][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 46][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 47][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 47][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 47][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 47][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 48][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 48][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 48][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 49][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 49][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 49][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 49][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 50][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 50][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 50][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 51][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 51][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 51][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 51][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 52][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 52][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 52][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 53][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 53][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 53][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 53][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 54][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 54][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 54][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 55][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 55][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 55][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 55][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 56][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 56][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 56][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 57][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 57][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 57][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 57][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 58][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 58][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 58][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            row = createDiv("row");
            row.appendChild(createLabel("col-12 col-sm-4", result[index + 59][0]));
            row.appendChild(createLabel("col-6 col-sm-3", result[index + 59][2]));
            row.appendChild(createLabel("col-6 col-sm-5", result[index + 59][3]));
            subCardBody.appendChild(row);
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Droits à payer
        if (result[index + 60][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Droits à payer"));
            subCardBody = createCardBody();
            if (result[index + 61][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 61][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 61][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 62][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 62][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 62][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 63][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 63][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 63][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 64][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 64][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 64][3]));
                subCardBody.appendChild(row);
            }
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        card.appendChild(cardBody);
        divSyntheseTaxePere.appendChild(card);
    }
}

function setValueSyntheseTaxeMere(result) {
    var divSyntheseTaxeMere = document.getElementById("div_synthese_taxe_mere");
    $("#div_synthese_taxe_mere").empty();
    for (var i = 0; i < ddl3Value; i++) {
        var index = 65 * i;
        var card = createCard();
        card.appendChild(createCardHeader(result[index][0].replace(':', '')));
        var cardBody = createCardBody("text-right");
        //sub-card : Part du donataire dans la donation
        var subCard = createSubCard();
        subCard.appendChild(createCardHeader("Part du donataire dans la donation"));
        var subCardBody = createCardBody();
        var row;
        if (result[index + 2][4] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-sm-6 col-6", result[index + 2][0]));
            row.appendChild(createLabel("col-sm-3 col-6", result[index + 2][3]));
            subCardBody.appendChild(row);
        }
        row = createDiv("row");
        row.appendChild(createLabel("col-sm-6 col-6", result[index + 3][0]));
        row.appendChild(createLabel("col-sm-3 col-6", result[index + 3][3]));
        subCardBody.appendChild(row);
        row = createDiv("row");
        row.appendChild(createLabel("col-sm-6 col-6", result[index + 4][0]));
        row.appendChild(createLabel("col-sm-3 col-6", result[index + 4][3]));
        subCardBody.appendChild(row);
        subCard.appendChild(subCardBody);
        cardBody.appendChild(subCard);
        //sub-card : Détermination de l'assiette taxable - Nue propriété
        if (result[index + 5][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Détermination de l'assiette taxable - Nue propriété"));
            subCardBody = createCardBody();
            if (result[index + 6][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 6][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 6][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 7][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 7][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 7][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 8][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 8][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 8][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 9][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 9][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 9][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 10][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 10][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 10][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 11][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 11][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 11][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 12][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 12][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 12][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 13][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 13][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 13][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 14][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 14][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 14][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 15][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 15][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 15][3]));
                subCardBody.appendChild(row);
            }
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Calcul des droits - Nue propriété
        if (result[index + 16][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Calcul des droits - Nue propriété"));
            subCardBody = createCardBody();
            if (result[index + 17][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 17][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 17][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 17][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 18][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 18][1]));
                var div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 18][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 19][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 19][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 19][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 19][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 20][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 20][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 20][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 21][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 21][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 21][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 21][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 22][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 22][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 22][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 23][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 23][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 23][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 23][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 24][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 24][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 24][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 25][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 25][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 25][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 25][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 26][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 26][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 26][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 27][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 27][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 27][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 27][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 28][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 28][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 28][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 29][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 29][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 29][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 29][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 30][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 30][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 30][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            row = createDiv("row");
            row.appendChild(createLabel("col-12 col-sm-4", result[index + 31][0]));
            row.appendChild(createLabel("col-6 col-sm-3", result[index + 31][2]));
            row.appendChild(createLabel("col-6 col-sm-5", result[index + 31][3]));
            subCardBody.appendChild(row);
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Détermination de l'assiette taxable - Pleine propriété
        if (result[index + 32][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Détermination de l'assiette taxable - Pleine propriété"));
            subCardBody = createCardBody();
            if (result[index + 33][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 33][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 33][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 34][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 34][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 34][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 35][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 35][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 35][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 36][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 36][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 36][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 37][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 37][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 37][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 38][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 38][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 38][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 39][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 39][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 39][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 40][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 40][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 40][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 41][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 41][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 41][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 42][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 42][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 42][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 43][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 43][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 43][3]));
                subCardBody.appendChild(row);
            }
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Calcul des droits - Pleine propriété
        if (result[index + 44][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Calcul des droits - Pleine propriété"));
            subCardBody = createCardBody();
            if (result[index + 45][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 45][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 45][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 45][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 46][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 46][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 46][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 47][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 47][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 47][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 47][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 48][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 48][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 48][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 49][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 49][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 49][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 49][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 50][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 50][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 50][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 51][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 51][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 51][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 51][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 52][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 52][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 52][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 53][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 53][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 53][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 53][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 54][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 54][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 54][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 55][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 55][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 55][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 55][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 56][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 56][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 56][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            if (result[index + 57][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 57][1]));
                row.appendChild(createLabel("col-2 col-sm-1", "sur"));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 57][2]));
                row.appendChild(createLabel("col-6 col-sm-2", "="));
                row.appendChild(createLabel("col-6 col-sm-3", result[index + 57][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 58][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-4 col-sm-3", result[index + 58][1]));
                div = createDiv("col-8 col-sm-9 text-center");
                div.appendChild(createLabel("", result[index + 58][2]))
                row.appendChild(div);
                subCardBody.appendChild(row);
            }
            row = createDiv("row");
            row.appendChild(createLabel("col-12 col-sm-4", result[index + 59][0]));
            row.appendChild(createLabel("col-6 col-sm-3", result[index + 59][2]));
            row.appendChild(createLabel("col-6 col-sm-5", result[index + 59][3]));
            subCardBody.appendChild(row);
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        //sub-card : Droits à payer
        if (result[index + 60][4] !== "0") {
            subCard = createSubCard();
            subCard.appendChild(createCardHeader("Droits à payer"));
            subCardBody = createCardBody();
            if (result[index + 61][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 61][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 61][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 62][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 62][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 62][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 63][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 63][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 63][3]));
                subCardBody.appendChild(row);
            }
            if (result[index + 64][4] !== "0") {
                row = createDiv("row");
                row.appendChild(createLabel("col-sm-6 col-6", result[index + 64][0]));
                row.appendChild(createLabel("col-sm-3 col-6", result[index + 64][3]));
                subCardBody.appendChild(row);
            }
            subCard.appendChild(subCardBody);
            cardBody.appendChild(subCard);
        }
        card.appendChild(cardBody);
        divSyntheseTaxeMere.appendChild(card);
    }
}

function setValueTaxeDonation(result) {
    var divTaxeDonation = document.getElementById("div_taxe_donation");
    $("#div_taxe_donation").empty();
    //card : TOTAL DES DROITS ET FRAIS
    var card = createCard();
    card.appendChild(createCardHeader("TOTAL DES DROITS ET FRAIS"));
    var cardBody = createCardBody("text-right");
    var row = createDiv("row");
    row.appendChild(createLabel("col-sm-6 col-6", "Trésor :"));
    row.appendChild(createLabel("col-sm-3 col-6", result[1][4]));
    cardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-sm-6 col-6", "Débours :"));
    row.appendChild(createLabel("col-sm-3 col-6", result[2][4]));
    cardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-sm-6 col-6", "Emoluments du notaire HT :"));
    row.appendChild(createLabel("col-sm-3 col-6", result[3][4]));
    cardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-sm-6 col-6", "Convention d'honoraires :"));
    row.appendChild(createLabel("col-sm-3 col-6", result[4][4]));
    cardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-sm-6 col-6", "Total des frais :"));
    row.appendChild(createLabel("col-sm-3 col-6", result[5][4]));
    cardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-sm-6 col-6", "Montant des droits :"));
    row.appendChild(createLabel("col-sm-3 col-6", result[6][4]));
    cardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-sm-6 col-6", "Montant des droits et frais :"));
    row.appendChild(createLabel("col-sm-3 col-6", result[7][4]));
    cardBody.appendChild(row);
    card.appendChild(cardBody);
    divTaxeDonation.appendChild(card);
    //card : DETAILS DES FRAIS
    card = createCard();
    card.appendChild(createCardHeader("DETAILS DES FRAIS"));
    cardBody = createCardBody("text-right");
    //sub-card : Emoluments du Notaire (Donateur) - C.com. Article A 444-67
    var subCard, subCardBody;
    if (result[9][5] !== "0") {
        subCard = createSubCard();
        subCard.appendChild(createCardHeader("Emoluments du Notaire (Donateur) - C.com. Article A 444-67"));
        subCardBody = createCardBody();
        if (result[10][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-4 col-sm-3", result[10][1]));
            row.appendChild(createLabel("col-2 col-sm-1", "sur"));
            row.appendChild(createLabel("col-6 col-sm-3", result[10][3]));
            row.appendChild(createLabel("col-6 col-sm-2", "="));
            row.appendChild(createLabel("col-6 col-sm-3", result[10][4]));
            subCardBody.appendChild(row);
        }
        if (result[11][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-4 col-sm-3", result[11][1]));
            row.appendChild(createLabel("col-2 col-sm-1", "sur"));
            row.appendChild(createLabel("col-6 col-sm-3", result[11][3]));
            row.appendChild(createLabel("col-6 col-sm-2", "="));
            row.appendChild(createLabel("col-6 col-sm-3", result[11][4]));
            subCardBody.appendChild(row);
        }
        if (result[12][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-4 col-sm-3", result[12][1]));
            row.appendChild(createLabel("col-2 col-sm-1", "sur"));
            row.appendChild(createLabel("col-6 col-sm-3", result[12][3]));
            row.appendChild(createLabel("col-6 col-sm-2", "="));
            row.appendChild(createLabel("col-6 col-sm-3", result[12][4]));
            subCardBody.appendChild(row);
        }
        if (result[13][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-4 col-sm-3", result[13][1]));
            row.appendChild(createLabel("col-2 col-sm-1", "sur"));
            row.appendChild(createLabel("col-6 col-sm-3", result[13][3]));
            row.appendChild(createLabel("col-6 col-sm-2", "="));
            row.appendChild(createLabel("col-6 col-sm-3", result[13][4]));
            subCardBody.appendChild(row);
        }
        if (result[14][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-3", "Total :"));
            row.appendChild(createLabel("col-6 col-sm-4", result[14][3]));
            subCardBody.appendChild(row);
        }
        if (result[15][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "TOTAL Hors T.V.A :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[15][4]));
            subCardBody.appendChild(row);
        }
        if (result[16][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Emolument minimum :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[16][4]));
            subCardBody.appendChild(row);
        }
        if (result[17][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Remise sur prestations non visées à l'article 444-174 du Code de Commerce :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[17][4]));
            subCardBody.appendChild(row);
        }
        if (result[18][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Remise sur prestations visées à l'article 444-174 du Code de Commerce (Entreprise) :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[18][4]));
            subCardBody.appendChild(row);
        }
        if (result[19][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Total après remise HT :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[19][4]));
            subCardBody.appendChild(row);
        }
        subCard.appendChild(subCardBody);
        cardBody.appendChild(subCard);
    }
    //sub-card : Emoluments du Notaire (Donatrice) - C.com. Article A 444-67
    if (result[20][5] !== "0") {
        subCard = createSubCard();
        subCard.appendChild(createCardHeader("Emoluments du Notaire (Donatrice) - C.com. Article A 444-67"));
        subCardBody = createCardBody();
        if (result[21][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-4 col-sm-3", result[21][1]));
            row.appendChild(createLabel("col-2 col-sm-1", "sur"));
            row.appendChild(createLabel("col-6 col-sm-3", result[21][3]));
            row.appendChild(createLabel("col-6 col-sm-2", "="));
            row.appendChild(createLabel("col-6 col-sm-3", result[21][4]));
            subCardBody.appendChild(row);
        }
        if (result[22][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-4 col-sm-3", result[22][1]));
            row.appendChild(createLabel("col-2 col-sm-1", "sur"));
            row.appendChild(createLabel("col-6 col-sm-3", result[22][3]));
            row.appendChild(createLabel("col-6 col-sm-2", "="));
            row.appendChild(createLabel("col-6 col-sm-3", result[22][4]));
            subCardBody.appendChild(row);
        }
        if (result[23][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-4 col-sm-3", result[23][1]));
            row.appendChild(createLabel("col-2 col-sm-1", "sur"));
            row.appendChild(createLabel("col-6 col-sm-3", result[23][3]));
            row.appendChild(createLabel("col-6 col-sm-2", "="));
            row.appendChild(createLabel("col-6 col-sm-3", result[23][4]));
            subCardBody.appendChild(row);
        }
        if (result[24][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-4 col-sm-3", result[24][1]));
            row.appendChild(createLabel("col-2 col-sm-1", "sur"));
            row.appendChild(createLabel("col-6 col-sm-3", result[24][3]));
            row.appendChild(createLabel("col-6 col-sm-2", "="));
            row.appendChild(createLabel("col-6 col-sm-3", result[24][4]));
            subCardBody.appendChild(row);
        }
        if (result[25][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-3", "Total :"));
            row.appendChild(createLabel("col-6 col-sm-4", result[25][3]));
            subCardBody.appendChild(row);
        }
        if (result[26][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "TOTAL Hors T.V.A :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[26][4]));
            subCardBody.appendChild(row);
        }
        if (result[27][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Emolument minimum :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[27][4]));
            subCardBody.appendChild(row);
        }
        if (result[28][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Remise sur prestations non visées à l'article 444-174 du Code de Commerce :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[28][4]));
            subCardBody.appendChild(row);
        }
        if (result[29][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Remise sur prestations visées à l'article 444-174 du Code de Commerce (Entreprise) :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[29][4]));
            subCardBody.appendChild(row);
        }
        if (result[30][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Total après remise HT :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[30][4]));
            subCardBody.appendChild(row);
        }
        subCard.appendChild(subCardBody);
        cardBody.appendChild(subCard);
    }
    //sub-card : TVA
    subCard = createSubCard();
    subCard.appendChild(createCardHeader("TVA"));
    subCardBody = createCardBody();
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "Emoluments du Notaire - Donateur :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[32][4]));
    subCardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "Emoluments du Notaire - Donatrice :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[33][4]));
    subCardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "Emoluments de formalités :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[34][4]));
    subCardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "Total Emoluments du notaire HT :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[35][4]));
    subCardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "TVA :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[36][4]));
    subCardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "Total TTC :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[37][4]));
    subCardBody.appendChild(row);
    subCard.appendChild(subCardBody);
    cardBody.appendChild(subCard);
    //sub-card : Convention d'honoraires
    subCard = createSubCard();
    subCard.appendChild(createCardHeader("Convention d'honoraires"));
    subCardBody = createCardBody();
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "Convention d'honoraires HT :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[39][4]));
    subCardBody.appendChild(row);
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "TVA :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[40][4]));
    subCardBody.appendChild(row);
    subCard.appendChild(subCardBody);
    cardBody.appendChild(subCard);
    //sub-card : Débours
    subCard = createSubCard();
    subCard.appendChild(createCardHeader("Débours"));
    subCardBody = createCardBody();
    row = createDiv("row");
    row.appendChild(createLabel("col-6 col-sm-9", "Débours :"));
    row.appendChild(createLabel("col-6 col-sm-3", result[42][4]));
    subCardBody.appendChild(row);
    subCard.appendChild(subCardBody);
    cardBody.appendChild(subCard);
    card.appendChild(cardBody);
    divTaxeDonation.appendChild(card);
    //card : TRESOR PUBLIC
    card = createCard();
    card.appendChild(createCardHeader("TRESOR PUBLIC"));
    cardBody = createCardBody("text-right");
    //sub-card : Publicité aux Hypothéques
    subCard = createSubCard();
    subCard.appendChild(createCardHeader("Publicité aux Hypothéques"));
    subCardBody = createCardBody();
    row = createDiv("row text-left");
    row.appendChild(createLabel("col", "Taxe Publicité Foncière :"));
    subCardBody.appendChild(row);
    if (result[46][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Total des biens immobiliers non exonérés (tenant compte d'un usufruit éventuel) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[46][4]));
        subCardBody.appendChild(row);
    }
    if (result[47][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Total des biens immobiliers exonérés (tenant compte d'un usufruit éventuel) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[47][4]));
        subCardBody.appendChild(row);
    }
    if (result[48][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Immobilier de l'entreprise individuelle (déduction d'un usufruit éventuel) (Bien 1) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[48][4]));
        subCardBody.appendChild(row);
    }
    if (result[49][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Immobilier de l'entreprise individuelle (déduction d'un usufruit éventuel) (Bien 2) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[49][4]));
        subCardBody.appendChild(row);
    }
    if (result[50][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Base fiscale :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[50][4]));
        subCardBody.appendChild(row);
    }
    if (result[51][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col", "Taxe Publicité Foncière :"));
        row.appendChild(createLabel("col", result[51][1]));
        row.appendChild(createLabel("col", result[51][3]));
        row.appendChild(createLabel("col", result[51][4]));
        subCardBody.appendChild(row);
    }
    row = createDiv("row text-left");
    row.appendChild(createLabel("col", "CSI (art. 879 du CGI) :"));
    subCardBody.appendChild(row);
    if (result[54][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Total des biens immobiliers non exonérés (tenant compte d'un usufruit éventuel) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[54][4]));
        subCardBody.appendChild(row);
    }
    if (result[55][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Total des biens immobiliers exonérés (tenant compte d'un usufruit éventuel) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[55][4]));
        subCardBody.appendChild(row);
    }
    if (result[56][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Total des biens immobiliers incorporés (donations antérieures) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[56][4]));
        subCardBody.appendChild(row);
    }
    if (result[57][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Immobilier de l'entreprise individuelle (déduction d'un usufruit éventuel) (Bien 1) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[57][4]));
        subCardBody.appendChild(row);
    }
    if (result[58][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Immobilier de l'entreprise individuelle (déduction d'un usufruit éventuel) (Bien 2) :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[58][4]));
        subCardBody.appendChild(row);
    }
    if (result[59][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Réversibilité de l'usufruit :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[59][4]));
        subCardBody.appendChild(row);
    }
    if (result[60][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col-6 col-sm-9", "Base fiscale :"));
        row.appendChild(createLabel("col-6 col-sm-3", result[60][4]));
        subCardBody.appendChild(row);
    }
    if (result[61][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col", "CSI (art. 879 du CGI) :"));
        row.appendChild(createLabel("col", result[61][1]));
        row.appendChild(createLabel("col", result[61][3]));
        row.appendChild(createLabel("col", result[61][4]));
        subCardBody.appendChild(row);
    }
    if (result[63][5] !== "0") {
        row = createDiv("row");
        row.appendChild(createLabel("col", "Droit de partage (Art.746 du CGI) :"));
        row.appendChild(createLabel("col", "CSI (art. 879 du CGI) :"));
        row.appendChild(createLabel("col", result[63][1]));
        row.appendChild(createLabel("col", result[63][3]));
        row.appendChild(createLabel("col", result[63][4]));
        subCardBody.appendChild(row);
    }
    subCard.appendChild(subCardBody);
    cardBody.appendChild(subCard);
    //sub-card : Enregistrement
    if (result[65][5] !== "0") {
        subCard = createSubCard();
        subCard.appendChild(createCardHeader("Enregistrement"));
        subCardBody = createCardBody();
        row = createDiv("row text-left");
        row.appendChild(createLabel("col", "Droit de partage (Art.746 du CGI) :"));
        subCardBody.appendChild(row);
        if (result[66][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col", "CSI (art. 879 du CGI) :"));
            row.appendChild(createLabel("col", result[66][1]));
            row.appendChild(createLabel("col", result[66][3]));
            row.appendChild(createLabel("col", result[66][4]));
            subCardBody.appendChild(row);
        }
        if (result[68][5] !== "0") {
            row = createDiv("row");
            row.appendChild(createLabel("col-6 col-sm-9", "Droit fixe (clause de réversion) :"));
            row.appendChild(createLabel("col-6 col-sm-3", result[68][4]));
            subCardBody.appendChild(row);
        }
        subCard.appendChild(subCardBody);
        cardBody.appendChild(subCard);
    }
    card.appendChild(cardBody);
    divTaxeDonation.appendChild(card);
}

function SynthaseDonationResult() {
    var row = 0, column = 0;
    if ($(".btnRappelPereResult.active").attr("data-value") !== undefined) {
        switch ($(".btnRappelPereResult.active").attr("data-value")) {
            case "1":
                row = 4;
                column = 3;
                break;
            case "2":
                row = 18;
                column = 4;
                break;
            case "3":
                row = 32;
                column = 5;
                break;
            case "4":
                row = 46;
                column = 6;
                break;
            case "5":
                row = 60;
                column = 7;
                break;
            case "6":
                row = 74;
                column = 8;
                break;
            case "7":
                row = 88;
                column = 9;
                break;
            case "8":
                row = 102;
                column = 10;
                break;
            case "9":
                row = 116;
                column = 11;
                break;
            case "10":
                row = 130;
                column = 12;
                break;
            default:
        }
    }
    else {
        switch ($(".btnRappelMereResult.active").attr("data-value")) {
            case "1":
                row = 11;
                column = 3;
                break;
            case "2":
                row = 25;
                column = 4;
                break;
            case "3":
                row = 39;
                column = 5;
                break;
            case "4":
                row = 53;
                column = 6;
                break;
            case "5":
                row = 67;
                column = 7;
                break;
            case "6":
                row = 81;
                column = 8;
                break;
            case "7":
                row = 95;
                column = 9;
                break;
            case "8":
                row = 109;
                column = 10;
                break;
            case "9":
                row = 123;
                column = 11;
                break;
            case "10":
                row = 137;
                column = 12;
                break;
            default:
        }
    }
    $.ajax({
        url: 'DON-01.aspx/SynthaseDonation',
        data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
            ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
            ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
            ", 'row':" + row + ", 'column':" + column + "}",
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        beforeSend: function () {
            $("#preloader").show();
        },
        success: function (response) {
            resultSynthaseDonation = response.d;
            var i = 1;
            $.map(response.d, function (item) {
                if (i === 7 && item[0] === "0") {
                    $("#SORTIE_DON_7").show();
                } else {
                    $("#SORTIE_DON_" + i).text(item[0]);
                }
                i += 1;
            });
            ShowHide_input_form($("#btnSynthaseDonation"));
        },
        complete: function () {
            $("#preloader").hide();
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
        }
    });
}

$("#btnSynthese").click(function () {
    enumCalculation = 1;
    var height = 61;
    switch (ddl3Value)
    {
        case 1:
            height = 171;
            break;
        case 2:
            height = 281;
            break;
        case 3:
            height = 391;
            break;
        case 4:
            height = 501;
            break;
        case 5:
            height = 611;
            break;
        case 6:
            height = 721;
            break;
        case 7:
            height = 831;
            break;
        case 8:
            height = 941;
            break;
        case 9:
            height = 1051;
            break;
        case 10:
            height = 1161;
            break;
        default:
            break;
    }
    $.ajax({
        url: 'DON-01.aspx/Synthese',
        data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
            ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
            ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
            ", 'height':" + height +
            "}",
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        beforeSend: function () {
            $("#preloader").show();
        },
        success: function (response) {
            resultSynthese = response.d;
            setValueSynthese(resultSynthese.result1, resultSynthese.result2);
            console.log(resultSynthese);
            ShowHide_input_form($("#btnSynthese"));
        },
        complete: function () {
            $("#preloader").hide();
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
        }
    });
});

$("#btnSyntheseTaxePere").click(function () {
    enumCalculation = 2;
    var height;
    switch (ddl3Value) {
        case 1:
            height = 65;
            break;
        case 2:
            height = 130;
            break;
        case 3:
            height = 195;
            break;
        case 4:
            height = 260;
            break;
        case 5:
            height = 325;
            break;
        case 6:
            height = 390;
            break;
        case 7:
            height = 455;
            break;
        case 8:
            height = 520;
            break;
        case 9:
            height = 585;
            break;
        case 10:
            height = 650;
            break;
        default:
            break;
    }
    $.ajax({
        url: 'DON-01.aspx/SyntheseTaxePere',
        data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
            ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
            ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
            ", 'height':" + height +
            "}",
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        beforeSend: function () {
            $("#preloader").show();
        },
        success: function (response) {
            resultSyntheseTaxePere = response.d;
            setValueSyntheseTaxePere(resultSyntheseTaxePere);
            ShowHide_input_form($("#btnSyntheseTaxePere"));
        },
        complete: function () {
            $("#preloader").hide();
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
        }
    });
});

$("#btnSyntheseTaxeMere").click(function () {
    enumCalculation = 3;
    var height;
    switch (ddl3Value) {
        case 1:
            height = 65;
            break;
        case 2:
            height = 130;
            break;
        case 3:
            height = 195;
            break;
        case 4:
            height = 260;
            break;
        case 5:
            height = 325;
            break;
        case 6:
            height = 390;
            break;
        case 7:
            height = 455;
            break;
        case 8:
            height = 520;
            break;
        case 9:
            height = 585;
            break;
        case 10:
            height = 650;
            break;
        default:
            break;
    }
    $.ajax({
        url: 'DON-01.aspx/SyntheseTaxeMere',
        data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
            ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
            ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
            ", 'height':" + height +
            "}",
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        beforeSend: function () {
            $("#preloader").show();
        },
        success: function (response) {
            resultSyntheseTaxeMere = response.d;
            setValueSyntheseTaxeMere(resultSyntheseTaxeMere);
            ShowHide_input_form($("#btnSyntheseTaxeMere"));
        },
        complete: function () {
            $("#preloader").hide();
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
        }
    });
});

$("#btnTaxeDonation").click(function () {
    enumCalculation = 4;
    $.ajax({
        url: 'DON-01.aspx/TaxeDonation',
        data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
            ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
            ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
            "}",
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        beforeSend: function () {
            $("#preloader").show();
        },
        success: function (response) {
            resultTaxeDonation = response.d;
            setValueTaxeDonation(resultTaxeDonation);
            ShowHide_input_form($("#btnTaxeDonation"));
        },
        complete: function () {
            $("#preloader").hide();
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
        }
    });
});

$("#btnSynthaseDonation").click(function () {
    enumCalculation = 5;
    SynthaseDonationResult();
});

if ($("#postback").val() === "true") {
    for (var i = 1; i <= determinationDesBiens.immobilierCount; i++) {
        AddImmobilier(i);
        $("#ddl1_immobilier_" + i).val(determinationDesBiens.arrImmobilier[i - 1].origine).trigger("change");
        $("#ddl2_immobilier_" + i).val(determinationDesBiens.arrImmobilier[i - 1].attribution).trigger("change");
        $("#cb1_immobilier_" + i).prop('checked', determinationDesBiens.arrImmobilier[i - 1].reserve);
        $("#txtZone01_immobilier_" + i).val(determinationDesBiens.arrImmobilier[i - 1].valeur);
        $("#txtZone02_immobilier_" + i).val(determinationDesBiens.arrImmobilier[i - 1].passif);
    }
    for (var i = 1; i <= determinationDesBiens.immobilierexonereCount; i++) {
        AddImmobilierExonere(i);
        $("#ddl1_immobilierexonéré_" + i).val(determinationDesBiens.arrImmobilierexonere[i - 1].origine).trigger("change");
        $("#ddl2_immobilierexonéré_" + i).val(determinationDesBiens.arrImmobilierexonere[i - 1].attribution).trigger("change");
        $("#ddl3_immobilierexonéré_" + i).val(determinationDesBiens.arrImmobilierexonere[i - 1].typedebien).trigger("change");
        $("#cb1_immobilierexonéré_" + i).prop('checked', determinationDesBiens.arrImmobilierexonere[i - 1].reserve);
        $("#txtZone01_immobilierexonéré_" + i).val(determinationDesBiens.arrImmobilierexonere[i - 1].valeur);
        $("#txtZone02_immobilierexonéré_" + i).val(determinationDesBiens.arrImmobilierexonere[i - 1].passif);
    }
    for (var i = 1; i <= determinationDesBiens.mobilierCount; i++) {
        AddMobilier(i);
        $("#ddl1_mobilier_" + i).val(determinationDesBiens.arrMobilier[i - 1].origine).trigger("change");
        $("#ddl2_mobilier_" + i).val(determinationDesBiens.arrMobilier[i - 1].attribution).trigger("change");
        $("#cb1_mobilier_" + i).prop('checked', determinationDesBiens.arrMobilier[i - 1].reserve);
        $("#txtZone01_mobilier_" + i).val(determinationDesBiens.arrMobilier[i - 1].valeur);
        $("#txtZone02_mobilier_" + i).val(determinationDesBiens.arrMobilier[i - 1].passif);
    }
    for (var i = 1; i <= determinationDesBiens.mobilierexonereCount; i++) {
        AddMobilierExonere(i);
        $("#ddl1_mobilierexonéré_" + i).val(determinationDesBiens.arrMobilierexonere[i - 1].origine).trigger("change");
        $("#ddl2_mobilierexonéré_" + i).val(determinationDesBiens.arrMobilierexonere[i - 1].attribution).trigger("change");
        $("#ddl3_mobilierexonéré_" + i).val(determinationDesBiens.arrMobilierexonere[i - 1].typedebien).trigger("change");
        $("#cb1_mobilierexonéré_" + i).prop('checked', determinationDesBiens.arrMobilierexonere[i - 1].reserve);
        $("#txtZone01_mobilierexonéré_" + i).val(determinationDesBiens.arrMobilierexonere[i - 1].valeur);
        $("#txtZone02_mobilierexonéré_" + i).val(determinationDesBiens.arrMobilierexonere[i - 1].passif);
    }
    for (var i = 1; i <= determinationDesBiens.sommeargentCount; i++) {
        AddSommeargent(i);
        $("#ddl1_sommeargent_" + i).val(determinationDesBiens.arrSommeargent[i - 1].origine).trigger("change");
        $("#ddl2_sommeargent_" + i).val(determinationDesBiens.arrSommeargent[i - 1].attribution).trigger("change");
        $("#txtZone01_sommeargent_" + i).val(determinationDesBiens.arrSommeargent[i - 1].valeur);
    }
}

function downloadFile(urlToSend, fileName) {
    var req = new XMLHttpRequest();
    req.open("GET", urlToSend, true);
    req.responseType = "blob";
    req.onload = function (event) {
        var blob = req.response;
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = fileName;
        link.click();
    };
    req.send();
};

$("#btnPrint").click(function () {
    var result;
    switch (enumCalculation) {
        case 1:
            $.ajax({
                url: 'DON-01.aspx/HtmlReportSynthese',
                data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
                    ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
                    ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
                    ", 'dossier':'" + $("#txtDossier").val() +
                    "', 'date':'" + $("#txtDateSignature").val() +
                    "', 'redacteur':'" + $("#txtRedacteur").val() + "'" +
                    ", 'result1':" + JSON.stringify(JSON.stringify(resultSynthese.result1)) +
                    ", 'result2':" + JSON.stringify(JSON.stringify(resultSynthese.result2)) +
                    "}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    var url = window.location.origin + "/tmp/DON-01/" + response.d;
                    downloadFile(url, response.d);
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            break;
        case 2:
            $.ajax({
                url: 'DON-01.aspx/HtmlReportSyntheseTaxePere',
                data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
                    ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
                    ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
                    ", 'dossier':'" + $("#txtDossier").val() +
                    "', 'date':'" + $("#txtDateSignature").val() +
                    "', 'redacteur':'" + $("#txtRedacteur").val() + "'" +
                    ", 'result':" + JSON.stringify(JSON.stringify(resultSyntheseTaxePere)) +
                    "}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    var url = window.location.origin + "/tmp/DON-01/" + response.d;
                    downloadFile(url, response.d);
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            break;
        case 3:
            $.ajax({
                url: 'DON-01.aspx/HtmlReportSyntheseTaxeMere',
                data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
                    ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
                    ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
                    ", 'dossier':'" + $("#txtDossier").val() +
                    "', 'date':'" + $("#txtDateSignature").val() +
                    "', 'redacteur':'" + $("#txtRedacteur").val() + "'" +
                    ", 'result':" + JSON.stringify(JSON.stringify(resultSyntheseTaxeMere)) +
                    "}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    var url = window.location.origin + "/tmp/DON-01/" + response.d;
                    downloadFile(url, response.d);
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            break;
        case 4:
            $.ajax({
                url: 'DON-01.aspx/HtmlReportSyntheseTaxeDonation',
                data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
                    ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
                    ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
                    ", 'dossier':'" + $("#txtDossier").val() +
                    "', 'date':'" + $("#txtDateSignature").val() +
                    "', 'redacteur':'" + $("#txtRedacteur").val() + "'" +
                    ", 'result':" + JSON.stringify(JSON.stringify(resultTaxeDonation)) +
                    "}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    var url = window.location.origin + "/tmp/DON-01/" + response.d;
                    downloadFile(url, response.d);
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            break;
        case 5:
            $.ajax({
                url: 'DON-01.aspx/HtmlReportSyntheseDonation',
                data: "{'strInputForm1':" + JSON.stringify($("#hdJsonDeterminationDesDonataires").val()) +
                    ", 'strInputForm2':" + JSON.stringify($("#hdJsonDeterminationDesBiens").val()) +
                    ", 'strInputForm3':" + JSON.stringify($("#hdJsonRappelDeDonationsAnterieures").val()) +
                    ", 'dossier':'" + $("#txtDossier").val() +
                    "', 'date':'" + $("#txtDateSignature").val() +
                    "', 'redacteur':'" + $("#txtRedacteur").val() + "'" +
                    ", 'result':" + JSON.stringify(JSON.stringify(resultSynthaseDonation)) +
                    ", 'headerPereMere':'" + $("#headerPereMere").text() +
                    "', 'subHeaderPereMere':'" + $("#subHeaderPereMere").text() +
                    "'}",
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    var url = window.location.origin + "/tmp/DON-01/" + response.d;
                    downloadFile(url, response.d);
                },
                failure: function (jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText); // Display error message  
                }
            });
            break;
        default:
    }
});
