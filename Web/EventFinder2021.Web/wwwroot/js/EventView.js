import { GoingNotGoing } from "./EventViewGoingNotGoing.js"
import { ShowComments, WriteCommentary, InitialComentariesLoad, LikeDislike, CreateReplyForm } from "./EventViewComentaries.js"
import { Vote, DecorateStars } from "./EventViewVote.js"
import { render } from "../lib/lit-html/lit-html.js";
import { singleComentaryTemplate } from "./EventViewTemplates.js";
var connection = new signalR.HubConnectionBuilder().withUrl("/eventViewHub").build();

connection.start().then(function () {

})
const comentaryContainerDiv = document.getElementsByName('comentaryDivContainer')[0];
var comentariesList = [];

Start();

connection.on("GetGroup", function (method, isAuthenticated) {
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
    displayUserContent(isAuthenticated)


})
async function Start() {
    document.getElementById('GoNotGoAndReportBtns').addEventListener('click', GoingNotGoing);
    document.getElementById('displayComments').addEventListener('click', ShowComments)
    document.getElementById('WriteCommentary').addEventListener('click', WriteCommentary);
    var smth = document.querySelectorAll('li i').forEach(x => x.addEventListener('click', Vote))
    
    var grade = document.getElementById('averageVoteGrade').textContent.split(' / ')[0];
    DecorateStars(grade)
    var comentaryTemplate = await InitialComentariesLoad(document.body.id, comentariesList)
    render(comentaryTemplate, comentaryContainerDiv)

}
connection.on('DisplayNewComment', function (comentary) {
    var btn = document.getElementById('displayComments');
    comentariesList.push(comentary)
    render(singleComentaryTemplate(comentariesList, LikeDislike, CreateReplyForm), comentaryContainerDiv);
})

export function CallLastComment() {
    connection.invoke('DisplayNewCommentToUsers')
}

function displayUserContent(isAuthenticated) {
    if (isAuthenticated) {
        document.getElementById('WriteCommentary').style.display = 'inline';
        document.querySelectorAll('#GoNotGoAndReportBtns>div>button').forEach(x => x.removeAttribute('disabled'))
        document.getElementById('displayComments').style.display = 'inline';
    }
}
