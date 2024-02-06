using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class RequestAPI_NotificationController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_NotificationController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getall-notificationcampaign")]
        public HttpResponseMessage GetNotificationsListing(Req_Notifications user)
         {
            log.Info($"{System.DateTime.Now.ToString()} inside GetNotificationsListing" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Notifications result = new Res_Notifications();
            var response = Request.CreateResponse<Res_Notifications>(System.Net.HttpStatusCode.BadRequest, result);
            string userInput = getRawPostData().Result;
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
                   /* string md5hash = Common.CheckHash(user);*/
                    string md5hash = Common.getHashMD5(userInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin res = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
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
                        List<AddNotificationCampaign> list = new List<AddNotificationCampaign>();
                        string msg = RepNotifications.GetApiNotifications(user.SearchText, res, user.Take.ToString(), user.Skip.ToString(), user.Type.ToString(), ref list);
                        if (msg.ToLower() == Common.CommonMessage.success)
                        {
                            result.Notifications = list;
                            result.Message = msg;
                            result.ReponseCode = 1;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Notifications>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            result.Notifications = list;
                            result.Message = msg;
                            result.ReponseCode = 0;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Notifications>(System.Net.HttpStatusCode.NotFound, result);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetNotificationsListing completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetNotificationsListing {ex.ToString()} " + Environment.NewLine);
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