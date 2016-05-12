// Write your Javascript code.
$(document)
    .ready(function () {
        var selectedNavPillId = $("#selectedNavPillId").attr("selectedNavPillId");
        $(".nav-pills>li")
            .each(function() {
                var pillId = $(this).attr("id");
                if (pillId === selectedNavPillId) {
                    $(this).addClass("active");
                }
            });
    });

