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
using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;
using MyPay.Models;
using commonresponsedata = MyPay.Models.VendorAPI.Get.BusSewaService.commonresponsedata;
using System.Web.Caching;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
using MyPay.API.Models.Airlines;
using System.Text.RegularExpressions;
using MyPay.API.Models.Request.TouristBus;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using MyPay.Models.VendorAPI.Get.TouristBus;
using Microsoft.Ajax.Utilities;
using MyPay.API.Models.Antivirus.Bussewa;
using static MyPay.Models.Add.AddUser;


namespace MyPay.API.Controllers
{
    public class TouristBus_RequestAPIController : ApiController
    {
        // GET: TouristBus_RequestAPI
        private static ILog log = LogManager.GetLogger(typeof(BusSewa_RequestAPIController));

        string ApiResponse = string.Empty;

        [HttpPost]
        [Route("api/tourist-bus-routes")]
        public HttpResponseMessage GetLookupService_BussewaRoutes(Req_Vendor_API_TouristBus_Routes_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside tourist-bus-routes" + Environment.NewLine);
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

                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
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
                        GetDataFromBusSewa getroutesdetail = RepKhalti.Get_TouristBus_ROUTES_RequestService(user.Reference, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.tourist_bus));
                        if (getroutesdetail.IsException == false)
                        {
                            GetVendor_API_TouristBus_Routes_Response obj = JsonConvert.DeserializeObject<GetVendor_API_TouristBus_Routes_Response>(getroutesdetail.message.ToString());
                            if (obj != null)
                            {
                                if (obj.code == "1")
                                {
                                   // RoutesDetail routeslist = JsonConvert.DeserializeObject<RoutesDetail>(obj.ds.ToString());
                                    result.status =  true ;
                                    result.ReponseCode =1 ;
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
                                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(Convert.ToString( obj.data));
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
                            log.Error($"{System.DateTime.Now.ToString()} tourist-bus-routes {getroutesdetail.message.ToString()} " + Environment.NewLine);
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
                log.Info($"{System.DateTime.Now.ToString()} tourist-bus-routes completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} tourist-bus-routes {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/tourist-bus-trip")]
        public HttpResponseMessage GetLookupService_TouristBusTrip(Req_Vendor_API_TouristBus_Lookup_RequestsTrip user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside tourist-bus-trip" + Environment.NewLine);
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
                        //    //result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
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
                        GetDataFromBusSewa tripdetails = RepKhalti.Get_TouristBus_TRIP_RequestService(user.Reference, user.from_location, user.to_location,  user.date, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.tourist_bus));

                        //------------------Check for exception--------------------------------------------//        
                        if (tripdetails.IsException == false)
                        {
                            Tourist_TripDetails_Response obj = JsonConvert.DeserializeObject<Tourist_TripDetails_Response>(tripdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.code == "1")
                                {
                                    result.status =  true ;
                                    result.ReponseCode =  1 ;
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
                                    result.Message =Convert.ToString( obj.data);
                                    result.status =  false;
                                    result.ReponseCode =  0;
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
                            log.Error($"{System.DateTime.Now.ToString()} tourist-bus-trip {tripdetails.message.ToString()} " + Environment.NewLine);
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
                log.Info($"{System.DateTime.Now.ToString()} tourist-bus-trip completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} tourist-bus-trip {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/tourist-bus-book-seat")]
        public HttpResponseMessage GetLookupService_BusSewaBookSeat(Req_Vendor_API_TouristBus_Requestsbookseat user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside tourist-bus-book-seat" + Environment.NewLine);
            BookSeat requestdata = new BookSeat();
            //if (!string.IsNullOrEmpty(Convert.ToString(user.seat)))
            //{
            //    var seat = JsonConvert.DeserializeObject<RequestBookSeats>(user.seat);
            //    requestdata.id = seat.id;
            //    requestdata.seat = seat.seat;
            //}
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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        if (CommonResult.ToLower() != "success")
                        {
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            return response;
                        }

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        //var jsondata = JsonConvert.SerializeObject(user.seat);
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        GetDataFromBusSewa bookseatdetails = RepKhalti.Get_TouristBus_BOOKSEAT_RequestService(user.Reference, user.busno, user.seat, user.totalseat,user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.tourist_bus));
                        if (bookseatdetails.IsException == false)
                        {
                            Tourist_TripDetails_Response obj = JsonConvert.DeserializeObject<Tourist_TripDetails_Response>(bookseatdetails.message.ToString());

                            if (obj != null)
                            {
                                if (obj.code == "1")
                                {
                                    GetVendor_API_TouristBus_Lookup_new _bus = new GetVendor_API_TouristBus_Lookup_new();
                                    CommonDBResonse addbusdetail = _bus.AddTouristBusDetails("i", user.MemberId,Convert.ToString( obj.data), user.from_location, user.to_location,user.date, user.time, user.staffnum, user.Cashback, user.Comission, user.seat,user.totalseat, "", user.busno, resuserdetails.ContactNumber, user.CompanyName, "", "", "");
                                    if (addbusdetail.code == "1")
                                    {
                                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                                        return response;
                                    }
                                    result.BusdetailId = addbusdetail.Id;
                                    result.status =  true ;
                                    result.ReponseCode =  1 ;
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
                                    result.Message =Convert.ToString( obj.data);
                                    result.status =  false;
                                    result.ReponseCode =  0;
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
                            log.Error($"{System.DateTime.Now.ToString()} tourist-bus-book-seat {bookseatdetails.message.ToString()} " + Environment.NewLine);
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
                log.Info($"{System.DateTime.Now.ToString()} tourist-bus-book-seat completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} tourist-bus-book-seat {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/tourist-bus-cancelholdingseat")]
        public HttpResponseMessage GetLookupService_CancelHoldingSeat(Req_Vendor_API_TouristBus_Lookup_RequestsCancel user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside tourist-bus-cancelholdingseat" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            commonresponsedata result = new commonresponsedata();
            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
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
                        //    //result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
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
                        GetDataFromBusSewa tripdetails = RepKhalti.Get_TouristBus_Cancel_RequestService(user.Reference, user.holdingnumber,  user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.tourist_bus));

                        //------------------Check for exception--------------------------------------------//        
                        if (tripdetails.IsException == false)
                        {
                            Tourist_TripDetails_Response obj = JsonConvert.DeserializeObject<Tourist_TripDetails_Response>(tripdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.code == "1")
                                {
                                    result.status = true;
                                    result.ReponseCode = 1;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    //result.BusdetailId = addbusdetail.Id;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);
                                    string Result = commonHelpers.GetScalarValueWithValue("update TouristBus_Detail set IsCancel=1  where Ticketno='" + user.holdingnumber + "'");

                                }
                                else
                                {
                                    result.Message = Convert.ToString(obj.data);
                                    result.status = false;
                                    result.ReponseCode = 0;
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
                            log.Error($"{System.DateTime.Now.ToString()} tourist-bus-cancelholdingseat {tripdetails.message.ToString()} " + Environment.NewLine);
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
                           
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "' where Id=" + tripdetails.Id + "");

                        }
                    }


                    //}
                }
                log.Info($"{System.DateTime.Now.ToString()} tourist-bus-cancelholdingseat completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} tourist-bus-trip {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/tourist-bus-passengerdetail")]
        public HttpResponseMessage GetLookupService_TouristBusPassengerdetail(Req_Vendor_API_TouristBus_Lookup_RequestsPassenger user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside tourist-bus-passengerdetail" + Environment.NewLine);
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
                        //    //result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        //    return response;
                        //}

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";

                        GetVendor_API_TouristBus_Lookup_new _bus = new GetVendor_API_TouristBus_Lookup_new();
                        CommonDBResonse addbusdetail = _bus.AddTouristBusDetails("u", user.MemberId, "", "", "", "","", "", "", "", "", "", user.pickup,"", user.BusDetailId, user.name, user.TicketNo, user.contact,user.drop);
                        if (addbusdetail.code == "1")
                        {
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            return response;
                        }
                        GetDataFromBusSewa tripdetails = RepKhalti.Get_TouristBus_PASSENGERDETAILS_RequestService(user.Reference, user.name, user.contact, user.pickup, user.drop, user.TicketNo, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.tourist_bus));

                        //------------------Check for exception--------------------------------------------//        
                        if (tripdetails.IsException == false)
                        {
                            Tourist_TripDetails_Response obj = JsonConvert.DeserializeObject<Tourist_TripDetails_Response>(tripdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.code == "1")
                                {
                                   // CommonDBResonse addbusdetail = _bus.AddPassengerInfo(user.inputcode, requestPassengerDetail.name, requestPassengerDetail.email, requestPassengerDetail.contactNumber, requestPassengerDetail.id, user.BusDetailId, requestPassengerDetail.boardingPoint);
                                    result.status = true;
                                    result.ReponseCode = 1;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    result.BusdetailId = user.BusDetailId;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = Convert.ToString(obj.data);
                                    result.status = false;
                                    result.ReponseCode = 0;
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
                            log.Error($"{System.DateTime.Now.ToString()} tourist-bus-passengerdetail {tripdetails.message.ToString()} " + Environment.NewLine);
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
                log.Info($"{System.DateTime.Now.ToString()} tourist-bus-passengerdetail completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} tourist-bus-passengerdetail {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/tourist-bus-passengerdetailget")]
        public HttpResponseMessage GetLookupService_TouristBusPassengerdetailGet(Req_Vendor_API_TouristBus_Lookup_RequestsPassenger user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside tourist-bus-passengerdetailget" + Environment.NewLine);
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
                        //    //result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        //    return response;
                        //}

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";

                        
                        GetDataFromBusSewa tripdetails = RepKhalti.Get_TouristBus_Detaillist_RequestService(user.Reference, user.TicketNo, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.tourist_bus));

                        //------------------Check for exception--------------------------------------------//        
                        if (tripdetails.IsException == false)
                        {
                            Tourist_TripDetails_Response obj = JsonConvert.DeserializeObject<Tourist_TripDetails_Response>(tripdetails.message.ToString());
                            if (obj != null)
                            {
                                if (obj.code == "1")
                                {
                                     result.status = true;
                                    result.ReponseCode = 1;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    result.BusdetailId = user.BusDetailId;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = Convert.ToString(obj.data);
                                    result.status = false;
                                    result.ReponseCode = 0;
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
                            log.Error($"{System.DateTime.Now.ToString()} tourist-bus-passengerdetailget {tripdetails.message.ToString()} " + Environment.NewLine);
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
                log.Info($"{System.DateTime.Now.ToString()} tourist-bus-passengerdetailget completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} tourist-bus-passengerdetailget {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPost]
        [Route("api/tourist-bus-paymentconfirm")]
        public HttpResponseMessage GetLookupService_TouristBusPaymentConfirm(Req_Vendor_API_TouristBus_Lookup_RequestsPayment user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside tourist-bus-paymentconfirm" + Environment.NewLine);
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

                        Int64 memId =Convert.ToInt64( user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true,user.amount, true, user.Mpin, "", false);
                        if (CommonResult.ToLower() != "success")
                        {
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            //result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                            return response;
                        }

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";

                        string API_KEY = string.Empty;
                        string UniqueTransactionId = string.Empty;
                        string RedirectURL = string.Empty;
                        string ContactNo = string.Empty;
                        string UserName = string.Empty;
                        string OrderToken = string.Empty;
                        string Password = string.Empty;
                        string TransactionID = string.Empty;
                        string UniqueMerchantId = string.Empty;
                        Hashtable HT = new Hashtable();
                        HT.Add("flag", "merdet");
                        DataTable dt1 = new DataTable();
                        MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                        dt1 = commonHelpers.GetDataFromStoredProcedure("sp_TouristBus_Detail", HT);
                        if (dt1.Rows.Count > 0)
                        {
                            DataRow row = dt1.Rows[0];
                            UserName = !string.IsNullOrEmpty(row["API_User"].ToString()) ? row["API_User"].ToString() : "";
                            Password = !string.IsNullOrEmpty(row["API_Password"].ToString()) ? row["API_Password"].ToString() : "";
                            ContactNo = !string.IsNullOrEmpty(row["ContactNo"].ToString()) ? row["ContactNo"].ToString() : "";
                            UniqueMerchantId = !string.IsNullOrEmpty(row["MerchantUniqueId"].ToString()) ? row["MerchantUniqueId"].ToString() : "";
                            API_KEY = !string.IsNullOrEmpty(row["apikey"].ToString()) ? row["apikey"].ToString() : "";
                        }

                        GetVendor_API_TouristBus_Lookup_new _bus = new GetVendor_API_TouristBus_Lookup_new();
                        CommonDBResonse addbusdetail = _bus.AddTouristBusDetails("ua", "", "", "", "", "", "", "", "", "", "", "", "", "", "", user.amount, user.commission, user.cashbackamount, user.BusDetailId);
                        if (addbusdetail.code == "1")
                        {
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(addbusdetail.Message);
                            result.status = false;
                            result.ReponseCode = 0;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                        }

                        GetDataFromBusSewa payment = RepKhalti.Get_TouristBus_Payment_RequestService("", user.Reference,user.commission, user.amount,user.cashbackamount, user.TicketNo, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.tourist_bus),
                               resGetCouponsScratched, ref TransactionID, user.BankTransactionId, user.PaymentMode,
                               user.UniqueCustomerId, user.amount, resuserdetails
                               , API_KEY, UniqueMerchantId, UserName, Password, RedirectURL, OrderToken, UniqueTransactionId);
                        // GetDataFromBusSewa tripdetails = RepKhalti.Get_TouristBus_Detaillist_RequestService(user.Reference, user.TicketNo, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa));

                        //------------------Check for exception--------------------------------------------//        
                        if (payment.IsException == false)
                        {
                            Tourist_TripDetails_Response obj = JsonConvert.DeserializeObject<Tourist_TripDetails_Response>(payment.message.ToString());
                            if (obj != null)
                            {
                                if (obj.code == "1")
                                {
                                    result.status = true;
                                    result.ReponseCode = 1;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = obj;
                                    result.TransactionUniqueId = payment.TransactionId;
                                    result.BusdetailId = user.BusDetailId;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    result.Message = Convert.ToString(obj.data);
                                    result.status = false;
                                    result.ReponseCode = 0;
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
                            log.Error($"{System.DateTime.Now.ToString()} tourist-bus-paymentconfirm {payment.message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(payment.message);
                            //cres = CommonApiMethod.ReturnBadRequestMessage(tripdetails.message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }

                        CommonDBResonse updatestatus = _bus.AddTouristBusDetails("us", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Rep_State, user.TicketNo, payment.TransactionId, user.BusDetailId);
                        if (updatestatus.code == "1")
                        {
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(updatestatus.Message);
                            result.status = false;
                            result.ReponseCode = 0;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

                        }
                        if (!string.IsNullOrEmpty(payment.Id))
                        {
                            var jsondata = Res_output;
                            
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "' where Id=" + payment.Id + "");

                        }


                        Hashtable HTs1 = new Hashtable();
                        HTs1.Add("flag", "get");
                        HTs1.Add("TransactionId", payment.TransactionId);
                        DataTable dt2 = new DataTable();
                        dt2 = commonHelpers.GetDataFromStoredProcedure("sp_TouristBus_Detail", HTs1);
                        Receipt receipt = new Receipt();
                        if (dt1.Rows.Count > 0)
                        {
                            DataRow row = dt2.Rows[0];
                            receipt.from = !string.IsNullOrEmpty(row["TripFrom"].ToString()) ? row["TripFrom"].ToString() : "";
                            receipt.to = !string.IsNullOrEmpty(row["TripTo"].ToString()) ? row["TripTo"].ToString() : "";
                            receipt.ticketSrlNo = !string.IsNullOrEmpty(row["TicketSerialNo"].ToString()) ? row["TicketSerialNo"].ToString() : "";
                            receipt.seat = !string.IsNullOrEmpty(row["Seat"].ToString()) ? row["Seat"].ToString() : "";
                            receipt.cashback = !string.IsNullOrEmpty(row["Cashback"].ToString()) ? row["Cashback"].ToString() : "0.00";
                            receipt.Time = !string.IsNullOrEmpty(row["DepartureTime"].ToString()) ? row["DepartureTime"].ToString() : "";

                            receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? Convert.ToDateTime(row["DepartureDate"].ToString()).ToString("yyyy-MM-dd") + ' ' + row["DepartureTime"].ToString() : "";
                            //receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? Convert.ToDateTime(Convert.ToDateTime(row["DepartureDate"].ToString()).ToString("yyyy-MM-dd") + ' ' + row["DepartureTime"].ToString()).ToString("yyyy-MM-dd") : "";
                            receipt.BusType = !string.IsNullOrEmpty(row["BusType"].ToString()) ? row["BusType"].ToString() : "";
                            receipt.BusNo = !string.IsNullOrEmpty(row["staffnum"].ToString()) ? row["staffnum"].ToString() : "";
                            receipt.PaymentStatus = !string.IsNullOrEmpty(row["PaymentStatus"].ToString()) ? row["PaymentStatus"].ToString() : "";
                            receipt.Platform = !string.IsNullOrEmpty(row["Platform"].ToString()) ? row["Platform"].ToString() : "";
                            receipt.Amount = !string.IsNullOrEmpty(row["Amount"].ToString()) ? row["Amount"].ToString() : "0.00";
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

                       

                        var list = new List<KeyValuePair<String, String>>();
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Date", receipt.TransactionDate);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Service", "Tourist Bus");
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "MyPay Txn Id", payment.TransactionId);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Ticket SerialNo", receipt.ticketSrlNo);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "From", receipt.from);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "To", receipt.to);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Seat No", receipt.seat);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Passenger Name", receipt.name);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Contact Number", receipt.contactNumber);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Staff Number", receipt.BusNo);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Departure Date", receipt.date);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Boarding Point", receipt.boardingPoint);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Status", receipt.PaymentStatus);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Service Charge(RED)", receipt.ServiceCharge);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Paid(RED)", receipt.Amount);
                        VendorApi_CommonHelper.addKeyValueToList(ref list, "Cashback(GREEN)", receipt.cashback);

                        string JSONForReceipt = VendorApi_CommonHelper.getJSONfromList(list);
                        VendorApi_CommonHelper.saveReceipt(receipt.Type.ToString(),
                            "Tourist Bus", user.MemberId, payment.TransactionId, JSONForReceipt,
                            receipt.userContact, receipt.FirstName + " " + receipt.MiddleName + " " + receipt.LastName,
                            "Ticketing", receipt.ticketSrlNo, receipt.Amount.ToString());
                    }


                    //}
                }
                log.Info($"{System.DateTime.Now.ToString()} tourist-bus-paymentconfirm completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} tourist-bus-paymentconfirm {ex.ToString()} " + Environment.NewLine);
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