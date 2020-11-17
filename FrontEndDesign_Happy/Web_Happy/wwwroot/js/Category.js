var table = null;

$(document).ready(function () {
    table = $('#Category').DataTable({
        "processing": true,
        "ajax": {
            url: "/Categorys/LoadCategory",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "data": null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "name"
            },
            {
                "data": null, "sortable": false,
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.id + ')"><i class="fa fa-pencil"></i></button>' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.id + ')"><i class="fa fa-eraser"></i></button>'
                }
            }]
    })
})

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Save').show();
}

function GetById(id) {
    $.ajax({
        url: "/Categorys/GetById/",
        data: { id: id }
    }).then((result) => {
        if (result) {
            $('#Id').val(result.id);
            $('#Name').val(result.name);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function Save() {
    var Category = new Object();
    Category.name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: '/Categorys/InsertOrUpdate/',
        data: Category
    }).then((result) => {
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Category Added Successfully'
            });
            table.ajax.reload();
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Update() {
    var Category = new Object();
    Category.id = $('#Id').val();
    Category.name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: '/Categorys/InsertOrUpdate/',
        data: Category
    }).then((result) => {
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Update Successfully'
            });
            table.ajax.reload();
        } else {
            Swal.fire('Error', 'Failed to Update', 'error');
            ClearScreen();
        }
    })
}

function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Categorys/Delete/",
                data: { id: id }
            }).then((result) => {
                if (result.statusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                    table.ajax.reload();
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        }
    })
}