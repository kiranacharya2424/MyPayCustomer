using iText.Html2pdf.Attach.Impl.Layout.Form.Renderer;
using MyPay.API.Models.Request.Voting.Consumer;
using MyPay.API.Models.Request.Voting.Partner;
using MyPay.API.Models.Response.Voting.Partner;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get.Events;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPay.API.Models.NPS;
using System.Web;
using MyPay.API.Models.NPS.Response.Partner;
using MyPay.API.Models.Request.NPS.Partner;
using MyPay.API.Models.NPS.Errors;
using log4net;

namespace MyPay.API.Repository
{
    public class NCHLRepo
    {

        public static string accessToken =  "27f3df2f-eaa6-4ee2-93e2-11e76feb564a";
        public static string getAccessToken() {
            return accessToken;
        }


        internal static string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        //votingAPI = votingAPI.loadSettings();
        //static VotingAPISetting votingAPI = new VotingAPISetting().loadSettings();
        public static string processRequest<T, U>(string baseURL, string endPoint, AddVendor_API_Requests objVendor_API_Requests, ref T objectToSend, ref U resObject, string instructionId, LodgeBill lodgeBill = null, bool isLodgeBill = false)
        {
            string msg = "";

            if (string.IsNullOrEmpty(msg))
            {
                string JSONResp = "";
                string JSONReq = "";
                NCHLPartnerResp objRes = sendRequestToPartner(objectToSend, ref resObject, baseURL, endPoint, ref JSONReq, ref JSONResp, instructionId, lodgeBill, isLodgeBill);
                objVendor_API_Requests.Req_Khalti_URL = baseURL + endPoint;
                objVendor_API_Requests.Req_Khalti_Input = JSONReq;
                // objVendor_API_Requests.Req_Token = $"CLIENT_CODE: {VendorApi_CommonHelper.EVENTS_API_CLIENT_CODE} USER_NAME:{VendorApi_CommonHelper.EVENTS_USER_NAME}  API_KEY:{VendorApi_CommonHelper.EVENTS_API_KEY}  ";
                objVendor_API_Requests.Res_Khalti_Output = JSONResp;
                if ( objRes.cipsBatchDetail != null)
                {
                    objVendor_API_Requests.Req_ReferenceNo = objRes.cipsBatchDetail.batchId;
                    objVendor_API_Requests.Res_Khalti_Id = objRes.cipsTransactionDetail.id.ToString();

                }

                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");
                msg = objRes.Message;
            }
            return msg;
        }

        public static NCHLPartnerResp sendRequestToPartner<T, U>(T objectToSend, ref U objectToReceive, string baseURL, string endPoint, ref string JSONReq, ref string JsonResponse, string instructionId, LodgeBill lodgeBill = null, bool isLodgeBill = false)
        {
            NCHLPartnerResp objRes = new NCHLPartnerResp();
            try
            {
                JsonResponse = postDataToPartnerV2(objectToSend, ref objectToReceive, baseURL, endPoint, instructionId, lodgeBill, isLodgeBill);
                if (JsonResponse != "success")
                {
                    objRes.status = false;
                    objRes.Message = JsonResponse;
                    return objRes;
                }

                objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<NCHLPartnerResp>(JsonConvert.SerializeObject(objectToReceive));
                //objRes.Message = (objRes.responseResult.responseCode == "000" ? "success" : ((String.IsNullOrEmpty(objRes.Message) ? objRes.Message : "failed")));
                objRes.Message = (objRes.responseResult.responseCode == "000" ? "success" : objRes.responseResult.fieldErrors[0].message); //"failed");

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
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<NCHLPartnerResp>(json);
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

        public static string postDataToPartnerV2<T, U>(T objectToSend, ref U objectToReceive, string baseURL, string endPoint, string instructionId, LodgeBill lodgeBill = null, bool isLodgeBill = false)

        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            int sendAttempt = 0;
            string result = "failed";
            generateNCHLAccessToken();
            try
            {
                while (sendAttempt < 2)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, baseURL + endPoint);//"http://demo.connectips.com:6065/api/billpayment/lodgebillpay.do");
                    //var content = new StringContent("{\r\n    \"cipsBatchDetail\": {\r\n    \"batchId\": \"PP-RAJASWA-123459\",\r\n    \"batchAmount\": 0,\r\n    \"batchCount\": 1,\r\n    \"batchCrncy\": \"NPR\",\r\n    \"categoryPurpose\": \"ECPG\",\r\n    \"debtorAgent\": \"2501\",\r\n    \"debtorBranch\": \"1\",\r\n    \"debtorName\": \"MONA SHRESTHA\",\r\n    \"debtorAccount\": \"0010000055900011\"\r\n},\r\n\"cipsTransactionDetail\": {\r\n    \"instructionId\": \"PP-RAJASWA-123459\",\r\n    \"endToEndId\": \"RAJASWA-1\",\r\n    \"amount\": 0,\r\n    \"appId\": \"MER-7-APP-4\",\r\n    \"refId\": \"2079-2483747\"\r\n},\r\n\"token\": \"cXNS0rZV2QLqL0dZO1w60te9UP/DRrgbOX3Da8eztZR/8hgrtc/ox5GfQCMyARrxzREcQx580qTEE/KJzfQUlo/+8wRVaYwiOtUO2Vzr4vVO9v/GkCV3rXbhGw6JE4bR3FPdJm6VmF+KWgYdD3R5ddi0lXzAkuMEr+BHefrwGtA=\"\r\n}", null, "application/json");

                    var content = JsonConvert.SerializeObject(objectToSend, Formatting.None);
                    var stringContent = new StringContent(content, null, "application/json");

                    request.Content = stringContent;

                    // client.DefaultRequestHeaders.Add("Accept", "application/json");
                   
                    
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    //client.DefaultRequestHeaders.Add("UserAgent", "PostmanRuntime/7.32.3");
                   client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                    client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");

                    var response = client.SendAsync(request);

                    response.Wait();

                    var postResult = response.Result;


                    //response.EnsureSuccessStatusCode();
                    // Console.WriteLine(await response.Content.ReadAsStringAsync());
                    //var resp = response.con
                    sendAttempt++;

                    NCHLGenericError error = JsonConvert.DeserializeObject<NCHLGenericError>(postResult.Content.ReadAsStringAsync().Result);

                    if (isLodgeBill)
                    {
                        lodgeBill.update(lodgeBill.instructionId, "0", "", postResult.Content.ReadAsStringAsync().Result, "", "");
                    }
                    else
                    {
                        lodgeBill.update(lodgeBill.instructionId, "0", "", "", "", postResult.Content.ReadAsStringAsync().Result);
                    }
                    

                    if (!string.IsNullOrEmpty(error.responseCode)) {
                        log.Info("Error received from NCHL: " + error.responseCode);
                        return error.responseCode;
                    }

                    //LodgeBill tempLodgeBill = new LodgeBill();
                    //tempLodgeBill.update(instructionId, "", "", )

                    if (!string.IsNullOrEmpty(error.responseMessage))
                    {
                        log.Info("Error received from NCHL: " + error.responseMessage);
                    }

                    if (!string.IsNullOrEmpty(error.error_description))
                    {
                        log.Info("Error received from NCHL: " + error.error_description);

                        return error.error_description;
                    }

                    if (postResult.Content.ReadAsStringAsync().Result.ToLower().Contains("invalid access token"))
                    {
                        log.Info("Invalid NCHL access token");
                        generateNCHLAccessToken();
                    }
                    else
                    {
                        objectToReceive = JsonConvert.DeserializeObject<U>(postResult.Content.ReadAsStringAsync().Result);
                        
                        result = "success";
                        break;
                    }
                }
            }
            catch (HttpException ex)
            {

            }
            catch (Exception ex2)
            {

                // throw;
            }


            return result;
        }

        //public static string postDataToPartner<T, U, V>(T objectToSend, ref U objectToReceive, string baseURL, string endPoint, V vendorObject)
        //{
        //    int sendAttempt = 1;
        //    string result = "failed";


        //    while (sendAttempt < 2)
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(baseURL);
        //        client.DefaultRequestHeaders.Add("Accept", "application/json");
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);


        //    try
        //    {
        //        var postTask = client.PostAsJsonAsync(endPoint, objectToSend);

        //        postTask.Wait();

        //        var postResult = postTask.Result;



        //    if (postResult.IsSuccessStatusCode)
        //        {
        //            var readTask = postResult.Content.ReadAsAsync<U>();
        //            readTask.Wait();

        //            var receivedResp = readTask.Result;
        //            objectToReceive = receivedResp;
        //            result = "success";
        //        }
        //        else
        //        {
        //            var readTask = postResult.Content.ReadAsAsync<U>();
        //            readTask.Wait();

        //            var receivedResp = readTask.Result;
        //            objectToReceive = receivedResp;


        //            sendAttempt++;
        //            if (postResult.Content.ReadAsStringAsync().Result.ToLower().Contains("invalid access token"))
        //            {
        //                generateNCHLAccessToken();
        //            }
        //        }
        //    }
           
        //    catch (HttpException ex) { 
            
        //    }
        //    catch (Exception ex2)
        //    {

        //        // throw;
        //    }


        //     }
        //    return result;
        //}


        public static string generateNCHLAccessToken()
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


            string result = "success";

            var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, "http://demo.connectips.com:6065/oauth/token");

            var URL = Common.ApplicationEnvironment.IsProduction ? RepNCHL.NCHLWithdrawAPIURl : RepNCHL.NCHLWithdrawAPIURl_LinkBank;
            URL = URL + "oauth/token";
            var key = Common.ApplicationEnvironment.IsProduction ? "Basic " + RepNCHL.WithdrawBasicApiKey : "Basic bXlwYXk6QWJjZEAxMjM=";
            var username = Common.ApplicationEnvironment.IsProduction ? RepNCHL.withdrawusername : RepNCHL.withdrawusername_staging;
            var password = Common.ApplicationEnvironment.IsProduction ? RepNCHL.withdrawpassword : RepNCHL.withdrawpassword_staging;

            log.Info("Username: " + username);
            log.Info("Password: " + password);
            log.Info("URL: " + URL);
            log.Info("Key: " + key);

            var request = new HttpRequestMessage(HttpMethod.Post, URL);

            request.Headers.Add("Authorization", key);
            
            
            var collection = new List<KeyValuePair<string, string>>();

         
                collection.Add(new KeyValuePair<string, string>("username", username));
                collection.Add(new KeyValuePair<string, string>("password", password));
          


            collection.Add(new KeyValuePair<string, string>("grant_type", "password"));


            //collection.Add(new("username", "MYPAY@999"));
            //collection.Add(new("password", "123Abcd@123"));
            //collection.Add(new("grant_type", "password"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;

            var postTask = client.SendAsync(request);  //.PostAsJsonAsync("/oauth/token", request);


            postTask.Wait();

            var postResult = postTask.Result;

            if (postResult.IsSuccessStatusCode)
            {
                var readTask = postResult.Content.ReadAsAsync<NCHLTokenResp>();
                readTask.Wait();

                var response = readTask.Result;
                //SETUP ACCESS TOKEN HERE
                accessToken = response.access_token;
                result = "success";
            }
            else
            {
                var readTask = postResult.Content.ReadAsAsync<NCHLTokenResp>();
                readTask.Wait();
                var response = readTask.Result;

                log.Info("Error occurred while generating NCHL access token: " + response);

                result = "failed";
               // THROW ERROR HERE SINCE TOKEN WAS NOT RECEIVED
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


        public static string saveLodgeBillToDB(NCHLLodgeReq req, NCHLPartnerResp resp) {

            return "";
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
                string JsonReq = JsonConvert.SerializeObject(objectToSendToPartner);

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
                                objectToSendToPartner.createSignature(VotingAPISetting.key);




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


        public static string completePaymentForNCHL(AddMerchantOrders resMerchantOrders, string merchantId, string orderId, string txnId, int paymentMethodId, string Amount, string WalletType, AddUserLoginWithPin resGetRecord, string authenticationToken, ref bool IsCouponUnlocked, ref string TransactionID, AddCouponsScratched resCoupon,
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
                                objectToSendToPartner.createSignature(VotingAPISetting.key);




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


    }
}
