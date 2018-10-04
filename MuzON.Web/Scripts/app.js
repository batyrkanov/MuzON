// Crud for Artists
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
                    return `<button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Artists/Edit/` + Id + `" id="btnEditArtist">
                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Edit
                            </button>`;
                }
            },
            {
                "data": "Id",
                "searchable": false,
                "sortable": false,
                "orderable": false,
                "render": function (Id) {
                    return `<button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Artists/Details/` + Id + `" id="btnDetailsArtist">
                                <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span> Details
                            </button>`;
                }
            },
            {
                "data": "Id",
                "orderable": false,
                "searchable": false,
                "sortable": false,  
                "render": function (Id) {
                    return `<button type="button" class="btn btn-danger btn-md" data-toggle="modal" data-url="/Artists/Delete/` + Id + `" id="btnDeleteArtist">
                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> Delete
                            </button>`;
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
    });

});

$("#tableArtistsGrid").on("click", "#btnEditArtist", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#editArtistContainer').html(data);

        $('#editArtistModal').modal('show');
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

    if (data != "success") {
        $('#deleteArtistContainer').html(data);
        return;
    }
    $('#deleteArtistModal').modal('hide');
    $('#deleteArtistContainer').html("");
}

function CreateArtistSuccess(data) {

    if (data != "success") {
        $('#createArtistContainer').html(data);
        return;
    }
    $('#createArtistModal').modal('hide');
    $('#createArtistContainer').html("");
}

function UpdateArtistSuccess(data) {

    if (data != "success") {
        $('#editArtistContainer').html(data);
        return;
    }
    $('#editArtistModal').modal('hide');
    $('#editArtistContainer').html("");

}

//crud for bands
$(document).ready(function () {
    $("#tableBandsGrid").DataTable({
        "ajax": {
            "url": "/Bands/GetList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Name" },
            { "data": "CountryName" },
            {
                "data": "CreatedDate", "className": "datetime",
                "render": function (CreatedDate) {
                    var fullDate = new Date(parseInt(CreatedDate.substr(6)));
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
                    return `<button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Bands/Edit/` + Id + `" id="btnEditBand">
                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Edit
                            </button>`;
                }
            },
            {
                "data": "Id",
                "searchable": false,
                "sortable": false,
                "orderable": false,
                "render": function (Id) {
                    return `<button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Bands/Details/` + Id + `" id="btnDetailsBand">
                                <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span> Details
                            </button>`;
                }
            },
            {
                "data": "Id",
                "orderable": false,
                "searchable": false,
                "sortable": false,
                "render": function (Id) {
                    return `<button type="button" class="btn btn-danger btn-md" data-toggle="modal" data-url="/Bands/Delete/` + Id + `" id="btnDeleteBand">
                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> Delete
                            </button>`;
                }
            }
        ]
    });
});

$("#btnCreateBand").on("click", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#createBandContainer').html(data);

        $('#createBandModal').modal('show');
    });

});

$("#tableBandsGrid").on("click", "#btnDetailsBand", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#detailsBandContainer').html(data);

        $('#detailsBandModal').modal('show');
    });

});

$("#tableBandsGrid").on("click", "#btnEditBand", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#editBandContainer').html(data);

        $('#editBandModal').modal('show');
    });

});

$("#tableBandsGrid").on("click", "#btnDeleteBand", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#deleteBandContainer').html(data);

        $('#deleteBandModal').modal('show');
    });

});

function DeleteBandSuccess(data) {

    if (data != "success") {
        $('#deleteBandContainer').html(data);
        return;
    }
    $('#deleteBandModal').modal('hide');
    $('#deleteBandContainer').html("");
}

function CreateBandSuccess(data) {

    if (data != "success") {
        $('#createBandContainer').html(data);
        return;
    }
    $('#createBandModal').modal('hide');
    $('#createBandContainer').html("");
}

function UpdateBandSuccess(data) {

    if (data != "success") {
        $('#editBandContainer').html(data);
        return;
    }
    $('#editBandModal').modal('hide');
    $('#editBandContainer').html("");

}