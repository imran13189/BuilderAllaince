using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace BuildersAlliances.Domain
{
 
    public class DiscountTypeModel
    {

        public int DiscountTypeId { get; set; }
        public string DiscountTypeName { get; set; }
        public  decimal Multiplier { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int TotalRows { get; set; }

        public bool IsSelected { get; set; }
        public int ManufacturerId { get; set; }
    }
}
