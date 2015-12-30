var DemoViewModelPresenter = (function () {
    function DemoViewModelPresenter() {
        this.$body = $('body');
        $("#leftCollapse").click(this.leftCollapse.bind(this));
        $("#moreCode").click(this.toggleCode.bind(this));
        $("#moreResult").click(this.toggleCode.bind(this));
        //$("#fullResult").click(this.expandResults.bind(this));
        $("#trimResults").change(this.expandResults.bind(this));
    }
    DemoViewModelPresenter.prototype.leftCollapse = function () {
        if (this.$body.hasClass('collapse-left')) {
            this.$body.removeClass('collapse-left');
        }
        else {
            this.$body.addClass('collapse-left');
        }
    };
    DemoViewModelPresenter.prototype.showCode = function () {
        this.$body.removeClass('showResult');
    };
    DemoViewModelPresenter.prototype.toggleCode = function () {
        if (this.$body.hasClass('showResult')) {
            this.$body.removeClass('showResult');
        }
        else {
            this.$body.addClass('showResult');
        }
    };
    DemoViewModelPresenter.prototype.showResults = function () {
        this.$body.addClass('showResult');
    };
    DemoViewModelPresenter.prototype.expandResults = function () {
        if ($('#resultsDiv').hasClass('showFull')) {
            $('#resultsDiv').removeClass('showFull');
        }
        else {
            $('#resultsDiv').addClass('showFull');
        }
    };
    return DemoViewModelPresenter;
})();
//# sourceMappingURL=demoViewModel.presenter.js.map