'use strict';

function CardHover(card) {
    card.classList.add('shadow-lg');
}
function CardExit(card) {
    card.classList.remove('shadow-lg');
}

function FindCardBase(card) {

    if (!card.classList.contains('card-click'))
        return FindCardBase(card.parentNode);

    return card;
}

function onMouseOver() {
    CardHover(FindCardBase(event.target));
}
function onMouseOut() {
    CardExit(FindCardBase(event.target));
}


function onClick() {
    let newEvent = new MouseEvent('click');
    let button = event.target.querySelector("#main-card-button");
    button.dispatchEvent(newEvent);
}

document.addEventListener("DOMContentLoaded", () => {
    let cards = document.getElementsByClassName("card-click");
    for (var i = 0; i < cards.length; ++i) {

        cards[i].addEventListener("mouseover", onMouseOver, false);
        cards[i].addEventListener("mouseleave", onMouseOut);
        cards[i].addEventListener("click", onClick);
    }

});
