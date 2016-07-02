using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace BuildersAlliances.Domain
{
    [Table("DoorStyle")]
    public class DoorStyle 
    {
        [Key]
        public long DoorId { get; set; }
        
        [Required]
       public string StyleName { get; set; }
        public bool IsDeleted { get; set; }

        public int ManufacturerId { get; set; }

    }
}
