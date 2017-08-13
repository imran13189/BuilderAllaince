using System.Web.Mvc;

namespace BuildersAlliances.Web.Areas.Qoute
{
    public class QouteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Qoute";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Qoute_default",
                "Qoute/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}