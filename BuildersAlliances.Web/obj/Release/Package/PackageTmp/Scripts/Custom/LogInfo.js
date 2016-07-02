﻿
$(document).ready(function () {
    $(".sidebar-menu li:eq(1) a").addClass('active')
    $(".sidebar-menu li:eq(1) .sub li:eq(3)").addClass('active')


 
    $("#addTruck").hide();


    $("#btnAdd").on('click', function () {
        $("form").find("input[type=text], textarea").val("")
        $("#addTruck").show();
        
        $("#TruckId").val('0');
        $("#btnSave").html("Add");
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
                    url: $_EditTruck,
                    data: row,
                    success: function (data) {
                        $("#addTruck").empty();
                        $("#addTruck").html(data);
                        $("#btnSave").html("Update");
                        $("#addTruck").show();


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
                                url: $_DeleteTruck,
                                data: row,
                                success: function (resultdata) {


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
            url: $_LogInfoList,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function (param) {
                param.model = {
                    ModuleName: $("#txtModuleName").val()
                  
                   
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
                     field: 'TruckId',
                     title: 'TruckId',
                     checkbox: false,
                     type: 'search',
                     visible: false,
                     switchable: false,
                     sortable: true,
                 },
                    {
                        field: 'ModuleName',
                        title: 'Module',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
                    }
                    ,
                     {
                         field: 'FieldName',
                         title: 'FieldName',
                        
                     },
                     
                     {
                         field: 'PreviousValue',
                         title: 'Previous Value',
                         
                     }
                     ,

                     {
                         field: 'NewValue',
                         title: 'New Value',

                     }
                     ,

                     {
                         field: 'ModifiedBy',
                         title: 'Modified By',

                     }
                     ,

                     {
                         field: 'ModifiedDate',
                         title: 'Modified Date',

                     }
                   //,

                   //   {
                   //       field: 'operate',
                   //       title: 'Actions',


                   //       clickToSelect: false,
                   //       formatter: operateFormatter,
                   //       events: operateEvents
                   //   }
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
        $("#addTruck").hide();

}

function AddSuccess(data) {
    debugger;
    RefreshGrid();
    $("#addTruck").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }
  
}


