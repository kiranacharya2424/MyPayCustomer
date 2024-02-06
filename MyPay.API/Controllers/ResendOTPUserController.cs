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

namespace MyPay.API.Controllers
{
    public class ResendOTPUserController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(ResendOTPUserController));
        public HttpResponseMessage Post(Req_RegisterVerification user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside ResendOTPUserController"+Environment.NewLine);
            Res_ResendOTP result = new Res_ResendOTP();
            var response = Request.CreateResponse<Res_ResendOTP>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string Otp = "";
                    string ReturnString = "";
                    bool IsDetailUpdated = false;
                    bool IsLoginWithPassword = false;
                    string msg = RepUser.RegisterVerification(ref ReturnString, ref IsLoginWithPassword, ref IsDetailUpdated, authenticationToken, user.ContactNumber, user.PhoneExt, user.PlatForm, user.DeviceCode, true, ref Otp, "yes", false, user.PlatForm + ":" + user.Version_Number);


                    if (msg.ToLower() == "get user detail" || msg.ToLower() == "success" || msg.ToLower() == "login")
                    {

                        result.ContactNumber = user.ContactNumber;
                        result.PhoneExt = user.ContactNumber;
                        //result.VerificationOTP = Otp;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_ResendOTP>(System.Net.HttpStatusCode.OK, result);
                        return response;

                    }
                    else if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        Res_RegisterVerification res = new Res_RegisterVerification();
                        res.Message = msg;
                        res.ReponseCode = 1;
                        //res.VerificationOtp = Otp;
                        res.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_RegisterVerification>(System.Net.HttpStatusCode.OK, res);
                        log.Info($"{System.DateTime.Now.ToString()}  ResendOTPUserController Completed"+Environment.NewLine);
                        return response;
                    }
                }                
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} ResendOTPUserController {ex.ToString()}"+ Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}