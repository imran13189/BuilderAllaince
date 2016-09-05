using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
    
    public class InvoiceModel
    {
        
        public long InvoiceId { get; set; }

       
        
        
        
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public int BuilderId { get; set; }
        
        public string BuilderName { get; set; }

        public int TotalRows { get; set; }

    }
}
