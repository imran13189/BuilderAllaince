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
    [UserAuthorized]
    public class BuilderController : Controller
    {
        IBuilder _builder;
        public BuilderController(IBuilder builder)
        {
            _builder = builder;
        }
        // GET: Admin/Manufacturer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBuilder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBuilder(Builder model)
        {
           
                string message = model.BuilderId == 0 ? "Saved Successfully" : "Updated Successfully";
                return Json(new { status = _builder.AddBuilder(model), message = message });
            
        }

        [HttpPost]
        public ActionResult EditBuilder(Builder model)
        {
            return View("AddBuilder", model);
        }

        [HttpPost]
        public JsonResult DeleteBuilder(BuilderModel model)
        {
            return Json(_builder.DeleteBuilder(model),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetDiscountType(int ManufacturerId)
        {

            return View("AddDiscountType",new DiscountType() { ManufacturerId=ManufacturerId});
        }

      /// <summary>
      /// Create by Imran, 28 jun 2016
      /// Get all orders of the builders
      /// </summary>
      /// <param name="BuilderId"></param>
      /// <returns></returns>
        public ActionResult BuilderOrders(int BuilderId)
        {
            
            return View();
        }

    }
}