using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyPay.Controllers
{
    public class MyPayPaymentsController : Controller
    {
        [HttpGet]

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            string msg = string.Empty;
            string OrderToken = string.Empty;
            string MerchantId = string.Empty;
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
            if (Request.QueryString["OrderToken"] == null || string.IsNullOrEmpty(Request.QueryString["OrderToken"]))
            {
                msg = Common.UnauthorizedRequest; ;
            }
            else if (Request.QueryString["mid"] == null || string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                msg = Common.UnauthorizedRequest;
            }
            else
            {
                if (Common.CheckServiceDown(ServiceId.ToString()))
                {
                    return RedirectToAction("ServiceDown", "MyPayPayments");
                }
                else
                {
                    OrderToken = Request.RawUrl.Split('?')[1].Replace("OrderToken=", "");
                    MerchantId = OrderToken.Split('&')[1].Replace("mid=", "");
                    OrderToken = OrderToken.Split('&')[0];
                    OrderToken = Server.UrlDecode(OrderToken);
                    MerchantId = Server.UrlDecode(MerchantId);
                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = Common.DecryptString(MerchantId);
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                        GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                        inobjectOrders.OrderToken = OrderToken;
                        AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                        if (resOrders == null || resOrders.Id == 0)
                        {
                            msg = Common.UnauthorizedRequest;
                        }
                        else if ((resOrders.Status != (int)AddMerchantOrders.MerchantOrderStatus.Pending) && (resOrders.Status != (int)AddMerchantOrders.MerchantOrderStatus.Incomplete))
                        {
                            msg = Common.UnauthorizedRequest;
                        }
                        else
                        {
                            string[] OrderTokenArray = Common.DecryptionFromKey(OrderToken, res.secretkey).Split(':');
                            if (OrderTokenArray.Length >= 3)
                            {
                                string Token_OrderId = OrderTokenArray[0];
                                string Token_MerchantID = OrderTokenArray[1];
                                string Token_UniqueTxnID = OrderTokenArray[2];
                            }
                            else
                            {
                                msg = Common.UnauthorizedRequest; ;
                            }
                        }
                    }
                    else
                    {
                        msg = Common.UnauthorizedRequest; ;
                    }
                }
            }
            ViewBag.PhoneNumber = "";
            ViewBag.OrderToken = OrderToken;
            ViewBag.MerchantId = MerchantId;
            ViewBag.Message = msg;
            return View();
        }

        [HttpPost]


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(string Phonenumber, string Password, FormCollection collection)
        {
            ViewBag.Message = "";
            string msg = string.Empty;
            string hdnOrderToken = collection.Get("hdnOrderToken");
            string hdnMerchantId = collection.Get("hdnMerchantId");
            if (string.IsNullOrEmpty(Phonenumber))
            {
                msg = "Please Enter Phone number ";
            }
            else if (string.IsNullOrEmpty(Password))
            {
                msg = "Please Enter Password ";
            }
            else if (string.IsNullOrEmpty(hdnOrderToken))
            {
                msg = "OrderToken not found";
            }
            else if (string.IsNullOrEmpty(hdnMerchantId))
            {
                msg = "MerchantId not found";
            }
            else
            {
                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
                if (Common.CheckServiceDown(ServiceId.ToString()))
                {
                    return RedirectToAction("ServiceDown", "MyPayPayments");
                }
                else
                {
                    string MerchantID = Common.DecryptString(hdnMerchantId);
                    AddMerchant outobjectMerchant = new AddMerchant();
                    GetMerchant inobjectMerchant = new GetMerchant();
                    inobjectMerchant.MerchantUniqueId = MerchantID;
                    inobjectMerchant.CheckActive = 1;
                    AddMerchant resMerchant = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectMerchant, outobjectMerchant);
                    if (resMerchant != null && resMerchant.Id != 0)
                    {
                        string[] OrderTokenArray = Common.DecryptionFromKey(hdnOrderToken, resMerchant.secretkey).Split(':');
                        if (OrderTokenArray.Length >= 3)
                        {
                            string Token_OrderId = OrderTokenArray[0];
                            string Token_MerchantID = OrderTokenArray[1];
                            string Token_UniqueTxnID = OrderTokenArray[2];

                            if (Token_MerchantID == MerchantID)
                            {
                                AddUserByPhone outobjectUser = new AddUserByPhone();
                                GetUserByPhone inobjectUser = new GetUserByPhone();
                                inobjectUser.ContactNumber = Phonenumber;
                                AddUserByPhone modelUser = RepCRUD<GetUserByPhone, AddUserByPhone>.GetRecord("sp_Users_GetByPhoneNumber", inobjectUser, outobjectUser);
                                if (modelUser != null && modelUser.MemberId != 0)
                                {
                                    if (modelUser.Password == Common.EncryptString(Password))
                                    {
                                        if (modelUser.IsActive == false)
                                        {
                                            msg = "User is In-active. Please try again later";
                                        }
                                        else
                                        {
                                            AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                            GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                            inobjectOrders.MerchantId = MerchantID;
                                            inobjectOrders.OrderId = Token_OrderId;
                                            inobjectOrders.TransactionId = Token_UniqueTxnID;
                                            AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                            if (resOrders != null || resOrders.Id != 0)
                                            {
                                                if ((resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Pending) || (resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Incomplete))
                                                {
                                                    resOrders.MemberId = modelUser.MemberId;
                                                    resOrders.MemberName = modelUser.FirstName + " " + modelUser.LastName;
                                                    resOrders.MemberContactNumber = modelUser.ContactNumber;
                                                    resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Pending;
                                                    resOrders.Remarks = "Order Pending for Contact no." + modelUser.ContactNumber;
                                                    resOrders.UpdatedBy = modelUser.MemberId;
                                                    resOrders.UpdatedByName = modelUser.FirstName + " " + modelUser.LastName;
                                                    bool IsOrderUpdate = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                                    if (IsOrderUpdate)
                                                    {
                                                        msg = "success";
                                                        Session["Phonenumber"] = inobjectUser.ContactNumber;
                                                        Common.AddLogs($"Mypay Merchant Orders Transaction Updated Successfully on {Common.fnGetdatetime()} for OrderID: {Token_OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), modelUser.MemberId, modelUser.FirstName + " " + modelUser.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);

                                                        var routeValues = new RouteValueDictionary { { "OrderToken", hdnOrderToken }, { "mid", hdnMerchantId } };
                                                        return RedirectToAction("CommitTransaction", "MyPayPayments", routeValues);
                                                    }
                                                    else
                                                    {
                                                        msg = "Order Update Failed. Please Try Again or Contact Support";
                                                    }
                                                }
                                                else
                                                {
                                                    msg = Common.UnauthorizedRequest;
                                                    ViewBag.Message = msg;
                                                }
                                            }
                                            else
                                            {
                                                msg = Common.UnauthorizedRequest; ;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg = "Phonenumber or password is invalid. Please try again.";
                                    }
                                }
                                else
                                {
                                    msg = "Invalid User. Please try again later.";
                                }
                            }
                            else
                            {
                                msg = Common.UnauthorizedRequest; ;
                            }
                        }
                        else
                        {
                            msg = Common.UnauthorizedRequest; ;
                        }
                    }
                    else
                    {
                        msg = Common.UnauthorizedRequest; ;
                    }
                }
            }
            ViewBag.OrderToken = hdnOrderToken;
            ViewBag.MerchantId = hdnMerchantId;
            ViewBag.PhoneNumber = Phonenumber;
            ViewBag.UserMessage = msg;
            return View();
        }
        [HttpGet]

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult CommitTransaction()
        {
            string msg = string.Empty;
            string OrderToken = string.Empty;
            string MerchantId = string.Empty;
            string TransactionID = string.Empty;
            string TransactionAmount = string.Empty;
            string ServiceCharge = string.Empty;
            string Cashback = string.Empty;
            string Discount = string.Empty;
            string NetAmount = string.Empty;
            string WalletBalance = string.Empty;
            string smartpayRupees = string.Empty;
            string smartpayCoins = string.Empty;
            string smartpayBalance = string.Empty;

            try
            {

                if (Request.QueryString["OrderToken"] == null || string.IsNullOrEmpty(Request.QueryString["OrderToken"]))
                {
                    msg = Common.UnauthorizedRequest; ;
                }
                else if (Request.QueryString["mid"] == null || string.IsNullOrEmpty(Request.QueryString["mid"]))
                {
                    msg = Common.UnauthorizedRequest; ;
                }
                else
                {
                    MerchantId = Convert.ToString(Request.QueryString["mid"]);
                    OrderToken = Request.RawUrl.Split('?')[1].Replace("OrderToken=", "");
                    if (!string.IsNullOrEmpty(MerchantId))
                    {
                        OrderToken = OrderToken.Split('&')[0];
                        OrderToken = Server.UrlDecode(OrderToken);
                    }
                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = Common.DecryptString(MerchantId);
                    AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                        GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                        inobjectOrders.OrderToken = OrderToken;
                        AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                        if (resOrders != null || resOrders.Id != 0)
                        {
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
                            AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceChargeMerchant(resOrders.MerchantId, resOrders.Amount.ToString(), VendorApiType.ToString());

                            string[] OrderTokenArray = Common.DecryptionFromKey(OrderToken, res.secretkey).Split(':');
                            if (OrderTokenArray.Length >= 3)
                            {
                                string Token_OrderId = OrderTokenArray[0];
                                string Token_MerchantID = OrderTokenArray[1];
                                string Token_UniqueTxnID = OrderTokenArray[2];
                                TransactionID = Token_UniqueTxnID;
                                TransactionAmount = "Rs. " + Convert.ToString(resOrders.Amount);
                                ServiceCharge = "Rs. " + Convert.ToString(resOrders.ServiceCharges);
                                Cashback = "Rs. " + Convert.ToString(objOut.CashbackAmount);
                                Discount = "Rs. " + Convert.ToString(resOrders.DiscountAmount);
                                NetAmount = "Rs. " + Convert.ToString(objOut.NetAmount);
                                smartpayCoins = "Rs. " + Convert.ToString(objOut.MPCoinsDebit);
                                smartpayRupees = "Rs. " + Convert.ToString(resOrders.Amount - objOut.MPCoinsDebit);

                                if (Session["Phonenumber"] == null || Convert.ToString(Session["Phonenumber"]) == "")
                                {

                                    var routeValues = new RouteValueDictionary { { "OrderToken", OrderToken }, { "mid", MerchantId } };
                                    return RedirectToAction("Index", "MyPayPayments", routeValues);
                                }
                                if ((resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Pending) || (resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Incomplete))
                                {
                                    AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                                    GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                                    inobjectUser.ContactNumber = Convert.ToString(Session["Phonenumber"]);
                                    AddUserLoginWithPin modelUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                                    if (modelUser != null && modelUser.MemberId != 0)
                                    {
                                        WalletBalance = "Rs. " + Convert.ToString(modelUser.TotalAmount);

                                        smartpayBalance = "Rs. " + Convert.ToString(modelUser.TotalRewardPoints);
                                        if (modelUser.TotalAmount < objOut.NetAmount)
                                        {
                                            ViewBag.UserInsufficeintBalanceMessage = "You donot have enough balance to continue this transaction";
                                        }
                                        if (resOrders.OTPAttemptCount > 3)
                                        {
                                            ViewBag.UserInsufficeintBalanceMessage = "Invalid OTP Attempt Exceeded";
                                        }
                                    }
                                    else
                                    {
                                        msg = Common.UnauthorizedRequest; ;
                                    }
                                }
                                else
                                {
                                    msg = "Order Already Updated";
                                }
                            }
                            else
                            {
                                msg = Common.UnauthorizedRequest; ;
                            }
                        }
                        else
                        {
                            msg = Common.UnauthorizedRequest; ;
                        }
                    }
                    else
                    {
                        msg = Common.UnauthorizedRequest; ;
                    }
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            ViewBag.OrderToken = OrderToken;
            ViewBag.MerchantId = MerchantId;
            ViewBag.TransactionID = TransactionID;
            ViewBag.Amount = TransactionAmount;
            ViewBag.ServiceCharge = ServiceCharge;
            ViewBag.smartpayRupees = smartpayRupees;
            ViewBag.smartpayCoins = smartpayCoins;
            ViewBag.smartpayBalance = smartpayBalance;
            ViewBag.Cashback = Cashback;
            ViewBag.Discount = Discount;
            ViewBag.NetAmount = NetAmount;
            ViewBag.Balance = WalletBalance;
            ViewBag.Phonenumber = Convert.ToString(Session["Phonenumber"]);
            ViewBag.Message = msg;
            return View();
        }

        [HttpPost]

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult CommitTransaction(string hdnOrderToken, string hdnMerchantId, string OTP, string hdnCancelled, string hfPaymentMode)
        {
            ViewBag.Message = "";
            string msg = string.Empty;
            string Usermsg = string.Empty;
            string TransactionID = string.Empty;
            string TransactionAmount = string.Empty;
            string TransactionUserWalletBalance = string.Empty;
            string ServiceCharge = string.Empty;
            string NetAmount = string.Empty;
            string Cashback = string.Empty;
            string Discount = string.Empty;
            string smartpayRupees = string.Empty;
            string smartpayCoins = string.Empty;
            string smartpayBalance = string.Empty;

            if (string.IsNullOrEmpty(OTP))
            {
                msg = "Please enter OTP";
            }
            else if (string.IsNullOrEmpty(hdnOrderToken))
            {
                msg = "OrderToken not found";
            }
            else if (string.IsNullOrEmpty(hdnMerchantId))
            {
                msg = "MerchantId not found";
            }

            try
            {

                string MerchantId = Common.DecryptString(hdnMerchantId);
                AddMerchant outobjectMerchant = new AddMerchant();
                GetMerchant inobjectMerchant = new GetMerchant();
                inobjectMerchant.MerchantUniqueId = MerchantId;
                AddMerchant resMerchant = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectMerchant, outobjectMerchant);
                if (resMerchant != null && resMerchant.Id != 0 && !string.IsNullOrEmpty(MerchantId))
                {

                    string[] OrderTokenArray = Common.DecryptionFromKey(hdnOrderToken, resMerchant.secretkey).Split(':');
                    if (OrderTokenArray.Length >= 3)
                    {
                        string Token_OrderId = OrderTokenArray[0];
                        string Token_MerchantID = OrderTokenArray[1];
                        string Token_UniqueTxnID = OrderTokenArray[2];

                        if (Token_MerchantID == MerchantId)
                        {
                            if (Session["Phonenumber"] == null || Convert.ToString(Session["Phonenumber"]) == "")
                            {
                                var routeValues = new RouteValueDictionary { { "OrderToken", hdnOrderToken }, { "mid", hdnMerchantId } };
                                return RedirectToAction("Index", "MyPayPayments", routeValues);
                            }
                            AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                            GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                            inobjectUser.CheckDelete = 0;
                            inobjectUser.ContactNumber = Convert.ToString(Session["Phonenumber"]);
                            AddUserLoginWithPin modelUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                            if (modelUser != null && modelUser.Id != 0)
                            {
                                if (modelUser.IsActive == false)
                                {
                                    Usermsg = "User is inactive.";
                                }
                                else
                                {
                                    AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                    GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                    inobjectOrders.MerchantId = MerchantId;
                                    inobjectOrders.OrderId = Token_OrderId;
                                    inobjectOrders.TransactionId = Token_UniqueTxnID;
                                    AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                    if (resOrders != null && resOrders.Id != 0 && (resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Pending || resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Incomplete))
                                    {
                                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
                                        AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceChargeMerchant(resOrders.MerchantId, resOrders.Amount.ToString(), VendorApiType.ToString());

                                        TransactionID = Token_UniqueTxnID;
                                        TransactionUserWalletBalance = "Rs. " + Convert.ToString(modelUser.TotalAmount);


                                        TransactionAmount = "Rs. " + Convert.ToString(resOrders.Amount);
                                        ServiceCharge = "Rs. " + Convert.ToString(resOrders.ServiceCharges);
                                        NetAmount = "Rs. " + Convert.ToString(objOut.NetAmount);
                                        Cashback = "Rs. " + Convert.ToString(objOut.CashbackAmount);
                                        Discount = "Rs. " + Convert.ToString(resOrders.DiscountAmount);
                                        smartpayCoins = "Rs. " + Convert.ToString(objOut.MPCoinsDebit);
                                        smartpayRupees = "Rs. " + Convert.ToString(resOrders.Amount - objOut.MPCoinsDebit);
                                        smartpayBalance = "Rs. " + Convert.ToString(modelUser.TotalRewardPoints);

                                        if (!string.IsNullOrEmpty(hdnCancelled))
                                        {
                                            resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Cancelled;
                                            resOrders.Remarks = hdnCancelled + " By  Contact no." + modelUser.ContactNumber;
                                            resOrders.UpdatedBy = modelUser.MemberId;
                                            resOrders.UpdatedByName = modelUser.FirstName + " " + modelUser.LastName;
                                            bool IsOrderUpdate = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                            if (IsOrderUpdate)
                                            {
                                                Usermsg = "success";
                                                Common.AddLogs($"Mypay Merchant Order Cancelled on {Common.fnGetdatetime()} For OrderID: {Token_OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), modelUser.MemberId, modelUser.FirstName + " " + modelUser.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                                if (!string.IsNullOrEmpty(resOrders.ReturnUrl))
                                                {
                                                    return Redirect(resOrders.ReturnUrl + "?MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                                                }
                                                else
                                                {
                                                    if (!string.IsNullOrEmpty(resMerchant.CancelURL))
                                                    {
                                                        return Redirect(resMerchant.CancelURL + "?MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                                                    }
                                                    else
                                                    {
                                                        return Redirect(resMerchant.WebsiteURL + "?MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Usermsg = "Order Update Failed. Please Try Again or Contact Support";
                                            }
                                        }
                                        else if (resOrders.OTPAttemptCount > 3)
                                        {
                                            Usermsg = "Invalid OTP Attempt Exceeded";
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(OTP) && OTP == Common.DecryptString(resOrders.OrderOTP))
                                            {
                                                if (Convert.ToDecimal(modelUser.TotalAmount) < (Convert.ToDecimal(resOrders.Amount + resOrders.ServiceCharges)))
                                                {
                                                    msg = Common.InsufficientBalance;
                                                    resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                                                    resOrders.Remarks = "Order Failed Due To Insufficient Balance for Contact no." + modelUser.ContactNumber;
                                                    resOrders.UpdatedBy = modelUser.MemberId;
                                                    resOrders.UpdatedByName = modelUser.FirstName + " " + modelUser.LastName;
                                                    bool IsOrderUpdate = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                                    if (IsOrderUpdate)
                                                    {
                                                        Usermsg = "success";
                                                        Common.AddLogs($"Mypay Merchant Orders Transaction Failed Due To Insufficient Balance on {Common.fnGetdatetime()} for OrderID: {Token_OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), modelUser.MemberId, modelUser.FirstName + " " + modelUser.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                                                        if (!string.IsNullOrEmpty(resOrders.ReturnUrl))
                                                        {
                                                            return Redirect(resOrders.ReturnUrl + "?MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                                                        }
                                                        else
                                                        {
                                                            if (!string.IsNullOrEmpty(resMerchant.CancelURL))
                                                            {
                                                                return Redirect(resMerchant.CancelURL + "?MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                                                            }
                                                            else
                                                            {
                                                                return Redirect(resMerchant.WebsiteURL + "?MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Usermsg = "Order Update Failed. Please Try Again or Contact Support";
                                                    }
                                                }
                                                else
                                                {
                                                    return MerchantTransactionCommit(out Usermsg, resMerchant, Token_OrderId, modelUser, resOrders, hfPaymentMode);
                                                }
                                            }
                                            else
                                            {
                                                resOrders.OTPAttemptCount = resOrders.OTPAttemptCount + 1;
                                                bool InvalidOTPFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                                                Usermsg = "Invalid OTP";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Message = Common.UnauthorizedRequest;
                                    }
                                }
                            }
                            else
                            {
                                Usermsg = "Invalid User. Please try again.";
                            }
                        }
                        else
                        {
                            ViewBag.Message = Common.UnauthorizedRequest; ;
                        }
                    }
                    else
                    {
                        ViewBag.Message = Common.UnauthorizedRequest; ;
                    }
                }
                else
                {
                    ViewBag.Message = Common.UnauthorizedRequest; ;
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            ViewBag.UserMessage = Usermsg;
            ViewBag.OrderToken = hdnOrderToken;
            ViewBag.MerchantId = hdnMerchantId;
            ViewBag.TransactionID = TransactionID;
            ViewBag.Amount = TransactionAmount;
            ViewBag.ServiceCharge = ServiceCharge;
            ViewBag.smartpayRupees = smartpayRupees;
            ViewBag.smartpayCoins = smartpayCoins;
            ViewBag.smartpayBalance = smartpayBalance;
            ViewBag.Cashback = Cashback;
            ViewBag.Discount = Discount;
            ViewBag.NetAmount = NetAmount;
            ViewBag.Balance = TransactionUserWalletBalance;
            ViewBag.Phonenumber = Convert.ToString(Session["Phonenumber"]);
            return View();
        }

        private ActionResult MerchantTransactionCommit(out string Usermsg, AddMerchant resMerchant, string Token_OrderId, AddUserLoginWithPin modelUser, AddMerchantOrders resOrders, string WalletType = "0")
        {
            Usermsg = string.Empty;
            bool IsSuccess = false;
            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
            AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceChargeMerchant(resMerchant.MerchantUniqueId, resOrders.Amount.ToString(), VendorApiType.ToString());
            WalletTransactions res_transaction = new WalletTransactions();
            string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
            string ReferenceNo = Common.GenerateReferenceUniqueID();
            decimal NetAmount = objOut.NetAmount;
            decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(modelUser.TotalAmount) - NetAmount);

            if (WalletType == "0" || WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Wallet))
            {
                WalletBalance = Convert.ToDecimal(Convert.ToDecimal(modelUser.TotalAmount) - (Convert.ToDecimal(objOut.NetAmount)));
            }
            else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins))
            {
                WalletBalance = Convert.ToDecimal(Convert.ToDecimal(modelUser.TotalAmount) - ((Convert.ToDecimal(objOut.NetAmount) - objOut.MPCoinsDebit)));
            }
            try
            {
                res_transaction.MemberId = modelUser.MemberId;
                res_transaction.ContactNumber = modelUser.ContactNumber;
                res_transaction.MemberName = modelUser.FirstName + " " + modelUser.MiddleName + " " + modelUser.LastName;
                res_transaction.ServiceCharge = Convert.ToDecimal(resOrders.ServiceCharges);
                res_transaction.NetAmount = NetAmount;
                res_transaction.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
                res_transaction.ParentTransactionId = resOrders.TransactionId;
                res_transaction.CurrentBalance = Convert.ToDecimal(WalletBalance);
                res_transaction.CreatedBy = resOrders.MemberId;
                res_transaction.CreatedByName = resOrders.MemberName;
                res_transaction.TransactionUniqueId = TransactionUniqueID;
                res_transaction.Remarks = $"MyPay Payments Transaction Successfully Completed With Order ID: {resOrders.OrderId}";
                res_transaction.Type = (int)(VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut);
                res_transaction.Description = $"MyPay Payments Transaction Completed. Order ID: {resOrders.OrderId} and Order TransactionID: {resOrders.TransactionId}";
                res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                res_transaction.Reference = resOrders.OrderId;
                res_transaction.IsApprovedByAdmin = true;
                res_transaction.IsActive = true;
                res_transaction.CashBack = objOut.CashbackAmount;
                res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Reciever;
                res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Debit);
                res_transaction.DeviceCode = resOrders.DeviceCode;
                res_transaction.Platform = "Web";
                res_transaction.WalletType = Convert.ToInt32(WalletType);
                res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                res_transaction.MerchantMemberId = resMerchant.MerchantMemberId;
                res_transaction.CustomerID = resMerchant.OrganizationName + " (" + resMerchant.MerchantUniqueId + ")";
                res_transaction.MerchantId = resMerchant.MerchantUniqueId;
                res_transaction.MerchantOrganization = resMerchant.OrganizationName;
                res_transaction.RewardPointBalance = (modelUser.TotalRewardPoints);
                if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Wallet))
                {
                    res_transaction.Amount = Convert.ToDecimal(resOrders.Amount - resOrders.DiscountAmount);
                    res_transaction.TransactionAmount = objOut.Amount;
                    res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                }
                else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins))
                {
                    res_transaction.Amount = Convert.ToDecimal(resOrders.Amount - resOrders.DiscountAmount - objOut.MPCoinsDebit);
                    res_transaction.TransactionAmount = objOut.Amount;
                    res_transaction.MPCoinsDebit = objOut.MPCoinsDebit;
                    res_transaction.RewardPointBalance = (modelUser.TotalRewardPoints) - objOut.MPCoinsDebit;
                    res_transaction.WalletType = (int)WalletTransactions.WalletTypes.MPCoins;
                }
                if (res_transaction.Add())
                {

                    res_transaction.Id = 0;
                    res_transaction.AddCashBack();
                    resOrders.TrackerId = res_transaction.VendorTransactionId;
                    resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Success;
                    resOrders.Remarks = "Order Completed for Contact no." + modelUser.ContactNumber;
                    resOrders.UpdatedBy = modelUser.MemberId;
                    resOrders.UpdatedByName = modelUser.FirstName + " " + modelUser.LastName;
                    resOrders.NetAmount = NetAmount;
                    resOrders.CurrentBalance = (resMerchant.MerchantTotalAmount + (NetAmount - resOrders.CommissionAmount));
                    bool IsOrderUpdate = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                    if (IsOrderUpdate)
                    {
                        Usermsg = "success";
                        resMerchant.MerchantTotalAmount = (resMerchant.MerchantTotalAmount + (NetAmount - resOrders.CommissionAmount));
                        bool IsWalletUpdated = (RepCRUD<AddMerchant, GetMerchant>.Update(resMerchant, "merchant"));
                        if (IsWalletUpdated)
                        {
                            string mystring = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Templates/MerchantTransfer.html"));
                            string body = mystring;
                            body = body.Replace("##UserName##", modelUser.FirstName + modelUser.LastName);
                            body = body.Replace("##OrganizationName##", resOrders.OrganizationName);
                            body = body.Replace("##MemberName##", resOrders.MemberName);
                            body = body.Replace("##Amount##", (resOrders.Amount).ToString("0.00"));
                            body = body.Replace("##TransactionId##", TransactionUniqueID);
                            body = body.Replace("##OrderId##", Token_OrderId);
                            body = body.Replace("##TransactionDate##", DateTime.UtcNow.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                            body = body.Replace("##PaidBy##", modelUser.ContactNumber);
                            body = body.Replace("##PaidTo##", res_transaction.RecieverBranch + "[" + res_transaction.RecieverName + "]");
                            //body = body.Replace("##Amount##", (resMerchant.MerchantTotalAmount).ToString("0.00"));
                            body = body.Replace("##Cashback##", res_transaction.CashBack.ToString("0.00"));
                            body = body.Replace("##Commission##", res_transaction.ServiceCharge.ToString("0.00"));
                            body = body.Replace("##ServiceCharge##", res_transaction.ServiceCharge.ToString("0.00"));
                            body = body.Replace("##Purpose##", "Merchant Payment");
                            body = body.Replace("##Remarks##", "Paid Successfully");
                            string Subject = MyPay.Models.Common.Common.WebsiteName + " - Merchant Payment Received";
                            if (!string.IsNullOrEmpty(resMerchant.EmailID))
                            {
                                body = body.Replace("##UserName##", resMerchant.FirstName);
                                Common.SendAsyncMail(resMerchant.EmailID, Subject, body);
                            }

                            IsSuccess = true;
                            Common.AddLogs($"Mypay Merchant Orders Transaction Completed Successfully on {Common.fnGetdatetime()} for OrderID: {Token_OrderId}", false, Convert.ToInt32(AddLog.LogType.Merchant), modelUser.MemberId, modelUser.FirstName + " " + modelUser.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);

                        }
                        else
                        {
                            Common.AddLogs($"Mypay Merchant Orders Transaction Completed. Merchant Wallet Update Failed on {Common.fnGetdatetime()} for OrderID: {Token_OrderId}", false, Convert.ToInt32(AddLog.LogType.Merchant), modelUser.MemberId, modelUser.FirstName + " " + modelUser.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                        }
                        if (!string.IsNullOrEmpty(resOrders.ReturnUrl))
                        {
                            return Redirect(resOrders.ReturnUrl + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(resMerchant.SuccessURL))
                            {
                                return Redirect(resMerchant.SuccessURL + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                            }
                            else
                            {
                                return Redirect(resMerchant.WebsiteURL + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                            }
                        }
                    }
                    else
                    {
                        resOrders.TrackerId = ReferenceNo;
                        resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                        resOrders.Remarks = "Order Failed for Contact no." + modelUser.ContactNumber;
                        resOrders.UpdatedBy = modelUser.MemberId;
                        resOrders.UpdatedByName = modelUser.FirstName + " " + modelUser.LastName;
                        RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                        Usermsg = "Order Update Failed. Please Try Again or Contact Support";
                        Common.AddLogs($"Mypay Merchant Orders Transaction Failed on {Common.fnGetdatetime()} for OrderID: {Token_OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), modelUser.MemberId, modelUser.FirstName + " " + modelUser.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);

                        if (!string.IsNullOrEmpty(resMerchant.CancelURL))
                        {
                            return Redirect(resMerchant.CancelURL + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                        }
                        else
                        {
                            return Redirect(resMerchant.WebsiteURL + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                        }
                    }
                }
                else
                {
                    resOrders.TrackerId = ReferenceNo;
                    resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                    resOrders.Remarks = "Transaction Failed for Contact no." + modelUser.ContactNumber;
                    resOrders.UpdatedBy = modelUser.MemberId;
                    resOrders.UpdatedByName = modelUser.FirstName + " " + modelUser.LastName;
                    RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                    Usermsg = "Order Update Failed. Please Try Again or Contact Support";
                    Common.AddLogs($"Mypay Merchant Orders Transaction Failed on {Common.fnGetdatetime()} for OrderID: {Token_OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), modelUser.MemberId, modelUser.FirstName + " " + modelUser.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                    if (!string.IsNullOrEmpty(resOrders.ReturnUrl))
                    {
                        return Redirect(resOrders.ReturnUrl + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(resMerchant.CancelURL))
                        {
                            return Redirect(resMerchant.CancelURL + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                        }
                        else
                        {
                            return Redirect(resMerchant.WebsiteURL + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                resOrders.Remarks = ex.Message;
                resOrders.UpdatedBy = modelUser.MemberId;
                resOrders.UpdatedByName = modelUser.FirstName + " " + modelUser.LastName;
                RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                Usermsg = ex.Message;
                Common.AddLogs($"Mypay Merchant Orders Transaction Failed on {Common.fnGetdatetime()} for OrderID: {Token_OrderId}", true, Convert.ToInt32(AddLog.LogType.Merchant), modelUser.MemberId, modelUser.FirstName + " " + modelUser.LastName, false, "", "", Convert.ToInt32(AddLog.LogActivityEnum.MerchantTransactions), Common.CreatedBy, Common.CreatedByName);
                if (!string.IsNullOrEmpty(resOrders.ReturnUrl))
                {
                    return Redirect(resOrders.ReturnUrl + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(resMerchant.CancelURL))
                    {
                        return Redirect(resMerchant.CancelURL + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                    }
                    else
                    {
                        return Redirect(resMerchant.WebsiteURL + "?GatewayTransactionId=" + res_transaction.VendorTransactionId + "&MerchantTransactionId=" + resOrders.TransactionId + "&TrnxId=" + resOrders.OrderId);
                    }
                }
            }
        }
        [HttpPost]
        public JsonResult GenerateTransactionOTP(string OrderToken, string MerchantId, string Phonenumber)
        {
            string msg = "";
            try
            {

                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = Common.DecryptString(MerchantId);
                AddMerchant res = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                if (res != null && res.Id != 0)
                {
                    AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                    GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                    inobjectOrders.OrderToken = OrderToken;
                    AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                    if (resOrders != null || resOrders.Id != 0 && (resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Pending || resOrders.Status == (int)AddMerchantOrders.MerchantOrderStatus.Incomplete))
                    {
                        if (Phonenumber == resOrders.MemberContactNumber)
                        {
                            AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                            GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                            inobjectUser.ContactNumber = Phonenumber;
                            AddUserLoginWithPin modelUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                            if (modelUser != null && modelUser.Id != 0)
                            {
                                string[] OrderTokenArray = Common.DecryptionFromKey(OrderToken, res.secretkey).Split(':');
                                if (OrderTokenArray.Length >= 3)
                                {
                                    string Token_OrderId = OrderTokenArray[0];
                                    string Token_MerchantID = OrderTokenArray[1];
                                    string Token_UniqueTxnID = OrderTokenArray[2];
                                    string OTP = Common.RandomNumber(100000, 999999).ToString();
                                    if (Common.ApplicationEnvironment.IsProduction == false)
                                    {
                                        OTP = "123456";
                                    }
                                    resOrders.OrderOTP = Common.EncryptString(OTP);
                                    resOrders.Remarks = "Sent Order OTP for Contact no." + modelUser.ContactNumber;
                                    resOrders.UpdatedBy = modelUser.MemberId;
                                    resOrders.UpdatedByName = modelUser.FirstName + " " + modelUser.LastName;
                                    bool IsOrderUpdate = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                    if (IsOrderUpdate)
                                    {
                                        Models.Common.Common.SendSMS(modelUser.ContactNumber, $"Your OTP For MyPay Payment is {OTP}. Thank you for using MyPay.");
                                        msg = "success";
                                    }
                                }
                                else
                                {
                                    msg = Common.UnauthorizedRequest; ;
                                }
                            }
                            else
                            {
                                msg = Common.UnauthorizedRequest; ;
                            }
                        }
                        else
                        {
                            msg = Common.UnauthorizedRequest; ;
                        }
                    }
                    else
                    {
                        msg = Common.UnauthorizedRequest; ;
                    }
                }
                else
                {
                    msg = Common.UnauthorizedRequest; ;
                }
            }
            catch (Exception ex)
            {

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult ServiceDown()
        {
            ViewBag.Message = "";
            return View();
        }
    }
}