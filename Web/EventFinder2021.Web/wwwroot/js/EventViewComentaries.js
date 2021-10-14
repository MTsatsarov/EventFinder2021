import { CallLastComment, } from "./EventView.js"
import { render } from "../lib/lit-html/lit-html.js"
import { SendNewReply, getAllComentaries, getLikesDislikes, sendNewComentary } from "./data.js"
import * as templates from "./EventViewTemplates.js"

async function comentaryTemplateResult(list) {
    return templates.singleComentaryTemplate(list, LikeDislike, CreateReplyForm);
}
export async function InitialComentariesLoad(id, list) {
    var result = await getAllComentaries({ eventId: id });
    result.forEach(c => list.push(c))
    return comentaryTemplateResult(result)
}

export async function CreateReplyForm(ev) {
    var div = ev.target.parentNode.parentNode;
    var anchor = ev.target;
    anchor.style.display = 'none'
    render(templates.replyFormTemplate(SendReply, () => dismissForm(anchor, 'reply-form-form-area'), anchor), div)

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

function dismissForm(buttonToShow, formToRemove) {
    if (formToRemove == 'reply-form-form-area') {
        document.querySelector(".reply-form-form-area").style.display = 'none';
        buttonToShow.style.display = 'inline';
    } else {
        document.getElementById(formToRemove).style.display = 'none';
        document.getElementById('WriteCommentary').style.display = 'inline';
    }
}

export async function ShowComments() {
    var id = document.querySelector('body').id;
    var commentaryBtn = document.getElementById('displayComments');
    if (!commentaryBtn.classList.contains("clicked")) {
        commentaryBtn.classList.add('clicked')
        commentaryBtn.textContent = 'Hide Commentary';
        document.getElementsByName('comentaryDivContainer')[0].style.display = 'block'

    } else {
        document.getElementsByName('comentaryDivContainer')[0].style.display = 'none'
        commentaryBtn.classList.remove('clicked')
        commentaryBtn.textContent = 'See comments';
    }
}
export function LikeDislike(ev) {
    var action = (ev.target.name)
    var controller = (ev.target.parentNode.getAttribute('data-controller'))
    var id = (ev.target.parentNode.getAttribute('data-id'))
    ReturnLikesAndDislikes(id, controller, action)

    async function ReturnLikesAndDislikes(id, controller, action) {
        var data;
        controller == 'Reply' ? data = { replyId: id } : data = { comentaryId: id };
        var result = await getLikesDislikes(controller, action, data)
        var buttons = ev.target.parentNode.querySelectorAll('button');
        buttons[0].textContent = `Like ${result.comentaryLikeCount}`;
        buttons[1].textContent = `Dislike ${result.comentaryDislikeCount}`;
    }
}
export function WriteCommentary(ev) {
    ev.target.style.display = 'none'
    document.getElementById('submitComment').addEventListener('click', SendComment)
    document.getElementById('sendComentboxDiv').style.display = 'block';

    document.getElementById('cancelForm').addEventListener('click', () => dismissForm(null, 'sendComentboxDiv'))
    async function SendComment() {
        var currEventId = document.getElementById('currEventId').value
        var commentContent = document.getElementById('comment').value
        var data = { eventId: Number(currEventId), content: commentContent };
        await sendNewComentary(data)

        CallLastComment();
        ev.target.style.display = 'inline';
        var box = document.getElementById('sendComentboxDiv');
        box.style.display = 'none'
        box.getElementsByTagName('TEXTAREA')[0].value = ''
    }
}