$(document).ready(function () {


    var Param = {};
   


    $("#btnAddColor").on('click', function () {
        $("form").find("input[type=text], textarea").val("")
        $("#addColor").show();
        
        $("#ColorId").val('0');
        $("#btnColor").html("Add");
        ApplyValidation('colorForm');
    });


   
   

    $(".FilterSearch").keyup(function () {
        
       // var FilterSearch = $(this).val();

            Param.clickBtn = true;
            // form = $('#EditForm');
          
            //Param.FilterSearch = FilterSearch;
            
           
            

            $('#RefreshColorGrid').bootstrapTable('filterBy');
            Param.clickBtn = false;
        });

    // Edit and Delete Formattte links
        function operateFormatter(value, row, index) {


            return [
                 '<a id="edit"  class="edit" href="javascript:void(0)" title="Edit">',
                    '<span class="btn btn-info btn-sm glyphicon glyphicon-edit">  </span>',
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
                $("#DoorId").val(row.DoorId);

                $.ajax({
                    type: "POST",
                    url: $_EditColors,
                    data: row,
                    success: function (data) {
                        $("#addColor").empty();
                        $("#addColor").html(data);
                        $("#btnColor").html("Update");
                        $("#addColor").show();
                        ApplyValidation('colorForm');
                 

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
                                type: "POST",
                                url: $_DeleteColor,
                                data: row,
                                success: function (resultdata) {


                                    dialogItself.close();
                                    ShowConfirmMessage("Deleted successfully");
                                    RefreshColorGrid();

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
        $('#colorgrid').bootstrapTable({
            headers: headers,
            method: 'post',
            url: $_GetColor,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function (p) {
                debugger;
                p.model = {
                    ColorName: "",
                    ManufacturerId: $(".ManufacturerId").val()
                  
                };
                return p;
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
                     field: 'ColorId',
                     title: 'DiscountTypeId',
                     checkbox: false,
                     type: 'search',
                     visible: false,
                     switchable: false,
                     sortable: true,
                 },
                    {
                        field: 'ColorName',
                        title: 'Color',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
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


function RefreshColorGrid() {
    $('#colorgrid').bootstrapTable('refresh', { silent: true });

}

function Close()
{
        $("#addManufaturer").hide();

}

function AddColorSuccess(data) {
    debugger;
    HideLoader();
    RefreshColorGrid();
    $("#ColorId").val("0");
    $("#ColorName").val("");
    
    if (data.status) {
        ShowConfirmMessage(data.message);
    }
  
}


