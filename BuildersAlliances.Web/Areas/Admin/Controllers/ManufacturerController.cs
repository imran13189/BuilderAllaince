using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Domain;
using BuildersAlliances.Web.Filters;

namespace BuildersAlliances.Web.Areas.Admin.Controllers
{
    [UserAuthorized]
    public class ManufacturerController : Controller
    {
        IManufacturer _manufacturer;
        public ManufacturerController(IManufacturer manufacturer)
        {
            _manufacturer = manufacturer;
        }
        // GET: Admin/Manufacturer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddManufacturer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddManufacturer(Manufacturer model)
        {
            if (ModelState.IsValid)
            {
                string message = model.ManufacturerId == 0 ? "Saved Successfully" : "Updated Successfully";
                return Json(new { status = _manufacturer.AddManufacturer(model), message = message });
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditManufacturer(Manufacturer model)
        {
            return View("AddManufacturer", model);
        }

        public JsonResult DeleteManufacturer(int ManufacturerId)
        {
            return Json(_manufacturer.DeleteManufacturer(ManufacturerId),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetDiscountType(int ManufacturerId)
        {

            return View("AddDiscountType",new DiscountType() { ManufacturerId=ManufacturerId});
        }

        [HttpPost]
        public ActionResult AddDiscountType(DiscountType model)
        {
            if (ModelState.IsValid)
            {
                string message = model.DiscountTypeId == 0 ? "Saved Successfully" : "Updated Successfully";
                return Json(new { status = _manufacturer.AddDiscoutType(model), message = message });
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditDiscountType(DiscountType model)
        {
            return View("AddDiscountType", model);
        }

        [HttpPost]
        public JsonResult DeleteDiscountType(DiscountType model)
        {
            return Json(_manufacturer.DeleteDiscoutType(model.DiscountTypeId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DiscountType(Manufacturer model)
        {

            return View(new DiscountType() { ManufacturerId = model.ManufacturerId });
        }

        [HttpPost]
        public ActionResult GetContacts(Manufacturer model)
        {
            return View("AddContacts",model);
        }


        [HttpPost]
        public ActionResult AddContact(Manufacturer model)
        {
            _manufacturer.AddContact(model);
            return Json(true);
        }

        //Colors
        [HttpPost]
        public ActionResult GetColors(Manufacturer model)
        {

            return View("ColorIndex", new Colors() { ManufacturerId = model.ManufacturerId });
        }

        [HttpPost]
        public ActionResult AddColor(Colors model)
        {
            if (ModelState.IsValid)
            {
                string message = model.ColorId == 0 ? "Saved Successfully" : "Updated Successfully";
                return Json(new { status = _manufacturer.AddColor(model), message = message });
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditColor(Colors model)
        {
            return View("AddColor", model);
        }

        [HttpPost]
        public JsonResult DeleteColor(Colors model)
        {
            return Json(_manufacturer.DeleteColor(model), JsonRequestBehavior.AllowGet);
        }

        //DoorStyle
        [HttpPost]
        public ActionResult GetDoor(DoorStyle model)
        {

            return View("DoorIndex", new DoorStyle() { ManufacturerId = model.ManufacturerId });
        }

        [HttpPost]
        public ActionResult AddDoorStyle(DoorStyle model)
        {
            if (ModelState.IsValid)
            {
                string message = model.DoorId == 0 ? "Saved Successfully" : "Updated Successfully";
                return Json(new { status = _manufacturer.AddDoorStyle(model), message = message });
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditDoor(DoorStyle model)
        {
            return View("AddDoorStyle", model);
        }

        [HttpPost]
        public JsonResult DeleteDoor(DoorStyle model)
        {
            return Json(_manufacturer.DeleteDoorStyle(model), JsonRequestBehavior.AllowGet);
        }



    }
}