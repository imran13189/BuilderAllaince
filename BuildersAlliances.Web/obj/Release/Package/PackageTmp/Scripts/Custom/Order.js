
$(document).ready(function () {

    $(".sidebar-menu .menu:eq(1) a").addClass("active")
    

    $("#addItem").hide();


    $("#btnAddOrder").on('click', function () {
        $("form").find("input[type=text], textarea").val("")
        $("form").find("select").val("")
        $("#addOrder").show();

        $("#OrderId").val('0');
        $("#btnSave").html("Add");
        ApplyValidation();
    });



    $(".FilterSearch").keyup(function () {
        $('#grid').bootstrapTable('filterBy');
    });




    // Edit and Delete Formattte links
    function operateFormatter(value, row, index) {


        return [
             '<a id="edit"  class="edit ml10 isAllowEdit" href="javascript:void(0)" title="Edit">',
                '<span class="btn btn-info btn-sm glyphicon glyphicon-edit"></span>',
            '</a>&nbsp;',
            '<a id="delete" class="remove ml10 isAllowDelete" href="javascript:void(0)" title="Remove">',
                '<span class="btn btn-danger btn-sm glyphicon glyphicon-remove"></span>',
            '</a>&nbsp;',
             '<a id="addItem" class="addItem ml10 isAllowDelete" href="javascript:void(0)" title="Add Item(s) to Order">',
                '<span class="btn btn-info btn-sm glyphicon glyphicon-plus"></span>',
            '</a>'

        ].join('');





    }


    // Link Events Edit and Delete
    window.operateEvents = {

        'click .edit': function (e, value, row, index) {
            
             $("#OrderId").val(row.OrderId);
            $.ajax({
                type: "POST",
                url: $_EditOrder,
                data: row,
                success: function (data) {
                    debugger;
                    $("#addOrder").empty();
                    $("#addOrder").html(data);

                    $("#btnSave").html("Update");
                    
                    $("#addOrder").show();
                    AutocompleteInit();
                    
                    

                }
            });
        },

        'click .remove': function (e, value, row, index) {
      
            $.ajax({
                type: "Get",
                url: $_DeleteOrderItems,
                data: { OrderId: row.OrderId },
                success: function (resultdata) {
                    $("#OrderItems").html(resultdata);
                    $(".OrderItem").modal('show');

                },

                headers: {
                    'RequestVerificationToken': $("#TokenValue").val()//'@TokenHeaderValue()'
                }
            });

            //BootstrapDialog.show({
            //    title: 'Confirmation',
            //    message: "Are you sure ?",
            //    buttons: [{
            //        label: 'Yes',
            //        cssClass: 'btn-primary',
            //        action: function (dialogItself) {
            //            $.ajax({
            //                type: "Get",
            //                url: $_DeleteOrder,
            //                data: { OrderId: row.OrderId },
            //                success: function (resultdata) {
            //                    debugger;
            //                    dialogItself.close();
            //                    ShowConfirmMessage("Deleted successfully");
            //                    RefreshGrid();


            //                },

            //                headers: {
            //                    'RequestVerificationToken': $("#TokenValue").val()//'@TokenHeaderValue()'
            //                }
            //            });
            //        }
            //    }, {
            //        label: 'No',
            //        cssClass: 'btn-danger',
            //        action: function (dialogItself) {
            //            dialogItself.close();
            //        }
            //    }]
            //});

        },
        'click .addItem': function (e, value, row, index) {
            debugger;
            $("#OrderId").val(row.OrderId);
            //$(".OrderItem").modal("show");

            $.ajax({
                type: "GET",
                url: $_GetOrderItem,
                data: { OrderId: row.OrderId },
                success: function (data) {
                    debugger;
                    $("#OrderItems").empty();
                    $("#OrderItems").html(data);
                    $(".OrderItem").modal("show");

                }
            });


            $("#IdOrder").val(row.OrderId);

        }
    };
    debugger;


    var headers = {};
    // alert("Header1=" + headers);
    $('#grid').bootstrapTable({
        headers: headers,
        method: 'post',
        url: $_OrderList,
        cache: true,
        height: 500,
        classes: 'table table-hover',
        queryParams: function (param) {
            param.model = {
                OrderId: $("#txtOrderId").val() == "" ? 0 : $("#txtOrderId").val()

                //ManufacturerName: $("#txtManufacturer").val(),
                //ItemSKU: $("#txtItemSKU").val(),
                //ItemName: $("#txtItemName").val(),

            };
            return param;
        },
        striped: true,
        pageNumber: 1,
        pagination: true,
        pageSize: 10,
        pageList: [5, 10, 20, 30],
        search: false,
        showColumns: true,
        //showRefresh: true,
        sidePagination: 'server',
        minimumCountColumns: 2,
        showHeader: true,
        showFilter: false,
        smartDisplay: true,
        clickToSelect: true,
        rowStyle: rowStyle,
        toolbar: '#custom-toolbar',
        columns: [
             //{
             //    field: 'OrderId',
             //    title: 'OrderId',
             //    checkbox: false,
             //    type: 'search',
             //    visible: false,
             //    switchable: false,
             //    sortable: true,
             //}
             //,
              {
                  field: 'OrderId',
                  title: 'Order Number',
                  checkbox: false,
                  type: 'search',

              }
               ,

                {
                    field: 'BuilderName',
                    title: 'Builder',
                    checkbox: false,
                    type: 'search',
                    sortable: true,
                }
             ,

                {
                    field: 'OrderTypeName',
                    title: 'Order Type',
                    checkbox: false,
                    type: 'search',
                    sortable: true,
                }
           ,

                {
                    field: 'CreatedDate',
                    title: 'Created Date',
                    checkbox: false,
                    type: 'search'
                 
                }
                 
                //,
                // {
                //     field: 'Installer',
                //     title: 'Installer',

                // },

                 ,
                 {
                     field: 'StatusName',
                     title: 'Order Status',

                 },
               

                  {
                      field: 'operate',
                      title: 'Actions',


                      clickToSelect: false,
                      formatter: operateFormatter,
                      events: operateEvents
                  }
        ],
        onSubmit: function () {
            var data = $('#filter-bar').bootstrapTableFilter('getData');
            console.log(data);
        },
        onLoadSuccess: function () {
            Addtitle();
        },
        onPageChange: function () {
            Addtitle();
        }
    });

    $(".columns.columns-right.btn-group.pull-right").remove()


    //for row styling 
    function rowStyle(row, index) {
        var classes = ['active', '', 'info', 'warning', 'danger'];

        if (index % 2 === 0) {
            return {
                classes: classes[1]
            };
        }
        return {};
    }
    //for adding titls -next,first,last,previos to paging
    function Addtitle() {
        $('.page-next').attr('Title', 'Next');
        $('.page-first').attr('Title', 'First');
        $('.page-last').attr('Title', 'Last');
        $('.page-pre').attr('Title', 'Previous');
    }


    $("#Name").on("keydown", function (e) {
        return e.which !== 32;
    });
    $("#Email").on("keydown", function (e) {
        return e.which !== 32;
    });
    $("#CellNumber").on("keydown", function (e) {
        return e.which !== 32;
    });

    //$("#ManufacturerId").change(function () {
    
    AutocompleteInit();
});



function RefreshGrid() {
    $('#grid').bootstrapTable('refresh', { silent: true });

}

function Close() {
    $("#addOrder").hide();

}

function AddSuccess(data) {
    debugger;
    HideLoader();
    RefreshGrid();
    $("#addOrder").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }

}


function AutocompleteInit()
{
    $("#SearchBuilder").autocomplete({

        minLength: 0,
        search: function () { $(this).addClass('ui-autocomplete-loading'); },
        open: function () { $(this).removeClass('ui-autocomplete-loading'); },
        source: function (request, response) {
            $.ajax({
                url: $_GetBuilders,
                type: "GET",
                dataType: "json",
                data: { BuilderName: request.term },
                success: function (data) {
                    if (data.length) {
                        response($.map(data, function (item) {
                            return { label: item.BuilderName, value: item.BuilderName, Id: item.BuilderId };
                        }))
                    }
                    else {
                        var result = [
                               {
                                   label: 'No matches found. Click here to add new',
                                   value: '',
                                   Id: 0
                               }
                        ];
                        return response(result);
                    }

                }
            })
        },
        select: function (event, ui) {
            if (ui.item.Id > 0) {

                $("#BuilderId").val(ui.item.Id);

            }

        },
        messages: {
            noResults: "", results: ""
        }
    });
}