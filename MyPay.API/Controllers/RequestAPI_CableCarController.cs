
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
using MyPay.API.Models.Request.CableCar;
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
using iTextSharp.text.pdf;
using iTextSharp.tool.xml.html;
using System.Web.Configuration;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using System.Collections;
//using System.Web.Mvc;

namespace MyPay.API.Controllers
{
    public class RequestAPI_CableCarController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_CableCarController));
        string ApiResponse = string.Empty;
        #region for Get_CableTicketTypes
        [HttpPost]
        [Route("api/get_CableTicketTypes")]
        public HttpResponseMessage GetTicketTypes(Req_API_Get_CableTicket_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get_Ticket_Types" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_TicketTypes result = new Res_Vendor_API_TicketTypes();

            var response = Request.CreateResponse<Res_Vendor_API_TicketTypes>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //Common.CheckHash(user);
                    string md5hash = Common.getHashMD5(UserInput);
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
                        string KhaltiAPIURL = "https://182.93.95.45:8091/api/ipg/TicketTypes";
                        string ApiResponse = string.Empty;
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        GetVendor_API_GetTicketTypes objRes = new GetVendor_API_GetTicketTypes();
                        string msg = String.Empty;
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        msg = RepKhalti.RequestGetTicketTypes(ref objVendor_API_Requests, user.Reference, user.Version, user.MemberId, user.DeviceCode, user.PlatForm, ref objRes);

                        if (!msg.Contains("TripType"))
                        {
                            cres.responseMessage = msg;
                            cres.status = false;
                            cres.ReponseCode = 0;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else
                        {
                            var data = JsonConvert.DeserializeObject(msg);
                            var cabledata = data.ToString();
                            cres.Message = "Success";
                            cres.Data = data;
                            cres.status = true;
                            cres.ReponseCode = 1;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, cres);
                            return response;
                        }

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-Cable completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-cable {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        #endregion GET Tiket types

        [HttpPost]
        [Route("api/CablePayTransaction")]
        public HttpResponseMessage PayCableTransaction(PayTransactionCableCar user)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Info($" {System.DateTime.Now.ToString()} inside CablePayTransaction  {Environment.NewLine}");
            string UserInput = getRawPostData().Result;
            string FinalResponse = string.Empty;
            CommonResponse cres = new CommonResponse();
            Res_Out_CableAPI_Requests RCP = new Res_Out_CableAPI_Requests();
            Res_Vendor_API_Requests result = new Res_Vendor_API_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.getHashMD5(UserInput);
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
                        AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Cable_Car;
                            int Type = Convert.ToInt32(user.PaymentMode);
                            PayTransactionCableCar payTransactionCableCar = new PayTransactionCableCar();
                            resUser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.Amount, true, user.Mpin);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Req_ReferenceNo = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false;
                        string TransactionID = string.Empty;
                        List<CableTicketDetails> mt = JsonConvert.DeserializeObject<List<CableTicketDetails>>(user.data);
                        List<CableTicketDetails> CTD = new List<CableTicketDetails>();
                        foreach (CableTicketDetails ticket in mt)
                        {

                            CableTicketDetails cableTicketDetails = new CableTicketDetails();
                            cableTicketDetails.PassengerType = ticket.PassengerType;
                            cableTicketDetails.PassengerCount = ticket.PassengerCount;
                            cableTicketDetails.TripType = ticket.TripType;
                            CTD.Add(cableTicketDetails);

                        }
                        string Ticketdatajson = JsonConvert.SerializeObject(CTD);
                        JToken jdataToken = JToken.Parse(Ticketdatajson);
                        user.TicketDetails = jdataToken;

                        CableResponses CRP = new CableResponses();
                        string msg = RepKhalti.PayTransactionCable(ref TransactionID, user.CustomerWalletID, user.Req_ReferenceNo, user.Amount, user.Number, user.MemberId, user.TotalPrice, user.User
                             , UserInput, user.DeviceCode, resUser, ref objVendor_API_Requests, user.TicketDetails, user.Reference, user.Version, user.PlatForm, authenticationToken, user.BankTransactionId, user.PaymentMode, ref IsCouponUnlocked, resGetCouponsScratched, user.Mpin, ref CRP);

                        if (CRP != null)
                        {
                            user.ReferenceNo = CRP.ReferenceNo;
                            user.TransactionId = CRP.TransactionID;
                        }

                        if (!msg.Contains("TransactionID"))
                        {
                            RCP.Message = msg;
                            RCP.status = false;
                            RCP.ReponseCode = 0;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, RCP);
                            return response;
                        }

                        else
                        {
                            CableCommon cableCommon = new CableCommon();
                            var CBSP = JsonConvert.DeserializeObject<CableResponses>(msg);
                            cableCommon = new CommonHelpers().AddResponseCable(CBSP.ReferenceNo, CBSP.TransactionID, CBSP.ResponseCode);
                            if (CBSP.ReferenceNo != null)
                            {
                                RCP.responseMessage = CBSP.ResponseDescription;
                                RCP.Message = "Success";
                                RCP.ReponseCode = 1;
                                RCP.status = true;
                                RCP.TransactionUniqueId = TransactionID;
                                RCP.ReferenceNo = CBSP.ReferenceNo;
                                FinalResponse = Newtonsoft.Json.JsonConvert.SerializeObject(RCP);
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, RCP);
                                //return response;
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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



                response = GetTicketInvoice(user, UserInput);
                log.Info($"  {System.DateTime.Now.ToString()}   RequestAPI_TopupController Completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()}   CableCarController {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()}   CableCarController {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return response;
        }
        [HttpPost]
        [Route("api/Get_TicketInvoice")]
        public HttpResponseMessage GetTicketInvoice(PayTransactionCableCar user, string UserInput)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Info($" {System.DateTime.Now.ToString()} inside GetTicketInvoice  {Environment.NewLine}");
            // string UserInput = getRawPostData().Result;
            string FinalResponse = string.Empty;
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Requests result = new Res_Vendor_API_Requests();
            Res_Vendor_CableAPI_TicketResponse RC = new Res_Vendor_CableAPI_TicketResponse();
            var response = Request.CreateResponse<Res_Vendor_API_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.getHashMD5(UserInput);
                    //string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    //if (results != "Success")
                    //{
                    //    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    //    response.StatusCode = HttpStatusCode.BadRequest;
                    //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    //    return response;
                    //}
                    //else
                    //{
                    string CommonResult = "";
                    AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string TransactionID = string.Empty;
                    var jsondatauser = new PayTransactionCableCar
                    {

                        TransactionId = user.TransactionId,
                        ReferenceNo = user.ReferenceNo,

                    };
                    object paycabledata = Newtonsoft.Json.JsonConvert.SerializeObject(jsondatauser);
                    string msg = RepKhalti.GetTicketInvoiceCable(user.TransactionId, user.CustomerWalletID, user.Req_ReferenceNo, user.Amount, user.Number, user.MemberId, user.TotalPrice, user.User
                        , UserInput, user.DeviceCode, resUser, ref objVendor_API_Requests, paycabledata);
                    var CBSP = JsonConvert.DeserializeObject<TicketResult>(msg);

                    List<TicketResponse> TR = JsonConvert.DeserializeObject<List<TicketResponse>>(JsonConvert.SerializeObject(CBSP.TicketResponse));
                    List<TicketResponse> LTD = new List<TicketResponse>();

                    TicketInvoiceCommon TicketCommon = new TicketInvoiceCommon();
                    foreach (var item in TR)
                    {
                        TicketResponse TicketResponse = new TicketResponse();
                        TicketResponse.QRCode = item.QRCode;
                        TicketResponse.BarCode = item.BarCode;
                        TicketResponse.PassengerType = item.PassengerType;
                        TicketResponse.TripType = item.TripType;
                        TicketResponse.TicketNumber = item.TicketNumber;
                        TicketResponse.ValidUntil = item.ValidUntil;
                        LTD.Add(TicketResponse);
                    };
                    var TicketData = JsonConvert.SerializeObject(LTD);
                    TicketCommon = new CommonHelpers().AddTicketInvoiceCable(TicketData, CBSP.InvoiceResponse, user.TransactionId, user.MemberId, msg);
                    if (CBSP.InvoiceResponse.ReferenceId != null)
                    {
                        RC.Message = "Sucess";
                        RC.status = true;
                        RC.ReponseCode = 1;
                        RC.InvoiceResponse = CBSP.InvoiceResponse;
                        RC.ticketResponse = CBSP.TicketResponse;
                        response.StatusCode = HttpStatusCode.Accepted;
                        RC.TransactionUniqueId = user.TransactionId;
                        RC.Details = "Payment successful for Annapurna Cable car";
                        RC.responseMessage = "Payment successful for Annapurna Cable car";
                        response = Request.CreateResponse<Res_Vendor_CableAPI_TicketResponse>(System.Net.HttpStatusCode.OK, RC);
                        return response;
                    }
                    else
                    {

                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        cres.status = false;
                        cres.ReponseCode = 3;
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


                log.Info($"  {System.DateTime.Now.ToString()}   RequestAPI_CableCarController Completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()}   CableCarController {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()}   CableCarController {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        [HttpPost]
        [Route("api/GetReconcileCableCar")]
        public HttpResponseMessage ReconcillationCable(PayTransactionCableCar user)
        {

            log.Info($" {System.DateTime.Now.ToString()} inside ReconcileCableCar  {Environment.NewLine}");
            string FinalResponse = string.Empty;
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Requests result = new Res_Vendor_API_Requests();
            Reconcile RP = new Reconcile();
            var response = Request.CreateResponse<Res_Vendor_API_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string UserInput = "";
                        AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Req_ReferenceNo = "";
                        string TransactionID = string.Empty;
                        var jsondatauser = new PayTransactionCableCar
                        {

                            TransactionId = user.TransactionId,
                            FromDate = user.FromDate,
                            ToDate = user.ToDate,

                        };

                        object paycabledata = Newtonsoft.Json.JsonConvert.SerializeObject(jsondatauser);
                        string msg = RepKhalti.GetTickeGetReconcile(user.TransactionId, user.CustomerWalletID, user.SecretKey, user.DeviceId, user.Number, user.MemberId, user.TotalPrice, user.User
                            , UserInput, user.DeviceCode, resUser, ref objVendor_API_Requests, paycabledata);

                        if (!msg.Contains("TransactionId"))
                        {
                            cres.responseMessage = msg;
                            cres.status = false;
                            cres.ReponseCode = 0;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);

                        }
                        else
                        {
                            var CBSP = JsonConvert.DeserializeObject<Reconcile>(msg);
                            RP.status = true;
                            RP.Message = "Success";
                            RP.ReponseCode = 1;
                            RP.ReconcileResponse = CBSP.ReconcileResponse;
                            response = Request.CreateResponse<Reconcile>(System.Net.HttpStatusCode.OK, RP);

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
                //RepKhalti.SaveAPIResponse(RepKhalti.Id, Newtonsoft.Json.JsonConvert.SerializeObject(FinalResponse));

                log.Info($"  {System.DateTime.Now.ToString()}   RequestAPI_TopupController Completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()}   RequestAPI_TopupController {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()}   RequestAPI_TopupController {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        [HttpPost]
        [Route("api/downloadServiceTicket")]
        public void DownloadTicket(string TransactionID)
        {
            WalletTransactions uw = new WalletTransactions();
            uw.TransactionUniqueId = TransactionID;

            if (uw.GetRecord())
            {
                switch (uw.Type)
                {
                    case 300:

                        GetVendor_API_Airlines_Lookup objRes = new GetVendor_API_Airlines_Lookup();
                        var responsed = Request.CreateResponse<GetVendor_API_Airlines_Lookup>(System.Net.HttpStatusCode.BadRequest, objRes);
                        try
                        {

                            string originalFileName = "";

                            // Add code for New PDF ticket
                            #region New Ticket PDF
                            originalFileName = $"CableCar_Ticket_{System.DateTime.UtcNow.ToFileTimeUtc()}";
                            string jsonData = "";
                            ReceiptsVendorResponse objreceiptsVendorResponse = null;
                            objreceiptsVendorResponse = VendorApi_CommonHelper.GetCableCarReceipts(TransactionID);
                            var CBSP = JsonConvert.DeserializeObject<TicketResult>(objreceiptsVendorResponse.table2JSONContent);
                            var Tdd = new InvoiceResponse
                            {
                                TicketMessage = CBSP.InvoiceResponse.TicketMessage,
                                ReferenceId = CBSP.InvoiceResponse.ReferenceId,
                                TotalAmount = CBSP.InvoiceResponse.TotalAmount.ToString(),
                                UserName = CBSP.InvoiceResponse.UserName,
                            };

                            // Var Ticked = JsonConvert.<InvoiceResponses>(CBSP.InvoiceResponse);
                            List<TicketResponse> TR = JsonConvert.DeserializeObject<List<TicketResponse>>(JsonConvert.SerializeObject(CBSP.TicketResponse));
                            List<TicketResponse> LTD = new List<TicketResponse>();
                            TicketInvoiceCommon TicketCommon = new TicketInvoiceCommon();

                            string tableData = "";
                            tableData = "<center><table>";

                            int numberOfRowsRequired = TR.Count / 2;

                            var TDCount = 0;

                            foreach (var item in TR)
                            {
                                TicketResponse TicketResponse = new TicketResponse();
                                TicketResponse.QRCode = item.QRCode;
                                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                QRCodeData qrCodeData = qrGenerator.CreateQrCode(item.QRCode, QRCodeGenerator.ECCLevel.Q);
                                QRCode qrCode = new QRCode(qrCodeData);
                                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                                string ImageUrl = "";
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    qrCodeImage.Save(stream, ImageFormat.Png);
                                    byte[] byteImage = stream.ToArray();
                                    ImageUrl = string.Format(Convert.ToBase64String(byteImage));
                                }
                                // qrCodeImage.Save(qrFilePath + qrImageName, ImageFormat.Png);
                                //GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(item.BarCode, BarcodeWriterEncoding.Code128);
                                //barcode.ResizeTo(400, 120);
                                //barcode.AddBarcodeValueTextBelowBarcode();
                                //barcode.ChangeBarCodeColor(Color.Black);
                                //barcode.SetMargins(10);
                                //var GuId1 = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                                //string qrFilePathbar = HttpContext.Current.Server.MapPath("~/Content/CableBarCode/");
                                //string BarImageName = "BarImage" + GuId1 + ".png";
                                //string ImageUrlBar = "";
                                //string tempFilePath = "";
                                //// Save the barcode image to the temporary file
                                //barcode.SaveAsPng(qrFilePathbar+ BarImageName);

                                //// Read the file into a MemoryStream
                                //using (FileStream fileStream = new FileStream(qrFilePathbar, FileMode.Open, FileAccess.Read))
                                //using (MemoryStream stream = new MemoryStream())
                                //{
                                //    fileStream.CopyTo(stream);
                                //    byte[] barcodeImageBytes = stream.ToArray();
                                //    ImageUrlBar = Convert.ToBase64String(barcodeImageBytes);
                                //}

                                //// Delete the temporary file if needed
                                //File.Delete(tempFilePath);
                                TicketResponse.PassengerType = item.PassengerType;
                                TicketResponse.TripType = item.TripType;
                                TicketResponse.TicketNumber = item.TicketNumber;
                                TicketResponse.ValidUntil = item.ValidUntil;
                                LTD.Add(TicketResponse);

                                string mystring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/CableCardownlod.html"));
                                string body = mystring;
                                if (TDCount % 2 == 0)
                                {
                                    tableData += "<tr>";
                                }

                                tableData += "<table cellspacing=\"0\" border=\"0\" cellpadding=\"0\" width=\"100%\" bgcolor=\"#f2f3f8\" style=\"@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;\">\r\n " +
                                    "<tr>\r\n <td>\r\n <table style=\"background-color: #fff; max-width:570px;  margin:0 auto;\" width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">\r\n<tr>\r\n<td style=\"height:80px;\">&nbsp;</td>\r\n </tr>\r\n<tr>\r\n" +
                                    "<td style=\"height:20px;\">&nbsp;</td>\r\n</tr>" +
                                    "\r\n <tr>\r\n <td style=\"text-align:center;\">\r\n <img width=\"160\" src=\"\" title=\"logo\" alt=\"logo\" style=\"margin: 0;\" />\r\n  <h1 style=\"color:#1e1e2d; font-weight:500; margin:15px 0;font-size:22px;font-family:'Rubik',sans-serif;\">Ticket Information </h1>\r\n  <span style=\"display:inline-block; vertical-align:middle; margin:0px 0; border-bottom:1px solid #cecece; width:100px;\" />\r\n  <h5 style=\"color:#1e1e2d; font-weight:500; margin:15px 0;font-size:13px;font-family:'Rubik',sans-serif;\"> Transaction Id. : " + objreceiptsVendorResponse.TxnID + " </h5>\r\n </td>\r\n </tr>\r\n <tr>\r\n<td>" +
                                    "\r\n<table width=\"95%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"max-width:670px; border-radius:3px;\">\r\n  <tr>\r\n <td style=\"height:20px;\">&nbsp;</td>\r\n  </tr>\r\n  <tr>\r\n  <td style=\"text-align:center;\">\r\n <img width=\"160\"" +
                                    " src=\"https://annapurnacablecar.com.np/img/4ca76379-3811-4e0c-9419-af71bd471a77/footer-logo.png\" title=\"logo\" alt=\"logo\" style=\"margin: 0;\" />\r\n\r\n </td>\r\n<td style=\"padding:0 15px;\">\r\n   <p style=\"color:#000000; font-size:15px;line-height:24px; margin:0;\">\r\n <strong>Annapurna Cable Car </strong>\r\n </p>\r\n <p style=\"color:#000000; font-size:15px;line-height:24px; margin:0;\">\r\n  <strong>Passenger Type:" + item.PassengerType + "</strong>\r\n </p>\r\n <p style=\"color:#000000; font-size:15px;line-height:24px; margin:0;\">\r\n  <strong>TicketNo :" + item.TicketNumber + "</strong>\r\n </p>\r\n </td>\r\n <td style=\"padding:0 15px;\">\r\n <p style=\"color:hsl(0, 0%, 0%); font-size:15px;line-height:24px; margin:0;\">\r\n " +
                                    " <strong>Rs:" + Tdd.TotalAmount + "</strong>\r\n  </p>\r\n<p style=\"color:#147900; font-size:15px;line-height:24px; margin:0;\">\r\n <strong>" + item.TripType + "</strong>\r\n  </p>\r\n <p style=\"color:#000000; font-size:15px;line-height:24px; margin:0;\">\r\n " +
                                    "<strong>Valid Till : " + item.ValidUntil + "  </strong>\r\n  </p>\r\n </td>\r\n </tr>\r\n  <tr>\r\n<td style=\"height:40px;\">&nbsp;</td>\r\n</tr>\r\n </table>\r\n </td>\r\n  </tr>\r\n <tr>\r\n  <td style=\"text-align:center;\">\r\n <img width=200 height=200 src=\"data: image/png; base64," + ImageUrl + "\"\\\"/> \r\n</td>\r\n\r\n  </tr>\r\n  <tr>\r\n <td style=\"height:20px;\">&nbsp;</td>\r\n  </tr>\r\n <tr>\r\n <td style=\"text-align:center;\">\r\n <p style=\"font-size:16px; color:rgba(0, 0, 0, 0.741); line-height:24px; margin:0 0 0;\">\r\n <strong>Purchased by : " + objreceiptsVendorResponse.FullName + "</strong>\r\n  </p>\r\n <p style=\"font-size:16px; color:rgba(0, 0, 0, 0.741); line-height:24px; margin:0 0 0;\">\r\n" +
                                    " <strong>Contact No. : " + objreceiptsVendorResponse.ContactNumber + " </strong>\r\n  </p>\r\n <span style=\"display:inline-block; vertical-align:middle; margin:10px 0; border-bottom:1px dashed #777777; width:500px;\" />\r\n </td>" +
                                    "\r\n </tr>\r\n <tr>\r\n  <td style=\"padding:0 50px;\">\r\n <p style=\"font-size:16px; color:rgba(0, 0, 0, 0.741); line-height:24px; margin:0 0 0;\">\r\n <strong>Keep In Mind</strong>\r\n   </p>\r\n   </td>\r\n</tr>\r\n <tr>\r\n <td style=\"padding:10px 25px; font-size: 13px;\">\r\n <ul>\r\n   <li> Valid Identification required for Students and Senior Citizens (60yrs & above).</li>\r\n <li>Failure in Passenger Verification will Require New Ticket Purchase.</li>\r\n <li>This ticket is non-transferrable, non-refundable and non-exchangeable.</li>\r\n <li>This ticket may not be altered, copied, transferred or resold.</li>\r\n<li>Every passenger is insured upto NPR 5,00,000/-</li>\r\n  <li>15 KG Baggage per Person Allowed. Extra Charge for excess Baggage.  </li>\r\n </ul>\r\n  </td>\r\n  </tr>\r\n  <tr>\r\n <td style=\"text-align:center;\">\r\n <p style=\"font-style: italic;font-size:16px; color:rgba(0, 0, 0, 0.741); line-height:24px; margin:0 0 0;\">\r\n <strong>Thank You,</strong>\r\n  </p>\r\n  </td>\r\n </tr>\r\n <tr>\r\n <td style=\"text-align:center;\">\r\n" +
                                    " <img width=\"160\" src=\"\" title=\"logo\" alt=\"logo\" style=\"margin: 0;\" />\r\n\r\n  </td>"
                                    + "\r\n </tr>\r\n  <tr>\r\n <td style=\"text-align:center;\">\r\n  <p style=\"font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;\">\r\n  &copy; <strong>www.mypay.com.np</strong>\r\n </p>\r\n </td>\r\n  </tr>\r\n <tr>\r\n <td style=\"height:80px;\">&nbsp;</td>\r\n  </tr>\r\n </table>\r\n </td>\r\n</tr>\r\n </table>" + "</p>" + "<hr>" + "</br>" + "\r\n<tr> \r\n"
                                     ;

                                TDCount += 1;

                                if (TDCount % 2 == 0)
                                {
                                    tableData += "</tr>";
                                }
                            };
                            tableData += "</table></center>";

                            if (objreceiptsVendorResponse.TxnID != null)
                            {
                                string mystring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/CableCardownlod.html"));
                                string body = mystring;
                                String str = "";
                                body = body.Replace("##tr##", str);
                                body = body.Replace("##QRTABLE##", tableData);
                                System.IO.File.Create(HttpContext.Current.Server.MapPath("~/Content/CableCarTicketPDF/" + originalFileName + ".html")).Dispose();
                                System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/Content/CableCarTicketPDF/" + originalFileName + ".html"), body);
                                System.IO.File.Create(HttpContext.Current.Server.MapPath("~/Content/CableCarTicketPDF/" + originalFileName + ".pdf")).Dispose();
                                FileInfo htmlsource = new FileInfo(HttpContext.Current.Server.MapPath("~/Content/CableCarTicketPDF/" + originalFileName + ".html"));
                                FileInfo pdfDest = new FileInfo(HttpContext.Current.Server.MapPath("~/Content/CableCarTicketPDF/" + originalFileName + ".pdf"));

                                // pdfHTML specific code
                                ConverterProperties converterProperties = new ConverterProperties();
                                HtmlConverter.ConvertToPdf(htmlsource, pdfDest, converterProperties);
                                // response
                                HttpContext.Current.Response.ContentType = "application/pdf";
                                // HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + originalFileName + ".pdf");
                                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + originalFileName + ".pdf");
                                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("~/Content/CableCarTicketPDF/" + originalFileName + ".pdf"));
                                //Response.End();
                                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                                                                      //HttpContext.Current.Response.SuppressContent = true;
                                                                      //HttpContext.Current.Response.TransmitFile(HttpContext.Current.Server.MapPath("~/Content/CableCarTicketPDF/" + originalFileName + ".pdf"));
                                                                      //HttpContext.Current.Response.End();




                            }
                            #endregion
                            //objRes.status = true;
                            //objRes.Message = "success";
                            //objRes.FilePath = HttpContext.Current.Server.MapPath("~/Content/CableCarTicketPDF/" + originalFileName);
                            //responsed = Request.CreateResponse<GetVendor_API_Airlines_Lookup>(System.Net.HttpStatusCode.OK, objRes);

                        }
                        catch (WebException e)
                        {
                            using (WebResponse response = e.Response)
                            {
                                HttpWebResponse httpResponse = (HttpWebResponse)response;
                                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                                using (Stream data = response.GetResponseStream())
                                using (var reader = new StreamReader(data))
                                {
                                    var json = reader.ReadToEnd();
                                    var Exceptiondata = (JObject)JsonConvert.DeserializeObject(json);
                                    string Error = ((Exceptiondata["error"] == null) ? String.Empty : Convert.ToString(Exceptiondata["error"])); string message = ((Exceptiondata["message"] == null) ? String.Empty : Convert.ToString(Exceptiondata["message"]));
                                    string Details = String.Empty;
                                    Details = ((Exceptiondata["details"] == null) ? String.Empty : Convert.ToString(Exceptiondata["details"]));
                                    objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ("Request Failed " + Details + " " + Error + " " + message) : objRes.Message;
                                    //return objRes;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            objRes.status = false;
                            objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                            //  return objRes;
                        }

                        break;
                    case 200:
                        DownloadPDF objResBusSewa = new DownloadPDF();
                        try
                        {
                            // Reference = new CommonHelpers().GenerateUniqueId();
                            //string LookupURL = $"{KhaltiAPIURL}/{LogID}?token={Req_TokenLive}&reference={Reference}";
                            //string basepath = Common.Common.GetBasePath(relativepath);
                            string originalFileName = $"BusTicket{System.DateTime.UtcNow.ToFileTimeUtc()}.pdf";
                            // fullPath = System.IO.Path.Combine(basepath, originalFileName);
                            Receipt receipt = new Receipt();
                            //get data bus sewa passenger
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            Hashtable HT = new Hashtable();
                            HT.Add("flag", "get");
                            HT.Add("Id", TransactionID);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            dt = commonHelpers.GetDataFromStoredProcedure("sp_BusDetail", HT);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow row = dt.Rows[0];
                                receipt.from = !string.IsNullOrEmpty(row["TripFrom"].ToString()) ? row["TripFrom"].ToString() : "";
                                receipt.to = !string.IsNullOrEmpty(row["TripTo"].ToString()) ? row["TripTo"].ToString() : "";
                                receipt.ticketSrlNo = !string.IsNullOrEmpty(row["TicketSerialNo"].ToString()) ? row["TicketSerialNo"].ToString() : "";
                                receipt.seat = !string.IsNullOrEmpty(row["Seat"].ToString()) ? row["Seat"].ToString() : "";
                                receipt.BookingDate = !string.IsNullOrEmpty(row["BookingDate"].ToString()) ? Convert.ToDateTime(row["BookingDate"]).ToString("yyyy-MM-dd") : "";
                                receipt.Time = !string.IsNullOrEmpty(row["DepartureTime"].ToString()) ? Convert.ToDateTime(row["DepartureTime"]).ToString("h:mm tt") : "";
                                receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? Convert.ToDateTime(row["DepartureDate"].ToString()).ToString("yyyy-MM-dd") : "";
                                //receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? Convert.ToDateTime(Convert.ToDateTime(row["DepartureDate"].ToString()).ToString("yyyy-MM-dd") + ' ' + row["DepartureTime"].ToString()).ToString("yyyy-MM-dd HH:mm") : "";
                                receipt.boardingPoint = !string.IsNullOrEmpty(row["PassengerBoardingPoint"].ToString()) ? row["PassengerBoardingPoint"].ToString() : "";
                                receipt.Operator = !string.IsNullOrEmpty(row["Operator"].ToString()) ? row["Operator"].ToString() : "";
                                receipt.BusType = !string.IsNullOrEmpty(row["BusType"].ToString()) ? row["BusType"].ToString() : "";
                                receipt.BusNo = !string.IsNullOrEmpty(row["BusNo"].ToString()) ? row["BusNo"].ToString() : "";
                                receipt.PaymentStatus = !string.IsNullOrEmpty(row["PaymentStatus"].ToString()) ? row["PaymentStatus"].ToString() : "";
                                receipt.Platform = !string.IsNullOrEmpty(row["Platform"].ToString()) ? row["Platform"].ToString() : "";
                                receipt.Amount = !string.IsNullOrEmpty(row["Amount"].ToString()) ? row["Amount"].ToString() : "0.00";
                                receipt.email = !string.IsNullOrEmpty(row["email"].ToString()) ? row["email"].ToString() : "";
                                receipt.name = !string.IsNullOrEmpty(row["PassengerName"].ToString()) ? row["PassengerName"].ToString() : "";
                                receipt.contactNumber = !string.IsNullOrEmpty(row["ContactNumber"].ToString()) ? row["ContactNumber"].ToString() : "";
                                receipt.contactInfo = !string.IsNullOrEmpty(row["operatorcontactInfo"].ToString()) ? row["operatorcontactInfo"].ToString() : "";
                                //receipt.boardingPoint = !string.IsNullOrEmpty(row["BoardingPoint"].ToString()) ? row["BoardingPoint"].ToString() : "";
                                receipt.TransactionDate = !string.IsNullOrEmpty(row["TransactionDate"].ToString()) ? row["TransactionDate"].ToString() : "";
                                receipt.ServiceCharge = !string.IsNullOrEmpty(row["ServiceCharge"].ToString()) ? row["ServiceCharge"].ToString() : "0.00";
                                receipt.Type = !string.IsNullOrEmpty(row["Type"].ToString()) ? row["Type"].ToString() : "";
                                receipt.FirstName = !string.IsNullOrEmpty(row["FirstName"].ToString()) ? row["FirstName"].ToString() : "";
                                receipt.LastName = !string.IsNullOrEmpty(row["LastName"].ToString()) ? row["LastName"].ToString() : "";
                                receipt.MiddleName = !string.IsNullOrEmpty(row["MiddleName"].ToString()) ? row["MiddleName"].ToString() : "";
                                receipt.userContact = !string.IsNullOrEmpty(row["userContact"].ToString()) ? row["userContact"].ToString() : "";
                                var name = !string.IsNullOrEmpty(receipt.MiddleName) ? receipt.FirstName + ' ' + receipt.MiddleName + ' ' + receipt.LastName : receipt.FirstName + ' ' + receipt.LastName;

                                string[] dataArraySeat = JsonConvert.DeserializeObject<string[]>(receipt.seat);
                                string commaSeparatedDataSeat = string.Join(", ", dataArraySeat);

                                string[] seats = commaSeparatedDataSeat.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                                int numberOfSeats = seats.Length;

                                // Add code for New PDF ticket
                                #region New Ticket PDF
                                originalFileName = $"Bus_Ticket{System.DateTime.UtcNow.ToFileTimeUtc()}";
                                //fullPath = System.IO.Path.Combine(basepath, originalFileName);

                                string mystring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/bus-pdf.html"));
                                string body = mystring;
                                body = body.Replace("##bustype##", receipt.BusType);
                                body = body.Replace("##amount##", receipt.Amount);
                                body = body.Replace("##seatno##", commaSeparatedDataSeat);
                                body = body.Replace("##busno##", receipt.BusNo);
                                body = body.Replace("##depaturetime##", receipt.Time);
                                body = body.Replace("##bookingdate##", receipt.BookingDate);
                                body = body.Replace("##journeydate##", receipt.date);
                                body = body.Replace("##ticketno##", receipt.ticketSrlNo);
                                body = body.Replace("##from##", receipt.from);
                                body = body.Replace("##to##", receipt.to);
                                body = body.Replace("##ticketno##", receipt.ticketSrlNo);
                                body = body.Replace("##Noofpassenger##", Convert.ToString(numberOfSeats));
                                body = body.Replace("##purchasedby##", name);
                                body = body.Replace("##boardingpoint##", receipt.boardingPoint);
                                body = body.Replace("##contactno##", receipt.contactNumber);
                                body = body.Replace("##LogID##", TransactionID);
                                body = body.Replace("##SupportEmail##", Common.FromEmail);
                                body = body.Replace("##LogoImage##", Common.LiveSiteUrl + "/Content/images/logonew.png");
                                body = body.Replace("##LiveUrl##", Common.LiveSiteUrl_User);
                                body = body.Replace("##tel1##", Common.tel1);
                                body = body.Replace("##tel2##", Common.tel2);
                                body = body.Replace("##tel3##", Common.tel3);
                                body = body.Replace("##tel4##", Common.tel4);
                                body = body.Replace("##WebsiteName##", Common.WebsiteName);
                                body = body.Replace("##WebsiteEmail##", Common.WebsiteEmail);
                                body = body.Replace("##SupportEmail##", Common.FromEmail);

                                System.IO.File.Create(HttpContext.Current.Server.MapPath("/Content/BusSewaTicketPDF/" + originalFileName + ".html")).Dispose();
                                System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("/Content/BusSewaTicketPDF/" + originalFileName + ".html"), body);

                                System.IO.File.Create(HttpContext.Current.Server.MapPath("/Content/BusSewaTicketPDF/" + originalFileName)).Dispose();

                                FileInfo htmlsource = new FileInfo(HttpContext.Current.Server.MapPath("/Content/BusSewaTicketPDF/" + originalFileName + ".html"));
                                FileInfo pdfDest = new FileInfo(HttpContext.Current.Server.MapPath("/Content/BusSewaTicketPDF/" + originalFileName + ".pdf"));

                                // pdfHTML specific code
                                ConverterProperties converterProperties = new ConverterProperties();
                                HtmlConverter.ConvertToPdf(htmlsource, pdfDest, converterProperties);

                                HttpContext.Current.Response.ContentType = "application/pdf";
                                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + originalFileName + ".pdf");
                                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("/Content/BusSewaTicketPDF/" + originalFileName + ".pdf"));
                                //Response.End();
                                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.

                                #endregion

                            }

                            // return objRes;

                        }
                        catch (WebException e)
                        {
                            using (WebResponse response = e.Response)
                            {
                                HttpWebResponse httpResponse = (HttpWebResponse)response;
                                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                                using (Stream data = response.GetResponseStream())
                                using (var reader = new StreamReader(data))
                                {
                                    var json = reader.ReadToEnd();
                                    var Exceptiondata = (JObject)JsonConvert.DeserializeObject(json);
                                    string Error = ((Exceptiondata["error"] == null) ? String.Empty : Convert.ToString(Exceptiondata["error"])); string message = ((Exceptiondata["message"] == null) ? String.Empty : Convert.ToString(Exceptiondata["message"]));
                                    string Details = String.Empty;
                                    Details = ((Exceptiondata["details"] == null) ? String.Empty : Convert.ToString(Exceptiondata["details"]));
                                    //  objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ("Request Failed " + Details + " " + Error + " " + message) : objRes.Message;
                                    // return objRes;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //objRes.status = false;
                            //objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                            //   return objRes;
                        }
                        break;

                    case 204:
                        DownloadPDF objResTouristBus = new DownloadPDF();
                        try
                        {
                            // Reference = new CommonHelpers().GenerateUniqueId();
                            //string LookupURL = $"{KhaltiAPIURL}/{LogID}?token={Req_TokenLive}&reference={Reference}";
                            //string basepath = Common.Common.GetBasePath(relativepath);
                            string originalFileName = $"BusTicket{System.DateTime.UtcNow.ToFileTimeUtc()}.pdf";
                            // fullPath = System.IO.Path.Combine(basepath, originalFileName);
                            Receipt receipt = new Receipt();
                            //get data bus sewa passenger
                            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                            Hashtable HT = new Hashtable();
                            HT.Add("flag", "get");
                            HT.Add("TransactionId", TransactionID);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            dt = commonHelpers.GetDataFromStoredProcedure("sp_TouristBus_Detail", HT);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow row = dt.Rows[0];
                                receipt.from = !string.IsNullOrEmpty(row["TripFrom"].ToString()) ? row["TripFrom"].ToString() : "";
                                receipt.to = !string.IsNullOrEmpty(row["TripTo"].ToString()) ? row["TripTo"].ToString() : "";
                                receipt.ticketSrlNo = !string.IsNullOrEmpty(row["TicketSerialNo"].ToString()) ? row["TicketSerialNo"].ToString() : "";
                                receipt.seat = !string.IsNullOrEmpty(row["Seat"].ToString()) ? row["Seat"].ToString() : "";
                                receipt.cashback = !string.IsNullOrEmpty(row["Cashback"].ToString()) ? row["Cashback"].ToString() : "0.00";
                                receipt.NoofSeat = !string.IsNullOrEmpty(row["NoofSeat"].ToString()) ? row["NoofSeat"].ToString() : "0";
                                receipt.BookingDate = !string.IsNullOrEmpty(row["BookingDate"].ToString()) ? Convert.ToDateTime(row["BookingDate"]).ToString("yyyy-MM-dd") : "";
                                receipt.Time = !string.IsNullOrEmpty(row["DepartureTime"].ToString()) ? Convert.ToDateTime(row["DepartureTime"]).ToString("h:mm tt") : "";
                                receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? Convert.ToDateTime(row["DepartureDate"].ToString()).ToString("yyyy-MM-dd") : "";
                                //receipt.date = !string.IsNullOrEmpty(row["DepartureDate"].ToString()) ? Convert.ToDateTime(Convert.ToDateTime(row["DepartureDate"].ToString()).ToString("yyyy-MM-dd") + ' ' + row["DepartureTime"].ToString()).ToString("yyyy-MM-dd HH:mm") : "";
                                receipt.boardingPoint = !string.IsNullOrEmpty(row["BoardingPoint"].ToString()) ? row["BoardingPoint"].ToString() : "";
                                receipt.Operator = !string.IsNullOrEmpty(row["BusType"].ToString()) ? row["BusType"].ToString() : "";
                                receipt.BusType = !string.IsNullOrEmpty(row["BusType"].ToString()) ? row["BusType"].ToString() : "";
                                receipt.BusNo = !string.IsNullOrEmpty(row["staffnum"].ToString()) ? row["staffnum"].ToString() : "";
                                receipt.PaymentStatus = !string.IsNullOrEmpty(row["PaymentStatus"].ToString()) ? row["PaymentStatus"].ToString() : "";
                                receipt.Platform = !string.IsNullOrEmpty(row["Platform"].ToString()) ? row["Platform"].ToString() : "";
                                receipt.Amount = !string.IsNullOrEmpty(row["Amount"].ToString()) ? row["Amount"].ToString() : "0.00";
                                //receipt.email = !string.IsNullOrEmpty(row["email"].ToString()) ? row["email"].ToString() : "";
                                receipt.name = !string.IsNullOrEmpty(row["PassengerName"].ToString()) ? row["PassengerName"].ToString() : "";
                                receipt.contactNumber = !string.IsNullOrEmpty(row["ContactNumber"].ToString()) ? row["ContactNumber"].ToString() : "";
                                receipt.contactInfo = !string.IsNullOrEmpty(row["staffnum"].ToString()) ? row["staffnum"].ToString() : "";
                                //receipt.boardingPoint = !string.IsNullOrEmpty(row["BoardingPoint"].ToString()) ? row["BoardingPoint"].ToString() : "";
                                receipt.TransactionDate = !string.IsNullOrEmpty(row["TransactionDate"].ToString()) ? row["TransactionDate"].ToString() : "";
                                receipt.ServiceCharge = !string.IsNullOrEmpty(row["ServiceCharge"].ToString()) ? row["ServiceCharge"].ToString() : "0.00";
                                receipt.Type = !string.IsNullOrEmpty(row["Type"].ToString()) ? row["Type"].ToString() : "";
                                receipt.FirstName = !string.IsNullOrEmpty(row["FirstName"].ToString()) ? row["FirstName"].ToString() : "";
                                receipt.LastName = !string.IsNullOrEmpty(row["LastName"].ToString()) ? row["LastName"].ToString() : "";
                                receipt.MiddleName = !string.IsNullOrEmpty(row["MiddleName"].ToString()) ? row["MiddleName"].ToString() : "";
                                receipt.userContact = !string.IsNullOrEmpty(row["userContact"].ToString()) ? row["userContact"].ToString() : "";
                                var name = !string.IsNullOrEmpty(receipt.MiddleName) ? receipt.FirstName + ' ' + receipt.MiddleName + ' ' + receipt.LastName : receipt.FirstName + ' ' + receipt.LastName;

                             
                                // Add code for New PDF ticket
                                #region New Ticket PDF
                                originalFileName = $"TouristBus_Ticket{System.DateTime.UtcNow.ToFileTimeUtc()}";
                                //fullPath = System.IO.Path.Combine(basepath, originalFileName);

                                string mystring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/bus-pdf.html"));
                                string body = mystring;
                                body = body.Replace("##bustype##", receipt.BusType);
                                body = body.Replace("##amount##", receipt.Amount);                                
                                body = body.Replace("##seatno##", receipt.seat);
                                body = body.Replace("##busno##", receipt.BusNo);
                                body = body.Replace("##depaturetime##", receipt.Time);
                                body = body.Replace("##bookingdate##", receipt.BookingDate);
                                body = body.Replace("##journeydate##", receipt.date);
                                body = body.Replace("##ticketno##", receipt.ticketSrlNo);
                                body = body.Replace("##from##", receipt.from);
                                body = body.Replace("##to##", receipt.to);
                                body = body.Replace("##ticketno##", receipt.ticketSrlNo);
                                body = body.Replace("##Noofpassenger##", Convert.ToString(receipt.NoofSeat));
                                body = body.Replace("##purchasedby##", name);
                                body = body.Replace("##boardingpoint##", receipt.boardingPoint);
                                body = body.Replace("##contactno##", receipt.contactNumber);
                                body = body.Replace("##LogID##", TransactionID);
                                body = body.Replace("##SupportEmail##", Common.FromEmail);
                                body = body.Replace("##LogoImage##", Common.LiveSiteUrl + "/Content/images/logonew.png");
                                body = body.Replace("##LiveUrl##", Common.LiveSiteUrl_User);
                                body = body.Replace("##tel1##", Common.tel1);
                                body = body.Replace("##tel2##", Common.tel2);
                                body = body.Replace("##tel3##", Common.tel3);
                                body = body.Replace("##tel4##", Common.tel4);
                                body = body.Replace("##WebsiteName##", Common.WebsiteName);
                                body = body.Replace("##WebsiteEmail##", Common.WebsiteEmail);
                                body = body.Replace("##SupportEmail##", Common.FromEmail);

                                System.IO.File.Create(HttpContext.Current.Server.MapPath("/Content/TouristBusSewaTicketPDF/" + originalFileName + ".html")).Dispose();
                                System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("/Content/TouristBusSewaTicketPDF/" + originalFileName + ".html"), body);

                                System.IO.File.Create(HttpContext.Current.Server.MapPath("/Content/TouristBusSewaTicketPDF/" + originalFileName)).Dispose();

                                FileInfo htmlsource = new FileInfo(HttpContext.Current.Server.MapPath("/Content/TouristBusSewaTicketPDF/" + originalFileName + ".html"));
                                FileInfo pdfDest = new FileInfo(HttpContext.Current.Server.MapPath("/Content/TouristBusSewaTicketPDF/" + originalFileName + ".pdf"));

                                // pdfHTML specific code
                                ConverterProperties converterProperties = new ConverterProperties();
                                HtmlConverter.ConvertToPdf(htmlsource, pdfDest, converterProperties);

                                HttpContext.Current.Response.ContentType = "application/pdf";
                                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + originalFileName + ".pdf");
                                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("/Content/TouristBusSewaTicketPDF/" + originalFileName + ".pdf"));
                                //Response.End();
                                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.

                                #endregion

                            }

                            // return objRes;

                        }
                        catch (WebException e)
                        {
                            using (WebResponse response = e.Response)
                            {
                                HttpWebResponse httpResponse = (HttpWebResponse)response;
                                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                                using (Stream data = response.GetResponseStream())
                                using (var reader = new StreamReader(data))
                                {
                                    var json = reader.ReadToEnd();
                                    var Exceptiondata = (JObject)JsonConvert.DeserializeObject(json);
                                    string Error = ((Exceptiondata["error"] == null) ? String.Empty : Convert.ToString(Exceptiondata["error"])); string message = ((Exceptiondata["message"] == null) ? String.Empty : Convert.ToString(Exceptiondata["message"]));
                                    string Details = String.Empty;
                                    Details = ((Exceptiondata["details"] == null) ? String.Empty : Convert.ToString(Exceptiondata["details"]));
                                    //  objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ("Request Failed " + Details + " " + Error + " " + message) : objRes.Message;
                                    // return objRes;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //objRes.status = false;
                            //objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                            //   return objRes;
                        }
                        break;
                    default:

                        break;
                }

            }
            //  return;

        }



        [HttpPost]
        [Route("api/downloadServiceCableTicket")]
        public HttpResponseMessage DownloadCableCarForWebTicket(string TransactionID)
        {
            WalletTransactions uw = new WalletTransactions();
            uw.TransactionUniqueId = TransactionID;
            GetVendor_API_Airlines_Lookup objRess = new GetVendor_API_Airlines_Lookup();
            var responsedd = Request.CreateResponse<GetVendor_API_Airlines_Lookup>(System.Net.HttpStatusCode.BadRequest, objRess);
            if (uw.GetRecord())
            {
                switch (uw.Type)
                {
                    case 300:

                        GetVendor_API_Airlines_Lookup objRes = new GetVendor_API_Airlines_Lookup();
                        var responsed = Request.CreateResponse<GetVendor_API_Airlines_Lookup>(System.Net.HttpStatusCode.BadRequest, objRes);
                        try
                        {

                            string originalFileName = "";

                            // Add code for New PDF ticket
                            #region New Ticket PDF
                            originalFileName = $"CableCar_Ticket_{System.DateTime.UtcNow.ToFileTimeUtc()}.pdf";
                            string jsonData = "";
                            ReceiptsVendorResponse objreceiptsVendorResponse = null;
                            objreceiptsVendorResponse = VendorApi_CommonHelper.GetCableCarReceipts(TransactionID);
                            var CBSP = JsonConvert.DeserializeObject<TicketResult>(objreceiptsVendorResponse.table2JSONContent);
                            var Tdd = new InvoiceResponse
                            {
                                TicketMessage = CBSP.InvoiceResponse.TicketMessage,
                                ReferenceId = CBSP.InvoiceResponse.ReferenceId,
                                TotalAmount = CBSP.InvoiceResponse.TotalAmount.ToString(),
                                UserName = CBSP.InvoiceResponse.UserName,
                            };

                            // Var Ticked = JsonConvert.<InvoiceResponses>(CBSP.InvoiceResponse);
                            List<TicketResponse> TR = JsonConvert.DeserializeObject<List<TicketResponse>>(JsonConvert.SerializeObject(CBSP.TicketResponse));
                            List<TicketResponse> LTD = new List<TicketResponse>();
                            TicketInvoiceCommon TicketCommon = new TicketInvoiceCommon();

                            string tableData = "";
                            tableData = "<center><table>";

                            int numberOfRowsRequired = TR.Count / 2;

                            var TDCount = 0;

                            foreach (var item in TR)
                            {
                                TicketResponse TicketResponse = new TicketResponse();
                                TicketResponse.QRCode = item.QRCode;
                                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                QRCodeData qrCodeData = qrGenerator.CreateQrCode(item.QRCode, QRCodeGenerator.ECCLevel.Q);
                                QRCode qrCode = new QRCode(qrCodeData);
                                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                                string ImageUrl = "";
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    qrCodeImage.Save(stream, ImageFormat.Png);
                                    byte[] byteImage = stream.ToArray();
                                    ImageUrl = string.Format(Convert.ToBase64String(byteImage));
                                }
                                // qrCodeImage.Save(qrFilePath + qrImageName, ImageFormat.Png);
                                //GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(item.BarCode, BarcodeWriterEncoding.Code128);
                                //barcode.ResizeTo(400, 120);
                                //barcode.AddBarcodeValueTextBelowBarcode();
                                //barcode.ChangeBarCodeColor(Color.Black);
                                //barcode.SetMargins(10);
                                //var GuId1 = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                                //string qrFilePathbar = HttpContext.Current.Server.MapPath("~/Content/CableBarCode/");
                                //string BarImageName = "BarImage" + GuId1 + ".png";
                                //string ImageUrlBar = "";
                                //string tempFilePath = "";
                                //// Save the barcode image to the temporary file
                                //barcode.SaveAsPng(qrFilePathbar+ BarImageName);

                                //// Read the file into a MemoryStream
                                //using (FileStream fileStream = new FileStream(qrFilePathbar, FileMode.Open, FileAccess.Read))
                                //using (MemoryStream stream = new MemoryStream())
                                //{
                                //    fileStream.CopyTo(stream);
                                //    byte[] barcodeImageBytes = stream.ToArray();
                                //    ImageUrlBar = Convert.ToBase64String(barcodeImageBytes);
                                //}

                                //// Delete the temporary file if needed
                                //File.Delete(tempFilePath);
                                TicketResponse.PassengerType = item.PassengerType;
                                TicketResponse.TripType = item.TripType;
                                TicketResponse.TicketNumber = item.TicketNumber;
                                TicketResponse.ValidUntil = item.ValidUntil;
                                LTD.Add(TicketResponse);
                                string mystring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/CableCardownlod.html"));
                                string body = mystring;
                                if (TDCount % 2 == 0)
                                {
                                    tableData += "<tr>";
                                }

                                tableData += "<table cellspacing=\"0\" border=\"0\" cellpadding=\"0\" width=\"100%\" bgcolor=\"#f2f3f8\" style=\"@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;\">\r\n " +
                                    "<tr>\r\n <td>\r\n <table style=\"background-color: #fff; max-width:570px;  margin:0 auto;\" width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">\r\n<tr>\r\n<td style=\"height:80px;\">&nbsp;</td>\r\n </tr>\r\n<tr>\r\n" +
                                    "<td style=\"height:20px;\">&nbsp;</td>\r\n</tr>" +
                                    "\r\n <tr>\r\n <td style=\"text-align:center;\">\r\n <img width=\"160\" src=\"\" title=\"logo\" alt=\"logo\" style=\"margin: 0;\" />\r\n  <h1 style=\"color:#1e1e2d; font-weight:500; margin:15px 0;font-size:22px;font-family:'Rubik',sans-serif;\">Ticket Information </h1>\r\n  <span style=\"display:inline-block; vertical-align:middle; margin:0px 0; border-bottom:1px solid #cecece; width:100px;\" />\r\n  <h5 style=\"color:#1e1e2d; font-weight:500; margin:15px 0;font-size:13px;font-family:'Rubik',sans-serif;\"> Transaction Id. : " + objreceiptsVendorResponse.TxnID + " </h5>\r\n </td>\r\n </tr>\r\n <tr>\r\n<td>" +
                                    "\r\n<table width=\"95%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"max-width:670px; border-radius:3px;\">\r\n  <tr>\r\n <td style=\"height:20px;\">&nbsp;</td>\r\n  </tr>\r\n  <tr>\r\n  <td style=\"text-align:center;\">\r\n <img width=\"160\"" +
                                    " src=\"https://annapurnacablecar.com.np/img/4ca76379-3811-4e0c-9419-af71bd471a77/footer-logo.png\" title=\"logo\" alt=\"logo\" style=\"margin: 0;\" />\r\n\r\n </td>\r\n<td style=\"padding:0 15px;\">\r\n   <p style=\"color:#000000; font-size:15px;line-height:24px; margin:0;\">\r\n <strong>Annapurna Cable Car </strong>\r\n </p>\r\n <p style=\"color:#000000; font-size:15px;line-height:24px; margin:0;\">\r\n  <strong>Passenger Type:" + item.PassengerType + "</strong>\r\n </p>\r\n <p style=\"color:#000000; font-size:15px;line-height:24px; margin:0;\">\r\n  <strong>TicketNo :" + item.TicketNumber + "</strong>\r\n </p>\r\n </td>\r\n <td style=\"padding:0 15px;\">\r\n <p style=\"color:hsl(0, 0%, 0%); font-size:15px;line-height:24px; margin:0;\">\r\n " +
                                    " <strong>Rs:" + Tdd.TotalAmount + "</strong>\r\n  </p>\r\n<p style=\"color:#147900; font-size:15px;line-height:24px; margin:0;\">\r\n <strong>" + item.TripType + "</strong>\r\n  </p>\r\n <p style=\"color:#000000; font-size:15px;line-height:24px; margin:0;\">\r\n " +
                                    "<strong>Valid Till : " + item.ValidUntil + "  </strong>\r\n  </p>\r\n </td>\r\n </tr>\r\n  <tr>\r\n<td style=\"height:40px;\">&nbsp;</td>\r\n</tr>\r\n </table>\r\n </td>\r\n  </tr>\r\n <tr>\r\n  <td style=\"text-align:center;\">\r\n <img width=200 height=200 src=\"data: image/png; base64," + ImageUrl + "\"\\\"/> \r\n</td>\r\n\r\n  </tr>\r\n  <tr>\r\n <td style=\"height:20px;\">&nbsp;</td>\r\n  </tr>\r\n <tr>\r\n <td style=\"text-align:center;\">\r\n <p style=\"font-size:16px; color:rgba(0, 0, 0, 0.741); line-height:24px; margin:0 0 0;\">\r\n <strong>Purchased by : " + objreceiptsVendorResponse.FullName + "</strong>\r\n  </p>\r\n <p style=\"font-size:16px; color:rgba(0, 0, 0, 0.741); line-height:24px; margin:0 0 0;\">\r\n" +
                                    " <strong>Contact No. : " + objreceiptsVendorResponse.ContactNumber + " </strong>\r\n  </p>\r\n <span style=\"display:inline-block; vertical-align:middle; margin:10px 0; border-bottom:1px dashed #777777; width:500px;\" />\r\n </td>" +
                                    "\r\n </tr>\r\n <tr>\r\n  <td style=\"padding:0 50px;\">\r\n <p style=\"font-size:16px; color:rgba(0, 0, 0, 0.741); line-height:24px; margin:0 0 0;\">\r\n <strong>Keep In Mind</strong>\r\n   </p>\r\n   </td>\r\n</tr>\r\n <tr>\r\n <td style=\"padding:10px 25px; font-size: 13px;\">\r\n <ul>\r\n   <li> Valid Identification required for Students and Senior Citizens (60yrs & above).</li>\r\n <li>Failure in Passenger Verification will Require New Ticket Purchase.</li>\r\n <li>This ticket is non-transferrable, non-refundable and non-exchangeable.</li>\r\n <li>This ticket may not be altered, copied, transferred or resold.</li>\r\n<li>Every passenger is insured upto NPR 5,00,000/-</li>\r\n  <li>15 KG Baggage per Person Allowed. Extra Charge for excess Baggage.  </li>\r\n </ul>\r\n  </td>\r\n  </tr>\r\n  <tr>\r\n <td style=\"text-align:center;\">\r\n <p style=\"font-style: italic;font-size:16px; color:rgba(0, 0, 0, 0.741); line-height:24px; margin:0 0 0;\">\r\n <strong>Thank You,</strong>\r\n  </p>\r\n  </td>\r\n </tr>\r\n <tr>\r\n <td style=\"text-align:center;\">\r\n" +
                                    " <img width=\"160\" src=\"\" title=\"logo\" alt=\"logo\" style=\"margin: 0;\" />\r\n\r\n  </td>"
                                    + "\r\n </tr>\r\n  <tr>\r\n <td style=\"text-align:center;\">\r\n  <p style=\"font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;\">\r\n  &copy; <strong>www.mypay.com.np</strong>\r\n </p>\r\n </td>\r\n  </tr>\r\n <tr>\r\n <td style=\"height:80px;\">&nbsp;</td>\r\n  </tr>\r\n </table>\r\n </td>\r\n</tr>\r\n </table>" + "</p>" + "<hr>" + "</br>" + "\r\n<tr> \r\n"
                                     ;

                                TDCount += 1;

                                if (TDCount % 2 == 0)
                                {
                                    tableData += "</tr>";
                                }
                            };
                            tableData += "</table></center>";

                            if (objreceiptsVendorResponse.TxnID != null)
                            {
                                string mystring = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/CableCardownlod.html"));
                                string body = mystring;
                                String str = "";
                                body = body.Replace("##tr##", str);
                                body = body.Replace("##QRTABLE##", tableData);
                                System.IO.File.Create(HttpContext.Current.Server.MapPath("/Content/CableCarTicketPDF/" + originalFileName + ".html")).Dispose();
                                System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("/Content/CableCarTicketPDF/" + originalFileName + ".html"), body);
                                System.IO.File.Create(HttpContext.Current.Server.MapPath("/Content/CableCarTicketPDF/" + originalFileName)).Dispose();
                                FileInfo htmlsource = new FileInfo(HttpContext.Current.Server.MapPath("/Content/CableCarTicketPDF/" + originalFileName + ".html"));
                                FileInfo pdfDest = new FileInfo(HttpContext.Current.Server.MapPath("/Content/CableCarTicketPDF/" + originalFileName));


                                // pdfHTML specific code
                                ConverterProperties converterProperties = new ConverterProperties();
                                HtmlConverter.ConvertToPdf(htmlsource, pdfDest, converterProperties);



                            }
                            #endregion
                            objRes.status = true;
                            objRes.Message = "success";
                            objRes.FilePath = Common.LiveApiUrl + "Content/CableCarTicketPDF/" + originalFileName;
                            responsed = Request.CreateResponse<GetVendor_API_Airlines_Lookup>(System.Net.HttpStatusCode.OK, objRes);
                            return responsed;

                        }
                        catch (WebException e)
                        {
                            using (WebResponse response = e.Response)
                            {
                                HttpWebResponse httpResponse = (HttpWebResponse)response;
                                Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                                using (Stream data = response.GetResponseStream())
                                using (var reader = new StreamReader(data))
                                {
                                    var json = reader.ReadToEnd();
                                    var Exceptiondata = (JObject)JsonConvert.DeserializeObject(json);
                                    string Error = ((Exceptiondata["error"] == null) ? String.Empty : Convert.ToString(Exceptiondata["error"])); string message = ((Exceptiondata["message"] == null) ? String.Empty : Convert.ToString(Exceptiondata["message"]));
                                    string Details = String.Empty;
                                    Details = ((Exceptiondata["details"] == null) ? String.Empty : Convert.ToString(Exceptiondata["details"]));
                                    objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ("Request Failed " + Details + " " + Error + " " + message) : objRes.Message;
                                    //return objRes;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            objRes.status = false;
                            objRes.Message = String.IsNullOrEmpty(objRes.Message) ? ex.Message : objRes.Message;
                            //  return objRes;
                        }

                        break;
                    case 200:
                        break;
                    default:

                        break;
                }

            }
            return responsedd;

        }


        public string ValidatePayment(Req_API_Add_Payment_Requests user)
        {

            string msg = "";
            if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_topup)
            {
                if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (user.Amount <= 0)
                {
                    msg = "Please enter Amount";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_cleartv)
            {
                if (string.IsNullOrEmpty(user.SubscriberID))
                {
                    msg = "Please enter Subscriber ID";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (user.Amount <= 0)
                {
                    msg = "Please enter Amount";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_dishhome)
            {
                if (string.IsNullOrEmpty(user.CasID))
                {
                    msg = "Please enter Cas ID";
                }


            }

            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_jagrititv)
            {
                if (string.IsNullOrEmpty(user.PackageID))
                {
                    msg = "Please enter PackageID";
                }
                else if (string.IsNullOrEmpty(user.PackageName))
                {
                    msg = "Please enter PackageName";
                }
                else if (string.IsNullOrEmpty(user.CasID))
                {
                    msg = "Please enter CasID";
                }
                else if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }
                else if (string.IsNullOrEmpty(user.CustomerName))
                {
                    msg = "Please enter Customer Name";
                }
                else if (string.IsNullOrEmpty(user.OldWardNumber))
                {
                    msg = "Please enter Old Ward Number";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_maxtv)
            {
                if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_mero)
            {
                if (string.IsNullOrEmpty(user.STB))
                {
                    msg = "Please enter STB";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_prabhutv)
            {
                if (string.IsNullOrEmpty(user.CasID))
                {
                    msg = "Please enter CasID";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_simtv)
            {
                if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_adsl)
            {
                if (string.IsNullOrEmpty(user.LandlineNumber))
                {
                    msg = "Please enter Landline Number";
                }


            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Arrownet) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_classictech))
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter UserName";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_png_network)
            {
                if (string.IsNullOrEmpty(user.PackageID))
                {
                    msg = "Please enter PackageID";
                }
                else if (string.IsNullOrEmpty(user.PackageName))
                {
                    msg = "Please enter Package Name";
                }
                else if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (string.IsNullOrEmpty(user.FullName))
                {
                    msg = "Please enter Full Name";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Pokhara)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (string.IsNullOrEmpty(user.Address))
                {
                    msg = "Please enter Address";
                }


            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_RoyalNetwork) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_VirtualNetwork) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebNetwork))
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }



            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_TechMinds) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_vianet) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebSurfer) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Worldlink))
            {
                if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_pstn_landline)
            {
                if (string.IsNullOrEmpty(user.LandlineNumber))
                {
                    msg = "Please enter Landline Number";
                }



            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nea)
            {
                if (string.IsNullOrEmpty(user.CounterID))
                {
                    msg = "Please enter Counter ID";
                }
                else if (string.IsNullOrEmpty(user.CounterName))
                {
                    msg = "Please enter Counter Name";
                }
                else if (string.IsNullOrEmpty(user.ScNumber))
                {
                    msg = "Please enter Sc Number";
                }
                else if (string.IsNullOrEmpty(user.ConsumerID))
                {
                    msg = "Please enter Consumer ID";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_khanepani)
            {
                if (string.IsNullOrEmpty(user.CounterID))
                {
                    msg = "Please enter Counter ID";
                }
                else if (string.IsNullOrEmpty(user.CounterName))
                {
                    msg = "Please enter Counter Name";
                }
                else if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }
            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Eset) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_k7) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_antivirus_kaspersky) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Mcafee) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Wardwiz))
            {
                if (string.IsNullOrEmpty(user.SubscriptionID))
                {
                    msg = "Please enter Subscription ID";
                }
                else if (string.IsNullOrEmpty(user.SubscriptionName))
                {
                    msg = "Please enter Subscription Name";
                }

            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ncell) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ntc))
            {
                if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant)
            {
                if (string.IsNullOrEmpty(user.AcceptanceNo))
                {
                    msg = "Please enter Acceptance No";
                }
            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Jyoti_Life) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Nepal_Life) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance))
            {
                if (string.IsNullOrEmpty(user.PolicyNumber))
                {
                    msg = "Please enter Policy Number";
                }
                else if (string.IsNullOrEmpty(user.DateOfBirth))
                {
                    msg = "Please enter Date Of Birth";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Neco)
            {
                if (string.IsNullOrEmpty(user.InsuranceID))
                {
                    msg = "Please enter Insurance ID";
                }
                else if (string.IsNullOrEmpty(user.InsuranceName))
                {
                    msg = "Please enter Insurance Name";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico)
            {
                if (string.IsNullOrEmpty(user.DebitNoteNo))
                {
                    msg = "Please enter Debit Note No";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Shikhar)
            {
                if (string.IsNullOrEmpty(user.CustomerName))
                {
                    msg = "Please enter Customer Name";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (string.IsNullOrEmpty(user.Email))
                {
                    msg = "Please enter Email";
                }
                else if (string.IsNullOrEmpty(user.PolicyType))
                {
                    msg = "Please enter Policy Type";
                }
                else if (string.IsNullOrEmpty(user.Branch))
                {
                    msg = "Please enter Branch";
                }
                else if (string.IsNullOrEmpty(user.PolicyNumber))
                {
                    msg = "Please enter Policy Number";
                }
                else if (string.IsNullOrEmpty(user.PolicyCategory))
                {
                    msg = "Please enter Policy Category";
                }
                else if (string.IsNullOrEmpty(user.Address))
                {
                    msg = "Please enter Address";
                }
                else if (string.IsNullOrEmpty(user.PolicyDescription))
                {
                    msg = "Please enter Policy Description";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine)
            {
                if (string.IsNullOrEmpty(user.ChitNumber))
                {
                    msg = "Please enter Chit Number";
                }
                else if (string.IsNullOrEmpty(user.FiscalYearID))
                {
                    msg = "Please enter Fiscal Year ID";
                }
                else if (string.IsNullOrEmpty(user.FiscalYearValue))
                {
                    msg = "Please enter Fiscal Year";
                }
                else if (string.IsNullOrEmpty(user.ProvinceID))
                {
                    msg = "Please enter Province ID";
                }
                else if (string.IsNullOrEmpty(user.ProvinceName))
                {
                    msg = "Please enter ProvinceName";
                }
                else if (string.IsNullOrEmpty(user.DistrictID))
                {
                    msg = "Please enter District ID";
                }
                else if (string.IsNullOrEmpty(user.DistrictValue))
                {
                    msg = "Please enter District Value";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment)
            {
                if (string.IsNullOrEmpty(user.BankID))
                {
                    msg = "Please enter Bank ID";
                }
                else if (string.IsNullOrEmpty(user.BankName))
                {
                    msg = "Please enter Bank Name";
                }
                else if (string.IsNullOrEmpty(user.CreditCardNumber))
                {
                    msg = "Please enter Credit Card Number";
                }
                else if (string.IsNullOrEmpty(user.CreditCardOwner))
                {
                    msg = "Please enter Credit Card Owner";
                }
            }
            else if (user.Amount <= 0)
            {
                msg = "Please enter Amount";
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