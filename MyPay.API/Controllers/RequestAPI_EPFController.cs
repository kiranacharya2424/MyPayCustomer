using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Events;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class RequestAPI_EPFController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_EPFController));
        string ApiResponse = string.Empty;


        [HttpPost]
        [Route("api/get-deposit-type")]
        public HttpResponseMessage GetServiceGroupDepositType(Req_Vendor_API_Epf_Deposit_Type_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group Deposit Type" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Epf_Deposit_Type_Requests result = new Res_Vendor_API_Epf_Deposit_Type_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Epf_Deposit_Type_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_EPF_Government_Payments;
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


                        GetVendor_API_Epf_Deposit_Type objRes = new GetVendor_API_Epf_Deposit_Type();
                        string msg = RepGovernmentNCHL.RequestServiceGroup_EPF_DepositType(user.CipsBatch.BatchId, user.CipsBatch.BatchAmount, user.CipsBatch.BatchCount, user.CipsBatch.BatchCrncy, user.CipsBatch.CategoryPurpose, user.CipsBatch.DebtorAgent, user.CipsBatch.DebtorBranch, user.CipsBatch.DebtorName, user.CipsBatch.DebtorAccount, user.CipsBatch.DebtorIdType, user.CipsBatch.DebtorIdValue, user.CipsBatch.DebtorAddress, user.CipsBatch.DebtorPhone, user.CipsBatch.DebtorMobile, user.CipsBatch.DebtorEmail,
                        user.CipsTransaction.InstructionId, user.CipsTransaction.EndToEndId, user.CipsTransaction.Amount, user.CipsTransaction.AppId, user.CipsTransaction.RefId, user.CipsTransaction.FreeText1, user.CipsTransaction.Addenda3, ref objRes);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = true;
                            result.Message = "success";
                            response = Request.CreateResponse<Res_Vendor_API_Epf_Deposit_Type_Requests>(System.Net.HttpStatusCode.Accepted, result);
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
                log.Info($"{System.DateTime.Now.ToString()} Get Service Group Deposit Type completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Deposit Type {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/get-contributor-detail")]
        public HttpResponseMessage GetServiceGroupContributorDetail(Req_Vendor_API_Epf_Contributor_Detail_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside Get Service Group Contributor Detail" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Epf_Contributor_Detail_Requests result = new Res_Vendor_API_Epf_Contributor_Detail_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Epf_Contributor_Detail_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.khalti_EPF_Government_Payments;
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


                        GetVendor_API_Epf_Contributor_Detail objRes = new GetVendor_API_Epf_Contributor_Detail();
                        string msg = RepGovernmentNCHL.RequestServiceGroup_EPF_ContributorDetail(user.CipsBatch.BatchCrncy, user.CipsBatch.DebtorName, user.CipsBatch.DebtorAddress, ref objRes);
                        if (msg.ToLower() == "success")
                        {

                            result.ReponseCode = objRes.status ? 1 : 0;
                            result.status = true;
                            result.Message = "success";
                            response = Request.CreateResponse<Res_Vendor_API_Epf_Contributor_Detail_Requests>(System.Net.HttpStatusCode.Accepted, result);
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
                log.Info($"{System.DateTime.Now.ToString()} Get Service Group Contributor Detail completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Contributor Detail {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/epf-commit")]
        public HttpResponseMessage ServiceGroupEpfCommit(Req_Vendor_API_Epf_Commit_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside  Epf Commit" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Epf_Commit_Requests result = new Res_Vendor_API_Epf_Commit_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Epf_Commit_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_EPF_Government_Payments;
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
                        else
                        {
                            string KhaltiAPIURL = "billpayment/lodgebillpay.do/";
                            string ApiResponse = string.Empty;

                            string authenticationToken = Request.Headers.Authorization.Parameter;
                            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                            string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(KhaltiAPIURL, user.ReferenceNo, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, "", authenticationToken, UserInput, user.DeviceCode, user.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.khalti).ToString());



                            GetVendor_API_Epf_Commit objCommitRes = new GetVendor_API_Epf_Commit();
                            string msgMerchantPayment = RepGovernmentNCHL.RequestServiceGroup_EPF_Commit(user.CipsBatch.BatchCrncy, user.CipsBatch.DebtorName, user.CipsBatch.DebtorAddress, ref objCommitRes);
                            if (msgMerchantPayment.ToLower() == "success")
                            {


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
                log.Info($"{System.DateTime.Now.ToString()}  Epf Commit completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Epf Commit {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/epf-confirm-payment")]
        public HttpResponseMessage ServiceGroupEpfConfirmPayment(Req_Vendor_API_Epf_Commit_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside  Epf Confirm Payment" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Epf_Commit_Requests result = new Res_Vendor_API_Epf_Commit_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Epf_Commit_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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

                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_EPF_Government_Payments;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", true, user.Mpin, "", false, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        else
                        {

                            GetVendor_API_Epf_Commit objRes = new GetVendor_API_Epf_Commit();
                            string msgMerchantPayment = RepGovernmentNCHL.RequestServiceGroup_EPF_ConfirmPayment(user.CipsBatch.BatchCrncy, user.CipsBatch.DebtorName, user.CipsBatch.DebtorAddress, ref objRes);
                            if (msgMerchantPayment.ToLower() == "success")
                            {


                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(msgMerchantPayment);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
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
                log.Info($"{System.DateTime.Now.ToString()}  Epf Confirm Payment" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Epf Confirm Payment {ex.ToString()} " + Environment.NewLine);
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