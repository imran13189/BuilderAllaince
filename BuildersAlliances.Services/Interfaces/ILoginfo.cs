using BuildersAlliances.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Services.Interfaces
{
    public interface ILoginfo
    {
        List<LogInfo> GetLogInfo(int limit, int offset, string sort, LogInfo model);
    }
}
