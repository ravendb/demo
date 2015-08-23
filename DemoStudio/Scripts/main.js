 requirejs.config({
    paths: {
        //durandal: '../Scripts/durandal',
    }
});

define('jquery', function() { return jQuery; });
define('knockout', ko);
// define('mapping', ko.mapping);
define(["require", "exports", '/studio/scripts/demoViewModel.js'], function (a, b, model) {
    var vm = new demoViewModel();
    ko.applyBindings(vm, document.getElementById("demoViewModel"));
});
