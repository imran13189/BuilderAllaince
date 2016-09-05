using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public long OrderId { get; set; }

        //[Required]
        //public int ManufacturerId { get; set; }

        //[Required]
        //public long ItemId { get; set; }
        public  int? OrderStatus{get;set;}

        [Required]
        public int OrderTypeId { get; set; }
        public int Installer { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public int BuilderId { get; set; }
        [NotMapped]
        public string BuilderName { get; set; }

        public virtual Builder Builder { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
