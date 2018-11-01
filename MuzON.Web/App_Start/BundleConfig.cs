using System.Web;
using System.Web.Optimization;

namespace MuzON.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/docsupport").Include(
                        "~/Content/docsupport/jquery-3.2.1.min.js",
                        "~/Scripts/chosen.jquery.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/DataTables/dataTables.bootstrap4.min.css",
                      "~/Content/DataTables/css/responsive.bootstrap4.min.css"));

            bundles.Add(new StyleBundle("~/ContentStyles/css").Include(
                    "~/Content/linearicons.css",
                    "~/Content/font-awesome.min.css",
                    "~/Content/bootstrap.css",
                    "~/Content/magnific-popup.css",
                    "~/Content/jquery-ui.css",
                    "~/Content/nice-select.css",
                    "~/Content/animate.min.css",
                    "~/Content/owl.carousel.css",
                    "~/Content/main.css",
                     "~/Content/Site.css",
                    "~/Content/DataTables/dataTables.bootstrap4.min.css",
                    "~/Content/DataTables/css/responsive.bootstrap4.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/mainJS").Include(
                      "~/Scripts/popper.min.js",
                      "~/Scripts/vendor/bootstrap.min.js",
                      "~/Scripts/easing.min.js",
                      "~/Scripts/hoverIntent.js",
                      "~/Scripts/superfish.min.js",
                      "~/Scripts/jquery.magnific-popup.min.js",
                      "~/Scripts/jquery.tabs.min.js",
                      "~/Scripts/jquery.nice-select.min.js",
                      "~/Scripts/isotope.pkgd.min.js",
                      "~/Scripts/waypoints.min.js",
                      "~/Scripts/jquery.counterup.min.js",
                      "~/Scripts/simple-skillbar.js",
                      "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/main.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap4.min.js"));
        }
    }
}
