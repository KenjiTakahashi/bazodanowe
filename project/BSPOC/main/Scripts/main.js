$(document).ready(function () {
    $("INPUT.auto-hint").focus(function () {
        if ($(this).val() == $(this).attr("name")) {
            $(this).val("");
            $(this).removeClass("auto-hint");
        }
    });
    $("INPUT.auto-hint").blur(function () {
        if ($(this).val() == "" && $(this).attr("name") != "") {
            $(this).val($(this).attr("name"));
            $(this).addClass("auto-hint");
        }
    });
    $("INPUT.auto-hint").each(function () {
        if ($(this).attr("name") == "") {
            return;
        }
        if ($(this).val() == "") {
            $(this).val($(this).attr("name"));
        } else {
            $(this).removeClass("auto-hint");
        }
    });
});