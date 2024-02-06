using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class DealsAndOffersController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetCouponsScratchedController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getDealsAndOffersAll")]
        public HttpResponseMessage GetDealsAndOffersAll(Req_DealsAndOffers user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside getDealsAndOffersAll" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_DealsAndOffers result = new Res_DealsAndOffers();
            var response = Request.CreateResponse<Res_DealsAndOffers>(System.Net.HttpStatusCode.BadRequest, result);
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
                        if (res != null && res.Id != 0)
                        {
                            List<AddDealsandOffers> list = new List<AddDealsandOffers>();
                            string msg = RepDealsAndOffers.GetApiDealsandOffers(res, user.Take.ToString(), user.Skip.ToString(), user.Type.ToString(), ref list);
                            if (msg.ToLower() == Common.CommonMessage.success)
                            {
                                result.data = list;
                                result.Message = msg;
                                result.ReponseCode = 1;
                                result.status = true;
                                response.StatusCode = HttpStatusCode.Accepted;
                                response = Request.CreateResponse<Res_DealsAndOffers>(System.Net.HttpStatusCode.OK, result);
                            }
                            else
                            {
                                result.data = list;
                                result.Message = msg;
                                result.ReponseCode = 0;
                                result.status = true;
                                response.StatusCode = HttpStatusCode.Accepted;
                                response = Request.CreateResponse<Res_DealsAndOffers>(System.Net.HttpStatusCode.NotFound, result);
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} getDealsAndOffersAll completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} getDealsAndOffersAll {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}