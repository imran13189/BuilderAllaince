using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuildersAlliances.Domain;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.CustomModel;
using Newtonsoft.Json.Linq;

namespace BuildersAlliances.Web.API
{
    public class ManuacturerFilter : DataObject
    {
       public Manufacturer model;
    }
    public class ManuacturerDetail
    {
        public List<ManufacturerModel> data;
        public int count;
    }


    public class DiscountFilter : DataObject
    {
        public DiscountType model;
    }
    public class DiscountDetail
    {
        public List<DiscountTypeModel> data;
        public int count;
    }

    public class ColorFilter : DataObject
    {
        public ColorModel model;
    }
    public class ColorDetail
    {
        public List<ColorModel> data;
        public int count;
    }



    public class DoorTypeFilter : DataObject
    {
        public DoorStyleModel model;
    }
    public class DoorDetail
    {
        public List<DoorStyleModel> data;
        public int count;
    }


    public class ManufacturerAPIController : ApiController
    {
        IManufacturer _manufacturer = null;
        public ManufacturerAPIController(IManufacturer manufacturer)
        {
            _manufacturer = manufacturer;
        }

        [HttpPost]
        public dynamic GetManufacturer(JObject Obj)
      {
            ManuacturerFilter filter = Obj.ToObject<ManuacturerFilter>();
            ManuacturerDetail re = new ManuacturerDetail();
            re.data = _manufacturer.GetManufacturer(filter.limit, filter.offset, filter.order,filter.sort, filter.model);
            return new { rows = re.data, total = re.data.Count>0?re.data.First().TotalRows:0 };
        }

        [HttpPost]
        public dynamic GetDiscountType(JObject Obj)
        {
            DiscountFilter filter = Obj.ToObject<DiscountFilter>();
            DiscountDetail re = new DiscountDetail();
            re.data = _manufacturer.GetDiscoutType(filter.limit, filter.offset, filter.order, filter.model);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }


        [HttpPost]
        public dynamic GetColors(JObject Obj)
        {
           ColorFilter filter = Obj.ToObject<ColorFilter>();
            ColorDetail re = new ColorDetail();
            re.data = _manufacturer.GetColors(filter.limit, filter.offset, filter.order, filter.model);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }


        [HttpPost]
        public dynamic GetDoor(JObject Obj)
        {
            DoorTypeFilter filter = Obj.ToObject<DoorTypeFilter>();
            DoorDetail re = new DoorDetail();
            re.data = _manufacturer.GetDoorStyle(filter.limit, filter.offset, filter.order, filter.model);
            return new { rows = re.data, total = re.data.Count > 0 ? re.data.First().TotalRows : 0 };
        }
    }
}
