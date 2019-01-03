'use strict';

function CardHover(card) {
    card.addClass('shadow-lg');
    card.removeClass('shadow-sm');
}
function CardExit(card) {
    card.addClass('shadow-sm');
    card.removeClass('shadow-lg');
}
function onMouseOver() {
    CardHover($(this));
}
function onMouseOut() {
    CardExit($(this));
}
function onMouseDown() {
    $(this).addClass('bg-light');
}
function onMouseUp() {
    $(this).removeClass('bg-light');
}

$('.card-click').mouseover(onMouseOver);
$('.card-click').mouseout(onMouseOut);
$('.card-click').mousedown(onMouseDown);
$('.card-click').mouseup(onMouseUp);


$('.favorite-card-button').click(function () {
    if ($(this).hasClass('far')) {
        $(this).removeClass('far');
        $(this).addClass('fas');
        $(this.nextElementSibling).trigger('click');
    }
    else {
        $(this).removeClass('fas');
        $(this).addClass('far');
    }
});

