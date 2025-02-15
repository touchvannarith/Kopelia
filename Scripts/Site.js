function HideElements(elements, parent) {
    $.each(elements, function (index, element) {
        if (parent !== "") {
            element.closest(parent).hide();
            element.hide();
        } else {
            element.hide();
        }
        switch (element.prop("type")) {
            case "checkbox":
                element.prop('checked', false);
                break;
            case "select-one":
                //element.val($('#' + element.attr('id') +' option:first-child').val()).trigger('change');
                element.val($('#' + element.attr('id') +' option:first-child').val());
                break;
            case "text":
                element.val("");
                break;
            default:
        }
    });
}

function ShowElements(elements, parent) {
    $.each(elements, function (index, element) {
        if (parent !== "") {
            element.closest(parent).show();
            element.show();
        } else {
            element.show();
        }
    });
}

$(".numberFormatFR").on("keypress change input", function (event) {
    $(this).val($(this).val().replace(/[^0-9\,]/g, ''));
    if ((($(this).val().indexOf(',') !== -1) && (event.which < 48 || event.which > 57)) || event.which === 46) {
        event.preventDefault();
    }
});

$(".card-header > button").click(function () {
    if ($(this).closest(".sub-card").length === 1) {
        if ($(this).children("i").hasClass("fa-minus")) {
            $(this).closest(".sub-card").children(".card-body").hide();
            $(this).children("i").removeClass("fa-minus").addClass("fa-plus");
        } else if ($(this).children("i").hasClass("fa-plus")) {
            $(this).closest(".sub-card").children(".card-body").show();
            $(this).children("i").removeClass("fa-plus").addClass("fa-minus");
        }
    } else {
        if ($(this).children("i").hasClass("fa-minus")) {
            $(this).closest(".card").children(".card-body").hide();
            $(this).children("i").removeClass("fa-minus").addClass("fa-plus");
        } else if ($(this).children("i").hasClass("fa-plus")) {
            $(this).closest(".card").children(".card-body").show();
            $(this).children("i").removeClass("fa-plus").addClass("fa-minus");
        }
    }
});

$('.datePicker').datetimepicker({
    locale: 'fr',
    format: 'L',
    defaultDate: new Date()
});

function ConvertToFloat(str) {
    try {
        if (str === '')
            return 0;
        return parseFloat(str.replace(',', '.'));
    } catch (e) {
        return 0;
    }
}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
function ValidationEmail() {
    if ($('#txtEmail').val() === "" || !isEmail($('#txtEmail').val())) {
        toastr.error("Le format de l'adresse email est incorrect.", "Notification", { timeOut: 6000 });
        return false;
    }
    return true;
}
function ValidationSave() {
    if ($('#txtLibelle').val() === "") {
        toastr.error("Veuillez saisir dans libellé de la simulation.", "Notification", { timeOut: 6000 });
        return false;
    }
    return true;
}
function ValidationSaveAs() {
    if ($('#txtLibelleSous').val() === "") {
        toastr.error("Veuillez saisir dans libellé de la simulation.", "Notification", { timeOut: 6000 });
        return false;
    }
    return true;
}

function ShowHide_input_form(element) {
    $(".btnInputForm, .btn-calculate").removeClass("active");
    element.addClass("active");
    $("#div_input_form_1, #divResult").hide();
    var id = element.attr("id");
    if (id === "btnInputForm1") {
        $("#div_input_form_1").show();
    } else if (id === "btnSynthese" || id === "btnCalculate") {
        $("#divResult").show();
    }
}
