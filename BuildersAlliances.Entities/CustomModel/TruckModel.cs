using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{

     public    class TruckModel
    {
        public int TruckId { get; set; }

        public string TruckNumber
        {
            get; set;
        }


        public int Capacity { get; set; }


        public string DriverAssigned { get; set; }
        public bool IsAvailable { get; set; }
        public int TotalRows { get; set; }

        public string TruckTypeName { get; set; }

        public int TruckTypeId { get; set; }

        public string ManufacturerName { get; set; }

        public int? ManufacturerId { get; set; }
    }
}
