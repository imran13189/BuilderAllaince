using BuildersAlliances.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Repository.Interfaces;

namespace BuildersAlliances.Repository
{
  public  class LogInfoRepository:ILogInfo
    {
        UnityOfWork uow = null;
        public LogInfoRepository()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }

      
    }
}
