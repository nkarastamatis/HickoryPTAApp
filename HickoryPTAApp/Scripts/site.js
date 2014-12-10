$(document).ready(function () {
    $('.carousel').carousel({
        interval: 3000
    });

    setActive();
    

    $('.carousel').carousel('cycle');

    
});

$('.carousel').on('slide.bs.carousel', function () {
    var carouselData = $(this).data('bs.carousel');
    localStorage['carouselIndex'] = carouselData.getActiveIndex() + 2;
});

function setActive()
{
    if (localStorage['carouselIndex'] != null) {
        var index = parseInt(localStorage['carouselIndex']);
        if (index == NaN)
            return;
        
        if ($('.carousel').find('.item').length < index)
            return;

        if (index >= 2) {

            var indicators = $('.carousel').find('.carousel-indicators');
            var active = $('.carousel').find('.item.active');

            if (active != null) {
                active.removeClass('active');
                $('.carousel-inner .item:nth-child(' + index + ')').addClass('active');
                $('.carousel-indicators li:nth-child(' + index + ')').addClass('active');
                $('.carousel-indicators').addClass('index'+index);
            }
        }
    }
}