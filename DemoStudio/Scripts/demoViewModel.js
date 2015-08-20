// demoViewModel.js

function DemoViewModel() {
    var self = this;

   // self.defdemo = ko.observable("BBB");

    // alert("View model is a live!!");
    // self.demoname = ko.observable(["Optimal"]);
    // self.availableDemos = ko.observableArray(["Optimal", "Normal", "Slow", "Hiccups", "Dropping", "Down"]);
    // self.availableDemos = [
        // { demoname: "AAA" }
    self.availableDemos = ko.observableArray( ["Choose Demo..."] );
    self.values = ko.observable("");
    self.defdemo = ko.observable();
    self.optionsText = ko.observable();

    self.getDemos = function () {
        var newDataRequest = $.ajax({
            url: "/Menu/Index",
            timeout: 30000
        });

        newDataRequest.done(function (data) {
            var listOfControllers = data["ListOfControllers"];

            listOfControllers.forEach(function (entry) {
                self.availableDemos.push(entry);
            });

        });
    }

    self.getDemos();

    self.runDemo = function() {
        var newDataRequest = $.ajax({
            url: self.defdemo(),
            timeout: 30000,
            data: self.values
        });

        newDataRequest.done(function (data) {
            console.log(data);

            });
    }
}

ko.applyBindings(new DemoViewModel(), document.getElementById("demoViewModel"));


