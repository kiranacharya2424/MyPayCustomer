
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
using ServiceStack.Auth;

namespace MyPay.Repository
{
    public class RepGovernmentNCHL
    {
        public static Int64 Id = 0;
        public static string UniqueTransactionId = string.Empty;
        public static string TransactionUniqueId = string.Empty;
        private static int InputNumber_Digits = 10;
        public static AddVendor_API_Requests resKhalti = new AddVendor_API_Requests();
        public static decimal WalletBalance = 0;
        public static string WithdrawBasicApiKey_Local = "bXlwYXk6QWJjZEAxMjM=";


        // *************** LIVE CREDENTIALS ***************** //
        public static string GOVERNMENT_SERVICE_API_URL_LINK = "https://182.93.93.107:7444/";

        // ************ TEST CREDENTIALS  *************** //
        public static string GOVERNMENT_SERVICE_API_URL_LINK_LOCAL = "http://demo.connectips.com:9095/";

        public static string RequestServiceGroup_EPF_DepositType(string batchId, decimal batchAmount, Int32 batchCount, string BatchCrncy, string categoryPurpose, string debtorAgent, string debtorBranch, string DebtorName, string debtorAccount, string debtorIdType, string debtorIdValue, string DebtorAddress, string debtorPhone, string debtorMobile, string debtorEmail,
            string instructionId, string endToEndId, decimal amount, string appId, string refId, string freeText1, string addenda3, ref GetVendor_API_Epf_Deposit_Type objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(BatchCrncy))
            {
                msg = "Please enter BatchCrncy.";
            }
            else if (string.IsNullOrEmpty(DebtorName))
            {
                msg = "Please enter DebtorName.";
            }
            else if (string.IsNullOrEmpty(DebtorAddress))
            {
                msg = "Please enter DebtorAddress.";
            }
            if (string.IsNullOrEmpty(msg))
            {

                //string EPFAPIURL = "tp/epf/deposit/type/";
                string EPFAPIURL = "billers/v2/detail/epf/deposit/type/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_EPF_DepositType(batchId, batchAmount, batchCount, BatchCrncy, categoryPurpose, debtorAgent, debtorBranch, DebtorName, debtorAccount, debtorIdType, debtorIdValue, DebtorAddress, debtorPhone, debtorMobile, debtorEmail, instructionId, endToEndId, amount, appId, refId, freeText1, addenda3);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_EPF_Deposit_Type(JsonReq, EPFAPIURL);
                msg = objRes.Message;
            }
            return msg;
        }
        public static string RequestServiceGroup_EPF_ContributorDetail(string BatchCrncy, string DebtorName, string DebtorAddress, ref GetVendor_API_Epf_Contributor_Detail objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(BatchCrncy))
            {
                msg = "Please enter BatchCrncy.";
            }
            else if (string.IsNullOrEmpty(DebtorName))
            {
                msg = "Please enter DebtorName.";
            }
            else if (string.IsNullOrEmpty(DebtorAddress))
            {
                msg = "Please enter DebtorAddress.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                string KhaltiAPIURL = "tp/epf/contributor/detail/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_EPF_ContributorDetail(BatchCrncy, DebtorName, DebtorAddress);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_EPF_Contributor_Detail(JsonReq, KhaltiAPIURL);
                msg = objRes.Message;
            }
            return msg;
        }
        public static string RequestServiceGroup_EPF_Commit(string BatchCrncy, string DebtorName, string DebtorAddress, ref GetVendor_API_Epf_Commit objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(BatchCrncy))
            {
                msg = "Please enter BatchCrncy.";
            }
            else if (string.IsNullOrEmpty(DebtorName))
            {
                msg = "Please enter DebtorName.";
            }
            else if (string.IsNullOrEmpty(DebtorAddress))
            {
                msg = "Please enter DebtorAddress.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                string KhaltiAPIURL = "billpayment/lodgebillpay.do/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_EPF_Commit(BatchCrncy, DebtorName, DebtorAddress);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_EPF_Commit(JsonReq, KhaltiAPIURL);
                msg = objRes.Message;
            }
            return msg;
        }
        public static string RequestServiceGroup_EPF_ConfirmPayment(string BatchCrncy, string DebtorName, string DebtorAddress, ref GetVendor_API_Epf_Commit objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(BatchCrncy))
            {
                msg = "Please enter BatchCrncy.";
            }
            else if (string.IsNullOrEmpty(DebtorName))
            {
                msg = "Please enter DebtorName.";
            }
            else if (string.IsNullOrEmpty(DebtorAddress))
            {
                msg = "Please enter DebtorAddress.";
            }
            if (string.IsNullOrEmpty(msg))
            {
                string KhaltiAPIURL = "billpayment/confirmbillpay.do/";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_EPF_ConfirmPayment(BatchCrncy, DebtorName, DebtorAddress);
                objRes = VendorApi_CommonHelper.RequestSERVICEGROUP_EPF_ConfirmPayment(JsonReq, KhaltiAPIURL);
                msg = objRes.Message;
            }
            return msg;
        }

        public static string RequestCIT_Categories(string Category, ref GetVendor_API_CIT_Category objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Category))
            {
                msg = "Please enter Category.";
            }

            if (string.IsNullOrEmpty(msg))
            {
                string AuthToken = gettoken_Local();
                string APINAME = "billers/v2/categories";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_CIT_Category(Category);
                objRes = RequestSERVICEGROUP_CIT_Category(JsonReq, APINAME, AuthToken);
                msg = objRes.Message;
            }
            return msg;
        }
        public static GetVendor_API_CIT_Category RequestSERVICEGROUP_CIT_Category(string JsonReq, string KhaltiUrl, string AuthToken)
        {
            GetVendor_API_CIT_Category objRes = new GetVendor_API_CIT_Category();
            try
            {
                string URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;

                if (MyPay.Models.Common.Common.ApplicationEnvironment.IsProduction == false)
                {
                    URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;
                }
                string json = PostMethod(URL, JsonReq, AuthToken);
                if (Common.IsValidJson(json))
                {

                    objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Category>(json);
                    objRes.Message = objRes.responseMessage;

                }
                else
                {
                    objRes.Message = json;
                }
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
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Category>(json);
                        return objRes;
                    }
                }
            }
            catch (Exception ex)
            {
                objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                //objRes.status = false;
                return objRes;
            }

        }
        public static string RequestCIT_JourneyDetails(string appCode, ref GetVendor_API_CIT_Journey_Details objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(appCode))
            {
                msg = "Please enter appCode.";
            }

            if (string.IsNullOrEmpty(msg))
            {
                string AuthToken = gettoken_Local();
                string APINAME = "billers/v2/appjourneydetails";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_CIT_JourneyDetails(appCode);
                objRes = RequestSERVICEGROUP_CIT_JourneyDetails(JsonReq, APINAME, AuthToken);
                msg = objRes.Message;
            }
            return msg;
        }
        public static GetVendor_API_CIT_Journey_Details RequestSERVICEGROUP_CIT_JourneyDetails(string JsonReq, string KhaltiUrl, string AuthToken)
        {
            GetVendor_API_CIT_Journey_Details objRes = new GetVendor_API_CIT_Journey_Details();
            try
            {
                string URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;

                if (MyPay.Models.Common.Common.ApplicationEnvironment.IsProduction == false)
                {
                    URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;
                }
                string json = PostMethod(URL, JsonReq, AuthToken);
                if (Common.IsValidJson(json))
                {

                    objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Journey_Details>(json);
                    objRes.Message = objRes.responseMessage;

                }
                else
                {
                    objRes.Message = json;
                }
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
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Journey_Details>(json);
                        return objRes;
                    }
                }
            }
            catch (Exception ex)
            {
                objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                //objRes.status = false;
                return objRes;
            }

        }
        public static string RequestCIT_LoanType(string appGroup, string fieldName, ref GetVendor_API_CIT_Loan_Type objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(appGroup))
            {
                msg = "Please enter appGroup.";
            }
            else if (string.IsNullOrEmpty(fieldName))
            {
                msg = "Please enter fieldName.";
            }

            if (string.IsNullOrEmpty(msg))
            {
                string AuthToken = gettoken_Local();
                string APINAME = "billers/v2/detail/cit-loan-type";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_CIT_LoanType(appGroup, fieldName);
                objRes = RequestSERVICEGROUP_CIT_LoanType(JsonReq, APINAME, AuthToken);
                msg = objRes.Message;
            }
            return msg;
        }
        public static GetVendor_API_CIT_Loan_Type RequestSERVICEGROUP_CIT_LoanType(string JsonReq, string KhaltiUrl, string AuthToken)
        {
            GetVendor_API_CIT_Loan_Type objRes = new GetVendor_API_CIT_Loan_Type();
            try
            {
                string URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;

                if (MyPay.Models.Common.Common.ApplicationEnvironment.IsProduction == false)
                {
                    URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;
                }
                string json = PostMethod(URL, JsonReq, AuthToken);
                if (Common.IsValidJson(json))
                {

                    objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Loan_Type>(json);
                    objRes.Message = objRes.responseMessage;

                }
                else
                {
                    objRes.Message = json;
                }
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
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Loan_Type>(json);
                        return objRes;
                    }
                }
            }
            catch (Exception ex)
            {
                objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                //objRes.status = false;
                return objRes;
            }

        }
        public static string RequestCIT_BillPayment(string batchId, string batchAmount, string batchCount, string batchCrncy, string categoryPurpose,
            string debtorAgent, string debtorBranch, string debtorName, string debtorAccount, string debtorIdType, string debtorIdValue,
            string debtorAddress, string debtorPhone, string debtorMobile, string debtorEmail, string instructionId, string endToEndId,
            string amount, string appId, string refId, string remarks, string freeCode1, ref GetVendor_API_CIT_Bill_Payment objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(batchId))
            {
                msg = "Please enter Batch Id.";
            }
            else if (string.IsNullOrEmpty(batchAmount))
            {
                msg = "Please enter Batch Amount.";
            }

            if (string.IsNullOrEmpty(msg))
            {
                string AuthToken = gettoken_Local();
                string APINAME = "api/billpayment/lodgebillpay.do";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_CIT_BillPayment(batchId, batchAmount, batchCount, batchCrncy, categoryPurpose,
                debtorAgent, debtorBranch, debtorName, debtorAccount, debtorIdType, debtorIdValue, debtorAddress, debtorPhone, debtorMobile, debtorEmail, instructionId, endToEndId,
                amount, appId, refId, remarks, freeCode1);
                objRes = RequestSERVICEGROUP_CIT_BillPayment(JsonReq, APINAME, AuthToken);
            }
            return msg;
        }

        public static GetVendor_API_CIT_Bill_Payment RequestSERVICEGROUP_CIT_BillPayment(string JsonReq, string KhaltiUrl, string AuthToken)
        {
            GetVendor_API_CIT_Bill_Payment objRes = new GetVendor_API_CIT_Bill_Payment();
            try
            {
                string URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;

                if (MyPay.Models.Common.Common.ApplicationEnvironment.IsProduction == false)
                {
                    URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;
                }
                string json = PostMethod(URL, JsonReq, AuthToken);
                if (Common.IsValidJson(json))
                {

                    objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Bill_Payment>(json);
                    objRes.Message = objRes.responseMessage;

                }
                else
                {
                    objRes.Message = json;
                }
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
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Bill_Payment>(json);
                        return objRes;
                    }
                }
            }
            catch (Exception ex)
            {
                objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                //objRes.status = false;
                return objRes;
            }

        }
        public static string RequestCIT_ConfirmBillPayment(string batchId, string batchAmount, string batchCount, string batchCrncy, string categoryPurpose,
           string debtorAgent, string debtorBranch, string debtorName, string debtorAccount, string debtorIdType, string debtorIdValue,
           string debtorAddress, string debtorPhone, string debtorMobile, string debtorEmail, string instructionId, string endToEndId,
           string amount, string appId, string refId, string remarks, string freeCode1, string authenticationToken, string UserInput,
           ref GetVendor_API_CIT_Bill_Payment objRes, ref AddVendor_API_Requests objVendor_API_Requests, ref bool IsCouponUnlocked, ref string TransactionID,
           AddCouponsScratched resCoupon, AddUserLoginWithPin resGetRecord, string BankTransactionId, string WalletType, string CustomerId,
           string Reference, string Version, string DeviceCode, string PlatForm, string MemberId)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(batchId))
            {
                msg = "Please enter Batch Id.";
            }
            else if (string.IsNullOrEmpty(batchAmount))
            {
                msg = "Please enter Batch Amount.";
            }

            if (string.IsNullOrEmpty(msg))
            {
                int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_CIT_Government_Payments;
                {
                    if (resGetRecord == null || resGetRecord.Id == 0)
                    {
                        msg = "Please enter Valid MemberId.";
                    }
                    else if (resGetRecord.IsKYCApproved != (int)AddUser.kyc.Verified)
                    {
                        msg = Common.GetKycMessage(resGetRecord, Convert.ToDecimal(batchAmount));
                    }
                    if (string.IsNullOrEmpty(msg))
                    {
                        WalletBalance = Convert.ToDecimal(resGetRecord.TotalAmount);
                        AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(resGetRecord.MemberId.ToString(), batchAmount, VendorApiType.ToString());
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
                            string AuthToken = gettoken_Local();
                            string APINAME = "api/billpayment/confirmbillpay.do";
                            string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_CIT_BillPayment(batchId, batchAmount, batchCount, batchCrncy, categoryPurpose,
                         debtorAgent, debtorBranch, debtorName, debtorAccount, debtorIdType, debtorIdValue, debtorAddress, debtorPhone, debtorMobile, debtorEmail, instructionId, endToEndId,
                         amount, appId, refId, remarks, freeCode1);

                            string MemberName = resGetRecord.FirstName + " " + resGetRecord.MiddleName + " " + resGetRecord.LastName;
                            decimal WalletBalance = Convert.ToDecimal(resGetRecord.TotalAmount);
                            if (resGetRecord == null || resGetRecord.Id == 0)
                            {
                                msg = "MemberId not found";
                                JsonReq = String.Empty;
                            }
                            else if (resGetRecord.IsKYCApproved != (int)AddUser.kyc.Verified)
                            {
                                msg = Common.GetKycMessage(resGetRecord, Convert.ToDecimal(batchAmount));
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
                                string TransactionUniqueId = string.Empty;
                                string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(VendorApiType)).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(APINAME,Reference, resGetRecord.MemberId, MemberName, JsonReq, authenticationToken, UserInput, DeviceCode, PlatForm, VendorApiType);
                                TransactionUniqueId = VendorApi_CommonHelper.UpdateWalletBalance(resCoupon, ref TransactionID, BankTransactionId, WalletType, CustomerId, batchAmount, out msg, VendorApiType, resGetRecord, objVendor_API_Requests, "", out WalletBalance);
                                if (msg == "success")
                                {
                                    string VendorOutputResponse = string.Empty;
                                    objRes = RequestSERVICEGROUP_CIT_ConfirmBillPayment(JsonReq, APINAME, AuthToken);
                                    objVendor_API_Requests = VendorApi_CommonHelper.UpdateVendorResponse(APINAME,JsonReq, DeviceCode, PlatForm, VendorApiType, VendorApiTypeName, objVendor_API_Requests.Id, "", VendorOutputResponse);
                                    if (objVendor_API_Requests.Id != 0 && objVendor_API_Requests.Res_Khalti_State.ToLower() == "success" && objRes.status == true)
                                    {

                                        msg = Common.UpdateCompleteTransaction(ref IsCouponUnlocked, ref TransactionID, resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName);
                                        if (msg.ToLower() == "success")
                                        {
                                            string Title = "Transaction successfull";
                                            string Message = $"Bill payment of amount Rs.{batchAmount} for {VendorApiTypeName} has been completed successfully.";//TransactionId " + resKhalti.TransactionUniqueId + " success for " + VendorApiTypeName;
                                            Models.Common.Common.SendNotification(authenticationToken, VendorApiType, resGetRecord.MemberId, Title, Message);
                                        }
                                    }
                                    else
                                    {
                                        msg = Common.RefundUpdateTransaction(resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, BankTransactionId, VendorApiType, WalletType, PlatForm, DeviceCode);
                                    }
                                }
                            }
                            objRes.UniqueTransactionId = VendorApi_CommonHelper.UniqueTransactionId.ToString();
                            Id = VendorApi_CommonHelper.Id;
                        }

                    }
                }
            }
            return msg;
        }
        public static GetVendor_API_CIT_Bill_Payment RequestSERVICEGROUP_CIT_ConfirmBillPayment(string JsonReq, string KhaltiUrl, string AuthToken)
        {
            GetVendor_API_CIT_Bill_Payment objRes = new GetVendor_API_CIT_Bill_Payment();
            try
            {
                string URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;

                if (MyPay.Models.Common.Common.ApplicationEnvironment.IsProduction == false)
                {
                    URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;

                }
                string json = PostMethod(URL, JsonReq, AuthToken);
                if (Common.IsValidJson(json))
                {

                    objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Bill_Payment>(json);
                    objRes.Message = objRes.responseMessage;

                }
                else
                {
                    objRes.Message = json;
                }
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
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_Bill_Payment>(json);
                        return objRes;
                    }
                }
            }
            catch (Exception ex)
            {
                objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                //objRes.status = false;
                return objRes;
            }

        }
        public static string RequestCIT_QueryTransaction(string instructionId, string batchId, string transactionId, string realTime, ref GetVendor_API_CIT_QueryTransaction objRes)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(instructionId))
            {
                msg = "Please enter instructionId.";
            }
            else if (string.IsNullOrEmpty(batchId))
            {
                msg = "Please enter batchId.";
            }
            else if (string.IsNullOrEmpty(transactionId))
            {
                msg = "Please enter transactionId.";
            }
            else if (string.IsNullOrEmpty(realTime))
            {
                msg = "Please enter realTime.";
            }

            if (string.IsNullOrEmpty(msg))
            {
                string AuthToken = gettoken_Local();
                string APINAME = "api/v2/query-transaction";
                string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest_SERVICEGROUP_CIT_QueryTransaction( instructionId,  batchId,  transactionId,  realTime);
                objRes = RequestSERVICEGROUP_CIT_QueryTransaction(JsonReq, APINAME, AuthToken);
                msg = objRes.Message;
            }
            return msg;
        }
        public static GetVendor_API_CIT_QueryTransaction RequestSERVICEGROUP_CIT_QueryTransaction(string JsonReq, string KhaltiUrl, string AuthToken)
        {
            GetVendor_API_CIT_QueryTransaction objRes = new GetVendor_API_CIT_QueryTransaction();
            try
            {
                string URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;

                if (MyPay.Models.Common.Common.ApplicationEnvironment.IsProduction == false)
                {
                    URL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + KhaltiUrl;
                }
                string json = PostMethod(URL, JsonReq, AuthToken);
                if (Common.IsValidJson(json))
                {

                    objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_QueryTransaction>(json);
                    objRes.Message = objRes.responseMessage;

                }
                else
                {
                    objRes.Message = json;
                }
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
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_CIT_QueryTransaction>(json);
                        return objRes;
                    }
                }
            }
            catch (Exception ex)
            {
                objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                //objRes.status = false;
                return objRes;
            }

        }
        public static string gettoken_Local()
        {
            string token = "";
            try
            {
                AddNCOauth obj = new AddNCOauth();
                string result = RepGovernmentNCHL.postwithdrawformmethodLocal("oauth/token", "grant_type=" + obj.grant_type + "&username=MYPAY@999" + "&password=123Abcd@123");
                if (!string.IsNullOrEmpty(result))
                {
                    GetNCAuthToken res = new GetNCAuthToken();
                    try
                    {
                        res = JsonConvert.DeserializeObject<GetNCAuthToken>(result);
                        Common.AddLogs(result, false, (int)AddLog.LogType.DBLogs);

                    }
                    catch (Exception ex)
                    {
                        Common.AddLogs(ex.Message + " : " + result, false, (int)AddLog.LogType.DBLogs);
                    }
                    if (!string.IsNullOrEmpty(res.access_token))
                    {
                        token = res.access_token;
                    }
                }
            }
            catch (Exception ex)
            {
                token = "Error:" + ex.Message;
            }
            return token;
        }

        public static string postwithdrawformmethodLocal(string apiname, string postData)
        {
            try
            {
                string responseFromServer = "";
                // Create a request using a URL that can receive a post.
                WebRequest request = WebRequest.Create(GOVERNMENT_SERVICE_API_URL_LINK_LOCAL + apiname);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                request.Headers.Add("Authorization", "Basic " + WithdrawBasicApiKey_Local);
                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Console.WriteLine(responseFromServer);
                }

                // Close the response.
                response.Close();
                return responseFromServer;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    if (httpResponse != null)
                    {
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            return text;
                        }
                    }
                    else
                    {
                        return e.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string PostMethod(string ApiName, string requestdata, string AuthToken)
        {
            string BaseURL = RepGovernmentNCHL.GOVERNMENT_SERVICE_API_URL_LINK_LOCAL;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(ApiName);
                byte[] bytes = null;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + AuthToken);
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.KeepAlive = false;
                request.Timeout = System.Threading.Timeout.Infinite;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Flush();
                requestStream.Close();

                HttpWebResponse response = default(HttpWebResponse);
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    Common.AddLogs("NCHLPostLinkMethod : ApiName " + BaseURL + ApiName + ". Request: " + requestdata + "  Response:" + responseStr, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true, "Web", "", (int)AddLog.LogActivityEnum.BankPostData, Common.CreatedBy, Common.CreatedByName);

                    return responseStr;
                }
                else
                {

                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    Common.AddLogs("NCHLPostLinkMethod : ApiName " + BaseURL + ApiName + ". Request: " + requestdata + "  Response:" + responseStr, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName);
                    return responseStr;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    if (e.Response == null && e.Message != null)
                    {
                        Common.AddLogs("Error NCHLPostLinkMethod: ApiName " + BaseURL + ApiName + ". Request: " + requestdata + " Response: " + e.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                        return e.Message;
                    }
                    else
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            Common.AddLogs("Error NCHLPostLinkMethod:" + text + e.Response.ToString() + e.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);

                            return text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs("Error NCHLPostLinkMethod:" + ex.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                return ex.Message;
            }
        }
    }
}