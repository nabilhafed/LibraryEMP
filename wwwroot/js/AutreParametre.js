//For loading icon
function loadingTemplate() {
    return '<i class="fa fa-spinner fa-spin fa-fw fa-2x"></i>'
}

// for edit and delete buttons
function operateFormatter(value, row, index) {
    return [
        '<div class="text-centers w-100 d-flex justify-content-around">',
        '<a class="edit"><i class="btn fa-solid fa-pen text-primary"></i></a>',
        '<a class="remove"><i class="btn fa fa-trash text-danger"></i></a>',
        '</div>'
    ].join('');
}
// for edit and delete buttons
window.operateEvents = {
    'click .edit': function (e, value, row, index) {
        $("#modifiedFouries").modal('show')
    },
    'click .remove': function (e, value, row, index) {
        $("#FeriesTable").bootstrapTable('remove', {
            field: 'dateJourFeries',
            values: [row.dateJourFeries]
        })
    }
}



/*
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
*/
 



