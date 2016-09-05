using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;
using BuildersAlliances.Web.Hubs;
using System.IO;
using BuildersAlliances.Web.Common;

namespace BuildersAlliances.Web.Areas.Qoute.Controllers
{
    
    public class QouteController : Controller
    {
        IQoute _qoute = null;
        IManufacturer _manufacturer = null;
        IItem _item = null;
        public QouteController(IQoute qoute, IManufacturer manufacturer,IItem item)
        {
            _qoute = qoute;
            _manufacturer = manufacturer;
            _item = item;
        }
        // GET: Order/Orders
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQoute()
        {
            ViewBag.BuilderName = "";
            return View(); 
        }
        [HttpPost]
        public ActionResult AddQoute(BuildersAlliances.Domain.Qoute model)
        {
          return Json(_qoute.AddQoute(model));

        }

        [HttpPost]
        public ActionResult EditQoute(BuildersAlliances.Domain.Qoute model)
        {
            //ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            
            ViewBag.BuilderName = model.BuilderName;
            return View("AddQoute", model);
        }
        
        public ActionResult DeleteQoute(int QouteId)
        {
           return Json(_qoute.DeleteQoute(QouteId),JsonRequestBehavior.AllowGet);
        }


        
        public ActionResult AddQouteItem(long QouteId)
        {
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            

            return View(new QouteItems() { QouteId = QouteId });
        }
        [HttpPost]
        public ActionResult AddQouteItem(QouteItems model)
        {
            _qoute.AddQouteItem(model);
            
            

            return Json(true);

        }

        [HttpPost]
        public ActionResult EditQouteItem(QouteItems model)
        {
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            
            return View("AddQouteItem", model);
        }

        public ActionResult DeleteQouteItem(int QouteItemId)
        {
            return Json(_qoute.DeleteQouteItem(QouteItemId), JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult QouteItem(long QouteId)
        {
            return View(new QouteItems() {QouteId= QouteId });
        }


        public ActionResult QouteTemplate(long QouteId)
        {
            return View(_qoute.GetQoute(QouteId));
        }


        public ActionResult SendQoute(long QouteId)
        {
            BuildersAlliances.Domain.Qoute data = _qoute.GetQoute(QouteId);
            string content = RenderRazorViewToString("QouteEmailTemplate", data);
            EmailService.SendQouteInEmail(data.Builder.Email,"Quotation", content, true, true);
            return Json(true,JsonRequestBehavior.AllowGet);
        }


        public ActionResult ApproveQoute(long Id)
        {
            _qoute.ApproveQoute(Id);

            return View("Message");
        }

        public ActionResult RejectQoute(long Id)
        {
            _qoute.RejectQoute(Id);

            return View("Message");
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}