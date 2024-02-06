using MyPay.API.Models;
using MyPay.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPay.Models.Common;
using System.Data.Entity.Validation;
using log4net;
using MyPay.API.Models.Settings;
using MyPay.Models.Add;
using System.IO;

namespace MyPay.API.Controllers
{
    public class RequestAPI_AppMessageController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_AppMessageController));

        [HttpPost]
        [Route("api/lookup-appmessage")]
        public HttpResponseMessage GetLookupAppMessage(Req_Settings user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-appmessage" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Settings_Requests result = new Res_Settings_Requests();
            var response = Request.CreateResponse<Res_Settings_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    //if (results != "Success")
                    //{
                    //    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    //    response.StatusCode = HttpStatusCode.BadRequest;
                    //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    //    return response;
                    //}
                    //else
                    {
                        string msg = string.Empty;
                        try
                        {
                            var json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/app_message.json"));
                            result = Newtonsoft.Json.JsonConvert.DeserializeObject<Res_Settings_Requests>(json);
                            msg = "success";
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                        }
                        
                        if (msg.ToLower() == "success")
                        {
                            
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Settings_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-appmessage completed" + Environment.NewLine);
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
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} lookup-appmessage  {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        } 
    }
}