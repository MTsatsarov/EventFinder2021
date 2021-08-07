function OnLoad(model) {
    var model = JSON.parse(model);
    var usersCount = model.usersCount;
    var mostVisitedEvents = model.mostVisitedEvents;
    var mostCommentedEvents = model.mostCommentedEvents;
    var topUsers = model.topUsers;

    var pageBody = document.getElementsByTagName('body')[0];
    var pageDiv = document.createElement('div');
    pageBody.appendChild(pageDiv);

    DisplayUsersCount(usersCount, pageDiv);

    CreateEventHtml(mostVisitedEvents, pageDiv,
        { firstTd: 'Event Name', secondTd: 'Creator', thirdTd: 'Going Users' });

    CreateEventHtml(mostCommentedEvents, pageDiv,
        { firstTd: 'Event Name', secondTd: 'Creator', thirdTd: 'Commentaries Count' });

    CreateUsersHtml(topUsers, pageDiv,
        { firstTd: 'Username', secondTd: 'Events Count' });



}


function DisplayUsersCount(usersCount, pageDiv) {

    var usersDiv = document.createElement('div');
    var usersSpan = document.createElement('span');
    var usersH3 = document.createElement('h3');
    usersH3.textContent = `We have currently ${usersCount} users registered.`;
    usersSpan.appendChild(usersH3);
    usersDiv.appendChild(usersSpan);
    pageDiv.appendChild(usersDiv);

}

function CreateEventHtml(mostVisitedEvents, pageDiv, firstRow) {
    var visitedEventsDiv = document.createElement('div');
    var visitedEventsTable = document.createElement('table');
    var visitedEventsTBody = document.createElement('tbody');

    var tr = document.createElement('tr');
    var firstTd = document.createElement('td');
    firstTd.textContent = firstRow.firstTd;
    var secondTd = document.createElement('td');
    secondTd.textContent = firstRow.secondTd;
    var thirdTd = document.createElement('td');
    thirdTd.textContent = firstRow.thirdTd;

    tr.appendChild(firstTd);
    tr.appendChild(secondTd);
    tr.appendChild(thirdTd);

    visitedEventsTBody.appendChild(tr);

    for (let i = 0; i < mostVisitedEvents.length; i++) {
        var currEvent = mostVisitedEvents[i]
        var eventTr = document.createElement('tr');

        var eventName = document.createElement('td');
        eventName.textContent = currEvent.name;

        var creatorNameTd = document.createElement('td');
        creatorNameTd.textContent = currEvent.creatorName;

        var countTd = document.createElement('td');
        countTd.textContent = currEvent.count;

        eventTr.append(eventName);
        eventTr.append(creatorNameTd);
        eventTr.append(countTd);
        visitedEventsTBody.appendChild(eventTr);
    }

    visitedEventsTable.appendChild(visitedEventsTBody);
    visitedEventsDiv.appendChild(visitedEventsTable);
    pageDiv.appendChild(visitedEventsDiv);
}

function CreateUsersHtml(topUsers, pageDiv, firstRow) {
    var usersDiv = document.createElement('div');

    var usersTable = document.createElement('table');

    var usersTbody = document.createElement('tbody');

    var firstTr = document.createElement('tr');

    var firstTd = document.createElement('td');
    firstTd.textContent = firstRow.firstTd;

    var secondTd = document.createElement('td');
    secondTd.textContent = firstRow.secondTd;

    firstTr.appendChild(firstTd);
    firstTr.appendChild(secondTd);
    usersTbody.appendChild(firstTr);

    for (let i = 0; i < topUsers.length; i++) {
        var currRow = topUsers[i];
        var eventTr = document.createElement('tr');

        var userNameTd = document.createElement('td');
        userNameTd.textContent = currRow.userName;

        var eventCount = document.createElement('td');
        eventCount.textContent = currRow.eventCount;

        eventTr.appendChild(userNameTd);
        eventTr.appendChild(eventCount);
        usersTbody.appendChild(eventTr);

    }
    usersTable.appendChild(usersTbody);
    usersDiv.appendChild(usersTable)
    pageDiv.appendChild(usersDiv)
}