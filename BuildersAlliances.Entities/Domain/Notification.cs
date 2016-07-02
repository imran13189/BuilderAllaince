using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace BuildersAlliances.Domain
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public long NotificationId { get; set; }
        
        
        public long OrderId { get; set; }
        public bool IsSeen { get; set; }

        public int ManufacturerId { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
