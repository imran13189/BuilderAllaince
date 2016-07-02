using BuildersAlliances.CustomModel;
using BuildersAlliances.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Services.Interfaces
{
public    interface IBuilder
    {
        bool AddBuilder(Builder model);
        bool DeleteBuilder(BuilderModel model);
        List<BuilderModel> GetBuilder(int limit, int offset,string order, string sort, BuilderModel model);
        List<OrderModel> GetBuilderOrders(int limit, int offset, string order, string sort, OrderModel model);
        List<Builder> GetBuilder(string BuilderName);

    }
}
