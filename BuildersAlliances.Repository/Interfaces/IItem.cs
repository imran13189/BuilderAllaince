using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Repository.Interfaces
{
    public interface IItem
    {
        bool AddItem(Items model);
        List<ItemModel> GetItem(int limit, int offset, string sort, ItemModel model);
        bool DeleteItem(long ItemId);
        List<Items> GetItemByManufacturer(int Manufacturer);
    }
}
