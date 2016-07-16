using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildersAlliances.Domain;
using BuildersAlliances.Services;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        IUsers _user;
        public UserController(IUsers users)
        {
            _user = users;
        }
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(Users model)
        {
            
            return Json(_user.CreateUser(model));
        }
        [HttpPost]
        public ActionResult EditUser(Users model)
        {
  

            return View("AddUser", model);
        }

        [HttpPost]
        public ActionResult DeleteUser(int UserId)
        {
            return Json(_user.DeleteUser(UserId));
        }

        public JsonResult IsEmailExist(string Email,int UserId)
        {
            return Json(_user.IsEmailExist(Email,UserId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUsernameExist(string Username, int UserId)
      {
            return Json(_user.IsUsernameExist(Username,UserId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignRole(int UserId)
        {
            return View(_user.AssignRole(UserId));
        }

        [HttpPost]
        public ActionResult AssignRole(UserInRoleModel model)
        {
            return Json(_user.AssignRole(model));
        }
    }
}