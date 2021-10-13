import { getVotesGrade } from "./data.js";

export async function Vote(ev) {
    var currEventId = document.querySelector('body').id;
    var gradeId = ev.target.parentNode.id;
    var sendData = { eventId: currEventId, grade: gradeId };
    var grade = await getVotesGrade(sendData);
    document.getElementById('averageVoteGrade').textContent = `${grade} / 5`;
    DecorateStars(grade);

}

export function DecorateStars(grade) {
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