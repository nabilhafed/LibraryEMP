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

function restUserPartHalf() {
    $("#bookPart").addClass("disabled");
    $("#datePart").addClass("disabled");
    $("#submitNewBorrow").prop("disabled", true);
    restBookPart();
}
function restUserPart(){
    restUserPartHalf();
    $("#adherentNameInput").val("XXXXX");
    $("#adherentPrenameInput").val("XXXXX");
    //$("#returnDateInput").val("dd/mm/yyyy");
    //$("#readyDateInput").val("dd/mm/yyyy");
    $("#booksAvailable").prop("disabled", true);
    $("#adherentStateAlert").find("p").text("veuillez sélectionner Adhérent pour obtenir des informations"); 
    $("#adherentStateAlert").addClass("alert-dark");
}
function restBookPart(){
    $("#bookStateAlert").addClass("alert-dark");
    $("#bookStateAlert").find("p").text("veuillez sélectionner le document pour obtenir le statut"); 
    $("#bookTitleInput").val("XXXXX");
    $("#booksAvailable").empty();
    $("#getBookCoteButton").empty();
    $("#booksAvailable").append('<option>', {text: "Sélectionner un exemple"});
    $("#submitNewBorrow").prop("disabled", true);
    $("#bookBadge").addClass("d-none");
}


$("#getUserIDButton").on('input',function () { 
    if($(this).val() == '') return;
    $(this).prop("disabled", true);
    let that = this;
    $.get($(this).data('request-url'), {IdAdherent:$(this).val()},function (data) 
        {
            $(that).prop("disabled", false);
            $(that).focus();
            $("#adherentStateAlert").removeClass("alert-dark").removeClass("alert-success").removeClass("alert-warning").removeClass("alert-danger");
            restBookPart();
            if (typeof data !== 'undefined') {
                $("#adherentNameInput").val(data.nom);
                $("#adherentPrenameInput").val(data.prenom);
                switch(data.etatAdherent){
                    //Pénalisé
                    case 2 :  
                        $("#adherentStateAlert").find("p").text("La personne est en état de Pénalisé à la bibliothèque. Emprunt impossible pour le moment."); 
                        $("#adherentStateAlert").addClass("alert-danger");
                        restUserPartHalf();
                    break;
                    //En règle
                    case 1 :  
                        let documentToborrow = data.documentsAuthorized - data.documentsOnborrow;
                        // in case cant borrow more books !!!
                        if ( documentToborrow <= 0 ){
                            $("#adherentStateAlert").find("p").text("La personne est en règle à la bibliothèque. mais Il ne peut pas emprunter plus des documents!!");
                            $("#adherentStateAlert").addClass("alert-warning");
                            $("#bookPart").addClass("disabled");
                            $("#datePart").addClass("disabled");
                            $("#submitNewBorrow").prop("disabled", true);
                            return;
                        }
                        $("#adherentStateAlert").find("p").text("La personne est en règle à la bibliothèque. Emprunt autorisé de moins que "+documentToborrow+" documents."); 
                        $("#adherentStateAlert").addClass("alert-success");
                        $("#bookPart").removeClass("disabled");
                        $("#datePart").removeClass("disabled");
                        $("#booksAvailable").prop("disabled", true);
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
                        restUserPartHalf();
                    break;
                }
            }else{
                restUserPart();
            }
        }
    );
});

// test M03356;
$("#getBookCoteButton").on('input', function () {
    if($(this).val() == '') return;
    $(this).prop("disabled", true);
    let that = this;
    $.get($(this).data('request-url'), {cote:$(this).val() , IdAdherent:$("#getUserIDButton").val()},function (data) 
        {
            $(that).prop("disabled", false);
            $(that).focus();
            $("#bookBadge").addClass("d-none");
            $("#bookStateAlert").removeClass("alert-dark").removeClass("alert-success").removeClass("alert-warning").removeClass("alert-primary");
            if (typeof data !== 'undefined') {
                //check if that document is allredy has been borrowd from the same Adhrent !!!
                if(!data.book.isCurrentlyBorrowedbyHim){
                    $("#booksAvailable").empty();
                    let is = [false,false,false,false];
                    let reservedValueToSet = false;
                    for (const exemple of data.exemples) {
                        is[exemple.idEtat] = true;
                        let option = $('<option>', {
                                value: exemple.idExemplaire,
                                text: exemple.idExemplaire
                            })
                        let isReserverByHim = 'reservedExemple' in data && data.reservedExemple == exemple.idExemplaire;
                        switch (exemple.idEtat) {
                            //En cours de traitement
                            case 0:
                                $(option).addClass("bg-info").prop("disabled", true);
                            break;
                            //Disponible
                            case 1:
                                //if(exemple.reservedByHim)
                                //    $(option).addClass("bg-success");
                            break;
                            //Prêté
                            case 2:
                                if(isReserverByHim)
                                    reservedValueToSet = $(option).addClass("bg-success");
                                else
                                    $(option).addClass("bg-warning").prop("disabled", true);
                            break;
                            //Perdu
                            case 3:
                                $(option).addClass("bg-danger").prop("disabled", true);
                            break;
                        }
                        $("#booksAvailable").append(option);
                    }
                    if(is[1]){
                        $("#bookStateAlert").addClass("alert-success");
                        $("#bookStateAlert").find("p").text("Il peut emprunter ce livre"); 
                        $("#bookTitleInput").val(data.book.titrePropre);
                        $("#submitNewBorrow").prop("disabled", false);
                        

                        if(reservedValueToSet){
                            $("#booksAvailable").val(data.reservedExemple);
                            $("#bookBadge").removeClass("d-none");
                        }else
                            $("#booksAvailable").prop("disabled", false);

                    }else if(is[2]){
                        $("#bookStateAlert").addClass("alert-primary");
                        $("#bookStateAlert").find("p").text("Il ne peut pas emprunter ce livre, mais il peut faire une réservation.");
                        $("#submitNewBorrow").text("Réserve");
                        $("#submitNewBorrow").prop("disabled", false);
                        $("#booksAvailable").prop("disabled", true);
                    }else{
                        $("#bookStateAlert").addClass("alert-danger");
                        $("#booksAvailable").prop("disabled", true);
                        $("#bookStateAlert").find("p").text("parce que il n'existe aucun exemplaire disponible ou dans l'état bien"); 
                    }
                }else{
                    $("#bookStateAlert").addClass("alert-warning");
                    $("#booksAvailable").prop("disabled", true);
                    $("#bookStateAlert").find("p").text("il ne peut pas emprunter ce livre parce qu'il l'empruntait comme avant"); 
                }
                
            }else{
                //rest
                restBookPart();
            };
        }
    );
});


//insert confim Modele
$("#submitNewBorrow").click(function (e) { 
    e.preventDefault();
    $('#newBorrowModel').find('strong').each(function() {
        $(this).text($($(this).data("linked")).val());
    });
});

//insert new Borrow Line in Pret TABLE
$("#confirmNewBorrow").click(function (e) { 
    e.preventDefault();

    let dataToSend = {};
    $('#newBorrowModel').find('strong').each(function() {dataToSend[$(this).data("name")] = $(this).text()});

    $.post($(this).data('request-url'),
    dataToSend,
    function (data) {
           if(data){
                $("#newBorrowModel").find(".btn-close").click();
                $('#newBorrowSuccessToast').toast('show');
           }else{
                $("#newBorrowModel").find(".btn-close").click();
                $('#newBorrowFaildToast').toast('show');
           }
    });
});



// Iterate over each popover trigger element and initialize Bootstrap Popover
$('[data-bs-toggle="popover"]').each(function (index, element) {new bootstrap.Popover($(element)[0]);});