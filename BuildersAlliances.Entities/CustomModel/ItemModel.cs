using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
   public class ItemModel
    {
        
       public long ItemId { get; set; }
    
       public int ManufacturerId { get; set; }
   
       public string ItemName { set; get; }
       public int? ColorId { get; set; }
        public string ColorName { get; set; }
       public int Cubes { get; set; }


        public decimal ListPrice { get; set; }
        public decimal SellingPrice { get; set; }
       public string ItemSKU { get; set; }

        
        public string ManufacturerName { get; set; }
        public decimal? TotalQuantity { get; set; }
        public int? DoorStyleId { get; set; }

        public string StyleName { get; set; }
        public int TotalRows { get; set; }
    }
}
