function OnLoad(model) {
    var model = JSON.parse(model);
    var usersCount = model.usersCount;
    var mostVisitedEvents = model.mostVisitedEvents;
    var mostCommentedEvents = model.mostCommentedEvents;
    var topUsers = model.topUsers;

    var pageBody = document.getElementsByTagName('body')[0];
    var pageDiv = document.createElement('div');
    pageDiv.setAttribute('class', 'statisticDiv')
    pageBody.appendChild(pageDiv);

    DisplayUsersCount(usersCount, pageDiv);

    CreateEventHtml(mostVisitedEvents, pageDiv,
        { firstTd: 'Position', secondTd: 'Event Name', thirdTd: 'Creator', fourthTd: 'Going Users' }, 'visited events');

    CreateEventHtml(mostCommentedEvents, pageDiv,
        { firstTd: 'Position', secondTd: 'Event Name', thirdTd: 'Creator', fourthTd: 'Commentaries' }, 'commented events.');

    CreateUsersHtml(topUsers, pageDiv,
        { firstTd: 'Position', secondTd: 'Username', thirdTd: 'Events Created' });

    CreateSearchHtml(pageDiv);

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

function CreateEventHtml(mostVisitedEvents, pageDiv, firstRow, tableName) {
    var visitedEventsDiv = document.createElement('div');
    var visitedEventsTable = document.createElement('table');
    var visitedEventsTBody = document.createElement('tbody');
    var h4 = document.createElement('h4');
    h4.textContent = `These are our most ${tableName}.`

    visitedEventsDiv.appendChild(h4);
    var visitedEventsTHead = document.createElement('thead');
    visitedEventsTable.appendChild(visitedEventsTHead);
    var tr = document.createElement('tr');
    var firstTd = document.createElement('td');
    firstTd.textContent = firstRow.firstTd;
    var secondTd = document.createElement('td');
    secondTd.textContent = firstRow.secondTd;
    var thirdTd = document.createElement('td');
    thirdTd.textContent = firstRow.thirdTd;
    var fourthTd = document.createElement('td');
    fourthTd.textContent = firstRow.fourthTd;

    tr.appendChild(firstTd);
    tr.appendChild(secondTd);
    tr.appendChild(thirdTd);
    tr.appendChild(fourthTd);
    visitedEventsTHead.appendChild(tr);

    for (let i = 0; i < mostVisitedEvents.length; i++) {
        var currEvent = mostVisitedEvents[i]
        var eventTr = document.createElement('tr');
        eventTr.setAttribute('class', 'contentTr');

        var positionTr = document.createElement('td');
        positionTr.setAttribute('class', 'positionTr');
        positionTr.textContent = i + 1;
        positionTr.setAttribute('id', 'positionTr');
        eventTr.appendChild(positionTr);

        var eventName = document.createElement('td');
        eventName.setAttribute('id', 'eventName');
        eventName.textContent = currEvent.name;

        var creatorNameTd = document.createElement('td');
        creatorNameTd.textContent = currEvent.creatorName;
        creatorNameTd.setAttribute('id', 'username');

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
    usersDiv.setAttribute('class', 'UsersDiv');
    var h4 = document.createElement('h4');
    h4.textContent = `These are top users`

    usersDiv.appendChild(h4);
    var usersTable = document.createElement('table');

    var usersTbody = document.createElement('tbody');

    var usersTHead = document.createElement('thead');
    usersTable.appendChild(usersTHead);

    var firstTr = document.createElement('tr');

    var firstTd = document.createElement('td');
    firstTd.textContent = firstRow.firstTd;

    var secondTd = document.createElement('td');
    secondTd.textContent = firstRow.secondTd;
    var thirdTd = document.createElement('td');
    thirdTd.textContent = firstRow.thirdTd;

    firstTr.appendChild(firstTd);
    firstTr.appendChild(secondTd);
    firstTr.appendChild(thirdTd);
    usersTHead.appendChild(firstTr);

    for (let i = 0; i < topUsers.length; i++) {
        var currRow = topUsers[i];
        var positionTr = document.createElement('td');
        positionTr.setAttribute('class', 'positionTr');
        positionTr.setAttribute('id', 'positionTr');
        positionTr.textContent = i + 1;
        var eventTr = document.createElement('tr');
        eventTr.setAttribute('class', 'contentTr');
        var userNameTd = document.createElement('td');
        userNameTd.setAttribute('id', 'username');
        userNameTd.textContent = currRow.userName;

        var eventCount = document.createElement('td');
        eventCount.textContent = currRow.eventCount;

        eventTr.appendChild(positionTr);
        eventTr.appendChild(userNameTd);
        eventTr.appendChild(eventCount);
        usersTbody.appendChild(eventTr);

    }
    usersTable.appendChild(usersTbody);
    usersDiv.appendChild(usersTable)
    pageDiv.appendChild(usersDiv)
}



function CreateSearchHtml(pageDiv) {

    var searchDiv = document.createElement('div');
    var inputField = document.createElement('input');
    inputField.setAttribute('id', 'input');
    var btn = document.createElement('button');
    btn.setAttribute('class', 'searchBtn');
    btn.textContent = 'Search';
    btn.addEventListener('click', SearchInput)
    searchDiv.appendChild(inputField);
    searchDiv.appendChild(btn);
    pageDiv.appendChild(searchDiv);
}

function SearchInput() {
    var inputField = document.getElementById('input')
    var input = inputField.value;

    var tds = [...document.querySelectorAll('tbody>tr>td')].filter(x => x.id != 'positionTr' && x.id == 'eventName' || x.id == 'username');
    tds.forEach(x => x.parentNode.style.backgroundColor = 'gold');
    var text = tds.filter(x => x.textContent.toLowerCase().includes(input.toLowerCase()));

    text.forEach(x => x.parentNode.style.backgroundColor = 'red');
    inputField.value="";
}