$(document).ready(function () {
    $("#tableArtistsGrid").DataTable({
        "ajax": {
            "url": "/Artists/GetList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "FullName" },
            { "data": "CountryName" },
            {
                "data": "BirthDate", "className": "datetime",
                "render": function (BirthDate) {
                    var fullDate = new Date(parseInt(BirthDate.substr(6)));
                    var twoDigitMonth = (fullDate.getMonth() + 1) + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;

                    var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
                    var currentDate = twoDigitMonth + "/" + twoDigitDate + "/" + fullDate.getFullYear();

                    return currentDate;
                }
            },
            {
                "data": "Image",
                "orderable": false,
                "searchable": false,
                "sortable": false,
                "render": function (Image) {
                    return "<img id=\"imageSize\" src=\"data:image/jpeg;base64," + Image + "\" />";
                }
            },
            {
                "data": "Id",
                "searchable": false,
                "sortable": false,
                "orderable": false,
                "render": function (Id) {
                    return `<div class="btn-group" role="group">
                                <button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Artists/Edit/` + Id + `" id="btnEditArtist">
                                    <span class="fa fa-pencil" aria-hidden="true"></span> Edit
                                </button>
                                <button type="button" class="btn btn-warning btn-md" data-toggle="modal" data-url="/Artists/Details/` + Id + `" id="btnDetailsArtist">
                                    <span class="fa fa-eye" aria-hidden="true"></span> Details
                                </button>
                                <button type="button" class="btn btn-danger btn-md" data-toggle="modal" data-url="/Artists/Delete/` + Id + `" id="btnDeleteArtist">
                                    <span class="fa fa-trash" aria-hidden="true"></span> Delete
                                </button>
                            </div>`;
                }
            }
        ]
    });
});

$("#btnCreateArtist").on("click", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#createArtistContainer').html(data);

        $('#createArtistModal').modal('show');

        $('.chosen-select').chosen({
            no_results_text: "Oops, nothing found!",
            placeholder_text_multiple: "Please, select bands",
            placeholder_text_single: "Please, select country",
            hide_results_on_select: false
        }).on('change', function (obj, result) {
            console.debug("changed: %o", arguments);
        });
    });


});

$("#tableArtistsGrid").on("click", "#btnEditArtist", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#editArtistContainer').html(data);

        $('#editArtistModal').modal('show');
        $('.chosen-select').chosen({
            no_results_text: "Oops, nothing found!",
            placeholder_text_multiple: "Please, select bands",
            placeholder_text_single: "Please, select country",
            hide_results_on_select: false
        }).on('change', function (obj, result) {
            console.debug("changed: %o", arguments);
        });
    });

});

$("#tableArtistsGrid").on("click", "#btnDeleteArtist", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#deleteArtistContainer').html(data);

        $('#deleteArtistModal').modal('show');
    });

});

$("#tableArtistsGrid").on("click", "#btnDetailsArtist", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#detailsArtistContainer').html(data);

        $('#detailsArtistModal').modal('show');
    });

});

function DeleteArtistSuccess(data) {

    if (data.data != "success") {
        $('#deleteArtistContainer').html(data.data);
        return;
    }
    $.notify({
        // options
        icon: 'fa fa-check-circle',
        title: '<strong>Success</strong>: ',
        message: 'Deleted!'
    }, {
            type: 'success',
            z_index: 1051,
            animate: {
                enter: 'animated bounceIn',
                exit: 'animated bounceOut'
            }
        });
    $('#deleteArtistModal').modal('hide');
    $('#deleteArtistContainer').html("");
    $('#tableArtistsGrid').DataTable().ajax.reload();
}

function CreateArtistSuccess(data) {

    if (data.data != "success") {
        $('#createArtistContainer').html(data.data);
        ErrorNotify(data);
        return;
    }
    $('#createArtistModal').modal('hide');
    $('#createArtistContainer').html("");
    $('#tableArtistsGrid').DataTable().ajax.reload();
}

function UpdateArtistSuccess(data) {

    if (data.data != "success") {
        $('#editArtistContainer').html(data.data);
        ErrorNotify(data);
        return;
    }
    $('#editArtistModal').modal('hide');
    $('#editArtistContainer').html("");
    $('#tableArtistsGrid').DataTable().ajax.reload();
}

function ErrorNotify(data) {
    if (data.errorMessage.length >= 1) {
        data.errorMessage.forEach(function (item) {
            $.notify({
                // options
                icon: 'fa fa-warning',
                title: '<strong>Warning</strong>: ',
                message: item
            }, {
                    type: 'warning',
                    z_index: 1051,
                    animate: {
                        enter: 'animated bounceIn',
                        exit: 'animated bounceOut'
                    }
                });
        });
    }
}