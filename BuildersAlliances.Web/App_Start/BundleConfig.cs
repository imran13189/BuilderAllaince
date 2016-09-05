using System.Web;
using System.Web.Optimization;

namespace BuildersAlliances.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                     "~/Scripts/js/jquery.dcjqaccordion.2.7.js",
                   "~/Scripts/js/jquery.scrollTo.min.js",
                   "~/Scripts/js/jquery.nicescroll.js",
                   "~/Scripts/BootStrapDailog/bootstrap-dialog.min.js",
                   "~/Scripts/Custom/Layout.js",
                   "~/Scripts/js/scripts.js",
                   "~/Scripts/Common.js"

                     ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap-table.js",
                     "~/Scripts/bs3/js/bootstrap.min.js",
                     "~/Scripts/js/bootstrap-datepicker/js/bootstrap-datepicker.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bs3/css/bootstrap.min.css",
                      "~/Content/css/bootstrap-reset.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/Site.css",
                      "~/Content/css/style.css",
                      "~/Content/css/style-responsive.css",
                      "~/Scripts/bootstrap-table.css",
                      "~/Scripts/BootStrapDailog/bootstrap-dialog.min.css",
                      "~/Scripts/js/bootstrap-datepicker/css/datepicker.css"));
        }
    }
}
