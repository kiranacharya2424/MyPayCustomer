using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using MyPay.Repository;
using Swashbuckle.Swagger;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetUserInfoController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetUserInfoController));
        public HttpResponseMessage Post(Req_GetUserInfo user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetUserInfoController" + Environment.NewLine);
            string WebPrefix = Common.LiveSiteUrl;
            Res_GetUserDetail result = new Res_GetUserDetail();
            var response = Request.CreateResponse<Res_GetUserDetail>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string md5hash = Common.CheckHash(user);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }

                    string CommonResult = "";
                    AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                    AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                    GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                    inobjectUser.MemberId = Convert.ToInt64(Common.DecryptString(user.RecieverMemberId));
                    AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                    if (res != null && res.Id != 0)
                    {
                        result.Name = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                        result.UserImage = res.UserImage;
                        result.RoleId = res.RoleId;
                        if (res.RoleId == 4) // Merchant
                        {
                            AddMerchant outobjectmobile = new AddMerchant();
                            GetMerchant inobjectmobile = new GetMerchant();
                            inobjectmobile.ContactNo = res.ContactNumber;
                            AddMerchant resmobile = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectmobile, outobjectmobile);
                            if (resmobile != null && resmobile.Id != 0)
                            {
                                result.Name = "Merchant: " + resmobile.OrganizationName;
                                if (!string.IsNullOrEmpty(resmobile.Image))
                                {
                                    if (Common.ApplicationEnvironment.IsProduction)
                                    {
                                        result.UserImage = Common.LiveSiteUrl + "/Images/MerchantImages/" + resmobile.Image;
                                    }
                                    else
                                    {
                                        result.UserImage = Common.TestSiteUrl + "/Images/MerchantImages/" + resmobile.Image;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(res.UserImage))
                            {
                                if (Common.ApplicationEnvironment.IsProduction)
                                {
                                    result.UserImage = Common.LiveSiteUrl + "/UserDocuments/Images/" + res.UserImage;
                                }
                                else
                                {
                                    result.UserImage = Common.TestSiteUrl + "/UserDocuments/Images/" + res.UserImage;
                                }
                            }
                        }
                        //result.ContactNumber = res.ContactNumber.Substring(0, 3) + "*****" + res.ContactNumber.Substring(8, 2);
                        result.ContactNumber = res.ContactNumber;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_GetUserDetail>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} GetUserInfoController completed" + Environment.NewLine);
                        return response;
                    }
                    else
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} GetUserInfoController {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        public CommonResponseData GetUserQR(Req_GetUserInfo user, AddUserLoginWithPin resuserdetails)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetUserInfoController" + Environment.NewLine);
            string WebPrefix = Common.LiveSiteUrl;
            CommonResponseData obj = new CommonResponseData();
            Res_GetUserDetail result = new Res_GetUserDetail();
            //var response = Request.CreateResponse<Res_GetUserDetail>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                inobjectUser.MemberId = Convert.ToInt64(Common.DecryptString(user.RecieverMemberId));
                AddUserLoginWithPin res = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                if (res != null && res.Id != 0)
                {
                    result.Name = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                    result.UserImage = res.UserImage;
                    result.RoleId = res.RoleId;
                    if (res.RoleId == 4) // Merchant
                    {
                        AddMerchant outobjectmobile = new AddMerchant();
                        GetMerchant inobjectmobile = new GetMerchant();
                        inobjectmobile.ContactNo = res.ContactNumber;
                        AddMerchant resmobile = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectmobile, outobjectmobile);
                        if (resmobile != null && resmobile.Id != 0)
                        {
                            result.Name = "Merchant: " + resmobile.OrganizationName;
                            if (!string.IsNullOrEmpty(resmobile.Image))
                            {
                                if (Common.ApplicationEnvironment.IsProduction)
                                {
                                    result.UserImage = Common.LiveSiteUrl + "/Images/MerchantImages/" + resmobile.Image;
                                }
                                else
                                {
                                    result.UserImage = Common.TestSiteUrl + "/Images/MerchantImages/" + resmobile.Image;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(res.UserImage))
                        {
                            if (Common.ApplicationEnvironment.IsProduction)
                            {
                                result.UserImage = Common.LiveSiteUrl + "/UserDocuments/Images/" + res.UserImage;
                            }
                            else
                            {
                                result.UserImage = Common.TestSiteUrl + "/UserDocuments/Images/" + res.UserImage;
                            }
                        }
                    }
                    //result.ContactNumber = res.ContactNumber.Substring(0, 3) + "*****" + res.ContactNumber.Substring(8, 2);
                    result.ContactNumber = res.ContactNumber;
                    result.Message = "Success";
                    result.ReponseCode = 1;
                    result.status = true;
                    obj.status = true;
                    obj.ReponseCode = 1;
                    obj.Message = "success";
                    //obj.StatusCode = response.StatusCode.ToString();
                    obj.Data = result;
                    log.Info($"{System.DateTime.Now.ToString()} GetUserInfoController completed" + Environment.NewLine);
                    return obj;
                }
                else
                {
                    obj.status = false;
                    obj.ReponseCode = 0;
                    obj.Message = "User Not Found";
                    //obj.StatusCode = response.StatusCode.ToString();
                    obj.Data = result;
                    //CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                    // response.StatusCode = HttpStatusCode.BadRequest;
                    // response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return obj;
                }

            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} GetUserInfoController {ex.ToString()} " + Environment.NewLine);
                obj.status = false;
                obj.ReponseCode = 0;
                obj.Message = ex.Message;
                //obj.StatusCode = response.StatusCode.ToString();
                obj.Data = result;
                return obj;
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}