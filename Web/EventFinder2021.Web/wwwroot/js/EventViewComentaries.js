import {CallLastComment,serverLocation} from "./EventView.js"
function CreateReplyForm(ev) {
    var comentaryId = ev.target.parentNode.children[1].id;
    var div = ev.target.parentNode.parentNode;
    var commentFormDiv = document.createElement('div');
    commentFormDiv.setAttribute('class', 'reply-form-form-area');

    var commentRespondDiv = document.createElement('div');
    commentRespondDiv.setAttribute('class', 'comment-respond');
    commentRespondDiv.setAttribute('id', 'respond');
    var h3 = document.createElement('h3');
    h3.textContent = 'Leave reply here.';
    commentFormDiv.appendChild(commentRespondDiv);
    commentRespondDiv.appendChild(h3);


    var commentP = document.createElement('p');
    commentP.setAttribute('class', 'comment-form-comment');

    var label = document.createElement('label');
    label.textContent = 'Reply';
    commentP.appendChild(label);
    commentRespondDiv.appendChild(commentP)

    var commentTextArea = document.createElement('textarea');
    commentTextArea.setAttribute('id', 'reply');
    commentTextArea.setAttribute('name', 'reply');
    commentTextArea.setAttribute('cols', '450')
    commentTextArea.setAttribute('rows', '8')
    commentTextArea.setAttribute('maxlength', '65525')
    commentTextArea.setAttribute('required', 'required')
    commentRespondDiv.appendChild(commentTextArea)
    div.appendChild(commentFormDiv);
    var btn = document.createElement('button');
    btn.type = 'submit';
    btn.setAttribute('id', 'submitReply')
    btn.textContent = 'Send';
    commentRespondDiv.appendChild(btn)
    var a = document.getElementById('replyA');
    a.parentNode.removeChild(a);
    document.getElementById('submitReply').addEventListener('click', SendReply)
}

async function SendReply(ev) {
    var comentaryId = ev.target.parentNode.parentNode.parentNode.children[0].children[1].id;
    var replyContent = document.getElementById('reply').value;
    var eventId = document.getElementsByTagName('BODY')[0].id;
    var data = { eventId: Number(eventId), comentaryId: Number(comentaryId), content: replyContent };
    const response = await fetch(serverLocation+"/Reply/WriteReply" , {
        method:'post',
        headers: {"Content-Type":"application/json"},
        body: JSON.stringify(data)
    })
    if (response.ok){
        var divToRemove = document.getElementsByClassName('reply-form-form-area')[0];
        var parent = divToRemove.parentNode.children[0];
        divToRemove.parentElement.removeChild(divToRemove);
    
        var a = document.createElement('a');
        a.addEventListener('click', CreateReplyForm);
        a.setAttribute('id', 'replyA');
        a.textContent = "Reply";
        parent.appendChild(a);
    } else {
        var error = response.json();
        alert(error.message)
    }

}

function SendComment() {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', "/Comentary/WriteComentary", true);
    xhttp.setRequestHeader("Content-Type", "application/json");

    var currEventId = document.getElementById('currEventId').value
    var commentContent = document.getElementById('comment').value
    var data = { eventId: Number(currEventId), content: commentContent };
    xhttp.send(JSON.stringify(data));
    CallLastComment();


    var divToRemove = document.getElementsByClassName('comment-form-form-area')[0];
    var parent = divToRemove.parentNode.children[0];
    divToRemove.parentElement.removeChild(divToRemove);



}

export function ShowComments() {
    var commentaryBtn = document.getElementById('displayComments');
    if (commentaryBtn.textContent == 'See comments') {
        var xhttp = new XMLHttpRequest();
        xhttp.open('POST', "/Comentary/AllComentaries", true);
        xhttp.setRequestHeader("Content-Type", "application/json");
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                // Response
                var responseText = JSON.parse(this.responseText);
                commentaryBtn.classList.add('clicked')
                commentaryBtn.textContent = 'Hide Commentary';
                CreateComentaryHtml(responseText)
            }

        };
        var currEvent = document.querySelector('body');
        var id = currEvent.id;
        var data = { eventId: id };
        xhttp.send(JSON.stringify(data));
    } else {
        commentaryBtn.classList.remove('clicked')
        commentaryBtn.textContent = 'See comments';
        document.getElementsByName('comentaryDivContainer')[0].remove();
    }


}

function CreateComentaryHtml(comentaries) {
    var body = document.getElementsByTagName('BODY')[0];
    var divContainer = document.createElement('div');
    divContainer.setAttribute('name', 'comentaryDivContainer')
    divContainer.setAttribute('class', 'container');
    divContainer.id = '12345'
    body.appendChild(divContainer);

    for (const commentary of comentaries) {

        var dialogBoxDiv = document.createElement('div');
        dialogBoxDiv.setAttribute('class', 'dialogbox');
        var comentarySpan = document.createElement('span');
        var comentaryA = document.createElement('a');
        comentaryA.textContent = commentary.userName;
        comentarySpan.appendChild(comentaryA);
        dialogBoxDiv.appendChild(comentarySpan);


        var bodyDiv = document.createElement('div');
        bodyDiv.setAttribute('class', 'body');
        var bodySpan = document.createElement('span');
        bodySpan.setAttribute('class', 'tip tip-up')
        bodyDiv.appendChild(bodySpan);

        var messageDiv = document.createElement('div');
        messageDiv.setAttribute('class', 'message');
        var messageSpan = document.createElement('span');
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

        var replyA = document.createElement('a');
        replyA.setAttribute('id', 'replyA')
        replyA.addEventListener('click', CreateReplyForm);
        replyA.textContent = 'Reply';

        messageSpan.appendChild(likeComentaryButton);
        messageSpan.appendChild(DislikeComentaryButton);
        messageSpan.appendChild(replyA);
        bodyDiv.appendChild(messageDiv);
        dialogBoxDiv.appendChild(bodyDiv);
        divContainer.appendChild(dialogBoxDiv);

        if (commentary.replies.length > 0) {
            for (let reply of commentary.replies) {
                var replyDialogBoxDiv = document.createElement('div');
                replyDialogBoxDiv.setAttribute('class', 'replyDialogbox');
               var replyComentarySpan = document.createElement('span');
                var replyComentaryA = document.createElement('a');
                replyComentaryA.textContent = reply.userName;
                replyComentarySpan.appendChild(replyComentaryA);
                replyDialogBoxDiv.appendChild(replyComentarySpan);


                var replyBodyDiv = document.createElement('div');
                replyBodyDiv.setAttribute('class', 'replyBody');
                var replyBodySpan = document.createElement('span');
                replyBodySpan.setAttribute('class', 'tip tip-up')
                replyBodyDiv.appendChild(replyBodySpan);

                var replyMessageDiv = document.createElement('div');
                replyMessageDiv.setAttribute('class', 'message');
                var replyMessageSpan = document.createElement('span');
                replyMessageDiv.appendChild(replyMessageSpan);
                var replyMessageP = document.createElement('p');
                replyMessageP.setAttribute('class', 'text-break');
                replyMessageP.textContent = `${reply.content}`;
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

    let arrToPrint = [...document.querySelectorAll('button')]
        .filter(x => x.name == 'LikeComentary' || x.name == 'DislikeComentary' ||
            x.name == 'LikeReply' || x.name == 'DislikeReply');

    for (const button of arrToPrint) {
        button.addEventListener('click', LikeDislikeComentary);
    }
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


export function WriteCommentary() {
    var body = [...document.getElementsByTagName('BODY')][0];
    var commentFormDiv = document.createElement('div');
    commentFormDiv.setAttribute('class', 'comment-form-form-area');

    var commentRespondDiv = document.createElement('div');
    commentRespondDiv.setAttribute('class', 'comment-respond');
    commentRespondDiv.setAttribute('id', 'respond');
    var h3 = document.createElement('h3');
    h3.textContent = 'Leave commentary here.';
    commentFormDiv.appendChild(commentRespondDiv);
    commentRespondDiv.appendChild(h3);


    var commentP = document.createElement('p');
    commentP.setAttribute('class', 'comment-form-comment');

    var label = document.createElement('label');
    label.textContent = 'Comment';
    commentP.appendChild(label);
    commentRespondDiv.appendChild(commentP)

    var commentTextArea = document.createElement('textarea');
    commentTextArea.setAttribute('id', 'comment');
    commentTextArea.setAttribute('name', 'comment');
    commentTextArea.setAttribute('cols', '450')
    commentTextArea.setAttribute('rows', '8')
    commentTextArea.setAttribute('maxlength', '65525')
    commentTextArea.setAttribute('required', 'required')
    commentRespondDiv.appendChild(commentTextArea)
    var eventId = body.id;

    var input = document.createElement('input');
    input.value = eventId;
    input.setAttribute('type', 'hidden');
    input.setAttribute('id', 'currEventId')
    commentRespondDiv.appendChild(input)

    var btn = document.createElement('button');
    btn.type = 'submit';
    btn.setAttribute('id', 'submitComment')
    btn.textContent = 'Send';
    btn.setAttribute('asp-action', 'WriteComentary');
    commentRespondDiv.appendChild(btn)

    body.appendChild(commentFormDiv);
    var btn = document.getElementById('WriteCommentary')
    btn.parentNode.removeChild(btn);
    document.getElementById('submitComment').addEventListener('click', SendComment)

}