// Write your Javascript code.
$(document)
    .ready(function () {
        setActivePill();
        updateProgressBar();
    });

function setActivePill() {
    var selectedNavPillId = $("#selectedNavPillId").attr("selectedNavPillId");
    $(".nav-pills>li")
        .each(function () {
            var pillId = $(this).attr("id");
            if (pillId === selectedNavPillId) {
                $(this).addClass("active");
            }
        });
};

function updateProgressBar() {
    var numberOfCompleted = $(".nav-pills>li>a>i.fa-check-circle").length;
    var numberOfNonCompleted = $(".nav-pills>li>a>i.fa-minus-circle").length;
    var total = numberOfCompleted + numberOfNonCompleted;
    var percentageString = (numberOfCompleted/total * 100) + "%";

    $("#pillProgressBar").css("width", percentageString);
    $("#pillProgressBar").text(percentageString);
};
