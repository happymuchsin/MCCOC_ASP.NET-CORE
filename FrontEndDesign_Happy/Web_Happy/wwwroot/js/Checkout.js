var table = null;
var Receipts = [];

$(document).ready(function () {
    table = $('#Checkout').DataTable({
        "processing": true,
        "ajax": {
            url: "/Checkouts/LoadCheckout",
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
                "data": "receiptTotalPrice"
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
    $('#ReceiptOption').val('');
    $('#Update').hide();
    $('#Save').show();
}

function LoadReceipt(element) {
    if (Receipts.length === 0) {
        $.ajax({
            type: "GET",
            url: "/Receipts/LoadReceipt",
            success: function (data) {
                Receipts = data;
                renderReceipt(element);
            }
        })
    }
    else {
        renderReceipt(element);
    }
}

function renderReceipt(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Receipt').hide());
    $.each(Receipts, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.totalPrice));
    });
}
LoadReceipt($('#ReceiptOption'));

function GetById(id) {
    $.ajax({
        url: "/Checkouts/GetById/",
        data: { id: id }
    }).then((result) => {
        if (result) {
            $('#Id').val(result.id);
            $('#ReceiptOption').val(result.receiptId);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function Save() {
    var Checkout = new Object();
    Checkout.ReceiptId = $('#ReceiptOption').val();
    $.ajax({
        type: 'POST',
        url: '/Checkouts/InsertOrUpdate/',
        data: Checkout
    }).then((result) => {
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Checkout Added Successfully'
            });
            table.ajax.reload();
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Update() {
    var Checkout = new Object();
    Checkout.id = $('#Id').val();
    Checkout.ReceiptId = $('#ReceiptOption').val();
    $.ajax({
        type: 'POST',
        url: '/Checkouts/InsertOrUpdate/',
        data: Checkout
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
                url: "/Checkouts/Delete/",
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