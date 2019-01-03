'use strict';

function CardHover(card) {
    card.addClass('shadow-lg');
}
function CardExit(card) {
    card.removeClass('shadow-lg');
}
function onMouseOver() {
    CardHover($(this));
}
function onMouseOut() {
    CardExit($(this));
}

$('.card-click').mouseover(onMouseOver);
$('.card-click').mouseout(onMouseOut);


$('.favorite-card-button').click(function () {
    if ($(this).hasClass('far')) {
        $(this).removeClass('far');
        $(this).addClass('fas');
    }
    else {
        $(this).removeClass('fas');
        $(this).addClass('far');
    }
});
