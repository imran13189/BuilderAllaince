using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildersAlliances.Domain
{
    [Table("Manufacturer")]
    public class Manufacturer:BaseEntity
    {
        [Key]
        public int ManufacturerId { get; set; }
        [Required]
        public string ManufacturerName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string WebSiteUrl { get; set; }

        [Required]
        //[StringLength(10, ErrorMessage = "Contact must be of 10 number")]
        public string ContactNumber { get; set; }
        public bool IsDeleted { get; set; }

        public string ServiceName { get; set; }
        public string ServiceContact { get; set; }
        public string SaleName { get; set; }
        public string SalesContact { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryContact { get; set; }


        [NotMapped]
        public int TotalRows { get; set; }


    }
}
