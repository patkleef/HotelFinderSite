using System.Web.Optimization;

namespace Site
{
    public class BundleConfig
    {

        /// <summary>
        /// Register bundles
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-2.1.3.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/knockout-3.3.0.js",
                "~/Scripts/require.js",
                "~/Scripts/modernizr-2.8.3.js",
                "~/Scripts/jquery.nouislider.js",
                "~/Scripts/jquery.liblink.js",
                "~/Scripts/App/init.js"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/jquery.nouislider.css")
                .Include("~/Content/jcarousel.css")
                .Include("~/Content/style.css"));
        }
    }
}