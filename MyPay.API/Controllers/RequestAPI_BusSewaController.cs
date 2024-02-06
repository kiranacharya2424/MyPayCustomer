using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Antivirus.Bussewa;
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
    public class RequestAPI_BusSewaController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_BusSewaController));
        string ApiResponse = string.Empty;

        [HttpPost]
        [Route("api/search-bus-routes")]
        public HttpResponseMessage GetLookupService_BussewaRoutes(Req_Vendor_API_Bussewa_Routes_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside search-bus-routes" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Bussewa_Routes_Lookup_Requests result = new Res_Vendor_API_Bussewa_Routes_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Bussewa_Routes_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        GetVendor_API_Bussewa_Routes_Lookup objRes = new GetVendor_API_Bussewa_Routes_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_Bussewa_ROUTES_LOOKUP(user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.routes = objRes.routes;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Bussewa_Routes_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} search-bus-routes completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} search-bus-routes {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/search-bus")]
        public HttpResponseMessage GetLookupService_Bussewa(Req_Vendor_API_Bussewa_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside search-bus" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Bussewa_Lookup_Requests result = new Res_Vendor_API_Bussewa_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Bussewa_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        GetVendor_API_Bussewa_Lookup objRes = new GetVendor_API_Bussewa_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_Bussewa_LOOKUP(user.Reference, user.BoardingFrom, user.ArrivalTo, user.ShiftDayNight, user.Date, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<Buses> objBusesList = new List<Buses>();
                            for (int i = 0; i < objRes.buses.Count; i++)
                            {
                                Buses objBuses = new Buses();
                                objBuses.NoOfColumn = objRes.buses[i].no_of_column;
                                objBuses.LockStatus = objRes.buses[i].lock_status;
                                objBuses.DepartureTime = objRes.buses[i].departure_time;
                                objBuses.ImageList = objRes.buses[i].image_list;
                                objBuses.BusNo = objRes.buses[i].bus_no;
                                objBuses.Operator = objRes.buses[i].@operator;
                                objBuses.TicketPrice = objRes.buses[i].ticket_price;
                                objBuses.PassengerDetail = objRes.buses[i].passenger_detail;
                                objBuses.Amenities = objRes.buses[i].amenities;
                                objBuses.InputTypeCode = objRes.buses[i].input_type_code;
                                objBuses.Date = objRes.buses[i].date;
                                objBuses.Id = objRes.buses[i].id;
                                objBuses.BusType = objRes.buses[i].bus_type;
                                objBuses.MultiPrice = objRes.buses[i].multi_price;
                                objBuses.Rating = objRes.buses[i].rating;
                                objBuses.DateEn = objRes.buses[i].date_en;
                                List<BusSeatLayout> objBusSeatLayoutList = new List<BusSeatLayout>();
                                for (int k = 0; k < objRes.buses[i].seat_layout.Count; k++)
                                {
                                    BusSeatLayout objBusSeatLayout = new BusSeatLayout();
                                    objBusSeatLayout.BookingStatus = objRes.buses[i].seat_layout[k].bookingStatus;
                                    objBusSeatLayout.DisplayName = objRes.buses[i].seat_layout[k].displayName;
                                    objBusSeatLayoutList.Add(objBusSeatLayout);
                                }
                                objBuses.SeatLayout = objBusSeatLayoutList;
                            }
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.SessionId = objRes.session_id;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Bussewa_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} search-bus completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} search-bus {ex.ToString()} " + Environment.NewLine);
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