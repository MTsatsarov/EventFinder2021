function GoingNotGoing() {
    document.getElementById('btnSuccess').addEventListener('click', Going);

    document.getElementById('btn-danger').addEventListener('click', NotGoing);

    document.getElementById('displayComments').addEventListener('click', ShowComments)

    var smth = document.querySelectorAll('li i').forEach(x => x.addEventListener('click', Vote))

    console.log(smth);
}
function ClearComments() {
    var element = document.getElementsByName('comentaryDivContainer')[0];
    element.parentNode.removeChild(element);
    var commentsBtn = document.getElementById('displayComments');
    commentsBtn.textContent = 'See comments';
    commentsBtn.removeEventListener('click',ClearComments);
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
    var table = document.createElement('table');
    var tbody = document.createElement('tbody');
    table.appendChild(tbody);
    divContainer.appendChild(table);
    body.appendChild(divContainer);
    for (const commentary of comentaries) {

        var userNameTr = document.createElement('tr');
        var userNameTd = document.createElement('td');
        var userNameDiv = document.createElement('div');
        userNameDiv.textContent = commentary.userName;
        userNameTd.appendChild(userNameDiv);
        userNameTr.appendChild(userNameTd);
        tbody.appendChild(userNameTr);

        var contentTr = document.createElement('tr');
        var contentTd = document.createElement('td');
        var contentDiv = document.createElement('div');
        contentDiv.textContent = commentary.content;
        contentTd.appendChild(contentDiv);
        contentTr.appendChild(contentTd);
        tbody.appendChild(contentTr);

        var buttonsTr = document.createElement('tr');
        var likeButtonTd = document.createElement('td');
        var dislikeButtonTd = document.createElement('td');
        var likeComentaryButton = document.createElement('button');
        var DislikeComentaryButton = document.createElement('button');
        likeComentaryButton.id = commentary.id;
        likeComentaryButton.name = "LikeComentary";
        likeComentaryButton.setAttribute('class', 'btn-success');
        likeComentaryButton.value = commentary.id;
        likeComentaryButton.textContent = `Like ${commentary.likesCount}`;
        likeComentaryButton.type = 'submit';
        likeButtonTd.appendChild(likeComentaryButton);
        buttonsTr.appendChild(likeButtonTd);

        DislikeComentaryButton.id = commentary.id;
        DislikeComentaryButton.name = "DislikeComentary";
        DislikeComentaryButton.setAttribute('class', 'btn-danger');
        DislikeComentaryButton.value = commentary.id;
        DislikeComentaryButton.textContent = `Dislikes ${commentary.dislikesCount}`;
        DislikeComentaryButton.type = 'submit';
        dislikeButtonTd.appendChild(DislikeComentaryButton);
        buttonsTr.appendChild(dislikeButtonTd);
        //TODO ---Replies;
        tbody.appendChild(buttonsTr);

    }
    var commentaryBtn = document.getElementById('displayComments');
    commentaryBtn.textContent = 'Hide Commentary';
    commentaryBtn.removeEventListener('click', ShowComments)
    commentaryBtn.addEventListener('click', ClearComments)
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