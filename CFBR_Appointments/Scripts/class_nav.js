var class_nav = (function () {
    var current_day,
        prev_button = $('#available_class_nav .prev'),
        next_button = $('#available_class_nav .next');
        // userAgent = navigator.userAgent.toLowerCase(),
        // isIphone = (userAgent.indexOf('iphone') != -1) ? true : false;

    // var click_event = (isIphone) ? 'tap' : 'click';

    function init() {
        $('.available-day').not(':first').hide();
        current_day = $('.available-day').first();
        update_nav_controls();
    }

    function has_prev() {
        return current_day.prev('.available-day').length > 0;
    }

    function has_next() {
        return current_day.next('.available-day').length > 0;
    }

    function update_nav_controls() {
        if (!has_prev()) {
            prev_button.addClass('disabled');
        } else {
            prev_button.removeClass('disabled');
        }

        if (!has_next()) {
            next_button.addClass('disabled');
        } else {
            next_button.removeClass('disabled');
        }
    }

    function prev(el) {
        if (el.tagName.toUpperCase() == "I") {
            el = $(el).parent();
        } else {
            el = $(el);
        }
            
        if (el.hasClass('disabled'))
            return;

        $('.available-day').hide();
        current_day = current_day.prev('.available-day');
        current_day.show();
        update_nav_controls();
    }

    function next(el) {
        if (el.tagName.toUpperCase() == "I") {
            el = $(el).parent();
        } else {
            el = $(el);
        }

        if (el.hasClass('disabled'))
            return;

        $('.available-day').hide();
        current_day = current_day.next('.available-day');
        current_day.show();
        update_nav_controls();
    }

    return {
        init: init,
        prev: prev,
        next: next
    };
})();