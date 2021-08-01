function Like() {

    let arrToPrint = document.querySelectorAll('button');

    for (const button of arrToPrint) {
        button.addEventListener('click', LikeDislikeComentary);
    }

}

function LikeDislikeComentary(ev) {
    var clickedBtn = ev.target;
    if (clickedBtn.name == 'LikeComentary') {
        ReturnComentaryLikesAndDislikes(clickedBtn.id,'Comentary', 'LikeComentary');
    }
     if (clickedBtn.name == 'DislikeComentary') {
        ReturnComentaryLikesAndDislikes(clickedBtn.id,'Comentary', 'DislikeComentary');
    }
    if (clickedBtn.name == 'LikeReply') {
        ReturnComentaryLikesAndDislikes(clickedBtn.id, 'Reply', 'LikeReply');
    }
    if (clickedBtn.name == 'DislikeReply') {
        ReturnComentaryLikesAndDislikes(clickedBtn.id, 'Reply', 'DislikeReply');
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
                
                 tr = document.getElementById(btnId).parentNode.parentNode;
            } else {
               var element  = Array.from(document.getElementsByName('LikeReply')).filter(x=>x.id==`${btnId}`);
               
                tr = element[0].parentNode.parentNode;
            }
            tr.children[0].children[0].textContent = `Like ${responseText.comentaryLikeCount}`;
            tr.children[1].children[0].textContent = `Dislike ${responseText.comentaryDislikeCount}`;

        }
    };
    var data = { comentaryId: btnId };
    xhttp.send(JSON.stringify(data));
}
