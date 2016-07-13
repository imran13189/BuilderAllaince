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

namespace BuildersAlliances.Web.Areas.Order.Controllers
{
    
    public class OrdersController : Controller
    {
        IOrder _order = null;
        IManufacturer _manufacturer = null;
        IItem _item = null;
        public OrdersController(IOrder order, IManufacturer manufacturer,IItem item)
        {
            _order = order;
            _manufacturer = manufacturer;
            _item = item;
        }
        // GET: Order/Orders
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddOrder()
        {
           // ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            ViewBag.OrderType = _order.GetOrderType();
            ViewBag.BuilderName = "";
            return View(); 
        }
        [HttpPost]
        public ActionResult AddOrder(Orders model)
        {
          return Json(_order.AddOrder(model));

        }

        [HttpPost]
        public ActionResult EditOrder(Orders model)
        {
            //ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            ViewBag.OrderType = _order.GetOrderType();
            ViewBag.BuilderName = model.BuilderName;
            return View("AddOrder", model);
        }
        
        public ActionResult DeleteOrder(int OrderId)
        {
           return Json(_order.DeleteOrder(OrderId),JsonRequestBehavior.AllowGet);
        }


        
        public ActionResult AddOrderItem(long OrderId)
        {
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            ViewBag.OrderType = _order.GetOrderType();

            return View(new OrderItem() { OrderId = OrderId,DeliveryDate=DateTime.UtcNow });
        }
        [HttpPost]
        public ActionResult AddOrderItem(OrderItem model)
        {
            _order.AddOrderItem(model);
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            string noti=  RenderRazorViewToString("_Notification", _order.GetNotification());
            context.Clients.All.Notify(noti);

            return Json(true);

        }

        [HttpPost]
        public ActionResult EditOrderItem(OrderItem model)
        {
            ViewBag.ManufacturerList = _manufacturer.GetManufacturer();
            ViewBag.OrderType = _order.GetOrderType();
            return View("AddOrderItem", model);
        }

        public ActionResult DeleteOrderItem(int OrderItemId)
        {
            return Json(_order.DeleteOrderItem(OrderItemId), JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult OrderItem(long OrderId)
        {
            return View(new OrderItem() {OrderId= OrderId });
        }

        [HttpPost]
        public ActionResult GetManufacturer(OrderItemModel model)
        {
          ViewBag.OrderItemId = model.OrderItemId;
          return View("Discount", _order.GetDiscountType(model.ManufacturerId,model.OrderItemId));
        }

        [HttpPost]
        public ActionResult SaveDiscountType(List<ItemDiscounts> model)
        {
            _order.SaveItemDiscount(model);
            return Json(true);
        }

        public ActionResult GetItemStatusList()
        {
         return Json(_order.GetItemStatus(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteItemOrder(int OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }

        public ActionResult ChangeOrderItemStatus(OrderItem item)
        {
            _order.ChangeOrderItemStatus(item);
            return Json(true);
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