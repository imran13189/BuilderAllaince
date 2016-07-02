using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using BuildersAlliances.Repository.Interfaces;
using BuildersAlliances.CustomModel;
using System.Data.Entity.Core.Objects;

namespace BuildersAlliances.Repository
{
    public class ManufacturerRepository:IManufacturer
    {
        UnityOfWork uow = null;
        public ManufacturerRepository()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }

      public  bool AddManufacturer(Manufacturer model)
        {
            try
            {
                if (model.ManufacturerId == 0)
                {

                    uow.Repository<Manufacturer>().Add(model);


                    
                }
                else
                {
                    Manufacturer data= uow.Repository<Manufacturer>().Get(x => x.ManufacturerId == model.ManufacturerId);
                    data.ManufacturerName = model.ManufacturerName;
                    data.EmailId = model.EmailId;
                    data.ContactNumber = model.ContactNumber;
                    data.WebSiteUrl = model.WebSiteUrl;
                    data.Address = model.Address;
                    
                    uow._context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                }
                uow.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    public    bool DeleteManufacturer(int ManufacturerId)
        {
            try
            {
                //    Manufacturer model = uow.Repository<Manufacturer>().Get(x => x.ManufacturerId == ManufacturerId);
                //    model.IsDeleted = true;
                SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@ManufacturerId", ManufacturerId)
                         
                         };
                uow.ExecuteCommand("exec DeleteManufacturer "+ManufacturerId);
            //    uow.SaveChanges();
            
                return true;
            }
            catch
            {
                return false;
            }

        }

    public    List<ManufacturerModel> GetManufacturer(int limit, int offset, string sort, Manufacturer model)
        {
           
         SqlParameter[] param= new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@Manufacturer", model.ManufacturerName),
                         new SqlParameter("@Contact", model.ContactNumber),
                         new SqlParameter("@Address", model.Address),
                         new SqlParameter("@Email", model.EmailId),
                         new SqlParameter("@Website", model.WebSiteUrl),
                         };

         return   uow.ExecuteProcedure<ManufacturerModel>("exec GetManufacturer @offset, @limit, @order,@Manufacturer,@Contact,@Address,@Email,@Website", param);

        }

        public List<Manufacturer> GetManufacturer()
        {
            return uow.Repository<Manufacturer>().GetAll(x=>x.IsDeleted==false).ToList();
        }


        public bool AddDiscoutType(DiscountType model)
        {
            try
            {
                if (model.DiscountTypeId == 0)
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.ModifiedDate = DateTime.UtcNow;
                   
                    uow.Repository<DiscountType>().Add(model);



                }
                else
                {
                    DiscountType data = uow.Repository<DiscountType>().Get(x => x.DiscountTypeId == model.DiscountTypeId);
                    data.DiscountTypeName = model.DiscountTypeName;
                    data.Multiplier = model.Multiplier;
                    data.ModifiedDate = DateTime.UtcNow;
                
                    uow._context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                }
                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeleteDiscoutType(int ManufacturerId)
        {
            try
            {
                DiscountType model = uow.Repository<DiscountType>().Get(x => x.DiscountTypeId == ManufacturerId);
                model.IsDeleted = true;

                uow.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<DiscountTypeModel> GetDiscoutType(int limit, int offset, string sort, DiscountType model)
        {

            SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", sort),
                         new SqlParameter("@DiscountType", model.DiscountTypeName)
                         
                         };

            return uow.ExecuteProcedure<DiscountTypeModel>("exec GetDiscoutType @offset, @limit, @order,@DiscountType", param);

        }

        public List<DiscountType> GetDiscoutType()
        {
            return uow.Repository<DiscountType>().GetAll(x => x.IsDeleted == false).ToList();
        }

        public bool AddContact(Manufacturer model)
        {
            try
            {
                Manufacturer data = uow.Repository<Manufacturer>().Get(x => x.ManufacturerId == model.ManufacturerId);
                data.ServiceContact = model.ServiceContact;
                data.ServiceName = model.ServiceName;
                data.DeliveryContact = model.DeliveryContact;
                data.DeliveryName = model.DeliveryName;
                data.SaleName = model.SaleName;
                data.SalesContact = model.SalesContact;
                uow.SaveChanges();
                return true;
            }
            catch (Exception e) { return true; }
        }

        

    }
}
