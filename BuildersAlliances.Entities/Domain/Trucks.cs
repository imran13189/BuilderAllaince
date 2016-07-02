using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Domain
{
    [Table("Trucks")]
public    class Trucks
    {
        [Key]
        public int TruckId { get; set; }
        [Required]
        public string TruckNumber
        {
            get; set;
        }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public string DriverAssigned { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }

        public int? ManufacturerId { get; set; }
        public int TruckTypeId { get; set; }
    }
}
