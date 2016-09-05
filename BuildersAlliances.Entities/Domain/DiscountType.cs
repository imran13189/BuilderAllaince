using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace BuildersAlliances.Domain
{
    [Table("DiscountType")]
    public class DiscountType 
    {
        [Key]
        public int DiscountTypeId { get; set; }

        [Required]
        public string DiscountTypeName { get; set; }

        [Required]

        [Range(typeof(decimal), "0.1", "0.99",ErrorMessage ="Multiplier value should be greater than 0 and less than 1")]
        public  decimal Multiplier { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ManufacturerId { get; set; }
    }
}
