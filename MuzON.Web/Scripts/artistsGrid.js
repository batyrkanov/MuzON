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
                                <button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Artists/Details/` + Id + `" id="btnDetailsArtist">
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
        $('.chosen-select').chosen({}).change(function (obj, result) {
            console.debug("changed: %o", arguments);

            console.log("selected: " + result.selected);
        });
    });
    

});

$("#tableArtistsGrid").on("click", "#btnEditArtist", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#editArtistContainer').html(data);

        $('#editArtistModal').modal('show');
        $('.chosen-select').chosen({}).change(function (obj, result) {
            console.debug("changed: %o", arguments);

            console.log("selected: " + result.selected);
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

