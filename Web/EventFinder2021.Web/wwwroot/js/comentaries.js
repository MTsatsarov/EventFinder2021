function Like() {

    let arrToPrint = document.querySelectorAll('button');

    for (const button of arrToPrint) {
        button.addEventListener('click', LikeDislikeComentary);
    }

}

function LikeDislikeComentary(ev) {
    var clickedBtn = ev.target;
    if (clickedBtn.name == 'LikeComentary') {
        ReturnComentaryLikesAndDislikes(clickedBtn.id, 'LikeComentary');
    }
    else if (clickedBtn.name == 'DislikeComentary') {
        ReturnComentaryLikesAndDislikes(clickedBtn.id, 'DislikeComentary');
    }

}


function ReturnComentaryLikesAndDislikes(btnId, action) {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', `/Comentary/${action}`, true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Response
            var responseText = JSON.parse(this.responseText);
            var tr = document.getElementById(btnId).parentNode.parentNode;
            tr.children[0].children[0].textContent = `Like ${responseText.comentaryLikeCount}`;
            tr.children[1].children[0].textContent = `Dislike ${responseText.comentaryDislikeCount}`;

        }
    };
    var data = { comentaryId: btnId };
    xhttp.send(JSON.stringify(data));
}
