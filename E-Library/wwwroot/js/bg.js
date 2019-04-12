function bg(path) {
    $.backstretch(path);
    $(".backstretch").css("filter", "blur(5px)");
    $(".backstretch").css("transform", "scale(1.05)");
}

$(document).ready(function () {
    $("nav").css("opacity", 0.9);
});