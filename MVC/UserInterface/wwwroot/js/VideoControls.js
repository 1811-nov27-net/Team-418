'use strict';

// 2. This code loads the IFrame Player API code asynchronously.
var tag = document.createElement('script');

tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
var player;
var set;
function onYouTubeIframeAPIReady() {
    if (set)
        return;
    set = true;
    player = new YT.Player('player', {
        height: '200',
        width: '200',
        //videoId: 'M7lc1UVf-VE',
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }


    });
}

// 4. The API will call this function when the video player is ready.
function onPlayerReady(event) {
    playVideo();
}
function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.ENDED) {
        onPlayerEnd();
    }
}

//function onPlayerEnd() {
//    ToggleMiniButtonsOff();
//    SetButtonPlay();
//}


//function stopVideo() {
//    player.stopVideo();
//    SetButtonPlay();
//}
//function pauseVideo() {
//    player.pauseVideo();
//    SetButtonPlay();
//}
//function playVideo() {
//    player.playVideo();
//    SetButtonPause();
//}

//function PlayToggleMain() {

//    let playButton = document.getElementById("play-button");

//    if (playButton.classList.contains('fa-play')) {
//        playButton.classList.remove('fa-play');
//        playButton.classList.add('fa-pause');
//        let playButtons = document.getElementsByClassName("play-button");
//        ToggleMiniButtonsOff();
//        for (var i = 0; i < playButtons.length; ++i) {
//            if (playButtons[i].dataset.youtubeid == player.videoId) {
//                ToggleMiniButton(playButtons[i]);
//            }
//        }
//        playVideo();
//    }
//    else {
//        playButton.classList.remove('fa-pause');
//        playButton.classList.add('fa-play');
//        ToggleMiniButtonsOff();
//        pauseVideo();
//    }
//}

//function SetMiniButtonPlay(button) {
//    if (button.classList.contains('fa-pause-circle')) {
//        button.classList.remove('fa-pause-circle');
//        button.classList.add('fa-play-circle');
//    }
//}
//function SetMiniButtonPause(button) {
//    if (button.classList.contains('fa-play-circle')) {
//        button.classList.remove('fa-play-circle');
//        button.classList.add('fa-pause-circle');
//    }
//}
//function SetButtonPlay() {
//    let button = document.getElementById("play-button");
//    if (button.classList.contains('fa-pause')) {
//        button.classList.remove('fa-pause');
//        button.classList.add('fa-play');
//    }
//}
//function SetButtonPause() {
//    let button = document.getElementById("play-button");
//    if (button.classList.contains('fa-play')) {
//        button.classList.remove('fa-play');
//        button.classList.add('fa-pause');
//    }
//}

//function ToggleMiniButton(button) {

//    if (button.classList.contains('fa-pause-circle')) {
//        SetMiniButtonPlay(button);
//    }
//    else {
//        SetMiniButtonPause(button);
//    }
//}

//function ToggleMiniButtonsOff(currentButton) {
//    // Mini play buttons
//    let playButtons = document.getElementsByClassName("play-button");
//    for (var i = 0; i < playButtons.length; ++i) {
//        SetMiniButtonPlay(playButtons[i]);
//    }
//}

//function PlayToggleMini(event) {
//    let playButton = event.target;
    
//    if (player.videoId != playButton.dataset.youtubeid) {
//        player.videoId = playButton.dataset.youtubeid;
//        player.loadVideoById(playButton.dataset.youtubeid);
//        //ToggleMiniButtonsOff();
//        PlayToggleMain();
//        SetButtonPause()
//        SetMiniButtonPause(playButton);
//        return;
//    }
    
//    ToggleMiniButton(playButton);
//    PlayToggleMain();
//}

//document.addEventListener("DOMContentLoaded", () => {
//    let playButton = document.getElementById("play-button");
//    playButton.addEventListener("click", PlayToggleMain);

//    let playButtons = document.getElementsByClassName("play-button");

//    for (var i = 0; i < playButtons.length; i++) {
//        playButtons[i].addEventListener("click", PlayToggleMini);
//    }
//});

function playVideo(youtubeid) {
    if (player.getVideoData()['video_id'] == youtubeid) {
        player.playVideo();
    }
    else {
        player.loadVideoById(youtubeid);
        player.playVideo();   
    }
}
function pauseVideo(youtubeid){
    if (player.getVideoData()['video_id'] == youtubeid) {
        player.pauseVideo();
    }
}

let isPlaying = false;
$('.card-click').click(function () {
    if (!isPlaying) {
        playVideo($(this).data('youtubeid'));
        isPlaying = true;
    }
    else {
        isPlaying = false;
        pauseVideo($(this).data('youtubeid'));
    }
});

$('.play-button').click(function () {
    if ($(this).hasClass('far')) {
        $(this).removeClass('far');
        $(this).addClass('fas');
    }
    else {
        $(this).removeClass('fas');
        $(this).addClass('far');
    }

});