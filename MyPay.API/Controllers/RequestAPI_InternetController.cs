using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Dishome;
using MyPay.API.Models.Internet;
using MyPay.API.Models.Internet.Arrownet;
using MyPay.API.Models.Internet.ClassiTech;
using MyPay.API.Models.Internet.DishHome;
using MyPay.API.Models.Internet.ViaNet;
using MyPay.API.Models.Internet.WebSurfer;
using MyPay.API.Models.Request.Internet;
using MyPay.API.Models.Response.Internet;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using MyPay.Models.VendorAPI.Get.Internet.SUBISU;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class RequestAPI_InternetController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_InternetController));
        string ApiResponse = string.Empty;

        [HttpPost]
        [Route("api/use-internet-adsl")]
        public HttpResponseMessage GetServiceInternet_ADSL(Req_Vendor_API_ADSL_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-adsl" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_ADSL_Requests result = new Res_Vendor_API_ADSL_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ADSL_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_adsl;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_ADSL_Payment_Request objRes = new GetVendor_API_ADSL_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_ADSL_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, user.IsVolumeBased, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "success";
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ADSL_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-adsl completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-adsl {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/lookup-internet-subisu")]
        public HttpResponseMessage GetLookupService_SUBISU(Req_Vendor_API_SUBISU_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-subisu" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Subisu_Lookup_Requests result = new Res_Vendor_API_Subisu_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Subisu_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_SUBISU_Lookup objRes = new GetVendor_API_SUBISU_Lookup();
                        GetVendor_API_SUBISU_Lookup_TV_ComboOffer objResOffer = new GetVendor_API_SUBISU_Lookup_TV_ComboOffer();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_SUBISU_LOOKUP(user.UserName, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);




                        if (objRes.plan_detail_list != null)
                        {
                            if (objRes.plan_detail_list.plan_detail_list != null)
                            {
                                double outstandinAmount = 0;
                                try
                                {
                                    Double.TryParse(objRes.outstanding_amount, out outstandinAmount);
                                }
                                catch (Exception)
                                {

                                    throw;
                                }

                                if (!objRes.plan_detail_list.plan_detail_list.status && objRes.plan_detail_list.plan_detail_list.detail != null && outstandinAmount <= 0)
                                {
                                    CommonResponse cres1 = new CommonResponse();
                                    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                    cres1.Message = objRes.plan_detail_list.plan_detail_list.detail;
                                    cres1.ReponseCode = 3;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                    return response;
                                }
                            }
                        }

                        if (msg.ToLower() != "success")
                        {
                            //tv only and offer
                            user.Reference = new CommonHelpers().GenerateUniqueId();
                            msg = RepKhalti.RequestServiceGroup_SUBISU_NEW_LOOKUP(user.UserName, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objResOffer);

                            if (msg.ToLower() == "success")
                            {
                                List<ComboPlanDetail> objDataOfferDetail = new List<ComboPlanDetail>();
                                List<TvDetail> objDataStb = new List<TvDetail>();
                                List<TvPlanDetail> objDataTv = new List<TvPlanDetail>();
                                List<PlanDetailListOffer> objDataOffer = new List<PlanDetailListOffer>();

                                if (objResOffer.plan_detail_list.plan_type == "tv")
                                {
                                    //tv
                                    for (int i = 0; i < objResOffer.plan_detail_list.plan_detail_list.Count; i++)
                                    {
                                        TvDetail objDataStbItem = new TvDetail();
                                        objDataStbItem.stb = objResOffer.plan_detail_list.plan_detail_list[i].stb;
                                        objDataStb.Add(objDataStbItem);
                                    }

                                    for (int i = 0; i < objResOffer.plan_detail_list.plan_detail_list[0].tv_plan_details.Count; i++)
                                    {
                                        TvPlanDetail objDataTvItem = new TvPlanDetail();
                                        objDataTvItem.plan_name = objResOffer.plan_detail_list.plan_detail_list[0].tv_plan_details[i].plan_name;
                                        objDataTvItem.validity = objResOffer.plan_detail_list.plan_detail_list[0].tv_plan_details[i].validity;
                                        objDataTvItem.amount = objResOffer.plan_detail_list.plan_detail_list[0].tv_plan_details[i].amount;
                                        objDataTv.Add(objDataTvItem);
                                    }
                                }
                                else
                                {
                                    //offer
                                    for (int i = 0; i < objResOffer.plan_detail_list.plan_detail_list.Count; i++)
                                    {
                                        PlanDetailListOffer objDataItem = new PlanDetailListOffer();
                                        objDataItem.offer_name = objResOffer.plan_detail_list.plan_detail_list[i].offer_name;
                                        objDataItem.offer_id = objResOffer.plan_detail_list.plan_detail_list[i].offer_id;
                                        objDataItem.validity = objResOffer.plan_detail_list.plan_detail_list[i].validity;
                                        objDataItem.amount = objResOffer.plan_detail_list.plan_detail_list[i].amount;
                                        objDataOffer.Add(objDataItem);
                                    }
                                }
                                result.plan_type = objResOffer.plan_detail_list.plan_type;
                                result.plan_detail_list_offer = objDataOffer;
                                result.tv_details = objDataStb;
                                result.tv_plan_details = objDataTv;

                                result.customer_name = objResOffer.customer_name;
                                result.address = objResOffer.address;
                                result.current_plan_name = objResOffer.current_plan_name;
                                result.user_id = objResOffer.user_id;
                                result.outstanding_amount = objResOffer.outstanding_amount;
                                result.expiry_date = objResOffer.expiry_date;
                                result.mobile_no = objResOffer.mobile_no;
                                result.onu_id = objResOffer.onu_id;
                                result.partner_name = objResOffer.partner_name;
                                result.token = objResOffer.token;
                                result.session_id = objResOffer.session_id;
                                //add reference no here
                                result.ReferenceNo = user.Reference;
                                result.ReponseCode = objResOffer.status ? 1 : 0;
                                result.status = objResOffer.status;
                                result.Message = "success";
                                response.StatusCode = HttpStatusCode.Accepted;
                                response = Request.CreateResponse<Res_Vendor_API_Subisu_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            }



                        }
                        else if (msg.ToLower() == "success")
                        {
                            //internet only and internet+tv

                            List<InternetPlanDetail> objData = new List<InternetPlanDetail>();
                            List<TvDetail> objDataStb = new List<TvDetail>();
                            List<TvPlanDetail> objDataTv = new List<TvPlanDetail>();

                            if (objRes.plan_detail_list.status == true || objRes.plan_detail_list.plan_detail_list != null)
                            {
                                if (objRes.plan_detail_list.plan_type == "internet")
                                {
                                    if (objRes.plan_detail_list.plan_detail_list.internet_plan_details != null)
                                    {
                                        for (int i = 0; i < objRes.plan_detail_list.plan_detail_list.internet_plan_details.Count; i++)
                                        {
                                            InternetPlanDetail objDataItem = new InternetPlanDetail();
                                            objDataItem.plan_name = objRes.plan_detail_list.plan_detail_list.internet_plan_details[i].plan_name;
                                            objDataItem.validity = objRes.plan_detail_list.plan_detail_list.internet_plan_details[i].validity;
                                            objDataItem.amount = objRes.plan_detail_list.plan_detail_list.internet_plan_details[i].amount;
                                            objData.Add(objDataItem);
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < objRes.plan_detail_list.plan_detail_list.internet_details.internet_plan_details.Count; i++)
                                    {
                                        InternetPlanDetail objDataItem = new InternetPlanDetail();
                                        objDataItem.plan_name = objRes.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[i].plan_name;
                                        objDataItem.validity = objRes.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[i].validity;
                                        objDataItem.amount = objRes.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[i].amount;
                                        objData.Add(objDataItem);
                                    }

                                    for (int i = 0; i < objRes.plan_detail_list.plan_detail_list.tv_details.Count; i++)
                                    {
                                        TvDetail objDataStbItem = new TvDetail();
                                        objDataStbItem.stb = objRes.plan_detail_list.plan_detail_list.tv_details[i].stb;
                                        objDataStb.Add(objDataStbItem);
                                    }

                                    for (int i = 0; i < objRes.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details.Count; i++)
                                    {
                                        TvPlanDetail objDataTvItem = new TvPlanDetail();
                                        objDataTvItem.plan_name = objRes.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[i].plan_name;
                                        objDataTvItem.validity = objRes.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[i].validity;
                                        objDataTvItem.amount = objRes.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[i].amount;
                                        objDataTv.Add(objDataTvItem);
                                    }
                                }
                            }

                            result.plan_type = objRes.plan_detail_list.plan_type;
                            result.internet_plan_details = objData;
                            result.tv_details = objDataStb;
                            result.tv_plan_details = objDataTv;

                            result.customer_name = objRes.customer_name;
                            result.address = objRes.address;
                            result.current_plan_name = objRes.current_plan_name;
                            result.user_id = objRes.user_id;
                            result.outstanding_amount = objRes.outstanding_amount;
                            result.expiry_date = objRes.expiry_date;
                            result.mobile_no = objRes.mobile_no;
                            result.onu_id = objRes.onu_id;
                            result.partner_name = objRes.partner_name;
                            result.token = objRes.token;
                            result.session_id = objRes.session_id;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "success";

                            result.ReferenceNo = user.Reference;

                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Subisu_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-subisu completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-subisu {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/use-internet-subisu")]
        public HttpResponseMessage GetServiceInternet_SUBISU(Req_Vendor_API_SUBISU_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-subisu" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_SUBISU_Requests result = new Res_Vendor_API_SUBISU_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_SUBISU_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    // string md5hash = Common.CheckHash(user);

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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_SUBISU_Payment_Request objRes = new GetVendor_API_SUBISU_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        //user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_SUBISU_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.SessionID, user.OfferName, user.PlanType, user.stb, user.CustomerID, user.Amount, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_SUBISU_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            try
                            {
                                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new;
                                var WalletTransactionId = TransactionID;
                                string jsonData = VendorApi_CommonHelper.Generate_json_Internet_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, user.CustomerID, "Subisu Internet", user.OfferName, "",
                                result.Message, user.Amount.ToString());
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "Subisu Internet", "", WalletTransactionId, jsonData, resGetRecord.ContactNumber, objVendor_API_Requests.MemberName, "Internet Payment", "Subisu Internet", user.Amount.ToString());
                            }
                            catch (Exception ex)
                            {

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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-subisu completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-subisu {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/use-internet-subisu-old")]
        public HttpResponseMessage GetServiceInternet_SUBISU_OLD(Req_Vendor_API_SUBISU_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-subisu" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_SUBISU_Requests result = new Res_Vendor_API_SUBISU_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_SUBISU_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_SUBISU_Payment_Request objRes = new GetVendor_API_SUBISU_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_SUBISU_PAYMENT_OLD(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.CustomerID, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_SUBISU_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-subisu completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-subisu {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/lookup-internet-vianet")]
        public HttpResponseMessage GetLookupService_ViaNet(Req_Vendor_API_ViaNet_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-vianet" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_ViaNet_Lookup_Requests result = new Res_Vendor_API_ViaNet_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ViaNet_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_ViaNet_Lookup objRes = new GetVendor_API_ViaNet_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_ViaNet_LOOKUP(user.CustomerID, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<ViaNet_Data> objData = new List<ViaNet_Data>();
                            for (int i = 0; i < objRes.bills.Count; i++)
                            {
                                ViaNet_Data objDataItem = new ViaNet_Data();
                                objDataItem.PaymentID = objRes.bills[i].payment_id;
                                objDataItem.BillDate = objRes.bills[i].bill_date;
                                objDataItem.Service_Details = objRes.bills[i].service_details;
                                objDataItem.Service_Name = objRes.bills[i].service_name;
                                objDataItem.BillAmount = objRes.bills[i].amount;
                                objData.Add(objDataItem);
                            }
                            result.SessionID = objRes.session_id;
                            result.CustomerID = objRes.customer_id;
                            result.CustomerName = objRes.customer_name;
                            result.Vianet_Bills = objData;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ViaNet_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-vianet completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-vianet {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/use-internet-vianet")]
        public HttpResponseMessage GetServiceInternet_ViaNet(Req_Vendor_API_ViaNet_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-vianet" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_ViaNet_Requests result = new Res_Vendor_API_ViaNet_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ViaNet_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //  string md5hash = Common.CheckHash(user);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_vianet;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_ViaNet_Payment_Request objRes = new GetVendor_API_ViaNet_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_ViaNet_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.CustomerID, user.SessionID, user.PaymentID, user.Amount, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.TransactionUniqueId = TransactionID;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ViaNet_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-vianet completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-vianet {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/lookup-internet-classitech")]
        public HttpResponseMessage GetLookupService_CLASSITECH(Req_Vendor_API_Classitech_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-classitech" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Classitech_Lookup_Requests result = new Res_Vendor_API_Classitech_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Classitech_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_Classitech_Lookup objRes = new GetVendor_API_Classitech_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_CLASSITECH_LOOKUP(user.UserName, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<ClassiTech_Data> objData = new List<ClassiTech_Data>();
                            for (int i = 0; i < objRes.available_plans.Count; i++)
                            {
                                ClassiTech_Data objDataItem = new ClassiTech_Data();
                                objDataItem.Package = objRes.available_plans[i].package;
                                objDataItem.Duration = objRes.available_plans[i].duration;
                                objDataItem.Amount = objRes.available_plans[i].amount;
                                objData.Add(objDataItem);
                            }
                            result.SessionID = objRes.session_id;
                            result.Available_Plans = objData;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Classitech_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-classitech completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-classitech {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/use-internet-classitech")]
        public HttpResponseMessage GetServiceInternet_CLASSITECH(Req_Vendor_API_Classitech_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-classitech" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Classitech_Requests result = new Res_Vendor_API_Classitech_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Classitech_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_classictech;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_Classitech_Payment_Request objRes = new GetVendor_API_Classitech_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_CLASSITECH_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.SessionID, user.Amount, user.Month, user.Package, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Classitech_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-classitech completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-classitech {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/lookup-internet-arrownet")]
        public HttpResponseMessage GetLookupService_Arrownet(Req_Vendor_API_Arrownet_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-arrownet" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Arrownet_Lookup_Requests result = new Res_Vendor_API_Arrownet_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Arrownet_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_Arrownet_Lookup objRes = new GetVendor_API_Arrownet_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_Arrownet_LOOKUP(user.UserName, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<Arrownet_Data> objData = new List<Arrownet_Data>();
                            for (int i = 0; i < objRes.plan_details.Count; i++)
                            {
                                Arrownet_Data objDataItem = new Arrownet_Data();
                                objDataItem.Duration = objRes.plan_details[i].duration;
                                objDataItem.Amount = objRes.plan_details[i].amount;
                                objData.Add(objDataItem);
                            }
                            result.Full_name = objRes.full_name;
                            result.Days_Remaining = objRes.days_remaining;
                            result.Current_Plan = objRes.current_plan;
                            result.Has_Due = objRes.has_due;
                            result.Plan_Details = objData;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Arrownet_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-arrownet completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-arrownet {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/use-internet-Arrownet")]
        public HttpResponseMessage GetServiceInternet_Arrownet(Req_Vendor_API_Arrownet_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-Arrownet" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Arrownet_Requests result = new Res_Vendor_API_Arrownet_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Arrownet_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Arrownet;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_Arrownet_Payment_Request objRes = new GetVendor_API_Arrownet_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_Arrownet_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Amount, user.Duration, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Arrownet_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-Arrownet completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-Arrownet {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/use-internet-virtual-network")]
        public HttpResponseMessage GetServiceInternet_VirtualNetwork(Req_Vendor_API_VirtualNetwork_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-virtual-network" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_VirtualNetwork_Requests result = new Res_Vendor_API_VirtualNetwork_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_VirtualNetwork_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    // string md5hash = Common.CheckHash(user);
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
                        //string UserInput = getRawPostData().Result;
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
                        GetVendor_API_VirtualNetwork_Payment_Request objRes = new GetVendor_API_VirtualNetwork_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_VirtualNetwork_PAYMENT(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, user.MemberId, authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_VirtualNetwork_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-virtual-network completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-virtual-network {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/use-internet-web-network")]
        public HttpResponseMessage GetServiceInternet_WebNetwork(Req_Vendor_API_WebNetwork_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-web-network" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_WebNetwork_Requests result = new Res_Vendor_API_WebNetwork_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_WebNetwork_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    //   string md5hash = Common.CheckHash(user);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebNetwork;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_WebNetwork_Payment_Request objRes = new GetVendor_API_WebNetwork_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_WebNetwork_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_WebNetwork_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-web-network completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-web-network {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/use-internet-royal-network")]
        public HttpResponseMessage GetServiceInternet_RoyalNetwork(Req_Vendor_API_RoyalNetwork_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-royal-network" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_RoyalNetwork_Requests result = new Res_Vendor_API_RoyalNetwork_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_RoyalNetwork_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_RoyalNetwork;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_RoyalNetwork_Payment_Request objRes = new GetVendor_API_RoyalNetwork_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_RoyalNetwork_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_RoyalNetwork_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-royal-network completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-royal-network {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/lookup-internet-websurfer-userlist")]
        public HttpResponseMessage GetLookupService_WebSurfer_Userlist(Req_Vendor_API_WebSurfer_UserListLookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-websurfer-userlist" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_WebSurfer_UserList_Lookup_Requests result = new Res_Vendor_API_WebSurfer_UserList_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_WebSurfer_UserList_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_WebSurfer_UserList_Lookup objRes = new GetVendor_API_WebSurfer_UserList_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_WebSurfer_UserList_LOOKUP(user.CustomerId, user.UniqueCustomerId, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.connection = objRes.connection;
                            result.customer = objRes.customer;
                            result.SessionId = objRes.session_id;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_WebSurfer_UserList_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-websurfer-userlist completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-websurfer-userlist {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/lookup-internet-websurfer")]
        public HttpResponseMessage GetLookupService_WebSurfer(Req_Vendor_API_WebSurfer_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-websurfer" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_WebSurfer_Lookup_Requests result = new Res_Vendor_API_WebSurfer_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_WebSurfer_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        GetVendor_API_WebSurfer_Lookup objRes = new GetVendor_API_WebSurfer_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_WebSurfer_LOOKUP(user.UserName, user.SessionId, user.UniqueCustomerId, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.Session_Id = objRes.session_id;
                            result.packages = objRes.packages;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_WebSurfer_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-websurfer completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-websurfer {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/use-internet-websurfer")]
        public HttpResponseMessage GetServiceInternet_WebSurfer(Req_Vendor_API_WebSurfer_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-websurfer" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_WebSurfer_Requests result = new Res_Vendor_API_WebSurfer_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_WebSurfer_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebSurfer;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_WebSurfer_Payment_Request objRes = new GetVendor_API_WebSurfer_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_WebSurfer_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.SessionID, user.Amount, user.package_id, user.Service, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_WebSurfer_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-websurfer completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-websurfer {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPost]
        [Route("api/lookup-internet-techminds")]
        public HttpResponseMessage GetLookupService_Techminds(Req_Vendor_API_Techminds_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-techminds" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Techminds_Lookup_Requests result = new Res_Vendor_API_Techminds_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Techminds_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_Techminds_Lookup objRes = new GetVendor_API_Techminds_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_Techminds_LOOKUP(user.RequestID, user.UniqueCustomerId, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            Techminds_Data objDataItemData = new Techminds_Data();
                            objDataItemData.CustomerName = objRes.data.customer_name;
                            objDataItemData.MobileNumber = objRes.data.mobile_number;
                            objDataItemData.PreviousBalance = objRes.data.previous_balance;
                            objDataItemData.MonthlyCharge = objRes.data.monthly_charge;
                            objDataItemData.ExpirationDate = objRes.data.expiration;
                            objDataItemData.Email = objRes.data.email;
                            objDataItemData.MonthlyCharge = objRes.data.monthly_charge;
                            result.Techminds_Data = objDataItemData;
                            Techminds_Plans objDataItem = new Techminds_Plans();
                            objDataItem.Plan_12Month = objRes.available_plans._12_Month;
                            objDataItem.Plan_6Month = objRes.available_plans._6_Month;
                            objDataItem.Plan_3Month = objRes.available_plans._3_Month;
                            objDataItem.Plan_1Month = objRes.available_plans._1_Month;
                            objDataItem.Plan_15Days = objRes.available_plans._15Days;
                            objDataItem.Plan_180Days = objRes.available_plans._180Days;
                            objDataItem.Plan_30Days = objRes.available_plans._30Days;
                            objDataItem.Plan_60Days = objRes.available_plans._60Days;
                            objDataItem.Plan_90Days = objRes.available_plans._90Days;

                            var serilaizeJson = JsonConvert.SerializeObject(objDataItem, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

                            var result1 = JsonConvert.DeserializeObject<dynamic>(serilaizeJson);
                            result.Available_Plans = result1;
                            result.SessionID = objRes.session_id;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Techminds_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-techminds completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-techminds {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/use-internet-techminds")]
        public HttpResponseMessage GetServiceInternet_Techminds(Req_Vendor_API_Techminds_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-techminds" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Techminds_Requests result = new Res_Vendor_API_Techminds_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Techminds_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_TechMinds;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_Techminds_Payment_Request objRes = new GetVendor_API_Techminds_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_Techminds_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.SessionID, user.Amount, user.RequestID, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Techminds_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-techminds completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-techminds {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/use-internet-pokhara")]
        public HttpResponseMessage GetServiceInternet_Pokhara(Req_Vendor_API_Pokhara_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-pokhara" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Pokhara_Requests result = new Res_Vendor_API_Pokhara_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Pokhara_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Pokhara;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        GetVendor_API_Pokhara_Payment_Request objRes = new GetVendor_API_Pokhara_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_INTERNET_Pokhara_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Number, user.Amount, user.Address, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Details = objRes.detail;
                            result.Message = "Success";
                            result.TransactionUniqueId = TransactionID;
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Pokhara_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-internet-pokhara completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-internet-pokhara {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/lookup-internet-dishhome")]
        public HttpResponseMessage GetLookupService_Dishhome(Req_Vendor_API_DishHome_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-dishhome" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_DishHome_Lookup_Requests result = new Res_Vendor_API_DishHome_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_DishHome_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        Vendor_API_DishHome objRes = new Vendor_API_DishHome();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        string msg = RepKhalti.RequestServiceGroup_DishHome_LOOKUP(user.CustomerID, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            //List<DishHome_Data> objData = new List<DishHome_Data>();
                            //for (int i = 0; i < objRes.bills.Count; i++)
                            //{
                            //    DishHome_Data objDataItem = new DishHome_Data();
                            //    objDataItem.PaymentID = objRes.bills[i].payment_id;
                            //    objDataItem.BillDate = objRes.bills[i].bill_date;
                            //    objDataItem.Service_Details = objRes.bills[i].service_details;
                            //    objDataItem.Service_Name = objRes.bills[i].service_name;
                            //    objDataItem.BillAmount = objRes.bills[i].amount;
                            //    objData.Add(objDataItem);
                            //}
                            result.SessionID = objRes.session_id;
                            result.CustomerID = objRes.customer_id;
                            result.CustomerName = objRes.customer_name;
                            result.PackageName = objRes.package_name;
                            result.Amount = objRes.amount;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_DishHome_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-dishhome completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-dishhome {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //[HttpPost]
        //[Route("api/use-internet-dishhome")]
        //public HttpResponseMessage GetServiceInternet_Dishhome(Req_Vendor_API_DishHome_Lookup_Requests user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside use-internet-DishHome" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_DishHome_Requests result = new Res_Vendor_API_DishHome_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_DishHome_Requests>(System.Net.HttpStatusCode.BadRequest, result);

        //    var userInput = getRawPostData().Result;
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
        //            //string md5hash = Common.CheckHash(user);
        //            string md5hash = Common.getHashMD5(userInput);
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
        //                //string UserInput = getRawPostData().Result;
        //                string CommonResult = "";
        //                AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
        //                if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
        //                {
        //                    Int64 memId = Convert.ToInt64(user.MemberId);
        //                    int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Dishhome;
        //                    int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
        //                    resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
        //                    if (CommonResult.ToLower() != "success")
        //                    {
        //                        cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                        return response;
        //                    }
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                    return response;
        //                }
        //                GetVendor_API_Dishhome_FTTH_Payment_Request objRes = new GetVendor_API_Dishhome_FTTH_Payment_Request();
        //                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

        //                string authenticationToken = Request.Headers.Authorization.Parameter;
        //                Common.authenticationToken = authenticationToken;
        //                user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                bool IsCouponUnlocked = false; string TransactionID = string.Empty;
        //                string msg = RepKhalti.RequestServiceGroup_INTERNET_DishHome_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.CustomerID, user.PackageName, user.SessionId, user.Amount, user.Duration, user.Reference, user.Version, user.DeviceCode,
        //                    user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
        //                if (msg.ToLower() == "success")
        //                {
        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.status = objRes.status;
        //                    result.UniqueTransactionId = objRes.UniqueTransactionId;
        //                    result.WalletBalance = objRes.WalletBalance;
        //                    result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
        //                    result.Details = objRes.detail;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    result.CreditsConsumed = objRes.credits_consumed;
        //                    result.CreditsAvailable = objRes.credits_available;
        //                    result.ExtraData = objRes.extra_data;
        //                    result.TransactionUniqueId = TransactionID;
        //                    result.Id = objRes.id;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_DishHome_Requests>(System.Net.HttpStatusCode.OK, result);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

        //                    try
        //                    {
        //                        int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Dishhome;
        //                        var WalletTransactionId = TransactionID;
        //                        string jsonData = VendorApi_CommonHelper.Generate_json_Internet_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, user.CustomerID, "DishHome FTTH Internet", user.PackageName, "",
        //                        result.Message, user.Amount.ToString());
        //                        VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "DishHome FTTH Internet", resGetRecord.MemberId.ToString(), WalletTransactionId, jsonData, resGetRecord.ContactNumber, objVendor_API_Requests.MemberName, "Internet Payment", "DishHome FTTH Internet", user.Amount.ToString());
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
        //                }
        //                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
        //                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
        //                inobjApiResponse.Id = objVendor_API_Requests.Id;
        //                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
        //                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
        //                {
        //                    resUpdateRecord.Res_Output = ApiResponse;
        //                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

        //                }
        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} use-internet-DishHome completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} use-internet-DishHome {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //[HttpPost]
        //[Route("api/use-internet-nt")]
        //public HttpResponseMessage GetServiceInternet_NT(Req_Vendor_API_NT_FTTH_Requests user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside use-internet-NT" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_NT_FTTH_Requests result = new Res_Vendor_API_NT_FTTH_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_NT_FTTH_Requests>(System.Net.HttpStatusCode.BadRequest, result);

        //    var userInput = getRawPostData().Result;
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
        //            //string md5hash = Common.CheckHash(user);
        //            string md5hash = Common.getHashMD5(userInput);
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
        //                //string UserInput = getRawPostData().Result;
        //                string CommonResult = "";
        //                AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
        //                if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
        //                {
        //                    Int64 memId = Convert.ToInt64(user.MemberId);
        //                    int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_NT_FTTH;
        //                    int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
        //                    resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
        //                    if (CommonResult.ToLower() != "success")
        //                    {
        //                        cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                        return response;
        //                    }
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                    return response;
        //                }
        //                GetVendor_API_NT_FTTH_Payment_Request objRes = new GetVendor_API_NT_FTTH_Payment_Request();
        //                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

        //                string authenticationToken = Request.Headers.Authorization.Parameter;
        //                Common.authenticationToken = authenticationToken;
        //                user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                bool IsCouponUnlocked = false; string TransactionID = string.Empty;
        //                string msg = RepKhalti.RequestServiceGroup_INTERNET_NT_FTTH_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.CustomerID, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
        //                    user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
        //                if (msg.ToLower() == "success")
        //                {
        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.status = objRes.status;
        //                    result.UniqueTransactionId = objRes.UniqueTransactionId;
        //                    result.WalletBalance = objRes.WalletBalance;
        //                    result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
        //                    result.Details = objRes.detail;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    result.CreditsConsumed = objRes.credits_consumed;
        //                    result.CreditsAvailable = objRes.credits_available;
        //                    result.ExtraData = objRes.extra_data;
        //                    result.TransactionUniqueId = TransactionID;
        //                    result.Id = objRes.id;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_NT_FTTH_Requests>(System.Net.HttpStatusCode.OK, result);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

        //                    try
        //                    {
        //                        var WalletTransactionId = TransactionID;
        //                        int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_NT_FTTH;
        //                        string jsonData = VendorApi_CommonHelper.Generate_json_Internet_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, user.CustomerID, "NT FTTH Internet", user.Number, "",
        //                        result.Message, user.Amount.ToString());
        //                        VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "NT FTTH Internet", resGetRecord.MemberId.ToString(), WalletTransactionId, jsonData, resGetRecord.ContactNumber, objVendor_API_Requests.MemberName, "Internet Payment", "NT FTTH Internet", user.Amount.ToString());
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
        //                }
        //                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
        //                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
        //                inobjApiResponse.Id = objVendor_API_Requests.Id;
        //                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
        //                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
        //                {
        //                    resUpdateRecord.Res_Output = ApiResponse;
        //                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

        //                }
        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} use-internet-NT completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} use-internet-NT {ex.ToString()} " + Environment.NewLine);
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
    }
}



//using log4net;
//using MyPay.API.Models;
//using MyPay.API.Models.Dishome;
//using MyPay.API.Models.Internet;
//using MyPay.API.Models.Internet.Arrownet;
//using MyPay.API.Models.Internet.ClassiTech;
//using MyPay.API.Models.Internet.DishHome;
//using MyPay.API.Models.Internet.ViaNet;
//using MyPay.API.Models.Internet.WebSurfer;
//using MyPay.API.Models.Request.Internet;
//using MyPay.API.Models.Response.Internet;
//using MyPay.Models.Add;
//using MyPay.Models.Common;
//using MyPay.Models.Get;
//using MyPay.Models.VendorAPI.Get.BusSewaService;
//using MyPay.Models.VendorAPI.Get.Internet.SUBISU;
//using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
//using MyPay.Repository;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Validation;
//using System.IO;
//using System.Net;
//using System.Net.Http;
//using System.Security.Cryptography.Xml;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;

//namespace MyPay.API.Controllers
//{
//    public class RequestAPI_InternetController : ApiController
//    {
//        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_InternetController));
//        string ApiResponse = string.Empty;

//        [HttpPost]
//        [Route("api/use-internet-adsl")]
//        public HttpResponseMessage GetServiceInternet_ADSL(Req_Vendor_API_ADSL_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-adsl" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_ADSL_Requests result = new Res_Vendor_API_ADSL_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_ADSL_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_adsl;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_ADSL_Payment_Request objRes = new GetVendor_API_ADSL_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_ADSL_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, user.IsVolumeBased, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "success";
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_ADSL_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-adsl completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-adsl {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/lookup-internet-subisu")]
//        public HttpResponseMessage GetLookupService_SUBISU(Req_Vendor_API_SUBISU_Lookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-subisu" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_Subisu_Lookup_Requests result = new Res_Vendor_API_Subisu_Lookup_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_Subisu_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

//                        Int64 memId = 0;
//                        int VendorAPIType = 0;
//                        int Type = 0;
//                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
//                        if (CommonResult.ToLower() != "success")
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        GetVendor_API_SUBISU_Lookup objRes = new GetVendor_API_SUBISU_Lookup();
//                        GetVendor_API_SUBISU_Lookup_TV_ComboOffer objResOffer = new GetVendor_API_SUBISU_Lookup_TV_ComboOffer();
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        string msg = RepKhalti.RequestServiceGroup_SUBISU_LOOKUP(user.UserName, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);

//                        if (objRes.plan_detail_list != null)
//                        {
//                            if (objRes.plan_detail_list.plan_detail_list != null)
//                            {
//                                if (!objRes.plan_detail_list.plan_detail_list.status && objRes.plan_detail_list.plan_detail_list.detail != null)
//                                {
//                                    CommonResponse cres1 = new CommonResponse();
//                                    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                    cres1.Message = objRes.plan_detail_list.plan_detail_list.detail;
//                                    cres1.ReponseCode = 3;
//                                    response.StatusCode = HttpStatusCode.BadRequest;
//                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                                    return response;
//                                }
//                            }
//                        }



//                        if (msg.ToLower() != "success")
//                        {
//                            //tv only and offer
//                            user.Reference = new CommonHelpers().GenerateUniqueId();
//                            msg = RepKhalti.RequestServiceGroup_SUBISU_NEW_LOOKUP(user.UserName, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objResOffer);

//                            if (msg.ToLower() == "success")
//                            {
//                                List<ComboPlanDetail> objDataOfferDetail = new List<ComboPlanDetail>();
//                                List<TvDetail> objDataStb = new List<TvDetail>();
//                                List<TvPlanDetail> objDataTv = new List<TvPlanDetail>();
//                                List<PlanDetailListOffer> objDataOffer = new List<PlanDetailListOffer>();

//                                if (objResOffer.plan_detail_list.plan_type == "tv")
//                                {
//                                    //tv
//                                    for (int i = 0; i < objResOffer.plan_detail_list.plan_detail_list.Count; i++)
//                                    {
//                                        TvDetail objDataStbItem = new TvDetail();
//                                        objDataStbItem.stb = objResOffer.plan_detail_list.plan_detail_list[i].stb;
//                                        objDataStb.Add(objDataStbItem);
//                                    }

//                                    for (int i = 0; i < objResOffer.plan_detail_list.plan_detail_list[0].tv_plan_details.Count; i++)
//                                    {
//                                        TvPlanDetail objDataTvItem = new TvPlanDetail();
//                                        objDataTvItem.plan_name = objResOffer.plan_detail_list.plan_detail_list[0].tv_plan_details[i].plan_name;
//                                        objDataTvItem.validity = objResOffer.plan_detail_list.plan_detail_list[0].tv_plan_details[i].validity;
//                                        objDataTvItem.amount = objResOffer.plan_detail_list.plan_detail_list[0].tv_plan_details[i].amount;
//                                        objDataTv.Add(objDataTvItem);
//                                    }
//                                }
//                                else
//                                {
//                                    //offer
//                                    for (int i = 0; i < objResOffer.plan_detail_list.plan_detail_list.Count; i++)
//                                    {
//                                        PlanDetailListOffer objDataItem = new PlanDetailListOffer();
//                                        objDataItem.offer_name = objResOffer.plan_detail_list.plan_detail_list[i].offer_name;
//                                        objDataItem.offer_id = objResOffer.plan_detail_list.plan_detail_list[i].offer_id;
//                                        objDataItem.validity = objResOffer.plan_detail_list.plan_detail_list[i].validity;
//                                        objDataItem.amount = objResOffer.plan_detail_list.plan_detail_list[i].amount;
//                                        objDataOffer.Add(objDataItem);
//                                    }
//                                }
//                                result.plan_type = objResOffer.plan_detail_list.plan_type;
//                                result.plan_detail_list_offer = objDataOffer;
//                                result.tv_details = objDataStb;
//                                result.tv_plan_details = objDataTv;

//                                result.customer_name = objResOffer.customer_name;
//                                result.address = objResOffer.address;
//                                result.current_plan_name = objResOffer.current_plan_name;
//                                result.user_id = objResOffer.user_id;
//                                result.outstanding_amount = objResOffer.outstanding_amount;
//                                result.expiry_date = objResOffer.expiry_date;
//                                result.mobile_no = objResOffer.mobile_no;
//                                result.onu_id = objResOffer.onu_id;
//                                result.partner_name = objResOffer.partner_name;
//                                result.token = objResOffer.token;
//                                result.session_id = objResOffer.session_id;
//                                //add reference no here
//                                result.ReferenceNo = user.Reference;
//                                result.ReponseCode = objResOffer.status ? 1 : 0;
//                                result.status = objResOffer.status;
//                                result.Message = "success";
//                                response.StatusCode = HttpStatusCode.Accepted;
//                                response = Request.CreateResponse<Res_Vendor_API_Subisu_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                            }
//                            else
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            }



//                        }
//                        else if (msg.ToLower() == "success")
//                        {
//                            //internet only and internet+tv

//                            List<InternetPlanDetail> objData = new List<InternetPlanDetail>();
//                            List<TvDetail> objDataStb = new List<TvDetail>();
//                            List<TvPlanDetail> objDataTv = new List<TvPlanDetail>();

//                            //  if (objRes.plan_detail_list.status == true)
//                            if (objRes.plan_detail_list.status == true || objRes.plan_detail_list.plan_detail_list != null)

//                            {
//                                if (objRes.plan_detail_list.plan_type == "internet")
//                                {
//                                    for (int i = 0; i < objRes.plan_detail_list.plan_detail_list.internet_plan_details.Count; i++)
//                                    {
//                                        InternetPlanDetail objDataItem = new InternetPlanDetail();
//                                        objDataItem.plan_name = objRes.plan_detail_list.plan_detail_list.internet_plan_details[i].plan_name;
//                                        objDataItem.validity = objRes.plan_detail_list.plan_detail_list.internet_plan_details[i].validity;
//                                        objDataItem.amount = objRes.plan_detail_list.plan_detail_list.internet_plan_details[i].amount;
//                                        objData.Add(objDataItem);
//                                    }
//                                }
//                                else
//                                {
//                                    for (int i = 0; i < objRes.plan_detail_list.plan_detail_list.internet_details.internet_plan_details.Count; i++)
//                                    {
//                                        InternetPlanDetail objDataItem = new InternetPlanDetail();
//                                        objDataItem.plan_name = objRes.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[i].plan_name;
//                                        objDataItem.validity = objRes.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[i].validity;
//                                        objDataItem.amount = objRes.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[i].amount;
//                                        objData.Add(objDataItem);
//                                    }

//                                    for (int i = 0; i < objRes.plan_detail_list.plan_detail_list.tv_details.Count; i++)
//                                    {
//                                        TvDetail objDataStbItem = new TvDetail();
//                                        objDataStbItem.stb = objRes.plan_detail_list.plan_detail_list.tv_details[i].stb;
//                                        objDataStb.Add(objDataStbItem);
//                                    }

//                                    for (int i = 0; i < objRes.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details.Count; i++)
//                                    {
//                                        TvPlanDetail objDataTvItem = new TvPlanDetail();
//                                        objDataTvItem.plan_name = objRes.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[i].plan_name;
//                                        objDataTvItem.validity = objRes.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[i].validity;
//                                        objDataTvItem.amount = objRes.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[i].amount;
//                                        objDataTv.Add(objDataTvItem);
//                                    }
//                                }
//                            }

//                            result.plan_type = objRes.plan_detail_list.plan_type;
//                            result.internet_plan_details = objData;
//                            result.tv_details = objDataStb;
//                            result.tv_plan_details = objDataTv;

//                            result.customer_name = objRes.customer_name;
//                            result.address = objRes.address;
//                            result.current_plan_name = objRes.current_plan_name;
//                            result.user_id = objRes.user_id;
//                            result.outstanding_amount = objRes.outstanding_amount;
//                            result.expiry_date = objRes.expiry_date;
//                            result.mobile_no = objRes.mobile_no;
//                            result.onu_id = objRes.onu_id;
//                            result.partner_name = objRes.partner_name;
//                            result.token = objRes.token;
//                            result.session_id = objRes.session_id;
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.Message = "success";

//                            result.ReferenceNo = user.Reference;

//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_Subisu_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-subisu completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-subisu {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/use-internet-subisu")]
//        public HttpResponseMessage GetServiceInternet_SUBISU(Req_Vendor_API_SUBISU_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-subisu" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_SUBISU_Requests result = new Res_Vendor_API_SUBISU_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_SUBISU_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    // string md5hash = Common.CheckHash(user);

//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_SUBISU_Payment_Request objRes = new GetVendor_API_SUBISU_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        //user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_SUBISU_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.SessionID, user.OfferName, user.PlanType, user.stb, user.CustomerID, user.Amount, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_SUBISU_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


//                            try
//                            {
//                                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new;
//                                var WalletTransactionId = TransactionID;
//                                string jsonData = VendorApi_CommonHelper.Generate_json_Internet_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, user.CustomerID, "Subisu Internet", user.OfferName, "",
//                                result.Message, user.Amount.ToString());
//                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "Subisu Internet", "", WalletTransactionId, jsonData, resGetRecord.ContactNumber, objVendor_API_Requests.MemberName, "Internet Payment", "Subisu Internet", user.Amount.ToString());
//                            }
//                            catch (Exception ex)
//                            {

//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-subisu completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-subisu {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/use-internet-subisu-old")]
//        public HttpResponseMessage GetServiceInternet_SUBISU_OLD(Req_Vendor_API_SUBISU_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-subisu" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_SUBISU_Requests result = new Res_Vendor_API_SUBISU_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_SUBISU_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_SUBISU_Payment_Request objRes = new GetVendor_API_SUBISU_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_SUBISU_PAYMENT_OLD(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.CustomerID, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_SUBISU_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-subisu completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-subisu {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/lookup-internet-vianet")]
//        public HttpResponseMessage GetLookupService_ViaNet(Req_Vendor_API_ViaNet_Lookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-vianet" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_ViaNet_Lookup_Requests result = new Res_Vendor_API_ViaNet_Lookup_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_ViaNet_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

//                        Int64 memId = 0;
//                        int VendorAPIType = 0;
//                        int Type = 0;
//                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
//                        if (CommonResult.ToLower() != "success")
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        GetVendor_API_ViaNet_Lookup objRes = new GetVendor_API_ViaNet_Lookup();
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        string msg = RepKhalti.RequestServiceGroup_ViaNet_LOOKUP(user.CustomerID, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
//                        if (msg.ToLower() == "success")
//                        {
//                            List<ViaNet_Data> objData = new List<ViaNet_Data>();
//                            for (int i = 0; i < objRes.bills.Count; i++)
//                            {
//                                ViaNet_Data objDataItem = new ViaNet_Data();
//                                objDataItem.PaymentID = objRes.bills[i].payment_id;
//                                objDataItem.BillDate = objRes.bills[i].bill_date;
//                                objDataItem.Service_Details = objRes.bills[i].service_details;
//                                objDataItem.Service_Name = objRes.bills[i].service_name;
//                                objDataItem.BillAmount = objRes.bills[i].amount;
//                                objData.Add(objDataItem);
//                            }
//                            result.SessionID = objRes.session_id;
//                            result.CustomerID = objRes.customer_id;
//                            result.CustomerName = objRes.customer_name;
//                            result.Vianet_Bills = objData;
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.Message = "success";
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_ViaNet_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-vianet completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-vianet {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/use-internet-vianet")]
//        public HttpResponseMessage GetServiceInternet_ViaNet(Req_Vendor_API_ViaNet_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-vianet" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_ViaNet_Requests result = new Res_Vendor_API_ViaNet_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_ViaNet_Requests>(System.Net.HttpStatusCode.BadRequest, result);
//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //  string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_vianet;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_ViaNet_Payment_Request objRes = new GetVendor_API_ViaNet_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_ViaNet_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.CustomerID, user.SessionID, user.PaymentID, user.Amount, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.TransactionUniqueId = TransactionID;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_ViaNet_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-vianet completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-vianet {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/lookup-internet-classitech")]
//        public HttpResponseMessage GetLookupService_CLASSITECH(Req_Vendor_API_Classitech_Lookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-classitech" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_Classitech_Lookup_Requests result = new Res_Vendor_API_Classitech_Lookup_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_Classitech_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

//                        Int64 memId = 0;
//                        int VendorAPIType = 0;
//                        int Type = 0;
//                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
//                        if (CommonResult.ToLower() != "success")
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        GetVendor_API_Classitech_Lookup objRes = new GetVendor_API_Classitech_Lookup();
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        string msg = RepKhalti.RequestServiceGroup_CLASSITECH_LOOKUP(user.UserName, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
//                        if (msg.ToLower() == "success")
//                        {
//                            List<ClassiTech_Data> objData = new List<ClassiTech_Data>();
//                            for (int i = 0; i < objRes.available_plans.Count; i++)
//                            {
//                                ClassiTech_Data objDataItem = new ClassiTech_Data();
//                                objDataItem.Package = objRes.available_plans[i].package;
//                                objDataItem.Duration = objRes.available_plans[i].duration;
//                                objDataItem.Amount = objRes.available_plans[i].amount;
//                                objData.Add(objDataItem);
//                            }
//                            result.SessionID = objRes.session_id;
//                            result.Available_Plans = objData;
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.Message = "success";
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_Classitech_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-classitech completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-classitech {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/use-internet-classitech")]
//        public HttpResponseMessage GetServiceInternet_CLASSITECH(Req_Vendor_API_Classitech_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-classitech" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_Classitech_Requests result = new Res_Vendor_API_Classitech_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_Classitech_Requests>(System.Net.HttpStatusCode.BadRequest, result);
//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                       // string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_classictech;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_Classitech_Payment_Request objRes = new GetVendor_API_Classitech_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_CLASSITECH_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.SessionID, user.Amount, user.Month, user.Package, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_Classitech_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-classitech completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-classitech {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/lookup-internet-arrownet")]
//        public HttpResponseMessage GetLookupService_Arrownet(Req_Vendor_API_Arrownet_Lookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-arrownet" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_Arrownet_Lookup_Requests result = new Res_Vendor_API_Arrownet_Lookup_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_Arrownet_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

//                        Int64 memId = 0;
//                        int VendorAPIType = 0;
//                        int Type = 0;
//                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
//                        if (CommonResult.ToLower() != "success")
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        GetVendor_API_Arrownet_Lookup objRes = new GetVendor_API_Arrownet_Lookup();
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        string msg = RepKhalti.RequestServiceGroup_Arrownet_LOOKUP(user.UserName, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
//                        if (msg.ToLower() == "success")
//                        {
//                            List<Arrownet_Data> objData = new List<Arrownet_Data>();
//                            for (int i = 0; i < objRes.plan_details.Count; i++)
//                            {
//                                Arrownet_Data objDataItem = new Arrownet_Data();
//                                objDataItem.Duration = objRes.plan_details[i].duration;
//                                objDataItem.Amount = objRes.plan_details[i].amount;
//                                objData.Add(objDataItem);
//                            }
//                            result.Full_name = objRes.full_name;
//                            result.Days_Remaining = objRes.days_remaining;
//                            result.Current_Plan = objRes.current_plan;
//                            result.Has_Due = objRes.has_due;
//                            result.Plan_Details = objData;
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.Message = "success";
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_Arrownet_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-arrownet completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-arrownet {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/use-internet-Arrownet")]
//        public HttpResponseMessage GetServiceInternet_Arrownet(Req_Vendor_API_Arrownet_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-Arrownet" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_Arrownet_Requests result = new Res_Vendor_API_Arrownet_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_Arrownet_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Arrownet;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_Arrownet_Payment_Request objRes = new GetVendor_API_Arrownet_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_Arrownet_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Amount, user.Duration, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_Arrownet_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-Arrownet completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-Arrownet {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/use-internet-virtual-network")]
//        public HttpResponseMessage GetServiceInternet_VirtualNetwork(Req_Vendor_API_VirtualNetwork_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-virtual-network" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_VirtualNetwork_Requests result = new Res_Vendor_API_VirtualNetwork_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_VirtualNetwork_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    // string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorAPIType = 0;
//                            int Type = 0;
//                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                CommonResponse cres1 = new CommonResponse();
//                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        GetVendor_API_VirtualNetwork_Payment_Request objRes = new GetVendor_API_VirtualNetwork_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_VirtualNetwork_PAYMENT(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, user.MemberId, authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_VirtualNetwork_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-virtual-network completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-virtual-network {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/use-internet-web-network")]
//        public HttpResponseMessage GetServiceInternet_WebNetwork(Req_Vendor_API_WebNetwork_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-web-network" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_WebNetwork_Requests result = new Res_Vendor_API_WebNetwork_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_WebNetwork_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //   string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebNetwork;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_WebNetwork_Payment_Request objRes = new GetVendor_API_WebNetwork_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_WebNetwork_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_WebNetwork_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-web-network completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-web-network {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/use-internet-royal-network")]
//        public HttpResponseMessage GetServiceInternet_RoyalNetwork(Req_Vendor_API_RoyalNetwork_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-royal-network" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_RoyalNetwork_Requests result = new Res_Vendor_API_RoyalNetwork_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_RoyalNetwork_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                       // string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_RoyalNetwork;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_RoyalNetwork_Payment_Request objRes = new GetVendor_API_RoyalNetwork_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_RoyalNetwork_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_RoyalNetwork_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-royal-network completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-royal-network {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/lookup-internet-websurfer-userlist")]
//        public HttpResponseMessage GetLookupService_WebSurfer_Userlist(Req_Vendor_API_WebSurfer_UserListLookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-websurfer-userlist" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_WebSurfer_UserList_Lookup_Requests result = new Res_Vendor_API_WebSurfer_UserList_Lookup_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_WebSurfer_UserList_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

//                        Int64 memId = 0;
//                        int VendorAPIType = 0;
//                        int Type = 0;
//                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
//                        if (CommonResult.ToLower() != "success")
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        GetVendor_API_WebSurfer_UserList_Lookup objRes = new GetVendor_API_WebSurfer_UserList_Lookup();
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        string msg = RepKhalti.RequestServiceGroup_WebSurfer_UserList_LOOKUP(user.CustomerId, user.UniqueCustomerId, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
//                        if (msg.ToLower() == "success")
//                        {
//                            result.connection = objRes.connection;
//                            result.customer = objRes.customer;
//                            result.SessionId = objRes.session_id;
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_WebSurfer_UserList_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-websurfer-userlist completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-websurfer-userlist {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/lookup-internet-websurfer")]
//        public HttpResponseMessage GetLookupService_WebSurfer(Req_Vendor_API_WebSurfer_Lookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-websurfer" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_WebSurfer_Lookup_Requests result = new Res_Vendor_API_WebSurfer_Lookup_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_WebSurfer_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    string md5hash = Common.CheckHash(user);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

//                        Int64 memId = 0;
//                        int VendorAPIType = 0;
//                        int Type = 0;
//                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
//                        if (CommonResult.ToLower() != "success")
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        GetVendor_API_WebSurfer_Lookup objRes = new GetVendor_API_WebSurfer_Lookup();
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        string msg = RepKhalti.RequestServiceGroup_WebSurfer_LOOKUP(user.UserName, user.SessionId, user.UniqueCustomerId, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
//                        if (msg.ToLower() == "success")
//                        {
//                            result.Session_Id = objRes.session_id;
//                            result.packages = objRes.packages;
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_WebSurfer_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-websurfer completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-websurfer {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/use-internet-websurfer")]
//        public HttpResponseMessage GetServiceInternet_WebSurfer(Req_Vendor_API_WebSurfer_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-websurfer" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_WebSurfer_Requests result = new Res_Vendor_API_WebSurfer_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_WebSurfer_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebSurfer;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_WebSurfer_Payment_Request objRes = new GetVendor_API_WebSurfer_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_WebSurfer_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.SessionID, user.Amount, user.package_id, user.Service, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_WebSurfer_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-websurfer completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-websurfer {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }



//        [HttpPost]
//        [Route("api/lookup-internet-techminds")]
//        public HttpResponseMessage GetLookupService_Techminds(Req_Vendor_API_Techminds_Lookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-techminds" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_Techminds_Lookup_Requests result = new Res_Vendor_API_Techminds_Lookup_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_Techminds_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

//                        Int64 memId = 0;
//                        int VendorAPIType = 0;
//                        int Type = 0;
//                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
//                        if (CommonResult.ToLower() != "success")
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        GetVendor_API_Techminds_Lookup objRes = new GetVendor_API_Techminds_Lookup();
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        string msg = RepKhalti.RequestServiceGroup_Techminds_LOOKUP(user.RequestID, user.UniqueCustomerId, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
//                        if (msg.ToLower() == "success")
//                        {
//                            Techminds_Data objDataItemData = new Techminds_Data();
//                            objDataItemData.CustomerName = objRes.data.customer_name;
//                            objDataItemData.MobileNumber = objRes.data.mobile_number;
//                            objDataItemData.PreviousBalance = objRes.data.previous_balance;
//                            objDataItemData.MonthlyCharge = objRes.data.monthly_charge;
//                            objDataItemData.ExpirationDate = objRes.data.expiration;
//                            objDataItemData.Email = objRes.data.email;
//                            objDataItemData.MonthlyCharge = objRes.data.monthly_charge;
//                            result.Techminds_Data = objDataItemData;
//                            Techminds_Plans objDataItem = new Techminds_Plans();
//                            objDataItem.Plan_12Month = objRes.available_plans._12_Month;
//                            objDataItem.Plan_6Month = objRes.available_plans._6_Month;
//                            objDataItem.Plan_3Month = objRes.available_plans._3_Month;
//                            objDataItem.Plan_1Month = objRes.available_plans._1_Month;
//                            objDataItem.Plan_15Days = objRes.available_plans._15Days;
//                            objDataItem.Plan_180Days = objRes.available_plans._180Days;
//                            objDataItem.Plan_30Days = objRes.available_plans._30Days;
//                            objDataItem.Plan_60Days = objRes.available_plans._60Days;
//                            objDataItem.Plan_90Days = objRes.available_plans._90Days;

//                            var serilaizeJson = JsonConvert.SerializeObject(objDataItem, Formatting.None,
//                            new JsonSerializerSettings
//                            {
//                                NullValueHandling = NullValueHandling.Ignore
//                            });

//                            var result1 = JsonConvert.DeserializeObject<dynamic>(serilaizeJson);
//                            result.Available_Plans = result1;
//                            result.SessionID = objRes.session_id;
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_Techminds_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-techminds completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-techminds {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }


//        [HttpPost]
//        [Route("api/use-internet-techminds")]
//        public HttpResponseMessage GetServiceInternet_Techminds(Req_Vendor_API_Techminds_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-techminds" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_Techminds_Requests result = new Res_Vendor_API_Techminds_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_Techminds_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_TechMinds;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_Techminds_Payment_Request objRes = new GetVendor_API_Techminds_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_Techminds_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.SessionID, user.Amount, user.RequestID, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.TransactionUniqueId = TransactionID;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_Techminds_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-techminds completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-techminds {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/use-internet-pokhara")]
//        public HttpResponseMessage GetServiceInternet_Pokhara(Req_Vendor_API_Pokhara_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-pokhara" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_Pokhara_Requests result = new Res_Vendor_API_Pokhara_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_Pokhara_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Pokhara;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_Pokhara_Payment_Request objRes = new GetVendor_API_Pokhara_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_Pokhara_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.UserName, user.Number, user.Amount, user.Address, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {

//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.TransactionUniqueId = TransactionID;
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_Pokhara_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-pokhara completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-pokhara {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/lookup-internet-dishhome")]
//        public HttpResponseMessage GetLookupService_Dishhome(Req_Vendor_API_DishHome_Lookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside lookup-internet-dishhome" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_DishHome_Lookup_Requests result = new Res_Vendor_API_DishHome_Lookup_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_DishHome_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

//                        Int64 memId = 0;
//                        int VendorAPIType = 0;
//                        int Type = 0;
//                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
//                        if (CommonResult.ToLower() != "success")
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        Vendor_API_DishHome objRes = new Vendor_API_DishHome();
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        string msg = RepKhalti.RequestServiceGroup_DishHome_LOOKUP(user.CustomerID, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
//                        if (msg.ToLower() == "success")
//                        {
//                            //List<DishHome_Data> objData = new List<DishHome_Data>();
//                            //for (int i = 0; i < objRes.bills.Count; i++)
//                            //{
//                            //    DishHome_Data objDataItem = new DishHome_Data();
//                            //    objDataItem.PaymentID = objRes.bills[i].payment_id;
//                            //    objDataItem.BillDate = objRes.bills[i].bill_date;
//                            //    objDataItem.Service_Details = objRes.bills[i].service_details;
//                            //    objDataItem.Service_Name = objRes.bills[i].service_name;
//                            //    objDataItem.BillAmount = objRes.bills[i].amount;
//                            //    objData.Add(objDataItem);
//                            //}
//                            result.SessionID = objRes.session_id;
//                            result.CustomerID = objRes.customer_id;
//                            result.CustomerName = objRes.customer_name;
//                            result.PackageName = objRes.package_name;
//                            result.Amount = objRes.amount;
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.Message = "success";
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_DishHome_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} lookup-internet-dishhome completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} lookup-internet-dishhome {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }
//        [HttpPost]
//        [Route("api/use-internet-dishhome")]
//        public HttpResponseMessage GetServiceInternet_Dishhome(Req_Vendor_API_DishHome_Lookup_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-DishHome" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_DishHome_Requests result = new Res_Vendor_API_DishHome_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_DishHome_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Dishhome;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_Dishhome_FTTH_Payment_Request objRes = new GetVendor_API_Dishhome_FTTH_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_DishHome_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.CustomerID, user.PackageName, user.SessionId, user.Amount, user.Duration, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.CreditsConsumed = objRes.credits_consumed;
//                            result.CreditsAvailable = objRes.credits_available;
//                            result.ExtraData = objRes.extra_data;
//                            result.TransactionUniqueId = TransactionID;
//                            result.Id = objRes.id;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_DishHome_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

//                            try
//                            {
//                                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Dishhome;
//                                var WalletTransactionId = TransactionID;
//                                string jsonData = VendorApi_CommonHelper.Generate_json_Internet_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, user.CustomerID, "DishHome FTTH Internet", user.PackageName, "",
//                                result.Message, user.Amount.ToString());
//                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "DishHome FTTH Internet", resGetRecord.MemberId.ToString(), WalletTransactionId, jsonData, resGetRecord.ContactNumber, objVendor_API_Requests.MemberName, "Internet Payment", "DishHome FTTH Internet", user.Amount.ToString());
//                            }
//                            catch (Exception ex)
//                            {

//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-DishHome completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-DishHome {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }

//        [HttpPost]
//        [Route("api/use-internet-nt")]
//        public HttpResponseMessage GetServiceInternet_NT(Req_Vendor_API_NT_FTTH_Requests user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside use-internet-NT" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_Vendor_API_NT_FTTH_Requests result = new Res_Vendor_API_NT_FTTH_Requests();
//            var response = Request.CreateResponse<Res_Vendor_API_NT_FTTH_Requests>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;
//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    //string md5hash = Common.CheckHash(user);
//                    string md5hash = Common.getHashMD5(userInput);
//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        //string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_NT_FTTH;
//                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
//                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        GetVendor_API_NT_FTTH_Payment_Request objRes = new GetVendor_API_NT_FTTH_Payment_Request();
//                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
//                        user.Reference = new CommonHelpers().GenerateUniqueId();
//                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
//                        string msg = RepKhalti.RequestServiceGroup_INTERNET_NT_FTTH_PAYMENT(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.CustomerID, user.Number, user.Amount, user.Reference, user.Version, user.DeviceCode,
//                            user.PlatForm, resGetRecord.MemberId.ToString(), authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
//                        if (msg.ToLower() == "success")
//                        {
//                            result.ReponseCode = objRes.status ? 1 : 0;
//                            result.status = objRes.status;
//                            result.UniqueTransactionId = objRes.UniqueTransactionId;
//                            result.WalletBalance = objRes.WalletBalance;
//                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
//                            result.Details = objRes.detail;
//                            result.Message = "Success";
//                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
//                            result.CreditsConsumed = objRes.credits_consumed;
//                            result.CreditsAvailable = objRes.credits_available;
//                            result.ExtraData = objRes.extra_data;
//                            result.TransactionUniqueId = TransactionID;
//                            result.Id = objRes.id;
//                            response.StatusCode = HttpStatusCode.Accepted;
//                            response = Request.CreateResponse<Res_Vendor_API_NT_FTTH_Requests>(System.Net.HttpStatusCode.OK, result);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

//                            try
//                            {
//                                var WalletTransactionId = TransactionID;
//                                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_NT_FTTH;
//                                string jsonData = VendorApi_CommonHelper.Generate_json_Internet_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, user.CustomerID, "NT FTTH Internet", user.Number, "",
//                                result.Message, user.Amount.ToString());
//                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "NT FTTH Internet", resGetRecord.MemberId.ToString(), WalletTransactionId, jsonData, resGetRecord.ContactNumber, objVendor_API_Requests.MemberName, "Internet Payment", "NT FTTH Internet", user.Amount.ToString());
//                            }
//                            catch (Exception ex)
//                            {

//                            }
//                        }
//                        else
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
//                        }
//                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                        inobjApiResponse.Id = objVendor_API_Requests.Id;
//                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
//                        {
//                            resUpdateRecord.Res_Output = ApiResponse;
//                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");

//                        }
//                    }
//                }
//                log.Info($"{System.DateTime.Now.ToString()} use-internet-NT completed" + Environment.NewLine);
//                return response;
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} use-internet-NT {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }
//        }
//        private async Task<String> getRawPostData()
//        {
//            using (var contentStream = await this.Request.Content.ReadAsStreamAsync())
//            {
//                contentStream.Seek(0, SeekOrigin.Begin);
//                using (var sr = new StreamReader(contentStream))
//                {
//                    return sr.ReadToEnd();
//                }
//            }
//        }
//    }
//}