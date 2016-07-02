using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
    public class OrderModel
    {
        public long OrderId { get; set; }
      
        public int? OrderStatus { get; set; }
        
        public int? Installer { get; set; }
        public DateTime CreatedDate { get; set; }
      
        public string OrderTypeName { get; set; }
       
        public String StatusName { get; set; }
        public int TotalRows { get; set; }
        public int OrderTypeId { get; set; }

        public int BuilderId { get; set; }
    }
}
