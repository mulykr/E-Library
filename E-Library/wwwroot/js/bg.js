function bg(path) {
    $.backstretch(path);
    $(".backstretch").css("filter", "blur(5px)");
    $(".backstretch").css("transform", "scale(1.05)");
}

$(document).ready(function () {
    $("nav").removeClass("bg-info");
    $("nav").css("opacity", 0.9);
    $("nav").css("background-color", "rgba(0, 0, 0, 0.75)");
});