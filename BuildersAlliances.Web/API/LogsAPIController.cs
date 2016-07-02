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
    class LogInfoListFilters : DataObject
    {
      public  LogInfo model;
    }
    class LogInfoReturnVal
    {
        public List<LogInfo> data;
        public int total;
    }
    public class LogInfoAPIController : ApiController
    {
        ILoginfo _lofInfo = null;
        public LogInfoAPIController(ILoginfo logInfo)
        {
            _lofInfo = logInfo;
        }

        [HttpPost]
        public dynamic GetLogInfo(JObject Obj)
      {
            LogInfoListFilters filter = Obj.ToObject<LogInfoListFilters>();
            LogInfoReturnVal re = new LogInfoReturnVal();
            re.data = _lofInfo.GetLogInfo(filter.limit, filter.offset, filter.order, filter.model);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }
    }
}
