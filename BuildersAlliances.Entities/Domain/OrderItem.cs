using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("OrderItem")]
   public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public long ItemId { get; set; }
        public int ItemStatus { get; set; }
        public int Quantity { get; set; }
      

        public long OrderId { get; set; }
        public DateTime DeliveryDate { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual Items Items { get; set; }
    }
}
