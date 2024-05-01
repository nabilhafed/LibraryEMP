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

            //le remplissage de date de retour qui representer la date actuel 
            // Obtenir la date actuelle
            var currentDate = new Date();

            // Formater la date au format requis (AAAA-MM-JJ)
            var formattedDate = currentDate.toISOString().slice(0, 10);
           
            // Remplir la case de date avec la date actuelle
            $("#ReturnDate").val(formattedDate);

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

            // Extraire les parties de la date
            var year = data.datePret.substring(0, 4);
            var month = data.datePret.substring(5, 7);
            var day = data.datePret.substring(8, 10);

            // Formater la date au format requis (AAAA-MM-JJ)
            var formattedDate = year + "-" + month + "-" + day;

            // Remplir la case de date avec la date formatée
            $("#PretData").val(formattedDate);

        });
});
