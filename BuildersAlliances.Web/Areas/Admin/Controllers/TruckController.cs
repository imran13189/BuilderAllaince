using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildersAlliances.Domain;
using BuildersAlliances.Services.Interfaces;

namespace BuildersAlliances.Web.Areas.Admin.Controllers
{
    public class TruckController : Controller
    {
        ITruck _truck = null;
        IManufacturer _manufacturer = null;

        public TruckController(ITruck truck, IManufacturer manufacturer)
        {
            _truck = truck;
            _manufacturer = manufacturer;
        }
        // GET: Admin/Truck
        public ActionResult Index()
        {
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            ViewBag.TruckType = _truck.GetTruckType();
            return View();
        }

        [HttpPost]
        public ActionResult AddTruck(Trucks model)
        {
            ViewBag.TruckType = _truck.GetTruckType();
            return Json(_truck.AddTruck(model));
        }
        [HttpPost]
        public ActionResult EditTruck(Trucks model)
        {
            ViewBag.TruckType = _truck.GetTruckType();
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            return View("AddTruck", model);
        }

        [HttpPost]
        public ActionResult DeleteTruck(int TruckId)
        {
         return Json(_truck.DeleteTruck(TruckId));
        }
    }
}