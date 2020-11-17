var table = null;
var Items = [];
var Buyers = [];

$(document).ready(function () {
    table = $('#Receipt').DataTable({
        "processing": true,
        "ajax": {
            url: "/Receipts/LoadReceipt",
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
                "data": "orderDate"
            },
            {
                "data": "itemName"
            },
            {
                "data": "quantity"
            },
            {
                "data": "totalPrice"
            },
            {
                "data": "buyerName"
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
    $('#OrderDate').val('');
    $('#Quantity').val('');
    $('#TotalPrice').val('');
    $('#Update').hide();
    $('#Save').show();
}

function LoadItem(element) {
    if (Items.length === 0) {
        $.ajax({
            type: "GET",
            url: "/Items/LoadItem",
            success: function (data) {
                Items = data;
                renderItem(element);
            }
        })
    }
    else {
        renderItem(element);
    }
}

function renderItem(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Item').hide());
    $.each(Items, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}
LoadItem($('#ItemOption'));

function LoadBuyer(element) {
    if (Buyers.length === 0) {
        $.ajax({
            type: "GET",
            url: "/Buyers/LoadBuyer",
            success: function (data) {
                Buyers = data;
                renderBuyer(element);
            }
        })
    }
    else {
        renderBuyer(element);
    }
}

function renderBuyer(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Buyer').hide());
    $.each(Buyers, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}
LoadBuyer($('#BuyerOption'));

function GetById(id) {
    $.ajax({
        url: "/Receipts/GetById/",
        data: { id: id }
    }).then((result) => {
        if (result) {
            $('#Id').val(result.id);
            $('#OrderDate').val(result.orderDate);
            $('#ItemOption').val(result.itemId);
            $('#Quantity').val(result.quantity);
            $('#TotalPrice').val(result.totalPrice);
            $('#BuyerOption').val(result.buyerId);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function Save() {
    var Receipt = new Object();
    Receipt.orderDate = $('#OrderDate').val();
    Receipt.itemId = $('#ItemOption').val();
    Receipt.quantity = $('#Quantity').val();
    Receipt.totalPrice = $('#TotalPrice').val();
    Receipt.buyerId = $('#BuyerOption').val();
    $.ajax({
        type: 'POST',
        url: '/Receipts/InsertOrUpdate/',
        data: Receipt
    }).then((result) => {
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Receipt Added Successfully'
            });
            table.ajax.reload();
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Update() {
    var Receipt = new Object();
    Receipt.id = $('#Id').val();
    Receipt.orderDate = $('#OrderDate').val();
    Receipt.itemId = $('#ItemOption').val();
    Receipt.quantity = $('#Quantity').val();
    Receipt.totalPrice = $('#TotalPrice').val();
    Receipt.buyerId = $('#BuyerOption').val();
    $.ajax({
        type: 'POST',
        url: '/Receipts/InsertOrUpdate/',
        data: Receipt
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
                url: "/Receipts/Delete/",
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