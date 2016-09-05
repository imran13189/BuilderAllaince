using System.ComponentModel.DataAnnotations;


namespace BuildersAlliances.CustomModel
{
    
   public class InvoiceItemModel
    {
        
        public long InvoiceItemId { get; set; }
        public long ItemId { get; set; }
       
        public int Quantity { get; set; }
      
        public decimal Price { get; set; }

        public long InvoiceId { get; set; }
       
        public string ManufacturerName { get; set; }

        public string ItemSKU { get; set; }
        public string ItemName { get; set; }

        public decimal TotalCost { get; set; }

        public int TotalRows { get; set; }

        public int ManufacturerId { get; set; }
    }
}
