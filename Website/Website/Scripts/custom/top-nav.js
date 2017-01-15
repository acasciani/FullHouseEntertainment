$(document).ready(function () {
    $('.navbar.top').affix({
        offset: {
            top: function () {
                return $('.body').offset().top;
            }
        }
    });


});