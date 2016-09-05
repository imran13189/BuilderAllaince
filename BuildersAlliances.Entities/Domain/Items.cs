using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("Items")]
    public class Items
    {
        [Key]
       public long ItemId { get; set; }
        [Required]
       public int ManufacturerId { get; set; }
        [Required]
       public string ItemName { set; get; }
        public int? ColorId { get; set; }
        [Required]
        public int Cubes { get; set; }
       
       
       [Required]
       public decimal ListPrice { get; set; }
       public string ItemSKU { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public string ManufacturerName { get; set; }

        public int? DoorStyleId { get; set; }

        public virtual ICollection<QouteItems> QouteItems { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
