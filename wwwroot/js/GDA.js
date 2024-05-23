//For loading icon
function loadingTemplate() {
    return '<i class="fa fa-spinner fa-spin fa-fw fa-2x"></i>'
}

// for edit and delete buttons
function operateFormatter(value, row, index) {
    return [
        '<div class="text-centers w-100 d-flex justify-content-around">',
        '<a class="edit"><i class="btn fa-solid fa-pen text-primary"></i></a>',
        '<a class="remove"><i class="btn fa fa-trash text-danger"></i></a>',
        '</div>'
    ].join('');
}
// for edit and delete buttons
window.operateEvents = {
    'click .edit': function (e, value, row, index) {
        $("#modifiedAdherentModal").modal('show')
    },
    'click .remove': function (e, value, row, index) {
        $("#adherentTable").bootstrapTable('remove', {
            field: 'idAdherent',
            values: [row.idAdherent]
        })
    }
}