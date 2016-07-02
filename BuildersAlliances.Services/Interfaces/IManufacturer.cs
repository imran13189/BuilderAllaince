using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Services.Interfaces
{ 
    public interface IManufacturer
    {
        bool AddManufacturer(Manufacturer model);
        bool DeleteManufacturer(int ManufacturerId);
        List<ManufacturerModel> GetManufacturer(int limit, int offset, string order, string sort, Manufacturer model);
        List<Manufacturer> GetManufacturer();

        //Discount
        bool AddDiscoutType(DiscountType model);
        bool DeleteDiscoutType(int ManufacturerId);

        List<DiscountType> GetDiscoutType();
        List<DiscountTypeModel> GetDiscoutType(int limit, int offset, string sort, DiscountType model);

        bool AddContact(Manufacturer model);

        //Colors

        bool AddColor(Colors model);
        List<ColorModel> GetColors(int limit, int offset, string sort, ColorModel model);
        bool DeleteColor(Colors model);

        //DoorStyle


        bool AddDoorStyle(DoorStyle model);
        List<DoorStyleModel> GetDoorStyle(int limit, int offset, string sort, DoorStyleModel model);
        bool DeleteDoorStyle(DoorStyle model);

    }
}
