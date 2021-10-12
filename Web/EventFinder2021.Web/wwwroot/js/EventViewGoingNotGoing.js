import * as api from "./api.js"
export async function GoingNotGoing(ev) {
    if (ev.target.tagName == 'BUTTON') {
        var currEvent = document.querySelector('body');
        var id = currEvent.id;

        if (ev.target.id == 'Going' || ev.target.id == 'NotGoing') {
            let action = ev.target.id;
            var url = api.host + "Event/" + action + "ToEvent";

            const response = await fetch(url, {
                method: 'post',
                headers: { 'Content-Type': 'Application/json' },
                body: JSON.stringify({ eventId: id })
            })
            
            if (response.ok) {
                const data = await response.json();
                var goingUsersCount = data.goingUsersCount;
                var btn = document.getElementById("Going");

                btn.textContent = `Going ${goingUsersCount}`;
                btn.style.backgroundColor = "Green";

                var notGoingUsersCount = data.notGoingUsersCount;
                var notGoingButton = document.getElementById("NotGoing");

                notGoingButton.textContent = `Not Going ${notGoingUsersCount}`;
                notGoingButton.style.backgroundColor = "Red";
            }
        }
    }
}