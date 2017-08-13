
$(document).ready(function () {

    $(".sidebar-menu .menu:eq(1) a").addClass("active")
    

    $("#addItem").hide();


    $("#btnAddInvoice").on('click', function () {
        $("form").find("input[type=text], textarea").val("")
        $("form").find("select").val("")
        $("#addInvoice").show();

        $("#InvoiceId").val('0');
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
             '<a id="addItem" class="addItem ml10 isAllowDelete" href="javascript:void(0)" title="Add Item(s) to Invoice">',
                '<span class="btn btn-info btn-sm glyphicon glyphicon-plus"></span>',
            '</a>'

        ].join('');





    }


    // Link Events Edit and Delete
    window.operateEvents = {

        'click .edit': function (e, value, row, index) {
            
             $("#InvoiceId").val(row.InvoiceId);
            $.ajax({
                type: "POST",
                url: $_EditInvoice,
                data: row,
                success: function (data) {
                    debugger;
                    $("#addInvoice").empty();
                    $("#addInvoice").html(data);

                    $("#btnSave").html("Update");
                    
                    $("#addInvoice").show();
                    AutocompleteInit();
                    
                    

                }
            });
        },

        'click .remove': function (e, value, row, index) {
      
            $.ajax({
                type: "Get",
                url: $_DeleteInvoiceItems,
                data: { InvoiceId: row.InvoiceId },
                success: function (resultdata) {
                    $("#InvoiceItems").html(resultdata);
                    $(".InvoiceItem").modal('show');

                },

                headers: {
                    'RequestVerificationToken': $("#TokenValue").val()//'@TokenHeaderValue()'
                }
            });

        
        },
        'click .addItem': function (e, value, row, index) {
            debugger;
            $("#InvoiceId").val(row.InvoiceId);
            //$(".InvoiceItem").modal("show");

            $.ajax({
                type: "GET",
                url: $_GetInvoiceItem,
                data: { InvoiceId: row.InvoiceId },
                success: function (data) {
                    debugger;
                    $("#InvoiceItems").empty();
                    $("#InvoiceItems").html(data);
                    $(".InvoiceItem").modal("show");

                }
            });


            $("#IdInvoice").val(row.InvoiceId);

        }
    };
    debugger;


    var headers = {};
    // alert("Header1=" + headers);
    $('#grid').bootstrapTable({
        headers: headers,
        method: 'post',
        url: $_InvoiceList,
        cache: true,
        height: 500,
        classes: 'table table-hover',
        queryParams: function (param) {
            param.model = {
                InvoiceId: $("#txtInvoiceId").val() == "" ? 0 : $("#txtInvoiceId").val()

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
             //    field: 'InvoiceId',
             //    title: 'InvoiceId',
             //    checkbox: false,
             //    type: 'search',
             //    visible: false,
             //    switchable: false,
             //    sortable: true,
             //}
             //,
              {
                  field: 'InvoiceId',
                  title: 'Invoice Number',
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
                    field: 'InvoiceTypeName',
                    title: 'Invoice Type',
                    checkbox: false,
                    type: 'search',
                    sortable: true,
                }
           ,

                {
                    field: 'CreatedDate',
                    title: 'Created Date',
                    checkbox: false,
                    type: 'search',
                    formatter: function (value, row, index) {
                        return new Date(row.CreatedDate).toDateString();

                    }
                 
                }
                 
                //,
                // {
                //     field: 'Installer',
                //     title: 'Installer',

                // },

                 ,
                 {
                     field: 'StatusName',
                     title: 'Invoice Status',

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
    $("#addInvoice").hide();

}

function AddSuccess(data) {
    debugger;
    HideLoader();
    RefreshGrid();
    $("#addInvoice").hide();
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