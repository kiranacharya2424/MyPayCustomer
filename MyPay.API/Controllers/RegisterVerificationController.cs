using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MyPay.API.Controllers
{
    public class RegisterVerificationController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RegisterVerificationController));


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
        public HttpResponseMessage Post(Req_RegisterVerification user)
        {
            string userInput = getRawPostData().Result;


            log.Info($"  {System.DateTime.Now.ToString()} inside RegisterVerification   {Environment.NewLine}");
            Res_UserDetail result = new Res_UserDetail();
            var response = Request.CreateResponse<Res_UserDetail>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.getHashMD5(userInput);  //Common.CheckHash(user);
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

                    log.Info($"  {System.DateTime.Now.ToString()}  RegisterVerification started  {Environment.NewLine} ");
                    string msg = RepUser.RegisterVerification(ref ReturnString, ref IsLoginWithPassword, ref IsDetailUpdated, authenticationToken, user.ContactNumber, user.PhoneExt, user.PlatForm, user.DeviceCode, true, ref Otp, "", false, user.PlatForm + ":" + user.Version_Number);
                    log.Info($"  {System.DateTime.Now.ToString()}  RegisterVerification ended  {Environment.NewLine} ");


                    if (msg.ToLower() == "get user detail")
                    {
                        result.ContactNumber = user.ContactNumber;
                        result.PhoneExt = user.PhoneExt;
                        result.IsDetailUpdated = IsDetailUpdated;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;

                        response = Request.CreateResponse<Res_UserDetail>(System.Net.HttpStatusCode.OK, result);

                        return response;
                    }
                    else if (msg.ToLower() == "login")
                    {
                        result.IsDetailUpdated = IsDetailUpdated;
                        result.ContactNumber = user.ContactNumber;
                        result.PhoneExt = user.PhoneExt;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                        result.IsLoginWithPassword = IsLoginWithPassword;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_UserDetail>(System.Net.HttpStatusCode.OK, result);
                        return response;
                    }
                    else if (msg.ToLower() == "success")
                    {
                        Res_RegisterVerification res = new Res_RegisterVerification();
                        res.Message = "Success";
                        res.ReponseCode = 1;
                        res.VerificationOtp = "4444";
                        res.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_RegisterVerification>(System.Net.HttpStatusCode.OK, res);
                        log.Info($"  {System.DateTime.Now.ToString()}  RegisterVerification Completed  {Environment.NewLine} ");
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
                log.Error($"  {System.DateTime.Now.ToString()}   RegisterVerification {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}