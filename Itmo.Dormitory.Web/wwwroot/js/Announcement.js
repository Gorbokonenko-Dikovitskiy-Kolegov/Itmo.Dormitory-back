var data;
var currentlyShowed;

$(document).ready(function () {
    for (var i = 0; i < data.length; i++) {
        data[i]['creationTime'] = new Date(data[i]['creationTime']);
    }
    currentlyShowed = data.slice();
    updateAnnouncements(data);
    document.getElementById('search').addEventListener('input', search);


    document.querySelector('#new-first').onchange = function () { sortByTime(this.value); };

});


function search(e) {
    text = e.target.value;
    var tmp = currentlyShowed.splice();
    for (var i = 0; i < data.length; i++) {
        isTextInTitle = data[i]['title'].indexOf(text) != -1;
        isTextInContent = data[i]['content'].indexOf(text) != -1;
        if (isTextInTitle || isTextInContent) {
            tmp.push(data[i])

        }
        else {
            tmp.splice(i, i);
        }
    }
    currentlyShowed = tmp;
    draw();
}

function hideAll() {
    for (var i = 0; i < data.length; i++) {
        document.getElementById('announcements__item_' + data[i]['id']).hidden = true;
    }
}

function draw() {
    hideAll();
    for (var i = 0; i < currentlyShowed.length; i++) {
        document.getElementById('announcements__item_' + currentlyShowed[i]['id']).hidden = false;
    }
}

function sortByTime(value) {
    if (value == 0)
        TimeAscending();
    else
        TimeDescending();
}
function TimeDescending() {
    document.getElementsByClassName('announcements__list')[0].style['flex-direction'] = 'column-reverse';
}

function TimeAscending() {
    document.getElementsByClassName('announcements__list')[0].style['flex-direction'] = 'column';
}
function getRandom() {
    // 16777215 (decimal) == ffffff in hexidecimal
    var newColor = '#' + Math.floor(Math.random() * 16777215).toString(16);
    return newColor;
};

function paint() {
    var items = document.getElementsByClassName("announcements__item ");
    for (let i = 0; i < items.length; i++) {
        items[i].style.borderColor = "#888787 #888787 #888787 " + getRandom()
    };
}


function renderAnnouncements(data, html_sample, className) {
    announcements = document.getElementsByClassName(className)[0];
    for (var i = 0; i < data.length; i++) {
        announcements.innerHTML += getHTML(data[i], html_sample);
    }
}

function clearAnnouncements(className) {
    document.getElementsByClassName(className)[0].innerHTML = "";
}

function updateAnnouncements(data, className = 'announcements__list') {
    html_sample = document.getElementsByClassName(className)[0].innerHTML
    clearAnnouncements(className);
    renderAnnouncements(data, html_sample, className);
    paint();
}
function getHTML(announcement, html_sample) {
    fields = html_sample.match(/\$__(.*)__\$/gi)

    new_html = html_sample;

    for (var i = 0; i < fields.length; i++) {
        field = fields[i].replace("$__", '').replace("__$", '')
        new_html = new_html.replace("$__" + field + "__$", announcement[field]);
    }
    return new_html;
}
