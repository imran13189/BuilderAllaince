using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Entities.CustomModel
{
   public class InventoryModel
    {
        
        public long InventoryId { get; set; }
        public long ItemId { get; set; }
        
        public decimal Quantity { get; set; }
        public string Comments { get; set; }

        public string ItemName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int TotalRows { get; set; }
    }
}
