$(document).ready(function () {
    $("#tableSongsGrid").DataTable({
        "ajax": {
            "url": "/Songs/GetList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Name" },
            {
                'render': function (row, type, data) {
                    return `<audio controls preload="metadata">  
                                <source src="/songs/` + data.Id + `/` + data.FileName + `" type="audio/mp3">  
                            </audio>`;
                },
                'orderable': false,
                'className': 'dt-right',
                'targets': [4]
            },
            {
                "data": "Id",
                "searchable": false,
                "sortable": false,
                "orderable": false,
                "render": function (Id) {
                    return `<div class="btn-group" role="group">
                                <button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Songs/Edit/` + Id + `" id="btnEditSong">
                                    <span class="fa fa-pencil" aria-hidden="true"></span> Edit
                                </button>
                                <button type="button" class="btn btn-info btn-md" data-toggle="modal" data-url="/Songs/Details/` + Id + `" id="btnDetailsSong">
                                    <span class="fa fa-eye" aria-hidden="true"></span> Details
                                </button>
                                <button type="button" class="btn btn-danger btn-md" data-toggle="modal" data-url="/Songs/Delete/` + Id + `" id="btnDeleteSong">
                                    <span class="fa fa-trash" aria-hidden="true"></span> Delete
                                </button>
                            </div>`;
                }
            }
        ]
    });
    $("audio").on("play", function () {
        $("audio").not(this).each(function (index, audio) {
            audio.pause();
        });
    });
});

$("#btnCreateSong").on("click", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#createSongContainer').html(data);

        $('#createSongModal').modal('show');

        //$('#artists').multiselect({
        //    enableClickableOptGroups: true
        //});
        $('.chosen-select').chosen({
            no_results_text: "Oops, nothing found!",
            placeholder_text_multiple: "Please, select some option",
            hide_results_on_select: false
        }).on('change', function (obj, result) {
            console.debug("changed: %o", arguments);
        });
    });

});

$("#tableSongsGrid").on("click", "#btnDetailsSong", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#detailsSongContainer').html(data);

        $('#detailsSongModal').modal('show');
    });

});

$("#tableSongsGrid").on("click", "#btnEditSong", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#editSongContainer').html(data);

        $('#editSongModal').modal('show');
        $('.chosen-select').chosen({
            no_results_text: "Oops, nothing found!",
            placeholder_text_multiple: "Please, select some option"
        }).on('change', function (obj, result) {
            console.debug("changed: %o", arguments);
        });
    });

});

$("#tableSongsGrid").on("click", "#btnDeleteSong", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#deleteSongContainer').html(data);

        $('#deleteSongModal').modal('show');
    });

});

function DeleteSongSuccess(data) {

    if (data.data != "success") {
        $('#deleteSongContainer').html(data.data);
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
    $('#deleteSongModal').modal('hide');
    $('#deleteSongContainer').html("");
    $('#tableSongsGrid').DataTable().ajax.reload();
}

function CreateSongSuccess(data) {
    console.log(data);
    if (data.data != "success") {
        $('#createSongContainer').html(data.data);
        ErrorNotify(data);
        return;
    }
    $('#createSongModal').modal('hide');
    $('#createSongContainer').html("");
    $('#tableSongsGrid').DataTable().ajax.reload();
}

function UpdateSongSuccess(data) {

    if (data.data != "success") {
        $('#editSongContainer').html(data.data);
        ErrorNotify(data);
        return;
    }
    $('#editSongModal').modal('hide');
    $('#editSongContainer').html("");
    $('#tableSongsGrid').DataTable().ajax.reload();
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