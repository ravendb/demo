requirejs.config({
    paths: {
    }
});

define('jquery', function () { return jQuery; });
define('knockout', ko);
define('lodash', _);

define(["require", "exports", '/studio/scripts/demoViewModel.presenter.js', '/studio/scripts/demoViewModel.js'], function (a, b, presenter, model) {
    var vm = new DemoViewModel();
    ko.applyBindings(vm, document.getElementById("demoViewModel"));
});
