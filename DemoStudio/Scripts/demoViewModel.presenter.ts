class DemoViewModelPresenter {
    $body = $('body');

    constructor() {
        $("#leftCollapse").click(this.leftCollapse.bind(this));
        $("#moreCode").click(this.toggleCode.bind(this));
        $("#moreResult").click(this.toggleCode.bind(this));
        $("#fullResult").click(this.expandResults.bind(this));
    }

    leftCollapse(): void {
        if (this.$body.hasClass('collapse-left')) {
            this.$body.removeClass('collapse-left');
        } else {
            this.$body.addClass('collapse-left');
        }
    }

    showCode(): void {
        this.$body.removeClass('showResult');
    }

    toggleCode(): void {
        if (this.$body.hasClass('showResult')) {
            this.$body.removeClass('showResult');
        } else {
            this.$body.addClass('showResult');
        }
    }

    showResults(): void {
        this.$body.addClass('showResult');
    }

    expandResults(): void {
        if ($('#resultsDiv').hasClass('showFull')) {
            $('#resultsDiv').removeClass('showFull');
        } else {
            $('#resultsDiv').addClass('showFull');
        }
    }
}











