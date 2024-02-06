using log4net;
using Microsoft.Ajax.Utilities;
using MyPay.API.Models;
using MyPay.API.Models.Airlines;
using MyPay.API.Models.PlasmaTech;
using MyPay.API.Models.Request.PlasmaTech;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
//using System.Web.Mvc;
using System.Windows;
using System.Windows.Interop;
using FlightInbound = MyPay.API.Models.Airlines.FlightInbound;
using FlightOutbound = MyPay.API.Models.Airlines.FlightOutbound;
using FligthSectors = MyPay.API.Models.Airlines.FligthSectors;

namespace MyPay.API.Controllers
{

        public class RequestAPI_AirlinesController : ApiController
    {

        string ApiResponse = string.Empty;

        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_AirlinesController));

        RequestAPI_PlasmaTechController _RequestAPI_PlasmaTechController = new RequestAPI_PlasmaTechController();
        [HttpPost]
        [Route("api/sectorsflight-lookup")]
        public HttpResponseMessage GetFlightSector(Req_Vendor_API_Airlines_Sector_Lookup_Requests user)
        {

            //var result = string.Empty;
            HttpResponseMessage response = new HttpResponseMessage();

            WebRes_FlightSector objResponse = new WebRes_FlightSector();
            //etVendor_API_Airlines_Sector_Lookup objRes = new GetVendor_API_Airlines_Sector_Lookup();
            var reqJSON = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/FlightSectors.json"));

            reqJSON.Replace("is_national", "IsNational");
            reqJSON.Replace("is_international", "IsInternational");
            reqJSON.Replace("is_active", "IsActive");
            reqJSON.Replace("name", "Name");
            reqJSON.Replace("code", "Code");

            JObject sectorListRaw = JObject.Parse(reqJSON); //new JObject(reqJSON.ToString());    


            //objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetVendor_API_Airlines_Sector_Lookup>(reqJSON);

            var sc = JsonConvert.DeserializeObject<List<FligthSectors>>(sectorListRaw["sectors"].ToString());
            var result = new Res_Vendor_API_Airlines_Sector_Lookup_Requests();
            result.FligthSectors = sc;

            //result.FligthSectors = objFligthSectorsList;
            result.ReponseCode = 1;//objRes.status ? 1 : 0;
            result.status = true;
            result.Message = "success";
            response.StatusCode = HttpStatusCode.Accepted;
            response = Request.CreateResponse<Res_Vendor_API_Airlines_Sector_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);

            return response;




            //int ServiceID = 0;
            //GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
            //inobject.IsActive = 1;
            //GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());

            //var userInput = getRawPostData().Result;
            //var request = Request;



            ////GetVendor_API_Airlines_Sector_Lookup
            //if (resSwitchType.FlightSwitchType == 1)
            //{
            //    ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
           // return GetLookupService_SectorsFlight_Lookup(user, request, userInput);
            //}
            //else if (resSwitchType.FlightSwitchType == 2)
            //{
            //    Req_Vendor_API_Airlines_Sector_Lookup_Requests obj = new Req_Vendor_API_Airlines_Sector_Lookup_Requests();
            //    obj.UserID = user.UserID;
            //    obj.Version = user.Version;
            //    obj.DeviceId = user.DeviceId;
            //    obj.DeviceCode = user.DeviceCode;
            //    obj.MemberId = user.MemberId;
            //    obj.SecretKey = user.SecretKey;
            //    obj.PlatForm = user.PlatForm;
            //    obj.Reference = user.Reference;
            //    obj.CouponCode = user.CouponCode;
            //    obj.BankTransactionId = user.BankTransactionId;
            //    obj.Hash = user.Hash;
            //    ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay;

            //    return _RequestAPI_PlasmaTechController.GetLookupServicePlasmaTechSector(obj, request, userInput);
            //}
            //return null;
        }
        public HttpResponseMessage GetLookupService_SectorsFlight_Lookup(Req_Vendor_API_Airlines_Sector_Lookup_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside sectorsflight-lookup" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_Sector_Lookup_Requests result = new Res_Vendor_API_Airlines_Sector_Lookup_Requests();
            //var response = Request.CreateResponse<Res_Vendor_API_Airlines_Sector_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            HttpResponseMessage response;

            HttpRequestMessage request;

            if (requestPassed != null)
            {
                request = requestPassed;
            }
            else
            {
                request = Request;
            }
            response = request.CreateResponse<Res_Vendor_API_Airlines_Sector_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            string userInput;

            if (userInputPassed != null)
            {
                userInput = userInputPassed;
            }
            else
            {
                userInput = getRawPostData().Result;
            }
            // var userInput = getRawPostData().Result;

            try
            {
                if (request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                        response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_Airlines_Sector_Lookup objRes = new GetVendor_API_Airlines_Sector_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_SECTOR_LOOKUP(user.MemberId, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<FligthSectors> objFligthSectorsList = new List<FligthSectors>();
                            for (int i = 0; i < objRes.sectors.Count; i++)
                            {
                                FligthSectors objFligthSectors = new FligthSectors();
                                objFligthSectors.Name = objRes.sectors[i].name;
                                objFligthSectors.Code = objRes.sectors[i].code;
                                objFligthSectors.IsNational = objRes.sectors[i].is_national;
                                objFligthSectors.IsInternational = objRes.sectors[i].is_international;
                                objFligthSectors.IsActive = objRes.sectors[i].is_active;

                                objFligthSectorsList.Add(objFligthSectors);
                            }
                            result.FligthSectors = objFligthSectorsList;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = request.CreateResponse<Res_Vendor_API_Airlines_Sector_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} sectorsflight-lookup completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} sectorsflight-lookup {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} sectorsflight-lookup {ex.ToString()} " + Environment.NewLine);
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/lookup-airlines")]
        public HttpResponseMessage GetFlightAirlines(Req_Vendor_API_Airlines_Lookup_Requests user)
        {
            int ServiceID = 0;
            var userInput = getRawPostData().Result;
            var request = Request;
            GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
            inobject.IsActive = 1;
            GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());

            if (resSwitchType.FlightSwitchType == 1)
            {
                ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                return GetLookupService_Airlines(user, request, userInput);
            }
            else if (resSwitchType.FlightSwitchType == 2)
            {

                if (user.Nationality == null || user.Nationality == "")
                {
                    user.Nationality = "NP";
                }
                else
                {
                    user.Nationality = user.Nationality;
                }

                user.UserID = Environment.GetEnvironmentVariable("PlasmaTech_UserName", EnvironmentVariableTarget.Machine);
                user.Password = Environment.GetEnvironmentVariable("PlasmaTech_Password", EnvironmentVariableTarget.Machine);
                user.AgencyId = Environment.GetEnvironmentVariable("PlasmaTech_AgencyId", EnvironmentVariableTarget.Machine);

                ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay;

                Req_Vendor_API_PlasmaTech_Flight_Available_Requests obj = new Req_Vendor_API_PlasmaTech_Flight_Available_Requests();

                obj = JsonConvert.DeserializeObject<Req_Vendor_API_PlasmaTech_Flight_Available_Requests>(JsonConvert.SerializeObject(user));
                obj.SectorFrom = user.FromDeparture;
                obj.SectorTo = user.ToArrival;
                obj.UserID = user.UserID;
                obj.Password = user.Password;
                obj.AgencyId = user.AgencyId;
                obj.Nationality = user.Nationality;
                obj.Adult = user.Adult;
                obj.Child = user.Child;
                obj.Version = user.Version;
                obj.DeviceId = user.DeviceId;
                obj.DeviceCode = user.DeviceCode;
                obj.MemberId = user.MemberId;
                obj.SecretKey = user.SecretKey;
                obj.PlatForm = user.PlatForm;
                DateTime date = DateTime.ParseExact(user.FlightDate, "yyyy-MM-dd", null);
                string Flight_Date = date.ToString("dd-MMM-yyyy").ToUpper();
                obj.FlightDate = Flight_Date;
                obj.TripType = user.TripType;

                if (obj.TripType == "R")
                {
                    date = DateTime.ParseExact(obj.ReturnDate, "yyyy-MM-dd", null);
                    var Return_Date = date.ToString("dd-MMM-yyyy").ToUpper();
                    obj.ReturnDate = Return_Date;
                }
                return _RequestAPI_PlasmaTechController.GetServiceFlightAvailable(obj, request, userInput);
            }
            return null;
        }
        public HttpResponseMessage GetLookupService_Airlines(Req_Vendor_API_Airlines_Lookup_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-airlines" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_Lookup_Requests result = new Res_Vendor_API_Airlines_Lookup_Requests();
            //var response = Request.CreateResponse<Res_Vendor_API_Airlines_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            //var userInput = getRawPostData().Result;
            HttpResponseMessage response;

            HttpRequestMessage request;

            if (requestPassed != null)
            {
                request = requestPassed;
            }
            else
            {
                request = Request;
            }
            response = request.CreateResponse<Res_Vendor_API_Airlines_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            string userInput;

            if (userInputPassed != null)
            {
                userInput = userInputPassed;
            }
            else
            {
                userInput = getRawPostData().Result;
            }

            try
            {
                if (request.Headers.Authorization == null)
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
                        response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_Airlines_Lookup objRes = new GetVendor_API_Airlines_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_LOOKUP(user.MemberId, user.Reference, user.FlightType, user.TripType, user.FlightDate, user.ReturnDate, user.Adult, user.Child, user.FromDeparture, user.ToArrival, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            // ******************  IsWalletBalanceSufficient  ****************************
                            AddUserLoginWithPin outobject = new AddUserLoginWithPin();
                            GetUserLoginWithPin inobject = new GetUserLoginWithPin();
                            inobject.MemberId = Convert.ToInt64(user.MemberId);
                            AddUserLoginWithPin resGetRecord = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject, outobject);
                            decimal WalletBalance = 0;
                            if (resGetRecord != null && resGetRecord.Id > 0)
                            {
                                WalletBalance = resGetRecord.TotalAmount;
                            }
                            string authenticationToken = Request.Headers.Authorization.Parameter;
                            Common.authenticationToken = authenticationToken;
                            List<FlightInbound> FlightInbound = new List<FlightInbound>();
                            List<FlightOutbound> FlightOutbound = new List<FlightOutbound>();
                            List<AddFlightBookingDetails> resFlightDbInsertList_Inbound = new List<AddFlightBookingDetails>();
                            List<AddFlightBookingDetails> resFlightDbInsertList_Outbound = new List<AddFlightBookingDetails>();
                            for (int i = 0; i < objRes.inbound.Count; i++)
                            {
                                FlightInbound objFlightInbound = new FlightInbound();
                                objFlightInbound.Aircrafttype = objRes.inbound[i].aircraft_type;
                                objFlightInbound.Airlinename = objRes.inbound[i].airline_name;
                                objFlightInbound.Departure = objRes.inbound[i].departure;
                                objFlightInbound.Refundable = objRes.inbound[i].refundable;
                                objFlightInbound.Infantfare = objRes.inbound[i].infant_fare;
                                objFlightInbound.Flightclasscode = objRes.inbound[i].flight_class_code;
                                objFlightInbound.Currency = objRes.inbound[i].currency;
                                objFlightInbound.Faretotal = objRes.inbound[i].fare_total;
                                objFlightInbound.Childfare = objRes.inbound[i].child_fare;
                                objFlightInbound.Child = objRes.inbound[i].child;
                                objFlightInbound.Departuretime = objRes.inbound[i].departure_time;
                                objFlightInbound.Tax = objRes.inbound[i].tax;
                                objFlightInbound.Airline = objRes.inbound[i].airline;
                                objFlightInbound.Adult = objRes.inbound[i].adult;
                                objFlightInbound.Adultfare = objRes.inbound[i].adult_fare;
                                objFlightInbound.Airlinelogo = objRes.inbound[i].airline_logo;
                                objFlightInbound.Flightno = objRes.inbound[i].flight_no;
                                objFlightInbound.Fuelsurcharge = objRes.inbound[i].fuel_surcharge;
                                objFlightInbound.Arrivaltime = objRes.inbound[i].arrival_time;
                                objFlightInbound.Resfare = objRes.inbound[i].res_fare;
                                objFlightInbound.Freebaggage = objRes.inbound[i].free_baggage;
                                objFlightInbound.Arrival = objRes.inbound[i].arrival;
                                objFlightInbound.Flightid = objRes.inbound[i].flight_id + "`" + objRes.inbound[i].fare_total;
                                objFlightInbound.Flightdate = objRes.inbound[i].flight_date;
                                objFlightInbound.Infant = objRes.inbound[i].infant;
                                objFlightInbound.IsWalletBalanceSufficient = (Convert.ToDecimal(objRes.inbound[i].fare_total) < WalletBalance) ? true : false;
                                string ServiceId = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines);
                                AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId, objFlightInbound.Faretotal, ServiceId);
                                objFlightInbound.Cashback = Convert.ToString(objOut.CashbackAmount);
                                objFlightInbound.RewardPoints = Convert.ToString(objOut.RewardPoints);
                                objFlightInbound.ServiceCharge = Convert.ToString(objOut.ServiceCharge);
                                FlightInbound.Add(objFlightInbound);

                                AddFlightBookingDetails res = new AddFlightBookingDetails();
                                res.BookingId = Convert.ToInt64(objRes.booking_id);
                                res.MemberId = Convert.ToInt64(user.MemberId);
                                res.TripType = user.TripType;
                                res.FlightType = user.FlightType;
                                res.Adult = Convert.ToInt32(objFlightInbound.Adult);
                                res.Child = Convert.ToInt32(objFlightInbound.Child);
                                res.IsInbound = true;
                                res.Aircrafttype = objFlightInbound.Aircrafttype;
                                res.Airlinename = objFlightInbound.Airlinename;
                                res.Departure = objFlightInbound.Departure;
                                res.Refundable = objFlightInbound.Refundable;
                                res.Infantfare = Convert.ToDecimal(objFlightInbound.Infantfare);
                                res.Flightclasscode = objFlightInbound.Flightclasscode;
                                res.Currency = objFlightInbound.Currency;
                                res.Faretotal = Convert.ToDecimal(objFlightInbound.Faretotal);
                                res.Adultfare = Convert.ToDecimal(objFlightInbound.Adultfare);
                                res.Childfare = Convert.ToDecimal(objFlightInbound.Childfare);
                                res.Departuretime = objFlightInbound.Departuretime;
                                res.Tax = Convert.ToDecimal(objFlightInbound.Tax);
                                res.Airline = objFlightInbound.Airline;
                                res.Airlinelogo = objFlightInbound.Airlinelogo;
                                res.Flightno = objFlightInbound.Flightno;
                                res.Fuelsurcharge = Convert.ToDecimal(objFlightInbound.Fuelsurcharge);
                                res.Arrivaltime = objFlightInbound.Arrivaltime;
                                res.Resfare = Convert.ToDecimal(objFlightInbound.Resfare);
                                res.Freebaggage = objFlightInbound.Freebaggage;
                                res.Arrival = objFlightInbound.Arrival;
                                res.Flightid = objFlightInbound.Flightid;
                                res.Flightdate = Convert.ToDateTime(objFlightInbound.Flightdate);
                                res.CreatedDate = System.DateTime.UtcNow;
                                res.UpdatedDate = System.DateTime.UtcNow;
                                res.IsActive = true;
                                res.IsDeleted = false;
                                res.IsApprovedByAdmin = true;
                                res.CreatedBy = Common.GetCreatedById(authenticationToken);
                                res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                res.IsFlightIssued = false;
                                res.IpAddress = Common.GetUserIP();
                                res.SectorFrom = user.ToArrival;
                                res.SectorTo = user.FromDeparture;
                                res.ReturnDate = user.ReturnDate;
                                resFlightDbInsertList_Inbound.Add(res);
                            }
                            if (resFlightDbInsertList_Inbound.Count > 0)
                            {
                                Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Inbound, "flightbookingdetails");
                                if (Id > 0)
                                {
                                    Common.AddLogs($"Added Inbound Flight Detail For Booking ID {resFlightDbInsertList_Inbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.ApiRequests);
                                }
                                else
                                {
                                    Common.AddLogs($"Added Inbound Flight Detail Failed For Booking ID {resFlightDbInsertList_Inbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.ApiRequests);
                                }
                            }
                            for (int i = 0; i < objRes.outbound.Count; i++)
                            {
                                FlightOutbound objFlightOutbound = new FlightOutbound();
                                objFlightOutbound.Aircrafttype = objRes.outbound[i].aircraft_type;
                                objFlightOutbound.Airlinename = objRes.outbound[i].airline_name;
                                objFlightOutbound.Rtdepaure = objRes.outbound[i].rtdepaure;
                                objFlightOutbound.Departure = objRes.outbound[i].departure;
                                objFlightOutbound.Refundable = objRes.outbound[i].refundable;
                                objFlightOutbound.Infantfare = objRes.outbound[i].infant_fare;
                                objFlightOutbound.Flightclasscode = objRes.outbound[i].flight_class_code;
                                objFlightOutbound.Currency = objRes.outbound[i].currency;
                                objFlightOutbound.Faretotal = objRes.outbound[i].fare_total;
                                objFlightOutbound.Childfare = objRes.outbound[i].child_fare;
                                objFlightOutbound.Child = objRes.outbound[i].child;
                                objFlightOutbound.Childcommission = objRes.outbound[i].child_commission;
                                objFlightOutbound.Departuretime = objRes.outbound[i].departure_time;
                                objFlightOutbound.Tax = objRes.outbound[i].tax;
                                objFlightOutbound.Agencycommission = objRes.outbound[i].agency_commission;
                                objFlightOutbound.Airline = objRes.outbound[i].airline;
                                objFlightOutbound.Adult = objRes.outbound[i].adult;
                                objFlightOutbound.Adultfare = objRes.outbound[i].adult_fare;
                                objFlightOutbound.Airlinelogo = objRes.outbound[i].airline_logo;
                                objFlightOutbound.Flightno = objRes.outbound[i].flight_no;
                                objFlightOutbound.Fuelsurcharge = objRes.outbound[i].fuel_surcharge;
                                objFlightOutbound.Arrivaltime = objRes.outbound[i].arrival_time;
                                objFlightOutbound.Resfare = objRes.outbound[i].res_fare;
                                objFlightOutbound.Freebaggage = objRes.outbound[i].free_baggage;
                                objFlightOutbound.Arrival = objRes.outbound[i].arrival;
                                objFlightOutbound.Flightid = objRes.outbound[i].flight_id + "`" + objRes.outbound[i].fare_total;
                                objFlightOutbound.Flightdate = objRes.outbound[i].flight_date;
                                objFlightOutbound.Infant = objRes.outbound[i].infant;
                                objFlightOutbound.IsWalletBalanceSufficient = (Convert.ToDecimal(objRes.outbound[i].fare_total) < WalletBalance) ? true : false;
                                string ServiceId = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines);
                                AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId, objFlightOutbound.Faretotal, ServiceId);
                                objFlightOutbound.Cashback = Convert.ToString(objOut.CashbackAmount);
                                objFlightOutbound.RewardPoints = Convert.ToString(objOut.RewardPoints);
                                objFlightOutbound.ServiceCharge = Convert.ToString(objOut.ServiceCharge);

                                FlightOutbound.Add(objFlightOutbound);

                                AddFlightBookingDetails res = new AddFlightBookingDetails();
                                res.BookingId = Convert.ToInt64(objRes.booking_id);
                                res.MemberId = Convert.ToInt64(user.MemberId);
                                res.TripType = user.TripType;
                                res.FlightType = user.FlightType;
                                res.Adult = Convert.ToInt32(objFlightOutbound.Adult);
                                res.Child = Convert.ToInt32(objFlightOutbound.Child);
                                res.IsInbound = false;
                                res.Aircrafttype = objFlightOutbound.Aircrafttype;
                                res.Airlinename = objFlightOutbound.Airlinename;
                                res.Departure = objFlightOutbound.Departure;
                                res.Refundable = objFlightOutbound.Refundable;
                                res.Infantfare = Convert.ToDecimal(objFlightOutbound.Infantfare);
                                res.Flightclasscode = objFlightOutbound.Flightclasscode;
                                res.Currency = objFlightOutbound.Currency;
                                res.Faretotal = Convert.ToDecimal(objFlightOutbound.Faretotal);
                                res.Adultfare = Convert.ToDecimal(objFlightOutbound.Adultfare);
                                res.Childfare = Convert.ToDecimal(objFlightOutbound.Childfare);
                                res.Departuretime = objFlightOutbound.Departuretime;
                                res.Tax = Convert.ToDecimal(objFlightOutbound.Tax);
                                res.Airline = objFlightOutbound.Airline;
                                res.Airlinelogo = objFlightOutbound.Airlinelogo;
                                res.Flightno = objFlightOutbound.Flightno;
                                res.Fuelsurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge);
                                res.Arrivaltime = objFlightOutbound.Arrivaltime;
                                res.Resfare = Convert.ToDecimal(objFlightOutbound.Resfare);
                                res.Freebaggage = objFlightOutbound.Freebaggage;
                                res.Arrival = objFlightOutbound.Arrival;
                                res.Flightid = objFlightOutbound.Flightid;
                                res.Flightdate = Convert.ToDateTime(objFlightOutbound.Flightdate);
                                res.CreatedDate = System.DateTime.UtcNow;
                                res.UpdatedDate = System.DateTime.UtcNow;
                                res.IsActive = true;
                                res.IsDeleted = false;
                                res.IsApprovedByAdmin = true;
                                res.CreatedBy = Common.GetCreatedById(authenticationToken);
                                res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                res.IsFlightIssued = false;
                                res.IpAddress = Common.GetUserIP();
                                res.SectorFrom = user.FromDeparture;
                                res.SectorTo = user.ToArrival;
                                res.ReturnDate = user.ReturnDate;
                                resFlightDbInsertList_Outbound.Add(res);
                            }
                            if (resFlightDbInsertList_Outbound.Count > 0)
                            {
                                Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");
                                if (Id > 0)
                                {
                                    Common.AddLogs($"Added Outbound Flight Detail For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
                                }
                                else
                                {
                                    Common.AddLogs($"Added Outbound Flight Detail Failed For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
                                }
                            }
                            FlightOutbound.Sort((x, y) => Convert.ToDecimal(x.Faretotal).CompareTo(Convert.ToDecimal(y.Faretotal)));
                            FlightInbound.Sort((x, y) => Convert.ToDecimal(x.Faretotal).CompareTo(Convert.ToDecimal(y.Faretotal)));
                            result.FlightOutbound = FlightOutbound;
                            result.FlightInbound = FlightInbound;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.BookingId = objRes.booking_id;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = request.CreateResponse<Res_Vendor_API_Airlines_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-airlines completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-airlines {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} lookup-airlines {ex.ToString()} " + Environment.NewLine);
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/book-flight-airlines")]
        public HttpResponseMessage GetFlightAirlinesBook(Req_Vendor_API_Airlines_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            int ServiceID = 0;
            var userInput = getRawPostData().Result;
            var request = Request;
            GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
            inobject.IsActive = 1;
            GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());

            if (resSwitchType.FlightSwitchType == 1)
            {
                ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                return GetServiceFlight_Airlines(user, request, userInput);
            }
            else if (resSwitchType.FlightSwitchType == 2)
            {
                ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay;
                Req_Vendor_API_PlasmaTech_Flight_Reserve_Requests obj = new Req_Vendor_API_PlasmaTech_Flight_Reserve_Requests();
                obj = JsonConvert.DeserializeObject<Req_Vendor_API_PlasmaTech_Flight_Reserve_Requests>(JsonConvert.SerializeObject(user));

                var test = _RequestAPI_PlasmaTechController.GetServiceReserveFlight(obj, request, userInput);
                return test;
            }
            return null;
        }

        public HttpResponseMessage GetServiceFlight_Airlines(Req_Vendor_API_Airlines_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside book-flight-airlines" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_Requests result = new Res_Vendor_API_Airlines_Requests();
            //var response = Request.CreateResponse<Res_Vendor_API_Airlines_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            //var userInput = getRawPostData().Result;
            HttpResponseMessage response;

            HttpRequestMessage request;

            if (requestPassed != null)
            {
                request = requestPassed;
            }
            else
            {
                request = Request;
            }
            response = request.CreateResponse<Res_Vendor_API_Airlines_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            string userInput;

            if (userInputPassed != null)
            {
                userInput = userInputPassed;
            }
            else
            {
                userInput = getRawPostData().Result;
            }

            try
            {
                if (request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                        response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        //  string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_Airlines_Payment_Request objRes = new GetVendor_API_Airlines_Payment_Request();

                        string authenticationToken = request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string FlightFareTotal = user.FlightID.Split('`')[1];
                        string ReturnFlightFareTotal = string.Empty;
                        if (!string.IsNullOrEmpty(user.ReturnFlightID))
                        {
                            ReturnFlightFareTotal = user.ReturnFlightID.Split('`')[1];
                        }
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_BOOK_FLIGHT(user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.BookingID, user.FlightID, user.ReturnFlightID, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, user.MemberId, authenticationToken, userInput, ref objRes, resuserdetails);
                        if (msg.ToLower() == "success")
                        {
                            result.Commission = objRes.commission;
                            result.FlightID = objRes.flight_id + "`" + FlightFareTotal;
                            result.FlightFareTotal = FlightFareTotal;
                            result.AdultCommission = objRes.adult_commission;
                            result.InboundCommission = objRes.inbound_commission;
                            result.InboundAdultCommission = objRes.inbound_adult_commission;
                            result.ChildCommission = objRes.child_commission;
                            result.TTL = objRes.ttl;
                            result.InboundChildCommission = objRes.inbound_child_commission;
                            result.InboundFlightID = objRes.inbound_flight_id + "`" + ReturnFlightFareTotal;
                            result.InboundFlightFareTotal = ReturnFlightFareTotal;

                            bool IsUpdated = false;
                            AddFlightBookingDetails resOut = new AddFlightBookingDetails();
                            GetFlightBookingDetails resIn = new GetFlightBookingDetails();
                            resIn.BookingId = Convert.ToInt64(user.BookingID);
                            resIn.Flightid = user.FlightID;
                            resIn.CheckInbound = 0;
                            AddFlightBookingDetails resGetRecord = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn, resOut);
                            if (resGetRecord.Id != 0)
                            {
                                resGetRecord.IsFlightBooked = true;
                                resGetRecord.BookingCreatedDate = System.DateTime.UtcNow;
                                resGetRecord.IpAddress = Common.GetUserIP();
                                resGetRecord.CreatedBy = Common.GetCreatedById(authenticationToken);
                                resGetRecord.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                resGetRecord.UpdatedBy = Common.GetCreatedById(authenticationToken);
                                resGetRecord.UpdatedDate = System.DateTime.UtcNow;
                                IsUpdated = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecord, "flightbookingdetails");
                            }
                            if (IsUpdated)
                            {
                                AddFlightBookingDetails resOut_returnFlight = new AddFlightBookingDetails();
                                GetFlightBookingDetails resIn_returnFlight = new GetFlightBookingDetails();
                                resIn_returnFlight.BookingId = Convert.ToInt64(user.BookingID);
                                resIn_returnFlight.Flightid = user.ReturnFlightID;
                                resIn_returnFlight.CheckInbound = 1;
                                AddFlightBookingDetails resGetRecord_ReturnFlight = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn_returnFlight, resOut_returnFlight);
                                if (resGetRecord_ReturnFlight.Id != 0)
                                {
                                    IsUpdated = false;
                                    resGetRecord_ReturnFlight.IsFlightBooked = true;
                                    resGetRecord_ReturnFlight.BookingCreatedDate = System.DateTime.UtcNow;
                                    resGetRecord_ReturnFlight.IpAddress = Common.GetUserIP();
                                    resGetRecord_ReturnFlight.CreatedBy = Common.GetCreatedById(authenticationToken);
                                    resGetRecord_ReturnFlight.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                    resGetRecord_ReturnFlight.UpdatedBy = Common.GetCreatedById(authenticationToken);
                                    resGetRecord_ReturnFlight.UpdatedDate = System.DateTime.UtcNow;
                                    IsUpdated = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecord_ReturnFlight, "flightbookingdetails");
                                }
                                if (IsUpdated)
                                {
                                    int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                                    string Title = "Flight Booked";
                                    string Message = "Your flight has been booked for 30 mins. Your Booking Id is " + resIn_returnFlight.BookingId;
                                    Common.SendNotification(authenticationToken, VendorAPIType, Convert.ToInt64(user.MemberId), Title, Message);
                                    result.ReponseCode = objRes.status ? 1 : 0;
                                    result.status = objRes.status;
                                    result.Message = "Success";
                                    result.Details = "Selected flights will be reserved for 30 minutes.";
                                    result.ApiMessage = objRes.detail;
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    response = request.CreateResponse<Res_Vendor_API_Airlines_Requests>(System.Net.HttpStatusCode.OK, result);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                                    Common.AddLogs($"Flight Booked for 30 Mins For Booking ID {user.BookingID} by(MemberId:{user.MemberId}). Flight ID: '{user.FlightID}'. Return Flight ID: '{user.ReturnFlightID}'", false, (int)AddLog.LogType.ApiRequests, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName);

                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("Your flight is not booked. Please try again.");
                                    cres.Details = objRes.detail;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Your flight is not booked. Please try again.");
                                cres.Details = objRes.detail;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            cres.Details = objRes.detail;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} book-flight-airlines completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} book-flight-airlines {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} book-flight-airlines {ex.ToString()} " + Environment.NewLine);
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/add-flight-passenger")]
        public HttpResponseMessage GetFlightAirlinesPassengers(Req_Vendor_API_Airlines_PassengerRequests user)
        {
            int ServiceID = 0;
            GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
            inobject.IsActive = 1;
            var userInput = getRawPostData().Result;
            var request = Request;

            GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());

            if (resSwitchType.FlightSwitchType == 1)
            {
                ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                return AddServiceFlight_AirlinesPassengers(user, request, userInput);
            }
            else if (resSwitchType.FlightSwitchType == 2)
            {
                ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay;
                var test = RepKhalti.AddPassengerDetail(user.MemberId,user.FlightId, user.BookingID, user.PassengersClassString,user.ContactName, user.ContactPhone, user.ContactEmail);

                var resp = JsonConvert.DeserializeObject<CommonResponse>(test);

                var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.OK, resp);
                return response;
            }
            return null;
        }
        public HttpResponseMessage AddServiceFlight_AirlinesPassengers(Req_Vendor_API_Airlines_PassengerRequests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside add-flight-passenger" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_AddPassenger_Requests result = new Res_Vendor_API_Airlines_AddPassenger_Requests();
            //var response = Request.CreateResponse<Res_Vendor_API_Airlines_AddPassenger_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            //var userInput = getRawPostData().Result;
            HttpResponseMessage response;

            HttpRequestMessage request;

            if (requestPassed != null)
            {
                request = requestPassed;
            }
            else
            {
                request = Request;
            }
            response = request.CreateResponse<Res_Vendor_API_Airlines_AddPassenger_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            string userInput;

            if (userInputPassed != null)
            {
                userInput = userInputPassed;
            }
            else
            {
                userInput = getRawPostData().Result;
            }

            try
            {
                if (request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                        response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        List<FlightPassenger> PassengersList = new List<FlightPassenger>();
                        GetVendor_API_Airlines_AddPassenger_Request objRes = new GetVendor_API_Airlines_AddPassenger_Request();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.Reference = new CommonHelpers().GenerateUniqueId();


                        string msg = String.Empty;
                        if (string.IsNullOrEmpty(user.PassengersClassString))
                        {
                            msg = "Please enter PassengersClassString.";
                        }
                        else
                        {
                            try
                            {
                                PassengersList = JsonConvert.DeserializeObject<List<FlightPassenger>>(user.PassengersClassString);
                            }
                            catch (Exception)
                            {
                                msg = "Invalid PassengersClassString.";
                            }
                            if (string.IsNullOrEmpty(msg))
                            {
                                if (PassengersList == null || PassengersList.Count == 0)
                                {
                                    msg = "Please enter Passengers.";
                                }
                                else
                                {
                                    msg = RepKhalti.RequestServiceGroup_AIRLINES_ADD_PASSENGER(resuserdetails, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.BookingID, user.ContactName, user.ContactPhone, PassengersList, user.PassengersClassString, user.Reference, user.Version, user.DeviceCode, user.PlatForm, user.MemberId, authenticationToken, userInput, ref objRes);
                                }
                            }
                        }

                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = PassengersList.Count.ToString() + " Passengers Added Successfully.";
                            for (int i = 0; i < PassengersList.Count; i++)
                            {
                                AddFlightPassengersDetails res = new AddFlightPassengersDetails();
                                res.BookingId = Convert.ToInt64(user.BookingID);
                                res.Firstname = PassengersList[i].FirstName;
                                res.Lastname = PassengersList[i].LastName;
                                res.Nationality = PassengersList[i].Nationality;
                                res.Title = PassengersList[i].Title;
                                res.Type = PassengersList[i].Type;
                                res.Gender = PassengersList[i].Gender;
                                res.CreatedDate = System.DateTime.UtcNow;
                                res.UpdatedDate = System.DateTime.UtcNow;
                                res.IsActive = true;
                                res.IsDeleted = false;
                                res.IsApprovedByAdmin = true;
                                res.CreatedBy = Common.GetCreatedById(authenticationToken);
                                res.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                Int64 Id = RepCRUD<AddFlightPassengersDetails, AddFlightPassengersDetails>.Insert(res, "flightpassengersdetails");
                                if (Id > 0)
                                {
                                    Common.AddLogs($"Added Passenger Detail For Booking ID {res.BookingId} by(MemberId:{user.MemberId}). Passenger Name {res.Firstname}", false, (int)AddLog.LogType.ApiRequests);
                                }
                                else
                                {
                                    Common.AddLogs($"Added Passenger Detail Failed For Booking ID {res.BookingId} by(MemberId:{user.MemberId}). Passenger Name {res.Firstname}", false, (int)AddLog.LogType.ApiRequests);
                                }
                            }

                            AddFlightBookingDetails resOut = new AddFlightBookingDetails();
                            GetFlightBookingDetails resIn = new GetFlightBookingDetails();
                            resIn.BookingId = Convert.ToInt64(user.BookingID);
                            resIn.CheckFlightBooked = 1;
                            resIn.CheckInbound = 0;
                            AddFlightBookingDetails resGetRecord = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn, resOut);
                            if (resGetRecord.Id != 0)
                            {
                                resGetRecord.ContactName = user.ContactName;
                                resGetRecord.ContactPhone = user.ContactPhone;
                                resGetRecord.ContactEmail = user.ContactEmail;
                                bool isUpdated = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecord, "flightbookingdetails");
                            }

                            resIn = new GetFlightBookingDetails();
                            resIn.BookingId = Convert.ToInt64(user.BookingID);
                            resIn.CheckFlightBooked = 1;
                            resIn.CheckInbound = 1;
                            resGetRecord = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn, resOut);
                            if (resGetRecord.Id != 0)
                            {
                                resGetRecord.ContactName = user.ContactName;
                                resGetRecord.ContactPhone = user.ContactPhone;
                                resGetRecord.ContactEmail = user.ContactEmail;
                                RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecord, "flightbookingdetails");
                            }
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Airlines_AddPassenger_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} add-flight-passenger completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} add-flight-passenger {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} add-flight-passenger {ex.ToString()} " + Environment.NewLine);
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/issue-flight-airlines")]
        public HttpResponseMessage GetFlightIssue_Airlines(Req_Vendor_API_Airlines_Requests user)
        {
            int ServiceID = 0;
            GetApiFlightSwitchSettings inobject = new GetApiFlightSwitchSettings();
            inobject.IsActive = 1;
            var userInput = getRawPostData().Result;
            var request = Request;
            GetApiFlightSwitchSettings resSwitchType = VendorApi_CommonHelper.GetFlightDetail(inobject.IsActive.ToString());

            if (resSwitchType.FlightSwitchType == 1)
            {
                ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                return GetServiceFlightIssue_Airlines(user, request, userInput);
            }
            else if (resSwitchType.FlightSwitchType == 2)
            {
                ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay;
                Req_Vendor_API_PlasmaTech_Issue_Ticket_Requests obj = new Req_Vendor_API_PlasmaTech_Issue_Ticket_Requests();
                obj = JsonConvert.DeserializeObject<Req_Vendor_API_PlasmaTech_Issue_Ticket_Requests>(JsonConvert.SerializeObject(user));

                return _RequestAPI_PlasmaTechController.GetLookupServiceIssueTicket(obj, request, userInput);
            }
            return null;
        }
        public HttpResponseMessage GetServiceFlightIssue_Airlines(Req_Vendor_API_Airlines_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside issue-flight-airlines" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_Flight_Issue_Requests result = new Res_Vendor_API_Airlines_Flight_Issue_Requests();
            //var response = Request.CreateResponse<Res_Vendor_API_Airlines_Flight_Issue_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            //var userInput = getRawPostData().Result;
            HttpResponseMessage response;

            HttpRequestMessage request;

            if (requestPassed != null)
            {
                request = requestPassed;
            }
            else
            {
                request = Request;
            }
            response = request.CreateResponse<Res_Vendor_API_Airlines_Flight_Issue_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            string userInput;

            if (userInputPassed != null)
            {
                userInput = userInputPassed;
            }
            else
            {
                userInput = getRawPostData().Result;
            }

            try
            {
                if (request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                        response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId.Trim() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.FareTotal, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string KhaltiAPIURL = "issue/flight/";
                        string log_id = string.Empty;
                        GetVendor_API_Airlines_IssueFlight_Request objRes = new GetVendor_API_Airlines_IssueFlight_Request();

                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string JsonReq = string.Empty;
                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                        objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(KhaltiAPIURL, user.Reference, resGetRecord.MemberId, resGetRecord.FirstName + " " + resGetRecord.LastName, JsonReq, authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorApiType);
                        string msg = "";
                        if ((new CommonHelpers()).GetFlightLogsIssuedStatus(resGetRecord.MemberId, user.BookingID))
                        {
                            msg = "Flight already Issued";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            cres.Details = msg;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                        else
                        {

                            msg = RepKhalti.RequestServiceGroup_AIRLINES_ISSUE_FLIGHT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.BookingID, user.FlightID, user.ReturnFlightID, user.FareTotal, user.Reference, user.Version, user.DeviceCode,
                               user.PlatForm, user.MemberId, authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);

                            // ** BEFORE SUCCESS:  GET FLIGHT TIKET LOG ID FOR ALL CASES. IF STATUS IS CHANGED FROM PROCESSING TO SUCCESS THEN LOGIDS ARE NOT RETUNR FROM KHALTI
                            #region logid_update
                            string log_idscsv = string.Empty;
                            if (objRes != null && objRes.log_ids != null)
                            {
                                for (int k = 0; k < objRes.log_ids.Count; k++)
                                {
                                    if (k == 0)
                                    {
                                        log_id = objRes.log_ids[k].ToString();
                                    }
                                    log_idscsv = log_idscsv + objRes.log_ids[k].ToString() + (k == 0 ? "" : ",");
                                }
                            }
                            if (!string.IsNullOrEmpty(user.FlightID))
                            {
                                AddFlightBookingDetails resOutLogIDS = new AddFlightBookingDetails();
                                GetFlightBookingDetails resInLogIDS = new GetFlightBookingDetails();
                                resInLogIDS.BookingId = Convert.ToInt64(user.BookingID);
                                //resInLogIDS.Flightid = user.FlightID;
                                resInLogIDS.CheckFlightBooked = 1;
                                resInLogIDS.CheckInbound = 0;
                                AddFlightBookingDetails resGetRecordFlightBookingDetails_LogIDS = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resInLogIDS, resOutLogIDS);
                                if (resGetRecordFlightBookingDetails_LogIDS.Id != 0)
                                {
                                    if (!string.IsNullOrEmpty(log_id))
                                    {
                                        if (objRes != null && objRes.outbound != null && objRes.outbound.pnr_no != null)
                                        {
                                            if (!string.IsNullOrEmpty(objRes.outbound.pnr_no))
                                            {
                                                resGetRecordFlightBookingDetails_LogIDS.PnrNumber = objRes.outbound.pnr_no;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(log_idscsv))
                                        {
                                            resGetRecordFlightBookingDetails_LogIDS.LogIDs = log_idscsv;
                                        }
                                        RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecordFlightBookingDetails_LogIDS, "flightbookingdetails");
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(user.ReturnFlightID))
                            {
                                AddFlightBookingDetails resOutLogIDS = new AddFlightBookingDetails();
                                GetFlightBookingDetails resInLogIDS = new GetFlightBookingDetails();
                                resInLogIDS.BookingId = Convert.ToInt64(user.BookingID);
                                //resInLogIDS.Flightid = user.ReturnFlightID;
                                resInLogIDS.CheckFlightBooked = 1;
                                resInLogIDS.CheckInbound = 1;
                                AddFlightBookingDetails resGetRecordFlightBookingDetails_LogIDS = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resInLogIDS, resOutLogIDS);
                                if (resGetRecordFlightBookingDetails_LogIDS.Id != 0)
                                {
                                    if (!string.IsNullOrEmpty(log_id))
                                    {
                                        if (objRes != null && objRes.inbound != null && objRes.inbound.pnr_no != null)
                                        {
                                            if (!string.IsNullOrEmpty(objRes.inbound.pnr_no))
                                            {
                                                resGetRecordFlightBookingDetails_LogIDS.PnrNumber = objRes.inbound.pnr_no;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(log_idscsv))
                                        {
                                            resGetRecordFlightBookingDetails_LogIDS.LogIDs = log_idscsv;
                                        }
                                        RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecordFlightBookingDetails_LogIDS, "flightbookingdetails");
                                    }
                                }
                            }
                            #endregion
                            if (msg.ToLower() == "success")
                            {
                                List<IssueFlightPassengerResponse> Passengers = new List<IssueFlightPassengerResponse>();
                                for (int i = 0; i < objRes.passengers.Count; i++)
                                {
                                    IssueFlightPassengerResponse objIssueFlightPassengerResponse = new IssueFlightPassengerResponse();
                                    objIssueFlightPassengerResponse.Barcode = objRes.passengers[i].barcode;
                                    objIssueFlightPassengerResponse.TicketNo = objRes.passengers[i].ticket_no;
                                    objIssueFlightPassengerResponse.InboundTicketNo = objRes.passengers[i].inbound_ticket_no;
                                    objIssueFlightPassengerResponse.Lastname = objRes.passengers[i].lastname;
                                    objIssueFlightPassengerResponse.PassengerType = objRes.passengers[i].passenger_type;
                                    objIssueFlightPassengerResponse.Title = objRes.passengers[i].title;
                                    objIssueFlightPassengerResponse.InboundBarcode = objRes.passengers[i].inbound_barcode;
                                    objIssueFlightPassengerResponse.Gender = objRes.passengers[i].gender;
                                    objIssueFlightPassengerResponse.Firstname = objRes.passengers[i].firstname;
                                    Passengers.Add(objIssueFlightPassengerResponse);

                                    AddFlightPassengersDetails resOut = new AddFlightPassengersDetails();
                                    GetFlightPassengersDetails resIn = new GetFlightPassengersDetails();
                                    resIn.BookingId = Convert.ToInt64(user.BookingID);
                                    resIn.FirstName = objIssueFlightPassengerResponse.Firstname;
                                    resIn.LastName = objIssueFlightPassengerResponse.Lastname;
                                    resIn.Type = (objIssueFlightPassengerResponse.PassengerType.ToLower() == "children" ? "CHILD" : objIssueFlightPassengerResponse.PassengerType.ToUpper());
                                    resIn.Gender = objIssueFlightPassengerResponse.Gender.Substring(0, 1);
                                    AddFlightPassengersDetails resGetRecordPassengers = RepCRUD<GetFlightPassengersDetails, AddFlightPassengersDetails>.GetRecord(Common.StoreProcedures.sp_FlightPassengersDetails_Get, resIn, resOut);
                                    if (resGetRecordPassengers.Id != 0)
                                    {
                                        resGetRecordPassengers.TicketNo = objIssueFlightPassengerResponse.TicketNo;
                                        resGetRecordPassengers.InboundTicketNo = objIssueFlightPassengerResponse.InboundTicketNo;
                                        resGetRecordPassengers.BarCode = objIssueFlightPassengerResponse.Barcode;
                                        resGetRecordPassengers.InboundBarCode = objIssueFlightPassengerResponse.InboundBarcode;
                                        RepCRUD<AddFlightPassengersDetails, GetFlightPassengersDetails>.Update(resGetRecordPassengers, "flightpassengersdetails");
                                    }
                                }

                                IssueFlightInboundResponse Inbound = new IssueFlightInboundResponse();
                                Inbound.PnrNo = objRes.inbound.pnr_no;
                                Inbound.FareTotal = objRes.inbound.fare_total;
                                Inbound.AirlineName = objRes.inbound.airline_name;
                                Inbound.Airline = objRes.inbound.airline;
                                Inbound.DepartureTime = objRes.inbound.departure_time;
                                Inbound.FlightNo = objRes.inbound.flight_no;
                                Inbound.FlightClassCode = objRes.inbound.flight_class_code;
                                Inbound.Currency = objRes.inbound.currency;
                                Inbound.InboundReportingTime = objRes.inbound.inbound_reporting_time;
                                Inbound.ArrivalTime = objRes.inbound.arrival_time;

                                IssueFlightOutboundResponse Outbound = new IssueFlightOutboundResponse();
                                Outbound.PnrNo = objRes.outbound.pnr_no;
                                Outbound.FareTotal = objRes.outbound.fare_total;
                                Outbound.AirlineName = objRes.outbound.airline_name;
                                Outbound.Airline = objRes.outbound.airline;
                                Outbound.DepartureTime = objRes.outbound.departure_time;
                                Outbound.FlightNo = objRes.outbound.flight_no;
                                Outbound.ReportingTime = objRes.outbound.reporting_time;
                                Outbound.FlightClassCode = objRes.outbound.flight_class_code;
                                Outbound.Currency = objRes.outbound.currency;
                                Outbound.ArrivalTime = objRes.outbound.arrival_time;

                                if (!string.IsNullOrEmpty(Outbound.DepartureTime))
                                {
                                    AddFlightBookingDetails resOut = new AddFlightBookingDetails();
                                    GetFlightBookingDetails resIn = new GetFlightBookingDetails();
                                    resIn.BookingId = Convert.ToInt64(user.BookingID);
                                    resIn.Flightid = user.FlightID;
                                    resIn.CheckFlightBooked = 1;
                                    resIn.CheckInbound = 0;
                                    AddFlightBookingDetails resGetRecordFlightBookingDetails = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn, resOut);
                                    if (resGetRecordFlightBookingDetails.Id != 0)
                                    {
                                        if (!string.IsNullOrEmpty(log_id))
                                        {
                                            string fullpath = string.Empty;
                                            KhaltiAPIURL = "download";
                                            Guid ReferenceGuid = Guid.NewGuid();
                                            string Reference = new CommonHelpers().GenerateUniqueId();
                                            GetVendor_API_Airlines_Lookup objResDownload = VendorApi_CommonHelper.RequestAirlines_DOWNLOAD_TICKET(Reference, user.MemberId, log_id, "/Content/FlightTicketPDF", ref fullpath, KhaltiAPIURL);
                                            resGetRecordFlightBookingDetails.TicketPDF = objResDownload.FilePath;
                                        }
                                        resGetRecordFlightBookingDetails.IsFlightIssued = true;
                                        resGetRecordFlightBookingDetails.PnrNumber = Outbound.PnrNo;
                                        resGetRecordFlightBookingDetails.LogIDs = log_idscsv;
                                        resGetRecordFlightBookingDetails.ReturnDate = objRes.return_date.ToString();
                                        resGetRecordFlightBookingDetails.SectorFrom = objRes.sector_from.ToString();
                                        resGetRecordFlightBookingDetails.SectorTo = objRes.sector_to.ToString();
                                        RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecordFlightBookingDetails, "flightbookingdetails");
                                    }
                                }

                                if (!string.IsNullOrEmpty(Inbound.DepartureTime))
                                {
                                    AddFlightBookingDetails resOut_returnFlight = new AddFlightBookingDetails();
                                    GetFlightBookingDetails resIn_returnFlight = new GetFlightBookingDetails();
                                    resIn_returnFlight.BookingId = Convert.ToInt64(user.BookingID);
                                    resIn_returnFlight.Flightid = user.ReturnFlightID;
                                    resIn_returnFlight.CheckInbound = 1;
                                    AddFlightBookingDetails resGetRecord_ReturnFlight = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn_returnFlight, resOut_returnFlight);
                                    if (resGetRecord_ReturnFlight.Id != 0)
                                    {
                                        resGetRecord_ReturnFlight.IsFlightIssued = true;
                                        resGetRecord_ReturnFlight.PnrNumber = Inbound.PnrNo;
                                        resGetRecord_ReturnFlight.LogIDs = log_idscsv;
                                        resGetRecord_ReturnFlight.ReturnDate = objRes.return_date.ToString();
                                        resGetRecord_ReturnFlight.SectorFrom = objRes.sector_to.ToString();
                                        resGetRecord_ReturnFlight.SectorTo = objRes.sector_from.ToString();
                                        RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecord_ReturnFlight, "flightbookingdetails");
                                    }
                                }
                                result.ReponseCode = objRes.status ? 1 : 0;
                                result.status = objRes.status;
                                result.TransactionUniqueId = TransactionID;
                                result.UniqueTransactionId = objRes.UniqueTransactionId;
                                result.WalletBalance = objRes.WalletBalance;
                                result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                                result.ResponseId = objRes.response_id;
                                result.LogIds = objRes.log_ids;
                                result.Passengers = Passengers;
                                result.ReturnDate = objRes.return_date;
                                result.FlightDate = objRes.flight_date;
                                result.Commission = objRes.commission;
                                result.TripType = objRes.trip_type;
                                result.SectorFrom = objRes.sector_from;
                                result.SectorTo = objRes.sector_to;
                                result.CreditsConsumed = objRes.credits_consumed;
                                result.Inbound = Inbound;
                                result.Outbound = Outbound;
                                result.Message = "Success";
                                result.Details = "Your Flight Booked Successfully.";
                                result.ApiMessage = RepKhalti.resKhalti.Res_Khalti_Message;
                                response.StatusCode = HttpStatusCode.Accepted;
                                response = Request.CreateResponse<Res_Vendor_API_Airlines_Flight_Issue_Requests>(System.Net.HttpStatusCode.OK, result);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                cres.Details = objRes.detail;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                            }
                        }
                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                        inobjApiResponse.Id = objVendor_API_Requests.Id;
                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} issue-flight-airlines completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} issue-flight-airlines {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} issue-flight-airlines {ex.ToString()} " + Environment.NewLine);
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/check-issue-flight-airlines")]
        public HttpResponseMessage GetServiceFlightIssue_Airlines_Check(Req_Vendor_API_Airlines_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside issue-flight-airlines" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_Flight_Issue_Check_Requests result = new Res_Vendor_API_Airlines_Flight_Issue_Check_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Airlines_Flight_Issue_Check_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId.Trim() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.FareTotal, true, user.Mpin);
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
                        string log_id = string.Empty;
                        GetVendor_API_Airlines_IssueFlight_Request objRes = new GetVendor_API_Airlines_IssueFlight_Request();

                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;

                        int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines;
                        string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                        objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", user.Reference, resGetRecord.MemberId, resGetRecord.FirstName + " " + resGetRecord.LastName, "", authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorApiType);

                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_ISSUE_FLIGHT_CHECK(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.BookingID, user.FlightID, user.ReturnFlightID, user.FareTotal, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, user.MemberId, authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);

                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "Success";
                            result.Details = "Success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Airlines_Flight_Issue_Check_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            cres.Details = msg + " (BookingID: " + user.BookingID + " and FlightID: " + user.FlightID + ")";
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                        inobjApiResponse.Id = objVendor_API_Requests.Id;
                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} issue-flight-airlines completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} issue-flight-airlines {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} issue-flight-airlines {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/cancel-flight-airlines")]
        public HttpResponseMessage GetServiceFlightCancel_Airlines(Req_Vendor_API_Airlines_CancelFlight_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside cancel-flight-airlines" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_CancelFlight_Requests result = new Res_Vendor_API_Airlines_CancelFlight_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Airlines_CancelFlight_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    //string md5hash = Common.CheckHash(user);
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
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                        GetVendor_API_Airlines_CancelFlight_Request objRes = new GetVendor_API_Airlines_CancelFlight_Request();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_CANCEL_FLIGHT(user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.BookingID, user.CancelTickets, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, user.MemberId, authenticationToken, userInput, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = "Tickets cancelled";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Airlines_CancelFlight_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} cancel-flight-airlines completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} cancel-flight-airlines {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} cancel-flight-airlines {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/download-flight-ticket")]
        public HttpResponseMessage GetDownloadTicket_Airlines(Req_Vendor_API_Airlines_DownloadTicket_Requests user)
        {

            Res_Vendor_API_Airlines_Download_Ticket_Requests result = new Res_Vendor_API_Airlines_Download_Ticket_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;


            if (user.LogID.StartsWith("Plasma_"))
            {
                var flightID = user.LogID.Replace("Plasma_", "");
                string fullpath = string.Empty;

                AddFlightBookingDetails outobject = new AddFlightBookingDetails();
                GetFlightBookingDetails_Plasma inobject = new GetFlightBookingDetails_Plasma();

                inobject.MemberId = Convert.ToInt64(user.MemberId);
                inobject.Flightid = flightID;
                inobject.LogIDs = user.LogID;

                AddFlightBookingDetails resGetRecord = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord("sp_FlightBookingDetails_Get_plasma", inobject, outobject);
                GetVendor_API_Airlines_Lookup objResDownload = VendorApi_CommonHelper.RequestPlasmaAirlines_DOWNLOAD_TICKET(resGetRecord.BookingId,user.MemberId, flightID, resGetRecord.ContactName, resGetRecord.ContactPhone, resGetRecord.ContactEmail, "/Content/FlightTicketPDF", ref fullpath);
             
                result.FilePath = Common.LiveApiUrl + objResDownload.FilePath;
                result.status = true;
                result.ReponseCode = 1;
                result.responseMessage = "Success";
                result.Message = "Success";

                return Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                log.Info($"{System.DateTime.Now.ToString()} inside download-flight-ticket" + Environment.NewLine);
            }

            CommonResponse cres = new CommonResponse();


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
                    //string md5hash = Common.CheckHash(user);
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
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                        GetVendor_API_Airlines_Lookup objRes = new GetVendor_API_Airlines_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_DOWNLOAD_TICKET(user.MemberId, user.Reference, user.LogID, user.Version, user.DeviceCode, user.PlatForm, false, "", ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = 1;
                            result.FilePath = objRes.FilePath;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} download-flight-ticket completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} download-flight-ticket {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} download-flight-ticket {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/download-flight-ticket-new")]
        public HttpResponseMessage GetDownloadTicket_Airlines_New(Req_Vendor_API_Airlines_DownloadTicket_Requests user)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            Res_Vendor_API_Airlines_Download_Ticket_Requests result = new Res_Vendor_API_Airlines_Download_Ticket_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;


            if (user.LogID.StartsWith("Plasma_"))
            {
                var flightID = user.LogID.Replace("Plasma_", "");
                string fullpath = string.Empty;

                AddFlightBookingDetails outobject = new AddFlightBookingDetails();
                GetFlightBookingDetails_Plasma inobject = new GetFlightBookingDetails_Plasma();

                inobject.MemberId = Convert.ToInt64(user.MemberId);
                inobject.Flightid = flightID;
                inobject.LogIDs = user.LogID;

                AddFlightBookingDetails resGetRecord = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord("sp_FlightBookingDetails_Get_plasma", inobject, outobject);
                GetVendor_API_Airlines_Lookup objResDownload = VendorApi_CommonHelper.RequestPlasmaAirlines_DOWNLOAD_TICKET(resGetRecord.BookingId, user.MemberId, flightID, resGetRecord.ContactName, resGetRecord.ContactPhone, resGetRecord.ContactEmail, "/Content/FlightTicketPDF", ref fullpath);


                ///Content/FlightTicketPDF/



                //HttpResponseMessage result2 = new HttpResponseMessage(HttpStatusCode.OK);
                //var stream2 = new FileStream(path, FileMode.Open);
                //result2.Content = new StreamContent(stream2);
                //result2.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //result2.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
                //result2.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                //result2.Content.Headers.ContentLength = stream2.Length;
                ////new HttpResponse().write
                //return result2;

                try
                {
                    var filename = Path.GetFileName(fullpath);
                    var path = System.Web.HttpContext.Current.Server.MapPath("~/Content/FlightTicketPDF/" + filename);

                    log.Info("TICKET PATH:" + path);

                    HttpResponseMessage result2 = new HttpResponseMessage(HttpStatusCode.OK);
                    var stream2 = new FileStream(path, FileMode.Open);
                    result2.Content = new StreamContent(stream2);
                    result2.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");//("attachment");
                    result2.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
                    result2.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    result2.Content.Headers.ContentLength = stream2.Length;
                    result2.Content.Headers.Add("myHeader", path);
                    //new HttpResponse().write
                    return result2;

                }
                catch (Exception ex)
                {
                    log.Info("TICKET ERROR: " + ex.ToString());
                    throw;
                }

               





                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=" + uw.TransactionUniqueId + ".pdf");
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.WriteFile(Server.MapPath("/Content/TransactionPDF/" + filename + ".pdf"));
                ////Response.End();
                //Response.Flush(); // Sends all currently buffered output to the client.
                //Response.SuppressContent = true;

                //result.FilePath = Common.LiveApiUrl + objResDownload.FilePath;
                //result.status = true;
                //result.ReponseCode = 1;
                //result.responseMessage = "Success";
                //result.Message = "Success";

                //return Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                log.Info($"{System.DateTime.Now.ToString()} inside download-flight-ticket" + Environment.NewLine);
            }

            CommonResponse cres = new CommonResponse();


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
                    //string md5hash = Common.CheckHash(user);
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
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                        GetVendor_API_Airlines_Lookup objRes = new GetVendor_API_Airlines_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_DOWNLOAD_TICKET(user.MemberId, user.Reference, user.LogID, user.Version, user.DeviceCode, user.PlatForm, false, "", ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            ///Content/FlightTicketPDF/
                            var filename = Path.GetFileName(objRes.FilePath);
                            var path = System.Web.HttpContext.Current.Server.MapPath("~/Content/FlightTicketPDF/" + filename);


                            HttpResponseMessage result2 = new HttpResponseMessage(HttpStatusCode.OK);
                            var stream2 = new FileStream(path, FileMode.Open);
                            result2.Content = new StreamContent(stream2);
                            result2.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                            result2.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
                            result2.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                            result2.Content.Headers.ContentLength = stream2.Length;
                            return result2;

                            //result.ReponseCode = 1;
                            //result.FilePath = objRes.FilePath;
                            //result.status = objRes.status;
                            //result.Message = "success";
                            //response.StatusCode = HttpStatusCode.Accepted;
                            //response = Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} download-flight-ticket completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} download-flight-ticket {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} download-flight-ticket {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/send-flight-ticket")]
        public HttpResponseMessage GetSendTicket_Airlines(Req_Vendor_API_Airlines_SendTicket_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside send-flight-ticket" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_Download_Ticket_Requests result = new Res_Vendor_API_Airlines_Download_Ticket_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                        GetVendor_API_Airlines_Lookup objRes = new GetVendor_API_Airlines_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_DOWNLOAD_TICKET(user.MemberId, user.Reference, user.LogID, user.Version, user.DeviceCode, user.PlatForm, true, user.ContactEmail, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = 1;
                            result.status = objRes.status;
                            result.FilePath = Common.LiveApiUrl + objRes.FilePath;
                            result.Message = "success";
                            result.Details = objRes.Message;
                            result.responseMessage = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Airlines_Download_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} send-flight-ticket completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} send-flight-ticket {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} send-flight-ticket {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/airlines-flight-status")]
        public HttpResponseMessage GetFlightStatus_Airlines(Req_Vendor_API_Airlines_Details_Requests user)
        {
            log.Info($"  {System.DateTime.Now.ToString()}   GetFlightStatus_Airlines Started  {Environment.NewLine}");

            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_StatusFlight_Requests result = new Res_Vendor_API_Airlines_StatusFlight_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Airlines_StatusFlight_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

            try
            {
                //if (Request.Headers.Authorization == null)
                //{
                //    string results = "Un-Authorized Request";
                //    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                //    response.StatusCode = HttpStatusCode.BadRequest;
                //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                //    return response;
                //}
                //else
                {
                    //string md5hash = Common.CheckHash(user);

                    //string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    //if (results != "Success")
                    //{
                    //    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    //    response.StatusCode = HttpStatusCode.BadRequest;
                    //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    //    return response;
                    //}
                    //else
                    {
                        //string CommonResult = "";
                        //AddUser resuserdetails = new AddUser();
                        //if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        //{
                        //    Int64 memId = Convert.ToInt64(user.MemberId);
                        //    int VendorAPIType = 0;
                        //    int Type = 0;
                        //    resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId,UserInput,user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId,ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin);
                        //    if (CommonResult.ToLower() != "success")
                        //    {
                        //        CommonResponse cres1 = new CommonResponse();
                        //        cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                        //        response.StatusCode = HttpStatusCode.BadRequest;
                        //        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //        return response;
                        //    }
                        //}
                        //else
                        //{
                        //    CommonResponse cres1 = new CommonResponse();
                        //    cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //    return response;
                        //}

                        GetVendor_API_Airlines_CheckStatus objRes = new GetVendor_API_Airlines_CheckStatus();
                        //user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_AIRLINES_FLIGHT_STATUS(user.MemberId, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            log.Info($"  {System.DateTime.Now.ToString()}   GetFlightStatus_Airlines() message from Vendor : success  {Environment.NewLine}");


                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "success";

                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Airlines_StatusFlight_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            log.Info($"  {System.DateTime.Now.ToString()}   GetFlightStatus_Airlines : {msg}  {Environment.NewLine}");

                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            cres.Details = objRes.details;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetFlightStatus_Airlines completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetFlightStatus_Airlines {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} GetFlightStatus_Airlines {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPost]
        [Route("api/get-flight-details")]
        public HttpResponseMessage GetServiceFlightDetails(Req_Vendor_API_Airlines_Details_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside get-flight-details" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_Details_Requests result = new Res_Vendor_API_Airlines_Details_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Airlines_Details_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = String.Empty;
                        List<AddFlightBookingDetails> InboundBooking = new List<AddFlightBookingDetails>();
                        List<AddFlightBookingDetails> OutBoundBooking = new List<AddFlightBookingDetails>();
                        msg = REQUEST_AIRLINES_DETAILS(user.MemberId, user.BookingID, user.Reference, user.Version, user.DeviceCode, user.PlatForm, user.MemberId, authenticationToken, ref InboundBooking, ref OutBoundBooking);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            //if (InboundBooking.Count > 0)
                            //{
                            //    result.Bookings = InboundBooking;
                            //}
                            //else
                            {
                                result.Bookings = OutBoundBooking;
                            }
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Airlines_Details_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} get-flight-details completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} get-flight-details {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} get-flight-details {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/get-flight-booking-details")]
        public HttpResponseMessage GetServiceFlightBDetails(Req_Vendor_API_Airlines_Details_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside get-flight-booking-details" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Airlines_Booking_Details_Requests result = new Res_Vendor_API_Airlines_Booking_Details_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Airlines_Booking_Details_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = String.Empty;
                        List<AddFlightBookingDetails> InboundBooking = new List<AddFlightBookingDetails>();
                        List<AddFlightBookingDetails> OutBoundBooking = new List<AddFlightBookingDetails>();
                        msg = REQUEST_AIRLINES_BOOKING_DETAILS(user.MemberId, user.BookingID, user.Reference, user.Version, user.DeviceCode, user.PlatForm, user.MemberId, authenticationToken, ref InboundBooking, ref OutBoundBooking);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            result.InBounds = InboundBooking;
                            result.OutBounds = OutBoundBooking;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Airlines_Booking_Details_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} get-flight-booking-details completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} get-flight-booking-details {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} get-flight-booking-details {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private string REQUEST_AIRLINES_DETAILS(string MemberId, string BookingID, string reference, string version, string deviceCode, string platForm, string memberId, string authenticationToken, ref List<AddFlightBookingDetails> InboundBooking, ref List<AddFlightBookingDetails> OutBoundBooking)
        {
            string msg = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
                {
                    msg = "Please enter MemberId.";
                }
                else
                {
                    Int64 FlightBookingID = 0;
                    if (!string.IsNullOrEmpty(BookingID) && BookingID != "0")
                    {
                        FlightBookingID = Convert.ToInt64(BookingID);
                    }
                    AddFlightBookingDetails resOut_Inbound = new AddFlightBookingDetails();
                    GetFlightBookingDetails resIn_Inbound = new GetFlightBookingDetails();
                    resIn_Inbound.MemberId = Convert.ToInt64(MemberId);
                    resIn_Inbound.BookingId = FlightBookingID;
                    resIn_Inbound.CheckFlightBooked = 1;
                    resIn_Inbound.CheckInbound = 1;
                    resIn_Inbound.CheckTTL = 0;
                    List<AddFlightBookingDetails> resGetRecord_Inbound = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecordList(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn_Inbound, resOut_Inbound);
                    //if (resGetRecord_Inbound != null && resGetRecord_Inbound.Count > 0)
                    {
                        InboundBooking = resGetRecord_Inbound;
                        for (int i = 0; i < InboundBooking.Count; i++)
                        {
                            AddFlightPassengersDetails resOut_PassengersDetails = new AddFlightPassengersDetails();
                            GetFlightPassengersDetails resIn_PassengersDetails = new GetFlightPassengersDetails();
                            resIn_PassengersDetails.BookingId = InboundBooking[i].BookingId;
                            List<AddFlightPassengersDetails> resGetRecord_PassengersDetails = RepCRUD<GetFlightPassengersDetails, AddFlightPassengersDetails>.GetRecordList(Common.StoreProcedures.sp_FlightPassengersDetails_Get, resIn_PassengersDetails, resOut_PassengersDetails);
                            InboundBooking[i].PassengersDetails = resGetRecord_PassengersDetails;
                            InboundBooking[i].BookingType = AddFlightBookingDetails.BookingTypes.Round_Trip.ToString().Replace("_", " ");
                        }
                    }
                    //else
                    {
                        AddFlightBookingDetails resOut_Outbound = new AddFlightBookingDetails();
                        GetFlightBookingDetails resIn_Outbound = new GetFlightBookingDetails();
                        resIn_Outbound.MemberId = Convert.ToInt64(MemberId);
                        resIn_Outbound.BookingId = FlightBookingID;
                        resIn_Outbound.CheckFlightBooked = 1;
                        resIn_Outbound.CheckInbound = 0;
                        resIn_Outbound.CheckTTL = 1;
                        List<AddFlightBookingDetails> resGetRecord_Outbound = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecordList(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn_Outbound, resOut_Outbound);
                        if (resGetRecord_Outbound != null && resGetRecord_Outbound.Count > 0)
                        {
                            OutBoundBooking = resGetRecord_Outbound;
                            for (int i = 0; i < OutBoundBooking.Count; i++)
                            {
                                AddFlightPassengersDetails resOut_PassengersDetails = new AddFlightPassengersDetails();
                                GetFlightPassengersDetails resIn_PassengersDetails = new GetFlightPassengersDetails();
                                resIn_PassengersDetails.BookingId = OutBoundBooking[i].BookingId;
                                long bookingId = OutBoundBooking[i].BookingId;
                                List<AddFlightPassengersDetails> resGetRecord_PassengersDetails = VendorApi_CommonHelper.GetPassRecordList(bookingId);
                                //List<AddFlightPassengersDetails> resGetRecord_PassengersDetails = RepCRUD<GetFlightPassengersDetails, AddFlightPassengersDetails>.GetRecordList(Common.StoreProcedures.sp_FlightPassengersDetails_Get, resIn_PassengersDetails, resOut_PassengersDetails);
                                OutBoundBooking[i].PassengersDetails = resGetRecord_PassengersDetails;

                                List<AddFlightBookingDetails> checkInboundBookings = InboundBooking.FindAll(c => c.BookingId == resIn_PassengersDetails.BookingId);
                                if (checkInboundBookings.Count > 0)
                                {
                                    OutBoundBooking[i].BookingType = AddFlightBookingDetails.BookingTypes.Round_Trip.ToString().Replace("_", " ");
                                    OutBoundBooking[i].TripFareTotal = Convert.ToDecimal(checkInboundBookings[0].Faretotal + OutBoundBooking[i].Faretotal).ToString("0.00");
                                }
                                else
                                {
                                    OutBoundBooking[i].BookingType = AddFlightBookingDetails.BookingTypes.One_Way.ToString().Replace("_", " ");
                                    OutBoundBooking[i].TripFareTotal = Convert.ToDecimal(OutBoundBooking[i].Faretotal).ToString("0.00");
                                }
                            }
                        }
                    }
                    msg = "success";
                }
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
                msg = $"error: {ex.Message}";
            }
            return msg;
        }


        private string REQUEST_AIRLINES_BOOKING_DETAILS(string MemberId, string BookingID, string reference, string version, string deviceCode, string platForm, string memberId, string authenticationToken, ref List<AddFlightBookingDetails> InboundBooking, ref List<AddFlightBookingDetails> OutBoundBooking)
        {
            bool isPlasmaTech = false;

            string msg = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(MemberId) || MemberId == "0")
                {
                    msg = "Please enter MemberId.";
                }
                else if (string.IsNullOrEmpty(BookingID) || BookingID == "0")
                {
                    msg = "Please enter BookingID.";
                }
                else
                {
                    Int64 FlightBookingID = 0;
                    if (!string.IsNullOrEmpty(BookingID) && BookingID != "0")
                    {
                        FlightBookingID = Convert.ToInt64(BookingID);
                    }
                    AddFlightBookingDetails resOut_Inbound = new AddFlightBookingDetails();
                    GetFlightBookingDetails resIn_Inbound = new GetFlightBookingDetails();
                    resIn_Inbound.MemberId = Convert.ToInt64(MemberId);
                    resIn_Inbound.BookingId = FlightBookingID;
                    resIn_Inbound.CheckFlightBooked = 1;
                    resIn_Inbound.CheckInbound = 1;
                    List<AddFlightBookingDetails> resGetRecord_Inbound = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecordList(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn_Inbound, resOut_Inbound);
                    if (resGetRecord_Inbound != null && resGetRecord_Inbound.Count > 0)
                    {
                        InboundBooking = resGetRecord_Inbound;
                        for (int i = 0; i < InboundBooking.Count; i++)
                        {
                            AddFlightPassengersDetails resOut_PassengersDetails = new AddFlightPassengersDetails();
                            GetFlightPassengersDetails resIn_PassengersDetails = new GetFlightPassengersDetails();
                            resIn_PassengersDetails.BookingId = InboundBooking[i].BookingId;
                            List<AddFlightPassengersDetails> resGetRecord_PassengersDetails = RepCRUD<GetFlightPassengersDetails, AddFlightPassengersDetails>.GetRecordList(Common.StoreProcedures.sp_FlightPassengersDetails_Get, resIn_PassengersDetails, resOut_PassengersDetails);
                            InboundBooking[i].PassengersDetails = resGetRecord_PassengersDetails;
                            if (resGetRecord_Inbound.Count > 0)
                            {
                                InboundBooking[i].BookingType = AddFlightBookingDetails.BookingTypes.Round_Trip.ToString().Replace("_", " ");
                            }
                            else
                            {
                                InboundBooking[i].BookingType = AddFlightBookingDetails.BookingTypes.One_Way.ToString().Replace("_", " ");
                            }
                        }
                    }
                    AddFlightBookingDetails resOut_Outbound = new AddFlightBookingDetails();
                    GetFlightBookingDetails resIn_Outbound = new GetFlightBookingDetails();
                    resIn_Outbound.MemberId = Convert.ToInt64(MemberId);
                    resIn_Outbound.BookingId = FlightBookingID;
                    resIn_Outbound.CheckFlightBooked = 1;
                    resIn_Outbound.CheckInbound = 0;
                    List<AddFlightBookingDetails> resGetRecord_Outbound = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecordList(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn_Outbound, resOut_Outbound);
                    if (resGetRecord_Outbound != null && resGetRecord_Outbound.Count > 0)
                    {


                        OutBoundBooking = resGetRecord_Outbound;
                        for (int i = 0; i < OutBoundBooking.Count; i++)
                        {
                            AddFlightPassengersDetails resOut_PassengersDetails = new AddFlightPassengersDetails();
                            GetFlightPassengersDetails resIn_PassengersDetails = new GetFlightPassengersDetails();
                            resIn_PassengersDetails.BookingId = OutBoundBooking[i].BookingId;
                            List<AddFlightPassengersDetails> resGetRecord_PassengersDetails = RepCRUD<GetFlightPassengersDetails, AddFlightPassengersDetails>.GetRecordList(Common.StoreProcedures.sp_FlightPassengersDetails_Get, resIn_PassengersDetails, resOut_PassengersDetails);
                            OutBoundBooking[i].PassengersDetails = resGetRecord_PassengersDetails;

                            isPlasmaTech = OutBoundBooking[i].LogIDs.Contains("Plasma_");

                            if (resGetRecord_Inbound.Count > 0)
                            {
                                OutBoundBooking[i].BookingType = AddFlightBookingDetails.BookingTypes.Round_Trip.ToString().Replace("_", " ");
                                OutBoundBooking[i].TripFareTotal = Convert.ToDecimal(resGetRecord_Inbound[0].Faretotal + OutBoundBooking[i].Faretotal).ToString("0.00");
                                OutBoundBooking[i].TransactionId = Common.GetTransactionId_FromCustomerId(OutBoundBooking[i].BookingId, isPlasmaTech ? (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay : (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines);
                            }
                            else
                            {
                                OutBoundBooking[i].BookingType = AddFlightBookingDetails.BookingTypes.One_Way.ToString().Replace("_", " ");
                                OutBoundBooking[i].TripFareTotal = Convert.ToDecimal(OutBoundBooking[i].Faretotal).ToString("0.00");
                                OutBoundBooking[i].TransactionId = Common.GetTransactionId_FromCustomerId(OutBoundBooking[i].BookingId, isPlasmaTech ? (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay : (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_flight_airlines);
                            }
                        }
                    }

                    if (InboundBooking.Count > 0 && OutBoundBooking.Count > 0)
                    {
                        if (InboundBooking[0].ContactName.Length <= 0)
                        {
                            InboundBooking[0].ContactName = OutBoundBooking[0].ContactName;
                            InboundBooking[0].ContactPhone = OutBoundBooking[0].ContactPhone;
                            InboundBooking[0].ContactEmail = OutBoundBooking[0].ContactEmail;
                        }
                    }
                    msg = "success";
                    
                }
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
                msg = $"error: {ex.Message}";
            }
            return msg;
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