using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("OrderType")]
    public class OrderType
    {
        [Key]
        public int OrderTypeId { get; set; }
        public string OrderTypeName{ get; set; }
    }
}
