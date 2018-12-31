﻿'use strict';

// 2. This code loads the IFrame Player API code asynchronously.
var tag = document.createElement('script');

tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
var player;
function onYouTubeIframeAPIReady() {
    player = new YT.Player('player', {
        height: '200',
        width: '355',
        videoId: 'M7lc1UVf-VE',
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }

    });
}

// 4. The API will call this function when the video player is ready.
function onPlayerReady(event) {
    event.target.playVideo();
}
function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.ENDED) {
        onPlayerEnd();
    }
}

function onPlayerEnd() {
    ToggleMiniButtonsOff();
    SetButtonPlay();
}


function stopVideo() {
    player.stopVideo();
}
function pauseVideo() {
    player.pauseVideo();
}
function playVideo() {
    player.playVideo();
}

function PlayToggleMain() {

    let playButton = document.getElementById("play-button");

    if (playButton.classList.contains('fa-play')) {
        playButton.classList.remove('fa-play');
        playButton.classList.add('fa-pause');
        let playButtons = document.getElementsByClassName("play-button");
        ToggleMiniButtonsOff();
        for (var i = 0; i < playButtons.length; ++i) {
            if (playButtons[i].dataset.youtubeid == player.videoId) {
                ToggleMiniButton(playButtons[i]);
            }
        }
        playVideo();
    }
    else {
        playButton.classList.remove('fa-pause');
        playButton.classList.add('fa-play');
        ToggleMiniButtonsOff();
        pauseVideo();
    }
}

function SetMiniButtonPlay(button) {
    if (button.classList.contains('fa-pause-circle')) {
        button.classList.remove('fa-pause-circle');
        button.classList.add('fa-play-circle');
    }
}
function SetMiniButtonPause(button) {
    if (button.classList.contains('fa-play-circle')) {
        button.classList.remove('fa-play-circle');
        button.classList.add('fa-pause-circle');
    }
}
function SetButtonPlay() {
    let button = document.getElementById("play-button");
    if (button.classList.contains('fa-pause')) {
        button.classList.remove('fa-pause');
        button.classList.add('fa-play');
    }
}
function SetButtonPause() {
    let button = document.getElementById("play-button");
    if (button.classList.contains('fa-play')) {
        button.classList.remove('fa-play');
        button.classList.add('fa-pause');
    }
}

function ToggleMiniButton(button) {

    if (button.classList.contains('fa-pause-circle')) {
        SetMiniButtonPlay(button);
    }
    else {
        SetMiniButtonPause(button);
    }
}

function ToggleMiniButtonsOff(currentButton) {
    // Mini play buttons
    let playButtons = document.getElementsByClassName("play-button");
    for (var i = 0; i < playButtons.length; ++i) {
        SetMiniButtonPlay(playButtons[i]);
    }
}

function PlayToggleMini(event) {
    let playButton = event.target;

    if (player.videoId != playButton.dataset.youtubeid) {
        player.videoId = playButton.dataset.youtubeid;
        player.loadVideoById(playButton.dataset.youtubeid);
        PlayToggleMain();
        SetButtonPause()
        ToggleMiniButtonsOff();
        ToggleMiniButton(playButton);
        return;
    }
    
    ToggleMiniButton(playButton);
    PlayToggleMain();
}

document.addEventListener("DOMContentLoaded", () => {
    let playButton = document.getElementById("play-button");
    playButton.addEventListener("click", PlayToggleMain);

    let playButtons = document.getElementsByClassName("play-button");

    for (var i = 0; i < playButtons.length; i++) {
        playButtons[i].addEventListener("click", PlayToggleMini);
    }
});