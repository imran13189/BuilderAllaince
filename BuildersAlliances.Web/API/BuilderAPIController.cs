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
    public class BuilderFilter : DataObject
    {
       public BuilderModel model;
    }
    public class BuilderDetail
    {
        public List<BuilderModel> data;
        public int count;
    }


 
   



  

    public class BuilderAPIController : ApiController
    {
        IBuilder _builder = null;
        public BuilderAPIController(IBuilder builder)
        {
            _builder = builder;
        }

        [HttpPost]
        public dynamic GetBuilder(JObject Obj)
       {
            BuilderFilter filter = Obj.ToObject<BuilderFilter>();
            BuilderDetail re = new BuilderDetail();
            re.data = _builder.GetBuilder(filter.limit, filter.offset, filter.order,filter.sort, filter.model);
            return new { rows = re.data, total = re.data.Count>0?re.data.First().TotalRows:0 };
        }


        [HttpPost]
        public dynamic GetBuilderOrders(JObject Obj)
        {
            OrderFilter filter = Obj.ToObject<OrderFilter>();
            OrderDetail re = new OrderDetail();
            re.data = _builder.GetBuilderOrders(filter.limit, filter.offset, filter.order, filter.sort, filter.model);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }

        public List<Builder> GetBuilders(string BuilderName)
        {
            List<Builder> data = _builder.GetBuilder(BuilderName);
            return data;
        }

    }
}
