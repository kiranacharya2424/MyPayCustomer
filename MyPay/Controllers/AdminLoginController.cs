using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Request;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPay.Controllers
{
    public class AdminLoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Request.Url.Host.ToLower() == "customer.mypay.com.np")
            {
                return RedirectToAction("Index", "MyPayUser");
            }
            if (Request.Url.OriginalString.ToLower().Contains("mypayuser"))
            {
                return RedirectToAction("Index", "MyPayUser");
            }
            return View();
        }


        private bool RememberMeLogin()
        {
            bool IsLogin = false;
            if (Request.Cookies["AdminLogin"] != null)
            {
                if (Request.Url.Host.ToLower().IndexOf("localhost") >= 0)
                {
                    var reqCookies = Request.Cookies["AdminLogin"];
                    if (reqCookies != null)
                    {
                        string UserId = reqCookies["username"].ToString();
                        AddAdmin outobject = new AddAdmin();
                        GetAdmin inobject = new GetAdmin();
                        inobject.UserId = UserId;
                        inobject.CheckDelete = 0;
                        inobject.CheckActive = 1;
                        AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);

                        if (res != null && res.Id != 0)
                        {
                            if (res.IsActive)
                            {
                                Common.SetAdminSession(res);
                                var expiration = DateTime.Now.AddHours(1);
                                FormsAuthentication.SetAuthCookie(UserId, true);
                                var authTicket = new FormsAuthenticationTicket(
                                1,
                                Convert.ToString(UserId),
                                DateTime.Now,
                                expiration,
                                true,
                                string.Empty,
                                "/"
                                );
                                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                                Response.Cookies.Add(cookie);
                                IsLogin = true;
                            }
                        }
                    }
                }
            }
            return IsLogin;
        }

        public ActionResult UpdateData()
        {
            int hours = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Hour;
            int Minutes = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Minute;
            int Seconds = Convert.ToDateTime(Common.fnGetdatetimeFromInput(DateTime.UtcNow)).Second;

            if (hours == 23 || hours == 0)
            {
                if (Minutes > 58 || (hours == 0 && Minutes <= 2))
                {
                   // if (hours == 11 || hours == 0)
            //{
            //    if (Minutes > 13 || (hours == 0 && Minutes <= 2))
            //    {
                    Common.EODBalanceADD();
                    Common.EODBalanceMerchantADD();
                    Common.AdminLoginPasswordExpire();
                    Common.EventDetailsExpireUpdate();
                }
            }
          // Common.BulkNotificationScheduler();
          Common.CheckTransactionStatusLookup();
           Common.BulkExcelNotificationScheduler();
            Common.BulkNotificationScheduler();
            Common.ServiceActivityMonitor();

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Req_AdminLogin model, FormCollection frm)
        {
            AddAdmin objAddAdmin = new AddAdmin();
            var result = RepAdminUser.Login(model.UserId, model.Password, model.RememberMe, ref objAddAdmin);
            if (result == "success")
            {
                ViewBag.DisplayUserDetails = objAddAdmin.FirstName + " " + objAddAdmin.LastName + " (" + model.UserId + "). email: " + objAddAdmin.Email.ToLower();
                var expiration = DateTime.Now.AddHours(1);
                if (model.RememberMe)
                {
                    expiration = DateTime.Now.AddDays(5);
                }
                FormsAuthentication.SetAuthCookie(model.UserId, model.RememberMe);
                var authTicket = new FormsAuthenticationTicket(
                1,
                Convert.ToString(model.UserId),
                DateTime.Now,
                expiration,
                model.RememberMe,
                string.Empty,
                "/"
                );
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = result;
                return View(model);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RequestLogin(Req_AdminLogin model)
        {
            AddAdmin objAddAdmin = new AddAdmin();
            var result = RepAdminUser.Login(model.UserId, model.Password, model.RememberMe, ref objAddAdmin);
            if (result == "success")
            {
                var expiration = DateTime.Now.AddHours(1);
                if (model.RememberMe)
                {
                    expiration = DateTime.Now.AddDays(5);
                }
                FormsAuthentication.SetAuthCookie(model.UserId, model.RememberMe);
                var authTicket = new FormsAuthenticationTicket(
                1,
                Convert.ToString(model.UserId),
                DateTime.Now,
                expiration,
                model.RememberMe,
                string.Empty,
                "/"
                );
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
            }
            return Json(result);
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            ViewBag.Title = "This is Admin Forgot Password Page";
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(Req_AdminLogin model, FormCollection frm)
        {
            AddAdmin objAddAdmin = new AddAdmin();
            var result = RepAdminUser.GetAdminDetailsFromEmail(model.Email, ref objAddAdmin);
            if (result == "success")
            {
                ViewBag.SucceessMessage = "Email has been sent";
            }
            else
            {
                ViewBag.Message = result;
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Dashboard()
        {
            AddAdmin outobject_admin = new AddAdmin();
            GetAdmin inobject_admin = new GetAdmin();
            inobject_admin.MemberId = Convert.ToInt64(Session["AdminMemberId"]);
            AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject_admin, outobject_admin);
            if (res.IsPasswordExpired)
            {
                return RedirectToAction("/ChangePassword");
            }

           (new CommonHelpers()).AccessRolesAuthentication();
            GetTransaction inobject_Trans = new GetTransaction();
            inobject_Trans.Today = DateTime.UtcNow.ToString();
            inobject_Trans.Take = 10;
            List<AddTransaction> model = new List<AddTransaction>();
            //  model = (List<AddTransaction>)RepTransactions.GetAllTransactions(inobject_Trans);

            GetTransaction inobject_Trans1 = new GetTransaction();
            inobject_Trans1.Yesterday = DateTime.UtcNow.ToString();
            inobject_Trans1.Take = 10;
            List<AddTransaction> model_Yesterday = new List<AddTransaction>();
            //   model_Yesterday=(List<AddTransaction>)RepTransactions.GetAllTransactions(inobject_Trans1);

            GetTransaction inobject_Trans2 = new GetTransaction();
            inobject_Trans2.Take = 10;
            List<AddTransaction> objTransactionList = new List<AddTransaction>();
            //  objTransactionList = (List<AddTransaction>)RepTransactions.GetAllTransactions(inobject_Trans2);


            //List<AddTransaction> model_Transactions_This_Month = objTransactionList.Where(c => c.CreatedDate.Month == System.DateTime.UtcNow.Month).OrderByDescending(c => c.CreatedDate).Take(10).ToList();
            //List<AddTransaction> model_Transactions_This_Week = model_Transactions_This_Month.Where(c => Common.GetWeekNumberOfMonth(c.CreatedDate) == Common.GetWeekNumberOfMonth(System.DateTime.UtcNow)).OrderByDescending(c => c.CreatedDate).Take(10).ToList();



            AddTicket outobject = new AddTicket();
            GetTicket inobject = new GetTicket();
            inobject.Take = 3;
            inobject.Skip = 0;
            List<AddTicket> objList = RepCRUD<GetTicket, AddTicket>.GetRecordList(Common.StoreProcedures.sp_Tickets_Get, inobject, outobject);

            //ViewBag.RecentTransaction = model;
            //ViewBag.YesterdayTransaction = model_Yesterday;
            //ViewBag.AllsTransaction = objTransactionList;
            ViewBag.RecentTickets = objList;

            AddAdmin outobject_dashboard = new AddAdmin();
            GetAdminDashboard inobject_dashboard = new GetAdminDashboard();
            AddAdmin res_dashboard = new AddAdmin();
            //res_dashboard = RepCRUD<GetAdminDashboard, AddAdmin>.GetRecord(Models.Common.Common.StoreProcedures.sp_AdminDashboard_Get, inobject_dashboard, outobject_dashboard);
            //ViewBag.KYCApprovedBar = (res_dashboard.CompleteKycRequest/ res_dashboard.TotalKYCRequests) * 100;
            //ViewBag.KYCPendingBar = (res_dashboard.PendingKycRequest / res_dashboard.TotalKYCRequests) * 100;
            //ViewBag.Dashboard = dashboard.GetAdminDashboard();
            return View(res_dashboard);
        }

        [Authorize]
        public ActionResult DashboardHome()
        {
            AddAdmin outobject_admin = new AddAdmin();
            GetAdmin inobject_admin = new GetAdmin();
            inobject_admin.MemberId = Convert.ToInt64(Session["AdminMemberId"]);
            AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject_admin, outobject_admin);
            if (res.IsPasswordExpired)
            {
                return RedirectToAction("/ChangePassword");
            }

            return View();
        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            if (RepAdminUser.Logout() == "success")
            {
                return RedirectToAction("/Index");
            }
            else
            {
                return RedirectToAction("/Dashboard");
            }
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "This is Admin Change Password Page";
            AddAdmin outobject = new AddAdmin();
            GetAdmin inobject = new GetAdmin();
            inobject.MemberId = Convert.ToInt64(Session["AdminMemberId"]);
            AddAdmin model = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
            if (model != null && model.Id != 0)
            {
                ViewBag.AdminOldPassword = Common.DecryptString(model.Password);
            }
            else
            {
                ViewBag.AdminOldPassword = "";
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(AddAdmin vmodel, string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                ViewBag.Message = "Please enter password";
            }
            else if (Password.Length < 8)
            {
                ViewBag.Message = "Minimum Password length should be 8 characters.";
            }
            else if (Password != "")
            {
                Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                Match m = test.Match(Password);
                if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                {
                    ViewBag.Message = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";

                }
            }
            AddAdmin outobject = new AddAdmin();
            GetAdmin inobject = new GetAdmin();
            inobject.MemberId = Convert.ToInt64(Session["AdminMemberId"]);
            AddAdmin model = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);
            if (model != null && model.Id != 0)
            {
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    model.Password = (Common.EncryptString(Password));
                    model.IsPasswordExpired = false;
                    bool status = RepCRUD<AddAdmin, GetAdmin>.Update(model, "adminlogin");
                    if (status)
                    {
                        string devicecode = Request.Browser.Type;
                        Common.AddLogs("Password changed successfully for admin by(MemberId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.User, Convert.ToInt64(Session["AdminMemberId"]), Convert.ToInt64(Session["AdminMemberId"]).ToString(), false, "Web", devicecode, (int)AddLog.LogActivityEnum.Password_Reset_Admin, Common.CreatedBy, Common.CreatedByName);
                        ViewBag.SuccessMessage = "Password changed successfully.";
                        FormsAuthentication.SignOut();
                        TempData["AdminMessage"] = "Password changed successfully. Please login to continue.";
                        return RedirectToAction("/Index");
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public JsonResult BindDashboardChart()
        {
            AddDashboardChart outobject = new AddDashboardChart();
            GetDashboardChart inobject = new GetDashboardChart();
            List<AddDashboardChart> resChart = RepCRUD<GetDashboardChart, AddDashboardChart>.GetRecordList(Common.StoreProcedures.sp_DashboardChart_Get, inobject, outobject);
            if (resChart != null && resChart.Count != 0)
            {

            }

            return Json(resChart, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult BindDashboard(string Type)
        {
            AddAdmin outobject_dashboard = new AddAdmin();
            GetAdminDashboard inobject_dashboard = new GetAdminDashboard();
            inobject_dashboard.Type = Type;
            AddAdmin res_dashboard = RepCRUD<GetAdminDashboard, AddAdmin>.GetRecord(Models.Common.Common.StoreProcedures.sp_AdminDashboard_Get, inobject_dashboard, outobject_dashboard);

            return Json(res_dashboard, JsonRequestBehavior.AllowGet);
        }
    }
}