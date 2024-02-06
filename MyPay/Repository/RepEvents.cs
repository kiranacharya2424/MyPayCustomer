using log4net;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using CommonUtils;
using RestSharp;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Digests;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Get.FonePay;
using MyPay.Models.Request;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using MyPay.Models.Get.Events;
using ServiceStack.Web;
using MyPay.Models.GenericCoupons;
using log4net;


namespace MyPay.Repository
{
    public class RepEvents
    {
        public static Int64 Id = 0;
        public static string UniqueTransactionId = string.Empty;
        public static string TransactionUniqueId = string.Empty;
        private static int InputNumber_Digits = 10;
        public static AddVendor_API_Requests resKhalti = new AddVendor_API_Requests();
        public static decimal WalletBalance = 0;
        public static string RequestServiceGroup_Events(ref AddVendor_API_Requests objVendor_API_Requests, int pageSize, int pageNumber, string searchVal, string sortOrder, string dateFrom, string dateTo, string sortBy, ref GetVendor_API_Events objRes)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Info("HELLLLLLLLLLO" );

            string msg = "";
            if (string.IsNullOrEmpty(dateFrom))
            {
                msg = "Please select start date.";
            }

            else if (string.IsNullOrEmpty(dateTo))
            {
                msg = "Please select end date.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                string JsonResponse = string.Empty;
                string EventsAPIURL = "/clientapi/get-event-list/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events(pageSize, pageNumber, searchVal, sortOrder, dateFrom, dateTo, sortBy);
                string URL = VendorApi_CommonHelper.EVENTS_API_URL_LINK + EventsAPIURL;
                log.Info("fetching events from URL: " + URL + "\n with JSON request: " + JsonReq);

                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events(JsonReq, URL, ref JsonResponse);
                objVendor_API_Requests.Req_Khalti_URL = URL;
                objVendor_API_Requests.Req_Khalti_Input = JsonReq;
                objVendor_API_Requests.Req_Token = $"CLIENT_CODE: {VendorApi_CommonHelper.EVENTS_API_CLIENT_CODE} USER_NAME:{VendorApi_CommonHelper.EVENTS_USER_NAME}  API_KEY:{VendorApi_CommonHelper.EVENTS_API_KEY}  ";
                objVendor_API_Requests.Res_Khalti_Output = JsonResponse;
                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                msg = objRes.message;
            }
            return msg;
        }

        public static string RequestServiceGroup_Events_Details(ref AddVendor_API_Requests objVendor_API_Requests, int eventId, string eventDate, string merchantCode, ref GetVendor_API_Events_Details objRes)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


            string msg = "";
            if (string.IsNullOrEmpty(eventDate))
            {
                msg = "Please select event Date.";
            }

            else if (string.IsNullOrEmpty(merchantCode))
            {
                msg = "Please enter merchant Code.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                string VendorURL = string.Empty;
                string JsonResponse = string.Empty;
                string EventsAPIURL = "/clientapi/get-event-details/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_Details(eventId, eventDate, merchantCode);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events_Details(EventsAPIURL, JsonReq, ref VendorURL, ref JsonResponse);

                log.Info("fetching events from URL: " + VendorURL + "\n with JSON request: " + JsonReq);



                objVendor_API_Requests.Req_Token = VendorApi_CommonHelper.EVENTS_API_KEY;
                objVendor_API_Requests.Req_Khalti_URL = VendorURL;
                objVendor_API_Requests.Req_Khalti_Input = JsonReq;
                objVendor_API_Requests.Res_Khalti_Output = JsonResponse;
                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                msg = objRes.message;
            }
            return msg;
        }


        public static string RequestServiceGroup_Event_Ticket(ref AddVendor_API_Requests objVendor_API_Requests, string merchantCode, string customerName, string customerMobile, string customerEmail, int eventId, int ticketCategoryId, string ticketCategoryName, string eventDate, decimal ticketRate, int noOfTicket, decimal totalPrice, int paymentMethodId, string DeviceId, ref GetVendor_API_Events_Ticket objRes,
           string CouponCode = ""
           )
        {
            string msg = "";
            if (string.IsNullOrEmpty(merchantCode))
            {
                msg = "Please enter merchant Code.";
            }
            else if (string.IsNullOrEmpty(customerName))
            {
                msg = "Please enter customerName.";
            }
            else if (string.IsNullOrEmpty(customerMobile))
            {
                msg = "Please enter customer Mobile.";
            }
            else if (string.IsNullOrEmpty(customerEmail))
            {
                msg = "Please enter customer Email.";
            }
            else if (eventId == 0)
            {
                msg = "Please enter event Id.";
            }
            else if (ticketCategoryId == 0)
            {
                msg = "Please select ticket Category.";
            }
            else if (string.IsNullOrEmpty(ticketCategoryName))
            {
                msg = "Please enter ticketCategoryName.";
            }
            else if (string.IsNullOrEmpty(eventDate))
            {
                msg = "Please select event Date.";
            }
            else if (string.IsNullOrEmpty(DeviceId))
            {
                msg = "Please enter DeviceId.";
            }
            else if (noOfTicket < 1)
            {
                msg = "Please enter no Of Ticket.";
            }
            else if (paymentMethodId < 1)
            {
                msg = "Please select payment Method.";
            }
            else if (ticketRate == 0)
            {
                msg = "Please enter Ticket Rate.";
            }
            else if (totalPrice == 0)
            {
                msg = "Please enter total Price.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                string VendorURL = string.Empty;
                string VendorOutput = string.Empty;

                string EventsAPIURL = "/clientapi/book-event-tickets/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_Ticket(merchantCode, customerName, customerMobile, customerEmail, eventId, ticketCategoryId, eventDate, ticketRate, noOfTicket, totalPrice, paymentMethodId, CouponCode);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events_Ticket(EventsAPIURL, JsonReq, ref VendorURL, ref VendorOutput);
                objVendor_API_Requests.Req_Token = VendorApi_CommonHelper.EVENTS_API_KEY;
                objVendor_API_Requests.Req_Khalti_URL = VendorURL;
                objVendor_API_Requests.Req_Khalti_Input = JsonReq;
                objVendor_API_Requests.Res_Khalti_Output = VendorOutput;
                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");
                if (objRes.success == true)
                {
                    msg = objRes.message;
                }
                else { 
                    msg = objRes.errors[0].ToString();
                }
                
            }
            return msg;
        }


        //public static string RequestServiceGroup_Event_Ticket(ref AddVendor_API_Requests objVendor_API_Requests, string merchantCode, string customerName, string customerMobile, string customerEmail, int eventId, int ticketCategoryId, string ticketCategoryName, string eventDate, decimal ticketRate, int noOfTicket, decimal totalPrice, int paymentMethodId, string DeviceId, ref GetVendor_API_Events_Ticket objRes)
        //{
        //    string msg = "";
        //    if (string.IsNullOrEmpty(merchantCode))
        //    {
        //        msg = "Please enter merchant Code.";
        //    }
        //    else if (string.IsNullOrEmpty(customerName))
        //    {
        //        msg = "Please enter customerName.";
        //    }
        //    else if (string.IsNullOrEmpty(customerMobile))
        //    {
        //        msg = "Please enter customer Mobile.";
        //    }
        //    else if (string.IsNullOrEmpty(customerEmail))
        //    {
        //        msg = "Please enter customer Email.";
        //    }
        //    else if (eventId == 0)
        //    {
        //        msg = "Please enter event Id.";
        //    }
        //    else if (ticketCategoryId == 0)
        //    {
        //        msg = "Please select ticket Category.";
        //    }
        //    else if (string.IsNullOrEmpty(ticketCategoryName))
        //    {
        //        msg = "Please enter ticketCategoryName.";
        //    }
        //    else if (string.IsNullOrEmpty(eventDate))
        //    {
        //        msg = "Please select event Date.";
        //    }
        //    else if (string.IsNullOrEmpty(DeviceId))
        //    {
        //        msg = "Please enter DeviceId.";
        //    }
        //    else if (noOfTicket < 1)
        //    {
        //        msg = "Please enter no Of Ticket.";
        //    }
        //    else if (paymentMethodId < 1)
        //    {
        //        msg = "Please select payment Method.";
        //    }
        //    else if (ticketRate == 0)
        //    {
        //        msg = "Please enter Ticket Rate.";
        //    }
        //    else if (totalPrice == 0)
        //    {
        //        msg = "Please enter total Price.";
        //    }
        //    if (string.IsNullOrEmpty(msg))
        //    {
        //        string VendorURL = string.Empty;
        //        string VendorOutput = string.Empty;

        //        string EventsAPIURL = "/clientapi/book-event-tickets/";
        //        string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_Ticket(merchantCode, customerName, customerMobile, customerEmail, eventId, ticketCategoryId, eventDate, ticketRate, noOfTicket, totalPrice, paymentMethodId);
        //        objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events_Ticket(EventsAPIURL, JsonReq, ref VendorURL, ref VendorOutput);
        //        objVendor_API_Requests.Req_Token = VendorApi_CommonHelper.EVENTS_API_KEY;
        //        objVendor_API_Requests.Req_Khalti_URL = VendorURL;
        //        objVendor_API_Requests.Req_Khalti_Input = JsonReq;
        //        objVendor_API_Requests.Res_Khalti_Output = VendorOutput;
        //        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

        //        msg = objRes.message;
        //    }
        //    return msg;
        //}

        public static string Request_Coupon_Verification(String customerEmail, int eventID, decimal amount, string couponCode, ref ValidateCouponResponse objRes, ref AddVendor_API_Requests objVendor_API_Requests, CouponTypes couponType, string merchantCode = "")
        //(ref AddVendor_API_Requests objVendor_API_Requests, string merchantCode, string customerName, string customerMobile, string customerEmail, int eventId, int ticketCategoryId, string ticketCategoryName, string eventDate, decimal ticketRate, int noOfTicket, decimal totalPrice, int paymentMethodId, string DeviceId, ref GetVendor_API_Events_Ticket objRes,
        //string CouponCode = ""
        //)
        {

            /*
             "merchantCode": "MER000001",
    "customerEmail": "rahul.rajbanshi@email.com",
    "eventId": 37,
    "amount": 2000.00,
    "couponCode": "4145LBWE",
             */
            string msg = "";
            //if (string.IsNullOrEmpty(merchantCode))
            //{
            //    msg = "Please enter merchant Code.";
            //}
            //else
            if (string.IsNullOrEmpty(customerEmail))
            {
                msg = "Please enter customer Email.";
            }
            else if (string.IsNullOrEmpty(eventID.ToString()))
            {
                msg = "Please enter event ID.";
            }
            else if (string.IsNullOrEmpty(amount.ToString()))
            {
                msg = "Please enter correct amount.";
            }
            else if (string.IsNullOrEmpty(couponCode))
            {
                msg = "Please enter coupon code.";
            }

            if (string.IsNullOrEmpty(msg))
            {
                // string VendorURL = string.Empty;
                string VendorOutput = string.Empty;

                string EventsAPIURL = "/clientapi/validate-promocode";
                if (couponType == CouponTypes.VotingPromoCode)
                {
                    EventsAPIURL = "/clientapi/validate-promocode";
                }

                string JsonReq = VendorApi_CommonHelper.Generate_JSONReq_Coupon_Validation(eventID, couponCode, amount, customerEmail, couponType, merchantCode);
                //GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_Ticket(merchantCode, customerName, customerMobile, customerEmail, eventId, ticketCategoryId, eventDate, ticketRate, noOfTicket, totalPrice, paymentMethodId, CouponCode);
                objRes = VendorApi_CommonHelper.Request_Event_Coupon_Validation(JsonReq, ref EventsAPIURL, ref VendorOutput, couponType);
                //RequestSERVICEGROUP_SERVICE_Events_Ticket(EventsAPIURL, JsonReq, ref VendorURL, ref VendorOutput);
                objVendor_API_Requests.Req_Token = VendorApi_CommonHelper.EVENTS_API_KEY;
                objVendor_API_Requests.Req_Khalti_URL = EventsAPIURL;
                objVendor_API_Requests.Req_Khalti_Input = JsonReq;
                objVendor_API_Requests.Res_Khalti_Output = VendorOutput;
                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                msg = objRes.Message;
            }
            return msg;
        }
        public static string RequestServiceGroup_Events_CommitMerchantTransactions(string orderId, string amount, string merchantId, string userName, string password, string DeviceId, ref GetVendor_API_Events_CommitMerchant objRes)
        {
            string msg = "";
            if (string.IsNullOrEmpty(orderId))
            {
                msg = "Please enter order id.";
            }

            else if (string.IsNullOrEmpty(amount))
            {
                msg = "Please enter amount.";
            }
            else if (string.IsNullOrEmpty(merchantId))
            {
                msg = "Please enter merchantId.";
            }
            else if (string.IsNullOrEmpty(userName))
            {
                msg = "Please enter user name.";
            }
            else if (string.IsNullOrEmpty(password))
            {
                msg = "Please enter password.";
            }
            else if (string.IsNullOrEmpty(DeviceId))
            {
                msg = "Please enter DeviceId.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                string EventsAPIURL = "api/use-mypay-payments/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_CommitMerchantTransactions(orderId, amount, merchantId, userName, password);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events_CommitMerchantTransaction(EventsAPIURL, JsonReq);
                msg = objRes.message;
            }
            return msg;
        }
        public static string RequestServiceGroup_Events_Commit(AddMerchantOrders resMerchantOrders, string EventId, string paymentMethodName, string ticketCategoryName, string merchantCode, string orderId, string txnId, int paymentMethodId, string Amount, string WalletType, AddUserLoginWithPin resGetRecord, string authenticationToken, ref bool IsCouponUnlocked, ref string TransactionID, AddCouponsScratched resCoupon,
            string BankTransactionId, string CustomerId, string Value, string Reference, string Version, string DeviceCode, string PlatForm, string MemberId, string UserInput, ref GetVendor_API_Events_Commit objRes, ref AddVendor_API_Requests objVendor_API_Requests,
            decimal netAmount = 0, decimal TransactionAmount = 0, string CouponCode = "", decimal couponDiscount = 0
            )
        {
            string msg = "";
            if (string.IsNullOrEmpty(orderId))
            {
                msg = "Please select order.";
            }

            else if (string.IsNullOrEmpty(EventId))
            {
                msg = "Please enter EventId  ";
            }
            else if (string.IsNullOrEmpty(paymentMethodName))
            {
                msg = "Please enter paymentMethodName.";
            }
            else if (string.IsNullOrEmpty(ticketCategoryName))
            {
                msg = "Please enter ticketCategoryName";
            }
            else if (string.IsNullOrEmpty(merchantCode))
            {
                msg = "Please enter merchant Code.";
            }
            else if (string.IsNullOrEmpty(txnId))
            {
                msg = "Please enter txn Id.";
            }
            else if (paymentMethodId == 0)
            {
                msg = "Please select payment Method.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                string EventsAPIURL = "/clientapi/push-booking-payment-txn/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_Commit(merchantCode, orderId, txnId, paymentMethodId);

                int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                {
                    if (resGetRecord == null || resGetRecord.Id == 0)
                    {
                        msg = "Please enter Valid MemberId.";
                    }
                    else if (resGetRecord.IsKYCApproved != (int)AddUser.kyc.Verified)
                    {
                        msg = Common.GetKycMessage(resGetRecord, Convert.ToDecimal(Amount));
                    }
                    if (string.IsNullOrEmpty(msg))
                    {
                        WalletBalance = Convert.ToDecimal(resGetRecord.TotalAmount);
                        AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceChargeMerchant(resMerchantOrders.MerchantId.ToString(), Amount, VendorApiType.ToString());
                        if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (Convert.ToDecimal(resGetRecord.TotalRewardPoints) < objOut.MPCoinsDebit))
                        {
                            msg = Common.InsufficientBalance_MPCoins;
                        }
                        else if (WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (WalletBalance < (Convert.ToDecimal(objOut.NetAmount) - objOut.MPCoinsDebit)))
                        {
                            msg = Common.InsufficientBalance;
                        }
                        else if (WalletType == "0" || WalletType == Convert.ToString((int)WalletTransactions.WalletTypes.Wallet) && WalletBalance < Convert.ToDecimal(objOut.NetAmount))
                        {
                            msg = Common.InsufficientBalance;
                        }
                        else
                        {
                            string TransactionUniqueId = string.Empty;

                            string MemberName = resGetRecord.FirstName + " " + resGetRecord.MiddleName + " " + resGetRecord.LastName;
                            decimal WalletBalance = Convert.ToDecimal(resGetRecord.TotalAmount);
                            if (resGetRecord == null || resGetRecord.Id == 0)
                            {
                                msg = "MemberId not found";
                                JsonReq = String.Empty;
                            }
                            else if (resGetRecord.IsKYCApproved != (int)AddUser.kyc.Verified)
                            {
                                msg = Common.GetKycMessage(resGetRecord, Convert.ToDecimal(Amount));
                                if (!string.IsNullOrEmpty(msg))
                                {
                                    JsonReq = String.Empty;
                                }
                            }
                            if (string.IsNullOrEmpty(msg))
                            {
                                if (resGetRecord.IsActive == false)
                                {
                                    msg = "Your account is not active.";
                                    JsonReq = String.Empty;
                                }
                            }
                            if (!string.IsNullOrEmpty(JsonReq))
                            {

                                string VendorApiTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(VendorApiType)).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                                //objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(Reference, resGetRecord.MemberId, MemberName, JsonReq, authenticationToken, UserInput, DeviceCode, PlatForm, VendorApiType);
                                //TransactionUniqueId = VendorApi_CommonHelper.UpdateWalletBalance(resCoupon, ref TransactionID, BankTransactionId, WalletType, CustomerId, Amount, out msg, VendorApiType, resGetRecord, objVendor_API_Requests, "", out WalletBalance, "", "", (int)VendorApi_CommonHelper.VendoTypes.MyPay, resMerchantOrders);
                                TransactionUniqueId = VendorApi_CommonHelper.UpdateWalletBalanceWithGenericCoupon(resCoupon, ref TransactionID, BankTransactionId, WalletType, CustomerId, Amount, out msg, VendorApiType, resGetRecord, objVendor_API_Requests, "", out WalletBalance, "", "", (int)VendorApi_CommonHelper.VendorTypes.MyPay, resMerchantOrders, netAmount, TransactionAmount, CouponCode, couponDiscount);
                                string VendorOutputResponse = string.Empty;
                                string VendorURL = string.Empty;
                                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events_Commit(EventsAPIURL, JsonReq, ref VendorOutputResponse, ref VendorURL);
                                msg = objRes.message;
                                objVendor_API_Requests = VendorApi_CommonHelper.UpdateVendorResponse(EventsAPIURL, JsonReq, DeviceCode, PlatForm, VendorApiType, VendorApiTypeName, objVendor_API_Requests.Id, "", VendorOutputResponse, VendorURL);
                                if (msg.ToLower() == "success" && objVendor_API_Requests.Id != 0 && objVendor_API_Requests.Res_Khalti_State.ToLower() == "success")
                                {
                                    msg = Common.UpdateCompleteTransaction(ref IsCouponUnlocked, ref TransactionID, resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, resMerchantOrders.OrganizationName);
                                    if (msg.ToLower() == "success")
                                    {
                                        string Title = "Transaction successfull";
                                        string Message = $"Bill payment of amount Rs.{Amount} for {VendorApiTypeName} has been completed successfully.";//TransactionId " + resKhalti.TransactionUniqueId + " success for " + VendorApiTypeName;
                                        Common.SendNotification(authenticationToken, VendorApiType, resGetRecord.MemberId, Title, Message);
                                    }
                                }
                                else
                                {
                                    msg = Common.RefundUpdateTransaction(resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, BankTransactionId, VendorApiType, WalletType, PlatForm, DeviceCode);
                                }
                            }
                            // **** Populate Values to be passed to Controller Action **** //

                            // objRes.UniqueTransactionId = VendorApi_CommonHelper.UniqueTransactionId.ToString();

                            Id = VendorApi_CommonHelper.Id;
                        }

                    }
                }


            }
            return msg;
        }
        public static string RequestServiceGroup_Events_Ticket_Download(string merchantCode, string orderId, ref GetVendor_API_Events_Ticket_Download objRes)
        {
            string msg = "";
            if (string.IsNullOrEmpty(orderId))
            {
                msg = "Please enter  order.";
            }

            else if (string.IsNullOrEmpty(merchantCode))
            {
                msg = "Please enter merchant Code.";
            }

            if (string.IsNullOrEmpty(msg))
            {
                string EventsAPIURL = "/clientapi/download-event-tickets/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_Ticket_Download(merchantCode, orderId);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events_Ticket_Download(EventsAPIURL, JsonReq);
                msg = objRes.message;
            }
            return msg;
        }




    }
}