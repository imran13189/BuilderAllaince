using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
    
   public class OrderItemModel
    {
    
        public int OrderItemId { get; set; }
        public long ItemId { get; set; }
        public int ItemStatus { get; set; }

        public string StatusName { get; set; }
        public int Quantity { get; set; }
        public decimal Multiplier { get; set; }

        public string ItemSKU { get; set; }
        public string ManufacturerName { get; set; }
        public string ItemName { get; set; }

        public int TotalRows { get; set; }

        public int ManufacturerId { get; set; }

        public long OrderId { get; set; }

        public decimal SellingPrice { get; set; }


        public decimal ListPrice { get; set; }

        public decimal TotalCost { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool OrderItemChk
        {
            get { return false; }
            set { }
        }
           

    }
}
