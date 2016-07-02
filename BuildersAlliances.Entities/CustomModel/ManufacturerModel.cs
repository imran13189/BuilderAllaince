using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
   
    public class ManufacturerModel
    {
        
        public int ManufacturerId { get; set; }
        
        public string ManufacturerName { get; set; }

        public string EmailId { get; set; }
        public string Address { get; set; }
        public string WebSiteUrl { get; set; }

        //[StringLength(10, ErrorMessage = "Contact must be of 10 number")]
        public string ContactNumber { get; set; }

        public int TotalRows { get; set; }

        public string ServiceName { get; set; }
        public string ServiceContact { get; set; }
        public string SaleName { get; set; }
        public string SalesContact { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryContact { get; set; }


    }
}
