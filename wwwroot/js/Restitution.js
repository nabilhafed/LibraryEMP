 $("#UserID").on('input', function () {
    var userId = $(this).val();
    console.log("test est valide ");
    if (userId === '') return;

    $(this).prop("disabled", true);

    var that = this; // Définir une référence à $(this) pour une utilisation dans la fonction de rappel

    $.get($(this).data('request-url'), { IdAdherent: $(this).val() })
        .done(function (data) {
            switch (data.user.etatAdherent) {
                //En cours de traitement
                case 1:
                    $("#etatAdherent").find("p").text("l'adherent est en rigle");
                    break;
                //Disponible
                case 2:
                    $("#etatAdherent").removeClass("alert-info");
                    $("#etatAdherent").addClass("alert-danger");
                    $("#etatAdherent").find("p").text("l'adherent est pénalisé");
                    $("#RenouvellementButton").prop("disabled", true);

 

                    break;
               
            }
            $(that).prop("disabled", false);
           //$("#adherentStateAlert").removeClass("alert-dark").removeClass("alert-success").removeClass("alert-warning").removeClass("alert-danger");
            if (typeof data !== 'undefined') {
                
                $("#NameAdherent").val(data.user.nom);
                $("#FamillyNameAdherent").val(data.user.prenom);
                // Ajoutez ici d'autres manipulations des données si nécessaire
            }
            $("#SelectExemplaire").empty();

            // Parcourir les données reçues et ajouter chaque option
            for (const exemplaire of data.exemplaires) {
                
                // Créer une option avec la valeur et le texte de l'exemplaire
                var option = $('<option>', {
                    value : exemplaire.idExemplaire,
                    text : exemplaire.idExemplaire
                });

                // Ajouter l'option au sélecteur
                $("#SelectExemplaire").append(option);
            }
        })
        .fail(function (error) {
            console.error("Erreur lors de la récupération des données:", error);
            $(that).prop("disabled", false); // Réactiver l'élément en cas d'échec de la requête
            // Gérez les erreurs éventuelles
        });
 });
 

$("#SelectExemplaire").on('change', function () {
    var selectedOption = $(this).val();
    // Utilisez la valeur sélectionnée comme vous le souhaitez
    $.get($(this).data('request-url'), { idExemplaire: selectedOption })
        .done(function (data) {
            $("#ProperTitle").find("textarea").text(data.propreTitle);
        });
});
