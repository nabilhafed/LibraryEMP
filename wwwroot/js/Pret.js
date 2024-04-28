//test 4/152   and A/067
var Holidays;
async function getHolidays(){
    if(!Array.isArray(Holidays))
        Holidays = (await $.get($("#getHolidaysInput").data('request-url'))).map(day => new Date(Date.parse(day)));
    return Holidays;
}

function isSameDate(date1, date2) {
    return (
      date1.getFullYear() === date2.getFullYear() &&
      date1.getMonth() === date2.getMonth() &&
      date1.getDate() === date2.getDate()
    );
  }


$("#getUserIDButton").on('input',function () { 
    if($(this).val() == '') return;
    $(this).prop("disabled", true);
    let that = this;
    $.get($(this).data('request-url'), {IdAdherent:$(this).val()},
        function (data) {
            $(that).prop("disabled", false);
            $("#adherentStateAlert").removeClass("alert-dark").removeClass("alert-success").removeClass("alert-warning").removeClass("alert-danger")
            if (typeof data !== 'undefined') {
                $("#adherentNameInput").val(data.nom);
                $("#adherentPrenameInput").val(data.prenom);
                switch(data.etatAdherent){
                    //Pénalisé
                    case 1 :  
                        $("#adherentStateAlert").find("p").text("La personne est en état de Pénalisé à la bibliothèque. Emprunt impossible pour le moment."); 
                        $("#adherentStateAlert").addClass("alert-danger");
                    break;
                    //En règle
                    case 2 :  
                        let documentToborrow = data.documentsAuthorized - data.documentsOnborrow;
                        // in case cant borrow more books !!!
                        if ( documentToborrow <= 0 ){
                            $("#adherentStateAlert").find("p").text("La personne est en règle à la bibliothèque. mais Il ne peut pas emprunter plus des documents!!");
                            $("#adherentStateAlert").addClass("alert-warning");
                            return;
                        }
                        $("#adherentStateAlert").find("p").text("La personne est en règle à la bibliothèque. Emprunt autorisé de moins que "+documentToborrow+" documents."); 
                        $("#adherentStateAlert").addClass("alert-success");
                        $("#bookPart").removeClass("disabled");
                        $("#datePart").removeClass("disabled");
                        $("#readyDateInput").val(new Date().toISOString().slice(0,10));
                        let returnDate = new Date(Date.now() + data.readyDate * 24 * 60 * 60 * 1000);
                        // search if the return day is holiday day !
                        getHolidays().then( Holidays =>{
                            for (const day of Holidays)
                                if(isSameDate(returnDate, day))
                                    returnDate.setDate(returnDate.getDate() +1);

                            $("#returnDateInput").val(returnDate.toISOString().slice(0,10));
                        });
                        
                    break;
                    //Suspendu
                    case 3 :  
                        $("#adherentStateAlert").find("p").text("La personne est suspendue à la bibliothèque. Emprunt indisponible."); 
                        $("#adherentStateAlert").addClass("alert-warning");
                    break;
                }
            }else{
                //rest
                $("#adherentNameInput").val("XXXXX");
                $("#adherentPrenameInput").val("XXXXX");
                //$("#returnDateInput").val("dd/mm/yyyy");
                //$("#readyDateInput").val("dd/mm/yyyy");
                $("#submitNewBorrow").prop("disabled", true);
                $("#adherentStateAlert").find("p").text("veuillez sélectionner Adhérent pour obtenir des informations"); 
                $("#adherentStateAlert").addClass("alert-dark");
                $("#bookPart").addClass("disabled");
                $("#datePart").addClass("disabled");
            }
        }
    );
});


$("#getBookCoteButton").on('input', function () {
    if($(this).val() == '') return;
    $(this).prop("disabled", true);
    let that = this;
    $.get($(this).data('request-url'), {cote:$(this).val() , IdAdherent:$("#getUserIDButton").val()},
        function (data) {
            $(that).prop("disabled", false);
            $("#bookStateAlert").removeClass("alert-dark").removeClass("alert-primary").removeClass("alert-warning");
            if (typeof data !== 'undefined') {
                //check if that document is allredy has been borrowd from the same Adhrent !!!
                if(data.book.canBeBorrow){
                    $("#bookStateAlert").addClass("alert-primary");
                    $("#bookStateAlert").find("p").text("il peut emprunter ce livre"); 
                    $("#bookTitleInput").val(data.book.titrePropre);
                    $("#booksAvailable").empty();
                    for (const exemple of data.avilables) {
                        let option = $('<option>', {
                                value: exemple.idEtat,
                                text: exemple.idExemplaire
                            })
                        switch (exemple.idEtat) {
                            //En cours de traitement
                            case 0:
                                $(option).addClass("bg-info").prop("disabled", true);
                            break;
                            //Disponible
                            case 1:
                                if(exemple.reservedByHim)
                                $(option).addClass("bg-success");
                                $("#submitNewBorrow").prop("disabled", false);
                            break;
                            //Prêté
                            case 2:
                                $(option).addClass("bg-warning").prop("disabled", true);
                            break;
                            //Perdu
                            case 3:
                                $(option).addClass("bg-danger").prop("disabled", true);
                            break;
                        }
                        $("#booksAvailable").append(option);
                    }
                    if ($("#submitNewBorrow").prop("disabled"))
                        $("#submitNewBorrow").text("Réserve");
                }else{
                    $("#bookStateAlert").addClass("alert-warning");
                    $("#bookStateAlert").find("p").text("il ne peut pas emprunter ce livre parce qu'il l'empruntait comme avant"); 
                }
                
            }else{
                //rest
                $("#bookStateAlert").addClass("alert-dark");
                $("#bookStateAlert").find("p").text("veuillez sélectionner le document pour obtenir le statut"); 
                $("#bookTitleInput").val("XXXXX");
                $("#booksAvailable").empty();
                $("#booksAvailable").append('<option>', {text: "Select Document"});
                $("#submitNewBorrow").prop("disabled", true);
            };
        }
    );
});

