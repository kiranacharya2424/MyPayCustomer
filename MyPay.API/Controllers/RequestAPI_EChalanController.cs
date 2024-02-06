using log4net;
using MyPay.API.Models;
using MyPay.API.Models.EChalan;
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

namespace MyPay.API.Controllers
{
    public class RequestAPI_EChalanController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_EChalanController));
        string ApiResponse = string.Empty;


        [HttpPost]
        [Route("api/fiscal-year-echalan")]
        public HttpResponseMessage GetLookupServiceTV_EChalan(Req_Vendor_API_EChalan_Lookup_FiscalYear_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-fiscal-echalan" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_EChalan_Lookup_Fisacal_Requests result = new Res_Vendor_API_EChalan_Lookup_Fisacal_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_EChalan_Lookup_Fisacal_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                       // string CommonResult = "success";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        //Int64 memId = 0; //Int64.Parse(user.MemberId); //0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    CommonResponse cres1 = new CommonResponse();
                        //    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //    return response;
                        //}
                        List<EChalan_Lookup_FisacalYear> objYearList = new List<EChalan_Lookup_FisacalYear>();

                        EChalan_Lookup_FisacalYear objYear = new EChalan_Lookup_FisacalYear();
                        int CurrentYear = System.DateTime.Now.Year - 2000;
                        int NepalCalAdjust = 58;
                        for (int i = CurrentYear; i > (CurrentYear - 3); i--)
                        {
                            objYear = new EChalan_Lookup_FisacalYear();
                            objYear.YearName = ((CurrentYear + (NepalCalAdjust - 1)).ToString() + "/" + (CurrentYear + NepalCalAdjust).ToString());
                            objYear.YearCode = ((CurrentYear + (NepalCalAdjust - 1)).ToString() + (CurrentYear + NepalCalAdjust).ToString());
                            NepalCalAdjust = NepalCalAdjust - 1;
                            objYearList.Add(objYear);
                        }
                        result.ReponseCode = 1;
                        result.status = true;
                        result.Year = objYearList;
                        result.Message = "success";
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_Vendor_API_EChalan_Lookup_Fisacal_Requests>(System.Net.HttpStatusCode.OK, result);
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-fiscal-echalan completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-fiscal--echalan {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }




        [HttpPost]
        [Route("api/lookup-echalan-district")]
        public HttpResponseMessage GetLookupServiceTV_EChalan_DISTRICT_CODES(Req_Vendor_API_EChalan_Lookup_DistrictCodes_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-echalan-district-codes" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_EChalan_Lookup_DistrictCodes_Requests result = new Res_Vendor_API_EChalan_Lookup_DistrictCodes_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_EChalan_Lookup_DistrictCodes_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    string md5hash = Common.getHashMD5(userInput);    //Common.CheckHash(user);

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
                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    CommonResponse cres1 = new CommonResponse();
                        //    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //    return response;
                        //}


                        GetVendor_API_EChalan_Lookup_DistrictCode objRes = new GetVendor_API_EChalan_Lookup_DistrictCode();
                        user.Reference = new CommonHelpers().GenerateUniqueId();

                        string msg = RepKhalti.RequestServiceGroup_EChalan_DISTRICT_CODES_LOOKUP(user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.locations = objRes.locations;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_EChalan_Lookup_DistrictCodes_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-echalan-district-codes completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-echalan-district-codes {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPost]
        [Route("api/lookup-echalan")]
        public HttpResponseMessage GetLookupServiceTV_EChalan(Req_Vendor_API_EChalan_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-echalan" + Environment.NewLine);
           // string UserInput = getRawPostData().Result;
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_EChalan_Lookup_Requests result = new Res_Vendor_API_EChalan_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_EChalan_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                        //string CommonResult = "";
                        //AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        //AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    CommonResponse cres1 = new CommonResponse();
                        //    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //    return response;
                        //}


                        GetVendor_API_EChalan_Lookup objRes = new GetVendor_API_EChalan_Lookup();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        //user.app_id = "MER-7-APP-10";
                        user.app_id = "GON-7-TVRS-1";

                        string msg = RepKhalti.RequestServiceGroup_EChalan_LOOKUP(user.Reference, user.app_id, user.voucher_no, user.service, user.fiscal_year, user.province_code, user.district_code, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.validity = objRes.validity;
                            result.creditor_name = objRes.creditor_name;
                            result.debitor_name = objRes.debitor_name;
                            result.app_id = objRes.app_id;
                            result.voucher_no = objRes.voucher_no;
                            result.remarks = objRes.remarks;
                            result.particulars = objRes.particulars;
                            result.amount = objRes.amount;
                            result.reference = user.Reference;
                            result.organization = objRes.organization;
                            result.session_id = objRes.session_id;
                            result.description = objRes.description;
                            result.full_name = objRes.full_name;
                            result.vehicle_category = objRes.vehicle_category;
                            result.tracing_id = objRes.tracing_id;
                            result.bank_code = objRes.bank_code;
                            result.chit_number = objRes.chit_number;
                            result.ebp_number = objRes.ebp_number;
                            result.Message = "success";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_EChalan_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-echalan completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-echalan {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/use-echalan")]
        public HttpResponseMessage GetService_EChalan(Req_Vendor_API_EChalan_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-echalan" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_EChalan_Requests result = new Res_Vendor_API_EChalan_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_EChalan_Requests>(System.Net.HttpStatusCode.BadRequest, result);

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
                    string md5hash = Common.getHashMD5(userInput); // Common.CheckHash(user);

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
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_EChalan_Payment_Request objRes = new GetVendor_API_EChalan_Payment_Request();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        //user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_EChalan_PAYMENT(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.Session_id, user.Amount, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, user.MemberId, authenticationToken, userInput, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.UniqueTransactionId = objRes.UniqueTransactionId;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Pin = objRes.pin;
                            result.Serial = objRes.serial;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_EChalan_Requests>(System.Net.HttpStatusCode.OK, result);
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
                log.Info($"{System.DateTime.Now.ToString()} use-echalan completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-echalan {ex.ToString()} " + Environment.NewLine);
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