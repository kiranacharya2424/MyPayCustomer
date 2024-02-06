using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class TicketDropDownController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(TicketDropDownController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getalldropdowns")]
        public HttpResponseMessage GetTicketDropDowns(Req_TicketCategory user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside getalldropdowns" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddTicketCategory> list = new List<AddTicketCategory>();
            Res_TicketDropDowns result = new Res_TicketDropDowns();
            var response = Request.CreateResponse<Res_TicketDropDowns>(System.Net.HttpStatusCode.BadRequest, result);
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
                        AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resUser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        //foreach (int r in Enum.GetValues(typeof(AddTicket.Priorities)))
                        //{
                        //    EnumDropDowns et = new EnumDropDowns();
                        //    et.Text = Enum.GetName(typeof(AddTicket.Priorities), r).ToString().Replace("_", " ");
                        //    et.Id = r.ToString();
                        //    result.Priority.Add(et);
                        //}
                        foreach (int r in Enum.GetValues(typeof(AddTicket.TicketStatus)))
                        {
                            EnumDropDowns et = new EnumDropDowns();
                            et.Text = Enum.GetName(typeof(AddTicket.TicketStatus), r).ToString().Replace("_", " ");
                            et.Id = r.ToString();
                            result.TicketStatus.Add(et);
                        }
                        //list = new List<AddTicketCategory>();
                        AddTicketCategory outobject = new AddTicketCategory();
                        GetTicketCategory inobject = new GetTicketCategory();
                        inobject.CheckDelete = 0;
                        inobject.CheckActive = 1;
                        list = RepCRUD<GetTicketCategory, AddTicketCategory>.GetRecordList(Common.StoreProcedures.sp_TicketsCategory_Get, inobject, outobject);
                        result.Category = list;
                        result.status = true;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} getalldropdowns completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} getalldropdowns {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} getalldropdowns {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}