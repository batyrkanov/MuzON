$(document).ready(function () {
    $("#tableGrid").DataTable({
        "ajax": {
            "url": "/Artists/GetList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "FullName" },
            { "data": "CountryId" },
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
                "data": "Image", "render": function (Image) {
                    return "<img id=\"imageSize\" src=\"data:image/jpeg;base64," + Image + "\" />";
                }
            },
            {
                "data": "Id", "render": function (Id) {
                    return "<a class=\"btn btn-info\" href=/Artists/Edit/" + Id + ">Edit</a>";
                }
            },
            {
                "data": "Id", "render": function (Id) {
                    return "<a class=\"btn btn-danger\" href=/Artists/Delete/" + Id + ">Delete</a>";
                }
            }
        ]
    });
});
$(document).on('shown.bs.modal', function (e) {
    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
});

function Edit() {
    $('#myModal').modal('show');
    $('#exampleModalLabel').text('Edit Artist');
    $('#MessageError').text('');
}
function Create() {
    $('#myModal').modal('show');
    $('#exampleModalLabel').text('Add Artist');
    $('#MessageError').text('');
}
