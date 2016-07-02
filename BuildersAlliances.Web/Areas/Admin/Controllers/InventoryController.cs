using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Domain;
using BuildersAlliances.Web.Filters;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Web.Areas.Admin.Controllers
{
    public class InventoryController : Controller
    {
        public IInventory _inventory = null;
        public InventoryController(IInventory inventory)
        {
            _inventory=inventory;
        }

        // GET: Admin/Inventory
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuantity(Inventory model)
        {
          return Json(_inventory.AddInventory(model));
        }


        [HttpPost]
        public ActionResult RemoveQuantity(Inventory model)
        {
            return Json(_inventory.RemoveQuantity(model));
        }

        public ActionResult GetInventoryHistory(long ItemId)
        {
            ViewBag.ItemId = ItemId;
            return View("_InventoryHistory");
        }


        [HttpPost]
        public ActionResult ItemDetails(ItemModel model)
        {
            return View(model);
        }
    }
}