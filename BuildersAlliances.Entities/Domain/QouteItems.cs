using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("QouteItems")]
   public class QouteItems
    {
        [Key]
        public long QouteItemId { get; set; }
        public long ItemId { get; set; }
       
        public int Quantity { get; set; }
      
        public decimal Price { get; set; }

        public long QouteId { get; set; }

        public virtual Qoute Qoute { get; set; }

        public virtual Items Items { get;set;}

    }
}
