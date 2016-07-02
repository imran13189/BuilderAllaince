using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuildersAlliances.Domain;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.CustomModel;
using Newtonsoft.Json.Linq;
using BuildersAlliances.Entities.CustomModel;

namespace BuildersAlliances.Web.API
{
    public class InventoryFilter : DataObject
    {
        public long ItemId { get; set; }
        public string FilterSearch { get; set; }
    }
    public class InventoryDetail
    {
        public List<InventoryModel> data;
        public int count;
    }
    public class InventoryAPIController : ApiController
    {
        IInventory _inventory = null;
        public InventoryAPIController(IInventory inventory)
        {
            _inventory = inventory;
        }

        [HttpPost]
        public dynamic GetItem(JObject Obj)
        {
            ItemFilter filter = Obj.ToObject<ItemFilter>();
            ItemDetail re = new ItemDetail();
            re.data = _inventory.GetItem(filter.limit, filter.offset, filter.order, filter.model);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }


        [HttpPost]
        public dynamic GetInventoryHistory(JObject Obj)
        {
            InventoryFilter filter = Obj.ToObject<InventoryFilter>();
            InventoryDetail re = new InventoryDetail();
            re.data = _inventory.GetInventoryHistory(filter.limit, filter.offset, filter.order, filter.ItemId,filter.FilterSearch);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }
    }
}
