// /// <reference path="knockout.d.ts" />
// /// <reference path="knockout.mapping.d.ts" />
// /// <reference path="require.d.ts" />
var demoViewModel = (function () {
    function demoViewModel() {
        this.isHtml = ko.observable(true);
        this.htmlView = ko.observable("x");
        this.availableDemos = ko.observableArray(["Choose Demo..."]);
        this.values = ko.observable("");
        this.defdemo = ko.observable();
        this.optionsText = ko.observable();
        this.urlstring = ko.observable();
        this.isSimpleJson = ko.observable(true);
        /*columns = ko.observableArray([{ header: "A", dataMember: "B" }, { header: "C", dataMember: "D" }]);
        rows = ko.observableArray([{ header: "A", dataMember: "B" }, { header: "C", dataMember: "D" }]);*/
        this.columns = ko.observableArray([]);
        this.rows = ko.observableArray([]);
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
    var _this = this;
    var url = this.defdemo();
    if (this.values() !== "") {
        url += "?" + this.values();
    }
    $.ajax(url, "GET").done(function (data) {
        console.log(data);
        var jsonObj = data;
        if (typeof (data) === "string") {
            _this.htmlView(data);
            return;
        }
        if (data instanceof Array === false) {
            jsonObj = [data];
        }
        //var newArr = [];
        _this.columns([]);
        _this.rows([]);
        for (var i = 0; i < jsonObj.length; i++) {
            var item = jsonObj[i];
            var newItem = {};
            for (var key in item) {
                if (i === 0)
                    _this.columns.push(key);
                if (typeof item[key] !== "object") {
                    newItem[key] = item[key]; // copy the new item
                }
                else {
                    for (var deeperKey in item[key]) {
                        _this.columns.push(deeperKey);
                        // var obj = {};
                        // obj[deeperKey] = item[key][deeperKey];
                        // itemsToBeCreated.push(obj);
                        newItem[deeperKey] = item[key][deeperKey];
                    }
                }
            }
            _this.rows.push(newItem);
        }
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