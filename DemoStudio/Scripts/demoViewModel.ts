// /// <reference path="knockout.d.ts" />
// /// <reference path="knockout.mapping.d.ts" />
// /// <reference path="require.d.ts" />

declare var ko;
declare var Prism;
declare var _;

class DemoViewModel {
    isHtml = ko.observable(false);
    htmlView = ko.observable("");
    htmlExpl = ko.observable("");
    htmlCode = ko.observable("");
    currentDemo = ko.observable();
    optionsText = ko.observable();
    urlstring = ko.observable();
    isSimpleJson = ko.observable(false);
    columns = ko.observableArray([]);
    rows = ko.observableArray([]);
    inProgress = ko.observable(false);

    presenter: DemoViewModelPresenter = new DemoViewModelPresenter();

    currentDemoCategory = ko.observable();
    demoCategories = ko.observableArray(['']);
    isDemoCategorySelected = ko.computed(() => {
        var category = this.currentDemoCategory();
        return category;
    });

    availableDemos = ko.observableArray();
    currentDemos = ko.computed(() => {
        var category = this.currentDemoCategory();

        return _.filter(this.availableDemos(), demo => demo.ControllerName === category);
    });

    currentDemoParameters = ko.observableArray();

    constructor() {
        this.currentDemo.subscribe(value => {
            this.reset();

            this.setDemoParameters(value);
        });

        $.ajax("/Menu/Index", "GET").done(data => {
            var demos = data["Demos"];

            demos.forEach(demo => {
                if (_.indexOf(this.demoCategories(), demo.ControllerName) === -1) {
                    this.demoCategories.push(demo.ControllerName);
                }

                this.availableDemos.push(demo);
            });
        }).fail(() => {
            this.availableDemos.push("Failed to retreive demos");
        });
    }

    runDemo(): void {
        this.presenter.showResults();

        var currentDemo = this.currentDemo();

        var url = this.getDemoUrl();
        url += this.getQueryString();

        this.isHtml(false);
        this.isSimpleJson(false);
        this.inProgress(true);
        $.ajax(url, "GET").done(data => {
            this.inProgress(false);
            console.log(data);

            var jsonObj = data;

            if (currentDemo.DemoOutputType === 'String') {
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
                        newItem[key] = item[key];
                    } else {
                        if (currentDemo.DemoOutputType === 'Flatten') {
                            for (var deeperKey in item[key]) {
                                if (i === 0)
                                    this.columns.push(deeperKey);
                                if (typeof item[key][deeperKey] !== "object")
                                    newItem[deeperKey] = item[key][deeperKey];
                                else
                                    newItem[deeperKey] = JSON.stringify(item[key][deeperKey]);
                            }
                        } else {
                            newItem[key] = JSON.stringify(item[key]);
                        }
                    }
                }
                this.rows.push(newItem);
            }
            this.inProgress(false);
            this.isSimpleJson(true);
        }).fail((jqXHR, textStatus, errorThrown) => {
            this.htmlView('Error Status : ' + jqXHR.status + '<br>' + jqXHR.responseText);
            this.inProgress(false);
            this.isHtml(true);
        });
    }

    getCode(): void {
        this.presenter.showCode();
        var demoUrl = this.getDemoUrl();
        $.ajax("/Menu/LoadCsFile?Filename=" + demoUrl, "GET").done(data => {
            console.log(data);
            this.htmlCode(data);
            Prism.highlightAll();
        });
        $.ajax("/Menu/LoadCsFile?Docname=" + demoUrl, "GET").done(data => {
            console.log(data);
            this.htmlExpl(data);
        });
    }

    genUrl(): void {
        var url = window.location.href.replace(/\/$/, "") + this.getDemoUrl();
        url += this.getQueryString();

        this.urlstring(url);
    }

    getQueryString(): string {
        var queryString = '';
        var parameters = this.currentDemoParameters();
        var firstParameter = true;

        parameters.forEach(parameter => {
            var value = parameter.ParameterValue();
            if (value) {
                var parameterQueryString = parameter.ParameterName + "=" + value;

                if (firstParameter) {
                    firstParameter = false;
                    queryString += "?" + parameterQueryString;
                } else {
                    queryString += "&" + parameterQueryString;
                }
            }
        });

        return queryString;
    }

    getDemoUrl(): string {
        var demo = this.currentDemo();
        return "/" + demo.ControllerName + "/" + demo.DemoName;
    }

    openNewTab(): void {
        window.open(this.urlstring(), '_blank');
    }

    reset(): void {
        this.genUrl();
        this.isHtml(false);
        this.isSimpleJson(false);
        this.getCode();
    }

    setDemoParameters(demo) {
        this.currentDemoParameters([]);
        var parameters = demo.DemoParameters;
        parameters.forEach(parameter => {
            var demoParameter = {
                ParameterName: parameter.ParameterName,
                ParameterType: parameter.ParameterType,
                ParameterIsRequired: parameter.IsRequired,
                ParameterValue: ko.observable()
            };

            demoParameter.ParameterValue.subscribe(() => {
                this.genUrl();
            });

            this.currentDemoParameters.push(demoParameter);
        });
    }
}











