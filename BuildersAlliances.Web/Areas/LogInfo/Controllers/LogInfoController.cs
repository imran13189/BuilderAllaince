using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildersAlliances.Services.Interfaces;
namespace BuildersAlliances.Web.Areas.LogInfo.Controllers
{
    public class LogInfoController : Controller
    {
        ILoginfo _logInfo { get; set; }  
       public LogInfoController(ILoginfo logInfo)
        {
            _logInfo = logInfo;
        }
        // GET: LogInfo/LogInfo
        public ActionResult Index()
        {
            return View();
        }
    }
}