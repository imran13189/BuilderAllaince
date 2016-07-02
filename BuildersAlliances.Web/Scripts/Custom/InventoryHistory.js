var Params = {};
$(document).ready(function () {
  
    
    debugger;
   



    $("#FilterQtySearch").keyup(function () {
        $('#gridInventoryHistory').bootstrapTable('filterBy');
    });
   

      

    // Edit and Delete Formattte links
        function operateFormatterQty(value, row, index) {


            return [
                 '<a id="edit"  class="edit ml10 isAllowEdit" href="javascript:void(0)" title="Edit">',
                    '<span class="btn btn-info btn-xs">Edit</span>',
                '</a>&nbsp;&nbsp;',
                '<a id="delete" class="remove ml10 isAllowDelete" href="javascript:void(0)" title="Remove">',
                    '<span class="btn btn-danger btn-xs">Delete</span>',
                '</a>'
            ].join('');





        }


    // Link Events Edit and Delete
        window.operateEventsQty = {

            'click .edit': function (e, value, row, index) {
                debugger;
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $.ajax({
                    type: "POST",
                    url: $_EditItem,
                    data: row,
                    success: function (data) {
                        $("#addItem").empty();
                        $("#addItem").html(data);
                        $("#btnSave").html("Update");
                        $("#addItem").show();


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
        $('#gridInventoryHistory').bootstrapTable({
            headers: headers,
            method: 'post',
            url: $_InventioryHistoryList,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function (Params) {
                debugger;
                Params.FilterSearch = $("#FilterSearch").val();
                Params.ItemId = $_ItemId;
                return Params;
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
            toolbar: '#customtoolbar',
            columns: [
                 {
                     field: 'InventoryId',
                     title: 'Inventory',
                     checkbox: false,
                     type: 'search',
                     visible: false,
                     switchable: false,
                     sortable: true,
                 },
                  {
                      field: 'Quantity',
                      title: 'Quantity',
                      checkbox: false,
                      type: 'search',
                     
                  }
                  ,
                    {
                        field: 'PurchaseDate',
                        title: 'PurchaseDate',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
                    }
                  ,

                    {
                        field: 'Comments',
                        title: 'Comments',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
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
    $('#gridInventoryHistory').bootstrapTable('refresh', { silent: true });

}

function Close()
{
        $("#addItem").hide();

}

//function AddSuccess(data) {
//    debugger;
//    RefreshGrid();
//    $("#addItem").hide();
//    if (data.status) {
//        ShowConfirmMessage(data.message);
//    }
  
//}


