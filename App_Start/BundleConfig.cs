using System.Web;
using System.Web.Optimization;

namespace MAYShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/Content/Myjs").Include(
                        "~/Content/js/bootstrap.min.js",
                        "~/Content/js/jquery-3.3.1.min.js",
                        "~/Content/js/jquery.countdown.min.js",
                        "~/Content/js/jquery.magnific-popup.min.js",
                        "~/Content/js/jquery.nice-select.min.js",
                        "~/Content/js/jquery.nicescroll.min.js",
                        "~/Content/js/jquery.slicknav.js",
                        "~/Content/js/main.js",
                        "~/Content/js/mixitup.min.js",
                        "~/Content/js/owl.carousel.min.js"
                        ));
            bundles.Add(new StyleBundle("~/Content/Mycss").Include(
                        "~/Content/css/bootstrap.min.css",
                        "~/Content/css/elegant-icons.css",
                        "~/Content/css/font-awesome.min.css",
                        "~/Content/css/magnific-popup.css",
                        "~/Content/css/nice-select.css",
                        "~/Content/css/owl.carousel.min.css",
                        "~/Content/css/slicknav.min.css",
                        "~/Content/css/style.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/Mysass").Include(
                        "~/Content/sass/_about.scss",
                        "~/Content/sass/_base.scss",
                        "~/Content/sass/_blog-details.scss",
                        "~/Content/sass/_blog-sidebar.scss",
                        "~/Content/sass/_blog.scss",
                        "~/Content/sass/_breadcrumb.scss",
                        "~/Content/sass/_contact.scss",
                        "~/Content/sass/_checkout.scss",
                        "~/Content/sass/_footer.scss",
                        "~/Content/sass/_header.scss",
                        "~/Content/sass/_hero.scss",
                        "~/Content/sass/_home-page.scss",
                        "~/Content/sass/_mixins.scss",
                        "~/Content/sass/_product.scss",
                        "~/Content/sass/_responsive.scss",
                        "~/Content/sass/_shop-detaiks.scss",
                        "~/Content/sass/_shop.scss",
                        "~/Content/sass/_shopping-cảt.scss",
                        "~/Content/sass/_vảiable.scss",
                        "~/Content/sass/_style.scss"
                ));
        }
    }
}
