﻿function GoingNotGoing() {
    document.getElementById('btnSuccess').addEventListener('click', Going);

    document.getElementById('btn-danger').addEventListener('click', NotGoing);

    document.getElementById('displayComments').addEventListener('click', ShowComments)

    var smth = document.querySelectorAll('li i').forEach(x => x.addEventListener('click', Vote))



}
function ClearComments() {
    var element = document.getElementsByName('comentaryDivContainer')[0];
    element.parentNode.removeChild(element);
    var commentsBtn = document.getElementById('displayComments');
    commentsBtn.textContent = 'See comments';
    commentsBtn.removeEventListener('click', ClearComments);
    commentsBtn.addEventListener('click', ShowComments);

}

function ShowComments() {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', "/Comentary/AllComentaries", true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Response
            var responseText = JSON.parse(this.responseText);
            CreateHtml(responseText)
        }

    };
    var currEvent = document.querySelector('body');
    var id = currEvent.id;
    var data = { eventId: id };
    xhttp.send(JSON.stringify(data));

}

function CreateHtml(comentaries) {
    var body = document.getElementsByTagName('BODY')[0];
    var divContainer = document.createElement('div');
    divContainer.setAttribute('name', 'comentaryDivContainer')
    divContainer.setAttribute('class', 'container');
    body.appendChild(divContainer);

    for (const commentary of comentaries) {

        dialogBoxDiv = document.createElement('div');
        dialogBoxDiv.setAttribute('class', 'dialogbox');
        comentarySpan = document.createElement('span');
        comentaryA = document.createElement('a');
        comentaryA.textContent = commentary.userName;
        comentarySpan.appendChild(comentaryA);
        dialogBoxDiv.appendChild(comentarySpan);


        bodyDiv = document.createElement('div');
        bodyDiv.setAttribute('class', 'body');
        bodySpan = document.createElement('span');
        bodySpan.setAttribute('class', 'tip tip-up')
        bodyDiv.appendChild(bodySpan);

        messageDiv = document.createElement('div');
        messageDiv.setAttribute('class', 'message');
        messageSpan = document.createElement('span');
        messageDiv.appendChild(messageSpan);
        var messageP = document.createElement('p');
        messageP.setAttribute('class', 'text-break');
        messageP.textContent = commentary.content;
        messageSpan.appendChild(messageP);


        var likeComentaryButton = document.createElement('button');
        var DislikeComentaryButton = document.createElement('button');
        likeComentaryButton.setAttribute('id', `${commentary.comentaryId}`);
        likeComentaryButton.name = "LikeComentary";
        likeComentaryButton.setAttribute('class', 'btn-success');
        likeComentaryButton.setAttribute('value', `${commentary.comentaryId}`);
        likeComentaryButton.textContent = `Like ${commentary.likesCount}`;
        likeComentaryButton.type = 'submit';

        DislikeComentaryButton.id = commentary.comentaryId;
        DislikeComentaryButton.name = "DislikeComentary";
        DislikeComentaryButton.setAttribute('class', 'btn-danger');
        DislikeComentaryButton.setAttribute('value', `${commentary.comentaryId}`);
        DislikeComentaryButton.textContent = `Dislike ${commentary.dislikesCount}`;
        DislikeComentaryButton.type = 'submit';

        messageSpan.appendChild(likeComentaryButton);
        messageSpan.appendChild(DislikeComentaryButton);
        bodyDiv.appendChild(messageDiv);
        dialogBoxDiv.appendChild(bodyDiv);
        divContainer.appendChild(dialogBoxDiv);

        if (commentary.replies.length > 0) {
            for (let reply of commentary.replies) {
                replyDialogBoxDiv = document.createElement('div');
                replyDialogBoxDiv.setAttribute('class', 'replyDialogbox');
                replyComentarySpan = document.createElement('span');
                replyComentaryA = document.createElement('a');
                replyComentaryA.textContent = reply.userName;
                replyComentarySpan.appendChild(replyComentaryA);
                replyDialogBoxDiv.appendChild(replyComentarySpan);


                replyBodyDiv = document.createElement('div');
                replyBodyDiv.setAttribute('class', 'replyBody');
                replyBodySpan = document.createElement('span');
                replyBodySpan.setAttribute('class', 'tip tip-up')
                replyBodyDiv.appendChild(replyBodySpan);

                replyMessageDiv = document.createElement('div');
                replyMessageDiv.setAttribute('class', 'message');
                replyMessageSpan = document.createElement('span');
                replyMessageDiv.appendChild(replyMessageSpan);
                var replyMessageP = document.createElement('p');
                replyMessageP.setAttribute('class', 'text-break');
                replyMessageP.textContent = `${reply.content} + I AM REPLLYYYY`;
                replyMessageSpan.appendChild(replyMessageP);


                var likeReplyButton = document.createElement('button');

                likeReplyButton.setAttribute('name', 'LikeReply');
                likeReplyButton.setAttribute('type', 'submit');
                likeReplyButton.setAttribute('class', 'btn-success');
                likeReplyButton.value = reply.replyId;
                likeReplyButton.textContent = `Like ${reply.replyLikesCount}`

                var dislikeReplyButton = document.createElement('button');
                dislikeReplyButton.setAttribute('name', 'DislikeReply');
                dislikeReplyButton.setAttribute('type', 'submit');
                dislikeReplyButton.setAttribute('class', 'btn-danger');
                dislikeReplyButton.value = reply.replyId;
                dislikeReplyButton.textContent = `Dislike ${reply.replyDislikesCount}`

                replyMessageSpan.appendChild(likeReplyButton);
                replyMessageSpan.appendChild(dislikeReplyButton);

                replyBodyDiv.appendChild(replyMessageDiv);
                replyDialogBoxDiv.appendChild(replyBodyDiv);
                divContainer.appendChild(replyDialogBoxDiv);
            }
        }


    }
    var commentaryBtn = document.getElementById('displayComments');
    commentaryBtn.textContent = 'Hide Commentary';
    commentaryBtn.removeEventListener('click', ShowComments)
    commentaryBtn.addEventListener('click', ClearComments)

    let arrToPrint = [...document.querySelectorAll('button')]
        .filter(x => x.name == 'LikeComentary' || x.name == 'DislikeComentary' ||
            x.name == 'LikeReply' || x.name == 'DislikeReply');

    for (const button of arrToPrint) {
        button.addEventListener('click', LikeDislikeComentary);
    }
}

function Going() {


    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', "/Event/GoingToEvent", true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Response
            var responseText = JSON.parse(this.responseText);
            var goingUsersCount = responseText.goingUsersCount;
            var btn = document.getElementById("goingButton");

            btn.textContent = `Going ${goingUsersCount}`;
            btn.style.backgroundColor = "Green";

            var notGoingUsersCount = responseText.notGoingUsersCount;
            var notGoingButton = document.getElementById("notGoingButton");

            notGoingButton.textContent = `Not Going ${notGoingUsersCount}`;
            notGoingButton.style.backgroundColor = "Red";

        }
    };
    var currEvent = document.querySelector('body');
    var id = currEvent.id;
    var data = { eventId: id };
    xhttp.send(JSON.stringify(data));
}
function NotGoing() {


    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', "/Event/NotGoingToEvent", true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Response
            var responseText = JSON.parse(this.responseText);
            var goingUsersCount = responseText.goingUsersCount;
            var btn = document.getElementById("goingButton");

            btn.textContent = `Going ${goingUsersCount}`;
            btn.style.backgroundColor = "Green";

            var notGoing = responseText.notGoingUsersCount;
            var notGoingButton = document.getElementById("notGoingButton");

            notGoingButton.textContent = `Not Going ${notGoing}`;
            notGoingButton.style.backgroundColor = "Red";

        }
    };
    var currEvent = document.querySelector('body');
    var id = currEvent.id;
    var data = { eventId: id };
    xhttp.send(JSON.stringify(data));
}
function Vote(ev) {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', "/api/Votes", true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Response
            var responseText = JSON.parse(this.responseText);
            var grade = responseText.averageVoteValue;
            document.getElementById('averageVoteGrade').textContent = `${grade} / 5`;
        }
    };
    var currEvent = document.querySelector('body');
    var currEventId = currEvent.id;
    var gradeId = ev.target.parentNode.id;
    var data = { eventId: currEventId, grade: gradeId };
    xhttp.send(JSON.stringify(data));
}

function LikeDislikeComentary(ev) {
    var clickedBtn = ev.target;
    if (clickedBtn.name == 'LikeComentary') {
        ReturnComentaryLikesAndDislikes(clickedBtn.id, 'Comentary', 'LikeComentary');
    }
    if (clickedBtn.name == 'DislikeComentary') {
        ReturnComentaryLikesAndDislikes(clickedBtn.id, 'Comentary', 'DislikeComentary');
    }
    if (clickedBtn.name == 'LikeReply') {
        ReturnComentaryLikesAndDislikes(clickedBtn.value, 'Reply', 'LikeReply');
    }
    if (clickedBtn.name == 'DislikeReply') {
        ReturnComentaryLikesAndDislikes(clickedBtn.value, 'Reply', 'DislikeReply');
    }

}


function ReturnComentaryLikesAndDislikes(btnId, controller, action) {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', `/${controller}/${action}`, true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Response
            var responseText = JSON.parse(this.responseText);
            var tr;
            if (controller == 'Comentary') {

                var smth = [...document.getElementsByName('LikeComentary')].filter(x => x.id == btnId);
                var tr = smth[0].parentNode.parentNode;
            } else {
                var smth = [...document.getElementsByName('LikeReply')].filter(x => x.value == btnId);
                var tr = smth[0].parentNode.parentNode;
            }
            var currentLikeButton = tr.children[0].children[1];
            var currentDislikeButton = tr.children[0].children[2];

            currentLikeButton.textContent = `Like ${responseText.comentaryLikeCount}`;
            currentDislikeButton.textContent = `Dislike ${responseText.comentaryDislikeCount}`;
        }
    };
    if (controller == 'Reply') {
        var data = { replyId: btnId };
    }
    else {
        var data = { comentaryId: btnId };
    }

    xhttp.send(JSON.stringify(data));
}
