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
    public class TruckFilter : DataObject
    {
        public TruckModel model { get; set; }
    }
    public class TruckDetail
    {
        public List<TruckModel> data;
        public int count;
    }
    public class TruckAPIController : ApiController
    {
        ITruck _truck = null;
        public TruckAPIController(ITruck truck)
        {
            _truck = truck;
        }

        [HttpPost]
        public dynamic GetTrucks(JObject Obj)
      {
            TruckFilter filter = Obj.ToObject<TruckFilter>();
            TruckDetail re = new TruckDetail();
            re.data = _truck.GetTrucks(filter.limit, filter.offset, filter.order, filter.model);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }
    }
}
