using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;

using System.Data.SqlClient;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.CustomModel;
using BuildersAlliances.Repository;

namespace BuildersAlliances.Services
{
  public  class ItemService:IItem
    {

        UnityOfWork uow = null;
        public ItemService()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }
        public bool AddItem(Items model)
        {
            try
            {
                if (model.ItemId == 0)
                {
                    uow.Repository<Items>().Add(model);


                }
                else
                {
                    Items data = uow.Repository<Items>().AsQuerable().FirstOrDefault(x => x.ItemId == model.ItemId);
                    data.ItemName = model.ItemName;
                    data.ItemSKU = model.ItemSKU;
                    data.ManufacturerId = model.ManufacturerId;
                    data.ListPrice = model.ListPrice;
                    
                    data.Cubes = model.Cubes;
                    
                    data.ColorId = model.ColorId;
                    data.DoorStyleId = model.DoorStyleId;

                }
                uow.SaveChanges();
                return true;
            }
            catch (Exception e) { return false; }

        }
        public List<ItemModel> GetItem(int limit, int offset, string order, string sort, ItemModel model)
        {
            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", order),
                         new SqlParameter("@sort", sort),
                         new SqlParameter("@ItemSKU", model.ItemSKU),
                         new SqlParameter("@Manufacturer", model.ManufacturerName),
                         new SqlParameter("@ItemName", model.ItemName),
                         new SqlParameter("@ItemColor",model.ColorName)
                         
                        // new SqlParameter("@Size", model.Size),

                         };

            return uow.ExecuteProcedure<ItemModel>("exec GetItems @offset,@limit,@order,@sort,@ItemSKU,@Manufacturer,@ItemName,@ItemColor", param);
        }

        public bool DeleteItem(long ItemId)
        {
            try
            {
                Items model = uow.Repository<Items>().Get(x => x.ItemId == ItemId);
                model.IsDeleted = true;
                //uow.ExecuteCommand("update Orders set IsDeleted=1 where ItemId=" + ItemId);
                uow.SaveChanges();
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Items> GetItemByManufacturer(int ManufacturerId)
        {
            return uow.Repository<Items>().GetAll(x => x.ManufacturerId == ManufacturerId&&x.IsDeleted==false).ToList();
        }


        public List<Colors> GetColors(int ManufacturerId)
        {
           return uow.Repository<Colors>().GetAll(x=>x.ManufacturerId==ManufacturerId&&x.IsDeleted==false).ToList();
        }

        public List<DoorStyle> GetDoors(int ManufacturerId)
        {
            return uow.Repository<DoorStyle>().GetAll(x => x.ManufacturerId == ManufacturerId&&x.IsDeleted==false).ToList();
        }


        //   Repository.Interfaces.IItem _item = null;
        //   public ItemService()
        //   {
        //       _item = new BuildersAlliances.Repository.ItemRepository();
        //   }
        //   public   bool AddItem(Items model)
        //   {
        //       return _item.AddItem(model);

        //   }
        //public   List<ItemModel> GetItem(int limit, int offset, string sort, ItemModel model)
        //   {
        //     return  _item.GetItem(limit, offset,  sort, model);
        //   }
        //   public bool DeleteItem(long ItemId)
        //   {
        //       return _item.DeleteItem(ItemId);

        //   }

        //  public List<Items> GetItemByManufacturer(int ManufacturerId)
        //   {
        //       return _item.GetItemByManufacturer(ManufacturerId);
        //   }

    }
}
