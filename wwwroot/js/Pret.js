//test 4/152   and A/067
$("#getUserIDButton").on('input',function (e) { 
    e.preventDefault();
    if($(this).val() == '') return;
    $(this).prop("disabled", true);
    let that = this;
    $.get($(this).data('request-url'), {IdAdherent:$(this).val()},
        function (data) {
            $(that).prop("disabled", false);
            if (typeof data !== 'undefined') {
                $("#adherentNameInput").val(data["nom"]);
                $("#adherentPrenameInput").val(data["prenom"]);
                
                $("#adherentStateAlert").removeClass("alert-dark").removeClass("alert-success").removeClass("alert-warning").removeClass("alert-danger")
                switch(data["etatAdherent"]){
                    //Pénalisé
                    case 1 :  
                    $("#adherentStateAlert").find("p").text("La personne est en état de Pénalisé à la bibliothèque. Emprunt impossible pour le moment."); 
                    $("#adherentStateAlert").addClass("alert-danger");
                    break;
                    //En règle
                    case 2 :  
                    $("#adherentStateAlert").find("p").text("La personne est en règle à la bibliothèque. Emprunt autorisé."); 
                    $("#adherentStateAlert").addClass("alert-success");
                    $("#bookPart").removeClass("disabled");
                    break;
                    //Suspendu
                    case 3 :  
                    $("#adherentStateAlert").find("p").text("La personne est suspendue à la bibliothèque. Emprunt indisponible."); 
                    $("#adherentStateAlert").addClass("alert-warning");
                    break;
                }
            }
        }
    );
});