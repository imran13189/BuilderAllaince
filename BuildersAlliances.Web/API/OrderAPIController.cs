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
    public class OrderFilter : DataObject
    {
        public OrderModel model { get; set; }
    }
    public class OrderDetail
    {
        public List<OrderModel> data;
        public int count;
    }


    public class OrderItemFilter : DataObject
    {
        public OrderItemModel model { get; set; }
    }
    public class OrderItemDetail
    {
        public List<OrderItemModel> data;
        public int count;
    }
    public class OrderAPIController : ApiController
    {
        IOrder _Order = null;
        IItem _item = null;
        public OrderAPIController(IOrder Order,IItem item)
        {
            _Order = Order;
            _item = item;
        }

        [HttpPost]
        public dynamic GetOrder(JObject Obj)
        {
            try
            {
                OrderFilter filter = Obj.ToObject<OrderFilter>();
                OrderDetail re = new OrderDetail();
                re.data = _Order.GetOrder(filter.limit, filter.offset, filter.order, filter.model);
                return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public dynamic GetOrderItem(JObject Obj)
        {
            try
            {
                OrderItemFilter filter = Obj.ToObject<OrderItemFilter>();
                OrderItemDetail re = new OrderItemDetail();
                re.data = _Order.GetOrderItem(filter.limit, filter.offset, filter.order, filter.model);
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
    }
}
