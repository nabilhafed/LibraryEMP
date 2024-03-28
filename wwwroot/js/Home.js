$("document").ready(function () {

    $(".nav a").click(function (event) {

        event.preventDefault();
        $.get($(this).data('request-url'), { page: $(this).attr("href") },
            function (response) {
                $("#Page-content").html(response);
            }
        );

        //left nav switch effect
        $(".active").attr("class", "nav-link link-dark");
        $(this).attr("class", "nav-link active");

        //top nav switch effect
        $("#currentPageIcon").attr("class", $(this).find("i").attr("class"));
        $("#currentPageName").text($(this).text());
    });

});