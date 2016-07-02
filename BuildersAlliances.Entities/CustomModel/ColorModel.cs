using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
  public  class ColorModel
    {
        public long ColorId { get; set; }

        
        public string ColorName { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsDeleted { get; set; }

        public int TotalRows { get; set; }
    }
}
