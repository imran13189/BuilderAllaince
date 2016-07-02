using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Repository
{
  public  class ItemRepository:IItem
    {
        UnityOfWork uow = null;
        public ItemRepository()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }
        public   bool AddItem(Items model)
        {
            try
            {
                if (model.ItemId == 0)
                {
                    uow.Repository<Items>().Add(model);
                    
                   
                }
                else
                {
                   Items data= uow.Repository<Items>().AsQuerable().FirstOrDefault(x => x.ItemId == model.ItemId);
                    data.ItemName = model.ItemName;
                    data.ItemSKU = model.ItemSKU;
                    data.ManufacturerId = model.ManufacturerId;
                    
                    data.Cubes = model.Cubes;
                    

                }
                uow.SaveChanges();
                return true;
            }
            catch(Exception e) { return false; }

        }
     public   List<ItemModel> GetItem(int limit, int offset, string sort, ItemModel model)
        {
            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@ItemSKU", model.ItemSKU),
                         new SqlParameter("@Manufacturer", model.ManufacturerName),
                         new SqlParameter("@ItemName", model.ItemName),
                         new SqlParameter("@ItemColor",model.ColorName)
                         
                        // new SqlParameter("@Size", model.Size),

                         };

            return uow.ExecuteProcedure<ItemModel>("exec GetItems @offset,@limit,@order,@ItemSKU,@Manufacturer,@ItemName,@ItemColor", param);
        }

        public bool DeleteItem(long ItemId)
        {
            try
            {
                Items model = uow.Repository<Items>().Get(x => x.ItemId == ItemId);
                model.IsDeleted = true;
                uow.ExecuteCommand("update Orders set IsDeleted=1 where ItemId=" + ItemId);
                uow.SaveChanges();
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<Items> GetItemByManufacturer(int ManufacturerId)
        {
           return  uow.Repository<Items>().GetAll(x => x.ManufacturerId == ManufacturerId).ToList();
        }


    }
}
