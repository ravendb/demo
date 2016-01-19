var DemoViewModelPresenter = (function () {
    function DemoViewModelPresenter() {
        this.$body = $('body');
        $("#leftCollapse").click(this.leftCollapse.bind(this));
        $("#moreCode").click(this.toggleCode.bind(this));
        $("#moreResult").click(this.toggleCode.bind(this));
        $("#compactResults").change(this.expandResults.bind(this));
        $('#cbp-qtrotator').cbpQTRotator({ interval: 10000 });
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
        var $compactResults = $("#compactResults");
        var isChecked = $compactResults.is(':checked');
        var $resultsDiv = $('#resultsDiv');
        if (isChecked) {
            $compactResults.prop('checked', true);
            $resultsDiv.removeClass('showFull');
        }
        else {
            $compactResults.prop('checked', false);
            $resultsDiv.addClass('showFull');
        }
    };
    return DemoViewModelPresenter;
})();
