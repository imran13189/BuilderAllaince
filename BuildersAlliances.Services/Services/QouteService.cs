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
  public  class QouteService:IQoute
    {
        UnityOfWork uow = null;
        public QouteService()
        {
            if (uow == null)
            {
                uow = new UnityOfWork();
            }
        }
        public bool AddQoute(Qoute model)
        {
            try
            {
                if (model.QouteId == 0)
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.State = 1;
                    
                    uow.Repository<Qoute>().Add(model);
                }
                else
                {
                    Qoute data = uow.Repository<Qoute>().Get(x => x.QouteId == model.QouteId);

                    data.State = 1;
                    
                    data.BuilderId = model.BuilderId;
                  //  data.Qoutetatus = model.Qoutetatus;

                }
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool DeleteQoute(long QouteId)
        {
            try
            {
                Qoute model = uow.Repository<Qoute>().Get(x => x.QouteId == QouteId);
                model.IsDeleted = true;
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<QouteModel> GetQoute(int limit, int offset, string sort, QouteModel model)
        {

            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@QouteId", model.QouteId),
                         //new SqlParameter("@ItemSKU",model.ItemSKU),
                         //new SqlParameter("@ItemName", model.ItemName)
                         };

            return uow.ExecuteProcedure<QouteModel>("exec GetQoutes @offset, @limit, @order,@QouteId", param);


        }

        /// <summary>
        /// QouteItems Repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public bool AddQouteItem(QouteItems model)
        {
            try
            {
                if (model.QouteItemId == 0)
                {
                    
                    uow.Repository<QouteItems>().Add(model);
                }
                else
                {
                    QouteItems data = uow.Repository<QouteItems>().AsQuerable().FirstOrDefault(x => x.QouteItemId == model.QouteItemId);
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
        public bool DeleteQouteItem(long QouteItemId)
        {
            try
            {
                QouteItems model = uow.Repository<QouteItems>().Get(x => x.QouteItemId == QouteItemId);
                uow.Repository<QouteItems>().Delete(model);  
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<QouteItemsModel> GetQouteItem(int limit, int offset, string sort, QouteItemsModel model)
        {

            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@Qoute", sort),
                         new SqlParameter("@Manufacturer", model.ManufacturerName),
                         new SqlParameter("@ItemSKU",model.ItemSKU),
                         new SqlParameter("@ItemName", model.ItemName),
                         new SqlParameter("@QouteId", model.QouteId)
                         };

            return uow.ExecuteProcedure<QouteItemsModel>("exec GetQouteItems @offset, @limit, @Qoute, @Manufacturer, @ItemSKU, @ItemName,@QouteId", param);


        }

    

     

     public Qoute GetQoute(long QouteId)
        {
            return uow.Repository<Qoute>().Get(x => x.QouteId == QouteId);
        }



        public bool ApproveQoute(long QouteId)
        {
            Qoute model = uow.Repository<Qoute>().Get(x => x.QouteId == QouteId);
            model.State = 2;
            uow.SaveChanges();
            return true;
        }
    public    bool RejectQoute(long QouteId)
        {
            Qoute model = uow.Repository<Qoute>().Get(x => x.QouteId == QouteId);
            model.State = 2;
            uow.SaveChanges();
            return true;
        }
    }
}
