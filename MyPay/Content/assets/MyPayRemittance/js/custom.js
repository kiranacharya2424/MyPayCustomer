$(document).ready(function() {

    $('.ui.selection.dropdown').dropdown();
    $('.ui.inline.dropdown').dropdown();
    $('.primarypopup').popup();

    $('.popuplink').popup({
        popup: $('.popup'),
        on: 'click'
    });
    $('.needhelp').popup({
        popup: $('.custom.popup'),
        on: 'click'
    });


    $('.toggle-calc>a').click(function() {
        $(this).text(function(i, v) {
            return v === 'See calculation' ? 'Hide calculation' : 'See calculation'
        });
        $(".calculation-values-wrap").toggleClass("expanddiv");
    });





});

$(document).ready(function() {
    var e = 0,
        i = $(".animate-map-points").length;
    ! function a() {
        e = 0, $(".animate-map-points").each(function() {
            var t = $(this);
            setTimeout(function() {
                t.removeClass("map-points-animated"), t.offsetWidth, setTimeout(function() {
                    t.addClass("map-points-animated"), t.attr("data-label-path-index") && setTimeout(function() {
                        ! function t(a, e) {
                            var i, n = $(a)[0],
                                s = n.getTotalLength(),
                                o = $(a).attr("data-multiplier") ? $(a).attr("data-multiplier") : 1,
                                l = $(a + "-dashed")[0];
                            l.style.opacity = 1, n.style.display = "block", n.style.opacity = "1", n.style.transition = n.style.WebkitTransition = "none", n.style.strokeDasharray = s + " " + s, n.style.strokeDashoffset = o * s, n.getBoundingClientRect(), n.style.transition = n.style.WebkitTransition = "stroke-dashoffset .7s ease-in-out, opacity 1s ease-in-out", n.style.strokeDashoffset = "0", $(a).attr("data-next-path") ? (i = $(a).attr("data-next-path"), setTimeout(function() { t(i), setTimeout(function() { $(e).addClass("active") }, 700) }, $(a).attr("data-next-path-delay"))) : setTimeout(function() { $(e).addClass("active") }, 700);
                            var d = $(a).attr("data-next-path-delay") ? $(a).attr("data-next-path-delay") : 0,
                                d = parseInt(d) + 2300;
                            setTimeout(function() { n.style.opacity = 0, $(e).removeClass("active"), setTimeout(function() { l.style.opacity = 0 }, 500) }, d)
                        }("#path-" + t.attr("data-label-path-index"), "#text-" + t.attr("data-label-path-index"))
                    }, 600)
                }, 100), t.index() === i && a()
            }, e), e += 1100
        })
    }()
});