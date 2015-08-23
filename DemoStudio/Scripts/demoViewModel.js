// /// <reference path="knockout.d.ts" />
// /// <reference path="require.d.ts" />
var demoViewModel = (function () {
    function demoViewModel() {
        this.availableDemos = ko.observableArray(["Choose Demo..."]);
        this.values = ko.observable("");
        this.defdemo = ko.observable();
        this.optionsText = ko.observable();
        this.urlstring = ko.observable();
        var selfAvailableDemos = this.availableDemos;
        $.ajax("/Menu/Index", "GET").done(function (data) {
            var listOfControllers = data["ListOfControllers"];
            listOfControllers.forEach(function (entry) {
                selfAvailableDemos.push(entry);
            });
        }).fail(function () {
            selfAvailableDemos.push("Failed to retreive demos");
        });
    }
    return demoViewModel;
})();
function runDemo() {
    var url = this.defdemo();
    if (this.values() !== "") {
        url += "?" + this.values();
    }
    $.ajax(url, "GET").done(function (data) {
        console.log(data);
    });
}
function genUrl() {
    var url = window.location.href.replace(/\/$/, "") + this.defdemo();
    if (this.values() !== "") {
        url += "?" + this.values();
    }
    this.urlstring(url);
}
//# sourceMappingURL=demoViewModel.js.map