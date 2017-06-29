var optionsView = null;
var canvasContainerGlobal = null;

function renderPDF(pdfDocument, canvasContainer, options) {
    var options = options || { scale: 1 };
    optionsView = options;
    canvasContainerGlobal = canvasContainer;
    PDFJS.disableWorker = true;
    displayPage(1, pdfDocument);
}

function renderPage(page) {
    var viewport = page.getViewport(optionsView.scale);
    var canvas = document.createElement('canvas');
    var ctx = canvas.getContext('2d');
    var renderContext = {
        canvasContext: ctx,
        viewport: viewport
    };

    canvas.height = viewport.height;
    canvas.width = viewport.width;
    canvasContainerGlobal.innerHTML = "";
    canvasContainerGlobal.appendChild(canvas);

    page.render(renderContext);
}

function displayPage(page, pdfDocument) {
    if (pdfDocument) {
        pdfDocument.getPage(page).then(renderPage);
    }
}

function getPdfNumberOfPages(pdfDocument) {
    if (pdfDocument) {
        return pdfDocument.numPages;
    }

}

function viewPdf(pdfDocument, containerId, options) {
    if (!pdfDocument) {
        return;
    }
    renderPDF(pdfDocument, document.getElementById(containerId), options);
}
