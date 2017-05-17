function validationimput()
{
    $('#codigo').keyup('input', function () {
        var input = $(this);
        var is_name = input.val();
        if (is_name) { input.removeClass("invalid").addClass("valid"); }
        else { input.removeClass("valid").addClass("invalid"); }
        $('btnSave').prop("disabled", true);
    });

    $('#nombre').keyup('input', function () {       
        var input = $(this);
        var is_name = input.val();
        if (is_name) { input.removeClass("invalid").addClass("valid"); }
        else { input.removeClass("valid").addClass("invalid"); }
    });

    $('#apellido').on('input', function () {
        var input = $(this);
        var is_name = input.val();
        if (is_name) { input.removeClass("invalid").addClass("valid"); }
        else { input.removeClass("valid").addClass("invalid"); }
    });

    $('#telefono').on('input', function () {
        var input = $(this);
        var is_name = input.val();
        if (is_name) { input.removeClass("invalid").addClass("valid"); }
        else { input.removeClass("valid").addClass("invalid"); }
    });

    $('#fechaCompra').on('input', function () {
        var input = $(this);
        var is_name = input.val();
        if (is_name) { input.removeClass("invalid").addClass("valid"); }
        else { input.removeClass("valid").addClass("invalid"); }
    });


    $('#mail').on('input', function () {
        var input = $(this);
        var is_name = input.val();
        if (isValidEmailAddress(is_name)) { input.removeClass("invalid").addClass("valid"); }
        else { input.removeClass("valid").addClass("invalid"); }
    });
    

}
    

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
}

function SaveValidation() {

    if ($('#codigo').val('')) {
        var is_name = $('#codigo').val();
        if (is_name) { $('#codigo').removeClass("invalid").addClass("valid"); }
        else { $('#codigo').removeClass("valid").addClass("invalid"); }
        $('#btnSave').prop("disabled", true);
    }
    if ($('#nombre').val('')) {
        var is_name = $('#codigo').val();
        if (is_name) { $('#codigo').removeClass("invalid").addClass("valid"); }
        else { $('#codigo').removeClass("valid").addClass("invalid"); }
        $('#btnSave').prop("disabled", true);
    }
    if ($('#apellido').val('')) {
        var is_name = $('#codigo').val();
        if (is_name) { $('#codigo').removeClass("invalid").addClass("valid"); }
        else { $('#codigo').removeClass("valid").addClass("invalid"); }
        $('#btnSave').prop("disabled", true);
    }

    if ($('#telefono').val('')) {
        var is_name = $('#codigo').val();
        if (is_name) { $('#codigo').removeClass("invalid").addClass("valid"); }
        else { $('#codigo').removeClass("valid").addClass("invalid"); }
        $('#btnSave').prop("disabled", true);
    }


}
       




   


