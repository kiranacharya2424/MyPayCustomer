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
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetReferEarnImageController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetReferEarnImageController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/get-refer-earn-image")]
        public HttpResponseMessage GetReferEarnImage(Req_ReferEarnImage user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetReferEarnImage" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddReferEarnImage> list = new List<AddReferEarnImage>();
            Res_ReferEarnImage result = new Res_ReferEarnImage();
            var response = Request.CreateResponse<Res_ReferEarnImage>(System.Net.HttpStatusCode.BadRequest, result);
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
                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        AddReferEarnImage outobject = new AddReferEarnImage();
                        GetReferEarnImage inobject = new GetReferEarnImage();
                        inobject.CheckActive = 1;
                        list = RepCRUD<GetReferEarnImage, AddReferEarnImage>.GetRecordList(Common.StoreProcedures.sp_ReferEarnImage_Get, inobject, outobject);
                        for (int i = 0; i < list.Count; i++)
                        {
                            list[i].Image = Common.LiveSiteUrl + $"/Images/ReferEarnImages/" + list[i].Image;
                        }
                        result.status = true;
                        result.data = list;
                        result.ReponseCode = 1;
                        result.Message = "Success";
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetReferEarnImage completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetReferEarnImage {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}