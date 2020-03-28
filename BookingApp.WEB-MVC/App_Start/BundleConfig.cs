using System.Web;
using System.Web.Optimization;

namespace BookingApp.WEB_MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scipts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/knockout-{version}.js")
                        );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/Scripts/bootstrap-datepicker.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/styles.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/datepicker.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/car").Include(
                      "~/Content/car.css"
                      ));
        }
    }
}
