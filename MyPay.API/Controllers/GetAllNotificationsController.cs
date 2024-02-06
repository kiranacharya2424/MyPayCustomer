using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
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
    public class GetAllNotificationsController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetAllNotificationsController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getallnotifications")]
        public HttpResponseMessage GetAllNotifications(Req_GetAllNotifications user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetAllNotifications" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddNotification> list = new List<AddNotification>();
            Res_GetAllNotifications result = new Res_GetAllNotifications();
            var response = Request.CreateResponse<Res_GetAllNotifications>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

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
                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

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
                        AddNotification outobject = new AddNotification();
                        GetNotification inobject = new GetNotification();
                        inobject.MemberId = user.MemberId;
                        inobject.CheckActive = 1;
                        inobject.CheckDelete = 0;
                        if (user.Take != "" && user.Skip != "")
                        {
                            inobject.Take = Convert.ToInt32(user.Take);
                            inobject.Skip = Convert.ToInt32(user.Take) * Convert.ToInt32(user.Skip);
                        }
                        else
                        {
                            inobject.Take = 10;
                        }
                        list = RepCRUD<GetNotification, AddNotification>.GetRecordList(Common.StoreProcedures.sp_Notification_Get, inobject, outobject);
                        result.status = true;
                        result.data = list;

                        //AddNotification outobject_Unread = new AddNotification();
                        //GetNotification inobject_Unread = new GetNotification();
                        //inobject_Unread.MemberId = user.MemberId;
                        //inobject_Unread.ReadStatus = 0;
                        //inobject_Unread.CheckActive = 1;
                        //inobject_Unread.CheckDelete = 0;
                        //List<AddNotification> list_Unread = new List<AddNotification>();
                        //list_Unread = RepCRUD<GetNotification, AddNotification>.GetRecordList(Common.StoreProcedures.sp_Notification_Get, inobject_Unread, outobject_Unread);
                        Int32 list_Unread = Common.GetNotificatUnReadCount(user.MemberId);
                        result.UnreadCount = Convert.ToInt32(list_Unread);
                        result.ReponseCode = 1;
                        result.Message = "Success";
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_GetAllNotifications>(System.Net.HttpStatusCode.Accepted, result);
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetAllNotifications completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()}   RegisterVerification {ex.ToString()} " + Environment.NewLine);
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