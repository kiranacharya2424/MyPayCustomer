using log4net;
using MyPay.API.Models;
using MyPay.API.Models.KhanePani;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Get.KhanePani;
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

namespace MyPay.API.Controllers
{
    public class RequestAPI_KhanePaniController : ApiController
    {
        string ApiResponse = string.Empty;

        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_KhanePaniController));
        [HttpPost]
        [Route("api/servicegroup-counters-khanepani")]
        public HttpResponseMessage GetServiceGroupCounters_Khanepani(Req_Vendor_API_Counters_KhanePani_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside servicegroup-counters-khanepani" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Counters_KhanePani_Requests result = new Res_Vendor_API_Counters_KhanePani_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Counters_KhanePani_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        GetVendor_API_KhanePani_Counters objRes = new GetVendor_API_KhanePani_Counters();
                        string msg = RepKhalti.RequestServiceGroup_KHANEPANI_COUNTER(user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<Models.KhanePani.service_group_counters> objCOunter = new List<Models.KhanePani.service_group_counters>();
                            for (int i = 0; i < objRes.counters.Count; i++)
                            {
                                Models.KhanePani.service_group_counters _obj = new Models.KhanePani.service_group_counters();
                                _obj.name = objRes.counters[i].name;
                                _obj.value = objRes.counters[i].value;
                                objCOunter.Add(_obj);
                            }
                            result.counters = objCOunter;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.state = objRes.status ? "1" : "0";
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Counters_KhanePani_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} servicegroup-counters-khanepani completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} servicegroup-counters-khanepani {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/servicegroup-details-khanepani")]
        public HttpResponseMessage GetServiceGroupDetailsKHANEPANI(Req_Vendor_API_ServiceGroup_KhanePani_Details_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside servicegroup-details-khanepani" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_ServiceGroup_KhanePani_Details_Requests result = new Res_Vendor_API_ServiceGroup_KhanePani_Details_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ServiceGroup_KhanePani_Details_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        GetVendor_API_ServiceGroup_KhanePani_Details objRes = new GetVendor_API_ServiceGroup_KhanePani_Details();
                        string msg = RepKhalti.RequestServiceGroup_DETAILS_KHANEPANI(user.Month_Id, user.Consumer_Code, user.Counter, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.Consumer_Code = objRes.customer_code;
                            result.Consumer_Name = objRes.customer_name;
                            result.Address = objRes.address;
                            result.Mobile = objRes.mobile_number;
                            result.Current_month_discount = objRes.current_month_discount;
                            result.Current_Month_Dues = objRes.current_month_dues;
                            result.Current_Month_fine = objRes.current_month_fine;
                            result.Total_credit_sales_amount = objRes.total_credit_sales_amount;
                            result.Total_advance_amount = objRes.total_advance_amount;
                            result.Previous_dues = objRes.previous_dues;
                            result.Total_Dues = objRes.total_dues;
                            result.Minimum_Payable_Amount = objRes.minimum_payable_amount;
                            result.status = objRes.status;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ServiceGroup_KhanePani_Details_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} servicegroup-details-khanepani completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} servicegroup-details-khanepani {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/servicegroup-commit-khanepani")]
        public HttpResponseMessage GetServiceGroupCommitKHANEPANI(Req_Vendor_API_Commit_KhanePani_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside servicegroup-commit-khanepani" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Commit_KhanePani_Requests result = new Res_Vendor_API_Commit_KhanePani_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_KhanePani_Requests>(System.Net.HttpStatusCode.BadRequest, result);
           
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
                            int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_khanepani;
                            int Type = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorApiType, memId, true, true, user.Amount, true, user.Mpin, "", false, true);
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
                        GetVendor_API_ServiceGroup_KhanePani_Commit objRes = new GetVendor_API_ServiceGroup_KhanePani_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.ReferenceNo = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_KHANEPANI(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.Amount, user.CustomerCode, user.ReferenceNo, user.Counter, userInput, user.MemberId,
                            authenticationToken, user.DeviceCode, user.PlatForm, user.VendorJsonLookup, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.status = objRes.status;
                            result.TransactionUniqueId = TransactionID;
                            result.Due_Amount = objRes.due_amount;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            Detail objDtl = new Detail();
                            if (objRes.detailsobject != null)
                            {
                                objDtl.Message = objRes.detailsobject.message;
                                objDtl.Due_Amount = objRes.detailsobject.due_amount;
                            }
                            result.Detail = objDtl;
                            result.Extra_Data = new Extra_Data();
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_KhanePani_Requests>(System.Net.HttpStatusCode.OK, result);
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
                log.Info($"{System.DateTime.Now.ToString()} servicegroup-commit-khanepani completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} servicegroup-commit-khanepani {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/servicegroup-servicecharge-khanepani")]
        public HttpResponseMessage GetServiceGroupServiceChargeKHANEPANI(Req_Vendor_API_ServiceCharge_KhanePani_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside servicegroup-servicecharge-khanepani" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_ServiceCharge_KhanePani_Requests result = new Res_Vendor_API_ServiceCharge_KhanePani_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ServiceCharge_KhanePani_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_ServiceGroup_KhaniPani_ServiceCharge objRes = new GetVendor_API_ServiceGroup_KhaniPani_ServiceCharge();
                        string msg = RepKhalti.RequestServiceGroup_SERVICE_CHARGE_KHANEPANI(user.Amount, user.Counter, ref objRes);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.service_charge = objRes.service_charge;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.status = objRes.status;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ServiceCharge_KhanePani_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} servicegroup-servicecharge-khanepani copleted" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} servicegroup-servicecharge-khanepani {ex.ToString()} " + Environment.NewLine);
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