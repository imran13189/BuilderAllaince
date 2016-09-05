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
    public class QouteFilter : DataObject
    {
        public QouteModel model { get; set; }
    }
    public class QouteDetail
    {
        public List<QouteModel> data;
        public int count;
    }


    public class QouteItemFilter : DataObject
    {
        public QouteItemsModel model { get; set; }
    }
    public class QouteItemDetail
    {
        public List<QouteItemsModel> data;
        public int count;
    }
    public class QouteAPIController : ApiController
    {
        IQoute _qoute = null;
        IItem _item = null;
        public QouteAPIController(IQoute qoute,IItem item)
        {
            _qoute = qoute;
            _item = item;
        }

        [HttpPost]
        public dynamic GetQoute(JObject Obj)
        {
            try
            {
                QouteFilter filter = Obj.ToObject<QouteFilter>();
                QouteDetail re = new QouteDetail();
                re.data = _qoute.GetQoute(filter.limit, filter.offset, filter.order, filter.model);
                return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public dynamic GetQouteItem(JObject Obj)
        {
            try
            {
                QouteItemFilter filter = Obj.ToObject<QouteItemFilter>();
                QouteItemDetail re = new QouteItemDetail();
                re.data = _qoute.GetQouteItem(filter.limit, filter.offset, filter.order, filter.model);

                return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public List<Items> GetItemByManufacturer(int ManufacturerId)
        {
           return _item.GetItemByManufacturer(ManufacturerId);
        }

        public decimal GetItemPrice(long ItemId)
        {
           return _item.GetItemPrice(ItemId);
            
        }
    }
}
