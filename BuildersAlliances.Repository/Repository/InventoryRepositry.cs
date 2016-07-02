using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Repository.Interfaces;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using BuildersAlliances.Entities.CustomModel;

namespace BuildersAlliances.Repository
{
   public class InventoryRepositry : IInventory
    {
        UnityOfWork uow = null;
        public InventoryRepositry()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }
      public  bool AddInventory(Inventory model)
        {
            try
            {
                uow.Repository<Inventory>().Add(model);
                uow.SaveChanges();
                return true;
            }
            catch(Exception e)
            {

                return false;
            }
        }
     public   bool RemoveQuantity(Inventory model)
        {
            try
            {
               Inventory data=uow.Repository<Inventory>().Get(x => x.InventoryId == model.InventoryId);
                data.Quantity -= model.Quantity;
                return true;
            }
            catch
            {

                return false;
            }
        }

        public List<ItemModel> GetItem(int limit, int offset, string sort, ItemModel model)
        {
            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@ItemSKU", model.ItemSKU),
                         new SqlParameter("@Manufacturer", model.ManufacturerName),
                         new SqlParameter("@ItemName", model.ItemName),
                         new SqlParameter("@ItemColor",model.ColorName)

                         //new SqlParameter("@Size", model.Size),

                         };
            return uow.ExecuteProcedure<ItemModel>("exec GetInventoryItems @offset, @limit, @order, @ItemSKU, @Manufacturer, @ItemName, @ItemColor", param);
            
        }

       public List<InventoryModel> GetInventoryHistory(int limit, int offset, string sort, long ItemId, string search)
        {
            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                      
                         new SqlParameter("@ItemId", ItemId)
                       //  new SqlParameter("@Search",search??DBNull.Value)

                         };

            return uow.ExecuteProcedure<InventoryModel>("exec GetInventoryHistory @offset, @limit, @order,@ItemId", param);
        }
    }
}
