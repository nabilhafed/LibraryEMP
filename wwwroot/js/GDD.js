
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
        '<a class="view"><i class="fa-solid fa-eye btn text-primary"></i></a>',
        '<a class="remove"><i class="btn fa fa-trash text-danger"></i></a>',
        '</div>'
    ].join('');
}

// for edit and delete buttons
let selectedviewRow;
window.operateEvents = {
    'click .view': function (e, value, row, index) {
        const chartData = [
            { name: "Total Exemplaire", value: row.totalExemplaire },
            { name: "Perdu", value: row.perduNumber },
            { name: "Disponible", value: row.disponibleNumber },
            { name: "Prêt", value: row.pretNumber },
            { name: "En cours de traitement", value: row.enCoursDeTraitementNumber }
        ];
        
        $("#viewDocumentModal").modal('show')
    },
    'click .remove': function (e, value, row, index) {
            $("#deleteConfirmationID").text(row.idNotice);
            $("#deleteConfirmation").modal('show')
    }
}

//deleteButton
$("#deleteConfirmationButton").click(function (e) { 
    let = idNotice = $("#deleteConfirmationID").text();
    $.get($(this).data("url"), {idNotice : idNotice},
        function (data) {
            if(data){
                $("#ExemplaireTable").bootstrapTable('remove', {
                    field: 'idNotice',
                    values: [idNotice]
                })
                $('#deleteExemplaireSuccessToast').toast('show');
            }else{
                $('#deleteExemplaireFaildToast').toast('show');
            }
            $("#deleteConfirmation").find(".btn-close").click();
        }
    );
});

