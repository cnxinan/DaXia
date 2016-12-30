using System.Web;
using System.Web.Optimization;

namespace DaXia.WebComAdmin
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                        "~/scripts/jquery-1.8.0.js",                        
                        "~/scripts/jquery.selectBox.min.js",
                        "~/scripts/easydialog/easydialog.js",
                        "~/scripts/datepicker/js/jquery-ui-datepicker.js",
                        "~/Scripts/MyScripts/fun.js",
                        "~/Scripts/MyScripts/show.js"                        
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/css/skin1/global.css",
                "~/css/skin1/right.css",
                "~/css/skin1/jquery.selectBox.css",
                "~/scripts/easydialog/easydialog.css",
                "~/scripts/datepicker/css/jquery-ui.css"
                ));
        }
    }
}