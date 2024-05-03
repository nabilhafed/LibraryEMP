$("#UserID").on('input', function () {
    var userId = $(this).val();

    if (userId === '') return;

    // $(this).prop("disabled", false); // It seems unnecessary to enable this element here

    var that = this; // Define a reference to $(this) for use in the callback function

    $.get($(this).data('request-url'), { IdAdherent: $(this).val() })
        .done(function (data) {

            $("#DatePretRoure").removeClass("disabled");
            $("#ExemplaireChoses").removeClass("disabled");

            switch (data.user.etatAdherent) {
                // En cours de traitement
                case 1:
                    $("#etatAdherent").find("p").text("L'adhérent est en règle");
                    break;
                // Disponible
                case 2:
                    $("#etatAdherent").removeClass("alert-info");
                    $("#etatAdherent").addClass("alert-danger");
                    $("#etatAdherent").find("p").text("L'adhérent est pénalisé");
                    $("#RenouvellementButton").prop("disabled", true);
                    break;
                default:
                    // Handle other cases if necessary
                    break;
            }

            if (typeof data !== 'undefined') {
                $("#NameAdherent").val(data.user.nom);
                $("#FamillyNameAdherent").val(data.user.prenom);
                // Add other data manipulations here if necessary
            }

            $("#SelectExemplaire").empty();

            // Loop through the received data and add each option
            for (const exemplaire of data.exemplaires) {
                // Create an option with the value and text of the exemplaire
                var option = $('<option>', {
                    value: exemplaire.idExemplaire,
                    text: exemplaire.idExemplaire
                });

                // Add the option to the selector
                $("#SelectExemplaire").append(option);
            }

            // Fill the return date field with the current date
            // Get the current date
            var currentDate = new Date();

            // Format the date in the required format (YYYY-MM-DD)
            var formattedDate = currentDate.toISOString().slice(0, 10);

            // Fill the date field with the current date
            $("#ReturnDate").val(formattedDate);

        })
        .fail(function (error) {
            console.error("Error while retrieving data:", error);
            $(that).prop("disabled", false); // Re-enable the element in case of request failure
            // Handle any potential errors
        });
});

 

$("#SelectExemplaire").on('change', function () {
    var selectedOption = $(this).val();
    // Utilisez la valeur sélectionnée comme vous le souhaitez
    $.get($(this).data('request-url'), { idExemplaire: selectedOption })
        .done(function (data) {
            $("#ProperTitle").find("textarea").text(data.propreTitle);

            // Extraire les parties de la date
            var year = data.datePret.substring(0, 4);
            var month = data.datePret.substring(5, 7);
            var day = data.datePret.substring(8, 10);

            // Formater la date au format requis (AAAA-MM-JJ)
            var formattedDate = year + "-" + month + "-" + day;

            // Remplir la case de date avec la date formatée
            $("#PretData").val(formattedDate); 

            $("#ButtonChoses").removeClass("disabled");
            $("#titrePropreBox").removeClass("disabled");
        });
});
