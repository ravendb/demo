// /// <reference path="knockout.d.ts" />
// /// <reference path="require.d.ts" />


declare var ko;
declare var availableDemos;

class demoViewModel {
    availableDemos = ko.observableArray(["Choose Demo..."]);
    values = ko.observable("");
    defdemo = ko.observable();
    optionsText = ko.observable();
    urlstring = ko.observable();

    constructor() {
        var selfAvailableDemos = this.availableDemos;
        $.ajax("/Menu/Index", "GET").done(data => {
            var listOfControllers = data["ListOfControllers"];

            listOfControllers.forEach(function(entry) {
                selfAvailableDemos.push(entry);
            });
        }).fail(() => {
            selfAvailableDemos.push("Failed to retreive demos");
        });
    }
}


function runDemo(): void {
    var url = this.defdemo();
    if (this.values() !== "") {
        url += "?" + this.values();
    }
    $.ajax(url, "GET").done(data => {
        console.log(data);
        
    });
}

function genUrl(): void {
    
    var url = window.location.href.replace(/\/$/, "") + this.defdemo();
    if (this.values() !== "") {
        url += "?" + this.values();
    }
    this.urlstring(url);
}








