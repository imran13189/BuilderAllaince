using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildersAlliances.Domain
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public long InvoiceId { get; set; }

       
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public int BuilderId { get; set; }
        [NotMapped]
        public string BuilderName { get; set; }

        

       // public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }


        public virtual Builder Builder { get; set; }


    }
}
