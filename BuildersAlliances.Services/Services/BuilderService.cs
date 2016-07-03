using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Domain;

using BuildersAlliances.CustomModel;
using BuildersAlliances.Repository;
using System.Data.SqlClient;

namespace BuildersAlliances.Services
{
    public class BuilderService: IBuilder
    {
        UnityOfWork uow = null;
        public BuilderService()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }
        public bool AddBuilder(Builder model)
        {
            try
            {
                if (model.BuilderId == 0)
                {
                    
                    uow.Repository<Builder>().Add(model);
                }
                else
                {
                    Builder data = uow.Repository<Builder>().AsQuerable().FirstOrDefault(x => x.BuilderId == model.BuilderId);
                    data.BuilderName = model.BuilderName;
                    data.Address1 = model.Address1;
                    data.Address2 = model.Address2;
                    data.Address3 = model.Address3;
                    data.Phone = model.Phone;
                    data.Email = model.Email;
                    data.City = model.City;

                }
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public bool DeleteBuilder(BuilderModel model)
        {
            try
            {
                Builder data = uow.Repository<Builder>().Get(x => x.BuilderId == model.BuilderId);
                data.IsDeleted = true;
                uow.SaveChanges();
                return true;
            }
            catch { return false; }

        }
        public List<BuilderModel> GetBuilder(int limit, int offset,string order, string sort, BuilderModel model)
        {
            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", order),
                         new SqlParameter("@sort", sort),
                         new SqlParameter("@BuilderName", model.BuilderName),
                         new SqlParameter("@Address1", model.Address1),
                         new SqlParameter("@Address2", model.Address2),
                         new SqlParameter("@Address3", model.Address3),
                         new SqlParameter("@Email", model.Email),
                         new SqlParameter("@Phone", model.Phone)
                         };
            var data= uow.ExecuteProcedure<BuilderModel>("exec GetBuilder @offset, @limit, @order,@sort, @BuilderName, @Address1,@Address2,@Address3, @Email,@Phone", param);
            return data;
        }

      public  List<OrderModel> GetBuilderOrders(int limit, int offset, string order, string sort, OrderModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", order),
                         new SqlParameter("@OrderId", model.OrderId),
                         new SqlParameter("@BuilderId",model.BuilderId),
                         //new SqlParameter("@ItemName", model.ItemName)
                         };

                return uow.ExecuteProcedure<OrderModel>("exec GetBuildersOrders @offset, @limit, @order,@OrderId,@BuilderId", param);
            }
            catch(Exception e) {

                throw e;
            }
        }

        public List<Builder> GetBuilder(string BuilderName)
        {

            return uow.Repository<Builder>().AsQuerable().Where(x => x.BuilderName.Contains(BuilderName)).ToList();

        }

    }
}
