// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function readURL(input, targetId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#'+targetId)
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function highlight(id) {
    $("#" + id).removeClass("text-light");
    $("#" + id).css("color", "rgb(186, 211, 252)");
}

$(document).ready(function () {
    $("nav").css("padding", "0");
})