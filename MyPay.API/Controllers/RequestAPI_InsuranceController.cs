using log4net;
using Microsoft.Owin.BuilderProperties;
using MyPay.API.Models;
using MyPay.API.Models.Request.Insurance;
using MyPay.API.Models.Response.Insurance;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.Get.Insurance.Arhant;
using MyPay.Models.VendorAPI.Get.Insurance.ArhantLife;
using MyPay.Models.VendorAPI.Get.Insurance.Citizen;
using MyPay.Models.VendorAPI.Get.Insurance.Himalayan;
using MyPay.Models.VendorAPI.Get.Insurance.IME;
using MyPay.Models.VendorAPI.Get.Insurance.IMEGeneral;
using MyPay.Models.VendorAPI.Get.Insurance.Jyoti;
using MyPay.Models.VendorAPI.Get.Insurance.Mahalaxmi;
using MyPay.Models.VendorAPI.Get.Insurance.National;
using MyPay.Models.VendorAPI.Get.Insurance.Neco;
using MyPay.Models.VendorAPI.Get.Insurance.Nepal;
using MyPay.Models.VendorAPI.Get.Insurance.Prabhu;
using MyPay.Models.VendorAPI.Get.Insurance.Prime;
using MyPay.Models.VendorAPI.Get.Insurance.Prudential;
using MyPay.Models.VendorAPI.Get.Insurance.Reliable;
using MyPay.Models.VendorAPI.Get.Insurance.Reliance;
using MyPay.Models.VendorAPI.Get.Insurance.Sagarmatha;
using MyPay.Models.VendorAPI.Get.Insurance.Sanima;
using MyPay.Models.VendorAPI.Get.Insurance.Shikhar;
using MyPay.Models.VendorAPI.Get.Insurance.Surya;
using MyPay.Models.VendorAPI.Get.Insurance.Union;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Services.Description;

namespace MyPay.API.Controllers
{
    public class RequestAPI_InsuranceController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_InsuranceController));
        string ApiResponse = string.Empty;
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/insurance-neco-insurance")]
        public HttpResponseMessage GetInsurance_NacoInsurance(Req_Vendor_API_NecoInsurance_Lookup user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside insurance-neco-insurance" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_NecoInsurance_Lookup_Requests result = new Res_Vendor_API_NecoInsurance_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_NecoInsurance_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_NecoInsurance_Lookup objRes = new GetVendor_API_NecoInsurance_Lookup();
                        string msg = RepKhalti.RequestServiceGroup_NecoInsurance_Lookup(user.insurance_slug, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            //List<NecoInsurance_category_list> objData = new List<NecoInsurance_category_list>();
                            //for (int i = 0; i < objRes.category_list.Count; i++)
                            //{
                            //    NecoInsurance_category_list objDataItem = new NecoInsurance_category_list();
                            //    objDataItem.neco_insurance = objRes.category_list[i].neco_insurance;
                            //    objData.Add(objDataItem);
                            //}
                            //result.NecoInsurance_categories = objData;
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.Branches = objRes.branches;
                            result.PolicyCategories = objRes.policy_categories;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_NecoInsurance_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} insurance-neco-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} insurance-neco-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-neco")]
        public HttpResponseMessage GetServiceInsurance_Neco(Req_Vendor_API_Commit_Insurance_Neco_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-neco" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_Neco_Requests result = new Res_Vendor_API_Commit_Insurance_Neco_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Neco_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Neco;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_NecoInsurance_Commit objRes = new GetVendor_API_ServiceGroup_NecoInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_NECOINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.mobile_number, user.policy_category, user.policy_number, user.policy_type, user.amount, user.customer_name, user.insurance_slug, user.reference, user.service_name, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.CustomerName = user.customer_name;
                            outobject.InsuranceSlug = user.insurance_slug;
                            outobject.MobileNumber = user.mobile_number;
                            outobject.Paymode = user.PaymentMode;
                            outobject.PolicyCategory = user.policy_category;
                            outobject.PolicyNumber = user.policy_number;
                            outobject.PolicyType = user.policy_type;
                            outobject.Reference = user.reference;
                            outobject.ServiceName = user.service_name;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Neco_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            
                            try
                            {
                                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Neco;
                                var WalletTransactionId = TransactionID;
                                string jsonData = VendorApi_CommonHelper.Generate_json_Insurance_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, "Neco Insurance", outobject.ProductName,
                                    outobject.CustomerName, outobject.PolicyNumber, outobject.Amount.ToString(), "", "", result.Message, "0","0", "0", outobject.Amount.ToString());

                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), outobject.ServiceName.ToString(), "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Prime Life Insurance", outobject.Amount.ToString());
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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-neco completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-neco {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/insurance-sagarmatha-insurance")]
        public HttpResponseMessage GetInsurance_sagarmathaInsurance(Req_Vendor_API_SagarmathaInsurance_Lookup user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside insurance-sagarmatha-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_SagarmathaInsurance_Lookup_Requests result = new Res_Vendor_API_SagarmathaInsurance_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_SagarmathaInsurance_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_SagarmathaInsurance_Lookup objRes = new GetVendor_API_SagarmathaInsurance_Lookup();
                        string msg = RepKhalti.RequestServiceGroup_SagarmathaInsurance_Lookup(user.DebitNote, user.Reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.ContactNo = objRes.contact_no;
                            result.DebitNoteNo = objRes.debit_note_no;
                            result.SessionId = objRes.session_id;
                            result.Address = objRes.address;
                            result.Name = objRes.name;
                            result.PayableAmount = objRes.payable_amount;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_SagarmathaInsurance_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id.ToString();
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Salico Insurance", "", result.Name, result.DebitNoteNo, result.PayableAmount, "", "", result.Message, "0",
                                                         "0", "0", result.PayableAmount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} insurance-sagarmatha-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} insurance-sagarmatha-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-sagarmatha")]
        public HttpResponseMessage GetServiceInsurance_sagarmathaInsuranceCommit(Req_Vendor_API_Commit_Insurance_Sagarmatha_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-sagarmatha" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;

            Res_Vendor_API_Commit_Insurance_Sagarmatha_Requests result = new Res_Vendor_API_Commit_Insurance_Sagarmatha_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Sagarmatha_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_SagarmathaInsurance_Commit objRes = new GetVendor_API_ServiceGroup_SagarmathaInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_SAGARMATHINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.Paymode = user.PaymentMode;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Sagarmatha_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Salico Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-sagarmatha completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-sagarmatha {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-reliance-insurance")]
        public HttpResponseMessage GetInsurance_RelianceInsurance(Req_Vendor_API_RelianceInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-reliance-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;

            Res_Vendor_API_RelianceInsurance_Detail_Requests result = new Res_Vendor_API_RelianceInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_RelianceInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_RelianceInsurance_Detail objRes = new GetVendor_API_RelianceInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_RelianceInsurance_Detail(user.PolicyNo, user.dob, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.Paymode = objRes.paymode;
                            result.TransactionId = objRes.transaction_id;
                            result.ProductName = objRes.product_name;
                            result.CustomerId = objRes.customer_id;
                            result.NextDueDate = objRes.next_due_date;
                            result.status = objRes.status;
                            result.InvoiceNumber = objRes.invoice_number;
                            result.Amount = objRes.amount;
                            result.CustomerName = objRes.customer_name;
                            result.Address = objRes.address;
                            result.FineAmount = objRes.fine_amount;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_RelianceInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.transaction_id;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Reliance Life Insurance", result.ProductName, result.CustomerName, result.CustomerId, result.Amount, "", result.NextDueDate, result.Message, "0",
                                                         result.FineAmount, "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-reliance-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-reliance-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-reliance")]
        public HttpResponseMessage GetServiceInsurance_RelianceInsuranceCommit(Req_Vendor_API_Commit_Insurance_Reliance_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-reliance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_Reliance_Requests result = new Res_Vendor_API_Commit_Insurance_Reliance_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Reliance_Requests>(System.Net.HttpStatusCode.BadRequest, result);


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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_RelianceInsurance_Commit objRes = new GetVendor_API_ServiceGroup_RelianceInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_RELIANCEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.PolicyNo, user.TransactionId, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = objRes.detail.customer_id;
                            outobject.CustomerName = objRes.detail.customer_name;
                            outobject.FineAmount = Convert.ToDecimal(objRes.detail.fine_amount);
                            outobject.NextDueDate = objRes.detail.next_due_date;
                            outobject.Paymode = user.PaymentMode;
                            outobject.InvoiceNumber = objRes.detail.invoice_number;
                            outobject.PolicyNumber = user.PolicyNo;
                            outobject.Reference = user.Reference;
                            outobject.TransactionId = user.TransactionId;
                            outobject.ProductName = objRes.detail.product_name;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");


                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Reliance_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            string SessionId = user.TransactionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Sanima Reliance Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-reliance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-reliance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-jyotilife-insurance")]
        public HttpResponseMessage GetInsurance_JyotiLifeInsurance(Req_Vendor_API_JyotiLifeInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-jyotilife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_JyotiLifeInsurance_Detail_Requests result = new Res_Vendor_API_JyotiLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_JyotiLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_JyotiLifeInsurance_Detail objRes = new GetVendor_API_JyotiLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_JyotiLifeInsurance_Detail(user.PolicyNo, user.dob, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.PolicyNo = objRes.policy_no;
                            result.Name = objRes.name; //objRes.customer_name;
                            result.ProductName = objRes.product_name;
                            result.PremiumAmount = objRes.premium_amount;
                            result.FineAmount = objRes.fine_amount;
                            result.TotalFine = objRes.fine_amount;//objRes.total_fine;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.amount; //objRes.total_amount;
                            result.PaymentDate = objRes.payment_date;
                            result.DueDate = objRes.due_date;
                            result.NextDueDate = objRes.next_due_date;
                            result.PolicyStatus = objRes.policy_status;
                            result.Term = objRes.term;
                            result.MaturityDate = objRes.maturity_date;
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_JyotiLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Jyoti_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Surya Jyoti Life Insurance", result.ProductName, result.Name, result.PolicyNo, result.PremiumAmount, "", result.NextDueDate, result.Message, "0",
                                                         result.FineAmount, "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-jyotilife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-jyotilife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-jyotilife")]
        public HttpResponseMessage GetServiceInsurance_JyotiLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_JyotiLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-jyotilife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_JyotiLife_Requests result = new Res_Vendor_API_Commit_Insurance_JyotiLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_JyotiLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    // string md5hash = Common.CheckHash(UserInput);
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
                       // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Jyoti_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_JyotiLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_JyotiLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_JYOTILIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_JyotiLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Jyoti_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Surya Jyoti Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-jyotilife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-jyotilife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //PrimeLife Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-primelife-insurance")]
        public HttpResponseMessage GetInsurance_PrimeLifeInsurance(Req_Vendor_API_PrimeLifeInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-primelife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_PrimeLifeInsurance_Detail_Requests result = new Res_Vendor_API_PrimeLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_PrimeLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                   // string md5hash = Common.CheckHash(UserInput);
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

                        //string ProviderEnumName = Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), ServiceId).ToString().ToUpper().Replace("_PNG_", "_P&G_").Replace("KHALTI_", "").Replace("_", " ");

                        GetVendor_API_PrimeLifeInsurance_Detail objRes = new GetVendor_API_PrimeLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_PrimeLifeInsurance_Detail(user.PolicyNo, user.dob, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.PolicyNo = objRes.policy_no;
                            result.Name = objRes.customer_name;
                            result.PremiumAmount = objRes.premium_amount;
                            result.FineAmount = objRes.fine_amount;
                            result.TotalFine = objRes.fine_amount;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.total_amount;
                            result.TotalAmount = objRes.total_amount;
                            result.InstallmentNo = objRes.installment_no;
                            result.PaymentDate = objRes.payment_date;
                            result.DueDate = objRes.next_due_date;
                            result.NextDueDate = objRes.next_due_date;
                            result.CurrentDueDate = objRes.current_due_date;
                            result.PolicyStatus = objRes.policy_status;
                            result.Term = objRes.installment_no;
                            result.ProductName = objRes.product_name;
                            result.PayMode = objRes.pay_mode;
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_PrimeLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);
                            

                            string SessionId = objRes.session_id;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Himalayan_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();                            
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Prime Life Insurance", result.ProductName, result.Name, result.PolicyNo, result.PremiumAmount, result.InstallmentNo, result.DueDate, result.Message, "0",
                                                         result.FineAmount, "0", result.TotalAmount);



                            //var list = new List<KeyValuePair<String, String>>();
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Date", DateTime.Now.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Type", "Insurance Payment");
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Insurance Service", "Prime Life Insurance");
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Product Name Details", (result.ProductName == null ? "" : result.ProductName.ToString()));
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Policy Holder Name", result.Name);
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Policy Number", result.PolicyNo);
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Sum Assured Amount", result.PremiumAmount.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Premium Number", (result.InstallmentNo == null ? "" : result.InstallmentNo.ToString()));
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Next Premium Date", result.DueDate.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Status", result.Message.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Premium Amount", result.PremiumAmount.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Fine Amount", result.FineAmount.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Servcice Charge", "0");
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Paid(RED)", result.TotalAmount.ToString());
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Remarks", "Insurance premium of " + result.TotalAmount.ToString() + " is paid for policy number {" + result.PolicyNo + "} successfully.");
                            //string jsonData = VendorApi_CommonHelper.getJSONfromList(list);

                            //VendorApi_CommonHelper.saveRecieptsVendorResponse(ServiceId.ToString(), SessionId, objRes.unique_id_guid, jsonData, "", "", "", "", "");



                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-primelife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-primelife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-primelife")]
        public HttpResponseMessage GetServiceInsurance_PrimeLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_PrimeLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-primelife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_PrimeLife_Requests result = new Res_Vendor_API_Commit_Insurance_PrimeLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_PrimeLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                   // string md5hash = Common.CheckHash(UserInput);
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
                       // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Himalayan_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_PrimeLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_PrimeLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_PRIMELIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_PrimeLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Himalayan_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);                                
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Prime Life Insurance", user.Amount.ToString());

                            }
                            catch(Exception ex)
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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-primelife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-primelife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //PrabhuLife Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-prabhulife-insurance")]
        public HttpResponseMessage GetInsurance_PrabhuLifeInsurance(Req_Vendor_API_PrabhuLifeInsurance_Detail user)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



            log.Info($"{System.DateTime.Now.ToString()} inside lookup-prabhulife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_PrabhuLifeInsurance_Detail_Requests result = new Res_Vendor_API_PrabhuLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_PrabhuLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;
            log.Info("lookup-prabhulife-insurance TESTTTTTTTT" + userInput);

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

                        GetVendor_API_PrabhuLifeInsurance_Detail objRes = new GetVendor_API_PrabhuLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_PrabhuLifeInsurance_Detail(user.PolicyNo, user.dob, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.PolicyNo = objRes.policy_no;
                            result.Name = objRes.customer_name;
                            result.PremiumAmount = objRes.premium_amount;
                            result.FineAmount = objRes.fine_amount;
                            result.TotalFine = objRes.fine_amount;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.total_amount;
                            result.PaymentDate = objRes.payment_date;
                            result.DueDate = objRes.next_due_date;
                            result.NextDueDate = objRes.next_due_date;
                            result.CurrentDueDate = objRes.current_due_date;
                            result.PolicyStatus = objRes.policy_status;
                            result.Term = objRes.installment_no;
                            result.ProductName = objRes.product_name;
                            result.PayMode = objRes.pay_mode;
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_PrabhuLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Prabhu_Mahalaxmi_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Prabhu Life Insurance", result.ProductName, result.Name, result.PolicyNo, result.PremiumAmount, result.Term, result.DueDate, result.Message, "0",
                                                         result.FineAmount, "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-prabhulife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-prabhulife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-prabhulife")]
        public HttpResponseMessage GetServiceInsurance_PrabhuLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_PrabhuLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-prabhulife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_PrabhuLife_Requests result = new Res_Vendor_API_Commit_Insurance_PrabhuLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_PrabhuLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.CheckHash(UserInput);
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
                       // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Prabhu_Mahalaxmi_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_PrabhuLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_PrabhuLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_PRABHULIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_PrabhuLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Prabhu_Mahalaxmi_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Prabhu Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-prabhulife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-prabhulife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //NationalLife Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-nationallife-insurance")]
        public HttpResponseMessage GetInsurance_NationalLifeInsurance(Req_Vendor_API_NationalLifeInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-nationallife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_NationalLifeInsurance_Detail_Requests result = new Res_Vendor_API_NationalLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_NationalLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.CheckHash(UserInput);
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

                        GetVendor_API_NationalLifeInsurance_Detail objRes = new GetVendor_API_NationalLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_NationalLifeInsurance_Detail(user.PolicyNo, user.dob, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.PolicyNo = objRes.policy_no;
                            result.Name = objRes.customer_name;
                            result.PremiumAmount = objRes.premium_amount;
                            result.FineAmount = objRes.fine_amount;
                            result.TotalFine = objRes.fine_amount;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.total_amount;
                            result.PaymentDate = objRes.payment_date;
                            result.DueDate = objRes.next_due_date;
                            result.NextDueDate = objRes.next_due_date;
                            result.CurrentDueDate = objRes.current_due_date;
                            result.PolicyStatus = objRes.policy_status;
                            result.Term = objRes.installment_no;
                            result.ProductName = objRes.product_name;
                            result.PayMode = objRes.pay_mode;
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_NationalLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_National_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "National Life Insurance", result.ProductName, result.Name, result.PolicyNo, result.PremiumAmount, "", result.DueDate, result.Message, "0",
                                                         result.FineAmount, "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-nationallife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-nationallife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-nationallife")]
        public HttpResponseMessage GetServiceInsurance_NationalLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_NationalLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-nationallife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_NationalLife_Requests result = new Res_Vendor_API_Commit_Insurance_NationalLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_NationalLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.CheckHash(UserInput);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_National_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_NationalLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_NationalLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_NATIONALLIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_NationalLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_National_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "National Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-nationallife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-nationallife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //Himalayan General Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-himalayangeneral-insurance")]
        public HttpResponseMessage GetInsurance_HimalayanGeneralInsurance(Req_Vendor_API_HimalayanGeneralInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-himalayangeneral-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_HimalayanGeneralInsurance_Detail_Requests result = new Res_Vendor_API_HimalayanGeneralInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_HimalayanGeneralInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.CheckHash(UserInput);
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

                        GetVendor_API_HimalayanGeneralInsurance_Detail objRes = new GetVendor_API_HimalayanGeneralInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_HimalayanGeneralInsurance_Detail(user.PolicyNo, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.PolicyNo = objRes.policy_no;
                            result.Name = objRes.insured_party;
                            result.PremiumAmount = objRes.premium_amount;
                            result.FineAmount = objRes.fine_amount;
                            result.TotalFine = objRes.fine_amount;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.total_amount;
                            result.ExpiryDate = objRes.expiry_date;
                            result.InsuredAmount = objRes.insured_amount;
                            result.CoverageType = objRes.coverage_type;
                            result.Ids = objRes.ids;
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_HimalayanGeneralInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Himalayan_Everest;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Himalayan General Insurance", "", result.Name, result.PolicyNo, result.PremiumAmount, "", result.DueDate, result.Message, result.InsuredAmount,
                                                         result.FineAmount, "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-himalayangeneral-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-himalayangeneral-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-himalayangeneral")]
        public HttpResponseMessage GetServiceInsurance_HimalayanGeneralInsuranceCommit(Req_Vendor_API_Commit_Insurance_HimalayanGeneral_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-himalayangeneral" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_HimalayanGeneral_Requests result = new Res_Vendor_API_Commit_Insurance_HimalayanGeneral_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_HimalayanGeneral_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.CheckHash(UserInput);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Himalayan_Everest;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_HimalayanGeneralInsurance_Commit objRes = new GetVendor_API_ServiceGroup_HimalayanGeneralInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_HIMALAYANGENERALINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_HimalayanGeneral_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Himalayan_Everest;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Himalayan General Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-himalayangeneral completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-himalayangeneral {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //MahalaxmiLife Insurance
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/lookup-mahalaxmilife-insurance")]
        //public HttpResponseMessage GetInsurance_MahalaxmiLifeInsurance(Req_Vendor_API_MahalaxmiLifeInsurance_Detail user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside lookup-mahalaxmilife-insurance" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_MahalaxmiLifeInsurance_Detail_Requests result = new Res_Vendor_API_MahalaxmiLifeInsurance_Detail_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_MahalaxmiLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }

        //                GetVendor_API_MahalaxmiLifeInsurance_Detail objRes = new GetVendor_API_MahalaxmiLifeInsurance_Detail();
        //                string msg = RepKhalti.RequestServiceGroup_MahalaxmiLifeInsurance_Detail(user.PolicyNo, user.dob, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
        //                if (msg.ToLower() == "success")
        //                {
        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.PolicyNo = objRes.policy_no;
        //                    result.Name = objRes.customer_name;
        //                    result.PremiumAmount = objRes.premium_amount;
        //                    result.FineAmount = objRes.fine_amount;
        //                    result.TotalFine = objRes.fine_amount;
        //                    result.RebateAmount = objRes.rebate_amount;
        //                    result.Amount = objRes.amount;
        //                    result.CustomerId = objRes.customer_id;
        //                    result.TransactionId = objRes.transaction_id;
        //                    result.PayMode = objRes.pay_mode;
        //                    result.ProductName = objRes.product_name;
        //                    result.NextDueDate = objRes.next_due_date;
        //                    result.Token = objRes.token;
        //                    result.UniqueIdGuid = objRes.unique_id_guid;
        //                    result.SessionId = objRes.session_id;
        //                    result.status = objRes.status;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_MahalaxmiLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

        //                    string SessionId = objRes.session_id;
        //                    int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Mahalaxmi_Life;
        //                    var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
        //                    VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Mahalaxmi Life Insurance", result.ProductName, result.Name, result.CustomerId, result.PremiumAmount, "", result.NextDueDate, result.Message, "0",
        //                                                 result.FineAmount, "0", result.Amount);
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                }
        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} lookup-mahalaxmilife-insurance completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} lookup-mahalaxmilife-insurance {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/use-insurance-mahalaxmilife")]
        //public HttpResponseMessage GetServiceInsurance_MahalaxmiLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_MahalaxmiLife_Requests user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-mahalaxmilife" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_Commit_Insurance_Mahalaxmi_Requests result = new Res_Vendor_API_Commit_Insurance_Mahalaxmi_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Mahalaxmi_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                string UserInput = getRawPostData().Result;
        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
        //                if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
        //                {
        //                    Int64 memId = Convert.ToInt64(user.MemberId);
        //                    int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Mahalaxmi_Life;
        //                    int Type = 0;
        //                    resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
        //                    if (CommonResult.ToLower() != "success")
        //                    {
        //                        CommonResponse cres1 = new CommonResponse();
        //                        cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                        return response;
        //                    }
        //                }
        //                else
        //                {
        //                    CommonResponse cres1 = new CommonResponse();
        //                    cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }
        //                GetVendor_API_ServiceGroup_MahalaxmiLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_MahalaxmiLifeInsurance_Commit();
        //                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

        //                string authenticationToken = Request.Headers.Authorization.Parameter;

        //                Common.authenticationToken = authenticationToken;
        //                user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                bool IsCouponUnlocked = false; string TransactionID = string.Empty;
        //                string msg = RepKhalti.RequestServiceGroup_COMMIT_MAHALAXMILIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
        //                if (msg.ToLower() == "success")
        //                {
        //                    AddInsuranceDetail outobject = new AddInsuranceDetail();
        //                    outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
        //                    outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
        //                    outobject.Error = objRes.error;
        //                    outobject.InsuranceId = Convert.ToInt64(objRes.id);
        //                    outobject.Message = objRes.message;
        //                    outobject.Status = objRes.status.ToString();
        //                    outobject.TransactionUniqueId = objRes.UniqueTransactionId;
        //                    if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
        //                    {
        //                        outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
        //                    }
        //                    outobject.MemberId = Convert.ToInt64(user.MemberId);
        //                    outobject.Amount = Convert.ToDecimal(user.Amount);
        //                    outobject.CustomerId = user.UniqueCustomerId;
        //                    outobject.InsuranceSlug = user.InsuranceSlug;
        //                    outobject.Paymode = user.PaymentMode;
        //                    outobject.Reference = user.Reference;
        //                    //outobject.TransactionId = user.TransactionId;
        //                    outobject.IsActive = true;
        //                    outobject.IpAddress = Common.GetUserIP();
        //                    outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
        //                    outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
        //                    Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.status = objRes.status;
        //                    result.Id = objRes.id;
        //                    result.CreditsAvailable = objRes.credits_available;
        //                    result.CreditsConsumed = objRes.credits_consumed;
        //                    result.WalletBalance = objRes.WalletBalance;
        //                    result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
        //                    result.Detail = objRes.Detail;
        //                    result.TransactionUniqueId = TransactionID;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Mahalaxmi_Requests>(System.Net.HttpStatusCode.OK, result);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


        //                    string SessionId = user.SessionId;
        //                    int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Mahalaxmi_Life;
        //                    var WalletTransactionId = TransactionID;

        //                    try
        //                    {
        //                        string jsonData = "";
        //                        ReceiptsVendorResponse objreceiptsVendorResponse = null;
        //                        objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
        //                        jsonData = objreceiptsVendorResponse.ReqJSONContent;
        //                        VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Mahalaxmi Life Insurance", user.Amount.ToString());

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
        //        log.Info($"{System.DateTime.Now.ToString()} use-insurance-mahalaxmilife completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} use-insurance-mahalaxmilife {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //IMELife Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-IMElife-insurance")]
        public HttpResponseMessage GetInsurance_IMELifeInsurance(Req_Vendor_API_ArhantLifeInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-IMElife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_ArhantLifeInsurance_Detail_Requests result = new Res_Vendor_API_ArhantLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ArhantLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_ArhantLifeInsurance_Detail objRes = new GetVendor_API_ArhantLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_IMELifeInsurance_Detail(user.PolicyNo, /*user.dob,*/ user.dob_year, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;

                            var test = JsonConvert.DeserializeObject<Datas>(JsonConvert.SerializeObject(objRes.properties));
                           
                            result.properties = test;
                            result.request_id = objRes.request_id;
                          /*  result.Name = objRes.properties.customer_name;
                            result.PremiumAmount = objRes.properties.product_name;
                            result.FineAmount = objRes.fine_amount;
                            result.TotalFine = objRes.fine_amount;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.amount;
                            result.DueDate = objRes.due_date;
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;*/
                            result.amount = objRes.amount;
                            result.session_id = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ArhantLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id.ToString();
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_IME_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                           VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "IME Life Insurance", "", "", "", "", "", "", "", "0",
                                                         "", "0", "");
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-IMElife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-IMElife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-IMElife")]
        public HttpResponseMessage GetServiceInsurance_IMELifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_ArhantLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-IMElife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_ArhantLife_Requests result = new Res_Vendor_API_Commit_Insurance_ArhantLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_ArhantLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.CheckHash(UserInput);
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
                      //  string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_IME_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_ArhantLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ArhantLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_IMELIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId,/* user.InsuranceSlug,*/ user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                       if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                           // outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            //outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.State = objRes.state;
                            result.Data = objRes.extra_data;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_ArhantLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_IME_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "IME Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-IMElife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-IMElife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //AsianLife Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-Asianlife-insurance")]
        public HttpResponseMessage GetInsurance_AsianLifeInsurance(Req_Vendor_API_ArhantLifeInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-Asianlife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_ArhantLifeInsurance_Detail_Requests result = new Res_Vendor_API_ArhantLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ArhantLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_ArhantLifeInsurance_Detail objRes = new GetVendor_API_ArhantLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_AsianLifeInsurance_Detail(user.PolicyNo, /*user.dob,*/ user.dob_year, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;

                            var test = JsonConvert.DeserializeObject<Datas>(JsonConvert.SerializeObject(objRes.properties));

                            result.properties = test;
                            result.request_id = objRes.request_id;
                            /*  result.Name = objRes.properties.customer_name;
                              result.PremiumAmount = objRes.properties.product_name;
                              result.FineAmount = objRes.fine_amount;
                              result.TotalFine = objRes.fine_amount;
                              result.RebateAmount = objRes.rebate_amount;
                              result.Amount = objRes.amount;
                              result.DueDate = objRes.due_date;
                              result.Token = objRes.token;
                              result.UniqueIdGuid = objRes.unique_id_guid;*/
                            result.amount = objRes.amount;
                            result.session_id = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ArhantLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id.ToString();
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_ASIAN_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "IME Life Insurance", "", "", "", "", "", "", "", "0",
                                                          "", "0", "");
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-ASIANlife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-ASIANlife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-Asianlife")]
        public HttpResponseMessage GetServiceInsurance_AsianLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_ArhantLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-Asianlife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_ArhantLife_Requests result = new Res_Vendor_API_Commit_Insurance_ArhantLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_ArhantLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.CheckHash(UserInput);
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
                        //  string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_IME_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_ArhantLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ArhantLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_AsianLIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId,/* user.InsuranceSlug,*/ user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            // outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            //outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.State = objRes.state;
                            result.Data = objRes.extra_data;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_ArhantLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_ASIAN_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "IME Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-IMElife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-IMElife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        //CitizenLife Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-citizenlife-insurance")]
        public HttpResponseMessage GetInsurance_CitizenLifeInsurance(Req_Vendor_API_CitizenLifeInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-citizenlife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_CitizenLifeInsurance_Detail_Requests result = new Res_Vendor_API_CitizenLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_CitizenLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_CitizenLifeInsurance_Detail objRes = new GetVendor_API_CitizenLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_CitizenLifeInsurance_Detail(user.PolicyNo, user.dob, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.PolicyNo = objRes.policy_no;
                            result.Name = objRes.customer_name;
                            result.PremiumAmount = objRes.premium_amount;
                            result.FineAmount = objRes.fine_amount;
                            result.TotalFine = objRes.fine_amount;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.total_amount;
                            result.PaymentDate = objRes.payment_date;
                            result.DueDate = objRes.next_due_date;
                            result.NextDueDate = objRes.next_due_date;
                            result.CurrentDueDate = objRes.current_due_date;
                            result.PolicyStatus = objRes.policy_status;
                            result.Term = objRes.installment_no;
                            result.ProductName = objRes.product_name;
                            result.PayMode = objRes.pay_mode;
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_CitizenLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Citizen_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Citizen Life Insurance", result.ProductName, result.Name, result.PolicyNo, result.PremiumAmount, "", result.DueDate, result.Message, "0",
                                                         result.FineAmount, "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-citizenlife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-citizenlife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-citizenlife")]
        public HttpResponseMessage GetServiceInsurance_CitizenLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_CitizenLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-citizenlife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_CitizenLife_Requests result = new Res_Vendor_API_Commit_Insurance_CitizenLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_CitizenLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Citizen_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_CitizenLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_CitizenLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_CITIZENLIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_CitizenLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Citizen_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Citizen Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-citizenlife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-citizenlife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //ReliableLife Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-reliablelife-insurance")]
        public HttpResponseMessage GetInsurance_ReliableLifeInsurance(Req_Vendor_API_ReliableLifeInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-reliablelife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_ReliableLifeInsurance_Detail_Requests result = new Res_Vendor_API_ReliableLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ReliableLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_ReliableLifeInsurance_Detail objRes = new GetVendor_API_ReliableLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_ReliableLifeInsurance_Detail(user.PolicyNo, user.dob, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.PolicyNo = objRes.policy_no;
                            result.Name = objRes.customer_name;
                            result.PremiumAmount = objRes.premium_amount;
                            result.FineAmount = objRes.fine_amount;
                            result.TotalFine = objRes.fine_amount;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.total_amount;
                            result.PaymentDate = objRes.payment_date;
                            result.DueDate = objRes.next_due_date;
                            result.NextDueDate = objRes.next_due_date;
                            result.CurrentDueDate = objRes.current_due_date;
                            result.PolicyStatus = objRes.policy_status;
                            result.Term = objRes.installment_no;
                            result.ProductName = objRes.product_name;
                            result.PayMode = objRes.pay_mode;
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ReliableLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);
                            
                            
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Reliable_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            string SessionId = objRes.session_id;
                            
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Reliable Life Insurance", result.ProductName, result.Name, result.PolicyNo, result.PremiumAmount, result.Term, result.DueDate, result.Message, "0",
                                                         result.FineAmount, "0", result.Amount);


                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-reliablelife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-reliablelife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-reliablelife")]
        public HttpResponseMessage GetServiceInsurance_ReliableLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_ReliableLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-reliablelife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_ReliableLife_Requests result = new Res_Vendor_API_Commit_Insurance_ReliableLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_ReliableLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                       // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Reliable_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_ReliableLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ReliableLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_RELIABLELIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_ReliableLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Reliable_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Reliable Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-reliablelife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-reliablelife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //UnionLife Insurance
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/lookup-unionlife-insurance")]
        //public HttpResponseMessage GetInsurance_UnionLifeInsurance(Req_Vendor_API_UnionLifeInsurance_Detail user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside lookup-unionlife-insurance" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_UnionLifeInsurance_Detail_Requests result = new Res_Vendor_API_UnionLifeInsurance_Detail_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_UnionLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }

        //                GetVendor_API_UnionLifeInsurance_Detail objRes = new GetVendor_API_UnionLifeInsurance_Detail();
        //                string msg = RepKhalti.RequestServiceGroup_UnionLifeInsurance_Detail(user.PolicyNo, user.dob, user.insuranceslug, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
        //                if (msg.ToLower() == "success")
        //                {
        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.PolicyNo = objRes.policy_no;
        //                    result.PlanCode = objRes.plan_code;
        //                    result.Name = objRes.name;
        //                    result.PremiumAmount = objRes.premium_amount;
        //                    result.FineAmount = objRes.fine_amount;
        //                    result.AdjustmentAmount = objRes.adjustment_amount;
        //                    result.RebateAmount = objRes.rebate_amount;
        //                    result.Amount = objRes.amount;
        //                    result.PaymentDate = objRes.payment_date;
        //                    result.DueDate = objRes.due_date;
        //                    result.NextDueDate = objRes.next_due_date;
        //                    result.MaturityDate = objRes.maturity_date;
        //                    result.PolicyStatus = objRes.policy_status;
        //                    result.Term = objRes.term;
        //                    result.PayMode = objRes.pay_mode;
        //                    result.Token = objRes.token;
        //                    result.UniqueIdGuid = objRes.unique_id_guid;
        //                    result.SessionId = objRes.session_id;
        //                    result.status = objRes.status;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_UnionLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

        //                    string SessionId = objRes.session_id;
        //                    int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Union_Life;
        //                    var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
        //                    VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Union Life Insurance", result.ProductName, result.Name, result.PolicyNo, result.PremiumAmount, "", result.DueDate, result.Message, "0",
        //                                                 result.FineAmount, "0", result.Amount);
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                }
        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} lookup-unionlife-insurance completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} lookup-unionlife-insurance {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/use-insurance-unionlife")]
        //public HttpResponseMessage GetServiceInsurance_UnionLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_UnionLife_Requests user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-unionlife" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_Commit_Insurance_UnionLife_Requests result = new Res_Vendor_API_Commit_Insurance_UnionLife_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_UnionLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                string UserInput = getRawPostData().Result;
        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
        //                if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
        //                {
        //                    Int64 memId = Convert.ToInt64(user.MemberId);
        //                    int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Union_Life;
        //                    int Type = 0;
        //                    resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
        //                    if (CommonResult.ToLower() != "success")
        //                    {
        //                        CommonResponse cres1 = new CommonResponse();
        //                        cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                        return response;
        //                    }
        //                }
        //                else
        //                {
        //                    CommonResponse cres1 = new CommonResponse();
        //                    cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }
        //                GetVendor_API_ServiceGroup_UnionLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_UnionLifeInsurance_Commit();
        //                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

        //                string authenticationToken = Request.Headers.Authorization.Parameter;

        //                Common.authenticationToken = authenticationToken;
        //                user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                bool IsCouponUnlocked = false; string TransactionID = string.Empty;
        //                string msg = RepKhalti.RequestServiceGroup_COMMIT_UNIONLIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
        //                if (msg.ToLower() == "success")
        //                {
        //                    AddInsuranceDetail outobject = new AddInsuranceDetail();
        //                    outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
        //                    outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
        //                    outobject.Error = objRes.error;
        //                    outobject.InsuranceId = Convert.ToInt64(objRes.id);
        //                    outobject.Message = objRes.message;
        //                    outobject.Status = objRes.status.ToString();
        //                    outobject.TransactionUniqueId = objRes.UniqueTransactionId;
        //                    if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
        //                    {
        //                        outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
        //                    }
        //                    outobject.MemberId = Convert.ToInt64(user.MemberId);
        //                    outobject.Amount = Convert.ToDecimal(user.Amount);
        //                    outobject.CustomerId = user.UniqueCustomerId;
        //                    outobject.InsuranceSlug = user.InsuranceSlug;
        //                    outobject.Paymode = user.PaymentMode;
        //                    outobject.Reference = user.Reference;
        //                    //outobject.TransactionId = user.TransactionId;
        //                    outobject.IsActive = true;
        //                    outobject.IpAddress = Common.GetUserIP();
        //                    outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
        //                    outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
        //                    Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.status = objRes.status;
        //                    result.Id = objRes.id;
        //                    result.CreditsAvailable = objRes.credits_available;
        //                    result.CreditsConsumed = objRes.credits_consumed;
        //                    result.WalletBalance = objRes.WalletBalance;
        //                    result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
        //                    result.Detail = objRes.Detail;
        //                    result.TransactionUniqueId = TransactionID;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_UnionLife_Requests>(System.Net.HttpStatusCode.OK, result);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


        //                    string SessionId = user.SessionId;
        //                    int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Union_Life;
        //                    var WalletTransactionId = TransactionID;

        //                    try
        //                    {
        //                        string jsonData = "";
        //                        ReceiptsVendorResponse objreceiptsVendorResponse = null;
        //                        objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
        //                        jsonData = objreceiptsVendorResponse.ReqJSONContent;
        //                        VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Union Life Insurance", user.Amount.ToString());

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
        //        log.Info($"{System.DateTime.Now.ToString()} use-insurance-unionlife completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} use-insurance-unionlife {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}


        //IMEGeneral Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-ime-general-insurance")]
        public HttpResponseMessage GetInsurance_IMEGeneralInsurance(Req_Vendor_API_IMEGeneralInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-ime-general-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_IMEGeneralInsurance_Detail_Requests result = new Res_Vendor_API_IMEGeneralInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_IMEGeneralInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                        GetVendor_API_IMEGeneralInsurance_Detail objRes = new GetVendor_API_IMEGeneralInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_IMEGeneralInsurance_Detail(user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            //result.PolicyNo = objRes.policy_no;
                            
                            result.policy_type = objRes.policy_type;
                            result.insurance_type = objRes.insurance_type;
                            result.branches = objRes.branches;
                            
                            result.Token = objRes.token;
                            result.UniqueIdGuid = objRes.unique_id_guid;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_IMEGeneralInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-ime-general-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-ime-general-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-ime-general")]
        public HttpResponseMessage GetServiceInsurance_IMEGeneralInsuranceCommit(Req_Vendor_API_Commit_Insurance_IMEGeneral_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-ime-general" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_IMEGeneral_Requests result = new Res_Vendor_API_Commit_Insurance_IMEGeneral_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_IMEGeneral_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                      //  string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_IGI_Prudential;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_IMEGeneralInsurance_Commit objRes = new GetVendor_API_ServiceGroup_IMEGeneralInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_IMEGENERALINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.PolicyType, user.InsuranceType, user.Branch, user.FullName, user.Address, user.MobileNumber, user.PolicyDescription, user.DebitNoteNo, user.BillNo, user.Email, user.MemberId, user.Amount, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.InsuranceSlug = user.InsuranceSlug;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.Detail;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_IMEGeneral_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            try
                            {
                                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_IGI_Prudential;
                                var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                                string jsonData = VendorApi_CommonHelper.Generate_json_Insurance_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, "IME General Insurance", user.InsuranceType,
                                    user.FullName, user.DebitNoteNo, user.Amount.ToString(), "", "", result.Message, "0", "0", "0", user.Amount.ToString());

                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), outobject.ServiceName.ToString(), "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "IME General Insurance", outobject.Amount.ToString());
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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-ime-general completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-ime-general {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        //Prudential Insurance
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/lookup-prudential-insurance")]
        //public HttpResponseMessage GetInsurance_PrudentialInsurance(Req_Vendor_API_PrudentialInsurance_Detail user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside lookup-prudential-insurance" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_PrudentialInsurance_Detail_Requests result = new Res_Vendor_API_PrudentialInsurance_Detail_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_PrudentialInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }

        //                GetVendor_API_PrudentialInsurance_Detail objRes = new GetVendor_API_PrudentialInsurance_Detail();
        //                string msg = RepKhalti.RequestServiceGroup_PrudentialInsurance_Detail(user.Version, user.DeviceCode, user.PlatForm, ref objRes);
        //                if (msg.ToLower() == "success")
        //                {
        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.detail = objRes.detail;
        //                    result.Token = objRes.token;
        //                    result.UniqueIdGuid = objRes.unique_id_guid;
        //                    result.SessionId = objRes.session_id;
        //                    result.status = objRes.status;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_PrudentialInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                }
        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} lookup-prudential-insurance completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} lookup-prudential-insurance {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/use-insurance-prudential")]
        //public HttpResponseMessage GetServiceInsurance_PrudentialInsuranceCommit(Req_Vendor_API_Commit_Insurance_Prudential_Requests user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-prudential" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_Commit_Insurance_Prudential_Requests result = new Res_Vendor_API_Commit_Insurance_Prudential_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Prudential_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                string UserInput = getRawPostData().Result;
        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
        //                if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
        //                {
        //                    Int64 memId = Convert.ToInt64(user.MemberId);
        //                    int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Prudential_Insurance;
        //                    int Type = 0;
        //                    resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
        //                    if (CommonResult.ToLower() != "success")
        //                    {
        //                        CommonResponse cres1 = new CommonResponse();
        //                        cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                        return response;
        //                    }
        //                }
        //                else
        //                {
        //                    CommonResponse cres1 = new CommonResponse();
        //                    cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }
        //                GetVendor_API_ServiceGroup_PrudentialInsurance_Commit objRes = new GetVendor_API_ServiceGroup_PrudentialInsurance_Commit();
        //                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

        //                string authenticationToken = Request.Headers.Authorization.Parameter;

        //                Common.authenticationToken = authenticationToken;
        //                user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                bool IsCouponUnlocked = false; string TransactionID = string.Empty;
        //                string msg = RepKhalti.RequestServiceGroup_COMMIT_PRUDENTIALINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.Branch, user.FullName, user.MobileNumber, user.DebitNoteNo, user.Email, user.MemberId, user.Amount, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
        //                if (msg.ToLower() == "success")
        //                {
        //                    AddInsuranceDetail outobject = new AddInsuranceDetail();
        //                    outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
        //                    outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
        //                    outobject.Error = objRes.error;
        //                    outobject.InsuranceId = Convert.ToInt64(objRes.id);
        //                    outobject.Message = objRes.message;
        //                    outobject.Status = objRes.status.ToString();
        //                    outobject.TransactionUniqueId = objRes.UniqueTransactionId;
        //                    if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
        //                    {
        //                        outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
        //                    }
        //                    outobject.MemberId = Convert.ToInt64(user.MemberId);
        //                    outobject.Amount = Convert.ToDecimal(user.Amount);
        //                    outobject.CustomerId = user.UniqueCustomerId;
        //                    outobject.InsuranceSlug = user.InsuranceSlug;
        //                    outobject.Paymode = user.PaymentMode;
        //                    outobject.Reference = user.Reference;
        //                    //outobject.TransactionId = user.TransactionId;
        //                    outobject.IsActive = true;
        //                    outobject.IpAddress = Common.GetUserIP();
        //                    outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
        //                    outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
        //                    Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.status = objRes.status;
        //                    result.Id = objRes.id;
        //                    result.CreditsAvailable = objRes.credits_available;
        //                    result.CreditsConsumed = objRes.credits_consumed;
        //                    result.WalletBalance = objRes.WalletBalance;
        //                    result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
        //                    result.Detail = objRes.Detail;
        //                    result.TransactionUniqueId = TransactionID;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Prudential_Requests>(System.Net.HttpStatusCode.OK, result);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


        //                    try
        //                    {
        //                        int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Prudential_Insurance;
        //                        var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
        //                        string jsonData = VendorApi_CommonHelper.Generate_json_Insurance_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, "Prudential Insurance", outobject.ProductName,
        //                            user.FullName, user.DebitNoteNo, user.Amount.ToString(), "", "", result.Message, "0", "0", "0", user.Amount.ToString());

        //                        VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), outobject.ServiceName.ToString(), "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Prudential Insurance", outobject.Amount.ToString());
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
        //        log.Info($"{System.DateTime.Now.ToString()} use-insurance-prudential completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} use-insurance-prudential {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}


        //NepalLife Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-nepallife-insurance")]
        public HttpResponseMessage GetInsurance_NepalLifeInsurance(Req_Vendor_API_NepalLifeInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-nepallife-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_NepalLifeInsurance_Detail_Requests result = new Res_Vendor_API_NepalLifeInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_NepalLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        GetVendor_API_NepalLifeInsurance_Detail objRes = new GetVendor_API_NepalLifeInsurance_Detail();
                        string msg = RepKhalti.RequestServiceGroup_NepalLifeInsurance_Detail(user.PolicyNo, user.dob, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.PolicyNo = objRes.policy_no;
                            result.CustomerName = objRes.customer_name;
                            result.PremiumAmount = objRes.premium_amount;
                            result.FineAmount = objRes.fine_amount;
                            result.DueDate = objRes.due_date;
                            result.RebateAmount = objRes.rebate_amount;
                            result.Amount = objRes.amount;
                            result.SessionId = objRes.session_id;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_NepalLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);

                            string SessionId = objRes.session_id;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Nepal_Life;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Nepal Life Insurance", "", result.CustomerName, result.PolicyNo, result.PremiumAmount, "", result.DueDate, result.Message, "0",
                                                         result.FineAmount, "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-nepallife-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-nepallife-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-nepallife")]
        public HttpResponseMessage GetServiceInsurance_NepalLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_NepalLife_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-nepallife" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_NepalLife_Requests result = new Res_Vendor_API_Commit_Insurance_NepalLife_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_NepalLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                       // string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Nepal_Life;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_NepalLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_NepalLifeInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_NEPALLIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            outobject.PolicyNumber = objRes.extra_data.policy_no;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.CreditsAvailable = objRes.credits_available;
                            result.CreditsConsumed = objRes.credits_consumed;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.detail;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.TransactionUniqueId = TransactionID;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_NepalLife_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.SessionId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Nepal_Life;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Nepal Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-nepallife completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-nepallife {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
       

        //SuryaLife Insurance
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/lookup-suryalife-insurance")]
        //public HttpResponseMessage GetInsurance_SuryaLifeInsurance(Req_Vendor_API_SuryaLifeInsurance_Detail user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside lookup-suryalife-insurance" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_SuryaLifeInsurance_Detail_Requests result = new Res_Vendor_API_SuryaLifeInsurance_Detail_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_SuryaLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }
        //                GetVendor_API_SuryaLifeInsurance_Detail objRes = new GetVendor_API_SuryaLifeInsurance_Detail();
        //                user.reference = new CommonHelpers().GenerateUniqueId();
        //                string msg = RepKhalti.RequestServiceGroup_SuryaLifeInsurance_Detail(user.PolicyNo, user.dob, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
        //                if (msg.ToLower() == "success")
        //                {
        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.PolicyNo = objRes.policy_no;
        //                    result.PlanCode = objRes.plan_code;
        //                    result.PayMode = objRes.pay_mode;
        //                    result.Name = objRes.name;
        //                    result.PremiumAmount = objRes.premium_amount;
        //                    result.FineAmount = objRes.fine_amount;
        //                    result.AdjustmentAmount = objRes.adjustment_amount;
        //                    result.PaymentDate = objRes.payment_date;
        //                    result.DueDate = objRes.due_date;
        //                    result.Amount = objRes.amount;
        //                    result.NextDueDate = objRes.next_due_date;
        //                    result.PolicyStatus = objRes.policy_status;
        //                    result.Term = objRes.term;
        //                    result.MaturityDate = objRes.maturity_date;
        //                    result.SessionId = objRes.session_id;
        //                    result.status = objRes.status;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_SuryaLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                }
        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} lookup-suryalife-insurance completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} lookup-suryalife-insurance {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/use-insurance-suryalife")]
        //public HttpResponseMessage GetServiceInsurance_SuryaLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_SuryaLife_Requests user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-suryalife" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_Commit_Insurance_SuryaLife_Requests result = new Res_Vendor_API_Commit_Insurance_SuryaLife_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_SuryaLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                string UserInput = getRawPostData().Result;
        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
        //                if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
        //                {
        //                    Int64 memId = Convert.ToInt64(user.MemberId);
        //                    int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Life;
        //                    int Type = 0;
        //                    resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.amount, true, user.Mpin);
        //                    if (CommonResult.ToLower() != "success")
        //                    {
        //                        CommonResponse cres1 = new CommonResponse();
        //                        cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                        return response;
        //                    }
        //                }
        //                else
        //                {
        //                    CommonResponse cres1 = new CommonResponse();
        //                    cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }
        //                GetVendor_API_ServiceGroup_SuryaLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_SuryaLifeInsurance_Commit();
        //                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

        //                string authenticationToken = Request.Headers.Authorization.Parameter;

        //                Common.authenticationToken = authenticationToken;
        //                user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                bool IsCouponUnlocked = false; string TransactionID = string.Empty;
        //                string msg = RepKhalti.RequestServiceGroup_COMMIT_SURYALIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.amount, user.session_id, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
        //                if (msg.ToLower() == "success")
        //                {
        //                    AddInsuranceDetail outobject = new AddInsuranceDetail();
        //                    outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
        //                    outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
        //                    outobject.Error = objRes.error;
        //                    outobject.InsuranceId = Convert.ToInt64(objRes.id);
        //                    outobject.Message = objRes.message;
        //                    outobject.Status = objRes.status.ToString();
        //                    outobject.TransactionUniqueId = objRes.UniqueTransactionId;
        //                    if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
        //                    {
        //                        outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
        //                    }
        //                    outobject.MemberId = Convert.ToInt64(user.MemberId);
        //                    //outobject.Amount = Convert.ToDecimal(user.Amount);
        //                    outobject.CustomerId = user.UniqueCustomerId;
        //                    outobject.Paymode = user.PaymentMode;
        //                    outobject.Reference = user.Reference;
        //                    outobject.PolicyNumber = objRes.extra_data.policy_no;
        //                    outobject.CustomerName = objRes.extra_data.customer_name;
        //                    outobject.AdjustmentAmount = Convert.ToDecimal(objRes.extra_data.adjustment_amount);
        //                    outobject.FineAmount = Convert.ToDecimal(objRes.extra_data.fine_amount);
        //                    outobject.NextDueDate = objRes.extra_data.next_due_date;
        //                    outobject.PaymentDate = objRes.extra_data.payment_date;
        //                    outobject.TpPremium = Convert.ToDecimal(objRes.extra_data.premium_amount);
        //                    outobject.Amount = Convert.ToDecimal(objRes.extra_data.total_amount);
        //                    //outobject.TransactionId = user.TransactionId;
        //                    outobject.IsActive = true;
        //                    outobject.IpAddress = Common.GetUserIP();
        //                    outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
        //                    outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);

        //                    Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.status = objRes.status;
        //                    result.Id = objRes.id;
        //                    result.CreditsAvailable = objRes.credits_available;
        //                    result.CreditsConsumed = objRes.credits_consumed;
        //                    result.WalletBalance = objRes.WalletBalance;
        //                    result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
        //                    result.Detail = objRes.detail;
        //                    result.TransactionUniqueId = TransactionID;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_SuryaLife_Requests>(System.Net.HttpStatusCode.OK, result);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
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
        //        log.Info($"{System.DateTime.Now.ToString()} use-insurance-suryalife completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} use-insurance-suryalife {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //SanimaLife Insurance

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/lookup-sanimalife-insurance")]
        //public HttpResponseMessage GetInsurance_SanimaLifeInsurance(Req_Vendor_API_SanimaLifeInsurance_Detail user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside lookup-sanimalife-insurance" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_SanimaLifeInsurance_Detail_Requests result = new Res_Vendor_API_SanimaLifeInsurance_Detail_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_SanimaLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }
        //                GetVendor_API_SanimaLifeInsurance_Detail objRes = new GetVendor_API_SanimaLifeInsurance_Detail();
        //                user.reference = new CommonHelpers().GenerateUniqueId();
        //                string msg = RepKhalti.RequestServiceGroup_SanimaLifeInsurance_Detail(user.policy_number, user.date_of_birth, user.reference, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
        //                if (msg.ToLower() == "success")
        //                {
        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.SessionId = objRes.session_id;
        //                    result.Detail.AssuredName = objRes.detail.assured_name;
        //                    result.Detail.PaymentMode = objRes.detail.payment_mode;
        //                    result.Detail.PremiumAmount = objRes.detail.premium_amount;
        //                    result.Detail.ProductName = objRes.detail.product_name;
        //                    result.status = objRes.status;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_SanimaLifeInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);
        //                }
        //                else
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                }
        //            }
        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} lookup-sanimalife-insurance completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} lookup-sanimalife-insurance {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/use-insurance-sanimalife")]
        //public HttpResponseMessage GetServiceInsurance_SanimaLifeInsuranceCommit(Req_Vendor_API_Commit_Insurance_SanimaLife_Requests user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-sanimalife" + Environment.NewLine);
        //    CommonResponse cres = new CommonResponse();
        //    Res_Vendor_API_Commit_Insurance_SanimaLife_Requests result = new Res_Vendor_API_Commit_Insurance_SanimaLife_Requests();
        //    var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_SanimaLife_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
        //            string md5hash = Common.CheckHash(user);

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
        //                string UserInput = getRawPostData().Result;
        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
        //                if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
        //                {
        //                    Int64 memId = Convert.ToInt64(user.MemberId);
        //                    int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Life;
        //                    int Type = 0;
        //                    resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
        //                    if (CommonResult.ToLower() != "success")
        //                    {
        //                        CommonResponse cres1 = new CommonResponse();
        //                        cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                        return response;
        //                    }
        //                }
        //                else
        //                {
        //                    CommonResponse cres1 = new CommonResponse();
        //                    cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
        //                    return response;
        //                }
        //                GetVendor_API_ServiceGroup_SanimaLifeInsurance_Commit objRes = new GetVendor_API_ServiceGroup_SanimaLifeInsurance_Commit();
        //                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

        //                string authenticationToken = Request.Headers.Authorization.Parameter;

        //                Common.authenticationToken = authenticationToken;
        //                user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                bool IsCouponUnlocked = false; string TransactionID = string.Empty;
        //                string msg = RepKhalti.RequestServiceGroup_COMMIT_SANIMALIFEINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.SessionId, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
        //                if (msg.ToLower() == "success")
        //                {
        //                    AddInsuranceDetail outobject = new AddInsuranceDetail();
        //                    outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
        //                    outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
        //                    outobject.Error = objRes.error;
        //                    outobject.InsuranceId = Convert.ToInt64(objRes.id);
        //                    outobject.Message = objRes.message;
        //                    outobject.Status = objRes.status.ToString();
        //                    outobject.TransactionUniqueId = objRes.UniqueTransactionId;
        //                    if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
        //                    {
        //                        outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
        //                    }
        //                    outobject.MemberId = Convert.ToInt64(user.MemberId);
        //                    outobject.Amount = Convert.ToDecimal(user.Amount);
        //                    outobject.CustomerId = user.UniqueCustomerId;
        //                    outobject.Paymode = user.PaymentMode;
        //                    outobject.Reference = user.Reference;
        //                    outobject.TransactionId = objRes.extra_data.transaction_id;
        //                    //outobject.TransactionId = user.TransactionId;
        //                    outobject.IsActive = true;
        //                    outobject.IpAddress = Common.GetUserIP();
        //                    outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
        //                    outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);

        //                    Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

        //                    result.ReponseCode = objRes.status ? 1 : 0;
        //                    result.status = objRes.status;
        //                    result.Id = objRes.id;
        //                    result.WalletBalance = objRes.WalletBalance;
        //                    result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
        //                    result.Detail = objRes.detail;
        //                    result.Message = "Success";
        //                    result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
        //                    result.TransactionUniqueId = TransactionID;
        //                    result._ExtraData.TransactionId = objRes.extra_data.transaction_id;
        //                    response.StatusCode = HttpStatusCode.Accepted;
        //                    response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_SanimaLife_Requests>(System.Net.HttpStatusCode.OK, result);
        //                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
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
        //        log.Info($"{System.DateTime.Now.ToString()} use-insurance-sanimalife completed" + Environment.NewLine);
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
        //        log.Error($"{System.DateTime.Now.ToString()} use-insurance-sanimalife {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //Shikhar Insurance

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-shikhar-insurance")]
        public HttpResponseMessage GetInsurance_ShikharInsurance(Req_Vendor_API_ShikharInsurance_GetPackages user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-shikhar-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_ShikharInsurance_GetPackages_Requests result = new Res_Vendor_API_ShikharInsurance_GetPackages_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ShikharInsurance_GetPackages_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        GetVendor_API_ShikharInsurance_GetPackages objRes = new GetVendor_API_ShikharInsurance_GetPackages();

                        string msg = RepKhalti.RequestServiceGroup_ShikharInsurance_GetPackages(user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<Models.Response.Insurance.ShikharPolicies> objPolicies = new List<Models.Response.Insurance.ShikharPolicies>();
                            result.ReponseCode = objRes.status ? 1 : 0;
                            for (int i = 0; i < objRes.detail.policies.Count; i++)
                            {
                                Models.Response.Insurance.ShikharPolicies objChildShikharPolicies = new Models.Response.Insurance.ShikharPolicies();
                                objChildShikharPolicies.label = objRes.detail.policies[i].label;
                                objChildShikharPolicies.value = objRes.detail.policies[i].value;
                                objPolicies.Add(objChildShikharPolicies);
                            }
                            Models.Response.Insurance.ShikharDetail Detail = new Models.Response.Insurance.ShikharDetail();
                            Detail.policies = objPolicies;
                            result.Detail = Detail;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ShikharInsurance_GetPackages_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-shikhar-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-shikhar-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-shikhar")]
        public HttpResponseMessage GetServiceInsurance_ShikharInsuranceCommit(Req_Vendor_API_Commit_Insurance_Shikhar_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-shikhar" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_Shikhar_Requests result = new Res_Vendor_API_Commit_Insurance_Shikhar_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Shikhar_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Shikhar;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_ShikharInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ShikharInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_SHIKHARINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.CustomerName, user.Address, user.ContactNumber, user.Email, user.PolicyType, user.PolicyNumber, user.Branch, user.PolicyDescription, user.PolicyName, user.Amount, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            outobject.Address = objRes.extra_data.address;
                            outobject.CustomerName = objRes.extra_data.customer_name;
                            outobject.MobileNumber = objRes.extra_data.contact_number;
                            outobject.PolicyType = objRes.extra_data.policy_type;
                            outobject.PolicyNumber = objRes.extra_data.policy_number;
                            outobject.ProductName = objRes.extra_data.policy_name;

                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);

                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.Detail = objRes.detail;
                            result.Message = "Success";
                            result.TransactionUniqueId = TransactionID;
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.ExtraData.CustomerName = objRes.extra_data.customer_name;
                            result.ExtraData.Address = objRes.extra_data.address;
                            result.ExtraData.ContactNumber = objRes.extra_data.contact_number;
                            result.ExtraData.Email = objRes.extra_data.email;
                            result.ExtraData.PolicyType = objRes.extra_data.policy_type;
                            result.ExtraData.PolicyNumber = objRes.extra_data.policy_number;
                            result.ExtraData.PolicyDescription = objRes.extra_data.policy_description;
                            result.ExtraData.PolicyName = objRes.extra_data.policy_name;
                            result.ExtraData.Branch = objRes.extra_data.branch;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Shikhar_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);


                            try
                            {
                                int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Shikhar;
                                var WalletTransactionId = result.TransactionUniqueId = TransactionID;

                                string jsonData = VendorApi_CommonHelper.Generate_json_Insurance_Reciepts_Data(ServiceId.ToString(), "", WalletTransactionId, "Shikhar Insurance", user.PolicyType,
                                    user.CustomerName, user.PolicyNumber, user.Amount.ToString(), "", "", result.Message, "0", "0", "0", user.Amount.ToString());

                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), outobject.ServiceName.ToString(), "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Shikhar Insurance", outobject.Amount.ToString());
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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-shikhar completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-shikhar {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //Arhant Insurance
        /* [System.Web.Http.HttpPost]
         [System.Web.Http.Route("api/lookup-arhant-insurance")]
         public HttpResponseMessage GetInsurance_ArhantInsurance(Req_Vendor_API_ArhantInsurance_Detail user)
         {
             log.Info($"{System.DateTime.Now.ToString()} inside lookup-arhant-insurance" + Environment.NewLine);
             CommonResponse cres = new CommonResponse();
             string UserInput = getRawPostData().Result;
             Res_Vendor_API_ArhantInsurance_Detail_Requests result = new Res_Vendor_API_ArhantInsurance_Detail_Requests();
             var response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                         GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();

                         string msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(user.InsuranceSlug, user.RequestId, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                         if (msg.ToLower() == "success")
                         {
                             result.ReponseCode = objRes.status ? 1 : 0;
                             result.Amount = objRes.amount;
                             result.ClassName = objRes.class_name;
                             result.Insured = objRes.insured;
                             result.SumInsured = objRes.sum_insured;
                             result.ProformaNo = objRes.proforma_no;
                             result.TpPremium = objRes.tp_premium;
                             result.status = objRes.status;
                             result.Message = "Success";
                             result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                             response.StatusCode = HttpStatusCode.Accepted;
                             response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);


                             string SessionId = user.RequestId;
                             int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant;
                             var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                             VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Arhant Life Insurance", result.ClassName, result.Insured, result.ProformaNo, result.TpPremium, "", "", result.Message, result.SumInsured,
                                                          "0", "0", result.Amount);
                         }
                         else
                         {
                             cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                             response.StatusCode = HttpStatusCode.BadRequest;
                             response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                         }
                     }
                 }
                 log.Info($"{System.DateTime.Now.ToString()} lookup-arhant-insurance completed" + Environment.NewLine);
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
                 log.Error($"{System.DateTime.Now.ToString()} lookup-arhant-insurance {ex.ToString()} " + Environment.NewLine);
                 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
             }
         }

         [System.Web.Http.HttpPost]
         [System.Web.Http.Route("api/use-insurance-arhant")]
         public HttpResponseMessage GetServiceInsurance_ArhantInsuranceCommit(Req_Vendor_API_Commit_Insurance_Arhant_Requests user)
         {
             log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-arhant" + Environment.NewLine);
             CommonResponse cres = new CommonResponse();
             string UserInput = getRawPostData().Result;
             Res_Vendor_API_Commit_Insurance_Arhant_Requests result = new Res_Vendor_API_Commit_Insurance_Arhant_Requests();
             var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                         //string UserInput = getRawPostData().Result;
                         string CommonResult = "";
                         AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                         AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                         if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                         {
                             Int64 memId = Convert.ToInt64(user.MemberId);
                             int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant;
                             int Type = 0;
                             resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                         GetVendor_API_ServiceGroup_ArhantInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ArhantInsurance_Commit();
                         AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                         string authenticationToken = Request.Headers.Authorization.Parameter;

                         Common.authenticationToken = authenticationToken;
                         user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                         user.Reference = new CommonHelpers().GenerateUniqueId();
                         bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                         string msg = RepKhalti.RequestServiceGroup_COMMIT_ARHANTINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.RequestId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                         if (msg.ToLower() == "success")
                         {
                             AddInsuranceDetail outobject = new AddInsuranceDetail();
                             outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                             outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                             outobject.Error = objRes.error;
                             outobject.InsuranceId = Convert.ToInt64(objRes.id);
                             outobject.Message = objRes.message;
                             outobject.Status = objRes.status.ToString();
                             outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                             if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                             {
                                 outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                             }
                             outobject.MemberId = Convert.ToInt64(user.MemberId);
                             outobject.Amount = Convert.ToDecimal(user.Amount);
                             outobject.CustomerId = user.UniqueCustomerId;
                             outobject.Paymode = user.PaymentMode;
                             outobject.Reference = user.Reference;
                             //outobject.TransactionId = user.TransactionId;
                             outobject.IsActive = true;
                             outobject.IpAddress = Common.GetUserIP();
                             outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                             outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);

                             Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                             result.ReponseCode = objRes.status ? 1 : 0;
                             result.status = objRes.status;
                             result.Id = objRes.id;
                             result.WalletBalance = objRes.WalletBalance;
                             result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                             result.TransactionUniqueId = TransactionID;
                             result.Message = "Success";
                             result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                             response.StatusCode = HttpStatusCode.Accepted;
                             response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.OK, result);
                             ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                             string SessionId = user.RequestId;
                             int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant;
                             var WalletTransactionId = TransactionID;

                             try
                             {
                                 string jsonData = "";
                                 ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                 objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                 jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                 VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Arhant Life Insurance", user.Amount.ToString());

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
                 log.Info($"{System.DateTime.Now.ToString()} use-insurance-arhant completed" + Environment.NewLine);
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
                 log.Error($"{System.DateTime.Now.ToString()} use-insurance-arhant {ex.ToString()} " + Environment.NewLine);
                 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
             }
         }
 */

        //Arhant Insurance
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-sanima-general-insurance")]
        public HttpResponseMessage GetInsurance_SanimaGICInsurance(Req_Vendor_API_ArhantInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-sanima-general-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_ArhantInsurance_Detail_Requests result = new Res_Vendor_API_ArhantInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();

                        string msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(user.InsuranceSlug, user.RequestId, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.Amount = objRes.amount;
                            result.ClassName = objRes.class_name;
                            result.Insured = objRes.insured;
                            result.SumInsured = objRes.sum_insured;
                            result.ProformaNo = objRes.proforma_no;
                            result.TpPremium = objRes.tp_premium;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);


                            string SessionId = user.RequestId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Arhant Life Insurance", result.ClassName, result.Insured, result.ProformaNo, result.TpPremium, "", "", result.Message, result.SumInsured,
                                                         "0", "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-sanima-general-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-sanima-general-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-sanima-general")]
        public HttpResponseMessage GetServiceInsurance_ArhantInsuranceCommit(Req_Vendor_API_Commit_Insurance_Arhant_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-sanima-general" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_Arhant_Requests result = new Res_Vendor_API_Commit_Insurance_Arhant_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_ArhantInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ArhantInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_ARHANTINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.RequestId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);

                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.RequestId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "Arhant Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-sanima-general completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-sanima-general {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-nlg-insurance")]
        public HttpResponseMessage GetInsurance_NLGInsurance(Req_Vendor_API_ArhantInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-nlg-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_ArhantInsurance_Detail_Requests result = new Res_Vendor_API_ArhantInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();

                        string msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(user.InsuranceSlug, user.RequestId, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.Amount = objRes.amount;
                            result.ClassName = objRes.class_name;
                            result.Insured = objRes.insured;
                            result.SumInsured = objRes.sum_insured;
                            result.ProformaNo = objRes.proforma_no;
                            result.TpPremium = objRes.tp_premium;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);


                            string SessionId = user.RequestId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_NLG;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Arhant Life Insurance", result.ClassName, result.Insured, result.ProformaNo, result.TpPremium, "", "", result.Message, result.SumInsured,
                                                         "0", "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-nlg-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-arhant-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-nlg")]
        public HttpResponseMessage GetServiceInsurance_NLGInsuranceCommit(Req_Vendor_API_Commit_Insurance_Arhant_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-nlg" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_Arhant_Requests result = new Res_Vendor_API_Commit_Insurance_Arhant_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_NLG;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_ArhantInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ArhantInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_NLGINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId,user.MemberId, user.Amount, user.RequestId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests,user.ClassName);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);

                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.RequestId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_NLG;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "NLG Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-arhant completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-arhant {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-united-ajod-insurance")]
        public HttpResponseMessage GetInsurance_United_AjodInsurance(Req_Vendor_API_ArhantInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-united-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_ArhantInsurance_Detail_Requests result = new Res_Vendor_API_ArhantInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();

                        string msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(user.InsuranceSlug, user.RequestId, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.Amount = objRes.amount;
                            result.ClassName = objRes.class_name;
                            result.Insured = objRes.insured;
                            result.SumInsured = objRes.sum_insured;
                            result.ProformaNo = objRes.proforma_no;
                            result.TpPremium = objRes.tp_premium;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);


                            string SessionId = user.RequestId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_UNITEDAJOD;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Arhant Life Insurance", result.ClassName, result.Insured, result.ProformaNo, result.TpPremium, "", "", result.Message, result.SumInsured,
                                                         "0", "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-united-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-arhant-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-united-ajod")]
        public HttpResponseMessage GetServiceInsurance_United_AjodInsuranceCommit(Req_Vendor_API_Commit_Insurance_Arhant_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-united" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_Arhant_Requests result = new Res_Vendor_API_Commit_Insurance_Arhant_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_UNITEDAJOD;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_ArhantInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ArhantInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_UNITEDINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.RequestId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests,user.ClassName);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);

                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.RequestId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_NLG;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "NLG Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-united completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-united {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/lookup-siddhartha-insurance")]
        public HttpResponseMessage GetInsurance_SiddharthaInsurance(Req_Vendor_API_ArhantInsurance_Detail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-siddhartha-insurance" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_ArhantInsurance_Detail_Requests result = new Res_Vendor_API_ArhantInsurance_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        GetVendor_API_ArhantInsurance_Detail objRes = new GetVendor_API_ArhantInsurance_Detail();

                        string msg = RepKhalti.RequestServiceGroup_ArhantInsurance_Detail(user.InsuranceSlug, user.RequestId, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.Amount = objRes.amount;
                            result.ClassName = objRes.class_name;
                            result.Insured = objRes.insured;
                            result.SumInsured = objRes.sum_insured;
                            result.ProformaNo = objRes.proforma_no;
                            result.TpPremium = objRes.tp_premium;
                            result.status = objRes.status;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_ArhantInsurance_Detail_Requests>(System.Net.HttpStatusCode.OK, result);


                            string SessionId = user.RequestId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_SIDDHARTHA;
                            var WalletTransactionId = new CommonHelpers().GenerateUniqueId();
                            VendorApi_CommonHelper.Generate_Insurance_Reciepts_Data(ServiceId.ToString(), SessionId, WalletTransactionId, "Siddhartha Insurance", result.ClassName, result.Insured, result.ProformaNo, result.TpPremium, "", "", result.Message, result.SumInsured,
                                                         "0", "0", result.Amount);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-nlg-insurance completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-arhant-insurance {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/use-insurance-siddhartha")]
        public HttpResponseMessage GetServiceInsurance_SiddharthaInsuranceCommit(Req_Vendor_API_Commit_Insurance_Arhant_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-insurance-siddhartha" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            string UserInput = getRawPostData().Result;
            Res_Vendor_API_Commit_Insurance_Arhant_Requests result = new Res_Vendor_API_Commit_Insurance_Arhant_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_SIDDHARTHA;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, false, user.Amount, true, user.Mpin);
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
                        GetVendor_API_ServiceGroup_ArhantInsurance_Commit objRes = new GetVendor_API_ServiceGroup_ArhantInsurance_Commit();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();

                        string authenticationToken = Request.Headers.Authorization.Parameter;

                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestServiceGroup_COMMIT_SIDDHARTHAINSURANCE(resuserdetails, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.MemberId, user.Amount, user.RequestId, user.InsuranceSlug, user.Reference, UserInput, authenticationToken, user.DeviceCode, user.PlatForm, ref objRes, ref objVendor_API_Requests,user.ClassName);
                        if (msg.ToLower() == "success")
                        {
                            AddInsuranceDetail outobject = new AddInsuranceDetail();
                            outobject.CreditsAvailable = Convert.ToDecimal(objRes.credits_available);
                            outobject.CreditsConsumed = Convert.ToDecimal(objRes.credits_consumed);
                            outobject.Error = objRes.error;
                            outobject.InsuranceId = Convert.ToInt64(objRes.id);
                            outobject.Message = objRes.message;
                            outobject.Status = objRes.status.ToString();
                            outobject.TransactionUniqueId = objRes.UniqueTransactionId;
                            if (!string.IsNullOrEmpty(objRes.WalletBalance) && objRes.WalletBalance != "0")
                            {
                                outobject.WalletBalance = Convert.ToDecimal(objRes.WalletBalance);
                            }
                            outobject.MemberId = Convert.ToInt64(user.MemberId);
                            outobject.Amount = Convert.ToDecimal(user.Amount);
                            outobject.CustomerId = user.UniqueCustomerId;
                            outobject.Paymode = user.PaymentMode;
                            outobject.Reference = user.Reference;
                            //outobject.TransactionId = user.TransactionId;
                            outobject.IsActive = true;
                            outobject.IpAddress = Common.GetUserIP();
                            outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);

                            Int64 Id = RepCRUD<AddInsuranceDetail, GetInsuranceDetail>.Insert(outobject, "insurancedetail");

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = objRes.status;
                            result.Id = objRes.id;
                            result.WalletBalance = objRes.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.TransactionUniqueId = TransactionID;
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Commit_Insurance_Arhant_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                            string SessionId = user.RequestId;
                            int ServiceId = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_SIDDHARTHA;
                            var WalletTransactionId = TransactionID;

                            try
                            {
                                string jsonData = "";
                                ReceiptsVendorResponse objreceiptsVendorResponse = null;
                                objreceiptsVendorResponse = VendorApi_CommonHelper.getRecordRecieptsVendorResponse(WalletTransactionId, ServiceId.ToString(), SessionId);
                                jsonData = objreceiptsVendorResponse.ReqJSONContent;
                                VendorApi_CommonHelper.saveReceipt(ServiceId.ToString(), "", "", WalletTransactionId, jsonData, resuserdetails.ContactNumber, objVendor_API_Requests.MemberName, "Insurance Payment", "NLG Life Insurance", user.Amount.ToString());

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
                log.Info($"{System.DateTime.Now.ToString()} use-insurance-siddhartha completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-insurance-arhant {ex.ToString()} " + Environment.NewLine);
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



