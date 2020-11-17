var table = null;
var Categorys = [];

$(document).ready(function () {
    table = $('#Item').DataTable({
        "processing": true,
        "ajax": {
            url: "/Items/LoadItem",
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
                "data": "stock"
            },
            {
                "data": "price"
            },
            {
                "data": "categoryName"
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
    $('#Stock').val('');
    $('#Price').val('');
    $('#Update').hide();
    $('#Save').show();
}

function LoadCategory(element) {
    if (Categorys.length === 0) {
        $.ajax({
            type: "GET",
            url: "/Categorys/LoadCategory",
            success: function (data) {
                Categorys = data;
                renderCategory(element);
            }
        })
    }
    else {
        renderCategory(element);
    }
}

function renderCategory(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Category').hide());
    $.each(Categorys, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}
LoadCategory($('#CategoryOption'));

function GetById(id) {
    $.ajax({
        url: "/Items/GetById/",
        data: { id: id }
    }).then((result) => {
        if (result) {
            $('#Id').val(result.id);
            $('#Name').val(result.name);
            $('#Stock').val(result.stock);
            $('#Price').val(result.price);
            $('#CategoryOption').val(result.categoryId);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function Save() {
    var Item = new Object();
    Item.name = $('#Name').val();
    Item.stock = $('#Stock').val();
    Item.price = $('#Price').val();
    Item.categoryId = $('#CategoryOption').val();
    $.ajax({
        type: 'POST',
        url: '/Items/InsertOrUpdate/',
        data: Item
    }).then((result) => {
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Item Added Successfully'
            });
            table.ajax.reload();
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Update() {
    var Item = new Object();
    Item.id = $('#Id').val();
    Item.name = $('#Name').val();
    Item.stock = $('#Stock').val();
    Item.price = $('#Price').val();
    Item.categoryId = $('#CategoryOption').val();
    $.ajax({
        type: 'POST',
        url: '/Items/InsertOrUpdate/',
        data: Item
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
                url: "/Items/Delete/",
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