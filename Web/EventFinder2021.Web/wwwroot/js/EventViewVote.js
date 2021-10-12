import { serverLocation, DecorateStars } from "./EventView.js";

export async function Vote(ev) {
    var currEvent = document.querySelector('body');
    var currEventId = currEvent.id;
    var gradeId = ev.target.parentNode.id;
    var sendData = { eventId: currEventId, grade: gradeId };
    const response = await fetch(serverLocation + "/api/Votes", {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(sendData)
    })
    if (response.ok) {
        const data = await response.json();
        var grade = data.averageVoteValue;
        grade = grade.toFixed(1);
        document.getElementById('averageVoteGrade').textContent = `${grade} / 5`;
        DecorateStars(grade);
    }

}