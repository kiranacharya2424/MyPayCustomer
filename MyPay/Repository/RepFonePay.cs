
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
using System.Net;
using System.Collections.Specialized;

namespace MyPay.Repository
{
    public class RepFonePay
    {

        public static Int64 Id = 0;
        public static string UniqueTransactionId = string.Empty;
        public static decimal WalletBalance = 0;

        private static ILog log = LogManager.GetLogger(typeof(RepFonePay));
        public static string HMACSHA512(string text)
        {
            var hash = new StringBuilder(); ;
            return hash.ToString().ToUpper();
        }

        public static string PostFundMethod(string requestdata)
        {
            try
            {
                string APINAME = "client/fonepayAuth/oauth/token";
                var request = (HttpWebRequest)WebRequest.Create(VendorApi_CommonHelper.FonePay_API_URL_Base_Root + APINAME);


                NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
                outgoingQueryString.Add("grant_type", "password");
                outgoingQueryString.Add("username", "MyPay001");
                outgoingQueryString.Add("password", "D5hn$*ixMM+9AFi0W11&WhkOM");
                string postData = outgoingQueryString.ToString();
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.Headers.Add("oauth-client-type", "FONEPAY_USER");
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
                //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create("https://ir-external-gateway.fonepay.com/client/fonepayAuth/oauth/token");
                //byte[] bytes = null;
                //bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                //request.ContentType = "application/json";
                //request.Headers.Add("oauth-client-type", "FONEPAY_USER");
                //request.ContentLength = bytes.Length;
                //request.Method = "POST";
                //request.KeepAlive = false;
                //request.Timeout = System.Threading.Timeout.Infinite;
                //Stream requestStream = request.GetRequestStream();
                //requestStream.Write(bytes, 0, bytes.Length);
                //requestStream.Flush();
                //requestStream.Close();

                //HttpWebResponse response = default(HttpWebResponse);
                //response = (HttpWebResponse)request.GetResponse();
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    Stream responseStream = response.GetResponseStream();
                //    string responseStr = new StreamReader(responseStream).ReadToEnd();
                //    return responseStr;
                //}
                //else
                //{

                //    Stream responseStream = response.GetResponseStream();
                //    string responseStr = new StreamReader(responseStream).ReadToEnd();
                //    return responseStr;
                //}
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    //Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        //Common.AddLogs("Error NPSPOstMethod:" + text, false, 1, Common.CreatedBy, Common.CreatedByName, true);
                        return text;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs("Error NPSPOstMethod:" + ex.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                return ex.Message;
            }
        }

        public static string PostTokenMethod()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string API_NAME = VendorApi_CommonHelper.FonePay_API_URL_Base_Root + "client/fonepayAuth/oauth/token";

                var request = (HttpWebRequest)WebRequest.Create(API_NAME);
                NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
                outgoingQueryString.Add("grant_type", "password");
                outgoingQueryString.Add("username", "MyPay001");
                outgoingQueryString.Add("password", "D5hn$*ixMM+9AFi0W11&WhkOM");
                string postData = outgoingQueryString.ToString();
                //var postData = "grant_type=password";
                //postData += "&username=globalBankQR";
                //postData += "&password=pi+2me!DkG@QKkx";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Headers.Add("oauth-client-type", "FONEPAY_USER");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.KeepAlive = false;
                request.Timeout = System.Threading.Timeout.Infinite;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        return responseString;
                    }
                    else
                    {

                        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        return responseString;
                    }
                }
                return "No reponse";

                //HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create("https://ir-external-gateway.fonepay.com/client/fonepayAuth/oauth/token");
                //byte[] bytes = null;
                //bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                //request.ContentType = "application/json";
                //request.Headers.Add("oauth-client-type", "FONEPAY_USER");
                //request.ContentLength = bytes.Length;
                //request.Method = "POST";
                //request.KeepAlive = false;
                //request.Timeout = System.Threading.Timeout.Infinite;
                //Stream requestStream = request.GetRequestStream();
                //requestStream.Write(bytes, 0, bytes.Length);
                //requestStream.Flush();
                //requestStream.Close();

                //HttpWebResponse response = default(HttpWebResponse);
                //response = (HttpWebResponse)request.GetResponse();
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    Stream responseStream = response.GetResponseStream();
                //    string responseStr = new StreamReader(responseStream).ReadToEnd();
                //    return responseStr;
                //}
                //else
                //{

                //    Stream responseStream = response.GetResponseStream();
                //    string responseStr = new StreamReader(responseStream).ReadToEnd();
                //    return responseStr;
                //}
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
                        Common.AddLogs("Error NPSPOstMethod:" + text, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                        return text;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs("Error NPSPOstMethod:" + ex.Message, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, true);
                return ex.Message;
            }
        }

        public static string GenerateFonePayAuthToken()
        {



            string API_NAME = VendorApi_CommonHelper.FonePay_API_URL_Base_Root + "client/fonepayAuth/oauth/token";
            var client = new RestClient(API_NAME);


            var _request = new RestRequest("", Method.Post);
            _request.AddHeader("oauth-client-type", "FONEPAY_USER");
            _request.AlwaysMultipartFormData = true;
            _request.Timeout = -1;
            _request.AddParameter("grant_type", "password");
            _request.AddParameter("username", VendorApi_CommonHelper.FonePay_UserName);
            _request.AddParameter("password", VendorApi_CommonHelper.FonePay_Password);
            string JsonReq = Newtonsoft.Json.JsonConvert.SerializeObject(_request);
            try
            {
                Common.AddLogs($"Requested API : {API_NAME} on {System.DateTime.Now.ToString()}. API INPUT POST : {JsonReq} API RESPONSE :  ", false, 0, 0, "", false, "Web", "", (int)AddLog.LogActivityEnum.FonePayTransactions);

                var _response = client.ExecuteAsync(_request).Result;
                Common.AddLogs($"Requested API : {API_NAME} on {System.DateTime.Now.ToString()}. API INPUT POST : {JsonReq} API RESPONSE : {(string.IsNullOrEmpty(_response.Content) ? "null" : _response.Content)} ", false, 0, 0, "", false, "Web", "", (int)AddLog.LogActivityEnum.FonePayTransactions);

                return _response.Content.ToString();
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
                        Common.AddLogs($"Requested API : {API_NAME} on {System.DateTime.Now.ToString()}. API INPUT POST : {JsonReq} API RESPONSE : {text} ", false, 0, 0, "", false, "Web", "", (int)AddLog.LogActivityEnum.FonePayTransactions);

                        return text;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddLogs($"Requested API : {API_NAME} on {System.DateTime.Now.ToString()}. API INPUT POST : {JsonReq} API RESPONSE : {ex.Message} ", false, 0, 0, "", false, "Web", "", (int)AddLog.LogActivityEnum.FonePayTransactions);
                return "";
            }
        }
        public static string GenerateSignature(string JsonString)
        {
            string signature = "";
            try
            {
                string PrefixLive = "";
                if (Common.ApplicationEnvironment.IsProduction)
                {
                    PrefixLive = "_Live";
                }
                string filepath = HttpRuntime.AppDomainAppPath + "/Certificates/FonePayCert" + PrefixLive + ".txt";
                string privateKeyString = File.ReadAllText(filepath);

                if (string.IsNullOrWhiteSpace(privateKeyString) || string.IsNullOrWhiteSpace(JsonString))
                {
                    return " Private key and string cannot be empty ";
                }

                string fnstr = JsonString;// String to be signed  

                RSACryptoServiceProvider rsaP = RsaUtil.LoadPrivateKey(privateKeyString, "PKCS8");

                byte[] data = Encoding.UTF8.GetBytes(fnstr);// The string to be signed is converted to byte Array ,UTF8

                byte[] byteSign = rsaP.SignData(data, "SHA256");// Corresponding JAVA Of RSAwithSHA256

                string sign = Convert.ToBase64String(byteSign);// Signature byte Array to BASE64 character string 
                signature = sign;
            }
            catch (Exception ex)
            {
                Common.AddLogs("Error Fonepay GenerateSignature:" + ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName, true);
            }
            return signature;
        }

        public static string GenerateEncryptionFromPublicKey(string InputString)
        {
            string ret = string.Empty;
            try
            {
                InputString = "4089720000008002";
                String pubB64 = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA3oLygOfWrlO5e+lIY2hnmIGEyjWoX+wqtaATeZn5+pPtQXaB75KZRzo2Xt1EqWQSaifYWrAMk1fSxj03vWV8sl1ki7oT090d+tU2RPme3MRB/aBYik3MEMWAVhfUo5a+RSYmPaBfmd0IEReR6vZmIzQWp8OuAbCCzI22SHtatrdRQk5TNx6Oka52GhCpnGGEejp9O/l5CLOx/QLs4xa8Jb3FKcvtPQQ7lZIyLBRf52rQs/uk/J1x2oO0zKNafznpmp1mFLp3hMPNGKZVyPdYEIHRgljnGhPwdhxnubZSnINKCmK0JDHxDEvM6fWY6DnYB8ejhb7kq0ePhpvAYBPivQIDAQAB";

                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(InputString);
                byte[] publicKeyBytes = Convert.FromBase64String(pubB64);

                var keyLengthBits = 2048;  // need to know length of public key in advance!
                byte[] exponent = new byte[3];
                byte[] modulus = new byte[keyLengthBits / 8];
                Array.Copy(publicKeyBytes, publicKeyBytes.Length - exponent.Length, exponent, 0, exponent.Length);
                Array.Copy(publicKeyBytes, publicKeyBytes.Length - exponent.Length - 2 - modulus.Length, modulus, 0, modulus.Length);

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters rsaKeyInfo = rsa.ExportParameters(false);
                rsaKeyInfo.Modulus = modulus;
                rsaKeyInfo.Exponent = exponent;
                rsa.ImportParameters(rsaKeyInfo);

                byte[] encrypted = rsa.Encrypt(textBytes, RSAEncryptionPadding.OaepSHA1);
                ret = Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }

        public static string GenChilkatEncrypt()
        {
            //Chilkat.PublicKey pubkey = new Chilkat.PublicKey();

            //Chilkat.StringBuilder sbPem = new Chilkat.StringBuilder();
            //bool bCrlf = true;
            //sbPem.AppendLine("-----BEGIN PUBLIC KEY-----", bCrlf);
            //sbPem.AppendLine("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA3oLygOfWrlO5e+lIY2hn", bCrlf);
            //sbPem.AppendLine("mIGEyjWoX+wqtaATeZn5+pPtQXaB75KZRzo2Xt1EqWQSaifYWrAMk1fSxj03vWV8", bCrlf);
            //sbPem.AppendLine("sl1ki7oT090d+tU2RPme3MRB/aBYik3MEMWAVhfUo5a+RSYmPaBfmd0IEReR6vZm", bCrlf);
            //sbPem.AppendLine("IzQWp8OuAbCCzI22SHtatrdRQk5TNx6Oka52GhCpnGGEejp9O/l5CLOx/QLs4xa8", bCrlf);
            //sbPem.AppendLine("Jb3FKcvtPQQ7lZIyLBRf52rQs/uk/J1x2oO0zKNafznpmp1mFLp3hMPNGKZVyPdY", bCrlf);
            //sbPem.AppendLine("EIHRgljnGhPwdhxnubZSnINKCmK0JDHxDEvM6fWY6DnYB8ejhb7kq0ePhpvAYBPi", bCrlf);
            //sbPem.AppendLine("vQIDAQAB", bCrlf);
            //sbPem.AppendLine("-----END PUBLIC KEY-----", bCrlf);

            //// Load the public key object from the PEM. 
            //bool success = pubkey.LoadFromString(sbPem.GetAsString());
            //if (success != true)
            //{
            //    string str = pubkey.LastErrorText;
            //}

            //string originalData = "4089720000001783";

            //// First we SHA-256 hash the original data.
            //Chilkat.Crypt2 crypt = new Chilkat.Crypt2();
            //crypt.HashAlgorithm = "SHA-256";
            //byte[] hashBytes = null;
            //hashBytes = crypt.HashString(originalData);

            //// Now to RSA encrypt using OAEP padding with SHA-1 for the mask function.
            //Chilkat.Rsa rsa = new Chilkat.Rsa();
            //rsa.OaepPadding = true;
            //rsa.OaepHash = "SHA1";
            //rsa.ImportPublicKeyObj(pubkey);
            //rsa.EncodingMode = "base64";

            //// Note: The OAEP padding uses random bytes in the padding, and therefore each time encryption happens,
            //// even using the same data and key, the result will be different --  but still valid.  One should not expect
            //// to get the same output.
            //bool bUsePrivateKey = false;
            //string encryptedStr = rsa.EncryptBytesENC(hashBytes, bUsePrivateKey);
            //if (rsa.LastMethodSuccess != true)
            //{

            //}
            return "";
        }

        public static string FonePay_EncryptData_From_Publickey(string InputString, bool IsCredentialsInfo = false, bool IsBankInfo = false)
        {
            string str = string.Empty;
            try
            {
                string PrefixLive = "";
                if (Common.ApplicationEnvironment.IsProduction)
                {
                    PrefixLive = "_Live";
                }
                string filepath = HttpRuntime.AppDomainAppPath + "/Certificates/FonePayPublic" + PrefixLive + ".txt";
                if (IsCredentialsInfo)
                {
                    filepath = HttpRuntime.AppDomainAppPath + "/Certificates/FonePayPublicKeyAuthentication" + PrefixLive + ".txt";
                }
                else if (IsBankInfo)
                {
                    filepath = HttpRuntime.AppDomainAppPath + "/Certificates/FonePayPublicKeyBank" + PrefixLive + ".txt";
                }
                string publicKey = File.ReadAllText(filepath);

                string plainText = InputString;
                byte[] plainTextToByte = Encoding.UTF8.GetBytes(plainText);
                var plain = Encoding.UTF8.GetBytes(InputString);
                // Read in public key from file
                var pemReader = new PemReader(File.OpenText(filepath));
                var rsaPub = (Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters)pemReader.ReadObject();
                // create encrypter
                var encrypter = new OaepEncoding(new RsaEngine(), new Sha256Digest(), new Sha256Digest(), null);
                encrypter.Init(true, rsaPub);
                var cipher = encrypter.ProcessBlock(plain, 0, plain.Length);
                str = (Convert.ToBase64String(cipher));
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }

        public static string RequestFonePay_QR_Payment(ref bool IsCouponUnlocked, ref string TransactionID,  AddCouponsScratched resCoupon, AddUserLoginWithPin resGetRecord, string BankTransactionId, string WalletType, string CustomerId, string Value, string Amount, string Reference, string Version, string DeviceCode, string PlatForm, string MemberId, string Remarks, string Purpose, string RecieverAccountNo, string SenderAccountNo, string SenderMobile, string SenderName, string RecieverAccountNo_Decrypted, string authenticationToken, string UserInput, GetVendor_API_FonePay_Payment_Request objPayReq, string qrRequestMessage, ref GetVendor_API_FonePay_Payment_Response objRes, ref AddVendor_API_Requests objVendor_API_Requests)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Amount) || Amount == "0")
            {
                msg = "Please enter Amount.";
            }
            else if (!string.IsNullOrEmpty(Amount))
            {
                decimal Num;
                bool isNum = decimal.TryParse(Amount, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid Amount.";
                }
                else if (Convert.ToDecimal(Amount) < 10)
                {
                    msg = "Please enter Amount greater or equal to Rs 10";
                }
            }
            if (string.IsNullOrEmpty(msg) && (string.IsNullOrEmpty(MemberId) || MemberId == "0"))
            {
                msg = "Please enter MemberId.";
            }
            else if (string.IsNullOrEmpty(msg) && (!string.IsNullOrEmpty(MemberId)))
            {
                Int64 Num;
                bool isNum = Int64.TryParse(MemberId, out Num);
                if (!isNum)
                {
                    msg = "Please enter valid MemberId.";
                }
            }

            if (string.IsNullOrEmpty(msg))
            {
                if (string.IsNullOrEmpty(SenderMobile))
                {
                    msg = "Please enter SenderMobile.";
                }
                else if (string.IsNullOrEmpty(SenderName))
                {
                    msg = "Please enter SenderName.";
                }
                else if (string.IsNullOrEmpty(Reference))
                {
                    msg = "Please enter Reference.";
                }
                else if (string.IsNullOrEmpty(Version))
                {
                    msg = "Please enter Version.";
                }
                else if (string.IsNullOrEmpty(DeviceCode))
                {
                    msg = "Please enter DeviceCode.";
                }
                else if (string.IsNullOrEmpty(PlatForm))
                {
                    msg = "Please enter PlatForm.";
                }
                else if (resGetRecord == null || resGetRecord.Id == 0)
                {
                    msg = "Please enter Valid MemberId.";
                }
                else if (resGetRecord.IsKYCApproved != (int)AddUser.kyc.Verified)
                {
                    msg = Common.GetKycMessage(resGetRecord, Convert.ToDecimal(Amount));
                }
                if (string.IsNullOrEmpty(msg))
                {
                    string JsonReq = FonePayCommon.FormatQR(Newtonsoft.Json.JsonConvert.SerializeObject(objPayReq));
                    VendorApi_CommonHelper.FonePayAPIURL = "fonepayQrSwitch/pushPayment/merchant";
                    string ApiUrl= "fonepayQrSwitch/pushPayment/merchant";
                    int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.FonePay_QR_Payments;
                    string API_URL = VendorApi_CommonHelper.FonePay_API_URL_Base_Prefix + VendorApi_CommonHelper.FonePayAPIURL;
                    WalletType = Convert.ToString((int)WalletTransactions.WalletTypes.FonePay);
                    WalletBalance = Convert.ToDecimal(resGetRecord.TotalAmount);
                    AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(resGetRecord.MemberId.ToString(), Amount, VendorApiType.ToString());
                    if (WalletBalance < Convert.ToDecimal(objOut.NetAmount))
                    {
                        msg = Common.InsufficientBalance;
                    }
                    else
                    {
                        string MemberName = resGetRecord.FirstName + " " + resGetRecord.MiddleName + " " + resGetRecord.LastName;
                        if (resGetRecord == null || resGetRecord.Id == 0)
                        {
                            msg = "MemberId not found";
                        }
                        else if (resGetRecord.IsKYCApproved != (int)AddUser.kyc.Verified)
                        {
                            msg = Common.GetKycMessage(resGetRecord, Convert.ToDecimal(Amount));
                        }
                        else if (resGetRecord.IsActive == false)
                        {
                            msg = "Your account is not active.";
                        }
                        if (string.IsNullOrEmpty(msg))
                        {
                            string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(VendorApiType)).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(ApiUrl,Reference, resGetRecord.MemberId, MemberName, JsonReq, authenticationToken, UserInput, DeviceCode, PlatForm, VendorApiType, "", "", API_URL, ((int)VendorApi_CommonHelper.VendorTypes.FonePay).ToString());
                            string TransactionUniqueId = VendorApi_CommonHelper.UpdateWalletBalance(resCoupon, ref TransactionID,  BankTransactionId, WalletType, CustomerId, Amount, out msg, VendorApiType, resGetRecord, objVendor_API_Requests, "", out WalletBalance, Remarks, Purpose);
                            if (msg == "success")
                            {
                                msg = FonePayCommon.GetFonePayQRPaymentRequest(JsonReq, ref objRes);
                                string VendorOutputResponse = Newtonsoft.Json.JsonConvert.SerializeObject(objRes);
                                objVendor_API_Requests = VendorApi_CommonHelper.UpdateVendorResponse_FonePay(JsonReq, DeviceCode, PlatForm, VendorApiType, VendorApiTypeName, ref objRes, objVendor_API_Requests.Id, "", VendorOutputResponse, qrRequestMessage);
                                if (objVendor_API_Requests.Id != 0 && objRes.responseCode == "RES000" && !string.IsNullOrEmpty(objRes.transactionIdentifier))
                                {
                                    msg = Common.UpdateCompleteTransaction(ref IsCouponUnlocked, ref TransactionID, resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, "", Remarks, RecieverAccountNo_Decrypted);
                                    if (msg.ToLower() == "success")
                                    {
                                        string Title = "Transaction successfull";
                                        string Message = $"QR Code payment of amount Rs.{Amount} has been completed successfully.";//TransactionId " + resKhalti.TransactionUniqueId + " success for " + VendorApiTypeName;
                                        Models.Common.Common.SendNotification(authenticationToken, VendorApiType, resGetRecord.MemberId, Title, Message);
                                    }
                                }
                                else
                                {
                                    msg = Common.RefundUpdateTransaction(resGetRecord, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, BankTransactionId, VendorApiType, WalletType, PlatForm, DeviceCode);
                                }
                            }
                        }
                    }
                    //AddUser outobject = new AddUser();
                    //GetUser inobject = new GetUser();
                    //inobject.MemberId = Convert.ToInt64(resGetRecord.MemberId);
                    //AddUser resGet = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
                    //objRes.WalletBalance = Convert.ToDecimal(resGet.TotalAmount).ToString();
                }
            }
            return msg;
        }
    }
}