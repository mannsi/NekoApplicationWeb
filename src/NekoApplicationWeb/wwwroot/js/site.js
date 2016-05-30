// Write your Javascript code.
$(document)
    .ready(function() {
        setActivePill();
        updateProgressBar();

        $(".currencyInput")
            .focusout(function() {
                setCurrencyNumber(this);
            });

        $(".currencyInput")
            .focus(function () {
                unformatCurrencyNumber(this);
        });

        $(".currencyInput").each(function () {
            setCurrencyNumber(this);
        });

    });


function setActivePill() {
    var selectedNavPillId = $("#selectedNavPillId").attr("selectedNavPillId");
    $(".nav-pills>li")
        .each(function () {
            var pillId = $(this).attr("id");
            if (pillId === selectedNavPillId) {
                $(this).addClass("active");
                $(this).children('a:first').children('i:first').remove();
            }
        });
};

function updateProgressBar() {
    var numberOfCompleted = $(".nav-pills>li>a>i.fa-check-circle").length;
    var numberOfNonCompleted = $(".nav-pills>li>a>i.fa-minus-circle").length;
    var total = numberOfCompleted + numberOfNonCompleted;
    var percentageString = (numberOfCompleted/total * 100) + "%";

    $("#pillProgressBar").css("width", percentageString);
    $("#pillProgressBar").text(numberOfCompleted + '/' + total);
};

function setCurrencyNumber(currencyInput) {
    var unformattedNumber = $(currencyInput).val();
    var formattedNumber = unformattedNumber.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
    $(currencyInput).val(formattedNumber);
}

function unformatCurrencyNumber(currencyInput) {
    var formattedNumber = $(currencyInput).val();
    var unformattedNumber = formattedNumber.replace(/\./g, "");
    $(currencyInput).val(unformattedNumber);
}