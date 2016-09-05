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

namespace BuildersAlliances.Web.Areas.Order.Controllers
{
    
    public class InvoiceController : Controller
    {
        IInvoice _Invoice = null;
       // IManufacturer _manufacturer = null;
        //IItem _item = null;
        public InvoiceController(IInvoice Invoice)
        {
            _Invoice = Invoice;
            
        }
        // GET: Invoice/Invoice
        public ActionResult Index()
        {
            return View("InvoiceItem");
        }

        [HttpPost]
        public ActionResult GenerateInvoice(long OrderId,long[] InvoiceItems)
        {
            
            return View("InvoiceTemplate",_Invoice.GetOrderItems(OrderId, InvoiceItems));
        }

        [HttpPost]
        public ActionResult SendInvoice(string InvoiceHtml,string Email)
        {
            
           // string content = RenderRazorViewToString("QouteEmailTemplate", data);
            EmailService.SendQouteInEmail(Email, "Quotation", InvoiceHtml, true, true);
            return Json(true, JsonRequestBehavior.AllowGet);
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