function OnLoad() {
    CreateInitialHtml();
    var eventBtn = document.getElementById('GetEventReports');
    eventBtn.addEventListener('click', GetEventReports)

    var commentsBtn = document.getElementById('GetCommentaryReports');
    commentsBtn.addEventListener('click', GetCommentaryReports)
}


function CreateInitialHtml() {
    var divContainer = document.getElementsByClassName('container')[1];
    var mainDiv = document.createElement('div');
    mainDiv.setAttribute('class', 'mainDiv');
    divContainer.appendChild(mainDiv);

    var headDiv = document.createElement('div');
    headDiv.setAttribute('class', 'headDiv');
    mainDiv.appendChild(headDiv);
    var headSpanH1 = document.createElement('h1');
    headSpanH1.textContent = "Reports";
    headDiv.appendChild(headSpanH1);

    var buttonDiv = document.createElement('div');
    buttonDiv.setAttribute('class', 'buttonDiv');
    mainDiv.appendChild(buttonDiv);

    var eventReportButton = document.createElement('button');
    eventReportButton.textContent = 'Event Reports';
    eventReportButton.id = 'GetEventReports';
    buttonDiv.appendChild(eventReportButton);

    var commentaryReportBtn = document.createElement('button');
    commentaryReportBtn.id = 'GetCommentaryReports';
    commentaryReportBtn.textContent = "Commentaries Reports";
    buttonDiv.appendChild(commentaryReportBtn);


    var reportDiv = document.createElement('div');
    reportDiv.setAttribute('class', 'reportDiv');
    divContainer.appendChild(reportDiv);

    var reportHeaderDiv = document.createElement('div');
    reportHeaderDiv.setAttribute('class', 'reportHeaderDiv');
    reportDiv.appendChild(reportHeaderDiv);
    var reportDivHead = document.createElement('h1');
    reportDivHead.textContent = "Current Report";
    reportHeaderDiv.appendChild(reportDivHead);

}


function GetEventReports() {
    var xhttp = new XMLHttpRequest();
    xhttp.open('GET', "/Report/GetEventReport", true);
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            // Response
            EventReportHTML(this.responseText);

        }
    };
    xhttp.send();
}

function EventReportHTML(responseText) {
var btnToDisable = document.getElementById('GetEventReports');
btnToDisable.setAttribute('disabled','true');
    var reportDiv = document.getElementsByClassName('reportDiv')[0];
    var tableDiv = document.createElement('div');
    tableDiv.setAttribute("class", 'tableDiv');
    reportDiv.appendChild(tableDiv);
    var table = document.createElement('table');
    tableDiv.appendChild(table);
    var tableHead = document.createElement('thead')
    var tr = document.createElement('tr');

    var reporterUserTd = document.createElement('td');
    reporterUserTd.textContent = 'Reported By:';
    tr.appendChild(reporterUserTd);

    var reasonTd = document.createElement('td');
    reasonTd.textContent = 'Reason';
    tr.appendChild(reasonTd);

    var creatorTd = document.createElement('td');
    creatorTd.textContent = 'Creator';
    tr.appendChild(creatorTd);

    var eventNumberTd = document.createElement('td');
    eventNumberTd.textContent = "Event";
    tr.appendChild(eventNumberTd);
    table.appendChild(tableHead);
    tableHead.appendChild(tr);
    var tableBody = document.createElement('tbody');
    table.appendChild(tableBody);
    var obj = JSON.parse(responseText);
    for (let i = 0; i < obj.length; i++) {

        var currentRow = obj[i];
        var tr = document.createElement('tr');
        tr.id= currentRow.reportId;
        tr.setAttribute('class', 'dataTr');
        tableBody.appendChild(tr);

        var reporterTd = document.createElement('td');
        reporterTd.setAttribute('class', 'dataTd');
        reporterTd.textContent = currentRow.reporterUserUsername;
        tr.appendChild(reporterTd);



        var reason = document.createElement('td');
        reason.setAttribute('class', 'dataTd');
        reason.textContent = currentRow.reason;
        tr.appendChild(reason);

        var eventCreatorTd = document.createElement('td');
        eventCreatorTd.setAttribute('class', 'dataTd');
        eventCreatorTd.textContent = currentRow.reportedUserUsername;
        tr.appendChild(eventCreatorTd);

        var eventTd = document.createElement('td');
        var eventTdA = document.createElement('a');
        eventTd.appendChild(eventTdA);
        eventTdA.href = `/Event/EventView/${currentRow.eventId}`;
        eventTd.setAttribute('class', 'dataTd');

        var iconSpan = document.createElement('button');
        iconSpan.setAttribute('class','deleteButton');
        var icon = document.createElement('i');
        icon.setAttribute('class', 'fas fa-eraser')
        icon.addEventListener('click', DeleteEvent);
        eventTdA.textContent = currentRow.eventId;

        var closeButton = document.createElement('button');
        closeButton.setAttribute('class','closeButton');
        closeButton.textContent='Clear';
        closeButton.addEventListener('click', ClearReport)
        closeButton.Id=currentRow.reportDivHead;
        eventTd.appendChild(closeButton);
        iconSpan.appendChild(icon);
        eventTd.appendChild(iconSpan);


        tr.appendChild(eventTd);


    }
}

function DeleteEvent(ev) {

    var eventId = ev.target.parentNode.parentNode.textContent;
    var tryToRemove=ev.target.parentNode.parentNode.parentNode;
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', "/Report/DeleteEvent", true);
    if (this.readyState == 4 && this.status == 200) {
        tryToRemove.parentNode.removeChild(tryToRemove);
    }
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(eventId);
}

function ClearReport(ev) {
    var trToRemove=ev.target.parentNode.parentNode;
  var reportId=ev.target.parentNode.parentNode.id;
  var xhttp = new XMLHttpRequest();
  xhttp.open('POST', "/Report/ClearEvent", true);
  trToRemove.parentNode.removeChild(trToRemove);
  xhttp.setRequestHeader("Content-Type", "application/json");
  xhttp.send(reportId);
}
function GetCommentaryReports() {
    var btnToDisable = document.getElementById('GetEventReports');
    btnToDisable.removeAttribute('disabled');
  var tableRemove =  document.getElementsByClassName('tableDiv')[0];
  tableRemove.parentNode.removeChild(tableRemove);
}