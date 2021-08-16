function Hover() {

    [...document.getElementsByName('eventCards')].forEach(x => {
        x.addEventListener('mouseover', onHover);
        x.addEventListener('mouseout', onMouseOut)
    })
}
function onHover(ev) {
    var element = ev.target;
    if (element.tagName == 'IMG' || element.tagName == 'A' || element.tagName == 'P') {
        element.parentNode.parentNode.setAttribute('style', 'background-color:gray;');
    }

    else {
        if (ev.target) {      
        }
        if (ev.target.name != 'eventCards') {
            element.parentNode.setAttribute('style', 'background-color:gray;');
        }


    }

    var div = document.getElementsByClassName('row')[0]
    div.setAttribute('style', 'background-color:none');
}
function onMouseOut(ev) {
    var element = ev.target;
    if (element.tagName == 'IMG' || element.tagName == 'A' || element.tagName == 'P') {
        element.parentNode.parentNode.setAttribute('style', 'background-color:none');
    }
    else {
        element.parentNode.setAttribute('style', 'background-color:none');
    }
    var div = document.getElementsByClassName('row')[0]
    div.setAttribute('style', 'background-color:none');
}