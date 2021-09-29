import { GoingNotGoing } from "./EventViewGoingNotGoing.js"
import { ShowComments, WriteCommentary } from "./EventViewComentaries.js"
import { Vote } from "./EventViewVote.js"
  const serverLocation = 'https://localhost:44319';

window.addEventListener('load', attachEvents)

function attachEvents() {
    document.getElementById('GoNotGoAndReportBtns').addEventListener('click', GoingNotGoing);

    document.getElementById('displayComments').addEventListener('click', ShowComments)

    var smth = document.querySelectorAll('li i').forEach(x => x.addEventListener('click', Vote))

    var grade = document.getElementById('averageVoteGrade').textContent.split(' / ')[0];
    DecorateStars(grade)

    document.getElementById('WriteCommentary').addEventListener('click', WriteCommentary)
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
    console.log(btn + 'is here')
    if (btn.classList.contains('clicked')) {
        console.log('im inside');

    }
})

 function CallLastComment() {
    connection.invoke('DisplayNewCommentToUsers')
}

 function DecorateStars(grade) {
    var stars = document.querySelectorAll("i");
    grade = Math.round(grade);
    for (let i = 0; i < grade; i++) {
        var currentStar = stars[i];
        currentStar.setAttribute('style', 'color:yellow');
    }

    for (let i = grade; i < stars.length; i++) {
        var currentStar = stars[i];
        currentStar.setAttribute('style', 'color:none');
    }
}
export {
    serverLocation,
    CallLastComment,
    DecorateStars
}