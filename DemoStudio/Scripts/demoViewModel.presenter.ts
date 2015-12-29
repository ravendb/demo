class DemoViewModelPresenter {
    constructor() {
        $("#leftCollapse").click(function () {
            if ($('body').hasClass('collapse-left')) {
                $('body').removeClass('collapse-left');
            } else {
                $('body').addClass('collapse-left');
            }
        });

        $("#moreCode").click(function () {
            if ($('body').hasClass('showResult')) {
                $('body').removeClass('showResult');
            } else {
                $('body').addClass('showResult');
            }
        });

        $("#moreResult").click(function () {
            if ($('body').hasClass('showResult')) {
                $('body').removeClass('showResult');
            } else {
                $('body').addClass('showResult');
            }
        });

        $("#fullResult").click(function () {
            if ($('#resultsDiv').hasClass('showFull')) {
                $('#resultsDiv').removeClass('showFull');
            } else {
                $('#resultsDiv').addClass('showFull');
            }
        });
    }
}











