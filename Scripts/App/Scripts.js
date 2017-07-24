
(function dataDashProcessing() {
    $('[data-val-display]').each(function () {
        $(this).css('display', $(this).attr('data-val-display'));
    });
})();

function onRequestBegin() {
    alert("Request Begin");
}

function onRequestComplete() {
    alert("onRequestComplete");
}

function onRequestSuccess() {
    alert("onRequestSuccess");
    var btn = document.getElementById("toggler");
    btn.click();
}

function onRequestFailure() {
    alert("onRequestFailure");
}

function deleteOnBegin() {
    //alert("deleteOnBegin");
}

function deleteOnComplete() {
    //alert("deleteOnComplete");
}

function deleteOnFailure() {
    alert("deleteOnFailure");
}

function deleteOnSuccess() {
    //alert("deleteOnSuccess");
    $('#modal-teacher-details').modal('show');
}
