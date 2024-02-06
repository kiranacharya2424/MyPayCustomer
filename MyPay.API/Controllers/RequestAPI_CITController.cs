using log4net;
using MyPay.API.Models;
using MyPay.API.Models.CIT;
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
    public class RequestAPI_CITController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_CITController));
        string ApiResponse = string.Empty;


        [HttpPost]
        [Route("api/get-cit-category")]
        public HttpResponseMessage GetServiceGroupCITCategory(Req_Vendor_API_CIT_Category_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group CIT Category" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_CIT_Category_Requests result = new Res_Vendor_API_CIT_Category_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_CIT_Category_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_CIT_Government_Payments;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string ApiResponse = string.Empty;


                        GetVendor_API_CIT_Category objRes = new GetVendor_API_CIT_Category();
                        string msg = RepGovernmentNCHL.RequestCIT_Categories(user.Category, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<Models.CIT.CIT_Category_Detail> objCategory = new List<Models.CIT.CIT_Category_Detail>();
                            for (int i = 0; i < objRes.data.Count; i++)
                            {

                                Models.CIT.CIT_Category_Detail category = new Models.CIT.CIT_Category_Detail();
                                category.category = objRes.data[i].category;
                                category.code = objRes.data[i].code;
                                category.labelText = objRes.data[i].labelText;
                                category.logoUrl = objRes.data[i].logoUrl;

                                objCategory.Add(category);
                            }
                            result.data = objCategory;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Message = "success";
                            result.success = true;
                            response = Request.CreateResponse<Res_Vendor_API_CIT_Category_Requests>(System.Net.HttpStatusCode.Accepted, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
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
                log.Info($"{System.DateTime.Now.ToString()}  Get Service Group CIT Category completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()}  Get Service Group CIT Category {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/get-journey-details")]
        public HttpResponseMessage GetServiceGroupCITJourneyDetails(Req_Vendor_API_CIT_Journey_Details_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group CIT journey details" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_CIT_Journey_Details_Requests result = new Res_Vendor_API_CIT_Journey_Details_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_CIT_Journey_Details_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_CIT_Government_Payments;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string ApiResponse = string.Empty;


                        GetVendor_API_CIT_Journey_Details objRes = new GetVendor_API_CIT_Journey_Details();
                        string msg = RepGovernmentNCHL.RequestCIT_JourneyDetails(user.AppCode, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<CITDatum> objJourneyDetails = new List<CITDatum>();
                            for (int i = 0; i < objRes.Data.Count; i++)
                            {
                                CITDatum journey = new CITDatum();
                                journey.ProcessSeq = Convert.ToInt32(objRes.TotalProcessSeq);
                                journey.RequiredFields = new List<CITRequiredField>();
                                journey.ResponseFieldMapping = new List<CITResponseFieldMapping>();
                                for (int j = 0; j < objRes.Data[i].RequiredFields.Count; j++)
                                {


                                    CITRequiredField RequiredFields = new CITRequiredField();
                                    RequiredFields.FieldName = objRes.Data[i].RequiredFields[j].FieldName;
                                    RequiredFields.FieldLabel = objRes.Data[i].RequiredFields[j].FieldLabel;
                                    RequiredFields.FieldType = objRes.Data[i].RequiredFields[j].FieldType;
                                    RequiredFields.IsRequired = objRes.Data[i].RequiredFields[j].IsRequired;
                                    RequiredFields.InputFormat = objRes.Data[i].RequiredFields[j].InputFormat;
                                    RequiredFields.AddnUrl = objRes.Data[i].RequiredFields[j].AddnUrl;
                                    RequiredFields.DataType = new CITDataType();
                                    RequiredFields.DataType.length = objRes.Data[i].RequiredFields[j].DataType.length;
                                    RequiredFields.DataType.minLength = objRes.Data[i].RequiredFields[j].DataType.minLength;
                                    RequiredFields.DataType.type = objRes.Data[i].RequiredFields[j].DataType.type;
                                    journey.RequiredFields.Add(RequiredFields);
                                }
                                for (int k = 0; k < objRes.Data[i].ResponseFieldMapping.Count; k++)
                                {
                                    CITResponseFieldMapping ResFields = new CITResponseFieldMapping();
                                    ResFields.FieldLabel = objRes.Data[i].ResponseFieldMapping[k].FieldLabel;
                                    ResFields.MapField = objRes.Data[i].ResponseFieldMapping[k].MapField;
                                    ResFields.FieldName = objRes.Data[i].ResponseFieldMapping[k].FieldName;
                                    journey.ResponseFieldMapping.Add(ResFields);
                                }

                                objJourneyDetails.Add(journey);
                            }
                            result.Data = objJourneyDetails;
                            result.TotalProcessSeq = objRes.TotalProcessSeq;
                            result.AppGroup = objRes.AppGroup;
                            result.AppId = objRes.AppId;
                            result.status = true;
                            result.Message = "success";
                            response = Request.CreateResponse<Res_Vendor_API_CIT_Journey_Details_Requests>(System.Net.HttpStatusCode.Accepted, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
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
                log.Info($"{System.DateTime.Now.ToString()}  Get Service Group CIT journey details completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group CIT journey details {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/get-loan-type")]
        public HttpResponseMessage GetServiceGroupCITLoanType(Req_Vendor_API_CIT_Loan_Type_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group CIT Loan Type" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_CIT_Loan_Type_Requests result = new Res_Vendor_API_CIT_Loan_Type_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_CIT_Loan_Type_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_CIT_Government_Payments;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string ApiResponse = string.Empty;


                        GetVendor_API_CIT_Loan_Type objRes = new GetVendor_API_CIT_Loan_Type();
                        string msg = RepGovernmentNCHL.RequestCIT_LoanType(user.AppGroup, user.FieldName, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<CITLoanType> objCITLoanType = new List<CITLoanType>();
                            for (int i = 0; i < objRes.Data.Count; i++)
                            {
                                CITLoanType CITLoanType = new CITLoanType();
                                CITLoanType.id = objRes.Data[i].id;
                                CITLoanType.option = objRes.Data[i].option;
                                CITLoanType.value = objRes.Data[i].value;
                                CITLoanType.code = objRes.Data[i].code;

                                objCITLoanType.Add(CITLoanType);
                            }
                            result.Data = objCITLoanType;
                            result.status = true;
                            result.Message = "success";
                            response = Request.CreateResponse<Res_Vendor_API_CIT_Loan_Type_Requests>(System.Net.HttpStatusCode.Accepted, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
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
                log.Info($"{System.DateTime.Now.ToString()}  Get Service Group CIT Loan Type completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group CIT Loan Type {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/cit-billpayment")]
        public HttpResponseMessage GetServiceGroupCITBillPayment(Req_Vendor_API_CIT_Bill_Payment_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group CIT Bill Payment" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_CIT_Bill_Payment_Requests result = new Res_Vendor_API_CIT_Bill_Payment_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_CIT_Bill_Payment_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_CIT_Government_Payments;
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
                        string ApiResponse = string.Empty;

                        GetVendor_API_CIT_Bill_Payment objRes = new GetVendor_API_CIT_Bill_Payment();

                        string msg = RepGovernmentNCHL.RequestCIT_BillPayment(user.CITBatchDetail.batchId, user.CITBatchDetail.batchAmount, user.CITBatchDetail.batchCount.ToString(), user.CITBatchDetail.batchCrncy, user.CITBatchDetail.categoryPurpose,
                        user.CITBatchDetail.debtorAgent, user.CITBatchDetail.debtorBranch, user.CITBatchDetail.debtorName, user.CITBatchDetail.debtorAccount, user.CITBatchDetail.debtorIdType, user.CITBatchDetail.debtorIdValue, user.CITBatchDetail.debtorAddress, user.CITBatchDetail.debtorPhone, user.CITBatchDetail.debtorMobile, user.CITBatchDetail.debtorEmail, user.CITTransactionDetail.instructionId, user.CITTransactionDetail.endToEndId,
                        user.CITTransactionDetail.amount, user.CITTransactionDetail.appId, user.CITTransactionDetail.refId, user.CITTransactionDetail.remarks, user.CITTransactionDetail.freeCode1, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<CITLoanType> objCITLoanType = new List<CITLoanType>();
                            //for (int i = 0; i < objRes.Data.Count; i++)
                            //{
                            //    CITLoanType CITLoanType = new CITLoanType();
                            //    CITLoanType.id = objRes.Data[i].id;
                            //    CITLoanType.option = objRes.Data[i].option;
                            //    CITLoanType.value = objRes.Data[i].value;
                            //    CITLoanType.code = objRes.Data[i].code;

                            //    objCITLoanType.Add(CITLoanType);
                            //}
                            // result.Data = objCITLoanType;
                            result.status = true;
                            result.Message = "success";
                            response = Request.CreateResponse<Res_Vendor_API_CIT_Bill_Payment_Requests>(System.Net.HttpStatusCode.Accepted, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
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
                log.Info($"{System.DateTime.Now.ToString()}  Get Service Group CIT  Bill Payment completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group CIT Bill Payment {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/cit-Confirm-billpayment")]
        public HttpResponseMessage GetServiceGroupCITConfirmBillPayment(Req_Vendor_API_CIT_Bill_Payment_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group CIT Confirm Bill Payment" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_CIT_Bill_Payment_Requests result = new Res_Vendor_API_CIT_Bill_Payment_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_CIT_Bill_Payment_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_CIT_Government_Payments;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string ApiResponse = string.Empty;

                        GetVendor_API_CIT_Bill_Payment objRes = new GetVendor_API_CIT_Bill_Payment();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Reference = new CommonHelpers().GenerateUniqueId();

                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepGovernmentNCHL.RequestCIT_ConfirmBillPayment(user.CITBatchDetail.batchId, user.CITBatchDetail.batchAmount, user.CITBatchDetail.batchCount.ToString(), user.CITBatchDetail.batchCrncy, user.CITBatchDetail.categoryPurpose,
             user.CITBatchDetail.debtorAgent, user.CITBatchDetail.debtorBranch, user.CITBatchDetail.debtorName, user.CITBatchDetail.debtorAccount, user.CITBatchDetail.debtorIdType, user.CITBatchDetail.debtorIdValue, user.CITBatchDetail.debtorAddress, user.CITBatchDetail.debtorPhone, user.CITBatchDetail.debtorMobile, user.CITBatchDetail.debtorEmail, user.CITTransactionDetail.instructionId, user.CITTransactionDetail.endToEndId,
             user.CITTransactionDetail.amount, user.CITTransactionDetail.appId, user.CITTransactionDetail.refId, user.CITTransactionDetail.remarks, user.CITTransactionDetail.freeCode1, authenticationToken, UserInput, ref objRes, ref objVendor_API_Requests, ref IsCouponUnlocked, ref TransactionID,
             resGetCouponsScratched, resuserdetails, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.Reference, user.Version, user.DeviceCode,
                            user.PlatForm, user.MemberId);
                        if (msg.ToLower() == "success")
                        {
                            List<CITLoanType> objCITLoanType = new List<CITLoanType>();
                            //for (int i = 0; i < objRes.Data.Count; i++)
                            //{
                            //    CITLoanType CITLoanType = new CITLoanType();
                            //    CITLoanType.id = objRes.Data[i].id;
                            //    CITLoanType.option = objRes.Data[i].option;
                            //    CITLoanType.value = objRes.Data[i].value;
                            //    CITLoanType.code = objRes.Data[i].code;

                            //    objCITLoanType.Add(CITLoanType);
                            //}
                            // result.Data = objCITLoanType;
                            result.status = true;
                            result.Message = "success";
                            response = Request.CreateResponse<Res_Vendor_API_CIT_Bill_Payment_Requests>(System.Net.HttpStatusCode.Accepted, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
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
                log.Info($"{System.DateTime.Now.ToString()}  Get Service Group CIT Confirm  Bill Payment completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group CIT Confirm Bill Payment {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/get-query-transaction")]
        public HttpResponseMessage GetServiceGroupCITQueryTransaction(Req_Vendor_API_CIT_Query_Transaction_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group CIT Query Transaction" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_CIT_Query_Transaction_Requests result = new Res_Vendor_API_CIT_Query_Transaction_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_CIT_Query_Transaction_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_CIT_Government_Payments;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string ApiResponse = string.Empty;


                        GetVendor_API_CIT_QueryTransaction objRes = new GetVendor_API_CIT_QueryTransaction();
                        string msg = RepGovernmentNCHL.RequestCIT_QueryTransaction(user.InstructionId, user.BatchId, user.TransactionId, user.RealTime, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            List<CITLoanType> objCITLoanType = new List<CITLoanType>();
                            //for (int i = 0; i < objRes.Data.Count; i++)
                            //{
                            //    CITLoanType CITLoanType = new CITLoanType();
                            //    CITLoanType.id = objRes.Data[i].id;
                            //    CITLoanType.option = objRes.Data[i].option;
                            //    CITLoanType.value = objRes.Data[i].value;
                            //    CITLoanType.code = objRes.Data[i].code;

                            //    objCITLoanType.Add(CITLoanType);
                            //}
                            // result.Data = objCITLoanType;
                            result.status = true;
                            result.Message = "success";
                            response = Request.CreateResponse<Res_Vendor_API_CIT_Query_Transaction_Requests>(System.Net.HttpStatusCode.Accepted, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
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
                log.Info($"{System.DateTime.Now.ToString()}  Get Service Group CIT Query Transaction completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group CIT Query Transaction {ex.ToString()} " + Environment.NewLine);
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