
using MyPay.Models.Add;
using MyPay.Models.Get;
using MyPay.Models.Get.FonePay;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MyPay.Models.Common
{
    public static class FonePayCommon
    {
        public static string TestUserAccountnoBank_Encrypted = "YkFU5RzjOlPzZoOj/cWvKEQUwJzlnWjDiKPO/pYSBOxwUWlkuUGcwCrdB9vQjtgIo48EvqU88OZZo1I8p+IecrbTZGv4l1TqzJVD+Rm3pIFh0xUDa9AWfyMOvoJWSGCVMjY9gQSFwQ7nxEzcWiYA0WP5geFtWbYx9maLUMarT3fctsdVMBTXHXvYprI7eLSY9zvuiskEvl5dQKj6BN8KbqdoTk2i3jizsy7UYjJ6DRtVJ2XoWdTfKAl0ac5dOCh/szJOPwdO+1ULYZ0BWXz6rm8k/vgFu5iuFV0y8DouLUn9fslM4URiqL8hnYOjqmIJrhf4a+MIlHD5Gro8YC+MNA==";
        //public static string IBFTEncryptedAuthString = "FKtXLwrKPmf4UVDQPgJonFCliCOqJ69CavZvGvAPZtepqHoyPGC18DiR9L5yLaarbOUandb9eD1f0PYrq/ue0H1eyb+7jnMTHXHlBql0yPwiE7BD5/ZAtxlvbL7xCSBiOc4HypatJz4m2nuswIVPatjarFb0irk5vBmy+FP/iX0f9RZS4Xx/M7eqzqK1VWOfCmjLeQS2seP5kVmJvvt1SIpnPTdg+sHtIA8vjKGQpAKA/exCbqpaIec+li95WiXkF+aNuUcxeg11YETOvqn/rNHUSqpTsezwhvYemfj2RCy7A549u90hdi39J3oRQjUYwd6+Bu5m6uVlE6qbEFFTcQ==";
        public static string GetFonePayQRRequest(string json, ref GetVendor_API_FonePay_QR_Parse_Lookup_Response obj)
        {
            try
            { 
                string signature = RepFonePay.GenerateSignature(json);
                //string AuthTokenResult = RepFonePay.GenerateFonePayAuthToken();
                //FonePayRoot objFonePayRoot = JsonConvert.DeserializeObject<FonePayRoot>(AuthTokenResult);              
                FonePayRoot objFonePayRoot = new FonePayRoot();
                string APIName = VendorApi_CommonHelper.FonePay_API_URL_Base_Prefix + VendorApi_CommonHelper.FonePayAPIURL;
                string Response = FonePayQRRequest(json, signature, objFonePayRoot, APIName);
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_FonePay_QR_Parse_Lookup_Response>(Response);
                Common.AddLogs($"FonePay APIName: {APIName} Request: {json} Response: {Response}", true, (int)AddLog.LogType.DBLogs);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            } 
        }

        public static string GetFonePayQRPaymentRequest(string json, ref GetVendor_API_FonePay_Payment_Response obj)
        {
            try
            { 
                string signature = RepFonePay.GenerateSignature(json);
                //string AuthTokenResult = RepFonePay.GenerateFonePayAuthToken();
                //FonePayRoot objFonePayRoot = JsonConvert.DeserializeObject<FonePayRoot>(AuthTokenResult);
                FonePayRoot objFonePayRoot = new FonePayRoot();
                string APIName = VendorApi_CommonHelper.FonePay_API_URL_Base_Prefix + VendorApi_CommonHelper.FonePayAPIURL;
                string Response = FonePayQRRequest(json, signature, objFonePayRoot, APIName);
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_FonePay_Payment_Response>(Response);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string GetFonePayQRPayment_CheckStatus(string json, ref GetVendor_API_FonePay_QR_Parse_CheckStatus_Response obj)
        {

            try
            {
                string SampleValue = $"{VendorApi_CommonHelper.FonePay_UserName}:{VendorApi_CommonHelper.FonePay_Password}";
                string ibftUserNamePwdEncrypt = RepFonePay.FonePay_EncryptData_From_Publickey(SampleValue);
                string signature = RepFonePay.GenerateSignature(json);
                //string AuthTokenResult = RepFonePay.GenerateFonePayAuthToken();
                //FonePayRoot objFonePayRoot = JsonConvert.DeserializeObject<FonePayRoot>(AuthTokenResult);
                FonePayRoot objFonePayRoot = new FonePayRoot();
                string APIName = VendorApi_CommonHelper.FonePay_API_URL_Base_Prefix + VendorApi_CommonHelper.FonePayAPIURL;
                string Response = FonePayQRRequest(json, signature, objFonePayRoot, APIName);
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_FonePay_QR_Parse_CheckStatus_Response>(Response);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string FormatQR(string input)
        {
            //return input.Replace(" ", "").Trim().Replace("\r\n", "").Trim();
            return input.Replace("\r\n", "").Trim();
        }
        private static string FonePayQRRequest(string json, string signature, FonePayRoot objFonePayRoot, string APIName)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpWebRequest request2 = (HttpWebRequest)System.Net.WebRequest.Create(APIName);
            byte[] bytes = null;
            bytes = System.Text.Encoding.ASCII.GetBytes(json);
            request2.ContentType = "application/json";
            request2.ContentLength = bytes.Length;
            request2.Method = "POST";
            request2.KeepAlive = false;
            request2.Timeout = System.Threading.Timeout.Infinite;
            request2.Headers["bin"] = VendorApi_CommonHelper.FonePay_IssuerBin;
            request2.Headers["signature"] = signature;
           // request2.Headers["Authorization"] = "Bearer " + objFonePayRoot.access_token;
            request2.Headers["PROXY-API-USERNAME"] = VendorApi_CommonHelper.FonePay_ProxyUserName;
            request2.Headers["PROXY-API-KEY"] = VendorApi_CommonHelper.FonePay_ProxyApiKey;
            Stream requestStream = request2.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Flush();
            requestStream.Close();

            HttpWebResponse response2 = default(HttpWebResponse);
            response2 = (HttpWebResponse)request2.GetResponse();
            Stream responseStream = response2.GetResponseStream();
            string responseStr = new StreamReader(responseStream).ReadToEnd();
            return responseStr;
        }
    }
    public class NavigationRoleResponse
    {
        public string uiName { get; set; }
        public List<FonePayRole> roles { get; set; }
        public object position { get; set; }
        public string name { get; set; }
        public object navigation { get; set; }
        public string description { get; set; }
    }
    public class FonePayRole
    {
        public string name { get; set; }
        public string description { get; set; }
        public string uiName { get; set; }
        public object navigation { get; set; }
        public object position { get; set; }
        public List<object> childRoles { get; set; }
    }
    public class FonePayRoot
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public int userId { get; set; }
        public string tokenCreatedDate { get; set; }
        public bool tempToken { get; set; }
        public List<NavigationRoleResponse> navigationRoleResponse { get; set; }
        public string username { get; set; }
        public int refresh_token_expires_in { get; set; }
    }
}