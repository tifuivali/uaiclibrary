var cachedJSDfd = null;
function loadWorkerURL(url) {
    cachedJSDfd = null;
    if (cachedJSDfd) { return cachedJSDfd; }
    cachedJSDfd = PDFJS.createPromiseCapability();
    var xmlhttp;
    xmlhttp = new XMLHttpRequest();

    //the callback function to be callled when AJAX request comes back
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var workerJSBlob = new Blob([xmlhttp.responseText], {
                type: "text/javascript"
            });
            cachedJSDfd.resolve(window.URL.createObjectURL(workerJSBlob));
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
    return cachedJSDfd.promise;
}


function initWebWorker() {
    return loadWorkerURL('http://localhost:4200/src/app/js/pdf.worker.js').then(function (blob) {
        PDFJS.workerSrc = blob;
        return PDFJS;
    });
}


function openPdf(url) {
    return initWebWorker().then(function (PDFJS) {
        return PDFJS.getDocument(url);
    });
}