using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request.Ride;
using MyPay.API.Models.Response.Ride;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.Get.Ride;
using MyPay.Models.VendorAPI.Get.WorldLink;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class RequestAPI_WorldLinksController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_WorldLinksController));
        string ApiResponse = string.Empty;
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/worldlink-lookup")]
        public HttpResponseMessage GetWorldlink_Lookup(Req_Vendor_API_WorldLink_Lookup user)
        {
            log.Info($" {System.DateTime.Now.ToString()} inside worldlink-lookup  {Environment.NewLine}");
            CommonResponse cres = new CommonResponse();
            Res_WorldLink_Lookup result = new Res_WorldLink_Lookup();
            var response = Request.CreateResponse<Res_WorldLink_Lookup>(System.Net.HttpStatusCode.BadRequest, result);
            var userInput = getRawPostData().Result;
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    //string md5hash = Common.CheckHash(user);
                    string md5hash = Common.getHashMD5(userInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resUser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        GetVendor_API_WorldLink_Lookup objRes = new GetVendor_API_WorldLink_Lookup();
                        string msg = RepKhalti.RequestServiceGroup_WorldLink_Lookup(user.Reference, user.Username, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.first_online_payment = objRes.first_online_payment;
                            result.renew_option = objRes.renew_option;
                            result.amount = objRes.amount;
                            result.days_remaining = objRes.days_remaining;
                            result.due_amount_till_now = objRes.due_amount_till_now;
                            result.session_id = objRes.session_id;
                            result.amount_detail = objRes.amount_detail;
                            result.available_renew_options = objRes.available_renew_options;
                            result.package_options = objRes.package_options;
                            result.branch = objRes.branch;
                            result.full_name = objRes.full_name;
                            result.log_idx = objRes.log_idx;
                            result.branch = objRes.branch;
                            result.Apimessage = objRes.message;
                            result.subscribed_package_name = objRes.subscribed_package_name;
                            result.subscribed_package_type = objRes.subscribed_package_type;
                            result.username = objRes.username;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_WorldLink_Lookup>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id.ToString();
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Worldlink;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Internet_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, user.Username, "Worldlink Communication", result.subscribed_package_name, result.subscribed_package_type, 
                                result.Message, result.amount.ToString());
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} worldlink-lookup completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} worldlink-lookup {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} worldlink-lookup {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-worldlink-commit")]
        public HttpResponseMessage GetServiceWorldlink_COMMIT(Req_Vendor_API_Commit_WorldLink_Requests user)
        {
            log.Info($" {System.DateTime.Now.ToString()} inside use-worldlink-commit  {Environment.NewLine}");
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Commit_WorldLink_Requests result = new Res_Vendor_API_Commit_WorldLink_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_WorldLink_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    // string md5hash = Common.CheckHash(user);

                    string md5hash = Common.getHashMD5(userInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId.Trim() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Worldlink;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_ServiceGroup_WorldLink_Commit objRes = new GetVendor_API_ServiceGroup_WorldLink_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_WorldLink(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.SessionID,
                            user.PackageID, user.Amount, user.Reference, userInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.TransactionUniqueId = TransactionID;
                            result.Details = objRes.message;
                            result.Message = "Success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_WorldLink_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            string SessionId = user.SessionID;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Worldlink;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resGetRecord.ContactNumber, objVendor_API_Requests.MemberName, "Internet Payment", "Worldlink Communication", user.Amount.ToString());

                            }
                            catch (Exception ex)
                            {

                            }
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} use-worldlink-commit completed {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} use-worldlink-commit {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} use-worldlink-commit {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


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