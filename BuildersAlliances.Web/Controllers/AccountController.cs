using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildersAlliances.Domain;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.CustomModel;
using System.Web.Security;
using BuildersAlliances.Common;

namespace BuildersAlliances.Web.Controllers
{
    public class AccountController : Controller
    {
        IUsers _user;
        public AccountController(IUsers user)
        {
            _user = user;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Login()
        //{
        //    return View();

        //}


        public ActionResult Login()
        {
            LoginModel model = new LoginModel()
            {
                Email = "Admin",
                Password = "123"
            };
            if (ModelState.IsValid)
            {
                Users data = _user.Authenticate(model.Email, model.Password);
                if (data != null)
                {
                    SessionManager.FillSession(data.UserId, data.Email, data.Name, data.UserInRole.Select(x => x.RoleId).ToArray());
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                   1, // Ticket version
                   model.Email, // Username associated with ticket
                   DateTime.Now, // Date/time issued
                   DateTime.Now.AddMinutes(60), // Date/time to expire
                   true, // "true" for a persistent user cookie
                   model.Email // User-data, in this case the roles
                  );

                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    Session["TokenID"] = ticket;


                    return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                }
                else
                {
                    ViewBag.Message = "User doesn't exist";
                    return View(model);
                }
            }
            else
            {
                ViewBag.Message = "User doesn't exist";
                return View(model);
            }

        }

        [HttpPost]
        //public ActionResult Login(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Users data = _user.Authenticate(model.Email, model.Password);
        //        if (data != null)
        //        {
        //            SessionManager.FillSession(data.UserId, data.Email, data.Name, data.UserInRole.Select(x => x.RoleId).ToArray());
        //            FormsAuthentication.SetAuthCookie(model.Email, true);
        //            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
        //           1, // Ticket version
        //           model.Email, // Username associated with ticket
        //           DateTime.Now, // Date/time issued
        //           DateTime.Now.AddMinutes(60), // Date/time to expire
        //           true, // "true" for a persistent user cookie
        //           model.Email // User-data, in this case the roles
        //          );

        //            string hash = FormsAuthentication.Encrypt(ticket);
        //            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
        //            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
        //            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        //            Session["TokenID"] = ticket;


        //            return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
        //        }
        //        else
        //        {
        //            ViewBag.Message = "User doesn't exist";
        //            return View(model);
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "User doesn't exist";
        //        return View(model);
        //    }

        //}

        public ActionResult LogOut()
        {
            //added by Rakesh Pathak on 4/11/2015
            FormsAuthentication.SignOut();
            Session.Abandon();
            var FormsCookie = new HttpCookie("LoginCookie");
            //FormsCookie = Request.Cookies["LoginCookie"];
            FormsCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(FormsCookie);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
           // _user.Register(model);
            return View();
        }

        public ActionResult LockScreen()
        {
            //added by Rakesh Pathak on 4/11/2015
            FormsAuthentication.SignOut();
            Session.Abandon();
            var FormsCookie = new HttpCookie("LoginCookie");
            //FormsCookie = Request.Cookies["LoginCookie"];
            FormsCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(FormsCookie);
            return View();
        }
    }
}