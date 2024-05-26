$("document").ready(function () {

    let pageHistory = {};
    //swap between Pages
    $(".nav a").click(function (event) {

        event.preventDefault();

        //if ($(this).attr("href") in pageHistory) {
        //    $("#Page-content").html(pageHistory[$(this).attr("href")]);
        //} else {
            let that = this;
            $.get($(this).data('request-url'), { page: $(this).attr("href") },
                function (response) {
                    pageHistory[$(that).attr("href")] = response;
                    $("#Page-content").html(response);
                }
            );
        //}

        //left nav switch effect
        $(".active").attr("class", "nav-link link-dark");
        $(this).attr("class", "nav-link active");

        $(this).addClass("animate__animated  animate__bounceIn").one('animationend', function () {
            $(this).removeClass("animate__animated  animate__bounceIn");
        });

        $('#pageContentAlert').find("div").removeClass("animate__zoomIn").removeClass("animate__zoomIn").addClass('animate__rubberBand').one('animationend', function () {
            $(this).removeClass("animate__rubberBand");
        });

    });

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



    // on page develepment phase !! , please remove it later 
    $("#Page-content").removeClass("disabled");
    $("#pageContentAlert").remove();
    $.get("/Home/pageManager", { page: "gestion_de_documents" },
        function (response) {
            $("#Page-content").html(response);
        }
    );
});