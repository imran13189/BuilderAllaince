
$(document).ready(function () {


    

    $("#addItem").hide();


   


    $(".FilterSearch").keyup(function () {
        $('#builderOrders').bootstrapTable('filterBy');
    });




    // Edit and Delete Formattte links
    function operateFormatter(value, row, index) {


        return [
            
             '<a id="addItem" class="addItem ml10 isAllowDelete" href="javascript:void(0)" title="View Items">',
                '<span class="btn btn-info btn-sm">View Items</span>',
            '</a>'

        ].join('');





    }


    // Link Events Edit and Delete
    window.operateEvents = {

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
                    $("#orderItemModal").empty();
                    $("#orderItemModal").html(data);
                    $(".OrderItem").modal("show");

                }
            });


            $("#IdOrder").val(row.OrderId);

        }
    };
    debugger;


    var headers = {};
    // alert("Header1=" + headers);
    $('#builderOrders').bootstrapTable({
        headers: headers,
        method: 'post',
        url: $_BuilderOrders,
        cache: true,
        height: 500,
        classes: 'table table-hover',
        queryParams: function (param) {
            param.model = {
             
                BuilderId: $("#BuilderId").val(),
                OrderId: $("#txtOrderId").val().trim() == "" ? 0 : $("#txtOrderId").val()

                //ManufacturerName: $("#txtManufacturer").val(),
                //ItemSKU: $("#txtItemSKU").val(),
                //ItemName: $("#txtItemName").val(),

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
    
    //    debugger;
    
    //       $.ajax({
    //           url: $_GetItemByManufacturer,
    //           type: "GET",
    //           async:false,
    //           data: { ManufacturerId: $(this).val() },
    //                  success:function(result)
    //                     {
    //                      debugger;
    //                      $("#ItemId").empty();
    //                      $("#ItemId").append("<option value>----Select---</option>")
    //                         $.each(result,function (i, item) {
    //                             debugger;
    //                             $("#ItemId").append("<option value="+item.ItemId+">"+item.ItemName+"</option>")

    //                             });

    //                     }

    //       });

    //});
});



function RefreshGrid() {
    $('#builderOrders').bootstrapTable('refresh', { silent: true });

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


