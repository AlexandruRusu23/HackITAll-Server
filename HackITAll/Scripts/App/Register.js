
$(document).ready(function (){
    $("#myHidden").hide();
})

$('#ShowRegisterPage').on('click', function (ev) {
    ev.preventDefault();
    $("#myHidden").slideToggle('slow');

    if ($("#myHidden").is(':visible')) {
        $("html, body").animate({ scrollTop: $("#myHidden").offset().top });
    }
});