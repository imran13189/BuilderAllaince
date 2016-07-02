using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
namespace BuildersAlliances.Repository.Interfaces
{
    public interface ILogInfo
    {
         List<LogInfo> GetLogInfo(int limit, int offset, string sort, LogInfo model);
    }
}
