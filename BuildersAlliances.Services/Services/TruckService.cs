using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Domain;

using BuildersAlliances.CustomModel;
using BuildersAlliances.Repository;
using System.Data.SqlClient;

namespace BuildersAlliances.Services
{
    public class TruckService: ITruck
    {
        UnityOfWork uow = null;
        public TruckService()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }
        public bool AddTruck(Trucks model)
        {
            try
            {
                if (model.TruckId == 0)
                {
                    model.IsAvailable = true;
                    uow.Repository<Trucks>().Add(model);
                }
                else
                {
                    Trucks data = uow.Repository<Trucks>().AsQuerable().FirstOrDefault(x => x.TruckId == model.TruckId);
                    data.TruckNumber = model.TruckNumber;
                    data.Capacity = model.Capacity;
                    data.DriverAssigned = model.DriverAssigned;
                    data.ManufacturerId = model.ManufacturerId;
                    data.TruckTypeId = model.TruckTypeId;

                }
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool DeleteTruck(int TruckId)
        {
            try
            {
                Trucks data = uow.Repository<Trucks>().Get(x => x.TruckId == TruckId);
                data.IsDeleted = true;
                uow.SaveChanges();
                return true;
            }
            catch { return false; }

        }
        public List<TruckModel> GetTrucks(int limit, int offset, string sort, TruckModel model)
        {
            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@TruckNumber", model.TruckNumber),
                         new SqlParameter("@Driver", model.DriverAssigned),
                         new SqlParameter("@Capacity", model.Capacity)
                         };
            var data= uow.ExecuteProcedure<TruckModel>("exec GetTrucks @offset, @limit, @order, @TruckNumber, @Driver, @Capacity", param);
            return data;
        }

        public List<TruckType> GetTruckType()
        {
            return uow.Repository<TruckType>().GetAll().ToList();
        }
        // Repository.Interfaces.ITruck _truck= null;
        // public TruckService()
        // {
        //     _truck = new BuildersAlliances.Repository.TruckRepository();
        // }
        //public bool AddTruck(Trucks model)
        // {
        //     return _truck.AddTruck(model);
        // }
        // public bool DeleteTruck(int TruckId)
        // {
        //     return _truck.DeleteTruck(TruckId);
        // }
        // public List<TruckModel> GetTrucks(int limit, int offset, string sort,TruckModel model)
        // {
        //     return _truck.GetTrucks(limit, offset, sort,model);
        // }

        // public List<TruckType> GetTruckType()
        // {
        //     return _truck.GetTruckType();
        // }
    }
}
