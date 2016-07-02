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
    public class ItemController : Controller
    {
        IItem _item = null;
        IManufacturer _manufacturer = null;
        

       public ItemController(IItem item,IManufacturer manufacturer)
        {
            _item = item;
            _manufacturer = manufacturer;
        }
        // GET: Admin/Item
        public ActionResult Index()
        {
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            return View();
        }

        public ActionResult AddItem()
        {
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            
           
            return View();
        }
        [HttpPost]
        public ActionResult AddItem(Items model)
        {

            if (ModelState.IsValid)
            {
                string message = model.ItemId == 0 ? "Saved Successfully" : "Updated Successfully";
                return Json(new { status = _item.AddItem(model), message = message });
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditItem(Items model)
        {
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            return View("AddItem", model);
        }

        public JsonResult DeleteItem(int ItemId)
        {
            return Json(_item.DeleteItem(ItemId),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetColorAndDoorStyle(int ManufacturerId)
        {
            List<DoorStyle> doorStyle = _item.GetDoors(ManufacturerId);
            List<Colors> color = _item.GetColors(ManufacturerId);
            return Json(new {doorStyle=doorStyle,colorList=color },JsonRequestBehavior.AllowGet);
        }

       
    }
}