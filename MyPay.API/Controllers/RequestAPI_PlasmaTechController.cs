using iText.Kernel.Pdf.Canvas.Parser.ClipperLib;
using log4net;
using Microsoft.Ajax.Utilities;
using MyPay.API.Models;
using MyPay.API.Models.Airlines;
using MyPay.API.Models.Coupons;
using MyPay.API.Models.PlasmaTech;
using MyPay.API.Models.Request.PlasmaTech;
using MyPay.API.Models.Response.PlasmaTech;
using MyPay.API.Models.Response.Voting.Partner;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Get.KhanePani;
using MyPay.Models.Get.PlasmaAirlines;
using MyPay.Models.Miscellaneous;
using MyPay.Models.Response.WebResponse;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;
using System.Xml;
using static iTextSharp.text.pdf.AcroFields;
using static MyPay.API.Controllers.RequestAPI_PlasmaTechController;
using static MyPay.API.Models.Response.PlasmaTech.Res_Vendor_API_Pnr_Detail_Requests;
using static MyPay.Models.Add.AddUser;
using static MyPay.Models.Response.Plasma_Tech_Response_Model;
using static MyPay.Models.Response.WebResponse.WebRes_FlightSector;
using static MyPay.Repository.RepKhalti;
using CommonResponse = MyPay.API.Models.CommonResponse;
using FlightInbound = MyPay.API.Models.PlasmaTech.FlightInbound;
using FlightOutbound = MyPay.API.Models.PlasmaTech.FlightOutbound;
using FligthSectors = MyPay.API.Models.Airlines.FligthSectors;
using Passenger = MyPay.Models.Get.PlasmaAirlines.Passenger;

namespace MyPay.API.Controllers
{
    public class RequestAPI_PlasmaTechController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_PlasmaTechController));

        string ApiResponse = string.Empty;

        [HttpPost]
        [Route("api/lookup-sector-plasmatech")]
        public HttpResponseMessage GetLookupServicePlasmaTechSector(Req_Vendor_API_Airlines_Sector_Lookup_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-plasmatech sector" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_PlasmaTech_Sector_Requests result = new Res_Vendor_API_PlasmaTech_Sector_Requests();
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
            response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Sector_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                //if (Request.Headers.Authorization == null)
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
                    //string md5hash = Common.CheckHash(user);
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

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }


                        string msg = "";

                        string SectorCodes = RepKhalti.GetFlightSectorCode(user.UserID);



                        SectorCodes = SectorCodes.Replace("is_national", "IsNational");
                        SectorCodes = SectorCodes.Replace("is_international", "IsInternational");
                        SectorCodes = SectorCodes.Replace("is_active", "IsActive");
                        SectorCodes = SectorCodes.Replace("name", "Name");
                        SectorCodes = SectorCodes.Replace("code", "Code");

                        var sc = JsonConvert.DeserializeObject<List<FligthSectors>>(SectorCodes);
                        // var newSectors = new FligthSectors();


                        if (SectorCodes.Length > 0)
                        {
                            result.FligthSectors = sc;

                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Sector_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            msg = "Record not found or Something went wrong";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.SectorCode completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.SectorCode {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.SectorCode {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/plasma-check-balance")]
        public HttpResponseMessage GetLookupServiceCheckBalance(Req_Vendor_API_Check_Balance_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-check balance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Check_Balance_Requests result = new Res_Vendor_API_Check_Balance_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Check_Balance_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        PlasmaBalance objRes = new PlasmaBalance();
                        string msg = "";

                        MyPay.org.usbooking.dev.UnitedSolutionsService PlasmaService = new MyPay.org.usbooking.dev.UnitedSolutionsService();
                        string Balance = PlasmaService.CheckBalance(user.UserID, user.AirlineID);

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(Balance);

                        string JsonResponse = JsonConvert.SerializeXmlNode(doc);
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<PlasmaBalance>(JsonResponse);
                        if (objRes.Balance.Airline.BalanceAmount != null)
                        {
                            result.Data.Airline = objRes.Balance.Airline;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Check_Balance_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            msg = "Record not found or Something went wrong";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.CheckBalance completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.CheckBalance {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.CheckBalance {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/plasma-flight-availability")]
        public HttpResponseMessage GetServiceFlightAvailable(Req_Vendor_API_PlasmaTech_Flight_Available_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside PlasmaService-FlightAvailability" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_PlasmaTech_Flight_Available_Requests result = new Res_Vendor_API_PlasmaTech_Flight_Available_Requests();

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
            response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Available_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    //string md5hash = Common.CheckHash(user);
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

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        PlasmaAvailableFlights objRes = new PlasmaAvailableFlights();
                        string msg = "";

                        var flightSearch = RepKhalti.FlightAvailabilitySearch(user.UserID, user.Password, user.AgencyId, user.SectorFrom, user.SectorTo, user.FlightDate, user.ReturnDate, user.TripType, user.Nationality, user.Adult, user.Child, user.ClientIP);

                        if (flightSearch.Contains("FlightId"))
                        {
                            List<FlightOutbound> FlightOutbound = new List<FlightOutbound>();
                            List<FlightInbound> FlightInbound = new List<FlightInbound>();
                            var bookingId = long.Parse(DateTime.Now.ToString("ddHHmmssfff"));
                            if (user.TripType.ToLower() == "r") // Field TwoWay
                            {
                                if (!flightSearch.Contains("[") && !flightSearch.Contains("]"))
                                {


                                    // Parse the JSON string
                                    JObject jsonObject = JObject.Parse(flightSearch);

                                    // Extract the "Outbound" part
                                    JToken outboundToken = jsonObject["Flightavailability"]["Outbound"]["Availability"];

                                    JToken inboundToken = jsonObject["Flightavailability"]["Inbound"]["Availability"];


                                    JObject respp = new JObject
                                    {
                                        ["Flightavailability"] = new JObject
                                        {
                                            ["Outbound"] = new JObject
                                            {
                                                ["Availability"] = new JArray(outboundToken)
                                            },
                                            ["Inbound"] = new JObject
                                            {
                                                ["Availability"] = new JArray(inboundToken)
                                            }
                                        }
                                    };

                                    flightSearch = respp.ToString();
                                }
                                objRes = JsonConvert.DeserializeObject<PlasmaAvailableFlights>(flightSearch);
                                if (user.FlightDate != string.Empty && user.ReturnDate != string.Empty)
                                {
                                    for (int i = 0; i < objRes.Flightavailability.Inbound.Availability.Count; i++) // InBound Response Mapping
                                    {
                                        FlightInbound objFlightInbound = new FlightInbound();
                                        objFlightInbound.Airline = objRes.Flightavailability.Inbound.Availability[i].Airline;

                                        Dictionary<string, string> airlineNameMap = new Dictionary<string, string>
                                           {
                                               { "U4", "Buddha Airlines" },
                                               { "S1", "Saurya Airlines" },
                                               { "YT", "Yeti Airlines" },
                                               { "SHA", "Shree Airlines" },
                                               { "RMK", "Simrik Airline" },
                                               { "GA", "Goma Airlines" },
                                               { "ST", "Sita Airlines" }
                                           };

                                        if (airlineNameMap.ContainsKey(objFlightInbound.Airline))
                                        {
                                            objFlightInbound.Airlinename = airlineNameMap[objFlightInbound.Airline];
                                        }
                                        objFlightInbound.Airlinelogo = objRes.Flightavailability.Inbound.Availability[i].AirlineLogo;
                                        //objFlightInbound.Flightdate = objRes.Flightavailability.Inbound.Availability[i].FlightDate;

                                        var flightDate = objRes.Flightavailability.Inbound.Availability[i].FlightDate;

                                        DateTime date = DateTime.ParseExact(flightDate, "dd-MMM-yyyy", null);
                                        string Flight_Date = date.ToString("yyyy-MM-dd").ToUpper();
                                        objFlightInbound.Flightdate = Flight_Date;

                                        objFlightInbound.Flightno = objRes.Flightavailability.Inbound.Availability[i].FlightNo;
                                        objFlightInbound.Departure = objRes.Flightavailability.Inbound.Availability[i].Departure;
                                        objFlightInbound.Departuretime = objRes.Flightavailability.Inbound.Availability[i].DepartureTime;
                                        objFlightInbound.Arrival = objRes.Flightavailability.Inbound.Availability[i].Arrival;
                                        objFlightInbound.Arrivaltime = objRes.Flightavailability.Inbound.Availability[i].ArrivalTime;
                                        objFlightInbound.Aircrafttype = objRes.Flightavailability.Inbound.Availability[i].AircraftType;
                                        objFlightInbound.Adult = objRes.Flightavailability.Inbound.Availability[i].Adult;
                                        objFlightInbound.Child = objRes.Flightavailability.Inbound.Availability[i].Child;
                                        objFlightInbound.Infant = objRes.Flightavailability.Inbound.Availability[i].Infant;
                                        objFlightInbound.Flightid = objRes.Flightavailability.Inbound.Availability[i].FlightId;
                                        objFlightInbound.Flightclasscode = objRes.Flightavailability.Inbound.Availability[i].FlightClassCode;
                                        objFlightInbound.Currency = objRes.Flightavailability.Inbound.Availability[i].Currency;
                                        objFlightInbound.Adultfare = objRes.Flightavailability.Inbound.Availability[i].AdultFare;
                                        objFlightInbound.Childfare = objRes.Flightavailability.Inbound.Availability[i].ChildFare;
                                        objFlightInbound.Resfare = objRes.Flightavailability.Inbound.Availability[i].ResFare;
                                        objFlightInbound.Fuelsurcharge = objRes.Flightavailability.Inbound.Availability[i].FuelSurcharge;
                                        objFlightInbound.Tax = objRes.Flightavailability.Inbound.Availability[i].Tax;
                                        if (objRes.Flightavailability.Inbound.Availability[i].Refundable == "T")
                                        {
                                            objFlightInbound.Refundable = true;
                                        }
                                        else
                                        {
                                            objFlightInbound.Refundable = false;
                                        }

                                        var adultTotalFare = Convert.ToDecimal(objFlightInbound.Adultfare) * Convert.ToUInt32(user.Adult);
                                        var adultTotalSurcharge = Convert.ToDecimal(objFlightInbound.Fuelsurcharge) * Convert.ToUInt32(user.Adult);
                                        var adultTotalTax = Convert.ToDecimal(objFlightInbound.Tax) * Convert.ToUInt32(user.Adult);
                                        var childTotalFare = Convert.ToDecimal(objFlightInbound.Childfare) * Convert.ToUInt32(user.Child);
                                        var childTotalSurcharge = Convert.ToDecimal(objFlightInbound.Fuelsurcharge) * Convert.ToUInt32(user.Child);
                                        var childTotalTax = Convert.ToDecimal(objFlightInbound.Tax) * Convert.ToUInt32(user.Child);

                                        var adultTotalFee = Convert.ToDecimal(adultTotalFare) + Convert.ToDecimal(adultTotalSurcharge) + Convert.ToDecimal(adultTotalTax);
                                        var childTotalFee = Convert.ToDecimal(childTotalFare) + Convert.ToDecimal(childTotalSurcharge) + Convert.ToDecimal(childTotalTax);

                                        objFlightInbound.Faretotal = (Convert.ToDecimal(adultTotalFee) + Convert.ToDecimal(childTotalFee)).ToString();

                                        objFlightInbound.Freebaggage = objRes.Flightavailability.Inbound.Availability[i].FreeBaggage;
                                        objFlightInbound.Agencycommission = objRes.Flightavailability.Inbound.Availability[i].AgencyCommission;
                                        objFlightInbound.Childcommission = objRes.Flightavailability.Inbound.Availability[i].ChildCommission;
                                        objFlightInbound.Callingstationid = objRes.Flightavailability.Inbound.Availability[i].CallingStationId;
                                        objFlightInbound.Callingstation = objRes.Flightavailability.Inbound.Availability[i].CallingStation;

                                        string ServiceId = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay);
                                        AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId, objFlightInbound.Faretotal, ServiceId);
                                        objFlightInbound.Cashback = Convert.ToString(objOut.CashbackAmount);
                                        objFlightInbound.RewardPoints = Convert.ToString(objOut.RewardPoints);
                                        objFlightInbound.ServiceCharge = Convert.ToString(objOut.ServiceCharge);

                                        FlightInbound.Add(objFlightInbound);


                                        //NEW CODE
                                        List<AddFlightBookingDetails> resFlightDbInsertList_Inbound = new List<AddFlightBookingDetails>();
                                        AddFlightBookingDetails res = new AddFlightBookingDetails();
                                        res.BookingId = bookingId;

                                        res.MemberId = Convert.ToInt64(user.MemberId);
                                        res.TripType = user.TripType;
                                        res.FlightType = "D"; //user.FlightType;
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
                                        res.Freebaggage = objFlightInbound.Freebaggage + " KG";
                                        res.Arrival = objFlightInbound.Arrival;
                                        res.Flightid = objFlightInbound.Flightid;
                                        res.Flightdate = Convert.ToDateTime(objFlightInbound.Flightdate);
                                        res.CreatedDate = System.DateTime.UtcNow;
                                        res.UpdatedDate = System.DateTime.UtcNow;
                                        res.IsActive = true;
                                        res.IsDeleted = false;
                                        res.IsApprovedByAdmin = true;
                                        res.CreatedBy = 10000;//Common.GetCreatedById(authenticationToken);
                                        res.CreatedByName = "Admin"; //Common.GetCreatedByName(authenticationToken);
                                        res.IsFlightIssued = false;
                                        res.IpAddress = Common.GetUserIP();
                                        res.SectorFrom = objFlightInbound.Departure; //user.ToArrival;
                                        res.SectorTo = objFlightInbound.Arrival;//user.FromDeparture;
                                        res.ReturnDate = user.ReturnDate;
                                        res.Faretotal = Convert.ToDecimal(objFlightInbound.Faretotal);
                                        resFlightDbInsertList_Inbound.Add(res);
                                        Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Inbound, "flightbookingdetails");

                                        //if (resFlightDbInsertList_Inbound.Count > 0)
                                        //{
                                        //    Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Inbound, "flightbookingdetails");
                                        //    if (Id > 0)
                                        //    {
                                        //        Common.AddLogs($"Added Inbound Flight Detail For Booking ID {resFlightDbInsertList_Inbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.ApiRequests);
                                        //    }
                                        //    else
                                        //    {
                                        //        Common.AddLogs($"Added Inbound Flight Detail Failed For Booking ID {resFlightDbInsertList_Inbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.ApiRequests);
                                        //    }
                                        //}
                                        //NEW CODE
                                    }
                                    for (int i = 0; i < objRes.Flightavailability.Outbound.Availability.Count; i++) // OutBount Response Mapping
                                    {
                                        FlightOutbound objFlightOutbound = new FlightOutbound();
                                        objFlightOutbound.Airline = objRes.Flightavailability.Outbound.Availability[i].Airline;

                                        Dictionary<string, string> airlineNameMap = new Dictionary<string, string>
                                           {
                                               { "U4", "Buddha Airlines" },
                                               { "S1", "Saurya Airlines" },
                                               { "YT", "Yeti Airlines" },
                                               { "SHA", "Shree Airlines" },
                                               { "RMK", "Simrik Airline" },
                                               { "GA", "Goma Airlines" },
                                               { "ST", "Sita Airlines" }
                                           };

                                        if (airlineNameMap.ContainsKey(objFlightOutbound.Airline))
                                        {
                                            objFlightOutbound.Airlinename = airlineNameMap[objFlightOutbound.Airline];
                                        }
                                        objFlightOutbound.Airlinelogo = objRes.Flightavailability.Outbound.Availability[i].AirlineLogo;
                                        var flightDate = objRes.Flightavailability.Outbound.Availability[i].FlightDate;

                                        DateTime date = DateTime.ParseExact(flightDate, "dd-MMM-yyyy", null);
                                        string Flight_Date = date.ToString("yyyy-MM-dd").ToUpper();
                                        objFlightOutbound.Flightdate = Flight_Date;

                                        objFlightOutbound.Flightno = objRes.Flightavailability.Outbound.Availability[i].FlightNo;
                                        objFlightOutbound.Departure = objRes.Flightavailability.Outbound.Availability[i].Departure;
                                        objFlightOutbound.Departuretime = objRes.Flightavailability.Outbound.Availability[i].DepartureTime;
                                        objFlightOutbound.Arrival = objRes.Flightavailability.Outbound.Availability[i].Arrival;
                                        objFlightOutbound.Arrivaltime = objRes.Flightavailability.Outbound.Availability[i].ArrivalTime;
                                        objFlightOutbound.Aircrafttype = objRes.Flightavailability.Outbound.Availability[i].AircraftType;
                                        objFlightOutbound.Adult = objRes.Flightavailability.Outbound.Availability[i].Adult;
                                        objFlightOutbound.Child = objRes.Flightavailability.Outbound.Availability[i].Child;
                                        objFlightOutbound.Infant = objRes.Flightavailability.Outbound.Availability[i].Infant;
                                        objFlightOutbound.Flightid = objRes.Flightavailability.Outbound.Availability[i].FlightId;
                                        objFlightOutbound.Flightclasscode = objRes.Flightavailability.Outbound.Availability[i].FlightClassCode;
                                        objFlightOutbound.Currency = objRes.Flightavailability.Outbound.Availability[i].Currency;
                                        objFlightOutbound.Adultfare = objRes.Flightavailability.Outbound.Availability[i].AdultFare;
                                        objFlightOutbound.Childfare = objRes.Flightavailability.Outbound.Availability[i].ChildFare;
                                        objFlightOutbound.Resfare = objRes.Flightavailability.Outbound.Availability[i].ResFare;
                                        objFlightOutbound.Fuelsurcharge = objRes.Flightavailability.Outbound.Availability[i].FuelSurcharge;
                                        objFlightOutbound.Tax = objRes.Flightavailability.Outbound.Availability[i].Tax;
                                        if (objRes.Flightavailability.Outbound.Availability[i].Refundable == "T")
                                        {
                                            objFlightOutbound.Refundable = true;
                                        }
                                        else
                                        {
                                            objFlightOutbound.Refundable = false;
                                        }

                                        var adultTotalFare = Convert.ToDecimal(objFlightOutbound.Adultfare) * Convert.ToUInt32(user.Adult);
                                        var adultTotalSurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge) * Convert.ToUInt32(user.Adult);
                                        var adultTotalTax = Convert.ToDecimal(objFlightOutbound.Tax) * Convert.ToUInt32(user.Adult);
                                        var childTotalFare = Convert.ToDecimal(objFlightOutbound.Childfare) * Convert.ToUInt32(user.Child);
                                        var childTotalSurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge) * Convert.ToUInt32(user.Child);
                                        var childTotalTax = Convert.ToDecimal(objFlightOutbound.Tax) * Convert.ToUInt32(user.Child);

                                        var adultTotalFee = Convert.ToDecimal(adultTotalFare) + Convert.ToDecimal(adultTotalSurcharge) + Convert.ToDecimal(adultTotalTax);
                                        var childTotalFee = Convert.ToDecimal(childTotalFare) + Convert.ToDecimal(childTotalSurcharge) + Convert.ToDecimal(childTotalTax);

                                        objFlightOutbound.Faretotal = (Convert.ToDecimal(adultTotalFee) + Convert.ToDecimal(childTotalFee)).ToString();

                                        objFlightOutbound.Freebaggage = objRes.Flightavailability.Outbound.Availability[i].FreeBaggage;
                                        objFlightOutbound.Agencycommission = objRes.Flightavailability.Outbound.Availability[i].AgencyCommission;
                                        objFlightOutbound.Childcommission = objRes.Flightavailability.Outbound.Availability[i].ChildCommission;
                                        objFlightOutbound.Callingstationid = objRes.Flightavailability.Outbound.Availability[i].CallingStationId;
                                        objFlightOutbound.Callingstation = objRes.Flightavailability.Outbound.Availability[i].CallingStation;

                                        string ServiceId = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay);
                                        AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId, objFlightOutbound.Faretotal, ServiceId);
                                        objFlightOutbound.Cashback = Convert.ToString(objOut.CashbackAmount);
                                        objFlightOutbound.RewardPoints = Convert.ToString(objOut.RewardPoints);
                                        objFlightOutbound.ServiceCharge = Convert.ToString(objOut.ServiceCharge);

                                        FlightOutbound.Add(objFlightOutbound);

                                        //NEW CODE
                                        List<AddFlightBookingDetails> resFlightDbInsertList_Outbound = new List<AddFlightBookingDetails>();
                                        AddFlightBookingDetails res = new AddFlightBookingDetails();
                                        res.BookingId = bookingId;
                                        res.MemberId = Convert.ToInt64(user.MemberId);
                                        res.TripType = user.TripType;
                                        res.FlightType = "D"; //user.FlightType;
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
                                        res.Freebaggage = objFlightOutbound.Freebaggage + " KG";
                                        res.Arrival = objFlightOutbound.Arrival;
                                        res.Flightid = objFlightOutbound.Flightid;
                                        res.Flightdate = Convert.ToDateTime(objFlightOutbound.Flightdate);
                                        res.CreatedDate = System.DateTime.UtcNow;
                                        res.UpdatedDate = System.DateTime.UtcNow;
                                        res.IsActive = true;
                                        res.IsDeleted = false;
                                        res.IsApprovedByAdmin = true;
                                        res.CreatedBy = 10000;//Common.GetCreatedById(authenticationToken);
                                        res.CreatedByName = "Admin"; //Common.GetCreatedByName(authenticationToken);
                                        res.IsFlightIssued = false;
                                        res.IpAddress = Common.GetUserIP();
                                        res.SectorFrom = objFlightOutbound.Departure; //user.FromDeparture;
                                        res.SectorTo = objFlightOutbound.Arrival; //user.ToArrival;
                                        res.ReturnDate = user.ReturnDate;
                                        res.Faretotal = Convert.ToDecimal(objFlightOutbound.Faretotal);
                                        resFlightDbInsertList_Outbound.Add(res);
                                        Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");

                                        //if (resFlightDbInsertList_Outbound.Count > 0)
                                        //{
                                        //    Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");
                                        //    if (Id > 0)
                                        //    {
                                        //        Common.AddLogs($"Added Outbound Flight Detail For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
                                        //    }
                                        //    else
                                        //    {
                                        //        Common.AddLogs($"Added Outbound Flight Detail Failed For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
                                        //    }
                                        //}
                                        //NEW CODE

                                    }

                                }
                            }
                            else if (user.TripType.ToLower() == "o") // Field OneWay
                            {
                                if (!flightSearch.Contains("[") && !flightSearch.Contains("]"))
                                {
                                    JArray combinedArray = new JArray();

                                    // Parse the JSON string
                                    JObject jsonObject = JObject.Parse(flightSearch);

                                    // Extract the "Outbound" part
                                    JToken outboundToken = jsonObject["Flightavailability"]["Outbound"]["Availability"];


                                    JObject respp = new JObject
                                    {
                                        ["Flightavailability"] = new JObject
                                        {
                                            ["Outbound"] = new JObject
                                            {
                                                ["Availability"] = new JArray(outboundToken)
                                            }
                                        }
                                    };


                                    flightSearch = respp.ToString();

                                }




                                objRes = JsonConvert.DeserializeObject<PlasmaAvailableFlights>(flightSearch);
                                for (int i = 0; i < objRes.Flightavailability.Outbound.Availability.Count; i++)// OutBount Response Mapping
                                {
                                    FlightOutbound objFlightOutbound = new FlightOutbound();

                                    objFlightOutbound.Airline = objRes.Flightavailability.Outbound.Availability[i].Airline;
                                    Dictionary<string, string> airlineNameMap = new Dictionary<string, string>
                                           {
                                               { "U4", "Buddha Airlines" },
                                               { "S1", "Saurya Airlines" },
                                               { "YT", "Yeti Airlines" },
                                               { "SHA", "Shree Airlines" },
                                               { "RMK", "Simrik Airline" },
                                               { "GA", "Goma Airlines" },
                                               { "ST", "Sita Air" }
                                           };

                                    if (airlineNameMap.ContainsKey(objFlightOutbound.Airline))
                                    {
                                        objFlightOutbound.Airlinename = airlineNameMap[objFlightOutbound.Airline];
                                    }

                                    objFlightOutbound.Airlinelogo = objRes.Flightavailability.Outbound.Availability[i].AirlineLogo;
                                    var flightDate = objRes.Flightavailability.Outbound.Availability[i].FlightDate;
                                    DateTime date = DateTime.ParseExact(flightDate, "dd-MMM-yyyy", null);
                                    string Flight_Date = date.ToString("yyyy-MM-dd").ToUpper();

                                    objFlightOutbound.Flightdate = Flight_Date;
                                    objFlightOutbound.Flightno = objRes.Flightavailability.Outbound.Availability[i].FlightNo;
                                    objFlightOutbound.Departure = objRes.Flightavailability.Outbound.Availability[i].Departure;
                                    objFlightOutbound.Departuretime = objRes.Flightavailability.Outbound.Availability[i].DepartureTime;
                                    objFlightOutbound.Arrival = objRes.Flightavailability.Outbound.Availability[i].Arrival;
                                    objFlightOutbound.Arrivaltime = objRes.Flightavailability.Outbound.Availability[i].ArrivalTime;
                                    objFlightOutbound.Aircrafttype = objRes.Flightavailability.Outbound.Availability[i].AircraftType;
                                    objFlightOutbound.Adult = objRes.Flightavailability.Outbound.Availability[i].Adult;
                                    objFlightOutbound.Child = objRes.Flightavailability.Outbound.Availability[i].Child;
                                    objFlightOutbound.Infant = objRes.Flightavailability.Outbound.Availability[i].Infant;
                                    objFlightOutbound.Flightid = objRes.Flightavailability.Outbound.Availability[i].FlightId;
                                    objFlightOutbound.Flightclasscode = objRes.Flightavailability.Outbound.Availability[i].FlightClassCode;
                                    objFlightOutbound.Currency = objRes.Flightavailability.Outbound.Availability[i].Currency;
                                    objFlightOutbound.Adultfare = objRes.Flightavailability.Outbound.Availability[i].AdultFare;
                                    objFlightOutbound.Childfare = objRes.Flightavailability.Outbound.Availability[i].ChildFare;
                                    objFlightOutbound.Fuelsurcharge = objRes.Flightavailability.Outbound.Availability[i].FuelSurcharge;
                                    objFlightOutbound.Tax = objRes.Flightavailability.Outbound.Availability[i].Tax;
                                    if (objRes.Flightavailability.Outbound.Availability[i].Refundable == "T")
                                    {
                                        objFlightOutbound.Refundable = true;
                                    }
                                    else
                                    {
                                        objFlightOutbound.Refundable = false;
                                    }
                                    var adultTotalFare = Convert.ToDecimal(objFlightOutbound.Adultfare) * Convert.ToUInt32(user.Adult);
                                    var adultTotalSurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge) * Convert.ToUInt32(user.Adult);
                                    var adultTotalTax = Convert.ToDecimal(objFlightOutbound.Tax) * Convert.ToUInt32(user.Adult);
                                    var childTotalFare = Convert.ToDecimal(objFlightOutbound.Childfare) * Convert.ToUInt32(user.Child);
                                    var childTotalSurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge) * Convert.ToUInt32(user.Child);
                                    var childTotalTax = Convert.ToDecimal(objFlightOutbound.Tax) * Convert.ToUInt32(user.Child);

                                    var adultTotalFee = Convert.ToDecimal(adultTotalFare) + Convert.ToDecimal(adultTotalSurcharge) + Convert.ToDecimal(adultTotalTax);
                                    var childTotalFee = Convert.ToDecimal(childTotalFare) + Convert.ToDecimal(childTotalSurcharge) + Convert.ToDecimal(childTotalTax);

                                    objFlightOutbound.Faretotal = (Convert.ToDecimal(adultTotalFee) + Convert.ToDecimal(childTotalFee)).ToString();

                                    objFlightOutbound.Resfare = objRes.Flightavailability.Outbound.Availability[i].ResFare;
                                    objFlightOutbound.Freebaggage = objRes.Flightavailability.Outbound.Availability[i].FreeBaggage;
                                    objFlightOutbound.Agencycommission = objRes.Flightavailability.Outbound.Availability[i].AgencyCommission;
                                    objFlightOutbound.Childcommission = objRes.Flightavailability.Outbound.Availability[i].ChildCommission;
                                    objFlightOutbound.Callingstationid = objRes.Flightavailability.Outbound.Availability[i].CallingStationId;
                                    objFlightOutbound.Callingstation = objRes.Flightavailability.Outbound.Availability[i].CallingStation;

                                    string ServiceId = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay);
                                    AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId, objFlightOutbound.Faretotal, ServiceId);
                                    objFlightOutbound.Cashback = Convert.ToString(objOut.CashbackAmount);
                                    objFlightOutbound.RewardPoints = Convert.ToString(objOut.RewardPoints);
                                    objFlightOutbound.ServiceCharge = Convert.ToString(objOut.ServiceCharge);
                                    FlightOutbound.Add(objFlightOutbound);

                                    //NEW CODE
                                    List<AddFlightBookingDetails> resFlightDbInsertList_Outbound = new List<AddFlightBookingDetails>();
                                    AddFlightBookingDetails res = new AddFlightBookingDetails();

                                    res.BookingId = bookingId;
                                    res.MemberId = Convert.ToInt64(user.MemberId);
                                    res.TripType = user.TripType;
                                    res.FlightType = "D"; //user.FlightType;
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
                                    res.Freebaggage = objFlightOutbound.Freebaggage + " KG";
                                    res.Arrival = objFlightOutbound.Arrival;
                                    res.Flightid = objFlightOutbound.Flightid;
                                    res.Flightdate = Convert.ToDateTime(objFlightOutbound.Flightdate);
                                    res.CreatedDate = System.DateTime.UtcNow;
                                    res.UpdatedDate = System.DateTime.UtcNow;
                                    res.IsActive = true;
                                    res.IsDeleted = false;
                                    res.IsApprovedByAdmin = true;
                                    res.CreatedBy = 10000;
                                    res.CreatedByName = "Admin";
                                    res.IsFlightIssued = false;
                                    res.IpAddress = Common.GetUserIP();
                                    res.SectorFrom = objFlightOutbound.Departure;
                                    res.SectorTo = objFlightOutbound.Arrival;
                                    res.ReturnDate = user.ReturnDate;
                                    resFlightDbInsertList_Outbound.Add(res);
                                    Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");

                                    //if (resFlightDbInsertList_Outbound.Count > 0)
                                    //{
                                    //    Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");
                                    //    if (Id > 0)
                                    //    {
                                    //        Common.AddLogs($"Added Outbound Flight Detail For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
                                    //    }
                                    //    else
                                    //    {
                                    //        Common.AddLogs($"Added Outbound Flight Detail Failed For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
                                    //    }
                                    //}
                                    //NEW CODE
                                }
                            }
                            else
                            {
                                result.Message = "Record not found or Something went wrong";
                            }
                            //result.Data = objRes.Flightavailability;
                            FlightOutbound.Sort((x, y) => Convert.ToDecimal(x.Faretotal).CompareTo(Convert.ToDecimal(y.Faretotal)));
                            FlightInbound.Sort((x, y) => Convert.ToDecimal(x.Faretotal).CompareTo(Convert.ToDecimal(y.Faretotal)));

                            result.FlightOutbound = FlightOutbound;
                            result.FlightInbound = FlightInbound;
                            result.BookingId = bookingId;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Available_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            msg = "Record not found or Something went wrong";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.FlightAvailability completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.FlightAvailability {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.FlightAvailability {ex.ToString()}  ");
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //public HttpResponseMessage GetServiceFlightAvailable(Req_Vendor_API_PlasmaTech_Flight_Available_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside PlasmaService-FlightAvailability" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_PlasmaTech_Flight_Available_Requests result = new Res_Vendor_API_PlasmaTech_Flight_Available_Requests();

        //    HttpResponseMessage response;

        //    HttpRequestMessage request;

        //    if (requestPassed != null)
        //    {
        //        request = requestPassed;
        //    }
        //    else
        //    {
        //        request = Request;
        //    }
        //    response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Available_Requests>(System.Net.HttpStatusCode.BadRequest, result);

        //    string userInput;

        //    if (userInputPassed != null)
        //    {
        //        userInput = userInputPassed;
        //    }
        //    else
        //    {
        //        userInput = getRawPostData().Result;
        //    }
        //    try
        //    {
        //        if (request.Headers.Authorization == null)
        //        {
        //            string results = "Un-Authorized Request";
        //            cres = CommonApiMethod.ReturnBadRequestMessage(results);
        //            response.StatusCode = HttpStatusCode.BadRequest;
        //            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //            return response;
        //        }
        //        else
        //        {
        //            //string md5hash = Common.CheckHash(user);
        //            string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

        //            string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
        //            if (results != "Success")
        //            {
        //                cres = CommonApiMethod.ReturnBadRequestMessage(results);
        //                response.StatusCode = HttpStatusCode.BadRequest;
        //                response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
        //                    response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }

        //                PlasmaAvailableFlights objRes = new PlasmaAvailableFlights();
        //                string msg = "";

        //                var flightSearch = RepKhalti.FlightAvailabilitySearch(user.UserID, user.Password, user.AgencyId, user.SectorFrom, user.SectorTo, user.FlightDate, user.ReturnDate, user.TripType, user.Nationality, user.Adult, user.Child, user.ClientIP);

        //                if (flightSearch.Contains("FlightId"))
        //                {
        //                    List<FlightOutbound> FlightOutbound = new List<FlightOutbound>();
        //                    List<FlightInbound> FlightInbound = new List<FlightInbound>();
        //                    var bookingId = long.Parse(DateTime.Now.ToString("ddHHmmssfff"));
        //                    if (user.TripType.ToLower() == "r") // Field TwoWay
        //                    {
        //                        objRes = JsonConvert.DeserializeObject<PlasmaAvailableFlights>(flightSearch);
        //                        if (user.FlightDate != string.Empty && user.ReturnDate != string.Empty)
        //                        {
        //                            for (int i = 0; i < objRes.Flightavailability.Inbound.Availability.Count; i++) // InBound Response Mapping
        //                            {
        //                                FlightInbound objFlightInbound = new FlightInbound();
        //                                objFlightInbound.Airline = objRes.Flightavailability.Inbound.Availability[i].Airline;

        //                                Dictionary<string, string> airlineNameMap = new Dictionary<string, string>
        //                                    {
        //                                        { "U4", "Buddha Airlines" },
        //                                        { "S1", "Saurya Airlines" },
        //                                        { "YT", "Yeti Airlines" },
        //                                        { "SHA", "Shree Airlines" },
        //                                        { "RMK", "Simrik Airline" },
        //                                        { "GA", "Goma Airlines" },
        //                                        { "ST", "Sita Airlines" }
        //                                    };

        //                                if (airlineNameMap.ContainsKey(objFlightInbound.Airline))
        //                                {
        //                                    objFlightInbound.Airlinename = airlineNameMap[objFlightInbound.Airline];
        //                                }
        //                                objFlightInbound.Airlinelogo = objRes.Flightavailability.Inbound.Availability[i].AirlineLogo;
        //                                //objFlightInbound.Flightdate = objRes.Flightavailability.Inbound.Availability[i].FlightDate;

        //                                var flightDate = objRes.Flightavailability.Inbound.Availability[i].FlightDate;

        //                                DateTime date = DateTime.ParseExact(flightDate, "dd-MMM-yyyy", null);
        //                                string Flight_Date = date.ToString("yyyy-MM-dd").ToUpper();
        //                                objFlightInbound.Flightdate = Flight_Date;

        //                                objFlightInbound.Flightno = objRes.Flightavailability.Inbound.Availability[i].FlightNo;
        //                                objFlightInbound.Departure = objRes.Flightavailability.Inbound.Availability[i].Departure;
        //                                objFlightInbound.Departuretime = objRes.Flightavailability.Inbound.Availability[i].DepartureTime;
        //                                objFlightInbound.Arrival = objRes.Flightavailability.Inbound.Availability[i].Arrival;
        //                                objFlightInbound.Arrivaltime = objRes.Flightavailability.Inbound.Availability[i].ArrivalTime;
        //                                objFlightInbound.Aircrafttype = objRes.Flightavailability.Inbound.Availability[i].AircraftType;
        //                                objFlightInbound.Adult = objRes.Flightavailability.Inbound.Availability[i].Adult;
        //                                objFlightInbound.Child = objRes.Flightavailability.Inbound.Availability[i].Child;
        //                                objFlightInbound.Infant = objRes.Flightavailability.Inbound.Availability[i].Infant;
        //                                objFlightInbound.Flightid = objRes.Flightavailability.Inbound.Availability[i].FlightId;
        //                                objFlightInbound.Flightclasscode = objRes.Flightavailability.Inbound.Availability[i].FlightClassCode;
        //                                objFlightInbound.Currency = objRes.Flightavailability.Inbound.Availability[i].Currency;
        //                                objFlightInbound.Adultfare = objRes.Flightavailability.Inbound.Availability[i].AdultFare;
        //                                objFlightInbound.Childfare = objRes.Flightavailability.Inbound.Availability[i].ChildFare;
        //                                objFlightInbound.Resfare = objRes.Flightavailability.Inbound.Availability[i].ResFare;
        //                                objFlightInbound.Fuelsurcharge = objRes.Flightavailability.Inbound.Availability[i].FuelSurcharge;
        //                                objFlightInbound.Tax = objRes.Flightavailability.Inbound.Availability[i].Tax;
        //                                if (objRes.Flightavailability.Inbound.Availability[i].Refundable == "T")
        //                                {
        //                                    objFlightInbound.Refundable = true;
        //                                }
        //                                else
        //                                {
        //                                    objFlightInbound.Refundable = false;
        //                                }

        //                                var adultTotalFare = Convert.ToDecimal(objFlightInbound.Adultfare) * Convert.ToUInt32(user.Adult);
        //                                var adultTotalSurcharge = Convert.ToDecimal(objFlightInbound.Fuelsurcharge) * Convert.ToUInt32(user.Adult);
        //                                var adultTotalTax = Convert.ToDecimal(objFlightInbound.Tax) * Convert.ToUInt32(user.Adult);
        //                                var childTotalFare = Convert.ToDecimal(objFlightInbound.Childfare) * Convert.ToUInt32(user.Child);
        //                                var childTotalSurcharge = Convert.ToDecimal(objFlightInbound.Fuelsurcharge) * Convert.ToUInt32(user.Child);
        //                                var childTotalTax = Convert.ToDecimal(objFlightInbound.Tax) * Convert.ToUInt32(user.Child);

        //                                var adultTotalFee = Convert.ToDecimal(adultTotalFare) + Convert.ToDecimal(adultTotalSurcharge) + Convert.ToDecimal(adultTotalTax);
        //                                var childTotalFee = Convert.ToDecimal(childTotalFare) + Convert.ToDecimal(childTotalSurcharge) + Convert.ToDecimal(childTotalTax);

        //                                objFlightInbound.Faretotal = (Convert.ToDecimal(adultTotalFee) + Convert.ToDecimal(childTotalFee)).ToString();

        //                                objFlightInbound.Freebaggage = objRes.Flightavailability.Inbound.Availability[i].FreeBaggage;
        //                                objFlightInbound.Agencycommission = objRes.Flightavailability.Inbound.Availability[i].AgencyCommission;
        //                                objFlightInbound.Childcommission = objRes.Flightavailability.Inbound.Availability[i].ChildCommission;
        //                                objFlightInbound.Callingstationid = objRes.Flightavailability.Inbound.Availability[i].CallingStationId;
        //                                objFlightInbound.Callingstation = objRes.Flightavailability.Inbound.Availability[i].CallingStation;

        //                                string ServiceId = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay);
        //                                AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId, objFlightInbound.Faretotal, ServiceId);
        //                                objFlightInbound.Cashback = Convert.ToString(objOut.CashbackAmount);
        //                                objFlightInbound.RewardPoints = Convert.ToString(objOut.RewardPoints);
        //                                objFlightInbound.ServiceCharge = Convert.ToString(objOut.ServiceCharge);

        //                                FlightInbound.Add(objFlightInbound);


        //                                //NEW CODE
        //                                List<AddFlightBookingDetails> resFlightDbInsertList_Inbound = new List<AddFlightBookingDetails>();
        //                                AddFlightBookingDetails res = new AddFlightBookingDetails();
        //                                res.BookingId = bookingId;

        //                                res.MemberId = Convert.ToInt64(user.MemberId);
        //                                res.TripType = user.TripType;
        //                                res.FlightType = "D"; //user.FlightType;
        //                                res.Adult = Convert.ToInt32(objFlightInbound.Adult);
        //                                res.Child = Convert.ToInt32(objFlightInbound.Child);
        //                                res.IsInbound = true;
        //                                res.Aircrafttype = objFlightInbound.Aircrafttype;
        //                                res.Airlinename = objFlightInbound.Airlinename;
        //                                res.Departure = objFlightInbound.Departure;
        //                                res.Refundable = objFlightInbound.Refundable;
        //                                res.Infantfare = Convert.ToDecimal(objFlightInbound.Infantfare);
        //                                res.Flightclasscode = objFlightInbound.Flightclasscode;
        //                                res.Currency = objFlightInbound.Currency;
        //                                res.Faretotal = Convert.ToDecimal(objFlightInbound.Faretotal);
        //                                res.Adultfare = Convert.ToDecimal(objFlightInbound.Adultfare);
        //                                res.Childfare = Convert.ToDecimal(objFlightInbound.Childfare);
        //                                res.Departuretime = objFlightInbound.Departuretime;
        //                                res.Tax = Convert.ToDecimal(objFlightInbound.Tax);
        //                                res.Airline = objFlightInbound.Airline;
        //                                res.Airlinelogo = objFlightInbound.Airlinelogo;
        //                                res.Flightno = objFlightInbound.Flightno;
        //                                res.Fuelsurcharge = Convert.ToDecimal(objFlightInbound.Fuelsurcharge);
        //                                res.Arrivaltime = objFlightInbound.Arrivaltime;
        //                                res.Resfare = Convert.ToDecimal(objFlightInbound.Resfare);
        //                                res.Freebaggage = objFlightInbound.Freebaggage + " KG";
        //                                res.Arrival = objFlightInbound.Arrival;
        //                                res.Flightid = objFlightInbound.Flightid;
        //                                res.Flightdate = Convert.ToDateTime(objFlightInbound.Flightdate);
        //                                res.CreatedDate = System.DateTime.UtcNow;
        //                                res.UpdatedDate = System.DateTime.UtcNow;
        //                                res.IsActive = true;
        //                                res.IsDeleted = false;
        //                                res.IsApprovedByAdmin = true;
        //                                res.CreatedBy = 10000;//Common.GetCreatedById(authenticationToken);
        //                                res.CreatedByName = "Admin"; //Common.GetCreatedByName(authenticationToken);
        //                                res.IsFlightIssued = false;
        //                                res.IpAddress = Common.GetUserIP();
        //                                res.SectorFrom = objFlightInbound.Departure; //user.ToArrival;
        //                                res.SectorTo = objFlightInbound.Arrival;//user.FromDeparture;
        //                                res.ReturnDate = user.ReturnDate;
        //                                res.Faretotal = Convert.ToDecimal(objFlightInbound.Faretotal);
        //                                resFlightDbInsertList_Inbound.Add(res);
        //                                Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Inbound, "flightbookingdetails");

        //                                //if (resFlightDbInsertList_Inbound.Count > 0)
        //                                //{
        //                                //    Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Inbound, "flightbookingdetails");
        //                                //    if (Id > 0)
        //                                //    {
        //                                //        Common.AddLogs($"Added Inbound Flight Detail For Booking ID {resFlightDbInsertList_Inbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.ApiRequests);
        //                                //    }
        //                                //    else
        //                                //    {
        //                                //        Common.AddLogs($"Added Inbound Flight Detail Failed For Booking ID {resFlightDbInsertList_Inbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.ApiRequests);
        //                                //    }
        //                                //}
        //                                //NEW CODE
        //                            }
        //                            for (int i = 0; i < objRes.Flightavailability.Outbound.Availability.Count; i++) // OutBount Response Mapping
        //                            {
        //                                FlightOutbound objFlightOutbound = new FlightOutbound();
        //                                objFlightOutbound.Airline = objRes.Flightavailability.Outbound.Availability[i].Airline;

        //                                Dictionary<string, string> airlineNameMap = new Dictionary<string, string>
        //                                    {
        //                                        { "U4", "Buddha Airlines" },
        //                                        { "S1", "Saurya Airlines" },
        //                                        { "YT", "Yeti Airlines" },
        //                                        { "SHA", "Shree Airlines" },
        //                                        { "RMK", "Simrik Airline" },
        //                                        { "GA", "Goma Airlines" },
        //                                        { "ST", "Sita Airlines" }
        //                                    };

        //                                if (airlineNameMap.ContainsKey(objFlightOutbound.Airline))
        //                                {
        //                                    objFlightOutbound.Airlinename = airlineNameMap[objFlightOutbound.Airline];
        //                                }
        //                                objFlightOutbound.Airlinelogo = objRes.Flightavailability.Outbound.Availability[i].AirlineLogo;
        //                                var flightDate = objRes.Flightavailability.Outbound.Availability[i].FlightDate;

        //                                DateTime date = DateTime.ParseExact(flightDate, "dd-MMM-yyyy", null);
        //                                string Flight_Date = date.ToString("yyyy-MM-dd").ToUpper();
        //                                objFlightOutbound.Flightdate = Flight_Date;

        //                                objFlightOutbound.Flightno = objRes.Flightavailability.Outbound.Availability[i].FlightNo;
        //                                objFlightOutbound.Departure = objRes.Flightavailability.Outbound.Availability[i].Departure;
        //                                objFlightOutbound.Departuretime = objRes.Flightavailability.Outbound.Availability[i].DepartureTime;
        //                                objFlightOutbound.Arrival = objRes.Flightavailability.Outbound.Availability[i].Arrival;
        //                                objFlightOutbound.Arrivaltime = objRes.Flightavailability.Outbound.Availability[i].ArrivalTime;
        //                                objFlightOutbound.Aircrafttype = objRes.Flightavailability.Outbound.Availability[i].AircraftType;
        //                                objFlightOutbound.Adult = objRes.Flightavailability.Outbound.Availability[i].Adult;
        //                                objFlightOutbound.Child = objRes.Flightavailability.Outbound.Availability[i].Child;
        //                                objFlightOutbound.Infant = objRes.Flightavailability.Outbound.Availability[i].Infant;
        //                                objFlightOutbound.Flightid = objRes.Flightavailability.Outbound.Availability[i].FlightId;
        //                                objFlightOutbound.Flightclasscode = objRes.Flightavailability.Outbound.Availability[i].FlightClassCode;
        //                                objFlightOutbound.Currency = objRes.Flightavailability.Outbound.Availability[i].Currency;
        //                                objFlightOutbound.Adultfare = objRes.Flightavailability.Outbound.Availability[i].AdultFare;
        //                                objFlightOutbound.Childfare = objRes.Flightavailability.Outbound.Availability[i].ChildFare;
        //                                objFlightOutbound.Resfare = objRes.Flightavailability.Outbound.Availability[i].ResFare;
        //                                objFlightOutbound.Fuelsurcharge = objRes.Flightavailability.Outbound.Availability[i].FuelSurcharge;
        //                                objFlightOutbound.Tax = objRes.Flightavailability.Outbound.Availability[i].Tax;
        //                                if (objRes.Flightavailability.Outbound.Availability[i].Refundable == "T")
        //                                {
        //                                    objFlightOutbound.Refundable = true;
        //                                }
        //                                else
        //                                {
        //                                    objFlightOutbound.Refundable = false;
        //                                }

        //                                var adultTotalFare = Convert.ToDecimal(objFlightOutbound.Adultfare) * Convert.ToUInt32(user.Adult);
        //                                var adultTotalSurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge) * Convert.ToUInt32(user.Adult);
        //                                var adultTotalTax = Convert.ToDecimal(objFlightOutbound.Tax) * Convert.ToUInt32(user.Adult);
        //                                var childTotalFare = Convert.ToDecimal(objFlightOutbound.Childfare) * Convert.ToUInt32(user.Child);
        //                                var childTotalSurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge) * Convert.ToUInt32(user.Child);
        //                                var childTotalTax = Convert.ToDecimal(objFlightOutbound.Tax) * Convert.ToUInt32(user.Child);

        //                                var adultTotalFee = Convert.ToDecimal(adultTotalFare) + Convert.ToDecimal(adultTotalSurcharge) + Convert.ToDecimal(adultTotalTax);
        //                                var childTotalFee = Convert.ToDecimal(childTotalFare) + Convert.ToDecimal(childTotalSurcharge) + Convert.ToDecimal(childTotalTax);

        //                                objFlightOutbound.Faretotal = (Convert.ToDecimal(adultTotalFee) + Convert.ToDecimal(childTotalFee)).ToString();

        //                                objFlightOutbound.Freebaggage = objRes.Flightavailability.Outbound.Availability[i].FreeBaggage;
        //                                objFlightOutbound.Agencycommission = objRes.Flightavailability.Outbound.Availability[i].AgencyCommission;
        //                                objFlightOutbound.Childcommission = objRes.Flightavailability.Outbound.Availability[i].ChildCommission;
        //                                objFlightOutbound.Callingstationid = objRes.Flightavailability.Outbound.Availability[i].CallingStationId;
        //                                objFlightOutbound.Callingstation = objRes.Flightavailability.Outbound.Availability[i].CallingStation;

        //                                string ServiceId = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay);
        //                                AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId, objFlightOutbound.Faretotal, ServiceId);
        //                                objFlightOutbound.Cashback = Convert.ToString(objOut.CashbackAmount);
        //                                objFlightOutbound.RewardPoints = Convert.ToString(objOut.RewardPoints);
        //                                objFlightOutbound.ServiceCharge = Convert.ToString(objOut.ServiceCharge);

        //                                FlightOutbound.Add(objFlightOutbound);

        //                                //NEW CODE
        //                                List<AddFlightBookingDetails> resFlightDbInsertList_Outbound = new List<AddFlightBookingDetails>();
        //                                AddFlightBookingDetails res = new AddFlightBookingDetails();
        //                                res.BookingId = bookingId;
        //                                res.MemberId = Convert.ToInt64(user.MemberId);
        //                                res.TripType = user.TripType;
        //                                res.FlightType = "D"; //user.FlightType;
        //                                res.Adult = Convert.ToInt32(objFlightOutbound.Adult);
        //                                res.Child = Convert.ToInt32(objFlightOutbound.Child);
        //                                res.IsInbound = false;
        //                                res.Aircrafttype = objFlightOutbound.Aircrafttype;
        //                                res.Airlinename = objFlightOutbound.Airlinename;
        //                                res.Departure = objFlightOutbound.Departure;
        //                                res.Refundable = objFlightOutbound.Refundable;
        //                                res.Infantfare = Convert.ToDecimal(objFlightOutbound.Infantfare);
        //                                res.Flightclasscode = objFlightOutbound.Flightclasscode;
        //                                res.Currency = objFlightOutbound.Currency;
        //                                res.Faretotal = Convert.ToDecimal(objFlightOutbound.Faretotal);
        //                                res.Adultfare = Convert.ToDecimal(objFlightOutbound.Adultfare);
        //                                res.Childfare = Convert.ToDecimal(objFlightOutbound.Childfare);
        //                                res.Departuretime = objFlightOutbound.Departuretime;
        //                                res.Tax = Convert.ToDecimal(objFlightOutbound.Tax);
        //                                res.Airline = objFlightOutbound.Airline;
        //                                res.Airlinelogo = objFlightOutbound.Airlinelogo;
        //                                res.Flightno = objFlightOutbound.Flightno;
        //                                res.Fuelsurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge);
        //                                res.Arrivaltime = objFlightOutbound.Arrivaltime;
        //                                res.Resfare = Convert.ToDecimal(objFlightOutbound.Resfare);
        //                                res.Freebaggage = objFlightOutbound.Freebaggage + " KG";
        //                                res.Arrival = objFlightOutbound.Arrival;
        //                                res.Flightid = objFlightOutbound.Flightid;
        //                                res.Flightdate = Convert.ToDateTime(objFlightOutbound.Flightdate);
        //                                res.CreatedDate = System.DateTime.UtcNow;
        //                                res.UpdatedDate = System.DateTime.UtcNow;
        //                                res.IsActive = true;
        //                                res.IsDeleted = false;
        //                                res.IsApprovedByAdmin = true;
        //                                res.CreatedBy = 10000;//Common.GetCreatedById(authenticationToken);
        //                                res.CreatedByName = "Admin"; //Common.GetCreatedByName(authenticationToken);
        //                                res.IsFlightIssued = false;
        //                                res.IpAddress = Common.GetUserIP();
        //                                res.SectorFrom = objFlightOutbound.Departure; //user.FromDeparture;
        //                                res.SectorTo = objFlightOutbound.Arrival; //user.ToArrival;
        //                                res.ReturnDate = user.ReturnDate;
        //                                res.Faretotal = Convert.ToDecimal(objFlightOutbound.Faretotal);
        //                                resFlightDbInsertList_Outbound.Add(res);
        //                                Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");

        //                                //if (resFlightDbInsertList_Outbound.Count > 0)
        //                                //{
        //                                //    Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");
        //                                //    if (Id > 0)
        //                                //    {
        //                                //        Common.AddLogs($"Added Outbound Flight Detail For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
        //                                //    }
        //                                //    else
        //                                //    {
        //                                //        Common.AddLogs($"Added Outbound Flight Detail Failed For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
        //                                //    }
        //                                //}
        //                                //NEW CODE

        //                            }

        //                        }
        //                    }
        //                    else if (user.TripType.ToLower() == "o") // Field OneWay
        //                    {
        //                        objRes = JsonConvert.DeserializeObject<PlasmaAvailableFlights>(flightSearch);
        //                        for (int i = 0; i < objRes.Flightavailability.Outbound.Availability.Count; i++)// OutBount Response Mapping
        //                        {
        //                            FlightOutbound objFlightOutbound = new FlightOutbound();

        //                            objFlightOutbound.Airline = objRes.Flightavailability.Outbound.Availability[i].Airline;
        //                            Dictionary<string, string> airlineNameMap = new Dictionary<string, string>
        //                                    {
        //                                        { "U4", "Buddha Airlines" },
        //                                        { "S1", "Saurya Airlines" },
        //                                        { "YT", "Yeti Airlines" },
        //                                        { "SHA", "Shree Airlines" },
        //                                        { "RMK", "Simrik Airline" },
        //                                        { "GA", "Goma Airlines" },
        //                                        { "ST", "Sita Air" }
        //                                    };

        //                            if (airlineNameMap.ContainsKey(objFlightOutbound.Airline))
        //                            {
        //                                objFlightOutbound.Airlinename = airlineNameMap[objFlightOutbound.Airline];
        //                            }

        //                            objFlightOutbound.Airlinelogo = objRes.Flightavailability.Outbound.Availability[i].AirlineLogo;
        //                            var flightDate = objRes.Flightavailability.Outbound.Availability[i].FlightDate;
        //                            DateTime date = DateTime.ParseExact(flightDate, "dd-MMM-yyyy", null);
        //                            string Flight_Date = date.ToString("yyyy-MM-dd").ToUpper();

        //                            objFlightOutbound.Flightdate = Flight_Date;
        //                            objFlightOutbound.Flightno = objRes.Flightavailability.Outbound.Availability[i].FlightNo;
        //                            objFlightOutbound.Departure = objRes.Flightavailability.Outbound.Availability[i].Departure;
        //                            objFlightOutbound.Departuretime = objRes.Flightavailability.Outbound.Availability[i].DepartureTime;
        //                            objFlightOutbound.Arrival = objRes.Flightavailability.Outbound.Availability[i].Arrival;
        //                            objFlightOutbound.Arrivaltime = objRes.Flightavailability.Outbound.Availability[i].ArrivalTime;
        //                            objFlightOutbound.Aircrafttype = objRes.Flightavailability.Outbound.Availability[i].AircraftType;
        //                            objFlightOutbound.Adult = objRes.Flightavailability.Outbound.Availability[i].Adult;
        //                            objFlightOutbound.Child = objRes.Flightavailability.Outbound.Availability[i].Child;
        //                            objFlightOutbound.Infant = objRes.Flightavailability.Outbound.Availability[i].Infant;
        //                            objFlightOutbound.Flightid = objRes.Flightavailability.Outbound.Availability[i].FlightId;
        //                            objFlightOutbound.Flightclasscode = objRes.Flightavailability.Outbound.Availability[i].FlightClassCode;
        //                            objFlightOutbound.Currency = objRes.Flightavailability.Outbound.Availability[i].Currency;
        //                            objFlightOutbound.Adultfare = objRes.Flightavailability.Outbound.Availability[i].AdultFare;
        //                            objFlightOutbound.Childfare = objRes.Flightavailability.Outbound.Availability[i].ChildFare;
        //                            objFlightOutbound.Fuelsurcharge = objRes.Flightavailability.Outbound.Availability[i].FuelSurcharge;
        //                            objFlightOutbound.Tax = objRes.Flightavailability.Outbound.Availability[i].Tax;
        //                            if (objRes.Flightavailability.Outbound.Availability[i].Refundable == "T")
        //                            {
        //                                objFlightOutbound.Refundable = true;
        //                            }
        //                            else
        //                            {
        //                                objFlightOutbound.Refundable = false;
        //                            }
        //                            var adultTotalFare = Convert.ToDecimal(objFlightOutbound.Adultfare) * Convert.ToUInt32(user.Adult);
        //                            var adultTotalSurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge) * Convert.ToUInt32(user.Adult);
        //                            var adultTotalTax = Convert.ToDecimal(objFlightOutbound.Tax) * Convert.ToUInt32(user.Adult);
        //                            var childTotalFare = Convert.ToDecimal(objFlightOutbound.Childfare) * Convert.ToUInt32(user.Child);
        //                            var childTotalSurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge) * Convert.ToUInt32(user.Child);
        //                            var childTotalTax = Convert.ToDecimal(objFlightOutbound.Tax) * Convert.ToUInt32(user.Child);

        //                            var adultTotalFee = Convert.ToDecimal(adultTotalFare) + Convert.ToDecimal(adultTotalSurcharge) + Convert.ToDecimal(adultTotalTax);
        //                            var childTotalFee = Convert.ToDecimal(childTotalFare) + Convert.ToDecimal(childTotalSurcharge) + Convert.ToDecimal(childTotalTax);

        //                            objFlightOutbound.Faretotal = (Convert.ToDecimal(adultTotalFee) + Convert.ToDecimal(childTotalFee)).ToString();

        //                            objFlightOutbound.Resfare = objRes.Flightavailability.Outbound.Availability[i].ResFare;
        //                            objFlightOutbound.Freebaggage = objRes.Flightavailability.Outbound.Availability[i].FreeBaggage;
        //                            objFlightOutbound.Agencycommission = objRes.Flightavailability.Outbound.Availability[i].AgencyCommission;
        //                            objFlightOutbound.Childcommission = objRes.Flightavailability.Outbound.Availability[i].ChildCommission;
        //                            objFlightOutbound.Callingstationid = objRes.Flightavailability.Outbound.Availability[i].CallingStationId;
        //                            objFlightOutbound.Callingstation = objRes.Flightavailability.Outbound.Availability[i].CallingStation;

        //                            string ServiceId = Convert.ToString((int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay);
        //                            AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId, objFlightOutbound.Faretotal, ServiceId);
        //                            objFlightOutbound.Cashback = Convert.ToString(objOut.CashbackAmount);
        //                            objFlightOutbound.RewardPoints = Convert.ToString(objOut.RewardPoints);
        //                            objFlightOutbound.ServiceCharge = Convert.ToString(objOut.ServiceCharge);
        //                            FlightOutbound.Add(objFlightOutbound);

        //                            //NEW CODE
        //                            List<AddFlightBookingDetails> resFlightDbInsertList_Outbound = new List<AddFlightBookingDetails>();
        //                            AddFlightBookingDetails res = new AddFlightBookingDetails();

        //                            res.BookingId = bookingId;
        //                            res.MemberId = Convert.ToInt64(user.MemberId);
        //                            res.TripType = user.TripType;
        //                            res.FlightType = "D"; //user.FlightType;
        //                            res.Adult = Convert.ToInt32(objFlightOutbound.Adult);
        //                            res.Child = Convert.ToInt32(objFlightOutbound.Child);
        //                            res.IsInbound = false;
        //                            res.Aircrafttype = objFlightOutbound.Aircrafttype;
        //                            res.Airlinename = objFlightOutbound.Airlinename;
        //                            res.Departure = objFlightOutbound.Departure;
        //                            res.Refundable = objFlightOutbound.Refundable;
        //                            res.Infantfare = Convert.ToDecimal(objFlightOutbound.Infantfare);
        //                            res.Flightclasscode = objFlightOutbound.Flightclasscode;
        //                            res.Currency = objFlightOutbound.Currency;
        //                            res.Faretotal = Convert.ToDecimal(objFlightOutbound.Faretotal);
        //                            res.Adultfare = Convert.ToDecimal(objFlightOutbound.Adultfare);
        //                            res.Childfare = Convert.ToDecimal(objFlightOutbound.Childfare);
        //                            res.Departuretime = objFlightOutbound.Departuretime;
        //                            res.Tax = Convert.ToDecimal(objFlightOutbound.Tax);
        //                            res.Airline = objFlightOutbound.Airline;
        //                            res.Airlinelogo = objFlightOutbound.Airlinelogo;
        //                            res.Flightno = objFlightOutbound.Flightno;
        //                            res.Fuelsurcharge = Convert.ToDecimal(objFlightOutbound.Fuelsurcharge);
        //                            res.Arrivaltime = objFlightOutbound.Arrivaltime;
        //                            res.Resfare = Convert.ToDecimal(objFlightOutbound.Resfare);
        //                            res.Freebaggage = objFlightOutbound.Freebaggage + " KG";
        //                            res.Arrival = objFlightOutbound.Arrival;
        //                            res.Flightid = objFlightOutbound.Flightid;
        //                            res.Flightdate = Convert.ToDateTime(objFlightOutbound.Flightdate);
        //                            res.CreatedDate = System.DateTime.UtcNow;
        //                            res.UpdatedDate = System.DateTime.UtcNow;
        //                            res.IsActive = true;
        //                            res.IsDeleted = false;
        //                            res.IsApprovedByAdmin = true;
        //                            res.CreatedBy = 10000;
        //                            res.CreatedByName = "Admin";
        //                            res.IsFlightIssued = false;
        //                            res.IpAddress = Common.GetUserIP();
        //                            res.SectorFrom = objFlightOutbound.Departure;
        //                            res.SectorTo = objFlightOutbound.Arrival;
        //                            res.ReturnDate = user.ReturnDate;
        //                            resFlightDbInsertList_Outbound.Add(res);
        //                            Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");

        //                            //if (resFlightDbInsertList_Outbound.Count > 0)
        //                            //{
        //                            //    Int64 Id = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.InsertList(resFlightDbInsertList_Outbound, "flightbookingdetails");
        //                            //    if (Id > 0)
        //                            //    {
        //                            //        Common.AddLogs($"Added Outbound Flight Detail For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
        //                            //    }
        //                            //    else
        //                            //    {
        //                            //        Common.AddLogs($"Added Outbound Flight Detail Failed For Booking ID {resFlightDbInsertList_Outbound[0].BookingId} by(MemberId:{user.MemberId}).", false, (int)AddLog.LogType.DBLogs);
        //                            //    }
        //                            //}
        //                            //NEW CODE
        //                        }
        //                    }
        //                    else
        //                    {
        //                        result.Message = "Record not found or Something went wrong";
        //                    }
        //                    //result.Data = objRes.Flightavailability;
        //                    FlightOutbound.Sort((x, y) => Convert.ToDecimal(x.Faretotal).CompareTo(Convert.ToDecimal(y.Faretotal)));
        //                    FlightInbound.Sort((x, y) => Convert.ToDecimal(x.Faretotal).CompareTo(Convert.ToDecimal(y.Faretotal)));

        //                    result.FlightOutbound = FlightOutbound;
        //                    result.FlightInbound = FlightInbound;
        //                    result.BookingId = bookingId;
        //                    result.ReponseCode = 1;
        //                    result.status = true;
        //                    result.Message = "success";
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Available_Requests>(System.Net.HttpStatusCode.OK, result);
        //                }
        //                else
        //                {
        //                    msg = "Record not found or Something went wrong";
        //                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                }
        //            }
        //        }
        //        log.Info($" {System.DateTime.Now.ToString()} PlasmaService.FlightAvailability completed  {Environment.NewLine}");
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
        //        log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.FlightAvailability {e.ToString()}  ");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.FlightAvailability {ex.ToString()}  ");
        //        return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}


        [HttpPost]
        [Route("api/plasma-reserve-flight")]
        public HttpResponseMessage GetServiceReserveFlight(Req_Vendor_API_PlasmaTech_Flight_Reserve_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside PlasmaService.ReserveFlight" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_PlasmaTech_Flight_Reserve_Requests result = new Res_Vendor_API_PlasmaTech_Flight_Reserve_Requests();
            //var response = Request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Reserve_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
            response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Reserve_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    //string md5hash = Common.CheckHash(user);
                    string md5hash = Common.getHashMD5(userInput);

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

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string msg = "";

                        var reserv = RepKhalti.FlightReservation(user.FlightID, user.ReturnFlightID);
                        bool IsUpdated = false;
                        AddFlightBookingDetails resOut = new AddFlightBookingDetails();
                        GetFlightBookingDetails resIn = new GetFlightBookingDetails();
                        resIn.BookingId = Convert.ToInt64(user.BookingID);
                        resIn.Flightid = user.FlightID;
                        resIn.CheckInbound = 0;
                        AddUser outobject = new AddUser();
                        GetUser inobject = new GetUser();
                        if (user.MemberId != "")
                        {
                            inobject.MemberId = Convert.ToInt64(user.MemberId);
                        }
                        AddUser model = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
                        if (reserv.Contains("FlightId"))
                        {
                            if (user.FlightID != string.Empty && user.ReturnFlightID != string.Empty)
                            {
                                ReservationRootTwoWay reservationRootTwoWay = JsonConvert.DeserializeObject<ReservationRootTwoWay>(reserv);
                                var pnr = "";
                                for (int i = 0; i < reservationRootTwoWay.ReservationDetail.PNRDetail.Count; i++)
                                {

                                    AddFlightBookingDetails resOut_R = new AddFlightBookingDetails();
                                    GetFlightBookingDetails resIn_R = new GetFlightBookingDetails();

                                    result.FlightID = user.FlightID;
                                    result.InboundFlightID = user.ReturnFlightID;

                                    if (reservationRootTwoWay.ReservationDetail.PNRDetail[i].AirlineID.ToUpper() == "YT")
                                    {
                                        result.TTL = "";
                                    }
                                    else
                                    {
                                        var datetime = reservationRootTwoWay.ReservationDetail.PNRDetail[i].TTLDate + "T" + reservationRootTwoWay.ReservationDetail.PNRDetail[i].TTLTime;
                                        DateTime parsedDate = DateTime.ParseExact(datetime, "dd-MMM-yyyyTHHmm", null);
                                        string formattedDate = parsedDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                                        result.TTL = formattedDate;
                                    }
                                    result.ReponseCode = 1;
                                    if (reservationRootTwoWay.ReservationDetail.PNRDetail[i].ReservationStatus == "OK")
                                    {
                                        result.status = true;
                                    }
                                    else if (reservationRootTwoWay.ReservationDetail.PNRDetail[i].ReservationStatus == "HK")
                                    {
                                        result.status = true;
                                    }
                                    pnr = reservationRootTwoWay.ReservationDetail.PNRDetail[i].PNRNO;

                                    resIn_R.BookingId = Convert.ToInt64(user.BookingID);
                                    resIn_R.Flightid = reservationRootTwoWay.ReservationDetail.PNRDetail[i].FlightId;
                                    
                                    if (resIn_R.Flightid == user.FlightID)
                                    {
                                        resIn_R.CheckInbound = 0;
                                    }
                                    else
                                    {
                                        resIn_R.CheckInbound = 1;
                                    }

                                    AddFlightBookingDetails resGetRecord = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn_R, resOut_R);
                                    if (resGetRecord.Id != 0)
                                    {
                                        resGetRecord.IsFlightBooked = true;
                                        resGetRecord.BookingCreatedDate = System.DateTime.UtcNow;
                                        resGetRecord.IpAddress = Common.GetUserIP();
                                        resGetRecord.PnrNumber = pnr;
                                        resGetRecord.LogIDs = "Plasma_" + resGetRecord.Flightid;

                                        resGetRecord.UpdatedDate = System.DateTime.UtcNow;
                                        IsUpdated = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecord, "flightbookingdetails");

                                        if (!IsUpdated)
                                        {
                                            result.Message = "Failed";
                                            result.status = false;
                                            response.StatusCode = HttpStatusCode.InternalServerError;
                                            response = Request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Reserve_Requests>(System.Net.HttpStatusCode.InternalServerError, result);
                                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                                        }
                                    }
                                }

                                result.Message = "Success";
                                result.status = true;
                                result.Details = "Selected flights will be reserved for 30 minutes.";
                                response.StatusCode = HttpStatusCode.Accepted;
                                response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Reserve_Requests>(System.Net.HttpStatusCode.OK, result);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            }
                            else
                            {
                                ReservationRootModel reservationRoot = JsonConvert.DeserializeObject<ReservationRootModel>(reserv);
                                result.FlightID = reservationRoot.ReservationDetail.PNRDetail.FlightId;

                                if (reservationRoot.ReservationDetail.PNRDetail.AirlineID.ToUpper() == "YT")
                                {
                                    result.TTL = "";
                                }
                                else
                                {
                                    var datetime = reservationRoot.ReservationDetail.PNRDetail.TTLDate + "T" + reservationRoot.ReservationDetail.PNRDetail.TTLTime;
                                    DateTime parsedDate = DateTime.ParseExact(datetime, "dd-MMM-yyyyTHHmm", null);
                                    string formattedDate = parsedDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                                    result.TTL = formattedDate;
                                }
                                result.ReponseCode = 1;
                                if (reservationRoot.ReservationDetail.PNRDetail.ReservationStatus == "OK")
                                {
                                    result.status = true;
                                }
                                else if (reservationRoot.ReservationDetail.PNRDetail.ReservationStatus.ToUpper() == "HK")
                                {
                                    result.status = true;
                                }

                                resIn.BookingId = Convert.ToInt64(user.BookingID);
                                resIn.Flightid = user.FlightID;
                                resIn.CheckInbound = 0;
                                AddFlightBookingDetails resGetRecord = RepCRUD<GetFlightBookingDetails, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, resIn, resOut);
                                if (resGetRecord.Id != 0)
                                {
                                    resGetRecord.IsFlightBooked = true;
                                    resGetRecord.BookingCreatedDate = System.DateTime.UtcNow;
                                    resGetRecord.IpAddress = Common.GetUserIP();
                                    resGetRecord.PnrNumber = reservationRoot.ReservationDetail.PNRDetail.PNRNO;
                                    resGetRecord.LogIDs = "Plasma_" + resGetRecord.Flightid;
                                    //resGetRecord.CreatedBy = Common.GetCreatedById(authenticationToken);
                                    //resGetRecord.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                    //resGetRecord.UpdatedBy = Common.GetCreatedById(authenticationToken);
                                    resGetRecord.UpdatedDate = System.DateTime.UtcNow;
                                    //resGetRecord.ContactName = model.FirstName + " " + model.MiddleName + " " + model.LastName;
                                    //resGetRecord.ContactPhone = model.ContactNumber;
                                    //resGetRecord.ContactEmail = model.Email;
                                    IsUpdated = RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecord, "flightbookingdetails");
                                }
                                if (IsUpdated)
                                {
                                    result.Message = "Success";
                                    result.status = true;
                                    result.Details = "Selected flights will be reserved for 30 minutes.";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Flight_Reserve_Requests>(System.Net.HttpStatusCode.OK, result);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                                }

                            }
                        }
                        else
                        {
                            msg = "Record not found or Something went wrong";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.ReserveFlight completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.ReserveFlight {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.ReserveFlight {ex.ToString()}  ");
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/plasma-issue-ticket")]
        public HttpResponseMessage GetLookupServiceIssueTicket(Req_Vendor_API_PlasmaTech_Issue_Ticket_Requests user, HttpRequestMessage requestPassed = null, string userInputPassed = null)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside PlasmaService.IssueFlight" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_PlasmaTech_Issue_Ticket_Requests result = new Res_Vendor_API_PlasmaTech_Issue_Ticket_Requests();
            //var response = Request.CreateResponse<Res_Vendor_API_PlasmaTech_Issue_Ticket_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
            response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Issue_Ticket_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    //string md5hash = Common.CheckHash(user);
                    string md5hash = Common.getHashMD5(userInput);

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
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId.Trim() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Airlines_MyPay;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.FareTotal, true, user.Mpin);
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

                        GetVendor_API_Airlines_MyPay_Payment_Request objResPayment = new GetVendor_API_Airlines_MyPay_Payment_Request();
                        string msg = "success";
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        //string UserInput = getRawPostData().Result;
                        bool IsCouponUnlocked = false;
                        string TransactionID = string.Empty;

                        AddFlightBookingDetails outobj1 = new AddFlightBookingDetails();
                        GetFlightBookingDetails_Plasma inobj1 = new GetFlightBookingDetails_Plasma();

                        inobj1.MemberId = Convert.ToInt64(user.MemberId);
                        inobj1.Flightid = user.FlightID;
                        inobj1.MemberId = Convert.ToInt64(user.MemberId);


                        AddFlightBookingDetails resGetReco = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord("sp_FlightBookingDetails_Get_plasma", inobj1, outobj1);

                        user.ContactName = resGetReco.ContactName;
                        user.ContactEmail = resGetReco.ContactEmail;
                        user.ContactMobile = resGetReco.ContactPhone;

                        //if ((new CommonHelpers()).GetFlightLogsIssuedStatus(resGetRecord.MemberId, user.BookingID))
                        //{
                        //    msg = "Flight already Issued";
                        //    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        //    cres.Details = msg;
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        //    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        //}

                        msg = RepKhalti.RequestServiceGroup_PLASMA_FLIGHT_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.FareTotal, user.Reference, user.Version, user.DeviceCode,
                           user.PlatForm, user.MemberId, authenticationToken, userInput, ref objResPayment, ref objVendor_API_Requests, resGetCouponsScratched, user.BookingID, user.FlightID, user.ReturnFlightID, user.ContactName, user.ContactEmail, user.ContactMobile);

                        if (msg.ToLower() == "success")
                        {
                            JToken parsedJson = JToken.Parse(objResPayment.Data);
                            JToken paxDetail = parsedJson["Itinerary"];
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "Success";
                            result.TransactionUniqueId = objResPayment.UniqueTransactionId;
                            result.Details = "Your Flight Issued Successfully.";
                            result.ApiMessage = "Transaction Completed Successfully.";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = request.CreateResponse<Res_Vendor_API_PlasmaTech_Issue_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            // regin for insert issue ticket save response//
                            AddPlasmaTechIssueFlightDetails outobject = new AddPlasmaTechIssueFlightDetails();
                            GetPlasmaTechIssueFlightDetails resIn = new GetPlasmaTechIssueFlightDetails();
                            AddFlightBookingDetails resOut = new AddFlightBookingDetails();
                            GetFlightBookingDetails_Plasma res_In = new GetFlightBookingDetails_Plasma();
                            Int64 Id = 0;
                            if (!string.IsNullOrEmpty(user.FlightID) && !string.IsNullOrEmpty(user.ReturnFlightID))// for twoway flight
                            {
                                IssueTicketTwoWayResp passRes = parsedJson.ToObject<IssueTicketTwoWayResp>();
                                List<Passenger> pList = new List<Passenger>();

                                List<AddPlasmaTechIssueFlightDetails> outobj = new List<AddPlasmaTechIssueFlightDetails>();
                                int counter = 0;
                                int counters = 0;
                                int a = 0;
                                foreach (var item in passRes.Itinerary.Passenger)
                                {
                                    MyPay.Models.Get.PlasmaAirlines.Passenger pass = (MyPay.Models.Get.PlasmaAirlines.Passenger)item;
                                    pass.MemberId = Convert.ToInt32(user.MemberId);
                                    pass.FlightId = user.FlightID;
                                    pass.ReturnFlightId = user.ReturnFlightID;
                                    if (item.Refundable == "Refundable")
                                    {
                                        pass.Refundable = "1";
                                    }
                                    else
                                    {
                                        pass.Refundable = "0";
                                    }


                                    pass.CreatedBy = Common.GetCreatedById(authenticationToken);
                                    pass.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                    pList.Add(pass);

                                    res_In.BookingId = Convert.ToInt64(user.BookingID);

                                    res_In.CheckFlightBooked = 1;

                                    if (counter % 2 == 0)
                                    {
                                        res_In.Flightid = user.FlightID;
                                    }
                                    else
                                    {
                                        res_In.Flightid = user.ReturnFlightID;
                                    }
                                    counter++;
                                    //if (user.FlightID == item.FlightId)
                                    //{
                                    //    res_In.CheckInbound = 0;
                                    //}
                                    //else
                                    //{
                                    //    res_In.CheckInbound = 1;
                                    //}

                                    AddFlightBookingDetails resGetRecordFlightBookingDetails = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get_plasma, res_In, resOut);
                                    if (resGetRecordFlightBookingDetails.Id != 0)
                                    {
                                        resGetRecordFlightBookingDetails.IsFlightIssued = true;
                                        resGetRecordFlightBookingDetails.UpdatedBy = Common.GetCreatedById(authenticationToken);
                                        resGetRecordFlightBookingDetails.UpdatedByName = Common.GetCreatedByName(authenticationToken);

                                        RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecordFlightBookingDetails, "flightbookingdetails");
                                    }


                                    GetFlightPassengersDetails ressIn = new GetFlightPassengersDetails();
                                    AddFlightPassengersDetails ressOut = new AddFlightPassengersDetails();

                                    ressIn.BookingId = Convert.ToInt64(user.BookingID);
                                    // ressIn.FlightId = user.FlightID;
                                    ressIn.FirstName = item.FirstName;

                                    AddFlightPassengersDetails resPassDetail = RepCRUD<GetFlightPassengersDetails, AddFlightPassengersDetails>.GetRecord(Common.StoreProcedures.sp_FlightPassengersDetails_Get, ressIn, ressOut);

                                    if (resPassDetail != null && resPassDetail.Id != 0)
                                    {
                                       var test = passRes.Itinerary.Passenger.Count;
                                        int splitIndex = test / 2;

                                        if (splitIndex > a)
                                        {
                                            if (item.TicketNo != null || item.TicketNo != "")
                                            {
                                                ressOut.TicketNo = item.TicketNo;
                                            }
                                            else
                                            {
                                                ressOut.TicketNo = pass.TicketNo;
                                            }
                                            a++;
                                        }
                                        else
                                        {
                                            if (item.TicketNo != null || item.TicketNo != "")
                                            {
                                                ressOut.InboundTicketNo = item.TicketNo;
                                            }
                                            else
                                            {
                                                ressOut.InboundTicketNo = pass.TicketNo;
                                            }
                                        }
                                        counters++;

                                        RepCRUD<AddFlightPassengersDetails, AddFlightPassengersDetails>.Update(ressOut, "flightpassengersdetails");
                                    }
                                }
                            }
                            else // for oneway flight
                            {
                                if (objResPayment.Data.Contains("[") && objResPayment.Data.Contains("]"))
                                {
                                    IssueTicketTwoWayResp passRes = parsedJson.ToObject<IssueTicketTwoWayResp>();
                                    List<Passenger> pList = new List<Passenger>();
                                    List<AddPlasmaTechIssueFlightDetails> outobj = new List<AddPlasmaTechIssueFlightDetails>();
                                    int counter = 0;
                                    foreach (var item in passRes.Itinerary.Passenger)
                                    {
                                        MyPay.Models.Get.PlasmaAirlines.Passenger pass = (MyPay.Models.Get.PlasmaAirlines.Passenger)item;
                                        pass.MemberId = Convert.ToInt32(user.MemberId);
                                        pass.FlightId = user.FlightID;
                                        pass.ReturnFlightId = user.ReturnFlightID;
                                        if (item.Refundable == "Refundable")
                                        {
                                            pass.Refundable = "1";
                                        }
                                        else
                                        {
                                            pass.Refundable = "0";
                                        }
                                        pass.CreatedBy = Common.GetCreatedById(authenticationToken);
                                        pass.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                        pList.Add(pass);

                                        res_In.BookingId = Convert.ToInt64(user.BookingID);
                                        res_In.Flightid = user.FlightID;
                                        res_In.CheckFlightBooked = 1;
                                        //if(user.FlightID == item.FlightId)
                                        //{
                                        //    res_In.CheckInbound = 0;
                                        //}
                                        //else
                                        //{
                                        //    res_In.CheckInbound = 1;
                                        //}

                                        if (counter % 2 == 0)
                                        {
                                            res_In.Flightid = user.FlightID;
                                        }
                                        else
                                        {
                                            res_In.Flightid = user.ReturnFlightID;
                                        }
                                        counter++;

                                        AddFlightBookingDetails resGetRecordFlightBookingDetails = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get_plasma, res_In, resOut);
                                        //AddFlightBookingDetails resGetRecordFlightBookingDetails = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, res_In, resOut);
                                        if (resGetRecordFlightBookingDetails.Id != 0)
                                        {
                                            resGetRecordFlightBookingDetails.IsFlightIssued = true;
                                            resGetRecordFlightBookingDetails.UpdatedBy = Common.GetCreatedById(authenticationToken);
                                            resGetRecordFlightBookingDetails.UpdatedByName = Common.GetCreatedByName(authenticationToken);

                                            RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecordFlightBookingDetails, "flightbookingdetails");
                                        }

                                        GetFlightPassengersDetails ressIn = new GetFlightPassengersDetails();
                                        AddFlightPassengersDetails ressOut = new AddFlightPassengersDetails();

                                        ressIn.BookingId = Convert.ToInt64(user.BookingID);
                                        // ressIn.FlightId = user.FlightID;
                                        ressIn.FirstName = item.FirstName;

                                        AddFlightPassengersDetails resPassDetail = RepCRUD<GetFlightPassengersDetails, AddFlightPassengersDetails>.GetRecord(Common.StoreProcedures.sp_FlightPassengersDetails_Get, ressIn, ressOut);
                                        if (resPassDetail != null && resPassDetail.Id != 0)
                                        {
                                            if (item.TicketNo != null || item.TicketNo != "")
                                            {
                                                ressOut.TicketNo = item.TicketNo;
                                            }
                                            else
                                            {
                                                ressOut.TicketNo = pass.TicketNo;
                                            }

                                            RepCRUD<AddFlightPassengersDetails, AddFlightPassengersDetails>.Update(ressOut, "flightpassengersdetails");
                                        }
                                        //ressOut.TicketNo = item.TicketNo;
                                        //RepCRUD<AddFlightPassengersDetails, AddFlightPassengersDetails>.Update(ressOut, "flightpassengersdetails");
                                    }
                                }
                                else
                                {
                                    IssueTicketResp passRes = parsedJson.ToObject<IssueTicketResp>();

                                    outobject = JsonConvert.DeserializeObject<AddPlasmaTechIssueFlightDetails>(JsonConvert.SerializeObject(passRes.Itinerary.Passenger));
                                    outobject.MemberId = Convert.ToInt32(user.MemberId);
                                    outobject.FlightId = user.FlightID;
                                    outobject.ReturnFlightId = user.ReturnFlightID;
                                    var ticketNumber = outobject.TicketNo;

                                    if (passRes.Itinerary.Passenger.Refundable == "Refundable")
                                    {
                                        outobject.Refundable = "1";
                                    }
                                    else
                                    {
                                        outobject.Refundable = "0";
                                    }
                                    outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                                    outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                                    res_In.BookingId = Convert.ToInt64(user.BookingID);
                                    res_In.Flightid = user.FlightID;
                                    res_In.CheckFlightBooked = 1;
                                    // res_In.CheckInbound = 0;
                                    AddFlightBookingDetails resGetRecordFlightBookingDetails = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get_plasma, res_In, resOut);
                                    //AddFlightBookingDetails resGetRecordFlightBookingDetails = RepCRUD<GetFlightBookingDetails_Plasma, AddFlightBookingDetails>.GetRecord(Common.StoreProcedures.sp_FlightBookingDetails_Get, res_In, resOut);
                                    if (resGetRecordFlightBookingDetails.Id != 0)
                                    {
                                        resGetRecordFlightBookingDetails.IsFlightIssued = true;
                                        resGetRecordFlightBookingDetails.UpdatedBy = Common.GetCreatedById(authenticationToken);
                                        resGetRecordFlightBookingDetails.UpdatedByName = Common.GetCreatedByName(authenticationToken);

                                        RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecordFlightBookingDetails, "flightbookingdetails");
                                    }

                                    GetFlightPassengersDetails ressIn = new GetFlightPassengersDetails();
                                    AddFlightPassengersDetails ressOut = new AddFlightPassengersDetails();

                                    ressIn.BookingId = Convert.ToInt64(user.BookingID);
                                    // ressIn.FlightId = user.FlightID;
                                    ressIn.FirstName = passRes.Itinerary.Passenger.FirstName;

                                    AddFlightPassengersDetails resPassDetail = RepCRUD<GetFlightPassengersDetails, AddFlightPassengersDetails>.GetRecord(Common.StoreProcedures.sp_FlightPassengersDetails_Get, ressIn, ressOut);
                                    if (resPassDetail != null && resPassDetail.Id != 0)
                                    {
                                        if (passRes.Itinerary.Passenger.TicketNo != null || passRes.Itinerary.Passenger.TicketNo != "")
                                        {
                                            ressOut.TicketNo = passRes.Itinerary.Passenger.TicketNo;
                                        }
                                        else
                                        {
                                            ressOut.TicketNo = ticketNumber;
                                        }

                                        RepCRUD<AddFlightPassengersDetails, AddFlightPassengersDetails>.Update(ressOut, "flightpassengersdetails");
                                    }
                                    //ressOut.TicketNo = passRes.Itinerary.Passenger.TicketNo;
                                    //RepCRUD<AddFlightPassengersDetails, AddFlightPassengersDetails>.Update(ressOut, "flightpassengersdetails");
                                }
                            }
                            //end//
                            //ticket download
                            AddFlightBookingDetails resGetRecordFlightBookingDetail = new AddFlightBookingDetails();
                            string fullpath = string.Empty;
                            //string KhaltiAPIURL = "download";
                            Guid ReferenceGuid = Guid.NewGuid();
                            string Reference = new CommonHelpers().GenerateUniqueId();
                            string log_id = string.Empty;
                            var bookingId = Convert.ToInt64(user.BookingID);
                            GetVendor_API_Airlines_Lookup objResDownload = VendorApi_CommonHelper.RequestPlasmaAirlines_DOWNLOAD_TICKET(bookingId, user.MemberId, user.FlightID, user.ContactName, user.ContactMobile, user.ContactEmail, "/Content/FlightTicketPDF", ref fullpath);
                            resGetRecordFlightBookingDetail.TicketPDF = objResDownload.FilePath;
                            RepCRUD<AddFlightBookingDetails, GetFlightBookingDetails>.Update(resGetRecordFlightBookingDetail, "flightbookingdetails");
                            //ticketdownload end
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            cres.Details = msg + " (FlightID: " + user.FlightID + ")";
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.IssueFlight completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.IssueFlight {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.IssueFlight {ex.ToString()}  ");
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/plasma-itinerary")]
        public HttpResponseMessage GetLookupServiceItinerary(Req_Vendor_API_PlasmaTech_Itinerary_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside PlasmaService.GetItinerary" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_PlasmaTech_Itinerary_Requests result = new Res_Vendor_API_PlasmaTech_Itinerary_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_PlasmaTech_Itinerary_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.getHashMD5(userInput);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        IssueTicketRes objRes = new IssueTicketRes();
                        string msg = "";

                        MyPay.org.usbooking.dev.UnitedSolutionsService PlasmaService = new MyPay.org.usbooking.dev.UnitedSolutionsService();
                        string itineraryResponse = PlasmaService.GetItinerary(user.PnoNo, user.TicketNo, user.AgencyId);
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(itineraryResponse);

                        string JsonResponse = JsonConvert.SerializeXmlNode(doc);
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<IssueTicketRes>(JsonResponse);
                        if (JsonResponse.Contains("TicketNo"))
                        {
                            JToken parsedJson = JToken.Parse(JsonResponse);
                            JToken itineraryDetail = parsedJson["Itinerary"];
                            result.Data = itineraryDetail;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_PlasmaTech_Itinerary_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            msg = "Record not found or Something went wrong";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }

                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.GetItinerary completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.GetItinerary {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.GetItinerary {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/plasma-flight-detail")]
        public HttpResponseMessage GetFlightDetailLookupService(Req_Vendor_API_PlasmaTech_FlightDetail_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside PlasmaService.FlightDetail" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Flight_Detail_Requests result = new Res_Vendor_API_Flight_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Flight_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.getHashMD5(userInput);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        GetFlightDetailRes objRes = new GetFlightDetailRes();
                        string msg = "";

                        MyPay.org.usbooking.dev.UnitedSolutionsService PlasmaService = new MyPay.org.usbooking.dev.UnitedSolutionsService();
                        string flightDetail = PlasmaService.GetFlightDetail(user.UserID, user.FlightId);

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(flightDetail);

                        string JsonResponse = JsonConvert.SerializeXmlNode(doc);
                        objRes = JsonConvert.DeserializeObject<GetFlightDetailRes>(JsonResponse);
                        if (objRes.Availability != null)
                        {
                            result.Data.Availability = objRes.Availability;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Flight_Detail_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            msg = "Record not found or Something went wrong";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.FlightDetail completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.FlightDetail {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.FlightDetail {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/plasma-sales-report")]
        public HttpResponseMessage GetSalesReportService(Req_Vendor_API_PlasmaTech_Sales_Report_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside PlasmaService.SalesReport" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Sales_Report_Requests result = new Res_Vendor_API_Sales_Report_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Sales_Report_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.getHashMD5(userInput);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        SalesReportRes objRes = new SalesReportRes();
                        string msg = "";

                        MyPay.org.usbooking.dev.UnitedSolutionsService PlasmaService = new MyPay.org.usbooking.dev.UnitedSolutionsService();
                        string salesRes = PlasmaService.SalesReport(user.UserID, user.Password, user.AgencyId, user.FromDate, user.ToDate);

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(salesRes);

                        string JsonResponse = JsonConvert.SerializeXmlNode(doc);
                        objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<SalesReportRes>(JsonResponse);
                        if (objRes.SalesSummary.TicketDetail != null)
                        {
                            result.Data = objRes.SalesSummary;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Sales_Report_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            msg = "Record not found or Something went wrong";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.SalesReport completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.SalesReport {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.SalesReport {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/plasma-pnr-detail")]
        public HttpResponseMessage GetPnrDetail(Req_Vendor_API_PlasmaTech_GetPnrDetail_Requests user) // Api Used for Reschedule/Cancellation
        {
            log.Info($"{System.DateTime.Now.ToString()} inside PlasmaService.GetPnrDetail" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Pnr_Detail_Requests result = new Res_Vendor_API_Pnr_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Pnr_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.getHashMD5(userInput);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        SalesReportRes objRes = new SalesReportRes();
                        string msg = "";

                        string pnrDetailRes = RepKhalti.GetPnrDetail(user.UserID, user.Password, user.AgencyId, user.PnrNo, user.LastName);

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(pnrDetailRes);

                        string JsonResponse = JsonConvert.SerializeXmlNode(doc);
                        //objRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetPnrDetailRootModel>(JsonResponse);
                        if (objRes.SalesSummary.TicketDetail != null)
                        {
                            //result.Data = objRes;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Pnr_Detail_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            msg = "Record not found or Something went wrong";
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($" {System.DateTime.Now.ToString()} PlasmaService.GetPnrDetail completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.GetPnrDetail {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()} PlasmaService.GetPnrDetail {ex.ToString()}  ");
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

        public class ReservationDetail
        {
            public List<PNRDetail> PNRDetail { get; set; }
        }

        public class ReservationRootTwoWay
        {
            public ReservationDetail ReservationDetail { get; set; }
        }

    }
}