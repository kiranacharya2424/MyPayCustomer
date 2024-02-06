using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MyPay.API.Controllers
{
    public class ChangePinWithOTCController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(ChangePinWithOTCController));
        public HttpResponseMessage Post(Req_ChangePin user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside ChangePinController" + Environment.NewLine);
            Res_ChangePin result = new Res_ChangePin();
            var response = Request.CreateResponse<Res_ChangePin>(System.Net.HttpStatusCode.BadRequest, result);
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
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    string md5hash = Common.CheckHash(user,"NO");
                    
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else if(string.IsNullOrEmpty(user.MemberId.ToString()) || user.MemberId.ToString() == "0")
                    {
                        CommonResponse cres1 = new CommonResponse();
                        cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        return response;
                    }
                    AddUserLoginWithPin res = new AddUserLoginWithPin();
                    bool status = false;
                    string msg = RepUser.ChangePinSendOTP(ref res, ref status, user.Password, user.Pin, user.ConfirmPin, user.MemberId, true, user.PlatForm, user.DeviceCode);
                    if (!status)
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = msg;
                        result.Details = msg;
                        result.responseMessage = msg;
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
    }
}