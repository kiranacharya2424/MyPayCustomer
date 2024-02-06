using log4net;
using MyPay.API.Models;
using MyPay.API.Models.MyPayPayments;
using MyPay.API.Models.Request.Merchant;
using MyPay.API.Models.Response.Merchant;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyPay.API.Controllers
{
    public class RequestAPI_MerchantController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_MerchantController));

        string ApiResponse = string.Empty;

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/use-mypay-payments")]
        public HttpResponseMessage CommitMerchantTransactions(Req_Merchant_Transaction_Requests user)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


            log.Info($"{System.DateTime.Now.ToString()} inside use-mypay-payments2222" + Environment.NewLine);
            log.Info("Mypay API_KEY: " + HttpContext.Current.Request.Headers["API_KEY"]);
            log.Info("Mypay TESTHEADER: " + HttpContext.Current.Request.Headers["TESTHEADER"]);
            //  log.Info("Mypay API_KEY at 0: " + HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString());
            //  log.Info("API_Key array length: " + HttpContext.Current.Request.Headers.GetValues("API_KEY"))

           // Request.Content.Header

            CommonResponse cres = new CommonResponse();
            Res_Merchant_Transaction_Requests result = new Res_Merchant_Transaction_Requests();
            var response = Request.CreateResponse<Res_Merchant_Transaction_Requests>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else
                {
                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string UserInput = getRawPostData().Result;
                    string UniqueTransactionId = string.Empty;
                    string RedirectURL = string.Empty;
                    string OrderToken = string.Empty;
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string PlatForm = "Web";
                    string DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;

                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", user.OrderId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);

                    string msg = RepMerchants.RequestMerchantOrderGenerate(API_KEY, user.ReturnUrl, user.OrderId, user.MerchantId, user.Amount, user.UserName, user.Password, PlatForm, DeviceCode, UserInput, ref UniqueTransactionId, ref RedirectURL, ref OrderToken, ref objVendor_API_Requests);
                    if (msg.ToLower() == "success")
                    {
                        result.ReponseCode = 1;
                        result.status = true;
                        result.MerchantTransactionId = UniqueTransactionId;
                        if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                        {
                            result.RedirectURL = Common.LiveSiteUrl + "MyPayPayments?OrderToken=" + HttpUtility.UrlEncode(OrderToken) + "&mid=" + HttpUtility.UrlEncode(Common.EncryptString(user.MerchantId));
                        }
                        else
                        {
                            result.RedirectURL = Common.TestSiteUrl + "MyPayPayments?OrderToken=" + HttpUtility.UrlEncode(OrderToken) + "&mid=" + HttpUtility.UrlEncode(Common.EncryptString(user.MerchantId));
                        }
                        result.Message = "Success";
                        result.Details = $"Order Generated Successfully With Txn ID: {UniqueTransactionId}";
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Merchant_Transaction_Requests>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                    inobjApiResponse.Id = objVendor_API_Requests.Id;
                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                    if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                    {
                        resUpdateRecord.Res_Output = ApiResponse;
                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-mypay-payments completed" + Environment.NewLine);
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} use-mypay-payments {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/use-mypay-payments-status")]
        public HttpResponseMessage CheckStatusMerchantTransactions(Req_Merchant_Transaction_Requests user)
        {
            //log.Info($"{System.DateTime.Now.ToString()} inside use-mypay-payments" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Merchant_Transaction_LookupRequests result = new Res_Merchant_Transaction_LookupRequests();
            var response = Request.CreateResponse<Res_Merchant_Transaction_LookupRequests>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else
                {
                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string UserInput = getRawPostData().Result;
                    AddMerchantOrders objAddMerchantOrders = new AddMerchantOrders();
                    string msg = RepMerchants.RequestMerchantOrderStatusCheck(API_KEY, user.MerchantTransactionId, user.GatewayTransactionId, ref objAddMerchantOrders);
                    if (msg.ToLower() == "success")
                    {
                        result.ReponseCode = 1;
                        result.status = true;
                        result.Message = "Status Fetched Successfully";
                        result.Details = $"Status Fetched Successfully for Order ID:{objAddMerchantOrders.OrderId}";
                        result.MerchantTransactionId = objAddMerchantOrders.TransactionId;
                        result.MemberContactNumber = objAddMerchantOrders.MemberContactNumber;
                        result.NetAmount = objAddMerchantOrders.NetAmount;
                        result.Status = objAddMerchantOrders.Status;
                        result.Remarks = objAddMerchantOrders.Remarks;
                        result.GatewayTransactionId = objAddMerchantOrders.TrackerId;
                        result.OrderId = objAddMerchantOrders.OrderId;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Merchant_Transaction_LookupRequests>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-mypay-payments completed" + Environment.NewLine);
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} use-mypay-payments {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/use-mypay-wallet-payments")]
        public HttpResponseMessage CommitMerchantWalletTransactions(Req_Merchant_Wallet_Transaction_Requests user)
        {
            //log.Info($"{System.DateTime.Now.ToString()} inside use-mypay-payments" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Merchant_Wallet_Transaction_Requests result = new Res_Merchant_Wallet_Transaction_Requests();
            var response = Request.CreateResponse<Res_Merchant_Wallet_Transaction_Requests>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (HttpContext.Current.Request.Headers.GetValues("Signature") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Signature Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AuthTokenString))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("AuthTokenString Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Reference))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Reference Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else
                {
                    string UserInput = getRawPostData().Result;
                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string Signature = HttpContext.Current.Request.Headers.GetValues("Signature")[0].ToString();
                    string UniqueTransactionId = string.Empty;
                    string SenderTransactionId = string.Empty;
                    string RedirectURL = string.Empty;
                    string OrderToken = string.Empty;
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string msg = string.Empty;
                    string PlatForm = "Web";
                    string DeviceCode = HttpContext.Current.Request.UserAgent.ToLower();

                    string Reference = user.Reference;
                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Merchant_Load;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);

                    msg = RepMerchants.RequestMerchantWalletTransaction(API_KEY, user.ContactNumber, user.MerchantId, user.Amount, user.UserName, user.Password, VendorApiType, PlatForm, DeviceCode, user.Remarks, Reference, user.AuthTokenString, UserInput, Signature, ref UniqueTransactionId, ref SenderTransactionId, ref RedirectURL, ref OrderToken, ref objVendor_API_Requests);
                    if (msg.ToLower() == "success")
                    {
                        result.ReponseCode = 1;
                        result.status = true;
                        result.MerchantWallet_TransactionId = UniqueTransactionId;
                        //result.Sender_TransactionId = SenderTransactionId;
                        result.Message = "Success";
                        result.Details = $"MyPay Merchant Wallet Transaction Completed Successfully With Txn ID: {UniqueTransactionId}";
                        result.responseMessage = result.Details;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Merchant_Wallet_Transaction_Requests>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                    inobjApiResponse.Id = objVendor_API_Requests.Id;
                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                    if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                    {
                        resUpdateRecord.Res_Output = ApiResponse;
                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-mypay-payments completed" + Environment.NewLine);
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} use-mypay-payments {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/mypay-checkstatus-merchant")]
        public HttpResponseMessage MerchantCheckStatus(Req_Merchant_CheckStatus user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside mypay-checkstatus-merchant" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Merchant_CheckStatus result = new Res_Merchant_CheckStatus();
            var response = Request.CreateResponse<Res_Merchant_CheckStatus>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (HttpContext.Current.Request.Headers.GetValues("Signature") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Signature Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AuthTokenString))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("AuthTokenString Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Reference))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Reference Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else
                {
                    string UserInput = getRawPostData().Result;
                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string Signature = HttpContext.Current.Request.Headers.GetValues("Signature")[0].ToString();
                    string UniqueTransactionId = string.Empty;
                    string RedirectURL = string.Empty;
                    string OrderToken = string.Empty;
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string msg = string.Empty;
                    string PlatForm = "Web";
                    string DeviceCode = HttpContext.Current.Request.UserAgent.ToLower();

                    string Reference = user.Reference;
                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Merchant_CheckOut;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);
                    string TransactionStatus = string.Empty;
                    string TransactionUniqueID = string.Empty;
                    Int32 StatusCode = 0;
                    msg = RepMerchants.RequestMerchantWalletTransactionCheckStatus(API_KEY, user.Reference, user.TransactionReference, VendorApiType, user.TransactionId, user.MerchantId, user.UserName, user.Password, PlatForm, DeviceCode, user.AuthTokenString, UserInput, Signature, ref objVendor_API_Requests, ref TransactionStatus, ref StatusCode, ref TransactionUniqueID);
                    if (msg.ToLower() == "success")
                    {
                        result.ReponseCode = 1;
                        result.status = true;
                        result.TransactionUniqueID = TransactionUniqueID;
                        result.TransactionStatus = TransactionStatus;
                        result.StatusCode = StatusCode;
                        result.Message = "Success";
                        result.Details = $"TransactionID: {TransactionUniqueID} Found";
                        result.responseMessage = result.Details;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Merchant_CheckStatus>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                    inobjApiResponse.Id = objVendor_API_Requests.Id;
                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                    if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                    {
                        resUpdateRecord.Res_Output = ApiResponse;
                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} mypay-CheckStatus-merchant completed" + Environment.NewLine);
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} mypay-CheckStatus-merchant {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/mypay-accountvalidation-merchant")]
        public HttpResponseMessage MerchantCheckAccountValidation(Req_Merchant_CheckAccountValidation user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside mypay-CheckAccountValidation-merchant" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Merchant_CheckAccountValidation result = new Res_Merchant_CheckAccountValidation();
            var response = Request.CreateResponse<Res_Merchant_CheckAccountValidation>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (HttpContext.Current.Request.Headers.GetValues("Signature") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Signature Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AuthTokenString))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("AuthTokenString Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Reference))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Reference Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else
                {
                    string UserInput = getRawPostData().Result;
                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string Signature = HttpContext.Current.Request.Headers.GetValues("Signature")[0].ToString();
                    string UniqueTransactionId = string.Empty;
                    string RedirectURL = string.Empty;
                    string OrderToken = string.Empty;
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string msg = string.Empty;
                    string PlatForm = "Web";
                    string DeviceCode = HttpContext.Current.Request.UserAgent.ToLower();

                    string Reference = user.Reference;
                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Merchant_Account_Validation;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);
                    string AccountStatus = string.Empty;
                    string FullName = string.Empty;
                    msg = RepMerchants.RequestMerchantAccountValidation(API_KEY, user.Reference, VendorApiType, user.ContactNumber, user.MerchantId, user.UserName, user.Password, PlatForm, DeviceCode, user.AuthTokenString, UserInput, Signature, ref objVendor_API_Requests, ref AccountStatus, ref FullName);
                    if (msg.ToLower() == "success")
                    {
                        result.ReponseCode = 1;
                        result.status = true;
                        result.IsAccountValidated = true;
                        result.ContactNumber = user.ContactNumber;
                        result.FullName = FullName;
                        result.AccountStatus = AccountStatus;
                        result.Message = "Success";
                        result.Details = $"Account Information Validated Successfully";
                        result.responseMessage = result.Details;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Merchant_CheckAccountValidation>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    else if (AccountStatus.ToLower() == "inactive")
                    {
                        result.ReponseCode = 1;
                        result.status = false;
                        result.IsAccountValidated = false;
                        result.ContactNumber = user.ContactNumber;
                        result.AccountStatus = AccountStatus;
                        result.Message = msg;
                        result.Details = $"Inactive Account";
                        result.responseMessage = result.Details;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Merchant_CheckAccountValidation>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                    inobjApiResponse.Id = objVendor_API_Requests.Id;
                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                    if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                    {
                        resUpdateRecord.Res_Output = ApiResponse;
                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} mypay-CheckAccountValidation-merchant completed" + Environment.NewLine);
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} mypay-CheckAccountValidation-merchant {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/use-mypay-direct-payments")]
        public HttpResponseMessage CommitMerchantDirectTransactions(Req_Merchant_Wallet_Transaction_Requests user)
        {
            //log.Info($"{System.DateTime.Now.ToString()} inside use-mypay-payments" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Merchant_Wallet_Transaction_Requests result = new Res_Merchant_Wallet_Transaction_Requests();
            var response = Request.CreateResponse<Res_Merchant_Wallet_Transaction_Requests>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (HttpContext.Current.Request.Headers.GetValues("Signature") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Signature Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AuthTokenString))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("AuthTokenString Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Reference))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Reference Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else
                {
                    string UserInput = getRawPostData().Result;
                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string Signature = HttpContext.Current.Request.Headers.GetValues("Signature")[0].ToString();
                    string UniqueTransactionId = string.Empty;
                    string SenderTransactionId = string.Empty;
                    string RedirectURL = string.Empty;
                    string OrderToken = string.Empty;
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string msg = string.Empty;
                    string PlatForm = "Web";
                    string DeviceCode = HttpContext.Current.Request.UserAgent.ToLower();

                    string Reference = user.Reference;
                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);

                    msg = RepMerchants.RequestMerchantWalletTransaction(API_KEY, user.ContactNumber, user.MerchantId, user.Amount, user.UserName, user.Password, VendorApiType, PlatForm, DeviceCode, user.Remarks, Reference, user.AuthTokenString, UserInput, Signature, ref UniqueTransactionId, ref SenderTransactionId, ref RedirectURL, ref OrderToken, ref objVendor_API_Requests);
                    if (msg.ToLower() == "success")
                    {
                        result.ReponseCode = 1;
                        result.status = true;
                        result.MerchantWallet_TransactionId = UniqueTransactionId;
                        //result.Sender_TransactionId = SenderTransactionId;
                        result.Message = "Success";
                        result.Details = $"MyPay Merchant Wallet Transaction Completed Successfully With Txn ID: {UniqueTransactionId}";
                        result.responseMessage = result.Details;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Merchant_Wallet_Transaction_Requests>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                    inobjApiResponse.Id = objVendor_API_Requests.Id;
                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                    if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                    {
                        resUpdateRecord.Res_Output = ApiResponse;
                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-mypay-payments completed" + Environment.NewLine);
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} use-mypay-payments {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        #region Remittance API

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/use-mypay-direct-remittance-payments")]
        public HttpResponseMessage CommitMerchantDirectRemittanceTransactions(Req_Merchant_Wallet_Transaction_Requests user)
        {
            //log.Info($"{System.DateTime.Now.ToString()} inside use-mypay-payments" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Merchant_Wallet_Transaction_Requests result = new Res_Merchant_Wallet_Transaction_Requests();
            var response = Request.CreateResponse<Res_Merchant_Wallet_Transaction_Requests>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (HttpContext.Current.Request.Headers.GetValues("Signature") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Signature Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AuthTokenString))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("AuthTokenString Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Reference))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Reference Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else
                {
                    string UserInput = getRawPostData().Result;
                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string Signature = HttpContext.Current.Request.Headers.GetValues("Signature")[0].ToString();
                    string UniqueTransactionId = string.Empty;
                    string SenderTransactionId = string.Empty;
                    string RedirectURL = string.Empty;
                    string OrderToken = string.Empty;
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string msg = string.Empty;
                    string PlatForm = "Web";
                    string DeviceCode = HttpContext.Current.Request.UserAgent.ToLower();

                    string Reference = user.Reference;
                    //int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load;
                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Remittance_Transactions;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToRemittance_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);

                    msg = RepMerchants.RequestMerchantWalletTransaction(API_KEY, user.ContactNumber, user.MerchantId, user.Amount, user.UserName, user.Password, VendorApiType, PlatForm, DeviceCode, user.Remarks, Reference, user.AuthTokenString, UserInput, Signature, ref UniqueTransactionId, ref SenderTransactionId, ref RedirectURL, ref OrderToken, ref objVendor_API_Requests);
                    if (msg.ToLower() == "success")
                    {
                        result.ReponseCode = 1;
                        result.status = true;
                        result.MerchantWallet_TransactionId = UniqueTransactionId;
                        //result.Sender_TransactionId = SenderTransactionId;
                        result.Message = "Success";
                        result.Details = $"MyPay Merchant Wallet Transaction Completed Successfully With Txn ID: {UniqueTransactionId}";
                        result.responseMessage = result.Details;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Merchant_Wallet_Transaction_Requests>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                    inobjApiResponse.Id = objVendor_API_Requests.Id;
                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_RemittanceAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                    if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                    {
                        resUpdateRecord.Res_Output = ApiResponse;
                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "remittance_api_requests");
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-mypay-payments completed" + Environment.NewLine);
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} use-mypay-payments {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        #endregion


        private async Task<String> getRawPostData()
        {
            using (var contentStream = await this.Request.Content.ReadAsStreamAsync())
            {
                contentStream.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(contentStream))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}