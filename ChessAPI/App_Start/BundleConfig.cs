using System.Web;
using System.Web.Optimization;

namespace ChessAPI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new Bundle("~/bundles/angular")
                    .Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-resource.js"
                    )
                );
            bundles.Add(
                new Bundle("~/bundles/core").IncludeDirectory("~/client/core", "*.js")
                );
            bundles.Add(
                new Bundle("~/bundles/models").IncludeDirectory("~/client/models", "*.js")
                );
            bundles.Add(
                new Bundle("~/bundles/controllers").IncludeDirectory("~/client/controllers", "*.js")
                );
            bundles.Add(
                new Bundle("~/bundles/services").IncludeDirectory("~/client/services", "*.js")
                );
        }
    }
}
