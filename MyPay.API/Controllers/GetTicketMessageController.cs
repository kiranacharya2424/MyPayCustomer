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
    public class GetTicketMessageController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetTicketMessageController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getticketmessage")]
        public HttpResponseMessage GetTicketDropDowns(Req_GetTicketMessage user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside getticketmessage" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddTicketsReply> list = new List<AddTicketsReply>();
            Res_GetTicketMessage result = new Res_GetTicketMessage();
            var response = Request.CreateResponse<Res_GetTicketMessage>(System.Net.HttpStatusCode.BadRequest, result);

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
                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        AddTicketsReply outobject = new AddTicketsReply();
                        GetTicketsReply inobject = new GetTicketsReply();
                        inobject.TicketId = user.TicketId;
                        inobject.CheckIsNote = 0;
                        inobject.Take = Convert.ToInt32(user.Take);
                        inobject.Skip = Convert.ToInt32(user.Skip) * Convert.ToInt32(user.Take);
                        list = RepCRUD<GetTicketsReply, AddTicketsReply>.GetRecordList(Common.StoreProcedures.sp_TicketsReply_Get, inobject, outobject);
                        if (Common.ApplicationEnvironment.IsProduction)
                        {
                            list.ForEach(x => x.AttachFile = Common.LiveSiteUrl + "/Images/TicketsImages/" + x.AttachFile);

                        }
                        else
                        {
                            list.ForEach(x => x.AttachFile = Common.TestSiteUrl + "/Images/TicketsImages/" + x.AttachFile);

                        }

                        result.TicketReplyList = list;


                        result.status = true;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} getticketmessage completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} getticketmessage {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getalltickets")]
        public HttpResponseMessage GetAllTickets(Req_GetAllTickets user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside getalltickets" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddTicket> list = new List<AddTicket>();
            Res_GetAllTickets result = new Res_GetAllTickets();
            var response = Request.CreateResponse<Res_GetAllTickets>(System.Net.HttpStatusCode.BadRequest, result);

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
                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string BasePath = Common.TestSiteUrl;
                        if (Common.ApplicationEnvironment.IsProduction)
                        {
                            BasePath = Common.LiveSiteUrl;
                        }
                        AddTicket outobject = new AddTicket();
                        GetTicket inobject = new GetTicket();
                        inobject.CreatedBy = user.MemberId;
                        inobject.TicketTitle = user.TicketTitle;
                        inobject.CheckIsFavourite = user.CheckIsFavourite;
                        inobject.Status = user.Status;
                        inobject.CheckIsSeen = user.CheckIsSeen;
                        inobject.Take = user.Take;
                        inobject.Skip = Convert.ToInt32(user.Skip) * Convert.ToInt32(user.Take);
                        list = RepCRUD<GetTicket, AddTicket>.GetRecordList(Common.StoreProcedures.sp_Tickets_Get, inobject, outobject);
                        result.TicketList = list;

                        //result.TicketImageLink = Common.LiveSiteUrl + "/Images/TicketsImages/";
                        result.status = true;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} getalltickets completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} getalltickets {ex.ToString()} " + Environment.NewLine);
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