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
                    return `<div class="btn-group" role="group">
                                <button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Bands/Edit/` + Id + `" id="btnEditBand">
                                    <span class="fa fa-pencil" aria-hidden="true"></span> Edit
                                </button>
                                <button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Bands/Details/` + Id + `" id="btnDetailsBand">
                                    <span class="fa fa-eye" aria-hidden="true"></span> Details
                                </button>
                                <button type="button" class="btn btn-danger btn-md" data-toggle="modal" data-url="/Bands/Delete/` + Id + `" id="btnDeleteBand">
                                    <span class="fa fa-trash" aria-hidden="true"></span> Delete
                                </button>
                            </div>`;
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
        
        $('.chosen-select').chosen({}).change(function (obj, result) {
            console.debug("changed: %o", arguments);

            console.log("selected: " + result.selected);
        });
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
        $('.chosen-select').chosen({}).change(function (obj, result) {
            console.debug("changed: %o", arguments);

            console.log("selected: " + result.selected);
        });
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

    if (data.data != "success") {
        $('#deleteBandContainer').html(data.data);
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
    $('#deleteBandModal').modal('hide');
    $('#deleteBandContainer').html("");
    $('#tableBandsGrid').DataTable().ajax.reload();
}

function CreateBandSuccess(data) {
    console.log(data);
    if (data.data != "success") {
        $('#createBandContainer').html(data.data);
        ErrorNotify(data);
        return;
    }
    $('#createBandModal').modal('hide');
    $('#createBandContainer').html("");
    $('#tableBandsGrid').DataTable().ajax.reload();
}

function UpdateBandSuccess(data) {

    if (data.data != "success") {
        $('#editBandContainer').html(data.data);
        ErrorNotify(data);
        return;
    }
    $('#editBandModal').modal('hide');
    $('#editBandContainer').html("");
    $('#tableBandsGrid').DataTable().ajax.reload();
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