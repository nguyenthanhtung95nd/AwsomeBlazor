// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

var exJsInterop = {};

exJsInterop.setDocumentTitle = function (title) {
    document.title = title;
}

exJsInterop.getWindowSize = function () {
    var size = {
        width: window.innerWidth,
        height: window.innerHeight
    };
    return size;
}

exJsInterop.registerResizeHandler = function (dotNetObjectRef) {
    function resizeHandler() {
        dotNetObjectRef.invokeMethodAsync('GetWindowSize',
            {
                width: window.innerWidth,
                height: window.innerHeight
            });
    };

    resizeHandler();

    window.addEventListener("resize", resizeHandler);
}

exJsInterop.setLocalStorage = function (key, data) {
    localStorage.setItem(key, data);
}

exJsInterop.getLocalStorage = function (key) {
    return localStorage.getItem(key);
}