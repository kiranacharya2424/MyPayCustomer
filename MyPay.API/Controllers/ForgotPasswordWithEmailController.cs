using MyPay.API.Models;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MyPay.Repository;
using System.Net;
using System.Web.Http;
using log4net;
using MyPay.Models.Add;

namespace MyPay.API.Controllers
{
    public class ForgotPasswordWithEmailController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(ForgotPasswordWithEmailController));
        public HttpResponseMessage Post(Req_ForgotPasswordWithEmail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside ForgotPasswordWithEmailController" + Environment.NewLine);
            Res_User result = new Res_User();
            var response = Request.CreateResponse<Res_User>(System.Net.HttpStatusCode.BadRequest, result);
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
                    AddUserLoginWithPin res = new AddUserLoginWithPin(); 
                    string msg = RepUser.ForgetPasswordwithEmail(user.Email, true, user.PlatForm, user.DeviceCode, ref res);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = msg;
                        result.ReponseCode = 1;
                        //result.MemberId = res.MemberId;
                        Common.SendSMS(RepUser.ContactNumber, "Your verification code is "+ RepUser.VerificationOtp + ".Please enter this code to verify your account.Thank you for using MyPay.");

                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_User>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} ForgotPasswordWithEmailController completed" + Environment.NewLine);
                        return response;

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} ForgotPasswordWithEmailController {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}