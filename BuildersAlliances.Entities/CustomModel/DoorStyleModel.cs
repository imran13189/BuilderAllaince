using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
public    class DoorStyleModel
    {
        public long DoorId { get; set; }

        
        public string StyleName { get; set; }
        public bool IsDeleted { get; set; }

        public int ManufacturerId { get; set; }

        public int TotalRows { get; set; }
    }
}
