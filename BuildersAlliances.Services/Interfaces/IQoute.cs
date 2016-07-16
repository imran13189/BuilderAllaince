using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Services.Interfaces
{
    public interface IQoute
    {
        bool AddQoute(Qoute model);
        List<QouteModel> GetQoute(int limit, int offset, string sort,QouteModel model);
        bool DeleteQoute(long QouteId);
        
        
        bool DeleteQouteItem(long QouteId);

        List<QouteItemsModel> GetQouteItem(int limit, int offset, string sort, QouteItemsModel model);

        bool AddQouteItem(QouteItems model);

        Qoute GetQoute(long QouteId);

        bool ApproveQoute(long QouteId);
        bool RejectQoute(long QouteId);

    }
}
