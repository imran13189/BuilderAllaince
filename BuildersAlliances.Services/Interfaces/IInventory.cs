using BuildersAlliances.CustomModel;
using BuildersAlliances.Domain;
using BuildersAlliances.Entities.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Services.Interfaces
{
    public interface IInventory
    {
        bool AddInventory(Inventory model);
        bool RemoveQuantity(Inventory model);
        List<ItemModel> GetItem(int limit, int offset, string sort, ItemModel  model);
        List<InventoryModel> GetInventoryHistory(int limit, int offset, string sort, long itenId, string search);
    }
}
