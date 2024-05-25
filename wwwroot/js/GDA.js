
let iconList = {
    spin: '<i class="fa fa-spinner fa-spin fa-fw fa-2x"></i>',
    pen: '<i class="fa-solid fa-pen"></i>',
    alert: '<i class="fa-solid fa-triangle-exclamation text-warning fa-beat"></i>',
    ok: '<i class="fa-solid fa-check text-success"></i>',
    switch : '<i class="fa-solid fa-repeat text-warning"></i>'
}

//For loading icon
function loadingTemplate() {
    return iconList.spin;
}

// for edit and delete buttons
function operateFormatter(value, row, index) {
    return [
        '<div class="text-centers w-100 d-flex justify-content-around">',
        '<a class="modified"><i class="btn fa-solid fa-pen text-primary"></i></a>',
        '<a class="remove"><i class="btn fa fa-trash '+((row.canBeDeleted)?'text-danger':'text-black-50')+'"></i></a>',
        '</div>'
    ].join('');
}

// for edit and delete buttons
let selectedmodifiedRow;
window.operateEvents = {
    'click .modified': function (e, value, row, index) {
        $("#modifiedUserID").val(row.idAdherent);
        $("#modifiedName").val(row.nom);
        $("#modifiedPrename").val(row.prenom);
        $("#modifiedPos").val(row.idPosition);
        $("#modifiedCat").val(row.idCategorie);
        
        $("#modifiedUserID").next().html(iconList.ok);
        $("#modifiedName").next().html(iconList.ok);
        $("#modifiedPrename").next().html(iconList.ok);
        $("#modifiedPos").next().html(iconList.ok);
        $("#modifiedCat").next().html(iconList.ok);

        $("#modifiedAdherentModalButton").prop("disabled", true);

        selectedmodifiedRow = row;

        modified_ID = modified_Nom = modified_Prenom = modified_Pos = modified_Cat = false;
        $("#modifiedAdherentModal").modal('show')
    },
    'click .remove': function (e, value, row, index) {
        if(row.canBeDeleted){
            $("#deleteConfirmationID").text(row.idAdherent);
            $("#deleteConfirmation").modal('show')
        }
    }
}

//deleteButton
$("#deleteConfirmationButton").click(function (e) { 
    let = idAdherent = $("#deleteConfirmationID").text();
    $.get($(this).data("url"), {idAdherent : idAdherent},
        function (data) {
            if(data){
                $("#adherentTable").bootstrapTable('remove', {
                    field: 'idAdherent',
                    values: [idAdherent]
                })
                $('#deleteAdherentSuccessToast').toast('show');
            }else{
                $('#deleteAdherentFaildToast').toast('show');
            }
            $("#deleteConfirmation").find(".btn-close").click();
        }
    );
});

//for modified and add Adherent information 
$.get($("#getInformation").val(),
    function (data) {
        for (const pos of data.pos) {
            $("#modifiedPos").append($('<option>', {value: pos.idPosition , text: pos.libellePosition}));
            $("#addPos").append($('<option>', {value: pos.idPosition , text: pos.libellePosition}));
        }

        for (const cat of data.cat) {
            $("#modifiedCat").append($('<option>', {value: cat.idCategorie , text: cat.libelleCategorie}));
            $("#addCat").append($('<option>', {value: cat.idCategorie , text: cat.libelleCategorie}));
        }

    }
);

//add Adherent Modal
let accept_ID = accept_Name = accept_Prename = false;
//userID input
$("#addUserID").on("input", function () {
    let adherentID = $("#addUserID").val();
    $("#addAdherentButton").prop("disabled", true);
    if (adherentID.length < 3) {
        $("#addName")   .prop("disabled", true );
        $("#addPrename").prop("disabled", true );
        $("#addPos")    .prop("disabled", true );
        $("#addCat")    .prop("disabled", true );
        $(this).next().html(iconList.pen);
        return;
    };
    let that = this;
    $.get($(this).data('url'), {idAdherent:adherentID},
        function (data) {
            if(data){
                $("#addName")   .prop("disabled", false );
                $("#addPrename").prop("disabled", false );
                $("#addPos")    .prop("disabled", false );
                $("#addCat")    .prop("disabled", false );
                $("#addEta")    .prop("disabled", false);
                $(that).next().html(iconList.ok);
                $("#addAdherentButton").prop("disabled", !(accept_Prename && accept_Name) );
            }else{
                $("#addName")   .prop("disabled", true );
                $("#addPrename").prop("disabled", true );
                $("#addPos")    .prop("disabled", true );
                $("#addCat")    .prop("disabled", true );
                $(that).next().html(iconList.alert);
            }
            accept_ID = data;
        }
    );
});

//Adherent Name and Prename input
function validateInputForAdd(inputElement) {
    let value = inputElement.val();
    if(value.length < 3){
        inputElement.next().html(iconList.pen);
        return false;
    }
    if (/[\d!@#$%^&*(),.?":{}|<>]/.test(value)) {
        inputElement.next().html(iconList.alert);
        return false;
    } else {
        inputElement.next().html(iconList.ok);
        return true;
    }
}
$("#addName").on("input", function () {
    accept_Name = validateInputForAdd($(this));
    $("#addAdherentButton").prop("disabled", !(accept_Prename && accept_Name) );

});
$("#addPrename").on("input", function () {
    accept_Prename = validateInputForAdd($(this));
    $("#addAdherentButton").prop("disabled", !(accept_Name && accept_Prename) );
});

// on Adherent create 
$("#addAdherentButton").click(function (e) { 
    let idAdherent      = $("#addUserID").val();
    let adherentName    = $("#addName").val();
    let adherentPrename = $("#addPrename").val();
    let idPosition      = $("#addPos").val();
    let idCategorie     = $("#addCat").val();
    $.get($(this).data("url"), {idAdherent , adherentName , adherentPrename , idPosition , idCategorie},
        function (data) {
            if(data){
                $('#addAdherentSuccessToast').toast('show');
                $("#adherentTable").bootstrapTable('append' , {
                    idAdherent:idAdherent,
                    nom:adherentName,
                    prenom:adherentPrename,
                    position: $('#addPos option:selected').text(),
                    idPosition: idPosition,
                    categorie: $('#addCat option:selected').text(),
                    idCategorie: idCategorie,
                    etat: "En régle",
                    etatAdherent: 1,
                    canBeDeleted: true
                })
            }else{
                $('#addAdherentFaildToast').toast('show');
            }
            $("#addAdherentModal").find(".btn-close").click();
            $("#addUserID").val("");
            $("#addUserID").trigger("input");
            $("#addName").val("");
            $("#addName").trigger("input");
            $("#addPrename").val("");
            $("#addPrename").trigger("input");
        }
    );
});

//modifiedAdherent Modal
let modified_ID = modified_Nom = modified_Prenom = modified_Pos = modified_Cat = 1;
function validateInputForModified(inputElement , realValue , test , optional = false) {
    let value = inputElement.val();
    if ( value != realValue && ( optional || (test && value.length < 3) || ( test && /[\d!@#$%^&*(),.?":{}|<>]/.test(value))) ) {
        inputElement.next().html(iconList.alert);
        return -99;
    } else {
        if(value == realValue){
            inputElement.next().html(iconList.ok);
            return 0;
        }else{
            inputElement.next().html(iconList.switch);
            return 1;
        }
    }
}
function testEnableModifiedButton(){
    if(modified_ID + modified_Nom + modified_Prenom + modified_Pos + modified_Cat > 0)
        $("#modifiedAdherentModalButton").prop("disabled", false );
    else
        $("#modifiedAdherentModalButton").prop("disabled", true );
}
$("#modifiedUserID").on("input", function () {
    let value = $(this).val();
    if(value.length < 3){
        modified_ID = validateInputForModified($(this) , selectedmodifiedRow.idAdherent , false , optional = true);
        testEnableModifiedButton();
    }else{
        let that = this;
        $.get($(this).data("url"), {idAdherent:value},
            function (data) {
                modified_ID = validateInputForModified($(that) , selectedmodifiedRow.idAdherent , false , optional = !data);
                testEnableModifiedButton();
            }
        );
    }
});
$("#modifiedName").on("input", function () {
    modified_Nom = validateInputForModified($(this) , selectedmodifiedRow.nom , true);
    testEnableModifiedButton();
});
$("#modifiedPrename").on("input", function () {
    modified_Prenom = validateInputForModified($(this) , selectedmodifiedRow.prenom , true);
    testEnableModifiedButton();
});
$("#modifiedPos").on("input", function () {
    modified_Pos =  validateInputForModified($(this) , selectedmodifiedRow.idPosition , false); 
    testEnableModifiedButton();
});
$("#modifiedCat").on("input", function () {
    modified_Cat = validateInputForModified($(this) , selectedmodifiedRow.idCategorie , false); 
    testEnableModifiedButton();
});
$("#modifiedAdherentModalButton").click(function (e) { 
    let idAdherentOld   = selectedmodifiedRow.idAdherent;
    let idAdherent      = $("#modifiedUserID").val();
    let adherentName    = $("#modifiedName").val();
    let adherentPrename = $("#modifiedPrename").val();
    let idPosition      = $("#modifiedPos").val();
    let idCategorie     = $("#modifiedCat").val();
    $.get($(this).data("url"), {idAdherentOld , idAdherent , adherentName , adherentPrename , idPosition , idCategorie},
        function (data) {
            if(data){
                $('#modifiedAdherentSuccessToast').toast('show');
                selectedmodifiedRow.idAdherent = idAdherent;
                selectedmodifiedRow.nom = adherentName;
                selectedmodifiedRow.prenom = adherentPrename;
                selectedmodifiedRow.idPosition = idPosition;
                selectedmodifiedRow.position = $("#modifiedPos option:selected").text();
                selectedmodifiedRow.idCategorie = idCategorie;
                selectedmodifiedRow.categorie = $("#modifiedCat option:selected").text();
                $("#adherentTable").bootstrapTable('updateByUniqueId', {
                    id: idAdherentOld,
                    row: selectedmodifiedRow
                });
            }else{
                $('#modifiedAdherentFaildToast').toast('show');
            }
            $("#modifiedAdherentModal").find(".btn-close").click();
        }
    );
});