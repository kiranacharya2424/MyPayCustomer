using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Events;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Get.Events;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class RequestAPI_EventsController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_EventsController));
        string ApiResponse = string.Empty;


        [HttpPost]
        [Route("api/get-event-list")]
        public HttpResponseMessage GetServiceGroupEvents(Req_Vendor_API_Events_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group Events" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Requests result = new Res_Vendor_API_Events_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                    {
                       // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                       // AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                        int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    CommonResponse cres1 = new CommonResponse();
                        //    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //    return response;
                        //}
                        string EventsAPIURL = "/clientapi/get-event-list/";
                        string ApiResponse = string.Empty;

                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                        string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                        objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, user.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", userInput, user.DeviceCode, user.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                        GetVendor_API_Events objRes = new GetVendor_API_Events();
                        string msg = RepEvents.RequestServiceGroup_Events(ref objVendor_API_Requests, user.PageSize, user.PageNumber, user.SearchVal, user.SortOrder, user.DateFrom, user.DateTo, user.SortBy, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<Models.Events.EventsData> objEvents = new List<Models.Events.EventsData>();
                            for (int i = 0; i < objRes.data.items.Count; i++)
                            {
                                Models.Events.EventsData events = new Models.Events.EventsData();
                                events.eventId = objRes.data.items[i].eventId;
                                events.eventName = objRes.data.items[i].eventName;
                                events.eventDate = objRes.data.items[i].eventDate;
                                events.eventDateDT = Convert.ToDateTime(objRes.data.items[i].eventDate).ToString("dd-MMM-yyyy");
                                events.eventDescription = objRes.data.items[i].eventDescription;
                                events.sliderImagePath = objRes.data.items[i].sliderImagePath;
                                events.promotionalBannerImagePath = objRes.data.items[i].promotionalBannerImagePath;
                                events.merchantCode = objRes.data.items[i].merchantCode;
                                events.organizerName = objRes.data.items[i].organizerName;
                                events.venueAddress = objRes.data.items[i].venueAddress;
                                objEvents.Add(events);
                            }
                            result.items = objEvents;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = true;
                            result.Message = "success";
                            result.success = true;
                            response = Request.CreateResponse<Res_Vendor_API_Events_Requests>(System.Net.HttpStatusCode.Accepted, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                        inobjApiResponse.Id = objVendor_API_Requests.Id;
                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                        }
                    }
                    else
                    {
                        CommonResponse cres1 = new CommonResponse();
                        cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} Get Service Group Events completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }




        [HttpPost]
        [Route("api/get-event-details")]
        public HttpResponseMessage GetServiceGroupEventDetail(Req_Vendor_API_Events_Details_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group Event Detail" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Details_Requests result = new Res_Vendor_API_Events_Details_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Details_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            //string UserInput = getRawPostData().Result;
                            string CommonResult = "";
                            AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                            string EventsAPIURL = "/clientapi/get-event-details/";
                            string ApiResponse = string.Empty;

                            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                            int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                            string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, user.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", userInput, user.DeviceCode, user.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());


                            GetVendor_API_Events_Details objRes = new GetVendor_API_Events_Details();
                            string msg = RepEvents.RequestServiceGroup_Events_Details(ref objVendor_API_Requests, user.EventId, user.EventDate, user.MerchantCode, ref objRes);
                            if (msg.ToLower() == "success")
                            {
                                Models.Events.EventDetail objEvents = new Models.Events.EventDetail();
                                objEvents.eventId = objRes.data.eventId;
                                objEvents.eventName = objRes.data.eventName;
                                objEvents.eventDate = objRes.data.eventDate;
                                objEvents.eventDateDT = Convert.ToDateTime(objRes.data.eventDate).ToString("dd-MMM-yyyy");
                                objEvents.eventDescription = objRes.data.eventDescription;
                                objEvents.eventDateString = objRes.data.eventDateString;
                                objEvents.eventDateNepali = objRes.data.eventDateNepali;
                                objEvents.eventStartTime = objRes.data.eventStartTime;
                                objEvents.eventEndTime = objRes.data.eventEndTime;
                                objEvents.venueName = objRes.data.venueName;
                                objEvents.venueAddress = objRes.data.venueAddress;
                                objEvents.venueImagePath1 = objRes.data.venueImagePath1;
                                objEvents.venueImagePath2 = objRes.data.venueImagePath2;
                                objEvents.venueImagePath3 = objRes.data.venueImagePath3;
                                objEvents.venueCapacity = objRes.data.venueCapacity;
                                objEvents.organizerName = objRes.data.organizerName;
                                objEvents.eventType = objRes.data.eventType;
                                objEvents.latitude = objRes.data.eventId;
                                objEvents.longitude = objRes.data.eventId;
                                objEvents.parkingAvailable = objRes.data.parkingAvailable;
                                objEvents.bannerImagePath = objRes.data.bannerImagePath;
                                objEvents.bannerImagePath = objRes.data.bannerImagePath;
                                objEvents.promotionalBannerImagePath = objRes.data.promotionalBannerImagePath;
                                objEvents.showArrivalTime = objRes.data.showArrivalTime;
                                objEvents.eventTermsAndCondition = objRes.data.eventTermsAndCondition;
                                objEvents.eventContactDtls = objRes.data.eventContactDtls;
                                objEvents.isSingleDayEvent = objRes.data.isSingleDayEvent;
                                objEvents.isActive = objRes.data.isActive;
                                if (objRes.data.sponserList.Count > 0)
                                {
                                    List<Models.Events.Sponser> sponsersList = new List<Models.Events.Sponser>();
                                    for (int i = 0; i < objRes.data.sponserList.Count; i++)
                                    {
                                        Models.Events.Sponser sponser = new Models.Events.Sponser();
                                        sponser.sponserId = objRes.data.sponserList[i].sponserId;
                                        sponser.sponserName = objRes.data.sponserList[i].sponserName;
                                        sponser.sponserLogoImagePath = objRes.data.sponserList[i].sponserLogoImagePath;
                                        sponser.sponserTypeId = objRes.data.sponserList[i].sponserTypeId;
                                        sponser.sponserTypeName = objRes.data.sponserList[i].sponserTypeName;
                                        sponsersList.Add(sponser);
                                    }
                                    objEvents.sponserList = sponsersList;
                                }
                                if (objRes.data.guestList.Count > 0)
                                {
                                    List<Models.Events.Guest> guestList = new List<Models.Events.Guest>();
                                    for (int i = 0; i < objRes.data.guestList.Count; i++)
                                    {
                                        Models.Events.Guest guest = new Models.Events.Guest();
                                        guest.guestId = objRes.data.guestList[i].guestId;
                                        guest.guestName = objRes.data.guestList[i].guestName;
                                        guest.speciality = objRes.data.guestList[i].speciality;
                                        guest.guestImagePath = objRes.data.guestList[i].guestImagePath;
                                        guest.guestTypeId = objRes.data.guestList[i].guestTypeId;
                                        guest.guestTypeName = objRes.data.guestList[i].guestTypeName;
                                        guestList.Add(guest);
                                    }
                                    objEvents.guestList = guestList;
                                }
                                if (objRes.data.paymentMethodList.Count > 0)
                                {
                                    List<Models.Events.PaymentMethod> paymentMethodList = new List<Models.Events.PaymentMethod>();
                                    for (int i = 0; i < objRes.data.paymentMethodList.Count; i++)
                                    {
                                        Models.Events.PaymentMethod PaymentMethod = new Models.Events.PaymentMethod();
                                        PaymentMethod.paymentMethodId = objRes.data.paymentMethodList[i].paymentMethodId;
                                        PaymentMethod.paymentMethodCode = objRes.data.paymentMethodList[i].paymentMethodCode;
                                        PaymentMethod.paymentMethodName = objRes.data.paymentMethodList[i].paymentMethodName;
                                        PaymentMethod.paymentMerchantId = objRes.data.paymentMethodList[i].paymentMerchantId;
                                        paymentMethodList.Add(PaymentMethod);
                                    }
                                    objEvents.paymentMethodList = paymentMethodList;
                                }
                                if (objRes.data.ticketCategoryList.Count > 0)
                                {
                                    List<Models.Events.TicketCategory> ticketCategoryList = new List<Models.Events.TicketCategory>();
                                    for (int i = 0; i < objRes.data.ticketCategoryList.Count; i++)
                                    {
                                        Models.Events.TicketCategory ticketCategory = new Models.Events.TicketCategory();
                                        ticketCategory.ticketCategoryId = objRes.data.ticketCategoryList[i].ticketCategoryId;
                                        ticketCategory.ticketCategoryName = objRes.data.ticketCategoryList[i].ticketCategoryName;
                                        ticketCategory.sectionName = objRes.data.ticketCategoryList[i].sectionName;
                                        ticketCategory.ticketRate = objRes.data.ticketCategoryList[i].ticketRate;
                                        ticketCategory.availableTickets = objRes.data.ticketCategoryList[i].availableTickets;
                                        if (objRes.data.ticketCategoryList[i].amenityList.Count > 0)
                                        {
                                            List<Models.Events.Amenity> amenityList = new List<Models.Events.Amenity>();
                                            for (int j = 0; j < objRes.data.ticketCategoryList[i].amenityList.Count; j++)
                                            {
                                                Models.Events.Amenity amenity = new Models.Events.Amenity();
                                                amenity.amenityId = objRes.data.ticketCategoryList[i].amenityList[j].amenityId;
                                                amenity.amenityName = objRes.data.ticketCategoryList[i].amenityList[j].amenityName;
                                                amenityList.Add(amenity);
                                            }
                                            ticketCategory.amenityList = amenityList;
                                        }

                                        ticketCategoryList.Add(ticketCategory);
                                    }
                                    objEvents.ticketCategoryList = ticketCategoryList;
                                }


                                AddEventDetails objAddEventDetail_CheckExists = new AddEventDetails();
                                objAddEventDetail_CheckExists.eventId = user.EventId;
                                objAddEventDetail_CheckExists.MemberId = resuserdetails.MemberId;
                                objAddEventDetail_CheckExists.CheckDelete = 0;
                                objAddEventDetail_CheckExists.CheckActive = 1;
                                if (objAddEventDetail_CheckExists.GetRecord())
                                {
                                    objAddEventDetail_CheckExists.IsDeleted = true;
                                    objAddEventDetail_CheckExists.IsActive = false;
                                    objAddEventDetail_CheckExists.Update();
                                }

                                AddEventDetails objAddEventDetail = new AddEventDetails();
                                objAddEventDetail.eventId = objEvents.eventId;
                                objAddEventDetail.eventName = objEvents.eventName;
                                objAddEventDetail.eventDate = Convert.ToDateTime(objEvents.eventDate);
                                objAddEventDetail.eventDescription = objEvents.eventDescription;
                                objAddEventDetail.eventDateString = objEvents.eventDateString;
                                objAddEventDetail.eventDateNepali = (objEvents.eventDateNepali);
                                objAddEventDetail.eventStartTime = objEvents.eventStartTime;
                                objAddEventDetail.eventEndTime = objEvents.eventEndTime;
                                objAddEventDetail.venueName = objEvents.venueName;
                                objAddEventDetail.venueAddress = objEvents.venueAddress;
                                objAddEventDetail.venueImagePath1 = objEvents.venueImagePath1;
                                objAddEventDetail.organizerName = objEvents.organizerName;
                                objAddEventDetail.eventType = objEvents.eventType;
                                objAddEventDetail.latitude = objEvents.eventId;
                                objAddEventDetail.longitude = objEvents.eventId;
                                objAddEventDetail.parkingAvailable = objEvents.parkingAvailable;
                                objAddEventDetail.bannerImagePath = objEvents.bannerImagePath;
                                objAddEventDetail.bannerImagePath = objEvents.bannerImagePath;
                                objAddEventDetail.isSingleDayEvent = objEvents.isSingleDayEvent ? 1 : 0;
                                objAddEventDetail.venueCapacity = Convert.ToInt64(objEvents.venueCapacity);
                                objAddEventDetail.showArrivalTime = Convert.ToInt32(objEvents.showArrivalTime);
                                objAddEventDetail.arrivalTime = objEvents.arrivalTime;
                                objAddEventDetail.sectionName = objEvents.sectionName;
                                objAddEventDetail.eventTermsAndCondition = objEvents.eventTermsAndCondition;
                                objAddEventDetail.eventContactDtls = objEvents.eventContactDtls;
                                objAddEventDetail.MemberId = resuserdetails.MemberId;
                                objAddEventDetail.ticketSentDatetime = DateTime.UtcNow.AddHours(-2);
                                if (objAddEventDetail.Add())
                                {
                                    result.data = objEvents;
                                    result.ReponseCode = objRes.status ? 1 : 0;
                                    result.status = true;
                                    result.Message = "success";
                                    result.success = true;
                                    response = Request.CreateResponse<Res_Vendor_API_Events_Details_Requests>(System.Net.HttpStatusCode.OK, result);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("Booking details not saved");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                            }
                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                            inobjApiResponse.Id = objVendor_API_Requests.Id;
                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                            if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                            {
                                resUpdateRecord.Res_Output = ApiResponse;
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
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

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} Get Service Group Event Detail completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Event Detail {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/book-event-ticket")]
        public HttpResponseMessage ServiceGroupBookEventTicket(Req_Vendor_API_Events_Ticket_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Book Event Ticket" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Ticket_Requests result = new Res_Vendor_API_Events_Ticket_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Ticket_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            //string UserInput = getRawPostData().Result;
                            string CommonResult = "";
                            AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                            string EventsAPIURL = "/clientapi/book-event-tickets/";
                            string ApiResponse = string.Empty;


                            string authenticationToken = Request.Headers.Authorization.Parameter;

                            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                            string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, user.ReferenceNo, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, "", authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                            GetVendor_API_Events_Ticket objRes = new GetVendor_API_Events_Ticket();
                            Res_Vendor_API_Events_Requests objMaxTickets = new Res_Vendor_API_Events_Requests();
                            if (user.NoOfTicket > objMaxTickets.MaxTicketsAllowed)
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Maximum number of ticket allowed is 25.");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                            }
                            else
                            {

                                string msg = RepEvents.RequestServiceGroup_Event_Ticket(ref objVendor_API_Requests, user.MerchantCode, user.CustomerName, user.CustomerMobile, user.CustomerEmail, user.EventId, user.TicketCategoryId, user.TicketCategoryName,
                                user.EventDate, user.TicketRate, user.NoOfTicket, user.TotalPrice, user.PaymentMethodId, user.DeviceId, ref objRes);
                                result.message = msg;
                                if (msg.ToLower() == "success")
                                {
                                    AddEventDetails objAddEventDetail = new AddEventDetails();
                                    objAddEventDetail.eventId = user.EventId;
                                    objAddEventDetail.MemberId = resuserdetails.MemberId;
                                    objAddEventDetail.CheckDelete = 0;
                                    objAddEventDetail.CheckActive = 1;
                                    objAddEventDetail.CheckPaymentDone = 0;
                                    if (objAddEventDetail.GetRecord())
                                    {
                                        objAddEventDetail.noOfTicket = user.NoOfTicket;
                                        objAddEventDetail.totalPrice = user.TotalPrice;
                                        objAddEventDetail.ticketRate = user.TicketRate;
                                        objAddEventDetail.paymentMethodId = user.PaymentMethodId;
                                        objAddEventDetail.merchantCode = user.MerchantCode;
                                        objAddEventDetail.customerName = user.CustomerName;
                                        objAddEventDetail.customerMobile = user.CustomerMobile;
                                        objAddEventDetail.customerEmail = user.CustomerEmail;
                                        objAddEventDetail.ticketCategoryId = user.TicketCategoryId;
                                        objAddEventDetail.ticketCategoryName = user.TicketCategoryName;
                                        objAddEventDetail.OrderId = objRes.data.orderId;
                                        objAddEventDetail.IsBooked = true;
                                        if (objAddEventDetail.Update())
                                        {
                                            Models.Events.Order objOrder = new Models.Events.Order();
                                            objOrder.MerchantCode = objRes.data.merchantCode;
                                            objOrder.OrderId = objRes.data.orderId;
                                            objOrder.PaymentMethodId = objRes.data.paymentMethodId;

                                            result.data = objOrder;
                                            result.ReponseCode = objRes.status ? 1 : 0;
                                            result.status = true;
                                            result.success = true;
                                            result.message = objRes.message;
                                            //response.StatusCode = HttpStatusCode.Accepted;
                                            response = Request.CreateResponse<Res_Vendor_API_Events_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                                        }
                                        else
                                        {
                                            cres = CommonApiMethod.ReturnBadRequestMessage("Event not updated");
                                            response.StatusCode = HttpStatusCode.BadRequest;
                                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                        }
                                    }
                                    else
                                    {
                                        cres = CommonApiMethod.ReturnBadRequestMessage("Event not Found");
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                    }
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                                }
                            }
                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                            inobjApiResponse.Id = objVendor_API_Requests.Id;
                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                            if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                            {
                                resUpdateRecord.Res_Output = ApiResponse;
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
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

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()}  Book Event Ticketcompleted" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()}  Book Event Ticket {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/v2/ConfirmEventOrder")]
        public HttpResponseMessage ServiceGroupBookEventTicket2(Req_Vendor_API_Events_Ticket_RequestsV2 user)
        {

            log.Info("ConfirmEventOrder called");

            string UserInput = getRawPostData().Result;

           
            if (string.IsNullOrEmpty(user.CustomerEmail))
            {
                user.CustomerEmail = user.MemberId + "@myPayMobile.com";
            }

            log.Info($"{System.DateTime.Now.ToString()} inside Book Event Ticket" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Ticket_Requests result = new Res_Vendor_API_Events_Ticket_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Ticket_Requests>(System.Net.HttpStatusCode.BadRequest, result);


            Req_Vendor_API_Events_Commit_Requests newReq = new Req_Vendor_API_Events_Commit_Requests();
            newReq = JsonConvert.DeserializeObject<Req_Vendor_API_Events_Commit_Requests>(JsonConvert.SerializeObject(user));

            string md5hash = Common.getHashMD5(UserInput);
                       //             string md5hash = Common.CheckHash(user);

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
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                           
                            string CommonResult = "";
                            AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", true, user.Mpin, "", false, true);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                            string EventsAPIURL = "/clientapi/book-event-tickets/";
                            string ApiResponse = string.Empty;


                            string authenticationToken = Request.Headers.Authorization.Parameter;

                            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                            string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, user.ReferenceNo, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, "", authenticationToken, UserInput, user.DeviceCode, user.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                            GetVendor_API_Events_Ticket objRes = new GetVendor_API_Events_Ticket();
                            Res_Vendor_API_Events_Requests objMaxTickets = new Res_Vendor_API_Events_Requests();
                            if (user.NoOfTicket > objMaxTickets.MaxTicketsAllowed)
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Maximum number of ticket allowed is 25.");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                            }
                            else
                            {

                                string msg = RepEvents.RequestServiceGroup_Event_Ticket(ref objVendor_API_Requests, user.MerchantCode, user.CustomerName, user.CustomerMobile, user.CustomerEmail, user.EventId, user.TicketCategoryId, user.TicketCategoryName,
                                user.EventDate, user.TicketRate, user.NoOfTicket, user.TotalPrice, user.PaymentMethodId, user.DeviceId, ref objRes, user.CouponCode);
                                result.message = msg;
                                if (msg.ToLower() == "success")
                                {
                                    AddEventDetails objAddEventDetail = new AddEventDetails();
                                    objAddEventDetail.eventId = user.EventId;
                                    objAddEventDetail.MemberId = resuserdetails.MemberId;
                                    objAddEventDetail.CheckDelete = 0;
                                    objAddEventDetail.CheckActive = 1;
                                    objAddEventDetail.CheckPaymentDone = 0;
                                    if (objAddEventDetail.GetRecord())
                                    {
                                        objAddEventDetail.noOfTicket = user.NoOfTicket;
                                        objAddEventDetail.totalPrice = objRes.data.PayableAmount; //user.TotalPrice;
                                        objAddEventDetail.ticketRate = user.TicketRate;
                                        objAddEventDetail.paymentMethodId = user.PaymentMethodId;
                                        objAddEventDetail.merchantCode = user.MerchantCode;
                                        objAddEventDetail.customerName = user.CustomerName;
                                        objAddEventDetail.customerMobile = user.CustomerMobile;
                                        objAddEventDetail.customerEmail = user.CustomerEmail;
                                        objAddEventDetail.ticketCategoryId = user.TicketCategoryId;
                                        objAddEventDetail.ticketCategoryName = user.TicketCategoryName;
                                        objAddEventDetail.OrderId = objRes.data.orderId;
                                        objAddEventDetail.IsBooked = true;
                                        if (objAddEventDetail.Update())
                                        {
                                            Models.Events.Order objOrder = new Models.Events.Order();
                                            objOrder.MerchantCode = objRes.data.merchantCode;
                                            objOrder.OrderId = objRes.data.orderId;
                                            objOrder.PaymentMethodId = objRes.data.paymentMethodId;
                                            objOrder.PayableAmount = objRes.data.PayableAmount;
                                            objOrder.PaymentMerchantId = objRes.data.PaymentMerchantId;

                                            result.data = objOrder;
                                            result.ReponseCode = objRes.status ? 1 : 0;
                                            result.status = true;
                                            result.success = true;
                                            result.message = objRes.message;
                                            //response.StatusCode = HttpStatusCode.Accepted;
                                            response = Request.CreateResponse<Res_Vendor_API_Events_Ticket_Requests>(System.Net.HttpStatusCode.OK, result);
                                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                                            //Req_Vendor_API_Events_Commit_Requests newReq = new Req_Vendor_API_Events_Commit_Requests();
                                            //newReq = JsonConvert.DeserializeObject<Req_Vendor_API_Events_Commit_Requests>(JsonConvert.SerializeObject(user));
                                            newReq.MerchantCode = result.data.MerchantCode;
                                            newReq.OrderId = result.data.OrderId;
                                            newReq.PaymentMethodId = result.data.PaymentMethodId;
                                            newReq.TxnId = result.TransactionUniqueId;
                                            newReq.EventId = user.EventId.ToString();
                                            newReq.PaymentMerchantId = result.data.PaymentMerchantId;
                                            newReq.MemberId = user.MemberId.ToString();
                                            newReq.token = user.Token;
                                            newReq.Reference = user.ReferenceNo;
                                            newReq.Amount = result.data.PayableAmount.ToString();
                                           newReq.paymentMethodName = result.data.PaymentMethodId.ToString();
                                            newReq.ticketCategoryName = user.TicketCategoryName;

                                            newReq.CouponCode = user.CouponCode;
                                            decimal amount = 0;
                                            try
                                            {
                                                if(!string.IsNullOrEmpty(newReq.CouponCode))
                                                {
                                                    if (!string.IsNullOrEmpty(user.Amount))
                                                    {
                                                        decimal.TryParse(user.Amount, out amount);
                                                        //decimal.TryParse(user.TotalPrice, out amount);
                                                        newReq.CouponDiscount = amount - result.data.PayableAmount;
                                                    }
                                                    else {
                                                        newReq.CouponDiscount = user.TotalPrice - result.data.PayableAmount;
                                                        amount = user.TotalPrice;
                                                    }
                                                }
                                                
                                                newReq.TransactionAmount = amount;
                                            }
                                            catch (Exception)
                                            {
                                                log.Info("couldnt parse the amount: " + user.Amount + " to decimal");
                                               // throw;
                                            }

                                          

                                            //newReq.PaymentMerchantId = user.PaymentMerchantId;
                                            //newReq.Value = results.va


                                            //ServiceGroupEventTicketCommit()
                                            //Req_Vendor_API_Events_Commit_Requests

                                            HttpResponseMessage responseFromSecondMethod = ServiceGroupEventTicketCommitV2(newReq, UserInput, resuserdetails);
                                            response = responseFromSecondMethod;

                                        }
                                        else
                                        {
                                            cres = CommonApiMethod.ReturnBadRequestMessage("Event not updated");
                                            response.StatusCode = HttpStatusCode.BadRequest;
                                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                        }
                                    }
                                    else
                                    {
                                        cres = CommonApiMethod.ReturnBadRequestMessage("Event not Found");
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                    }
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                                }
                            }
                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                            inobjApiResponse.Id = objVendor_API_Requests.Id;
                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                            if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                            {
                                resUpdateRecord.Res_Output = ApiResponse;
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                            }

                            //HttpResponseMessage responseFromSecondMethod = ServiceGroupEventTicketCommitV2(newReq, UserInput, resuserdetails);
                            //response = responseFromSecondMethod;

                            //return responseFromSecondMethod;
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()}  Book Event Ticketcompleted" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()}  Book Event Ticket {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/v2/event-ticket-commit")]
        public HttpResponseMessage ServiceGroupEventTicketCommitV2(Req_Vendor_API_Events_Commit_Requests user, string UserInput, AddUserLoginWithPin resuserdetails)
        {
            
            log.Info($"{System.DateTime.Now.ToString()} inside Event Ticket Commit" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Commit_Requests result = new Res_Vendor_API_Events_Commit_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Commit_Requests>(System.Net.HttpStatusCode.BadRequest, result);

           // var userInput = getRawPostData().Result;
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
                    string md5hash = Common.getHashMD5(UserInput);

                  //  UserInput = UserInput + ": COMMITEventReq";
                    int length = UserInput.Length; 
                    UserInput = UserInput.Substring(0, length - 1) + ", \"additionalInfo\" : \"commitReq\"}";

                    //{\"SessionId\":\"\",\"MemberId\":\"304055\",\"ReferenceNo\":\"\",\"TicketCategoryName\":\"\",\"merchantCode\":\"MER000001\",\"customerName\":\"Rahul Rajbanshi\",\"customerMobile\":\"9803505220\",\"customerEmail\":\"rahul.rajbanshi@email.com\",\"eventId\":29,\"ticketCategoryId\":2,\"eventDate\":\"2023-09-23\",\"ticketRate\":0.1,\"noOfTicket\":3,\"totalPrice\":0.3,\"paymentMethodId\":1,\"apiClientCode\":\"\",\"DeviceCode\":\"mobile\",\"DeviceId\":\"3ABAF738-4036-4B61-9541-88284EFA8184-iPhone-iPhone-iOS-16.5.1\",\"Version\":\"1.0\",\"PlatForm\":\"mobile\",\"TimeStamp\":551881827757,\"Token\":\"\",\"UniqueCustomerId\":\"9801129367\",\"PaymentMode\":\"1\",\"BankTransactionId\":\"\",\"SecretKey\":\"ua5MRVa70MizJ1x5aDvMWRVeoH4vohGRUJ374IgJt4M+pdbI56WrtjDQ38K23GkFZrGTUKR/UYthR9N1QYly37jWZ3j0fNsdO0DM4NyekC5jsop/E54795BGGva4iTZ21uR0OM8LH6g=\",\"VendorJsonLookup\":\"string\",\"paymentMethodName\":\"Mypay Merchant\",\"value\":1,\"ticketCategoryName\":\"Normal\",\"PaymentMerchantId\":\"MER74776614\",\"hash\":\"920eb8936af
                    //3cf37ca018ccbce6c34e7\",\"Mpin\":\"6APdvyp0OtG2GJAnjPhntA==\"}: COMMITEventReq

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }

                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "success";
                       // AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                        int Type = 0;
                       // resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        //else if (user.BankTransactionId == "0")
                        //{
                        //    CommonResponse cres1 = new CommonResponse();
                        //    cres1 = CommonApiMethod.ReturnBadRequestMessage("Linked Bank Transactions for Events are not available at the moment. Please try again later Or Use Wallet Payments.");
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //    return response;
                        //}
                        else
                        {
                            string EventsAPIURL = "api/use-mypay-payments/";
                            string ApiResponse = string.Empty;

                            string authenticationToken = Request.Headers.Authorization.Parameter;
                            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                            string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, user.Reference, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, "", authenticationToken, UserInput, user.DeviceCode, user.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                            AddMerchant outobject = new AddMerchant();
                            GetMerchant inobject = new GetMerchant();
                            inobject.MerchantUniqueId = user.PaymentMerchantId;
                            AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                            VendorApi_CommonHelper.MERCHANT_API_KEY = model.apikey;
                            GetVendor_API_Events_CommitMerchant objCommitRes = new GetVendor_API_Events_CommitMerchant();
                            string MerchantAPIPassword = Common.DecryptionFromKey(model.API_Password, model.secretkey);
                            string msgMerchantPayment = RepEvents.RequestServiceGroup_Events_CommitMerchantTransactions(user.OrderId, user.Amount, user.PaymentMerchantId, model.UserName, MerchantAPIPassword, user.DeviceId, ref objCommitRes);
                            if (msgMerchantPayment.ToLower() == "success")
                            {
                                AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                inobjectOrders.MerchantId = user.PaymentMerchantId;
                                inobjectOrders.OrderId = user.OrderId;
                                AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                if (resOrders != null && resOrders.Id != 0)
                                {
                                    resOrders.Remarks = $"Events Payment Pending for Contact no. {resuserdetails.ContactNumber}";
                                    resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Success;
                                    resOrders.MemberContactNumber = resuserdetails.ContactNumber;
                                    resOrders.MemberName = resuserdetails.FirstName + " " + resuserdetails.LastName;
                                    resOrders.MemberId = resuserdetails.MemberId;
                                    resOrders.CurrentBalance = model.MerchantTotalAmount;
                                    bool resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                                    if (resOrdersFlag)
                                    {
                                        GetVendor_API_Event_Payment_Request objPaymentRes = new GetVendor_API_Event_Payment_Request();
                                        user.TxnId = objCommitRes.merchantTransactionId;

                                        objVendor_API_Requests.Res_Khalti_Id = resOrders.TransactionId;
                                        objVendor_API_Requests.Req_ReferenceNo = resOrders.OrderId;
                                        objVendor_API_Requests.Req_Khalti_ReferenceNo = resOrders.OrderId;
                                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                                        GetVendor_API_Events_Commit objRes = new GetVendor_API_Events_Commit();
                                        Common.authenticationToken = authenticationToken;
                                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                                        user.Reference = resOrders.OrderId;

                                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                                        //string msg = RepEvents.RequestServiceGroup_Events_Commit(resOrders, user.EventId, user.paymentMethodName, user.ticketCategoryName, user.MerchantCode, user.OrderId, user.TxnId, user.PaymentMethodId, user.Amount, user.PaymentMode, resuserdetails, authenticationToken, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.UniqueCustomerId, user.Value, user.Reference, user.Version, user.DeviceCode, user.PlatForm, user.MemberId, UserInput, ref objRes, ref objVendor_API_Requests);

                                        string msg = RepEvents.RequestServiceGroup_Events_Commit(resOrders, user.EventId, user.paymentMethodName, user.ticketCategoryName, user.MerchantCode, user.OrderId, user.TxnId, user.PaymentMethodId, user.Amount, user.PaymentMode, resuserdetails, authenticationToken, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.UniqueCustomerId, user.Value, user.Reference, user.Version, user.DeviceCode, user.PlatForm, user.MemberId, UserInput, ref objRes, ref objVendor_API_Requests,
                                            user.TransactionAmount, user.TransactionAmount, user.CouponCode, user.CouponDiscount
                                            );

                                        /*
                                          newReq.CouponCode = user.CouponCode;
                                            decimal amount = 0;
                                            try
                                            {
                                                decimal.TryParse(user.Amount, out amount);
                                                newReq.CouponDiscount = amount - result.data.PayableAmount;
                                                newReq.TransactionAmount = amount;
                                            }
                                            catch (Exception)
                                            {
                                                log.Info("couldnt parse the amount: " + user.Amount + " to decimal");
                                               // throw;
                                            }
                                         */
                                        //decimal netAmount = 0, decimal TransactionAmount = 0, string CouponCode = "", decimal couponDiscount = 0
                                        result.message = msg;
                                        if (msg.ToLower() == "success")
                                        {
                                            decimal MerchantBalance = model.MerchantTotalAmount + (resOrders.Amount);
                                            resOrders.Remarks = $"Events Payment Completed for Contact no. {resuserdetails.ContactNumber}";
                                            resOrders.CurrentBalance = MerchantBalance;
                                            resOrders.NetAmount = resOrders.Amount;
                                           // resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                                            model.MerchantTotalAmount = MerchantBalance;
                                           // bool resFlag = RepCRUD<AddMerchant, GetMerchant>.Update(model, "merchant");

                                            AddEventDetails objAddEventDetail = new AddEventDetails();
                                            objAddEventDetail.eventId = Convert.ToInt64(user.EventId);
                                            objAddEventDetail.OrderId = user.OrderId;
                                            objAddEventDetail.MemberId = resuserdetails.MemberId;
                                            objAddEventDetail.CheckDelete = 0;
                                            objAddEventDetail.CheckActive = 1;
                                            objAddEventDetail.CheckIsBooked = 1;
                                            if (objAddEventDetail.GetRecord())
                                            {
                                                objAddEventDetail.TransactionUniqueId = resOrders.TransactionId;
                                                objAddEventDetail.paymentMethodName = user.paymentMethodName;
                                                objAddEventDetail.ticketCategoryName = user.ticketCategoryName;
                                                objAddEventDetail.paymentMerchantId = user.PaymentMerchantId;
                                                objAddEventDetail.IsPaymentDone = true;
                                                if (objAddEventDetail.Update())
                                                {

                                                    Models.Events.Commit objCommit = new Models.Events.Commit();
                                                    objCommit.MerchantCode = objRes.data.merchantCode;
                                                    objCommit.OrderId = objRes.data.orderId;
                                                    objCommit.TransactionId = objRes.data.transactionId;
                                                    objCommit.Remarks = objRes.data.remarks;
                                                    result.data = objCommit;
                                                    result.ReponseCode = objRes.status ? 1 : 0;
                                                    result.status = true;
                                                    result.success = true;
                                                    result.Details = objCommit.Remarks;
                                                    result.ReponseCode = 1;
                                                    result.responseMessage = objCommit.Remarks;
                                                    result.TransactionUniqueId = objCommit.TransactionId;
                                                    result.MerchantCode = objCommit.MerchantCode;
                                                    result.OrderId = objCommit.OrderId;
                                                    response = Request.CreateResponse<Res_Vendor_API_Events_Commit_Requests>(System.Net.HttpStatusCode.OK, result);
                                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                                                }
                                                else
                                                {
                                                    cres = CommonApiMethod.ReturnBadRequestMessage("Event Details Not Updated");
                                                    response.StatusCode = HttpStatusCode.BadRequest;
                                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                                }
                                            }
                                            else
                                            {
                                                cres = CommonApiMethod.ReturnBadRequestMessage("Event Details Not Found");
                                                response.StatusCode = HttpStatusCode.BadRequest;
                                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                            }
                                        }
                                        else
                                        {

                                            resOrders.Remarks = $"Events Payment Failed for Contact no. {resuserdetails.ContactNumber}";
                                            resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                                            //resOrders.CurrentBalance = model.MerchantTotalAmount;
                                            bool resOrdersFailedFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                            response.StatusCode = HttpStatusCode.BadRequest;
                                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                        }
                                    }
                                    else
                                    {
                                        cres = CommonApiMethod.ReturnBadRequestMessage("Order not updated");
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                    }
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("Order not found");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(msgMerchantPayment);
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
                    else
                    {
                        CommonResponse cres1 = new CommonResponse();
                        cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        return response;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()}  Event Ticket Commit completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()}  Event Ticket Commit {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPost]
        [Route("api/event-ticket-commit")]
        public HttpResponseMessage ServiceGroupEventTicketCommit(Req_Vendor_API_Events_Commit_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside  Event Ticket Commit" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Commit_Requests result = new Res_Vendor_API_Events_Commit_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Commit_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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

                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        string UserInput = getRawPostData().Result ;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        //else if (user.BankTransactionId == "0")
                        //{
                        //    CommonResponse cres1 = new CommonResponse();
                        //    cres1 = CommonApiMethod.ReturnBadRequestMessage("Linked Bank Transactions for Events are not available at the moment. Please try again later Or Use Wallet Payments.");
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //    return response;
                        //}
                        else
                        {
                            string EventsAPIURL = "api/use-mypay-payments/";
                            string ApiResponse = string.Empty;

                            string authenticationToken = Request.Headers.Authorization.Parameter;
                            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                            string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, user.Reference, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, "", authenticationToken, UserInput, user.DeviceCode, user.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                            AddMerchant outobject = new AddMerchant();
                            GetMerchant inobject = new GetMerchant();
                            inobject.MerchantUniqueId = user.PaymentMerchantId;
                            AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                            VendorApi_CommonHelper.MERCHANT_API_KEY = model.apikey;
                            GetVendor_API_Events_CommitMerchant objCommitRes = new GetVendor_API_Events_CommitMerchant();
                            string MerchantAPIPassword = Common.DecryptionFromKey(model.API_Password, model.secretkey);
                            string msgMerchantPayment = RepEvents.RequestServiceGroup_Events_CommitMerchantTransactions(user.OrderId, user.Amount, user.PaymentMerchantId, model.UserName, MerchantAPIPassword, user.DeviceId, ref objCommitRes);
                            if (msgMerchantPayment.ToLower() == "success")
                            {
                                AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                                GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                                inobjectOrders.MerchantId = user.PaymentMerchantId;
                                inobjectOrders.OrderId = user.OrderId;
                                AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                                if (resOrders != null && resOrders.Id != 0)
                                {
                                    resOrders.Remarks = $"Events Payment Pending for Contact no. {resuserdetails.ContactNumber}";
                                    resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Success;
                                    resOrders.MemberContactNumber = resuserdetails.ContactNumber;
                                    resOrders.MemberName = resuserdetails.FirstName + " " + resuserdetails.LastName;
                                    resOrders.MemberId = resuserdetails.MemberId;
                                    resOrders.CurrentBalance = model.MerchantTotalAmount;
                                    bool resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                                    if (resOrdersFlag)
                                    {
                                        GetVendor_API_Event_Payment_Request objPaymentRes = new GetVendor_API_Event_Payment_Request();
                                        user.TxnId = objCommitRes.merchantTransactionId;

                                        objVendor_API_Requests.Res_Khalti_Id = resOrders.TransactionId;
                                        objVendor_API_Requests.Req_ReferenceNo = resOrders.OrderId;
                                        objVendor_API_Requests.Req_Khalti_ReferenceNo = resOrders.OrderId;
                                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                                        GetVendor_API_Events_Commit objRes = new GetVendor_API_Events_Commit();
                                        Common.authenticationToken = authenticationToken;
                                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                                        user.Reference = resOrders.OrderId;

                                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                                        string msg = RepEvents.RequestServiceGroup_Events_Commit(resOrders, user.EventId, user.paymentMethodName, user.ticketCategoryName, user.MerchantCode, user.OrderId, user.TxnId, user.PaymentMethodId, user.Amount, user.PaymentMode, resuserdetails, authenticationToken, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.UniqueCustomerId, user.Value, user.Reference, user.Version, user.DeviceCode, user.PlatForm, user.MemberId, UserInput, ref objRes, ref objVendor_API_Requests);
                                        result.message = msg;
                                        if (msg.ToLower() == "success")
                                        {
                                            decimal MerchantBalance = model.MerchantTotalAmount + (resOrders.Amount);
                                            resOrders.Remarks = $"Events Payment Completed for Contact no. {resuserdetails.ContactNumber}";
                                            resOrders.CurrentBalance = MerchantBalance;
                                            resOrders.NetAmount = resOrders.Amount;

                                            model.MerchantTotalAmount = MerchantBalance;

                                            AddEventDetails objAddEventDetail = new AddEventDetails();
                                            objAddEventDetail.eventId = Convert.ToInt64(user.EventId);
                                            objAddEventDetail.OrderId = user.OrderId;
                                            objAddEventDetail.MemberId = resuserdetails.MemberId;
                                            objAddEventDetail.CheckDelete = 0;
                                            objAddEventDetail.CheckActive = 1;
                                            objAddEventDetail.CheckIsBooked = 1;
                                            if (objAddEventDetail.GetRecord())
                                            {
                                                objAddEventDetail.TransactionUniqueId = resOrders.TransactionId;
                                                objAddEventDetail.paymentMethodName = user.paymentMethodName;
                                                objAddEventDetail.ticketCategoryName = user.ticketCategoryName;
                                                objAddEventDetail.paymentMerchantId = user.PaymentMerchantId;
                                                objAddEventDetail.IsPaymentDone = true;
                                                if (objAddEventDetail.Update())
                                                {

                                                    Models.Events.Commit objCommit = new Models.Events.Commit();
                                                    objCommit.MerchantCode = objRes.data.merchantCode;
                                                    objCommit.OrderId = objRes.data.orderId;
                                                    objCommit.TransactionId = objRes.data.transactionId;
                                                    objCommit.Remarks = objRes.data.remarks;

                                                    result.data = objCommit;
                                                    result.ReponseCode = objRes.status ? 1 : 0;
                                                    result.status = true;
                                                    result.success = true;
                                                    result.Details = objCommit.Remarks;
                                                    result.ReponseCode = 1;
                                                    result.responseMessage = objCommit.Remarks;
                                                    result.TransactionUniqueId = objCommit.TransactionId;
                                                    result.MerchantCode = objCommit.MerchantCode;
                                                    result.OrderId = objCommit.OrderId;
                                                    response = Request.CreateResponse<Res_Vendor_API_Events_Commit_Requests>(System.Net.HttpStatusCode.OK, result);
                                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                                                }
                                                else
                                                {
                                                    cres = CommonApiMethod.ReturnBadRequestMessage("Event Details Not Updated");
                                                    response.StatusCode = HttpStatusCode.BadRequest;
                                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                                }
                                            }
                                            else
                                            {
                                                cres = CommonApiMethod.ReturnBadRequestMessage("Event Details Not Found");
                                                response.StatusCode = HttpStatusCode.BadRequest;
                                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                            }
                                        }
                                        else
                                        {

                                            resOrders.Remarks = $"Events Payment Failed for Contact no. {resuserdetails.ContactNumber}";
                                            resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Failed;
                                            //resOrders.CurrentBalance = model.MerchantTotalAmount;
                                            bool resOrdersFailedFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                            response.StatusCode = HttpStatusCode.BadRequest;
                                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                        }
                                    }
                                    else
                                    {
                                        cres = CommonApiMethod.ReturnBadRequestMessage("Order not updated");
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                    }
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("Order not found");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(msgMerchantPayment);
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
                    else
                    {
                        CommonResponse cres1 = new CommonResponse();
                        cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        return response;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()}  Event Ticket Commit completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()}  Event Ticket Commit {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



       

        [HttpPost]
        [Route("api/event-ticket-download")]
        public HttpResponseMessage ServiceGroupEventTicketDownload(Req_Vendor_API_Events_Ticket_Download_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside  Event Ticket Download" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Ticket_Download_Requests result = new Res_Vendor_API_Events_Ticket_Download_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Ticket_Download_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_Events_Ticket_Download objRes = new GetVendor_API_Events_Ticket_Download();
                        string msg = RepEvents.RequestServiceGroup_Events_Ticket_Download(user.MerchantCode, user.OrderId, ref objRes);
                        result.message = msg;
                        if (msg.ToLower() == "success")
                        {
                            AddEventDetails objAddEventDetail = new AddEventDetails();
                            objAddEventDetail.OrderId = user.OrderId;
                            objAddEventDetail.merchantCode = user.MerchantCode;
                            objAddEventDetail.MemberId = resuserdetails.MemberId;
                            objAddEventDetail.CheckDelete = 0;
                            objAddEventDetail.CheckActive = 1;

                            if (objAddEventDetail.GetRecord())
                            {
                                objAddEventDetail.TicketURL = objRes.data[0];
                                objAddEventDetail.Update();
                            }
                            result.data = objRes.data;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = true;
                            result.success = true;
                            response = Request.CreateResponse<Res_Vendor_API_Events_Ticket_Download_Requests>(System.Net.HttpStatusCode.OK, result);

                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }


                    }
                }
                log.Info($"{System.DateTime.Now.ToString()}  Event Ticket Download completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()}  Event Ticket Download {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/get-event-booking")]
        public HttpResponseMessage GetServiceGroupEventsBooking(Req_Vendor_API_Events_Booking_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group Events Booking" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Booking_Requests result = new Res_Vendor_API_Events_Booking_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Booking_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        List<AddEventDetails> objEventList = new List<AddEventDetails>();
                        AddEventDetails w = new AddEventDetails();
                        w.MemberId = resuserdetails.MemberId;
                        w.CheckPaymentDone = 1;
                        w.CheckDelete = 0;
                        w.CheckActive = 1;
                        DataTable dt = w.GetList();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                AddEventDetails obj = new AddEventDetails();
                                obj.MemberId = Convert.ToInt64(dt.Rows[i]["MemberId"]);
                                obj.eventId = Convert.ToInt64(dt.Rows[i]["eventId"]);
                                obj.eventName = dt.Rows[i]["eventName"].ToString();
                                obj.eventDate = Convert.ToDateTime(dt.Rows[i]["eventDate"].ToString());
                                obj.eventDateString = Convert.ToString(dt.Rows[i]["eventDateString"].ToString());
                                obj.eventDateNepali = Convert.ToString(dt.Rows[i]["eventDateNepali"].ToString());
                                obj.eventStartTime = Convert.ToString(dt.Rows[i]["eventStartTime"].ToString());
                                obj.eventEndTime = Convert.ToString(dt.Rows[i]["eventEndTime"].ToString());
                                obj.eventDescription = Convert.ToString(dt.Rows[i]["eventDescription"].ToString());
                                obj.venueName = Convert.ToString(dt.Rows[i]["venueName"].ToString());
                                obj.venueAddress = Convert.ToString(dt.Rows[i]["venueAddress"].ToString());
                                obj.venueImagePath1 = Convert.ToString(dt.Rows[i]["venueImagePath1"].ToString());
                                obj.venueCapacity = Convert.ToInt64(dt.Rows[i]["venueCapacity"].ToString());
                                obj.parkingAvailable = Convert.ToString(dt.Rows[i]["parkingAvailable"].ToString());
                                obj.latitude = Convert.ToInt32(dt.Rows[i]["latitude"].ToString());
                                obj.longitude = Convert.ToInt32(dt.Rows[i]["longitude"].ToString());
                                obj.eventType = Convert.ToString(dt.Rows[i]["eventType"].ToString());
                                obj.organizerName = Convert.ToString(dt.Rows[i]["organizerName"].ToString());
                                obj.bannerImagePath = Convert.ToString(dt.Rows[i]["bannerImagePath"].ToString());
                                obj.showArrivalTime = Convert.ToInt32(dt.Rows[i]["showArrivalTime"]);
                                obj.arrivalTime = Convert.ToString(dt.Rows[i]["arrivalTime"].ToString());
                                obj.isSingleDayEvent = Convert.ToInt32(dt.Rows[i]["isSingleDayEvent"]);
                                obj.merchantCode = Convert.ToString(dt.Rows[i]["merchantCode"].ToString());
                                obj.customerName = Convert.ToString(dt.Rows[i]["customerName"].ToString());
                                obj.customerMobile = Convert.ToString(dt.Rows[i]["customerMobile"].ToString());
                                obj.customerEmail = Convert.ToString(dt.Rows[i]["customerEmail"].ToString());
                                obj.ticketCategoryId = Convert.ToInt64(dt.Rows[i]["ticketCategoryId"]);
                                obj.ticketCategoryName = Convert.ToString(dt.Rows[i]["ticketCategoryName"].ToString());
                                obj.sectionName = Convert.ToString(dt.Rows[i]["sectionName"].ToString());
                                obj.eventDate = Convert.ToDateTime(dt.Rows[i]["eventDate"].ToString());
                                obj.ticketRate = Convert.ToDecimal(dt.Rows[i]["ticketRate"]);
                                obj.noOfTicket = Convert.ToInt64(dt.Rows[i]["noOfTicket"]);
                                obj.totalPrice = Convert.ToDecimal(dt.Rows[i]["totalPrice"].ToString());
                                obj.paymentMethodId = Convert.ToInt32(dt.Rows[i]["paymentMethodId"]);
                                obj.paymentMethodCode = Convert.ToString(dt.Rows[i]["paymentMethodCode"].ToString());
                                obj.paymentMethodName = Convert.ToString(dt.Rows[i]["paymentMethodName"].ToString());
                                obj.paymentMerchantId = Convert.ToString(dt.Rows[i]["paymentMerchantId"].ToString());
                                obj.IsBooked = Convert.ToInt32(dt.Rows[i]["IsBooked"]) == 1;
                                obj.IsPaymentDone = Convert.ToInt32(dt.Rows[i]["IsPaymentDone"]) == 1;
                                obj.OrderId = Convert.ToString(dt.Rows[i]["OrderId"].ToString());
                                obj.TransactionUniqueId = Convert.ToString(dt.Rows[i]["TransactionUniqueId"].ToString());
                                obj.TicketURL = Convert.ToString(dt.Rows[i]["TicketURL"].ToString());
                                obj.eventTermsAndCondition = Convert.ToString(dt.Rows[i]["eventTermsAndCondition"].ToString());
                                obj.eventContactDtls = Convert.ToString(dt.Rows[i]["eventContactDtls"].ToString());
                                obj.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"]);
                                obj.CreatedByName = Convert.ToString(dt.Rows[i]["CreatedByName"].ToString());
                                obj.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);
                                obj.UpdatedDate = Convert.ToDateTime(dt.Rows[i]["UpdatedDate"]);
                                obj.UpdatedBy = Convert.ToInt64(dt.Rows[i]["UpdatedBy"]);
                                obj.UpdatedByName = Convert.ToString(dt.Rows[i]["UpdatedByName"].ToString());
                                obj.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                                obj.IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[i]["IsApprovedByAdmin"]);
                                obj.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                                obj.Id = Convert.ToInt64(dt.Rows[i]["Id"]);
                                objEventList.Add(obj);
                            }
                        }
                        result.data = objEventList;
                        result.ReponseCode = 1;
                        result.status = true;
                        result.Message = "success";
                        result.success = true;

                        response = Request.CreateResponse<Res_Vendor_API_Events_Booking_Requests>(System.Net.HttpStatusCode.Accepted, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                    }
                    else
                    {
                        CommonResponse cres1 = new CommonResponse();
                        cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} Get Service Group Events booking completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events booking {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/event-ticket-email")]
        public HttpResponseMessage ServiceGroupEventTicketEmail(Req_Vendor_API_Events_Ticket_Download_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside  Event Ticket Email" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Events_Ticket_Download_Requests result = new Res_Vendor_API_Events_Ticket_Download_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Events_Ticket_Download_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_Events_Ticket_Download objRes = new GetVendor_API_Events_Ticket_Download();
                        string msg = RepEvents.RequestServiceGroup_Events_Ticket_Download(user.MerchantCode, user.OrderId, ref objRes);
                        result.message = msg;
                        if (msg.ToLower() == "success")
                        {
                            AddEventDetails objAddEventDetail = new AddEventDetails();
                            objAddEventDetail.OrderId = user.OrderId;
                            objAddEventDetail.merchantCode = user.MerchantCode;
                            objAddEventDetail.MemberId = resuserdetails.MemberId;
                            objAddEventDetail.CheckDelete = 0;
                            objAddEventDetail.CheckActive = 1;

                            if (objAddEventDetail.GetRecord())
                            {
                                var d2 = DateTime.UtcNow;
                                var d1 = objAddEventDetail.ticketSentDatetime;

                                TimeSpan t = d2 - d1;
                                double TotalMinutes = t.TotalMinutes;
                                if (TotalMinutes < 60)
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("Ticket have already been sent on your email ID " + objAddEventDetail.customerEmail + ". Please wait for next 1 hr to send a new email");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                }
                                else
                                {
                                    objAddEventDetail.ticketSentDatetime = DateTime.UtcNow;
                                    objAddEventDetail.Update();

                                    string filePath = objRes.data[0];
                                    string fileName = System.IO.Path.GetFileName(filePath);
                                    string localServerPath = HttpContext.Current.Server.MapPath("~/ExportData/" + fileName);
                                    using (var client = new WebClient())
                                    {
                                        client.DownloadFile(new Uri(filePath), localServerPath);
                                    }

                                    string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/ExportData"), fileName);
                                    #region SendEmailConfirmation
                                    string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/EventDetails.html"));
                                    string body = mystring;
                                    body = body.Replace("##UserName##", objAddEventDetail.customerName);
                                    body = body.Replace("##EventName##", objAddEventDetail.eventName);
                                    body = body.Replace("##WebsiteName##", MyPay.Models.Common.Common.WebsiteName);


                                    string Subject = MyPay.Models.Common.Common.WebsiteName + " - Event Tickets";
                                    if (!string.IsNullOrEmpty(objAddEventDetail.customerEmail))
                                    {
                                        Common.SendAsyncMail(objAddEventDetail.customerEmail, Subject, body, fullPath);
                                    }
                                    #endregion


                                    result.ReponseCode = objRes.status ? 1 : 0;
                                    result.status = true;
                                    result.success = true;
                                    result.Message = "Ticket sent successfully";
                                    response = Request.CreateResponse<Res_Vendor_API_Events_Ticket_Download_Requests>(System.Net.HttpStatusCode.OK, result);
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Email not found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }


                    }
                }
                log.Info($"{System.DateTime.Now.ToString()}  Event Ticket Email completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()}  Event Ticket Download {ex.ToString()} " + Environment.NewLine);
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