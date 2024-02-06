using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Get.KhanePani;
using MyPay.Models.Get.Nea;
using MyPay.Models.Get.PlasmaAirlines;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Request;
using MyPay.Models.Request.WebRequest;
using MyPay.Models.Request.WebRequest.Common;
using MyPay.Models.Response;
using MyPay.Models.Response.WebResponse;
using MyPay.Models.Response.WebResponse.Common;
using MyPay.Models.VendorAPI.Get.Antivirus.K7;
using MyPay.Models.VendorAPI.Get.Antivirus.Mcafee;
using MyPay.Models.VendorAPI.Get.Insurance.Arhant;
using MyPay.Models.VendorAPI.Get.Insurance.Jyoti;
using MyPay.Models.VendorAPI.Get.Insurance.National;
using MyPay.Models.VendorAPI.Get.Insurance.Neco;
using MyPay.Models.VendorAPI.Get.Insurance.Nepal;
using MyPay.Models.VendorAPI.Get.Insurance.Prabhu;
using MyPay.Models.VendorAPI.Get.Insurance.Prime;
using MyPay.Models.VendorAPI.Get.Insurance.Reliance;
using MyPay.Models.VendorAPI.Get.Insurance.Sagarmatha;
using MyPay.Models.VendorAPI.Get.Insurance.Shikhar;
using MyPay.Models.VendorAPI.Get.Internet.SUBISU;
using MyPay.Models.VendorAPI.Get.WorldLink;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using ServiceStack;
using ServiceStack.Html;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Runtime.Remoting;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
using static MyPay.Models.Get.GetTicketTypesCableCar;
using System.Windows.Interop;
using System.Xml;
using static MyPay.Models.Add.AddUser;
using static MyPay.Models.Request.WebRequest.WebRequest_FlightPassanger;
using static MyPay.Models.Response.WebResponse.WebRes_FlightSector;
using static MyPay.Repository.RepKhalti;
using Formatting = Newtonsoft.Json.Formatting;
using Passenger = MyPay.Models.Get.PlasmaAirlines.Passenger;
using System.Web.Services.Description;
using System.Web.Http.Results;
using System.Runtime.Remoting;
using System.IO;
using System.Net.Http;
using MyPay.Models;
using BitMiracle.LibTiff.Classic;
using System.Windows.Interop;
using System.IO;


namespace MyPay.Controllers
{
    public class MyPayUserController : BaseMyPayUserSessionController
    {
        // GET: MyPayUserLogin
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Req_MyPayUserLogin model, FormCollection frm)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult MyPayUserTopup()
        {
            List<SelectListItem> Providerlist = CommonHelpers.GetSelectList_Providerchange("17");

            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.Title == "Mobile Topup").ToList();
            ViewBag.Providerlist = Providerlist;
            ViewBag.Topuplist = objRes;
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);

            AddUser model = new AddUser();
            return View(model);
        }

        [HttpGet]
        public ActionResult MyPayUserKhanePani()
        {
            GetVendor_API_KhanePani_Counters objRes = new GetVendor_API_KhanePani_Counters();
            string DeviceCode = Convert.ToString(Session["MyPayUserBrowserID"]);
            List<SelectListItem> DisplayCounterList = new List<SelectListItem>();
            string msg = RepKhalti.RequestServiceGroup_KHANEPANI_COUNTER("1", DeviceCode, "Web", ref objRes);
            if (msg.ToLower() == "success")
            {
                List<SelectListItem> CounterList = (from p in objRes.counters.AsEnumerable()
                                                    select new SelectListItem
                                                    {
                                                        Text = p.name,
                                                        Value = p.value.ToString()
                                                    }).ToList();
                DisplayCounterList = CommonHelpers.CreateDropdown("0", CounterList, "Select Counters");
            }
            ViewBag.Counters = DisplayCounterList;
            ViewBag.ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_khanepani;
            if (Common.CheckServiceDown(ViewBag.ServiceId.ToString()))
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");

            }
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            return View();
        }


        [HttpGet]
        public ActionResult MyPayUserCreditCard()
        {
            string DeviceCode = Convert.ToString(Session["MyPayUserBrowserID"]);
            List<SelectListItem> DisplayBankList = new List<SelectListItem>();
            List<GetCreditCardIssuerList> list = RepPrabhu.GetCrediCardIssuerList();

            List<SelectListItem> BankCodeList = (from p in list.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.Name,
                                                     Value = p.Code.ToString()
                                                 }).ToList();
            DisplayBankList = CommonHelpers.CreateDropdown("0", BankCodeList, "Select Bank Name");
            ViewBag.BankCodes = DisplayBankList;
            ViewBag.ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;

            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);

            if (Common.CheckServiceDown(ViewBag.ServiceId.ToString()))
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");
            }

            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserAntivirus()
        {
            List<SelectListItem> DisplayCounterList = new List<SelectListItem>();
            int ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_antivirus_kaspersky; ;
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().ToList();
            objRes = objRes.Where(x => x.Title == "Antivirus").ToList();
            ViewBag.AntivirusList = objRes;
            ViewBag.ServiceId = ServiceID;
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserServiceDown()
        {
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserFlightBookings()
        {
            int ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
            ViewBag.ServiceId = ServiceID;
            ViewBag.MyPayContactNumber = Session["MyPayContactNumber"].ToString();
            ViewBag.MyPayEmail = Session["MyPayEmail"].ToString();
            ViewBag.MyPayFullName = Session["MyPayFullName"].ToString();
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserLinkedAccount()
        {
            var MemberID = Session["MyPayUserMemberId"];
            ViewBag.ContactNumber = Session["MyPayContactNumber"];
            ViewBag.MemberId = Session["MyPayUserMemberId"];
            if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
            {
                bool isEmailVeriFied = Common.ISEmailVeriFied(MemberID.ToString());
                ViewBag.isEmailVeriFied = isEmailVeriFied;
            }
            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserTransactions()
        {
            List<SelectListItem> objTransactionTypeList = new List<SelectListItem>();
            SelectListItem objTransactionType = new SelectListItem();
            objTransactionType.Text = "Transaction Type";
            objTransactionType.Value = "0";
            objTransactionType.Selected = true;
            objTransactionTypeList.Add(objTransactionType);

            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().ToList();
            for (int i = 0; i < objRes.Count; i++)
            {
                objTransactionType = new SelectListItem();
                objTransactionType.Text = objRes[i].ProviderName;
                objTransactionType.Value = objRes[i].ProviderTypeId;
                objTransactionTypeList.Add(objTransactionType);
            }
            ViewBag.objTransactionTypeList = objTransactionTypeList;
            ViewBag.IsTransferByPhone = "";
            if (TempData.ContainsKey("IsTransferByPhone"))
            {
                string IsTransferByPhone = TempData["IsTransferByPhone"].ToString();
                ViewBag.IsTransferByPhone = IsTransferByPhone;
            }
            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserRedirectBankPayment(string otc)
        {
            if (!string.IsNullOrEmpty(otc))
            {

                string RedirectURL = Request.RawUrl.Split('?')[1].Replace("otc=", "");

                string decrypted = Common.Decryption(RedirectURL);
                if (decrypted != "error" && Session["MyPayUserMemberId"] != null)
                {
                    RedirectURL = Server.UrlDecode(decrypted);
                    if (Common.ApplicationEnvironment.IsProduction)
                    {
                        RedirectURL = RedirectURL.Replace(Common.LiveSiteUrl, "");
                    }
                    else
                    {
                        RedirectURL = RedirectURL.Replace(Common.TestSiteUrl, "");
                    }

                    System.Web.HttpContext.Current.Server.TransferRequest(RedirectURL, true);
                }
                //Server.Transfer(RedirectURL);
                //Transfer(decrypted);
            }

            return View();
        }
        private void Transfer(string url)
        {
            // Create URI builder
            var uriBuilder = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port, Request.ApplicationPath);
            // Add destination URI
            uriBuilder.Path += url;
            // Because UriBuilder escapes URI decode before passing as an argument
            string path = Server.UrlDecode(uriBuilder.Uri.PathAndQuery);
            // Rewrite path
            System.Web.HttpContext.Current.RewritePath(path, false);
            IHttpHandler httpHandler = new MvcHttpHandler();
            // Process request
            httpHandler.ProcessRequest(System.Web.HttpContext.Current);
        }

        [HttpGet]
        public ActionResult MyPayUserProfile()
        {

            ViewBag.QRCode = Common.GetQRMemberID_Encrypted(Convert.ToString(Session["MyPayUserMemberId"]));
            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserSettings()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MyPayRightSide()
        {

            return View();
        }

        [HttpPost]
        public JsonResult WalletBalance()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    AddUserLoginWithPin resUserOutObject = new AddUserLoginWithPin();
                    GetUserLoginWithPin resUserInObject = new GetUserLoginWithPin();
                    resUserInObject.CheckDelete = 0;
                    resUserInObject.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, resUserInObject, resUserOutObject);
                    Session["MyPayUserJWTToken"] = resUser.JwtToken;
                    Session["MyPayUserBrowserID"] = resUser.DeviceId;

                    string ApiName = "api/GetUserWalletWithKYC";
                    WebRequest_Topup objReq = new WebRequest_Topup();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.Type = (int)WalletTransactions.WalletTypes.Wallet;
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebRes_GetUserWalletWithKYC objResponse = new WebRes_GetUserWalletWithKYC();
                        objResponse = JsonConvert.DeserializeObject<WebRes_GetUserWalletWithKYC>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            Session["MyPayUserWalletbalance"] = objResponse.TotalAmount.ToString();
                            Session["MyPayUserMPCoinsbalance"] = objResponse.TotalRewardPoints.ToString();
                            Session["MyPayUserTotalCashback"] = objResponse.TotalCashback.ToString();
                            Session["MyPayUserRefCode"] = objResponse.RefCode.ToString();
                            Session["IsKycVerified"] = objResponse.IsKycVerified.ToString();
                            Session["MPCoinsFlushDate"] = objResponse.MPCoinsFlushDateDt.ToString();
                        }
                        else if (!objResponse.IsDeviceActive || objResponse.IsLogout || objResponse.IsResetPasswordFromAdmin || objResponse.ReponseCode == 7)
                        {
                            //RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult ServiceCharge(string Amount, int ServiceId)
        {
            var result = string.Empty;
            try
            {
                if (ServiceId == 13 && Session["KhanePani_TotalDues"] != null)
                {
                    Amount = Convert.ToString(Session["KhanePani_TotalDues"]);
                }
                if (string.IsNullOrEmpty(Amount) || Amount == "" || Amount == "0" || Amount == "undefined")
                {
                    result = "Please Enter Amount";
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/GetServiceCharge";
                        WebRequest_Topup objReq = new WebRequest_Topup();
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.Amount = Convert.ToDecimal(Amount);
                        objReq.ServiceId = ServiceId;
                        objReq.PlatForm = "Web";
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserTopup(string Providerlist, string MobileNo, string Amount, string Mpin, string PaymentMode, string Couponcode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Providerlist) || Providerlist == "")
                {
                    result = "Please Select Providerlist";
                }
                else if (string.IsNullOrEmpty(MobileNo) || MobileNo == "")
                {
                    result = "Please Enter MobileNo";
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, Providerlist, "Top up payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }


                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "";
                        if (Providerlist == "2")
                        {
                            ApiName = "api/user-ntc";
                        }
                        else if (Providerlist == "3")
                        {
                            ApiName = "api/user-ncell";
                        }
                        else if (Providerlist == "4")
                        {
                            ApiName = "api/user-smartcell";
                        }
                        WebRequest_Topup objReq = new WebRequest_Topup();
                        objReq.Providerlist = Providerlist;
                        objReq.Number = MobileNo;
                        objReq.UniqueCustomerId = MobileNo;
                        objReq.Amount = Convert.ToDecimal(Amount);
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.CouponCode = Couponcode;
                        objReq.BankTransactionId = BankTransactionID;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {



                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserLandline(string ServiceID, string Number, string Amount, string Mpin, string PaymentMode, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceID) || ServiceID == "")
                {
                    result = "Please Select ServiceID";
                }
                else if (string.IsNullOrEmpty(Number) || Number == "")
                {
                    result = "Please Enter Number";
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID, "Landline payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/user-pstn-landline";
                        WebRequest_Topup objReq = new WebRequest_Topup();
                        objReq.ServiceId = Convert.ToInt64(ServiceID);
                        objReq.Number = Number;
                        objReq.UniqueCustomerId = Number;
                        objReq.Amount = Convert.ToDecimal(Amount);
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.CouponCode = CouponCode;
                        objReq.BankTransactionId = BankTransactionID;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }



        [HttpPost]
        public JsonResult MyPayUserKhanePaniCounterDetails(string Counter, string CustomerID)
        {
            var result = string.Empty;
            try
            {
                string Month_Id = System.DateTime.Now.Month.ToString();
                if (string.IsNullOrEmpty(Month_Id) || Month_Id == "")
                {
                    result = "Please Select Month_Id";
                }
                else if (string.IsNullOrEmpty(Counter) || Counter == "")
                {
                    result = "Please Enter Counter";
                }
                else if (string.IsNullOrEmpty(CustomerID) || CustomerID == "")
                {
                    result = "Please Enter CustomerID";
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebResponse_KhanePaniDetails objresult = new WebResponse_KhanePaniDetails();
                        GetVendor_API_ServiceGroup_KhanePani_Details objRes = new GetVendor_API_ServiceGroup_KhanePani_Details();
                        string msg = RepKhalti.RequestServiceGroup_DETAILS_KHANEPANI(Month_Id, CustomerID, Counter, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            objresult.ReponseCode = objRes.status ? 1 : 0;
                            objresult.Consumer_Code = objRes.customer_code;
                            objresult.Consumer_Name = objRes.customer_name;
                            objresult.Address = objRes.address;
                            objresult.Mobile = objRes.mobile_number;
                            objresult.Current_month_discount = objRes.current_month_discount;
                            objresult.Current_Month_Dues = objRes.current_month_dues;
                            objresult.Current_Month_fine = objRes.current_month_fine;
                            objresult.Total_credit_sales_amount = objRes.total_credit_sales_amount;
                            objresult.Total_advance_amount = objRes.total_advance_amount;
                            objresult.Previous_dues = objRes.previous_dues;
                            objresult.Total_Dues = objRes.total_dues;
                            Session["KhanePani_TotalDues"] = objRes.total_dues;
                            objresult.Minimum_Payable_Amount = objRes.minimum_payable_amount;
                            objresult.status = objRes.status;
                            objresult.Message = "success";
                        }
                        else
                        {
                            objresult.Message = msg;
                        }
                        result = JsonConvert.SerializeObject(objresult);
                        TempData["BillDetail"] = result;
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserCreditCardDetails(string Amount, string Code)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Amount) || Amount == "0")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(Code) || Code == "")
                {
                    result = "Please Enter Code";
                }

                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        GetCreditCardCharge senddoc = RepPrabhu.GetCreditCardCharge(Amount.ToString(), Code);
                        if (senddoc.Message.ToLower() == "success")
                        {
                            objResponse.status = true;
                            objResponse.ReponseCode = 1;
                            objResponse.Message = "Success";
                            objResponse.Details = senddoc.SCharge;
                        }
                        else
                        {
                            objResponse.Message = senddoc.Message;
                            objResponse.Details = senddoc.Message;
                        }
                        result = JsonConvert.SerializeObject(objResponse);

                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }



        [HttpPost]
        public JsonResult MyPayUserCreditCard(string ServiceID, string Amount, string BankName, string BankCode, string CardNumber, string ServiceCharge, string Mpin, string PaymentMode, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(BankName) || BankName == "")
                {
                    result = "Please Select Bank";
                }
                if (string.IsNullOrEmpty(BankCode) || BankCode == "")
                {
                    result = "Please Select Bank";
                }
                else if (string.IsNullOrEmpty(CardNumber) || CardNumber == "")
                {
                    result = "Please Enter Card Number";
                }
                else if (string.IsNullOrEmpty(ServiceCharge) || ServiceCharge == "")
                {
                    result = "Please Enter ServiceCharge";
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID, "Credit card payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/CreditCardPayment";
                        WebRequest_CreditCard objReq = new WebRequest_CreditCard();
                        objReq.ServiceId = Convert.ToInt64(ServiceID);
                        objReq.Name = BankName;
                        objReq.Code = BankCode;
                        objReq.CardNumber = CardNumber;
                        objReq.ServiceCharge = ServiceCharge;
                        objReq.UniqueCustomerId = CardNumber;
                        objReq.Amount = Convert.ToDecimal(Amount);
                        objReq.Mpin = Common.Encryption(Mpin); ;
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.PaymentMode = PaymentMode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.CouponCode = CouponCode;
                        objReq.BankTransactionId = BankTransactionID;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }





        [HttpPost]
        public JsonResult MyPayUserKhanePani(string ServiceID, string Counter, string CustomerID, string Amount, string Mpin, string PaymentMode, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceID) || ServiceID == "")
                {
                    result = "Please Select ServiceID";
                }
                else if (string.IsNullOrEmpty(Counter) || Counter == "")
                {
                    result = "Please Enter Counter";
                }
                else if (string.IsNullOrEmpty(CustomerID) || CustomerID == "")
                {
                    result = "Please Enter CustomerID";
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID, "Khanepani payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/servicegroup-commit-khanepani";
                        WebRequest_KhanePani objReq = new WebRequest_KhanePani();
                        objReq.ServiceId = Convert.ToInt64(ServiceID);
                        objReq.Counter = Counter;
                        objReq.CustomerCode = CustomerID;
                        objReq.UniqueCustomerId = CustomerID;
                        objReq.Amount = Convert.ToDecimal(Amount);
                        objReq.Mpin = Common.Encryption(Mpin); ;
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        WebResponse_KhanePaniDetails objBillDetail = new WebResponse_KhanePaniDetails();
                        string BillDetail;
                        VendorJsonLookupItemsKhanepani objKhanepani = new VendorJsonLookupItemsKhanepani();
                        objKhanepani.Due_Bills = new List<KhanepaniDueBills>();
                        if (TempData.ContainsKey("BillDetail"))
                        {
                            BillDetail = TempData["BillDetail"].ToString();
                            objBillDetail = JsonConvert.DeserializeObject<WebResponse_KhanePaniDetails>(BillDetail);
                            objKhanepani.Customer_Id = objBillDetail.Consumer_Code;
                            objKhanepani.Customer_Name = objBillDetail.Consumer_Name;
                            objKhanepani.Address = objBillDetail.Address;

                            //objKhanepani.Reference_No = objBillDetail.Mobile;
                            objKhanepani.Total_Due_Amount = objBillDetail.Total_Dues;
                            objKhanepani.Status = objBillDetail.status;
                            KhanepaniDueBills KhanepaniDue = new KhanepaniDueBills();
                            foreach (var bill in objKhanepani.Due_Bills)
                            {
                                KhanepaniDue = new KhanepaniDueBills();
                                KhanepaniDue.Bill_Amount = bill.Bill_Amount;
                                KhanepaniDue.Bill_Date = bill.Bill_Date;
                                KhanepaniDue.Days = bill.Days;
                                KhanepaniDue.Due_Bill_Of = bill.Due_Bill_Of;
                                KhanepaniDue.Payable_Amount = bill.Payable_Amount;
                                KhanepaniDue.Status = bill.Status;
                                objKhanepani.Due_Bills.Add(KhanepaniDue);
                            }
                        }
                        objReq.VendorJsonLookup = JsonConvert.SerializeObject(objKhanepani);
                        objReq.CouponCode = CouponCode;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserAntivirusDetails(int ServiceId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        if (Common.CheckServiceDown(ServiceId.ToString()))
                        {
                            result = Common.ServiceDown;
                        }
                        else
                        {
                            WebRes_Antivirus objResponse = new WebRes_Antivirus();
                            string msg = "";
                            if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_antivirus_kaspersky)
                            {
                                GetVendor_API_Kaspersky_Lookup objRes = new GetVendor_API_Kaspersky_Lookup();
                                string Reference = new CommonHelpers().GenerateUniqueId();
                                string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                                msg = RepKhalti.RequestServiceGroup_Kaspersky_LOOKUP(Reference, "1", deviceId, "Web", ref objRes, true);
                                if (msg.ToLower() == "success")
                                {
                                    result = JsonConvert.SerializeObject(objRes);
                                    objResponse = JsonConvert.DeserializeObject<WebRes_Antivirus>(result);
                                    objResponse.Kaspersky_Bills = objRes.data;
                                    objResponse.ReponseCode = objRes.status ? 1 : 0;
                                    objResponse.status = objRes.status;
                                    objResponse.Message = "success";

                                    result = JsonConvert.SerializeObject(objResponse);
                                    if (objResponse.status && objResponse.Message.ToLower() == "success")
                                    {

                                    }
                                    else if (objResponse.ReponseCode == 7)
                                    {
                                        RepMyPayUserLogin.Logout();
                                        result = objResponse.Message;
                                    }
                                    else
                                    {
                                        result = objResponse.Message;
                                    }
                                }
                                else
                                {
                                    result = "Invalid API Request";
                                }
                            }
                            else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Eset)
                            {
                                GetVendor_API_Eset_Lookup objRes = new GetVendor_API_Eset_Lookup();
                                string Reference = new CommonHelpers().GenerateUniqueId();
                                string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                                msg = RepKhalti.RequestServiceGroup_Eset_LOOKUP(Reference, "1", deviceId, "Web", ref objRes, true);
                                if (msg.ToLower() == "success")
                                {
                                    result = JsonConvert.SerializeObject(objRes);
                                    objResponse = JsonConvert.DeserializeObject<WebRes_Antivirus>(result);
                                    //objResponse.Kaspersky_Bills = objRes.data;
                                    objResponse.ReponseCode = objRes.status ? 1 : 0;
                                    objResponse.status = objRes.status;
                                    objResponse.Message = "success";

                                    result = JsonConvert.SerializeObject(objResponse);
                                    if (objResponse.status && objResponse.Message.ToLower() == "success")
                                    {

                                    }
                                    else if (objResponse.ReponseCode == 7)
                                    {
                                        RepMyPayUserLogin.Logout();
                                        result = objResponse.Message;
                                    }
                                    else
                                    {
                                        result = objResponse.Message;
                                    }
                                }
                                else
                                {
                                    result = "Invalid API Request";
                                }
                            }
                            else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Wardwiz)
                            {
                                GetVendor_API_Wardwiz_Lookup objRes = new GetVendor_API_Wardwiz_Lookup();
                                string Reference = new CommonHelpers().GenerateUniqueId();
                                string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                                msg = RepKhalti.RequestServiceGroup_Wardwiz_LOOKUP(Reference, "1", deviceId, "Web", ref objRes, true);
                                if (msg.ToLower() == "success")
                                {
                                    result = JsonConvert.SerializeObject(objRes);
                                    objResponse = JsonConvert.DeserializeObject<WebRes_Antivirus>(result);
                                    //objResponse.Kaspersky_Bills = objRes.data;
                                    objResponse.ReponseCode = objRes.status ? 1 : 0;
                                    objResponse.status = objRes.status;
                                    objResponse.Message = "success";

                                    result = JsonConvert.SerializeObject(objResponse);
                                    if (objResponse.status && objResponse.Message.ToLower() == "success")
                                    {

                                    }
                                    else if (objResponse.ReponseCode == 7)
                                    {
                                        RepMyPayUserLogin.Logout();
                                        result = objResponse.Message;
                                    }
                                    else
                                    {
                                        result = objResponse.Message;
                                    }
                                }
                                else
                                {
                                    result = "Invalid API Request";
                                }
                            }
                            else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_k7)
                            {
                                GetVendor_API_K7_Lookup objRes = new GetVendor_API_K7_Lookup();
                                string Reference = new CommonHelpers().GenerateUniqueId();
                                string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                                msg = RepKhalti.RequestServiceGroup_K7_LOOKUP(Reference, "1", deviceId, "Web", ref objRes, true);
                                if (msg.ToLower() == "success")
                                {
                                    result = JsonConvert.SerializeObject(objRes);
                                    objResponse = JsonConvert.DeserializeObject<WebRes_Antivirus>(result);
                                    //objResponse.Kaspersky_Bills = objRes.data;
                                    objResponse.ReponseCode = objRes.status ? 1 : 0;
                                    objResponse.status = objRes.status;
                                    objResponse.Message = "success";

                                    result = JsonConvert.SerializeObject(objResponse);
                                    if (objResponse.status && objResponse.Message.ToLower() == "success")
                                    {

                                    }
                                    else if (objResponse.ReponseCode == 7)
                                    {
                                        RepMyPayUserLogin.Logout();
                                        result = objResponse.Message;
                                    }
                                    else
                                    {
                                        result = objResponse.Message;
                                    }
                                }
                                else
                                {
                                    result = "Invalid API Request";
                                }
                            }
                            else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Mcafee)
                            {
                                GetVendor_API_Mcafee_Lookup objRes = new GetVendor_API_Mcafee_Lookup();
                                string Reference = new CommonHelpers().GenerateUniqueId();
                                string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                                msg = RepKhalti.RequestServiceGroup_Mcafee_LOOKUP(Reference, "1", deviceId, "Web", ref objRes, true);
                                if (msg.ToLower() == "success")
                                {
                                    result = JsonConvert.SerializeObject(objRes);
                                    objResponse = JsonConvert.DeserializeObject<WebRes_Antivirus>(result);
                                    //objResponse.Kaspersky_Bills = objRes.data;
                                    objResponse.ReponseCode = objRes.status ? 1 : 0;
                                    objResponse.status = objRes.status;
                                    objResponse.Message = "success";

                                    result = JsonConvert.SerializeObject(objResponse);
                                    if (objResponse.status && objResponse.Message.ToLower() == "success")
                                    {

                                    }
                                    else if (objResponse.ReponseCode == 7)
                                    {
                                        RepMyPayUserLogin.Logout();
                                        result = objResponse.Message;
                                    }
                                    else
                                    {
                                        result = objResponse.Message;
                                    }
                                }
                                else
                                {
                                    result = "Invalid API Request";
                                }
                            }
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserAntivirus(string ServiceID, string Subscription, string Amount, string Mpin, string PaymentMode, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceID) || ServiceID == "")
                {
                    result = "Please Select ServiceID";
                }
                else if (string.IsNullOrEmpty(Subscription) || Subscription == "")
                {
                    result = "Please Enter Subscription";
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID, "Antivirus payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }


                }

                if (string.IsNullOrEmpty(result))
                {

                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/use-antivirus-kaspersky";
                        WebRequest_Antivirus objReq = new WebRequest_Antivirus();
                        objReq.ServiceId = Convert.ToInt64(ServiceID);
                        objReq.Value = Subscription;
                        objReq.Amount = Convert.ToDecimal(Amount);
                        objReq.Mpin = Common.Encryption(Mpin); ;
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.Version = "1.0";
                        objReq.PaymentMode = PaymentMode;
                        objReq.CouponCode = CouponCode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.BankTransactionId = BankTransactionID;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return Json(result);
        }
        [HttpGet]
        public ActionResult MyPayUserChangePin()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePin(string Pin, string ConfirmPin, string Password)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Pin) || Pin == "")
                {
                    result = "Please Enter New Pin";
                }
                else if (string.IsNullOrEmpty(ConfirmPin) || ConfirmPin == "")
                {
                    result = "Please Enter Confirm Pin";
                }
                else if (string.IsNullOrEmpty(Password) || Password == "")
                {
                    result = "Please Enter Password";
                }
                else if (Pin != ConfirmPin)
                {
                    result = "Confirm Pin doesn't match.";
                }
                else if (Pin.Length < 4)
                {
                    result = "Pin must be 4 characters long";
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/ChangePin";
                        WebRequest_Pin objReq = new WebRequest_Pin();
                        objReq.Pin = Pin;
                        objReq.ConfirmPin = ConfirmPin;
                        objReq.Password = Password;
                        objReq.MemberId = Convert.ToInt32(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.ReponseCode.ToString() == "1")
                            {
                                result = "success";
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserTransactionList(string Take, string Skip, string Type, string DateFilterType, string DateFrom, string DateTo)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getall-transactions";
                    WebRequest_Transactions objReq = new WebRequest_Transactions();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.Type = Convert.ToInt32(Type);
                    objReq.DateFilterType = DateFilterType;
                    objReq.DateFrom = DateFrom;
                    objReq.DateTo = DateTo;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserCheckServiceDown(string ServiceId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        if (Common.CheckServiceDown(ServiceId.ToString()))
                        {
                            result = Common.ServiceDown;
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpGet]
        public ActionResult MyPayUserCoinsHistory()
        {
            return View();
        }
        [HttpPost]
        public JsonResult MyPayUserBookingsList(string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/get-flight-details";
                    WebRequest_Flights objReq = new WebRequest_Flights();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.BookingID = 0;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebRes_Flights objResponse = new WebRes_Flights();
                        objResponse = JsonConvert.DeserializeObject<WebRes_Flights>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserProfileDetails()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/GetUserDetail";
                    WebRequest_Profile objReq = new WebRequest_Profile();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpGet]
        public ActionResult MyPayUserQR()
        {
            if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
            {
                ViewBag.MemberName = Session["MyPayFullName"];
                ViewBag.MemberContactNumber = Session["MyPayContactNumber"];
                ViewBag.QRCode = Common.GetQRMemberID_Encrypted(Convert.ToString(Session["MyPayUserMemberId"]));
            }
            else
            {
                return RedirectToAction("Index", "MyPayUserLogin");
            }
            return View();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "This is MyPayUser Change Password Page";
            AddUserLoginWithPin model = new AddUserLoginWithPin();

            if (Session["MyPayUserMemberId"] != null)
            {
                AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                inobject.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                model = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                if (model != null && model.Id != 0)
                {
                    ViewBag.MyPayUserOldPassword = Common.DecryptString(model.Password);
                }
                else
                {
                    ViewBag.MyPayUserOldPassword = "";
                }
            }
            else
            {
                return RedirectToAction("Index", "MyPayUserLogin"); ;
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult ChangePassword(string OldPassword, string Password, string ConfirmPassword)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(OldPassword) || OldPassword == "")
                {
                    result = "Please Enter Old Password";
                }
                else if (string.IsNullOrEmpty(Password) || Password == "")
                {
                    result = "Please Enter New Password";
                }
                else if (string.IsNullOrEmpty(ConfirmPassword) || ConfirmPassword == "")
                {
                    result = "Please Enter Confirm Password";
                }
                else if (Password != ConfirmPassword)
                {
                    result = "Confirm Password doesn't match.";
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/ChangePassword";
                        WebRequest_ChangePassword objReq = new WebRequest_ChangePassword();
                        objReq.OldPassword = OldPassword;
                        objReq.Password = Password;
                        objReq.ConfirmPassword = ConfirmPassword;
                        objReq.MemberId = Convert.ToInt32(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.ReponseCode.ToString() == "1")
                            {
                                RepMyPayUserLogin.Logout();
                                result = "success";
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult MyPayUserNotifications()
        {
            if (TempData.ContainsKey("IsRequestFund"))
            {
                string IsRequestFund = TempData["IsRequestFund"].ToString();
                ViewBag.IsRequestFund = "IsRequestFund";
            }
            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserCashbakOffers()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MyPayUserLoadWallet()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MyPayUserLandline()
        {
            int serviceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_pstn_landline;

            if (Common.CheckServiceDown(serviceID.ToString()))
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");

            }
            List<GetService_Providers> objList = VendorApi_CommonHelper.GetServiceProvidersWithUtility();
            List<GetService_Providers> objListNew = objList.Where(x => x.ProviderTypeId == serviceID.ToString()).ToList();
            List<SelectListItem> objValidAmount = new List<SelectListItem>();
            if (objListNew.Count > 0)
            {
                string[] ValidAmountCSV = objListNew[0].ValidAmount.Split(',');
                for (int i = 0; i < ValidAmountCSV.Length; i++)
                {
                    SelectListItem objItem = new SelectListItem();
                    objItem.Value = ValidAmountCSV[i];
                    objItem.Text = ValidAmountCSV[i];
                    if (i == 0)
                    {
                        objItem.Selected = true;
                    }
                    objValidAmount.Add(objItem);
                }
            }
            ViewBag.ServiceId = serviceID;
            ViewBag.Amount = objValidAmount;

            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            return View();

        }
        [HttpPost]
        public JsonResult MyPayUserCashbackOffersList()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getofferbanners";
                    WebRequest_CashbackOffers objReq = new WebRequest_CashbackOffers();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.IsHome = 0;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebRes_GetUserCashbackOffersList objResponse = new WebRes_GetUserCashbackOffersList();
                        objResponse = JsonConvert.DeserializeObject<WebRes_GetUserCashbackOffersList>(result);
                        string MarqueeText = string.Empty;
                        for (int i = 0; i < objResponse.MarqueList.Count; i++)
                        {
                            if (objResponse.MarqueList[i].Link != "")
                            {
                                MarqueeText += $"<a target='_blank' href='{objResponse.MarqueList[i].Link}'>{objResponse.MarqueList[i].Description}</a>.  ";
                            }
                            else
                            {
                                MarqueeText += $"<a target='_blank'>{objResponse.MarqueList[i].Description}</a>.  ";
                            }
                        }
                        Session["UserMarqueeList"] = MarqueeText;
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            MyPayEntities context = new MyPayEntities();
                            objResponse.list = context.ProviderServiceCategoryLists.ToList();
                            result = JsonConvert.SerializeObject(objResponse);
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpGet]
        public ActionResult MyPayUserVoting()
        {
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Voting;
            ViewBag.ServiceId = ServiceId;
            if (Common.CheckServiceDown(ServiceId.ToString()))
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");
            }
            return View();
        }

        [HttpPost]
        public JsonResult MyPayUserVotingList()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getvotingcompetition";
                    WebRequest_CashbackOffers objReq = new WebRequest_CashbackOffers();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.IsHome = 0;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserVotingCandidateList(string CompetitionId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/GetVotingcCandidates";
                    WebRequest_VotingCandidates objReq = new WebRequest_VotingCandidates();
                    objReq.VotingCompetitionID = Convert.ToInt64(CompetitionId);
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    //objReq.IsHome = 0;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserVotingPackagesList(string CompetitionId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/GetVotingPackages";
                    WebRequest_VotingCandidates objReq = new WebRequest_VotingCandidates();
                    objReq.VotingCompetitionID = Convert.ToInt64(CompetitionId);
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    //objReq.IsHome = 0;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserVoting(string CandidateUniqueId, string Type, string NoOfVotes, string Mpin, string PaymentMode, string PackageId, string Amount, string BankId)

        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            int ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.Voting;
            try
            {
                if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID.ToString(), "voting  payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/AddVotingList";
                        WebRequest_AddVoting objReq = new WebRequest_AddVoting();
                        objReq.VotingCandidateUniqueId = CandidateUniqueId;
                        if (Type == "VotingPackage")
                        {
                            objReq.Type = (int)AddVotingList.Type.VotingPackage;
                            objReq.VotingPackageID = Convert.ToInt64(PackageId);
                        }
                        else
                        {
                            objReq.VotingPackageID = 0;
                            objReq.Type = (int)AddVotingList.Type.Manual;
                        }
                        objReq.NoofVotes = Convert.ToInt32(NoOfVotes);

                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        //objReq.IsHome = 0;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                result = "success";
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserNotificationList(string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getallnotifications";
                    WebRequest_Notifications objReq = new WebRequest_Notifications();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserLoadFund(string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/get-transfer-byphone";
                    WebRequest_Topup objReq = new WebRequest_Topup();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetBankDetail()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getuserbankdetail";
                    WebRequest_GetUserBankDetails objReq = new WebRequest_GetUserBankDetails();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsPrimary = true;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        public JsonResult GetBankDetailOthers()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getuserbankdetail";
                    WebRequest_GetUserBankDetails objReq = new WebRequest_GetUserBankDetails();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsPrimary = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserBankListAll(string TransferType)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    bool IsInstrument = false;
                    if (TransferType != null)
                    {
                        IsInstrument = (TransferType.ToLower() == "mbanking") || (TransferType.ToLower() == "ebanking");
                    }
                    string ApiName = "api/GetTransferBankList";
                    if (IsInstrument)
                    {
                        ApiName = "api/GetBankInstrument";
                    }
                    WebRequest_Banklistall objReq = new WebRequest_Banklistall();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    GetPaymentInstrument objResponse = new GetPaymentInstrument();
                    if (!string.IsNullOrEmpty(result))
                    {
                        objResponse = JsonConvert.DeserializeObject<GetPaymentInstrument>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            List<dataInstruments> filerdata = new List<dataInstruments>();
                            if (IsInstrument)
                            {

                                switch (TransferType.ToLower())
                                {
                                    case "ebanking":
                                        filerdata = objResponse.data.Where(x => x.BankType == "EBanking").ToList();
                                        objResponse.data = filerdata;
                                        break;
                                    case "mbanking":
                                        filerdata = objResponse.data.Where(x => x.BankType == "MBanking").ToList();
                                        objResponse.data = filerdata;
                                        break;
                                    default:
                                        break;
                                }
                                result = JsonConvert.SerializeObject(objResponse);

                            }
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetLinkBankList()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/GetLinkBankList";
                    WebRequest_Banklistall objReq = new WebRequest_Banklistall();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Details;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult GetLoadFundsURL(string TransferType, string amount, string remarks, string particulars, string code)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string URL = string.Empty;
                    WebCommonResponse objResponse = new WebCommonResponse();
                    if (Common.ApplicationEnvironment.IsProduction)
                    {
                        URL = Common.LiveSiteUrl;
                    }
                    else
                    {
                        URL = Common.TestSiteUrl;
                    }
                    int VendorApiType = 0;
                    string MemberId = Convert.ToString(Session["MyPayUserMemberId"].ToString());
                    AddCalculateServiceChargeAndCashback objRet = new AddCalculateServiceChargeAndCashback();
                    switch (TransferType)
                    {
                        case "mbanking":
                            VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Mobile_Banking;
                            RepServiceCharge.GetServiceCharge(MemberId, amount, VendorApiType.ToString(), "Web", "", true, ref objRet);
                            URL = URL + $"/userauthorizations/Banking?amount={amount}&remarks={remarks}&particulars={particulars}&code={code}&MemberId={MemberId}&servicecharge={objRet.ServiceCharge}&Type=2";
                            break;
                        case "ebanking":
                            VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Internet_Banking;
                            RepServiceCharge.GetServiceCharge(MemberId, amount, VendorApiType.ToString(), "Web", "", true, ref objRet);
                            URL = URL + $"/userauthorizations/Banking?amount={amount}&remarks={remarks}&particulars={particulars}&code={code}&MemberId={MemberId}&servicecharge={objRet.ServiceCharge}";
                            break;
                        case "cards":
                            VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit;
                            RepServiceCharge.GetServiceCharge(MemberId, amount, VendorApiType.ToString(), "Web", "", true, ref objRet);
                            URL = URL + $"/userauthorizations/card?amount={amount}&remarks={remarks}&particulars={particulars}&code={code}&MemberId={MemberId}&servicecharge={objRet.ServiceCharge}";
                            break;
                        case "cardspayment":
                            VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_debit_credit;
                            RepServiceCharge.GetServiceCharge(MemberId, amount, VendorApiType.ToString(), "Web", "", true, ref objRet);
                            URL = URL + $"/userauthorizations/cardpayment?amount={amount}&remarks={remarks}&particulars={particulars}&code={code}&MemberId={MemberId}&servicecharge={objRet.ServiceCharge}";
                            break;
                        case "cips":
                            VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_connectips;
                            RepServiceCharge.GetServiceCharge(MemberId, amount, VendorApiType.ToString(), "Web", "", true, ref objRet);
                            URL = URL + $"/userauthorizations?amount={amount}&remarks={remarks}&particulars={particulars}&code={code}&MemberId={MemberId}&servicecharge={objRet.ServiceCharge}";
                            break;
                    }
                    objResponse.Details = "/MyPayUser/MyPayUserRedirectBankPayment?otc=" + Common.Encryption(URL);
                    objResponse.Message = "success";
                    result = JsonConvert.SerializeObject(objResponse);

                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpGet]
        public ActionResult MyPayUserBankListall()
        {
            if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
            {
                string MemberID = Convert.ToString(Session["MyPayUserMemberId"]);
                bool isEmailVeriFied = Common.ISEmailVeriFied(MemberID);
                if (!isEmailVeriFied)
                {
                    return RedirectToAction("MyPayUserLinkedAccount", "MyPayUser");
                }
            }
            return View();
        }

        [HttpPost]
        public JsonResult LinkBankAccount(string BankCode, string AccountNumber)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/LinkBankAccount";
                    WebRequest_Banklistall objReq = new WebRequest_Banklistall();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.BankCode = BankCode;
                    objReq.AccountNumber = AccountNumber;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            //result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult RemoveBankAccount(string BankId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/RemoveUserBankAccount";
                    WebRequest_BankRemove objReq = new WebRequest_BankRemove();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.BankId = BankId;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            //result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult BankAccountConvertPrimary(string BankCode, string BankName, string BranchId, string BranchName, string AccountNumber)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/MakePrimaryUserBank";
                    WebRequest_BankAccountMakePrimary objReq = new WebRequest_BankAccountMakePrimary();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.BankCode = BankCode.Trim();
                    objReq.BankName = BankName.Trim();
                    objReq.BranchId = BranchId.Trim();
                    objReq.BranchName = BranchName.Trim();
                    objReq.AccountNumber = AccountNumber.Trim();
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            //result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpGet]
        public ActionResult MyPayUserLinkedBankTransfer()
        {
            return View();
        }
        [HttpPost]
        public JsonResult MyPayUserLinkedBankTransfer(string Amount, string Remarks, string BankId, string Mpin)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/LinkedBankTransfer";
                    WebRequest_LinkedBankTransfer objReq = new WebRequest_LinkedBankTransfer();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Type = 1;
                    objReq.Description = Remarks;
                    objReq.Amount = Convert.ToDecimal(Amount);
                    objReq.BankId = Convert.ToString(BankId);
                    objReq.Mpin = Common.Encryption(Mpin);
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            //result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserLinkedBankTransaction(string Amount, string VendorType, string Remarks, string BankId, string Mpin)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/LinkedBankTransfer";
                    WebRequest_LinkedBankTransfer objReq = new WebRequest_LinkedBankTransfer();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Type = 2;
                    objReq.VendorType = VendorType;
                    objReq.Description = Remarks;
                    objReq.Amount = Convert.ToDecimal(Amount);
                    objReq.BankId = Convert.ToString(BankId);
                    objReq.Mpin = Common.Encryption(Mpin);
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            //result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserLoadWallet(string Amount, string Remarks, string BankId, string Mpin)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/LinkedBankTransfer";
                    WebRequest_LinkedBankTransfer objReq = new WebRequest_LinkedBankTransfer();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Type = 1;
                    objReq.VendorType = "0";
                    objReq.Description = Remarks;
                    objReq.Amount = Convert.ToDecimal(Amount);
                    objReq.BankId = Convert.ToString(BankId);
                    objReq.Mpin = Common.Encryption(Mpin);
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            //result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpGet]
        public ActionResult MyPayUserSendMoney()
        {

            if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
            {
                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone;
                if (Request.QueryString["mode"] != null && Convert.ToString(Request.QueryString["mode"]).ToLower() == "banktransfer")
                {
                    ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                }
                if (Common.CheckServiceDown(ServiceId.ToString()))
                {
                    return RedirectToAction("MyPayUserServiceDown", "MyPayUser");
                }
                else
                {

                    if (TempData.ContainsKey("RequestFundId"))
                    {
                        string RequestFundId = TempData["RequestFundId"].ToString();

                        AddRequestFund outobject = new AddRequestFund();
                        GetRequestFund inobject = new GetRequestFund();
                        inobject.Id = Convert.ToInt64(RequestFundId);
                        inobject.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        AddRequestFund res = RepCRUD<GetRequestFund, AddRequestFund>.GetRecord(Common.StoreProcedures.sp_RequestFund_Get, inobject, outobject);
                        if (res != null && res.Id != 0)
                        {
                            ViewBag.RequestFundId = res.Id;
                            ViewBag.SenderPhoneNumber = res.SenderPhoneNumber;
                            ViewBag.Amount = res.Amount;
                        }
                    }
                }
                ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            }
            return View();
        }

        [HttpPost]
        public ActionResult MyPayUserKYCSaveImages(HttpPostedFileBase imageFile, string Type)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        string MemberId = Convert.ToString(Session["MyPayUserMemberId"]);
                        UploadImage objUploadImage = new UploadImage();
                        HttpContext objcontext = System.Web.HttpContext.Current;
                        Request.Form.GetType().BaseType.BaseType.GetField("_readOnly", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(Request.Form, false);
                        objcontext.Request.Form["Type"] = Type;
                        objcontext.Request.Form["Method"] = "";
                        objcontext.Request.Form["MemberId"] = MemberId;
                        objcontext.Request.Form["UserId"] = "Web";
                        objcontext.Request.Form["Password"] = Common.WebPassword;
                        objUploadImage.ProcessRequest(objcontext);
                        //string msg = objUploadImage.ReturnMessage;
                        result = "success";
                        if (!string.IsNullOrEmpty(result))
                        {
                            result = "";
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult MyPayUserLoadMoney()
        {

            if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
            {

            }
            return View();
        }


        [HttpGet]
        public ActionResult MyPayUserKYC()
        {

            if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
            {
                ViewBag.MemberId = Session["MyPayUserMemberId"].ToString();
                AddUserAuthorization outobjectauth = new AddUserAuthorization();
                GetUserAuthorization inobjectauth = new GetUserAuthorization();
                inobjectauth.UserName = "web";
                AddUserAuthorization resauth = RepCRUD<GetUserAuthorization, AddUserAuthorization>.GetRecord("sp_UserAuthorization_Get", inobjectauth, outobjectauth);
                if (resauth != null && resauth.Id != 0)
                {
                    ViewBag.WebUserId = resauth.UserName;
                    ViewBag.WebPassword = Common.DecryptString(resauth.Password);
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult MyPayUserKYCStep1(string StepCompleted, string FirstName, string LastName, string Gender, string Marital, string SpouseName, string FatherName, string MotherName, string GrandFatherName, string Occupation, string Nationality)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (string.IsNullOrEmpty(StepCompleted))
                    {
                        result = "Please enter step";
                    }
                    else if (string.IsNullOrEmpty(FirstName))
                    {
                        result = "Please enter FirstName";
                    }
                    else if (string.IsNullOrEmpty(LastName))
                    {
                        result = "Please enter Last Name";
                    }
                    else if (string.IsNullOrEmpty(Gender) || Gender == "0")
                    {
                        result = "Please select Gender";
                    }
                    else if (string.IsNullOrEmpty(Marital) || Marital == "0")
                    {
                        result = "Please select Merital status";
                    }
                    else if (string.IsNullOrEmpty(SpouseName) && Marital == Convert.ToString((int)AddUser.meritalstatus.Married))
                    {
                        result = "Please enter SpouseName";
                    }
                    else if (string.IsNullOrEmpty(MotherName))
                    {
                        result = "Please enter MotherName";
                    }
                    else if (string.IsNullOrEmpty(FatherName))
                    {
                        result = "Please enter FatherName";
                    }
                    else if (string.IsNullOrEmpty(GrandFatherName))
                    {
                        result = "Please enter GrandFather Name";
                    }
                    else if (string.IsNullOrEmpty(Occupation) || Occupation == "0")
                    {
                        result = "Please select Occupation";
                    }
                    else if (string.IsNullOrEmpty(Nationality) || Nationality == "0")
                    {
                        result = "Please select Nationality";
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        string ApiName = "api/CompleteUserProfile";
                        WebRequest_UserKyc objReq = new WebRequest_UserKyc();
                        objReq.StepCompleted = StepCompleted;
                        objReq.FirstName = FirstName;
                        objReq.LastName = LastName;
                        objReq.Gender = Convert.ToInt32(Gender);
                        objReq.MeritalStatus = Convert.ToInt32(Marital);
                        objReq.SpouseName = SpouseName;
                        objReq.FatherName = FatherName;
                        objReq.MotherName = MotherName;
                        objReq.GrandFatherName = GrandFatherName;
                        objReq.Occupation = Occupation;
                        objReq.Nationality = Convert.ToInt32(Nationality);
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                            {
                                result = "success";
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserKYCStep2(string StepCompleted, string StateId, string State, string DistrictId, string District, string MunicipalityId, string Municipality, string WardNumber, string StreetName, string HouseNumber, string Occupation)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (string.IsNullOrEmpty(StepCompleted))
                    {
                        result = "Please enter step";
                    }
                    else if (string.IsNullOrEmpty(StateId) || StateId == "0" || string.IsNullOrEmpty(State))
                    {
                        result = "Please select  state";
                    }
                    else if (string.IsNullOrEmpty(DistrictId) || DistrictId == "0" || string.IsNullOrEmpty(District))
                    {
                        result = "Please select  district";
                    }
                    else if (string.IsNullOrEmpty(MunicipalityId) || MunicipalityId == "0" || string.IsNullOrEmpty(Municipality))
                    {
                        result = "Please select  municipality";
                    }
                    else if (string.IsNullOrEmpty(WardNumber))
                    {
                        result = "Please enter ward number";
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        string ApiName = "api/CompleteUserProfile";
                        WebRequest_UserKyc objReq = new WebRequest_UserKyc();
                        objReq.StepCompleted = StepCompleted;
                        objReq.StateId = Convert.ToInt32(StateId);
                        objReq.DistrictId = Convert.ToInt32(DistrictId);
                        objReq.MunicipalityId = Convert.ToInt32(MunicipalityId);
                        objReq.State = State;
                        objReq.District = District;
                        objReq.Municipality = Municipality;
                        objReq.WardNumber = WardNumber;
                        objReq.StreetName = StreetName;
                        objReq.HouseNumber = HouseNumber;
                        objReq.CurrentStateId = Convert.ToInt32(StateId);
                        objReq.CurrentDistrictId = Convert.ToInt32(DistrictId);
                        objReq.CurrentMunicipalityId = Convert.ToInt32(MunicipalityId);
                        objReq.CurrentState = State;
                        objReq.CurrentDistrict = District;
                        objReq.CurrentMunicipality = Municipality;
                        objReq.CurrentWardNumber = WardNumber;
                        objReq.CurrentStreetName = StreetName;
                        objReq.CurrentHouseNumber = HouseNumber;
                        objReq.Occupation = Occupation;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                            {
                                result = "success";
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserKYCStep3(string StepCompleted, string DocType, string Occupation)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (string.IsNullOrEmpty(StepCompleted))
                    {
                        result = "Please enter step";
                    }
                    else if (string.IsNullOrEmpty(DocType) || DocType == "0")
                    {
                        result = "Please select  your proof type";
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        string ApiName = "api/CompleteUserProfile";
                        WebRequest_UserKyc objReq = new WebRequest_UserKyc();
                        objReq.StepCompleted = StepCompleted;
                        objReq.ProofType = Convert.ToInt32(DocType);
                        objReq.Occupation = Occupation;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                            {
                                result = "success";
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult GetOccupation()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getoccupation";
                    WebRequest_Profile objReq = new WebRequest_Profile();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetState()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getstate";
                    WebRequest_Profile objReq = new WebRequest_Profile();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetDistrict(string ProvinceCode)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getdistrict";
                    WebRequest_District objReq = new WebRequest_District();
                    objReq.ProvinceCode = ProvinceCode;
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    //objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetMunicipality(string DistrictCode)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getlocalLevel";
                    WebRequest_Municipality objReq = new WebRequest_Municipality();
                    objReq.DistrictCode = DistrictCode;
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    //objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult GetPurpose()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/GetPurpose";
                    WebRequest_Profile objReq = new WebRequest_Profile();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult AccountValidation(string BankId, string AccountId, string AccountName)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/AccountValidation";
                    WebRequest_SendMoney objReq = new WebRequest_SendMoney();
                    objReq.BankId = Convert.ToString(BankId);
                    objReq.AccountId = Convert.ToString(AccountId);
                    objReq.AccountName = AccountName;
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult RecipientDetail(string RecipientPhone)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/RecipientDetail";
                    WebRequest_SendMoney objReq = new WebRequest_SendMoney();
                    objReq.RecipientPhone = RecipientPhone;
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult TransferByPhone(string RecipientPhone, string Amount, string Remarks, string Mpin, string UniqueCustomerId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/TransferByPhone";
                    WebRequest_SendMoney objReq = new WebRequest_SendMoney();
                    objReq.RecipientPhone = RecipientPhone;
                    objReq.Amount = Convert.ToDecimal(Amount);
                    objReq.Remarks = Common.HTMLToPlainText(Remarks);
                    objReq.UniqueCustomerId = UniqueCustomerId;
                    objReq.Mpin = Common.Encryption(Mpin);
                    objReq.Pin = Common.Encryption(Mpin);
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.Referenceno = Common.RandomNumber(100000, 999999).ToString() + Common.RandomNumber(100000, 999999).ToString();
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        TempData["IsTransferByPhone"] = "Yes";
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status)
                        {
                            objResponse.responseMessage = "success";
                        }
                        result = JsonConvert.SerializeObject(objResponse);
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult RequestTransferByphone(string RecipientPhone, string Amount, string Remarks, string Mpin, string UniqueCustomerId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/request-transfer-byphone";
                    WebRequest_SendMoney objReq = new WebRequest_SendMoney();
                    objReq.RecipientPhone = RecipientPhone;
                    objReq.Amount = Convert.ToDecimal(Amount);
                    objReq.Remarks = Common.HTMLToPlainText(Remarks);
                    objReq.UniqueCustomerId = UniqueCustomerId;
                    objReq.Pin = Common.Encryption(Mpin);
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        TempData["IsRequestFund"] = "Yes";
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status)
                        {
                            objResponse.responseMessage = "success";
                        }
                        result = JsonConvert.SerializeObject(objResponse);

                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult TransferByPhoneReject(string UniqueCustomerId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/transfer-byphone-reject";
                    WebRequest_SendMoney objReq = new WebRequest_SendMoney();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.UniqueCustomerId = UniqueCustomerId;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult TransferByPhoneAccept(string UniqueCustomerId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    AddRequestFund outobject = new AddRequestFund();
                    GetRequestFund inobject = new GetRequestFund();
                    inobject.Id = Convert.ToInt64(UniqueCustomerId);
                    inobject.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    AddRequestFund res = RepCRUD<GetRequestFund, AddRequestFund>.GetRecord(Common.StoreProcedures.sp_RequestFund_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        TempData["RequestFundId"] = UniqueCustomerId;
                        result = "success";
                    }
                    else
                    {
                        result = "failed";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult BankTransfer(string Name, string AccountNumber, string BranchName, string BankName, string BankId, string BranchId, string Amount, string Description, string Mpin)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/BankTransfer";
                    WebRequest_SendMoney objReq = new WebRequest_SendMoney();
                    objReq.Name = Name;
                    objReq.AccountNumber = Convert.ToString(AccountNumber);
                    objReq.BranchName = BranchName;
                    objReq.BankName = BankName;
                    objReq.BankId = Convert.ToString(BankId);
                    objReq.BranchId = Convert.ToString(BranchId);
                    objReq.Amount = Convert.ToDecimal(Amount);
                    objReq.Description = Common.HTMLToPlainText(Description);
                    objReq.Mpin = Common.Encryption(Mpin);
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult MyPayUserCoins()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MyPayUserEarnedCoinsList(string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/rewardpoints";
                    WebRequest_EarnedCoins objReq = new WebRequest_EarnedCoins();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserRedeemedCoinsList(string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/rewardpoints";
                    WebRequest_EarnedCoins objReq = new WebRequest_EarnedCoins();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult MyPayUserDataPack()
        {
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.Title == "Datapack").ToList();
            ViewBag.Providerlist = objRes;
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NTC;
            AddUser model = new AddUser();
            return View(model);
        }

        [HttpPost]
        public JsonResult MyPayUserDataPackDetails(int ServiceId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        if (Common.CheckServiceDown(ServiceId.ToString()))
                        {
                            result = Common.ServiceDown;
                        }
                        else
                        {
                            WebRes_DataPack objResponse = new WebRes_DataPack();
                            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                            GetVendor_API_DataPack_Lookup objRes = new GetVendor_API_DataPack_Lookup();
                            string Reference = new CommonHelpers().GenerateUniqueId();
                            string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                            string msg = "";
                            if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NTC)
                            {
                                msg = RepKhalti.RequestServiceGroup_DATAPACK_LOOKUP_NTC(ref objVendor_API_Requests, Reference, "1", deviceId, "Web", ref objRes, true);
                            }
                            else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NCell)
                            {
                                msg = RepKhalti.RequestServiceGroup_DATAPACK_LOOKUP_NCELL(ref objVendor_API_Requests, Reference, "1", deviceId, "Web", ref objRes, true);
                            }

                            if (msg.ToLower() == "success")
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                objResponse = JsonConvert.DeserializeObject<WebRes_DataPack>(result);
                                objResponse.detail = objRes.detail;
                                objResponse.packages = objRes.detail.packages;
                                objResponse.ReponseCode = objRes.status ? 1 : 0;
                                objResponse.status = objRes.status;
                                objResponse.Message = "success";

                                result = JsonConvert.SerializeObject(objResponse);
                                if (objResponse.status && objResponse.Message.ToLower() == "success")
                                {

                                }
                                else if (objResponse.ReponseCode == 7)
                                {
                                    RepMyPayUserLogin.Logout();
                                    result = objResponse.Message;
                                }
                                else
                                {
                                    result = objResponse.Message;
                                }
                            }
                            else
                            {
                                result = "Invalid API Request";
                            }
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserDataPack(string ServiceID, string MobileNumber, string Amount, string Mpin, string PaymentMode, string ProductCode, string PackageId, string VendorJsonLookup, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceID) || ServiceID == "")
                {
                    result = "Please Select ServiceID";
                }
                else if (string.IsNullOrEmpty(MobileNumber) || MobileNumber == "")
                {
                    result = "Please Enter MobileNumber";
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID, "Data pack payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "";
                        if ((ServiceID == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NTC).ToString()) || (ServiceID == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_VoicePack_NTC).ToString()) || (ServiceID == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_SMSPack_NTC).ToString()))
                        {
                            ApiName = "api/use-datapack-ntc";
                        }
                        else if (ServiceID == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NCell).ToString())
                        {
                            ApiName = "api/use-datapack-ncell";
                        }

                        WebRequest_DataPack objReq = new WebRequest_DataPack();
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        //objReq.ServiceId = Convert.ToInt64(ServiceID);
                        objReq.Amount = Amount;
                        objReq.Number = MobileNumber;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.ProductCode = ProductCode;
                        //objReq.ProductType = ProductType;
                        objReq.PackageId = PackageId;
                        objReq.UniqueCustomerId = MobileNumber;
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.CouponCode = CouponCode;

                        var DataPackJson = JsonConvert.DeserializeObject<datapack_packages>(VendorJsonLookup);
                        DataPackDetail dataPackDetail = new DataPackDetail();
                        dataPackDetail.PriorityNo = Convert.ToInt32(DataPackJson.priority_no);
                        dataPackDetail.ProductName = DataPackJson.product_name;
                        dataPackDetail.Amount = Convert.ToDecimal(DataPackJson.amount);
                        dataPackDetail.ShortDetail = DataPackJson.short_detail;
                        dataPackDetail.ProductCode = DataPackJson.product_code;
                        dataPackDetail.Description = DataPackJson.description;
                        dataPackDetail.ProductType = DataPackJson.product_type;
                        dataPackDetail.PackageId = DataPackJson.package_id == "" ? 0 : Convert.ToInt64(Convert.ToDecimal((DataPackJson.package_id)));
                        dataPackDetail.Validity = DataPackJson.validity;


                        objReq.VendorJsonLookup = JsonConvert.SerializeObject(dataPackDetail);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult MyPayUserTV()
        {
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.Title == "TV").ToList();
            ViewBag.TVList = objRes;
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.ServiceId = "";
            AddUser model = new AddUser();
            return View(model);
        }


        [HttpPost]
        public JsonResult MyPayUserTVDetails(int ServiceId, string CasId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRes_TV objResponse = new WebRes_TV();
                        string Reference = new CommonHelpers().GenerateUniqueId();
                        string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                        string msg = "";
                        if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_dishhome)
                        {
                            GetVendor_API_Dishome_Lookup objRes = new GetVendor_API_Dishome_Lookup();
                            msg = RepKhalti.RequestServiceGroup_DISHOME_LOOKUP(CasId, "1", deviceId, "Web", ref objRes, true);
                            if (msg == "success")
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                //objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);
                                objResponse.data_dishhome = objRes.data;
                            }
                            else
                            {
                                msg = "Please enter valid CAS ID/Chip ID/Account";
                            }
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = msg;
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_simtv)
                        {

                            GetVendor_API_SIMTV_Lookup objRes = new GetVendor_API_SIMTV_Lookup();
                            msg = RepKhalti.RequestServiceGroup_SIMTV_LOOKUP(CasId, "1", deviceId, "Web", ref objRes);
                            if (msg == "success")
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);
                                objResponse.data_simtv = objRes.data;
                            }
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = msg;
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_mero)
                        {

                            GetVendor_API_MEROTV_User_Lookup objRes = new GetVendor_API_MEROTV_User_Lookup();
                            msg = RepKhalti.RequestServiceGroup_MEROTV_USER_LOOKUP(CasId, Reference, "1", deviceId, "Web", ref objRes);
                            //MeroTvUserLookup(CasId, ServiceId);
                            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
                            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
                            ViewBag.ServiceId = ServiceId;
                            //object BusRoutes = Session["BusRoute"];
                            string ApiName = "api/lookup-tv-merotv";
                            ReqVendor_API_MEROTV_User_Lookup objReq = new ReqVendor_API_MEROTV_User_Lookup();
                            objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                            objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                            objReq.PlatForm = "Web";
                            objReq.customer_id = CasId;
                            objReq.SecretKey = Common.SecretKeyForWebAPICall;
                            string JSON = JsonConvert.SerializeObject(objReq);
                            string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                            result = Common.RequestMeroTvAPI(ApiName, JSON, JwtToken);
                            objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);

                            if (objResponse.Message.ToLower() == "success")
                            {
                                objRes = JsonConvert.DeserializeObject<GetVendor_API_MEROTV_User_Lookup>(result);
                                result = JsonConvert.SerializeObject(objRes);
                                //objResponse.data_merotv = objRes.offer_list;
                                objResponse.first_name = objRes.customer.first_name;
                                objResponse.last_name = objRes.customer.last_name;
                                //objResponse.wallet_balance = objRes.wallet_balance;
                                objResponse.session_id = objRes.session_id;
                            }
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            msg = objResponse.Message;
                            return Json(result);
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_cleartv)
                        {

                            GetVendor_API_SIMTV_Lookup objRes = new GetVendor_API_SIMTV_Lookup();
                            msg = RepKhalti.RequestServiceGroup_SIMTV_LOOKUP(CasId, "1", deviceId, "Web", ref objRes);
                            if (msg == "success")
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);
                                objResponse.data_simtv = objRes.data;
                            }
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = msg;
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_maxtv)
                        {

                            string reference = Common.GenerateReferenceUniqueID();
                            GetVendor_API_MAXTV_Lookup objRes = new GetVendor_API_MAXTV_Lookup();
                            msg = RepKhalti.RequestServiceGroup_MAXTV_LOOKUP(CasId, reference, "1", deviceId, "Web", ref objRes);
                            if (msg == "success")
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);
                                objResponse.amount = objRes.amount;
                                objResponse.cid_stb_smartcard = objRes.cid_stb_smartcard;
                                objResponse.stb_no = objRes.tvs[0].stb_no;
                                objResponse.smartcard_no = objRes.tvs[0].smartcard_no;

                            }
                            else
                            {
                                msg = "Please enter valid Customer ID/Card Number";
                            }
                            objResponse.Message = msg;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;

                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_prabhutv)
                        {
                            string reference = Common.GenerateReferenceUniqueID();
                            GetVendor_API_PRABHUTV_Lookup objRes = new GetVendor_API_PRABHUTV_Lookup();
                            msg = RepKhalti.RequestServiceGroup_PRABHUTV_LOOKUP(CasId, reference, "1", deviceId, "Web", ref objRes);
                            if (msg == "success")
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);
                                objResponse.stb_count = objRes.stb_count;
                                objResponse.customer_id = objRes.customer_id;
                                objResponse.expiry_date = objRes.current_packages[0].expiry_date;
                                objResponse.product_name = objRes.current_packages[0].product_name;
                            }
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = msg;
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_png_network_tv)
                        {

                            GetVendor_API_PNGNETWORKTV_Lookup objRes = new GetVendor_API_PNGNETWORKTV_Lookup();
                            msg = RepKhalti.RequestServiceGroup_PNGNETWORKTV_LOOKUP(CasId, "1", deviceId, "Web", ref objRes);
                            if (msg == "success")
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);
                            }
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = msg;
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_jagrititv)
                        {

                            GetVendor_API_JAGRITITV_Lookup objRes = new GetVendor_API_JAGRITITV_Lookup();
                            msg = RepKhalti.RequestServiceGroup_JAGRITITV_LOOKUP(CasId, "1", deviceId, "Web", ref objRes);
                            if (msg == "success")
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);
                            }
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = msg;
                        }

                        if (msg.ToLower() == "success")
                        {


                            result = JsonConvert.SerializeObject(objResponse);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = msg;
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserMeroTvPackage(string ServiceId, string stb, string sessionId)
        {
            string result = string.Empty;
            //MeroTvUserLookup(CasId, ServiceId);
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.ServiceId = ServiceId;
            //object BusRoutes = Session["BusRoute"];
            string ApiName = "api/lookup-tv-merotv-packages";
            ReqVendor_API_MEROTV_User_Lookup objReq = new ReqVendor_API_MEROTV_User_Lookup();
            objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
            objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
            objReq.PlatForm = "Web";
            objReq.SecretKey = Common.SecretKeyForWebAPICall;
            objReq.stb = stb;
            objReq.session_id = sessionId;
            string JSON = JsonConvert.SerializeObject(objReq);
            string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
            result = Common.RequestMeroTvAPI(ApiName, JSON, JwtToken);
            //objResponse = JsonConvert.DeserializeObject<WebRes_TV>(result);
            objReq = JsonConvert.DeserializeObject<ReqVendor_API_MEROTV_User_Lookup>(result);


            //if (objRes.Message.ToLower() == "success")
            //{
            //    result = JsonConvert.SerializeObject(objRes);
            //    //objResponse.data_merotv = objRes.offer_list;
            //    objResponse.first_name = objRes.customer.first_name;
            //    objResponse.last_name = objRes.customer.last_name;
            //    //objResponse.wallet_balance = objRes.wallet_balance;
            //    objResponse.session_id = objRes.session_id;
            //}
            //objResponse.ReponseCode = objRes.status ? 1 : 0;
            //objResponse.status = objRes.status;
            //msg = objResponse.Message;
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserBookingDetail(string Take, string Skip, string BookingId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/get-flight-booking-details";
                    WebRequest_Flights objReq = new WebRequest_Flights();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.BookingID = Convert.ToInt64(BookingId);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    //objReq.BookingID = 0;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebRes_Flights objResponse = new WebRes_Flights();
                        objResponse = JsonConvert.DeserializeObject<WebRes_Flights>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserSubmitFeedback(string Message, string Rating)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/add-userfeedback";
                    WebRequest_AddFeedback objReq = new WebRequest_AddFeedback();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                    objReq.Subject = Rating;
                    objReq.UserMessage = Message;
                    //objReq.Subject = "";
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.ReponseCode == 1)
                        {
                            result = "success";
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }



        [HttpGet]
        public ActionResult MyPayUserRefernEarn()
        {
            AddUser model = new AddUser();
            return View(model);
        }

        [HttpPost]
        public JsonResult MyPayUserTVAmountDetails(string ServiceId)
        {
            var result = string.Empty;
            List<GetService_Providers> objRes = new List<GetService_Providers>();
            try
            {
                objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.ProviderTypeId == ServiceId).ToList();

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(objRes);
        }

        [HttpPost]
        public JsonResult MyPayUserTV(string ServiceId, string CasId, string Amount, string Mpin, string PaymentMode, string SessionId, string PackageId, string UserName, string ContactNumber, string STB_OR_CAS_ID, string WardNumber, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceId) || ServiceId == "")
                {
                    result = "Please Select ServiceID";
                }
                else if ((string.IsNullOrEmpty(CasId) || CasId == "") && (ServiceId != ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_mero).ToString()))
                {
                    if ((ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_dishhome).ToString()) || (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_simtv).ToString()))
                    {
                        result = "Please Enter CAS ID/Chip ID/Account";
                    }
                    else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_cleartv).ToString())
                    {
                        result = "Please Enter Subscriber Id";
                    }
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please select Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if (string.IsNullOrEmpty(Mpin) || Mpin == "" || Mpin == "0")
                {
                    result = "Please Enter Mpin";
                }
                else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_mero).ToString())
                {
                    if (string.IsNullOrEmpty(SessionId) || SessionId == "")
                    {
                        result = "Please Enter SessionId";
                    }
                    else if (string.IsNullOrEmpty(PackageId) || PackageId == "" || PackageId == "0")
                    {
                        result = "Please select Package";
                    }
                }
                else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_cleartv).ToString())
                {
                    if (string.IsNullOrEmpty(PackageId) || PackageId == "" || PackageId == "0")
                    {
                        result = "Please enter Mobile Number";
                    }
                }
                else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_prabhutv).ToString())
                {
                    if (string.IsNullOrEmpty(SessionId) || SessionId == "")
                    {
                        result = "Please Enter SessionId";
                    }

                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceId, "Tv payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_TV objReq = new WebRequest_TV();
                        string ApiName = "";
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_dishhome).ToString())
                        {
                            ApiName = "api/use-tv-dishhome";
                            objReq.CasId = CasId;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_simtv).ToString())
                        {
                            ApiName = "api/use-tv-simtv";
                            objReq.CustomerId = CasId;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_mero).ToString())
                        {
                            ApiName = "api/use-tv-merotv";
                            objReq.PackageId = PackageId;
                            objReq.SessionId = SessionId;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_maxtv).ToString())
                        {
                            ApiName = "api/use-tv-maxtv";
                            objReq.SessionId = SessionId;
                            objReq.CustomerId = CasId;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_cleartv).ToString())
                        {
                            ApiName = "api/use-tv-cleartv";
                            objReq.CustomerId = CasId;
                            objReq.Number = PackageId;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_jagrititv).ToString())
                        {
                            ApiName = "api/use-tv-jagrititv";
                            objReq.CustomerId = CasId;
                            objReq.CustomerName = UserName;
                            objReq.Package = PackageId;
                            objReq.STB_OR_CAS_ID = STB_OR_CAS_ID;
                            objReq.Old_Ward_Number = WardNumber;
                            objReq.Mobile_Number_1 = ContactNumber;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_png_network_tv).ToString())
                        {
                            ApiName = "api/use-tv-pngnetworktv";
                            objReq.Package = PackageId;
                            objReq.UserName = UserName;
                            objReq.CustomerName = CasId;
                            objReq.ContactNumber = ContactNumber;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_prabhutv).ToString())
                        {
                            ApiName = "api/use-tv-prabhutv";
                            objReq.SessionId = SessionId;
                            objReq.CAS_ID = CasId;
                        }

                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.Amount = Amount;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.Pin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.CouponCode = CouponCode;
                        objReq.SessionId = SessionId;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpGet]

        public ActionResult MyPayUserEchallan()
        {
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.ProviderTypeId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine).ToString()).ToList();
            ViewBag.Providerlist = objRes;
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine;

            if (Common.CheckServiceDown(ViewBag.ServiceId.ToString()))
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");

            }
            List<SelectListItem> objYearList = new List<SelectListItem>();

            SelectListItem objYear = new SelectListItem();
            objYear.Text = "Select Fiscal year";
            objYear.Value = "0";
            objYear.Selected = true;
            objYearList.Add(objYear);
            int CurrentYear = System.DateTime.Now.Year - 2000;
            int NepalCalAdjust = 58;
            for (int i = CurrentYear; i > (CurrentYear - 3); i--)
            {
                objYear = new SelectListItem();
                objYear.Text = ((CurrentYear + (NepalCalAdjust - 1)).ToString() + "/" + (CurrentYear + NepalCalAdjust).ToString());
                objYear.Value = ((CurrentYear + (NepalCalAdjust - 1)).ToString() + (CurrentYear + NepalCalAdjust).ToString());
                NepalCalAdjust = NepalCalAdjust - 1;
                objYearList.Add(objYear);
            }
            ViewBag.objYearList = objYearList;

            GetVendor_API_EChalan_Lookup_DistrictCode objResDistrictCode = new GetVendor_API_EChalan_Lookup_DistrictCode();
            string Reference = new CommonHelpers().GenerateUniqueId();
            WebCommonProp objCommonReq = new WebCommonProp();
            objCommonReq.PlatForm = "Web";
            objCommonReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
            string msg = RepKhalti.RequestServiceGroup_EChalan_DISTRICT_CODES_LOOKUP(Reference, objCommonReq.Version, objCommonReq.DeviceCode, objCommonReq.PlatForm, ref objResDistrictCode);
            List<SelectListItem> provinceList = new List<SelectListItem>();
            SelectListItem province = new SelectListItem();
            province.Text = "Select Province";
            province.Value = "0";
            province.Selected = true;
            provinceList.Add(province);
            if (msg.ToLower() == "success")
            {
                int locationCount = objResDistrictCode.locations.Count;
                for (int i = 0; i < objResDistrictCode.locations.Count; i++)
                {
                    province = new SelectListItem();
                    province.Text = objResDistrictCode.locations[i].province_name;
                    province.Value = objResDistrictCode.locations[i].province_code;
                    provinceList.Add(province);
                }
            }
            ViewBag.provinceList = provinceList;
            ViewBag.provinceListJson = JsonConvert.SerializeObject(objResDistrictCode);

            return View();
        }


        [HttpPost]
        public JsonResult MyPayUserEchallanDetails(int ServiceId, string VoucherNo, string FiscalYear, string ProvinceCode, string DistrictCode)
        {
            var result = string.Empty;
            try
            {
                if (ServiceId < 1)
                {
                    result = "Please Select ServiceID";
                }

                else if (string.IsNullOrEmpty(VoucherNo) || VoucherNo == "")
                {
                    result = "Please Enter Chit Number";
                }
                else if (string.IsNullOrEmpty(FiscalYear) || FiscalYear == "")
                {
                    result = "Please Select FiscalYear";
                }
                else if (string.IsNullOrEmpty(ProvinceCode) || ProvinceCode == "")
                {
                    result = "Please Select ProvinceCode";
                }
                else if (string.IsNullOrEmpty(DistrictCode) || DistrictCode == "")
                {
                    result = "Please Enter DistrictCode";
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {

                        WebRes_Echallan objResponse = new WebRes_Echallan();
                        string Reference = new CommonHelpers().GenerateUniqueId();
                        string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                        string service = "echalan";
                        string appId = "MER-7-APP-10";
                        WebCommonProp objCommonReq = new WebCommonProp();
                        objCommonReq.PlatForm = "Web";
                        objCommonReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        string msg = "";
                        if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine)
                        {
                            GetVendor_API_EChalan_Lookup objRes = new GetVendor_API_EChalan_Lookup();
                            msg = RepKhalti.RequestServiceGroup_EChalan_LOOKUP(Reference, appId, VoucherNo, service, FiscalYear, ProvinceCode, DistrictCode, objCommonReq.Version, objCommonReq.DeviceCode, objCommonReq.PlatForm, ref objRes);
                            result = JsonConvert.SerializeObject(objRes);
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";

                            if (msg.ToLower() == "success")
                            {
                                objResponse = JsonConvert.DeserializeObject<WebRes_Echallan>(result);
                                objResponse.CreditorName = objRes.creditor_name;
                                objResponse.ChitNumber = objRes.chit_number;
                                objResponse.FullName = objRes.full_name;
                                objResponse.Description = objRes.description;
                                objResponse.EbpNumber = objRes.ebp_number;
                                objResponse.Amout = objRes.amount;
                                objResponse.Session_Id = objRes.session_id;
                                objResponse.Reference = Reference;
                                result = JsonConvert.SerializeObject(objResponse);


                                if (objResponse.ReponseCode == 7)
                                {
                                    RepMyPayUserLogin.Logout();
                                    result = objResponse.Message;
                                }

                            }
                            else
                            {
                                result = "Invalid API Request";
                            }
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserEchallan(string ServiceID, string Amount, string Mpin, string PaymentMode, string SessionId, string Reference, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {

                if (string.IsNullOrEmpty(ServiceID) || ServiceID == "")
                {
                    result = "Please Select ServiceID";
                }

                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(Mpin) || Mpin == "")
                {
                    result = "Please Enter Mpin";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "")
                {
                    result = "Please Select PaymentMode";
                }
                else if (string.IsNullOrEmpty(SessionId) || SessionId == "")
                {
                    result = "Please Enter SessionId";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID, "Challan payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }

                if (string.IsNullOrEmpty(result))
                {

                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/use-echalan";
                        WebRequest_Echallan objReq = new WebRequest_Echallan();
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.Amount = Amount;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.CouponCode = CouponCode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.Session_id = SessionId;
                        objReq.Version = "1.0";
                        objReq.Remarks = "";
                        objReq.TimeStamp = "";
                        objReq.Hash = "";
                        objReq.Reference = Reference;

                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);

        }
        [HttpGet]

        public ActionResult MyPayUserElectricity()
        {
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nea;
            if (Common.CheckServiceDown(ViewBag.ServiceId.ToString()))
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");

            }
            List<SelectListItem> objCounterList = new List<SelectListItem>();
            SelectListItem objCounter = new SelectListItem();


            WebCommonProp objCommonReq = new WebCommonProp();
            objCommonReq.PlatForm = "Web";
            objCommonReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;


            GetVendor_API_Nea_Counters objRes = new GetVendor_API_Nea_Counters();
            string msg = RepKhalti.RequestServiceGroup_COUNTER_NEA(objCommonReq.Version, objCommonReq.DeviceCode, objCommonReq.PlatForm, ref objRes);
            if (msg.ToLower() == "success")
            {
                for (int i = 0; i < objRes.counters.Count; i++)
                {
                    objCounter = new SelectListItem();
                    objCounter.Text = objRes.counters[i].name;
                    objCounter.Value = objRes.counters[i].value;
                    objCounterList.Add(objCounter);
                }
            }
            ViewBag.objCounterList = objCounterList;
            return View();
        }

        [HttpPost]
        public JsonResult MyPayUserElectricityDetails(int ServiceId, string Counter, string ScNumber, string ConsumerId)
        {
            var result = string.Empty;
            try
            {
                if (ServiceId < 1)
                {
                    result = "Please Select ServiceID";
                }
                else if (string.IsNullOrEmpty(Counter) || Counter == "")
                {
                    result = "Please Select Counter";
                }
                else if (string.IsNullOrEmpty(ScNumber) || ScNumber == "")
                {
                    result = "Please Enter ScNumber";
                }
                else if (string.IsNullOrEmpty(ConsumerId) || ConsumerId == "")
                {
                    result = "Please Enter ConsumerId";
                }

                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRes_Electricity objResponse = new WebRes_Electricity();
                        string Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = "";
                        if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nea)
                        {
                            GetVendor_API_ServiceGroup_Nea_Details objRes = new GetVendor_API_ServiceGroup_Nea_Details();
                            msg = RepKhalti.RequestServiceGroup_DETAILS_NEA(ScNumber, ConsumerId, Reference, Counter, ref objRes);
                            result = JsonConvert.SerializeObject(objRes);
                            TempData["BillDetail"] = result;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            if (msg.ToLower() == "success")
                            {
                                objResponse = JsonConvert.DeserializeObject<WebRes_Electricity>(result);
                                result = JsonConvert.SerializeObject(objResponse);

                                if (objResponse.ReponseCode == 7)
                                {
                                    RepMyPayUserLogin.Logout();
                                    result = objResponse.Message;
                                }
                            }
                            else
                            {
                                result = msg;
                            }
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserElectricity(string ServiceID, string Amount, string Mpin, string PaymentMode, string SessionId, string Counter, string ScNumber, string ConsumerId, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceID) || ServiceID == "")
                {
                    result = "Please Select ServiceID";
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(Mpin) || Mpin == "")
                {
                    result = "Please Enter Mpin";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "")
                {
                    result = "Please Select PaymentMode";
                }
                else if (string.IsNullOrEmpty(SessionId) || SessionId == "")
                {
                    result = "Please Enter SessionId";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID, "Electricity payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/servicegroup-commit-nea";
                        WebRequest_Electricity objReq = new WebRequest_Electricity();
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.Amount = Amount;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.SessionId = SessionId;
                        objReq.Version = "1.0";
                        objReq.TimeStamp = "";
                        objReq.Hash = "";
                        objReq.UniqueCustomerId = ConsumerId;
                        objReq.ReferenceNo = new CommonHelpers().GenerateUniqueId();


                        WebRes_Electricity objBillDetail = new WebRes_Electricity();
                        string BillDetail;
                        VendorJsonLookupItemsElecticityNEA objNea = new VendorJsonLookupItemsElecticityNEA();
                        objNea.Due_Bills = new List<ElecticityNEADueBills>();
                        if (TempData.ContainsKey("BillDetail"))
                        {
                            BillDetail = TempData["BillDetail"].ToString();
                            objBillDetail = JsonConvert.DeserializeObject<WebRes_Electricity>(BillDetail);
                            objNea.Consumer_Name = objBillDetail.consumer_name;
                            objNea.Counter = Counter;
                            objNea.SC_NO = ScNumber;
                            objNea.Customer_Id = ConsumerId;
                            objNea.Total_Due_Amount = objBillDetail.total_due_amount;
                            objNea.Status = objBillDetail.status;
                            objNea.Session_Id = SessionId;
                            ElecticityNEADueBills electicityNEADue = new ElecticityNEADueBills();
                            foreach (var bill in objBillDetail.due_bills)
                            {
                                electicityNEADue = new ElecticityNEADueBills();
                                electicityNEADue.Bill_Amount = bill.bill_amount;
                                electicityNEADue.Bill_Date = bill.bill_date;
                                electicityNEADue.Days = bill.days;
                                electicityNEADue.Due_Bill_Of = bill.due_bill_of;
                                electicityNEADue.Payable_Amount = bill.payable_amount;
                                electicityNEADue.Status = bill.status;
                                objNea.Due_Bills.Add(electicityNEADue);
                            }
                        }
                        objReq.VendorJsonLookup = JsonConvert.SerializeObject(objNea);
                        objReq.CouponCode = CouponCode;

                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);

        }
        [HttpGet]
        public JsonResult MyPayUserProvinceList()
        {
            GetVendor_API_EChalan_Lookup_DistrictCode objResDistrictCode = new GetVendor_API_EChalan_Lookup_DistrictCode();
            string Reference = new CommonHelpers().GenerateUniqueId();
            WebCommonProp objCommonReq = new WebCommonProp();
            objCommonReq.PlatForm = "Web";
            objCommonReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
            string msg = RepKhalti.RequestServiceGroup_EChalan_DISTRICT_CODES_LOOKUP(Reference, objCommonReq.Version, objCommonReq.DeviceCode, objCommonReq.PlatForm, ref objResDistrictCode);
            List<SelectListItem> provinceList = new List<SelectListItem>();
            SelectListItem province = new SelectListItem();
            if (msg.ToLower() == "success")
            {
                int locationCount = objResDistrictCode.locations.Count;
                for (int i = 0; i < objResDistrictCode.locations.Count; i++)
                {
                    province = new SelectListItem();
                    province.Text = objResDistrictCode.locations[i].province_name;
                    province.Value = objResDistrictCode.locations[i].province_code;
                    provinceList.Add(province);
                }
            }


            return Json(provinceList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult MyPayUserInsurance()
        {
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.Title == "Insurance").ToList();
            ViewBag.InsuranceList = objRes;
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.ServiceId = "";
            AddUser model = new AddUser();
            return View(model);
        }

        [HttpPost]
        public JsonResult MyPayUserInsuranceDetails(int ServiceId, string InsuranceType, string DebitNote, string PolicyNumber, string DOB, string RequestId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRes_Insurance objResponse = new WebRes_Insurance();
                        string Reference = new CommonHelpers().GenerateUniqueId();
                        string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                        string msg = "";
                        string insuranceslug = "";
                        string ProviderEnumName = Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), ServiceId).ToString().ToUpper().Replace("_PNG_", "_P&G_").Replace("KHALTI_", "").Replace("_", " ");

                        if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Neco)
                        {
                            GetVendor_API_NecoInsurance_Lookup objRes = new GetVendor_API_NecoInsurance_Lookup();
                            msg = RepKhalti.RequestServiceGroup_NecoInsurance_Lookup(InsuranceType, "1", deviceId, "Web", ref objRes);
                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.PolicyCategories = objRes.policy_categories;
                            objResponse.Branches = objRes.branches;
                            //objResponse.data_dishhome = objRes.data;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico)
                        {
                            GetVendor_API_SagarmathaInsurance_Lookup objRes = new GetVendor_API_SagarmathaInsurance_Lookup();
                            msg = RepKhalti.RequestServiceGroup_SagarmathaInsurance_Lookup(DebitNote, Reference, "1", deviceId, "Web", ref objRes);
                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.ContactNo = objRes.contact_no;
                            objResponse.Address = objRes.address;
                            objResponse.DebitNoteNo = objRes.debit_note_no;
                            objResponse.SessionId = objRes.session_id;
                            objResponse.Name = objRes.name;
                            objResponse.PayableAmount = objRes.payable_amount;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            string SessionId = objRes.session_id.ToString();
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Salico Insurance", objResponse.ProductName, objResponse.Name, objResponse.DebitNoteNo, objResponse.PayableAmount, "", "", objResponse.Message, "0",
                                                         "0", "0", objResponse.PayableAmount);

                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance)
                        {
                            GetVendor_API_RelianceInsurance_Detail objRes = new GetVendor_API_RelianceInsurance_Detail();
                            msg = RepKhalti.RequestServiceGroup_RelianceInsurance_Detail(PolicyNumber, DOB, "1", deviceId, "Web", ref objRes);
                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.Paymode = objRes.paymode;
                            objResponse.Address = objRes.address;
                            objResponse.TransactionId = objRes.transaction_id;
                            objResponse.ProductName = objRes.product_name;
                            objResponse.CustomerId = objRes.customer_id;
                            objResponse.NextDueDate = objRes.next_due_date;
                            objResponse.InvoiceNumber = objRes.invoice_number;
                            objResponse.Amount = objRes.amount;
                            objResponse.CustomerName = objRes.customer_name;
                            objResponse.FineAmount = objRes.fine_amount;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            string SessionId = objRes.transaction_id;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Sanima Reliance Life Insurance", objResponse.ProductName, objResponse.CustomerName, objResponse.CustomerId, objResponse.Amount, "", objResponse.NextDueDate, objResponse.Message, "0",
                                                         objResponse.FineAmount, "0", objResponse.Amount);

                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Jyoti_Life)
                        {
                            GetVendor_API_JyotiLifeInsurance_Detail objRes = new GetVendor_API_JyotiLifeInsurance_Detail();
                            insuranceslug = "surya-jyoti-life-insurance";
                            msg = RepKhalti.RequestServiceGroup_JyotiLifeInsurance_Detail(PolicyNumber, DOB, insuranceslug, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.DueDate = objRes.due_date;
                            objResponse.MaturityDate = objRes.maturity_date;
                            objResponse.Name = objRes.customer_name;
                            objResponse.PaymentDate = objRes.payment_date;
                            objResponse.PolicyNo = objRes.policy_no;
                            objResponse.NextDueDate = objRes.next_due_date;
                            objResponse.PolicyStatus = objRes.policy_status;
                            objResponse.Amount = objRes.total_amount;
                            objResponse.PremiumAmount = objRes.premium_amount;
                            objResponse.FineAmount = objRes.fine_amount;
                            objResponse.RebateAmount = objRes.rebate_amount;
                            objResponse.SessionId = Convert.ToInt32(objRes.session_id);
                            objResponse.Term = objRes.term;
                            objResponse.Token = objRes.token;
                            objResponse.TotalFine = objRes.total_fine;
                            objResponse.UniqueIdGuid = objRes.unique_id_guid;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            string SessionId = objRes.session_id;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Surya Jyoti Life Insurance", objResponse.ProductName, objResponse.Name, objResponse.PolicyNo, objResponse.PremiumAmount, objResponse.InstallmentNo, objResponse.NextDueDate, objResponse.Message, "0",
                                                         objResponse.FineAmount, "0", objResponse.Amount);


                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Himalayan_Life)
                        {
                            GetVendor_API_PrimeLifeInsurance_Detail objRes = new GetVendor_API_PrimeLifeInsurance_Detail();
                            insuranceslug = "himalayan-life-insurance";
                            msg = RepKhalti.RequestServiceGroup_PrimeLifeInsurance_Detail(PolicyNumber, DOB, insuranceslug, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.DueDate = objRes.next_due_date;
                            objResponse.CurrentDueDate = objRes.current_due_date;
                            objResponse.Name = objRes.customer_name;
                            objResponse.ProductName = objRes.product_name;
                            objResponse.PolicyNo = objRes.policy_no;
                            objResponse.InstallmentNo = objRes.installment_no;
                            objResponse.PremiumAmount = objRes.premium_amount;
                            objResponse.FineAmount = objRes.fine_amount;
                            objResponse.TotalAmount = objRes.total_amount;
                            objResponse.Paymode = objRes.pay_mode;
                            objResponse.SessionId = Convert.ToInt32(objRes.session_id);
                            objResponse.Token = objRes.token;
                            objResponse.UniqueIdGuid = objRes.unique_id_guid;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            string SessionId = objRes.session_id;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Himalayan Life Insurance", objResponse.ProductName, objResponse.Name, objResponse.PolicyNo, objResponse.PremiumAmount, objResponse.InstallmentNo, objResponse.DueDate, objResponse.Message, "0",
                                                         objResponse.FineAmount, "0", objResponse.TotalAmount);



                            //var list = new List<KeyValuePair<String, String>>();
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Date", DateTime.Now.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Type", "Insurance Payment");
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Insurance Service", "Prime Life Insurance");
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Product Name Details", (objResponse.ProductName == null ? "" : objResponse.ProductName.ToString()));
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Policy Holder Name", objResponse.Name);
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Policy Number", objResponse.PolicyNo);
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Sum Assured Amount", objResponse.PremiumAmount.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Premium Number", (objResponse.InstallmentNo == null ? "" : objResponse.InstallmentNo.ToString()));
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Next Premium Date", objResponse.DueDate.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Status", objResponse.Message.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Premium Amount", objResponse.PremiumAmount.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Fine Amount", objResponse.FineAmount.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Servcice Charge", "0");
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Paid(RED)", objResponse.TotalAmount.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Remarks", "Insurance premium of " + objResponse.TotalAmount.ToString() + " is paid for policy number {" + objResponse.PolicyNo + "} successfully.");

                            //string jsonData = VendorApi_CommonHelper.getJSONfromList(list);
                            //VendorApi_CommonHelper.saveRecieptsVendorResponse(ServiceId.ToString(), SessionId, objRes.unique_id_guid, jsonData, "", "", "", "", "");


                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_National_Life)
                        {
                            GetVendor_API_NationalLifeInsurance_Detail objRes = new GetVendor_API_NationalLifeInsurance_Detail();
                            insuranceslug = "national-life-insurance";
                            msg = RepKhalti.RequestServiceGroup_NationalLifeInsurance_Detail(PolicyNumber, DOB, insuranceslug, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.DueDate = objRes.next_due_date;
                            //objResponse.CurrentDueDate = objRes.current_due_date;
                            objResponse.Name = objRes.customer_name;
                            objResponse.ProductName = objRes.product_name;
                            objResponse.PolicyNo = objRes.policy_no;
                            //objResponse.InstallmentNo = objRes.installment_no;
                            objResponse.PremiumAmount = objRes.premium_amount;
                            objResponse.FineAmount = objRes.fine_amount;
                            objResponse.TotalAmount = objRes.total_amount;
                            objResponse.Paymode = objRes.pay_mode;
                            objResponse.SessionId = Convert.ToInt32(objRes.session_id);
                            objResponse.Token = objRes.token;
                            objResponse.UniqueIdGuid = objRes.unique_id_guid;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            string SessionId = objRes.session_id;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "National Life Insurance", objResponse.ProductName, objResponse.Name, objResponse.PolicyNo, objResponse.PremiumAmount, "", objResponse.DueDate, objResponse.Message, "0",
                                                         objResponse.FineAmount, "0", objResponse.TotalAmount);

                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Prabhu_Mahalaxmi_Life)
                        {
                            GetVendor_API_PrabhuLifeInsurance_Detail objRes = new GetVendor_API_PrabhuLifeInsurance_Detail();
                            insuranceslug = "prabhu-life-insurance";
                            msg = RepKhalti.RequestServiceGroup_PrabhuLifeInsurance_Detail(PolicyNumber, DOB, insuranceslug, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.DueDate = objRes.next_due_date;
                            //objResponse.CurrentDueDate = objRes.current_due_date;
                            objResponse.Name = objRes.customer_name;
                            objResponse.ProductName = objRes.product_name;
                            objResponse.PolicyNo = objRes.policy_no;
                            //objResponse.InstallmentNo = objRes.installment_no;
                            objResponse.PremiumAmount = objRes.premium_amount;
                            objResponse.FineAmount = objRes.fine_amount;
                            objResponse.TotalAmount = objRes.total_amount;
                            objResponse.Paymode = objRes.pay_mode;
                            objResponse.SessionId = Convert.ToInt32(objRes.session_id);
                            objResponse.Token = objRes.token;
                            objResponse.UniqueIdGuid = objRes.unique_id_guid;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            string SessionId = objRes.session_id;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Prabhu Life Insurance", objResponse.ProductName, objResponse.Name, objResponse.PolicyNo, objResponse.PremiumAmount, "", objResponse.DueDate, objResponse.Message, "0",
                                                         objResponse.FineAmount, "0", objResponse.TotalAmount);
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Nepal_Life)
                        {
                            GetVendor_API_NepalLifeInsurance_Detail objRes = new GetVendor_API_NepalLifeInsurance_Detail();
                            msg = RepKhalti.RequestServiceGroup_NepalLifeInsurance_Detail(PolicyNumber, DOB, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.DueDate = objRes.due_date;
                            objResponse.CustomerName = objRes.customer_name;
                            objResponse.PolicyNo = objRes.policy_no;
                            objResponse.Amount = objRes.amount;
                            objResponse.PremiumAmount = objRes.premium_amount;
                            objResponse.FineAmount = objRes.fine_amount;
                            objResponse.RebateAmount = objRes.rebate_amount;
                            objResponse.SessionId = Convert.ToInt32(objRes.session_id);
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            string SessionId = objRes.session_id;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Nepal Life Insurance", objResponse.ProductName, objResponse.CustomerName, objResponse.PolicyNo, objResponse.PremiumAmount, "", objResponse.DueDate, objResponse.Message, "0",
                                                         objResponse.FineAmount, "0", objResponse.Amount);

                        }
                        //else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Life)
                        //{
                        //    GetVendor_API_SuryaLifeInsurance_Detail objRes = new GetVendor_API_SuryaLifeInsurance_Detail();
                        //    msg = RepKhalti.RequestServiceGroup_SuryaLifeInsurance_Detail(PolicyNumber, DOB, Reference, "1", deviceId, "Web", ref objRes);

                        //    result = JsonConvert.SerializeObject(objRes);
                        //    objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                        //    objResponse.DueDate = objRes.due_date;
                        //    objResponse.PolicyNo = objRes.policy_no;
                        //    objResponse.Amount = objRes.amount;
                        //    objResponse.PremiumAmount = objRes.premium_amount;
                        //    objResponse.FineAmount = objRes.fine_amount;
                        //    objResponse.SessionId = Convert.ToInt32(objRes.session_id);
                        //    objResponse.PlanCode = objRes.plan_code;
                        //    objResponse.AdjustmentAmount = objRes.adjustment_amount;
                        //    objResponse.MaturityDate = objRes.maturity_date;
                        //    objResponse.Name = objRes.name;
                        //    objResponse.NextDueDate = objRes.next_due_date;
                        //    objResponse.PaymentDate = objRes.payment_date;
                        //    objResponse.Paymode = objRes.pay_mode;
                        //    objResponse.PolicyStatus = objRes.policy_status;
                        //    objResponse.Term = objRes.term;
                        //    objResponse.ReponseCode = objRes.status ? 1 : 0;
                        //    objResponse.status = objRes.status;
                        //    objResponse.Message = objRes.Message;
                        //}
                        //else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Life)
                        //{
                        //    GetVendor_API_SanimaLifeInsurance_Detail objRes = new GetVendor_API_SanimaLifeInsurance_Detail();
                        //    msg = RepKhalti.RequestServiceGroup_SanimaLifeInsurance_Detail(PolicyNumber, DOB, Reference, "1", deviceId, "Web", ref objRes);

                        //    result = JsonConvert.SerializeObject(objRes);
                        //    objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                        //    objResponse.SessionId = Convert.ToInt32(objRes.session_id);
                        //    objResponse.detail = objRes.detail;
                        //    objResponse.ReponseCode = objRes.status ? 1 : 0;
                        //    objResponse.status = objRes.status;
                        //    objResponse.Message = objRes.Message;
                        //}
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Shikhar)
                        {
                            GetVendor_API_ShikharInsurance_GetPackages objRes = new GetVendor_API_ShikharInsurance_GetPackages();
                            msg = RepKhalti.RequestServiceGroup_ShikharInsurance_GetPackages("1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.shikhardetail = objRes.detail;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant)
                        {
                            GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();
                            insuranceslug = "sanima-general-insurance";
                            msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(insuranceslug, RequestId, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.ClassName = objRes.class_name;
                            objResponse.Insured = objRes.insured;
                            objResponse.ProformaNo = objRes.proforma_no;
                            objResponse.SumInsured = objRes.sum_insured;
                            objResponse.TpPremium = objRes.tp_premium;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;

                            //string SessionId = objRes.session_id;
                            //var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            //VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Arhant Life Insurance", objResponse.ProductName, objResponse.CustomerName, objResponse.PolicyNo, objResponse.PremiumAmount, "", objResponse.DueDate, objResponse.Message, "0",
                            //                             objResponse.FineAmount, "0", objResponse.Amount);


                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_NLG)
                        {
                            GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();
                            insuranceslug = "ngl-insurance";
                            msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(insuranceslug, RequestId, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.ClassName = objRes.class_name;
                            objResponse.Insured = objRes.insured;
                            objResponse.ProformaNo = objRes.proforma_no;
                            objResponse.SumInsured = objRes.sum_insured;
                            objResponse.TpPremium = objRes.tp_premium;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;


                        }

                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_SIDDHARTHA)
                        {
                            GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();
                            insuranceslug = "siddhartha-insurance";
                            msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(insuranceslug, RequestId, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.ClassName = objRes.class_name;
                            objResponse.Insured = objRes.insured;
                            objResponse.ProformaNo = objRes.proforma_no;
                            objResponse.SumInsured = objRes.sum_insured;
                            objResponse.TpPremium = objRes.tp_premium;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;


                        }

                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_UNITEDAJOD)
                        {
                            GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();
                            insuranceslug = "united-insurance";
                            msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(insuranceslug, RequestId, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Insurance>(result);
                            objResponse.ClassName = objRes.class_name;
                            objResponse.Insured = objRes.insured;
                            objResponse.ProformaNo = objRes.proforma_no;
                            objResponse.SumInsured = objRes.sum_insured;
                            objResponse.TpPremium = objRes.tp_premium;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = objRes.Message;


                        }



                        if (msg.ToLower() == "success")
                        {


                            result = JsonConvert.SerializeObject(objResponse);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = msg;
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserInsurance(string ServiceId, string PolicyCategory, string PolicyType, string CustomerName, string MobileNumber, string PolicyNumber, string Amount, string Mpin, string PaymentMode, string InsuranceType, string SessionId, string TxnId, string RequestId, string Address, string PolicyName, string Email, string Branch, string PolicyDescription, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceId) || ServiceId == "")
                {
                    result = "Please Select ServiceID";
                }
                if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Neco).ToString())
                {
                    if (string.IsNullOrEmpty(PolicyCategory) || PolicyCategory == "" || PolicyCategory == "0")
                    {
                        result = "Please select Policy Category";
                    }
                    else if (string.IsNullOrEmpty(PolicyType) || PolicyType == "" || PolicyType == "0")
                    {
                        result = "Please select Policy Type";
                    }

                    else if (string.IsNullOrEmpty(CustomerName) || CustomerName == "")
                    {
                        result = "Please enter Customer Name";
                    }
                    else if (string.IsNullOrEmpty(MobileNumber) || MobileNumber == "")
                    {
                        result = "Please enter Mobile Number";
                    }
                }
                else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico).ToString())
                {
                    if (string.IsNullOrEmpty(SessionId) || SessionId == "")
                    {
                        result = "Please enter SessionId";
                    }
                }
                else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance).ToString())
                {
                    if (string.IsNullOrEmpty(TxnId) || TxnId == "")
                    {
                        result = "Please enter TxnId from get Details";
                    }
                }
                if (string.IsNullOrEmpty(Amount) || Amount == "" || Amount == "0")
                {
                    result = "Please enter Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if (string.IsNullOrEmpty(Mpin) || Mpin == "" || Mpin == "0")
                {
                    result = "Please Enter Mpin";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceId, "Insurance payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }

                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_Insurance objReq = new WebRequest_Insurance();
                        string ApiName = "";
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Neco).ToString())
                        {
                            ApiName = "api/use-insurance-neco";
                            objReq.policy_category = PolicyCategory;
                            objReq.policy_type = PolicyType;
                            objReq.policy_number = PolicyNumber;
                            objReq.customer_name = CustomerName;
                            objReq.mobile_number = MobileNumber;
                            objReq.policy_category = PolicyCategory;
                            objReq.service_name = InsuranceType;
                            objReq.insurance_slug = InsuranceType;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico).ToString())
                        {
                            ApiName = "api/use-insurance-sagarmatha";
                            objReq.SessionId = SessionId;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance).ToString())
                        {
                            ApiName = "api/use-insurance-reliance";
                            objReq.TransactionId = TxnId;
                            objReq.PolicyNo = PolicyNumber;

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Jyoti_Life).ToString())
                        {
                            ApiName = "api/use-insurance-jyotilife";
                            objReq.SessionId = SessionId;
                            objReq.InsuranceSlug = InsuranceType;

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Nepal_Life).ToString())
                        {
                            ApiName = "api/use-insurance-nepallife";
                            objReq.SessionId = SessionId;

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Himalayan_Life).ToString())
                        {
                            ApiName = "api/use-insurance-primelife";
                            objReq.SessionId = SessionId;
                            objReq.InsuranceSlug = InsuranceType;

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Prabhu_Mahalaxmi_Life).ToString())
                        {
                            ApiName = "api/use-insurance-prabhulife";
                            objReq.SessionId = SessionId;
                            objReq.InsuranceSlug = InsuranceType;

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_National_Life).ToString())
                        {
                            ApiName = "api/use-insurance-nationallife";
                            objReq.SessionId = SessionId;
                            objReq.InsuranceSlug = InsuranceType;

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Shikhar).ToString())
                        {
                            ApiName = "api/use-insurance-shikhar";
                            objReq.CustomerName = CustomerName;
                            objReq.Address = Address;
                            objReq.ContactNumber = MobileNumber;
                            //objReq.Amount = "";
                            objReq.Email = Email;
                            objReq.PolicyType = PolicyType;
                            objReq.PolicyNo = PolicyNumber;
                            objReq.Branch = Branch;
                            objReq.PolicyDescription = PolicyDescription;
                            objReq.PolicyName = PolicyName;

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant).ToString())
                        {
                            ApiName = "api/use-insurance-arhant";
                            objReq.RequestId = RequestId;
                            objReq.InsuranceSlug = "sanima-general-insurance";

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_NLG).ToString())
                        {
                            ApiName = "api/use-insurance-arhant";
                            objReq.RequestId = RequestId;
                            objReq.InsuranceSlug = "ngl-insurance";

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_SIDDHARTHA).ToString())
                        {
                            ApiName = "api/use-insurance-arhant";
                            objReq.RequestId = RequestId;
                            objReq.InsuranceSlug = "siddhartha-insurance";

                        }

                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_UNITEDAJOD).ToString())
                        {
                            ApiName = "api/use-insurance-arhant";
                            objReq.RequestId = RequestId;
                            objReq.InsuranceSlug = "united-insurance";

                        }

                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.amount = Amount;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpGet]
        public ActionResult MyPayUserRCCard()
        {
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.Title == "Recharge Card").ToList();
            ViewBag.TVList = objRes;
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.MyPayContactNumber = Session["MyPayContactNumber"].ToString();
            ViewBag.ServiceId = "";
            AddUser model = new AddUser();
            return View(model);
        }

        [HttpPost]
        public JsonResult MyPayUserRCCardDetails(string ServiceId)
        {
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.ProviderTypeId == ServiceId).ToList();

            return Json(objRes);
        }

        [HttpPost]
        public JsonResult MyPayUserRCCard(string ServiceId, string MobileNumber, string Amount, string Mpin, string PaymentMode, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceId) || ServiceId == "")
                {
                    result = "Please Select ServiceID";
                }
                if (string.IsNullOrEmpty(Amount) || Amount == "" || Amount == "0")
                {
                    result = "Please select a valid Amount";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if (string.IsNullOrEmpty(Mpin) || Mpin == "" || Mpin == "0")
                {
                    result = "Please Enter Mpin";
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceId, "Rc card payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {

                        WebRequest_RCCard objReq = new WebRequest_RCCard();
                        string ApiName = "";
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_dishhome_erc).ToString())
                        {
                            ApiName = "api/user-dishhome-erc";

                        }
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ntc_erc).ToString())
                        {
                            ApiName = "api/user-ntc-erc";

                        }
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_smart_erc).ToString())
                        {
                            ApiName = "api/user-smart-erc";

                        }
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_broadlink_erc).ToString())
                        {
                            ApiName = "api/user-broadlink-erc";

                        }
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nettv_erc).ToString())
                        {
                            ApiName = "api/user-nettv-erc";

                        }
                        objReq.Number = MobileNumber;
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.Amount = Amount;
                        objReq.UniqueCustomerId = MobileNumber;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.CouponCode = CouponCode;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_RCCard objResponse = new WebRes_RCCard();
                            objResponse = JsonConvert.DeserializeObject<WebRes_RCCard>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return Json(result);
        }
        [HttpGet]
        public ActionResult MyPayUserInternet()
        {
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.Title == "Internet").ToList();
            ViewBag.InternetList = objRes;
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.MyPayContactNumber = Session["MyPayContactNumber"].ToString();
            ViewBag.ServiceId = "";
            AddUser model = new AddUser();
            return View(model);
        }

        [HttpPost]
        public JsonResult MyPayUserInternetDetails(int ServiceId, string CustomerId, string UserName)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRes_Internet objResponse = new WebRes_Internet();
                        string Reference = new CommonHelpers().GenerateUniqueId();
                        string UniqueCustomerId = Convert.ToString(Session["MyPayContactNumber"]);
                        string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                        string msg = "";
                        if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_vianet)
                        {
                            GetVendor_API_ViaNet_Lookup objRes = new GetVendor_API_ViaNet_Lookup();
                            msg = RepKhalti.RequestServiceGroup_ViaNet_LOOKUP(CustomerId, Reference, "1", deviceId, "Web", ref objRes);
                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            objResponse.bills = objRes.bills;
                            objResponse.SessionID = objRes.session_id;
                            objResponse.CustomerName = objRes.customer_name;
                            objResponse.CustomerID = objRes.customer_id;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_classictech)
                        {
                            GetVendor_API_Classitech_Lookup objRes = new GetVendor_API_Classitech_Lookup();
                            msg = RepKhalti.RequestServiceGroup_CLASSITECH_LOOKUP(UserName, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            objResponse.available_plans = objRes.available_plans;
                            objResponse.SessionID = objRes.session_id;
                            objResponse.token = objRes.token;
                            objResponse.CustomerName = objRes.username;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new)
                        {
                            GetVendor_API_SUBISU_Lookup objRes = new GetVendor_API_SUBISU_Lookup();
                            GetVendor_API_SUBISU_Lookup_TV_ComboOffer objResOffer = new GetVendor_API_SUBISU_Lookup_TV_ComboOffer();

                            msg = RepKhalti.RequestServiceGroup_SUBISU_LOOKUP(UserName, Reference, "1", deviceId, "Web", ref objRes);
                            //Session["SubisuReferenceTrackerId"] = Reference;
                            if (msg != "success")
                            {
                                Reference = Reference + "01";
                                msg = RepKhalti.RequestServiceGroup_SUBISU_NEW_LOOKUP(UserName, Reference, "1", deviceId, "Web", ref objResOffer);
                                //Session["SubisuReferenceTrackerId"] = Reference;

                                //result = JsonConvert.SerializeObject(objResOffer);
                                //objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                                objResponse.CustomerName = objResOffer.customer_name;
                                objResponse.address = objResOffer.address;
                                objResponse.current_plan_name = objResOffer.current_plan_name;
                                objResponse.user_id = objResOffer.user_id;
                                objResponse.outstanding_amount = objResOffer.outstanding_amount;
                                objResponse.expiry_date = objResOffer.expiry_date;
                                objResponse.mobile_no = objResOffer.mobile_no;
                                objResponse.onu_id = objResOffer.onu_id;
                                objResponse.partner_name = objResOffer.partner_name;

                                objResponse.plan_detail_list_offer = objResOffer.plan_detail_list;
                                objResponse.tv_plan_list = objResOffer.tv_plan_list;

                                objResponse.ReferenceNo = Reference;
                                objResponse.token = objResOffer.token;
                                objResponse.plan_type = objResOffer.plan_detail_list.plan_type;
                                objResponse.ReponseCode = objResOffer.status ? 1 : 0;
                                objResponse.SessionID = objResOffer.session_id;
                                objResponse.status = objResOffer.status;
                                objResponse.Message = "success";
                            }
                            else
                            {
                                result = JsonConvert.SerializeObject(objRes);
                                objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                                objResponse.CustomerName = objRes.customer_name;
                                objResponse.address = objRes.address;
                                objResponse.current_plan_name = objRes.current_plan_name;
                                objResponse.user_id = objRes.user_id;
                                objResponse.outstanding_amount = objRes.outstanding_amount;
                                objResponse.expiry_date = objRes.expiry_date;
                                objResponse.mobile_no = objRes.mobile_no;
                                objResponse.onu_id = objRes.onu_id;
                                objResponse.partner_name = objRes.partner_name;

                                objResponse.plan_detail_list = objRes.plan_detail_list;
                                objResponse.tv_plan_list = objRes.tv_plan_list;

                                objResponse.ReferenceNo = Reference;
                                objResponse.token = objRes.token;
                                objResponse.plan_type = objRes.plan_detail_list.plan_type;
                                objResponse.ReponseCode = objRes.status ? 1 : 0;
                                objResponse.SessionID = objRes.session_id;
                                objResponse.status = objRes.status;
                                objResponse.Message = "success";
                            }
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Arrownet)
                        {
                            GetVendor_API_Arrownet_Lookup objRes = new GetVendor_API_Arrownet_Lookup();
                            msg = RepKhalti.RequestServiceGroup_Arrownet_LOOKUP(UserName, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            objResponse.current_plan = objRes.current_plan;
                            objResponse.days_remaining = objRes.days_remaining;
                            objResponse.full_name = objRes.full_name;
                            objResponse.has_due = objRes.has_due;
                            objResponse.plan_details = objRes.plan_details;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebSurfer)
                        {
                            GetVendor_API_WebSurfer_UserList_Lookup objRes = new GetVendor_API_WebSurfer_UserList_Lookup();
                            msg = RepKhalti.RequestServiceGroup_WebSurfer_UserList_LOOKUP(CustomerId, UniqueCustomerId, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            objResponse.customer = objRes.customer;
                            objResponse.connection = objRes.connection;
                            objResponse.SessionID = objRes.session_id.ToString();
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_TechMinds)
                        {
                            GetVendor_API_Techminds_Lookup objRes = new GetVendor_API_Techminds_Lookup();
                            msg = RepKhalti.RequestServiceGroup_Techminds_LOOKUP(CustomerId, UniqueCustomerId, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            //objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            Techminds_Plans objDataItem = new Techminds_Plans();
                            objDataItem.Plan_12Month = objRes.available_plans._12_Month;
                            objDataItem.Plan_6Month = objRes.available_plans._6_Month;
                            objDataItem.Plan_3Month = objRes.available_plans._3_Month;
                            objDataItem.Plan_1Month = objRes.available_plans._1_Month;
                            objDataItem.Plan_15Days = objRes.available_plans._15Days;
                            objDataItem.Plan_180Days = objRes.available_plans._180Days;
                            objDataItem.Plan_30Days = objRes.available_plans._30Days;
                            objDataItem.Plan_60Days = objRes.available_plans._60Days;
                            objDataItem.Plan_90Days = objRes.available_plans._90Days;

                            var serilaizeJson = JsonConvert.SerializeObject(objDataItem, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

                            var result1 = JsonConvert.DeserializeObject<dynamic>(serilaizeJson);
                            objResponse.Available_Plans = result1;
                            objResponse.data = objRes.data;
                            objResponse.SessionID = objRes.session_id.ToString();
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_png_network)
                        {
                            GetVendor_API_PNGNETWORKTV_Lookup objRes = new GetVendor_API_PNGNETWORKTV_Lookup();
                            msg = RepKhalti.RequestServiceGroup_PNGNETWORKTV_LOOKUP("internet", "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            objResponse.detail = objRes.detail;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_jagriti)
                        {
                            GetVendor_API_JAGRITITV_Lookup objRes = new GetVendor_API_JAGRITITV_Lookup();
                            msg = RepKhalti.RequestServiceGroup_JAGRITITV_LOOKUP("internet", "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            objResponse.Jagritidetail = objRes.detail;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }
                        else if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Worldlink)
                        {
                            GetVendor_API_WorldLink_Lookup objRes = new GetVendor_API_WorldLink_Lookup();
                            msg = RepKhalti.RequestServiceGroup_WorldLink_Lookup(Reference, UserName, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            objResponse.first_online_payment = objRes.first_online_payment;
                            objResponse.renew_option = objRes.renew_option;
                            objResponse.amount = objRes.amount;
                            objResponse.due_amount_till_now = objRes.due_amount_till_now;
                            objResponse.SessionID = Convert.ToString(objRes.session_id);
                            objResponse.amount_detail = objRes.amount_detail;
                            objResponse.available_renew_options = objRes.available_renew_options;
                            objResponse.package_options = objRes.package_options;
                            objResponse.branch = objRes.branch;
                            objResponse.log_idx = objRes.log_idx;
                            objResponse.message = objRes.message;
                            objResponse.subscribed_package_name = objRes.subscribed_package_name;
                            objResponse.subscribed_package_type = objRes.subscribed_package_type;
                            objResponse.username = objRes.username;
                            objResponse.full_name = objRes.full_name;
                            objResponse.days_remaining = objRes.days_remaining.ToString();

                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }

                        if (msg.ToLower() == "success")
                        {
                            result = JsonConvert.SerializeObject(objResponse);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserInternetWebSurferDetails(int ServiceId, string SessionId, string UserName)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRes_Internet objResponse = new WebRes_Internet();
                        string Reference = new CommonHelpers().GenerateUniqueId();
                        string UniqueCustomerId = Convert.ToString(Session["MyPayContactNumber"]);
                        string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                        string msg = "";
                        if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebSurfer)
                        {
                            GetVendor_API_WebSurfer_Lookup objRes = new GetVendor_API_WebSurfer_Lookup();
                            msg = RepKhalti.RequestServiceGroup_WebSurfer_LOOKUP(UserName, SessionId, UniqueCustomerId, Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_Internet>(result);
                            objResponse.packages = objRes.packages;
                            objResponse.SessionID = objRes.session_id.ToString();
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";
                        }

                        if (msg.ToLower() == "success")
                        {
                            result = JsonConvert.SerializeObject(objResponse);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserInternet(string ServiceId, string ReferenceNo, string Number, string Amount, string Mpin, string PaymentMode, string volumebased, string UserName, string SessionId, string PaymentId, string Package, string Month, string Token, string Address, string CustomerName, string CustomerId, string CasId, string Oldwardno, string CouponCode, string BankId, string STB, string PlanType)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ServiceId) || ServiceId == "")
                {
                    result = "Please Select ServiceID";
                }
                else if (string.IsNullOrEmpty(Amount) || Amount == "" || Amount == "0")
                {
                    result = "Please enter a valid Amount";
                }
                //else if (string.IsNullOrEmpty(Number) || Number == "" || Number == "0")
                //{
                //    result = "Please enter a Number";
                //}
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "" || PaymentMode == "0")
                {
                    result = "Please Enter Payment Mode";
                }
                else if (string.IsNullOrEmpty(Mpin) || Mpin == "" || Mpin == "0")
                {
                    result = "Please Enter Mpin";
                }
                else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_adsl).ToString())
                {
                    if (string.IsNullOrEmpty(volumebased) || volumebased == "")
                    {
                        result = "Please select an option Unlimited/VolumeBased";
                    }
                }
                else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new).ToString())
                {
                    if (string.IsNullOrEmpty(UserName) || UserName == "")
                    {
                        result = "Please enter UserName";
                    }
                }
                else if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceId, "Internet payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_Internet objReq = new WebRequest_Internet();
                        string ApiName = "";
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_adsl).ToString())
                        {
                            ApiName = "api/use-internet-adsl";
                            if (volumebased == "Unlimited")
                            {
                                objReq.IsVolumeBased = "False";
                            }
                            else if (volumebased == "VolumeBased")
                            {
                                objReq.IsVolumeBased = "True";
                            }
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new).ToString())
                        {
                            ApiName = "api/use-internet-subisu";
                            objReq.CustomerID = UserName;
                            objReq.token = Token;
                            objReq.SessionID = SessionId;
                            objReq.OfferName = Package;
                            objReq.stb = STB;
                            objReq.PlanType = PlanType;
                            objReq.Reference = ReferenceNo;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_vianet).ToString())
                        {
                            ApiName = "api/use-internet-vianet";
                            objReq.CustomerID = Number;
                            objReq.SessionID = SessionId;
                            objReq.PaymentID = PaymentId;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_classictech).ToString())
                        {
                            ApiName = "api/use-internet-classitech";
                            objReq.Month = Month;
                            objReq.Package = Package;
                            objReq.token = Token;
                            objReq.SessionID = SessionId;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Arrownet).ToString())
                        {
                            ApiName = "api/use-internet-Arrownet";
                            objReq.Duration = Month;
                            objReq.UserName = UserName;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_VirtualNetwork).ToString())
                        {
                            ApiName = "api/use-internet-virtual-network";
                            if (Session["MyPayContactNumber"] != null && !string.IsNullOrEmpty(Session["MyPayContactNumber"].ToString()))
                            {
                                Number = Session["MyPayContactNumber"].ToString();
                            }
                            else
                            {
                                RepMyPayUserLogin.Logout();
                                //result = objResponse.Message;
                            }

                            objReq.UserName = UserName;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebNetwork).ToString())
                        {
                            ApiName = "api/use-internet-web-network";
                            if (Session["MyPayContactNumber"] != null && !string.IsNullOrEmpty(Session["MyPayContactNumber"].ToString()))
                            {
                                Number = Session["MyPayContactNumber"].ToString();
                            }
                            else
                            {
                                RepMyPayUserLogin.Logout();
                                //result = objResponse.Message;
                            }

                            objReq.UserName = UserName;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_RoyalNetwork).ToString())
                        {
                            ApiName = "api/use-internet-royal-network";
                            if (Session["MyPayContactNumber"] != null && !string.IsNullOrEmpty(Session["MyPayContactNumber"].ToString()))
                            {
                                Number = Session["MyPayContactNumber"].ToString();
                            }
                            else
                            {
                                RepMyPayUserLogin.Logout();
                                //result = objResponse.Message;
                            }

                            objReq.UserName = UserName;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebSurfer).ToString())
                        {
                            ApiName = "api/use-internet-websurfer";
                            objReq.SessionID = SessionId;
                            objReq.package_id = PaymentId;
                            objReq.Service = "websurfer";
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_TechMinds).ToString())
                        {
                            ApiName = "api/use-internet-techminds";
                            objReq.SessionID = SessionId;
                            objReq.RequestID = Number;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Pokhara).ToString())
                        {
                            ApiName = "api/use-internet-pokhara";
                            objReq.UserName = UserName;
                            objReq.Address = Address;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_png_network).ToString())
                        {
                            ApiName = "api/use-tv-pngnetworktv";
                            objReq.UserName = UserName;
                            objReq.Package = Package;
                            objReq.CustomerName = CustomerName;
                            objReq.ContactNumber = Number;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_jagriti).ToString())
                        {
                            ApiName = "api/use-tv-jagrititv";
                            objReq.Package = Package;
                            objReq.CustomerName = CustomerName;
                            objReq.ContactNumber = Number;
                            objReq.CustomerID = CustomerId;
                            objReq.STB_OR_CAS_ID = CasId;
                            objReq.Old_Ward_Number = Oldwardno;
                            objReq.Mobile_Number_1 = Number;
                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Worldlink).ToString())
                        {
                            ApiName = "api/use-worldlink-commit";
                            objReq.Package = Package;
                            objReq.CustomerName = CustomerName;
                            objReq.ContactNumber = Number;
                            objReq.CustomerID = CustomerId;
                            objReq.STB_OR_CAS_ID = CasId;
                            objReq.Old_Ward_Number = Oldwardno;
                            objReq.Mobile_Number_1 = Number;
                        }

                        objReq.Reference = ReferenceNo;
                        objReq.Number = Number;
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.Amount = Amount;
                        objReq.UniqueCustomerId = Number;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.CouponCode = CouponCode;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            var settings = new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore
                            };
                            objResponse = JsonConvert.DeserializeObject<WebRes_RCCard>(result, settings);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpGet]
        public ActionResult MyPayUserFlightAirlines()
        {
            int ServiceID = 0;
            GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
            inobject.IsActive = 1;
            GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());
            ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
            bool isServiceDown = Common.CheckServiceDown(ServiceID.ToString());
            if (isServiceDown)
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");
            }
            List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility().Where(x => x.ProviderTypeId == ServiceID.ToString()).ToList();
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            ViewBag.MyPayContactNumber = Session["MyPayContactNumber"].ToString();
            ViewBag.MyPayEmail = Session["MyPayEmail"].ToString();
            ViewBag.MyPayFullName = Session["MyPayFullName"].ToString();
            ViewBag.ServiceId = ServiceID;
            AddUser model = new AddUser();
            return View(model);
        }


        [HttpPost]
        public JsonResult MyPayUserFlightSectorDetails(int ServiceId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRes_FlightSector objResponse = new WebRes_FlightSector();
                        string Reference = new CommonHelpers().GenerateUniqueId();
                        string deviceId = Convert.ToString(Session["MyPayUserMemberId"]);
                        string msg = "";
                        GetVendor_API_Airlines_Sector_Lookup objRes = new GetVendor_API_Airlines_Sector_Lookup();
                        if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines)
                        {

                            var reqJSON = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/FlightSectors.json"));
                            objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_Airlines_Sector_Lookup>(reqJSON);
                            msg = objRes.Message.ToLower();

                            //msg = RepKhalti.RequestServiceGroup_AIRLINES_SECTOR_LOOKUP(Session["MyPayUserMemberId"].ToString(), Reference, "1", deviceId, "Web", ref objRes);

                            result = JsonConvert.SerializeObject(objRes);
                            objResponse = JsonConvert.DeserializeObject<WebRes_FlightSector>(result);
                            objResponse.sectors = objRes.sectors;
                            objResponse.ReponseCode = objRes.status ? 1 : 0;
                            objResponse.status = objRes.status;
                            objResponse.Message = "success";

                            if (msg.ToLower() == "success")
                            {
                                result = JsonConvert.SerializeObject(objResponse);
                                if (objResponse.status && objResponse.Message.ToLower() == "success")
                                {

                                }
                                else if (objResponse.ReponseCode == 7)
                                {
                                    RepMyPayUserLogin.Logout();
                                    result = objResponse.Message;
                                }
                                else
                                {
                                    result = objResponse.Message;
                                }
                            }
                            else
                            {
                                result = "Invalid API Request";
                            }
                        }

                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        //[HttpPost]
        //public JsonResult MyPayUserFlightSectorDetails(int ServiceId)
        //{
        //    var result = string.Empty;
        //    try
        //    {
        //        result = RepKhalti.GetFlightSectorCode(Session["MyPayUserMemberId"].ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //    }
        //    return Json(result);
        //}


        [HttpPost]
        public JsonResult MyPayUserFlightLookupDetails(String Origin, string Destination, string Adults, string Child, string Departure, string Return, string TripType)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_FlightLookup objReq = new WebRequest_FlightLookup();
                        string ApiName = "api/lookup-airlines";
                        objReq.Adult = Adults;
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.Child = Child;
                        objReq.FlightDate = Departure;

                        DateTime date = DateTime.ParseExact(objReq.FlightDate, "yyyy-MM-dd", null);
                        string Flight_Date = date.ToString("dd-MMM-yyyy").ToUpper();

                        objReq.FlightType = "D";
                        objReq.FromDeparture = Origin;
                        objReq.ToArrival = Destination;
                        objReq.TripType = TripType;
                        //string Nationality = "NP";
                        string UserId = Environment.GetEnvironmentVariable("PlasmaTech_UserName", EnvironmentVariableTarget.Machine);//VendorApi_CommonHelper.Plasma_UserID;
                        string Password = Environment.GetEnvironmentVariable("PlasmaTech_Password", EnvironmentVariableTarget.Machine);//VendorApi_CommonHelper.PlasmaTech_Password;
                        string AgencyId = Environment.GetEnvironmentVariable("PlasmaTech_AgencyId", EnvironmentVariableTarget.Machine);//VendorApi_CommonHelper.PlasmaTech_AgencyId;
                        //string ClientIP = "";
                        string Return_Date = "";
                        if (TripType == "R")
                        {
                            objReq.ReturnDate = Return;
                            date = DateTime.ParseExact(objReq.ReturnDate, "yyyy-MM-dd", null);
                            Return_Date = date.ToString("dd-MMM-yyyy").ToUpper();

                        }
                        //objReq.Mpin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        //objReq.PaymentMode = PaymentMode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);

                        //GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
                        //inobject.IsActive = 1;
                        //GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());
                      
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);

                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_FlightLookup objResponse = new WebRes_FlightLookup();
                            objResponse = JsonConvert.DeserializeObject<WebRes_FlightLookup>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                //result = "success";
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserFlightBook(String FlightId, string BookingId, string ReturnFlightId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_FlightBooking objReq = new WebRequest_FlightBooking();
                        string ApiName = "api/book-flight-airlines";
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.FlightID = FlightId;
                        objReq.ReturnFlightID = ReturnFlightId;
                        objReq.BookingID = BookingId;
                        //objReq.Mpin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        //objReq.PaymentMode = PaymentMode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);

                        //GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
                        //inobject.IsActive = 1;
                        //GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_FlightBooking objResponse = new WebRes_FlightBooking();
                            objResponse = JsonConvert.DeserializeObject<WebRes_FlightBooking>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                //result = objResponse;
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserFlightPassangerDetails(string BookingID, string FlightId, string ContactName, string ContactPhone, string ContactEmail, string PassengersClassString)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_FlightPassanger objReq = new WebRequest_FlightPassanger();
                        string ApiName = "api/add-flight-passenger";
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.ContactName = ContactName;
                        objReq.ContactPhone = ContactPhone;
                        objReq.ContactEmail = ContactEmail;
                        objReq.BookingID = BookingID;
                        objReq.FlightId = FlightId;
                        objReq.PassengersClassString = PassengersClassString.Replace("'", "\"");
                        //objReq.Mpin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        //objReq.PaymentMode = PaymentMode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;

                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        //GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
                        //inobject.IsActive = 1;
                        //GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());
                     
                        var pass = JsonConvert.DeserializeObject<List<FlightPassenger>>(PassengersClassString);
                        objReq.PassengersClassString = JsonConvert.SerializeObject(pass);
                        string JSON = JsonConvert.SerializeObject(objReq);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);

                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_FlightBooking objResponse = new WebRes_FlightBooking();
                            objResponse = JsonConvert.DeserializeObject<WebRes_FlightBooking>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                //result = objResponse;
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserGetFlightPassengerDetail(string BookingId, string FlightId, string ReturnFlightId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_FlightBooking objReq = new WebRequest_FlightBooking();
                        string ApiName = "api/get-flight-booking-details";
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.BookingID = BookingId;
                        objReq.FlightID = FlightId;
                        objReq.ReturnFlightID = ReturnFlightId;
                        string UserId = Environment.GetEnvironmentVariable("PlasmaTech_UserName", EnvironmentVariableTarget.Machine);//VendorApi_CommonHelper.Plasma_UserID;
                        //objReq.Mpin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        //objReq.PaymentMode = PaymentMode;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        string MyPayContactNumber = Session["MyPayContactNumber"].ToString();
                        string MyPayEmail = Session["MyPayEmail"].ToString();
                        string MyPayFullName = Session["MyPayFullName"].ToString();
                        //GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
                        //inobject.IsActive = 1;
                        //GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());
                    
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                      
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_FlightBooking objResponse = new WebRes_FlightBooking();
                            objResponse = JsonConvert.DeserializeObject<WebRes_FlightBooking>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                //result = objResponse;
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserIssueFlight(string BookingId, string Amount, string Mpin, string PaymentMode, string FlightId, string ReturnFlightId, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            int ServiceId = 0;

            GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
            inobject.IsActive = 1;
            GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());
            ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
            try
            {
                if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceId.ToString(), "Flight payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_IssueFlight objReq = new WebRequest_IssueFlight();
                        string ApiName = "api/issue-flight-airlines";
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.BookingID = BookingId;
                        objReq.FareTotal = Amount;
                        objReq.FlightID = FlightId;
                        objReq.ReturnFlightID = ReturnFlightId;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.CouponCode = CouponCode;


                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        //if (ServiceId == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines)
                        //{
                            result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        //}
                        //else
                        //{
                        //    List<AddFlightPassengersDetails> paxDetailsList = VendorApi_CommonHelper.GetPaxDetailsList(FlightId);
                        //    List<Passengers> passengers = new List<Passengers>();
                        //    foreach (var item in paxDetailsList)
                        //    {
                        //        Passengers pax = new Passengers();
                        //        pax.PaxType = item.Type;
                        //        pax.Firstname = item.Firstname;
                        //        pax.Lastname = item.Lastname;
                        //        pax.Nationality = item.Nationality;
                        //        pax.Title = item.Title;
                        //        pax.Gender = item.Gender;
                        //        pax.Remarks = item.Remarks;
                        //        passengers.Add(pax);
                        //    }
                        //    objReq.PassengerDetail = JsonConvert.SerializeObject(passengers);
                        //    objReq.ContactName = Session["MyPayFullName"].ToString();
                        //    objReq.ContactEmail = Session["MyPayEmail"].ToString();
                        //    objReq.ContactMobile = Session["MyPayContactNumber"].ToString();
                        //    JSON = JsonConvert.SerializeObject(objReq);
                        //    ApiName = "/api/plasma-issue-ticket";

                        //    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        //}

                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_FlightBooking objResponse = new WebRes_FlightBooking();
                            objResponse = JsonConvert.DeserializeObject<WebRes_FlightBooking>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserTVPackageDetails(string ServiceId)
        {
            var result = string.Empty;

            try
            {

                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        WebRequest_TV objReq = new WebRequest_TV();
                        string ApiName = "";
                        if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_png_network_tv).ToString())
                        {
                            ApiName = "api/lookup-tv-pngnetworktv";

                        }
                        else if (ServiceId == ((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_jagrititv).ToString())
                        {
                            ApiName = "api/lookup-tv-jagrititv";

                        }

                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.IsMobile = false;
                        objReq.PlatForm = "Web";
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.PackageType = "Tv";
                        objReq.Version = "1.0";
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserLoadNews(string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getall-notificationcampaign";
                    WebRequest_Topup objReq = new WebRequest_Topup();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetProvidersMyPayWebURL(string RedirectType)
        {
            var result = string.Empty;
            try
            {
                AddProviderLogoList outobject = new AddProviderLogoList();
                GetProviderLogoList inobject = new GetProviderLogoList();
                inobject.ProviderTypeId = Convert.ToInt32(RedirectType);
                inobject.IsActive = 1;
                AddProviderLogoList res = RepCRUD<GetProviderLogoList, AddProviderLogoList>.GetRecord(Common.StoreProcedures.sp_ProviderLogoList_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    result = res.WebURL;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        //Events
        [HttpGet]
        public ActionResult MyPayUserEvents()
        {
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
            if (Common.CheckServiceDown(ServiceId.ToString()))
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");
            }
            ViewBag.ServiceId = ServiceId;
            return View();
        }
        [HttpPost]
        public JsonResult MyPayUserEventList(string PageSize, string PageNumber, string SearchVal, string SortOrder, string DateFrom, string DateTo, string SortBy)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/get-event-list";
                    WebRequest_Events objReq = new WebRequest_Events();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.PageSize = Convert.ToInt32(PageSize);
                    objReq.PageNumber = Convert.ToInt32(PageNumber);
                    objReq.SearchVal = SearchVal;
                    objReq.SortOrder = SortOrder;
                    objReq.DateFrom = DateFrom;
                    objReq.DateTo = DateTo;
                    objReq.SortBy = SortBy;

                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserEventDetail(string EventId, string EventDate, string MerchantCode)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/get-event-details";
                    WebRequest_EventDetail objReq = new WebRequest_EventDetail();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.EventId = Convert.ToInt32(EventId);
                    objReq.EventDate = EventDate;
                    objReq.MerchantCode = MerchantCode;


                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserBookEvent(string MerchantCode, string CustomerName, string CustomerMobile, string CustomerEmail, string EventId, string TicketCategoryId, string TicketCategoryName, string EventDate, string TicketRate, string NoOfTicket, string TotalPrice, string PaymentMethodId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/book-event-ticket";
                    WebRequest_EventBook objReq = new WebRequest_EventBook();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.MerchantCode = MerchantCode;
                    objReq.CustomerName = CustomerName;
                    objReq.CustomerMobile = CustomerMobile;
                    objReq.CustomerEmail = CustomerEmail;
                    objReq.EventId = Convert.ToInt32(EventId);
                    objReq.TicketCategoryId = Convert.ToInt32(TicketCategoryId);
                    objReq.TicketCategoryName = TicketCategoryName;
                    objReq.EventDate = EventDate;
                    objReq.TicketRate = Convert.ToDecimal(TicketRate);
                    objReq.NoOfTicket = Convert.ToInt32(NoOfTicket);
                    objReq.TotalPrice = Convert.ToDecimal(TotalPrice);
                    objReq.PaymentMethodId = Convert.ToInt32(PaymentMethodId);

                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status)
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserEventTicketCommit(string ServiceID, string Amount, string Mpin, string PaymentMode, string MerchantCode, string OrderId, string EventId, string PaymentMethodId, string PaymentMerchantId, string PaymentMethodName, string TicketCategoryName, string CouponCode, string BankId)
        {
            var result = string.Empty;
            var BankTransactionID = string.Empty;
            try
            {

                if (string.IsNullOrEmpty(ServiceID) || ServiceID == "")
                {
                    result = "Please Select ServiceID";
                }

                else if (string.IsNullOrEmpty(Amount) || Amount == "")
                {
                    result = "Please Enter Amount";
                }
                else if (string.IsNullOrEmpty(Mpin) || Mpin == "")
                {
                    result = "Please Enter Mpin";
                }
                else if (string.IsNullOrEmpty(PaymentMode) || PaymentMode == "")
                {
                    result = "Please Enter PaymentMode";
                }
                else if (string.IsNullOrEmpty(MerchantCode) || MerchantCode == "")
                {
                    result = "Please Enter MerchantCode";
                }
                else if (string.IsNullOrEmpty(OrderId) || OrderId == "")
                {
                    result = "Please Enter OrderId";
                }
                else if (string.IsNullOrEmpty(EventId) || EventId == "")
                {
                    result = "Please Enter EventId";
                }
                else if (string.IsNullOrEmpty(PaymentMethodId) || PaymentMethodId == "")
                {
                    result = "Please Enter PaymentMethodId";
                }
                else if (string.IsNullOrEmpty(PaymentMerchantId) || PaymentMerchantId == "")
                {
                    result = "Please Enter PaymentMerchantId";
                }
                else if (string.IsNullOrEmpty(PaymentMethodName) || PaymentMethodName == "")
                {
                    result = "Please Enter PaymentMethodName";
                }
                else if (string.IsNullOrEmpty(TicketCategoryName) || TicketCategoryName == "")
                {
                    result = "Please Enter TicketCategoryName";
                }


                if ((string.IsNullOrEmpty(BankId)) && (PaymentMode == "2"))
                {
                    result = "Please Enter Bank Id";
                }
                if ((string.IsNullOrEmpty(result)) && (PaymentMode == "2"))
                {
                    var BankTransferResult = MyPayUserLinkedBankTransaction(Amount, ServiceID, "My Pay Event payment", BankId, Mpin);
                    WebRes_BankTransfer objResponse = new WebRes_BankTransfer();
                    objResponse = JsonConvert.DeserializeObject<WebRes_BankTransfer>(BankTransferResult.Data.ToString());
                    if (objResponse.ReponseCode == 1)
                    {
                        BankTransactionID = objResponse.TransactionId;
                    }
                    else
                    {
                        result = objResponse.Message;
                    }
                }
                if (string.IsNullOrEmpty(result))
                {

                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        string ApiName = "api/event-ticket-commit";
                        WebRequest_EventTicketCommit objReq = new WebRequest_EventTicketCommit();
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        objReq.Amount = Amount;
                        objReq.Mpin = Common.Encryption(Mpin);
                        objReq.PlatForm = "Web";
                        objReq.PaymentMode = PaymentMode;
                        objReq.BankTransactionId = BankTransactionID;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        // objReq.Session_id = SessionId;
                        objReq.Version = "1.0";
                        objReq.Remarks = "";
                        objReq.TimeStamp = "";
                        objReq.Hash = "";
                        objReq.Req_ReferenceNo = new CommonHelpers().GenerateUniqueId();
                        objReq.MerchantCode = MerchantCode;
                        objReq.OrderId = OrderId;
                        objReq.EventId = EventId;
                        objReq.Value = "";
                        objReq.PaymentMethodId = Convert.ToInt32(PaymentMethodId);
                        objReq.PaymentMerchantId = PaymentMerchantId;
                        objReq.paymentMethodName = PaymentMethodName;
                        objReq.ticketCategoryName = TicketCategoryName;
                        objReq.CouponCode = CouponCode;

                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.status)
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);

        }
        [HttpGet]
        public ActionResult MyPayUserEventsBooking()
        {
            int ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
            if (Common.CheckServiceDown(ServiceID.ToString()))
            {
                return RedirectToAction("MyPayUserServiceDown", "MyPayUser");
            }
            ViewBag.ServiceId = ServiceID;
            ViewBag.MyPayContactNumber = Session["MyPayContactNumber"].ToString();
            ViewBag.MyPayEmail = Session["MyPayEmail"].ToString();
            ViewBag.MyPayFullName = Session["MyPayFullName"].ToString();
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            return View();
        }
        [HttpPost]
        public JsonResult MyPayUserEventBookingsList(string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/get-event-booking";
                    WebRequest_EventBookings objReq = new WebRequest_EventBookings();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status)
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserEventTicketDownload(string MerchantCode, string OrderId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/event-ticket-download";
                    WebRequest_EventTicketCommit objReq = new WebRequest_EventTicketCommit();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.MerchantCode = MerchantCode;
                    objReq.OrderId = OrderId;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status)
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpGet]
        [Authorize]
        public ActionResult MyPayUserEventDownloadTicketFile(string filePath)
        {
            try
            {
                string file = System.IO.Path.GetFileName(filePath);
                byte[] blob;
                using (var client = new WebClient())
                {
                    blob = client.DownloadData(filePath);
                }
                return File(blob, "application/pdf", file);
            }
            catch (Exception ex)
            {
                return RedirectToAction("/Index");
            }
        }

        [HttpPost]
        public JsonResult ServiceChargeMerchant(string MerchantId, string Amount, string ServiceId)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(MerchantId) || MerchantId == "")
                {
                    result = "Please Enter MerchantId";
                }
                if (string.IsNullOrEmpty(ServiceId) || ServiceId == "")
                {
                    result = "Please Enter ServiceId";
                }
                if (string.IsNullOrEmpty(Amount) || Amount == "" || Amount == "0" || Amount == "undefined")
                {
                    result = "Please Enter Amount";
                }
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceChargeMerchant(MerchantId, Amount.ToString(), ServiceId);
                        result = JsonConvert.SerializeObject(objOut);
                        if (string.IsNullOrEmpty(result))
                        {
                            result = "Invalid API Request";
                        }

                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserEventTicketEmail(string MerchantCode, string OrderId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/event-ticket-email";
                    WebRequest_EventTicketCommit objReq = new WebRequest_EventTicketCommit();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.MerchantCode = MerchantCode;
                    objReq.OrderId = OrderId;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status)
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserGetServiceUrl(string ServiceId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    result = Common.GetServiceUrl(ServiceId);
                    if (!string.IsNullOrEmpty(result))
                    {

                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyPayUserTransactionDownloadFile(string filePath)
        {
            try
            {
                string file = System.IO.Path.GetFileName(filePath) + ".pdf";
                byte[] blob;
                using (var client = new WebClient())
                {
                    blob = client.DownloadData(filePath);
                }
                return File(blob, "application/pdf", file);
            }
            catch (Exception ex)
            {
                return RedirectToAction("/Index");
            }
        }
        [HttpPost]
        public JsonResult MyPayUserTransactionHistoryDownload(string FromDate, string ToDate)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (string.IsNullOrEmpty(FromDate))
                    {
                        result = "Please enter From Date";
                    }
                    else if (string.IsNullOrEmpty(ToDate))
                    {
                        result = "Please enter ToDate";
                    }
                    if (string.IsNullOrEmpty(result))
                    {
                        string ApiName = "api/getuserestatement";
                        WebRequest_Transactions_Download objReq = new WebRequest_Transactions_Download();
                        objReq.FromDate = FromDate;
                        objReq.ToDate = ToDate;
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }

                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserCheckEmailVerify()
        {
            var result = string.Empty;

            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string MemberID = Convert.ToString(Session["MyPayUserMemberId"]);
                    bool isEmailVeriFied = Common.ISEmailVeriFied(MemberID);
                    result = isEmailVeriFied.ToString();
                }
                else
                {
                    result = "Invalid Request";
                }


            }
            catch (Exception)
            {

                throw;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult EmailVerify(String Email)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        Req_Web_User objReq = new Req_Web_User();
                        string ApiName = "api/VerifyEmail";
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        //objReq.IsMobile = false;
                        objReq.Email = Email;
                        objReq.PlatForm = "Web";
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_UserDetail objResponse = new WebRes_UserDetail();
                            objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                result = objResponse.Message;
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MypayUseOTPVerify(int OTP, string Email)
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                    {
                        Req_Web_User objReq = new Req_Web_User();
                        string ApiName = "api/EmailConfirm";
                        objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                        //objReq.IsMobile = false;
                        objReq.OTP = Convert.ToString(OTP);
                        objReq.Email = Email;
                        objReq.PlatForm = "Web";
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebRes_UserDetail objResponse = new WebRes_UserDetail();
                            objResponse = JsonConvert.DeserializeObject<WebRes_UserDetail>(result);
                            if (objResponse.status && objResponse.Message.ToLower() == "success")
                            {
                                result = objResponse.Message;
                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }
                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                    else
                    {
                        result = "Invalid Request";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyPayCouponScratched()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public JsonResult MyPayUserAllCoupons(string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        string ApiName = "api/getCouponsAll";
                        WebRequest_AllCoupons objReq = new WebRequest_AllCoupons();
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        objReq.Take = Take;
                        objReq.Skip = Skip;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }

                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserScratchCouponNow(int Id, string Couponcode)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        string ApiName = "api/ScratchCoupons";
                        WebRequest_CouponScratchNow objReq = new WebRequest_CouponScratchNow();
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                        objReq.Id = Id;
                        objReq.CouponCode = Couponcode;
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }

                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpGet]
        public ActionResult MyPayUserGrievance()
        {

            if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
            {
                List<SelectListItem> ticketcategory = CommonHelpers.GetSelectList_TicketCategory();
                ViewBag.Category = ticketcategory;
            }
            return View();
        }

        [HttpPost]
        public JsonResult MyPayUserSubmitGrievance(string CategoryId, string CategoryName, string Subject, string TxnId, string Description, string filename)
        {


            bool hasiInvalidChars = (!Regex.IsMatch(Subject, @"^[a-zA-Z0-9,. ]+$") ||
                !Regex.IsMatch(Description, @"^[a-zA-Z0-9,. ]+$") ||
                !Regex.IsMatch(TxnId, @"^[a-zA-Z0-9,. ]+$"));


            if (hasiInvalidChars)
            {
                return Json("Special characters are not allowed.");
            }

            //hasValidChars = (Regex.IsMatch(Description, @"^[a-zA-Z0-9,. ]+$"));
            //if (!hasValidChars)
            //{
            //    return Json("Special characters are not allowed.");
            //}

            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/addticket";
                    WebRequest_AddSupport objReq = new WebRequest_AddSupport();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.TicketTitle = Subject;
                    objReq.Description = Description;
                    objReq.CategoryId = Convert.ToInt32(CategoryId);
                    objReq.CategoryName = CategoryName;
                    objReq.TransactionId = TxnId;
                    objReq.Image = filename;
                    objReq.ContactNumber = Session["MyPayContactNumber"].ToString();
                    objReq.Name = Session["MyPayFullName"].ToString(); //objReq.Subject = "";
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.ReponseCode == 1)
                        {
                            result = "success";
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserAllGrievance(string Take, string Skip, string TicketTitle, string Status)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getalltickets";
                    WebRequest_AllSupportTickets objReq = new WebRequest_AllSupportTickets();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.CheckIsFavourite = 2;
                    objReq.CheckIsSeen = 2;
                    objReq.Status = Convert.ToInt32(Status);
                    objReq.TicketTitle = TicketTitle;
                    //objReq.Subject = "";
                    objReq.IsMobile = false;
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.ReponseCode == 1)
                        {
                            //result = "success";
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserGrievanceReplyCount(string Id)
        {
            var result = string.Empty;
            try
            {
                AddTicketsReply outobject = new AddTicketsReply();
                result = outobject.TotalTicketReplyCount(Id).ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserSingleGrievance(string Id, string Take, string Skip)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/getticketmessage";
                    WebReq_TicketReply objReq = new WebReq_TicketReply();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.TicketId = Id;
                    //objReq.Subject = "";
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.Take = Take;
                    objReq.Skip = Skip;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.ReponseCode == 1)
                        {
                            //result = "success";
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult MyPayUserSubmitReply(string Message, string TicketId, string filename)
        {

            bool hasValidChars = Regex.IsMatch(Message, @"^[a-zA-Z0-9,. ]+$");
            if (!hasValidChars)
            {
                return Json("Special characters are not allowed.");
            }

            var result = string.Empty;



            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/ticketreply";
                    WebReq_TicketReply objReq = new WebReq_TicketReply();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.TicketId = TicketId;
                    objReq.Description = Message;
                    objReq.Image = filename;
                    objReq.Name = Session["MyPayFullName"].ToString();
                    //objReq.Subject = "";
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    // if (hasValidChars) {
                    result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                    // }

                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.ReponseCode == 1)
                        {
                            result = "success";
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayUserChangeTheme()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    AddUser outobject = new AddUser();
                    GetUser inobject = new GetUser();
                    inobject.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    AddUser res = RepCRUD<GetUser, AddUser>.GetRecord(Common.StoreProcedures.sp_Users_Get, inobject, outobject);
                    if (res != null && res.Id > 0)
                    {
                        if (res.IsDarkTheme)
                        {
                            res.IsDarkTheme = false;
                        }
                        else
                        {
                            res.IsDarkTheme = true;
                        }

                        bool IsUpdated = RepCRUD<AddUser, GetUser>.Update(res, "user");
                        if (IsUpdated)
                        {
                            Session["MyPayDarkTheme"] = res.IsDarkTheme;
                            Common.AddLogs("Updated DarkTheme by user(MemberId:" + res.MemberId + ")", false, (int)AddLog.LogType.User, res.MemberId, res.FirstName + " " + res.LastName, false);
                            result = "success";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MyPayUserGetScratchedCoupon(string ServiceId)
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        string ApiName = "api/getScratchedCoupons";
                        WebRequest_ScratchedCoupons objReq = new WebRequest_ScratchedCoupons();
                        objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                        objReq.MemberId = Convert.ToString(Convert.ToInt64(Session["MyPayUserMemberId"]));
                        objReq.ServiceId = ServiceId;
                        objReq.PlatForm = "Web";
                        objReq.SecretKey = Common.SecretKeyForWebAPICall;
                        string JSON = JsonConvert.SerializeObject(objReq);
                        string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                        result = Common.RequestMyPayAPI(ApiName, JSON, JwtToken);
                        if (!string.IsNullOrEmpty(result))
                        {
                            WebCommonResponse objResponse = new WebCommonResponse();
                            objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                            if (objResponse.Message.ToLower() == "success" && objResponse.ReponseCode == 1)
                            {

                            }
                            else if (objResponse.ReponseCode == 7)
                            {
                                RepMyPayUserLogin.Logout();
                                result = objResponse.Message;
                            }
                            else
                            {
                                result = objResponse.Message;
                            }

                        }
                        else
                        {
                            result = "Invalid API Request";
                        }
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }
        [HttpGet]
        public ActionResult MyPayUserCableCar()
        {
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Cable_Car;
            ViewBag.ServiceId = ServiceId;
            //if (Common.CheckServiceDown(ServiceId.ToString()))
            //{
            //    return RedirectToAction("MyPayUserServiceDown", "MyPayUser");
            //}
            return View();
        }
        [HttpGet]
        public ActionResult MyPayUserCableCarList()
        {
            var result = string.Empty;
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Cable_Car;
            ViewBag.ServiceId = ServiceId;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/get_CableTicketTypes";
                    WebRequest_CashbackOffers objReq = new WebRequest_CashbackOffers();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.IsHome = 0;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestCableCarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            GetTicketTypesCableCar TicketTypes = JsonConvert.DeserializeObject<GetTicketTypesCableCar>(result);
                            return View(TicketTypes);
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return View(result);
        }
        [HttpPost]
        public JsonResult MyPayUserCableCarPayment(string list, string CustomerName, string CustomerMobile, string CustomerEmail, string BookDate, string TotalTicket, decimal Amount, string Mpin, string PaymentMode)
        {
            var result = string.Empty;
            List<TicketType> listdata = JsonConvert.DeserializeObject<List<TicketType>>(list);
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/CablePayTransaction";
                    WebRequest_CableCarBook objReq = new WebRequest_CableCarBook();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Session["MyPayUserMemberId"].ToString();
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.UniqueCustomerId = Session["MyPayContactNumber"].ToString();
                    objReq.User = CustomerName;
                    objReq.CustomerWalletId = CustomerMobile;
                    objReq.CustomerEmail = CustomerEmail;
                    objReq.Mpin = Common.Encryption(Mpin);
                    objReq.Pin = Common.Encryption(Mpin);
                    objReq.PaymentMode = PaymentMode;
                    //objReq.TicketRate = Convert.ToDecimal(TicketRate);
                    objReq.NoOfTicket = Convert.ToInt32(TotalTicket);
                    objReq.Amount = Amount.ToString();
                    //objReq.PaymentMethodId = Convert.ToInt32(PaymentMethodId);
                    objReq.Data = list;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestCableCarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status)
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        //[HttpPost]
        //public JsonResult MyPayUserCableCarInvoice(string PaymentMode, string ReferenceNo, string TransactionUniqueId)
        //{
        //    var result = string.Empty;
        //    try
        //    {
        //        if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
        //        {
        //            string ApiName = "api/Get_TicketInvoice";
        //            WebRequest_CableCarBook objReq = new WebRequest_CableCarBook();
        //            objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
        //            objReq.MemberId = Session["MyPayUserMemberId"].ToString();
        //            objReq.PlatForm = "Web";
        //            objReq.SecretKey = Common.SecretKeyForWebAPICall;
        //            objReq.UniqueCustomerId = Session["MyPayContactNumber"].ToString();
        //            objReq.PaymentMode = PaymentMode;
        //            objReq.ReferenceNo = ReferenceNo;
        //            objReq.TransactionId = TransactionUniqueId;
        //            string JSON = JsonConvert.SerializeObject(objReq);
        //            string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
        //            result = Common.RequestCableCarAPI(ApiName, JSON, JwtToken);
        //            if (!string.IsNullOrEmpty(result))
        //            {
        //                WebCommonResponse objResponse = new WebCommonResponse();
        //                objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
        //                if (objResponse.status)
        //                {

        //                }
        //                else if (objResponse.ReponseCode == 7)
        //                {
        //                    RepMyPayUserLogin.Logout();
        //                    result = objResponse.Message;
        //                }
        //                else
        //                {
        //                    result = objResponse.Message;
        //                }
        //            }
        //            else
        //            {
        //                result = "Invalid API Request";
        //            }
        //        }
        //        else
        //        {
        //            result = "Invalid Request";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //    }
        //    return Json(result);
        //}

        [HttpGet]
        public ActionResult MyPayUserCableCarBooking()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    int ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.Cable_Car;
                    var data = VendorApi_CommonHelper.getCableCarDetails(Session["MyPayUserMemberId"].ToString(), ServiceID.ToString());
                    //if (Common.CheckServiceDown(ServiceID.ToString()))
                    //{
                    //    return RedirectToAction("MyPayUserServiceDown", "MyPayUser");
                    //}
                    ViewBag.ServiceId = ServiceID;
                    ViewBag.MyPayContactNumber = Session["MyPayContactNumber"].ToString();
                    ViewBag.MyPayEmail = Session["MyPayEmail"].ToString();
                    ViewBag.MyPayFullName = Session["MyPayFullName"].ToString();
                    ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
                    return View(data);
                }
                else
                {
                    result = "Invalid Request";
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return View(result);
        }
        public ActionResult MyPayUserCableCarTicketDownload(string TransactionId)
        {
            var result = string.Empty;
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Cable_Car;
            ViewBag.ServiceId = ServiceId;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/downloadServiceCableTicket?" + "TransactionID=" + TransactionId;
                    WebRequest_CableCarBook objReq = new WebRequest_CableCarBook();
                    objReq.TransactionId = TransactionId;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.GetCableCarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        GetVendor_API_Airlines_Lookup obj = JsonConvert.DeserializeObject<GetVendor_API_Airlines_Lookup>(result);
                        string pathName = obj.FilePath;
                        string fileName = Path.GetFileName(pathName);
                        try
                        {
                            using (WebClient client = new WebClient())
                            {
                                byte[] fileBytes = client.DownloadData(pathName);
                                return File(fileBytes, "application/pdf", fileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return RedirectToAction("MyPayUserCableCarBooking");
        }
    }
}