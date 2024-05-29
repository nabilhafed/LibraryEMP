var inupteModifier;
//For loading icon
function loadingTemplate() {
    return '<i class="fa fa-spinner fa-spin fa-fw fa-2x"></i>'
}

// for edit and delete buttons
function operateFormatter(value, row, index) {
    return [
        '<div class="text-centers w-100 d-flex justify-content-around">',
        '<a class="edit"><i class="btn fa-solid fa-pen text-primary"></i></a>',
        '<a class="remove" ><i class="btn fa fa-trash text-danger"></i></a>',
        '</div>'
    ].join('');
}
// for edit and delete buttons
window.operateEvents = {
    'click .edit': function (e, value, row, index) {
        $("#modifiedFouries").modal('show');
        $("#saveChangeUpdateFeries").prop("disabled", false);
        inupteModifier = row.dateJourFeries;


    },
    'click .remove': function (e, value, row, index) {

        $("#deleteConfirmationJourFeries").text(row.dateJourFeries);
        $("#deleteConfirme").modal('show');
        $("#saveChangeDeleteJour").prop("disabled", false);
         
        }
}

 

$("#saveChangeDeleteJour").click(function (e) {

    var idJourFeries = $("#deleteConfirmationJourFeries").text();
   
   
    $.get($(this).data("url"), { dateJourFeries: idJourFeries },
        function (data) {
            
            if (data) {
                $("#saveChangeDeleteJour").prop("disabled", true);
                $("#closeButton").click();

         
                location.reload();
            }
        }
    );
});


$("#dateJoursFeriesAdd").on('input', function () {

    $("#saveChangeAddFeries").prop("disabled", false);
});





$("#saveChangeAddFeries").click(function (e) {
    var idJourFeries = $("#dateJoursFeriesAdd").val();


    $.get($(this).data("url"), { inputDate: idJourFeries },
        function (data) {
            if (data) {


                $("#closeButtonAdd").click();

                // Rafraîchissement de la page après la suppression
                location.reload();
            }
        }
    );
});


$("#saveChangeUpdateFeries").click(function (e) {

    var idJourFeries = $("#dateJoursFeriesUpdate").val();
 

    $.get($(this).data("url"), { inputModifier: inupteModifier , inputDate: idJourFeries },
        function (data) {
            if (data) {


                 $("#closeButtonUpdate").click();

                // Rafraîchissement de la page après la suppression
                location.reload();
            }
        }
    );
});

 






 

 



