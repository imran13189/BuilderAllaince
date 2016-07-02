var itemStatusList = {};

$(document).ready(function () {
  

    var html = GetItemStatus();
   
   
   

    $(".FilterSearch").keyup(function () {
            
           
            

            $('#grid').bootstrapTable('filterBy');
        
        });

    // status list
        function operateItemStatus(value, row, index) {


            return html;




        }


        window.operateItemStatusChange = {

            'change .itemStatusChange': function (e, value, row, index) {
                row.ItemStatus = $(this).val();

                $.ajax({
                    type: "POST",
                    url: $_ChangeOrderItemStatus,
                    data:row,
                    success: function (data) {
                        debugger;
                        RefreshItemStatusGrid();
                      
                    }
                });
            }

        };


    // Edit and Delete Formattte links
        function operateApplyFormatters(value, row, index) {


            return [
                '<a id="statusChange" class="statusChange ml10 isAllowDelete" href="javascript:void(0)" title="Apply discount">',
                    '<span class="btn btn-success btn-sm">Apply</span>',
                '</a>'
            ].join('');





        }


    // Link Events Edit and Delete
        window.operateApply = {

            'click .statusChange': function (e, value, row, index) {
                debugger;
                $.ajax({
                    type: "POST",
                    url: $_EditTruck,
                    data: row,
                    success: function (data) {
                        $("#addTruck").empty();
                        $("#addTruck").html(data);
                        $("#btnSave").html("Update");
                        $("#addTruck").show();

                        ApplyValidation();
                    }
                });
            }
            
        };

   
        var headers = {};
        // alert("Header1=" + headers);
        $('#gridItemSatus').bootstrapTable({
            headers: headers,
            method: 'post',
            url: $_OrderItem,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function (param) {
                param.model = {
                    ManufacturerName: "",
                    ItemSKU:"",
                    ItemName:"",
                    OrderId: $("#IdOrder").val()

                };
                debugger;
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
            //   rowStyle: rowStyle,
            toolbar: '#custom-toolbar',
            columns: [
                 {
                     field: 'OrderItemId',
                     title: 'OrderItemId',
                     checkbox: false,
                     type: 'search',
                     visible: false,
                     switchable: false,
                     sortable: true,
                 }
                 ,
                  {
                      field: 'ItemSKU',
                      title: 'Item SKU',
                      checkbox: false,
                      type: 'search',

                  }
                  ,

                  {
                      field: 'ManufacturerName',
                      title: 'Manufacturer',
                      checkbox: false,
                      type: 'search',

                  }

                   ,

                  {
                      field: 'CostPrice',
                      title: 'Cost Price',
                      checkbox: false,
                      type: 'search',

                  }
                  ,



                    {
                        field: 'Quantity',
                        title: 'Quantity',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
                    },
                    {
                        field: 'TotalCost',
                        title: 'TotalCost',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
                    },
                    {
                        field: 'Multiplier',
                        title: 'Multiplier',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
                    }

                     ,

                    {
                        field: 'SellingPrice',
                        title: 'Cost',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
                    }
                    ,
                     {
                         field: 'StatusName',
                         title: 'Status',

                     },




                      {
                          field: 'operate',
                          title: 'ItemStatus',


                          clickToSelect: false,
                          formatter: operateItemStatus,
                          events:operateItemStatusChange
                      }
                      //,
                      //  {
                      //      field: 'operate1',
                      //      title: 'Actions',


                      //      clickToSelect: false,
                      //      formatter: operateApplyFormatters,
                      //      events: operateApply
                      //  }


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


    //$(".itemstatuschange").change(function (e) {
    //    debugger;
    //});
    
});


function RefreshItemStatusGrid() {
    $('#gridItemSatus').bootstrapTable('refresh', { silent: true });

}

function Close()
{
        $("#addTruck").hide();

}

function AddSuccess(data) {
    debugger;
    HideLoader();
    RefreshGrid();
    $("#addTruck").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }
  
}

//Get status list
function GetItemStatus()
{
    $.ajax({
        type: "GET",
        async:false,
        url: $_ItemStatusList,
        success: function (data) {
            debugger;
            itemStatusList = data;
        }
    });

    var html = "<Select class='itemStatusChange'><option>----Select----</option>";
    debugger;

    $.each(itemStatusList, function (i, item) {
        html += "<option value=" + item.ItemStatusId + ">" + item.StatusName + "</option>";

    });

    return html;

}


//$(document).on("change", ".itemstatuschange", function (e) {

//    debugger;

//    $.ajax({
//        url: $_GetItemByManufacturer,
//        type: "GET",
//        // async: false,
//        data: { ManufacturerId: $(this).val() },
//        success: function (result) {
//            debugger;
//            $("#ItemId").empty();
//            $("#ItemId").append("<option value>----Select---</option>")
//            $.each(result, function (i, item) {
//                debugger;
//                $("#ItemId").append("<option value=" + item.ItemId + ">" + item.ItemName + "</option>")

//            });

//        }

//    });

//});

function DeleteOrder(OrderId)
{
    BootstrapDialog.show({
        title: 'Confirmation',
        message: "Are you sure ?",
        buttons: [{
            label: 'Yes',
            cssClass: 'btn-primary',
            action: function (dialogItself) {
                $.ajax({
                    type: "Get",
                    url: $_DeleteOrder,
                    data: { OrderId: OrderId },
                    success: function (resultdata) {
                        debugger;
                        dialogItself.close();
                        ShowConfirmMessage("Deleted successfully");
                        RefreshGrid();
                        $("#OrderItem").empty();
                        $(".OrderItem").modal('hide');

                    },

                    headers: {
                        'RequestVerificationToken': $("#TokenValue").val()//'@TokenHeaderValue()'
                    }
                });
            }
        }, {
            label: 'No',
            cssClass: 'btn-danger',
            action: function (dialogItself) {
                dialogItself.close();
            }
        }]
    });
}