using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Services.Interfaces
{
    public interface IInvoice
    {

        Orders GetOrderItems(long OrderId, long[] InventoryItems);

    }
}
