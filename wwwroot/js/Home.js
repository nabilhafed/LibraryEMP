$("document").ready(function () {

    //swap between Pages
    $(".nav a").click(function (event) {

        event.preventDefault();
        $.get($(this).data('request-url'), { page: $(this).attr("href") },
            function (response) {

                //let $clonedDiv = $('#Page-content').clone();
                //$clonedDiv.removeClass('h-100');
                //$clonedDiv.css({
                //    position: 'absolute',
                //    top: 8,
                //    height: $('#Page-content').height()
                //});

                //$("#Page-content").parent().append($clonedDiv);
                //$clonedDiv.addClass('animate__slideOutLeft').one('animationend', function () {
                //    $(this).remove();
                //});

                //$("#Page-content").html(response).addClass('animate__slideInRight').one('animationend', function () {
                //    $(this).removeClass('animate__slideInRight');
                //});
                $("#Page-content").html(response);
            }
        );

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
        let databaseName = $("#databaseName").val();
        let databaseHost = $("#databaseHost").val();
        let databasePort = $("#databasePort").val();

        let that = this;
        $.get($(that).data('request-url'), { databaseName: databaseName, host: databaseHost, port: databasePort, username: databaseUsername, password: databasePassword },
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

});