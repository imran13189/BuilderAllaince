$(document).ready(function () {

    $(".sidebar-menu .menu:eq(2) a").addClass("active")


    var Param = {};
    $("#addBuilder").hide();


    $("#btnAdd").on('click', function () {
        $("#searchBuilder").hide();
        $("form").find("input[type=text], textarea").val("")
        $("#addBuilder").show();
        
     
        $("#btnSave").html("Add");
        ApplyValidation();
    });

    $("#btnAdvanceSearch").click(function () {

        $("#addBuilder").hide();
        $("#searchBuilder").show();

    });

   
 

    $(".FilterSearch").keyup(function () {
        
     
            $('#grid').bootstrapTable('filterBy');
           
        });

    // Edit and Delete Formattte links
        function operateFormatter(value, row, index) {


            return [
                 '<a id="edit"  class="edit" href="javascript:void(0)" title="Edit">',
                    '<span class="btn btn-info btn-sm glyphicon glyphicon-edit">  </span>',
                '</a>&nbsp;',
                '<a id="delete" class="remove ml10 isAllowDelete" href="javascript:void(0)" title="Remove">',
                    '<span class="btn btn-danger btn-sm glyphicon glyphicon-remove"></span>&nbsp;'
                    ,
                '<a id="orders" class="viewOrders ml10 isAllowDelete" href="javascript:void(0)" title="View Orders">',
                    '<span class="btn btn-danger btn-sm glyphicon glyphicon-th-list"></span>'
               
            ].join('');





        }


    // Link Events Edit and Delete
        window.operateEvents = {

            'click .edit': function (e, value, row, index) {
                debugger;
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $.ajax({
                    type: "POST",
                    url: $_EditBuilder,
                    data: row,
                    success: function (data) {
                        $("#searchBuilder").hide();
                        $("#addBuilder").empty();
                        $("#addBuilder").html(data);
                        $("#btnSave").html("Update");
                        $("#addBuilder").show();
                        ApplyValidation();

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
                                url: $_DeleteBuilder,
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

            },
            'click .viewOrders': function (e, value, row, index) {
                debugger;
                 $("#BuilderId").val(row.BuilderId);
                $.ajax({
                    type: "POST",
                    url: $_GetBuilderOrders,
                    data: {BuilderId:row.BuilderId},
                    success: function (data) {
                        $("#orderModal").empty()

                        $("#orderModal").html(data);
                        $(".orderModal").modal('show');

                    }
                });
            },
         
        };
        debugger;

        
        var headers = {};
        // alert("Header1=" + headers);
        $('#grid').bootstrapTable({
            headers: headers,
            method: 'post',
            url: $_BuilderList,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function (Param) {
              
                Param.model = {
                    BuilderName: $("#txtBuilder").val().trim(),
                    Address1: $("#txtAddress1").val().trim(),
                    Address2: $("#txtAddress2").val().trim(),
                    Address3: $("#txtAddress3").val().trim(),
                    Email: $("#txtEmail").val().trim(),
                    Phone: $("#txtPhone").val().trim()
                };
                return Param;
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
                     field: 'BuilderId',
                     title: 'BuilderId',
                     checkbox: false,
                     type: 'search',
                     visible: false,
                     switchable: false,
                     sortable: true,
                 },
                    {
                        field: 'BuilderName',
                        title: 'Builder Name',
                        checkbox: false,
                        type: 'search',
                        sortable: true
                       
                    }
                    ,
                     {
                         field: 'Address1',
                         title: 'Address1',
                        
                     },
                     
                     {
                         field: 'Address2',
                         title: 'Address2',
                         
                     }
                     ,
                     {
                         field: 'Address3',
                         title: 'Address3',
                         sortable: true
                        
                     }
                     ,
                     {
                         field: 'Email',
                         title: 'Email',
                        
                     },
                      
                     {
                         field: 'Phone',
                         title: 'Phone',

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
    $("#addBuilder").hide();
    $("#searchBuilder").hide();

}

function AddSuccess(data) {
    debugger;
    HideLoader();
    RefreshGrid();
    $("#addBuilder").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }
  
}


