// /// <reference path="knockout.d.ts" />
// /// <reference path="knockout.mapping.d.ts" />
// /// <reference path="require.d.ts" />


declare var ko;
declare var availableDemos;

class demoViewModel {
    isHtml = ko.observable(false);
    htmlView = ko.observable("");
    availableDemos = ko.observableArray(["Choose Demo..."]);
    values = ko.observable("");
    defdemo = ko.observable();
    optionsText = ko.observable();
    urlstring = ko.observable();
    isSimpleJson = ko.observable(false);
    columns = ko.observableArray([]);
    rows = ko.observableArray([]);
    inProgress = ko.observable(false);
    

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

    this.isHtml(false);
    this.isSimpleJson(false);
    this.inProgress(true);
    $.ajax(url, "GET").done(data => {
        this.inProgress(false);
        console.log(data);

        var jsonObj = data;

        if (typeof (data) === "string") {
            this.htmlView(data);
            this.inProgress(false);
            this.isHtml(true);
            return;
        }

        if (data instanceof Array === false) {
            jsonObj = [data];
        }
        
        this.columns([]);
        this.rows([]);

        for (var i = 0; i < jsonObj.length; i++) {

            var item = jsonObj[i];
            var newItem = {};        

            for (var key in item) {
                if (i === 0)
                    this.columns.push(key);

                if (typeof item[key] !== "object") {
                    newItem[key] = item[key]; // copy the new item
                } else {
                    for (var deeperKey in item[key]) {
                        this.columns.push(deeperKey);
                        newItem[deeperKey] = item[key][deeperKey];
                    }
                }
            }
            this.rows.push(newItem);
        }
        this.inProgress(false);
        this.isSimpleJson(true);
    });
}




function genUrl(): void {
    
    var url = window.location.href.replace(/\/$/, "") + this.defdemo();
    if (this.values() !== "") {
        url += "?" + this.values();
    }
    this.urlstring(url);
}








