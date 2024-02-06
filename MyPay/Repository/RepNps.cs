

using CSharp_easy_RSA_PEM;
using log4net;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using NepaliCalendarBS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MyPay.Repository
{
    public class RepNps
    {
        //public static string MerchantId = "32";
        //public static string MerchantName = "smartcardapi";
        //public static string npsurl = "https://gateway.nepalpayment.com/";
        //public static string BasicAPIKEY = "c21hcnRjYXJkYXBpOiR3QHI3Q0ByZCFBRCEjNTExCgo=";
        //public static string secretKey = "Sm@r7c@7d!12g$%5";
        //public static string MerchantId = "1093";
        //public static string MerchantName = "smartcardnepalapi";
        //public static string npsurl = "https://apisandbox.nepalpayment.com/";
        //public static string BasicAPIKEY = "c21hcnRjYXJkbmVwYWxhcGk6JHdAclRjQHJEQGQhIzUw";
        //public static string secretKey = "swArtc@4d@12cr1t";

        public static string MerchantId = "58";
        public static string MerchantName = "mypayapi";
        public static string npsurl = "https://apigateway.nepalpayment.com/";
        public static string BasicAPIKEY = "bXlwYXlhcGk6TSNwQHlAcDE=";
        public static string secretKey = "mYp@y$ecK3y";

        //public static string LinkMerchantId_UAT = "1093";
        //public static string LinkBankUrl_UAT = "https://linkaccountsandbox.nepalpayment.com/";
        //public static string LinkedMerchantName_UAT = "smartcardftuser";
        //public static string LinkBankApiKey_UAT = "c21hcnRjYXJkZnR1c2VyOlN3QDR0VUBycFBhJCQ=";

        public static string LinkMerchantId = Common.ApplicationEnvironment.IsProduction ? "58" : "1093";
        public static string LinkBankUrl = Common.ApplicationEnvironment.IsProduction ? "https://linkaccount.nepalpayment.com:6002/" : "https://linkaccountsandbox.nepalpayment.com/";
        public static string LinkedMerchantName = Common.ApplicationEnvironment.IsProduction ? "mypayftapi" : "smartcardftuser";
        public static string LinkBankApiKey = Common.ApplicationEnvironment.IsProduction ? "bXlwYXlmdGFwaTpNeVBAeUZ0QHAx" : "c21hcnRjYXJkZnR1c2VyOlN3QDR0VUBycFBhJCQ=";

        //public static string FundTransferUrl = " https://fundtransferapisandbox.nepalpayment.com/";
        //public static string sourcebank = "FTTESTBANK";
        //public static string sourceaccno = "1900000000000155";
        //public static string sourcename = "Smart Card Nepal";


        public static string FundTransferUrl = "https://fundtransfer.nepalpayment.com:6001/";
        public static string sourcebank = "NICENPKA";
        public static string sourceaccno = "2844150059837002";
        public static string sourcename = "Smart Card Nepal Pvt Ltd";

        private static ILog log = LogManager.GetLogger(typeof(RepNps));
        public static string HMACSHA512(string text)
        {
            var hash = new StringBuilder(); ;
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


        public static string GenerateSignature(string JsonString)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info($"{System.DateTime.Now.ToString()} inside RepNPS GenerateSignature start" + Environment.NewLine);
            string signature = "";
            try
            {
                string certkey = File.ReadAllText(HttpContext.Current.Server.MapPath("/Certificates/LiveCert.txt"));
              
                byte[] importantMessageBytes = Encoding.UTF8.GetBytes(JsonString);

                RSACryptoServiceProvider privateRSAkey = Crypto.DecodeRsaPrivateKey(certkey);
               

                byte[] bytes = privateRSAkey.SignData(importantMessageBytes, typeof(SHA256));
               

                signature = Convert.ToBase64String(bytes);
              


                log.Info($"{System.DateTime.Now.ToString()} RepNPS GenerateSignature complete" + Environment.NewLine);
                return signature;
            }
            catch (Exception ex)
            {
                log.Info("Error while generating signature");

                log.Error($"{System.DateTime.Now.ToString()} RepNPS2 GenerateSignature {ex.ToString()} " + Environment.NewLine);
                Common.AddLogs("Error GenerateSignature:" + ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName, true);
            }
            log.Info($"{System.DateTime.Now.ToString()} RepNPS GenerateSignature complete" + Environment.NewLine);
            return signature;
        }


        public static GetFundTransferToken GetToken()
        {
            GetFundTransferToken result = new GetFundTransferToken();
            try
            {
                AddFundTransferToken obj = new AddFundTransferToken();

                obj.MerchantId = LinkMerchantId;
                obj.ApiUserName = LinkedMerchantName;
                obj.Signature = GenerateSignature(obj.ApiUserName + obj.MerchantId + obj.TimeStamp);
                string data = PostFundMethod("v1/SignIn", JsonConvert.SerializeObject(obj));
                if (!string.IsNullOrEmpty(data))
                {
                    result = JsonConvert.DeserializeObject<GetFundTransferToken>(data);
                    if (result.code == "0" && result.message == "Success")
                    {
                        result.ReponseCode = 1;
                        result.Message = "Success";
                        result.Details = "Token Generated Successfully";
                        result.status = true;
                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = "Failed";
                        if (result.errors.Count > 0)
                        {
                            result.Details = result.errors[0].error_message;
                        }
                        else
                        {
                            result.Details = result.message;
                        }
                    }
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    result.Details = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public static GetFundBankList GetFundBankList()
        {
            GetFundBankList result = new GetFundBankList();
            try
            {
                AddFundTransferToken obj = new AddFundTransferToken();

                obj.MerchantId = LinkMerchantId;
                obj.ApiUserName = LinkedMerchantName;
                obj.Signature = GenerateSignature(obj.ApiUserName + obj.MerchantId + obj.TimeStamp);
                GetFundTransferToken token = GetToken();
                if (token.data != null && token.data.AccessToken != "")
                {
                    string data = PostFundMethodWithToken("v1/GetBankList", JsonConvert.SerializeObject(obj), token.data.AccessToken);
                    if (!string.IsNullOrEmpty(data))
                    {
                        result = JsonConvert.DeserializeObject<GetFundBankList>(data);
                        if (result.code == "0" && result.message.ToLower() == "success")
                        {
                            result.ReponseCode = 1;
                            result.Message = "Success";
                            result.Details = "Bank List Fetched Successfully";
                            result.status = true;
                        }
                        else
                        {
                            result.ReponseCode = 3;
                            result.Message = "Failed";
                            if (result.errors.Count > 0)
                            {
                                result.Details = result.errors[0].error_message;
                            }
                            else
                            {
                                result.Details = result.message;
                            }
                        }
                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = "Failed";
                        result.Details = "Data Not Found";
                    }
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    result.Details = "Token Not Found";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public static GetFundTxnStatus CheckFundTransactionStatus(string TransactionId)
        {
            AddFundTxnStatus obj = new AddFundTxnStatus();
            GetFundTxnStatus result = new GetFundTxnStatus();
            obj.MerchantId = LinkMerchantId;
            obj.ApiUsername = LinkedMerchantName;
            obj.MerchantTxnId = TransactionId;
            obj.Signature = GenerateSignature(obj.ApiUsername + obj.MerchantId + obj.MerchantTxnId + obj.TimeStamp);
            string data = PostLinkMethod("v1/CheckTransactionStatus", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetFundTxnStatus>(data);
                if (result.code == "0" && result.message.ToLower() == "success")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Transaction Status Fetched Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static GetLinkServiceCharge GetFundServiceCharge()
        {
            GetLinkServiceCharge result = new GetLinkServiceCharge();
            try
            {
                AddFundTransferToken obj = new AddFundTransferToken();

                obj.MerchantId = LinkMerchantId;
                obj.ApiUserName = LinkedMerchantName;
                obj.Signature = GenerateSignature(obj.ApiUserName + obj.MerchantId + obj.TimeStamp);
                GetFundTransferToken token = GetToken();
                if (token.data != null && token.data.AccessToken != "")
                {
                    string data = PostFundMethodWithToken("v1/GetServiceCharge", JsonConvert.SerializeObject(obj), token.data.AccessToken);
                    if (!string.IsNullOrEmpty(data))
                    {
                        result = JsonConvert.DeserializeObject<GetLinkServiceCharge>(data);
                        if (result.code == "0" && result.message == "Success")
                        {
                            result.ReponseCode = 1;
                            result.Message = "Success";
                            result.Details = "Service List Fetched Successfully";
                            result.status = true;
                        }
                        else
                        {
                            result.ReponseCode = 3;
                            result.Message = "Failed";
                            if (result.errors.Count > 0)
                            {
                                result.Details = result.errors[0].error_message;
                            }
                            else
                            {
                                result.Details = result.message;
                            }
                        }
                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = "Failed";
                        result.Details = "Data Not Found";
                    }
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    result.Details = "Token Not Found";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public static GetFundAccountValidation FundAccountValidation(string AccountName, string AccountNumber, string BankCode)
        {
            GetFundAccountValidation result = new GetFundAccountValidation();
            try
            {
                AddFundAccountValidation obj = new AddFundAccountValidation();

                obj.MerchantId = LinkMerchantId;
                obj.ApiUserName = LinkedMerchantName;
                obj.AccountName = AccountName;
                obj.AccountNumber = AccountNumber;
                obj.BankCode = BankCode;
                obj.Signature = GenerateSignature(obj.AccountName + obj.AccountNumber + obj.ApiUserName + obj.BankCode + obj.MerchantId + obj.TimeStamp);
                GetFundTransferToken token = GetToken();
                if (token.data != null && token.data.AccessToken != "")
                {
                    string data = PostFundMethodWithToken("v1/AccountValidation", JsonConvert.SerializeObject(obj), token.data.AccessToken);
                    Common.AddLogs($"v1/AccountValidation Request: {JsonConvert.SerializeObject(obj)} Response: {data}", false, (int)AddLog.LogType.DBLogs);

                    if (!string.IsNullOrEmpty(data))
                    {
                        result = JsonConvert.DeserializeObject<GetFundAccountValidation>(data);
                        if (result.code == "0" && result.message == "Success")
                        {
                            result.ReponseCode = 1;
                            result.Message = "Success";
                            result.Details = "Account Is Validated Successfully";
                            result.status = true;
                        }
                        else
                        {
                            result.ReponseCode = 3;
                            result.Message = "Failed";
                            if (result.errors.Count > 0)
                            {
                                result.Details = result.errors[0].error_message;
                            }
                            else
                            {
                                result.Details = result.message;
                            }
                        }
                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = "Failed";
                        result.Details = "Data Not Found";
                    }
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    result.Details = "Token Not Found";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public static GetFundTransferRequest FundTransferRequest(string AccountName, string AccountNumber, string BankCode, string MerchantTxnId, string MerchantProcessID, string Amount, string TransactionRemarks)
        {
            GetFundTransferRequest result = new GetFundTransferRequest();
            try
            {
                AddFundTransferRequest obj = new AddFundTransferRequest();

                obj.MerchantId = LinkMerchantId;
                obj.ApiUserName = LinkedMerchantName;
                obj.MerchantTxnId = MerchantTxnId;
                obj.MerchantProcessID = MerchantProcessID;
                obj.Amount = Convert.ToDecimal(Amount).ToString("f2");
                obj.SourceBank = sourcebank;
                obj.SourceAccNo = sourceaccno;
                obj.SourceAccName = sourcename;
                obj.SourceCurrency = "NPR";
                obj.DestinationAccName = AccountName;
                obj.DestinationBank = BankCode;
                obj.DestinationCurrency = "NPR";
                obj.DestinationAccNo = AccountNumber;
                obj.TransactionRemarks = TransactionRemarks;
                obj.Signature = GenerateSignature(obj.Amount + obj.ApiUserName + obj.DestinationAccName + obj.DestinationAccNo + obj.DestinationBank + obj.DestinationCurrency + obj.MerchantId + obj.MerchantProcessID + obj.MerchantTxnId + obj.SourceAccName + obj.SourceAccNo + obj.SourceBank + obj.SourceCurrency + obj.TimeStamp + obj.TransactionRemarks + obj.TransactionRemarks2 + obj.TransactionRemarks3);
                GetFundTransferToken token = GetToken();
                if (token.data != null && token.data.AccessToken != "")
                {
                    string data = PostFundMethodWithToken("v1/FundTransferRequest", JsonConvert.SerializeObject(obj), token.data.AccessToken);

                    if (!string.IsNullOrEmpty(data))
                    {
                        result = JsonConvert.DeserializeObject<GetFundTransferRequest>(data);
                        if (result.code == "0" && result.message.ToLower() == "success")
                        {
                            result.ReponseCode = 1;
                            result.Message = "Success";
                            result.Details = "Transaction Completed Successfully";
                            result.status = true;
                        }
                        else if (result.code == "2" && result.message.ToLower() == "pending")
                        {
                            result.ReponseCode = 2;
                            result.Message = "Pending";
                            result.Details = "Transaction Pending";
                            result.status = true;
                        }
                        else
                        {
                            result.ReponseCode = 3;
                            result.Message = "Failed";
                            if (result.errors.Count > 0)
                            {
                                result.Details = result.errors[0].error_message;
                            }
                            else
                            {
                                result.Details = result.message;
                            }
                        }
                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = "Failed";
                        result.Details = "Data Not Found";
                    }
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    result.Details = "Token Not Found";
                }
                Common.AddLogs("NPs Fund Transfer Request: " + JsonConvert.SerializeObject(obj) + " Response:" + JsonConvert.SerializeObject(result), false, (int)AddLog.LogType.BankTransfer, Common.CreatedBy, Common.CreatedByName, true);
            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }
            return result;
        }

        public static GetPaymentInstrument GetBankList()
        {
            GetPaymentInstrument result = new GetPaymentInstrument();
            try
            {
                ApiSetting objApiSettings = new ApiSetting();
                using (var db = new MyPayEntities())
                {
                    objApiSettings = db.ApiSettings.FirstOrDefault();

                    if ((objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.ALL) && (objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.NPS))
                    {
                        result.ReponseCode = 3;
                        result.Message = Common.TemporaryServiceUnavailable;
                        result.Details = Common.TemporaryServiceUnavailable;
                        return result;
                    }
                }
                AddBankListLinkedAccount obj = new AddBankListLinkedAccount();

                obj.MerchantId = LinkMerchantId;
                obj.MerchantName = LinkedMerchantName;
                obj.Signature = GenerateSignature(obj.MerchantId + obj.MerchantName + obj.TimeStamp);
                string data = PostLinkMethod("api/GetBankList", JsonConvert.SerializeObject(obj));
                if (!string.IsNullOrEmpty(data))
                {
                    data = data.Replace("https://linkaccount.nepalpayment.com:6002/UploadedImages/PaymentInstitution/", Common.LiveSiteUrl + "/Content/assets/images/BankLinkLogo/");
                    result = JsonConvert.DeserializeObject<GetPaymentInstrument>(data);
                    if (result.code == "0" && result.message == "Success")
                    {
                        result.ReponseCode = 1;
                        result.Message = "Success";
                        result.Details = "Bank List Fetched Successfully";
                        result.status = true;
                        List<dataInstruments> objdataInstruments = result.data;
                        objdataInstruments.Sort((x, y) => Convert.ToString(x.InstitutionName).CompareTo(Convert.ToString(y.InstitutionName)));

                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = "Failed";
                        if (result.errors.Count > 0)
                        {
                            result.Details = result.errors[0].error_message;
                        }
                        else
                        {
                            result.Details = result.message;
                        }
                    }
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    result.Details = "Data Not Found";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public static GetLinkServiceCharge GetServicCharge()
        {
            AddBankListLinkedAccount obj = new AddBankListLinkedAccount();
            GetLinkServiceCharge result = new GetLinkServiceCharge();
            obj.MerchantId = LinkMerchantId;
            obj.MerchantName = LinkedMerchantName;
            obj.Signature = GenerateSignature(obj.MerchantId + obj.MerchantName + obj.TimeStamp);
            string data = PostLinkMethod("api/GetServiceCharge", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetLinkServiceCharge>(data);
                if (result.code == "0" && result.message == "Success")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Service Charges Fetched Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static GetLoadWalletWithToken LoadWalletWithToken(string API_NAME, string TransactionId, string Token, string BankCode, string Amount, string TransactionRemarks, ref AddVendor_API_Requests objVendor_API_Requests)
        {

            AddLoadWalletWithToken obj = new AddLoadWalletWithToken();
            GetLoadWalletWithToken result = new GetLoadWalletWithToken();
            //result.ReponseCode = 3;
            //result.Message = "Failed";
            //result.Details = "Temporarily service unavailabel";

            //return result;
            obj.MerchantId = LinkMerchantId;
            obj.MerchantName = LinkedMerchantName;
            obj.TransactionId = TransactionId;
            obj.Token = Token;
            obj.BankCode = BankCode;
            obj.Amount = Amount;
            obj.TransactionRemarks = TransactionRemarks;
            obj.Signature = GenerateSignature(obj.Amount + obj.BankCode + obj.MerchantId + obj.MerchantName + obj.TimeStamp + obj.Token + obj.TransactionId + obj.TransactionRemarks);

            objVendor_API_Requests.Req_Token = Token;
            objVendor_API_Requests.Req_Khalti_ReferenceNo = TransactionId;
            objVendor_API_Requests.Req_Khalti_URL = LinkBankUrl + API_NAME;
            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

            string data = PostLinkMethod(API_NAME, JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {

                objVendor_API_Requests.Req_Khalti_Input = JsonConvert.SerializeObject(obj);
                objVendor_API_Requests.Res_Khalti_Output = data;
                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                Common.AddLogs(data, false, (int)AddLog.LogType.DBLogs);
                result = JsonConvert.DeserializeObject<GetLoadWalletWithToken>(data);

                if (result.code == "0")
                {
                    objVendor_API_Requests.Res_Khalti_Status = true;
                    objVendor_API_Requests.Res_Khalti_State = "Success";
                    objVendor_API_Requests.Res_Khalti_Message = result.message;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Transaction Completed Successfully";
                    result.status = true;
                }
                else if (result.code == "2")
                {
                    result.ReponseCode = 2;
                    result.Message = "Pending";
                    result.Details = "Transaction Pending Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                    objVendor_API_Requests.Res_Khalti_Status = false;
                    objVendor_API_Requests.Res_Khalti_State = "Failed";
                    objVendor_API_Requests.Res_Khalti_Message = result.Details;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static GetRegisterBankLink RegisterAccount(int DobType, string MobileNumber, string AccountNumber, string AccountName, string BankCode, string DateOfBirth, string TransactionId)
        {
            GetRegisterBankLink result = new GetRegisterBankLink();
            try
            {
                log.Info($"{System.DateTime.Now.ToString()} inside RepNPS RegisterAccount start" + Environment.NewLine);
                ApiSetting objApiSettings = new ApiSetting();
                using (var db = new MyPayEntities())
                {
                    objApiSettings = db.ApiSettings.FirstOrDefault();

                    if ((objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.ALL) && (objApiSettings.LinkBankTransferType != (int)AddApiSettings.LinkBankType.NPS))
                    {
                        result.ReponseCode = 3;
                        result.Message = Common.TemporaryServiceUnavailable;
                        result.Details = Common.TemporaryServiceUnavailable;
                        return result;
                    }
                }
                AddRegisterLinkedBank obj = new AddRegisterLinkedBank();
                obj.MerchantId = LinkMerchantId;
                obj.MerchantName = LinkedMerchantName;
                obj.MobileNumber = MobileNumber;
                obj.AccountName = AccountName;
                obj.AccountNumber = AccountNumber;
                obj.BankCode = BankCode;
                string[] splitdata = DateOfBirth.Split('/');
                if (splitdata.Length == 3)
                {
                    if (splitdata[0] != "" && Convert.ToInt32(splitdata[0]) > 12 && Convert.ToInt32(splitdata[0]) <= 31)
                    {
                        DateOfBirth = splitdata[1] + "/" + splitdata[0] + "/" + splitdata[2];
                    }
                }
                else
                {
                    splitdata = DateOfBirth.Split('-');
                    if (splitdata.Length == 3)
                    {
                        if (splitdata[0] != "" && Convert.ToInt32(splitdata[0]) > 12 && Convert.ToInt32(splitdata[0]) <= 31)
                        {
                            DateOfBirth = splitdata[1] + "/" + splitdata[0] + "/" + splitdata[2];
                        }
                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = "Failed";
                        result.Details = "Date format of this user is not correct.Please contact with customer support";
                        return result;
                    }
                }
                obj.DateOfBirth = Convert.ToDateTime(DateOfBirth).ToString("yyyy-MM-dd");
                if (DobType == 2)
                {
                    obj.DateOfBirth = NepaliCalendar.Convert_BS2AD(obj.DateOfBirth).ToString("yyyy-MM-dd");
                }

                obj.TransactionId = TransactionId;
                obj.Signature = GenerateSignature(obj.AccountName + obj.AccountNumber + obj.BankCode + obj.DateOfBirth + obj.MerchantId + obj.MerchantName + obj.MobileNumber + obj.TimeStamp + obj.TransactionId);
                string RequestData = JsonConvert.SerializeObject(obj);
                string data = PostLinkMethod("api/RegisterAccount", RequestData);
                if (!string.IsNullOrEmpty(data))
                {
                    result = JsonConvert.DeserializeObject<GetRegisterBankLink>(data);
                    if (result.code == "0" && result.message == "Success")
                    {
                        result.ReponseCode = 1;
                        result.Message = "Success";
                        result.Details = "Account Registered Successfully";
                        result.status = true;
                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = "Failed";
                        if (result.errors.Count > 0)
                        {
                            result.Details = result.errors[0].error_message;
                        }
                        else
                        {
                            result.Details = result.message;
                        }
                    }
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    result.Details = "Data Not Found";
                }
                log.Info($"{System.DateTime.Now.ToString()} RepNPS RegisterAccount complete" + Environment.NewLine);
                string LogMessage = $"LinkBankRegisterAccount INPUT: {RequestData} OUTPUT:{data}";
                Common.AddLogs(LogMessage, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);

            }
            catch (Exception ex)
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = ex.Message;
            }
            return result;
        }


        public static GetVerifyLinkBank VerifyAccount(string Otp, string BankCode, string TransactionId)
        {
            AddVerifyLinkedBank obj = new AddVerifyLinkedBank();
            GetVerifyLinkBank result = new GetVerifyLinkBank();
            obj.MerchantId = LinkMerchantId;
            obj.MerchantName = LinkedMerchantName;
            obj.BankCode = BankCode;
            obj.TransactionId = TransactionId;
            obj.OTPCode = Otp;
            obj.Signature = GenerateSignature(obj.BankCode + obj.MerchantId + obj.MerchantName + obj.OTPCode + obj.TimeStamp + obj.TransactionId);
            string RequestData = JsonConvert.SerializeObject(obj);
            string data = PostLinkMethod("api/VerifyAccount", RequestData);
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetVerifyLinkBank>(data);
                if (result.code == "0" && result.message == "Success")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Account Verified Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            string LogMessage = $"LinkBankRegisterAccount INPUT: {RequestData} OUTPUT:{data}";
            Common.AddLogs(LogMessage, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
            return result;
        }

        public static GetVerifyLinkBank ResendOtp(string TransactionId)
        {
            AddResendOtpLinkBank obj = new AddResendOtpLinkBank();
            GetVerifyLinkBank result = new GetVerifyLinkBank();
            obj.MerchantId = LinkMerchantId;
            obj.MerchantName = LinkedMerchantName;
            obj.TransactionId = TransactionId;
            obj.Signature = GenerateSignature(obj.MerchantId + obj.MerchantName + obj.TimeStamp + obj.TransactionId);
            string data = PostLinkMethod("api/ResendOTP", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetVerifyLinkBank>(data);
                if (result.code == "0")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Otp Sent To Your Mobile Number Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static GetVerifyLinkBank CheckLinkedAccountStatus(string TransactionId)
        {
            AddResendOtpLinkBank obj = new AddResendOtpLinkBank();
            GetVerifyLinkBank result = new GetVerifyLinkBank();
            obj.MerchantId = LinkMerchantId;
            obj.MerchantName = LinkedMerchantName;
            obj.TransactionId = TransactionId;
            obj.Signature = GenerateSignature(obj.MerchantId + obj.MerchantName + obj.TimeStamp + obj.TransactionId);
            string data = PostLinkMethod("api/CheckLinkedAccountStatus", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetVerifyLinkBank>(data);
                if (result.code == "0" && result.message == "Success")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Account Is Linked Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static GetLinkBankTransactionStatus CheckTransactionStatus(string TransactionId)
        {
            AddResendOtpLinkBank obj = new AddResendOtpLinkBank();
            GetLinkBankTransactionStatus result = new GetLinkBankTransactionStatus();
            obj.MerchantId = LinkMerchantId;
            obj.MerchantName = LinkedMerchantName;
            obj.TransactionId = TransactionId;
            obj.Signature = GenerateSignature(obj.MerchantId + obj.MerchantName + obj.TimeStamp + obj.TransactionId);
            string data = PostLinkMethod("api/CheckTransactionStatus", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetLinkBankTransactionStatus>(data);
                if (result.code == "0" && result.message == "Success")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Transaction Status Fetched Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static GetLinkBankTransactionStatus CheckLoadWalletTransactionStatus(string TransactionId)
        {
            AddResendOtpLinkBank obj = new AddResendOtpLinkBank();
            GetLinkBankTransactionStatus result = new GetLinkBankTransactionStatus();
            obj.MerchantId = LinkMerchantId;
            obj.MerchantName = LinkedMerchantName;
            obj.TransactionId = TransactionId;
            obj.Signature = GenerateSignature(obj.MerchantId + obj.MerchantName + obj.TimeStamp + obj.TransactionId);
            string data = PostLinkMethod("api/CheckLoadWalletTxStatus", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetLinkBankTransactionStatus>(data);
                if (result.code == "0" && result.message == "Success")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Transaction Status Fetched Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }

            Common.AddLogs(JsonConvert.SerializeObject(result), true, (int)AddLog.LogType.Linked_BankTransfer, Common.CreatedBy, Common.CreatedByName, false, "Web", System.Web.HttpContext.Current.Request.Browser.Type);
            return result;
        }

        public static GetTokenUnLink UnlinkAccount(string Token)
        {
            AddUnlinkToken obj = new AddUnlinkToken();
            GetTokenUnLink result = new GetTokenUnLink();
            obj.MerchantId = LinkMerchantId;
            obj.MerchantName = LinkedMerchantName;
            obj.Token = Token;
            obj.Signature = GenerateSignature(obj.MerchantId + obj.MerchantName + obj.TimeStamp + obj.Token);
            string data = PostLinkMethod("api/UnlinkLinkedAccountToken", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetTokenUnLink>(data);
                if (result.code == "0" && result.message == "Success")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Account Is UnLinked Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static GetPaymentInstrument GetPaymentInstruments()
        {
            AddPaymentInstrument obj = new AddPaymentInstrument();
            GetPaymentInstrument result = new GetPaymentInstrument();
            obj.MerchantId = MerchantId;
            obj.MerchantName = MerchantName;
            obj.Signature = HMACSHA512(MerchantId + MerchantName);
            string data = PostMethod("GetPaymentInstrumentDetails", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetPaymentInstrument>(data);
                if (result.code == "0" && result.message == "Success")
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Link Generated Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static GetProcessId GetProcessId(decimal amount, string transactionid)
        {
            AddProcessId obj = new AddProcessId();
            GetProcessId result = new GetProcessId();
            obj.Amount = amount;
            obj.MerchantTxnId = transactionid;
            obj.MerchantId = MerchantId;
            obj.MerchantName = MerchantName;
            obj.Signature = HMACSHA512(obj.Amount + obj.MerchantId + obj.MerchantName + obj.MerchantTxnId);
            string data = PostMethod("GetProcessId", JsonConvert.SerializeObject(obj));
            if (!string.IsNullOrEmpty(data))
            {
                result = JsonConvert.DeserializeObject<GetProcessId>(data);
                if (result.code == "0" && result.message.ToLower().Contains("success"))
                {
                    result.ReponseCode = 1;
                    result.Message = "Success";
                    result.Details = "Link Generated Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = "Failed";
                    if (result.errors.Count > 0)
                    {
                        result.Details = result.errors[0].error_message;
                    }
                    else
                    {
                        result.Details = result.message;
                    }
                }
            }
            else
            {
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = "Data Not Found";
            }
            return result;
        }

        public static string PostMethod(string ApiName, string requestdata)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(npsurl + ApiName);
                byte[] bytes = null;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Basic " + BasicAPIKEY);
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
                    return responseStr;
                }
                else
                {

                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
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
                        string text = reader.ReadToEnd();
                        return text;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string PostLinkMethod(string ApiName, string requestdata)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
               

                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(LinkBankUrl + ApiName);
                byte[] bytes = null;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Basic " + LinkBankApiKey);
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
                    Common.AddLogs("NPSPOstMethod: ApiName " + ApiName + ". Request: " + requestdata + "  Response:" + responseStr, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true, "Web", "", (int)AddLog.LogActivityEnum.BankPostData, Common.CreatedBy, Common.CreatedByName);

                    return responseStr;
                }
                else
                {

                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    Common.AddLogs("NPSPOstMethod:" + responseStr, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);

                    return responseStr;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    if (e.Response == null && e.Message != null)
                    {
                        Common.AddLogs("Error NPSPOstMethod: ApiName " + ApiName + ". Request: " + requestdata + " Response: " + e.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                        return e.Message;
                    }
                    else
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;

                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            Common.AddLogs("Error NPSPOstMethod:" + text, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);

                            return text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs("Error NPSPOstMethod:" + ex.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);

                return ex.Message;
            }
        }

        public static string PostFundMethod(string ApiName, string requestdata)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(FundTransferUrl + ApiName);
                byte[] bytes = null;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Basic " + LinkBankApiKey);
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
                    return responseStr;
                }
                else
                {

                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
            }
            catch (WebException e)
            {
                if (e.Response == null && e.Message != null)
                {
                    Common.AddLogs("Error NPSPOstMethod:" + e.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                    return e.Message;
                }
                else
                {
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            Common.AddLogs("Error NPSPOstMethod:" + text, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                            return text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs("Error NPSPOstMethod:" + ex.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                return ex.Message;
            }
        }

        public static string PostFundMethodWithToken(string ApiName, string requestdata, string Token)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(FundTransferUrl + ApiName);
                byte[] bytes = null;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + Token);
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
                    return responseStr;
                }
                else
                {

                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
            }
            catch (WebException e)
            {
                if (e.Response == null && e.Message != null)
                {
                    Common.AddLogs("Error NPSPOstMethod: ApiName " + ApiName + ". Request: " + requestdata + " Response: " + e.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                    return e.Message;
                }
                else
                {
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            Common.AddLogs("Error NPSPOstMethod:" + text, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                            return text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs("Error NPSPOstMethod:" + ex.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                return ex.Message;
            }
        }
    }
}