var Param = {};
$(document).ready(function () {
  
    $("#addItem").hide();


    $("#btnAdd").on('click', function () {
        $("#addItem").show();
        
        //$("#ItemId").val('0');
        $("#btnSave").html("Add");
    });



    $("#FilterSearch").keyup(function () {
        $('#grid').bootstrapTable('filterBy');
    });
   

      

    // Edit and Delete Formattte links
        function operateFormatter(value, row, index) {


            return [
                 '<a id="edit"  class="addQuantity ml10 isAllowEdit" href="javascript:void(0)" title="Edit">',
                    '<span class="btn btn-info btn-xs">Add Quantity</span>',
                '</a>&nbsp;&nbsp;',
                '<a id="delete" class="removeQuantity ml10 isAllowDelete" href="javascript:void(0)" title="Remove">',
                    '<span class="btn btn-danger btn-xs">Remove Quantity</span>',
                '</a>'
            ].join('');





        }


    // Link Events Edit and Delete
        window.operateEvents = {

            'click .addQuantity': function (e, value, row, index) {
                debugger;
                $(".modal-title").html("Add Quantity");
                $("#btnAdd").html("Add");
               
                $("#myModal").modal('show');
                $("#ItemId").val(row.ItemId);
              //  $.mask.definitions['~'] = '[+]';
                $("#Quantity").mask("999999999");
            },

            'click .removeQuantity': function (e, value, row, index) {
                debugger;
                BootstrapDialog.show({
                    title: 'Confirmation',
                    message: "Are you sure ?",
                    buttons: [{
                        label: 'Yes',
                        cssClass: 'btn-primary',
                        action: function (dialogItself) {
                            dialogItself.close();
                            $(".modal-title").html("Remove Quantity");
                            $("#btnAdd").html("Remove");
                            $("#ItemId").val(row.ItemId);
                           // $.mask.definitions['~'] = '[-]';
                            $("#Quantity").mask("-999999999");
                            $("#myModal").modal('show');
                            
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
            url: $_InventioryItemList,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function(param){
                        param.FilterSearch = $("#FilterSearch").val();
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
                     
                  }
                 
                  ,

                    {
                        field: 'ItemName',
                        title: 'Item Name',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
                    }
                   ,
                     {
                         field: 'TotalQuantity',
                         title: 'Quantity',
                        
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
    
   
  

});


function RefreshGrid() {
    $('#grid').bootstrapTable('refresh', { silent: true });

}

function Close()
{
        $("#addItem").hide();

}

function AddSuccess(data) {
    debugger;
    RefreshGrid();
    $("#myModal").modal('hide');
    $("#Quantity").val('');
    $("#Comments").val('');

  
}


