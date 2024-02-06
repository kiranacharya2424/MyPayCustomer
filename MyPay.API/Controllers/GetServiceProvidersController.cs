using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetServiceProvidersController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetServiceProvidersController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getall-providers")]
        public HttpResponseMessage GetProvidersListing(Req_Service_Providers user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetProvidersListing" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Service_Providers result = new Res_Service_Providers();
            var response = Request.CreateResponse<Res_Service_Providers>(System.Net.HttpStatusCode.BadRequest, result);
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


                        List<GetService_Providers> objRes = VendorApi_CommonHelper.GetServiceProvidersWithUtility();

                        List<GetService_Providers> objRes2 = new List<GetService_Providers>();

                        string version_number = user.Version_Number;

                        

                        foreach (GetService_Providers obj in objRes)
                        {
                            if (obj.isActive == 1)
                            {

                                if (string.IsNullOrEmpty(version_number) && user.PlatForm.ToUpper() != "WEB")
                                {
                                    if (!(obj.ProviderTypeId == "134" || obj.ProviderTypeId == "122" || obj.ProviderTypeId == "140" || obj.ProviderTypeId == "142" || obj.ProviderTypeId == "143" || obj.ProviderTypeId == "151" || obj.ProviderTypeId == "154" || obj.ProviderTypeId == "155"))
                                    {
                                        objRes2.Add(obj);
                                    }
                                }
                                else
                                {
                                    if ((user.PlatForm.ToLower() == "ios" && isFirstVersionHigher(version_number, "15.9"))
                                        ||
                                        (user.PlatForm.ToLower() == "android" && isFirstVersionHigher(version_number + ".0", "93.0"))
                                        ||
                                        (user.PlatForm.ToLower() == "web")
                                        )
                                    {
                                        if (!(obj.ProviderTypeId == "73"))
                                        {
                                            objRes2.Add(obj);
                                        }
                                    }
                                    else
                                    {
                                        if (!(obj.ProviderTypeId == "134" || obj.ProviderTypeId == "122" || obj.ProviderTypeId == "140" || obj.ProviderTypeId == "142" || obj.ProviderTypeId == "143" || obj.ProviderTypeId == "151" || obj.ProviderTypeId == "154" || obj.ProviderTypeId == "155"))
                                        {
                                            objRes2.Add(obj);
                                        }
                                    }
                                }
                            }
                        }
                        //foreach (GetService_Providers obj in objRes)
                        //{
                        //    if (obj.ProviderTypeId == "134")
                        //    {
                        //        //objRes.Remove(obj);
                        //    }
                        //    else { 
                        //        objRes2.Add(obj);
                        //    }

                        //}


                        result.Providers = objRes2;
                        result.Message = "success";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Service_Providers>(System.Net.HttpStatusCode.OK, result);

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetProvidersListing completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetProvidersListing {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public bool isFirstVersionHigher(string v1, string v2)
        {
            var version1 = new Version(v1);
            var version2 = new Version(v2);

            var result = version1.CompareTo(version2);
            if (result > 0)
                return true;
            //Console.WriteLine("version1 is greater");
            else if (result < 0)
                return false;
            //Console.WriteLine("version2 is greater");
            else
                return false;
        }
    }
}