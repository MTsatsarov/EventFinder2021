import { CallLastComment, serverLocation } from "./EventView.js"
import { html, render } from "../lib/lit-html/lit-html.js"
import { SendNewReply, getAllComentaries, getLikesDislikes, sendNewComentary } from "./data.js"

function CreateReplyForm(ev) {
    var div = ev.target.parentNode.parentNode;
    var anchor = ev.target;
    anchor.style.display = 'none'
    render(replyFormTemplate(SendReply), div)

    async function SendReply() {
        var comentaryId = anchor.parentNode.getAttribute('data-id');
        var replyContent = document.getElementById('reply').value;
        var eventId = document.getElementsByTagName('BODY')[0].id;
        var data = { eventId: Number(eventId), comentaryId: Number(comentaryId), content: replyContent };
        await SendNewReply(data);
        var divToRemove = document.getElementsByClassName('reply-form-form-area')[0];
        var parent = divToRemove.parentNode.children[0];
        divToRemove.parentElement.removeChild(divToRemove);
        anchor.style.display = 'inline'
    }
}

export async function ShowComments() {
    var id = document.querySelector('body').id;
    var commentaryBtn = document.getElementById('displayComments');
    if (!commentaryBtn.classList.contains("clicked")) {
        var result = await getAllComentaries({ eventId: id });
        commentaryBtn.classList.add('clicked')
        commentaryBtn.textContent = 'Hide Commentary';
        CreateComentaryHtml(result)
    } else {
        commentaryBtn.classList.remove('clicked')
        commentaryBtn.textContent = 'See comments';
        document.getElementsByName('comentaryDivContainer')[0].remove();
    }
}
function CreateComentaryHtml(comentaries) { render(comentaryBoxTemplate(comentaries, LikeDislikeComentary), document.body); }


function LikeDislikeComentary(ev) {
    var action = (ev.target.name)
    var controller = (ev.target.parentNode.getAttribute('data-controller'))
    var id = (ev.target.parentNode.getAttribute('data-id'))
    ReturnComentaryLikesAndDislikes(id, controller, action)

    async function ReturnComentaryLikesAndDislikes(id, controller, action) {
        var data;
        controller == 'Reply' ? data = { replyId: id } : data = { comentaryId: id };
        var result = await getLikesDislikes(controller, action, data)
        var buttons = ev.target.parentNode.querySelectorAll('button');
        buttons[0].textContent = `Like ${result.comentaryLikeCount}`;
        buttons[1].textContent = `Dislike ${result.comentaryDislikeCount}`;
    }
}

export function WriteCommentary() {
    var body = [...document.getElementsByTagName('BODY')][0];
    render(postComentaryBoxTemplate(body), body)

    var btn = document.getElementById('WriteCommentary');
    btn.style.display = 'none'
    document.getElementById('submitComment').addEventListener('click', SendComment)

    async function SendComment() {
        var currEventId = document.getElementById('currEventId').value
        var commentContent = document.getElementById('comment').value
        var data = { eventId: Number(currEventId), content: commentContent };
        await sendNewComentary(data)

        CallLastComment();
        btn.style.display = 'inline';
        var divToRemove = document.getElementsByClassName('comment-form-form-area')[0];
        var parent = divToRemove.parentNode.children[0];
        divToRemove.parentElement.removeChild(divToRemove);
    }

}

const replyTeplate = (reply, onClick) => html`
<div class="replyDialogbox"><span><a>${reply.userName}</a></span>
    <div class="replyBody"><span class="tip tip-up"></span>
        <div class="message">
            <span data-controller="Reply" , data-id=${reply.replyId}>
                <p class="text-break">${reply.content}</p>
                <button @click=${onClick} name="LikeReply" type="submit" class="btn-success" value=${reply.replyId}>Like
                    ${reply.replyLikesCount}</button>
                <button @click=${onClick} name="DislikeReply" type="submit" class="btn-danger"
                    value=${reply.replyId}>Dislike
                    ${reply.replyDislikesCount}</button>
            </span>
        </div>
    </div>
</div>
`;

const singleComentaryTemplate = (comentary, onClick) => html`
<div class="dialogbox">
    <span><a>${comentary.userName}</a>
    </span>
    <div class="body">
        <span class="tip tip-up"></span>
        <div class="message">
            <span data-controller="Comentary" , data-id="${comentary.comentaryId}">
                <p class="text-break">${comentary.content}</p>
                <button @click=${onClick} name="LikeComentary" class="btn-success" type="submit">Like
                    ${comentary.likesCount}
                </button>
                <button @click=${onClick} name="DislikeComentary" class="btn-danger" type="submit">Dislike
                    ${comentary.dislikesCount}</button>
                <a @click=${CreateReplyForm} id="replyA">Reply</a>
            </span>
        </div>
    </div>
</div>
${comentary.replies.map(r => replyTeplate(r, onClick))}
`;

const comentaryBoxTemplate = (comentaries, onClick) => html`
<div name='comentaryDivContainer'>
    ${comentaries.map(c => singleComentaryTemplate(c, onClick))}
</div>
`;

const replyFormTemplate = (SendReply) => html`
<div class="reply-form-form-area">
    <div class="comment-respond" id="respond">
        <h3>Leave reply here.</h3>
        <p class="comment-form-comment"><label>Reply</label></p><textarea id="reply" name="reply" cols="450" rows="8"
            maxlength="65525" required="required"></textarea>
        <button @click=${SendReply} type="submit" id="submitReply">Send</button>
    </div>
</div>
`;

const postComentaryBoxTemplate = (body) => html`
<div class="comment-form-form-area">
    <div class="comment-respond" id="respond">
        <h3>Leave commentary here.</h3>
        <p class="comment-form-comment"><label>Comment</label></p><textarea id="comment" name="comment" cols="450"
            rows="8" maxlength="65525" required="required"></textarea><input type="hidden" value="${body.id}"
            id="currEventId"><button type="submit" id="submitComment">Send</button>
    </div>
</div>
`;