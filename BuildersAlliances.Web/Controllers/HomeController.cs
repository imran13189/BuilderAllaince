using BuildersAlliances.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildersAlliances.Web.Controllers
{
    public class HomeController : Controller
    {
        IOrder _order = null;
        public HomeController(IOrder order)
        {
            _order = order;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Notification()
        {

            return View("_Notification",_order.GetNotification());
        }
    }
}