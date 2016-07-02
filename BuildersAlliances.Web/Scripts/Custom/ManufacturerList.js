$(document).ready(function () {

 

   
   

    $(".FilterManufacturer").keyup(function () {
        
      
           
            

        $('#gridManufacturer').bootstrapTable('filterBy');
      
        });

    // Edit and Delete Formattte links
        function operateFormatter(value, row, index) {


            return [
                 '<a id="select"  class="select" href="javascript:void(0)" title="Edit">',
                    '<span class="btn btn-info btn-sm glyphicon glyphicon-edit"> Select  </span>',
                '</a>&nbsp;&nbsp;'
               
            ].join('');





        }


    // Link Events Edit and Delete
        window.operateEvents = {

            'click .select': function (e, value, row, index) {
                debugger;
                // $("#MedicationTemplateId").val(row.MedicationTemplateId);
                $('select[name=ManufacturerId]').val(row.ManufacturerId);
                $('#ManufacturerId').selectpicker('refresh')
                $("#modalManufacture").modal('hide');
                $("#ManufacturerId").trigger('change');
            }

       
        };
        debugger;

        var reqUrl = $_BaseUrl + 'api/ManufacturerAPI/GetManufacturer';
        var headers = {};
        // alert("Header1=" + headers);
        $('#gridManufacturer').bootstrapTable({
            headers: headers,
            method: 'post',
            url: $_ManufacturerList,
            cache: true,
            height: 500,
          //  classes: 'table table-hover',
            queryParams: function (p) {
                debugger;
                p.model = {
                    ManufacturerName: $("#txtManufacturerListName").val().trim(),
                    EmailId: $("#txtEmailId").val().trim(),
                    Address: $("#txtAddress").val().trim(),
                    WebSiteUrl: $("#txtWebSiteUrl").val().trim(),
                    ContactNumber: $("#txtContactNumber").val().trim()
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
          //  rowStyle: rowStyle,
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
                        sortable: true,
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
            }
            //onLoadSuccess: function () {
            //    Addtitle();
            //},
            //onPageChange: function () {
            //    Addtitle();
            //}
        });

        $(".columns.columns-right.btn-group.pull-right").remove()



   
    
});


