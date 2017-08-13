
$(document).ready(function () {
    $(".sidebar-menu .menu:eq(3) a").addClass("active")
 
    $("#addTruck").hide();


    $("#btnAdd").on('click', function () {
        $("form").find("input[type=text], textarea").val("")
        $("#searchUser").hide();
        $("#addUser").show();
        
        $("#UserId").val('0');
        $("#btnSave").html("Add");
        ApplyValidation();
        
        
    });

    $("#btnAdvanceSearch").click(function () {
        $("#addUser").hide();
        $("#searchUser").show();

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
                '</a>&nbsp;&nbsp;',
                 '<a id="assign" class="assingRole ml10 isAllowDelete" href="javascript:void(0)" title="Assign Role">',
                    '<span class="btn btn-info btn-sm  glyphicon glyphicon-th-list"></span>',
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
                    url: $_EditUser,
                    data: row,
                    success: function (data) {
                        $("#searchUser").hide();
                        $("#addUser").empty();
                        $("#addUser").html(data);
                        
                        $("#btnSave").html("Update");
                        $("#addUser").show();

                        ApplyValidation();
                        $("#UserId").val();
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
                                url: $_DeleteUser,
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
            'click .assingRole': function (e, value, row, index) {
                debugger;
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $.ajax({
                    type: "GET",
                    url: $_AssignRole,
                    data: { UserId: row.UserId },
                    success: function (data) {
                     
                        $("#assignrole").empty();
                        $("#assignrole").html(data);
                        $(".assignrole").modal("show");
                    
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
            url: $_UsersList,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function (param) {
                param.model = {
                    Name: $("#txtName").val(),
                    Email: $("#txtEmail").val(),
                    Username: $("#txtUsername").val()
                   
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
                     field: 'UserId',
                     title: 'UserId',
                     checkbox: false,
                     type: 'search',
                     visible: false,
                     switchable: false
                     
                 },
                    {
                        field:'Name',
                        title: 'Name',
                        checkbox: false,
                        type: 'search',
                        sortable: true,
                    }
                    ,
                     {
                         field: 'Username',
                         title: 'Username',
                        
                     },
                     
                     {
                         field: 'Email',
                         title: 'Email',
                         
                     }
                   ,
                     {
                         field: 'Phone',
                         title: 'Phone',

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


$(document).on("change", "#TruckTypeId", function () {
    debugger;
    if ($(this).val() == "1") {
        $(".Manufacturer").show();
    }
    else
    {
        $("#ManufacturerId").val('0');
        $(".Manufacturer").hide();
    }
   
});


function RefreshGrid() {
    $('#grid').bootstrapTable('refresh', { silent: true });

}

function Close()
{
    $("#addUser").hide();
    $("#searchUser").hide();

}

function AddSuccess(data) {
    debugger;
    HideLoader();
    RefreshGrid();
    $("#addUser").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }
    $(".assignrole").modal("hide");
}


