using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Repository.Interfaces
{
   public interface ITruck
    {

         bool AddTruck(Trucks model);
         bool DeleteTruck(int TruckId);
         List<TruckModel> GetTrucks(int limit, int offset, string sort,TruckModel model);

        List<TruckType> GetTruckType();

    }
}
