import { GoingNotGoing } from "./EventViewGoingNotGoing.js"
import { ShowComments, WriteCommentary, InitialComentariesLoad, LikeDislike, CreateReplyForm } from "./EventViewComentaries.js"
import { Vote, DecorateStars } from "./EventViewVote.js"
import { render } from "../lib/lit-html/lit-html.js";
import { singleComentaryTemplate } from "./EventViewTemplates.js";
const comentaryContainerDiv = document.getElementsByName('comentaryDivContainer')[0];
var comentariesList = [];
Start();

async function Start() {
    document.getElementById('GoNotGoAndReportBtns').addEventListener('click', GoingNotGoing);
    document.getElementById('displayComments').addEventListener('click', ShowComments)
    var smth = document.querySelectorAll('li i').forEach(x => x.addEventListener('click', Vote))
    var grade = document.getElementById('averageVoteGrade').textContent.split(' / ')[0];
    DecorateStars(grade)
    document.getElementById('WriteCommentary').addEventListener('click', WriteCommentary);
    comentaryContainerDiv.style.display = 'none'
    var comentaries = await InitialComentariesLoad(document.body.id, comentariesList)
    render(comentaries, comentaryContainerDiv)

}

var connection = new signalR.HubConnectionBuilder().withUrl("/eventViewHub").build();
connection.start().then(function () {

})

connection.on("GetGroup", function (method) {
    let groupId = document.querySelector('body').id;
    if (method == 'add') {
        connection.invoke("AddUserToGroup", groupId).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        connection.invoke("RemoveUserFromGroup", groupId).catch(function (err) {
            return console.error(err.toString());
        });

    }
})
connection.on('DisplayNewComment', function (comentary) {
    var btn = document.getElementById('displayComments');
    comentariesList.push(comentary)
    render(singleComentaryTemplate(comentariesList, LikeDislike, CreateReplyForm), comentaryContainerDiv);
})

export function CallLastComment() {
    connection.invoke('DisplayNewCommentToUsers')
}

