using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace BuildersAlliances.Domain
{
    [Table("Colors")]
    public class Colors 
    {
        [Key]
        public long ColorId { get; set; }
        
        [Required]
       public string ColorName { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
