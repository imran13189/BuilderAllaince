using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildersAlliances.Domain
{
    [Table("InvoiceItems")]
   public class InvoiceItems
    {
        [Key]
        public long InvoiceItemId { get; set; }
        public long ItemId { get; set; }
       
        public int Quantity { get; set; }
      
        public decimal Price { get; set; }

        public long InvoiceId { get; set; }

        
    }
}
