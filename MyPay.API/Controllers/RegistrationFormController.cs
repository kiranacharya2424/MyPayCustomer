using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static MyPay.Models.OrganizationModel;
using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;

namespace MyPay.API.Controllers
{
    public class RegistrationFormController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RegistrationFormController));
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

        [HttpPost]
        [System.Web.Http.Route("api/GetOrganizationList")]
        public HttpResponseMessage GetOrganizationList(CommonProp user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside RegistrationFormController " + Environment.NewLine);
            CommonResponseDataOrganization result = new CommonResponseDataOrganization();
            var response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string UserInput = getRawPostData().Result;
                    string CommonResult = "";
                    AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                    int VendorAPIType = 0;
                    int Type = 0;
                    Int64 memId = Convert.ToInt64(user.MemberId);
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                    if (CommonResult.ToLower() != "success")
                    {
                        CommonResponseDataOrganization cres1 = new CommonResponseDataOrganization();
                        cres1 = CommonApiMethod.ReturnBadRequestMessages(CommonResult);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres1);
                        return response;
                    }

                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string Ipaddress = Common.GetUserIP();
                    GetDataFromOrganization obj = RepKhalti.RequestOrganization("", user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.organization_events));
                    
                    List<Res_Organization_API_Requests> itemobj = JsonConvert.DeserializeObject<List<Res_Organization_API_Requests>>(obj.message);
                    if (string.IsNullOrEmpty(obj.message))
                    {
                        CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(obj.message);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        cres.status = response.IsSuccessStatusCode;
                        cres.Message = response.ReasonPhrase;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = "Success";
                        result.ResponseCode = 1;
                        result.status = true;
                        result.Data = itemobj;
                        result.StatusCode = Convert.ToString(HttpStatusCode.Accepted);
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} RegistrationFormController completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()}   RegistrationFormController {ex.ToString()}" + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            return response;
        }
        [HttpPost]
        [System.Web.Http.Route("api/GetEventList")]
        public HttpResponseMessage GetEventList(CommonProp user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside RegistrationFormController " + Environment.NewLine);
            CommonResponseDataOrganization result = new CommonResponseDataOrganization();
            var response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string UserInput = getRawPostData().Result;
                    string CommonResult = "";
                    AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                    int VendorAPIType = 0;
                    int Type = 0;
                    Int64 memId = Convert.ToInt64(user.MemberId);
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                    if (CommonResult.ToLower() != "success")
                    {
                        CommonResponseDataOrganization cres1 = new CommonResponseDataOrganization();
                        cres1 = CommonApiMethod.ReturnBadRequestMessages(CommonResult);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres1);
                        return response;
                    }

                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string Ipaddress = Common.GetUserIP();
                    GetDataFromOrganization obj = RepKhalti.RequestEventList("", user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.organization_events));

                    List<Res_Organization_API_Requests> itemobj = JsonConvert.DeserializeObject<List<Res_Organization_API_Requests>>(obj.message);
                    if (string.IsNullOrEmpty(obj.message))
                    {
                        CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(obj.message);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        cres.status = response.IsSuccessStatusCode;
                        cres.Message = response.ReasonPhrase;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = "Success";
                        result.ResponseCode = 1;
                        result.status = true;
                        result.Data = itemobj;
                        result.StatusCode = Convert.ToString(HttpStatusCode.Accepted);
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} RegistrationFormController completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()}   RegistrationFormController {ex.ToString()}" + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            return response;
        }
        [HttpPost]
        [System.Web.Http.Route("api/GetRegistrationForm")]
        public HttpResponseMessage GetRegistrationForm(CommonProp user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside RegistrationFormController " + Environment.NewLine);
            CommonResponseDataOrganization result = new CommonResponseDataOrganization();
            var response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string UserInput = getRawPostData().Result;
                    string CommonResult = "";
                    AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                    int VendorAPIType = 0;
                    int Type = 0;
                    Int64 memId = Convert.ToInt64(user.MemberId);
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                    if (CommonResult.ToLower() != "success")
                    {
                        CommonResponseDataOrganization cres1 = new CommonResponseDataOrganization();
                        cres1 = CommonApiMethod.ReturnBadRequestMessages(CommonResult);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres1);
                        return response;
                    }

                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string Ipaddress = Common.GetUserIP();
                    GetDataFromOrganization obj = RepKhalti.RequestRegistrationData("", user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.organization_events));

                    List<Res_Registration_API_Requests> itemobj = JsonConvert.DeserializeObject<List<Res_Registration_API_Requests>>(obj.message);
                    if (string.IsNullOrEmpty(obj.message))
                    {
                        CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(obj.message);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = "Success";
                        result.ResponseCode = 1;
                        result.status = true;
                        result.Data = itemobj;
                        result.StatusCode = Convert.ToString(HttpStatusCode.Accepted);
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} RegistrationFormController completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()}   RegistrationFormController {ex.ToString()}" + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            return response;
        }
        [HttpPost]
        [System.Web.Http.Route("api/PostRegistrationForm")]
        public HttpResponseMessage PostRegistrationForm(Req_Registration_API_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside RegistrationFormController " + Environment.NewLine);
            CommonResponseDataOrganization result = new CommonResponseDataOrganization();
            var response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string UserInput = getRawPostData().Result;
                    string CommonResult = "";
                    AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                    int VendorAPIType = 0;
                    int Type = 0;
                    Int64 memId = Convert.ToInt64(user.MemberId);
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                    if (CommonResult.ToLower() != "success")
                    {
                        CommonResponseDataOrganization cres1 = new CommonResponseDataOrganization();
                        cres1 = CommonApiMethod.ReturnBadRequestMessages(CommonResult);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres1);
                        return response;
                    }

                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string Ipaddress = Common.GetUserIP();

                    var jsonData = new RegistrationForm
                    {
                        id = user.id,
                        orgId = user.orgId,
                        eventName = user.eventName,
                        jsonField = user.jsonField,
                        createdDate = user.createdDate,
                        Name = user.Name,
                        Address = user.Address,
                        PhoneNo = user.PhoneNo,
                        Email = user.Email,
                        isActive = user.isActive,
                        isDeleted = user.isDeleted
                    };
                    var data = JsonConvert.SerializeObject(jsonData);
                    GetDataFromOrganization obj = RepKhalti.PostRegistrationData("", user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.organization_events), data);

                    if (string.IsNullOrEmpty(obj.message))
                    {
                        CommonResponseDataOrganization cres = CommonApiMethod.ReturnBadRequestMessages(obj.message);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = "Success";
                        result.ResponseCode = 1;
                        result.status = true;
                        result.StatusCode = Convert.ToString(HttpStatusCode.Accepted);
                        response = Request.CreateResponse<CommonResponseDataOrganization>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} RegistrationFormController completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()}   RegistrationFormController {ex.ToString()}" + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            return response;
        }
    }
}

