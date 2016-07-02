using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Domain;

namespace BuildersAlliances.Services
{
   public class LogInfoServices:ILoginfo
    {
        Repository.Interfaces.ILogInfo _logInfo = null;
        public LogInfoServices()
        {
            _logInfo = new BuildersAlliances.Repository.LogInfoRepository();
        }
        public List<LogInfo> GetLogInfo(int limit, int offset, string sort, LogInfo model)
        {
                return   _logInfo.GetLogInfo(limit, offset, sort, model);
        }
    }
}
