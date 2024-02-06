using log4net;
using MyPay.API.Models;
using MyPay.Models.Common;
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
    public class ResetPasswordWithOTCController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(ResetPasswordWithOTCController));
        public HttpResponseMessage Post(Req_ResetPasswordWithOTC user)
        {
            string userInput = getRawPostData().Result;




            log.Info($"{System.DateTime.Now.ToString()} inside ResetPasswordWithOTCController" + Environment.NewLine);
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
                    string md5hash = Common.getHashMD5(userInput); // Common.CheckHash(user,"No");
                  
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }

                    string msg = RepUser.ResetPasswordWithOTC(user.ContactNumber,user.Password, user.OTC, user.MemberId, true, user.PlatForm, user.DeviceCode);
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
                        //result.MemberId = RepUser.MemberId;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_User>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} ResetPasswordWithOTCController completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} ResetPasswordWithOTCController {ex.ToString()} " + Environment.NewLine);
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