using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace BuildersAlliances.Domain
{
    [Table("OrderStatus")]
    public class OrderStatus 
    {
        [Key]
        public int OrderStatusId { get; set; }
        
        
       public string StatusName { get; set; }
        
        
    }
}
