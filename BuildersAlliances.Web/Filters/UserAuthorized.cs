using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Routing;

namespace BuildersAlliances.Web.Filters
{
    public class UserAuthorized : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (BuildersAlliances.Common.SessionManager.LoggedInUser.RoleId==null)
                {
                    if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    {
                        HttpContext.Current.Response.Redirect("~/Account/Login");
                    }
                }
            }
            catch
            {

            }
            
        }
    }
}