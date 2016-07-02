function AlertMessage(message)
{
    $(".alertBody").html(message);
    $("#alertModel").modal('show');
}

function ToggleSearch()
{
    $("#advanceSearch").slideToggle("fast");
    if($("#advanceSearch").hasClass("advanceSearch"))
    {
        $("#btnAdvanceSearch").html("Advance search");
    }
    else
    {
        $("#btnAdvanceSearch").html("Close");
    }

}

function ShowLoader()
{
    $("#modalLoader").modal('show');
}


function HideLoader() {
    $("#modalLoader").modal('hide');
}

function ApplyValidation()
{
    var form = $("form");
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}

function ApplyValidations(formName) {
    var form = $("'#"+formName+"'");
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}