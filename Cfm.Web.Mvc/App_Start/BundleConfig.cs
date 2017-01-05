using System.Web;
using System.Web.Optimization;

namespace Cfm.Web.Mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/pub/StyleError").Include(
               "~/Content/bootstrap.min.css"
               ));


            bundles.Add(new StyleBundle("~/pub/Login/css").Include(
                      "~/Assets/AdminTemp/css/bootstrap_V2.css"
                      ));

            bundles.Add(new ScriptBundle("~/pub/Login/js").Include(
                        "~/Assets/AdminTemp/js/jquery-1.10.2.min.js",
                        "~/Assets/AdminTemp/js/bootstrap.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"
                     ));

            bundles.Add(new StyleBundle("~/pub/Center/css").Include(
                "~/Assets/AdminTemp/css/bootstrap.css",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/daterangepicker-bs3.css",
                "~/Assets/AdminTemp/plugins/pines-notify/pnotify.css",
                "~/Assets/AdminTemp/js/jqueryui.css",
                "~/Content/style-loading.css",
                "~/Content/style.css",
                "~/Content/jquery-ui.css",
                "~/Content/jquery.tree.min.css"
                ));

            bundles.Add(new ScriptBundle("~/pub/Center/js").Include(
                    "~/Assets/AdminTemp/js/jquery-1.10.2.min.js",
                    "~/Assets/AdminTemp/js/jqueryui-1.9.2.min.js",
                    "~/Assets/AdminTemp/js/bootstrap.min.js",
                    "~/Assets/AdminTemp/plugins/jquery-slimscroll/jquery.slimscroll.js",
                    "~/Assets/AdminTemp/js/enquire.min.js",
                    "~/Assets/AdminTemp/js/application.js",
                    "~/Assets/AdminTemp/plugins/form-daterangepicker/moment.min.js",
                    "~/Assets/AdminTemp/plugins/form-daterangepicker/daterangepicker.js",
                    "~/Assets/AdminTemp/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js",
                    "~/Assets/AdminTemp/plugins/clockface/js/clockface.js",
                    "~/Assets/AdminTemp/plugins/pines-notify/pnotify.min.js",
                    "~/Assets/AdminTemp/plugins/form-inputmask/jquery.inputmask.bundle.min.js",
                    "~/Scripts/shortcut.js",
                    "~/Scripts/jquery.unobtrusive-ajax.min.js",
                    "~/Scripts/CFM/Dictionary.js",
                    "~/Scripts/CFM/Mapping.js",
                    "~/Scripts/CFM/Common.js",
                    "~/Scripts/CFM/Configuration.js",
                    "~/Scripts/CFM/System.js",
                    "~/Scripts/jquery.isloading.min.js",
                    "~/Scripts/jquery-ui.js",
                    "~/Content/jquery.tree.min.js"
                ));


            bundles.Add(new StyleBundle("~/pub/CFMCounter/css").Include(
                "~/Assets/AdminTemp/css/bootstrap.css",
                "~/Content/style.css",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/daterangepicker-bs3.css",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker3.min.css",
                "~/Assets/AdminTemp/plugins/pines-notify/pnotify.css",
                "~/Assets/AdminTemp/js/jqueryui.css",
                "~/Content/style-loading.css",
                "~/Content/jquery-ui.css",
                "~/Content/jquery.tree.min.css"
               ));

            bundles.Add(new ScriptBundle("~/pub/CFMCounter/js").Include(
                "~/Assets/AdminTemp/js/jquery-1.10.2.min.js",
                "~/Assets/AdminTemp/js/jqueryui-1.9.2.min.js",
                "~/Assets/AdminTemp/js/bootstrap.min.js",
                "~/Assets/AdminTemp/plugins/jquery-slimscroll/jquery.slimscroll.js",
                "~/Assets/AdminTemp/js/enquire.min.js",
                "~/Assets/AdminTemp/js/application.js",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/moment.min.js",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/daterangepicker.js",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker.min.js",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker.vi.js",
                "~/Assets/AdminTemp/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js",
                "~/Assets/AdminTemp/plugins/clockface/js/clockface.js",
                "~/Assets/AdminTemp/plugins/pines-notify/pnotify.min.js",
                "~/Assets/AdminTemp/plugins/form-inputmask/jquery.inputmask.bundle.min.js",
                "~/Scripts/shortcut.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/CFM/Dictionary.js",
                "~/Scripts/CFM/Mapping.js",
                "~/Scripts/CFM/Common.js",
                "~/Scripts/CFM/Configuration.js",
                "~/Scripts/CFM/filter.js",
                "~/Scripts/CFM/Accounting.js",
                "~/Scripts/CFM/AccountingEntry.js",
                "~/Scripts/CFM/ReportCD.js",
                "~/Scripts/CFM/Report.js",
                "~/Scripts/CFM/Report04CDProcess.js",
                "~/Scripts/CFM/System.js",
                "~/Scripts/CFM/ReportTQ.js",
                "~/Scripts/CFM/DashBoard.js",
                "~/Scripts/jquery.isloading.min.js",
                "~/Scripts/jquery-ui.js",
                "~/Content/jquery.tree.min.js"
              ));


            bundles.Add(new StyleBundle("~/pub/CFMDistrict/css").Include(
               "~/Assets/AdminTemp/css/bootstrap.css",
               "~/Assets/AdminTemp/plugins/form-daterangepicker/daterangepicker-bs3.css",
               "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker3.min.css",
               "~/Assets/AdminTemp/plugins/pines-notify/pnotify.css",
               "~/Assets/AdminTemp/js/jqueryui.css",
               "~/Content/style-loading.css",
               "~/Content/style.css",
               "~/Content/jquery-ui.css",
                "~/Content/jquery.tree.min.css"
            ));


            bundles.Add(new ScriptBundle("~/pub/CFMDistrict/js").Include(
                "~/Assets/AdminTemp/js/jquery-1.10.2.min.js",
                "~/Assets/AdminTemp/js/jqueryui-1.9.2.min.js",
                "~/Assets/AdminTemp/js/bootstrap.min.js",
                "~/Assets/AdminTemp/plugins/jquery-slimscroll/jquery.slimscroll.js",
                "~/Assets/AdminTemp/js/enquire.min.js",
                "~/Assets/AdminTemp/js/application.js",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/moment.min.js",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/daterangepicker.js",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker.min.js",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker.vi.js",
                "~/Assets/AdminTemp/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js",
                "~/Assets/AdminTemp/plugins/clockface/js/clockface.js",
                "~/Assets/AdminTemp/plugins/pines-notify/pnotify.min.js",
                "~/Assets/AdminTemp/plugins/form-inputmask/jquery.inputmask.bundle.min.js",
                "~/Scripts/shortcut.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/CFM/Dictionary.js",
                "~/Scripts/CFM/Mapping.js",
                "~/Scripts/CFM/Common.js",
                "~/Scripts/CFM/Configuration.js",
                "~/Scripts/CFM/filter.js",
                "~/Scripts/CFM/System.js",
                "~/Scripts/CFM/Accounting.js",
                "~/Scripts/CFM/AccountingEntry.js",
                "~/Scripts/CFM/ReportCD.js",
                "~/Scripts/CFM/Report.js",
                "~/Scripts/CFM/Report04CDProcess.js",
                "~/Scripts/jquery.isloading.min.js",
                "~/Scripts/CFM/ReportTQ.js",
                "~/Scripts/jquery-ui.js",
                "~/Content/jquery.tree.min.js"
              ));


            bundles.Add(new StyleBundle("~/pub/Branch/css").Include(
                "~/Assets/AdminTemp/css/bootstrap.css",
                "~/Assets/AdminTemp/plugins/form-daterangepicker/daterangepicker-bs3.css",
                         "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker3.min.css",
                "~/Assets/AdminTemp/plugins/pines-notify/pnotify.css",
                "~/Assets/AdminTemp/js/jqueryui.css",
                "~/Content/style-loading.css",
                "~/Content/style.css",
                "~/Content/jquery-ui.css",
                "~/Content/jquery.tree.min.css"
            ));

            bundles.Add(new ScriptBundle("~/pub/Branch/js").Include(
                    "~/Assets/AdminTemp/js/jquery-1.10.2.min.js",
                    "~/Assets/AdminTemp/js/jqueryui-1.9.2.min.js",
                    "~/Assets/AdminTemp/js/bootstrap.min.js",
                    "~/Assets/AdminTemp/plugins/jquery-slimscroll/jquery.slimscroll.js",
                    "~/Assets/AdminTemp/js/enquire.min.js",
                    "~/Assets/AdminTemp/js/application.js",
                    "~/Assets/AdminTemp/plugins/form-daterangepicker/moment.min.js",
                    "~/Assets/AdminTemp/plugins/form-daterangepicker/daterangepicker.js",
                    "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker.min.js",
                    "~/Assets/AdminTemp/plugins/form-daterangepicker/bootstrap-datepicker.vi.js",
                    "~/Assets/AdminTemp/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js",
                    "~/Assets/AdminTemp/plugins/clockface/js/clockface.js",
                    "~/Assets/AdminTemp/plugins/pines-notify/pnotify.min.js",
                    "~/Assets/AdminTemp/plugins/form-inputmask/jquery.inputmask.bundle.min.js",
                    "~/Scripts/shortcut.js",
                    "~/Scripts/jquery.unobtrusive-ajax.min.js",
                    "~/Scripts/CFM/Accounting.js",
                    "~/Scripts/CFM/AccountingEntry.js",
                    "~/Scripts/CFM/filter.js",
                    "~/Scripts/CFM/Dictionary.js",
                    "~/Scripts/CFM/Mapping.js",
                    "~/Scripts/CFM/Common.js",
                    "~/Scripts/CFM/Configuration.js",
                    "~/Scripts/CFM/ReportCD.js",
                    "~/Scripts/CFM/Report.js",
                    "~/Scripts/CFM/System.js",
                    "~/Scripts/CFM/Report04CDProcess.js",
                    "~/Scripts/jquery.isloading.min.js",
                    "~/Scripts/jquery-ui.js",
                    "~/Content/jquery.tree.min.js"
             ));
        }
    }
}
