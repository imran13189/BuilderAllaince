using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;

using System.Data.SqlClient;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.CustomModel;
using BuildersAlliances.Repository;

namespace BuildersAlliances.Services
{
  public  class InvoiceService:IInvoice
    {
        UnityOfWork uow = null;
        public InvoiceService()
        {
            if (uow == null)
            {
                uow = new UnityOfWork();
            }
        }
        public Orders GetOrderItems(long OrderId,long[] InventoryItems)
        {
            try
            {
                // uow.DisableProxy();
                Orders orders = uow.Repository<Orders>().AsQuerable().FirstOrDefault(x => x.OrderId == OrderId);
                // orders.OrderItem=orders.OrderItem.Where(x)
                orders.OrderItem = orders.OrderItem.Where(x => InventoryItems.Contains(x.OrderItemId)).ToList();

                return orders;
            }
            catch(Exception e)
            {
                throw e;
            }
            


        }

    }
}
