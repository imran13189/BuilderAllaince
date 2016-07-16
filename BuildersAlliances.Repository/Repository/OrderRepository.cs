using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using BuildersAlliances.Repository.Interfaces;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Repository
{
   public class OrderRepository: IOrder
    {
        UnityOfWork uow = null;
        public OrderRepository()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
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
                    Orders data= uow.Repository<Orders>().Get(x => x.OrderId == model.OrderId);
                    
                    data.OrderTypeId = model.OrderTypeId;
                    data.Installer = model.Installer;
                    data.OrderStatus = model.OrderStatus;

                }
                uow.SaveChanges();
                return true;
            }
            catch(Exception e) {
                return false;
            }

        }
        public bool DeleteOrder(long OrderId)
        {
            try
            {
               Orders model= uow.Repository<Orders>().Get(x => x.OrderId == OrderId);
                model.IsDeleted = true;
                uow.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
     public   List<OrderModel> GetOrder(int limit, int offset, string sort, OrderModel model)
        {

            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort)
                         //new SqlParameter("@Manufacturer", model.ManufacturerName),
                         //new SqlParameter("@ItemSKU",model.ItemSKU),
                         //new SqlParameter("@ItemName", model.ItemName)
                         };

            return uow.ExecuteProcedure<OrderModel>("exec GetOrders @offset, @limit, @order", param);


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
                    
                    uow.Repository<OrderItem>().Add(model);
                }
                else
                {
                    OrderItem data = uow.Repository<OrderItem>().AsQuerable().FirstOrDefault(x=>x.OrderItemId==model.OrderItemId);
                    data.ItemId = model.ItemId;
                    
                    data.Quantity = model.Quantity;
                  
                    data.ItemId = model.ItemId;

                }
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool DeleteOrderItem(long OrderId)
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
          return   uow.Repository<OrderType>().GetAll().ToList();
        }

       public  List<DiscountType> GetDiscountType(int ManufacturerId)
        {
            List<DiscountType> model = uow.Repository<DiscountType>().AsQuerable().Where(x => x.ManufacturerId == ManufacturerId).ToList();
            return model;
        }
    }
}
