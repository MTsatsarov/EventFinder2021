function OnLoad() {
    var divContainer = document.getElementsByClassName('container')[1];
console.log(divContainer);
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
    buttonDiv.appendChild(eventReportButton);

    var commentaryReportBtn = document.createElement('button');
    commentaryReportBtn.textContent = "Commentaries Reports";
    buttonDiv.appendChild(commentaryReportBtn);

    var reportDiv = document.createElement('div');
    reportDiv.setAttribute('class', 'reportDiv');
    divContainer.appendChild(reportDiv);

    var reportHeaderDiv = document.createElement('div');
    reportHeaderDiv.setAttribute('class','reportHeaderDiv');
     reportDiv.appendChild(reportHeaderDiv);
    var reportDivHead = document.createElement('h1');
    reportDivHead.textContent="Current Report";
    reportHeaderDiv.appendChild(reportDivHead);

}