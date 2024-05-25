
$("#HiddenButton").on('click', function () {
    $.get($(this).data('request-url'), {})
        .done(function (data) {
            $("#JourFeries").empty();

            console.log(data);


                 for (const element of data) {
                  var row = '<tr>' +
                         '<td>' + element.dateJourFeries + '</td>'
                          + '</tr>';
                  $("#JourFeries").append(row);
              }


        })
        .fail(function (error) {
            console.error("Error:", error);
            // Add code to handle the error response here
            // For example, display an error message
            alert("Error occurred while returning the exemplaire. Please try again.");
        });
});
$("#HiddenButton").click();

 



