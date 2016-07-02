using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("OrderItemStatus")]
    public  class OrderItemStatus
    {
        [Key]
        public int ItemStatusId { get; set; }
        public string StatusName { get; set; }
    }
}
