var Param = {};
$(document).ready(function () {
   $(".sidebar-menu li:eq(1) a").addClass('active')
   $(".sidebar-menu li:eq(1) .sub li:eq(1)").addClass('active')

    $("#addItem").hide();


    $("#btnAdd").on('click', function () {
        $("form").find("input[type=text], textarea").val("")
        $("form").find("select").val("")
        $("#itemSearch").hide();
        $("#addItem").show();
        
        $("#ItemId").val('0');
        $("#btnSave").html("Add");
        ApplyValidation();
    });


    $("#btnAdvanceSearch").click(function () {
        $("#addItem").hide();
        $("#itemSearch").show();

    });


    $(".FilterSearch").keyup(function () {
        $('#grid').bootstrapTable('filterBy');
    });
   

      

    // Edit and Delete Formattte links
        function operateFormatter(value, row, index) {


            return [
                 '<a id="edit"  class="edit ml10 isAllowEdit" href="javascript:void(0)" title="Edit">',
                    '<span class="btn btn-info btn-sm glyphicon glyphicon-edit"></span>',
                '</a>&nbsp;&nbsp;',
                '<a id="delete" class="remove ml10 isAllowDelete" href="javascript:void(0)" title="Remove">',
                    '<span class="btn btn-danger btn-sm glyphicon glyphicon-remove"></span>',
                '</a>'
            ].join('');





        }


    // Link Events Edit and Delete
        window.operateEvents = {

            'click .edit': function (e, value, row, index) {
                debugger;
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $.ajax({
                    type: "POST",
                    url: $_EditItem,
                    data: row,
                    success: function (data) {
                        $("#itemSearch").hide();
                        $("#addItem").empty();
                        $("#addItem").html(data);
                        $("#btnSave").html("Update");
                        $("#addItem").show();
                        $("#ManufacturerId").trigger('change');
                        ApplyValidation();
                        $("#ColorId").val(row.ColorId);
                        $("#DoorStyleId").val(row.DoorStyleId);

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
                                url: $_DeleteItem,
                                data: { ItemId: row.ItemId },
                                success: function (resultdata) {
                                    debugger;
                                        dialogItself.close();
                                        ShowConfirmMessage("Deleted successfully");
                                        RefreshGrid();
                                   

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
        };
        debugger;

       
        var headers = {};
        // alert("Header1=" + headers);
        $('#grid').bootstrapTable({
            headers: headers,
            method: 'post',
            url: $_ItemList,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function(param){
                param.model = {
                    ItemSKU: $("#txtItemSKU").val(),
                   // Size: $("#txtSize").val(),
                    ManufacturerName:  $("#txtManufacturerName").val(),
                    ItemName: $("#txtItemName").val(),
                    ColorName: $("#txtItemColor").val(),
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
                 {
                     field: 'ItemId',
                     title: 'ItemId',
                     checkbox: false,
                     type: 'search',
                     visible: false,
                     switchable: false,
                     sortable: true,
                 },
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
                        sortable: true
                       
                    }
                  ,

                    {
                        field: 'ItemName',
                        title: 'Item Name',
                        checkbox: false,
                        type: 'search',
                        sortable: true
                       
                    }

                    ,
                     {
                         field: 'ColorName',
                         title: 'Color',
                        
                     },
                      
                     {
                         field: 'StyleName',
                         title: 'Door Style',

                     },
                     
                     {
                         field: 'Cubes',
                         title: 'Cubes',
                         
                     }
                    
                     ,
                     {
                         field: 'ListPrice',
                         title: 'List Price',
                        
                     }
                    ,

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
    
});


function RefreshGrid() {
    $('#grid').bootstrapTable('refresh', { silent: true });

}

function Close()
{
    $("#addItem").hide();
    $("#itemSearch").hide();

}

function AddSuccess(data) {
    debugger;
    HideLoader();
    RefreshGrid();
    $("#addItem").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }
  
}


$(document).on("change", "#ManufacturerId", function () {

    debugger;

    $.ajax({
        url: $_GetColorAndDoorStyle,
        type: "GET",
        async: false,
        data: { ManufacturerId: $(this).val() },
        success: function (result) {
            debugger;
            $("#DoorStyleId").empty();
            $("#DoorStyleId").append("<option value>----Select---</option>")
            $.each(result.doorStyle, function (i, item) {
                debugger;
                $("#DoorStyleId").append("<option value=" + item.DoorId + ">" + item.StyleName + "</option>")

            });


            $("#ColorId").empty();
            $("#ColorId").append("<option value>----Select---</option>")

            $.each(result.colorList, function (i, item) {
                debugger;
                $("#ColorId").append("<option value=" + item.ColorId + ">" + item.ColorName + "</option>")

            });

        }

    });

});


$(document).on("change", "#txtManufacturerName", function () {

    $('#grid').bootstrapTable('filterBy');
});


function GetColorAndDoorStyle()
{

}

