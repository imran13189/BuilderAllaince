
var InvoiceItems = [];
 var $table;

$(document).ready(function () {

    $(".sidebar-menu .menu:eq(1) a").addClass("active")
    

    $("#addOrderInvoice").hide();


    $("#btnAddInvoice").on('click', function () {
        
       // $("form").find("input[type=text], textarea").val("")
        $("form").find("select").val("")
        $("#addOrderInvoice").show();

        $("#OrderInvoiceId").val('0');
        $("#btnSaveInvoice").html("Add");
     //   ApplyValidation('InvoiceForm');
    });



    $(".FilterSearch1").keyup(function () {
        $('#gridInvoices').bootstrapTable('filterBy');
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
    window.operateInvoiceEvents = {

        'click .edit': function (e, value, row, index) {
            
         
            $.ajax({
                type: "POST",
                url: $_EditOrderInvoice,
                data: row,
                success: function (data) {
                    
                    $("#addOrderInvoice").empty();
                    $("#addOrderInvoice").html(data);

                    $("#btnSaveInvoice").html("Update");
                    
                    $("#addOrderInvoice").show();
                    $("#ManufacturerId").val(row.ManufacturerId);
                    
                    $("#ManufacturerId").trigger("change");
                    $("#InvoiceId").val(row.InvoiceId);
                    ApplyValidation('InvoiceForm');
                }
            });
        },

        'click .remove': function (e, value, row, index) {
            
            BootstrapDialog.show({
                title: 'Confirmation',
                message: "Are you sure ?",
                buttons: [{
                    label: 'Yes',
                    cssClass: 'btn-primary',
                    action: function (dialogItself) {
                        $.ajax({
                            type: "Get",
                            url: $_DeleteOrderInvoice,
                            data: { OrderInvoiceId: row.OrderInvoiceId },
                            success: function (resultdata) {
                                
                                dialogItself.close();
                                ShowConfirmMessage("Deleted successfully");
                                RefreshInvoiceGrid();


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
    


    var headers = {};
    // alert("Header1=" + headers);
  $table=   $('#gridInvoices').bootstrapTable({
        headers: headers,
        method: 'post',
        url: $_OrderItem,
        cache: true,
        height: 529,
        classes: 'table table-hover',
        queryParams: function (param) {
            param.model = {
                ManufacturerName: "",
                ItemSKU: "",
                ItemName: "",
                OrderId:$("#OrderId").val()

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
     //   rowStyle: rowStyle,
        toolbar: '#custom-toolbar',
        columns: [

            {
                field: 'OrderItemChk',
                
                 checkbox: true,
                 
                 visible: true
                
                 
                 
             }
            
              
             ,
              {
                  field: 'ItemId',
                  title: 'Invoice SKU',
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

                 }
                 //,
                 //{
                 //     field: 'operate',
                 //     title: 'Actions',


                 //     clickToSelect: false,
                 //     formatter: operateFormatters,
                 //     events: operateInvoiceEvents
                 // }
        ],
        onSubmit: function () {
            var data = $('#filter-bar').bootstrapTableFilter('getData');
            console.log(data);
        },
        onLoadSuccess: function () {
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


  

    
    $table.on('check-all.bs.table', function (e, row) {
        debugger;
        $.each(row, function (index, value) {
            if ($.inArray(value.OrderItemId, InvoiceItems) == -1) {
                InvoiceItems.push(value.OrderItemId);
            }
        });
    });


    $table.on('check.bs.table', function (e, row) {
        debugger;
        if ($.inArray(row.OrderItemId, InvoiceItems) == -1) {
            InvoiceItems.push(row.OrderItemId);
        }
    });

    $table.on('uncheck.bs.table', function (e, row) {
        
        $.each(InvoiceItems, function (index, value) {

            if (value === row.OrderItemId) {
                var ind = InvoiceItems.indexOf(row.OrderItemId);
                InvoiceItems.splice(ind, 1);
            }
        });
    });


    $table.on('uncheck-all.bs.table', function (e, row) {
        
        $.each(row, function (index, value) {
            if ($.inArray(value.OrderItemId, InvoiceItems) > -1) {
               
                var ind = InvoiceItems.indexOf(value.OrderItemId);
                InvoiceItems.splice(ind, 1);

            }
        });
    });
  
  
});


function getIdSelections() {

    return $.map($table.bootstrapTable('getSelections'), function (row) {
        return row.OrderItemId;
    });
}

function responseHandler(res) {
    $.each(res.rows, function (i, row) {
        row.OrderItemChk = $.inArray(row.OrderItemId, InvoiceItems) !== -1;
    });
    return res;
}



function RefreshInvoiceGrid() {
    $('#gridInvoices').bootstrapTable('refresh', { silent: true });

}

function CloseInvoice() {
    $("#addOrderInvoice").hide();

}

function AddInvoiceSuccess(data) {
    
    HideLoader();
    RefreshInvoiceGrid();
    $("#addOrderInvoice").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }

}



function SumMultiplier()
{
    var sum = 0;
    $(".discounTypeChk").each(function (i,Invoice) {
        
        
      

        if($(Invoice).find("input")[0].checked)
        {
            sum += parseFloat($(Invoice).siblings(".Multiplier").html());
        }
       

    });

    $("#TotalDiscount").val(sum.toFixed(3));
}

function ApplyDiscount(OrderInvoiceId)
{
    var model = [];
    $(".discounTypeChk").each(function (i, Invoice) {

        


        if ($(Invoice).find("input")[0].checked) {
            model.push({ OrderInvoiceId: OrderInvoiceId, DiscountTypeId: $(Invoice).find(".discountType").val(), Multiplier: $(Invoice).siblings(".Multiplier").html() });
        }


    });
    

    $.ajax({
        url: $_SaveDiscountType,
        type: "POST",
        async: false,
      //  dataType: 'json',
        //contentType: "application/json; charset=utf-8",
        data: { model: model } ,
        success: function (result) {

            $(".Discount").modal('hide');
            RefreshInvoiceGrid();
        }

    });

}


function ItemCheckBox()
{
    debugger;
    $(".chkItems").each(function (i,items) {


        ap

        var itemId = parseInt($(items).val());

        if ($(items)[0].checked)
        {
            if ($.inArray(itemId, InvoiceItems) == -1)
            {
                InvoiceItems.push(itemId);
            }
        }
        else
        {
            if ($.inArray(itemId, InvoiceItems) > -1)
            {
                
                //var index = InvoiceItems.indexOf(itemId);
                //if (index > -1) {
                //    InvoiceItems=  InvoiceItems.splice(index, 1);
                //}
                InvoiceItems= jQuery.grep(InvoiceItems, function (value) {
                    return value != itemId;
                });
            }
        }

    });
}

function GenerateInvoice() {
    $.ajax({
        type: "POST",
        url: $_GenerateInvocie,
        data: { OrderId: $("#OrderId").val(), InvoiceItems: InvoiceItems },
        success: function (data) {
            $("#InvoiceModal").empty();
            $("#InvoiceModal").html(data);
            $(".InvoiceModal").modal("show");


        }
    });
}


function SendInvoice()
{
    ShowLoader();
   
    $("#invoice-btn").remove();
    var Email=  $("#BuilderEmail").html();
    var html = $("#InvoiceModal").html();

    var data = JSON.stringify({ InvoiceHtml: html, Email: Email });

    $.ajax({
        type: "POST",
        url: $_SentInvoice,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: data,
        success: function (data) {
        
            HideLoader();
             $(".InvoiceModal").modal("hide");
            ShowConfirmMessage("Invoice sent successfully");
        }
    });



}
