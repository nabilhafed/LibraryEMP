$("document").ready(function () {

    //database status 
    $("#connectBTN").click(function (e) {
        e.preventDefault();

        $(this).prop('disabled', true);
        $(this).find("span").removeClass('d-none');

        let databaseUsername = $("#databaseUsername").val();
        let databasePassword = $("#databasePassword").val();

        let that = this;
        $.get($(that).data('request-url'), { username: databaseUsername, password: databasePassword },
            function (response) {
                if (response == true) {

                    $(that).prop('disabled', false);
                    $(that).find("span").addClass('d-none');

                    $("#serverStatus").removeClass("alert-danger");
                    $("#serverStatus").addClass("alert-success");
                    $("#serverStatus").find("span").html("online");

                    $("#databaseConnectionCloseBTN").click();

                    $("#Page-content").removeClass("disabled");


                    $('#pageContentAlert').find("div").removeClass("animate__zoomIn").addClass('animate__zoomOut').one('animationend', function () {
                        $("#pageContentAlert").remove();
                    });


                } else {
                    $("#databaseConnectionAlert").removeClass("d-none");

                    $(that).prop('disabled', false);
                    $(that).find("span").addClass('d-none');
                }
            }
        );
    });

    //on page load animation for the navbar 
    let link = $('a[href="/' + window.location.href.substring(window.location.href.lastIndexOf('/') + 1) + '"]');

    $(link).attr("class", "nav-link active");
    $(link).addClass("animate__animated  animate__bounceIn").one('animationend', function () {
        $(link).removeClass("animate__animated  animate__bounceIn");
    });
    $('#pageContentAlert').find("div").removeClass("animate__zoomIn").removeClass("animate__zoomIn").addClass('animate__rubberBand').one('animationend', function () {
        $(link).removeClass("animate__rubberBand");
    });

    // on page develepment phase !! , please remove it later 
    $("#Page-content").removeClass("disabled");
    $("#serverStatus").removeClass("alert-danger");
    $("#serverStatus").addClass("alert-success");
    $("#serverStatus").find("span").html("online");
    $("#pageContentAlert").remove();
    // $.get("/Home/pageManager", { page: "gestion_de_documents" },
    //     function (response) {
    //         $("#Page-content").html(response);
    //     }
    // );
});