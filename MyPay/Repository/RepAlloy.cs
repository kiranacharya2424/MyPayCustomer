
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

namespace MyPay.Repository
{
    public class RepAlloy
    {

        public static Int64 Id = 0;
        public static string UniqueTransactionId = string.Empty;
        public static decimal WalletBalance = 0;

        private static ILog log = LogManager.GetLogger(typeof(RepFonePay));

        public static void SampleRequest()
        {
            try
            {
                string JsonString = string.Empty;
                Req_Alloy_EndUser objReq = new Req_Alloy_EndUser();
                objReq.email = $"testRest{Common.RandomNumber(10, 100)}@email.com";
                Alloy_EndUser_Person objPerson = new Alloy_EndUser_Person();
                objPerson.first_name = "Rest" + Common.RandomNumber(10, 100);
                objPerson.last_name = "Rest" + Common.RandomNumber(10, 100);


                Alloy_EndUser_Address objAdderss = new Alloy_EndUser_Address();
                objAdderss.address_postal_code = "110002";
                objAdderss.address_city = "Pokhara";
                objAdderss.address_iso_country = "NP";
                objAdderss.address_region = "Pokhara";
                objAdderss.address_number = "10354478";
                objAdderss.address_street = "Street 32";
                objAdderss.address_postal_code = "110002";

                objPerson.address = objAdderss;
                objPerson.name = "Rahul" + Common.RandomNumber(10, 100);
                objPerson.nationality = "Nepali";
                objPerson.date_of_birth = System.DateTime.Now.AddYears(-Common.RandomNumber(10, 20)).ToString("yyyy-MM-dd");
                objPerson.telephone = Common.RandomNumber(91111, 99999).ToString() + Common.RandomNumber(91111, 99999).ToString();
                objReq.person = objPerson;


                Alloy_EndUser_Questionnaire objQuest = new Alloy_EndUser_Questionnaire();
                objQuest.occupationtype = "Domestic";
                objQuest.employmentstatus = "Active";
                objQuest.incomeband = "100000";

                objReq.person.questionnaire = objQuest;
                JsonString = JsonConvert.SerializeObject(objReq);
                string Signature = GenerateSignature(JsonString);
                string API_NAME = "https://j8oribek4b.execute-api.eu-west-2.amazonaws.com/sandbox/customer/endusers";
                AlloyRequest(JsonString, Signature, VendorApi_CommonHelper.ALLOY_TOKEN, API_NAME);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }


        public static void  SampleRequest2()
        {
            try
            {
                string JsonString = string.Empty;
                Req_Alloy_CorporateUser objReq = new Req_Alloy_CorporateUser();
                objReq.phoneNumber = Common.RandomNumber(91111, 99999).ToString() + Common.RandomNumber(91111, 99999).ToString();
                objReq.email = $"testRest{Common.RandomNumber(10, 100)}@email.com";
                Corporate_Company objCompany = new Corporate_Company();
                objCompany.type = "test";
                objCompany.name = "Rahul" + Common.RandomNumber(10, 100);
                objCompany.registrationNumber = "RG" + Common.RandomNumber(10, 1000);
                objCompany.registrationCountry = "NP";
                objCompany.houseId = "06304772";

                Corporate_BusinessAddress objAddress = new Corporate_BusinessAddress();
                objAddress.addressLine1 = "Pokhara 110002";
                objAddress.addressLine2 = "Street 32";
                objAddress.state = "Pokhara";
                objAddress.city = "Pokhara";
                objAddress.country = "NP";
                objAddress.postalCode = "110002";
                objCompany.businessAddress = objAddress;
                objReq.company = objCompany;

                JsonString = JsonConvert.SerializeObject(objReq);
                string Signature = GenerateSignature(JsonString);
                string API_NAME = "https://j8oribek4b.execute-api.eu-west-2.amazonaws.com/sandbox/customer/corporates";
                AlloyRequest(JsonString, Signature, VendorApi_CommonHelper.ALLOY_TOKEN, API_NAME);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }


        public static void SampleRequest3(string enduser_id)
        {
            try
            {
                string JsonString = string.Empty;
                Req_Alloy_UserKyc objReq = new Req_Alloy_UserKyc();
                objReq.provider = "sumsub";

                JsonString = JsonConvert.SerializeObject(objReq);
                string Signature = GenerateSignature(JsonString);
                string API_NAME = "https://j8oribek4b.execute-api.eu-west-2.amazonaws.com/sandbox/customer/endusers/" + enduser_id + "/kyc";
                AlloyRequest(JsonString, Signature, VendorApi_CommonHelper.ALLOY_TOKEN, API_NAME);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public static string GenerateSignature(string JsonString)
        {
            string signature = "";
            try
            {
                string filepath = HttpRuntime.AppDomainAppPath + "/Certificates/AlloyFintech/AlloyFintech_PrivateKey.txt";
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

        private static string AlloyRequest(string json, string signature, string access_token, string APIName)
        {
            try
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
                request2.Headers["DigitalSignature"] = signature;
                request2.Headers["Token"] = access_token;
                Stream requestStream = request2.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Flush();
                requestStream.Close();

                HttpWebResponse response2 = default(HttpWebResponse);
                response2 = (HttpWebResponse)request2.GetResponse();
                Stream responseStream = response2.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();


                //using (var client = new HttpClient())
                //{
                //    using (var multipartFormDataContent = new MultipartFormDataContent())
                //    {
                //        var values = new[]
                //        {
                //            new KeyValuePair<string, string>("Id", Guid.NewGuid().ToString()),
                //            new KeyValuePair<string, string>("Key", "awesome"),
                //            new KeyValuePair<string, string>("From", "nitinemail@emailshome.com")
                //        };

                //        foreach (var keyValuePair in values)
                //        {
                //            multipartFormDataContent.Add(new StringContent(keyValuePair.Value),
                //                String.Format("\"{0}\"", keyValuePair.Key));
                //        }

                //        multipartFormDataContent.Add(new ByteArrayContent(File.ReadAllBytes("test.txt")), '"' + "File" + '"', '"' + "test.txt" + '"');

                //        var requestUri = "http://apidomain.com/api/uri/apiname/";
                //        var result = client.PostAsync(requestUri, multipartFormDataContent).Result;
                //    }
                //}

                return responseStr;

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
                        var jsonEx = reader.ReadToEnd(); 
                        return jsonEx;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}