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
  public  class OrderService:IOrder
    {
        UnityOfWork uow = null;
        public OrderService()
        {
            if (uow == null)
            {
                uow = new UnityOfWork();
            }
        }
        public bool AddOrder(Orders model)
        {
            try
            {
                if (model.OrderId == 0)
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.OrderStatus = 1;
                    uow.Repository<Orders>().Add(model);
                }
                else
                {
                    Orders data = uow.Repository<Orders>().Get(x => x.OrderId == model.OrderId);

                    data.OrderTypeId = model.OrderTypeId;
                    data.Installer = model.Installer;
                    data.BuilderId = model.BuilderId;
                  //  data.OrderStatus = model.OrderStatus;

                }
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool DeleteOrder(long OrderId)
        {
            try
            {
                Orders model = uow.Repository<Orders>().Get(x => x.OrderId == OrderId);
                model.IsDeleted = true;
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<OrderModel> GetOrder(int limit, int offset, string sort, OrderModel model)
        {

            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@OrderId", model.OrderId),
                         //new SqlParameter("@ItemSKU",model.ItemSKU),
                         //new SqlParameter("@ItemName", model.ItemName)
                         };

            return uow.ExecuteProcedure<OrderModel>("exec GetOrders @offset, @limit, @order,@OrderId", param);


        }

        /// <summary>
        /// OrderItems Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public bool AddOrderItem(OrderItem model)
        {
            try
            {
                if (model.OrderItemId == 0)
                {
                    model.ItemStatus = 7;
                    uow.Repository<OrderItem>().Add(model);
                }
                else
                {
                    OrderItem data = uow.Repository<OrderItem>().AsQuerable().FirstOrDefault(x => x.OrderItemId == model.OrderItemId);
                    data.ItemId = model.ItemId;

                    data.Quantity = model.Quantity;
                    data.DeliveryDate = model.DeliveryDate;
                   
                  
                    data.ItemId = model.ItemId;

                }
                uow.SaveChanges();
                AddNotification(model.OrderId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool DeleteOrderItem(long OrderItemId)
        {
            try
            {
                OrderItem model = uow.Repository<OrderItem>().Get(x => x.OrderItemId == OrderItemId);
                uow.Repository<OrderItem>().Delete(model);  
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<OrderItemModel> GetOrderItem(int limit, int offset, string sort, OrderItemModel model)
        {

            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@Manufacturer", model.ManufacturerName),
                         new SqlParameter("@ItemSKU",model.ItemSKU),
                         new SqlParameter("@ItemName", model.ItemName),
                         new SqlParameter("@OrderId", model.OrderId)
                         };

            return uow.ExecuteProcedure<OrderItemModel>("exec GetOrderItems @offset, @limit, @order, @Manufacturer, @ItemSKU, @ItemName,@OrderId", param);


        }

        public List<OrderType> GetOrderType()
        {
            return uow.Repository<OrderType>().GetAll().ToList();
        }

        public List<DiscountTypeModel> GetDiscountType(int ManufacturerId,int OrderItemId)
        {
            IQueryable<DiscountType> model = uow.Repository<DiscountType>().AsQuerable().Where(x => x.ManufacturerId == ManufacturerId);
            IQueryable<int> data = uow.Repository<ItemDiscounts>().AsQuerable().Where(x => x.OrderItemId == OrderItemId).Select(x=>x.DiscountTypeId);
            var data1 = from m in model where data.Contains(m.DiscountTypeId) select m;
            var data2 = from m in model where !data.Contains(m.DiscountTypeId) select m;
            List<DiscountTypeModel> model1 = new List<DiscountTypeModel>();


            foreach (DiscountType item in data1)
            {
                model1.Add(new DiscountTypeModel() {
                    DiscountTypeId=item.DiscountTypeId,
                    IsSelected=true,
                    DiscountTypeName=item.DiscountTypeName,
                    Multiplier=item.Multiplier

                });
            }


            foreach (DiscountType item in data2)
            {
                model1.Add(new DiscountTypeModel()
                {
                    DiscountTypeId = item.DiscountTypeId,
                    IsSelected = false,
                    DiscountTypeName = item.DiscountTypeName,
                    Multiplier = item.Multiplier

                });
            }

            return model1;
        }


        public bool SaveItemDiscount(List<ItemDiscounts> itemDiscount)
        {
            try
            {
              
                foreach (ItemDiscounts item in itemDiscount)
                {
                    ItemDiscounts md = uow.Repository<ItemDiscounts>().AsQuerable().FirstOrDefault(x => x.DiscountTypeId == item.DiscountTypeId && x.OrderItemId == item.OrderItemId);
                    if (md == null)
                    {
                        uow.Repository<ItemDiscounts>().Add(new ItemDiscounts()
                        {
                            DiscountTypeId = item.DiscountTypeId,
                            OrderItemId = item.OrderItemId,
                            CreatedDate = DateTime.UtcNow,
                            Multiplier = item.Multiplier

                        });
                    }
                }

                uow.SaveChanges();
                return true;
            }
            catch(Exception e) {
                return false;
            }
        }

        public List<OrderItemStatus> GetItemStatus()
        {
          return   uow.Repository<OrderItemStatus>().GetAll().ToList();
        }

        public bool ChangeOrderItemStatus(OrderItem items)
        {
            try
            {
                OrderItem model = uow.Repository<OrderItem>().Get(x => x.OrderItemId ==items.OrderItemId);
                model.ItemStatus = items.ItemStatus;
                uow.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddNotification(long OrderId)
        {
            SqlParameter[] param = new SqlParameter[] {
                   
                         new SqlParameter("@OrderId", OrderId)
                         };

           List<NotificationItem> data =   uow.ExecuteProcedure<NotificationItem>("exec AddNotification @OrderId", param);

            foreach (var item in data)
            {
                uow.Repository<Notification>().Add(new Notification() {
                    ManufacturerId =item.ManufacturerId,
                    OrderId=OrderId,
                    CreatedDate=DateTime.UtcNow
                });
            }
            uow.SaveChanges();


            return true;
        }

       public List<NotificationModel> GetNotification()
        {
            SqlParameter[] param = new SqlParameter[] { };

            List<NotificationModel> data = uow.ExecuteProcedure<NotificationModel>("exec GetNotifications", param);
            return data;
        }
    }
}
