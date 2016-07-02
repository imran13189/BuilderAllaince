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

       public List<LogInfo> GetLogInfo(int limit, int offset, string sort, LogInfo model)
        {
       
            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@TypeKey", model.ModuleName),
                         new SqlParameter("@UserId",null),
                         new SqlParameter("@Message",""),
                         new SqlParameter("@PatientId",null),
                         new SqlParameter("@ObjectId",null ),
                         new SqlParameter("@PropertyKey", "")
                         };

            return uow.ExecuteProcedure<LogInfo>("exec GetLogs @offset, @limit, @order, @TypeKey, @UserId, @Message,@PatientId,@ObjectId,@PropertyKey", param);
        
    }
    }
}
