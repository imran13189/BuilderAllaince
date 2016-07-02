using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("Inventory")]
    public  class Inventory
    {
        [Key]
       public long InventoryId { get; set; }
        public long ItemId { get; set; }
        //[Required]
        public decimal Quantity { get; set; }
        public string Comments { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

    }
}
