using log4net;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyPay.API.Models
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {

        private static ILog log = LogManager.GetLogger(typeof(BasicAuthenticationAttribute));
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            log.Info("hi there");
            var checkkey = actionContext.Request.Headers.SingleOrDefault(x => x.Key == "utoken").Value?.First();
            CommonResponse cres = new CommonResponse();

            if ((!actionContext.Request.RequestUri.ToString().ToLower().Contains("userauthorization")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("updateauthorizationip")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("getapiuser")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("airlines-flight-status")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("use-mypay-payments")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("use-mypay-wallet-payments")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("use-mypay-direct-remittance-payments")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("bank-account-validation")) &&                
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("bank-transfer-remittance")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("use-mypay-direct-payments")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("mypay-accountvalidation-merchant")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("mypay-checkstatus-merchant")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("lookup-appmessage")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("createnchlhash")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("downloadserviceticket")) &&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("linkbankaccountnchltoken"))&&
                (!actionContext.Request.RequestUri.ToString().ToLower().Contains("npi-to-issuer-refund")))

            //downloadServiceTicket
            {
                
                if (actionContext.Request.Headers.Authorization == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                else
                {

                    var seckey = actionContext.Request.Headers.SingleOrDefault(x => x.Key == "secret-key").Value?.First();
                    log.Info("secret-key: " + seckey);
                    if (seckey != null)
                    {

                        string secretkey = actionContext.Request.Headers.GetValues("secret-key").First();
                        if (Common.SecretKey == Common.DecryptString(secretkey))
                        {


                            string authenticationToken = actionContext.Request.Headers
                                                        .Authorization.Parameter;

                            if (!string.IsNullOrEmpty(authenticationToken))
                            {

                                log.Debug($"token generated on {System.DateTime.Now.ToString()}: " + authenticationToken);


                                var handler = new JwtSecurityTokenHandler();
                                var jwtSecurityToken = handler.ReadJwtToken(authenticationToken);

                                string username = jwtSecurityToken.Payload["username"].ToString();
                                string password = jwtSecurityToken.Payload["password"].ToString();
                                string exp = jwtSecurityToken.Payload["exp"].ToString();
                                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));

                                ApplyCurrentUserIdentity_With_Token(username);
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Token Not Found");
                                cres.ReponseCode = 4;
                                actionContext.Response = actionContext.Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Invalid Key");
                            cres.ReponseCode = 3;
                            actionContext.Response = actionContext.Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);

                        }
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage("Invalid Key");
                        cres.ReponseCode = 3;
                        actionContext.Response = actionContext.Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);


                    }
                }
            }
            else
            {
                string username = "authorization";
                //Thread.CurrentPrincipal = new GenericPrincipal(
                //             new GenericIdentity(username), null);
                //if (HttpContext.Current != null)
                //{
                //    HttpContext.Current.User = new GenericPrincipal(
                //    new GenericIdentity(username), null);
                //}
                ApplyCurrentUserIdentity_With_Token(username);
            }
        }

        private static void ApplyCurrentUserIdentity_With_Token(string username)
        {
            var json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/apisettings.json"));
            AddApiSettings objApiSettings = new AddApiSettings();
            objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<AddApiSettings>(json);
            if (objApiSettings != null)
            {
                if (objApiSettings.IsTestVendor || Common.ApplicationEnvironment.IsProduction == false)
                {
                    SetAPIServerDownToLocalUAT();
                }
                else
                {
                    ResetAPIServerToLiveEnvironment();
                }
            }
            else if (HttpContext.Current.Request.Url.Host.ToLower().IndexOf("localhost") >= 0)
            {
                // **************************//
                // ***** SET LOCAL KEYS ******//
                // **************************//
                SetAPIServerDownToLocalUAT();
            }
            //else if ((HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("insurance")))
            //{
            //    // **************************//
            //    // ***** SET LOCAL KEYS ******//
            //    // **************************//
            //    SetAPIServerDownToLocalUAT();
            //}
            //else if ((HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("echalan")))
            //{
            //    // **************************//
            //    // ***** SET LOCAL KEYS ******//
            //    // **************************//
            //    SetAPIServerDownToLocalUAT();
            //}
            //else if ((HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("flight")) || (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("airline")))
            //{
            //    SetAPIServerDownToLocalUAT();
            //}
            else
            {
                // ****************************************//
                // ***** RESET TO LIVE SERVER KEYS ******//
                // ****************************************//
                ResetAPIServerToLiveEnvironment();

            }
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(username), null);
            }
        }

        private static void ResetAPIServerToLiveEnvironment()
        {
            string KhaltiApiUrl_Root = "https://services.khalti.com/api/";
            string KhaltiApiUrl_Prefix = "https://services.khalti.com/api/use/";
            string Vendor_Lookup_URL_Prefix = "https://services.khalti.com/api/service/";
            string ServiceGroup_COUNTERS_URL_Prefix = "https://services.khalti.com/api/servicegroup/";
            string TransactionLookup_URL_Prefix = "https://services.khalti.com/api/";
            string KhaltiApiSchoolUrl_Root = "https://khalti.com/api/v2/";
            string Req_TokenTest = "yKutgSRojJTpcnxKBPOx";
            string Req_TokenLive = "yKutgSRojJTpcnxKBPOx";
            string SchoolAuthenticationKey = "Key live_secret_key_9fab30bbee074013a3f0fb804eb4e578";//"TEST:4vVfxLDCA3d4GQmZYFea";
            string SendSMSToken = "28826134c0c12588ebf248d27926ec849476c3f98ceab1536f12326c84421772";


            string EVENTS_API_URL_LINK = "https://eventapi.mypay.com.np";
            //string EVENTS_API_KEY = "Zq4t7w!z%C*F-JaNdRfUjXn2r5u8x/A?D(G+KbPeShVkYp3s6v9y$B&E)H@McQfT";
            string EVENTS_API_KEY = "eShVmYq3t6w9z$C&F)J@McQfTjWnZr4u7x!A%D*G-KaPdRgUkXp2s5v8y/B?E(H+";
            string EVENTS_USER_NAME = "mypaywallet";
            string EVENTS_API_CLIENT_CODE = "CL3683171";

            VendorApi_CommonHelper.KhaltiApiUrl_Root = KhaltiApiUrl_Root;
            VendorApi_CommonHelper.KhaltiApiUrl_Prefix = KhaltiApiUrl_Prefix;
            VendorApi_CommonHelper.Vendor_Lookup_URL_Prefix = Vendor_Lookup_URL_Prefix;
            VendorApi_CommonHelper.ServiceGroup_COUNTERS_URL_Prefix = ServiceGroup_COUNTERS_URL_Prefix;
            VendorApi_CommonHelper.TransactionLookup_URL_Prefix = TransactionLookup_URL_Prefix;
            VendorApi_CommonHelper.KhaltiApiSchoolUrl_Root = KhaltiApiSchoolUrl_Root;

            VendorApi_CommonHelper.Req_TokenTest = Req_TokenTest;
            VendorApi_CommonHelper.Req_TokenLive = Req_TokenLive;
            VendorApi_CommonHelper.SchoolAuthenticationKey = SchoolAuthenticationKey;
            Common.SendSMSToken = SendSMSToken;

            string FonePay_API_URL_Base_Root = "https://fonepayqrapi.mypay.com.np/proxy/";
            string FonePay_API_URL_Base_Prefix = "https://fonepayqrapi.mypay.com.np/proxy/";
            string FonePay_IssuerBin = "122221";
            string FonePay_Bank_UserName = "MyPay";
            string FonePay_Bank_Password = "G29412116@";
            string FonePay_Bank_Password_Encrypted = "TXlQYXk6RzI5NDEyMTE2QA==";
            string FonePay_UserName = "MyPay001";
            string FonePay_Password = "D5hn$*ixMM+9AFi0W11&WhkOM";
            string FonePay_TestUserAccountnoBank = "2844150059837002";
            string FonePay_MerchantPAN = "602483993";
            string FonePay_ProxyUserName = "mypay";
            string FonePay_ProxyApiKey = "@NcRfUjXn2r5u8x/A?D(G-KaPdSgVkYp3s6v9y$B&E)H@MbQeThWmZq4t7w!z%C*";


            VendorApi_CommonHelper.FonePay_API_URL_Base_Root = FonePay_API_URL_Base_Root;
            VendorApi_CommonHelper.FonePay_API_URL_Base_Prefix = FonePay_API_URL_Base_Prefix;
            VendorApi_CommonHelper.FonePay_IssuerBin = FonePay_IssuerBin;
            VendorApi_CommonHelper.FonePay_Bank_UserName = FonePay_Bank_UserName;
            VendorApi_CommonHelper.FonePay_Bank_Password = FonePay_Bank_Password;
            VendorApi_CommonHelper.FonePay_UserName = FonePay_UserName;
            VendorApi_CommonHelper.FonePay_Password = FonePay_Password;
            VendorApi_CommonHelper.FonePay_UserAccountnoBank = FonePay_TestUserAccountnoBank;
            VendorApi_CommonHelper.FonePay_MerchantPAN = FonePay_MerchantPAN;
            VendorApi_CommonHelper.FonePay_ProxyUserName = FonePay_ProxyUserName;
            VendorApi_CommonHelper.FonePay_ProxyApiKey = FonePay_ProxyApiKey;
            VendorApi_CommonHelper.FonePay_Bank_Password_Encrypted = FonePay_Bank_Password_Encrypted;


            VendorApi_CommonHelper.EVENTS_API_URL_LINK = EVENTS_API_URL_LINK;
            VendorApi_CommonHelper.EVENTS_API_KEY = EVENTS_API_KEY;
            VendorApi_CommonHelper.EVENTS_USER_NAME = EVENTS_USER_NAME;
            VendorApi_CommonHelper.EVENTS_API_CLIENT_CODE = EVENTS_API_CLIENT_CODE;

            string RepPrabhu_url = "https://merchant.prabhupay.com/Api/Utility.svc?wsdl";
            RepPrabhu.url = RepPrabhu_url;
            RepPrabhu.username = "MYPAY789";
            RepPrabhu.password = "A?p)M[&2GjM";

            RepNCHL.NCHLApiUrl_LinkBank = RepNCHL.NCHLApiUrl;
            RepNCHL.NCHLWithdrawAPIURl_LinkBank = RepNCHL.NCHLWithdrawAPIURl;
            RepNCHL.APPID_LinkBank = RepNCHL.APPID;
            RepNCHL.APPID_NCHL_STAGING = RepNCHL.APPID_STAGE_PAYMENT_LIVE;
            RepNCHL.UserId_LinkBank = RepNCHL.UserId;
            RepNCHL.APPNAME_LinkBank = RepNCHL.APPNAME;
            RepNCHL.WithdrawBasicApiKey_LinkBank = RepNCHL.WithdrawBasicApiKey;
            RepNCHL.withdrawusername_LinkBank = RepNCHL.withdrawusername;
            RepNCHL.withdrawpassword_LinkBank = RepNCHL.withdrawpassword;
            RepNCHL.participantId_LinkBank = "MYPAY";
            RepNCHL.PFXFile_LinkBank = RepNCHL.PFXFile;
            RepNCHL.PFXPassword_LinkBank = RepNCHL.PFXPassword;
            RepNCHL.TokenizationURL_LinkBank = RepNCHL.TokenizationURL;
            RepNCHL.TokenizationLinkBankAPIURl_LinkBank = RepNCHL.TokenizationLinkBankAPIURl;
        }

        private static void SetAPIServerDownToLocalUAT()
        {
            VendorApi_CommonHelper.KhaltiApiUrl_Root = VendorApi_CommonHelper.KhaltiApiUrl_Root_localhost;
            VendorApi_CommonHelper.KhaltiApiUrl_Prefix = VendorApi_CommonHelper.KhaltiApiUrl_Prefix_localhost;
            VendorApi_CommonHelper.Vendor_Lookup_URL_Prefix = VendorApi_CommonHelper.Vendor_Lookup_URL_Prefix_localhost;
            VendorApi_CommonHelper.ServiceGroup_COUNTERS_URL_Prefix = VendorApi_CommonHelper.ServiceGroup_COUNTERS_URL_Prefix_localhost;
            VendorApi_CommonHelper.TransactionLookup_URL_Prefix = VendorApi_CommonHelper.TransactionLookup_URL_Prefix_localhost;
            VendorApi_CommonHelper.KhaltiApiSchoolUrl_Root = VendorApi_CommonHelper.KhaltiApiSchoolUrl_Root_localhost;

            VendorApi_CommonHelper.Req_TokenTest = VendorApi_CommonHelper.Req_TokenTest_localhost;
            VendorApi_CommonHelper.Req_TokenLive = VendorApi_CommonHelper.Req_TokenLive_localhost;
            VendorApi_CommonHelper.SchoolAuthenticationKey = VendorApi_CommonHelper.SchoolAuthenticationKey_localhost;
            Common.LiveSiteUrl = Common.TestSiteUrl;
            Common.SendSMSToken = Common.LocalSendSMSToken;


            VendorApi_CommonHelper.FonePay_API_URL_Base_Root = VendorApi_CommonHelper.FonePay_API_URL_Base_Root_localhost;
            VendorApi_CommonHelper.FonePay_API_URL_Base_Prefix = VendorApi_CommonHelper.FonePay_API_URL_Base_Prefix_localhost;
            VendorApi_CommonHelper.FonePay_IssuerBin = VendorApi_CommonHelper.FonePay_IssuerBin_localhost;
            VendorApi_CommonHelper.FonePay_Bank_UserName = VendorApi_CommonHelper.FonePay_Bank_UserName_localhost;
            VendorApi_CommonHelper.FonePay_Bank_Password = VendorApi_CommonHelper.FonePay_Bank_Password_localhost;
            VendorApi_CommonHelper.FonePay_UserName = VendorApi_CommonHelper.FonePay_UserName_localhost;
            VendorApi_CommonHelper.FonePay_Password = VendorApi_CommonHelper.FonePay_Password_localhost;
            VendorApi_CommonHelper.FonePay_UserAccountnoBank = VendorApi_CommonHelper.FonePay_TestUserAccountnoBank_localhost;
            VendorApi_CommonHelper.FonePay_MerchantPAN = VendorApi_CommonHelper.FonePay_MerchantPAN_localhost;
            VendorApi_CommonHelper.FonePay_ProxyUserName = VendorApi_CommonHelper.FonePay_ProxyUserName_localhost;
            VendorApi_CommonHelper.FonePay_ProxyApiKey = VendorApi_CommonHelper.FonePay_ProxyApiKey_localhost;
            VendorApi_CommonHelper.FonePay_Bank_Password_Encrypted = VendorApi_CommonHelper.FonePay_Bank_Password_Encrypted_localhost;
            RepPrabhu.url = RepPrabhu.url_localhost;
            RepPrabhu.username = RepPrabhu.username_localhost;
            RepPrabhu.password = RepPrabhu.password_localhost;


            VendorApi_CommonHelper.EVENTS_API_URL_LINK = VendorApi_CommonHelper.EVENTS_API_URL_LINK_LOCAL;
            VendorApi_CommonHelper.EVENTS_API_KEY = VendorApi_CommonHelper.EVENTS_API_KEY_LOCAL;
            VendorApi_CommonHelper.EVENTS_USER_NAME = VendorApi_CommonHelper.EVENTS_USER_NAME_LOCAL;
            VendorApi_CommonHelper.EVENTS_API_CLIENT_CODE = VendorApi_CommonHelper.EVENTS_API_CLIENT_CODE_LOCAL;


        }
    }
}