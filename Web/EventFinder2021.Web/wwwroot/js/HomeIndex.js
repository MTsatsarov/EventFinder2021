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
        element.parentNode.setAttribute('style', 'background-color:gray;');
    }



}
function onMouseOut(ev) {
    var element = ev.target;
    if (element.tagName == 'IMG' || element.tagName == 'A' || element.tagName == 'P') {
        element.parentNode.parentNode.setAttribute('style', 'background-color:none');
    }
    else {
        element.parentNode.setAttribute('style', 'background-color:none');
    }

}