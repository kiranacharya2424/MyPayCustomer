using MyPay.API.Models.Response.Voting.Partner;
using MyPay.Models.Add;
using MyPay.Models.Get;
using MyPay.Models.Get.Events;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Web.Http.Results;
using MyPay.API.Models.Request.Voting.Consumer;
using Dapper;
using System.Configuration;
using Microsoft.Ajax.Utilities;
using MyPay.API.Models.Events;
using MyPay.Models.Common;
using MyPay.Models.Miscellaneous;
using MyPay.API.Models.Request.Voting.Partner;

namespace MyPay.API.Repository
{
    public class VotingRepo
    {

        internal static string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        //votingAPI = votingAPI.loadSettings();
        //static VotingAPISetting votingAPI = new VotingAPISetting().loadSettings();
        public static string processRequest<T, U, V>(string baseURL, string endPoint, AddVendor_API_Requests objVendor_API_Requests, ref T objectToSend, ref U resObject, V vendorReqObject)
        {
            
            string msg = "";

            if (string.IsNullOrEmpty(msg))
            {
                string JSONResp = "";
                string JSONReq = "";
                VotingPartnerResp objRes = sendRequestToPartner(objectToSend, ref resObject, baseURL, endPoint, ref JSONReq, ref JSONResp, vendorReqObject);
                objVendor_API_Requests.Req_Khalti_URL = baseURL + endPoint;
                objVendor_API_Requests.Req_Khalti_Input = JSONReq;
                objVendor_API_Requests.Req_Token = $"CLIENT_CODE: {VendorApi_CommonHelper.EVENTS_API_CLIENT_CODE} USER_NAME:{VendorApi_CommonHelper.EVENTS_USER_NAME}  API_KEY:{VendorApi_CommonHelper.EVENTS_API_KEY}  ";
                objVendor_API_Requests.Res_Khalti_Output = JSONResp;

                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");
                msg = objRes.Message;
            }
            return msg;
        }

        public static VotingPartnerResp sendRequestToPartner<T, U, V>(T objectToSend, ref U objectToReceive, string baseURL, string endPoint, ref string JSONReq, ref string JsonResponse, V vendorObject)
        {
            VotingPartnerResp objRes = new VotingPartnerResp();
            try
            {
                JsonResponse = postDataToPartner(objectToSend, ref objectToReceive, baseURL, endPoint, vendorObject);
                objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<VotingPartnerResp>(JsonConvert.SerializeObject(objectToReceive));
                objRes.Message = (objRes.success ? "success" : ((String.IsNullOrEmpty(objRes.Message) ? objRes.Message : objRes.Message)));
                objRes.status = true;
                return objRes;
            }

            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        var json = reader.ReadToEnd();
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<VotingPartnerResp>(json);
                        return objRes;
                    }
                }
            }
            catch (Exception ex)
            {
                objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                objRes.status = false;
                return objRes;
            }
            return objRes;
        }

        public static string postDataToPartner<T, U, V>(T objectToSend, ref U objectToReceive, string baseURL, string endPoint, V vendorObject)
        {
            string result = "success";


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + createBase64(VotingAPISetting_API.user + ":" + VotingAPISetting_API.pass));


            vendorObject = JsonConvert.DeserializeObject<V>(JsonConvert.SerializeObject(objectToSend));

            var postTask = client.PostAsJsonAsync(endPoint, vendorObject);

            postTask.Wait();

            var postResult = postTask.Result;

            if (postResult.IsSuccessStatusCode)
            {
                var readTask = postResult.Content.ReadAsAsync<U>();
                readTask.Wait();

                var receivedResp = readTask.Result;
                objectToReceive = receivedResp;
            }
            else
            {
                var readTask = postResult.Content.ReadAsAsync<U>();
                readTask.Wait();

                var receivedResp = readTask.Result;
                objectToReceive = receivedResp;
            }
            return result;
        }

        public static string createBase64(string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        internal static string HMACSHA512(string text, string secretKey)
        {
            var hash = new StringBuilder();
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString().ToUpper();
        }



        public string completeVoteBookingOrder(BookVotesReq_C orderRequest, BookVotesResp_P response)
        {
            string result = string.Empty;

            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "update booking Voting_orders set paymentMethodId = @paymentMethodId, paymentMerchantId = @paymentMerchantId where orderID = @orderID ";

                //var parameters = new { deviceID = deviceID, JWTToken = JWTToken };

                //var userResult = connection.QueryFirst<MyUser>(sql, parameters);
            }
            return result;
        }

        public static string createMerchantOrder(string orderId, string amount, string merchantId, string userName, string password, string DeviceId, ref GetVendor_API_Events_CommitMerchant objRes)
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
            //else if (string.IsNullOrEmpty(DeviceId))
            //{
            //    msg = "Please enter DeviceId.";
            //}
            if (string.IsNullOrEmpty(msg))
            {
                string EventsAPIURL = "api/use-mypay-payments/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_CommitMerchantTransactions(orderId, amount, merchantId, userName, password);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events_CommitMerchantTransaction(EventsAPIURL, JsonReq);
                msg = objRes.message;
            }
            return msg;
        }


        //        public static string completeVotingPayment(AddMerchantOrders resMerchantOrders, string merchantId, string orderId, string txnId, int paymentMethodId, string Amount, string WalletType, AddUserLoginWithPin resGetRecord, string authenticationToken, ref bool IsCouponUnlocked, ref string TransactionID, AddCouponsScratched resCoupon,
        //        string BankTransactionId, string CustomerId, tring Reference, string DeviceCode, string PlatForm, string MemberId, ref GetVendor_API_Events_Commit objRes, ref AddVendor_API_Requests objVendor_API_Requests)



        //public static string completeVotingPayment(AddMerchantOrders resMerchantOrders, string EventId, string paymentMethodName, string ticketCategoryName, string merchantId, string orderId, string txnId, string Amount, string WalletType, AddUserLoginWithPin resGetRecord, string authenticationToken, ref bool IsCouponUnlocked, ref string TransactionID, AddCouponsScratched resCoupon,
        //    string BankTransactionId, string CustomerId, string Value, string Reference, string Version, string DeviceCode, string PlatForm, string MemberId, string UserInput, ref GetVendor_API_Events_Commit objRes, ref AddVendor_API_Requests objVendor_API_Requests)

        public static string completeVotingPayment(AddMerchantOrders resMerchantOrders, string merchantId, string orderId, string txnId, int paymentMethodId, string Amount, string WalletType, AddUserLoginWithPin resGetRecord, string authenticationToken, ref bool IsCouponUnlocked, ref string TransactionID, AddCouponsScratched resCoupon,
          string BankTransactionId, string CustomerId, string Reference, string DeviceCode, string PlatForm, string MemberId, ref GetVendor_API_Events_Commit objRes, ref AddVendor_API_Requests objVendor_API_Requests, CompleteVoteBookingReq_P objectToSendToPartner)

        {
            string msg = "";
            if (string.IsNullOrEmpty(orderId))
            {
                msg = "Please select order.";
            }
            else if (string.IsNullOrEmpty(merchantId))
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
                string EventsAPIURL = "/clientapi/push-vote-payment-txn";
                string JsonReq = JsonConvert.SerializeObject(objectToSendToPartner); //VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_Events_Commit(merchantId, orderId, txnId, paymentMethodId);

                int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.International_Voting;
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
                        decimal WalletBalance = Convert.ToDecimal(resGetRecord.TotalAmount);
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
                            WalletBalance = Convert.ToDecimal(resGetRecord.TotalAmount);
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
                                TransactionUniqueId = VendorApi_CommonHelper.UpdateWalletBalance(resCoupon, ref TransactionID, BankTransactionId, WalletType, CustomerId, Amount, out msg, VendorApiType, resGetRecord, objVendor_API_Requests, "", out WalletBalance, "", "", (int)VendorApi_CommonHelper.VendorTypes.MyPay, resMerchantOrders);
                                string VendorOutputResponse = string.Empty;
                                string VendorURL = string.Empty;
                                objectToSendToPartner.transactionId = resMerchantOrders.TransactionId;
                                objectToSendToPartner.createSignature(VotingAPISetting_API.key);

                               


                                //Voting partner commit disabled
                                string newJSON = JsonConvert.SerializeObject(objectToSendToPartner);
                                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_SERVICE_Events_Commit(EventsAPIURL, newJSON, ref VendorOutputResponse, ref VendorURL, true);
                                msg = objRes.message;

                                string votingTxnDescription = $"{objRes.data.NoOfVotes} votes(s) of amount Rs. {objRes.data.PayableAmt} to {objRes.data.ContestantName} ({objRes.data.ContestName} {objRes.data.SubContestName})";


                                objVendor_API_Requests = VendorApi_CommonHelper.UpdateVendorResponse(EventsAPIURL, newJSON, DeviceCode, PlatForm, VendorApiType, VendorApiTypeName, objVendor_API_Requests.Id, "", VendorOutputResponse, VendorURL);
                                if (msg.ToLower() == "success" && objVendor_API_Requests.Id != 0 && objVendor_API_Requests.Res_Khalti_State.ToLower() == "success")
                                {
                                    msg = Common.UpdateCompleteTransaction(ref IsCouponUnlocked, ref TransactionID, resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, resMerchantOrders.OrganizationName, votingTxnDescription);
                                    if (msg.ToLower() == "success")
                                    {
                                        string Title = "Transaction successfull";
                                        string Message = $"Bill payment of amount Rs.{Amount} for {VendorApiTypeName} has been completed successfully.";//TransactionId " + resKhalti.TransactionUniqueId + " success for " + VendorApiTypeName;
                                        Common.SendNotification(authenticationToken, VendorApiType, resGetRecord.MemberId, Title, Message);
                                    }
                                }
                                else
                                {
                                    Common.RefundUpdateTransaction(resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, BankTransactionId, VendorApiType, WalletType, PlatForm, DeviceCode);
                                }
                                //Voting partner commit disabled
                            }
                            // **** Populate Values to be passed to Controller Action **** //

                            // objRes.UniqueTransactionId = VendorApi_CommonHelper.UniqueTransactionId.ToString();

                            long Id = VendorApi_CommonHelper.Id;

                            //////////////////////
                            //update transaction ID in response object with Wallet transaction ID
                            //////////////////////
                            ///


                            if (objRes.data != null)
                            {
                                objRes.data.transactionId = TransactionID;

                            }
                            

                        }

                    }
                }


            }
            return msg;
        }

        public static string saveVoteBookingOrder(BookVotesReq_C orderRequest, BookVotesResp_P response)
        {
            string result = string.Empty;

            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "insert into Voting_orders(MemberId," +
                    "contestId" + "," +
                    "subContestId" + "," +
                    "votingCandidateUniqueId" + "," +
                    "customerName" + "," +
                    "customerEmail" + "," +
                    "pricePerVote" + "," +
                    "NoOfVotes" + "," +
                    "amount" + "," +
                    "isPaidVote" + "," +
                    "isPackageUsed" + "," +
                    "votingPackageId" + "," +
                    "serviceCharge" + "," +
                    "couponCode" + "," +
                      "isDeleted" + "," +
                                           "platform" + "," +

                      "orderId" + "," +
                    "MerchantId" +
                    ") values(";

                sql = sql + orderRequest.MemberId + ",";
                //sql = sql + orderRequest.memberName ;
                //sql = sql + memberContactNumber;
                sql = sql + orderRequest.contestId + ",";
                sql = sql + orderRequest.subContestId + ",";
                //sql = sql + deviceCode;
                sql = sql + orderRequest.contestantId + ",'";
                //sql = sql + votingCadidateName;
                sql = sql + orderRequest.customerName + "','";
                sql = sql + orderRequest.customerEmail + "',";
                sql = sql + orderRequest.pricePerVote + ",";
                sql = sql + orderRequest.totalVote + ",";
                sql = sql + response.data.payableAmount + ",";
                sql = sql + (orderRequest.isPaidVote == "true" ? "1" : "0") + ",";
                sql = sql + (orderRequest.isPackageUsed == "true" ? "1" : "0") + ",";
                sql = sql + orderRequest.packageId + ",";
                sql = sql + orderRequest.serviceCharge + ",'";
                sql = sql + orderRequest.couponCode + "',";
                //sql = sql + createdBy + "','";
                // sql = sql + createdByName + "','";
                //sql = sql + createdDate + "','";
                //sql = sql + UpdatedBy + "','";
                //sql = sql + UpdatedByName + "','";
                //sql = sql + UpdatedDate + "','";
                sql = sql + "0,'";
                sql = sql + "mobile" + "',";
                sql = sql + response.data.orderId + ",'";
                sql = sql + response.data.paymentMerchantId + "'";
                //sql = sql + TransactionUniqueID;
                //sql = sql + paymentMethodId;
                sql = sql + ")";

                var updatedRow = connection.Execute(sql, null);

                //var parameters = new { deviceID = deviceID, JWTToken = JWTToken };

                //var userResult = connection.QueryFirst<MyUser>(sql, parameters);


            }
            return result;
        }

        public static string saveVoteBookingOrderV2(BookVotesAndConfirmReq_C orderRequest, BookVotesResp_P response)
        {
            string result = string.Empty;

            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "insert into Voting_orders(MemberId," +
                    "contestId" + "," +
                    "subContestId" + "," +
                    "votingCandidateUniqueId" + "," +
                    "customerName" + "," +
                    "customerEmail" + "," +
                    "pricePerVote" + "," +
                    "NoOfVotes" + "," +
                    "amount" + "," +
                    "isPaidVote" + "," +
                    "isPackageUsed" + "," +
                    "votingPackageId" + "," +
                    "serviceCharge" + "," +
                    "couponCode" + "," +
                      "isDeleted" + "," +
                                           "platform" + "," +

                      "orderId" + "," +
                    "MerchantId" +
                    ") values(";

                sql = sql + orderRequest.MemberId + ",";
                //sql = sql + orderRequest.memberName ;
                //sql = sql + memberContactNumber;
                sql = sql + orderRequest.contestId + ",";
                sql = sql + orderRequest.subContestId + ",";
                //sql = sql + deviceCode;
                sql = sql + orderRequest.contestantId + ",'";
                //sql = sql + votingCadidateName;
                sql = sql + orderRequest.customerName + "','";
                sql = sql + orderRequest.customerEmail + "',";
                sql = sql + orderRequest.pricePerVote + ",";
                sql = sql + orderRequest.totalVote + ",";
                sql = sql + response.data.payableAmount + ",";
                sql = sql + (orderRequest.isPaidVote == "true" ? "1" : "0") + ",";
                sql = sql + (orderRequest.isPackageUsed == "true" ? "1" : "0") + ",";
                sql = sql + orderRequest.packageId + ",";
                sql = sql + orderRequest.serviceCharge + ",'";
                sql = sql + orderRequest.couponCode + "',";
                //sql = sql + createdBy + "','";
                // sql = sql + createdByName + "','";
                //sql = sql + createdDate + "','";
                //sql = sql + UpdatedBy + "','";
                //sql = sql + UpdatedByName + "','";
                //sql = sql + UpdatedDate + "','";
                sql = sql + "0,'";
                sql = sql + "mobile" + "',";
                sql = sql + response.data.orderId + ",'";
                sql = sql + response.data.paymentMerchantId + "'";
                //sql = sql + TransactionUniqueID;
                //sql = sql + paymentMethodId;
                sql = sql + ")";

                var updatedRow = connection.Execute(sql, null);

                //var parameters = new { deviceID = deviceID, JWTToken = JWTToken };

                //var userResult = connection.QueryFirst<MyUser>(sql, parameters);


            }
            return result;
        }


        public static VotingOrder getVoteBookingOrder(string orderId)
        {
            VotingOrder votingOrder = new VotingOrder();
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = $"select * from Voting_Orders where orderId = '{orderId}'";
                votingOrder = connection.QueryFirstOrDefault<VotingOrder>(sql);
            }
            return votingOrder;
        }

        public static int updateVoteBookingOrder(string orderId, string tranID, string paymentMethodId, bool paymentComplete) { 
            int updatedRecords = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                int isPaymentComplete = paymentComplete ? 1 : 0;
                var sql = $"update Voting_orders set TransactionUniqueId= {tranID}, paymentMethodId = {paymentMethodId}, status =  { isPaymentComplete } " + 
                    $" where orderId= {orderId}";
                updatedRecords = connection.Execute(sql, null);
            }

            return updatedRecords;
        }

        public class VotingOrder
        {
            public long Id { get; set; }
            public long memberID { get; set; }
            public string memberName { get; set; }
            public string memberContactNumber { get; set; }
            public long contestID { get; set; }
            public int subcontestID { get; set; }
            public string deviceCode { get; set; }
            public long votingCandidateUniqueId { get; set; }
            public string votingCadidateName { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmail { get; set; }
            public decimal PricePerVote { get; set; }
            public int NoOfVotes { get; set; }
            public decimal amount { get; set; }
            public bool isPaidVote { get; set; }
            public bool isPackageUsed { get; set; }
            public long votingPackageID { get; set; }
            public decimal serviceCharge { get; set; }
            public string couponCode { get; set; }
            public long createdBy { get; set; }
            public string createdByName { get; set; }
            public DateTime createdDate { get; set; }
            public long UpdatedBy { get; set; }
            public string UpdatedByName { get; set; }
            public DateTime UpdatedDate { get; set; }
            public bool isDeleted { get; set; }
            public string platform { get; set; }
            public long orderId { get; set; }
            public string merchantID { get; set; }
            public string TransactionUniqueID { get; set; }
            public string paymentMethodId { get; set; }
        }
    }
}