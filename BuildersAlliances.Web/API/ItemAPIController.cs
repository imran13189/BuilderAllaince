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


namespace BuildersAlliances.Web.API
{
    public class ItemFilter : DataObject
    {
        public ItemModel model;
    }
    public class ItemDetail
    {
        public List<ItemModel> data;
        public int count;
    }
    public class ItemAPIController : ApiController
    {
        IItem _item = null;
        public ItemAPIController(IItem item)
        {
            _item = item;
        }

        [HttpPost]
        public dynamic GetItem(JObject Obj)
        {
            ItemFilter filter = Obj.ToObject<ItemFilter>();
            ItemDetail re = new ItemDetail();
            re.data = _item.GetItem(filter.limit, filter.offset, filter.order,filter.sort, filter.model);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }
    }
}
