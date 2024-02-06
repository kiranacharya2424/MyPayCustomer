using MyPay.Models.Common;
using MyPay.Models.Request.WebRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ClosedXML.Excel.XLPredefinedFormat;
using System.Web.Services.Description;
using MyPay.Models;
using System.Runtime.Remoting;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using MyPay.Repository;
using static ServiceStack.LicenseUtils;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using NepaliCalendarBS;
using Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml.EMMA;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using commonresponsedata = MyPay.Models.commonresponsedata;
using commonresponsedatas = MyPay.Models.VendorAPI.Get.BusSewaService.commonresponsedata;
using System.Net;
using System.IO;
using MyPay.Models.Miscellaneous;

namespace MyPay.Controllers
{
    public class BusSewaController : BaseMyPayUserSessionController
    {
        // GET: BusSewa
        [HttpGet]
        public ActionResult Index()
        {
            var result = string.Empty;
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa;
            ViewBag.ServiceId = ServiceId;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/bus-routes";
                    WebRequest_CashbackOffers objReq = new WebRequest_CashbackOffers();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.IsHome = 0;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestBusSewarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            commonresponsedata BusRoute = JsonConvert.DeserializeObject<commonresponsedata>(result);
                            string[] routelist = JsonConvert.DeserializeObject<string[]>(Convert.ToString(BusRoute.Data));
                            ViewBag.Routelist = routelist;
                            System.Runtime.Caching.MemoryCache.Default.Add("key", routelist, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30) });

                            // Retrieve data
                            var data = System.Runtime.Caching.MemoryCache.Default["key"];
                            BusSewaTrip bussewa = new BusSewaTrip();
                            return View(bussewa);
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                            return RedirectToAction("Dashboard", "MyPayUserLogin");
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                    return RedirectToAction("Index", "MyPayUserLogin");
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return View(result);
        }

        [HttpPost]
        public JsonResult SearchBus(BusSewaTrip model)
        {
            var result = string.Empty;
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa;
            ViewBag.ServiceId = ServiceId;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/bus-trip";
                    BusSewaTrip objReq = new BusSewaTrip();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.from = model.from;
                    objReq.to = model.to;
                    objReq.date = model.date;
                    objReq.shift = model.shift;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestBusSewarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            commonresponsedata BusRoute = JsonConvert.DeserializeObject<commonresponsedata>(result);
                            TripDatas BusTrip = JsonConvert.DeserializeObject<TripDatas>(Convert.ToString(BusRoute.Data));
                            string Message = "success";
                            string BUS = JsonConvert.SerializeObject(BusTrip);
                            Session["BusTrip"] = BUS;
                            Session["BusRoute"] = JsonConvert.SerializeObject(model);
                            return Json(new { objReq = objReq, Message });
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult SearchBusList()
        {
            string result = string.Empty;
            string BusTrip = Convert.ToString(Session["BusTrip"]);
            string BusRoute = Convert.ToString(Session["BusRoute"]);
            var data = System.Runtime.Caching.MemoryCache.Default["key"];
            //Session["BusTrip"] = null;
            //Session["BusRoute"] = null;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    if (BusRoute != "" || BusTrip != "")
                    {
                        BusSewaTrip model = JsonConvert.DeserializeObject<BusSewaTrip>(BusRoute);
                        TripDatas tripData = JsonConvert.DeserializeObject<TripDatas>(BusTrip);
                        ViewBag.Routelist = data;
                        ViewData["from"] = model.from;
                        ViewData["to"] = model.to;
                        ViewData["date"] = model.date;
                        ViewData["shift"] = model.shift;
                        //ViewData["operator"+ 0] = tripData.trips[0].operator_name;

                        return View(tripData);
                    }
                    else
                    {
                        ViewBag.Routelist = data;
                        BusSewaTrip bussewa = new BusSewaTrip();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    result = "Invalid Request";
                    return RedirectToAction("Index", "MyPayUserLogin");
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return View(result);
        }

        [HttpGet]
        public ActionResult BusProceed(string TripId, string TicketPrice, string DepartTime, string Operator, string Shift, string BusType, string inputcode)
        {
            string result = string.Empty;
            string BusRoute = Convert.ToString(Session["BusRoute"]);
            Session["BusTrip"] = null;
            
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa;
            ViewBag.ServiceId = ServiceId;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/bus-seat-refresh";
                    BusRefresh objReq = new BusRefresh();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.Id = TripId;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestBusSewarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            commonresponsedata BusSeat = JsonConvert.DeserializeObject<commonresponsedata>(result);
                            RootobjectRefresh BusSeatLayout = JsonConvert.DeserializeObject<RootobjectRefresh>(Convert.ToString(BusSeat.Data));

                            BusSewaTrip model = JsonConvert.DeserializeObject<BusSewaTrip>(BusRoute);
                            ViewData["BusSewaTrip"] = model;
                            ViewData["from"] = model.from;
                            ViewData["to"] = model.to;
                            ViewData["date"] = model.date;
                            ViewData["shift"] = model.shift;
                            ViewData["ticketPrice"] = TicketPrice;
                            ViewData["departTime"] = DepartTime;
                            ViewData["BusId"] = TripId;
                            ViewData["BusType"] = BusType;
                            ViewData["inputCode"] = inputcode;
                            ViewBag.OperatorName = Operator;
                            ViewBag.Shift = Shift;
                            //ViewBag.Shift = Shift;
                            ViewBag.BusSeatLayout = BusSeatLayout.seatLayout;
                            string Message = BusSeat.Message;
                            return View(BusSeatLayout);
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                    return RedirectToAction("Index", "MyPayUserLogin");
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return View(result);
        }

        [HttpPost]
        public JsonResult BookSeat(string data, string DepartureDate, string DepartureTime, string From, string To, string Operator, string BusType)
        {
            var result = string.Empty;
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa;
            ViewBag.ServiceId = ServiceId;
            //object BusRoutes = Session["BusRoute"];
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/bus-book-seat";
                    Busbookseat objReq = new Busbookseat();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]); 
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.data = data;
                    objReq.Operator = Operator;
                    objReq.BusType = BusType;
                    objReq.DepatureDate = DepartureDate;
                    objReq.DepatureTime = DepartureTime;
                    objReq.from = From;
                    objReq.to = To;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestBusSewarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            commonresponsedatas Response = JsonConvert.DeserializeObject<commonresponsedatas>(Convert.ToString(result));
                            RootobjectBookSeat Data = JsonConvert.DeserializeObject<RootobjectBookSeat>(Convert.ToString(Response.Data));
                            return Json(new { objReq = Response, boardingPoints = Data.boardingPoints, ticketSrlNo = Data.ticketSrlNo});
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayBusPassenger(string data, string BusdetailId, string inputcode)
        {
            var result = string.Empty;
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa;
            ViewBag.ServiceId = ServiceId;
            //object BusRoutes = Session["BusRoute"];
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/bus-passengerInfo";
                    BusPassengerInfo objReq = new BusPassengerInfo();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.inputcode = inputcode;
                    objReq.BusDetailId = BusdetailId;
                    objReq.data = data;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestBusSewarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status && objResponse.Message.ToLower() == "success")
                        {
                            CustomResponseException Response = JsonConvert.DeserializeObject<CustomResponseException>(result);
                            ViewData["BusSewaTrip"] = null;
                            ViewData["from"] = null;
                            ViewData["to"] = null;
                            ViewData["date"] = null;
                            ViewData["shift"] = null;
                            ViewData["ticketPrice"] = null;
                            ViewData["departTime"] = null;
                            ViewData["BusId"] = null;
                            ViewData["BusType"] = null;
                            Session["BusTrip"] = null;
                            Session["BusRoute"] = null;
                            string Message = "success";
                            return Json(new { objReq = Response , Message = Message});
                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult MyPayBusSewaPayment(string Id, string ticketSrlNo, string Amount, string busdetailId, string Mpin, string PaymentMode)
        {
            var result = string.Empty;
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa;
            ViewBag.ServiceId = ServiceId;
            //object BusRoutes = Session["BusRoute"];
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/bus-payment-confirmation";
                    BuspaymentConfirmation objReq = new BuspaymentConfirmation();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.PaymentMode = PaymentMode;
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.Mpin = Common.Encryption(Mpin);
                    objReq.Pin = Common.Encryption(Mpin);
                    objReq.id = Id;
                    objReq.ticketSrlNo = ticketSrlNo;
                    objReq.Amount = Amount;
                    objReq.BusDetailId = busdetailId;
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestBusSewarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        WebCommonResponse objResponse = new WebCommonResponse();
                        objResponse = JsonConvert.DeserializeObject<WebCommonResponse>(result);
                        if (objResponse.status)
                        {

                        }
                        else if (objResponse.ReponseCode == 7)
                        {
                            RepMyPayUserLogin.Logout();
                            result = objResponse.Message;
                        }
                        else
                        {
                            result = objResponse.Message;
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult MyPayUserBusBooking()
        {
            var result = string.Empty;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    int ServiceID = (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa;
                    var data = VendorApi_CommonHelper.getBusSewaDetails(Session["MyPayUserMemberId"].ToString(), ServiceID.ToString());
                    ViewBag.ServiceId = ServiceID;
                    ViewBag.MyPayContactNumber = Session["MyPayContactNumber"].ToString();
                    ViewBag.MyPayEmail = Session["MyPayEmail"].ToString();
                    ViewBag.MyPayFullName = Session["MyPayFullName"].ToString();
                    ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
                    return View(data);
                }
                else
                {
                    result = "Invalid Request";
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return View(result);
        }
        public ActionResult MyPayUserBusTicketDownload(string LogID)
        {
            var result = string.Empty;
            ViewBag.IsKycVerified = Convert.ToString(Session["IsKycVerified"]);
            ViewBag.MyPayUserWalletbalance = Convert.ToString(Session["MyPayUserWalletbalance"]);
            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.bus_sewa;
            ViewBag.ServiceId = ServiceId;
            try
            {
                if (Session["MyPayUserMemberId"] != null && !string.IsNullOrEmpty(Session["MyPayUserMemberId"].ToString()))
                {
                    string ApiName = "api/DownloadPDF?";
                    BusTicketDownload objReq = new BusTicketDownload();
                    objReq.DeviceId = Convert.ToString(Session["MyPayUserBrowserID"]);
                    objReq.MemberId = Convert.ToInt64(Session["MyPayUserMemberId"]);
                    objReq.IsMobile = false;
                    objReq.PlatForm = "Web";
                    objReq.SecretKey = Common.SecretKeyForWebAPICall;
                    objReq.LogID = LogID;
                    objReq.VendorApiType = Convert.ToString(ServiceId);
                    string JSON = JsonConvert.SerializeObject(objReq);
                    string JwtToken = Convert.ToString(Session["MyPayUserJWTToken"]);
                    result = Common.RequestBusSewarAPI(ApiName, JSON, JwtToken);
                    if (!string.IsNullOrEmpty(result))
                    {
                        CustomResponseException data = JsonConvert.DeserializeObject<CustomResponseException>(result);
                        GetVendor_API_Airlines_Lookup obj = JsonConvert.DeserializeObject<GetVendor_API_Airlines_Lookup>(Convert.ToString(data.Data));
                        string pathName = obj.FilePath;
                        string fileName = Path.GetFileName(pathName);
                        try
                        {
                            using (WebClient client = new WebClient())
                            {
                                byte[] fileBytes = client.DownloadData(pathName);
                                return File(fileBytes, "application/pdf", fileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        result = "Invalid API Request";
                    }
                }
                else
                {
                    result = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return RedirectToAction("MyPayUserBusBooking");
        }
    }
}