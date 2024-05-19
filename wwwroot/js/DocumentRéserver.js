$("#TriAdherentButton").on('click', function () {
    $.get($(this).data('request-url'), {})
        .done(function (data) {
            $("#ReservationListe").empty();
            console.log(data);
            for (const element of data) {
                var row = '<tr>' +
                    '<td>' + element.idAdherent + '</td>' +
                    '<td>' + element.nom +' ' +element.prenom + '</td>' +
                    '<td>' + element.cote + '</td>' +
                    '<td>' + element.heureReservation + '</td>' +
                    '</tr>';

                $("#ReservationListe").append(row);
            }
        })
        .fail(function (error) {
            console.error("Error:", error);
            // Add code to handle the error response here
            // For example, display an error message
            alert("Error occurred while returning the exemplaire. Please try again.");
        });
});
$("#TriCoteButton").on('click', function () {
    $.get($(this).data('request-url'), {})
        .done(function (data) {
            $("#ReservationListe").empty();
            console.log(data);
            for (const element of data) {
                var row = '<tr>' +
                    '<td>' + element.idAdherent + '</td>' +
                    '<td>' + element.nom + ' ' + element.prenom + '</td>' +
                    '<td>' + element.cote + '</td>' +
                    '<td>' + element.heureReservation + '</td>' +
                    '</tr>';

                $("#ReservationListe").append(row);
            }
        })
        .fail(function (error) {
            console.error("Error:", error);
            // Add code to handle the error response here
            // For example, display an error message
            alert("Error occurred while returning the exemplaire. Please try again.");
        });
});
$("#TriHeureButton").on('click', function () {
    $.get($(this).data('request-url'), {})
        .done(function (data) {
            $("#ReservationListe").empty();
            console.log(data);
            for (const element of data) {
                var row = '<tr>' +
                    '<td>' + element.idAdherent + '</td>' +
                    '<td>' + element.nom + ' ' + element.prenom + '</td>' +
                    '<td>' + element.cote + '</td>' +
                    '<td>' + element.heureReservation + '</td>' +
                    '</tr>';

                $("#ReservationListe").append(row);
            }
        })
        .fail(function (error) {
            console.error("Error:", error);
            // Add code to handle the error response here
            // For example, display an error message
            alert("Error occurred while returning the exemplaire. Please try again.");
        });
});

$("#TriAdherentButton").click();