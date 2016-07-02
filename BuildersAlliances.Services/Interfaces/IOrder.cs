using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Services.Interfaces
{
    public interface IOrder
    {
        bool AddOrder(Orders model);
        List<OrderModel> GetOrder(int limit, int offset, string sort,OrderModel model);
        bool DeleteOrder(long OrderId);
        List<OrderType> GetOrderType();

        bool AddOrderItem(OrderItem model);
        List<OrderItemModel> GetOrderItem(int limit, int offset, string sort, OrderItemModel model);
        bool DeleteOrderItem(long OrderId);

        List<DiscountTypeModel> GetDiscountType(int ManufacturerId, int OrderItemId);


         bool SaveItemDiscount(List<ItemDiscounts> itemDiscount);

        List<OrderItemStatus> GetItemStatus();
        bool ChangeOrderItemStatus(OrderItem items);

        List<NotificationModel> GetNotification();


        //List<OrderType> GetOrderTypeItem();

    }
}
