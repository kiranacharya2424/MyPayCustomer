using DocumentFormat.OpenXml.Wordprocessing;
using log4net;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyPay.Repository
{
    public class RepNCHL
    {
        private static ILog log = LogManager.GetLogger(typeof(RepNCHL));
        public static string privateFile_NCHLQR = "/Certificates/NPI.pem";
        public static string publicFile_NCHLQR_cer= "/Certificates/NQR.cer"; 
        public static string publicFile_NCHLQR= "/Certificates/public_key.pem";


        public static string privateKeyLIVE_NCHLQR = "/Certificates/NepalPayQRLIVEPrivatekey.pem";
        public static string publicKeyLIVE_NCHLQR = "/Certificates/NepalPayQRLive_PublicKey.pem";
        public static string pfxLIVE_NCHLQR = "/Certificates/SMARTCARD.pfx";
        public static string pfxPasswordLIVE_LinkBank = "smartcard@123";
        public static string encryptionsrlnoLIVE = "";


        // *************** LIVE CREDENTIALS ***************** //
        public static string NCHLApiUrl = "https://login.connectips.com/";
        public static string NCHLWithdrawAPIURl = "https://182.93.93.107:7444/";
        //public static string NCHLWithdrawAPIURl = "http://demo.connectips.com:6065/";
        //public static string APPID = "MER-527-APP-1";
        //public static string MERCHANTID = "527";
        public static string APPID = "WAL-504-SMR-1";
        public static string MERCHANTID = "504";
        public static string UserId = "";
        public static string APPNAME = "MyPay";
        public static string WithdrawBasicApiKey = "c21hcnRjYXJkOkJhczFjQCRtciFjcmRAJG1y";
        public static string withdrawusername = "SMARTCARD@999";
        public static string withdrawpassword = "123R3M@U5er$m@r!@3mt";
        private static string BasicAPIKEY = "V0FMLTUwNC1TTVItMTpNeVBANTA0eVk=";
        public static string participantId = "SMARTCARD@999";
        public static string PFXFile = "/Certificates/SMARTCARD.pfx";
        public static string PFXPassword = "smartcard@123";
        public static string TokenizationURL = "https://tokenization.connectips.com/tokenization-gw/loginpage";
        public static string TokenizationLinkBankAPIURl = "https://182.93.93.107/";
        public static string APPID_STAGE_PAYMENT_LIVE = "R2P-504-APP-2";

        // ************ TEST CREDENTIALS  *************** //
        public static string NCHLApiUrl_LinkBank = "https://uat.connectips.com/";
        public static string NCHLWithdrawAPIURl_LinkBank = "http://demo.connectips.com:6065/";
        public static string APPID_LinkBank = "MER-527-APP-1";
        public static string APPID_NCHL_STAGING = "MER-527-APP-1";
        public static string UserId_LinkBank = "";
        public static string APPNAME_LinkBank = "MyPay";
        public static string APPNAME_Password_LinkBank = "Abcd@123";
        public static string WithdrawBasicApiKey_LinkBank = "bXlwYXk6QWJjZEAxMjM=";
        public static string withdrawusername_LinkBank = "MYPAY@999";
        public static string withdrawpassword_LinkBank = "123Abcd@123";

        //public static string withdrawusername_staging = "MYPAY@999";
        //public static string withdrawpassword_staging = "123Abcd@123";
        //public static string WithdrawBasicApiKey_staging =  "bXlwYXk6QWJjZEAxMjM=";
        //public static string PFXFile_staging = "/Certificates/NPI.cer";
        /// <summary>
        /// public static string PFXPassword_staging = "123";
        /// </summary>

        public static string participantId_LinkBank = "MyPay";
        public static string PFXFile_LinkBank = "/Certificates/NPI.pfx";
        public static string PFXPassword_LinkBank = "123";
        public static string TokenizationURL_LinkBank = "https://uat.connectips.com/tokenization-gw/loginpage";
        public static string TokenizationLinkBankAPIURl_LinkBank = "http://demo.connectips.com:9095/";


        public static string withdrawusername_staging = "MYPAY@999";
        public static string withdrawpassword_staging = "123Abcd@123";
        public static string WithdrawBasicApiKey_staging = "bXlwYXk6QWJjZEAxMjM=";
        public static string PFXFile_staging = "/Certificates/NPI.cer";
        public static string PFXPassword_staging = "123";

        //// ************ LIVE CREDENTIALS  *************** //

        ////  *********** WE ARE SETTING THE LIVE CREDENTIALS IN BASICAUTHENTICATION AND ***************
        ////  *********** USERAUTHORIZATION CONTROLLER  *************** //

        //public static string NCHLApiUrl_LinkBank = NCHLApiUrl;
        //public static string NCHLWithdrawAPIURl_LinkBank = NCHLWithdrawAPIURl;
        //public static string APPID_LinkBank = APPID;
        //public static string APPID_NCHL_STAGING = APPID_STAGE_PAYMENT_LIVE;
        //public static string UserId_LinkBank = UserId;
        //public static string APPNAME_LinkBank = APPNAME;
        //public static string WithdrawBasicApiKey_LinkBank = WithdrawBasicApiKey;
        //public static string withdrawusername_LinkBank = withdrawusername;
        //public static string withdrawpassword_LinkBank = withdrawpassword;
        //public static string participantId_LinkBank = "MYPAY";
        //public static string PFXFile_LinkBank = PFXFile;
        //public static string PFXPassword_LinkBank = PFXPassword;
        //public static string TokenizationURL_LinkBank = TokenizationURL;
        //public static string TokenizationLinkBankAPIURl_LinkBank = TokenizationLinkBankAPIURl;

        public static string ConnectRequest(string TXNID, string TXNCRNCY, string REFERENCEID, int TXNAMT, string REMARKS, string PARTICULARS)
        {

            //Dictionary<string, string> data = new Dictionary<string, string>();
            //data.Add("APPID", APPID);
            //data.Add("MERCHANTID", MERCHANTID);
            //data.Add("APPNAME", APPNAME);
            //data.Add("TXNID", TXNID);
            //data.Add("TXNDATE", DateTime.UtcNow.ToString("dd-MM-yyyy"));
            //data.Add("TXNCRNCY", TXNCRNCY);
            //data.Add("TXNAMT", TXNAMT.ToString());
            //data.Add("REFERENCEID", REFERENCEID);
            //data.Add("REMARKS", REMARKS);
            //data.Add("PARTICULARS", PARTICULARS);
            //data.Add("TOKEN", Common.GenerateConnectIPSToken("MERCHANTID=" + MERCHANTID + ",APPID=" + APPID + ",APPNAME=" + APPNAME + ",TXNID=" + TXNID + ",TXNDATE=" + obj.TXNDATE + ",TXNCRNCY=" + TXNCRNCY + ",TXNAMT=" + TXNAMT.ToString() + ",REFERENCEID=" + REFERENCEID + ",REMARKS=" + REMARKS + ",PARTICULARS=" + PARTICULARS + ",TOKEN=TOKEN"));
            //string json = JsonConvert.SerializeObject(data);
            Add_ConnectIPS obj = new Add_ConnectIPS();
            obj.APPID = APPID;
            obj.MERCHANTID = MERCHANTID;
            obj.APPNAME = APPNAME;
            obj.TXNID = TXNID;
            obj.TXNDATE = DateTime.UtcNow.ToString("dd-MM-yyyy");
            obj.TXNCRNCY = TXNCRNCY;
            obj.TXNAMT = TXNAMT;
            obj.REFERENCEID = REFERENCEID;
            obj.REMARKS = REMARKS;
            obj.PARTICULARS = PARTICULARS;
            obj.TOKEN = Common.GenerateConnectIPSToken("MERCHANTID=" + obj.MERCHANTID + ",APPID=" + obj.APPID + ",APPNAME=" + obj.APPNAME + ",TXNID=" + obj.TXNID + ",TXNDATE=" + obj.TXNDATE + ",TXNCRNCY=" + obj.TXNCRNCY + ",TXNAMT=" + obj.TXNAMT.ToString() + ",REFERENCEID=" + obj.REFERENCEID + ",REMARKS=" + obj.REMARKS + ",PARTICULARS=" + obj.PARTICULARS + ",TOKEN=TOKEN");
            NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
            outgoingQueryString.Add("MERCHANTID", obj.MERCHANTID);
            outgoingQueryString.Add("APPID", obj.APPID);
            outgoingQueryString.Add("APPNAME", obj.APPNAME);
            outgoingQueryString.Add("TXNID", obj.TXNID);
            outgoingQueryString.Add("TXNDATE", obj.TXNDATE);
            outgoingQueryString.Add("TXNCRNCY", obj.TXNCRNCY);
            outgoingQueryString.Add("TXNAMT", obj.TXNAMT.ToString());
            outgoingQueryString.Add("REFERENCEID", obj.REFERENCEID);
            outgoingQueryString.Add("REMARKS", obj.REMARKS);
            outgoingQueryString.Add("PARTICULARS", obj.PARTICULARS);
            outgoingQueryString.Add("TOKEN", obj.TOKEN);
            string postData = outgoingQueryString.ToString();
            //string postData = "MERCHANTID=" + obj.MERCHANTID + "&APPID=" + obj.APPID + "&APPNAME=" + obj.APPNAME + "&TXNID=" + obj.TXNID + "&TXNDATE=" + obj.TXNDATE + "&TXNCRNCY=" + obj.TXNCRNCY + "&TXNAMT=" + obj.TXNAMT.ToString() + "&REFERENCEID=" + obj.REFERENCEID + "&REMARKS=" + obj.REMARKS + "&PARTICULARS=" + obj.PARTICULARS + "&TOKEN=" + obj.TOKEN);
            return postwithdrawformmethod("connectipswebgw/loginpage", postData);
        }

        public static string postwithdrawformmethod(string apiname, string postData)
        {
            try
            {
                string responseFromServer = "";
                // Create a request using a URL that can receive a post.
                WebRequest request = WebRequest.Create(NCHLWithdrawAPIURl + apiname);

                if (!Common.ApplicationEnvironment.IsProduction)
                {
                    request = WebRequest.Create(NCHLWithdrawAPIURl_LinkBank + apiname);
                    WithdrawBasicApiKey = WithdrawBasicApiKey_staging;
                }
                

                // Set the Method property of the request to POST.
                request.Method = "POST";
                request.Headers.Add("Authorization", "Basic " + WithdrawBasicApiKey);
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

        public static string postwithdrawformmethodLocal(string apiname, string postData)
        {
            try
            {
                string responseFromServer = "";
                // Create a request using a URL that can receive a post.
                WebRequest request = WebRequest.Create(TokenizationLinkBankAPIURl_LinkBank + apiname);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                request.Headers.Add("Authorization", "Basic " + WithdrawBasicApiKey_LinkBank);
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

        public static async Task<TResult> PostFormUrlEncoded<TResult>(string url, Dictionary<string, string> postData)
        {
            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    return await response.Content.ReadAsAsync<TResult>();
                }
            }
        }

        public static string PostMethod(string ApiName, string requestdata)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(NCHLApiUrl + ApiName);
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

        public static List<BankList> GetDataBankList()
        {
            MyPayEntities context = new MyPayEntities();
            List<BankList> list = context.BankLists.ToList();
            list = list.Where(c => c.IsActive == true && c.IsDeleted == false).ToList();
            List<BankList> newlist = new List<BankList>();
            foreach (var item in list)
            {
                BankList obj = new BankList();
                obj.ICON_NAME = Common.LiveSiteUrl + "/Content/assets/Images/BanksLogos/M-Banking/" + item.ICON_NAME;
                obj.BANK_CD = item.BANK_CD;
                obj.BANK_NAME = item.BANK_NAME;
                obj.BRANCH_CD = item.BRANCH_CD;
                obj.Id = item.Id;
                obj.SHORTCODE = item.SHORTCODE;
                obj.BRANCH_NAME = item.BRANCH_NAME;
                newlist.Add(obj);
            }
            newlist.Sort((x, y) => Convert.ToString(x.BANK_NAME).CompareTo(Convert.ToString(y.BANK_NAME)));
            return newlist;
        }

        public static string PostMethodWithToken(string ApiName, string requestdata, string token)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(NCHLWithdrawAPIURl + ApiName);

                HttpWebRequest request = null;
                if (!Common.ApplicationEnvironment.IsProduction)
                {
                    request = (HttpWebRequest)System.Net.WebRequest.Create(NCHLWithdrawAPIURl_LinkBank + ApiName);
                }
                else
                {
                    request = (HttpWebRequest)System.Net.WebRequest.Create(NCHLWithdrawAPIURl + ApiName);
                }



                byte[] bytes = null;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + token);
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
                    Common.AddLogs("Error NCHLPOstMethod: ApiName " + ApiName + ". Request: " + requestdata + " Response: " + e.Message, false, 1, Common.CreatedBy, Common.CreatedByName, true);
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
                            Common.AddLogs(text, false, 1, 10000, "", true);
                            return text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string PostWithdrawMethod(string ApiName, string requestdata)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(NCHLWithdrawAPIURl + ApiName);
                byte[] bytes = null;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Basic " + WithdrawBasicApiKey);

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
        public static string GetMethod(string ApiName)
        {
            string responseStr = "";
            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(NCHLApiUrl + ApiName);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        responseStr = new StreamReader(stream).ReadToEnd();
                        stream.Flush();
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                responseStr = ex.Message;
            }
            return responseStr;
        }

        public static string gettoken()
        {
            string token = "";
            try
            {
                AddNCOauth obj = new AddNCOauth();
                if (!Common.ApplicationEnvironment.IsProduction)
                {
                    obj.username= RepNCHL.withdrawusername_staging;
                    obj.password = RepNCHL.withdrawpassword_staging;
                }
                
                string result = RepNCHL.postwithdrawformmethod("oauth/token", "grant_type=" + obj.grant_type + "&username=" + obj.username + "&password=" + obj.password);
                if (!string.IsNullOrEmpty(result))
                {
                    GetNCAuthToken res = new GetNCAuthToken();
                    try
                    {
                        res = JsonConvert.DeserializeObject<GetNCAuthToken>(result);
                    }
                    catch (Exception ex)
                    {

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


        public static string gettoken_LinkBank()
        {
            string token = "";
            try
            {
                AddNCOauth obj = new AddNCOauth();
                string result = RepNCHL.postwithdrawformmethodLocal("oauth/token", "grant_type=" + obj.grant_type + "&username=" + withdrawusername_LinkBank + "&password=" + withdrawpassword_LinkBank);
                if (!string.IsNullOrEmpty(result))
                {
                    GetNCAuthToken res = new GetNCAuthToken();
                    Common.AddLogs($"token: {(TokenizationLinkBankAPIURl_LinkBank + "oauth/token") + " " + "grant_type=" + obj.grant_type + "&username=" + withdrawusername_LinkBank + "&password=" + withdrawpassword_LinkBank} WithdrawBasicApiKey_LinkBank:{WithdrawBasicApiKey_LinkBank} Response : {result}", false, (int)AddLog.LogType.DBLogs);

                    try
                    {
                        res = JsonConvert.DeserializeObject<GetNCAuthToken>(result);

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

        public static List<GetNCBankList> GetBankList(string type)
        {
            List<GetNCBankList> result = new List<GetNCBankList>();
            try
            {
                string token = gettoken();
                if (!token.Contains("Error"))
                {
                    string data = "";
                    if (type == "cips")
                    {
                        data = PostMethodWithToken("api/getcipsbanklist", "", token);
                        Common.AddLogs($"getcipsbanklist:{data}", false, (int)AddLog.LogType.DBLogs);
                    }
                    else if (type == "valid")
                    {
                        data = PostMethodWithToken("api/getacvalidationenabledbanklist", "", token);
                    }
                    else if (type == "reversal")
                    {
                        data = PostMethodWithToken("api/getreversalenabledbanklist", "", token);
                    }
                    else
                    {
                        data = PostMethodWithToken("api/getbanklist", "", token);
                    }
                    Common.AddLogs($"GetNCBankList: {type} data: {data}", false, (int)AddLog.LogType.DBLogs);
                    if (!string.IsNullOrEmpty(data))
                    {
                        result = JsonConvert.DeserializeObject<List<GetNCBankList>>(data);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static List<GetNCBranchList> GetBranchList(string type, string bankid)
        {
            List<GetNCBranchList> result = new List<GetNCBranchList>();
            try
            {
                string token = gettoken();
                if (!token.Contains("Error"))
                {
                    string data = "";
                    string ApiName = "";
                    if (type == "cips" && !string.IsNullOrEmpty(bankid))
                    {
                        ApiName = "api/getcipsbranchlist/" + bankid;
                    }

                    else if (!string.IsNullOrEmpty(bankid))
                    {
                        ApiName = "api/getbranchlist/" + bankid;
                    }
                    else if (type == "cips")
                    {
                        ApiName = "api/getcipsbranchlist";
                    }
                    else
                    {
                        ApiName = "api/getbranchlist";
                    }
                    data = PostMethodWithToken(ApiName, "", token);
                    Common.AddLogs($"GetBranchList: ApiName:{ApiName} Response: {data}", false, (int)AddLog.LogType.DBLogs);
                    if (!string.IsNullOrEmpty(data))
                    {
                        result = JsonConvert.DeserializeObject<List<GetNCBranchList>>(data);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static List<GetAllCipsTransctions> GetAllBankTransactions(string StartDate, string EndDate, ref string msg)
        {
            List<GetAllCipsTransctions> objRes = new List<GetAllCipsTransctions>();
            try
            {

                if (string.IsNullOrEmpty(StartDate))
                {
                    msg = "Please enter start date.";
                }
                else if (string.IsNullOrEmpty(EndDate))
                {
                    msg = "Please end date.";
                }

                if (string.IsNullOrEmpty(msg))
                {


                    Req_CipsBankTransaction bank = new Req_CipsBankTransaction();
                    bank.txnDateFrom = StartDate;
                    bank.txnDateTo = EndDate;
                    string token = RepNCHL.gettoken();
                    string data = RepNCHL.PostMethodWithToken("api/getcipstxnlistbydate", JsonConvert.SerializeObject(bank), token);
                    if (!string.IsNullOrEmpty(data))
                    {

                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetAllCipsTransctions>>(data);
                        msg = "success";
                    }
                    else
                    {
                        msg = "Data Not Found";
                    }


                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return objRes;
        }

        public static string RefundBankTransfer(CipsBatchDetail objRes, AddUserLoginWithPin resuserdetails, AddDepositOrders resDeposit, string data, string PlatForm, string DeviceCode, string TransactionUniqueId)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside RepNCHL RefundBankTransfer start " + Environment.NewLine);
            try
            {
                WalletTransactions oldres_transaction = new WalletTransactions();
                oldres_transaction.Sign = (int)WalletTransactions.Signs.Debit;
                if (Convert.ToDateTime(resDeposit.CreatedDate) >= Convert.ToDateTime("15-Jun-2022 13:30"))
                {
                    oldres_transaction.Reference = TransactionUniqueId;
                }
                else
                {
                    oldres_transaction.TransactionUniqueId = TransactionUniqueId;
                }
                if (oldres_transaction.GetRecord())
                {
                    oldres_transaction.VendorTransactionId = objRes.cipsTransactionDetailList[0].id;
                    oldres_transaction.BatchTransactionId = objRes.id;
                    oldres_transaction.TxnInstructionId = objRes.cipsTransactionDetailList[0].id;
                    oldres_transaction.Status = (int)WalletTransactions.Statuses.Failed;
                    oldres_transaction.GatewayStatus = objRes.cipsTransactionDetailList[0].creditStatus.ToString();
                    oldres_transaction.ResponseCode = objRes.cipsTransactionDetailList[0].reasonDesc;
                    if (oldres_transaction.Update())
                    {
                        decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuserdetails.TotalAmount) + Convert.ToDecimal(objRes.cipsTransactionDetailList[0].amount) + resDeposit.ServiceCharges);
                        WalletTransactions res_transaction = new WalletTransactions();
                        res_transaction.Sign = (int)WalletTransactions.Signs.Credit;
                        res_transaction.Status = (int)WalletTransactions.Statuses.Refund;
                        res_transaction.ParentTransactionId = objRes.cipsTransactionDetailList[0].instructionId;
                        if (!res_transaction.GetRecordCheckExists())
                        {
                            string TransactionId = new CommonHelpers().GenerateUniqueId();
                            res_transaction.MemberId = Convert.ToInt64(resuserdetails.MemberId);
                            res_transaction.ContactNumber = resuserdetails.ContactNumber;
                            res_transaction.MemberName = resuserdetails.FirstName + " " + resuserdetails.MiddleName + " " + resuserdetails.LastName;
                            res_transaction.Amount = Convert.ToDecimal(objRes.cipsTransactionDetailList[0].amount) + resDeposit.ServiceCharges;
                            res_transaction.UpdateBy = Convert.ToInt64(resuserdetails.MemberId);
                            res_transaction.UpdateByName = resuserdetails.FirstName + " " + resuserdetails.MiddleName + " " + resuserdetails.LastName;
                            res_transaction.CurrentBalance = WalletBalance;
                            if (HttpContext.Current.Session["AdminMemberId"] != null)
                            {
                                res_transaction.CreatedBy = Convert.ToInt64(HttpContext.Current.Session["AdminMemberId"]);
                                res_transaction.CreatedByName = HttpContext.Current.Session["AdminUserName"].ToString();
                            }
                            else
                            {
                                res_transaction.CreatedBy = Common.CreatedBy;
                                res_transaction.CreatedByName = Common.CreatedByName;
                            }
                            res_transaction.TransactionUniqueId = TransactionId;
                            res_transaction.VendorTransactionId = objRes.id;
                            res_transaction.Reference = objRes.batchId;
                            res_transaction.BatchTransactionId = objRes.batchId;
                            res_transaction.TxnInstructionId = objRes.cipsTransactionDetailList[0].instructionId;
                            res_transaction.ParentTransactionId = objRes.cipsTransactionDetailList[0].instructionId;
                            res_transaction.Remarks = $"Refunded Successfully For Failed TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}";
                            res_transaction.Purpose = resDeposit.Remarks;
                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                            res_transaction.ResponseCode = objRes.cipsTransactionDetailList[0].creditStatus;
                            res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                            res_transaction.Description = Common.TruncateLongString(objRes.cipsTransactionDetailList[0].endToEndId, 29);
                            res_transaction.Status = (int)WalletTransactions.Statuses.Refund;
                            res_transaction.IsApprovedByAdmin = true;
                            res_transaction.IsActive = true;
                            res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                            res_transaction.RecieverName = objRes.cipsTransactionDetailList[0].creditorName;
                            res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                            res_transaction.RecieverAccountNo = objRes.cipsTransactionDetailList[0].creditorAccount;
                            res_transaction.RecieverBankCode = objRes.cipsTransactionDetailList[0].creditorAgent;
                            res_transaction.RecieverBranch = objRes.cipsTransactionDetailList[0].creditorBranch;
                            res_transaction.SenderAccountNo = objRes.debtorAccount;
                            res_transaction.SenderBankCode = objRes.debtorAgent;
                            res_transaction.SenderBranch = objRes.debtorBranch;
                            res_transaction.ServiceCharge = 0;
                            res_transaction.NetAmount = res_transaction.Amount;
                            res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                            res_transaction.SenderBankName = Common.ConnectIPs_BankName;
                            res_transaction.SenderBranchName = Common.ConnectIPs_BranchName;

                            AddUserBankDetail outobjectBankDtl = new AddUserBankDetail();
                            GetUserBankDetail inobjectBankDtl = new GetUserBankDetail();
                            inobjectBankDtl.BankCode = objRes.cipsTransactionDetailList[0].creditorAgent;
                            AddUserBankDetail resBankDtl = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobjectBankDtl, outobjectBankDtl);
                            if (resBankDtl != null && resBankDtl.Id != 0)
                            {
                                res_transaction.RecieverBankName = resBankDtl.BankName;
                                res_transaction.RecieverBranchName = resBankDtl.BranchName;
                            }
                            res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NCHL;

                            resDeposit.Particulars = objRes.cipsTransactionDetailList[0].reasonDesc;
                            resDeposit.JsonResponse = data;
                            resDeposit.ResponseCode = objRes.cipsTransactionDetailList[0].creditStatus;
                            resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
                            RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                            if (res_transaction.Add())
                            {
                                Common.AddLogs($"Refunded Successfully For Failed TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}", false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, PlatForm, DeviceCode);
                                string Title = "Transaction Refunded";
                                string Message = $"Amount Of Rs. {objRes.cipsTransactionDetailList[0].amount} Refunded Successfully For Failed TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}";
                                Common.SendNotification("Refunded Successfully", (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer, resuserdetails.MemberId, Title, Message);
                                //log.Info($"{System.DateTime.Now.ToString()} inside RepNCHL RefundBankTransfer complete " + Environment.NewLine);
                                // SendEmailConfirmation();  
                                return "Refunded Succesfully";
                            }
                            else
                            {
                                string result1s = $"Something Went Wrong Refund Not Sent For Failed TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}";
                                Common.AddLogs(result1s, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, PlatForm, DeviceCode);
                                return "Transaction Not Created";
                            }
                        }
                        else
                        {
                            string result1s = $"Refund Already Sent For Failed TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}";
                            Common.AddLogs(result1s, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, PlatForm, DeviceCode);
                            return "Transaction Already Refunded";
                        }
                    }
                    else
                    {
                        string result1s = $"Transaction Not Updated For Failed TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}";
                        Common.AddLogs(result1s, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, PlatForm, DeviceCode);
                        return "Transaction Not Updated";
                    }
                }
                else
                {
                    string result1s = $"Old Transaction Not Found For Failed TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}";
                    Common.AddLogs(result1s, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, PlatForm, DeviceCode);
                    return "Old Transaction Not Found";
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} RepNCHL RefundBankTransfer {ex.ToString()} " + Environment.NewLine);
                return ex.Message;
            }
        }

        public static string CipsStatusJSONResponseProcess(string BATCHID, string MemberId, string authenticationToken, string Version, string DeviceCode, string PlatForm, ref CipsBatchDetail objRes)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside RepNCHL CipsStatusJSONResponseProcess start" + Environment.NewLine);
            string msg = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(DeviceCode))
                {
                    msg = "Please enter DeviceCode.";
                }
                else if (string.IsNullOrEmpty(Version))
                {
                    msg = "Please enter Version.";
                }
                else if (string.IsNullOrEmpty(PlatForm))
                {
                    msg = "Please enter PlatForm.";
                }
                else if (string.IsNullOrEmpty(BATCHID))
                {
                    msg = "Please Enter BATCHID";
                }

                else if (string.IsNullOrEmpty(msg) && (string.IsNullOrEmpty(MemberId) || MemberId == "0"))
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
                    AddUserLoginWithPin outer = new AddUserLoginWithPin();
                    GetUserLoginWithPin inner = new GetUserLoginWithPin();
                    inner.MemberId = Convert.ToInt64(MemberId);
                    AddUserLoginWithPin resuserdetails = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inner, outer);
                    if (resuserdetails.Id == 0)
                    {
                        msg = "User Not Found";
                    }
                    else
                    {
                        AddDepositOrders outobject = new AddDepositOrders();
                        GetDepositOrders inobject = new GetDepositOrders();
                        inobject.TransactionId = BATCHID;
                        inobject.MemberId = Convert.ToInt64(MemberId);
                        AddDepositOrders resDeposit = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);

                        if (resDeposit != null && resDeposit.Id > 0)
                        {
                            Req_CipsBatch bank = new Req_CipsBatch();
                            bank.batchId = BATCHID;
                            Common.AddLogs($"{System.DateTime.Now.ToString()} RepNCHL CipsStatusJSONResponseProcess RepNCHL.gettoken start" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                            string token = RepNCHL.gettoken();
                            Common.AddLogs($"{System.DateTime.Now.ToString()} RepNCHL CipsStatusJSONResponseProcess RepNCHL.PostMethodWithToken start" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                            string data = RepNCHL.PostMethodWithToken("api/getcipstxnlistbybatchid", JsonConvert.SerializeObject(bank), token);
                            //log.Info($"{System.DateTime.Now.ToString()} RepNCHL CipsStatusJSONResponseProcess RepNCHL.PostMethodWithToken complete" + Environment.NewLine);
                            Common.AddLogs($"{System.DateTime.Now.ToString()} RepNCHL CipsStatusJSONResponseProcess RepNCHL.PostMethodWithToken complete Jsondata : {data}" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                            if (!string.IsNullOrEmpty(data) && data != "[]")
                            {
                                //Common.AddLogs(data, false, 1, 10000, "admin", true);

                                objRes = new CipsBatchDetail();
                                objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<CipsBatchDetail>(data);
                                Common.AddLogs($"{System.DateTime.Now.ToString()} inside RepNCHL CipsStatusJSONResponseProcess Json DeserializeObject Completed" + Environment.NewLine, false, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false);
                                if (objRes.cipsTransactionDetailList.Count > 0 && objRes.cipsTransactionDetailList[0].creditStatus == "000")
                                {
                                    // **********************************************************
                                    // ******** UPDATE TRANSACTION SUCCESS STATUS HERE *********
                                    // **********************************************************
                                    resDeposit.Status = (int)AddDepositOrders.DepositStatus.Success;
                                    resDeposit.Particulars = objRes.cipsTransactionDetailList[0].reasonDesc;
                                    resDeposit.JsonResponse = data;
                                    resDeposit.ResponseCode = objRes.cipsTransactionDetailList[0].creditStatus;
                                    resDeposit.RefferalsId = objRes.cipsTransactionDetailList[0].id;
                                    RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                    WalletTransactions res_transaction = new WalletTransactions();
                                    res_transaction.Sign = (int)WalletTransactions.Signs.Debit;
                                    if (Convert.ToDateTime(resDeposit.CreatedDate) >= Convert.ToDateTime("15-Jun-2022 13:30"))
                                    {
                                        res_transaction.Reference = BATCHID;
                                    }
                                    else
                                    {
                                        res_transaction.TransactionUniqueId = BATCHID;
                                    }
                                    if (res_transaction.GetRecord())
                                    {
                                        res_transaction.VendorTransactionId = objRes.cipsTransactionDetailList[0].id;
                                        res_transaction.BatchTransactionId = objRes.id;
                                        res_transaction.TxnInstructionId = objRes.cipsTransactionDetailList[0].id;
                                        res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                        res_transaction.GatewayStatus = objRes.cipsTransactionDetailList[0].creditStatus.ToString();
                                        res_transaction.ResponseCode = objRes.cipsTransactionDetailList[0].reasonDesc;
                                        res_transaction.Update();
                                    }
                                    string result1s = $"Cips Transaction Successfully Completed and Updated For TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}";
                                    Common.AddLogs(result1s, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, PlatForm, DeviceCode);
                                    return "success";
                                }
                                else if (objRes.cipsTransactionDetailList.Count > 0 && ((objRes.cipsTransactionDetailList[0].creditStatus.ToUpper() == "114") || (objRes.cipsTransactionDetailList[0].creditStatus.ToUpper() == "1001") || (objRes.cipsTransactionDetailList[0].creditStatus.ToUpper() == "1000") || (objRes.cipsTransactionDetailList[0].creditStatus.ToUpper() == "-01")))
                                {
                                    // **********************************************************
                                    // *********************  REFUND AMOUNT ********************
                                    // **********************************************************
                                    msg = RefundBankTransfer(objRes, resuserdetails, resDeposit, data, PlatForm, DeviceCode, BATCHID);
                                }
                                else
                                {
                                    // **********************************************************
                                    // ******** UPDATE TRANSACTION  STATUS HERE *********
                                    // **********************************************************
                                    resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                    resDeposit.JsonResponse = data;
                                    if (objRes.cipsTransactionDetailList.Count > 0)
                                    {
                                        resDeposit.Particulars = objRes.cipsTransactionDetailList[0].reasonDesc;
                                        resDeposit.ResponseCode = objRes.cipsTransactionDetailList[0].creditStatus;
                                    }
                                    RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                    string result1s = $"Cips Status Check executed for TransactionId: {objRes.cipsTransactionDetailList[0].instructionId}";
                                    Common.AddLogs(result1s, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, PlatForm, DeviceCode);
                                    return result1s;
                                }
                            }
                            else
                            {
                                WalletTransactions objTrans = new WalletTransactions();
                                if (Convert.ToDateTime(resDeposit.CreatedDate) >= Convert.ToDateTime("15-Jun-2022 13:30"))
                                {
                                    objTrans.Reference = BATCHID;
                                }
                                else
                                {
                                    objTrans.TransactionUniqueId = BATCHID;
                                }
                                objTrans.Sign = (int)WalletTransactions.Signs.Debit;
                                if (objTrans.GetRecord())
                                {
                                    WalletTransactions objTrans1 = new WalletTransactions();
                                    objTrans1.ParentTransactionId = objTrans.TransactionUniqueId;
                                    objTrans1.Sign = (int)WalletTransactions.Signs.Credit;
                                    if (!objTrans1.GetRecordCheckExists())
                                    {
                                        msg = "Json Data Not Found";
                                    }
                                    else
                                    {
                                        msg = "Refund Already Processed";
                                    }

                                }
                                else
                                {
                                    msg = "Transaction Not Found";
                                }
                            }
                        }
                        else
                        {
                            msg = "Data Not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} RepNCHL CipsStatusJSONResponseProcess {ex.ToString()} " + Environment.NewLine);
                msg = ex.Message;
            }
            log.Info($"{System.DateTime.Now.ToString()} RepNCHL CipsStatusJSONResponseProcess complete" + Environment.NewLine);
            return msg;
        }

        public static string PostMethod(string ApiName, string requestdata, string AuthToken)
        {
            string BaseURL = RepNCHL.TokenizationLinkBankAPIURl_LinkBank;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(BaseURL + ApiName);
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

        public static GetLoadWalletWithTokenNCHL LoadWalletWithToken(string APINAME, AddUserLoginWithPin resuser, string Reference, string TransactionId, string Token, string BankCode, string Amount, string TransactionRemarks, ref AddVendor_API_Requests objVendor_API_Requests)
        {

            AddLoadWalletWithTokenNCHL obj = new AddLoadWalletWithTokenNCHL();
            GetLoadWalletWithTokenNCHL result = new GetLoadWalletWithTokenNCHL();
            obj.participantId = RepNCHL.participantId_LinkBank;
            obj.mandateToken = Token;
            obj.userIdentifier = resuser.MemberId.ToString();
            obj.amount = Convert.ToDecimal(Amount);
            obj.appId = RepNCHL.APPID_NCHL_STAGING;
            obj.instructionId = TransactionId;
            obj.refId = Reference;
            obj.particulars = "linkbankstaging";
            obj.remarks = TransactionRemarks;
            obj.addnField1 = resuser.ContactNumber;
            obj.addnField2 = resuser.FirstName + " " + resuser.LastName;
            string TokenString = obj.participantId + "," + obj.mandateToken + "," + obj.userIdentifier + "," + obj.amount.ToString() + "," + obj.appId + "," + obj.instructionId + "," + obj.refId + "," + RepNCHL.withdrawusername_LinkBank;
            obj.token = Common.GenerateConnectIPSToken_LinkBank(TokenString);
            string Authtoken = gettoken_LinkBank();
            Common.AddLogs($"TokenString:{TokenString} SignedToken:{obj.token}");
            string data = PostMethod(APINAME, JsonConvert.SerializeObject(obj), Authtoken);
            if (!string.IsNullOrEmpty(data))
            {
                Common.AddLogs($"tokenization URL:{APINAME}: Request:{JsonConvert.SerializeObject(obj)} Response: {data}", false, (int)AddLog.LogType.DBLogs);
                result = JsonConvert.DeserializeObject<GetLoadWalletWithTokenNCHL>(data);
                if (result.responseCode == "000")
                {
                    result = LoadWalletPaymentCommit(result, Authtoken, ref objVendor_API_Requests);
                    result.instructionId = obj.instructionId;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = result.responseMessage;
                    if (result.responseErrors != null && result.responseErrors.Count > 0)
                    {
                        result.Details = result.responseErrors[0].ToString();
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

        public static GetLoadWalletWithTokenNCHL LoadWalletPaymentCommit(GetLoadWalletWithTokenNCHL objFinalPayment, string Authtoken, ref AddVendor_API_Requests objVendor_API_Requests)
        {
            AddLinkBankTransactionNCHL obj = new AddLinkBankTransactionNCHL();
            GetLoadWalletWithTokenNCHL result = new GetLoadWalletWithTokenNCHL();
            obj.participantId = RepNCHL.participantId_LinkBank;
            obj.paymentToken = objFinalPayment.paymentToken;
            obj.amount = Convert.ToDecimal(objFinalPayment.amount);
            obj.appId = RepNCHL.APPID_NCHL_STAGING;
            string TokenString = obj.participantId + "," + objFinalPayment.paymentToken + "," + objFinalPayment.amount.ToString() + "," + objFinalPayment.appId.ToString() + "," + RepNCHL.withdrawusername_LinkBank;
            obj.token = Common.GenerateConnectIPSToken_LinkBank(TokenString);

            string API_NAME = "tokenization/requestpayment";
            objVendor_API_Requests.Req_Token = obj.token;
            objVendor_API_Requests.Req_Khalti_ReferenceNo = objFinalPayment.instructionId;
            objVendor_API_Requests.Req_Khalti_URL = RepNCHL.TokenizationLinkBankAPIURl_LinkBank + API_NAME;
            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

            string data = PostMethod(API_NAME, JsonConvert.SerializeObject(obj), Authtoken);
            if (!string.IsNullOrEmpty(data))
            {
                objVendor_API_Requests.Req_Khalti_Input = JsonConvert.SerializeObject(obj);
                objVendor_API_Requests.Res_Khalti_Output = data;
                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                Common.AddLogs($"tokenization/requestpayment: Request:{JsonConvert.SerializeObject(obj)} Response: {data}", false, (int)AddLog.LogType.DBLogs);
                result = JsonConvert.DeserializeObject<GetLoadWalletWithTokenNCHL>(data);

                if (result.responseCode == "000")
                {

                    objVendor_API_Requests.Res_Khalti_Status = true;
                    objVendor_API_Requests.Res_Khalti_State = "Success";
                    objVendor_API_Requests.Res_Khalti_Message = result.responseMessage;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                    result.ReponseCode = 1;
                    result.instructionId = objFinalPayment.instructionId;
                    result.refId = objFinalPayment.refId;
                    result.Message = "Success";
                    result.Details = "Transaction Completed Successfully";
                    result.status = true;
                }
                else
                {
                    result.ReponseCode = 3;
                    result.Message = result.responseMessage;
                    if (result.responseErrors != null && result.responseErrors.Count > 0)
                    {
                        result.Details = result.responseErrors[0].ToString();
                    }
                    else
                    {
                        result.Details = result.creditDescription + " " + result.debitDescription;
                    }
                    objVendor_API_Requests.Res_Khalti_Status = false;
                    objVendor_API_Requests.Res_Khalti_State = "Failed";
                    objVendor_API_Requests.Res_Khalti_Message = result.responseMessage;
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

        public static GetTokenUnLinkNCHL UnlinkAccount(string branchName, string identifier, Int64 MemberId)
        {
            GetTokenUnLinkNCHL result = new GetTokenUnLinkNCHL();

            try
            {

                AddLinkBankRemoveNCHL objData = new AddLinkBankRemoveNCHL();
                objData.participantId = RepNCHL.participantId_LinkBank;
                objData.identifier = identifier;
                objData.userIdentifier = MemberId.ToString();
                objData.mandateToken = branchName;
                objData.cancelReasonCode = "INVALID_AC";
                objData.cancelReasonMessage = "Invalid Account Number";
                string AuthToken = RepNCHL.gettoken_LinkBank();
                string TokenString = objData.participantId + "," + objData.identifier + "," + objData.userIdentifier.ToString() + "," + objData.mandateToken.ToString() + "," + objData.cancelReasonCode.ToString() + "," + RepNCHL.withdrawusername_LinkBank;
                objData.token = Common.GenerateConnectIPSToken_LinkBank(TokenString);
                string APINAME = "tokenization/cancel";
                string data = PostMethod(APINAME, JsonConvert.SerializeObject(objData), AuthToken);
                Common.AddLogs($"ApiName: {RepNCHL.TokenizationLinkBankAPIURl_LinkBank + APINAME} Req:{JsonConvert.SerializeObject(objData)} Res:{data}", false, (int)AddLog.LogType.DBLogs);
                if (!string.IsNullOrEmpty(data))
                {
                    if (Common.IsValidJson(data))
                    {
                        result = JsonConvert.DeserializeObject<GetTokenUnLinkNCHL>(data);
                        if (result.responseCode == "000")
                        {
                            result.ReponseCode = 1;
                            result.Message = "Success";
                            result.Details = "Bank Unlinked Successfully";
                        }
                        else
                        {
                            result.ReponseCode = 3;
                            if (!string.IsNullOrEmpty(result.error))
                            {
                                result.Message = result.error;
                                result.Details = result.error;
                            }
                            else if (!string.IsNullOrEmpty(result.responseMessage))
                            {
                                result.Message = result.responseMessage;
                                result.Details = result.responseMessage;
                            }
                            else if (!string.IsNullOrEmpty(result.message))
                            {
                                result.Message = result.message;
                                result.Details = result.message;
                            }
                            else
                            {
                                result.Message = "Failed";
                                result.Details = "Failed";
                            }
                        }
                    }
                    else
                    {
                        result.ReponseCode = 3;
                        result.Message = data;
                        result.Details = data;
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
                Common.AddLogs($"GetTokenUnLinkNCHL UnlinkAccount Req:{ex.Message}", false, (int)AddLog.LogType.DBLogs);
                result.ReponseCode = 3;
                result.Message = "Failed";
                result.Details = ex.Message;
            }
            return result;
        }
    }
}