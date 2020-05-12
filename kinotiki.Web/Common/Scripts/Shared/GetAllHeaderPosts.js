var rotateOnHover = function(div,imgChildren,deg,time)  {
    $(div).hover(function () {
        $(div).children(imgChildren).css({
            "-webkit-transform": "rotate(" + deg + "deg)",
            "-moz-transform": "rotate(" + deg + "deg)",
            "transform": "rotate(" + deg + "deg)",
            "-webkit-transition": "all " + time + "s ease",
            "-moz-transition": "all " + time + "s ease",
            "-o-transition": "all " + time + "s ease",
            "transition": "all " + time + "s ease"
        });
    },
    function () {
        $(div).children(imgChildren).css({
            "-webkit-transform": "rotate(0deg)",
            "-moz-transform": "rotate(0deg)",
            "transform": "rotate(0deg)",
            "-webkit-transition": "all " + time + "s ease",
            "-moz-transition": "all " + time + "s ease",
            "-o-transition": "all " + time + "s ease",
            "transition": "all " + time + "s ease"
        });
    });
}

$(document).ready(function () {
    rotateOnHover('.pagination-menu-back-page', 'img', '360', '0.5');
    rotateOnHover('.pagination-menu-next-page', 'img', '360', '0.5');
});