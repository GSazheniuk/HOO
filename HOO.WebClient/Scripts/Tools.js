//http://stackoverflow.com/a/8809472/5382796
function generateUUID() {
    var d = new Date().getTime();
    if (window.performance && typeof window.performance.now === "function") {
        d += performance.now();
    }
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}

function getTemplate(template, callback) {
    var x = new XMLHttpRequest();
    x.addEventListener("load", callback);
    x.open("get", template);
    x.send();
}

function showPopup(popupName, code) {
    $('[data-popup="' + popupName + '"]').find("span").empty();
    $('[data-popup="' + popupName + '"]').find("span").append(code);
    $('[data-popup="' + popupName + '"]').fadeIn(350);
}

function closePopup(popupName, nextCall) {
    $('[data-popup="' + popupName + '"]').fadeOut(350);

    if (typeof nextCall === "function")
        $('[data-popup="' + popupName + '"]').queue(nextCall());
}

