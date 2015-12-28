// /// <reference path="knockout.d.ts" />
// /// <reference path="knockout.mapping.d.ts" />
// /// <reference path="require.d.ts" />
$("#leftCollapse").click(function () {
    if ($('body').hasClass('collapse-left')) {
        $('body').removeClass('collapse-left');
    }
    else {
        $('body').addClass('collapse-left');
    }
});
$("#moreCode").click(function () {
    if ($('body').hasClass('showResult')) {
        $('body').removeClass('showResult');
    }
    else {
        $('body').addClass('showResult');
    }
});
$("#moreResult").click(function () {
    if ($('body').hasClass('showResult')) {
        $('body').removeClass('showResult');
    }
    else {
        $('body').addClass('showResult');
    }
});
$("#fullResult").click(function () {
    if ($('#resultsDiv').hasClass('showFull')) {
        $('#resultsDiv').removeClass('showFull');
    }
    else {
        $('#resultsDiv').addClass('showFull');
    }
});
var demoViewModel = (function () {
    function demoViewModel() {
        this.isHtml = ko.observable(false);
        this.htmlView = ko.observable("");
        this.htmlExpl = ko.observable("");
        this.htmlCode = ko.observable("");
        this.availableDemos = ko.observableArray(["Choose Demo..."]);
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
    demoViewModel.prototype.clickForceJson = function () {
        if (this.chkForceJson() === true) {
            this.chkForceString(false);
        }
        return true;
    };
    demoViewModel.prototype.clickForceString = function () {
        if (this.chkForceString() == true) {
            this.chkForceJson(false);
        }
        return true;
    };
    demoViewModel.prototype.runDemo = function () {
        var _this = this;
        var url = this.defdemo();
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
    demoViewModel.prototype.getCode = function () {
        var _this = this;
        var selDemo = this.defdemo();
        $.ajax("/Menu/LoadCsFile?Filename=" + this.defdemo(), "GET").done(function (data) {
            console.log(data);
            _this.htmlCode(data);
            Prism.highlightAll();
        });
        $.ajax("/Menu/LoadCsFile?Docname=" + this.defdemo(), "GET").done(function (data) {
            console.log(data);
            _this.htmlExpl(data);
        });
    };
    demoViewModel.prototype.genUrl = function () {
        var url = window.location.href.replace(/\/$/, "") + this.defdemo();
        if (this.values() !== "") {
            url += "?" + this.values();
        }
        this.urlstring(url);
    };
    demoViewModel.prototype.availableDemoChangeEvent = function () {
        this.values("");
        this.genUrl();
        this.isHtml(false);
        this.isSimpleJson(false);
        this.getCode();
        this.chkForceString(false);
        this.chkForceJson(false);
        this.chkAllowFlatten(false);
    };
    demoViewModel.prototype.valuesKeyPressEvent = function (data, event) {
        this.genUrl();
        return true;
    };
    demoViewModel.prototype.openNewTab = function () {
        window.open(this.urlstring(), '_blank');
    };
    return demoViewModel;
})();
//# sourceMappingURL=demoViewModel.js.map