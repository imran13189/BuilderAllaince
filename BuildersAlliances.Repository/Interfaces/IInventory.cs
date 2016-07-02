using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;
using BuildersAlliances.Entities.CustomModel;

namespace BuildersAlliances.Repository.Interfaces
{
   public interface IInventory
    {
        List<ItemModel> GetItem(int limit, int offset, string sort, ItemModel model);
        List<InventoryModel> GetInventoryHistory(int limit, int offset, string sort,long itenId, string search);
        bool AddInventory(Inventory model);
        bool RemoveQuantity(Inventory model);
    }
}
