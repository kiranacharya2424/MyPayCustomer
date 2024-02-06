using System.Web;
using System.Web.Optimization;

namespace MyPay
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

            // MERCHANT
            #region MERCHANT

            bundles.Add(new ScriptBundle("~/bundles/committransaction").Include(
                        "~/Content/assets/MyPayPayments/js/committransaction.js"));

            bundles.Add(new ScriptBundle("~/bundles/merchantdashboard").Include(
                       "~/Content/assets/js/merchantdashboard.js"));
            #endregion

            // MYPAY USER
            #region MYPAY_USER

            bundles.Add(new ScriptBundle("~/bundles/mypayuserappcommon").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_common.js",
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_dashboard.js"));


            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_login").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_login.js"));

            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_requestlogin").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_requestlogin.js"));

             
            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_forgotpassword").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_forgotpassword.js"));


            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_registerverification").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_registerverification.js"));


            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_loadmoney").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_loadmoney.js"));


            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_sendmoney").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_sendmoney.js"));


            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_LoadWallet").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_LoadWallet.js"));

            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_Banklistall").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_Banklistall.js"));

            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_LinkedAccount").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_LinkedAccount.js"));

            bundles.Add(new ScriptBundle("~/bundles/mypayuserapp_LinkedBankTransfer").Include(
                        "~/Content/assets/MyPayUserApp/js/mypayuserapp_LinkedBankTransfer.js"));

            #endregion

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
