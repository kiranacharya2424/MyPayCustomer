using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;
using MyPay.Models;
using commonresponsedata = MyPay.Models.VendorAPI.Get.BusSewaService.commonresponsedata;
using GetVendor_API_BusSewa_Routes_Lookup = MyPay.Models.GetVendor_API_BusSewa_Routes_Lookup;
using System.Web.Caching;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
using MyPay.API.Models.Airlines;
using System.Text.RegularExpressions;

namespace MyPay.API.Controllers
{
    public class BusSewa_RequestAPIController : ApiController
    {
        // GET: BusSewa_RequestAPI
        private static ILog log = LogManager.GetLogger(typeof(BusSewa_RequestAPIController));

        string ApiResponse = string.Empty;

        [HttpPost]
        [Route("api/bus-routes")]
        public HttpResponseMessage GetLookupService_BussewaRoutes(Req_Vendor_API_Bussewa_Routes_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside bus-routes" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            //CommonResponse cres = new CommonResponse();
            commonresponsedata result = new commonresponsedata();
            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput); //Common.CheckHash(user);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {
                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        //    return response;
                        //}


                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        GetDataFromBusSewa getroutesdetail = RepKhalti.Get_BusSewa_ROUTES_RequestService(user.Reference, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa));
                        if (getroutesdetail.IsException == false)
                        {
                            GetVendor_API_BusSewa_Routes_Lookup obj = JsonConvert.DeserializeObject<GetVendor_API_BusSewa_Routes_Lookup>(getroutesdetail.message.ToString());
                            if (obj != null)
                            {
                                if (obj.status == "1")
                                {
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj.routes;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);
                                }
                                else
                                {
                                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(obj.message);
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    Res_output = JsonConvert.SerializeObject(result);
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                                    Rep_State = "Failed";
                                }
                            }
                            else
                            {
                                result = CommonApiMethod.BusSewaReturnBadRequestMessage(getroutesdetail.message);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                                Rep_State = "Failed";

                            }

                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} bus-routes {getroutesdetail.message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(getroutesdetail.message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }
                        if (!string.IsNullOrEmpty(getroutesdetail.Id))
                        {
                            var jsondata = Res_output;
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ", Res_Output='" + jsondata + "' where Id=" + getroutesdetail.Id + "");

                        }
                    }

                }
                log.Info($"{System.DateTime.Now.ToString()} bus-routes completed" + Environment.NewLine);
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
                //throw new CustomResponseException(false, "Bad Request", 0, "Exception Error", "", "");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} bus-routes {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/bus-trip")]
        public HttpResponseMessage GetLookupService_BusSewaTrip(Req_Vendor_API_Bussewa_Lookup_RequestsTrip user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside bus-trip" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            commonresponsedata result = new commonresponsedata();
            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {

                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        //    return response;
                        //}

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        //BusSewaModel _bus = new BusSewaModel();
                        //CommonDBResonse addbusdetail = _bus.AddBusDetails(user.from, user.to, user.MemberId, user.PlatForm, user.DeviceId);
                        //if (addbusdetail.Id == "1")
                        //{
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        //    return response;
                        //}
                        //else
                        //{
                        GetDataFromBusSewa tripdetails = RepKhalti.Get_BusSewa_TRIP_RequestService(user.Reference, user.from, user.to, user.shift, user.date, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa));

                        //------------------Check for exception--------------------------------------------//        
                        if (tripdetails.IsException == false)
                        {
                            Rootobject obj = JsonConvert.DeserializeObject<Rootobject>(tripdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.status == 1)
                                {
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    //result.BusdetailId = addbusdetail.Id;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = obj.message;
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Failed";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                                }
                            }
                            else
                            {
                                //cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                result.status = false;
                                result.ReponseCode = 0;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                result.Data = obj;
                                result.StatusCode = response.StatusCode.ToString();
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                            }
                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} bus-trip {tripdetails.message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(tripdetails.message);
                            //cres = CommonApiMethod.ReturnBadRequestMessage(tripdetails.message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }

                        if (!string.IsNullOrEmpty(tripdetails.Id))
                        {
                            var jsondata = Res_output;
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "' where Id=" + tripdetails.Id + "");

                        }
                    }


                    //}
                }
                log.Info($"{System.DateTime.Now.ToString()} bus-trip completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} bus-trip {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/bus-seat-refresh")]
        public HttpResponseMessage GetLookupService_BusSewaRefresh(Req_Vendor_API_Bussewa_Lookup_RequestsRefresh user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside bus-seat-refresh" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            commonresponsedata result = new commonresponsedata();
            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {

                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        //    return response;
                        //}

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        GetDataFromBusSewa refreshseatdetails = RepKhalti.Get_BusSewa_REFRESH_RequestService(user.Reference, user.Id, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa));
                        if (refreshseatdetails.IsException == false)
                        {
                            RootobjectRefreshAPI obj = JsonConvert.DeserializeObject<RootobjectRefreshAPI>(refreshseatdetails.message.ToString());

                            if (obj != null)
                            {
                                if (obj.status == 1)
                                {
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = obj.message;
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Failed";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                                }
                            }
                            else
                            {
                                result.status = false;
                                result.ReponseCode = 0;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                result.Data = obj;
                                result.StatusCode = response.StatusCode.ToString();
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                            }
                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} bus-seat-refresh {refreshseatdetails.message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(refreshseatdetails.message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }
                        if (!string.IsNullOrEmpty(refreshseatdetails.Id))
                        {
                            var jsondata = Res_output;
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "' where Id=" + refreshseatdetails.Id + "");

                        }

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} bus-seat-refresh completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} bus-seat-refresh {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/bus-book-seat")]
        public HttpResponseMessage GetLookupService_BusSewaBookSeat(Req_Vendor_API_Bussewa_Lookup_Requestsbookseat user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside bus-book-seat" + Environment.NewLine);
            BookSeat requestdata = new BookSeat();
            if (!string.IsNullOrEmpty(Convert.ToString(user.data)))
            {
                var seat = JsonConvert.DeserializeObject<RequestBookSeats>(user.data);
                requestdata.id = seat.id;
                requestdata.seat = seat.seat;
            }
            string UserInput = getRawPostData().Result;
            commonresponsedata result = new commonresponsedata();

            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {
                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        //    return response;
                        //}

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        //var jsondata = JsonConvert.SerializeObject(user.seat);
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        GetDataFromBusSewa bookseatdetails = RepKhalti.Get_BusSewa_BOOKSEAT_RequestService(user.Reference, requestdata.id, requestdata.seat, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa));
                        if (bookseatdetails.IsException == false)
                        {
                            RootobjectBookSeat obj = JsonConvert.DeserializeObject<RootobjectBookSeat>(bookseatdetails.message.ToString());

                            if (obj != null)
                            {
                                if (obj.status == 1)
                                {
                                    BusSewaModel _bus = new BusSewaModel();
                                    CommonDBResonse addbusdetail = _bus.AddBusDetails(user.from, user.to, user.MemberId, user.PlatForm, user.DeviceId, requestdata.id, obj.ticketSrlNo, user.DepatureTime, user.Operator, user.DepatureDate, user.BusType, user.BusDetailId, JsonConvert.SerializeObject(obj.boardingPoints), JsonConvert.SerializeObject(requestdata.seat), "TR", "", "", "");
                                    if (addbusdetail.code == "1")
                                    {
                                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                                        return response;
                                    }
                                    result.BusdetailId = addbusdetail.Id;
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = obj.message;
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Failed";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                                }
                            }
                            else
                            {
                                //cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                result.ReponseCode = 0;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                result.Data = obj;
                                result.StatusCode = response.StatusCode.ToString();
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                            }
                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} bus-book-seat {bookseatdetails.message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(bookseatdetails.message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }
                        if (!string.IsNullOrEmpty(bookseatdetails.Id))
                        {
                            var jsondata = Res_output;
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "' where Id=" + bookseatdetails.Id + "");

                        }

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} bus-book-seat completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} bus-book-seat {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/bus-cancel-queue")]
        public HttpResponseMessage GetLookupService_BusSewaCanceQueue(Req_Vendor_API_Bussewa_Lookup_RequestsCancelQueue user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside bus-cancel-queue" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            commonresponsedata result = new commonresponsedata();
            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {
                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        //    return response;
                        //}

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        //RepCRUD<AddLog, GetLog>.Insert(log, "logs");
                        //var jsondata = JsonConvert.SerializeObject(user.seat);

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        GetDataFromBusSewa bookseatdetails = RepKhalti.Get_BusSewa_CANCELQUEUE_RequestService(user.Reference, user.Id, user.ticketSrlNo, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa));
                        if (bookseatdetails.IsException == false)
                        {
                            RootobjectCancelQueue obj = JsonConvert.DeserializeObject<RootobjectCancelQueue>(bookseatdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.status == 1)
                                {
                                    BusSewaModel _bus = new BusSewaModel();
                                    CommonDBResonse addbusdetail = _bus.update("", user.Id, "", "", "", "", "", user.BusDetailId, "", "", "CTR", "", "", "");
                                    if (addbusdetail.code == "1")
                                    {
                                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                                        return response;
                                    }
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = obj.message;
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Failed";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                                }
                            }
                            else
                            {
                                //cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                result.status = false;
                                result.ReponseCode = 0;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                result.Data = obj;
                                result.StatusCode = response.StatusCode.ToString();
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                            }
                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} bus-cancel {bookseatdetails.message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(bookseatdetails.message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }
                        if (!string.IsNullOrEmpty(bookseatdetails.Id))
                        {
                            var jsondata = Res_output;
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ", Res_Output='" + jsondata + "' where Id=" + bookseatdetails.Id + "");
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} bus-cancel-queue completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} bus-cancel-queue {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/bus-passengerInfo")]
        public HttpResponseMessage GetLookupService_BusSewaPassengerInfo(Req_Vendor_API_Bussewa_Lookup_RequestPassengerDetail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside bus-passengerInfo" + Environment.NewLine);
            string UserInput = getRawPostData().Result;

            RequestPassengerDetail requestPassengerDetail = new RequestPassengerDetail();
            string jsondata2 = "";
            PassengerRootobject obj2 = new PassengerRootobject();
            if (!string.IsNullOrEmpty(Convert.ToString(user.data)))
            {
                var passenger = JsonConvert.DeserializeObject<RequestPassengerDetail>(Convert.ToString(user.data));
                requestPassengerDetail.id = passenger.id;
                requestPassengerDetail.contactNumber = passenger.contactNumber;
                requestPassengerDetail.email = passenger.email;
                requestPassengerDetail.name = passenger.name;
                requestPassengerDetail.boardingPoint = passenger.boardingPoint;
                requestPassengerDetail.ticketSrlNo = passenger.ticketSrlNo;
                //if (user.inputcode == "2")
                //{
                //    requestPassengerDetail.passengerTypeDetail = passenger.passengerTypeDetail;
                //}
                //if (user.inputcode == "3")
                //{
                //    requestPassengerDetail.passengerPriceDetail = passenger.passengerPriceDetail;
                //}
                //if (user.inputcode == "4")
                //{
                //    requestPassengerDetail.passengerFullDetail = passenger.passengerFullDetail;
                //}

                if (user.inputcode == "2")
                {
                    MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                    Hashtable HT = new Hashtable();
                    HT.Add("flag", "apidata");
                    HT.Add("MemberId", user.MemberId);
                    HT.Add("BusDetailId", user.BusDetailId);
                    DataTable dt = new DataTable();
                    dt = commonHelpers.GetDataFromStoredProcedure("sp_BusDetail", HT);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        var passengerTypeDetail = !string.IsNullOrEmpty(row["Res_Khalti_Output"].ToString()) ? row["Res_Khalti_Output"].ToString() : "";
                        var Seat = !string.IsNullOrEmpty(row["Seat"].ToString()) ? row["Seat"].ToString() : "";

                        if (!string.IsNullOrEmpty(Convert.ToString(passengerTypeDetail)))
                        {
                            var Responsedata = JsonConvert.DeserializeObject<TripResponse>(Convert.ToString(passengerTypeDetail));
                            if (Responsedata.status == 1)
                            {
                                var serialize = JsonConvert.SerializeObject(Responsedata.trips);
                                List<TripAPiData> Tripdata = JsonConvert.DeserializeObject<List<TripAPiData>>(Convert.ToString(serialize));
                                var item = Tripdata.Where(i => i.id == requestPassengerDetail.id).ToList();
                                var passengerdetail = item.FirstOrDefault().passengerDetail;
                                List<Passengerdetail> data = JsonConvert.DeserializeObject<List<Passengerdetail>>(Convert.ToString(passengerdetail));

                                Passengertypedetail obj = new Passengertypedetail();

                                foreach (var items in data)
                                {
                                    string names = "name";
                                    bool containsSearchTerm_names = items.detail.ToLower().Contains(names);
                                    string age = "age";
                                    bool containsSearchTerm_age = items.detail.ToLower().Contains(age);
                                    string gender = "gender";
                                    bool containsSearchTerm_gender = items.detail.ToLower().Contains(gender);
                                    string licence = "licence";
                                    bool containsSearchTerm_licence = items.detail.ToLower().Contains(licence);
                                    string nationality = "national";
                                    bool containsSearchTerm_nationality = items.detail.ToLower().Contains(nationality);
                                    string fathername = "father";
                                    bool containsSearchTerm_fathername = items.detail.ToLower().Contains(fathername);
                                    string citizen = "citizen";
                                    bool containsSearchTerm_citizen = items.detail.ToLower().Contains(citizen);
                                    string residence = "residence";
                                    bool containsSearchTerm_residence = items.detail.ToLower().Contains(residence);
                                    string mobile = "mob";
                                    bool containsSearchTerm_mobile = items.detail.ToLower().Contains(mobile);
                                    string contact = "contact";
                                    bool containsSearchTerm_contact = items.detail.ToLower().Contains(mobile);
                                    string Number = "number";
                                    bool containsSearchTerm_number = items.detail.ToLower().Contains(mobile);
                                    string email = "email";
                                    bool containsSearchTerm_email = items.detail.ToLower().Contains(email);

                                    if (containsSearchTerm_names == true)
                                    {
                                        items.detail = requestPassengerDetail.name;
                                    }
                                    else if (containsSearchTerm_age == true)
                                    {
                                        items.detail = "1";
                                    }
                                    else if (containsSearchTerm_gender == true)
                                    {
                                        items.detail = "test";
                                    }
                                    else if (containsSearchTerm_licence == true)
                                    {
                                        items.detail = "test";
                                    }
                                    else if (containsSearchTerm_nationality == true)
                                    {
                                        items.detail = "Nepali";
                                    }
                                    else if (containsSearchTerm_fathername == true)
                                    {
                                        items.detail = "test";
                                    }
                                    else if (containsSearchTerm_citizen == true)
                                    {
                                        items.detail = "test";
                                    }
                                    else if (containsSearchTerm_residence == true)
                                    {
                                        items.detail = "test";
                                    }
                                    else if (containsSearchTerm_mobile == true || containsSearchTerm_contact == true || containsSearchTerm_number == true)
                                    {
                                        items.detail = requestPassengerDetail.contactNumber;
                                    }
                                    else if (containsSearchTerm_email == true)
                                    {
                                        items.detail = requestPassengerDetail.email;
                                    }
                                }

                                obj.passengerDetail = data;
                                object obj1 = Seat;
                                string[] dataArraySeat = JsonConvert.DeserializeObject<string[]>(Seat);

                                foreach (string seat in dataArraySeat)
                                {
                                    var passengerRoot = new Passengertypedetail
                                    {
                                        seat = seat,
                                        passengerDetail = obj.passengerDetail
                                    };
                                    obj2.passengerTypeDetail.Add(passengerRoot); //Add(passengerRoot);
                                }
                                //PassengerRootobject deserializedList = JsonConvert.DeserializeObject<List<Passengertypedetail>>(Convert.ToString( obj2.passengerTypeDetail)).ToList();
                                var jsonData = new jsonPassenger2
                                {
                                    id = requestPassengerDetail.id,
                                    email = requestPassengerDetail.email,
                                    contactNumber = requestPassengerDetail.contactNumber,
                                    boardingPoint = requestPassengerDetail.boardingPoint,
                                    ticketSrlNo = requestPassengerDetail.ticketSrlNo,
                                    passengerTypeDetail = obj2.passengerTypeDetail,
                                };
                                //Data = JsonConvert.SerializeObject(jsonData);
                                jsondata2 = JsonConvert.SerializeObject(jsonData);
                            }
                            //requestPassengerDetail.passengerTypeDetail = obj2.passengerTypeDetail;
                        }
                    }
                }
            }
            commonresponsedata result = new commonresponsedata();
            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {
                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        //    return response;
                        //}

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        GetDataFromBusSewa bookseatdetails = RepKhalti.Get_BusSewa_PASSENGERINFO_RequestService(user.Reference, requestPassengerDetail.id, requestPassengerDetail.ticketSrlNo, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa), requestPassengerDetail.name, requestPassengerDetail.contactNumber, requestPassengerDetail.email, requestPassengerDetail.boardingPoint, user.inputcode, requestPassengerDetail.passengerTypeDetail, requestPassengerDetail.passengerPriceDetail, requestPassengerDetail.passengerFullDetail, jsondata2);
                        if (bookseatdetails.IsException == false)
                        {
                            PassengerConfirmation obj = JsonConvert.DeserializeObject<PassengerConfirmation>(bookseatdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.status == 1)
                                {
                                    var xmlPassengerdetail = string.Empty;
                                    //if (user.inputcode == "2")
                                    //{
                                    //    if (!string.IsNullOrEmpty(Convert.ToString(requestPassengerDetail.passengerTypeDetail)))
                                    //    {
                                    //        List<passengertype> passengertype = JsonConvert.DeserializeObject<List<passengertype>>(requestPassengerDetail.passengerTypeDetail.ToString());
                                    //        xmlPassengerdetail = XMLForPayType(passengertype);

                                    //    }
                                    //}
                                    //else if (user.inputcode == "3")
                                    //{
                                    //    if (!string.IsNullOrEmpty(Convert.ToString(requestPassengerDetail.passengerPriceDetail)))
                                    //    {
                                    //        List<passengertype3> passengertype = JsonConvert.DeserializeObject<List<passengertype3>>(requestPassengerDetail.passengerPriceDetail.ToString());
                                    //        xmlPassengerdetail = XMLForPayPriceDetailType(passengertype);

                                    //    }
                                    //}
                                    //else if (user.inputcode == "4")
                                    //{
                                    //    if (!string.IsNullOrEmpty(Convert.ToString(reque-stPassengerDetail.passengerFullDetail)))
                                    //    {
                                    //        List<passengertype4> passengertype = JsonConvert.DeserializeObject<List<passengertype4>>(requestPassengerDetail.passengerFullDetail.ToString());
                                    //        xmlPassengerdetail = XMLForFullPassengerType(passengertype);

                                    //    }
                                    //}
                                    BusSewaModel _bus = new BusSewaModel();
                                    CommonDBResonse addbusdetail = _bus.AddPassengerInfo(user.inputcode, requestPassengerDetail.name, requestPassengerDetail.email, requestPassengerDetail.contactNumber, requestPassengerDetail.id, user.BusDetailId, requestPassengerDetail.boardingPoint);
                                    if (addbusdetail.code == "1")
                                    {
                                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                                        return response;
                                    }
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = obj.message;
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Failed";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                                }
                            }
                            else
                            {
                                //cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                result.status = false;
                                result.ReponseCode = 0;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                result.Data = obj;
                                result.StatusCode = response.StatusCode.ToString();
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                            }
                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} bus-passengerInfo {bookseatdetails.message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(bookseatdetails.message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }
                        if (!string.IsNullOrEmpty(bookseatdetails.Id))
                        {
                            var jsondata = Res_output;
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ", Res_Output='" + jsondata + "' where Id=" + bookseatdetails.Id + "");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} bus-passengerInfo completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} bus-passengerInfo {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/bus-payment-confirmation")]
        public HttpResponseMessage GetLookupService_BusSewaPaymentConfirmation(Req_Vendor_API_Bussewa_Lookup_RequestsPaymentConfirm user)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info($"{System.DateTime.Now.ToString()} inside bus-payment-confirmation" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            commonresponsedata result = new commonresponsedata();
            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.Amount, true, user.Mpin, "", false);
                        if (CommonResult.ToLower() != "success")
                        {
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            return response;
                        }

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = user.ticketSrlNo;
                        //RepCRUD<AddLog, GetLog>.Insert(log, "logs");
                        //var jsondata = JsonConvert.SerializeObject(user.seat);

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        bool IsCouponUnlocked = false;
                        string TransactionID = string.Empty;
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                        string API_KEY = string.Empty;
                        string UniqueTransactionId = string.Empty;
                        string RedirectURL = string.Empty;
                        string ContactNo = string.Empty;
                        string UserName = string.Empty;
                        string OrderToken = string.Empty;
                        string Password = string.Empty;
                        string UniqueMerchantId = string.Empty;
                        Hashtable HT = new Hashtable();
                        HT.Add("flag", "merdet");
                        DataTable dt1 = new DataTable();
                        dt1 = commonHelpers.GetDataFromStoredProcedure("sp_BusDetail", HT);
                        if (dt1.Rows.Count > 0)
                        {
                            DataRow row = dt1.Rows[0];
                            UserName = !string.IsNullOrEmpty(row["API_User"].ToString()) ? row["API_User"].ToString() : "";
                            Password = !string.IsNullOrEmpty(row["API_Password"].ToString()) ? row["API_Password"].ToString() : "";
                            ContactNo = !string.IsNullOrEmpty(row["ContactNo"].ToString()) ? row["ContactNo"].ToString() : "";
                            UniqueMerchantId = !string.IsNullOrEmpty(row["MerchantUniqueId"].ToString()) ? row["MerchantUniqueId"].ToString() : "";
                            API_KEY = !string.IsNullOrEmpty(row["apikey"].ToString()) ? row["apikey"].ToString() : "";
                        }
                        GetDataFromBusSewa bookseatdetails = RepKhalti.Get_BusSewa_PAYMENTCONFIRMATION_RequestService("", user.Reference, user.Id, user.ticketSrlNo, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa),
                           resGetCouponsScratched, ref TransactionID, user.BankTransactionId, user.PaymentMode,
                           user.UniqueCustomerId, user.Amount, resuserdetails
                           , API_KEY, UniqueMerchantId, UserName, Password, RedirectURL, OrderToken, UniqueTransactionId);
                        var busNo = string.Empty;
                        string operatorcontactInfo = null;
                        if (bookseatdetails.IsException == false)
                        {
                            paymentConfirmation obj = JsonConvert.DeserializeObject<paymentConfirmation>(bookseatdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.status == 1)
                                {

                                    busNo = obj.BusNo;
                                    operatorcontactInfo = JsonConvert.SerializeObject(obj.contactInfo);
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;

                                    result.TransactionUniqueId = bookseatdetails.TransactionId;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);
                                    Rep_State = "Success";

                                    Rep_status = "1";
                                }
                                else if (obj.status == 2)
                                {
                                    result.Message = obj.message;
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                                    Rep_State = "Failed";

                                }
                            }
                            else
                            {
                                //cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                result.status = false;
                                result.ReponseCode = 0;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                result.Data = obj;
                                result.StatusCode = response.StatusCode.ToString();
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);


                            }

                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} bus-payment-confirmation {bookseatdetails.message.ToString()} " + Environment.NewLine);
                            result.status = false;
                            result.ReponseCode = 0;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            result.Data = bookseatdetails.message.ToString();
                            result.StatusCode = response.StatusCode.ToString();
                            Res_output = JsonConvert.SerializeObject(result);
                            Rep_State = "Failed";
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                        }

                        BusSewaModel _bus = new BusSewaModel();
                        CommonDBResonse addbusdetail = _bus.update(operatorcontactInfo, user.Id, user.Amount, "", user.ticketSrlNo, "", "", user.BusDetailId, "", "", "PCR", Rep_State, bookseatdetails.TransactionId, busNo);

                        if (addbusdetail.code == "1")
                        {
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
                            result.status = false;
                            result.ReponseCode = 0;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                        }




                        if (!string.IsNullOrEmpty(bookseatdetails.Id))
                        {
                            var jsondata = Res_output;

                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "' where Id=" + bookseatdetails.Id + "");

                        }
                        if (Rep_status == "1")  //---Receipt for success case only--//
                        {


                            Hashtable HTs = new Hashtable();
                            HTs.Add("flag", "get");
                            HTs.Add("Id", bookseatdetails.TransactionId);
                            DataTable dt = new DataTable();
                            dt = commonHelpers.GetDataFromStoredProcedure("sp_BusDetail", HTs);
                            Receipt receipt = new Receipt();
                            if (dt.Rows.Count > 0)
                            {
                                DataRow row = dt.Rows[0];
                                receipt.from = !string.IsNullOrEmpty(row["TripFrom"].ToString()) ? row["TripFrom"].ToString() : "";
                                receipt.to = !string.IsNullOrEmpty(row["TripTo"].ToString()) ? row["TripTo"].ToString() : "";
                                receipt.ticketSrlNo = !string.IsNullOrEmpty(row["TicketSerialNo"].ToString()) ? row["TicketSerialNo"].ToString() : "";
                                receipt.seat = !string.IsNullOrEmpty(row["Seat"].ToString()) ? row["Seat"].ToString() : "";
                                //receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? row["DepartureDate"].ToString() : "";
                                receipt.Time = !string.IsNullOrEmpty(row["DepartureTime"].ToString()) ? row["DepartureTime"].ToString() : "";

                                //receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? Convert.ToDateTime(row["DepartureDate"].ToString()).ToString("yyyy-MM-dd") + ' ' + row["DepartureTime"].ToString() : "";
                                receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? Convert.ToDateTime(Convert.ToDateTime(row["DepartureDate"].ToString()).ToString("yyyy-MM-dd") + ' ' + row["DepartureTime"].ToString()).ToString("yyyy-MM-dd HH:mm") : "";

                                receipt.Operator = !string.IsNullOrEmpty(row["Operator"].ToString()) ? row["Operator"].ToString() : "";
                                receipt.BusType = !string.IsNullOrEmpty(row["BusType"].ToString()) ? row["BusType"].ToString() : "";
                                receipt.BusNo = !string.IsNullOrEmpty(row["BusNo"].ToString()) ? row["BusNo"].ToString() : "";
                                receipt.PaymentStatus = !string.IsNullOrEmpty(row["PaymentStatus"].ToString()) ? row["PaymentStatus"].ToString() : "";
                                receipt.Platform = !string.IsNullOrEmpty(row["Platform"].ToString()) ? row["Platform"].ToString() : "";
                                receipt.Amount = !string.IsNullOrEmpty(row["Amount"].ToString()) ? row["Amount"].ToString() : "0.00";
                                receipt.email = !string.IsNullOrEmpty(row["email"].ToString()) ? row["email"].ToString() : "";
                                receipt.name = !string.IsNullOrEmpty(row["PassengerName"].ToString()) ? row["PassengerName"].ToString() : "";
                                receipt.contactNumber = !string.IsNullOrEmpty(row["ContactNumber"].ToString()) ? row["ContactNumber"].ToString() : "";
                                receipt.contactInfo = !string.IsNullOrEmpty(row["ContactNumber"].ToString()) ? row["ContactNumber"].ToString() : "";
                                receipt.boardingPoint = !string.IsNullOrEmpty(row["BoardingPoint"].ToString()) ? row["BoardingPoint"].ToString() : "";
                                receipt.TransactionDate = !string.IsNullOrEmpty(row["TransactionDate"].ToString()) ? row["TransactionDate"].ToString() : "";
                                receipt.ServiceCharge = !string.IsNullOrEmpty(row["ServiceCharge"].ToString()) ? row["ServiceCharge"].ToString() : "0.00";
                                receipt.Type = !string.IsNullOrEmpty(row["Type"].ToString()) ? row["Type"].ToString() : "";
                                receipt.FirstName = !string.IsNullOrEmpty(row["FirstName"].ToString()) ? row["FirstName"].ToString() : "";
                                receipt.LastName = !string.IsNullOrEmpty(row["LastName"].ToString()) ? row["LastName"].ToString() : "";
                                receipt.MiddleName = !string.IsNullOrEmpty(row["MiddleName"].ToString()) ? row["MiddleName"].ToString() : "";
                                receipt.userContact = !string.IsNullOrEmpty(row["userContact"].ToString()) ? row["userContact"].ToString() : "";
                            }


                            object obj1 = receipt.seat;
                            string[] dataArraySeat = JsonConvert.DeserializeObject<string[]>(receipt.seat);

                            string commaSeparatedDataSeat = string.Join(", ", dataArraySeat);
                            string[] dataArrayContactInfo = JsonConvert.DeserializeObject<string[]>(operatorcontactInfo);

                            string commaSeparatedDataContact = string.Join(", ", dataArrayContactInfo);

                            var list = new List<KeyValuePair<String, String>>();
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Date", receipt.TransactionDate);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Service", "Bus Sewa");
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "MyPay Txn Id", bookseatdetails.TransactionId);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Ticket SerialNo", receipt.ticketSrlNo);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "From", receipt.from);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "To", receipt.to);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Seat No", commaSeparatedDataSeat);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Passenger Name", receipt.name);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Contact Number", receipt.contactNumber);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Email", receipt.email);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Operator", receipt.Operator);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Bus No", receipt.BusNo);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Departure Date", receipt.date);
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Departure Time", receipt.Time);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Status", receipt.PaymentStatus);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Service Charge(RED)", receipt.ServiceCharge);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Paid(RED)", receipt.Amount);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Operator Contact", commaSeparatedDataContact);
                            string JSONForReceipt = VendorApi_CommonHelper.getJSONfromList(list);
                            VendorApi_CommonHelper.saveReceipt(receipt.Type.ToString(),
                                "Bus Sewa", user.MemberId, bookseatdetails.TransactionId, JSONForReceipt,
                                receipt.userContact, receipt.FirstName + " " + receipt.MiddleName + " " + receipt.LastName,
                                "Ticketing", receipt.ticketSrlNo, receipt.Amount.ToString());
                        }

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} bus-payment-confirmation completed" + Environment.NewLine);
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    log.Info("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" +
                        eve.Entry.Entity.GetType().Name + " STATE:" + eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        log.Info("- Property: \"{0}\", Error: \"{1}\"" +
                            ve.PropertyName + " ERROR MSG: " + ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} bus-payment-confirmation {ex.ToString()} " + Environment.NewLine);
                log.Info($"{System.DateTime.Now.ToString()} bus-payment-confirmation {ex.ToString()} " + Environment.NewLine);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/bus-ticket-query")]
        public HttpResponseMessage GetLookupService_BusSewaTicketQuery(Req_Vendor_API_Bussewa_Lookup_RequestsCancelQueue user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside bus-ticket-query" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            commonresponsedata result = new commonresponsedata();
            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {
                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        //    return response;
                        //}

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        GetDataFromBusSewa bookseatdetails = RepKhalti.Get_BusSewa_TicketQuery_RequestService(user.Reference, user.Id, user.ticketSrlNo, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa));
                        if (bookseatdetails.IsException == false)
                        {
                            TicketQuery obj = JsonConvert.DeserializeObject<TicketQuery>(bookseatdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.status == 1)
                                {
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = obj.message;
                                    result.status = obj.status.ToString() == "1" ? true : false;
                                    result.ReponseCode = result.status ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = obj;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Failed";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                                }
                            }
                            else
                            {
                                //cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                result.status = obj.status.ToString() == "1" ? true : false;
                                result.ReponseCode = result.status ? 1 : 0;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                result.Data = obj;
                                result.StatusCode = response.StatusCode.ToString();
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                            }
                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} bus-ticket-query {bookseatdetails.message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(bookseatdetails.message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }
                        if (!string.IsNullOrEmpty(bookseatdetails.Id))
                        {
                            var jsondata = Res_output;
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ", Res_Output='" + jsondata + "' where Id=" + bookseatdetails.Id + "");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} bus-ticket-query completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} bus-ticket-query {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpPost]
        //[Route("api/download-bus-ticket")]
        //public HttpResponseMessage GetDownloadTicket_Bus(Request_BusTicket_Download user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside download-bus-ticket" + Environment.NewLine);
        //    string UserInput = getRawPostData().Result;
        //    commonresponsedata result = new commonresponsedata();

        //    var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //    try
        //    {
        //        if (Request.Headers.Authorization == null)
        //        {
        //            string results = "Un-Authorized Request";
        //            result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //            response.StatusCode = HttpStatusCode.BadRequest;
        //            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //            return response;
        //        }
        //        else
        //        {
        //            string md5hash = Common.getHashMD5(UserInput);
        //            string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
        //            if (results != "Success")
        //            {
        //                result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //                response.StatusCode = HttpStatusCode.BadRequest;
        //                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
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
        //                resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
        //                if (CommonResult.ToLower() != "success")
        //                {
        //                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                    return response;
        //                }

        //                Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                //var jsondata = JsonConvert.SerializeObject(user.seat);
        //                string authenticationToken = Request.Headers.Authorization.Parameter;
        //                Common.authenticationToken = authenticationToken;
        //                string Res_output = response.ToString();
        //                var Rep_State = string.Empty;
        //                var Rep_status = "0";
        //                BusTicket_Download bookseatdetails = RepKhalti.Get_BUSTICKET_Download(user.MemberId, user.Reference, user.LogID, user.Version, user.DeviceCode, user.PlatForm, false, resuserdetails.Email);
        //                //if (bookseatdetails.IsException == false)
        //                //{
        //                //    RootobjectBookSeat obj = JsonConvert.DeserializeObject<RootobjectBookSeat>(bookseatdetails.message.ToString());

        //                //    if (obj != null)
        //                //    {
        //                //        if (obj.status == 1)
        //                //        {
        //                //            BusSewaModel _bus = new BusSewaModel();
        //                //            CommonDBResonse addbusdetail = _bus.AddBusDetails(user.from, user.to, user.MemberId, user.PlatForm, user.DeviceId, requestdata.id, obj.ticketSrlNo, user.DepatureTime, user.Operator, user.DepatureDate, user.BusType, user.BusDetailId, JsonConvert.SerializeObject(obj.boardingPoints), JsonConvert.SerializeObject(requestdata.seat), "TR", "", "", "");
        //                //            if (addbusdetail.code == "1")
        //                //            {
        //                //                result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
        //                //                response.StatusCode = HttpStatusCode.BadRequest;
        //                //                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                //                return response;
        //                //            }
        //                //            result.BusdetailId = addbusdetail.Id;
        //                //            result.status = obj.status.ToString() == "1" ? true : false;
        //                //            result.ReponseCode = result.status ? 1 : 0;
        //                //            result.Message = "success";
        //                //            response.StatusCode = HttpStatusCode.Accepted;
        //                //            result.Data = obj;
        //                //            result.StatusCode = response.StatusCode.ToString();
        //                //            Res_output = JsonConvert.SerializeObject(result);
        //                //            Rep_State = "Success";
        //                //            Rep_status = "1";
        //                //            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

        //                //        }
        //                //        else
        //                //        {
        //                //            result.Message = obj.message;
        //                //            result.status = obj.status.ToString() == "1" ? true : false;
        //                //            result.ReponseCode = result.status ? 1 : 0;
        //                //            response.StatusCode = HttpStatusCode.BadRequest;
        //                //            result.Data = obj;
        //                //            result.StatusCode = response.StatusCode.ToString();
        //                //            Res_output = JsonConvert.SerializeObject(result);
        //                //            Rep_State = "Failed";
        //                //            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

        //                //        }
        //                //    }
        //                //    else
        //                //    {
        //                //        //cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                //        result.ReponseCode = 0;
        //                //        response.StatusCode = HttpStatusCode.BadRequest;
        //                //        result.Data = obj;
        //                //        result.StatusCode = response.StatusCode.ToString();
        //                //        Res_output = JsonConvert.SerializeObject(result);
        //                //        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    log.Error($"{System.DateTime.Now.ToString()} download-bus-ticket {bookseatdetails.message.ToString()} " + Environment.NewLine);
        //                //    result = CommonApiMethod.BusSewaReturnBadRequestMessage(bookseatdetails.message);
        //                //    response.StatusCode = HttpStatusCode.BadRequest;
        //                //    Res_output = JsonConvert.SerializeObject(result);
        //                //    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                //    Rep_State = "Failed";
        //                //}


        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} download-bus-ticket completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} download-bus-ticket {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

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

        public static string XMLForPayPriceDetailType(List<passengertype3> passengertypexml)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            DataTable dt = new DataTable("Temp");
            StringWriter sw = new StringWriter();
            dt.Columns.Add("PassengerName");
            dt.Columns.Add("PassengerSeat");
            dt.Columns.Add("Gender");
            dt.Columns.Add("PassengerType");
            DataRow dr;
            foreach (var item in passengertypexml)
            {
                dr = dt.NewRow();
                dr["PassengerName"] = item.name;
                dr["PassengerType"] = item.id;
                dr["PassengerSeat"] = item.seat;
                dt.Rows.Add(dr);
            }
            dt.WriteXml(sw);
            return sw.ToString();
        }
        public static string XMLForFullPassengerType(List<passengertype4> passengertypexml)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            DataTable dt = new DataTable("Temp");
            StringWriter sw = new StringWriter();
            dt.Columns.Add("PassengerName");
            dt.Columns.Add("PassengerSeat");
            dt.Columns.Add("FatherName");
            dt.Columns.Add("PassengerType");
            DataRow dr;

            foreach (var item in passengertypexml)
            {
                dr = dt.NewRow();
                List<passengername> ept = ser.Deserialize<List<passengername>>(Convert.ToString(item.passengerDetail));
                foreach (var items in ept)
                {
                    if (items.id == "73")
                    {
                        dr["PassengerName"] = items.detail;
                    }
                    if (items.id == "109")
                    {
                        dr["FatherName"] = items.detail;
                    }
                }

                dr["PassengerType"] = item.id;
                dr["PassengerSeat"] = item.seat;
                dt.Rows.Add(dr);
            }
            dt.WriteXml(sw);
            return sw.ToString();
        }
        public static string XMLForPayType(List<passengertype> passengertypexml)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            DataTable dt = new DataTable("Temp");
            StringWriter sw = new StringWriter();
            dt.Columns.Add("PassengerName");
            dt.Columns.Add("PassengerSeat");
            dt.Columns.Add("Gender");
            dt.Columns.Add("PassengerType");
            DataRow dr;

            // passengertype ept = ser.Deserialize<passengertype>(Convert.ToString(passengertypexml));

            foreach (var item in passengertypexml)
            {
                dr = dt.NewRow();
                List<passengername> ept = ser.Deserialize<List<passengername>>(Convert.ToString(item.passengerDetail));
                foreach (var items in ept)
                {
                    if (items.id == "59")
                    {
                        dr["PassengerName"] = items.detail;
                    }
                    if (items.id == "62")
                    {
                        dr["Gender"] = items.detail;
                    }
                }
                dr["PassengerSeat"] = item.seat;

                dt.Rows.Add(dr);
            }
            dt.WriteXml(sw);
            return sw.ToString();
        }


    }
}