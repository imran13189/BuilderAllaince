using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;


namespace BuildersAlliances.Repository.Interfaces
{
    public interface IManufacturer
    {
        bool AddManufacturer(Manufacturer model);
        bool DeleteManufacturer(int ManufacturerId);

        List<Manufacturer> GetManufacturer();
        List<ManufacturerModel> GetManufacturer(int limit, int offset, string sort, Manufacturer model);


        bool AddDiscoutType(DiscountType model);
        bool DeleteDiscoutType(int ManufacturerId);

        List<DiscountType> GetDiscoutType();
        List<DiscountTypeModel> GetDiscoutType(int limit, int offset, string sort, DiscountType model);

         bool AddContact(Manufacturer model);
    }
}
