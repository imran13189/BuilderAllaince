﻿
$(document).ready(function () {

    $(".sidebar-menu .menu:eq(1) a").addClass("active")
    

    $("#addOrderItem").hide();


    $("#btnAddItem").on('click', function () {
        debugger;
       // $("form").find("input[type=text], textarea").val("")
        $("form").find("select").val("")
        $("#addOrderItem").show();

        $("#OrderItemId").val('0');
        $("#btnSaveItem").html("Add");
     //   ApplyValidation('ItemForm');
    });



    $(".FilterSearch1").keyup(function () {
        $('#gridItems').bootstrapTable('filterBy');
    });




    // Edit and Delete Formattte links
    function operateFormatters(value, row, index) {


        return [
             '<a id="edit"  class="edit ml10 isAllowEdit" href="javascript:void(0)" title="Edit">',
                '<span class="btn btn-info btn-sm glyphicon glyphicon-edit"></span>',
            '</a>&nbsp;',
            '<a id="delete" class="remove ml10 isAllowDelete" href="javascript:void(0)" title="Remove">',
                '<span class="btn btn-danger btn-sm glyphicon glyphicon-remove"></span>',
            '</a>&nbsp;',
            '<a id="discount" class="discount ml10 isAllowDelete" href="javascript:void(0)" title="Apply discount">',
                '<span class="btn btn-success btn-sm glyphicon glyphicon-tasks"></span>',
            '</a>'
        ].join('');





    }


    // Link Events Edit and Delete
    window.operateItemEvents = {

        'click .edit': function (e, value, row, index) {
            debugger;
         
            $.ajax({
                type: "POST",
                url: $_EditOrderItem,
                data: row,
                success: function (data) {
                    debugger;
                    $("#addOrderItem").empty();
                    $("#addOrderItem").html(data);

                    $("#btnSaveItem").html("Update");
                    
                    $("#addOrderItem").show();
                    $("#ManufacturerId").val(row.ManufacturerId);
                    
                    $("#ManufacturerId").trigger("change");
                    $("#ItemId").val(row.ItemId);
                    ApplyValidation('ItemForm');
                }
            });
        },

        'click .remove': function (e, value, row, index) {
            debugger;
            BootstrapDialog.show({
                title: 'Confirmation',
                message: "Are you sure ?",
                buttons: [{
                    label: 'Yes',
                    cssClass: 'btn-primary',
                    action: function (dialogItself) {
                        $.ajax({
                            type: "Get",
                            url: $_DeleteOrderItem,
                            data: { OrderItemId: row.OrderItemId },
                            success: function (resultdata) {
                                debugger;
                                dialogItself.close();
                                ShowConfirmMessage("Deleted successfully");
                                RefreshItemGrid();


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

        },
        'click .discount': function (e, value, row, index) {
            
            $.ajax({
                type: "POST",
                url: $_GetManufacturerDiscount,
                data: { model: row },
                success: function (data) {

                    $("#Discount").empty();
                    $("#Discount").html(data);
                    $(".Discount").modal('show');

                }
            });
        },
    };
    debugger;


    var headers = {};
    // alert("Header1=" + headers);
    $('#gridItems').bootstrapTable({
        headers: headers,
        method: 'post',
        url: $_OrderItem,
        cache: true,
        height: 500,
        classes: 'table table-hover',
        queryParams: function (param) {
            param.model = {
                ManufacturerName: $("#txtManufacturer").val(),
                ItemSKU: $("#txtItemSKU").val(),
                ItemName: $("#txtItemName").val(),
                OrderId:$("#OrderId").val()

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
                 switchable: false
                 
             }
             ,
              {
                  field: 'ItemSKU',
                  title: 'Item SKU',
                  checkbox: false,
                  type: 'search',
                  sortable: true

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
                  field: 'ListPrice',
                  title: 'List Price',
                  checkbox: false,
                  type: 'search',

              }
              ,

               

                {
                    field: 'Quantity',
                    title: 'Quantity',
                    checkbox: false,
                    type: 'search'
                    
                },
                {
                    field: 'TotalCost',
                    title: 'TotalCost',
                    checkbox: false,
                    type: 'search'
                    
                },
                {
                    field: 'Multiplier',
                    title: 'Multiplier',
                    checkbox: false,
                    type: 'search'
                    
                }
             
                 ,

                {
                    field: 'SellingPrice',
                    title: 'Selling Price',
                    checkbox: false,
                    type: 'search'
                    
                }
                ,

                 

                {
                    field: 'DeliveryDate',
                    title: 'Delivery Date',
                    checkbox: false,
                    type: 'search'

                }
                ,
                 {
                     field: 'StatusName',
                     title: 'Status Name',

                 },

                
               

                  {
                      field: 'operate',
                      title: 'Actions',


                      clickToSelect: false,
                      formatter: operateFormatters,
                      events: operateItemEvents
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
    //         async:false,
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

$(document).on("change","#ManufacturerId",function () {

    debugger;

    $.ajax({
        url: $_GetItemByManufacturer,
        type: "GET",
       async: false,
        data: { ManufacturerId: $(this).val() },
        success: function (result) {
            debugger;
            $("#ItemId").empty();
            $("#ItemId").append("<option value>----Select---</option>")
            $.each(result, function (i, item) {
                debugger;
                $("#ItemId").append("<option value=" + item.ItemId + ">" + item.ItemName + "</option>")

            });

        }

    });

});

function RefreshItemGrid() {
    $('#gridItems').bootstrapTable('refresh', { silent: true });

}

function CloseItem() {
    $("#addOrderItem").hide();

}

function AddItemSuccess(data) {
    debugger;
    HideLoader();
    RefreshItemGrid();
    $("#addOrderItem").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }

}



function SumMultiplier()
{
    var sum = 0;
    $(".discounTypeChk").each(function (i,item) {
        
        debugger;
      

        if($(item).find("input")[0].checked)
        {
            sum += parseFloat($(item).siblings(".Multiplier").html());
        }
       

    });

    $("#TotalDiscount").val(sum.toFixed(3));
}

function ApplyDiscount(OrderItemId)
{
    var model = [];
    $(".discounTypeChk").each(function (i, item) {

        debugger;


        if ($(item).find("input")[0].checked) {
            model.push({ OrderItemId: OrderItemId, DiscountTypeId: $(item).find(".discountType").val(), Multiplier: $(item).siblings(".Multiplier").html() });
        }


    });
    debugger;

    $.ajax({
        url: $_SaveDiscountType,
        type: "POST",
        async: false,
      //  dataType: 'json',
        //contentType: "application/json; charset=utf-8",
        data: { model: model } ,
        success: function (result) {

            $(".Discount").modal('hide');
            RefreshItemGrid();
        }

    });

}
