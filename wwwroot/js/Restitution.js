function ViderLesInputes() {

    $("#UserID").val('');
    $("#SelectExemplaire").empty();
    $("#NameAdherent").val('');
    $("#FamillyNameAdherent").val('');
    $("#DatePretRoure").addClass("disabled");
    $("#ExemplaireChoses").addClass("disabled");
    $("#etatAdherent").removeClass("alert-danger").removeClass("alert-warning").addClass("alert-info");
    $("#etatAdherent").find("p").text("Attend qeulque minute pour le traitement");
    $("#RenouvellementButton").prop("disabled", false);
    $("#ButtonChoses").addClass("disabled");
    $("#ProperTitle").find("textarea").text('');
    $("#titrePropreBox").addClass("disabled");
    $("#PretData").val('');
    $("#ReturnDate").val('');
    $("SelectExemplaire").prop("disabled", false);
}

function ViderLesInputesSansButton() {

    $("#SelectExemplaire").empty();
    $("#NameAdherent").val('');
    $("#FamillyNameAdherent").val('');
    $("#DatePretRoure").addClass("disabled");
    $("#ExemplaireChoses").addClass("disabled");
    $("#etatAdherent").removeClass("alert-danger").removeClass("alert-warning").addClass("alert-info");
    $("#etatAdherent").find("p").text("Attend qeulque minute pour le traitement");
    $("#RenouvellementButton").prop("disabled", false);
    $("#ButtonChoses").addClass("disabled");
    $("#ProperTitle").find("textarea").text('');
    $("#titrePropreBox").addClass("disabled");
    $("#PretData").val('');
    $("#ReturnDate").val('');
    $("SelectExemplaire").prop("disabled", false);
}

$("#UserID").on('input', function () {

   
    ViderLesInputesSansButton();

    var userId = $(this).val();

    if (userId === '') return;

    var that = this;

    $.get($(this).data('request-url'), { IdAdherent: userId })
        .done(function (data) {


            if (typeof data !== 'undefined') {
                if (data.adherent != null) {
                    $("#NameAdherent").val(data.adherent.nom);
                    $("#FamillyNameAdherent").val(data.adherent.prenom);

                    switch (data.adherent.etatAdherent) {
                        case 1:
                            $("#etatAdherent").addClass("alert-info");
                            $("#etatAdherent").find("p").text("L'adhérent est en règle");
                            break;
                        case 2:
                            $("#etatAdherent").removeClass("alert-info").addClass("alert-danger");
                            $("#etatAdherent").find("p").text("L'adhérent est pénalisé");
                            $("#RenouvellementButton").prop("disabled", true);
                            break;
                        case 3:
                            console.log('Aucun utilisateur trouvé avec le statut 3.');
                            break;
                        default:
                            // Autres cas à gérer si nécessaire
                            break;
                    }

                    $("#SelectExemplaire").empty();
 
                    if (data.exemplaires.length > 0) {

                        $("#DatePretRoure").removeClass("disabled");
                        $("#ExemplaireChoses").removeClass("disabled");

                        
                        for (const exemplaire of data.exemplaires) {
                            var option = $('<option>', {
                                value: exemplaire.idExemplaire,
                                text: exemplaire.idExemplaire 
                            });

                            $("#SelectExemplaire").append(option);

                           
                        }
                            $("#SelectExemplaire").trigger('change');
                         
                    }
                    else {
                        $("#etatAdherent").removeClass("alert-danger").addClass("alert-warning");
                        $("#etatAdherent").find("p").text("L'adhérent ne possède aucun exemplaire pour la restitution.");
                    }

                    var currentDate = new Date();
                    var formattedDate = currentDate.toISOString().slice(0, 10);
                    $("#ReturnDate").val(formattedDate);
                }
                else {
                    ViderLesInputesSansButton();
                    $("#etatAdherent").removeClass("alert-info").addClass("alert-danger");
                    $("#etatAdherent").find("p").text("L'adhérent n'existe pas .");
                   
                }
            }
        })
        .fail(function (error) {
            console.error("Erreur lors de la récupération des données :", error);
            $(that).prop("disabled", false);
        });
});

$("#SelectExemplaire").on('change', function () {
    var selectedOption = $(this).val();

    $.get($(this).data('request-url'), { idExemplaire: selectedOption })
        .done(function (data) {

            $("#ProperTitle").find("textarea").text(data.titrePropre);

            var year = data.datePret.substring(0, 4);
            var month = data.datePret.substring(5, 7);
            var day = data.datePret.substring(8, 10);
            var formattedDate = year + "-" + month + "-" + day;
           
            $("#PretData").val(formattedDate);
            $("#ButtonChoses").removeClass("disabled");
            $("#titrePropreBox").removeClass("disabled");

        });
});

$("#RetourButton").on('click', function () {
    var VarIdAdherent = $("#UserID").val();
    var VarIdExemplaire = $("#SelectExemplaire").val();

    if (VarIdAdherent === '' || VarIdExemplaire === '') {
        console.error("Adherent ID or Exemplaire ID is missing.");
      
        return;
    }

    $.get($(this).data('request-url'), { IdAdherent: VarIdAdherent, IdExemplaire: VarIdExemplaire })
        .done(function (data) {

            console.log(data)

            // Add code to handle the successful response here
            // For example, display a success message or update the UI
            $('#restitutionSuccessToast').toast('show');

            // Optionally, you can reset some form fields or update the UI
            ViderLesInputes();

        })
        .fail(function (error) {
            console.error("Error:", error);
            // Add code to handle the error response here
            // For example, display an error message
            $('#restitutionFaildToast').toast('show');
        });
});

$("#RenouvellementButton").on('click', function () {
    var VarIdAdherent = $("#UserID").val();
    var VarIdExemplaire = $("#SelectExemplaire").val();

    if (VarIdAdherent === '' || VarIdExemplaire === '') {
        console.error("Adherent ID or Exemplaire ID is missing.");

        return;
    }

    $.get($(this).data('request-url'), { IdAdherent: VarIdAdherent, IdExemplaire: VarIdExemplaire })
        .done(function (data) {
            console.log("Success:", data);

            // Add code to handle the successful response here
            // For example, display a success message or update the UI
            alert("Exemplaire returned successfully!");

            // Optionally, you can reset some form fields or update the UI
            ViderLesInputes();


        })
        .fail(function (error) {
            console.error("Error:", error);
            // Add code to handle the error response here
            // For example, display an error message
            alert("Error occurred while returning the exemplaire. Please try again.");
        });
});


