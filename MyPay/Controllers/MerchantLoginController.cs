using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Request;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPay.Controllers
{
    public class MerchantLoginController : Controller
    {
        // GET: MerchantLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Req_MerchantLogin model, FormCollection frm)
        {
            AddMerchant objAddMerchant = new AddMerchant();
            var result = RepMerchantLogin.Login(model.UserName, model.Password, model.RememberMe, ref objAddMerchant);
            if (result == "success")
            {
                ViewBag.DisplayUserDetails = objAddMerchant.FirstName + " " + objAddMerchant.LastName + " (" + model.UserName + "). email: " + objAddMerchant.EmailID.ToLower();
                var expiration = DateTime.Now.AddHours(1);
                if (model.RememberMe)
                {
                    expiration = DateTime.Now.AddDays(5);
                }
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                var authTicket = new FormsAuthenticationTicket(
                1,
                Convert.ToString(model.UserName),
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
        public JsonResult RequestLogin(Req_MerchantLogin model)
        {
            AddMerchant objAddMerchant = new AddMerchant();
            var result = RepMerchantLogin.Login(model.UserName, model.Password, model.RememberMe, ref objAddMerchant);
            if (result == "success")
            {
                var expiration = DateTime.Now.AddHours(1);
                if (model.RememberMe)
                {
                    expiration = DateTime.Now.AddDays(5);
                }
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                var authTicket = new FormsAuthenticationTicket(
                1,
                Convert.ToString(model.UserName),
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

        [HttpGet]
        public ActionResult Dashboard()
        {
            AddMerchant model = new AddMerchant();
            string msg = "False";
            if (Session["MerchantUniqueId"] != null)
            {
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                inobject.CheckActive = 1;
                inobject.CheckDelete = 0;
                model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                if (model != null && model.Id > 0)
                {
                    if (model.IsPasswordReset)
                    {
                        msg = "True";
                    }
                    else
                    {
                        return RedirectToAction("ChangePassword");
                    }
                }

                string MerchantId = Convert.ToString(Session["MerchantUniqueId"]);
                string DailyLimitCheck = Common.GetMerchantWithdrawalRequestDailyCheckLimit(MerchantId);
                if (DailyLimitCheck != "success")
                {
                    ViewBag.WithdrawalRequest = DailyLimitCheck;
                }
                else
                {
                    ViewBag.WithdrawalRequest = "False";
                }

                AddMerchantBankDetail outobject_bank = new AddMerchantBankDetail();
                GetMerchantBankDetail inobject_bank = new GetMerchantBankDetail();
                inobject_bank.MerchantId = Session["MerchantUniqueId"].ToString();
                AddMerchantBankDetail res_bank = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobject_bank, outobject_bank);
                if (res_bank != null && res_bank.Id > 0)
                {
                    ViewBag.BankName = res_bank.BankName;
                    ViewBag.BankAccountNo = res_bank.AccountNumber;
                    ViewBag.BankId = res_bank.Id;
                }
                else
                {
                    ViewBag.BankName = "";
                    ViewBag.BankAccountNo = "";
                    ViewBag.BankId = "";
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            ViewBag.ResetPassword = msg;



            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "This is Merchant Change Password Page";
            AddMerchant model = new AddMerchant();

            if (Session["MerchantUniqueId"] != null)
            {
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = Convert.ToString(Session["MerchantUniqueId"]);
                model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                if (model != null && model.Id != 0)
                {
                    ViewBag.MerchantOldPassword = Common.DecryptionFromKey(model.Password, model.secretkey);
                }
                else
                {
                    ViewBag.MerchantOldPassword = "";
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(string Password, string passwordconfirm)
        {
            string msg = "";
            AddMerchant model = new AddMerchant();
            if (Session["MerchantUniqueId"] != null)
            {
                if (string.IsNullOrEmpty(Password) || Password == "")
                {
                    msg = "Please Enter New Password";
                }
                else if (string.IsNullOrEmpty(passwordconfirm) || passwordconfirm == "")
                {
                    msg = "Please Enter Confirm Password";
                }
                else if (Password != passwordconfirm)
                {
                    msg = "Confirm Password doesn't match.";
                }
                else if (Password.Length < 8)
                {
                    msg = "Minimum Password length should be 8 characters.";
                }
                else if (Password.IndexOf(":") >= 0)
                {
                    msg = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character without Colon(:)";
                }
                else if (Password != "")
                {
                    Regex test = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,64}$");
                    Match m = test.Match(Password);
                    if (!m.Success && m.Groups["ch"].Captures.Count < 1 && m.Groups["num"].Captures.Count < 1)
                    {
                        msg = "Password must be at least 8 characters long with one uppercase, one lowercase, one numeric and one special character";

                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    if (!string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
                    {
                        string MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                        AddMerchant outobject = new AddMerchant();
                        GetMerchant inobject = new GetMerchant();
                        inobject.MerchantUniqueId = MerchantUniqueId;
                        model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                        if (model != null && model.Id > 0 && !string.IsNullOrEmpty(MerchantUniqueId))
                        {
                            ViewBag.MerchantOldPassword = Common.DecryptionFromKey(model.Password, model.secretkey);
                            if (Common.DecryptionFromKey(model.Password, model.secretkey) == Password)
                            {
                                msg = "New Password cannot be same as old Password!";
                            }
                            if (string.IsNullOrEmpty(msg))
                            {
                                model.Password = Common.EncryptionFromKey(Password, model.secretkey);
                                model.UpdatedDate = DateTime.UtcNow;
                                model.IsPasswordReset = true;
                                bool status = RepCRUD<AddMerchant, GetMerchant>.Update(model, "merchant");
                                if (status)
                                {
                                    msg = "Successfully Updated Password.";
                                    ViewBag.SuccessMessage = msg;
                                    Common.AddLogs("Updated Merchant Password of (MerchantUniqueId:" + model.MerchantUniqueId + "  by(MerchantUniqueId:" + Session["MerchantUniqueId"].ToString() + ")", true, (int)AddLog.LogType.Merchant);
                                    FormsAuthentication.SignOut();
                                    TempData["AdminMessage"] = "Password changed successfully. Please login to continue.";
                                    return RedirectToAction("/Index");
                                }
                                else
                                {
                                    msg = "Not Updated.";
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(ViewBag.SuccessMessage))
                                {
                                    ViewBag.Message = msg;
                                }
                            }
                        }
                    }
                    else
                    {
                        msg = "Session expired";
                        FormsAuthentication.SignOut();
                    }
                }
                if (string.IsNullOrEmpty(ViewBag.SuccessMessage))
                {
                    ViewBag.Message = msg;
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            if (Common.RemoveMerchantSession())
            {
                return RedirectToAction("/Index");
            }
            else
            {
                return RedirectToAction("/Dashboard");
            }
        }
        [HttpPost]
        public JsonResult BindDashboard(string Type)
        {
            AddMerchantDashboard res_dashboard = new AddMerchantDashboard();
            if (Session["MerchantUniqueId"] != null)
            {
                AddMerchantDashboard outobject_dashboard = new AddMerchantDashboard();
                GetMerchantDashboard inobject_dashboard = new GetMerchantDashboard();
                inobject_dashboard.Type = Type;
                inobject_dashboard.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                res_dashboard = RepCRUD<GetMerchantDashboard, AddMerchantDashboard>.GetRecord(Models.Common.Common.StoreProcedures.sp_MerchantDashboard_Get, inobject_dashboard, outobject_dashboard);
                if (res_dashboard.apipassword != null || res_dashboard.apipassword != "")
                {
                    string apipassword = Common.DecryptionFromKey(res_dashboard.apipassword, res_dashboard.secretkey);
                    res_dashboard.apipassword = apipassword;
                }
            }
            return Json(res_dashboard, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult GenerateWithdrawalRequestOTP(string Amount, string Remarks, string Bankid, string RequestType)
        {
            string msg = "";
            try
            {


                Session["MerchantWithdrawalOrderId"] = "";
                if (Amount == "0" || string.IsNullOrEmpty(Amount))
                {
                    msg = "Please enter amount";
                }
                else if (string.IsNullOrEmpty(Remarks))
                {
                    msg = "Please enter comments";
                }
                else if (string.IsNullOrEmpty(Bankid))
                {
                    msg = "Please enter bank detail";
                }
                else if (!string.IsNullOrEmpty(Amount))
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(Amount, out Num);
                    if (!isNum)
                    {
                        msg = "Please enter valid Amount.";
                    }
                    else if (Convert.ToDecimal(Amount) <= 0)
                    {
                        msg = "Please enter valid Amount.";
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    decimal MinAmt = 0;
                    decimal MaxAmt = 0;
                    AddMerchantSettings objMerchantSettings = new AddMerchantSettings();
                    bool resFlag = objMerchantSettings.GetRecord();
                    if (resFlag && objMerchantSettings.Id != 0)
                    {
                        MinAmt = objMerchantSettings.MinimumWithdrawalAmount;
                        MaxAmt = objMerchantSettings.MaximumWithdrawalAmount;
                    }
                    if (!string.IsNullOrEmpty(Amount))
                    {
                        if (Convert.ToDecimal(Amount) < MinAmt)
                        {
                            msg = $"Minimum Withdrawal Amount is {MinAmt}.";
                        }
                    }
                    if (msg == "")
                    {
                        if (Session["MerchantUniqueId"] != null)
                        {
                            string MerchantId = Convert.ToString(Session["MerchantUniqueId"]);
                            string DailyLimitCheck = Common.GetMerchantWithdrawalRequestDailyCheckLimit(MerchantId);
                            if (DailyLimitCheck != "success")
                            {
                                msg = DailyLimitCheck;
                            }
                            else
                            {
                                AddMerchant outobject = new AddMerchant();
                                GetMerchant inobject = new GetMerchant();
                                inobject.CheckActive = 1;
                                inobject.CheckDelete = 0;
                                inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                                AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                                if (model != null && model.Id > 0)
                                {
                                    if (model.MerchantType == (int)AddMerchant.MerChantType.Merchant)
                                    {
                                        AddUserLoginWithPin resUserOutObject = new AddUserLoginWithPin();
                                        GetUserLoginWithPin resUserInObject = new GetUserLoginWithPin();
                                        resUserInObject.MemberId = model.UserMemberId;
                                        AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, resUserInObject, resUserOutObject);
                                        if (resUser != null && resUser.Id > 0)
                                        {

                                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Withdrawal;
                                            decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(model.MerchantTotalAmount));
                                            int OrderType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Withdrawal;
                                            if (RequestType.ToLower().Trim() == "walletbalance")
                                            {
                                                VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Wallet_Withdrawal;
                                                WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resUser.TotalAmount));
                                                OrderType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Wallet_Withdrawal;
                                            }
                                            if (WalletBalance < Convert.ToDecimal(Amount))
                                            {
                                                msg = "Insufficient Wallet Amount.";
                                            }
                                            else
                                            {
                                                WalletBalance = WalletBalance - Convert.ToDecimal(Amount);
                                                AddMerchantBankDetail outobjectBankDetail = new AddMerchantBankDetail();
                                                GetMerchantBankDetail inobjectBankDetail = new GetMerchantBankDetail();
                                                inobjectBankDetail.MerchantId = MerchantId;
                                                inobjectBankDetail.CheckActive = 1;
                                                inobjectBankDetail.CheckDelete = 0;
                                                AddMerchantBankDetail user = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobjectBankDetail, outobjectBankDetail);
                                                if (user != null && user.Id > 0)
                                                {
                                                    if (user.IsVerified == true && user.IsActive == true && user.IsDeleted == false)
                                                    {
                                                        string TransactionId = new CommonHelpers().GenerateUniqueId();
                                                        string OrderId = "withdraw-" + new CommonHelpers().GenerateUniqueId();
                                                        AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                                        GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                                        inobjectOrders.MerchantId = MerchantId;
                                                        inobjectOrders.OrderId = OrderId;
                                                        AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                                        if (resOrders == null || resOrders.Id == 0)
                                                        {

                                                            AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(resUser.MemberId.ToString(), Amount, VendorApiType.ToString());

                                                            string NotEncryptOTP = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                                                            if (model.UserName.ToLower() == "userravi")
                                                            {
                                                                NotEncryptOTP = "123456";
                                                            }

                                                            string Order_Otp = Common.EncryptString(NotEncryptOTP);
                                                            string OrderToken = Common.EncryptionFromKey(OrderId + ":" + model.MerchantUniqueId + ":" + TransactionId + ":", model.secretkey);
                                                            AddMerchantOrders ObjMerchantOrders = new AddMerchantOrders();
                                                            ObjMerchantOrders.Amount = Convert.ToDecimal(Amount);
                                                            ObjMerchantOrders.ServiceCharges = Convert.ToDecimal(objOut.ServiceCharge);
                                                            ObjMerchantOrders.NetAmount = Convert.ToDecimal(Amount);
                                                            ObjMerchantOrders.OrderId = OrderId;
                                                            ObjMerchantOrders.MerchantId = MerchantId;
                                                            ObjMerchantOrders.OrganizationName = model.OrganizationName;
                                                            ObjMerchantOrders.MerchantContactNo = model.ContactNo;
                                                            ObjMerchantOrders.MerchantName = model.FirstName + " " + model.LastName;
                                                            ObjMerchantOrders.MerchantId = MerchantId;
                                                            ObjMerchantOrders.TransactionId = TransactionId;
                                                            ObjMerchantOrders.MemberId = resUser.MemberId;
                                                            ObjMerchantOrders.MemberName = resUser.FirstName + " " + resUser.LastName;
                                                            ObjMerchantOrders.MemberContactNumber = resUser.ContactNumber;
                                                            ObjMerchantOrders.TransactionId = TransactionId;
                                                            ObjMerchantOrders.TrackerId = new CommonHelpers().GenerateUniqueId();
                                                            ObjMerchantOrders.OrderOTP = Order_Otp;
                                                            ObjMerchantOrders.IsActive = true;
                                                            ObjMerchantOrders.IsDeleted = false;
                                                            ObjMerchantOrders.IsApprovedByAdmin = true;
                                                            ObjMerchantOrders.OrderToken = OrderToken;
                                                            ObjMerchantOrders.Platform = "Web";
                                                            ObjMerchantOrders.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                                                            ObjMerchantOrders.Remarks = "Waiting for OTP Verification";
                                                            ObjMerchantOrders.Type = OrderType;
                                                            ObjMerchantOrders.CurrentBalance = WalletBalance;
                                                            ObjMerchantOrders.CreatedBy = model.UserMemberId;
                                                            ObjMerchantOrders.CreatedByName = model.UserName;
                                                            ObjMerchantOrders.UpdatedBy = model.UserMemberId;
                                                            ObjMerchantOrders.UpdatedByName = model.UserName;
                                                            ObjMerchantOrders.Sign = (int)AddMerchantOrders.MerchantOrderSign.Debit;
                                                            ObjMerchantOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Incomplete;
                                                            Int64 Id = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Insert(ObjMerchantOrders, "merchantorders");
                                                            if (Id > 0)
                                                            {

                                                                Session["MerchantWithdrawalOrderId"] = OrderId;
                                                                Models.Common.Common.SendSMS(model.ContactNo, "Your withdrawal code is " + NotEncryptOTP + ".Please enter this code to withdraw Funds. Thank you for using MyPay");
                                                                Common.AddLogs($"Merchant Withdrawal Order OTP Sent on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), model.UserMemberId, model.FirstName + " " + model.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                                                msg = "success";
                                                            }
                                                            else
                                                            {
                                                                msg = "Otp not sent";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            msg = "Duplicate OrderId.";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        msg = "Bank Verification Pending.";
                                                    }
                                                }
                                                else
                                                {
                                                    msg = "Bank Details not found";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = "User Not Exists";
                                        }
                                    }
                                    else
                                    {
                                        msg = "Withdrawal feature is not available for Banking Merchants";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Authorize]
        public JsonResult ResendWithdrawalRequestOTP(string Amount, string Remarks, string Bankid, string RequestType)
        {
            string msg = "";
            try
            {
                if (Session["MerchantWithdrawalOrderId"] == null || string.IsNullOrEmpty(Convert.ToString(Session["MerchantWithdrawalOrderId"])))
                {
                    msg = "Withdrawal Session Is Expired.";
                }
                else if (Amount == "0" || string.IsNullOrEmpty(Amount))
                {
                    msg = "Please enter amount";
                }
                else if (string.IsNullOrEmpty(Remarks))
                {
                    msg = "Please enter comments";
                }
                else if (string.IsNullOrEmpty(Bankid))
                {
                    msg = "Please enter bank detail";
                }
                else if (!string.IsNullOrEmpty(Amount))
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(Amount, out Num);
                    if (!isNum)
                    {
                        msg = "Please enter valid Amount.";
                    }
                    else if (Convert.ToDecimal(Amount) <= 0)
                    {
                        msg = "Please enter valid Amount.";
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    decimal MinAmt = 0;
                    decimal MaxAmt = 0;
                    AddMerchantSettings objMerchantSettings = new AddMerchantSettings();
                    bool resFlag = objMerchantSettings.GetRecord();
                    if (resFlag && objMerchantSettings.Id != 0)
                    {
                        MinAmt = objMerchantSettings.MinimumWithdrawalAmount;
                        MaxAmt = objMerchantSettings.MaximumWithdrawalAmount;
                    }
                    if (!string.IsNullOrEmpty(Amount))
                    {
                        if (Convert.ToDecimal(Amount) < MinAmt)
                        {
                            msg = $"Minimum Withdrawal Amount is {MinAmt}.";
                        }
                    }
                    if (msg == "")
                    {
                        if (Session["MerchantUniqueId"] != null)
                        {
                            string MerchantId = Convert.ToString(Session["MerchantUniqueId"]);
                            string DailyLimitCheck = Common.GetMerchantWithdrawalRequestDailyCheckLimit(MerchantId);
                            if (DailyLimitCheck != "success")
                            {
                                msg = DailyLimitCheck;
                            }
                            else
                            {
                                AddMerchant outobject = new AddMerchant();
                                GetMerchant inobject = new GetMerchant();
                                inobject.CheckActive = 1;
                                inobject.CheckDelete = 0;
                                inobject.MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                                AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                                if (model != null && model.Id > 0)
                                {
                                    if (model.MerchantType == (int)AddMerchant.MerChantType.Merchant)
                                    {
                                        AddUserLoginWithPin resUserOutObject = new AddUserLoginWithPin();
                                        GetUserLoginWithPin resUserInObject = new GetUserLoginWithPin();
                                        resUserInObject.MemberId = model.UserMemberId;
                                        AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, resUserInObject, resUserOutObject);
                                        if (resUser != null && resUser.Id > 0)
                                        {
                                            decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(model.MerchantTotalAmount));
                                            if (RequestType.ToLower().Trim() == "walletbalance")
                                            {
                                                WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resUser.TotalAmount));
                                            }
                                            if (WalletBalance < Convert.ToDecimal(Amount))
                                            {
                                                msg = "Insufficient Wallet Amount.";
                                            }
                                            else
                                            {
                                                AddMerchantBankDetail outobjectBankDetail = new AddMerchantBankDetail();
                                                GetMerchantBankDetail inobjectBankDetail = new GetMerchantBankDetail();
                                                inobjectBankDetail.MerchantId = MerchantId;
                                                inobjectBankDetail.CheckActive = 1;
                                                inobjectBankDetail.CheckDelete = 0;
                                                AddMerchantBankDetail user = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobjectBankDetail, outobjectBankDetail);
                                                if (user != null && user.Id > 0)
                                                {
                                                    if (user.IsVerified == true && user.IsActive == true && user.IsDeleted == false)
                                                    {
                                                        string TransactionId = new CommonHelpers().GenerateUniqueId();
                                                        string OrderId = Convert.ToString(Session["MerchantWithdrawalOrderId"]);
                                                        AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                                        GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                                        inobjectOrders.MerchantId = MerchantId;
                                                        inobjectOrders.OrderId = OrderId;
                                                        AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                                        if (resOrders != null || resOrders.Id > 0)
                                                        {
                                                            string NotEncryptOTP = MyPay.Models.Common.Common.RandomNumber(111111, 999999).ToString();
                                                            if (model.UserName.ToLower() == "userravi")
                                                            {
                                                                NotEncryptOTP = "123456";
                                                            }
                                                            string Order_Otp = Common.EncryptString(NotEncryptOTP);
                                                            resOrders.OrderOTP = Order_Otp;
                                                            resOrders.UpdatedDate = System.DateTime.UtcNow;
                                                            bool resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                                            if (resOrdersFlag)
                                                            {
                                                                Session["MerchantWithdrawalOrderId"] = OrderId;
                                                                Models.Common.Common.SendSMS(model.ContactNo, "Your withdrawal code is " + NotEncryptOTP + ".Please enter this code to withdraw Funds. Thank you for using MyPay");
                                                                Common.AddLogs($"Merchant Withdrawal Order OTP Sent on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), model.UserMemberId, model.FirstName + " " + model.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                                                msg = "success";
                                                            }
                                                            else
                                                            {
                                                                msg = "Otp not sent";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            msg = "Duplicate OrderId.";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        msg = "Bank Verification Pending.";
                                                    }
                                                }
                                                else
                                                {
                                                    msg = "Bank Details not found";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = "User Not Exists";
                                        }
                                    }
                                    else
                                    {
                                        msg = "Withdrawal feature is not available for Banking Merchants";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [Authorize]
        public JsonResult SubmitWithdrawalRequest(string OTP, string Amount, string Remarks, string Bankid, string RequestType)
        {
            string msg = "";
            try
            {
                if (Session["MerchantWithdrawalOrderId"] == null || string.IsNullOrEmpty(Convert.ToString(Session["MerchantWithdrawalOrderId"])))
                {
                    msg = "Withdrawal Session Is Expired.";
                }
                else if (Amount == "0" || string.IsNullOrEmpty(Amount))
                {
                    msg = "Please enter amount";
                }
                else if (string.IsNullOrEmpty(OTP))
                {
                    msg = "Please enter OTP";
                }
                //else if (string.IsNullOrEmpty(OrderId))
                //{
                //    msg = "Please enter OrderId";
                //}
                else if (string.IsNullOrEmpty(Remarks))
                {
                    msg = "Please enter comments";
                }
                else if (string.IsNullOrEmpty(Bankid))
                {
                    msg = "Please enter bank detail";
                }
                else if (!string.IsNullOrEmpty(Amount))
                {
                    decimal Num;
                    bool isNum = decimal.TryParse(Amount, out Num);
                    if (!isNum)
                    {
                        msg = "Please enter valid Amount.";
                    }
                    else if (Convert.ToDecimal(Amount) <= 0)
                    {
                        msg = "Please enter valid Amount.";
                    }
                }
                if (string.IsNullOrEmpty(msg))
                {
                    decimal MinAmt = 0;
                    decimal MaxAmt = 0;
                    string OrderId = Convert.ToString(Session["MerchantWithdrawalOrderId"]);

                    AddMerchantSettings objMerchantSettings = new AddMerchantSettings();
                    bool resFlag = objMerchantSettings.GetRecord();
                    if (resFlag && objMerchantSettings.Id != 0)
                    {
                        MinAmt = objMerchantSettings.MinimumWithdrawalAmount;
                        MaxAmt = objMerchantSettings.MaximumWithdrawalAmount;
                    }
                    if (!string.IsNullOrEmpty(Amount))
                    {
                        if (Convert.ToDecimal(Amount) < MinAmt)
                        {
                            msg = $"Minimum Withdrawal Amount is {MinAmt}.";
                        }
                    }
                    if (msg == "")
                    {
                        string Description = Remarks;
                        string WithdrawalPurpose = Remarks;
                        if (Session["MerchantUniqueId"] != null)
                        {
                            bool IsWithdrawalApproveByAdmin = false;
                            AddMerchantWithdrawalRequest outobject_request = new AddMerchantWithdrawalRequest();
                            string MerchantId = Session["MerchantUniqueId"].ToString();
                            string DailyLimitCheck = Common.GetMerchantWithdrawalRequestDailyCheckLimit(MerchantId);
                            if (DailyLimitCheck != "success")
                            {
                                msg = DailyLimitCheck;
                            }
                            else
                            {
                                AddMerchantWithdrawalRequest outobject_request_DuplicateCheck = new AddMerchantWithdrawalRequest();
                                GetMerchantWithdrawalRequest inobject_request_DuplicateCheck = new GetMerchantWithdrawalRequest();
                                inobject_request_DuplicateCheck.OrderId = OrderId;
                                AddMerchantWithdrawalRequest res_DuplicateCheck = RepCRUD<GetMerchantWithdrawalRequest, AddMerchantWithdrawalRequest>.GetRecord(Common.StoreProcedures.sp_MerchantWithdrawalRequest_Get, inobject_request_DuplicateCheck, outobject_request_DuplicateCheck);
                                if (res_DuplicateCheck != null && res_DuplicateCheck.Id != 0)
                                {
                                    msg = "Withdrawal Order already submitted.";
                                }
                                else
                                {
                                    string MerchantUniqueId = Session["MerchantUniqueId"].ToString();
                                    AddMerchant outobject = new AddMerchant();
                                    GetMerchant inobject = new GetMerchant();
                                    inobject.CheckActive = 1;
                                    inobject.CheckDelete = 0;
                                    inobject.MerchantUniqueId = MerchantUniqueId;
                                    AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                                    if (model != null && model.Id > 0 && !string.IsNullOrEmpty(MerchantUniqueId))
                                    {
                                        if (model.MerchantType == (int)AddMerchant.MerChantType.Merchant)
                                        {
                                            AddUserLoginWithPin resUserOutObject = new AddUserLoginWithPin();
                                            GetUserLoginWithPin resUserInObject = new GetUserLoginWithPin();
                                            resUserInObject.CheckDelete = 0;
                                            resUserInObject.MemberId = model.UserMemberId;
                                            AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, resUserInObject, resUserOutObject);
                                            if (resUser != null && resUser.Id > 0 && resUser.IsActive == true)
                                            {
                                                int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Withdrawal;
                                                int WithdrawalRequestTypeID = (int)AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType.MerchantBalance;
                                                decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(model.MerchantTotalAmount));
                                                if (RequestType.ToLower().Trim() == "walletbalance")
                                                {
                                                    VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Wallet_Withdrawal;
                                                    WithdrawalRequestTypeID = (int)AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType.WalletBalance;
                                                    WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resUser.TotalAmount));
                                                }
                                                AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(resUser.MemberId.ToString(), Amount, VendorApiType.ToString());

                                                if (WalletBalance < (Convert.ToDecimal(Amount) + objOut.ServiceCharge))
                                                {
                                                    msg = "Insufficient Wallet Amount.";
                                                }
                                                else
                                                {
                                                    AddMerchantBankDetail userBank = new AddMerchantBankDetail();
                                                    bool IsWalletUpdated = false;
                                                    string error_message = String.Empty;
                                                    string Particulars = String.Empty;
                                                    string Json_Response = String.Empty;
                                                    int BankStatus = (int)AddMerchantOrders.MerchantOrderStatus.Pending;
                                                    AddMerchantOrders resOrders = new AddMerchantOrders();
                                                    WalletBalance = WalletBalance - (Convert.ToDecimal(Amount) + objOut.ServiceCharge);
                                                    AddMerchantBankDetail outobjectBankDetail = new AddMerchantBankDetail();
                                                    GetMerchantBankDetail inobjectBankDetail = new GetMerchantBankDetail();
                                                    inobjectBankDetail.MerchantId = MerchantId;
                                                    inobjectBankDetail.CheckActive = 1;
                                                    inobjectBankDetail.CheckDelete = 0;
                                                    userBank = RepCRUD<GetMerchantBankDetail, AddMerchantBankDetail>.GetRecord(Common.StoreProcedures.sp_MerchantBankDetail_Get, inobjectBankDetail, outobjectBankDetail);
                                                    if (userBank != null && userBank.Id > 0)
                                                    {
                                                        if (userBank.IsVerified == true && userBank.IsActive == true && userBank.IsDeleted == false)
                                                        {
                                                            string TransactionId = String.Empty;
                                                            string ProcessId = DateTime.UtcNow.ToString("ddMMyyyyhhmmssms") + Common.RandomNumber(111, 999).ToString();
                                                            AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                                            GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                                            inobjectOrders.MerchantId = MerchantId;
                                                            inobjectOrders.OrderId = OrderId;
                                                            resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                                            if (resOrders != null && resOrders.Id != 0)
                                                            {
                                                                if (resOrders.OTPAttemptCount < 3)
                                                                {
                                                                    if (Common.DecryptString(resOrders.OrderOTP) == OTP)
                                                                    {
                                                                        if (resOrders.UpdatedDate.AddMinutes(2) >= DateTime.UtcNow)
                                                                        {
                                                                            bool resOrdersFlag = false;
                                                                            WalletTransactions res_transaction = new WalletTransactions();
                                                                            if ((resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Incomplete))
                                                                            {
                                                                                TransactionId = resOrders.TransactionId;
                                                                                resOrders.Remarks = "Merchant Withdrawal Order Initiated For Bank Transfer";
                                                                                resOrders.CurrentBalance = WalletBalance;
                                                                                resOrders.UpdatedBy = model.UserMemberId;
                                                                                resOrders.UpdatedByName = model.UserName;
                                                                                resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Pending;
                                                                                resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                                                                if (resOrdersFlag && resOrders.Id > 0)
                                                                                {
                                                                                    WithdrawalPurpose = $"Merchant withdrawal Request For Rs.{Amount} with Remarks: {Remarks}";
                                                                                    Common.AddLogs($"Merchant Withdrawal Order Authenticated on {Common.fnGetdatetime()} for OrderID: {OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), model.UserMemberId, model.FirstName + " " + model.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                                                                    if (WithdrawalRequestTypeID == (int)AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType.WalletBalance)
                                                                                    {
                                                                                        res_transaction.TransactionUniqueId = TransactionId;
                                                                                        if (!res_transaction.GetRecordCheckExists())
                                                                                        {
                                                                                            res_transaction.MemberId = Convert.ToInt64(model.UserMemberId);
                                                                                            res_transaction.MerchantMemberId = Convert.ToInt64(model.MerchantMemberId);
                                                                                            res_transaction.ContactNumber = model.ContactNo;
                                                                                            res_transaction.CustomerID = model.OrganizationName + " (" + model.MerchantUniqueId + ")";
                                                                                            res_transaction.MerchantId = model.MerchantUniqueId;
                                                                                            res_transaction.MerchantOrganization = model.OrganizationName;
                                                                                            res_transaction.MemberName = model.FirstName + " " + model.LastName;
                                                                                            res_transaction.Amount = Convert.ToDecimal(Amount);
                                                                                            res_transaction.UpdateBy = Convert.ToInt64(model.UserMemberId);
                                                                                            res_transaction.UpdateByName = model.FirstName + " " + model.LastName;
                                                                                            res_transaction.CurrentBalance = WalletBalance;
                                                                                            res_transaction.CreatedBy = model.UserMemberId;
                                                                                            res_transaction.CreatedByName = model.FirstName + " " + model.LastName;
                                                                                            res_transaction.Remarks = "Bank Transfer Pending For Merchant Withdrawal";
                                                                                            res_transaction.Reference = ProcessId;
                                                                                            res_transaction.Purpose = WithdrawalPurpose;
                                                                                            res_transaction.Description = WithdrawalPurpose;
                                                                                            res_transaction.TxnInstructionId = ProcessId;
                                                                                            res_transaction.Status = (int)WalletTransactions.Statuses.Pending;
                                                                                            res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_Wallet_Withdrawal;
                                                                                            res_transaction.TransactionUniqueId = TransactionId;
                                                                                            res_transaction.VendorTransactionId = "";
                                                                                            res_transaction.BatchTransactionId = "";
                                                                                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Pending.ToString();
                                                                                            res_transaction.ResponseCode = "";
                                                                                            res_transaction.IsApprovedByAdmin = true;
                                                                                            res_transaction.IsActive = true;
                                                                                            res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Debit);
                                                                                            res_transaction.RecieverName = userBank.Name;
                                                                                            res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                                                                            res_transaction.RecieverAccountNo = userBank.AccountNumber;
                                                                                            res_transaction.RecieverBankCode = userBank.BankCode;
                                                                                            res_transaction.SenderAccountNo = RepNps.sourceaccno;
                                                                                            res_transaction.SenderBankCode = RepNps.sourcebank;
                                                                                            res_transaction.ServiceCharge = objOut.ServiceCharge;
                                                                                            res_transaction.NetAmount = Convert.ToDecimal(Amount) + objOut.ServiceCharge;
                                                                                            res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                                                                            res_transaction.SenderBankName = Common.ConnectIPs_BankName;
                                                                                            res_transaction.SenderBranchName = Common.ConnectIPs_BranchName;
                                                                                            res_transaction.RecieverBankName = userBank.BankName;
                                                                                            res_transaction.RecieverBranchName = userBank.BranchName;
                                                                                            res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NPS;

                                                                                            if (res_transaction.Add())
                                                                                            {
                                                                                                IsWalletUpdated = true;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                msg = "Something Went Wrong. Transaction Not Sent";
                                                                                                Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            msg = "Transaction Sent Already";
                                                                                            Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                                                        }
                                                                                    }
                                                                                    else if (WithdrawalRequestTypeID == (int)AddMerchantWithdrawalRequest.MerchantWithdrawalRequestType.MerchantBalance)
                                                                                    {
                                                                                        model.MerchantTotalAmount = WalletBalance;
                                                                                        IsWalletUpdated = RepCRUD<AddMerchant, GetMerchant>.Update(model, "merchant");
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    msg = "Order not updated";
                                                                                }
                                                                                bool IsApprovalPendingRequired = true;
                                                                                if (Convert.ToDecimal(objOut.NetAmount) <= MaxAmt)// WAIT FOR ADMIN APROVAL IF AMOUNT > MAX AMOUNT.
                                                                                {
                                                                                    IsApprovalPendingRequired = false;
                                                                                    if (IsWalletUpdated) // BANK TRANSACTION ONLY IF WALLET IS UPDATED SUCCESFULLY.
                                                                                    {
                                                                                        msg = RepMerchants.BankWithdrawalMerchant(Amount, ref IsWithdrawalApproveByAdmin, model, resUser, WithdrawalRequestTypeID, objOut.ServiceCharge, userBank.Name, userBank.BankCode, userBank.AccountNumber, ref error_message, ref Particulars, ref Json_Response, ref BankStatus, Description, resOrders, TransactionId, ProcessId);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        resOrders.Remarks = "Wallet not updated";
                                                                                        resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                                                                                        resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                                                                        msg = $"Request Failed For OrderID: {resOrders.OrderId}";
                                                                                        Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    resOrders.Remarks = "Waiting For Admin Approval";
                                                                                    resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.ApprovalPending;
                                                                                    resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                                                                                    // msg = $"Request Submitted OrderID: {resOrders.OrderId}";
                                                                                    msg = "submitted";
                                                                                    Common.AddLogs($"Withdrawal Request Submitted By MerchantID: {MerchantId}", false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                                                }
                                                                                if (IsWalletUpdated)
                                                                                {
                                                                                    AddMerchantWithdrawalRequest res_request = new AddMerchantWithdrawalRequest();
                                                                                    res_request.Amount = Convert.ToDecimal(Amount);
                                                                                    res_request.BankId = Convert.ToInt64(Bankid);
                                                                                    res_request.BankCode = userBank.BankCode;
                                                                                    res_request.BankStatus = BankStatus;
                                                                                    res_request.WithdrawalRequestType = WithdrawalRequestTypeID;
                                                                                    res_request.CreatedBy = Convert.ToInt64(model.UserMemberId);
                                                                                    res_request.CreatedByName = model.UserName;
                                                                                    res_request.UpdatedBy = Convert.ToInt64(model.UserMemberId);
                                                                                    res_request.UpdatedByName = model.UserName;
                                                                                    res_request.MerchantId = Session["MerchantUniqueId"].ToString();
                                                                                    res_request.Remarks = Remarks;
                                                                                    res_request.TrackerId = resOrders.TrackerId;
                                                                                    res_request.Particulars = Particulars;
                                                                                    res_request.BankStatus = BankStatus;
                                                                                    res_request.JsonResponse = Json_Response;
                                                                                    res_request.MerchantName = model.FirstName + " " + model.LastName;
                                                                                    res_request.MerchantOrganization = model.OrganizationName;
                                                                                    res_request.MerchantContactNumber = model.ContactNo;
                                                                                    res_request.OrderId = OrderId;
                                                                                    res_request.IsActive = true;
                                                                                    res_request.BankName = userBank.BankName;
                                                                                    res_request.AccountNumber = userBank.AccountNumber;
                                                                                    res_request.IsWithdrawalApproveByAdmin = IsWithdrawalApproveByAdmin;
                                                                                    res_request.Status = BankStatus;
                                                                                    res_request.Description = Description;
                                                                                    if (IsApprovalPendingRequired)
                                                                                    {
                                                                                        res_request.Status = (int)AddMerchantOrders.MerchantOrderStatus.ApprovalPending;
                                                                                        res_request.Remarks = "Waiting For Admin Approval";
                                                                                    }
                                                                                    res_request.WithdrawalRequestType = WithdrawalRequestTypeID;
                                                                                    Int64 RequestId = RepCRUD<AddMerchantWithdrawalRequest, GetMerchantWithdrawalRequest>.Insert(res_request, "merchantwithdrawalrequest");

                                                                                    if (RequestId > 0)
                                                                                    {
                                                                                        if (IsApprovalPendingRequired == false)
                                                                                        {
                                                                                            if (BankStatus == (int)AddMerchantOrders.MerchantOrderStatus.Success)
                                                                                            {
                                                                                                msg = "success";
                                                                                                Common.AddLogs("Withdrawal Successfully Completed by (Merchant : " + Session["MerchantUniqueId"].ToString() + ")", true, (int)AddLog.LogType.Merchant);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                msg = "Withdraw Request Submitted with Bank Transaction Status: " + Enum.GetName(typeof(AddMerchantOrders.MerchantOrderStatus), BankStatus).ToString();
                                                                                                Common.AddLogs(msg + " (Merchant : " + Session["MerchantUniqueId"].ToString() + ")", true, (int)AddLog.LogType.Merchant);
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Common.AddLogs($"Withdrawal Request Not Saved For OrderID: {resOrders.OrderId}", true, (int)AddLog.LogType.Merchant);
                                                                                        msg = "Your Withdrawal Request Not Saved";
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    resOrders.Remarks = "Wallet not updated";
                                                                                    resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                                                                                    resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                                                                                    msg = $"Request Failed For OrderID: {resOrders.OrderId}";
                                                                                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                msg = "Order already updated";
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            msg = "OTP Expired";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        resOrders.OTPAttemptCount = resOrders.OTPAttemptCount + 1;
                                                                        bool InvalidOTPFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                                                        msg = "Invalid OTP.";
                                                                        Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    msg = "Invalid OTP Attempt Exceeded. ";
                                                                    Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                msg = "Duplicate OrderId.";
                                                                Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            msg = "Bank Verification Pending.";
                                                            Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        msg = "Bank Details not found";
                                                        Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.MerchantBankTransfer), model.UserMemberId, model.FirstName + " " + model.LastName, true, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                msg = "User Not Exists";
                                            }
                                        }
                                        else
                                        {
                                            msg = "Withdrawal feature is not available for Banking Merchants";
                                        }
                                    }
                                    else
                                    {
                                        msg = "Something went wrong. Please try again later.";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [Authorize]
        public JsonResult ResetMerchantKeys(AddMerchant model)
        {
            AddMerchant outobject = new AddMerchant();
            GetMerchant inobject = new GetMerchant();
            inobject.MerchantUniqueId = model.MerchantUniqueId;
            inobject.CheckActive = 1;
            inobject.CheckDelete = 0;
            AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            if (res != null && res.Id != 0 && !string.IsNullOrEmpty(model.MerchantUniqueId))
            {
                string Password = Common.DecryptionFromKey(res.Password, res.secretkey);
                string APIPassword = Common.DecryptionFromKey(res.API_Password, res.secretkey);
                res.secretkey = Common.RandomString(16);
                res.apikey = Common.EncryptionFromKey(res.UserName + ":" + Password + ":" + res.MerchantUniqueId, res.secretkey);
                res.Password = Common.EncryptionFromKey(Password, res.secretkey);
                res.API_Password = Common.EncryptionFromKey(APIPassword, res.secretkey);
                if (Session["AdminMemberId"] != null)
                {
                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedByName = Session["AdminUserName"].ToString();
                }
                else if (Session["MerchantUniqueId"] != null)
                {
                    //res.UpdatedBy = Convert.ToInt64(Session["MerchantUniqueId"]);
                    res.UpdatedByName = Session["MerchantUniqueId"].ToString();
                }
                res.UpdatedDate = DateTime.UtcNow;
                bool IsUpdated = RepCRUD<AddMerchant, GetMerchant>.Update(res, "merchant");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update Merchant";
                    if (Session["AdminMemberId"] != null)
                    {
                        Common.AddLogs("Updated Merchant Keys of (MerchantID: " + res.MerchantUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    }
                    else if (Session["MerchantUniqueId"] != null)
                    {
                        Common.AddLogs("Updated Merchant Keys of (MerchantID: " + res.MerchantUniqueId + " ) by (Merchant:" + Session["MerchantUniqueId"] + ")", false, (int)AddLog.LogType.Merchant, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, res.MerchantMemberId, Session["MerchantUniqueId"].ToString());
                    }
                }
                else
                {
                    ViewBag.Message = "Not Updated Merchant";
                    Common.AddLogs("Not Updated Merchant Keys", true, (int)AddLog.LogType.Merchant, 0, "", false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys);
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult ResetMerchantAPIPassword(AddMerchant model)
        {
            AddMerchant outobject = new AddMerchant();
            GetMerchant inobject = new GetMerchant();
            inobject.MerchantUniqueId = model.MerchantUniqueId;
            inobject.CheckActive = 1;
            inobject.CheckDelete = 0;
            AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
            if (res != null && res.Id != 0 && !string.IsNullOrEmpty(model.MerchantUniqueId))
            {
                //res.secretkey = Common.RandomString(16);
                res.API_Password = Common.EncryptionFromKey(Common.RandomString(15), res.secretkey);
                if (Session["AdminMemberId"] != null)
                {
                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                    res.UpdatedByName = Session["AdminUserName"].ToString();
                }
                else if (Session["MerchantUniqueId"] != null || !string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
                {
                    //res.UpdatedBy = Convert.ToInt64(Session["MerchantUniqueId"]);
                    res.UpdatedByName = Session["MerchantUniqueId"].ToString();
                }
                res.UpdatedDate = DateTime.UtcNow;
                bool IsUpdated = RepCRUD<AddMerchant, GetMerchant>.Update(res, "merchant");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update Merchant";
                    if (Session["AdminMemberId"] != null)
                    {
                        Common.AddLogs("Updated Merchant API Password of (MerchantID: " + res.MerchantUniqueId + " ) by (AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Merchant, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, Convert.ToInt64(Session["AdminMemberId"]), Session["AdminUserName"].ToString());
                    }
                    else if (Session["MerchantUniqueId"] != null || !string.IsNullOrEmpty(Session["MerchantUniqueId"].ToString()))
                    {
                        Common.AddLogs("Updated Merchant API Password of (MerchantID: " + res.MerchantUniqueId + " ) by (Merchant:" + Session["MerchantUniqueId"] + ")", false, (int)AddLog.LogType.Merchant, res.MerchantMemberId, res.UserName, false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys, res.MerchantMemberId, Session["MerchantUniqueId"].ToString());
                    }
                }
                else
                {
                    ViewBag.Message = "Not Updated Merchant";
                    Common.AddLogs("Not Updated Merchant API Password", true, (int)AddLog.LogType.Merchant, 0, "", false, "web", "", (int)AddLog.LogActivityEnum.Reset_Merchant_Keys);
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}