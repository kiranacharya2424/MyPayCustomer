﻿using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class ChangePinController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(ChangePinController));
        public HttpResponseMessage Post(Req_ChangePin user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside ChangePinController" + Environment.NewLine);
            Res_ChangePin result = new Res_ChangePin();
            var response = Request.CreateResponse<Res_ChangePin>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

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
                    string CommonResult = "";
                    AddUserLoginWithPin res = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    string md5hash = Common.getHashMD5(userInput);// Common.CheckHash(user, "NO");

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                    string msg = RepUser.ChangePin(res, user.Password, user.Pin, user.ConfirmPin, user.MemberId, true, user.PlatForm, user.DeviceCode);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = "MPIN Changed Successfully";
                        result.ReponseCode = 1;
                        result.MemberId = user.MemberId;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_ChangePin>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} ChangePinController completed" + Environment.NewLine);
                        return response;

                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()}   ChangePinController {ex.ToString()} " + Environment.NewLine);
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