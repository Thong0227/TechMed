window.awe = window.awe || {};
awe.init = function () {
    awe.showPopup();
    awe.hidePopup();
};
$(document).ready(function ($) {
    "use strict";
    awe_backtotop();
    awe_category();
});

$(document).on('click', '.overlay, .close-popup, .btn-continue, .fancybox-close', function () {
    hidePopup('.awe-popup');
    setTimeout(function () {
        $('.loading').removeClass('loaded-content');
    }, 500);
    return false;
})
function awe_showLoading(selector) {
    var loading = $('.loader').html();
    $(selector).addClass("loading").append(loading);
} window.awe_showLoading = awe_showLoading;
function awe_hideLoading(selector) {
    $(selector).removeClass("loading");
    $(selector + ' .loading-icon').remove();
} window.awe_hideLoading = awe_hideLoading;
function awe_showPopup(selector) {
    $(selector).addClass('active');
} window.awe_showPopup = awe_showPopup;
function awe_hidePopup(selector) {
    $(selector).removeClass('active');
} window.awe_hidePopup = awe_hidePopup;
awe.hidePopup = function (selector) {
    $(selector).removeClass('active');
}
$(document).on('click', '.overlay, .close-window, .btn-continue, .fancybox-close', function () {
    awe.hidePopup('.awe-popup');
    setTimeout(function () {
        $('.loading').removeClass('loaded-content');
    }, 500);
    return false;
})
var wDWs = $(window).width();


function awe_category() {
    $('.nav-category .fa-plus').click(function (e) {
        $(this).toggleClass('fa-minus fa-plus');
        $(this).parent().toggleClass('active');
    });
    $('.nav-category .fa-minus').click(function (e) {
        $(this).toggleClass('fa-plus');
        $(this).parent().toggleClass('active');
    });
    $('.nav-category .fa-angle-down').click(function (e) {
        $(this).toggleClass('fa-angle-up');
        $(this).parent().toggleClass('active');
    });
} window.awe_category = awe_category;


function awe_backtotop() {
    $(window).scroll(function () {
        $(this).scrollTop() > 200 ? $('.backtop').addClass('show') : $('.backtop').removeClass('show')
    });
    $('.backtop').click(function () {
        return $("body,html").animate({
            scrollTop: 0
        }, 800), !1
    });
} window.awe_backtotop = awe_backtotop;

$('.btn-close').click(function () {
    $(this).parents('.dropdown').toggleClass('open');
});
$(document).on('keydown', '#qty, .number-sidebar', function (e) { -1 !== $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) || /65|67|86|88/.test(e.keyCode) && (!0 === e.ctrlKey || !0 === e.metaKey) || 35 <= e.keyCode && 40 >= e.keyCode || (e.shiftKey || 48 > e.keyCode || 57 < e.keyCode) && (96 > e.keyCode || 105 < e.keyCode) && e.preventDefault() });
$(document).on('click', '.qtyplus', function (e) {
    e.preventDefault();
    fieldName = $(this).attr('data-field');
    var currentVal = parseInt($('input[data-field=' + fieldName + ']').val());
    if (!isNaN(currentVal)) {
        $('input[data-field=' + fieldName + ']').val(currentVal + 1);
    } else {
        $('input[data-field=' + fieldName + ']').val(0);
    }
});
$(document).on('click', '.qtyminus', function (e) {
    e.preventDefault();
    fieldName = $(this).attr('data-field');
    var currentVal = parseInt($('input[data-field=' + fieldName + ']').val());
    if (!isNaN(currentVal) && currentVal > 1) {
        $('input[data-field=' + fieldName + ']').val(currentVal - 1);
    } else {
        $('input[data-field=' + fieldName + ']').val(1);
    }
});

function awe_menumobile() {
    $('#nav .fa').click(function (e) {
        e.preventDefault();
        $(this).parent().parent().toggleClass('open');
    });
} window.awe_menumobile = awe_menumobile;
var ww = $(window).width();
$('.menu-bar').on('click', function () {
    $('.header-menu').addClass('active');
    $('#menu-overlay').addClass('reveal');
});
$('#menu-overlay').on('click', function () {
    $('.header-menu').removeClass('active');
    $(this).removeClass('reveal');
});
if (ww < 768) {
    $('.tab-link span').click(function () {
        var title_tabs = $(this).text();
        $('.button_show_tab').text(title_tabs);
    })
}
/*Double click go to link menu*/
; (function ($, window, document, undefined) {
    $.fn.doubleTapToGo = function (params) {
        if (!('ontouchstart' in window) &&
            !navigator.msMaxTouchPoints &&
            !navigator.userAgent.toLowerCase().match(/windows phone os 7/i)) return false;

        this.each(function () {
            var curItem = false;

            $(this).on('click', function (e) {
                var item = $(this);
                if (item[0] != curItem[0]) {
                    e.preventDefault();
                    curItem = item;
                }
            });

            $(document).on('click touchstart MSPointerDown', function (e) {
                var resetItem = true,
                    parents = $(e.target).parents();

                for (var i = 0; i < parents.length; i++)
                    if (parents[i] == curItem[0])
                        resetItem = false;

                if (resetItem)
                    curItem = false;
            });
        });
        return this;
    };
})(jQuery, window, document);
var wDWs = $(window).width();
if (wDWs < 1199) {
    $('.ul_menu li:has(ul)').doubleTapToGo();
    $('.item_big li:has(ul)').doubleTapToGo();
}
$(document).ready(function () {
    /*fix menu sub*/
    jQuery(".item_big li li").mouseover(function () {
        if (jQuery(window).width() >= 740) {
            jQuery(this).children('ul').css({ top: 0, left: "-200px" });
            var offset = jQuery(this).offset();
            if (offset && (jQuery(window).width() < offset.left + 300)) {
                jQuery(this).children('ul').removeClass("right-sub");
                jQuery(this).children('ul').addClass("left-sub");
                jQuery(this).children('ul').css({ top: 0, left: "-158px" });
            } else {
                jQuery(this).children('ul').removeClass("left-sub");
                jQuery(this).children('ul').addClass("right-sub");
            }
            jQuery(this).children('ul').fadeIn(100);
        }
    }).mouseleave(function () {
        if (jQuery(window).width() >= 740) {
            jQuery(this).children('ul').fadeOut(100);
        }
    });
});
$(".opacity_menu").click(function () {
    $('.opacity_menu').removeClass('open_opacity');
});

$('a[data-toggle="collapse"]').click(function (e) {
    if ($(window).width() >= 767) {
        e.preventDefault();
        e.stopPropagation();
    }
});

//$('.section_counter .counter').counterUp({
//	delay: 10,
//	time: 1000
//});

$(window).scroll(function () {
    if ($(this).scrollTop() > 130 && !$('.mid-header').hasClass('fixed')) {
        $('.mid-header').addClass('fixed');
    } else if ($(this).scrollTop() <= 130) {
        $('.mid-header').removeClass('fixed');
    }
});


$(document).ready(function ($) {
    
    var swiper = new Swiper('.home-slider', {
        slidesPerView: 1,
        autoplay: {
            delay: 2000,
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        }
    });

    const swipers1 = new Swiper('.s_img1', {
        slidesPerView: 1,
        spaceBetween: 10,
        autoplay: true,
        loop: true,
        breakpoints: {
            320: {
                slidesPerView: 2,
                spaceBetween: 20
            },
            480: {
                slidesPerView: 3,
                spaceBetween: 30
            },
            640: {
                slidesPerView: 3,
                spaceBetween: 40
            },
            768: {
                slidesPerView: 3,
                spaceBetween: 30
            },
            992: {
                slidesPerView: 3,
                spaceBetween: 30
            },
            1200: {
                slidesPerView: 5,
                spaceBetween: 30
            }
        }
    })

    var swiper = new Swiper('.home-slider-text', {
        autoplay: {
            delay: 2000,
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        }
    });

    var swiper_2 = new Swiper('.control_slide', {
        slidesPerView: 4,
        spaceBetween: 20,
        loop: false,
        freeMode: true,
        watchSlidesProgress: true,
        breakpoints: {
            300: {
                slidesPerView: 2,
                spaceBetween: 20,
            },
            500: {
                slidesPerView: 2,
                spaceBetween: 20,
            },
            640: {
                slidesPerView: 3,
                spaceBetween: 20,
            },
            768: {
                slidesPerView: 3,
                spaceBetween: 20,
            },
            991: {
                slidesPerView: 4,
                spaceBetween: 30,
            },
            1200: {
                slidesPerView: 4,
                spaceBetween: 30,
            }
        }
    });
    var swiper_1 = new Swiper('.home-slider-control', {
        autoplay: true,
        lazy: true,
        hashNavigation: true,
        pagination: {
            el: '.home-slider-control .swiper-pagination',
            clickable: true,
        },
        thumbs: {
            swiper: swiper_2,
        }
    });

    const swipers2 = new Swiper('.s_img2', {
        slidesPerView: 1,
        spaceBetween: 10,
        autoplay: true,
        loop: true,
        breakpoints: {
            320: {
                slidesPerView: 2,
                spaceBetween: 20
            },
            480: {
                slidesPerView: 3,
                spaceBetween: 30
            },
            640: {
                slidesPerView: 3,
                spaceBetween: 40
            },
            768: {
                slidesPerView: 3,
                spaceBetween: 30
            },
            992: {
                slidesPerView: 3,
                spaceBetween: 30
            },
            1200: {
                slidesPerView: 5,
                spaceBetween: 30
            }
        }
    })

    var swiper = new Swiper('.home-slider2', {
        autoplay: {
            delay: 2000,
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        }
    });

    var swiper = new Swiper('.home-slider3', {
        slidesPerView: 1,
        loopedSlides: 1,
        autoplay: {
            delay: 2000,
        },
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        }
    });


    $(".table-fill").wrap("<div class='table-responsive'></div>");

    function activeTab(obj) {
        $('.tab-ul ul li').removeClass('active');
        $(obj).addClass('active');
        var id = $(obj).attr('data-tab');
        $('.tab-content').removeClass('active');
        $(id).addClass('active');
    }


    $('.tab-ul ul li').click(function () {
        console.log(123);
        activeTab(this);
        return false;
    });

    $('.item_big .icon-subnav').on('click', function () {
        $(this).next().slideToggle();
        $(this).toggleClass('active_menu_suv');
    });

});


(function (e) {
    "use strict";
    e.fn.counterUp = function (t) {
        var n = e.extend({
            time: 400,
            delay: 10
        }, t);
        return this.each(function () {
            var t = e(this)
                , r = n
                , i = function () {
                    var e = []
                        , n = r.time / r.delay
                        , i = t.text()
                        , s = /[0-9]+,[0-9]+/.test(i);
                    i = i.replace(/,/g, "");
                    var o = /^[0-9]+$/.test(i)
                        , u = /^[0-9]+\.[0-9]+$/.test(i)
                        , a = u ? (i.split(".")[1] || []).length : 0;
                    for (var f = n; f >= 1; f--) {
                        var l = parseInt(i / n * f);
                        u && (l = parseFloat(i / n * f).toFixed(a));
                        if (s)
                            while (/(\d+)(\d{3})/.test(l.toString()))
                                l = l.toString().replace(/(\d+)(\d{3})/, "$1,$2");
                        e.unshift(l)
                    }
                    t.data("counterup-nums", e);
                    t.text("0");
                    var c = function () {
                        t.text(t.data("counterup-nums").shift());
                        if (t.data("counterup-nums").length)
                            setTimeout(t.data("counterup-func"), r.delay);
                        else {
                            delete t.data("counterup-nums");
                            t.data("counterup-nums", null);
                            t.data("counterup-func", null)
                        }
                    };
                    t.data("counterup-func", c);
                    setTimeout(t.data("counterup-func"), r.delay)
                };
            t.waypoint(i, {
                offset: "100%",
                triggerOnce: !0
            })
        })
    }
}
)(jQuery);