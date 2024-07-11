
$(document).ready(function ($) {
    let isAnimated = false;

    $(window).scroll(function () {
        const windowHeight = $(window).height();
        const scrollTop = $(window).scrollTop();

        if (scrollTop + windowHeight > $("#counters-section").offset().top && !isAnimated) {
            $('.counters .item .counter').each(function () {
                const startValue = parseInt($(this).text());
                const endValue = parseInt($(this).attr('data-animate-value'));
                animateValue(this.id, startValue, endValue, 3000);
            });
            isAnimated = true;
        }
    });
    function animateValue(id, start, end, duration) {
        let startTime = null;
        const elem = $('#' + id);

        const step = (timestamp) => {
            if (!startTime) {
                startTime = timestamp;
            }
            if (isNaN(start)) {
                start = 0
            }
            const progress = Math.min((timestamp - startTime) / duration, 1);

            const value = Math.floor(progress * (end - start) + start);
            elem.text(value.toLocaleString());


            if (progress < 1) {
                window.requestAnimationFrame(step);
            }
        };

        window.requestAnimationFrame(step);
    }
})