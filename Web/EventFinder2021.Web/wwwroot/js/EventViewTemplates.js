import { html, render } from "../lib/lit-html/lit-html.js"

export let replyTeplate = (reply, onClick) => html`
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

export let singleComentaryTemplate = (comentary, onClick, createReplyForm) => html`
${comentary.map(c => html`
<div class="dialogbox">
    <span><a>${c.userName}</a>
    </span>
    <div class="body">
        <span class="tip tip-up"></span>
        <div class="message">
            <span data-controller="Comentary" , data-id="${c.comentaryId}">
                <p class="text-break">${c.content}</p>
                <button @click=${onClick} name="LikeComentary" class="btn-success" type="submit">Like
                    ${c.likesCount}
                </button>
                <button @click=${onClick} name="DislikeComentary" class="btn-danger" type="submit">Dislike
                    ${c.dislikesCount}</button>
                <a @click=${createReplyForm} .id="replyA">Reply</a>
            </span>
        </div>
    </div>
</div>
${c.replies.map(r => replyTeplate(r, onClick))}
`)}`;


export let replyFormTemplate = (SendReply, dismissForm, anchor) => html`
    <div class="reply-form-form-area">
        <div class="comment-respond" id="respond">
            <h3>Leave reply here.</h3>
            <p class="comment-form-comment"><label>Reply</label></p><textarea id="reply" name="reply" cols="450" rows="8"
                maxlength="65525" required="required"></textarea>
            <button @click=${SendReply} type="submit" id="submitReply">Send</button>
            <button @click=${()=> dismissForm(anchor, "reply-form-form-area")} type = "submit" id = "clearForm" >
                Cancel</button>
        </div>
    </div>
    `;

export let postComentaryBoxTemplate = (body) => html`
    <div class="comment-form-form-area">
        <div class="comment-respond" id="respond">
            <h3>Leave commentary here.</h3>
            <p class="comment-form-comment"><label>Comment</label></p><textarea id="comment" name="comment" cols="450"
                rows="8" maxlength="65525" required="required"></textarea><input type="hidden" value="${body.id}"
                id="currEventId"><button type="submit" id="submitComment">Send</button>
            <button @click=${()=> dismissForm(null, "comment-form-form-area")} type="submit"
                id="submitComment">Cancel</button>
        </div>
    </div>
    `;