class DemoViewModelPresenter {
    $body = $('body');

    constructor() {
        $("#leftCollapse").click(this.leftCollapse.bind(this));
        $("#moreCode").click(this.toggleCode.bind(this));
        $("#moreResult").click(this.toggleCode.bind(this));
        $("#compactResults").change(this.expandResults.bind(this));
        $('#cbp-qtrotator').cbpQTRotator();
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
        var $compactResults = $("#compactResults");
        var isChecked = $compactResults.is(':checked');
        var $resultsDiv = $('#resultsDiv');
        if (isChecked) {
            $compactResults.prop('checked', true);
            $resultsDiv.removeClass('showFull');
        } else {
            $compactResults.prop('checked', false);
            $resultsDiv.addClass('showFull');
        }
    }
}











