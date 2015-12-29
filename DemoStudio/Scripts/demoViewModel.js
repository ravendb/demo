// /// <reference path="knockout.d.ts" />
// /// <reference path="knockout.mapping.d.ts" />
// /// <reference path="require.d.ts" />
var DemoViewModel = (function () {
    function DemoViewModel() {
        var _this = this;
        this.isHtml = ko.observable(false);
        this.htmlView = ko.observable("");
        this.htmlExpl = ko.observable("");
        this.htmlCode = ko.observable("");
        this.values = ko.observable("");
        this.defdemo = ko.observable();
        this.optionsText = ko.observable();
        this.urlstring = ko.observable();
        this.isSimpleJson = ko.observable(false);
        this.columns = ko.observableArray([]);
        this.rows = ko.observableArray([]);
        this.inProgress = ko.observable(false);
        this.chkForceJson = ko.observable(false);
        this.chkForceString = ko.observable(false);
        this.chkAllowFlatten = ko.observable(false);
        this.currentDemoCategory = ko.observable();
        this.demoCategories = ko.observableArray(['']);
        this.isDemoCategorySelected = ko.computed(function () {
            var category = _this.currentDemoCategory();
            return category;
        });
        this.availableDemos = ko.observableArray();
        this.currentDemos = ko.computed(function () {
            var category = _this.currentDemoCategory();
            return _.filter(_this.availableDemos(), function (demo) { return demo.ControllerName === category; });
        });
        $.ajax("/Menu/Index", "GET").done(function (data) {
            var demos = data["Demos"];
            demos.forEach(function (demo) {
                if (_.indexOf(_this.demoCategories(), demo.ControllerName) === -1) {
                    _this.demoCategories.push(demo.ControllerName);
                }
                _this.availableDemos.push(demo);
            });
        }).fail(function () {
            _this.availableDemos.push("Failed to retreive demos");
        });
        var presenter = new DemoViewModelPresenter();
    }
    DemoViewModel.prototype.clickForceJson = function () {
        if (this.chkForceJson() === true) {
            this.chkForceString(false);
        }
        return true;
    };
    DemoViewModel.prototype.clickForceString = function () {
        if (this.chkForceString() == true) {
            this.chkForceJson(false);
        }
        return true;
    };
    DemoViewModel.prototype.runDemo = function () {
        var _this = this;
        $('body').addClass('showResult');
        var url = this.getDemoUrl();
        if (this.values() !== "") {
            url += "?" + this.values();
        }
        this.isHtml(false);
        this.isSimpleJson(false);
        this.inProgress(true);
        $.ajax(url, "GET").done(function (data) {
            _this.inProgress(false);
            console.log(data);
            var jsonObj = data;
            if (_this.chkForceJson() === false && (_this.chkForceString() === true || typeof (data) === "string")) {
                _this.htmlView(data);
                _this.inProgress(false);
                _this.isHtml(true);
                return;
            }
            if (data instanceof Array === false) {
                jsonObj = [data];
            }
            _this.columns([]);
            _this.rows([]);
            for (var i = 0; i < jsonObj.length; i++) {
                var item = jsonObj[i];
                var newItem = {};
                for (var key in item) {
                    if (i === 0)
                        _this.columns.push(key);
                    if (typeof item[key] !== "object") {
                        newItem[key] = item[key];
                    }
                    else {
                        if (_this.chkAllowFlatten() === true) {
                            for (var deeperKey in item[key]) {
                                if (i === 0)
                                    _this.columns.push(deeperKey);
                                if (typeof item[key][deeperKey] !== "object")
                                    newItem[deeperKey] = item[key][deeperKey];
                                else
                                    newItem[deeperKey] = JSON.stringify(item[key][deeperKey]);
                            }
                        }
                        else {
                            newItem[key] = JSON.stringify(item[key]);
                        }
                    }
                }
                _this.rows.push(newItem);
            }
            _this.inProgress(false);
            _this.isSimpleJson(true);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            _this.htmlView('Error Status : ' + jqXHR.status + '<br>' + jqXHR.responseText);
            _this.inProgress(false);
            _this.isHtml(true);
        });
    };
    DemoViewModel.prototype.getCode = function () {
        var _this = this;
        $('body').removeClass('showResult');
        var demoUrl = this.getDemoUrl();
        $.ajax("/Menu/LoadCsFile?Filename=" + demoUrl, "GET").done(function (data) {
            console.log(data);
            _this.htmlCode(data);
            Prism.highlightAll();
        });
        $.ajax("/Menu/LoadCsFile?Docname=" + demoUrl, "GET").done(function (data) {
            console.log(data);
            _this.htmlExpl(data);
        });
    };
    DemoViewModel.prototype.genUrl = function () {
        var url = window.location.href.replace(/\/$/, "") + this.getDemoUrl();
        if (this.values() !== "") {
            url += "?" + this.values();
        }
        this.urlstring(url);
    };
    DemoViewModel.prototype.getDemoUrl = function () {
        var demo = this.defdemo();
        return "/" + demo.ControllerName + "/" + demo.DemoName;
    };
    DemoViewModel.prototype.availableDemoChangeEvent = function () {
        this.values("");
        this.genUrl();
        this.isHtml(false);
        this.isSimpleJson(false);
        this.getCode();
        this.chkForceString(false);
        this.chkForceJson(false);
        this.chkAllowFlatten(false);
    };
    DemoViewModel.prototype.valuesKeyPressEvent = function (data, event) {
        this.genUrl();
        return true;
    };
    DemoViewModel.prototype.openNewTab = function () {
        window.open(this.urlstring(), '_blank');
    };
    return DemoViewModel;
})();
