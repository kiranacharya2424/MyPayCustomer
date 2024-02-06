using MyPay.API.Models;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using MyPay.Models.Common;
using MyPay.Models.Add;
using log4net;
using System.IO;
using System.Threading.Tasks;

namespace MyPay.API.Controllers
{
    public class ForgotResendOTPUserController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(ForgotResendOTPUserController));
        public HttpResponseMessage Post(Req_RegisterVerification user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside ForgotResendOTPUserController" + Environment.NewLine);
            Res_ResendOTP result = new Res_ResendOTP();
            var response = Request.CreateResponse<Res_ResendOTP>(System.Net.HttpStatusCode.BadRequest, result);

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
                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);
                 
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string msg = RepUser.ResendOtp( user.ContactNumber,true,user.PlatForm, user.DeviceCode);
                    if (msg.ToLower() == "success" )
                    {
                        result.ContactNumber = user.ContactNumber;
                        result.PhoneExt = user.ContactNumber;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_ResendOTP>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} ForgotResendOTPUserController completed" + Environment.NewLine);
                        return response;
                    }
                    else 
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }                   
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} ForgotResendOTPUserController {ex.ToString()} " + Environment.NewLine);
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