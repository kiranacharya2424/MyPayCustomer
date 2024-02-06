
using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Antivirus.DataPack;
using MyPay.API.Models.DataPack;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MyPay.Models.VendorAPI.Get.CableCar;
using MyPay.API.Models.Response.CableCar;
using System.Data;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Remoting;
using Microsoft.Ajax.Utilities;
using Dapper;
using System.Xml.Linq;
using MyPay.Models.Miscellaneous;
using System.Web.Services.Description;
using System.Runtime.Remoting.Messaging;
using MyPay.API.Models.Airlines;
using iText.Html2pdf;
using QRCoder;
using System.Drawing;
using System.Web;
using System.Drawing.Imaging;
using Swashbuckle.Swagger;
using System.Windows;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using System.Collections;
//using MyPay.API.Models.Request.Horoscope;
//using MyPay.API.Models.Response.Horoscope;
using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;
using MyPay.API.Models.Antivirus.Bussewa;
using System.Windows.Interop;
//using System.Web.Mvc;

namespace MyPay.API.Controllers
{
    public class RequestAPI_HoroscopeController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_HoroscopeController));
        /* string ApiResponse = string.Empty;*/
        //#region for Horoscope_Details
        //[HttpPost]
        //[Route("api/get-current-horoscope-details")]
        //public HttpResponseMessage GetDetails_Horoscope(Req_Vendor_API_Horoscope_Details user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside horoscope_details" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_Horoscope_Details_Response result = new Res_Vendor_API_Horoscope_Details_Response();
        //    var response = Request.CreateResponse<Res_Vendor_API_Horoscope_Details_Response>(System.Net.HttpStatusCode.BadRequest, result);
        //    try
        //    {
        //        if (Request.Headers.Authorization == null)
        //        {
        //            string results = "Un-Authorized Request";
        //            cres = CommonApiMethod.ReturnBadRequestMessage(results);
        //            response.StatusCode = HttpStatusCode.BadRequest;
        //            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //            return response;
        //        }
        //        else
        //        {
        //            string md5hash = Common.CheckHash(user);

        //            string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
        //            if (results != "Success")
        //            {
        //                cres = CommonApiMethod.ReturnBadRequestMessage(results);
        //                response.StatusCode = HttpStatusCode.BadRequest;
        //                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                return response;
        //            }
        //            else
        //            {
        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

        //                Int64 memId = 0;
        //                int VendorAPIType = 0;
        //                int Type = 0;
        //                resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
        //                if (CommonResult.ToLower() != "success")
        //                {
        //                    CommonResponse cres1 = new CommonResponse();
        //                    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }
        //                getcurrenthoroscope objRes = new getcurrenthoroscope();
        //                var jsonData = new getcurrenthoroscope
        //                {
        //                    frequency = user.Frequency
                            
        //                };
        //                var data = JsonConvert.SerializeObject(jsonData);
        //                string KhaltiAPIURL = "/get-current-horoscope-details";
        //                string msg = VendorApi_CommonHelper.PostMethod_HoroscopeDetails(VendorApi_CommonHelper.Horoscope_URL_Prefix_localhost + KhaltiAPIURL, data);

        //                Res_Vendor_API_Horoscope_Details_Response res = JsonConvert.DeserializeObject<Res_Vendor_API_Horoscope_Details_Response>(msg);
        //                res.Message = "success";
        //                response.StatusCode = HttpStatusCode.BadRequest;
        //                response = Request.CreateResponse<Res_Vendor_API_Horoscope_Details_Response>(System.Net.HttpStatusCode.OK, res);

        //                return response;

        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} search-horoscope-details-completed" + Environment.NewLine);
        //        return response;
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error($"{System.DateTime.Now.ToString()} search-bus-routes {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
       // }

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