$(document).ready(function () {

    $(".sidebar-menu li:eq(1) a").addClass('active')
    $(".sidebar-menu li:eq(1) .sub li:eq(0)").addClass('active')


    var Param = {};
    $("#addManufaturer").hide();


    $("#btnAdd").on('click', function () {
        $("#searchManufaturer").hide();
        $("form").find("input[type=text], textarea").val("")
        $("#addManufaturer").show();
        
        $("#ManufacturerId").val('0');
        $("#btnSave").html("Add");
        ApplyValidation();
    });

    $("#btnAdvanceSearch").click(function () {

        $("#addManufaturer").hide();
        $("#searchManufaturer").show();

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
                    '<span class="btn btn-danger btn-sm glyphicon glyphicon-remove"></span>',
                '</a>&nbsp;',
                 '<a id="discount" class="discount ml10 isAllowDelete" href="javascript:void(0)" title="Discount">',
                    '<span class="btn btn-info btn-sm glyphicon glyphicon-th-list"></span>',
                '</a>&nbsp;',
                   '<a id="contacts" class="contact ml10 isAllowDelete" href="javascript:void(0)" title="Contacts">',
                    '<span class="btn btn-success btn-sm glyphicon glyphicon-earphone"></span>',
                      '</a>&nbsp;',
                     '<a id="doorStyle" class="doorStyle ml10 isAllowDelete" href="javascript:void(0)" title="Door Style">',
                    '<span class="btn btn-success btn-sm glyphicon glyphicon-new-window"></span>',
                      '</a>&nbsp;',
                    '<a id="color" class="color ml10 isAllowDelete" href="javascript:void(0)" title="Color">',
                    '<span class="btn btn-success btn-sm glyphicon glyphicon-cloud"></span>',
                      '</a>&nbsp;'
            ].join('');





        }


    // Link Events Edit and Delete
        window.operateEvents = {

            'click .edit': function (e, value, row, index) {
                debugger;
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $.ajax({
                    type: "POST",
                    url: $_EditManufacturer,
                    data: row,
                    success: function (data) {
                        $("#searchManufaturer").hide();
                        $("#addManufaturer").empty();
                        $("#addManufaturer").html(data);
                        $("#btnSave").html("Update");
                        $("#addManufaturer").show();
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
                                url: $_DeleteManufacturer,
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
            'click .discount': function (e, value, row, index) {
                debugger;
                $("#popModal").empty();
                $.ajax({
                    type: "POST",
                    url: $_GetDiscountType,
                    data: row,
                    success: function (data) {
                        $("#DiscountType").empty();
                        $("#DiscountType").html(data);
                      //  $("#btnSave").html("Update");
                        $(".DiscountType").modal('show');
                        ApplyValidation('dicountForm');

                    }
                });

            },
            'click .contact': function (e, value, row, index) {
                debugger;
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $.ajax({
                    type: "POST",
                    url: $_GetContacts,
                    data: row,
                    success: function (data) {
                        $("#Contact").empty();
                        $("#Contact").html(data);
                        
                        $(".Contact").modal('show');
                        

                    }
                });
            },
            'click .doorStyle': function (e, value, row, index) {
                debugger;
                $("#DiscountType").empty();
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $.ajax({
                    type: "POST",
                    url: $_GetDoorStyle,
                    data: row,
                    success: function (data) {
                        $("#popModal").empty();
                       
                        $("#popModal").html(data);

                        $(".popModal").modal('show');


                    }
                });
            },
            'click .color': function (e, value, row, index) {
                debugger;
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $.ajax({
                    type: "POST",
                    url: $_GetColors,
                    data: row,
                    success: function (data) {
                        $("#popModal").empty();
                        $("#popModal").html(data);

                        $(".popModal").modal('show');


                    }
                });
            },
        };
        debugger;

        var reqUrl = $_BaseUrl + 'api/ManufacturerAPI/GetManufacturer';
        var headers = {};
        // alert("Header1=" + headers);
        $('#grid').bootstrapTable({
            headers: headers,
            method: 'post',
            url: $_ManufacturerList,
            cache: true,
            height: 500,
            classes: 'table table-hover',
            queryParams: function (Param) {
              
                Param.model = {
                    ManufacturerName: $("#txtManufacturerName").val().trim(),
                    EmailId: $("#txtEmailId").val().trim(),
                    Address: $("#txtAddress").val().trim(),
                    WebSiteUrl: $("#txtWebSiteUrl").val().trim(),
                    ContactNumber: $("#txtContactNumber").val().trim()
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
                     field: 'ManufacturerId',
                     title: 'ManufacturerId',
                     checkbox: false,
                     type: 'search',
                     visible: false,
                     switchable: false,
                     sortable: true,
                 },
                    {
                        field: 'ManufacturerName',
                        title: 'Manufacturer',
                        checkbox: false,
                        type: 'search',
                        sortable: true
                       
                    }
                    ,
                     {
                         field: 'ContactNumber',
                         title: 'Contact',
                        
                     },
                     
                     {
                         field: 'Address',
                         title: 'Address',
                         
                     }
                     ,
                     {
                         field: 'EmailId',
                         title: 'Email',
                         sortable: true
                        
                     }
                     ,
                     {
                         field: 'WebSiteUrl',
                         title: 'WebSiteUrl',
                        
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
    $("#addManufaturer").hide();
    $("#searchManufaturer").hide();

}

function AddSuccess(data) {
    debugger;
    HideLoader();
    RefreshGrid();
    $("#addManufaturer").hide();
    if (data.status) {
        ShowConfirmMessage(data.message);
    }
  
}

function AddContactSuccess()
{
    HideLoader();
    RefreshGrid();
    $(".Contact").modal('hide');
   
}

function CloseContact()
{
    $(".Contact").modal('hide');
}

