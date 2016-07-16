using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("Qoute")]
    public class Qoute
    {
        [Key]
        public long QouteId { get; set; }

       
        [Required]
        
        public int State { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public int BuilderId { get; set; }
        [NotMapped]
        public string BuilderName { get; set; }

        public virtual ICollection<QouteItems> QouteItems { get; set; }


        public virtual Builder Builder { get; set; }

    }
}
