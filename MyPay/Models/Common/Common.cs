using ClosedXML;
using ClosedXML.Excel;
using DeviceId;
using Microsoft.IdentityModel.Tokens;
using MyPay.Models.Add;
using MyPay.Models.Common.Notifications;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Request;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.ConditionalFormatting;
using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MyPay.Models.Common
{
    public static class Common
    {
        public static string AndroidVersion = "3.1.5";
        public static string authenticationToken = String.Empty;
        public static Int64 DiffSeconds = 20;
        public static Int64 StartingNumber = 10000;
        public static Int64 CreatedBy = 10000;
        public static string CreatedByName = "Admin";
        public static string LocalSendSMSToken = "a2ba7aad024d1750e5d1f34204f0d594eb8b71129faffc750dff5b476aeb56ae";
        public static string SendSMSToken = "28826134c0c12588ebf248d27926ec849476c3f98ceab1536f12326c84421772";
        public static string WebsiteName = "MyPay";
        public static string AppLink = "#";
        public static string PlayStoreLink = "#";
        public static string IosVersion = "9.3";
        public static string dumpurl = "webdemo";
        //public static string dumpurl = "helloworld";
        public static string IsDevelopment = System.Configuration.ConfigurationManager.AppSettings["IsDevelopment"];

        ////public static string livesiteurl = "https://testwebdemo.mypay.com.np/";
        //public static string livesiteurl = "https://webdemo.mypay.com.np/";
        //public static string testsiteurl = "https://testwebdemo.mypay.com.np/";
        //public static string livesiteurl_user = "https://mypay.com.np/";
        //public static string liveapiurl = "https://smartdigitalnepal.com/";
        //public static string testapiurl = "https://testapi.mypay.com.np/";
        ////public static string liveapiurl = "https://testapi.mypay.com.np/";
        ///

        //public static string LiveSiteUrl = "http://localhost:56821/";
        //public static string TestSiteUrl = "http://localhost:56821/";
        //public static string LiveSiteUrl_User = "http://localhost:56821/";
        //public static string LiveApiUrl = "http://localhost:52676/";
        //public static string TestApiUrl = "http://localhost:52676/";
        //public static string CustomerPortalLive = "http://localhost:56821/";
        //public static string CustomerPortalTest = "http://localhost:56821/";


        // LIVE SERVER //
        //public static string LiveSiteUrl = "https://webdemo.mypay.com.np/";
        //public static string TestSiteUrl = "https://testwebdemo.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://mypay.com.np/";
        //public static string LiveApiUrl = "https://smartdigitalnepal.com/";
        //public static string TestApiUrl = "https://testapi.mypay.com.np/";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";


        //Reporting server
        //public static string LiveSiteUrl = "http://103.90.85.76:8090";
        //public static string TestSiteUrl = "http://103.90.85.76:8090";
        //public static string LiveSiteUrl_User = "http://103.90.85.76:8090";
        //public static string LiveApiUrl = "http://103.90.85.76:8090";
        //public static string TestApiUrl = "http://103.90.85.76:8090";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";

        /// <summary>
        /// STAGING
        ///// </summary>
        //public static string LiveSiteUrl = "https://staging1.mypay.com.np/";
        //public static string TestSiteUrl = "https://staging1.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://staging1.mypay.com.np/";
        //public static string LiveApiUrl = "https://stagingapi1.mypay.com.np/";
        //public static string TestApiUrl = "https://stagingapi1.mypay.com.np/";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";


        /// <summary>
        /// STAGING 2
        ///// </summary>
        //public static string LiveSiteUrl = "https://staging2.mypay.com.np/";
        //public static string TestSiteUrl = "https://staging2.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://staging2.mypay.com.np/";
        //public static string LiveApiUrl = "https://stagingapi2.mypay.com.np/";
        //public static string TestApiUrl = "https://stagingapi2.mypay.com.np/";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";


        //STAGING
        //public static string LiveSiteUrl = "https://staging2.mypay.com.np/";
        //public static string TestSiteUrl = "https://staging2.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://staging2.mypay.com.np/";
        //public static string LiveApiUrl = "https://stagingapi2.mypay.com.np/";
        //public static string TestApiUrl = "https://stagingapi2.mypay.com.np/";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";


        //ROSHAN
        //public static string LiveSiteUrl = "https://roshan.mypay.com.np/";
        //public static string TestSiteUrl = "https://roshan.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://roshan.mypay.com.np/";
        //public static string LiveApiUrl = "https://roshanapi.mypay.com.np/";
        //public static string TestApiUrl = "https://roshanapi.mypay.com.np/";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";


        //public static string LiveSiteUrl = "https://staging3.mypay.com.np/";
        //public static string TestSiteUrl = "https://staging3.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://staging3.com.np/";
        //public static string LiveApiUrl = "https://stagingapi3.mypay.com.np/";
        //public static string TestApiUrl = "https://stagingapi3.mypay.com.np/";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";



        //public static string LiveSiteUrl = "https://staging4.mypay.com.np/";
        //public static string TestSiteUrl = "https://staging4.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://staging4.com.np/";
        //public static string LiveApiUrl = "https://stagingapi4.mypay.com.np/";
        //public static string TestApiUrl = "https://stagingapi4.mypay.com.np/";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";

        //public static string LiveSiteUrl = "https://staging1.mypay.com.np/";
        //public static string TestSiteUrl = "https://staging1.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://staging1.com.np/";
        //public static string LiveApiUrl = "https://stagingapi1.mypay.com.np/";
        //public static string TestApiUrl = "https://stagingapi1.mypay.com.np/";
        //public static string CustomerPortalLive = "http://customer.mypay.com.np/";
        //public static string CustomerPortalTest = "http://testwebdemo.mypay.com.np/";

        //rabin_staging
        //public static string LiveSiteUrl = "https://staging4.mypay.com.np/";
        //public static string TestSiteUrl = "https://staging4.mypay.com.np/";
        //public static string LiveSiteUrl_User = "https://staging4.com.np/";
        //public static string LiveApiUrl = "https://stagingapi4.mypay.com.np/";
        //public static string TestApiUrl = "https://stagingapi4.mypay.com.np/";
        //public static string CustomerPortalLive = "https://staging4.mypay.com.np/";
        //public static string CustomerPortalTest = "https://staging4.mypay.com.np/";


        //Kiran_Agent
        public static string LiveSiteUrl = "https://agentweb.mypay.com.np/";
        public static string TestSiteUrl = "https://agentweb.mypay.com.np/";
        public static string LiveSiteUrl_User = "https://agentweb.com.np/";
        public static string LiveApiUrl = "https://agentapi.mypay.com.np/";
        public static string TestApiUrl = "https://agentapi.mypay.com.np/";
        public static string CustomerPortalLive = "https://agentweb.mypay.com.np/";
        public static string CustomerPortalTest = "https://agentweb.mypay.com.np/";


        public static string Linkedinlink = "https://www.linkedin.com/in/my-pay-97399b21a/";
        public static string FacebookLink = "https://www.facebook.com/MyPay.official";
        public static string TwitterLink = "https://twitter.com/MyPayofficial";
        public static string InstagramLink = "https://www.instagram.com/mypay.official/";
        public static string tel1 = "1660-016-2000";
        public static string tel2 = "01-5907481";
        public static string tel3 = "01-5907482";
        public static string tel4 = "01-5970139";
        public static string WebsiteEmail = "info@mypay.com.np";
        public static string FromEmail = "noreply@mypay.com.np";
        public static string FromEmailPassword = "Magh@123"; //"Dab81180";//"MyPay#0803"; //
        public static string FromEmailName = "MyPay Support";
        public static string EmailHost = "smtp.office365.com"; //"smtppro.zoho.com"; //
        public static string InsufficientBalance = "Insufficient Balance";
        public static string InsufficientBalance_MPCoins = "Insufficient MyPay Coins Balance";
        public static string Invalidpin = "Invalid Pin";
        public static string InvalidOTPMessage = "Too many Wrong OTP attempted. Please try again later after 2 hours. ";
        public static string InactiveUserMessage = "Your account status has been inactive due to some reasons please contact support@mypay.com.np";
        public static string UnauthorizedRequest = "Unauthorized Request";
        public static string Invalidusertoken = "Invalid User Token";
        public static string SessionExpired = "Session Expired";
        public static string Relogin = "Relogin";
        public static string LoginWithOTP = "Please enter the Code received in SMS.";
        public static string SetYourPin = "Please set your MPin";
        public static string FailedTxnLimit = "Failed Transactions Limit Reached. Please try again after 30 mins.";
        public static string TemporaryErrorMessage = "Network error. Please try again.";
        public static string TemporaryServiceUnavailable = "Temporarily service is unavailable.Please try after some time!";
        public static bool EnableSSl = true;
        public static Int32 EmailPort = 587;

        //public static string NICSecret = "f3127d45b3104fe2ac8abfb0aa490f4ab0c56dea580d4252acf95d136b29946a82597d4ca9be47a09189f8d60bc1420cb73752c841f4432d9cb11072098dad48c22101770750403fbe2ddfd2e225b8cf83ef79d15b834212814742c69661040f3b8ca12b4a5241f0baa89ce556e9c0cf3ac482c5d1524fcbb60b52bc3d6fc4ee";
        //public static string NICAccess_Key = "cc50749a81c830b5bab5a2fc85fa32d6";
        //public static string NICprofile_id = "44F103D8-E704-4749-BAF1-89EAE7DF5B30";

        public static string VISA_CardPaymentNICSecret = "9d425837f3744216af37f70f075475cca0b5d8140d0849bc8686935e1f2388fafd4f1cf132a84a6f8cef92d9128eb187ef800eb732084574b5ad5dba60ca715a3f93cb7fa5ac4ea0aa08ffec69cbab1bc8d0343e0193424ba7542cabbd6767a5e5deecd8159546d799a66c9d25d1046104e44a32a35d44ac811d11c25486327d";
        public static string VISA_CardPaymentNICAccess_Key = "a220ab5269a2355dacc71135b27cbdb9";
        public static string VISA_CardPaymentNICprofile_id = "6ADD8362-FA0D-4258-8738-B96870298E1B";

        //public static string NICSecret_Old = "435a361dbc6b4962b34fa1fcc73ab947792bc1a980034e3baba83687338ebd2ba951f9a22aa94e0f8cb8ab0bdcc589519b00ef9aa1824e2ab1a3d9285619ef59fa3e6f5d811446c8854172c02d919973d8d0b9d5dc624bb0b2a5a283da3de644507d34b903924abc8c75d3a809600f7795bc5c7fb50d4cbb9ffc074ac1401cdb";
        //public static string NICAccess_Key_Old = "f2ddcf81e6cf32ee992bdce1a71633b2";

        // Changed on 26-May-2023 on Sangita maam request ticket no. #464
        public static string NICSecret = "849b67b0477c460a8deb0fa1b0b32de12f8cbcef894f43c8916f081ccafcf5b2089554e71e8a4b149507300dfb3d122a23ae06886bfc457892a8fd3516b8528ce28c8bf4e39246ddbd077e13b5249cbdefc6a0361e4042e28901acd9b4e8470f2fcd356887204a2bbc9794787af985a05df5372f46344bdf9965c4d72f2cc18d";
        public static string NICAccess_Key = "55abc029a7c03c06bcdbf150de720837";

        public static string NICprofile_id = "A6CCA219-2D15-4F83-BFD2-E72FF68172BD";
        public static string ConnectIps_BankId = "2301";
        public static string ConnectIps_AccountName = "Smart Card Nepal Pvt Ltd";
        public static string ConnectIPs_BranchId = "284";
        public static string ConnectIPs_AccountNumber = "2844150059837002";
        public static string ConnectIPs_BankName = "NIC ASIA Bank Limited";
        public static string ConnectIPs_BranchName = "Radhe Radhe";

        //public static string ConnectIps_BankId_Staging = "2501";
        //public static string ConnectIps_AccountName_Staging = "Tika Ram Basnet";
        //public static string ConnectIPs_BranchId_Staging = "990";
        //public static string ConnectIPs_AccountNumber_Staging = "0010120320200016";
        //public static string ConnectIPs_BankName_Staging = "NMB Bank Limited";
        //public static string ConnectIPs_BranchName_Staging = "Radhe Radhe";

        public static string ConnectIps_BankId_Staging = "2501";
        public static string ConnectIps_AccountName_Staging = "Ankit Neupane";
        public static string ConnectIPs_BranchId_Staging = "990";
        public static string ConnectIPs_AccountNumber_Staging = "0010055573200018";
        public static string ConnectIPs_BankName_Staging = "NMB Bank Limited";
        public static string ConnectIPs_BranchName_Staging = "Radhe Radhe";

        public static string SecretKey = "nNSvnwvz4C2qju90mf7YzHiWInrbvDG2YPbbtf2L0Dj7QtDeYBTNJ83M1EWR2Mjd4OmMuJNBqG2EBujqGfJTwFxLDRNknvqt";
        public static string SecretKeyForWebAPICall = "ua5MRVa70MizJ1x5aDvMWRVeoH4vohGRUJ374IgJt4M+pdbI56WrtjDQ38K23GkFZrGTUKR/UYthR9N1QYly37jWZ3j0fNsdO0DM4NyekC5jsop/E54795BGGva4iTZ21uR0OM8LH6g=";
        public static string WebPassword = "test123456";


        public static List<string> SandboxContactsList = new List<string>(new string[] { "9817946035", "1111111111", "9800000001", "9863535353", "9812343248", "9801129367", "9802771800", "9803505220" });

        public static string ServiceDown = "ServiceDown";

        public static string ReSendOTPMessage = "You cannot request to resend OTP before " + ApplicationEnvironment.ResendOTPTime + " sec.";
        public static string AdminEmail = ApplicationEnvironment.AdminEmail;
        public static string BCC = ApplicationEnvironment.BCC;
        public static string BCC2 = ApplicationEnvironment.BCC2;
        private static string[] mobileDevices = new string[] {"iphone","ppc",
                                                      "windows ce","blackberry",
                                                      "opera mini","mobile","palm",
                                                      "portable","opera mobi" };

        public static string RegEx_SpecialChars = @"[^0-9a-zA-Z]+";
        public static string GenerateReferenceUniqueID()
        {
            Guid g = Guid.NewGuid();
            return DateTime.UtcNow.ToString("ddMMyyhhmmssms") + Common.RandomNumber(111, 999).ToString();
        }
        public static bool IsMobileDevice(string userAgent)
        {
            // TODO: null check
            userAgent = userAgent.ToLower();
            return mobileDevices.Any(x => userAgent.Contains(x));
        }
        public static string UpdateVotingCandidateRank(AddVotingCandidate objCandidate, VotingLists res)
        {
            string result = "";
            try
            {
                CommonHelpers obj = new CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("VotingCompetitionID", objCandidate.VotingCompetitionID);
                string ResultId = obj.ExecuteProcedureGetReturnValue(Common.StoreProcedures.sp_VotingCandidate_Rank_Update, HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    result = "Voting Candidate Rank Updated for Competition ID: " + objCandidate.VotingCompetitionID.ToString();
                }
                else
                {
                    result = $"Voting Candidate Rank Updated failed for Competition ID: {objCandidate.VotingCompetitionID.ToString()}";
                }
            }
            catch (Exception ex)
            {
                result = $"Voting Candidate Rank Updated failed for Competition ID: {objCandidate.VotingCompetitionID.ToString()} Exception : {ex.Message}";
            }
            AddLogs(result, false, Convert.ToInt32(AddLog.LogType.Voting), Convert.ToInt64(res.MemberID), "", true, res.PlatForm, res.DeviceCode);
            return result;
        }
        public static string UpdateCompleteTransaction(ref bool IsCouponUnlocked, ref string TransactionID, AddUserLoginWithPin resGetRecord, AddVendor_API_Requests objVendor_API_Requests, string TransactionUniqueId, string VendorApiTypeName, string CustomerID = "", string Remarks = "", string RecieverAccountNo = "", Int32 CouponApplyType = 1, string AdditionalInfo = "", string overriddenTxnDescription = "", string gatewayId = "", string trackerId = "")
        {
            string result = "";
            try
            {
                WalletTransactions res_transaction = new WalletTransactions();
                res_transaction.MemberId = Convert.ToInt64(resGetRecord.MemberId);
                res_transaction.TransactionUniqueId = TransactionUniqueId;
                if (!string.IsNullOrEmpty(TransactionUniqueId) && res_transaction.GetRecord())
                {
                    res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                    res_transaction.GatewayStatus = objVendor_API_Requests.Res_Khalti_Status ? "Success" : res_transaction.GatewayStatus;

                    string CustomRemarks = Common.GetTransactionRemarks(res_transaction.Type, res_transaction.TransactionAmount, res_transaction.CustomerID);
                    if (CustomRemarks != "")
                    {
                        res_transaction.Remarks = CustomRemarks;
                    }
                    else
                    {
                        res_transaction.Remarks = (Remarks == "" ? objVendor_API_Requests.Res_Khalti_Message : Remarks);
                    }
                    if (!string.IsNullOrEmpty(overriddenTxnDescription))
                    {
                        res_transaction.Description = overriddenTxnDescription;
                    }
                    else
                    {
                        res_transaction.Description = res_transaction.Remarks;
                    }
                    //res_transaction.Description = res_transaction.Remarks;

                    //New code for NCHL added here by ROshan
                    if (trackerId != "")
                    {
                        res_transaction.Reference = trackerId;
                        res_transaction.VendorTransactionId = gatewayId;
                    }

                    res_transaction.VendorTransactionId = objVendor_API_Requests.Res_Khalti_Id;
                    res_transaction.VendorResponsePin = objVendor_API_Requests.Res_Khalti_Pin;
                    res_transaction.VendorResponseSerial = objVendor_API_Requests.Res_Khalti_Serail;
                    res_transaction.AdditionalInfo1 = string.IsNullOrEmpty(res_transaction.AdditionalInfo1) ? "" : res_transaction.AdditionalInfo1;
                    if (res_transaction.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay)
                    {
                        res_transaction.VendorTransactionId = gatewayId;
                    }
                    if (res_transaction.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.FonePay_QR_Payments)
                    {
                        res_transaction.VendorTransactionId = objVendor_API_Requests.Res_TraceId;
                    }
                    if (!string.IsNullOrEmpty(objVendor_API_Requests.Res_Khalti_Pin) && !string.IsNullOrEmpty(objVendor_API_Requests.Res_Khalti_Serail))
                    {
                        res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    }
                    if (!string.IsNullOrEmpty(CustomerID))
                    {
                        res_transaction.CustomerID = CustomerID;
                    }
                    if (!string.IsNullOrEmpty(RecieverAccountNo))
                    {
                        res_transaction.RecieverAccountNo = RecieverAccountNo;
                    }

                    if (res_transaction.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine)
                    {
                        res_transaction.AdditionalInfo1 = CustomerID;
                        res_transaction.AdditionalInfo2 = AdditionalInfo;
                        res_transaction.Description = $"Traffic Fine Payment of Chit no. {CustomerID}. Violation: {Remarks}  by  {RecieverAccountNo.ToUpper()}. ";
                        res_transaction.Remarks = objVendor_API_Requests.Res_Khalti_Message;
                    }
                    //if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_antivirus_kaspersky)
                    //{
                    //    res_transaction.VendorResponsePin = objVendor_API_Requests.Res_Khalti_Pin;
                    //    res_transaction.VendorResponseSerial = objVendor_API_Requests.Res_Khalti_Serail;
                    //    res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    //}
                    //if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ntc_erc)
                    //{
                    //    res_transaction.VendorResponsePin = objVendor_API_Requests.Pin;
                    //    res_transaction.VendorResponseSerial = objVendor_API_Requests.Serial;
                    //    res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    //}
                    //if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_broadlink_erc)
                    //{
                    //    res_transaction.VendorResponsePin = VendorApi_CommonHelper.Pin;
                    //    res_transaction.VendorResponseSerial = VendorApi_CommonHelper.Serial;
                    //    res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    //}
                    //if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_dishhome_erc)
                    //{
                    //    res_transaction.VendorResponsePin = VendorApi_CommonHelper.Pin;
                    //    res_transaction.VendorResponseSerial = VendorApi_CommonHelper.Serial;
                    //    res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    //}
                    //if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nettv_erc)
                    //{
                    //    res_transaction.VendorResponsePin = VendorApi_CommonHelper.Pin;
                    //    res_transaction.VendorResponseSerial = VendorApi_CommonHelper.Serial;
                    //    res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    //}
                    //if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ntc_erc)
                    //{
                    //    res_transaction.VendorResponsePin = VendorApi_CommonHelper.Pin;
                    //    res_transaction.VendorResponseSerial = VendorApi_CommonHelper.Serial;
                    //    res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    //}
                    //if (VendorApiType == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_smart_erc)
                    //{
                    //    res_transaction.VendorResponsePin = VendorApi_CommonHelper.Pin;
                    //    res_transaction.VendorResponseSerial = VendorApi_CommonHelper.Serial;
                    //    res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    //}
                }

                if (Convert.ToString(objVendor_API_Requests.VendorApiType) == "202")
                {
                    res_transaction.VendorTransactionId = gatewayId;
                }
                
               
                if (res_transaction.Update())
                {
                    res_transaction.Id = 0;
                    res_transaction.AddCashBack();
                    IsCouponUnlocked = Common.AssignCoupons(resGetRecord.MemberId, res_transaction.TransactionUniqueId, CouponApplyType);
                    VendorApi_CommonHelper.DistributeComission(res_transaction, resGetRecord, VendorApiTypeName, objVendor_API_Requests);
                    Common.AddLogs("Transaction Completed For " + VendorApiTypeName + " (Transaction ID: " + TransactionUniqueId.ToString() + " )", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(res_transaction.MemberId), res_transaction.MemberName, true, res_transaction.Platform, res_transaction.DeviceCode);

                    result = "success";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string UpdateCableCompleteTransaction(ref bool IsCouponUnlocked, ref string TransactionID, AddUserLoginWithPin resGetRecord, AddVendor_API_Requests objVendor_API_Requests, string TransactionUniqueId, string VendorApiTypeName, string CustomerID = "", string Remarks = "", string RecieverAccountNo = "", Int32 CouponApplyType = 1, string AdditionalInfo = "")
        {
            string result = "";
            try
            {
                WalletTransactions res_transaction = new WalletTransactions();
                res_transaction.MemberId = Convert.ToInt64(resGetRecord.MemberId);
                res_transaction.TransactionUniqueId = TransactionUniqueId;
                if (!string.IsNullOrEmpty(TransactionUniqueId) && res_transaction.GetRecord())
                {
                    res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                    res_transaction.GatewayStatus = objVendor_API_Requests.Res_Khalti_Status ? "Success" : res_transaction.GatewayStatus;
                    string CustomRemarks = Common.GetTransactionRemarks(res_transaction.Type, res_transaction.TransactionAmount, res_transaction.CustomerID);
                    if (CustomRemarks != "")
                    {
                        res_transaction.Remarks = CustomRemarks;
                    }
                    else
                    {
                        res_transaction.Remarks = (Remarks == "" ? objVendor_API_Requests.Res_Khalti_Message : Remarks);
                    }

                    res_transaction.Description = "Payment successful for cablecar";
                    res_transaction.VendorTransactionId = objVendor_API_Requests.TransactionUniqueId;
                    res_transaction.VendorResponsePin = objVendor_API_Requests.Res_Khalti_Pin;
                    res_transaction.VendorResponseSerial = objVendor_API_Requests.Res_Khalti_Serail;
                    res_transaction.AdditionalInfo1 = "";
                    res_transaction.Reference = objVendor_API_Requests.Req_ReferenceNo;
                    res_transaction.CustomerID = objVendor_API_Requests.Req_ReferenceNo;

                    res_transaction.TransactionUniqueId = TransactionUniqueId;
                    if (res_transaction.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.FonePay_QR_Payments)
                    {
                        res_transaction.VendorTransactionId = objVendor_API_Requests.Res_TraceId;
                    }
                    if (!string.IsNullOrEmpty(objVendor_API_Requests.Res_Khalti_Pin) && !string.IsNullOrEmpty(objVendor_API_Requests.Res_Khalti_Serail))
                    {
                        res_transaction.Remarks = $"{res_transaction.Remarks}. Pin: {res_transaction.VendorResponsePin} Serial: {res_transaction.VendorResponseSerial}";
                    }
                    if (!string.IsNullOrEmpty(CustomerID))
                    {
                        res_transaction.CustomerID = CustomerID;
                    }
                    if (!string.IsNullOrEmpty(RecieverAccountNo))
                    {
                        res_transaction.RecieverAccountNo = RecieverAccountNo;
                    }

                    if (res_transaction.Type == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine)
                    {
                        res_transaction.AdditionalInfo1 = CustomerID;
                        res_transaction.AdditionalInfo2 = AdditionalInfo;
                        res_transaction.Description = $"Traffic Fine Payment of Chit no. {CustomerID}. Violation: {Remarks}  by  {RecieverAccountNo.ToUpper()}. ";
                        res_transaction.Remarks = objVendor_API_Requests.Res_Khalti_Message;
                    }

                }
                if (res_transaction.Update())
                {
                    res_transaction.Id = 0;
                    res_transaction.AddCashBack();

                    IsCouponUnlocked = Common.AssignCoupons(resGetRecord.MemberId, res_transaction.TransactionUniqueId, CouponApplyType);
                    VendorApi_CommonHelper.DistributeComission(res_transaction, resGetRecord, VendorApiTypeName, objVendor_API_Requests);
                    Common.AddLogs("Transaction Completed For " + VendorApiTypeName + " (Transaction ID: " + TransactionUniqueId.ToString() + " )", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(res_transaction.MemberId), res_transaction.MemberName, true, res_transaction.Platform, res_transaction.DeviceCode);

                    result = "success";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }



        public static string RefundUpdateTransaction(AddUserLoginWithPin resGetRecord, AddVendor_API_Requests objVendor_API_Requests, string TransactionUniqueId, string VendorApiTypeName, string BankTransactionId, int VendorApiType, string WalletType, string PlatForm, string DeviceCode)
        {
            string msg = "";
            try
            {

                if (objVendor_API_Requests.Id != 0)
                {
                    // Queued , Processing, Pending, Failed, Expired, Error, Status Error
                    string LogMsg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + " for " + VendorApiTypeName + " on " + fnGetdatetime() + ". " + objVendor_API_Requests.Res_Khalti_Message;

                    if (objVendor_API_Requests.Res_Khalti_ErrorCode == "1009")
                    {
                        LogMsg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + " for " + VendorApiTypeName + " on " + fnGetdatetime() + ". Invalid Number.";
                        msg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + ": Invalid Number.";
                    }
                    WalletTransactions res_transaction = new WalletTransactions();
                    res_transaction.MemberId = Convert.ToInt64(resGetRecord.MemberId);
                    res_transaction.TransactionUniqueId = TransactionUniqueId;
                    if (res_transaction.GetRecord())
                    {
                        if (objVendor_API_Requests.Res_Khalti_State.ToLower() == "failed" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "expired" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "error" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "status error")
                        {
                            res_transaction.Status = (int)WalletTransactions.Statuses.Failed;
                        }
                        else if (objVendor_API_Requests.Res_Khalti_State.ToLower() == "queued" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "processing" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "pending")
                        {
                            res_transaction.Status = (int)WalletTransactions.Statuses.Pending;
                        }
                        res_transaction.Remarks = objVendor_API_Requests.Res_Khalti_Message;
                        res_transaction.Description = objVendor_API_Requests.Res_Khalti_Message;


                        if (VendorApiType == 200 || VendorApiType == 202)  //--bus Sewa --// //-- Nepal Pay QR --// 
                        {
                            res_transaction.GatewayStatus = objVendor_API_Requests.Res_Khalti_Status == false ? "Failed" : "";
                            //if (VendorApiType == 202)
                            //{
                            //    resGetRecord=
                            //}
                        }
                        if (res_transaction.Update())
                        {
                            Common.AddLogs("Transaction " + objVendor_API_Requests.Res_Khalti_State.ToLower() + " For " + VendorApiTypeName + " (Transaction ID: " + res_transaction.TransactionUniqueId.ToString() + " )", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(res_transaction.MemberId), res_transaction.MemberName, true, res_transaction.Platform, res_transaction.DeviceCode);

                            if ((objVendor_API_Requests.Res_Khalti_State.ToLower() == "expired") || (objVendor_API_Requests.Res_Khalti_State.ToLower() == "error") || (objVendor_API_Requests.Res_Khalti_State.ToLower() == "status error") || (objVendor_API_Requests.Res_Khalti_State.ToLower() == "failed"))
                            {
                                if (VendorApiType == 202)
                                {
                                    msg = VendorApi_CommonHelper.RefundLinkedBankTransfer(authenticationToken, BankTransactionId, VendorApiType, WalletType, resGetRecord, TransactionUniqueId);

                                }
                                else
                                {
                                    msg = VendorApi_CommonHelper.RefundLinkedBankTransfer(authenticationToken, BankTransactionId, VendorApiType, WalletType, resGetRecord, TransactionUniqueId);

                                }
                                msg = "Transaction ID : " + res_transaction.TransactionUniqueId + " Is " + objVendor_API_Requests.Res_Khalti_State;
                                LogMsg = LogMsg + msg + "  as '" + objVendor_API_Requests.Res_Khalti_Message + "'";
                            }
                            else
                            {
                                msg = "Transaction ID : " + res_transaction.TransactionUniqueId + " Is " + objVendor_API_Requests.Res_Khalti_State;
                                LogMsg = msg + "  as '" + objVendor_API_Requests.Res_Khalti_Message + "'";
                            }
                        }
                        else
                        {
                            msg = "Refund Transaction Not Updated For TransactionID " + res_transaction.TransactionUniqueId;
                            LogMsg = msg;
                        }
                    }
                    AddLogs(LogMsg, false, Convert.ToInt32(AddLog.LogType.Utility), Convert.ToInt64(objVendor_API_Requests.MemberId), "", true, PlatForm, DeviceCode);
                }

            }
            catch (Exception ex)
            {
                msg = "Refund:" + ex.Message;
            }
            return msg;

        }

        public static string RefundUpdateTransactionNCHL(string name,AddUserLoginWithPin resGetRecord, AddVendor_API_Requests objVendor_API_Requests, string TransactionUniqueId, string VendorApiTypeName, string BankTransactionId, int VendorApiType, string WalletType, string PlatForm, string DeviceCode)
        {
            string msg = "";
            try
            {

                if (objVendor_API_Requests.Id != 0)
                {
                    // Queued , Processing, Pending, Failed, Expired, Error, Status Error
                    string LogMsg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + " for " + VendorApiTypeName + " on " + fnGetdatetime() + ". " + objVendor_API_Requests.Res_Khalti_Message;

                    if (objVendor_API_Requests.Res_Khalti_ErrorCode == "1009")
                    {
                        LogMsg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + " for " + VendorApiTypeName + " on " + fnGetdatetime() + ". Invalid Number.";
                        msg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + ": Invalid Number.";
                    }
                    WalletTransactions res_transaction = new WalletTransactions();
                    res_transaction.MemberId = Convert.ToInt64(resGetRecord.MemberId);
                    res_transaction.TransactionUniqueId = TransactionUniqueId;
                    if (res_transaction.GetRecord())
                    {
                        if (objVendor_API_Requests.Res_Khalti_State.ToLower() == "failed" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "expired" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "error" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "status error")
                        {
                            res_transaction.Status = (int)WalletTransactions.Statuses.Failed;
                        }
                        else if (objVendor_API_Requests.Res_Khalti_State.ToLower() == "queued" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "processing" ||
                            objVendor_API_Requests.Res_Khalti_State.ToLower() == "pending")
                        {
                            res_transaction.Status = (int)WalletTransactions.Statuses.Pending;
                        }
                        res_transaction.Remarks = objVendor_API_Requests.Res_Khalti_Message;
                        res_transaction.Description = objVendor_API_Requests.Res_Khalti_Message;


                        if ( VendorApiType == 202)   //-- Nepal Pay QR --// 
                        {
                            res_transaction.Status = (int)WalletTransactions.Statuses.Failed;
                            res_transaction.GatewayStatus = objVendor_API_Requests.Res_Khalti_Status == false ? "Failed" : "";
                            //if (VendorApiType == 202)
                            //{
                            //    resGetRecord=
                            //}
                        }
                        if (res_transaction.Update())
                        {
                            Common.AddLogs("Transaction " + objVendor_API_Requests.Res_Khalti_State.ToLower() + " For " + VendorApiTypeName + " (Transaction ID: " + res_transaction.TransactionUniqueId.ToString() + " )", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(res_transaction.MemberId), res_transaction.MemberName, true, res_transaction.Platform, res_transaction.DeviceCode);

                            if (VendorApiType == 202)
                            {
                                msg = VendorApi_CommonHelper.RefundLinkedBankTransferNCHL(name,authenticationToken, BankTransactionId, VendorApiType, WalletType, resGetRecord, TransactionUniqueId);

                            }
                            else
                            {
                                msg = "Refund Transaction Not Updated For TransactionID " + res_transaction.TransactionUniqueId;
                                LogMsg = msg;
                                AddLogs(LogMsg, false, Convert.ToInt32(AddLog.LogType.Utility), Convert.ToInt64(objVendor_API_Requests.MemberId), "", true, PlatForm, DeviceCode);
                                return msg;
                            }
                            //msg = "Transaction ID : " + res_transaction.TransactionUniqueId + " Is " + objVendor_API_Requests.Res_Khalti_State;
                            //LogMsg = LogMsg + msg + "  as '" + objVendor_API_Requests.Res_Khalti_Message + "'";

                        }
                        else
                        {
                            msg = "Refund Transaction Not Updated For TransactionID " + res_transaction.TransactionUniqueId;
                            LogMsg = msg;
                        }
                    }
                    AddLogs(LogMsg, false, Convert.ToInt32(AddLog.LogType.Utility), Convert.ToInt64(objVendor_API_Requests.MemberId), "", true, PlatForm, DeviceCode);
                }

            }
            catch (Exception ex)
            {
                msg = "Refund:" + ex.Message;
            }
            return msg;

        }



        //public static string RefundUpdateTransaction_NCHL(string refundtype,string amount,AddUserLoginWithPin resGetRecord, AddVendor_API_Requests objVendor_API_Requests, string TransactionUniqueId, string VendorApiTypeName, string BankTransactionId, int VendorApiType, string WalletType, string PlatForm, string DeviceCode)
        //{
        //    string msg = "";
        //    try
        //    {

        //        if (objVendor_API_Requests.Id != 0)
        //        {
        //            // Queued , Processing, Pending, Failed, Expired, Error, Status Error
        //            string LogMsg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + " for " + VendorApiTypeName + " on " + fnGetdatetime() + ". " + objVendor_API_Requests.Res_Khalti_Message;

        //            if (objVendor_API_Requests.Res_Khalti_ErrorCode == "1009")
        //            {
        //                LogMsg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + " for " + VendorApiTypeName + " on " + fnGetdatetime() + ". Invalid Number.";
        //                msg = "Transaction " + objVendor_API_Requests.Res_Khalti_State + ": Invalid Number.";
        //            }
        //            WalletTransactions res_transaction = new WalletTransactions();
        //            res_transaction.MemberId = Convert.ToInt64(resGetRecord.MemberId);
        //            res_transaction.TransactionUniqueId = TransactionUniqueId;
        //            if (res_transaction.GetRecord())
        //            {
        //                if (objVendor_API_Requests.Res_Khalti_State.ToLower() == "failed" ||
        //                    objVendor_API_Requests.Res_Khalti_State.ToLower() == "expired" ||
        //                    objVendor_API_Requests.Res_Khalti_State.ToLower() == "error" ||
        //                    objVendor_API_Requests.Res_Khalti_State.ToLower() == "status error")
        //                {
        //                    res_transaction.Status = (int)WalletTransactions.Statuses.Failed;
        //                }
        //                else if (objVendor_API_Requests.Res_Khalti_State.ToLower() == "queued" ||
        //                    objVendor_API_Requests.Res_Khalti_State.ToLower() == "processing" ||
        //                    objVendor_API_Requests.Res_Khalti_State.ToLower() == "pending")
        //                {
        //                    res_transaction.Status = (int)WalletTransactions.Statuses.Pending;
        //                }
        //                res_transaction.Remarks = objVendor_API_Requests.Res_Khalti_Message;
        //                res_transaction.Description = objVendor_API_Requests.Res_Khalti_Message;


        //                if (VendorApiType == 200 || VendorApiType == 202)  //--bus Sewa --// //-- Nepal Pay QR --// 
        //                {
        //                    res_transaction.GatewayStatus = objVendor_API_Requests.Res_Khalti_Status == false ? "Failed" : "";
        //                    //if (VendorApiType == 202)
        //                    //{
        //                    //    resGetRecord=
        //                    //}
        //                }
        //                if (res_transaction.Update())
        //                {
        //                    Common.AddLogs("Transaction " + objVendor_API_Requests.Res_Khalti_State.ToLower() + " For " + VendorApiTypeName + " (Transaction ID: " + res_transaction.TransactionUniqueId.ToString() + " )", false, Convert.ToInt32(AddLog.LogType.Transaction), Convert.ToInt64(res_transaction.MemberId), res_transaction.MemberName, true, res_transaction.Platform, res_transaction.DeviceCode);

        //                    if ((objVendor_API_Requests.Res_Khalti_State.ToLower() == "expired") || (objVendor_API_Requests.Res_Khalti_State.ToLower() == "error") || (objVendor_API_Requests.Res_Khalti_State.ToLower() == "status error") || (objVendor_API_Requests.Res_Khalti_State.ToLower() == "failed"))
        //                    {
        //                        if (VendorApiType == 202 && refundtype.ToLower()=="partial")
        //                        {

        //                            msg = VendorApi_CommonHelper.RefundLinkedBankTransfer_NCHL("partial", amount,authenticationToken, BankTransactionId, VendorApiType, WalletType, resGetRecord, TransactionUniqueId);

        //                        }
        //                        else if (VendorApiType ==  202 && refundtype.ToLower() == "full")
        //                        {
        //                            msg = VendorApi_CommonHelper.RefundLinkedBankTransfer(authenticationToken, BankTransactionId, VendorApiType, WalletType, resGetRecord, TransactionUniqueId);

        //                        }
        //                        else
        //                        {
        //                            msg = "Refund Transaction Not Updated For TransactionID " + res_transaction.TransactionUniqueId;
        //                            LogMsg = msg;
        //                            AddLogs(LogMsg, false, Convert.ToInt32(AddLog.LogType.Utility), Convert.ToInt64(objVendor_API_Requests.MemberId), "", true, PlatForm, DeviceCode);
        //                            return msg;
        //                        }
        //                        msg = "Transaction ID : " + res_transaction.TransactionUniqueId + " Is " + objVendor_API_Requests.Res_Khalti_State;
        //                        LogMsg = LogMsg + msg + "  as '" + objVendor_API_Requests.Res_Khalti_Message + "'";
        //                    }
        //                    else
        //                    {
        //                        msg = "Transaction ID : " + res_transaction.TransactionUniqueId + " Is " + objVendor_API_Requests.Res_Khalti_State;
        //                        LogMsg = msg + "  as '" + objVendor_API_Requests.Res_Khalti_Message + "'";
        //                    }
        //                }
        //                else
        //                {
        //                    msg = "Refund Transaction Not Updated For TransactionID " + res_transaction.TransactionUniqueId;
        //                    LogMsg = msg;
        //                }
        //            }
        //            AddLogs(LogMsg, false, Convert.ToInt32(AddLog.LogType.Utility), Convert.ToInt64(objVendor_API_Requests.MemberId), "", true, PlatForm, DeviceCode);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Refund:" + ex.Message;
        //    }
        //    return msg;

        //}



        public static string getHashMD5(string JSONInput)
        {
            //var jsonObject = JsonConvert.DeserializeObject(JSONInput);
            string concatenated = "";

            JObject jsonObject = JObject.Parse(JSONInput);

            foreach (var item in jsonObject)
            {
                if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()) && item.Key.ToLower() != "hash" &&
                        item.Key.ToLower() != "passengersclassstring" && item.Key.ToLower() != "playerlist" && item.Key.ToLower() != "bankdata" &&
                        item.Key.ToLower() != "vendorjsonlookup")
                {
                    if (item.Key.ToLower() == "mpin")
                    {
                        concatenated += Common.Decryption(item.Value.ToString());
                    }
                    else
                    {
                        concatenated += item.Value.ToString();
                    }
                }
            }

            string result = "";
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(concatenated));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();

            return result;
        }

        //public static string getHashMD5(string JSONInput)
        //{

        //    //var jsonObject = JsonConvert.DeserializeObject(JSONInput);
        //    string concatenated = "";

        //    JObject jsonObject = JObject.Parse(JSONInput);

        //    foreach (var item in jsonObject)
        //    {
        //        if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()) && item.Key.ToLower() != "hash" &&
        //                item.Key.ToLower() != "passengersclassstring" && item.Key.ToLower() != "playerlist" && item.Key.ToLower() != "bankdata" &&
        //                item.Key.ToLower() != "vendorjsonlookup")
        //        {
        //            if (item.Key.ToLower() == "mpin")
        //            {
        //                concatenated += Common.Decryption(item.Value.ToString());
        //            }
        //            else
        //            {
        //                concatenated += item.Value.ToString();
        //            }
        //        }

        //    }
        //    return concatenated;
        //}
        public static string CheckHash<T>(T myObject, string Check = "")
        {
            string result = "";
            try
            {

                Type myType = myObject.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(myObject, null);
                    if (propValue != null && !string.IsNullOrEmpty(propValue.ToString()) && prop.Name != "Hash" && prop.Name != "PassengersClassString" && prop.Name != "PlayerList" && prop.Name != "BankData" && prop.Name.ToLower() != "vendorjsonlookup")
                    {
                        if (prop.Name == "Mpin")
                        {
                            result += Common.Decryption(propValue.ToString());
                        }
                        else
                        {
                            result += propValue.ToString();
                        }
                    }
                }
                if (!string.IsNullOrEmpty(result))
                {
                    result = MD5Hash(result).ToLower();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public static Int64 GetNewAdminLoginId()
        {
            Int64 Id = 0;
            MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
            string Result = commonHelpers.GetScalarValueWithValue("SELECT TOP 1 max(MemberId) FROM AdminLogin with(nolock)");
            if (!string.IsNullOrEmpty(Result) && Result != "0")
            {
                Id = Convert.ToInt64(Result) + 1;
            }
            else
            {
                Id = MyPay.Models.Common.Common.StartingNumber;
            }
            return Id;
        }

        public static string TruncateLongString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            else
            {
                return str.Substring(0, Math.Min(str.Length, maxLength));
            }
        }

        public static string CheckPin(Int64 MemberId, string pin)
        {

            string result = "";
            try
            {
                AddUserLoginWithPin outer = new AddUserLoginWithPin();
                GetUserLoginWithPin inner = new GetUserLoginWithPin();
                inner.MemberId = Convert.ToInt64(MemberId);
                AddUserLoginWithPin resuserdetails = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inner, outer);
                if (resuserdetails.Id == 0)
                {
                    result = "User Not Found";
                }
                else if (Common.DecryptString(resuserdetails.Pin) != pin)
                {
                    result = "You have entered wrong pin";
                }
                else
                {
                    result = "success";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        public static string ValidateCoupon(Int64 MemberId, string CouponCode, int PaymentMode, ref AddCouponsScratched resGetCouponsScratched, int ServiceId = 0)
        {
            string msg = string.Empty;
            try
            {
                if (MemberId == 0)
                {
                    msg = "Please enter MemberId";
                    return msg;
                }
                else if (string.IsNullOrEmpty(CouponCode))
                {
                    msg = "Please enter Coupon";
                    return msg;
                }
                else if (ServiceId == 0)
                {
                    msg = "Please enter ServiceId";
                    return msg;
                }
                else if (!string.IsNullOrEmpty(CouponCode))
                {
                    AddCouponsScratched ObjCoupon = new AddCouponsScratched();
                    GetCouponsScratched inobjectCoupon = new GetCouponsScratched();
                    inobjectCoupon.MemberId = MemberId;
                    inobjectCoupon.CouponCode = CouponCode;
                    inobjectCoupon.CheckActive = 1;
                    inobjectCoupon.CheckDelete = 0;
                    inobjectCoupon.IsUsed = 0;
                    inobjectCoupon.IsExpired = 0;
                    inobjectCoupon.IsScratched = 1;
                    inobjectCoupon.ServiceId = ServiceId;
                    inobjectCoupon.CouponType = (int)AddCoupons.CouponTypeEnum.Coupon;
                    resGetCouponsScratched = RepCRUD<GetCouponsScratched, AddCouponsScratched>.GetRecord(Common.StoreProcedures.sp_CouponsScratched_Get, inobjectCoupon, ObjCoupon);
                    if (resGetCouponsScratched != null && resGetCouponsScratched.Id != 0)
                    {
                        msg = "success";
                    }
                    else
                    {
                        AddDealsandOffers ObjDealsOffers = new AddDealsandOffers();
                        GetDealsandOffers inobjectDealsOffers = new GetDealsandOffers();
                        inobjectDealsOffers.PromoCode = CouponCode;
                        inobjectDealsOffers.CheckActive = 1;
                        inobjectDealsOffers.CheckDelete = 0;
                        inobjectDealsOffers.ServiceId = ServiceId;
                        inobjectDealsOffers.Running = "Running";
                        AddDealsandOffers resGetDealsOfferssScratched = RepCRUD<GetDealsandOffers, AddDealsandOffers>.GetRecord(Common.StoreProcedures.sp_DealsAndOffers_Get, inobjectDealsOffers, ObjDealsOffers);
                        if (resGetDealsOfferssScratched != null && resGetDealsOfferssScratched.Id != 0)
                        {
                            WalletTransactions objChkDealOffers = new WalletTransactions();
                            objChkDealOffers.Type = ServiceId;
                            objChkDealOffers.MemberId = MemberId;
                            objChkDealOffers.CouponCode = CouponCode;
                            if (resGetDealsOfferssScratched.IsOneTime == 1 && !objChkDealOffers.GetRecord())
                            {
                                resGetCouponsScratched.CouponPercentage = resGetDealsOfferssScratched.CouponPercentage;
                                resGetCouponsScratched.CouponCode = resGetDealsOfferssScratched.PromoCode;
                                resGetCouponsScratched.MinimumAmount = resGetDealsOfferssScratched.MinimumAmount;
                                resGetCouponsScratched.MaximumAmount = resGetDealsOfferssScratched.MaximumAmount;
                                resGetCouponsScratched.ServiceId = resGetDealsOfferssScratched.ServiceId;
                                resGetCouponsScratched.Id = resGetDealsOfferssScratched.Id;
                            }
                            msg = "success";
                        }
                        else
                        {
                            msg = "Invalid Coupon";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public static string SendAsyncMail(string email, string subject, string body, string path = "", string BCC = "", string BCC2 = "")
        {
            Task.Factory.StartNew(() => MyPay.Models.Common.Common.SendMail(email, subject, body, path, BCC, BCC2));
            return "done";

        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        public static bool IsSerialNumberOrder(string str)
        {
            bool check = false;
            if (str == "1234")
            {
                check = true;
            }
            else if (str == "2345")
            {
                check = true;
            }
            else if (str == "3456")
            {
                check = true;
            }
            else if (str == "4567")
            {
                check = true;
            }
            else if (str == "5678")
            {
                check = true;
            }
            else if (str == "6789")
            {
                check = true;
            }
            return check;
        }
        public static bool allCharactersSame(string s)
        {
            int n = s.Length;
            for (int i = 1; i < n; i++)
                if (s[i] != s[0])
                    return false;

            return true;
        }

        //public static void renameFiles(String path)
        //{
        //    string[] files = System.IO.Directory.GetFiles(path);
        //    foreach (string s in files)
        //    {
        //        string[] ab = s.Split('_');
        //        //if (ab.Length > 3)
        //        {
        //            string newName = s.Replace(" ", "_");
        //            System.IO.File.Move(s,  newName);
        //        }
        //    }
        //}
        public static Hashtable CreateHasTable<T>(T myObject)
        {
            Hashtable result = new Hashtable();
            Type myType = myObject.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(myObject, null);
                result.Add(prop.Name, propValue);
            }
            return result;
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string calcHmac(string data)
        {
            byte[] key = Encoding.ASCII.GetBytes(NICSecret);
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            string result = myhmacsha256.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
            //Console.WriteLine(result);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(result);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string GenerateHashNIC(string message)
        {

            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(NICSecret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        public static string GenerateHashVISACardNIC(string message)
        {

            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(VISA_CardPaymentNICSecret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        public static DateTime GetDateByArea(DateTime inputdate, string inputString)
        {
            DateTime result = new DateTime();
            try
            {
                result = TimeZoneInfo.ConvertTimeFromUtc(inputdate, TimeZoneInfo.FindSystemTimeZoneById(inputString));
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public static string GenerateConnectIPSToken2(string stringToHash)
        {
            try
            {

                X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile_LinkBank), "123", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;

                RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
                privateKey1.ImportParameters(privateKey.ExportParameters(true));
                byte[] data = Encoding.UTF8.GetBytes(stringToHash);
                byte[] signature = privateKey1.SignData(data, "SHA256");
                string signaturresult = Convert.ToBase64String(signature);
                return signaturresult;

            }
            catch (Exception ex)
            {
                Common.AddLogs(ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName);
                return string.Empty;
            }
        }
        public static string GenerateConnectIPSToken(string stringToHash)
        {
            try
            {

                //X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile), RepNCHL.PFXPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                X509Certificate2 privateCert = null;
                if (!Common.ApplicationEnvironment.IsProduction)
                {
                    privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile_staging), RepNCHL.PFXPassword_staging, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                }
                else
                {
                    privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile), RepNCHL.PFXPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                }


                RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;

                RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
                privateKey1.ImportParameters(privateKey.ExportParameters(true));
                byte[] data = Encoding.UTF8.GetBytes(stringToHash);
                byte[] signature = privateKey1.SignData(data, "SHA256");
                //privateKey1.Decrypt()
                string signaturresult = Convert.ToBase64String(signature);
                return signaturresult;

                //X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath("/Certificates/MYPAY.pfx"), "mypay134679@iii", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                //X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile), RepNCHL.PFXPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                //X509Certificate2 privateCert = null;

                //if (!Common.ApplicationEnvironment.IsProduction)
                //{
                //    privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile_staging), RepNCHL.PFXPassword_staging, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                //}
                //else
                //{
                //    privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile), RepNCHL.PFXPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                //}

                ////NPI.cer
                ////RSACryptoServiceProvider privateKey =  (RSACryptoServiceProvider)privateCert.PrivateKey;
                //RSACryptoServiceProvider privateKey = null;               

                //RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
                //privateKey1.ImportParameters(privateKey.ExportParameters(true));
                //byte[] data = Encoding.UTF8.GetBytes(stringToHash);
                ////byte[] signature = privateKey1.SignData(data, "SHA256");
                //byte[] signature = privateKey1.SignData(data,0,data.Length, nameof(HashAlgorithmName.SHA256));
                //string signaturresult = Convert.ToBase64String(signature);
                //return signaturresult;

                //string Filename = HttpContext.Current.Server.MapPath("/Certificates/CREDITOR.pfx");
                //string certPath = Path.Combine(Environment.CurrentDirectory, "Certificates", HttpContext.Current.Server.MapPath("/Certificates/CREDITOR.pfx"));
                //using (var crypt = new SHA256Managed())
                //using (var cert = new X509Certificate2(certPath, "123", X509KeyStorageFlags.MachineKeySet
                //             | X509KeyStorageFlags.PersistKeySet
                //             | X509KeyStorageFlags.Exportable))
                //string Filename = HttpContext.Current.Server.MapPath("/Certificates/MYPAY.pfx");
                //string certPath = Path.Combine(Environment.CurrentDirectory, "Certificates", HttpContext.Current.Server.MapPath("/Certificates/MYPAY.pfx"));
                //using (var crypt = new SHA256Managed())
                //using (var cert = new X509Certificate2(certPath, "mypay134679@iii", X509KeyStorageFlags.MachineKeySet
                //             | X509KeyStorageFlags.PersistKeySet
                //             | X509KeyStorageFlags.Exportable))
                //{
                //    byte[] data = Encoding.UTF8.GetBytes(stringToHash);

                //    RSA csp = null;
                //    if (cert != null)
                //    {
                //        csp = cert.PrivateKey as RSA;
                //    }

                //    if (csp == null)
                //    {
                //        throw new Exception("No valid cert was found");
                //    }

                //    csp.ImportParameters(csp.ExportParameters(true));
                //    byte[] signatureByte = csp.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                //    string tokenStringForReference = Convert.ToBase64String(signatureByte);
                //    return Convert.ToBase64String(signatureByte);
                //}
            }
            catch (Exception ex)
            {
                Common.AddLogs(ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName);
                return string.Empty;
            }
        }

        public static string GenerateConnectIPSToken_LinkBank(string stringToHash)
        {
            try
            {
                X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile_LinkBank), RepNCHL.PFXPassword_LinkBank, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;

                RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
                privateKey1.ImportParameters(privateKey.ExportParameters(true));
                byte[] data = Encoding.UTF8.GetBytes(stringToHash);
                byte[] signature = privateKey1.SignData(data, "SHA256");
                string signaturresult = Convert.ToBase64String(signature);
                Common.AddLogs($"Signature Created From {RepNCHL.PFXFile_LinkBank} Response : {signaturresult}", false, (int)AddLog.LogType.DBLogs);

                return signaturresult;
                //string Filename = HttpContext.Current.Server.MapPath("/Certificates/CREDITOR.pfx");
                //string certPath = Path.Combine(Environment.CurrentDirectory, "Certificates", HttpContext.Current.Server.MapPath("/Certificates/CREDITOR.pfx"));
                //using (var crypt = new SHA256Managed())
                //using (var cert = new X509Certificate2(certPath, "123", X509KeyStorageFlags.MachineKeySet
                //             | X509KeyStorageFlags.PersistKeySet
                //             | X509KeyStorageFlags.Exportable))
                //string Filename = HttpContext.Current.Server.MapPath("/Certificates/MYPAY.pfx");
                //string certPath = Path.Combine(Environment.CurrentDirectory, "Certificates", HttpContext.Current.Server.MapPath("/Certificates/MYPAY.pfx"));
                //using (var crypt = new SHA256Managed())
                //using (var cert = new X509Certificate2(certPath, "mypay134679@iii", X509KeyStorageFlags.MachineKeySet
                //             | X509KeyStorageFlags.PersistKeySet
                //             | X509KeyStorageFlags.Exportable))
                //{
                //    byte[] data = Encoding.UTF8.GetBytes(stringToHash);

                //    RSA csp = null;
                //    if (cert != null)
                //    {
                //        csp = cert.PrivateKey as RSA;
                //    }

                //    if (csp == null)
                //    {
                //        throw new Exception("No valid cert was found");
                //    }

                //    csp.ImportParameters(csp.ExportParameters(true));
                //    byte[] signatureByte = csp.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                //    string tokenStringForReference = Convert.ToBase64String(signatureByte);
                //    return Convert.ToBase64String(signatureByte);
                //}
            }
            catch (Exception ex)
            {
                Common.AddLogs(ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName);
                return string.Empty;
            }
        }

        public static bool VerifyConnectIPSToken_LinkBank(string stringToHash, string signedMessage)
        {
            bool success = false;
            try
            {
                // Get server certificate.
                string serverCertPath = string.Empty;
                if (Common.ApplicationEnvironment.IsProduction)
                {
                    serverCertPath = HttpContext.Current.Server.MapPath("/Certificates/NEPALPAY.cer");
                }
                else
                {
                    serverCertPath = HttpContext.Current.Server.MapPath("/Certificates/NPI.cer");
                    //serverCertPath = HttpContext.Current.Server.MapPath("/Certificates/NPI.pfx");
                }
                var serverCert = new X509Certificate2(serverCertPath);
                using (var publicKey = serverCert.GetRSAPublicKey())
                {
                    var dataByteArray = Encoding.UTF8.GetBytes(stringToHash);
                    var signatureByteArray = Convert.FromBase64String(signedMessage);
                    success = publicKey.VerifyData(
                        data: dataByteArray,
                        signature: signatureByteArray,
                        hashAlgorithm: HashAlgorithmName.SHA256,
                        padding: RSASignaturePadding.Pkcs1);
                }
                return success;

                //X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(RepNCHL.PFXFile_LinkBank), RepNCHL.PFXPassword_LinkBank, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                //RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;
                //using (var rsa = new RSACryptoServiceProvider())
                //{
                //    var encoder = new UTF8Encoding();
                //    byte[] bytesToVerify = encoder.GetBytes(stringToHash);
                //    byte[] signedBytes = Convert.FromBase64String(signedMessage);
                //    try
                //    {
                //        rsa.ImportParameters(privateKey.ExportParameters(true));

                //        SHA256Managed Hash = new SHA256Managed();

                //        byte[] hashedData = Hash.ComputeHash(signedBytes);

                //        success = rsa.VerifyData(bytesToVerify, CryptoConfig.MapNameToOID("SHA256"), signedBytes);
                //    }
                //    catch (CryptographicException e)
                //    {
                //        Console.WriteLine(e.Message);
                //    }
                //    finally
                //    {
                //        rsa.PersistKeyInCsp = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Common.AddLogs(ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName);

            }
            return success;
        }

        public static string GenerateConnectIPSWithdrawToken(string stringToHash)
        {
            try
            {
                //    string Filename = HttpContext.Current.Server.MapPath("/Certificates/NPI.pfx");
                //    string certPath = Path.Combine(Environment.CurrentDirectory, "Certificates", HttpContext.Current.Server.MapPath("/Certificates/NPI.pfx"));
                //    using (var crypt = new SHA256Managed())
                //    using (var cert = new X509Certificate2(certPath, "123", X509KeyStorageFlags.MachineKeySet
                //                 | X509KeyStorageFlags.PersistKeySet
                //                 | X509KeyStorageFlags.Exportable))

                //string Filename = HttpContext.Current.Server.MapPath("/Certificates/MYPAY.pfx");
                string Filename = HttpContext.Current.Server.MapPath(RepNCHL.PFXFile);

                //string certPath = Path.Combine(Environment.CurrentDirectory, "Certificates", HttpContext.Current.Server.MapPath("/Certificates/MYPAY.pfx"));
                string certPath = Path.Combine(Environment.CurrentDirectory, "Certificates", HttpContext.Current.Server.MapPath(RepNCHL.PFXFile));
                using (var crypt = new SHA256Managed())
                //using (var cert = new X509Certificate2(certPath, "mypay@123", X509KeyStorageFlags.MachineKeySet
                //             | X509KeyStorageFlags.PersistKeySet
                //             | X509KeyStorageFlags.Exportable))
                using (var cert = new X509Certificate2(certPath, RepNCHL.PFXPassword, X509KeyStorageFlags.MachineKeySet
                            | X509KeyStorageFlags.PersistKeySet
                            | X509KeyStorageFlags.Exportable))
                {
                    byte[] data = Encoding.UTF8.GetBytes(stringToHash);

                    RSA csp = null;
                    if (cert != null)
                    {
                        csp = cert.PrivateKey as RSA;
                    }

                    if (csp == null)
                    {
                        throw new Exception("No valid cert was found");
                    }

                    csp.ImportParameters(csp.ExportParameters(true));
                    byte[] signatureByte = csp.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    string tokenStringForReference = Convert.ToBase64String(signatureByte);
                    return Convert.ToBase64String(signatureByte);
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs(ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName);
                return string.Empty;
            }
        }

        public static string createSha256(string sign)
        {
            X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath("/CREDITOR.pfx"), "123", X509KeyStorageFlags.Exportable);

            // This instance can not sign and verify with SHA256:
            RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;

            // This one can:
            RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
            privateKey1.ImportParameters(privateKey.ExportParameters(true));

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(sign));
                byte[] signature = privateKey1.SignData(bytes, "SHA256");
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();

                bool isValid = privateKey1.VerifyData(bytes, "SHA256", signature);
                if (isValid)
                {
                    return Convert.ToBase64String(signature);
                }
                else
                {
                    return "";
                }

            }



        }

        //private static string podpisz(X509Certificate2 cert, string toSign)
        //{
        //    string output = "";

        //    try
        //    {
        //        RSACryptoServiceProvider csp = null;
        //        csp = (RSACryptoServiceProvider)cert.PrivateKey;

        //        // Hash the data
        //        SHA256Managed sha256 = new SHA256Managed();
        //        UnicodeEncoding encoding = new UnicodeEncoding();
        //        byte[] data = Encoding.Default.GetBytes(toSign);
        //        byte[] hash = sha256.ComputeHash(data);

        //        // Sign the hash
        //        byte[] wynBin = csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));
        //        output = Convert.ToBase64String(wynBin);

        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return output;
        //}

        public static string GetCreatedByName(string authenticationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(authenticationToken);
            return jwtSecurityToken.Payload["username"].ToString();
        }

        public static Int64 GetCreatedById(string authenticationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(authenticationToken);
            return Convert.ToInt64(jwtSecurityToken.Payload["memberid"].ToString());
        }
        public static string GetBasePath(string FolderName)
        {
            string basePath = Path.Combine(HttpContext.Current.Server.MapPath($"~/{FolderName}"));
            return basePath;
        }
        //public static string GetSqlConnection(string connectionStringName = "DefaultConnection")
        //{
        //    // optionally defaults to "DefaultConnection" if no connection string name is inputted
        //    string connectionString =ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        //    // decrypt password
        //    string password = get_prase_after_word(connectionString, "password=", ";");
        //    connectionString = connectionString.Replace(password, DecryptString(password));
        //    return connectionString;
        //}

        public static string get_prase_after_word(string search_string_in, string word_before_in, string word_after_in)
        {
            int myStartPos = 0;
            string myWorkString = "";

            // get position where phrase "word_before_in" ends

            if (!string.IsNullOrEmpty(word_before_in))
            {
                myStartPos = search_string_in.ToLower().IndexOf(word_before_in) + word_before_in.Length;

                // extract remaining text
                myWorkString = search_string_in.Substring(myStartPos, search_string_in.Length - myStartPos).Trim();

                if (!string.IsNullOrEmpty(word_after_in))
                {
                    // get position where phrase starts in the working string
                    myWorkString = myWorkString.Substring(0, myWorkString.IndexOf(word_after_in)).Trim();

                }
            }
            else
            {
                myWorkString = string.Empty;
            }
            return myWorkString.Trim();
        }


        //public static string CheckApiToken(string hash, Int64 timestamp, string md5hash, string platform, string version, string devicecode, string secretkey)
        //{
        //    string result = String.Empty;
        //    try
        //    {
        //        //
        //        //System.Threading.Thread.Sleep(1000);
        //        //return "MyPay will be performing System Maintenence from Sunday, Jun 5, 2022 4:00 PM to next 3Hrs. During this time you will not be able to access your account through App. Thank you for your patience as we make system improvements. ";

        //        var userAgent = HttpContext.Current.Request.UserAgent.ToLower();

        //        string json = File.ReadAllText((HttpContext.Current.Server.MapPath("~/apisettings.json"))) ;
        //        ApiSetting objApiSettings = new ApiSetting();
        //        objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiSetting>(json);
        //        //using (MyPayEntities db = new MyPayEntities())
        //        //{
        //        //    objApiSettings = db.ApiSettings.FirstOrDefault();
        //        //}

        //        Common.IosVersion = objApiSettings.IOSVersion;
        //        Common.AndroidVersion = objApiSettings.AndroidVersion;

        //        DateTime date = new DateTime(1970, 01, 01).AddMilliseconds(timestamp);
        //        double diffInSeconds = (DateTime.UtcNow - date).TotalSeconds;

        //        //if (objApiSettings != null && !string.IsNullOrEmpty(objApiSettings.SchedulerMessage) && (objApiSettings.ScheduleStatus) && ((objApiSettings.ScheduleStartTime < Convert.ToDateTime(Common.fnGetdatetimeFromInput(System.DateTime.UtcNow)) && objApiSettings.ScheduleEndTime > Convert.ToDateTime(Common.fnGetdatetimeFromInput(System.DateTime.UtcNow)))))
        //        //{
        //        //    result = objApiSettings.SchedulerMessage;

        //        //}
        //        //else
        //        if (string.IsNullOrEmpty(version))
        //        {
        //            result = "Please Enter Version";
        //        }
        //        else if (string.IsNullOrEmpty(result)  && string.IsNullOrEmpty(devicecode))
        //        {
        //            result = "Please Enter Device Code";
        //        }
        //        else if (!userAgent.Contains("android") && !userAgent.Contains("mypay"))
        //        {
        //            result = "Invalid Request";
        //        }
        //        else if (Common.SecretKey != Common.DecryptString(secretkey))
        //        {
        //            result = "Invalid Key";
        //        }
        //        //else if (string.IsNullOrEmpty(result) && (objApiSettings == null || objApiSettings.CheckAndroidVersion == 1) && version != AndroidVersion)
        //        //{
        //        //    result = "Please update the application";
        //        //}
        //        //else if (platform != "ios" && string.IsNullOrEmpty(result) && (timestamp == null || timestamp == 0 || Convert.ToInt64(diffInSeconds) > DiffSeconds))
        //        //{
        //        //    result = "Server communication timeout, Please try again.";
        //        //}
        //        else if (string.IsNullOrEmpty(result)  && (hash.Trim() != md5hash.Trim()))
        //        {
        //            result = "Hash Not Matched";
        //        }
        //        else if (string.IsNullOrEmpty(result)  && string.IsNullOrEmpty(platform))
        //        {
        //            result = "Please Enter Platform";
        //        }
        //        else
        //        {
        //            result = "Success";
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //    }
        //    return result;
        //}

        public static bool AssignCoupons(Int64 MemberId, string TransactionId, int ApplyType = (int)AddCoupons.CouponReceivedBy.Transaction)
        {
            Int64 Id = 0;
            bool DataRecieved = false;
            try
            {
                string UniqueTransactionIdCoupons = DateTime.UtcNow.ToString("ddMMyyyyhhmmssms") + Common.RandomNumber(111, 999).ToString();
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();

                Hashtable HT = new Hashtable();
                HT.Add("MemberId", MemberId);
                HT.Add("TransactionUniqueId", TransactionId);
                HT.Add("UniqueTransactionId", UniqueTransactionIdCoupons);
                HT.Add("ApplyType", ApplyType);
                string ResultId = "0";
                ResultId = obj.ExecuteProcedureGetReturnValue("sp_AssignCoupons_Get", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public static string EncryptString(string inputString)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] key = {

            };
                byte[] IV = {
                12,
                21,
                43,
                17,
                57,
                35,
                67,
                27
            };
                string encryptKey = "aHV2ty4z";
                key = Encoding.UTF8.GetBytes(encryptKey);
                byte[] byteInput = Encoding.UTF8.GetBytes(inputString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                memStream = new MemoryStream();
                ICryptoTransform transform = provider.CreateEncryptor(key, IV);
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();

            }
            catch (Exception ex)
            {
            }

            return Convert.ToBase64String(memStream.ToArray());
        }

        public static string DecryptString(string inputString)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] key = {
                      
            };
                byte[] IV = {
                12,
                21,
                43,
                17,
                57,
                35,
                67,
                27
            };
                string encryptKey = "aHV2ty4z";

                key = Encoding.UTF8.GetBytes(encryptKey);
                byte[] byteInput = new byte[inputString.Length];
                byteInput = Convert.FromBase64String(inputString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                memStream = new MemoryStream();
                ICryptoTransform transform = provider.CreateDecryptor(key, IV);
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();

            }
            catch (Exception ex)
            {
                return "";
            }

            Encoding encoding1 = Encoding.UTF8;
            return encoding1.GetString(memStream.ToArray());
        }
        public static string Encryption(string plainText)
        {
            string key = "ZZ9489Slw8OslwoS";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                aes.Mode = CipherMode.ECB;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array, 0, array.Length);
        }
        public static string Decryption(string cipherText)
        {
            try
            {
                string key = "ZZ9489Slw8OslwoS";
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    aes.Mode = CipherMode.ECB;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "error";
            }
        }
        public static string base64Encode(string sData)
        {
            try
            {
                byte[] encData_byte = new byte[(sData.Length)];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string base64Decode(string sData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            try
            {
                byte[] todecode_byte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[(charCount)];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new string(decoded_char);
                return result;
            }
            catch (Exception err)
            {
                return "";
            }
        }

        public static string EncryptionFromKey(string plainText, string key)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                aes.Mode = CipherMode.ECB;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array, 0, array.Length);
        }
        public static string DecryptionFromKey(string cipherText, string key)
        {
            try
            {
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    aes.Mode = CipherMode.ECB;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "error";
            }
        }
        public static string GetMerchantUserIP()
        {
            string VisitorsIPAddr = string.Empty;
            try
            {

                if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    //Common.AddLogs("VisitorsIPAddr: HTTP_X_FORWARDED_FOR : " + VisitorsIPAddr, true, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false, "", "", 0, Common.CreatedBy, Common.CreatedByName);
                }
                else if (HttpContext.Current != null && HttpContext.Current.Request.UserHostAddress.Length != 0)
                {
                    VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
                    if (VisitorsIPAddr == "::1")
                    {
                        if (HttpContext.Current.Request.Url.Host != "localhost")
                        {
                            VisitorsIPAddr = new WebClient().DownloadString("http://icanhazip.com");
                            //Common.AddLogs("VisitorsIPAddr: icanhazip : " + VisitorsIPAddr, true, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false, "", "", 0, Common.CreatedBy, Common.CreatedByName);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
            }

            return VisitorsIPAddr;
        }

        public static string GetMerchantUserInvalidIP()
        {
            string VisitorsIPAddr = string.Empty;
            try
            {

                {
                    VisitorsIPAddr = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    // VisitorsIPAddr = new WebClient().DownloadString("http://icanhazip.com");
                    //Common.AddLogs("VisitorsIPAddr: icanhazip : " + VisitorsIPAddr, true, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false, "", "", 0, Common.CreatedBy, Common.CreatedByName);
                }

            }
            catch (Exception ex)
            {
                //Common.AddLogs("VisitorsIPAddr: icanhazip : " + ex.Message, true, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false, "", "", 0, Common.CreatedBy, Common.CreatedByName);

            }

            return VisitorsIPAddr;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static bool GetMerchantIPValidate(string MerchantUniqueId)
        {
            bool VisitorsIPFlag = false;
            try
            {
                string MerchantUserIP = Common.GetMerchantUserIP().Trim();
                string MerchantUserIPv2 = Common.GetMerchantUserInvalidIP().Trim();
                int Count = Convert.ToInt32((new CommonHelpers()).GetScalarValueWithValue($"select  count(0) from [MerchantIPAddress] where MerchantUniqueId = '{MerchantUniqueId}' and (IPAddress = '{MerchantUserIP}' or IPAddress = '{MerchantUserIPv2}' ) and IsDeleted=0 and IsActive = 1"));
                if (Common.ApplicationEnvironment.IsProduction == false)
                {
                    VisitorsIPFlag = true;
                }
                else if (Count > 0)
                {
                    VisitorsIPFlag = true;
                }
                else
                {
                    VisitorsIPFlag = false;
                }
            }
            catch (Exception ex)
            {
            }

            return VisitorsIPFlag;
        }

        public static bool GetRemittanceIPValidate(string MerchantUniqueId)
        {
            bool VisitorsIPFlag = false;
            try
            {
                string MerchantUserIP = Common.GetMerchantUserIP().Trim();
                string MerchantUserIPv2 = Common.GetMerchantUserInvalidIP().Trim();
                int Count = Convert.ToInt32((new CommonHelpers()).GetScalarValueWithValue($"select  count(0) from [RemittanceUserIPAddress] where RemittanceUniqueId = '{MerchantUniqueId}' and (IPAddress = '{MerchantUserIP}' or IPAddress = '{MerchantUserIPv2}' ) and IsDeleted=0 and IsActive = 1"));
                if (Common.ApplicationEnvironment.IsProduction == false)
                {
                    VisitorsIPFlag = true;
                }
                else if (Count > 0)
                {
                    VisitorsIPFlag = true;
                }
                else
                {
                    VisitorsIPFlag = false;
                }
            }
            catch (Exception ex)
            {
            }

            return VisitorsIPFlag;
        }


        public static string GetMerchantWithdrawalRequestDailyCheckLimit(string MerchantUniqueId)
        {
            string ReturnMessage = string.Empty;
            try
            {
                ReturnMessage = Convert.ToString((new CommonHelpers()).GetScalarValueWithValue($"Exec sp_MerchantWithdrawalRequest_DailyLimitCheck_Get '{MerchantUniqueId}' "));

            }
            catch (Exception ex)
            {
            }

            return ReturnMessage;
        }
        public static string GetUserIP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current != null && HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
                if (VisitorsIPAddr == "::1")
                {
                    if (HttpContext.Current.Request.Url.Host != "localhost")
                        VisitorsIPAddr = new WebClient().DownloadString("http://icanhazip.com");
                }
            }
            VisitorsIPAddr = VisitorsIPAddr.Replace("\n", "");
            return VisitorsIPAddr;
        }
        public static string GetServerIPAddress()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");

            request.UserAgent = "curl"; // this will tell the server to return the information as if the request was made by the linux "curl" command

            string publicIPAddress;

            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    publicIPAddress = reader.ReadToEnd();
                }
            }

            return publicIPAddress.Replace("\n", "");
        }

        // Instantiate random number generator.  
        private static readonly Random _random = new Random();

        // Generates a random number within a range.      
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static string SendSMS(string to, string text)
        {
            if (!Common.ApplicationEnvironment.IsProduction)
            {
                return "";
            }
            string msg = string.Empty;
            try
            {

                if ((!string.IsNullOrEmpty(to)) && (!string.IsNullOrEmpty(text)))
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["auth_token"] = SendSMSToken;
                        values["to"] = to;
                        values["text"] = text;
                        var response = client.UploadValues("https://sms.aakashsms.com/sms/v3/send/", "Post", values);
                        var responseString = Encoding.Default.GetString(response);
                        msg = responseString;
                        string DeviceCode = String.Empty;// System.Web.HttpContext.Current.Request.Browser.Type;
                        string PlatForm = "Web";
                        Int64 CreatedBy = Common.CreatedBy;
                        string CreatedByName = Common.CreatedByName;
                        //Int64 MemberId = 0;
                        //string MemberName = string.Empty;
                        //AddUser outobjectUser = new AddUser();
                        //GetUser inobjectUser = new GetUser();
                        //inobjectUser.ContactNumber = to;
                        //AddUser resUser = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobjectUser, outobjectUser);
                        //if (resUser != null && resUser.Id != 0)
                        //{
                        //    MemberId = resUser.MemberId;
                        //    MemberName = resUser.FirstName + " " + resUser.LastName;
                        //}
                        AddUserBasicInfo resuserdetails = new AddUserBasicInfo();
                        resuserdetails.ContactNumber = to;
                        resuserdetails.GetUserInformationBasic();
                        if (resuserdetails.Id == 0)
                        {
                            resuserdetails.MemberId = Common.CreatedBy;
                            resuserdetails.FirstName = Common.CreatedByName;
                        }
                        //Models.Common.Common.AddLogs(text + $" (SMS User : {to})", false, Convert.ToInt32(AddLog.LogType.DBLogs), resuserdetails.MemberId, resuserdetails.FirstName, true, PlatForm, DeviceCode, (int)AddLog.LogActivityEnum.SMS, CreatedBy, CreatedByName);
                    }
                }
                else
                {
                    msg = string.Empty;
                }

            }
            catch (Exception ex)
            {

            }
            return msg;
        }
        public static string fnGetdatetime()
        {
            return System.DateTime.UtcNow.AddMinutes(345).ToString("dd-MMM-yyyy hh:mm:ss tt");
        }
        public static string fnGetdatetimeFromInput(DateTime datetime)
        {
            return datetime.AddMinutes(345).ToString("dd-MMM-yyyy hh:mm:ss tt");
        }

        public static int GetNotificatUnReadCount(Int64 MemberId)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($"select count(0) from Notification  with(nolock)  where memberid = '{MemberId}' and ReadStatus = 0 and IsDeleted=0 and IsActive = 1"));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }
        public static int GetFailedTxn(AddUserLoginWithPin resGetRecord, int VendorApiType)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($"select count(Id) from wallettransactions  with(nolock) where MemberId = {@resGetRecord.MemberId} and Type = {VendorApiType} and status in (3,6,7,8) and convert(date,createddate) = convert(date, getdate())"));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }
        public static int GetLastFailedTxn(AddUserLoginWithPin resGetRecord, int VendorApiType)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($"select top 1 datediff(MINUTE,dbo.fngetdatetime(CreatedDate),getdate()) from wallettransactions where MemberId = '{@resGetRecord.MemberId}' and Type = '{VendorApiType}' and status in (3,6,7,8) and convert(date,createddate) = convert(date, getdate()) order by createddate desc "));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }
        public static int GetLastTxnStatus(AddUserLoginWithPin resGetRecord, int VendorApiType)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($"select top 1 status from wallettransactions where MemberId = '{@resGetRecord.MemberId}' and Type = '{VendorApiType}' and convert(date,createddate) = convert(date, getdate()) order by createddate desc "));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }

        public static int GetFailedTxnOld(AddUser resGetRecord, int VendorApiType)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($"select count(Id) from wallettransactions  with(nolock) where MemberId = {@resGetRecord.MemberId} and Type = {VendorApiType} and status in (3,6,7,8) and convert(date,createddate) = convert(date, getdate())"));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }
        public static int GetLastFailedTxnOld(AddUser resGetRecord, int VendorApiType)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($"select top 1 datediff(MINUTE,dbo.fngetdatetime(CreatedDate),getdate()) from wallettransactions where MemberId = '{@resGetRecord.MemberId}' and Type = '{VendorApiType}' and status in (3,6,7,8) and convert(date,createddate) = convert(date, getdate()) order by createddate desc "));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }
        public static int GetLastTxnStatusOld(AddUser resGetRecord, int VendorApiType)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($"select top 1 status from wallettransactions where MemberId = '{@resGetRecord.MemberId}' and Type = '{VendorApiType}' and convert(date,createddate) = convert(date, getdate()) order by createddate desc "));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }
        public static int GetDepulicateRequestCheck(AddUserLoginWithPin resGetRecord, int VendorApiType, string UserInput)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($"select count(Id) from Vendor_API_Requests  with(nolock) where MemberId = '{resGetRecord.MemberId}' and  CONVERT(NVARCHAR(MAX),Req_Input) = N'{UserInput}'  and  VendorApiType = '{VendorApiType}' and Req_URL not like '%accountvalidation%' and Req_URL not like '%lookup%' and Req_URL not like '%get-event%' and DATEDIFF(minute,createddate,getutcdate()) <= '{Common.ApplicationEnvironment.DuplicateMinutesCheck}' "));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }
        public static int GetDepulicateTransactionRequestCheck(AddUserLoginWithPin resGetRecord, int VendorApiType, string CustomerID, string Amount, string WalletType)
        {
            int Count = 0;
            try
            {
                MyPay.Models.Common.CommonHelpers commonHelpers = new Models.Common.CommonHelpers();
                Count = Convert.ToInt32(commonHelpers.GetScalarValueWithValue($" select count(Id) from WalletTransactions  with(nolock) where MemberId = '{resGetRecord.MemberId}' and WalletType ='{WalletType}' and CONVERT(NVARCHAR(MAX),CustomerID) = N'{CustomerID}'  and  Type = '{VendorApiType}' and  (TransactionAmount = '{Amount}' or Amount = '{Amount}') and  DATEDIFF(minute,createddate,getutcdate()) <= '{Common.ApplicationEnvironment.DuplicateMinutesCheck}' "));

            }
            catch (Exception ex)
            {

            }
            return Count;
        }
        public static decimal GetTotalTransactionsSum(Int64 MemberId)
        {
            decimal data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select sum(amount) from WalletTransactions with(nolock) where MemberId='" + MemberId.ToString() + "'  and  Sign = 2 and IsActive=1 and IsDeleted=0";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (decimal.Parse(Result));
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public static string GetKycMessage(AddUserLoginWithPin resGetRecord, decimal Amount)
        {
            //GetTransaction inobject_Trans = new GetTransaction();
            //inobject_Trans.MemberId = resGetRecord.MemberId;
            //inobject_Trans.Sign = (int)WalletTransactions.Signs.Debit;
            //List<AddTransaction> UserTransactions = (List<AddTransaction>)RepTransactions.GetAllTransactions(inobject_Trans);
            //decimal total = UserTransactions.Sum(item => Convert.ToDecimal(item.Amount));
            decimal total = GetTotalTransactionsSum(resGetRecord.MemberId);
            if ((total + Amount) < 5000)
            {
                return String.Empty;
            }
            if ((total + Amount) > 5000 && resGetRecord.IsKYCApproved == (int)AddUser.kyc.Rejected)
            {
                return "You have reached the transaction limit of amount Rs. 5,000. To proceed further please complete you KYC.";
            }
            if (resGetRecord.IsKYCApproved == (int)AddUser.kyc.Not_Filled)
            {
                return "Kindly complete your KYC";
            }
            else if (resGetRecord.IsKYCApproved == (int)AddUser.kyc.Pending)
            {
                return "We are reviewing your submitted KYC details. It may take up to 1 to 2 working days.";
            }
            else if (resGetRecord.IsKYCApproved == (int)AddUser.kyc.InComplete)
            {
                return "Kindly complete your KYC";
            }
            else if (resGetRecord.IsKYCApproved == (int)AddUser.kyc.Rejected)
            {
                return "We're having difficulties verifying your identity. The information you had submitted was unfortunately rejected. Please complete your profile and KYC again.";
            }
            else
            {
                return String.Empty;
            }
        }
        public static async void SendMail(string email, string subject, string body, string path = "", string BCC = "", string BCC2 = "")
        {
            try
            {
                body = body.Replace("##WebsiteName##", WebsiteName);
                body = body.Replace("##WebsiteEmail##", WebsiteEmail);
                body = body.Replace("##LogoImage##", LiveSiteUrl + "/Content/assets/images/logo.png");
                body = body.Replace("##AppImage##", LiveSiteUrl + "/Content/assets/images/appstore.png");
                body = body.Replace("##PlayStore##", LiveSiteUrl + "/Content/assets/images/playstore.png");
                body = body.Replace("##QRCodeImage##", LiveSiteUrl + "/Content/assets/images/qr.png");
                body = body.Replace("##FacebookImage##", LiveSiteUrl + "/Content/assets/images/fb.png");
                body = body.Replace("##TwitterImage##", LiveSiteUrl + "/Content/assets/images/twitter.png");
                body = body.Replace("##LinkedinImage##", LiveSiteUrl + "/Content/assets/images/linkdeal.png");
                body = body.Replace("##InstagramImage##", LiveSiteUrl + "/Content/assets/images/instagram.png");
                body = body.Replace("##LiveURL##", LiveSiteUrl_User);
                body = body.Replace("##FacebookLink##", FacebookLink);
                body = body.Replace("##TwitterLink##", TwitterLink);
                body = body.Replace("##LinkedinLink##", Linkedinlink);
                body = body.Replace("##InstagramLink##", InstagramLink);
                body = body.Replace("##AppLink##", AppLink);
                body = body.Replace("##PlayLink##", PlayStoreLink);
                body = body.Replace("##tel1##", tel1);
                body = body.Replace("##tel2##", tel2);
                body = body.Replace("##tel3##", tel3);
                body = body.Replace("##tel4##", tel4);


                var fromAddress = new MailAddress(FromEmail);
                var fromPassword = FromEmailPassword;
                var toAddress = new MailAddress(email);

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = EmailHost,
                    Port = Convert.ToInt32(EmailPort),
                    EnableSsl = EnableSSl,

                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };

                //AddLog(FromEmail+ FromEmailPassword, false, 0, "", true);

                MailMessage message = new MailMessage(fromAddress, toAddress);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(BCC))
                {
                    message.Bcc.Add(BCC);
                }
                if (!string.IsNullOrEmpty(BCC2))
                {
                    message.Bcc.Add(BCC2);
                }
                if (path != "")
                {
                    //message.cc.add("ceo@one11sports.com");
                    System.Net.Mail.Attachment att1 = new System.Net.Mail.Attachment(path);

                    message.Attachments.Add(att1);
                }
                smtp.Send(message);
                message.Dispose();
                string DeviceCode = String.Empty;// System.Web.HttpContext.Current.Request.Browser.Type;
                string PlatForm = "Web";
                Int64 CreatedBy = Common.CreatedBy;
                string CreatedByName = Common.CreatedByName;
                AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                inobjectUser.Email = email;
                AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                if (resUser != null && resUser.Id != 0)
                {
                    CreatedBy = resUser.MemberId;
                    CreatedByName = resUser.FirstName + " " + resUser.LastName;
                }
                Models.Common.Common.AddLogs(subject + $" (Email User: {toAddress} )", false, Convert.ToInt32(AddLog.LogType.User), CreatedBy, CreatedByName, true, PlatForm, DeviceCode, (int)AddLog.LogActivityEnum.Email);

            }
            catch (Exception ex)
            {
                //AddLog(ex.Message, true,0,"",true);

            }
        }

        public static string BankStatusCheck(string MerchantTxnId)
        {
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(MerchantTxnId))
                {

                    result = "Merchant Transaction Id Not Found";
                    return result;
                }

                //System.Threading.Thread.Sleep(10000);
                AddDepositOrders outobject = new AddDepositOrders();
                GetDepositOrders inobject = new GetDepositOrders();
                inobject.TransactionId = MerchantTxnId;
                AddDepositOrders res = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);


                if (res.Id > 0)
                {
                    AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                    GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                    inuser.MemberId = Convert.ToInt64(res.MemberId);
                    AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                    if (resuser.Id > 0)
                    {
                        AddBankingNPSStatus req = new AddBankingNPSStatus();
                        req.MerchantId = RepNps.MerchantId;
                        req.MerchantName = RepNps.MerchantName;
                        req.MerchantTxnId = inobject.TransactionId;
                        req.Signature = RepNps.HMACSHA512(req.MerchantId + req.MerchantName + req.MerchantTxnId);
                        string response = RepNps.PostMethod("CheckTransactionStatus", JsonConvert.SerializeObject(req));
                        if (!string.IsNullOrEmpty(response))
                        {
                            GetBankingNPSStatus res1 = JsonConvert.DeserializeObject<GetBankingNPSStatus>(response);
                            if (res1.data != null && !string.IsNullOrEmpty(res1.code) && !string.IsNullOrEmpty(res1.data.GatewayReferenceNo))
                            {
                                if (res1.code == "0" && res1.data.Status == "Pending")
                                {
                                    outobject.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                }
                                else
                                {
                                    outobject.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                }
                                if (res1.data != null)
                                {
                                    if (res1.data.CbsMessage != "")
                                    {
                                        outobject.Particulars = res1.data.CbsMessage;
                                    }
                                    else
                                    {
                                        outobject.Particulars = res1.data.Status + " with Response Code: " + res1.code;
                                    }
                                }
                                else if (res1.errors.Count > 0)
                                {
                                    outobject.Particulars = res1.errors[0].error_message;
                                }

                                //outobject.Particulars = res1.message;

                                outobject.JsonResponse = response;
                                outobject.ResponseCode = res1.code;
                                if (RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders"))
                                {
                                    decimal amount = 0;
                                    if (res1.data.Amount == "")
                                    {
                                        amount = res.Amount;
                                    }
                                    else
                                    {
                                        amount = Convert.ToDecimal(res1.data.Amount);
                                    }
                                    //decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) + (amount-Convert.ToDecimal(res1.data.ServiceCharge)));
                                    decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) + ((amount) - res.ServiceCharges));
                                    WalletTransactions res_transaction = new WalletTransactions();
                                    res_transaction.VendorTransactionId = res1.data.GatewayReferenceNo;
                                    if (!res_transaction.GetRecordCheckExists())
                                    {
                                        if (res1.code == "0" && res1.data.Status == "Success")
                                        {
                                            res_transaction.VendorTransactionId = res1.data.GatewayReferenceNo;
                                            // res_transaction.ServiceCharge = res.ServiceCharges + Convert.ToDecimal(res1.data.ServiceCharge);
                                            res_transaction.ServiceCharge = res.ServiceCharges;
                                            res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
                                            res_transaction.ContactNumber = resuser.ContactNumber;
                                            res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                            // res_transaction.Amount = amount - Convert.ToDecimal(res1.data.ServiceCharge);
                                            res_transaction.Amount = amount;
                                            res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
                                            res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                            res_transaction.CurrentBalance = WalletBalance;
                                            res_transaction.CreatedBy = res.CreatedBy;
                                            res_transaction.CreatedByName = res.CreatedByName;
                                            res_transaction.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                                            res_transaction.Remarks = "Wallet Credited Successfully";
                                            if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                            {
                                                res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Internet_Banking;
                                            }
                                            else
                                            {
                                                res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Mobile_Banking;
                                            }

                                            res_transaction.Description = outobject.Particulars;
                                            res_transaction.Purpose = res.Remarks;
                                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                            res_transaction.Reference = MerchantTxnId;
                                            res_transaction.IsApprovedByAdmin = true;
                                            res_transaction.IsActive = true;
                                            res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                            res_transaction.NetAmount = amount;
                                            res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                            res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NPS;
                                            res_transaction.SenderBankName = res1.data.Institution;
                                            res_transaction.SenderBankCode = res1.data.Instrument;
                                            if (res_transaction.Add())
                                            {
                                                //if (resuser.RefId != 0)
                                                //{
                                                //    Common.FirstTransactionCommisipon(resuser, res, res_transaction.TransactionUniqueId);
                                                //}
                                                outobject.Status = (int)AddDepositOrders.DepositStatus.Success;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders");

                                                if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                                {
                                                    string Title = "Internet Banking";
                                                    string Message = "Hello " + resuser.FirstName + "! Internet Banking Transaction Completed Successfully By Transaction ID :" + res_transaction.TransactionUniqueId;
                                                    Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.Internet_Banking, res.MemberId, Title, Message);
                                                    Common.AddLogs("Transaction Completed Successfully By This TXNId:" + res_transaction.TransactionUniqueId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                                }
                                                else
                                                {
                                                    string Title = "Mobile Banking";
                                                    string Message = "Hello " + resuser.FirstName + "! Mobile Banking Transaction Completed Successfully By Transaction ID :" + res_transaction.TransactionUniqueId;
                                                    Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.Mobile_Banking, res.MemberId, Title, Message);
                                                    Common.AddLogs("Transaction Completed Successfully By This TXNId:" + res_transaction.TransactionUniqueId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                                }

                                                result = "Recieved";
                                            }
                                            else
                                            {
                                                if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                                {
                                                    Common.AddLogs("Something went wrong data is not updated By This TXNId:" + res_transaction.TransactionUniqueId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                                }
                                                else
                                                {
                                                    Common.AddLogs("Something went wrong data is not updated By This TXNId:" + res_transaction.TransactionUniqueId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                                }
                                                result = "Pending";
                                            }
                                        }
                                        else
                                        {
                                            if (res1.data.CbsMessage != "")
                                            {
                                                if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                                {
                                                    Common.AddLogs(res1.data.CbsMessage + " By This TXNId:" + res_transaction.TransactionUniqueId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                                }
                                                else
                                                {
                                                    Common.AddLogs(res1.data.CbsMessage + " By This TXNId:" + res_transaction.TransactionUniqueId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                                }
                                                result = res1.data.CbsMessage;
                                            }
                                            else
                                            {
                                                if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                                {
                                                    Common.AddLogs("Transaction Failed By This TXNId:" + res_transaction.TransactionUniqueId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                                }
                                                else
                                                {
                                                    Common.AddLogs("Transaction Failed By This TXNId:" + res_transaction.TransactionUniqueId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                                }
                                                result = "Transaction Failed";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                        {
                                            Common.AddLogs("Transaction Already Updated By This TXNId:" + res_transaction.VendorTransactionId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                        }
                                        else
                                        {
                                            Common.AddLogs("Transaction Already Updated By This TXNId:" + res_transaction.VendorTransactionId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                        }

                                        result = "Already Recieved";
                                    }


                                }
                                else
                                {
                                    if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                    {
                                        Common.AddLogs("Transaction Not Updated By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                    }
                                    else
                                    {
                                        Common.AddLogs("Transaction Not Updated By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                    }

                                    result = "Transaction Not Updated";
                                }
                            }
                            else if (res1.data == null && !string.IsNullOrEmpty(res1.code) && res1.errors.Count > 0)
                            {

                                outobject.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                outobject.Particulars = res1.errors[0].error_message;
                                outobject.JsonResponse = response;
                                outobject.ResponseCode = res1.code;
                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders");
                                if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                {
                                    Common.AddLogs(res1.errors[0].error_message + " By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                }
                                else
                                {
                                    Common.AddLogs(res1.errors[0].error_message + " By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                }

                                result = res1.errors[0].error_message;
                            }
                            else
                            {
                                if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                                {
                                    Common.AddLogs("Something went wrong try again later By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                }
                                else
                                {
                                    Common.AddLogs("Something went wrong try again later By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                }

                                result = "Something went wrong try again later";
                            }
                        }
                        else
                        {
                            if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                            {
                                Common.AddLogs("Response Not Found By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                            }
                            else
                            {
                                Common.AddLogs("Response Not Found By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                            }

                            result = "Response Not Found";
                        }
                    }
                    else
                    {
                        if (res.Type == (int)AddDepositOrders.DepositType.Internet_Banking)
                        {
                            Common.AddLogs("User Not Found By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Internet_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                        }
                        else
                        {
                            Common.AddLogs("User Not Found By This TXNId:" + MerchantTxnId, false, Convert.ToInt32(AddLog.LogType.Mobile_Banking), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                        }

                        result = "User Not Found";
                    }
                }
                else
                {

                    result = "Transaction not found";

                }


            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static string ConnectIPSSuccess(string TXNID)
        {
            string result = "";
            try
            {
                AddDepositOrders outobject = new AddDepositOrders();
                GetDepositOrders inobject = new GetDepositOrders();
                inobject.TransactionId = TXNID;
                AddDepositOrders res = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);


                if (res.Id > 0)
                {
                    AddUserLoginWithPin outuser = new AddUserLoginWithPin();
                    GetUserLoginWithPin inuser = new GetUserLoginWithPin();
                    inuser.MemberId = Convert.ToInt64(res.MemberId);
                    AddUserLoginWithPin resuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), inuser, outuser);
                    if (resuser.Id > 0)
                    {
                        Req_ConnectPaymentValidation req = new Req_ConnectPaymentValidation();
                        req.merchantId = RepNCHL.MERCHANTID;
                        req.appId = RepNCHL.APPID;
                        req.referenceId = TXNID;
                        req.txnAmt = (Convert.ToInt64(res.Amount + res.ServiceCharges) * 100).ToString();
                        req.token = Common.GenerateConnectIPSToken("MERCHANTID=" + req.merchantId + ",APPID=" + req.appId + ",REFERENCEID=" + req.referenceId + ",TXNAMT=" + req.txnAmt);
                        string response = RepNCHL.PostMethod("connectipswebws/api/creditor/validatetxn", JsonConvert.SerializeObject(req));
                        if (!string.IsNullOrEmpty(response))
                        {
                            Res_ConnectPaymentValidation res1 = JsonConvert.DeserializeObject<Res_ConnectPaymentValidation>(response);
                            if (!string.IsNullOrEmpty(res1.referenceId))
                            {
                                if (res1.status == "FAILED" || res1.status == "ERROR")
                                {
                                    outobject.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                }
                                else if (res1.status == "CANCELLED")
                                {
                                    outobject.Status = (int)AddDepositOrders.DepositStatus.Cancelled;
                                }
                                else
                                {
                                    outobject.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                }
                                outobject.Particulars = res1.statusDesc;
                                outobject.JsonResponse = response;
                                outobject.ResponseCode = res1.status;
                                if (RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders"))
                                {
                                    decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) + ((Convert.ToDecimal(res1.txnAmt) - (res.ServiceCharges * 100)) / 100));
                                    WalletTransactions res_transaction = new WalletTransactions();
                                    res_transaction.VendorTransactionId = res1.referenceId;
                                    if (!res_transaction.GetRecordCheckExists())
                                    {
                                        if (res1.status == "SUCCESS")
                                        {
                                            res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
                                            res_transaction.ContactNumber = resuser.ContactNumber;
                                            res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                            res_transaction.Amount = (Convert.ToDecimal(res1.txnAmt) - (res.ServiceCharges * 100)) / 100;
                                            res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
                                            res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                                            res_transaction.CurrentBalance = WalletBalance;
                                            res_transaction.CreatedBy = res.CreatedBy;
                                            res_transaction.CreatedByName = res.CreatedByName;
                                            res_transaction.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                                            res_transaction.Remarks = "Wallet Credited Successfully";
                                            res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_connectips;
                                            res_transaction.Description = outobject.Particulars;
                                            res_transaction.Purpose = res.Remarks;
                                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                            res_transaction.Reference = res1.referenceId;
                                            res_transaction.IsApprovedByAdmin = true;
                                            res_transaction.IsActive = true;
                                            res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                            res_transaction.GatewayStatus = res1.status;
                                            res_transaction.ServiceCharge = res.ServiceCharges;
                                            res_transaction.NetAmount = res_transaction.Amount + res_transaction.ServiceCharge;
                                            res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                            res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NCHL;
                                            if (res_transaction.Add())
                                            {
                                                //if (resuser.RefId != 0)
                                                //{
                                                //    Common.FirstTransactionCommisipon(resuser, res, res_transaction.TransactionUniqueId);
                                                //}

                                                outobject.Status = (int)AddDepositOrders.DepositStatus.Success;
                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(outobject, "depositorders");
                                                string Title = "Connect IPS";
                                                string Message = "Hello " + resuser.FirstName + "! Connect IPS Transaction Completed Successfully By Transaction ID :" + TXNID;
                                                Models.Common.Common.SendNotification("", (int)VendorApi_CommonHelper.KhaltiAPIName.deposit_by_connectips, res.MemberId, Title, Message);
                                                Common.AddLogs("Transaction Completed Successfully By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);
                                                result = "Transaction Completed Successfully";
                                            }
                                            else
                                            {
                                                Common.AddLogs("Something went wrong data is not updated By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                                result = "Something went wrong data is not updated";
                                            }
                                        }
                                        else
                                        {
                                            Common.AddLogs(res1.statusDesc + " By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                            result = res1.statusDesc;
                                        }
                                    }
                                    else
                                    {
                                        Common.AddLogs("Transaction Already Updated By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                        result = "Transaction Already Updated";
                                    }


                                }
                                else
                                {
                                    Common.AddLogs("Transaction Not Updated By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                    result = "Transaction Not Updated";
                                }
                            }
                            else
                            {
                                Common.AddLogs("Something went wrong try again later By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                                result = "Something went wrong try again later";
                            }
                        }
                        else
                        {
                            Common.AddLogs("Response Not Found By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                            result = "Response Not Found";
                        }
                    }
                    else
                    {
                        Common.AddLogs("User Not Found By This TXNId:" + TXNID, false, Convert.ToInt32(AddLog.LogType.ConnectIps_Deposit), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, resuser.PlatForm, resuser.DeviceCode);

                        result = "User Not Found";
                    }
                }
                else
                {

                    result = "Transaction not found";

                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }

        public static void SendNotification(string authenticationToken, Int32 VendorApiType, Int64 memberid, string Title, string Message, string InactiveDeviceCode = "", string path = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(authenticationToken))
                {
                    CreatedBy = Common.GetCreatedById(authenticationToken);
                    CreatedByName = Common.GetCreatedByName(authenticationToken);
                }
                AddNotification objNotification = new AddNotification();
                objNotification.Title = Title;
                objNotification.NotificationMessage = Message;
                objNotification.NotificationType = VendorApiType;
                objNotification.MemberId = memberid;
                objNotification.CreatedBy = CreatedBy;
                objNotification.CreatedByName = CreatedByName;
                SentNotifications.ExecuteNotification(authenticationToken, objNotification, InactiveDeviceCode);

            }
            catch (Exception ex)
            {
                //AddLog(ex.Message, true,0,"",true);

            }
        }

        public static void AddLogs(string Message, bool IsAdmin = false, int type = 0, Int64 memberid = 0, string UserId = "", bool IsMobile = false, string Platform = "", string DeviceCode = "", Int32 LogActivity = 0, Int64 CreatedBy = 0, string CreatedByName = "")
        {
            AddLog log = new AddLog();
            if (IsMobile)
            {
                log.MemberId = memberid;
                log.UserId = UserId;
                log.Action = Message;
                log.Platform = Platform;
                log.IsMobile = IsMobile;
                log.DeviceCode = DeviceCode;
                if ((CreatedBy != 0 && !string.IsNullOrEmpty(CreatedByName)))
                {
                    log.CreatedBy = CreatedBy;
                    log.CreatedByName = CreatedByName;
                }
                else if (!string.IsNullOrEmpty(Common.authenticationToken))
                {
                    log.CreatedBy = Common.GetCreatedById(authenticationToken);
                    log.CreatedByName = Common.GetCreatedByName(authenticationToken);
                }
            }
            else
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["AdminMemberId"] != null && IsAdmin)
                {
                    log.MemberId = memberid;
                    log.CreatedBy = Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]);
                    log.CreatedByName = HttpContext.Current.Session["AdminUserName"].ToString();
                    log.Action = Message;
                }
                else if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserId"] != null && !IsAdmin)
                {
                    log.MemberId = memberid;
                    log.CreatedBy = Convert.ToInt64(HttpContext.Current.Session["UserId"].ToString());
                    log.CreatedByName = HttpContext.Current.Session["UserName"].ToString();
                    log.Action = Message;
                }
                else if (memberid != 0 && !string.IsNullOrEmpty(UserId))
                {
                    log.MemberId = memberid;
                    log.UserId = UserId;
                    log.Action = Message;
                    if (CreatedBy != 0)
                    {
                        log.CreatedBy = CreatedBy;
                        log.CreatedByName = CreatedByName;
                    }
                    else
                    {
                        log.CreatedBy = Common.CreatedBy;
                        log.CreatedByName = Common.CreatedByName;
                    }
                }
                log.Platform = "WEB";
                log.IsMobile = IsMobile;
                string deviceId = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber().AddSystemDriveSerialNumber().ToString();
                log.DeviceCode = deviceId;
            }
            if (!IsAdmin)
            {
                if (Convert.ToInt64(memberid) > 0)
                {
                    AddUserBasicInfo resUser = new AddUserBasicInfo();
                    resUser.MemberId = memberid;
                    resUser.GetUserInformationBasic();
                    if (resUser != null && resUser.Id != 0)
                    {
                        log.ContactNumber = resUser.ContactNumber;
                        log.OldUserStatus = resUser.IsOldUser ? 1 : 0;
                        log.UserType = resUser.RoleId;
                        if (string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(resUser.FirstName))
                        {
                            log.MemberId = resUser.MemberId;
                            log.UserId = resUser.FirstName + " " + resUser.LastName;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(log.UserId) || log.MemberId == 0)
            {
                log.MemberId = Common.CreatedBy;
                log.UserId = Common.CreatedByName;
            }
            if (string.IsNullOrEmpty(log.CreatedByName) || log.CreatedBy == 0)
            {
                log.CreatedBy = Common.CreatedBy;
                log.CreatedByName = Common.CreatedByName;
            }
            log.Action = Message;
            log.IsAdmin = IsAdmin;
            log.IsActive = true;
            log.IsApprovedByAdmin = true;
            log.Type = type;
            log.LogActivity = LogActivity;
            RepCRUD<AddLog, GetLog>.Insert(log, "logs");
        }
        public static bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !Char.IsLetterOrDigit(ch));
        }
        public static System.Data.DataTable ConvertToDataTable<T>(List<T> models)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static Int32 GetMerchantLoginMemberId()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["MerchantRole"])))
            {
                return Convert.ToInt32(HttpContext.Current.Session["MerchantRole"]);
            }
            else
            {
                return 0;
            }
        }
        public static Int32 GetMyPayUserLoginMemberId()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["MyPayUserRole"])))
            {
                return Convert.ToInt32(HttpContext.Current.Session["MyPayUserRole"]);
            }
            else
            {
                return 0;
            }
        }
        public static bool SetMerchantSession(AddMerchant usr)
        {
            if (usr != null)
            {
                HttpContext.Current.Session["MerchantUniqueId"] = usr.MerchantUniqueId;
                HttpContext.Current.Session["MerchantUserName"] = usr.UserName;
                HttpContext.Current.Session["MerchantName"] = usr.FirstName;
                HttpContext.Current.Session["MerchantStatus"] = usr.IsActive;
                HttpContext.Current.Session["MerchantRole"] = usr.RoleId;
                HttpContext.Current.Session["MerchantPasswordReset"] = usr.IsPasswordReset;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool RemoveMerchantSession()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["MerchantUniqueId"] = null;
            HttpContext.Current.Session["MerchantUserName"] = null;
            HttpContext.Current.Session["MerchantName"] = null;
            HttpContext.Current.Session["MerchantStatus"] = null;
            HttpContext.Current.Session["MerchantPasswordReset"] = null;
            HttpContext.Current.Session["MerchantRole"] = null;
            return true;
        }
        public static bool SetMyPayUserSession(AddUserLoginWithPin usr, string Token = "")
        {
            if (usr != null)
            {
                HttpContext.Current.Session["MyPayUserMemberId"] = usr.MemberId;
                HttpContext.Current.Session["MyPayUserName"] = usr.FirstName;
                HttpContext.Current.Session["MyPayFullName"] = usr.FirstName + " " + usr.LastName;
                HttpContext.Current.Session["MyPayContactNumber"] = usr.ContactNumber;
                HttpContext.Current.Session["MyPayEmail"] = usr.Email;
                HttpContext.Current.Session["MyPayDarkTheme"] = usr.IsDarkTheme;
                string UserImage = (usr.UserImage.Trim() == "" ? "" : ("/UserDocuments/Images/" + usr.UserImage));
                if (!string.IsNullOrEmpty(UserImage))
                {
                    if (Common.ApplicationEnvironment.IsProduction)
                    {
                        UserImage = Common.LiveSiteUrl + UserImage;
                    }
                    else
                    {
                        UserImage = Common.TestSiteUrl + UserImage;
                    }
                }
                HttpContext.Current.Session["MyPayUserImage"] = UserImage;
                HttpContext.Current.Session["MyPayUserStatus"] = usr.IsActive;
                HttpContext.Current.Session["MyPayUserRole"] = usr.RoleId;
                HttpContext.Current.Session["MyPayUserWalletbalance"] = usr.TotalAmount;
                HttpContext.Current.Session["MyPayUserMPCoinsbalance"] = usr.TotalRewardPoints;
                HttpContext.Current.Session["MyPayUserJWTToken"] = Token;
                HttpContext.Current.Session["IsKycVerified"] = usr.IsKYCApproved;
                HttpContext.Current.Session["MyPayUserTotalCashback"] = "0.00";
                HttpContext.Current.Session["MyPayUserRefCode"] = usr.RefCode;
                HttpContext.Current.Session["UserIsFirstLogin"] = "1";
                HttpContext.Current.Session["MyPayUserBrowserID"] = usr.DeviceId;

                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool RemoveMyPayUserSession()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["MyPayUserMemberId"] = null;
            HttpContext.Current.Session["MyPayUserName"] = null;
            HttpContext.Current.Session["MyPayFullName"] = null;
            HttpContext.Current.Session["MyPayContactNumber"] = null;
            HttpContext.Current.Session["MyPayEmail"] = null;
            HttpContext.Current.Session["MyPayUserImage"] = null;
            HttpContext.Current.Session["MyPayUserStatus"] = null;
            HttpContext.Current.Session["MyPayUserRole"] = null;
            HttpContext.Current.Session["MyPayUserWalletbalance"] = null;
            HttpContext.Current.Session["MyPayUserMPCoinsbalance"] = null;
            HttpContext.Current.Session["MyPayUserJWTToken"] = null;
            HttpContext.Current.Session["IsKycVerified"] = null;
            HttpContext.Current.Session["MyPayUserTotalCashback"] = null;
            HttpContext.Current.Session["MyPayUserRefCode"] = null;
            HttpContext.Current.Session["UserMarqueeList"] = null;
            HttpContext.Current.Session["UserIsFirstLogin"] = null;
            HttpContext.Current.Session["MyPayUserBrowserID"] = null;
            HttpContext.Current.Session["MyPayDarkTheme"] = null;

            return true;
        }
        public static void GenerateExcel(System.Data.DataTable dataTable, HttpResponseBase Response, string filename)
        {

            //DataSet dataSet = new DataSet();
            //dataSet.Tables.Add(dataTable);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable, "ExportSheet");
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xlsx", filename + System.DateTime.Now.ToFileTime()));

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        public static bool FirstTransactionCommisipon(AddUser resuser, AddDepositOrders res, string TransactionId)
        {
            bool result = false;
            try
            {
                AddUserLoginWithPin paroutuser = new AddUserLoginWithPin();
                GetUserLoginWithPin parinuser = new GetUserLoginWithPin();
                parinuser.MemberId = Convert.ToInt64(resuser.RefId);
                AddUserLoginWithPin parresuser = new AddUserLoginWithPin();
                if (resuser.RefId > 0)
                {
                    parresuser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin), parinuser, paroutuser);
                }
                //if (parresuser.Id > 0)
                {
                    decimal RewardPoint = 0;
                    result = MyPay.Models.Common.Common.DistributeRegistrationCommisionPoints(res.Platform, res.DeviceCode, true, resuser.MemberId, parresuser, "", "", ref RewardPoint, (int)AddSettings.CommissionType.TransactionCommission, TransactionId);

                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public static bool DistributeRegistrationCommisionPoints(string platform, string devicecode, bool ismobile, Int64 MemberId, AddUserLoginWithPin refres_Parent, string authenticationToken, string UserInput, ref decimal RewardPoint, int CommissionType, string CommissionFromTransactionID = "", bool IsSameDeviceID = false)
        {
            AddUserLoginWithPin outobjectRefree = new AddUserLoginWithPin();
            GetUserLoginWithPin inobjectRefree = new GetUserLoginWithPin();
            inobjectRefree.MemberId = MemberId;

            AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Models.Common.Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectRefree, outobjectRefree);

            bool resFlag = false;
            decimal RegistrationCommissionReferal = 0;
            decimal RegistrationRewardPointsReferal = 0;
            decimal RegistrationCommissionReferee = 0;
            decimal RegistrationRewardPointsReferee = 0;
            Int64 CreatedBy = 0;
            string CreatedByName = string.Empty;
            if (res.IsOldUser == false)
            {
                if (!string.IsNullOrEmpty(authenticationToken))
                {
                    CreatedBy = Common.GetCreatedById(authenticationToken);
                    CreatedByName = Common.GetCreatedByName(authenticationToken);

                }
                else if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["AdminMemberId"] != null && HttpContext.Current.Session["AdminUserName"] != null)
                {
                    CreatedBy = Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]);
                    CreatedByName = HttpContext.Current.Session["AdminUserName"].ToString();
                }
                else
                {
                    CreatedBy = 10000;
                    CreatedByName = "Admin";
                }
                Common.AddLogs($"DistributeRegistrationCommisionPoints for MemberID {res.MemberId}", true, (int)AddLog.LogType.DBLogs);
                if (res.CreatedDate > Convert.ToDateTime("02-Aug-2022"))
                {
                    #region NEW REGISTERATION_COMMISSION 
                    if ((CommissionType == (int)AddSettings.CommissionType.RegistrationCommission))
                    {
                        if (IsSameDeviceID == false)
                        {
                            // *****************************   CHECK IF NEW REGISTERATION COMMISSION ALREADY DISTRIBUTED OR NOT   ****************************
                            AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
                            GetTransactionCount inobjectTrans = new GetTransactionCount();
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.register_cashback;
                            inobjectTrans.MemberId = Convert.ToInt64(res.MemberId);
                            inobjectTrans.Type = VendorAPIType;
                            inobjectTrans.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                            AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
                            if (modelTrans != null)
                            {
                                if ((modelTrans.Count == 0) && (CommissionType == (int)AddSettings.CommissionType.RegistrationCommission))
                                {
                                    AddSettings outrefobjectSettings = new AddSettings();
                                    GetSettings inrefobjectSettings = new GetSettings();
                                    List<AddSettings> refres_settings = RepCRUD<GetSettings, AddSettings>.GetRecordList(Common.StoreProcedures.sp_Settings_Get, inrefobjectSettings, outrefobjectSettings);
                                    if (refres_settings != null && res.RefId != 0)
                                    {
                                        // NEW USER COMMISION
                                        AddSettings objReferal = refres_settings.Where(x => x.Type == (int)AddSettings.ReferType.Refferal && x.GenderType == res.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.NotVerified).FirstOrDefault();
                                        if (objReferal != null)
                                        {
                                            RegistrationCommissionReferal = objReferal.RegistrationCommission;
                                            RegistrationRewardPointsReferal = objReferal.RegistrationRewardPoint;
                                        }
                                        // PARENT USER COMMISION
                                        AddSettings objReferee = new AddSettings();
                                        if (refres_Parent.Id > 0)
                                        {
                                            if (refres_Parent.IsKYCApproved == (int)AddUser.kyc.Verified)
                                            {
                                                objReferee = refres_settings.Where(x => x.Type == (int)AddSettings.ReferType.Referee && x.GenderType == refres_Parent.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.Verified).FirstOrDefault();
                                            }
                                            else
                                            {
                                                objReferee = refres_settings.Where(x => x.Type == (int)AddSettings.ReferType.Referee && x.GenderType == refres_Parent.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.NotVerified).FirstOrDefault();
                                            }
                                        }
                                        RegistrationCommissionReferee = objReferee.RegistrationCommission;
                                        RegistrationRewardPointsReferee = objReferee.RegistrationRewardPoint;
                                    }
                                    // ***************************************************************************
                                    // *************** DISTRIBUTE REGISTRATION COMMISSION ***************
                                    // ***************************************************************************
                                    Common.AddLogs($"USER REGISTRATION COMMISION for MemberID {res.MemberId}. RegistrationCommissionReferee {RegistrationCommissionReferee}. RegistrationCommissionReferal {RegistrationCommissionReferal}", true, (int)AddLog.LogType.DBLogs);

                                    string CommissionMessageLog = $"Commission of Rs. {RegistrationCommissionReferal} for new registration ";
                                    if (!string.IsNullOrEmpty(refres_Parent.ContactNumber))
                                    {
                                        CommissionMessageLog = $"Commission of Rs. {RegistrationCommissionReferal} for new registration referred from RefCode:" + refres_Parent.RefCode;
                                    }
                                    Guid referenceGuid = Guid.NewGuid();
                                    string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                    string ReferenceNo = referenceGuid.ToString();
                                    WalletTransactions res_transaction = new WalletTransactions();

                                    // DISTRIBUTE REGISTRATION COMMISION TO NEW USER
                                    if (RegistrationCommissionReferal > 0)
                                    {
                                        decimal currentBalance = res.TotalAmount + Convert.ToDecimal(RegistrationCommissionReferal);
                                        res_transaction.MemberId = res.MemberId;
                                        res_transaction.ContactNumber = res.ContactNumber;
                                        res_transaction.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                        res_transaction.Amount = Convert.ToDecimal(RegistrationCommissionReferal);
                                        res_transaction.VendorTransactionId = TransactionUniqueID;
                                        res_transaction.ParentTransactionId = String.Empty; // THIS FIELD IS KEPT EMPTY FOR NOW -- IT SHOULD BE PARENT TRANSACTION ID AGAINST CASHBACK.
                                        res_transaction.CurrentBalance = currentBalance;
                                        res_transaction.CreatedBy = CreatedBy;
                                        res_transaction.CreatedByName = CreatedByName;
                                        res_transaction.TransactionUniqueId = TransactionUniqueID;
                                        res_transaction.Remarks = CommissionMessageLog;
                                        res_transaction.Type = VendorAPIType;
                                        res_transaction.Description = CommissionMessageLog;
                                        res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                        res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                        res_transaction.Reference = ReferenceNo;
                                        res_transaction.IsApprovedByAdmin = true;
                                        res_transaction.IsActive = true;
                                        res_transaction.CashBack = Convert.ToDecimal(RegistrationCommissionReferal);
                                        res_transaction.NetAmount = res_transaction.Amount;
                                        res_transaction.RecieverId = refres_Parent.MemberId;
                                        res_transaction.RecieverContactNumber = refres_Parent.ContactNumber;
                                        res_transaction.RecieverName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                        res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Reciever;
                                        res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                        res_transaction.DeviceCode = devicecode;
                                        res_transaction.Platform = platform;
                                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                        res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                                        bool NewUserCommissionUpdate = false;
                                        // DISTRIBUTE REGISTRATION COMMISION TO NEW USER
                                        NewUserCommissionUpdate = res_transaction.Add();
                                        if (NewUserCommissionUpdate)
                                        {
                                            Common.AddLogs($"NEW USER REGISTRATION COMMISION for MemberID {res.MemberId}. COMPLETED.", true, (int)AddLog.LogType.DBLogs);
                                            // SEND NOTIFICATION TO NEW USER
                                            string Title = "Commission for Registration";
                                            string Message = CommissionMessageLog;
                                            Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message);

                                            // ********************************************************************************
                                            // *************** DISTRIBUTE REGISTRATION REWARD POINTS TO NEW USER ***************
                                            // ********************************************************************************
                                            if (RegistrationRewardPointsReferal != 0)
                                            {
                                                string RewardPointMessageLog = $"Earned MyPay Coin {RegistrationRewardPointsReferal} from new registration referred from this Refer Code:" + refres_Parent.RefCode;
                                                RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                                                res_rewardpoint.MemberId = res.MemberId;
                                                res_rewardpoint.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                                res_rewardpoint.Amount = Convert.ToDecimal(RegistrationRewardPointsReferal);
                                                res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                                                res_rewardpoint.ParentTransactionId = String.Empty; // THIS FIELD IS KEPT EMPTY FOR NOW -- IT SHOULD BE PARENT TRANSACTION ID AGAINST CASHBACK.
                                                res_rewardpoint.CurrentBalance = Convert.ToDecimal(res.TotalRewardPoints) + Convert.ToDecimal(RegistrationRewardPointsReferal);
                                                res_rewardpoint.CreatedBy = CreatedBy;
                                                res_rewardpoint.CreatedByName = CreatedByName;
                                                res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                                                res_rewardpoint.Remarks = RewardPointMessageLog;
                                                res_rewardpoint.Type = (int)RewardPointTransactions.RewardTypes.Registration;
                                                res_rewardpoint.Description = RewardPointMessageLog;
                                                res_rewardpoint.Status = 1;
                                                res_rewardpoint.Reference = ReferenceNo;
                                                res_rewardpoint.IsApprovedByAdmin = true;
                                                res_rewardpoint.IsActive = true;
                                                res_rewardpoint.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Credit);
                                                bool NewUserRewardPointUpdate = false;
                                                // DISTRIBUTE REGISTRATION  REWARD POINTS TO NEW USER
                                                NewUserRewardPointUpdate = res_rewardpoint.Add();
                                                if (NewUserRewardPointUpdate)
                                                {
                                                    // SEND NOTIFICATION TO NEW USER
                                                    Title = "Earned MyPay Coin";
                                                    Message = RewardPointMessageLog;
                                                    Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message);
                                                }
                                            }
                                        }
                                        Models.Common.Common.AddLogs(CommissionMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", ismobile, platform, devicecode, (int)AddLog.LogActivityEnum.Registration_Commission);
                                        resFlag = NewUserCommissionUpdate;
                                    }

                                    // DISTRIBUTE REGISTRATION COMMISION TO PARENT
                                    if (RegistrationCommissionReferee > 0)
                                    {
                                        // *****************************   CHECK IF REGISTER REFERAL AMOUNT ALREADY DISTRIBUTED OR NOT   ****************************
                                        string ParentTransactionUniqueID = TransactionUniqueID;
                                        Guid TransactionGuid = Guid.NewGuid();
                                        TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                        AddTransactionSumCount outobjectTransParent = new AddTransactionSumCount();
                                        GetTransactionCount inobjectTransParent = new GetTransactionCount();
                                        inobjectTransParent.MemberId = Convert.ToInt64(refres_Parent.MemberId);
                                        inobjectTransParent.RecieverId = Convert.ToInt64(res.MemberId);
                                        inobjectTransParent.Type = VendorAPIType;
                                        inobjectTransParent.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                        AddTransactionSumCount modelTransParent = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTransParent, outobjectTransParent);
                                        if (modelTransParent != null)
                                        {
                                            if ((modelTransParent.Count == 0) && (CommissionType == (int)AddSettings.CommissionType.RegistrationCommission))
                                            {
                                                res_transaction = new WalletTransactions();
                                                CommissionMessageLog = $"Commission of Rs. {RegistrationCommissionReferee} for new registration from this contact number:" + res.ContactNumber;
                                                res_transaction.MemberId = refres_Parent.MemberId;
                                                res_transaction.ContactNumber = refres_Parent.ContactNumber;
                                                res_transaction.MemberName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                                res_transaction.Amount = Convert.ToDecimal(RegistrationCommissionReferee);
                                                res_transaction.VendorTransactionId = TransactionUniqueID;
                                                res_transaction.ParentTransactionId = ParentTransactionUniqueID;
                                                res_transaction.CurrentBalance = Convert.ToDecimal(refres_Parent.TotalAmount) + RegistrationCommissionReferee;
                                                res_transaction.CreatedBy = CreatedBy;
                                                res_transaction.CreatedByName = CreatedByName;
                                                res_transaction.TransactionUniqueId = TransactionUniqueID;
                                                res_transaction.Remarks = CommissionMessageLog;
                                                res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.register_cashback;
                                                res_transaction.Description = CommissionMessageLog;
                                                res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                                res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                                res_transaction.Reference = ReferenceNo;
                                                res_transaction.IsApprovedByAdmin = true;
                                                res_transaction.IsActive = true;
                                                res_transaction.CashBack = Convert.ToDecimal(RegistrationCommissionReferee);
                                                res_transaction.NetAmount = Convert.ToDecimal(RegistrationCommissionReferee);
                                                res_transaction.RecieverId = res.MemberId;
                                                res_transaction.RecieverContactNumber = res.ContactNumber;
                                                res_transaction.RecieverName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                                res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                                res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                                res_transaction.DeviceCode = devicecode;
                                                res_transaction.Platform = platform;
                                                res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                                res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                                                res_transaction.ReceiverAmount = Convert.ToDecimal(RegistrationCommissionReferal); ;
                                                res_transaction.RefCode = refres_Parent.RefCode;
                                                bool ParentCommissionUpdate = res_transaction.Add();

                                                if (ParentCommissionUpdate)
                                                {
                                                    // SEND NOTIFICATION TO PARENT REFREE
                                                    Common.AddLogs($"PARENT USER REGISTRATION COMMISION for MemberID {refres_Parent.MemberId}. COMPLETED.", true, (int)AddLog.LogType.DBLogs);
                                                    string Title = "Commission for Registration";
                                                    string Message = CommissionMessageLog;
                                                    Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, refres_Parent.MemberId, Title, Message);
                                                    Models.Common.Common.AddLogs(CommissionMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(refres_Parent.MemberId), "", ismobile, platform, devicecode, (int)AddLog.LogActivityEnum.Registration_Commission);
                                                }

                                                // ***************************************************************************
                                                // *************** DISTRIBUTE REGISTRATION REWARD POINTS ***************
                                                // ***************************************************************************

                                                if (RegistrationRewardPointsReferee > 0)
                                                {
                                                    // DISTRIBUTE REGISTRATION REWARD POINTS TO PARENT 
                                                    string RewardPointMessageLog = $"Earned MyPay Coin {RegistrationRewardPointsReferee}from new registration from this contact number:" + res.ContactNumber;
                                                    RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                                                    res_rewardpoint.MemberId = refres_Parent.MemberId;
                                                    res_rewardpoint.MemberName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                                    res_rewardpoint.Amount = Convert.ToDecimal(RegistrationRewardPointsReferee);
                                                    res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                                                    res_rewardpoint.ParentTransactionId = ParentTransactionUniqueID;
                                                    res_rewardpoint.CurrentBalance = Convert.ToDecimal(refres_Parent.TotalRewardPoints) + RegistrationRewardPointsReferee;
                                                    res_rewardpoint.CreatedBy = CreatedBy;
                                                    res_rewardpoint.CreatedByName = CreatedByName;
                                                    res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                                                    res_rewardpoint.Remarks = RewardPointMessageLog;
                                                    res_rewardpoint.Type = (int)RewardPointTransactions.RewardTypes.Registration;
                                                    res_rewardpoint.Description = RewardPointMessageLog;
                                                    res_rewardpoint.Status = 1;
                                                    res_rewardpoint.Reference = ReferenceNo;
                                                    res_rewardpoint.IsApprovedByAdmin = true;
                                                    res_rewardpoint.IsActive = true;
                                                    res_rewardpoint.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Credit);
                                                    bool ParentRewardPointUpdate = false;
                                                    // DISTRIBUTE REGISTRATION  REWARD POINTS TO PARENT REFREE
                                                    ParentRewardPointUpdate = res_rewardpoint.Add();
                                                    if (ParentRewardPointUpdate)
                                                    {
                                                        // SEND NOTIFICATION TO PARENT REFREE
                                                        string Title = "Earned MyPay Coin";
                                                        string Message = RewardPointMessageLog;
                                                        Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, refres_Parent.MemberId, Title, Message);
                                                        Models.Common.Common.AddLogs(RewardPointMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(refres_Parent.MemberId), "", ismobile, platform, devicecode);

                                                        resFlag = ParentRewardPointUpdate;
                                                    }
                                                }
                                                resFlag = ParentCommissionUpdate;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        return resFlag;
                    }
                    #endregion
                }
                #region KYC APPROVAL_COMMISSION 
                if ((CommissionType == (int)AddSettings.CommissionType.KycApprovedCommission))
                {
                    // *****************************   CHECK IF KYC APPROVAL COMMISSION ALREADY DISTRIBUTED OR NOT   ****************************
                    AddTransactionSumCount outobjectTransKYC = new AddTransactionSumCount();
                    GetTransactionCount inobjectTransKYC = new GetTransactionCount();
                    int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.kyc_cashback;
                    inobjectTransKYC.MemberId = Convert.ToInt64(res.MemberId);
                    inobjectTransKYC.Type = VendorAPIType;
                    inobjectTransKYC.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                    AddTransactionSumCount modelTransKYC = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTransKYC, outobjectTransKYC);
                    if (modelTransKYC != null)
                    {
                        // ********* IF KYC COMMISSION NEVER DISTRIBUTED BEFORE ******* //
                        if ((modelTransKYC.Count == 0) && (CommissionType == (int)AddSettings.CommissionType.KycApprovedCommission))
                        {
                            decimal KYCCommissionReferal = 0;
                            decimal KYCRewardPointsReferal = 0;

                            decimal KYCCommissionReferee = 0;
                            decimal KYCRewardPointsReferee = 0;
                            AddSettings outrefobjectSettings = new AddSettings();
                            GetSettings inrefobjectSettings = new GetSettings();
                            List<AddSettings> refres_settings = RepCRUD<GetSettings, AddSettings>.GetRecordList(Common.StoreProcedures.sp_Settings_Get, inrefobjectSettings, outrefobjectSettings);
                            if (refres_settings != null)
                            {
                                // NEW USER COMMISION
                                AddSettings objReferal = new AddSettings();
                                objReferal = refres_settings.Where(x => x.Type == (int)AddSettings.ReferType.Refferal && x.GenderType == res.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.NotVerified).FirstOrDefault();
                                KYCCommissionReferal = objReferal.KYCCommission;
                                KYCRewardPointsReferal = objReferal.KYCRewardPoint;

                                // PARENT USER COMMISION
                                AddSettings objReferee = new AddSettings();
                                if (refres_Parent.Id > 0)
                                {
                                    if (refres_Parent.IsKYCApproved == (int)AddUser.kyc.Verified)
                                    {
                                        objReferee = refres_settings.Where(x => x.Type == (int)AddSettings.ReferType.Referee && x.GenderType == refres_Parent.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.Verified).FirstOrDefault();
                                    }
                                    else
                                    {
                                        objReferee = refres_settings.Where(x => x.Type == (int)AddSettings.ReferType.Referee && x.GenderType == refres_Parent.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.NotVerified).FirstOrDefault();
                                    }
                                }
                                KYCCommissionReferee = objReferee.KYCCommission;
                                KYCRewardPointsReferee = objReferee.KYCRewardPoint;
                            }
                            // ***************************************************************************
                            // *************** DISTRIBUTE KYC COMMISSION ***************
                            // ***************************************************************************

                            string CommissionMessageLog = $"Commission of Rs. {KYCCommissionReferal} from new KYC";
                            if (!string.IsNullOrEmpty(refres_Parent.ContactNumber))
                            {
                                CommissionMessageLog = $"Commission of Rs. {KYCCommissionReferal} from new KYC referred from RefCode:" + refres_Parent.RefCode;
                            }
                            Guid referenceGuid = Guid.NewGuid();
                            string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                            string ReferenceNo = referenceGuid.ToString();
                            WalletTransactions res_transaction = new WalletTransactions();

                            // DISTRIBUTE KYC COMMISION TO NEW USER
                            if (KYCCommissionReferal > 0)
                            {
                                res_transaction.MemberId = res.MemberId;
                                res_transaction.ContactNumber = res.ContactNumber;
                                res_transaction.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                res_transaction.Amount = Convert.ToDecimal(KYCCommissionReferal);
                                res_transaction.VendorTransactionId = TransactionUniqueID;
                                res_transaction.ParentTransactionId = String.Empty; // THIS FIELD IS KEPT EMPTY FOR NOW -- IT SHOULD BE PARENT TRANSACTION ID AGAINST CASHBACK.
                                res_transaction.CurrentBalance = Convert.ToDecimal(res.TotalAmount) + Convert.ToDecimal(KYCCommissionReferal);
                                res_transaction.CreatedBy = CreatedBy;
                                res_transaction.CreatedByName = CreatedByName;
                                res_transaction.TransactionUniqueId = TransactionUniqueID;
                                res_transaction.Remarks = CommissionMessageLog;
                                res_transaction.Type = VendorAPIType;
                                res_transaction.Description = CommissionMessageLog;
                                res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                res_transaction.Reference = ReferenceNo;
                                res_transaction.IsApprovedByAdmin = true;
                                res_transaction.IsActive = true;
                                res_transaction.CashBack = Convert.ToDecimal(KYCCommissionReferal);
                                res_transaction.NetAmount = res_transaction.Amount;
                                res_transaction.RecieverId = refres_Parent.MemberId;
                                res_transaction.RecieverContactNumber = refres_Parent.ContactNumber;
                                res_transaction.RecieverName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Reciever;
                                res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                res_transaction.DeviceCode = devicecode;
                                res_transaction.Platform = platform;
                                res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                                bool NewUserCommissionUpdate = false;
                                // DISTRIBUTE KYC COMMISION TO NEW USER
                                NewUserCommissionUpdate = res_transaction.Add();
                                if (NewUserCommissionUpdate)
                                {
                                    // SEND NOTIFICATION TO NEW USER
                                    string Title = "Commission for KYC";
                                    string Message = CommissionMessageLog;
                                    Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message);
                                    Models.Common.Common.AddLogs(CommissionMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", ismobile, platform, devicecode, (int)AddLog.LogActivityEnum.KYC_Commission);

                                    if (KYCRewardPointsReferal != 0)
                                    {
                                        string RewardPointMessageLog = $"Earned MyPay Coin {KYCRewardPointsReferal} from new KYC";
                                        RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                                        res_rewardpoint.MemberId = res.MemberId;
                                        res_rewardpoint.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                        res_rewardpoint.Amount = Convert.ToDecimal(KYCRewardPointsReferal);
                                        res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                                        res_rewardpoint.ParentTransactionId = String.Empty;
                                        res_rewardpoint.CurrentBalance = Convert.ToDecimal(res.TotalRewardPoints) + Convert.ToDecimal(KYCRewardPointsReferal);
                                        res_rewardpoint.CreatedBy = CreatedBy;
                                        res_rewardpoint.CreatedByName = CreatedByName;
                                        res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                                        res_rewardpoint.Remarks = RewardPointMessageLog;
                                        res_rewardpoint.Type = (int)RewardPointTransactions.RewardTypes.Kyc;
                                        res_rewardpoint.Description = RewardPointMessageLog;
                                        res_rewardpoint.Status = 1;
                                        res_rewardpoint.Reference = ReferenceNo;
                                        res_rewardpoint.IsApprovedByAdmin = true;
                                        res_rewardpoint.IsActive = true;
                                        res_rewardpoint.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Credit);
                                        bool ParentRewardPointUpdate = false;
                                        // DISTRIBUTE KYC  REWARD POINTS TO NEW USER
                                        ParentRewardPointUpdate = res_rewardpoint.Add();
                                        if (ParentRewardPointUpdate)
                                        {
                                            // SEND NOTIFICATION TO NEW USER
                                            Title = "Earned MyPay Coin for KYC";
                                            Message = RewardPointMessageLog;
                                            Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message);

                                            resFlag = ParentRewardPointUpdate;
                                        }
                                    }
                                }
                            }

                            // DISTRIBUTE KYC COMMISION TO PARENT USER
                            if (KYCCommissionReferee > 0)
                            {
                                string Title = String.Empty;
                                string Message = String.Empty;

                                AddTransactionSumCount outobjectTransParent = new AddTransactionSumCount();
                                GetTransactionCount inobjectTransParent = new GetTransactionCount();
                                inobjectTransParent.MemberId = Convert.ToInt64(refres_Parent.MemberId);
                                inobjectTransParent.RecieverId = Convert.ToInt64(res.MemberId);
                                inobjectTransParent.Type = VendorAPIType;
                                inobjectTransParent.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                AddTransactionSumCount modelTransKYCParent = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTransParent, outobjectTransParent);
                                if (modelTransKYCParent != null)
                                {
                                    // ********* IF KYC COMMISSION NEVER DISTRIBUTED BEFORE ******* //
                                    if ((modelTransKYCParent.Count == 0) && (CommissionType == (int)AddSettings.CommissionType.KycApprovedCommission))
                                    {
                                        // DISTRIBUTE KYC COMMISION TO PARENT
                                        string ParentTransactionUniqueID = TransactionUniqueID;
                                        Guid transactionGuid = Guid.NewGuid();
                                        TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                        res_transaction = new WalletTransactions();
                                        CommissionMessageLog = $"Commission  of Rs. {KYCCommissionReferee} for KYC of contact number:" + res.ContactNumber;

                                        res_transaction.MemberId = refres_Parent.MemberId;
                                        res_transaction.ContactNumber = refres_Parent.ContactNumber;
                                        res_transaction.MemberName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                        res_transaction.Amount = Convert.ToDecimal(KYCCommissionReferee);
                                        res_transaction.VendorTransactionId = TransactionUniqueID;
                                        res_transaction.ParentTransactionId = ParentTransactionUniqueID;
                                        res_transaction.CurrentBalance = Convert.ToDecimal(refres_Parent.TotalAmount + (decimal)(KYCCommissionReferee));
                                        res_transaction.CreatedBy = CreatedBy;
                                        res_transaction.CreatedByName = CreatedByName;
                                        res_transaction.TransactionUniqueId = TransactionUniqueID;
                                        res_transaction.Remarks = CommissionMessageLog;
                                        res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.kyc_cashback;
                                        res_transaction.Description = CommissionMessageLog;
                                        res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                        res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                        res_transaction.Reference = ReferenceNo;
                                        res_transaction.IsApprovedByAdmin = true;
                                        res_transaction.IsActive = true;
                                        res_transaction.CashBack = Convert.ToDecimal(KYCCommissionReferee);
                                        res_transaction.NetAmount = res_transaction.Amount;
                                        res_transaction.RecieverId = res.MemberId;
                                        res_transaction.RecieverContactNumber = res.ContactNumber;
                                        res_transaction.RecieverName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                        res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                        res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                        res_transaction.DeviceCode = devicecode;
                                        res_transaction.Platform = platform;
                                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                        res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                                        bool ParentCommissionUpdate = false;
                                        // DISTRIBUTE KYC COMMISION TO NEW USER
                                        ParentCommissionUpdate = res_transaction.Add();
                                        if (ParentCommissionUpdate)
                                        {
                                            // SEND NOTIFICATION TO PARENT REFEREE
                                            Title = "Commission for KYC";
                                            Message = CommissionMessageLog;
                                            Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, refres_Parent.MemberId, Title, Message);
                                            Models.Common.Common.AddLogs(CommissionMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(refres_Parent.MemberId), "", ismobile, platform, devicecode, (int)AddLog.LogActivityEnum.KYC_Commission);
                                        }
                                        // DISTRIBUTE KYC REWARD POINTS TO PARENT
                                        if (RegistrationRewardPointsReferee != 0)
                                        {
                                            string RewardPointMessageLog = $"Earned MyPay Coin {RegistrationRewardPointsReferee} from KYC of contact number:" + res.ContactNumber;

                                            RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                                            res_rewardpoint.MemberId = refres_Parent.MemberId;
                                            res_rewardpoint.MemberName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                            res_rewardpoint.Amount = Convert.ToDecimal(RegistrationRewardPointsReferee);
                                            res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                                            res_rewardpoint.ParentTransactionId = ParentTransactionUniqueID;
                                            res_rewardpoint.CurrentBalance = Convert.ToDecimal(refres_Parent.TotalRewardPoints) + KYCRewardPointsReferee;
                                            res_rewardpoint.CreatedBy = CreatedBy;
                                            res_rewardpoint.CreatedByName = CreatedByName;
                                            res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                                            res_rewardpoint.Remarks = RewardPointMessageLog;
                                            res_rewardpoint.Type = (int)RewardPointTransactions.RewardTypes.Registration;
                                            res_rewardpoint.Description = RewardPointMessageLog;
                                            res_rewardpoint.Status = 1;
                                            res_rewardpoint.Reference = ReferenceNo;
                                            res_rewardpoint.IsApprovedByAdmin = true;
                                            res_rewardpoint.IsActive = true;
                                            res_rewardpoint.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Credit);
                                            bool ParentRewardPointUpdate = res_rewardpoint.Add();
                                            // SEND NOTIFICATION TO PARENT REFREE

                                            Title = "Earned MyPay Coin for KYC";
                                            Message = RewardPointMessageLog;
                                            Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, refres_Parent.MemberId, Title, Message);
                                            Models.Common.Common.AddLogs(RewardPointMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(refres_Parent.MemberId), "", ismobile, platform, devicecode);

                                            resFlag = ParentCommissionUpdate;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                #endregion

                #region FIRST_TRANSACTION_COMMISSION 
                if ((CommissionType == (int)AddSettings.CommissionType.TransactionCommission))
                {
                    if (Common.CheckFirstTransactionExists(res.MemberId) == false)
                    {
                        Common.AddLogs($"TransactionCommission for MemberID {refres_Parent.MemberId}. CommissionFromTransactionID {CommissionFromTransactionID} INITIATED.", true, (int)AddLog.LogType.DBLogs);
                        // *****************************   CHECK IF FIRST_TRANSACTION_COMMISSION ALREADY DISTRIBUTED OR NOT   ****************************
                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.transaction_cashback;
                        AddTransactionSumCount outobjectTransaction = new AddTransactionSumCount();
                        GetTransactionCount inobjectTransaction = new GetTransactionCount();
                        inobjectTransaction.MemberId = Convert.ToInt64(refres_Parent.MemberId);
                        inobjectTransaction.RecieverId = Convert.ToInt64(res.MemberId);
                        inobjectTransaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                        inobjectTransaction.Type = VendorAPIType;
                        AddTransactionSumCount GetTransactionCommisionTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTransaction, outobjectTransaction);
                        if (GetTransactionCommisionTrans != null)
                        {
                            // ********* IF TRANSACTION NEVER DONE BEFORE ******* //
                            if (refres_Parent.MemberId != 0 && (GetTransactionCommisionTrans.Count == 0) && (!string.IsNullOrEmpty(CommissionFromTransactionID)) && (CommissionType == (int)AddSettings.CommissionType.TransactionCommission))
                            {
                                Common.AddLogs($"TransactionCommission for MemberID {refres_Parent.MemberId}. GetTransactionCommisionTrans.Count {GetTransactionCommisionTrans.Count} .", true, (int)AddLog.LogType.DBLogs);
                                // ************* GET TRANSACTION AMOUNT *********** //
                                AddTransaction outobjectTransactionAmount = new AddTransaction();
                                GetTransaction inobjectTransactionAmount = new GetTransaction();
                                inobjectTransactionAmount.TransactionUniqueId = CommissionFromTransactionID;
                                inobjectTransactionAmount.Sign = Convert.ToInt16(WalletTransactions.Signs.Debit);
                                AddTransaction GetTransactionCommisionTransAmount = RepCRUD<GetTransaction, AddTransaction>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Get, inobjectTransactionAmount, outobjectTransactionAmount);
                                if (GetTransactionCommisionTransAmount != null && GetTransactionCommisionTransAmount.Id != 0)
                                {
                                    // *****************************   CHECK IF UTILITY TRANSACTION  OR NOT   ****************************
                                    string UtilityName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(GetTransactionCommisionTransAmount.VendorType)).ToString().ToUpper().Replace("_", " ");
                                    if (UtilityName.Contains("KHALTI"))
                                    {
                                        decimal TransactionCommissionReferee = 0;
                                        decimal TransactionRewardPointsReferee = 0;
                                        decimal TransactionCommissionRefferal = 0;
                                        decimal TransactionRewardPointsRefferal = 0;
                                        AddSettings outrefobjectSettingsTxn = new AddSettings();
                                        GetSettings inrefobjectSettingsTxn = new GetSettings();
                                        List<AddSettings> refres_settingsTxn = RepCRUD<GetSettings, AddSettings>.GetRecordList(Common.StoreProcedures.sp_Settings_Get, inrefobjectSettingsTxn, outrefobjectSettingsTxn);
                                        if (refres_settingsTxn != null)
                                        {
                                            // PARENT USER COMMISION
                                            AddSettings objReferee = new AddSettings();
                                            if (refres_Parent.Id > 0)
                                            {
                                                if (refres_Parent.IsKYCApproved == (int)AddUser.kyc.Verified)
                                                {
                                                    objReferee = refres_settingsTxn.Where(x => x.Type == (int)AddSettings.ReferType.Referee && x.GenderType == refres_Parent.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.Verified).FirstOrDefault();
                                                }
                                                else
                                                {
                                                    objReferee = refres_settingsTxn.Where(x => x.Type == (int)AddSettings.ReferType.Referee && x.GenderType == refres_Parent.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.NotVerified).FirstOrDefault();
                                                }
                                            }
                                            if (objReferee != null && objReferee.Id != 0)
                                            {
                                                // *************** TransactionCommission***************
                                                TransactionCommissionReferee = Convert.ToDecimal((objReferee.TransactionCommission * Convert.ToDecimal(GetTransactionCommisionTransAmount.Amount)) / 100);
                                                if (TransactionCommissionReferee < objReferee.MinAmountTransactionCommission)
                                                {
                                                    TransactionCommissionReferee = objReferee.MinAmountTransactionCommission;
                                                }
                                                else if (TransactionCommissionReferee > objReferee.MaxAmountTransactionCommission)
                                                {
                                                    TransactionCommissionReferee = objReferee.MaxAmountTransactionCommission;
                                                }
                                                // *************** TransactionRewardPoints***************
                                                TransactionRewardPointsReferee = Convert.ToDecimal((objReferee.TransactionRewardPoint * Convert.ToDecimal(GetTransactionCommisionTransAmount.Amount)) / 100);
                                                if (TransactionRewardPointsReferee < objReferee.MinRewardPointTransactionCommission)
                                                {
                                                    TransactionRewardPointsReferee = objReferee.MinRewardPointTransactionCommission;
                                                }
                                                else if (TransactionRewardPointsReferee > objReferee.MaxRewardPointTransactionCommission)
                                                {
                                                    TransactionRewardPointsReferee = objReferee.MaxRewardPointTransactionCommission;
                                                }
                                            }

                                            // NEW USER COMMISION
                                            AddSettings objRefferal = new AddSettings();
                                            if (res.IsKYCApproved == (int)AddUser.kyc.Verified)
                                            {
                                                objRefferal = refres_settingsTxn.Where(x => x.Type == (int)AddSettings.ReferType.Refferal && x.GenderType == res.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.Verified).FirstOrDefault();
                                            }
                                            else
                                            {
                                                objRefferal = refres_settingsTxn.Where(x => x.Type == (int)AddSettings.ReferType.Refferal && x.GenderType == res.Gender && x.IsKYCApproved == (int)AddSettings.KycTypes.NotVerified).FirstOrDefault();
                                            }
                                            if (objRefferal != null && objRefferal.Id != 0)
                                            {
                                                // *************** TransactionCommission***************
                                                TransactionCommissionRefferal = Convert.ToDecimal((objRefferal.TransactionCommission * Convert.ToDecimal(GetTransactionCommisionTransAmount.Amount)) / 100);
                                                if (TransactionCommissionRefferal < objRefferal.MinAmountTransactionCommission)
                                                {
                                                    TransactionCommissionRefferal = objRefferal.MinAmountTransactionCommission;
                                                }
                                                else if (TransactionCommissionRefferal > objRefferal.MaxAmountTransactionCommission)
                                                {
                                                    TransactionCommissionRefferal = objRefferal.MaxAmountTransactionCommission;
                                                }
                                                // *************** TransactionRewardPoints***************
                                                TransactionRewardPointsRefferal = Convert.ToDecimal((objRefferal.TransactionRewardPoint * Convert.ToDecimal(GetTransactionCommisionTransAmount.Amount)) / 100);
                                                if (TransactionRewardPointsRefferal < objRefferal.MinRewardPointTransactionCommission)
                                                {
                                                    TransactionRewardPointsRefferal = objRefferal.MinRewardPointTransactionCommission;
                                                }
                                                else if (TransactionRewardPointsRefferal > objRefferal.MaxRewardPointTransactionCommission)
                                                {
                                                    TransactionRewardPointsRefferal = objRefferal.MaxRewardPointTransactionCommission;
                                                }
                                            }
                                        }
                                        // ***************************************************************************
                                        // *************** DISTRIBUTE TRANSACTION COMMISSION ***************
                                        // ***************************************************************************
                                        Common.AddLogs($"TransactionCommission for MemberID {refres_Parent.MemberId}. TransactionCommissionRefferal{TransactionCommissionRefferal}. TransactionCommissionReferee {TransactionCommissionReferee}", true, (int)AddLog.LogType.DBLogs);

                                        string CommissionMessageLog = $"Commission of Rs. {TransactionCommissionReferee} from first TransactionId {CommissionFromTransactionID} completed by contact number:{res.ContactNumber}";
                                        Guid referenceGuid = Guid.NewGuid();
                                        string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                        string ReferenceNo = referenceGuid.ToString();
                                        WalletTransactions res_transaction = new WalletTransactions();

                                        if (TransactionCommissionReferee > 0)
                                        {
                                            res_transaction.MemberId = refres_Parent.MemberId;
                                            res_transaction.ContactNumber = refres_Parent.ContactNumber;
                                            res_transaction.MemberName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                            res_transaction.Amount = Convert.ToDecimal(TransactionCommissionReferee);
                                            res_transaction.NetAmount = res_transaction.Amount;
                                            res_transaction.CurrentBalance = Convert.ToDecimal(refres_Parent.TotalAmount + (decimal)(TransactionCommissionReferee));
                                            res_transaction.VendorTransactionId = TransactionUniqueID;
                                            res_transaction.CreatedBy = CreatedBy;
                                            res_transaction.CreatedByName = CreatedByName;
                                            res_transaction.TransactionUniqueId = TransactionUniqueID;
                                            res_transaction.ParentTransactionId = CommissionFromTransactionID;
                                            res_transaction.Remarks = CommissionMessageLog;
                                            res_transaction.Type = VendorAPIType;
                                            res_transaction.Description = CommissionMessageLog;
                                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                            res_transaction.Reference = ReferenceNo;
                                            res_transaction.IsApprovedByAdmin = true;
                                            res_transaction.IsActive = true;
                                            res_transaction.CashBack = Convert.ToDecimal(TransactionCommissionReferee);
                                            res_transaction.NetAmount = res_transaction.Amount;
                                            res_transaction.RecieverId = res.MemberId;
                                            res_transaction.RecieverContactNumber = res.ContactNumber;
                                            res_transaction.RecieverName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                            res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                            res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                            res_transaction.DeviceCode = devicecode;
                                            res_transaction.Platform = platform;
                                            res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                            bool ParentCommissionUpdate = false;
                                            // DISTRIBUTE Transaction COMMISION TO PARENT
                                            ParentCommissionUpdate = res_transaction.Add();
                                            if (ParentCommissionUpdate)
                                            {
                                                Models.Common.Common.AddLogs(CommissionMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(refres_Parent.MemberId), "", ismobile, platform, devicecode, (int)AddLog.LogActivityEnum.First_Transaction_Commission);
                                                resFlag = ParentCommissionUpdate;
                                            }

                                            // ***************************************************************************
                                            // *************** DISTRIBUTE TRANSACTION REWARD POINTS ***************
                                            // ***************************************************************************

                                            if (TransactionRewardPointsReferee != 0)
                                            {
                                                string RewardPointMessageLog = $"Earned MyPay Coin {RegistrationRewardPointsReferee} From First TransactionId {CommissionFromTransactionID} From Contact Number:{res.ContactNumber}";
                                                RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                                                res_rewardpoint.MemberId = refres_Parent.MemberId;
                                                res_rewardpoint.MemberName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                                res_rewardpoint.Amount = Convert.ToDecimal(TransactionRewardPointsReferee);
                                                res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                                                res_rewardpoint.ParentTransactionId = CommissionFromTransactionID;
                                                res_rewardpoint.CurrentBalance = Convert.ToDecimal(refres_Parent.TotalRewardPoints) + TransactionRewardPointsReferee;
                                                res_rewardpoint.CreatedBy = CreatedBy;
                                                res_rewardpoint.CreatedByName = CreatedByName;
                                                res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                                                res_rewardpoint.Remarks = RewardPointMessageLog;
                                                res_rewardpoint.Type = (int)RewardPointTransactions.RewardTypes.Transaction;
                                                res_rewardpoint.VendorServiceID = VendorAPIType;
                                                res_rewardpoint.Description = RewardPointMessageLog;
                                                res_rewardpoint.Status = 1;
                                                res_rewardpoint.Reference = ReferenceNo;
                                                res_rewardpoint.IsApprovedByAdmin = true;
                                                res_rewardpoint.IsActive = true;
                                                res_rewardpoint.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Credit);
                                                bool ParentRewardPointUpdate = false;
                                                // DISTRIBUTE TRANSACTION  REWARD POINTS TO PARENT
                                                ParentRewardPointUpdate = res_rewardpoint.Add();
                                                if (ParentRewardPointUpdate)
                                                {
                                                    Models.Common.Common.AddLogs(RewardPointMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(refres_Parent.MemberId), "", ismobile, platform, devicecode);
                                                    resFlag = ParentRewardPointUpdate;
                                                }
                                            }
                                        }

                                        AddTransactionSumCount outobjectTransactionRefferal = new AddTransactionSumCount();
                                        GetTransactionCount inobjectTransactionRefferal = new GetTransactionCount();
                                        inobjectTransactionRefferal.MemberId = Convert.ToInt64(res.MemberId);
                                        inobjectTransactionRefferal.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                        inobjectTransactionRefferal.Type = VendorAPIType;
                                        AddTransactionSumCount GetTransactionCommisionTransRefferal = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTransactionRefferal, outobjectTransactionRefferal);
                                        if (GetTransactionCommisionTransRefferal != null)
                                        {
                                            // ********* IF TRANSACTION NEVER DONE BEFORE ******* //
                                            if ((GetTransactionCommisionTransRefferal.Count == 0) && (!string.IsNullOrEmpty(CommissionFromTransactionID)) && (CommissionType == (int)AddSettings.CommissionType.TransactionCommission))
                                            {

                                                if (TransactionCommissionRefferal > 0)
                                                {
                                                    res_transaction = new WalletTransactions();
                                                    CommissionMessageLog = $"Commission of Rs. {TransactionCommissionRefferal} from first TransactionId {CommissionFromTransactionID} ";
                                                    TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                                    res_transaction.MemberId = res.MemberId;
                                                    res_transaction.ContactNumber = res.ContactNumber;
                                                    res_transaction.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                                    res_transaction.Amount = Convert.ToDecimal(TransactionCommissionRefferal);
                                                    res_transaction.NetAmount = res_transaction.Amount;
                                                    res_transaction.CurrentBalance = Convert.ToDecimal(res.TotalAmount + (decimal)(TransactionCommissionRefferal));
                                                    res_transaction.VendorTransactionId = TransactionUniqueID;
                                                    res_transaction.CreatedBy = CreatedBy;
                                                    res_transaction.CreatedByName = CreatedByName;
                                                    res_transaction.TransactionUniqueId = TransactionUniqueID;
                                                    res_transaction.ParentTransactionId = CommissionFromTransactionID;
                                                    res_transaction.Remarks = CommissionMessageLog;
                                                    res_transaction.Type = VendorAPIType;
                                                    res_transaction.Description = CommissionMessageLog;
                                                    res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                                    res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                                    res_transaction.Reference = ReferenceNo;
                                                    res_transaction.IsApprovedByAdmin = true;
                                                    res_transaction.IsActive = true;
                                                    res_transaction.CashBack = Convert.ToDecimal(TransactionCommissionRefferal);
                                                    res_transaction.NetAmount = res_transaction.Amount;
                                                    res_transaction.RecieverId = refres_Parent.MemberId;
                                                    res_transaction.RecieverContactNumber = refres_Parent.ContactNumber;
                                                    res_transaction.RecieverName = refres_Parent.FirstName + " " + refres_Parent.MiddleName + " " + refres_Parent.LastName;
                                                    res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                                                    res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                                    res_transaction.DeviceCode = devicecode;
                                                    res_transaction.Platform = platform;
                                                    res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                                    bool ChildCommissionUpdate = false;
                                                    // DISTRIBUTE Transaction COMMISION TO PARENT
                                                    ChildCommissionUpdate = res_transaction.Add();
                                                    if (ChildCommissionUpdate)
                                                    {
                                                        Models.Common.Common.AddLogs(CommissionMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", ismobile, platform, devicecode, (int)AddLog.LogActivityEnum.First_Transaction_Commission);
                                                        resFlag = ChildCommissionUpdate;
                                                    }

                                                    // ***************************************************************************
                                                    // *************** DISTRIBUTE TRANSACTION REWARD POINTS ***************
                                                    // ***************************************************************************

                                                    if (TransactionRewardPointsRefferal != 0)
                                                    {
                                                        string RewardPointMessageLog = $"Earned MyPay Coin {TransactionRewardPointsRefferal} From First TransactionId {CommissionFromTransactionID} ";
                                                        RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                                                        res_rewardpoint.MemberId = res.MemberId;
                                                        res_rewardpoint.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                                        res_rewardpoint.Amount = Convert.ToDecimal(TransactionRewardPointsRefferal);
                                                        res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                                                        res_rewardpoint.ParentTransactionId = CommissionFromTransactionID;
                                                        res_rewardpoint.CurrentBalance = Convert.ToDecimal(res.TotalRewardPoints) + TransactionRewardPointsRefferal;
                                                        res_rewardpoint.CreatedBy = CreatedBy;
                                                        res_rewardpoint.CreatedByName = CreatedByName;
                                                        res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                                                        res_rewardpoint.Remarks = RewardPointMessageLog;
                                                        res_rewardpoint.Type = (int)RewardPointTransactions.RewardTypes.Transaction;
                                                        res_rewardpoint.VendorServiceID = VendorAPIType;
                                                        res_rewardpoint.Description = RewardPointMessageLog;
                                                        res_rewardpoint.Status = 1;
                                                        res_rewardpoint.Reference = ReferenceNo;
                                                        res_rewardpoint.IsApprovedByAdmin = true;
                                                        res_rewardpoint.IsActive = true;
                                                        res_rewardpoint.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Credit);
                                                        bool ChildRewardPointUpdate = false;
                                                        // DISTRIBUTE TRANSACTION  REWARD POINTS TO NEW USER
                                                        ChildRewardPointUpdate = res_rewardpoint.Add();
                                                        if (ChildRewardPointUpdate)
                                                        {
                                                            Models.Common.Common.AddLogs(RewardPointMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", ismobile, platform, devicecode);
                                                            resFlag = ChildRewardPointUpdate;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            return resFlag;
        }

        public static bool SignUpBonusDistribution(string platform, string devicecode, bool ismobile, AddUser res, string authenticationToken)
        {
            bool resFlag = false;
            if (res.RoleId == (int)AddUser.UserRoles.User)
            {
                decimal SignUpBonus = 0;
                decimal SignUpBonusRewardPoints = 0;
                // *****************************   CHECK IF NEW REGISTERATION COMMISSION ALREADY DISTRIBUTED OR NOT   ****************************
                AddTransactionSumCount outobjectTrans = new AddTransactionSumCount();
                GetTransactionCount inobjectTrans = new GetTransactionCount();
                int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Signup_Bonus;
                inobjectTrans.MemberId = Convert.ToInt64(res.MemberId);
                inobjectTrans.Type = VendorAPIType;
                inobjectTrans.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                AddTransactionSumCount modelTrans = RepCRUD<GetTransactionCount, AddTransactionSumCount>.GetRecord(Common.StoreProcedures.sp_WalletTransactions_Count, inobjectTrans, outobjectTrans);
                if (modelTrans != null)
                {
                    if ((modelTrans.Count == 0))
                    {
                        AddSettings outrefobjectSettings = new AddSettings();
                        GetSettings inrefobjectSettings = new GetSettings();
                        List<AddSettings> refres_settings = RepCRUD<GetSettings, AddSettings>.GetRecordList(Common.StoreProcedures.sp_Settings_Get, inrefobjectSettings, outrefobjectSettings);
                        if (refres_settings != null)
                        {
                            // NEW USER SignUpBonus
                            AddSettings objReferal = refres_settings.Where(x => x.Type == (int)AddSettings.ReferType.Refferal && x.GenderType == 0 && x.IsKYCApproved == (int)AddSettings.KycTypes.NotVerified).FirstOrDefault();
                            if (objReferal != null)
                            {
                                SignUpBonus = objReferal.SignUpBonus;
                                SignUpBonusRewardPoints = objReferal.SignUpBonusRewardPoint;
                            }
                        }
                        // ***************************************************************************
                        // *************** DISTRIBUTE SIGNUPBONUS   ***************
                        // ***************************************************************************

                        string CommissionMessageLog = $"SignUp Bonus of Rs. {SignUpBonus} for new registration ";
                        Guid referenceGuid = Guid.NewGuid();
                        string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                        string ReferenceNo = referenceGuid.ToString();
                        WalletTransactions res_transaction = new WalletTransactions();

                        // DISTRIBUTE SIGNUP BONUS TO NEW USER
                        if (SignUpBonus > 0)
                        {
                            res_transaction.MemberId = res.MemberId;
                            res_transaction.ContactNumber = res.ContactNumber;
                            res_transaction.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                            res_transaction.Amount = Convert.ToDecimal(SignUpBonus);
                            res_transaction.VendorTransactionId = TransactionUniqueID;
                            res_transaction.ParentTransactionId = String.Empty;
                            res_transaction.CurrentBalance = Convert.ToDecimal(SignUpBonus);
                            res_transaction.CreatedBy = CreatedBy;
                            res_transaction.CreatedByName = CreatedByName;
                            res_transaction.TransactionUniqueId = TransactionUniqueID;
                            res_transaction.Remarks = CommissionMessageLog;
                            res_transaction.Type = VendorAPIType;
                            res_transaction.Description = CommissionMessageLog;
                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                            res_transaction.Reference = ReferenceNo;
                            res_transaction.IsApprovedByAdmin = true;
                            res_transaction.IsActive = true;
                            res_transaction.CashBack = Convert.ToDecimal(SignUpBonus);
                            res_transaction.NetAmount = res_transaction.Amount;
                            res_transaction.RecieverId = res.MemberId;
                            res_transaction.RecieverContactNumber = res.ContactNumber;
                            res_transaction.RecieverName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                            res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Reciever;
                            res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                            res_transaction.DeviceCode = devicecode;
                            res_transaction.Platform = platform;
                            res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                            res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                            bool NewUserCommissionUpdate = false;
                            // DISTRIBUTE REGISTRATION SIGNUP BONUS TO NEW USER
                            NewUserCommissionUpdate = res_transaction.Add();
                            if (NewUserCommissionUpdate)
                            {
                                // SEND NOTIFICATION TO NEW USER
                                string Title = "SignUp Bonus";
                                string Message = CommissionMessageLog;
                                Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message);

                                // ********************************************************************************
                                // *************** DISTRIBUTE SIGNUP BONUS REWARD POINTS TO NEW USER ***************
                                // ********************************************************************************
                                if (SignUpBonusRewardPoints != 0)
                                {
                                    string RewardPointMessageLog = $"Earned MyPay Coin {SignUpBonusRewardPoints} from new registration ";
                                    RewardPointTransactions res_rewardpoint = new RewardPointTransactions();
                                    res_rewardpoint.MemberId = res.MemberId;
                                    res_rewardpoint.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                    res_rewardpoint.Amount = Convert.ToDecimal(SignUpBonusRewardPoints);
                                    res_rewardpoint.VendorTransactionId = TransactionUniqueID;
                                    res_rewardpoint.ParentTransactionId = String.Empty;
                                    res_rewardpoint.CurrentBalance = Convert.ToDecimal(SignUpBonusRewardPoints);
                                    res_rewardpoint.CreatedBy = CreatedBy;
                                    res_rewardpoint.CreatedByName = CreatedByName;
                                    res_rewardpoint.TransactionUniqueId = TransactionUniqueID;
                                    res_rewardpoint.Remarks = RewardPointMessageLog;
                                    res_rewardpoint.Type = (int)RewardPointTransactions.RewardTypes.Registration;
                                    res_rewardpoint.Description = RewardPointMessageLog;
                                    res_rewardpoint.Status = 1;
                                    res_rewardpoint.Reference = ReferenceNo;
                                    res_rewardpoint.IsApprovedByAdmin = true;
                                    res_rewardpoint.IsActive = true;
                                    res_rewardpoint.Sign = Convert.ToInt16(RewardPointTransactions.Signs.Credit);
                                    bool NewUserRewardPointUpdate = false;
                                    // DISTRIBUTE SIGNUP BONUS  REWARD POINTS TO NEW USER
                                    NewUserRewardPointUpdate = res_rewardpoint.Add();
                                    if (NewUserRewardPointUpdate)
                                    {
                                        // SEND NOTIFICATION TO NEW USER
                                        Title = "Earned SignUp Bonus Reward point";
                                        Message = RewardPointMessageLog;
                                        Models.Common.Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message);
                                    }
                                }
                            }
                            Models.Common.Common.AddLogs(CommissionMessageLog, false, Convert.ToInt32(AddLog.LogType.User), Convert.ToInt64(res.MemberId), "", ismobile, platform, devicecode, (int)AddLog.LogActivityEnum.Registration_Commission);
                            resFlag = NewUserCommissionUpdate;
                        }
                    }
                }
            }
            return resFlag;
        }
        public static string GetTransactionId_FromCustomerId(Int64 CustomerId, int TransactionType)
        {
            CommonHelpers obj = new CommonHelpers();
            string Result = "";
            string str = $"select TransactionUniqueId from WalletTransactions with(nolock) where CustomerID =  '{CustomerId}' and Type='{TransactionType}' ";
            Result = obj.GetScalarValueWithValue(str);
            return Result;
        }
        public static Int32 GetAdminLoginMemberId()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["AdminRole"])))
            {
                return Convert.ToInt32(HttpContext.Current.Session["AdminRole"]);
            }
            else
            {
                return 0;
            }
        }
        public static bool SetAdminSession(AddAdmin usr)
        {
            if (usr != null)
            {
                //RemoveAdminSession();
                HttpContext.Current.Session["AdminMemberId"] = usr.MemberId;
                HttpContext.Current.Session["AdminUserName"] = usr.FirstName;
                HttpContext.Current.Session["AdminRole"] = usr.RoleId;
                HttpContext.Current.Session["ServiceStatus"] = usr.ServiceStatus;
                HttpContext.Current.Session["PasswordStatus"] = usr.IsPasswordExpired;
                Models.Common.Common.CreatedBy = usr.MemberId;
                Models.Common.Common.CreatedByName = usr.FirstName;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool RemoveAdminSession()
        {

            Models.Common.Common.CreatedBy = 10000;
            Models.Common.Common.CreatedByName = "Admin";
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["AdminMemberId"] = null;
            HttpContext.Current.Session["AdminUserName"] = null;
            HttpContext.Current.Session["AdminRole"] = null;
            if (HttpContext.Current.Request.Cookies["AdminLogin"] != null)
            {
                HttpCookie myCookie = new HttpCookie("AdminLogin");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
                HttpContext.Current.Session["PasswordStatus"] = null;
            }
            return true;
        }
        public static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }

        public static bool EODBalanceADD()
        {

            AddBalanceHistory.UpdateServiceConnection();
            AddBalanceHistory outobject = new AddBalanceHistory();
            GetBalanceHistory inobject = new GetBalanceHistory();
            inobject.CheckTodayDate = DateTime.UtcNow.ToShortDateString();
            AddBalanceHistory res = RepCRUD<GetBalanceHistory, AddBalanceHistory>.GetRecord(Common.StoreProcedures.sp_BalanceHistory_Get, inobject, outobject);
            if (res.Id == 0)
            {
                AddBalanceHistory addhistory = new AddBalanceHistory();
                addhistory.Type = (int)AddBalanceHistory.Types.User;
                addhistory.ActiveUser = AddBalanceHistory.GetTotalActiveUsers();
                addhistory.InActiveUser = AddBalanceHistory.GetTotalInActiveUsers();
                addhistory.TotalBalance = AddBalanceHistory.GetTotalBalance();
                addhistory.UserCount = AddBalanceHistory.GetTotalUsers();
                addhistory.TotalCoinsBalance = AddBalanceHistory.GetTotalCoinsBalance();
                addhistory.UpdatedBy = Common.CreatedBy;
                addhistory.IsActive = true;
                addhistory.IsApprovedByAdmin = true;
                addhistory.CreatedBy = Common.CreatedBy;
                addhistory.CreatedByName = Common.CreatedByName;
                Int64 id = RepCRUD<AddBalanceHistory, GetBalanceHistory>.Insert(addhistory, "balancehistory");
                if (id > 0)
                {
                    Common.AddLogs("EOD Balance Added Successfully (Date:" + DateTime.UtcNow.ToString() + ")", true, (int)AddLog.LogType.User, 10000, "Admin", false, "WEB", System.Web.HttpContext.Current.Request.Browser.Type);
                }
            }
            return true;
        }
        public static bool EODBalanceMerchantADD()
        {

            AddBalanceHistoryMerchant.UpdateServiceConnection();
            AddBalanceHistoryMerchant outobject = new AddBalanceHistoryMerchant();
            GetBalanceHistoryMerchant inobject = new GetBalanceHistoryMerchant();
            inobject.CheckTodayDate = DateTime.UtcNow.ToShortDateString();
            AddBalanceHistoryMerchant res = RepCRUD<GetBalanceHistoryMerchant, AddBalanceHistoryMerchant>.GetRecord(Common.StoreProcedures.sp_BalanceHistoryMerchant_Get, inobject, outobject);
            if (res.Id == 0)
            {
                AddBalanceHistoryMerchant addmhistory = new AddBalanceHistoryMerchant();
                addmhistory.Type = (int)AddBalanceHistoryMerchant.Types.Merchant;
                addmhistory.ActiveMerchant = AddBalanceHistoryMerchant.GetTotalActiveMerchant();
                addmhistory.InActiveMerchant = AddBalanceHistoryMerchant.GetTotalInActiveMerchant();
                addmhistory.TotalBalance = AddBalanceHistoryMerchant.GetTotalBalance();
                addmhistory.MerchantCount = AddBalanceHistoryMerchant.GetTotalMerchant();
                // addhistory.TotalCoinsBalance = AddBalanceHistoryMerchant.GetTotalCoinsBalance();
                addmhistory.UpdatedBy = Common.CreatedBy;
                addmhistory.IsActive = true;
                addmhistory.IsApprovedByAdmin = true;
                addmhistory.CreatedBy = Common.CreatedBy;
                addmhistory.CreatedByName = Common.CreatedByName;
                Int64 id = RepCRUD<AddBalanceHistoryMerchant, GetBalanceHistoryMerchant>.Insert(addmhistory, "balancehistorymerchant");
                if (id > 0)
                {
                    Common.AddLogs("EOD Merchant Balance Added Successfully (Date:" + DateTime.UtcNow.ToString() + ")", true, (int)AddLog.LogType.Merchant, 10000, "Admin", false, "WEB", System.Web.HttpContext.Current.Request.Browser.Type);
                }
            }
            return true;
        }
        public static void ExecuteBulkNotificationCampaignExcel(Int64 NotificationCampaignID)
        {
            AddExcelNotificationCampaignIDs outobjectIDs = new AddExcelNotificationCampaignIDs();
            GetExcelNotificationCampaignIDs inobjectIDs = new GetExcelNotificationCampaignIDs();
            inobjectIDs.NotificationCampaignID = NotificationCampaignID;
            inobjectIDs.ScheduleDateTime = Common.fnGetdatetimeFromInput(System.DateTime.UtcNow);
            AddExcelNotificationCampaignIDs objUpdateNotificationCampaignIDs = RepCRUD<GetExcelNotificationCampaignIDs, AddExcelNotificationCampaignIDs>.GetRecord(Common.StoreProcedures.sp_ExcelNotificationCampaignIDs_Get, inobjectIDs, outobjectIDs);
            if (objUpdateNotificationCampaignIDs != null)
            {
                string DeviceIDs = objUpdateNotificationCampaignIDs.NotificationDeviceIDs;
                Int64 DeviceCount = DeviceIDs.Split(',').Length;
                if (string.IsNullOrEmpty(DeviceIDs))
                {
                    DeviceCount = 0;
                }
                #region
                if (DeviceCount > 0)
                {
                    // Send Bulk Notification Code goes here.
                    SentNotifications.ExecuteBulkNotificationCampaignExcelFromAdmin(objUpdateNotificationCampaignIDs.NotificationCampaignId, objUpdateNotificationCampaignIDs.NotificationDeviceIDs);
                }
                #endregion
                AddNotificationCampaignExcel outobject = new AddNotificationCampaignExcel();
                GetNotificationCampaignExcel inobject = new GetNotificationCampaignExcel();
                inobject.Id = NotificationCampaignID;
                AddNotificationCampaignExcel objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject, outobject);
                if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0)
                {
                    objUpdateNotificationCampaign.SentStatus = (int)AddNotificationCampaignExcel.SentStatuses.Progress;
                    objUpdateNotificationCampaign.TotalNotificationSent = objUpdateNotificationCampaign.TotalNotificationSent + DeviceCount;
                    objUpdateNotificationCampaign.CurrentSkipIndex = objUpdateNotificationCampaignIDs.OffsetValue + objUpdateNotificationCampaignIDs.PagingSize;
                    if (DeviceCount == 0)
                    {
                        objUpdateNotificationCampaign.IsCompleted = 1;
                        objUpdateNotificationCampaign.SentStatus = (int)AddNotificationCampaignExcel.SentStatuses.Sent;
                    }
                    bool IsUpdated = RepCRUD<AddNotificationCampaignExcel, GetNotificationCampaignExcel>.Update(objUpdateNotificationCampaign, "notificationcampaignexcel");
                }
            }
        }

        public static bool BulkExcelNotificationScheduler()
        {

            
            AddNotificationCampaignExcel outobject = new AddNotificationCampaignExcel();
            GetNotificationCampaignExcel inobject = new GetNotificationCampaignExcel();
            inobject.CheckSentStatus = 2;
            inobject.CheckCompleted = 0;
            inobject.CheckDelete = 0;
            inobject.ScheduleDateTime = Common.fnGetdatetimeFromInput(System.DateTime.UtcNow);
            AddNotificationCampaignExcel objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaignExcel, AddNotificationCampaignExcel>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobject, outobject);
            if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0)
            {
                ExecuteBulkNotificationCampaignExcel(objUpdateNotificationCampaign.Id);
            }
            return true;
        }

        public static bool ServiceActivityMonitor()
        {
            return Common.CheckServiceActivityMonitor();
        }

        public static string GenerateBankContactNumber()
        {
            int Count = 0;
            try
            {

                Count = Convert.ToInt32((new CommonHelpers()).GetScalarValueWithValue($"select  count(0) from [Merchant] where MerchantType = 1  or MerchantType = 3"));

            }
            catch (Exception ex)
            {

            }

            return Count.ToString();
        }
        public static bool ReorganizeIndexScheduler()
        {
            string val = (new CommonHelpers()).GetScalarValueWithValue("EXEC sp_ReorganizeIndex_Scheduler");
            return true;
        }
        public static bool CheckServiceActivityMonitor()
        {
            bool flag = false;
            try
            {
                //if (Common.ApplicationEnvironment.IsProduction == false)
                //{
                //    flag = true;
                //}
                //else
                if (Common.ApplicationEnvironment.IsProduction)
                {
                    Int64 val = Convert.ToInt64((new CommonHelpers()).GetScalarValueWithValue("EXEC sp_ServiceActivityMonitor_Scheduler"));
                    if (val == 0)
                    {
                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/ServiceActivityMonitor.html"));
                        string body = mystring;

                        string Subject = MyPay.Models.Common.Common.WebsiteName + " Service Activity Monitor ";
                        if (!string.IsNullOrEmpty(Common.FromEmail))
                        {
                            MyPay.Models.Common.Common.SendAsyncMail(Common.AdminEmail, Subject, body, "", Common.BCC, Common.BCC2);
                        }
                        Common.AddLogs($"Service Activity Monitor Executed Successfully at {System.DateTime.UtcNow}", false, Convert.ToInt32(AddLog.LogType.Maintenance));
                    }
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs($"Service Activity Monitor Failed at {System.DateTime.UtcNow} : {ex.Message}", false, Convert.ToInt32(AddLog.LogType.Maintenance));
            }
            return flag;
        }
        public static bool BulkNotificationScheduler()
        {
            AddNotificationCampaign outobject = new AddNotificationCampaign();
            GetNotificationCampaign inobject = new GetNotificationCampaign();
            inobject.CheckSentStatus = 2;
            inobject.CheckCompleted = 0;
            inobject.CheckDelete = 0;
            inobject.ScheduleDateTime = Common.fnGetdatetimeFromInput(System.DateTime.UtcNow);
            AddNotificationCampaign objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
            if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0)
            {
                ExecuteBulkNotificationCampaign(objUpdateNotificationCampaign.Id, objUpdateNotificationCampaign.Province, objUpdateNotificationCampaign.District);

            }
            return true;
        }
        public static void ExecuteBulkNotificationCampaign(Int64 NotificationCampaignID,string province,string district)
        {
            AddNotificationCampaignIDs outobjectIDs = new AddNotificationCampaignIDs();
            Get.GetNotificationCampaignIDs inobjectIDs = new Get.GetNotificationCampaignIDs();
            inobjectIDs.NotificationCampaignID = NotificationCampaignID;
            inobjectIDs.ScheduleDateTime = Common.fnGetdatetimeFromInput(System.DateTime.UtcNow);
            inobjectIDs.Province = province;
            inobjectIDs.District = district;
            AddNotificationCampaignIDs objUpdateNotificationCampaignIDs = RepCRUD<Get.GetNotificationCampaignIDs, AddNotificationCampaignIDs>.GetRecord(Common.StoreProcedures.sp_NotificationCampaignIDs_Get, inobjectIDs, outobjectIDs);
            if (objUpdateNotificationCampaignIDs != null)
            {
                string DeviceIDs = objUpdateNotificationCampaignIDs.NotificationDeviceIDs;
                
                Int64 DeviceCount = DeviceIDs.Split(',').Length;
                if (string.IsNullOrEmpty(DeviceIDs))
                {
                    DeviceCount = 0;
                }
                
                #region
                if (DeviceCount > 0)
                {
                    // Send Bulk Notification Code goes here.
                    SentNotifications.ExecuteBulkNotificationFromAdmin(objUpdateNotificationCampaignIDs.NotificationCampaignId, objUpdateNotificationCampaignIDs.NotificationDeviceIDs, objUpdateNotificationCampaignIDs.Province, objUpdateNotificationCampaignIDs.District);
                }
                #endregion
                AddNotificationCampaign outobject = new AddNotificationCampaign();
                GetNotificationCampaign inobject = new GetNotificationCampaign();
                inobject.Id = NotificationCampaignID;
                
                AddNotificationCampaign objUpdateNotificationCampaign = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecord(Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
                if (objUpdateNotificationCampaign != null && objUpdateNotificationCampaign.Id > 0)
                {
                    objUpdateNotificationCampaign.SentStatus = (int)AddNotificationCampaign.SentStatuses.Progress;
                    objUpdateNotificationCampaign.TotalNotificationSent = objUpdateNotificationCampaign.TotalNotificationSent + DeviceCount;
                    objUpdateNotificationCampaign.CurrentSkipIndex = objUpdateNotificationCampaignIDs.OffsetValue + objUpdateNotificationCampaignIDs.PagingSize;
                    if (DeviceCount == 0)
                    {
                        objUpdateNotificationCampaign.IsCompleted = 1;
                        objUpdateNotificationCampaign.SentStatus = (int)AddNotificationCampaign.SentStatuses.Sent;
                    }
                    bool IsUpdated = RepCRUD<AddNotificationCampaign, Get.GetNotificationCampaign>.Update(objUpdateNotificationCampaign, "notificationcampaign");
                }
            }
        }

        public static Byte[] GetQRReferCode(AddUser model)
        {
            //string s = "Hi! Join MyPay and earn up to Rs. 100 on signup using referral code - " + model.RefCode.ToString() + ". You will also get a chance to earn every time on successful referral registration.";
            string s = model.MemberId.ToString();
            return GetQR(s);
        }
        public static Byte[] GetQRContactNumber(string ContactNumber)
        {
            //string s = "Hi! Join MyPay and earn up to Rs. 100 on signup using referral code - " + model.RefCode.ToString() + ". You will also get a chance to earn every time on successful referral registration.";
            string s = ContactNumber;
            return GetQR(s);
        }
        public static Byte[] GetQRMemberID_Encrypted(string MemberID)
        {
            //string s = "Hi! Join MyPay and earn up to Rs. 100 on signup using referral code - " + model.RefCode.ToString() + ". You will also get a chance to earn every time on successful referral registration.";
            string s = Common.EncryptString(MemberID);
            return GetQR(s);
        }
        public static Byte[] GetQR(string s)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(s, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        public static AddCalculateServiceChargeAndCashback CalculateNetAmountWithServiceCharge(string MemberId, string TotalAmount, string ServiceId)
        {
            AddCalculateServiceChargeAndCashback objOut;
            AddCalculateServiceChargeAndCashback outobjectCashback = new AddCalculateServiceChargeAndCashback();
            GetCalculateServiceChargeAndCashback inobjectCashback = new GetCalculateServiceChargeAndCashback();
            inobjectCashback.MemberId = Convert.ToInt32(MemberId);
            inobjectCashback.Amount = Convert.ToDecimal(TotalAmount);
            inobjectCashback.ServiceId = Convert.ToInt32(ServiceId);
            objOut = RepCRUD<GetCalculateServiceChargeAndCashback, AddCalculateServiceChargeAndCashback>.GetRecord(Common.StoreProcedures.sp_CalculateServiceChargeAndCashback_Get, inobjectCashback, outobjectCashback);
            return objOut;
        }
        public static AddCalculateServiceChargeAndCashback CalculateNetAmountWithServiceChargeMerchant(string MerchantUniqueId, string TotalAmount, string ServiceId)
        {
            AddCalculateServiceChargeAndCashback objOut = new AddCalculateServiceChargeAndCashback();
            objOut.MerchantUniqueId = Convert.ToString(MerchantUniqueId);
            objOut.Amount = Convert.ToDecimal(TotalAmount);
            objOut.ServiceId = Convert.ToInt32(ServiceId);
            objOut.CalculateServiceChargeAndCashbackMerchant();
            return objOut;
        }

        public static AddRemittanceCalculateServiceCharge RemittanceCalculateServiceCharge(Int64 SourceCurrencyId, decimal TotalAmount, Int64 DestinationCurrencyId)
        {
            AddRemittanceCalculateServiceCharge objOut = new AddRemittanceCalculateServiceCharge();
            objOut.SourceCurrencyId = Convert.ToInt32(SourceCurrencyId);
            objOut.Amount = Convert.ToDecimal(TotalAmount);
            objOut.DestinationCurrencyId = Convert.ToInt32(DestinationCurrencyId);
            objOut.GetRecord();
            return objOut;
        }


        public static AddRemittanceCalculateServiceCharge RemittanceCalculateServiceCharge(string SourceCurrency, decimal TotalAmount, string DestinationCurrency)
        {
            AddRemittanceCalculateServiceCharge objOut = new AddRemittanceCalculateServiceCharge();
            objOut.SourceCurrency = SourceCurrency;
            objOut.Amount = Convert.ToDecimal(TotalAmount);
            objOut.DestinationCurrency = DestinationCurrency;
            objOut.GetRecord();
            return objOut;
        }
        public static string CheckTransactionLimit(Int32 ServiceTypeId, Int32 TransactionTransferType, Int64 MemberId, decimal TotalAmount, int Sign = 0)
        {
            AddTransactionLimit objOut = new AddTransactionLimit();
            return objOut.GetTransactionLimit(ServiceTypeId, TransactionTransferType, MemberId, TotalAmount, Sign);
        }
        public static async Task<int> UpdateDataRunningOperationAsync() // assume we return an int from this long running operation 
        {
            string URL = string.Empty;
            WebClient Detail = new WebClient();
            if (Common.ApplicationEnvironment.IsProduction)
            {
                URL = Common.LiveSiteUrl + "AdminLogin/UpdateData";
            }
            else
            {
                URL = Common.TestSiteUrl + "AdminLogin/UpdateData";

            }
            await Detail.DownloadStringTaskAsync(new Uri(URL));
            return 1;
        }
        public static bool CheckTransactionStatusLookup()
        {
            string result = string.Empty;
            WalletTransactions objtrans = new WalletTransactions();
            objtrans.VendorType = 1;
            objtrans.Status = 2;
            objtrans.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_khanepani;
            GetVendor_API_TransactionLookup objRes = new GetVendor_API_TransactionLookup();
            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

            if (objtrans.GetRecord())
            {
                RepKhalti.RequestTransactionLookup(objtrans.TransactionUniqueId, objtrans.Reference, objtrans.Type.ToString(), objtrans.MemberId.ToString(), authenticationToken, "", "1.0", "WebService", "Web", ref objRes, ref objVendor_API_Requests);
            }
            return true;
        }
        public static bool InvalidOTPUpdate(ref AddUserOTPAttempt objOTPAttemp, string PhoneNumber, Int64 MemberId = 0)
        {
            bool res = false;
            try
            {
                if (objOTPAttemp.Id != 0)
                {
                    objOTPAttemp.AttemptCount = objOTPAttemp.AttemptCount + 1;
                    objOTPAttemp.AttemptDateTime = System.DateTime.UtcNow;
                    objOTPAttemp.Update();
                }
                else
                {
                    objOTPAttemp.AttemptCount = 1;
                    objOTPAttemp.AttemptDateTime = System.DateTime.UtcNow;
                    objOTPAttemp.Add();
                }
                res = true;
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public static string GetWebToken()
        {
            DateTime _ExpirationTime = DateTime.UtcNow.AddMinutes(60);
            string token = GetToken("Web", Common.WebPassword, 0, ref _ExpirationTime);
            return token;
        }
        public static string RequestMyPayAPI(string APIName, string JsonReq, string UToken)
        {
            string URL = string.Empty;
            if (Common.ApplicationEnvironment.IsProduction)
            {
                URL = Common.LiveApiUrl;
            }
            else
            {
                URL = Common.TestApiUrl;
            }

            string Result = string.Empty;
            try
            {
                string AuthToken = GetWebToken();
                Result = VendorApi_CommonHelper.MyPayWebPostMethod(URL + APIName, JsonReq, UToken, AuthToken);
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public static string RequestCableCarAPI(string APIName, string JsonReq, string UToken)
        {
            string URL = string.Empty;
            if (Common.ApplicationEnvironment.IsProduction)
            {
                URL = Common.LiveApiUrl;
            }
            else
            {
                URL = Common.TestApiUrl;
            }

            string Result = string.Empty;
            try
            {
                string AuthToken = GetWebToken();
                Result = VendorApi_CommonHelper.MyPayWebPostMethod(URL + APIName, JsonReq, UToken, AuthToken);
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public static string GetCableCarAPI(string APIName, string JsonReq, string UToken)
        {
            string URL = string.Empty;
            if (Common.ApplicationEnvironment.IsProduction)
            {
                URL = Common.LiveApiUrl;
            }
            else
            {
                URL = Common.TestApiUrl;
            }

            string Result = string.Empty;
            try
            {
                string AuthToken = GetWebToken();
                Result = VendorApi_CommonHelper.MyPayWebPostMethod(URL + APIName, JsonReq, UToken, AuthToken);
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public static string RequestBusSewarAPI(string APIName, string JsonReq, string UToken)
        {
            string URL = string.Empty;
            if (Common.ApplicationEnvironment.IsProduction)
            {
                URL = Common.LiveApiUrl;
            }
            else
            {
                URL = Common.TestApiUrl;
            }

            string Result = string.Empty;
            try
            {
                string AuthToken = GetWebToken();
                Result = VendorApi_CommonHelper.MyPayWebPostMethod(URL + APIName, JsonReq, UToken, AuthToken);
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public static string RequestMeroTvAPI(string APIName, string JsonReq, string UToken)
        {
            string URL = string.Empty;
            if (Common.ApplicationEnvironment.IsProduction)
            {
                URL = Common.LiveApiUrl;
            }
            else
            {
                URL = Common.TestApiUrl;
            }

            string Result = string.Empty;
            try
            {
                string AuthToken = GetWebToken();
                Result = VendorApi_CommonHelper.MyPayWebPostMethod(URL + APIName, JsonReq, UToken, AuthToken);
            }
            catch (Exception ex)
            {

            }
            return Result;
        }
        public static string GetToken(string username, string pwd, long memberid, ref DateTime _ExpirationTime)
        {
            string key = "mypayappsecurecred495a"; //Secret
            var issuer = "http://mypayappwebsite.com";
            _ExpirationTime = DateTime.UtcNow.AddMinutes(60);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("username", username));
            permClaims.Add(new Claim("password", pwd));
            permClaims.Add(new Claim("memberid", memberid.ToString()));
            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: _ExpirationTime,
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }

        public static string GetServiceUrl(string ServiceId)
        {
            string Url = "";
            try
            {
                Url = (new CommonHelpers()).GetScalarValueWithValue($"select WebURL  from ProviderLogosList where ProviderTypeId = '{ServiceId}'");

            }
            catch (Exception ex)
            {
            }

            return Url;
        }


        public static bool GetDeviceActiveStatus(Int64 MemberId, string DeviceId)
        {
            bool data = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "SELECT count(0) FROM UsersDeviceRegistration with(nolock) where MemberId='" + MemberId.ToString() + "'   and DeviceCode = '" + DeviceId + "'  and IsActive=1 and IsDeleted=0";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = true;
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public static bool GetDeviceLogoutStatus(Int64 MemberId, string DeviceCode)
        {
            bool data = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string DisabledFromDeviceCode = string.Empty;
                string str = "SELECT DisabledFromDeviceCode FROM UsersDeviceRegistration with(nolock) where MemberId='" + MemberId.ToString() + "'   and DeviceCode = '" + DeviceCode + "'  and IsActive=0 and IsDeleted=1";
                DisabledFromDeviceCode = obj.GetScalarValueWithValue(str);
                if (string.IsNullOrEmpty(DisabledFromDeviceCode) || DisabledFromDeviceCode == DeviceCode)
                {
                    data = false;
                }
                else
                {
                    data = true;
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        public static class ApplicationEnvironment
        {
            public static bool IsProduction { get { return System.Configuration.ConfigurationManager.AppSettings["IsProduction"] == "1"; } }
            public static bool IsDevelopmentMachine { get { return System.Configuration.ConfigurationManager.AppSettings["IsDevelopmentMachine"] == "1"; } }
            public static string ResendOTPTime { get { return System.Configuration.ConfigurationManager.AppSettings["ResendOTPTime"].ToString(); } }
            public static string AdminEmail { get { return System.Configuration.ConfigurationManager.AppSettings["AdminEmail"].ToString(); } }
            public static string BCC { get { return System.Configuration.ConfigurationManager.AppSettings["BCC"].ToString(); } }
            public static string BCC2 { get { return System.Configuration.ConfigurationManager.AppSettings["BCC2"].ToString(); } }
            public static string DuplicateMinutesCheck { get { return System.Configuration.ConfigurationManager.AppSettings["DuplicateMinutesCheck"].ToString(); } }
        }
        public static class StoreProcedures
        {
            public static string sp_GetFlightSwitchSettings { get { return "sp_GetFlightSwitchSettings"; } }
            
            public static string sp_FlightBookingDetails_Get_plasma { get { return "sp_FlightBookingDetails_Get_plasma"; } }
            public static string sp_PlasmaTechIssueTicketResDetail { get { return "sp_PlasmaTechIssueTicketResDetail"; } }
            public static string UserDetail_Get { get { return "UserDetail_Get"; } }

            public static string sp_RemittanceAPIRequest_Get { get { return "sp_RemittanceAPIRequest_Get"; } }

            public static string sp_APIDealsandOffers_Get { get { return "sp_APIDealsandOffers_Get"; } }
            public static string sp_DealsAndOffers_Get { get { return "sp_DealsAndOffers_Get"; } }
            public static string sp_RemittanceIPAddress_Get { get { return "sp_RemittanceIPAddress_Get"; } }
            public static string sp_RemittanceCommission_Get { get { return "sp_RemittanceCommission_Get"; } }
            public static string sp_RemittanceDashboard_Get { get { return "sp_RemittanceDashboard_Get"; } }
            public static string sp_Coupons_Get { get { return "sp_Coupons_Get"; } }
            public static string sp_CalculateBalance_From_Date { get { return "sp_CalculateBalance_From_Date"; } }
            public static string sp_CalculateBalanceMerchant_From_Date { get { return "sp_CalculateBalanceMerchant_From_Date"; } } 
            public static string sp_VotingCandidate_Rank_Update { get { return "sp_VotingCandidate_Rank_Update"; } }
            public static string sp_MerchantWithdrawalRequest_Get { get { return "sp_MerchantWithdrawalRequest_Get"; } }
            public static string sp_ExportData_Get { get { return "sp_ExportData_Get"; } }
            public static string sp_Country_Get { get { return "sp_Country_Get"; } }
            public static string sp_Menus_Get { get { return "sp_Menus_Get"; } }
            public static string sp_Merchant_Get { get { return "sp_Merchant_Get"; } }
            public static string sp_MerchantIPAddress_Get { get { return "sp_MerchantIPAddress_Get"; } }
            public static string sp_MenusAssign_Get { get { return "sp_MenusAssign_Get"; } }
            public static string sp_Role_Get { get { return "sp_Role_Get"; } }
            public static string sp_KYCRemarks_Get { get { return "sp_KYCRemarks_Get"; } }
            public static string sp_TicketImages_Get { get { return "sp_TicketImages_Get"; } }
            public static string sp_Tickets_Get { get { return "sp_Tickets_Get"; } }
            public static string sp_TicketsReply_Get { get { return "sp_TicketsReply_Get"; } }
            public static string sp_TicketsCategory_Get { get { return "sp_TicketsCategory_Get"; } }
            public static string sp_TicketRecordDetail_Get { get { return "sp_TicketRecordDetail_Get"; } }
            public static string sp_Notification_Get { get { return "sp_Notification_Get"; } }
            public static string sp_TransactionLimit_Datatable { get { return "sp_TransactionLimit_Datatable"; } }
            public static string sp_TransactionLimitCheck_Get { get { return "sp_TransactionLimitCheck_Get"; } }
            public static string sp_TransactionLimit_Get { get { return "sp_TransactionLimit_Get"; } }
            public static string sp_CommissionUpdateHistory_Get { get { return "sp_CommissionUpdateHistory_Get"; } }
            public static string sp_RequestFund_Get { get { return "sp_RequestFund_Get"; } }
            public static string sp_UserBankDetail_Get { get { return "sp_UserBankDetail_Get"; } }
            public static string sp_RedeemPoints_Get { get { return "sp_RedeemPoints_Get"; } }
            public static string sp_RewardPointTransactions_Get { get { return "sp_RewardPointTransactions_Get"; } }
            public static string sp_NotificationCampaignIDs_Get { get { return "sp_NotificationCampaignIDs_Get"; } }
            public static string sp_NotificationCampaign_Get { get { return "sp_NotificationCampaign_Get"; } }
            public static string sp_AdminDashboard_Get { get { return "sp_AdminDashboard_Get"; } }
            public static string sp_CouponsScratched_Get { get { return "sp_CouponsScratched_Get"; } }
            public static string sp_MerchantDashboard_Get { get { return "sp_MerchantDashboard_Get"; } }
            public static string sp_CurrencyList_Get { get { return "sp_CurrencyList_Get"; } }
            public static string sp_MerchantOrders_Get { get { return "sp_MerchantOrders_Get"; } }
            public static string sp_ApiSettingsHistory_Get { get { return "sp_ApiSettingsHistory_Get"; } }
            public static string sp_logs_Get { get { return "sp_logs_Get"; } }
            public static string sp_Occupation_Get { get { return "sp_Occupation_Get"; } }
            public static string sp_State_Get { get { return "sp_State_Get"; } }
            public static string sp_InsuranceDetail_Get { get { return "sp_InsuranceDetail_Get"; } }
            public static string sp_UserAuthorization_Get { get { return "sp_UserAuthorization_Get"; } }
            public static string sp_FlightBookingDetails_Get { get { return "sp_FlightBookingDetails_Get"; } }
            public static string sp_FlightPassengersDetails_Get { get { return "sp_FlightPassengersDetails_Get"; } }
            public static string sp_UserDocuments_Get { get { return "sp_UserDocuments_Get"; } }
            public static string sp_Users_Get { get { return "sp_Users_Get"; } }
            public static string sp_DataPackDetail_Get { get { return "sp_DataPackDetail_Get"; } }
            public static string sp_Users_GetAdmin { get { return "sp_Users_GetAdmin"; } }
            public static string sp_ShareReferLink_Get { get { return "sp_ShareReferLink_Get"; } }
            public static string sp_DashboardChart_Get { get { return "sp_DashboardChart_Get"; } }
            public static string sp_UsersKYC_DatatableCounter { get { return "sp_UsersKYC_DatatableCounter"; } }
            public static string sp_UsersDeviceRegistration_Check { get { return "sp_UsersDeviceRegistration_Check"; } }
            public static string sp_UsersDeviceRegistration_Get { get { return "sp_UsersDeviceRegistration_Get"; } }
            public static string sp_ReferEarnImage_Get { get { return "sp_ReferEarnImage_Get"; } }
            public static string sp_OfferBanners_Get { get { return "sp_OfferBanners_Get"; } }
            public static string sp_Users_Get_all { get { return "sp_Users_Get_all"; } }
            public static string sp_verification_Get { get { return "sp_verification_Get"; } }
            public static string sp_WalletTransactions_AddNew { get { return "sp_WalletTransactions_AddNew"; } }
            public static string sp_WalletTransactions_Get { get { return "sp_WalletTransactions_Get"; } }
            public static string sp_WalletTransactions_Count { get { return "sp_WalletTransactions_Count"; } }
            public static string sp_WalletTransactions_Update { get { return "sp_WalletTransactions_Update"; } }
            public static string sp_AdminUser_Get { get { return "sp_AdminUser_Get"; } }

            public static string sp_BalanceHistory_Get { get { return "sp_BalanceHistory_Get"; } }
            public static string sp_BalanceHistoryMerchant_Get { get { return "sp_BalanceHistoryMerchant_Get"; } }
            public static string sp_Province_Get { get { return "sp_Province_Get"; } }
            public static string sp_District_Get { get { return "sp_District_Get"; } }
            public static string sp_LocalLevel_Get { get { return "sp_LocalLevel_Get"; } }
            public static string sp_DepositOrders_Get { get { return "sp_DepositOrders_Get"; } }
            public static string sp_MerchantBankDetail_Get { get { return "sp_MerchantBankDetail_Get"; } }
            public static string sp_BankList_Get { get { return "sp_BankList_Get"; } }
            public static string sp_Settings_Get { get { return "sp_Settings_Get"; } }
            public static string sp_Commission_Get { get { return "sp_Commission_Get"; } }
            public static string sp_ProviderServiceCategoryList_Get { get { return "sp_ProviderServiceCategoryList_Get"; } }
            public static string sp_Feedback_Get { get { return "sp_Feedback_Get"; } }
            public static string sp_KYCStatusHistory_Get { get { return "sp_KYCStatusHistory_Get"; } }
            public static string sp_ProviderLogoList_Get { get { return "sp_ProviderLogoList_Get"; } }
            public static string sp_VendorAPIRequest_Get { get { return "sp_VendorAPIRequest_Get"; } }
            public static string sp_CalculateServiceChargeAndCashback_Get { get { return "sp_CalculateServiceChargeAndCashback_Get"; } }
            public static string sp_CalculateServiceChargeAndCashbackMerchant_Get { get { return "sp_CalculateServiceChargeAndCashbackMerchant_Get"; } }
            public static string sp_VotingCandidate_Get { get { return "sp_VotingCandidate_Get"; } }
            public static string sp_VotingCompetition_Get { get { return "sp_VotingCompetition_Get"; } }
            public static string sp_VotingPackages_Get { get { return "sp_VotingPackages_Get"; } }
            public static string sp_VotingList_Get { get { return "sp_VotingList_Get"; } }
            public static string sp_Marque_Get { get { return "sp_Marque_Get"; } }
            public static string sp_BankTransactions_Get { get { return "sp_BankTransactions_Get"; } }
            public static string sp_Purpose_Get { get { return "sp_Purpose_Get"; } }
            public static string sp_UserInActiveRemarks_Get { get { return "sp_UserInActiveRemarks_Get"; } }
            public static string sp_Users_GetByPhoneNumber { get { return "sp_Users_GetByPhoneNumber"; } }
            public static string sp_Users_GetLoginWithPin { get { return "sp_Users_GetLoginWithPin"; } }
            public static string sp_Users_GetBasicInfo { get { return "sp_Users_GetBasicInfo"; } }

            public static string sp_NotificationCampaignExcel_Get { get { return "sp_NotificationCampaignExcel_Get"; } }
            public static string sp_ExcelNotificationCampaignIDs_Get { get { return "sp_ExcelNotificationCampaignIDs_Get"; } }
            public static string sp_EstatementPDFToken_Get { get { return "sp_EstatementPDFToken_Get"; } }
            public static string sp_AdminLoginPasswordExpire_Update { get { return "sp_AdminLoginPasswordExpire_Update"; } }
            public static string sp_CouponsHistory_Get { get { return "sp_CouponsHistory_Get"; } }
            public static string sp_EventDetailsExpire_Update { get { return "sp_EventDetailsExpire_Update"; } }
            public static string sp_GetFlightPassengersDetails { get { return "sp_GetFlightPassengersDetails"; } }
            public static string sp_GetPlasmaTechIssueTicketResDetails { get { return "sp_GetPlasmaTechIssueTicketResDetails"; } }
            public static string CableCar { get { return "CableCar"; } }
            public static string sp_Get_AgentBankList { get { return "sp_Get_AgentBankList"; } }
            public static string Usp_AgentUserList { get { return "Usp_AgentUserList"; } }
            public static string sp_AirlineList_Get { get { return "sp_AirlineList_Get"; } }
            public static string sp_SectorList_Get { get { return "sp_SectorList_Get"; } }
            public static string sp_AddAirlineCommision { get { return "sp_AddAirlineCommision"; } }
            public static string sp_AirlinesCommission { get { return "sp_AirlinesCommission"; } }

        }

        public static class CommonMessage
        {
            public static string success { get { return "success"; } }
            public static string MemberId_Not_Found { get { return "MemberId not found"; } }
            public static string Data_Not_Found { get { return "Data not found"; } }
        }
        public static bool SetRemittanceSession(AddRemittanceUser usr)
        {
            if (usr != null)
            {
                HttpContext.Current.Session["RemittanceUniqueId"] = usr.MerchantUniqueId;
                HttpContext.Current.Session["RemittanceUserName"] = usr.UserName;
                HttpContext.Current.Session["RemittanceName"] = usr.ContactName;
                HttpContext.Current.Session["RemittanceStatus"] = usr.IsActive;
                HttpContext.Current.Session["RemittanceRole"] = usr.RoleId;
                HttpContext.Current.Session["RemittancePasswordReset"] = usr.IsPasswordReset;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool RemoveRemittanceSession()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["RemittanceUniqueId"] = null;
            HttpContext.Current.Session["RemittanceUserName"] = null;
            HttpContext.Current.Session["RemittanceName"] = null;
            HttpContext.Current.Session["RemittanceStatus"] = null;
            HttpContext.Current.Session["RemittancePasswordReset"] = null;
            HttpContext.Current.Session["RemittanceRole"] = null;
            return true;
        }
        public static Int32 GetRemittanceLoginMemberId()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["RemittanceRole"])))
            {
                return Convert.ToInt32(HttpContext.Current.Session["RemittanceRole"]);
            }
            else
            {
                return 0;
            }
        }

        public static bool CheckMerchantUserExists(string ContactNumber)
        {
            bool ExistsFlag = true;
            try
            {
                int Count = Convert.ToInt32((new CommonHelpers()).GetScalarValueWithValue($"select  count(0) from Users WITH (NOLOCK)  where ContactNumber = '{ContactNumber}' "));
                if (Count == 0)
                {
                    ExistsFlag = false;
                }
                else
                {
                    ExistsFlag = true;
                }
            }
            catch (Exception ex)
            {
            }

            return ExistsFlag;
        }

        public static bool CheckServiceDown(string ServiceId)
        {
            bool ExistsFlag = true;
            try
            {
                int Count = Convert.ToInt32((new CommonHelpers()).GetScalarValueWithValue($"select convert(int,IsServiceDown)  from ProviderLogosList where ProviderTypeId = '{ServiceId}'"));
                if (Count == 0)
                {
                    ExistsFlag = false;
                }
                else
                {
                    ExistsFlag = true;
                }
            }
            catch (Exception ex)
            {
            }

            return ExistsFlag;
        }

        public static string AdminLoginPasswordExpire()
        {
            string result = "";
            try
            {
                CommonHelpers obj = new CommonHelpers();
                Hashtable HT = new Hashtable();
                string ResultId = obj.ExecuteProcedureGetReturnValue(Common.StoreProcedures.sp_AdminLoginPasswordExpire_Update, HT);
                AddLogs("AdminLoginPasswordExpire Executed Successfully", false, Convert.ToInt32(AddLog.LogType.Maintenance), Common.CreatedBy, Common.CreatedByName, true, "Web", "");
            }
            catch (Exception ex)
            {
                AddLogs("AdminLoginPasswordExpire Failed:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.Maintenance), Common.CreatedBy, Common.CreatedByName, true, "Web", "");
            }
            return result;
        }


        public static string EventDetailsExpireUpdate()
        {
            string result = "";
            try
            {
                CommonHelpers obj = new CommonHelpers();
                Hashtable HT = new Hashtable();
                string ResultId = obj.ExecuteProcedureGetReturnValue(Common.StoreProcedures.sp_EventDetailsExpire_Update, HT);
                AddLogs("EventDetailsExpireUpdate Executed Successfully", false, Convert.ToInt32(AddLog.LogType.DBLogs), Common.CreatedBy, Common.CreatedByName, true, "Web", "");
            }
            catch (Exception ex)
            {
                AddLogs("EventDetailsExpireUpdate Failed:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.DBLogs), Common.CreatedBy, Common.CreatedByName, true, "Web", "");
            }
            return result;
        }
        public static bool CheckBankTransactionExists(Int64 MemberId, string BankTransactionId)
        {
            bool ExistsFlag = true;
            try
            {
                int Count = Convert.ToInt32((new CommonHelpers()).GetScalarValueWithValue($"select count(0) from WalletTransactions  with(nolock) where sign = 2 and ParentTransactionId= '{BankTransactionId}' and MemberId = '{MemberId}' and WalletType = '2'"));
                if (Count == 0)
                {
                    ExistsFlag = false;
                }
                else
                {
                    ExistsFlag = true;
                }
            }
            catch (Exception ex)
            {
            }

            return ExistsFlag;
        }

        public static bool CheckFirstTransactionExists(Int64 MemberId)
        {
            bool ExistsFlag = true;
            try
            {
                int Count = Convert.ToInt32((new CommonHelpers()).GetScalarValueWithValue($"select count(0) from WalletTransactions  with(nolock) where sign = 2 and  MemberId = '{MemberId}' "));
                if (Count == 0)
                {
                    ExistsFlag = false;
                }
                else
                {
                    ExistsFlag = true;
                }
            }
            catch (Exception ex)
            {
            }

            return ExistsFlag;
        }
        public static string GetTransactionRemarks(Int32 ServiceId, decimal amount, string CustomerId)
        {
            string Remarks = "";
            try
            {
                switch (ServiceId)
                {
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ntc:
                        Remarks = $"NTC - Top up of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ncell:
                        Remarks = $"NCELL - Top up of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_smartcell:
                        Remarks = $"Smart - Top up of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_cleartv:
                        Remarks = $"ClearTV- Payment of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_dishhome:
                        Remarks = $"DishHome TV- Payment of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_jagrititv:
                        Remarks = $"Jagriti TV- Payment of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_maxtv:
                        Remarks = $"Max TV- Payment of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_mero:
                        Remarks = $"Mero TV- Payment of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_png_network_tv:
                        Remarks = $"P&G TV- Payment of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_prabhutv:
                        Remarks = $"Prabhu TV- Payment of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_simtv:
                        Remarks = $"SIM TV- Payment of Rs. {amount} in {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_adsl:
                        Remarks = $"ADSL - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Arrownet:
                        Remarks = $"ArrowNet - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_classictech:
                        Remarks = $"ClassicTech - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_vianet:
                        Remarks = $"Vianet - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_jagriti:
                        Remarks = $"Jagriti - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_png_network:
                        Remarks = $"P&G - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Pokhara:
                        Remarks = $"Pokhara - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_RoyalNetwork:
                        Remarks = $"Royal Network - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new:
                        Remarks = $"Subisu - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu:
                        Remarks = $"Subisu - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_TechMinds:
                        Remarks = $"TechMinds - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_VirtualNetwork:
                        Remarks = $"Virtual Network - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebNetwork:
                        Remarks = $"Web Network - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebSurfer:
                        Remarks = $"WebSurfer - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Worldlink:
                        Remarks = $"WorldLink - Internet payment of Rs. {amount} for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_pstn_landline:
                        Remarks = $"Landline bills of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nea:
                        Remarks = $"NEA - bills of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ntc_erc:
                        Remarks = $"NTC - Recharge card of Rs. {amount} paid";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_dishhome_erc:
                        Remarks = $"DishHome - Recharge card of Rs. {amount} paid";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_smart_erc:
                        Remarks = $"Smart - Recharge card of Rs. {amount} paid";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_broadlink_erc:
                        Remarks = $"Broadlink - Recharge card of Rs. {amount} paid";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_khanepani:
                        Remarks = $"Khanepani - Bills of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant:
                        Remarks = $"Arhant - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Jyoti_Life:
                        Remarks = $"Jyoti Life - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Neco:
                        Remarks = $"Neco - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Nepal_Life:
                        Remarks = $"Nepal Life - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance:
                        Remarks = $"Reliance - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico:
                        Remarks = $"Sagarmatha - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                        break;
                    //case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Life:
                    //    Remarks = $"Sanima Life - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                    //    break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Shikhar:
                        Remarks = $"Shikhar - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                        break;
                    //case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Life:
                    //    Remarks = $"Surya Life - Insurnace premium of Rs. {amount} paid for {CustomerId}";
                    //    break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NTC:
                        Remarks = $"NTC - Datapack of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_DataPack_NCell:
                        Remarks = $"Ncell - Datapack of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_VoicePack_NTC:
                        Remarks = $"NTC - Voicepack of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_VoicePack_NCELL:
                        Remarks = $"Ncell - Voicepack of Rs. {amount} paid for {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine:
                        Remarks = $"Traffic Fine Payment of {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment:
                        Remarks = "Credit Card Payment Successfully Deposited"; //$"Credit Card - {CustomerId} of Rs.{amount} paid";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines:
                        Remarks = $"Flight Ticket of Rs. {amount}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events:
                        Remarks = $"Payment of Rs {amount} to {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_antivirus_kaspersky:
                        Remarks = $"Payment of {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Eset:
                        Remarks = $"Payment of {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_k7:
                        Remarks = $"Payment of {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Mcafee:
                        Remarks = $"Payment of {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Wardwiz:
                        Remarks = $"Payment of {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay:
                        Remarks = $"Successfully Completed Transaction of Rs. {amount}.";
                        break;
                        break; 
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa:
                        Remarks = $"Payment successful for serial number: {CustomerId}";
                        break;
                    case (int)VendorApi_CommonHelper.KhaltiAPIName.NepalPay_QR_Payments:
                        Remarks = $"Payment successful for merchant: {CustomerId}";
                        break;
                    default:
                        Remarks = "";
                        break;
                }
            }
            catch (Exception ex)
            {
            }

            return Remarks;
        }
        public static string HTMLToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }

        public static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ISEmailVeriFied(string MemberID)
        {
            bool ExistsFlag = true;
            try
            {
                int Count = Convert.ToInt32((new CommonHelpers()).GetScalarValueWithValue($"select convert(int,IsEmailVerified)  from Users where MemberId = '{MemberID}'"));
                if (Count == 0)
                {
                    ExistsFlag = false;
                }
                else
                {
                    ExistsFlag = true;
                }
            }
            catch (Exception ex)
            {
            }

            return ExistsFlag;
        }

       

        //public static string getHashMD5(string userInput)
        //{


        //        //var jsonObject = JsonConvert.DeserializeObject(JSONInput);
        //        string concatenated = "";

        //        JObject jsonObject = JObject.Parse(userInput);

        //        foreach (var item in jsonObject)
        //        {
        //            if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()) && item.Key.ToLower() != "hash" &&
        //                    item.Key.ToLower() != "passengersclassstring" && item.Key.ToLower() != "playerlist" && item.Key.ToLower() != "bankdata" &&
        //                    item.Key.ToLower() != "vendorjsonlookup")
        //            {
        //                if (item.Key.ToLower() == "mpin")
        //                {
        //                    concatenated += Common.Decryption(item.Value.ToString());
        //                }
        //                else
        //                {
        //                    concatenated += item.Value.ToString();
        //                }
        //            }

        //        }

        //        string result = "";
        //        StringBuilder hash = new StringBuilder();
        //        MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        //        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(concatenated));

        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            hash.Append(bytes[i].ToString("x2"));
        //        }
        //        return hash.ToString();

        //    return result;  
        //}
    }

    public class CommonLoginAttempt
    {
        private string _AttemptedDatetime = "";
        public string AttemptedDatetime
        {
            get { return _AttemptedDatetime; }
            set { _AttemptedDatetime = value; }
        }
        private string _AttemptedCount = "";
        public string AttemptedCount
        {
            get { return _AttemptedCount; }
            set { _AttemptedCount = value; }
        }


    }


}