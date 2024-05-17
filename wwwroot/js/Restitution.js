$("#UserID").on('input', function () {
    var userId = $(this).val();

    if (userId === '') return;

    var that = this;

    $.get($(this).data('request-url'), { IdAdherent: userId })
        .done(function (data) {

            console.log("the test passed from here");

            if (typeof data !== 'undefined') {
                $("#DatePretRoure").removeClass("disabled");
                $("#ExemplaireChoses").removeClass("disabled");

                $("#NameAdherent").val(data.adherent.nom);
                $("#FamillyNameAdherent").val(data.adherent.prenom);

                switch (data.adherent.etatAdherent) {
                    case 1:
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

                for (const exemplaire of data.exemplaires) {
                    var option = $('<option>', {
                        value: exemplaire.idExemplaire,
                        text: exemplaire.idExemplaire
                    });

                    $("#SelectExemplaire").append(option);
                }

                var currentDate = new Date();
                var formattedDate = currentDate.toISOString().slice(0, 10);
                $("#ReturnDate").val(formattedDate);
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
            $("#ProperTitle").find("textarea").text(data.propreTitle);

            var year = data.datePret.substring(0, 4);
            var month = data.datePret.substring(5, 7);
            var day = data.datePret.substring(8, 10);
            var formattedDate = year + "-" + month + "-" + day;

            $("#PretData").val(formattedDate);
            $("#ButtonChoses").removeClass("disabled");
            $("#titrePropreBox").removeClass("disabled");

        });
});

$("RetourButton").on('change', function () {
    var selectedOption = $(this).val();

    $.get($(this).data('request-url'), { idExemplaire: selectedOption })
        .done(function (data) {
            $("#ProperTitle").find("textarea").text(data.propreTitle);

            var year = data.datePret.substring(0, 4);
            var month = data.datePret.substring(5, 7);
            var day = data.datePret.substring(8, 10);
            var formattedDate = year + "-" + month + "-" + day;

            $("#PretData").val(formattedDate);
            $("#ButtonChoses").removeClass("disabled");
            $("#titrePropreBox").removeClass("disabled");

        });
});


